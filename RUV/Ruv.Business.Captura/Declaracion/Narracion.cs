using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Common;

namespace Ruv.Business.Captura.Declaracion
{
    public class Narracion
    {
        #region Guardar
        public static void Guardar(clsDeclaracion declaracionView, DbTransaction tran)
        {
            Ruv.Data.TBNARRACIONES narracionData = new Ruv.Data.TBNARRACIONES();
            narracionData.ID_DECLARACION = (int)declaracionView.ID;
            narracionData.NARRACION = declaracionView.DescripcionHechos.Narracion;
            
            //if (string.IsNullOrWhiteSpace(narracionData.NARRACION)) return;

            entNarracion entNarr = new entNarracion();
            switch (declaracionView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    entNarr.setData(narracionData, tran);
                    break;
                case eEstadoRegistro.Modificado:
                    entNarr.updateData(narracionData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    break;
                case eEstadoRegistro.SinModificaciones:
                    break;
            }
        }

        #endregion

        #region Obtener Datos
        public static void Obtener(int id_declaracion, ref clsDescripcionHechos narracionView)
        {
            entNarracion entNar = new entNarracion();
            TBNARRACIONES narracion = entNar.getData(id_declaracion);
            if (narracion != null)
                narracionView.Narracion = narracion.NARRACION;

            narracionView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
        }
        #endregion
    }
}