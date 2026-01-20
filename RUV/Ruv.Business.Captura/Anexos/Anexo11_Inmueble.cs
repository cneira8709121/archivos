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
    public class Anexo11_Inmueble
    {
        #region Guardar
        public static void Guardar(clsAnexo11_BienInmueble bienView, int idAnexo11, int idValanexo, DbTransaction tran)
        {
            entInmuebles entBD = new entInmuebles();
            TBANEXO11_INMUEBLES bienData = new TBANEXO11_INMUEBLES();
            Anexo11_Inmueble.ParseViewToData(-1, idAnexo11, bienView, ref bienData);
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            switch (bienView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    bienData.ACTIVO = 1;
                    entBD.setAnexo11_Inmuebles(bienData, tran);
                    bienView.ID = bienData.ID;
                    if (idValanexo > 0 && bienView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)bienView.PersonaAfectadaId, idAnexo11, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBD.updAnexo11_Inmuebles(bienData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    bienData.ACTIVO = 0;
                    entBD.updAnexo11_Inmuebles(bienData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData(int id, int idAnexo, clsAnexo11_BienInmueble bienView, ref TBANEXO11_INMUEBLES bienData)
        {
            if (bienData == null)
                bienData = new TBANEXO11_INMUEBLES();
            bienData.ID = bienView.ID ?? id; // Calculado Automáticamente

            if (bienData.TBANEXO11 == null)
                bienData.TBANEXO11 = new TBANEXO11();
            bienData.TBANEXO11.ID = idAnexo;

            if (bienData.TBREGISTROS_PERSONAS == null)
                bienData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            bienData.TBREGISTROS_PERSONAS.ID = (int)bienView.PersonaAfectadaId;


            bienData.PARAM_TIPO_INMUBLE = bienView.TipoInmueble;
            bienData.ID_DEPARTAMENTO = bienView.LocalizacionDepartamento;
            bienData.ID_MUNICIPIO = bienView.LocalizacionMunicipio;

            bienData.PARAM_TIPO_TENENCIA = bienView.TipoTenencia;
            bienData.NOMBRE_DIRECCION = bienView.NombreDireccion;
            bienData.AREA = (decimal?)bienView.ExtensionArea;
            bienData.PARAM_UNIDAD_AREA = bienView.ExtensionUnidadDeArea;

            //ENTORNO
            bienData.PARAM_TIPO_ENTORNO = (int?)bienView.TipoEntorno;
            bienData.PARAM_LOCALIDAD_CORREG = bienView.LocalidadCorregimientoId;
            bienData.OTRO_LOCALIDAD_CORREG = bienView.LocalidadCorregimientoNombre;
            bienData.PARAM_BARRIO_VEREDA = bienView.BarrioVeredaId;
            bienData.OTRO_BARRIO_VEREDA = bienView.BarrioVeredaNombre;


            bienData.ACTIVO = (short)((bienView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }


        #endregion

        #region Obtener
        public static ObservableCollection<clsAnexo11_BienInmueble> Obtener(int id_anexo)
        {
            ObservableCollection<clsAnexo11_BienInmueble> Bienes = new ObservableCollection<clsAnexo11_BienInmueble>();
            entInmuebles entBd = new entInmuebles();
            List<TBANEXO11_INMUEBLES> bienesData = entBd.getData(id_anexo);
            foreach (TBANEXO11_INMUEBLES bienData in bienesData)
            {
                clsAnexo11_BienInmueble bienView = new clsAnexo11_BienInmueble();

                Anexo11_Inmueble.ParseDataToView(bienData, ref bienView);

                //Reiniciar Estado
                bienView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                Bienes.Add(bienView);
            }
            return Bienes;
        }

        public static void ParseDataToView(TBANEXO11_INMUEBLES inmuebleData, ref clsAnexo11_BienInmueble inmuebleView)
        {
            inmuebleView.ID = inmuebleData.ID;

            if (inmuebleData.TBREGISTROS_PERSONAS != null)
                inmuebleView.PersonaAfectadaId = inmuebleData.TBREGISTROS_PERSONAS.ID;

            inmuebleView.TipoInmueble = inmuebleData.PARAM_TIPO_INMUBLE;
            inmuebleView.LocalizacionDepartamento = inmuebleData.ID_DEPARTAMENTO;
            inmuebleView.LocalizacionMunicipio = inmuebleData.ID_MUNICIPIO;

            inmuebleView.TipoTenencia = inmuebleData.PARAM_TIPO_TENENCIA;
            inmuebleView.NombreDireccion = inmuebleData.NOMBRE_DIRECCION;
            inmuebleView.ExtensionArea = (inmuebleData.AREA == null)? 0 : (int)inmuebleData.AREA;
            inmuebleView.ExtensionUnidadDeArea = inmuebleData.PARAM_UNIDAD_AREA;
            
            //ENTORNO
            inmuebleView.TipoEntorno = (eTipoEntorno?)inmuebleData.PARAM_TIPO_ENTORNO;
            inmuebleView.LocalidadCorregimientoId = inmuebleData.PARAM_LOCALIDAD_CORREG;
            inmuebleView.LocalidadCorregimientoNombre = inmuebleData.OTRO_LOCALIDAD_CORREG;
            inmuebleView.BarrioVeredaId = inmuebleData.PARAM_BARRIO_VEREDA;
            inmuebleView.BarrioVeredaNombre = inmuebleData.OTRO_BARRIO_VEREDA;
                        
            //bienAfectadoView.EstadoRegistro = bienAfectadoData.ACTIVO;
        }
        #endregion
    }
}

