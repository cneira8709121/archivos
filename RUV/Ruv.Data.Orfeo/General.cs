using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Ruv.Business.DTO.Orfeo;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using code = Ruv.Data.Orfeo.ServiceImplementation.OrfeoCode;
using file = Ruv.Data.Orfeo.ServiceImplementation.OrfeoFile;
using System.Reflection;

namespace Ruv.Data.Orfeo
{
    public class General : Services.IManageOrfeo {

        #region Code Service (http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php)

        public Secuencia InsertaDignatario(Dignatario dig, ref string cError) {
            var client = new code::OrfeoCodeReference();
            try {
                var result = client.insert_dignatario2 (dig.NTipoRadicado.ToString()
                                                      , dig.CNombreDeclarante
                                                      , dig.CPrimerApellido
                                                      , dig.CSegundoApellido
                                                      , dig.CCedula
                                                      , dig.CDireccion
                                                      , dig.CTelefono
                                                      , dig.CEntidad
                                                      , dig.NIdDepartamento.ToString()
                                                      , dig.NIdMunicipio.ToString()
                                                      , dig.CEmail);
                if (result == null) {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaDignatario::: " + "Secuencia nula");
                    cError = "Secuencia nula";
                    return null;
                }
                return new Secuencia { SecuenciaMensaje = result.secuencia, Estado = result.estado };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaDignatario::: ", e);
                cError = e.Message;
                return null;
            }
        }

        public Secuencia InsertaRadicado(Radicado rad, ref string cError) {
            
            var client = new code::OrfeoCodeReference();
            try {
                var result = client.insert_radicado3 (rad.NTipoRadicado
                                                    , rad.NDepartamentoRadicado
                                                    , rad.NDepartamentoDestino
                                                    , rad.NCodigoUsuario
                                                    , rad.NCodigoUsuarioDestino
                                                    , rad.DFechaOficial.ToString("dd/MM/yyyy")
                                                    , rad.CRadicadoEntrada
                                                    , rad.CDescanex
                                                    , rad.CAsunto
                                                    , rad.CNRoofic
                                                    , rad.CRutaRadicado
                                                    , rad.CExpe
                                                    , int.Parse(rad.CRadicado));
                if (result == null) {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaRadicado::: " + "Secuencia nula");
                    cError = "Secuencia nula";
                    return null;
                }
                return new Secuencia { SecuenciaMensaje = result.secuencia, Estado = result.estado };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaRadicado::: ", e);
                cError = e.Message;
                return null;
            }
        }

        public Secuencia InsertaDireccion(Direccion dir, ref string cError) {

            var client = new code::OrfeoCodeReference();
            try {
                var result = client.insert_direccion (dir.tipdesrem.ToString()
                                                    , dir.coddir
                                                    , dir.numradicado
                                                    , dir.direccion
                                                    , dir.dirtelefono
                                                    , dir.dirnombre
                                                    , dir.coddpto.ToString()
                                                    , dir.codmpio.ToString());
                if (result == null) {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaDireccion::: " + "Secuencia nula");
                    cError = "Secuencia nula";
                    return null;
                }
                return new Secuencia { SecuenciaMensaje = result.secuencia, Estado = result.estado };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaDireccion::: ", e);
                cError = e.Message;
                return null;
            }
        }

        public Secuencia InsertaEvento(Evento evt, ref string cError) {
            
            var client = new code::OrfeoCodeReference();
            try {
                var result = client.insert_eventhist (evt.tiporad
                                                    , evt.numradicado
                                                    , evt.deprad
                                                    , evt.codiusu
                                                    , evt.ttrcodi);
                if (result == null) {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaEvento::: " + "Secuencia nula");
                    cError = "Secuencia nula";
                    return null;
                }
                return new Secuencia { SecuenciaMensaje = result.secuencia, Estado = result.estado };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaEvento::: ", e);
                cError = e.Message;
                return null;
            }
        }

