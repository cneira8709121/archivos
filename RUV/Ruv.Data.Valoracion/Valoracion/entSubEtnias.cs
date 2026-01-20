using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Ruv.Business.DTO.Valoracion;
using System.Data.Common;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entSubEtnias
    {
        public List<clsSubEtniasdto> GetSubEtnias(int etniaId)
        {
            //List<TBETNIACOMUNIDADES> SubEtnias = new List<TBETNIACOMUNIDADES>();
            //using (IDataReader dr = dbRUV.ExecuteReader("PKG_Common.SP_OBTIENESUBETNIAS", new object[] { null }))
            //{
            //    while (dr.Read())
            //    {
            //        int index = 0;
            //        TBETNIACOMUNIDADES SubEtn = EnterpriseLibraryContainer.Current.GetInstance<TBETNIACOMUNIDADES>();
            //        SubEtn.ID = dbDefaults.getInt32(dr, index++).Value;
            //        SubEtn.ETNIAGRUPOID = dbDefaults.getDecimal(dr, index++).Value;
            //        SubEtn.NOMBRE = dbDefaults.getString(dr, index++);                    
            //        SubEtn.NUMERO = dbDefaults.getInt32(dr, index++).Value;
            //        SubEtnias.Add(SubEtn);                    
            //    }
            //}
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.Id, OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = etniaId });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.Resultado, OracleType = System.Data.OracleClient.OracleType.Cursor, Direction = ParameterDirection.Output });

            string cError= string.Empty;
            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtieneSubEtnias, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::GetSubEtnias", ex);
                cError = ex.Message;
                return null;
            }
            return ComplexDataAccessImplements.MapFromDataReaderI<clsSubEtniasdto>(dr, true);
        }
            //return SubEtnias;
        }
    }

