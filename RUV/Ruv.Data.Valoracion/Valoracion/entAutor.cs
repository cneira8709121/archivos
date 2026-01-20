using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.OracleClient;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entAutor : entidadRUV
    {
        /*public TBAUTORHV_VAL_ANEXO Insertar(TBAUTORHV_VAL_ANEXO antorValAnexo)
        {
            List<object> objetos = ParametrosGuardar(antorValAnexo);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbAutorHvAnexoPer", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int inserto = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Afectadas"));
            if (inserto > 0)
            {
                antorValAnexo.ID = inserto;
                return antorValAnexo;
            }
            else { return antorValAnexo; }
        }
        private List<object> ParametrosGuardar(TBAUTORHV_VAL_ANEXO antorValAnexo)
        {
            return new List<object>(){
                antorValAnexo.ID_AUTORHV,
                antorValAnexo.ID_VAL_ANEXO_PERSONA
            };
        }*/



        public bool Eliminar(int IdValAnexo, DbTransaction tra)
        {
            //DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_EliminaTbAutorValAnexoPer", new object[] { IdValAnexo, null });
            using (var d = new Dao())
            {
                d.AddParameter(new OracleParameter() { ParameterName = "P_ID_ValAnexoPer", OracleType = OracleType.Number, Value = IdValAnexo, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = "P_AFECTADAS", OracleType = System.Data.OracleClient.OracleType.Number, Direction = ParameterDirection.Output });
                d.ExecuteNonQuery("pkg_valoracion.sp_EliminaTbAutorValAnexoPer", tra);
                int afectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));
                return afectadas > 0 ? true : false;
            }
        }

        public List<TBAUTORHV> GetAutores()
        {
            List<TBAUTORHV> autores = new List<TBAUTORHV>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetAutores", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBAUTORHV autor = EnterpriseLibraryContainer.Current.GetInstance<TBAUTORHV>();
                    autor.ID = dbDefaults.getInt32(dr, index++).Value;
                    autor.NOMBRE = dbDefaults.getString(dr, index++);
                    autor.TEXTO = dbDefaults.getString(dr, index++);

                    autores.Add(autor);
                }
            }
            return autores;
        }

      

        public List<TBAUTORHV> GetAutoresPorValAnexoPersona(int ValAnexoPerId)
        {
            
            List<TBAUTORHV> autores = new List<TBAUTORHV>();

            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetAutoresPorValAnexoPerId", new object[] { ValAnexoPerId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBAUTORHV autor = EnterpriseLibraryContainer.Current.GetInstance<TBAUTORHV>();
                    autor.ID = dbDefaults.getInt32(dr, index++).Value;
                    autor.NOMBRE = dbDefaults.getString(dr, index++);
                    autor.FECHA_CREACION = dbDefaults.getDateTime(dr, index++);
                    autor.FECHA_DESMOVILIZACION = dbDefaults.getDateTime(dr, index++);
                    autores.Add(autor);
                }
            }
            /*using (RuvEntities Context = new RuvEntities())
            {
                TBVAL_ANEXO_PERSONA persona = Context.TBVAL_ANEXO_PERSONA.First(x=>x.ID == ValAnexoPerId);
                autores = persona.TBAUTORHV.ToList();
            }*/
            return autores;
        }

        public void Insertar(int AutorId, int ValAnexoId, DbTransaction tra)
        {
            /*
            RuvEntities context = new RuvEntities();

            TBVAL_ANEXO_PERSONA persona = context.TBVAL_ANEXO_PERSONA.First(x=> x.ID == ValAnexoId);
            TBAUTORHV autor = context.TBAUTORHV.First(x=> x.ID == AutorId);

            if(!persona.TBAUTORHV.Contains(autor)){
                persona.TBAUTORHV.Add(autor);
            }

            context.SaveChanges();
            */

            using (var d = new Dao())
            {
                d.AddInputParameter(new OracleParameter() { ParameterName = "P_ID_AUTOR", OracleType = OracleType.Number, Value = AutorId });
                d.AddInputParameter(new OracleParameter() { ParameterName = "P_ID_VAL_ANEXO_PER", OracleType = OracleType.Number, Value = ValAnexoId });
                d.AddParameter(new OracleParameter() { ParameterName = "P_Afectadas", OracleType = OracleType.Number, Direction = ParameterDirection.Output });
                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbAutorHvAnexoPer", tra);
            }
        }
    }
}
