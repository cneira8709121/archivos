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
    public class Anexo11_Credito
    {
        #region Guardar
        public static void Guardar(clsAnexo11_CreditoPasivo creditoView, int idAnexo11, DbTransaction tran)
        {
            entCreditos entBD = new entCreditos();

            TBANEXO11_CREDITOS bienData = new TBANEXO11_CREDITOS();

            Anexo11_Credito.ParseViewToData(-1, idAnexo11, creditoView, ref bienData);

            switch (creditoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    bienData.ACTIVO = 1;
                    entBD.setAnexo11_Creditos(bienData, tran);
                    creditoView.ID = bienData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entBD.updAnexo11_Creditos(bienData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    bienData.ACTIVO = 0;
                    entBD.updAnexo11_Creditos(bienData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData(int id, int idAnexo, clsAnexo11_CreditoPasivo creditoView, ref  TBANEXO11_CREDITOS creditoData)
        {
            if (creditoData == null)
                creditoData = new TBANEXO11_CREDITOS();
            creditoData.ID = creditoView.ID ?? id; // Calculado Automáticamente

            if (creditoData.TBANEXO11 == null)
                creditoData.TBANEXO11 = new TBANEXO11();
            creditoData.TBANEXO11.ID = idAnexo;

            /*if (creditoData.TBREGISTROS_PERSONAS == null)
                creditoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            creditoData.TBREGISTROS_PERSONAS.ID = 4; //(int)creditoView.PersonaAfectadaId;*/
            creditoData.PARAM_TIPO_ACREEDOR = creditoView.TipoAcreedor;
            creditoData.NOMBRE_ACREEDOR = creditoView.NombreAcreedor;
            creditoData.FECHA_DEUDA = creditoView.FechaContrajoObligacion;
            creditoData.MONTO_ADEUDADO = (decimal)creditoView.MontoAdeudado;

            creditoData.ACTIVO = (short)((creditoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        #endregion

        #region Obtener
        public static ObservableCollection<clsAnexo11_CreditoPasivo> Obtener(int id_anexo)
        {
            ObservableCollection<clsAnexo11_CreditoPasivo> Creditos = new ObservableCollection<clsAnexo11_CreditoPasivo>();
            entCreditos entBd = new entCreditos();
            List<TBANEXO11_CREDITOS> creditosData = entBd.getData(id_anexo);
            foreach (TBANEXO11_CREDITOS creditoData in creditosData)
            {
                clsAnexo11_CreditoPasivo creditoView = new clsAnexo11_CreditoPasivo();

                Anexo11_Credito.ParseDataToView(creditoData, ref creditoView);

                //Reiniciar Estado
                creditoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                Creditos.Add(creditoView);
            }
            return Creditos;
        }

        public static void ParseDataToView(TBANEXO11_CREDITOS creditoData, ref clsAnexo11_CreditoPasivo creditoView)
        {
            creditoView.ID = creditoData.ID;

            //if (creditoData.TBREGISTROS_PERSONAS != null)
            //    creditoView.PersonaAfectadaId = creditoData.TBREGISTROS_PERSONAS.ID;

            creditoView.TipoAcreedor = creditoData.PARAM_TIPO_ACREEDOR;
            creditoView.NombreAcreedor = creditoData.NOMBRE_ACREEDOR;
            creditoView.FechaContrajoObligacion = creditoData.FECHA_DEUDA;
            creditoView.MontoAdeudado = (double)creditoData.MONTO_ADEUDADO;

            //creditoView.EstadoRegistro = creditoData.ACTIVO;
        }
        #endregion
    }
}
