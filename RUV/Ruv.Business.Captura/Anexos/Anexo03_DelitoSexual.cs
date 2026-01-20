using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Ruv.Business.Captura.Anexos
{
    public class Anexo03_DelitoSexual
    {
        #region Guardar
        public static void Guardar(clsAnexo03_Victima anexoView, DbTransaction tran)
        {
            //Borrar todos los delitos sexuales (se asigna ACTIVO = 0 si existe)
            entDelitosSexuales entDS = new entDelitosSexuales();
            entDS.delDelitoSexual((int)anexoView.ID, tran);

            int cont = 0;
            foreach (int delitoSexId in anexoView.DelitosSexuales)
            {
                //Insertar delito sexual (si existe ACTIVO = 1)
                TBDELITO_SEXUAL_A3 delitoSexData = new TBDELITO_SEXUAL_A3();
                Anexo03_DelitoSexual.ParseViewToData(cont, anexoView.ID ?? -1, delitoSexId, anexoView.EstadoRegistro, delitoSexData);

                entDS.setDelitoSexual(delitoSexData, tran);
                cont++;
            }
        }

        public static void ParseViewToData(int id, int idAnexo, int idDelitoSexual, eEstadoRegistro estadoReg, TBDELITO_SEXUAL_A3 delitoData)
        {
            if (delitoData == null)
                delitoData = new TBDELITO_SEXUAL_A3();
            delitoData.ID = id;     // Calculado Automáticamente
            delitoData.PARAM_DELITOSEXUAL = idDelitoSexual;
            if (delitoData.TBANEXO3 == null)
                delitoData.TBANEXO3 = new TBANEXO3();
            delitoData.TBANEXO3.ID = idAnexo;
            delitoData.ACTIVO = (short)((estadoReg == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        #endregion

        #region Obtener
        public static List<int> Obtener(int id_anexo03)
        {
            List<int> DelitosSexuales = new List<int>();

            entDelitosSexuales entDelitoSex = new entDelitosSexuales();
            List<TBDELITO_SEXUAL_A3> delitosSexualData = new List<TBDELITO_SEXUAL_A3>();
            delitosSexualData = entDelitoSex.getData(id_anexo03);

            foreach (TBDELITO_SEXUAL_A3 delitoSexualData in delitosSexualData)
            {
                //Agregar el delito sexual a la victima
                DelitosSexuales.Add(delitoSexualData.PARAM_DELITOSEXUAL);
            }
            return DelitosSexuales;
        }
        #endregion
    }
}
