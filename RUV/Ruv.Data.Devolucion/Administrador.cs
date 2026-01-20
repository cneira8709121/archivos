using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.Devolucion;

namespace Ruv.Data.Devolucion
{
    public class Administrador : Contratos.IDevolucion
    {
        #region Public methods

        #region Services implementation

        public clsDevolucion ObtenerDevolucion(Int32 idDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDeclaracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idDeclaracion,
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
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerDevolucion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsDevolucion> listClsDevolucion = ComplexDataAccessImplements.MapFromDataReaderI<clsDevolucion>(dr, true);

            if (listClsDevolucion != null && listClsDevolucion.Count > 0)
            {
                clsDevolucion dev = listClsDevolucion.FirstOrDefault();
                dev.IdsCausales = ObtenerListaCausales(dev.NId, ref cError);
                if (!string.IsNullOrEmpty(cError)) return null;
                return dev;
            }
            else
                return null;
        }

        public Boolean ActualizarDevolucion(clsDevolucion devolucion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDevolucion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = devolucion.NId,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = devolucion.NIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ParteEmotiva,
                OracleType = System.Data.OracleClient.OracleType.LongVarChar,
                Value = devolucion.CParteEmotiva,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroGuia,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = devolucion.CNumeroGuia,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Direccion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = devolucion.CDireccion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Telefono,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = devolucion.NTelefono,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Funcionario,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = devolucion.CFuncionario,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizarDevolucion, tra, ref cError);
            }

            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public Boolean SolicitarDevolucion(clsDevolucion devolucion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDeclaracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = devolucion.NIdDeclaracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                IsNullable = true,
                Value = devolucion.NIdEntidadMunicipio,
                Direction = ParameterDirection.Input
            });
            
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = devolucion.NIdUsuario,
                Direction = ParameterDirection.Input
            });


            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Observaciones,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = devolucion.CObservaciones,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.CausalesDevolucion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = String.Join(",", devolucion.IdsCausales.Select(x => x.ToString()).ToArray()),
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.SolicitarDevolucion, tra, ref cError);
            }

            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public clsDatosparaDevolucion CargaDatosparaDevolucion(int NIdDevolucion, ref string cError) 
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDevolucion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = NIdDevolucion,
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
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CargaDatosparaDevolucion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsDatosparaDevolucion> listClsDevolucion = ComplexDataAccessImplements.MapFromDataReaderI<clsDatosparaDevolucion>(dr, true);

            if (listClsDevolucion != null && listClsDevolucion.Count > 0)
                return listClsDevolucion.FirstOrDefault();
            else
                return null;
        }

        public List<clsCausalDevolucion> ObtenerCausalesDevolucion(ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerListadoCausalesDevolucion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsCausalDevolucion> lstCausalesDevolucion = ComplexDataAccessImplements.MapFromDataReaderI<clsCausalDevolucion>(dr, true);
            return lstCausalesDevolucion;
        }

        #endregion

        #endregion
        #region Private methods

        private List<int> ObtenerListaCausales(int nIdDevolucion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDevolucion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdDevolucion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            List<int> lstCausales = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerListaCausales, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;

                lstCausales = new List<int>();
                while (dr.Read())
                {
                    lstCausales.Add(int.Parse(dr["ID_CAUSAL"].ToString()));
                }
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }
            return lstCausales;
        }

        #endregion
    }
}
