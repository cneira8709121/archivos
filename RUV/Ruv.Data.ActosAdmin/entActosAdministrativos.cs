using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ruv.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace Ruv.Data.ActosAdmin
{
    public class entActosAdministrativos : entidadRUV
    {
        public entActosAdministrativos()
        {
        }

        public DataTable GetActosAdministrativosPaginado(int Inicio, int Fin, string sortColumns)
        {
            DataSet ds = dbRUV.ExecuteDataSet("PKG_ACTOSADMIN.sp_getActosAdminPaginado", new object[] { Inicio, Fin, sortColumns, null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public int GetCantidad()
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_ACTOSADMIN.sp_getActosAdminCantidad", new object[] { null });

            dbRUV.ExecuteNonQuery(cmd);
            return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Cantidad"));
        }

        public bool ExisteDeclaracion(string formulario)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_ACTOSADMIN.sp_getCantidadFormulario", new object[] { formulario, null });

            dbRUV.ExecuteNonQuery(cmd);
            int cantidad = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Cantidad"));

            if (cantidad > 0)
                return true;
            else
                return false;
        }

        public string Insertar(object[] data)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_ACTOSADMIN.sp_setActoAdministrativo", data);

            dbRUV.ExecuteNonQuery(cmd);
            string consecutivo = dbRUV.GetParameterValue(cmd, "P_Consecutivo").ToString();

            return consecutivo;
        }

        public string Actualizar(object[] data)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_ACTOSADMIN.sp_updActoAdministrativo", data);

            dbRUV.ExecuteNonQuery(cmd);
            string consecutivo = dbRUV.GetParameterValue(cmd, "P_Consecutivo").ToString();

            return consecutivo;
        }

        public TBACTO_ADMINISTRATIVO GetPorId(int id)
        {
            TBACTO_ADMINISTRATIVO actoadm = new TBACTO_ADMINISTRATIVO();
            using (IDataReader dr = dbRUV.ExecuteReader("PKG_ACTOSADMIN.sp_GetActoAdminPorId", new object[] { id, null }))
            {
                while (dr.Read())
                {
                    
                    int index = 0;
                    actoadm.TBDECLARACIONES = new TBDECLARACIONES();
                    //actoadm.TBPARAMETROS = new TBPARAMETROS();

                    //actoadm.TBAREADOCUMENTO.TBPARAMETROS.NOMBRE = dbDefaults.getString(dr, index++);
                    actoadm.PARAM_DOCUMENTO = dbDefaults.getInt32(dr, index++).Value;
                    actoadm.NUM_INTERNO = dbDefaults.getString(dr, index++);
                    actoadm.TBDECLARACIONES.NUMEROFORMULARIO = dbDefaults.getString(dr, index++);
                    actoadm.DESCRIPCION = dbDefaults.getString(dr, index++);
                    actoadm.ID_USUARIO = dbDefaults.getInt32(dr, index++).Value;
                    actoadm.PARAM_ESTADO = dbDefaults.getInt32(dr, index++).Value;
                    actoadm.FECHA = dbDefaults.getDateTime(dr, index++).Value;
                    actoadm.DIRIGIDO = dbDefaults.getString(dr, index++);
                    actoadm.ID = dbDefaults.getInt32(dr, index++).Value; ;
                    actoadm.CONSECUTIVO = dbDefaults.getString(dr, index++);
                }
            }
            return actoadm;
        }

        public DataTable GetActosAdministrativosFiltro(string tipoFiltro, string valorFiltro)
        {
            DataSet ds = dbRUV.ExecuteDataSet("PKG_ACTOSADMIN.sp_getActosAdminFiltro", new object[] { tipoFiltro, valorFiltro,  null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}
