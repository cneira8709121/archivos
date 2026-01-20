using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.Radicacion;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.OracleClient;
using System.IO;
using System.Configuration;

namespace Ruv.Data.Radicacion
{
    public class IntegradorGDData
    {
        FachadaGD.FachadaGDClient client;
        public IntegradorGDData()
        {
            client = new FachadaGD.FachadaGDClient();
        }

        public string CedulaGestor
        {
            get
            {
                return ConfigurationManager.AppSettings["CedulaGestor"].ToString();
            }
        }

        public string Dependencia
        {
            get
            {
                return ConfigurationManager.AppSettings["Dependencia"].ToString();
            }
        }

        /// <summary>
        /// Obtiene los datos de las credenciales para consumo de la fachada del Gestor Documental
        /// </summary>
        /// <returns>Entidad de Tipo Credencial con la información de acceso a la fachada</returns>
        public FachadaGD.Credencial ObtenerCredencial()
        {
            return new FachadaGD.Credencial() { UsuarioWsFachada = ConfigurationManager.AppSettings["usuarioServicioGestorDocumental"], ContrasenaWsFachada = ConfigurationManager.AppSettings["contrasenaServicioGestorDocumental"], IdAplicacion = 10 };
        }

        /// <summary>
        /// Radicar de Entrada al gestor documental
        /// </summary>
        /// <param name="entidad">Entidad de radicación de RUV</param>
        /// <param name="tra">Transacción</param>
        /// <param name="cError">Error en caso de fallo</param>
        public void RadicarEntrada(clsRadicacionIntegradorGD entidad, DbTransaction tra, ref string cError)
        {
            try
            {
                FachadaGD.Credencial credencial = ObtenerCredencial();
                if (entidad.CEDULA == null)
                {
                    entidad.CEDULA = "No registrar";
                }
                var result = client.radicarDeEntrada(credencial, entidad.NUM_DECLARACION, 1, entidad.NOMBRE, entidad.PRIMER_APELIIDO, entidad.SEGUNDO_APELLIDO,
                    entidad.CEDULA, entidad.DIRECCION, entidad.TELEFONO, "UARIV_SVR", entidad.PAIS, entidad.DEPARTAMENTO, entidad.MUNICIPIO,
                    entidad.CORREO, Dependencia, entidad.DESCRIPCION_ANEXO, string.Format("RADICACION DECLARACION {0}", entidad.NUM_DECLARACION), 13, entidad.NOMBRE_ARCHIVO, entidad.ARCHIVO, 1,
                    entidad.SEGUNDO_NOMBRE, entidad.ID_USUARIO.ToString(), string.Empty, "RUV", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0,"");

                //client.avanzarTareaGestor(credencial, new FachadaGD.EntPeticionAvanzarTareaRequest() { codigoDependencia = Dependencia, numeroCedulaGestor = CedulaGestor, numeroRadicadoEntrada = result.code });

                GuardarLogIntegrador(entidad.NUM_DECLARACION, result.idSolicitud, result.code, string.Empty, 0, entidad.ID_USUARIO, tra, ref cError);
            }
            catch (Exception ex)
            {
                
                cError = ex.Message;
            }
        }

        public bool ActualizarLogIntegrador(string numDeclaracion, long? idIntegradorGD, string codIntegradorGD, string numExpedienteGD, int? idExpedienteGD, int idUsuario, DbTransaction tra, ref string cError)
        {
            bool result = false;
            using (Dao d = new Dao())
            {
                d.RefreshParameters();

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                    OracleType = System.Data.OracleClient.OracleType.VarChar,
                    Value = numDeclaracion,
                    Direction = ParameterDirection.Input
                });
                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdIntegradorGD,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idIntegradorGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.CodIntegradorGD,
                    OracleType = System.Data.OracleClient.OracleType.NVarChar,
                    Value = codIntegradorGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idUsuario,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ExpedienteGD,
                    OracleType = System.Data.OracleClient.OracleType.NVarChar,
                    Value = numExpedienteGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdExpedienteGD,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idExpedienteGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                    OracleType = System.Data.OracleClient.OracleType.Int16,
                    Direction = ParameterDirection.Output
                });

                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizarLogIntegrador, tra, ref cError);

                    if (!string.IsNullOrEmpty(cError)) result = false;

                    DbParameter dbParameter = d.LstParameter.FirstOrDefault(x => x.ParameterName == resx::Parametros.ResultadoConsulta);
                    result = dbParameter == null ? false : Convert.ToBoolean(dbParameter.Value);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    result = false;
                }

                if (!(cError == null || cError == string.Empty)) result = false ;
                return result;
            }
        }

        /// <summary>
        /// Guardar log del integrador y RUV
        /// </summary>
        /// <param name="numDeclaracion">Numero de la declaración RUV</param>
        /// <param name="idIntegradorGD">Identificador del del Gestor Documental</param>
        /// <param name="codIntegradorGD">Codigo del Integrador del Gestor Documental</param>
        /// <param name="idUsuario">Identificador del Usuario que realiza la Finalización de la declaración</param>
        /// <param name="numExpedienteGD">Identificador del expediente del gestor documental</param>
        /// <param name="tra">Transacción</param>
        /// <param name="cError">Error en caso que retorne</param>
        /// <returns>Verdadero o falso de exito de la transacción</returns>
        public bool GuardarLogIntegrador(string numDeclaracion, long? idIntegradorGD, string codIntegradorGD, string numExpedienteGD, int? idExpedienteGD, int idUsuario, DbTransaction tra, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                    OracleType = System.Data.OracleClient.OracleType.VarChar,
                    Value = numDeclaracion,
                    Direction = ParameterDirection.Input
                });
                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdIntegradorGD,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idIntegradorGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.CodIntegradorGD,
                    OracleType = System.Data.OracleClient.OracleType.NVarChar,
                    Value = codIntegradorGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idUsuario,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ExpedienteGD,
                    OracleType = System.Data.OracleClient.OracleType.NVarChar,
                    Value = numExpedienteGD,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdExpedienteGD,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = idExpedienteGD,
                    Direction = ParameterDirection.Input
                });


                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.InsertarIntregacionGD, tra, ref cError);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return false;
                }

                if (!(cError == null || cError == string.Empty)) return false;
                return true;
            }
        }
    }

}
