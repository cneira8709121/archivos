using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Configuration;
using System.Data.EntityClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.OracleClient;
using Ruv.Infrastructure.Crosscutting.Resources.DB;
using Ruv.Business.DTO.Valoracion;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;


namespace Ruv.Data.Valoracion.Valoracion
{
    public class entConceptoDeclaracion
    {
        public clsConceptoDeclaracion ObtenerConceptoDeclaracion(int idDeclaracion)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = "pi_Id_Declaracion", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = idDeclaracion });
                d.AddParameter(new OracleParameter { ParameterName = "po_Cursor", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                IDataReader dr = null;               
                dr = d.ExecuteReader("PKG_VALORACION.SP_GET_ULTIMOCONCEPTOASOCIADO");
                List<clsConceptoDeclaracion> conceptoDeclaracion = ComplexDataAccessImplements.MapFromDataReaderI<clsConceptoDeclaracion>(dr, true);
                if (conceptoDeclaracion != null && conceptoDeclaracion.Count > 0)
                    return conceptoDeclaracion.FirstOrDefault();
                else
                    return null;
            }
        }

        public bool InsertaConceptoDeclaracion(clsConceptoDeclaracion conceptoDeclaracion)
        {
            bool resultado = false;
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = "pi_Id_Declaracion", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = conceptoDeclaracion.Id_Declaracion });
                d.AddParameter(new OracleParameter { ParameterName = "pi_Id_Concepto", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = conceptoDeclaracion.Id_Concepto });
                DbTransaction transact = null;
                d.ExecuteNonQuery("PKG_VALORACION.SP_SET_CONCEPTODECLARACION", transact);
                resultado = true;
            }
            return resultado;
        }
    }
}
