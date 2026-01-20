using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using Ruv.Infrastructure.Crosscutting.Resources.DB;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using System.Data;
using System.Data.Common;

namespace Ruv.Data.GestionGlosa
{
    public class clsGestionGlosa : Contratos.IGestionGlosa
    {
        public void AsignarGlosa(int? nIdAsignaGlosa, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
                {
                    ParameterName = Parametros.IdDeclaracion,
                    OracleType = OracleType.Int32,
                    Value = nIdAsignaGlosa,
                    Direction = ParameterDirection.Input
                });
            try
            {
                d.ExecuteNonQuery(Procedimientos.AsignaGlosa, tra, ref cError);
            }
            catch (Exception e)
            {
                cError = e.Message;
            }
        }
    }
}
