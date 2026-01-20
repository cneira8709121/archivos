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
    public class Anexo05_Desplazado
    {
        #region Guardar
        public static void Guardar(clsAnexo05_Victima desplazadoView, int idAnexo05, int id_jefeHogar, int idValanexo, DbTransaction tran)
        {
            TBANEXO5_DESPLAZADOS desplazadoData = new TBANEXO5_DESPLAZADOS();
            Anexo05_Desplazado.ParseViewToData(desplazadoView, idAnexo05, ref desplazadoData);
            entAnexo5Desplazado entDespBd = new entAnexo5Desplazado();
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            if (desplazadoView.PersonaAfectadaId == id_jefeHogar)
                desplazadoData.JEFE_HOGAR = 1;

            /*Inserción en la base de datos*/            
            switch (desplazadoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    desplazadoData.ACTIVO = 1;
                    entDespBd.setData(desplazadoData, tran);
                    //anexo.ID = anexoData.ID;
                    if (idValanexo > 0 && desplazadoView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)desplazadoView.PersonaAfectadaId, idAnexo05, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entDespBd.updData(desplazadoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    desplazadoData.ACTIVO = 0;
                    entDespBd.updData(desplazadoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData(clsAnexo05_Victima desplazadoView, int id_anexo, ref TBANEXO5_DESPLAZADOS desplazadoData)
        {
            desplazadoData.ID = desplazadoView.ID ?? -1;

            if (desplazadoData.TBREGISTROS_PERSONAS == null)
                desplazadoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            desplazadoData.TBREGISTROS_PERSONAS.ID = (int)desplazadoView.PersonaAfectadaId;
            if (desplazadoData.TBANEXO5 == null)
                desplazadoData.TBANEXO5 = new TBANEXO5();
            desplazadoData.TBANEXO5.ID = id_anexo;

            desplazadoData.SE_DESPLAZO = Common.ParseIntToShortNullable(desplazadoView.SeDesplazo);

            desplazadoData.ACTIVO = (short)((desplazadoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
                
        #endregion

        #region Obtener
        public static ObservableCollection<clsAnexo05_Victima> Obtener(int id_anexo, ref int? id_jefe_hogar)
        {
            ObservableCollection<clsAnexo05_Victima> desplazadosView = new ObservableCollection<clsAnexo05_Victima>();
            //Obtener las victimas
            entAnexo5Desplazado entDesplazado = new entAnexo5Desplazado();
            List<TBANEXO5_DESPLAZADOS> desplazadosdata = entDesplazado.getData(id_anexo);
            foreach (TBANEXO5_DESPLAZADOS desplazadoData in desplazadosdata)
            {
                clsAnexo05_Victima desplazadoView = new clsAnexo05_Victima();

                Anexo05_Desplazado.ParseDataToView(desplazadoData, ref desplazadoView);

                desplazadoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                if (desplazadoData.JEFE_HOGAR == 1)
                    id_jefe_hogar = desplazadoData.TBREGISTROS_PERSONAS.ID;
                
                //Agregar personaAfectada a la declaración
                desplazadosView.Add(desplazadoView);
            }
            return desplazadosView;
        }

        /// <summary>
        /// Tabla de Resultados de Consulta del anexo 5
        /// </summary>
        /// <param name="desplazadoData: entidad con Datos del Anexo 06"></param>
        /// <param name="? null"></param>
        public static void ParseDataToView(TBANEXO5_DESPLAZADOS desplazadoData, ref clsAnexo05_Victima anexoView)
        {
            if (anexoView == null)
                anexoView = new clsAnexo05_Victima();
            anexoView.ID = desplazadoData.ID;
            anexoView.PersonaAfectadaId = desplazadoData.TBREGISTROS_PERSONAS.ID;
            anexoView.SeDesplazo =  desplazadoData.SE_DESPLAZO;
        }
        #endregion
    }
}