        public Secuencia VincularArchivoCargado(string rutaArchivo, string numeroRadicado)
        {
            var client = new code::OrfeoCodeReference();
            try {
                var result = client.insert_radpath(rutaArchivo, numeroRadicado);
                if (result == null) {
                    //throw new TargetInvocationException("VincularArchivoCargado::No se pudo vincular el archivo cargado con su código - el resultado es vacio", null);
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::VincularArchivoCargado::: " + "Result = null");
                }
                return new Secuencia { SecuenciaMensaje = result.secuencia, Estado = result.estado };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::VincularArchivoCargado::: ", e);
                throw new TargetInvocationException("VincularArchivoCargado::No se pudo vincular el archivo cargado con su código", e);
            }
        }

        #endregion

        #region File Service (http://orfeo.unidadvictimas.gov.co/webservice/upload.php)

        public Resultado ObtenerNombreAnexo(string numeroRadicado, string nombreArchivo) {
            var client = new file::OrfeoFileReference();
            try {
                var result = client.nombreanexo(numeroRadicado, nombreArchivo);
                if (result == null) {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerNombreAnexo::: result = null");
                    throw new TargetInvocationException("ObtenerNombreAnexo::No se pudo obtener el nombre para el archivo remoto - el resultado es vacio", null);
                }
                return new Resultado { Mensaje = result.mensaje, Error = result.error };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerNombreAnexo::: ", e);
                throw new TargetInvocationException("ObtenerNombreAnexo::No se pudo obtener el nombre para el archivo remoto", e);
            }
        }

        public string CargarArchivoRemoto(string numeroRadicado, string base64EncodedBytes, string nombreArchivo) {
            var client = new file::OrfeoFileReference();
            try {
                return client.publicar(numeroRadicado, base64EncodedBytes, nombreArchivo);
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::CargarArchivoRemoto:::", e);
                throw new TargetInvocationException("CargarArchivoRemoto::No se pudo cargar el archivo al servidor remoto", e);
            }
        }

        public Resultado RegistrarEventoCargaArchivo(string rutaArchivo, int numeroPaginas, string numeroRadicado, string usuarioDigitalizador) {
            var client = new file::OrfeoFileReference();
            try {
                var result = client.registrar(rutaArchivo, numeroPaginas, numeroRadicado, usuarioDigitalizador);
                if (result == null) {
                    //throw new TargetInvocationException("RegistrarEventoCargaArchivo::No se pudo registrar el evento - el resultado es vacio", null);
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::RegistrarEventoCargaArchivo::: result = null");
                }
                return new Resultado { Mensaje = result.mensaje, Error = result.error };
            }
            catch (Exception e) {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::RegistrarEventoCargaArchivo:::", e);
                throw new TargetInvocationException("RegistrarEventoCargaArchivo::No se pudo registrar el evento", e);
            }
        }

        #endregion

        #region Data Methods

        public bool RelacionarOrfeoValoracion(string cOrfeo, int nValoracion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdValoracion, OracleType = OracleType.Number, Value = nValoracion, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.PICodigoOrfeo, OracleType = OracleType.VarChar, Value = cOrfeo, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            bool bExito = false;
            try
            {
                bExito = d.ExecuteNonQuery(resx::Procedimientos.RelacionarCodigoOrfeoValoracion, tra, ref cError);
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::RelacionarOrfeoValoracion:::", ex);
                cError = ex.Message;
                return false;
            }

            if (!(bExito && string.IsNullOrEmpty(cError))) return false;
            return true;
        }

        public string ObtenerCodigoOrfeoPorIdVal(int idValoracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdValoracion, OracleType = OracleType.Number, Value = idValoracion, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.POCodigoOrfeo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            string CodigoOrfeo = string.Empty;
            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.ObtieneCodigoOrfeoPorIdValoracion, null, ref cError);
                object objCodigoOrfeo = d.GetOutputParameter(resx::Parametros.POCodigoOrfeo);
                if (objCodigoOrfeo != null)
                    CodigoOrfeo = objCodigoOrfeo.ToString();
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerCodigoOrfeoPorIdVal:::", ex);
                cError = ex.Message;
            }

            return CodigoOrfeo;
        }

        #endregion

    }
}
