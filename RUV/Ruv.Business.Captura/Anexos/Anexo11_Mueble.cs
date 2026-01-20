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
    public class Anexo11_Mueble
    {
        #region Guardar
        public static void Guardar(clsAnexo11_BienMueble bienView, int idAnexo11, int idValanexo, DbTransaction tran)
        {
            entMuebles entBD = new entMuebles();
            TBANEXO11_MUEBLES bienData = new TBANEXO11_MUEBLES();
            Anexo11_Mueble.ParseViewToData(-1, idAnexo11, bienView, ref bienData);
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            switch (bienView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    bienData.ACTIVO = 1;
                    entBD.setAnexo11_Muebles(bienData, tran);
                    bienView.ID = bienData.ID;
                    if (idValanexo > 0 && bienView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)bienView.PersonaAfectadaId, idAnexo11, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBD.updAnexo11_Muebles(bienData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    bienData.ACTIVO = 0;
                    entBD.updAnexo11_Muebles(bienData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData(int id, int idAnexo, clsAnexo11_BienMueble bienView, ref TBANEXO11_MUEBLES bienData)
        {
            if (bienData == null)
                bienData = new TBANEXO11_MUEBLES();
            bienData.ID = bienView.ID ?? id; // Calculado Automáticamente

            if (bienData.TBANEXO11 == null)
                bienData.TBANEXO11 = new TBANEXO11();
            bienData.TBANEXO11.ID = idAnexo;

            if (bienData.TBREGISTROS_PERSONAS == null)
                bienData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            //bienData.TBREGISTROS_PERSONAS.ID = (int)bienView.PersonaAfectadaId;
            if (bienView.PersonaAfectadaId.HasValue)
            {
                bienData.TBREGISTROS_PERSONAS.ID = (int)bienView.PersonaAfectadaId;
            }
            bienData.PARAM_TIPO_MUBLE = bienView.TipoBien;
            bienData.PARAM_TIPO_TENENCIA = bienView.TipoTenencia;
            bienData.DESCRIPCION = bienView.Descripcion;
            bienData.CANTIDAD = (short?)bienView.Cantidad;

            bienData.ACTIVO = (short)((bienView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }


        #endregion

        #region Obtener
        public static ObservableCollection<clsAnexo11_BienMueble> Obtener(int id_anexo)
        {
            ObservableCollection<clsAnexo11_BienMueble> BienesMuebles = new ObservableCollection<clsAnexo11_BienMueble>();
            entMuebles entMueble = new entMuebles();
            List<TBANEXO11_MUEBLES> mueblesData = entMueble.getData(id_anexo);
            foreach (TBANEXO11_MUEBLES muebleData in mueblesData)
            {
                clsAnexo11_BienMueble muebleView = new clsAnexo11_BienMueble();

                Anexo11_Mueble.ParseDataToView(muebleData, ref muebleView);

                //Reiniciar Estado
                muebleView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                BienesMuebles.Add(muebleView);
            }
            return BienesMuebles;
        }

        public static void ParseDataToView(TBANEXO11_MUEBLES muebleData, ref clsAnexo11_BienMueble muebleView)
        {
            muebleView.ID = muebleData.ID;

            if (muebleData.TBREGISTROS_PERSONAS != null)
                muebleView.PersonaAfectadaId = muebleData.TBREGISTROS_PERSONAS.ID;

            muebleView.TipoBien = muebleData.PARAM_TIPO_MUBLE;
            muebleView.Descripcion = muebleData.DESCRIPCION;
            muebleView.TipoTenencia = muebleData.PARAM_TIPO_TENENCIA;
            muebleView.Cantidad = (double?)muebleData.CANTIDAD;

            //bienAfectadoView.EstadoRegistro = bienAfectadoData.ACTIVO;


        }
        #endregion
    }
}
