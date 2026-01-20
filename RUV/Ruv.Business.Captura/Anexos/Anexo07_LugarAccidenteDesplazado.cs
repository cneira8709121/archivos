using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Ruv.Business.Captura.Anexos
{
    public class Anexo07_LugarAccidenteDesplazado
    {
        #region Guardar
        public static void Guardar(int idAnexo07, string lugarAccidente, DbTransaction tran)
        {
            entLugarAccidente entDS = new entLugarAccidente();

            TBANEXO7_LUGARACCIDENTE LugAccData = new TBANEXO7_LUGARACCIDENTE();
            Anexo07_LugarAccidenteDesplazado.ParseViewToData(-1, idAnexo07, lugarAccidente, ref LugAccData);
            entDS.setLugarAccidente(LugAccData, tran);
        }

        public static void ParseViewToData(int id, int id_siniestro, String descripcion, ref TBANEXO7_LUGARACCIDENTE LugarData)
        {
            if (LugarData == null)
                LugarData = new TBANEXO7_LUGARACCIDENTE();
            LugarData.ID = id; // Calculado Automáticamente
            LugarData.DESCRIPCION = descripcion;
            LugarData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            LugarData.TBSINIESTROS_PERSONA.ID = id_siniestro;
        }
        #endregion

        #region Obtener
        public static string Obtener(int id_anexo)
        {
            entLugarAccidente entLugar = new entLugarAccidente();
            TBANEXO7_LUGARACCIDENTE lugarData = entLugar.getData(id_anexo);

            return lugarData.DESCRIPCION;
        }
        #endregion
    }
}
