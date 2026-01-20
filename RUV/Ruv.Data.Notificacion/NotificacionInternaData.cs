using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Notificacion;
using System.Data;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.OracleClient;
using Ruv.Data.Notificacion.Contratos;
using Ruv.Infrastructure.Crosscutting.Utilities;
using System.Data.Common;

namespace Ruv.Data.Notificacion
{
    public class NotificacionInternaData : INotificacionInternaData
    {
       public IList<clsNotificacionInterna> ObtenerNotificacionesInternas(int nIdUsuario, ref string cError)
       {
           Dao d = new Dao();
           d.RefreshParameters();

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuarioRecibe,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nIdUsuario,
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
               dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerNotificacionInterna, ref cError);
               if (!(cError == null || cError == string.Empty)) return null;
           }
           catch (Exception ex)
           {
               cError = ex.Message;
               return null;
           }

           return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacionInterna>(dr, true);
       }

       public bool GenerarNotificacionInterna(int nIdProceso, int nIdUsuarioGenera,int nTipoProceso,int nIdUsuarioRecibe,string cTexto, string cDescripcion, DbTransaction tra, ref string cError)
       {
           Dao d = new Dao();
           d.RefreshParameters();

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuarioGenera,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nIdUsuarioGenera,
               Direction = ParameterDirection.Input
           });
           
           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdProcesos,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nIdProceso,
               Direction = ParameterDirection.Input
           });

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.TipoProceso,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nTipoProceso,
               Direction = ParameterDirection.Input
           });           
           
           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuarioRecibe,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nIdUsuarioRecibe,
               Direction = ParameterDirection.Input
           });

           

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.TextoNotificacionInterna,
               OracleType = System.Data.OracleClient.OracleType.NVarChar,
               Value = cTexto,
               Direction = ParameterDirection.Input
           });

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.DescripcionNotificacionInterna,
               OracleType = System.Data.OracleClient.OracleType.NVarChar,
               Value = cDescripcion,
               Direction = ParameterDirection.Input
           });

           try
           {
               d.ExecuteNonQuery(resx::Procedimientos.GeneraNotificacionInterna, tra, ref cError);
           }
           catch (Exception ex)
           {
               cError = ex.Message;
           }

           if (!string.IsNullOrEmpty(cError)) return false;
           return true;
       }

       public bool MarcarLeido(int nIdNotificacionInterna, DbTransaction tra, ref string cError)
       {
           Dao d = new Dao();
           d.RefreshParameters();

           d.AddParameter(new System.Data.OracleClient.OracleParameter
           {
               ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacionInterna,
               OracleType = System.Data.OracleClient.OracleType.Number,
               Value = nIdNotificacionInterna,
               Direction = ParameterDirection.Input
           });

           try
           {
               d.ExecuteNonQuery(resx::Procedimientos.MarcarLeidos, tra, ref cError);
           }
           catch (Exception ex)
           {
               cError = ex.Message;
           }

           if (!string.IsNullOrEmpty(cError)) return false;
           return true;
       }

    }
}
