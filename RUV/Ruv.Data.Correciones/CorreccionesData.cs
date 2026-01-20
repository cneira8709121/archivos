using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data.Correcciones.Contratos;
using Ruv.Business.DTO.Correcciones;
using Ruv.Business.DTO.Reporteador;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;

namespace Ruv.Data.Correcciones
{
    public class CorreccionesData : ICorreccionesData
    {
        #region Public methods

        #region Services implementation

        public bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdRegistroPersona,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = IdRegPersona,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuarioSolicitud,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = idUsuarioSolicita,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.CamposCorrecciones,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = String.Join(",", correcciones.Select(x => x.Campo).ToArray()),
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ValoresCorrecciones,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = String.Join(",", correcciones.Select(x => x.Valor).ToArray()),
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.SolicitarCorreccion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdRegistroPersona,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = IdRegPersona,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuarioSolicitud,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = idUsuarioSolicita,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.CamposCorrecciones,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = String.Join(",", correcciones.Select(x => x.Campo).ToArray()),
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ValoresCorrecciones,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = String.Join(",", correcciones.Select(x => x.Valor).ToArray()),
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdCorreccionOut,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            int idCorreccionOut = 0;

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.SolicitarCorreccion, tra, ref cError);
                idCorreccionOut = int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdCorreccionOut).ToString());
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            if (!(cError == null || cError == string.Empty)) return 0;

            return idCorreccionOut;
        }

        public List<clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegistroPersona, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdRegistroPersona,
                OracleType = OracleType.Number,
                Value = IdRegistroPersona,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.CargaDatosCorreccion, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsCargaDatosCorreccion>(dr, true);
        }

        public clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdCorreccion,
                OracleType = OracleType.Number,
                Value = idCorreccion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ConsultarCorreccion, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsCargaDatosCorreccion> listClsCargaDatosCorreccion = ComplexDataAccessImplements.MapFromDataReaderI<clsCargaDatosCorreccion>(dr, true);

            if (listClsCargaDatosCorreccion != null && listClsCargaDatosCorreccion.Count > 0)
                return listClsCargaDatosCorreccion.FirstOrDefault();
            else
                return null;
        }

        public bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdCorreccion,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = idCorreccion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuarioRechaza,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = idUsuarioRechaza,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.Observacion,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = observaciones,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.RechazarCorreccion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = declarante.CNumeroFormulario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.PrimerNombre, OracleType = OracleType.VarChar, Value = declarante.CPrimerNombre, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.PrimerApellido, OracleType = OracleType.VarChar, Value = declarante.CPrimerApellido, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.NumeroDocumento, OracleType = OracleType.VarChar, Value = declarante.CNumeroDocumento, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.BuscaPersonasCount, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return 0;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
        }

        public List<clsDeclarante> ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = declarante.CNumeroFormulario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.PrimerNombre, OracleType = OracleType.VarChar, Value = declarante.CPrimerNombre, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.PrimerApellido, OracleType = OracleType.VarChar, Value = declarante.CPrimerApellido, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.NumeroDocumento, OracleType = OracleType.VarChar, Value = declarante.CNumeroDocumento, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.NumeroPagina, OracleType = OracleType.Int32, Value = numeroPagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.RegistrosPorPagina, OracleType = OracleType.Int32, Value = registrosPorPagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.BuscaPersonas, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsDeclarante>(dr, true);
        }
        
        public bool AprobarCorreccion(int IdCorreccion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdCorreccion,
                OracleType = OracleType.Number,
                Value = IdCorreccion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.AprobarCorreccion, tra, ref cError);
            }

            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;

        }

        public IList<clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdCorreccion,
                OracleType = OracleType.Number,
                Value = idCorreccion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ConsultarCamposCorreccion, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsCorreccion> listClsCorreccion = ComplexDataAccessImplements.MapFromDataReaderI<clsCorreccion>(dr, true);

            return listClsCorreccion;
        }

        public clsInformacionCorreccion CargaInformacionCorreccion(int nIdCorreccion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.IdCorreccion,
                OracleType = OracleType.Number,
                Value = nIdCorreccion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ObtenerInfoCorreccion, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsInformacionCorreccion> ClsInfoCorreccion = ComplexDataAccessImplements.MapFromDataReaderI<clsInformacionCorreccion>(dr, true);

            if (ClsInfoCorreccion != null && ClsInfoCorreccion.Count > 0)
                return ClsInfoCorreccion.FirstOrDefault();
            else
              return null;
        }

        public string ObtieneNombreSubEtnia(int nIdSubetnia, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.Id,
                OracleType = OracleType.Number,
                Value = nIdSubetnia,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NombreSubetnia,
                OracleType = System.Data.OracleClient.OracleType.NVarChar,
                Direction = ParameterDirection.Output,
                Size = 170
            });

            
            string NombreSubEtnia = string.Empty;
            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.ObtieneNombreSubetnia,null, ref cError);
                NombreSubEtnia = d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.NombreSubetnia) as string;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            if (!(cError == null || cError == string.Empty)) return string.Empty;

            return NombreSubEtnia;
        }

        #endregion

        #endregion
    }
}
