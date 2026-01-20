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
    public class Anexo03_NacidoDelitoSexual
    {
        #region Guardar
        public static void Guardar(clsAnexo03 anexo, DbTransaction tran)
        {
            entNacidoViolacion entNacido = new entNacidoViolacion();
            //Borrar (actualizar ACTIVO a 0 si existe) a todos los nacidos por violacion del anexo
            entNacido.deleteData((int)anexo.ID, tran);

            //Agregar (actualizar ACTIVO a 1 si existe) a cada uno de los nacidos por violacion del anexo
            int cont = 0; // Caso se Inserción, se crean nuevas numeraciones que no son tenidas en cuenta mas si en E.F.
            foreach (int Idniño in anexo.NiñosNacidosPorAbusoSexual)
            {
                TBNACIDO_VIOLACION_A3 nacidoViolacion = new TBNACIDO_VIOLACION_A3();
                Anexo03_NacidoDelitoSexual.ParseViewToData(cont, (int)anexo.ID, Idniño, anexo.EstadoRegistro, nacidoViolacion);

                nacidoViolacion.ACTIVO = 1;
                entNacido.setNacidoViolacion(nacidoViolacion, tran);
                cont++;
            }
        }

        public static void ParseViewToData(int id, int idAnexo, int idRegPersona, eEstadoRegistro estadoReg, TBNACIDO_VIOLACION_A3 nacidoData)
        {
            if (nacidoData == null)
                nacidoData = new TBNACIDO_VIOLACION_A3();
            nacidoData.ID = id; // Calculado Automáticamente

            if (nacidoData.TBREGISTROS_PERSONAS == null)
                nacidoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            nacidoData.TBREGISTROS_PERSONAS.ID = idRegPersona;

            if (nacidoData.TBSINIESTROS_PERSONA == null)
                nacidoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            nacidoData.TBSINIESTROS_PERSONA.ID = idAnexo;
            nacidoData.ACTIVO = (short)((estadoReg == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        #endregion

        #region Obtener
        public static ObservableCollection<int> Obtener(int id_siniestro)
        {
            ObservableCollection<int> NacidosPorAbusoSexual = new ObservableCollection<int>();
            entNacidoViolacion entNacido = new entNacidoViolacion();
            List<TBNACIDO_VIOLACION_A3> nacidosData = entNacido.getData(id_siniestro);
            foreach (TBNACIDO_VIOLACION_A3 nacidoData in nacidosData)
            {
                NacidosPorAbusoSexual.Add(nacidoData.TBREGISTROS_PERSONAS.ID);
            }
            return NacidosPorAbusoSexual;
        }

        #endregion
    }
}
