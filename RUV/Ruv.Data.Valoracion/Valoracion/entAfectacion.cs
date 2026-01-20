using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entAfectacion : entidadRUV
    {

        public List<TBPARAMETROS> GetAfectacionesPorPersonaId(int IdPersona)
        {
            List<TBPARAMETROS> afectaciones = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetAfectacionesPorPersona", new object[] { IdPersona, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPARAMETROS afect = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    afect.ID = dbDefaults.getInt32(dr, index++).Value;
                    afect.NOMBRE = dbDefaults.getString(dr, index++);
                    afectaciones.Add(afect);
                }
                
            }
            return afectaciones;
        }

        public bool Eliminar(int Id, DbTransaction tra)
        {
            using (Dao d = new Dao()) 
            {
                d.RefreshParameters();
                d.AddInputParameter (new OracleParameter() { ParameterName = resx::Parametros.Id, OracleType = OracleType.Number, Value = Id });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number });
                d.ExecuteNonQuery("pkg_valoracion.sp_EliminarTbAfectacionVal", tra);
                int afectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));
                return afectadas > 0 ? true : false;
            }

            //DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_EliminarTbAfectacionVal", new object[] { Id, null });
            //dbRUV.ExecuteNonQuery(cmd);
            //int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Afectadas"));
            //return afectadas > 0 ? true : false;
        }


        public void Insertar(int afectacion, int anexoPerId, DbTransaction tra)
        {
            using (Dao d = new Dao())
            {
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_id_valanexoperson", OracleType = OracleType.Number, Value = anexoPerId });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_param_afectacion", OracleType = OracleType.Number, Value = afectacion });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_Afectadas", OracleType = OracleType.Number });
                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbAfectacionVal", tra);
            }

            //DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbAfectacionVal", new object[] { anexoPerId, afectacion, null });
            //dbRUV.ExecuteNonQuery(cmd);
            //int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Afectadas"));
        }
    }
}
