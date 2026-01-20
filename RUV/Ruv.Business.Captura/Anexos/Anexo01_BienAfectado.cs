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
using Ruv.Data.Radicacion;
using System.Data.Common;

namespace Ruv.Business.Captura.Anexos
{
    public class Anexo01_BienAfectado
    {
        #region Guardar
        public static void Guardar(clsAnexo01_Victima_Bien Victima_Bien, int id_anexo, DbTransaction tran)
        {
            entBienAfectado entBienAfect = new entBienAfectado();

            TBBIEN_AFECTADO_A1 bienAfectadoData = new TBBIEN_AFECTADO_A1();
            Anexo01_BienAfectado.ParseViewToData(id_anexo, Victima_Bien, bienAfectadoData);
            switch (Victima_Bien.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    bienAfectadoData.ACTIVO = 1;

                    entBienAfect.setData(bienAfectadoData, tran);
                    Victima_Bien.ID = bienAfectadoData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entBienAfect.updData(bienAfectadoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    bienAfectadoData.ACTIVO = 0;
                    entBienAfect.updData(bienAfectadoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }
        
        private static void ParseViewToData(int id_anexo01, clsAnexo01_Victima_Bien bienAfectadoView, TBBIEN_AFECTADO_A1 bienAfectadoData)
        {
            bienAfectadoData.ID = bienAfectadoView.ID ?? -1;
            if (bienAfectadoData.TBANEXO1 == null)
                bienAfectadoData.TBANEXO1 = new TBANEXO1();
            bienAfectadoData.TBANEXO1.ID = id_anexo01;

            bienAfectadoData.INMUEBLE = Common.ParseIntToShortNullable(bienAfectadoView.TipoBien);            
            bienAfectadoData.PARAM_TIPOPERTENENCIA = Common.ParseIntToShortNullable(bienAfectadoView.CalidadDeLaVictima);
            bienAfectadoData.ACTIVO = (short)((bienAfectadoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
            bienAfectadoData.DESCRIPCION = bienAfectadoView.Descripcion;
        }
        #endregion
        
        #region Obtener
        public static ObservableCollection<clsAnexo01_Victima_Bien> Obtener(int id_anexo)
        {
            ObservableCollection<clsAnexo01_Victima_Bien> bienes = new ObservableCollection<clsAnexo01_Victima_Bien>();
            //Obtener los bienes muebles o inmuebles activos
            entBienAfectado entBien = new entBienAfectado();
            List<TBBIEN_AFECTADO_A1> VictimaBienesData = entBien.getData(id_anexo);

            foreach (TBBIEN_AFECTADO_A1 VictimabienData in VictimaBienesData)
            {
                clsAnexo01_Victima_Bien VictimaBienView = new clsAnexo01_Victima_Bien();

                Anexo01_BienAfectado.ParseDataToView(VictimabienData, ref VictimaBienView);

                VictimaBienView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Agregar los bienes a la persona afectada
                bienes.Add(VictimaBienView);
            }
            return bienes;
        }

        private static void ParseDataToView(TBBIEN_AFECTADO_A1 bienAfectadoData, ref clsAnexo01_Victima_Bien bienAfectadoView)
        {
            bienAfectadoView.ID = bienAfectadoData.ID;

            bienAfectadoView.TipoBien = bienAfectadoData.INMUEBLE;
            bienAfectadoView.CalidadDeLaVictima = bienAfectadoData.PARAM_TIPOPERTENENCIA;

            bienAfectadoView.Descripcion = bienAfectadoData.DESCRIPCION;
        }
        #endregion
    }
}
