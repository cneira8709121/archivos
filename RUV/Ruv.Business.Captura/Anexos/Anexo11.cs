using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using Ruv.Data.Radicacion;
using System.Data.Common;


namespace Ruv.Business.Captura.Anexos
{
    public class Anexo11
    {
        public const int TipoAnexo = (int)Common.eTiposAnexo.Anexo11;

        #region Guardar
        public static void Guardar(clsAnexo11 anexoSiniestro, int declarante, int idValoracion, DbTransaction tran)
        {
            int idValanexo = 0;
            Anexo11.GuardarSiniestro(anexoSiniestro, declarante, idValoracion, ref idValanexo, tran);

            //Guardar Anexo:  Info Extra del Siniestro para el Anexo11
            int idAnexo11 = -1;
            Anexo11.GuardarAnexo(anexoSiniestro, ref idAnexo11, tran);

            //Obtener Anexo
            if (idValanexo == 0)
            {
                Anexo11.ObtenerValoracionAnexo(anexoSiniestro, idValoracion, ref idValanexo, tran);
            }

            #region Afectaciones o causas del Despojo
            if (anexoSiniestro.EstadoActualLote.HasValue)
            {
                clsAnexo_Afectacion afectacionAnexo11 = new clsAnexo_Afectacion();
                afectacionAnexo11.TiposDeAfectacion = new List<int>();
                afectacionAnexo11.TiposDeAfectacion.Add((int)anexoSiniestro.EstadoActualLote);
                afectacionAnexo11.Otro = null;

                //Actualizar Afectaciones del Anexo
                Afectaciones.Guardar(idAnexo11, TipoAnexo, afectacionAnexo11, tran);
            }
            #endregion

            //Actualizar Muebles
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.SinModificaciones && anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
            {
                foreach (clsAnexo11_BienMueble bienView in anexoSiniestro.BienesMuebles)
                {
                    Anexo11_Mueble.Guardar(bienView, idAnexo11, idValanexo, tran);
                    //Reiniciar EstadoRegistro
                    if (bienView.EstadoRegistro != eEstadoRegistro.Eliminado)
                        bienView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
                }

                // Eliminar de la vista
                var deletedBienesMueblesIds = anexoSiniestro.BienesMuebles.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedBienesMueblesIds != null && deletedBienesMueblesIds.Count > 0) {
                    for (int i = 0; i < anexoSiniestro.BienesMuebles.Count; i++) {
                        if (deletedBienesMueblesIds.Contains(anexoSiniestro.BienesMuebles[i].ID))
                            anexoSiniestro.BienesMuebles.RemoveAt(i);
                    }
                }
            }

            //Actualizar Inmuebles
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.SinModificaciones && anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
            {
                foreach (clsAnexo11_BienInmueble bienView in anexoSiniestro.BienesInmuebles)
                {
                    Anexo11_Inmueble.Guardar(bienView, idAnexo11, idValanexo, tran);
                    //Reiniciar EstadoRegistro
                    if (bienView.EstadoRegistro != eEstadoRegistro.Eliminado)
                        bienView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
                }

                // Eliminar de la vista
                var deletedBienesInmueblesIds = anexoSiniestro.BienesInmuebles.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedBienesInmueblesIds != null && deletedBienesInmueblesIds.Count > 0) {
                    for (int i = 0; i < anexoSiniestro.BienesInmuebles.Count; i++) {
                        if (deletedBienesInmueblesIds.Contains(anexoSiniestro.BienesInmuebles[i].ID))
                            anexoSiniestro.BienesInmuebles.RemoveAt(i);
                    }
                }
            }

            //Actualizar Creditos
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.SinModificaciones && anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
            {
                foreach (clsAnexo11_CreditoPasivo creditoView in anexoSiniestro.CreditosPasivos)
                {
                    Anexo11_Credito.Guardar(creditoView, idAnexo11, tran);
                    //Reiniciar EstadoRegistro
                    if (creditoView.EstadoRegistro != eEstadoRegistro.Eliminado)
                        creditoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
                }

                // Eliminar de la vista
                var deletedCreditoPasivoIds = anexoSiniestro.CreditosPasivos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedCreditoPasivoIds != null && deletedCreditoPasivoIds.Count > 0) {
                    for (int i = 0; i < anexoSiniestro.CreditosPasivos.Count; i++) {
                        if (deletedCreditoPasivoIds.Contains(anexoSiniestro.CreditosPasivos[i].ID))
                            anexoSiniestro.CreditosPasivos.RemoveAt(i);
                    }
                }
            }

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;
        }

        public static void ObtenerValoracionAnexo(clsAnexo11 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdAnexo = new entSiniestroPersona();
            idValanexo = entBdAnexo.GetDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);

        }

        #region Siniestro
        private static void GuardarSiniestro(clsAnexo11 anexoSiniestro, int declarante, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo11.ParseViewToData_Siniestro(anexoSiniestro, declarante, ref siniestroData);

            //Asignar tipo hecho
            siniestroData.PARAM_TIPOHECHO = TipoAnexo;

            switch (anexoSiniestro.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    siniestroData.ACTIVO = 1;
                    entBdSiniestro.setData(siniestroData, tran);
                    anexoSiniestro.ID = siniestroData.ID;
                    if (idValoracion > 0 && anexoSiniestro.ID > 0)
                    {
                        idValanexo = entBdSiniestro.insDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBdSiniestro.updateData(siniestroData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    siniestroData.ACTIVO = 0;
                    entBdSiniestro.updateData(siniestroData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_Siniestro(clsAnexo11 anexo, int declarante, ref Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
        {
            siniestroData.ID = anexo.ID ?? -1;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            if (anexo.BienesInmuebles.Count > 0)
                siniestroData.TBREGISTROS_PERSONAS.ID = (int)anexo.BienesInmuebles.First().PersonaAfectadaId;
            else //if (anexo.BienesMuebles.Count > 0)
                //siniestroData.TBREGISTROS_PERSONAS.ID = (int)anexo.BienesMuebles.First().PersonaAfectadaId;
                if (anexo.BienesMuebles.Count > 0 && anexo.BienesMuebles.FirstOrDefault().PersonaAfectadaId.HasValue)
                {
                    siniestroData.TBREGISTROS_PERSONAS.ID = anexo.BienesMuebles.FirstOrDefault().PersonaAfectadaId.Value;
                }
                else
                {
                    //Traer el declarante
                    siniestroData.TBREGISTROS_PERSONAS.ID = declarante;
                }

            //siniestroData.TBREGISTROS_PERSONAS.ID = 4; //(int)anexo.JefeGrupoFamiliarId;
            //A-1
            //TBSINIESTROS_PERSONA siniestro = new TBSINIESTROS_PERSONA();
            siniestroData.FECHASINIESTRO = anexo.FechaYLugar.HechosFecha;
            siniestroData.ID_DEPARTAMENTO = anexo.FechaYLugar.HechosDepartamento;
            siniestroData.ID_MUNICIPIO = anexo.FechaYLugar.HechosMunicipio;
            //ENTORNO
            siniestroData.PARAM_TIPO_ENTORNO = (int?)anexo.FechaYLugar.TipoEntorno;
            siniestroData.PARAM_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoId;
            siniestroData.OTRO_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoNombre;
            siniestroData.PARAM_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaId;
            siniestroData.OTRO_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaNombre;

            siniestroData.ACTIVO = (short)((anexo.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        
        #endregion

        #region Anexo
        private static void GuardarAnexo(clsAnexo11 anexoView, ref int idAnexo11, DbTransaction tran)
        {
            TBANEXO11 anexoData = new TBANEXO11();
            int id_siniestro = (int)anexoView.ID;
            Anexo11.ParseViewToData_Anexo(anexoView, id_siniestro, ref anexoData);

            entAnexo11 entBd = new entAnexo11();
            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo11(anexoData, tran);
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo11(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo11(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }

            idAnexo11 = anexoData.ID;
        }

        public static void ParseViewToData_Anexo(clsAnexo11 anexoView,  int id_siniestro, ref TBANEXO11 anexoData)
        {
            #region Common Anexos
            //anexoData.ID = anexoView.ID ?? -1;
            anexoData.ID = anexoView.IdAnexo11 ?? -1;

            if (anexoData.TBSINIESTROS_PERSONA == null)
                anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();                     

            anexoData.TBSINIESTROS_PERSONA.ID = id_siniestro;
            
            //anexoData.FECHA_DENUNCIAPREV = (anexoView.VictimaDeEsteHecho.HasValue) ? (short)anexoView.VictimaDeEsteHecho : Common.ShortNull;

            //clsAnexo_Afectacion anexoAfectacionView = anexoView.Afectacion;
            //anexoData.AFECTADO  = (anexoAfectacionView.Afectado.HasValue) ? (short)anexoAfectacionView.Afectado : Common.ShortNull; ;
            //anexoData.OTRA_AFECTACION = anexoAfectacionView.Otro;
            
            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            anexoData.DECLARACIONPREV = (denunciaPreviaView.SePresento.HasValue) ? (short)denunciaPreviaView.SePresento : Common.ShortNull;
            anexoData.PARAM_ENTIDAD_DENUNCIAPREV = denunciaPreviaView.Entidad;
            anexoData.FECHA_DENUNCIAPREV = denunciaPreviaView.Fecha;
            anexoData.ID_PAIS_DENUNCIAPREV = denunciaPreviaView.Pais;
            anexoData.ID_DEPARTAMENTO_DENUNCIAPREV = denunciaPreviaView.Departamento;
            anexoData.ID_MUNICIPIO_DENUNCIAPREV = denunciaPreviaView.Municipio;
            anexoData.NUMERO_RADICADO_DENUNCIAPREV = denunciaPreviaView.Codigo;

            anexoData.PARAM_TIERRA_DESPOJADA = anexoView.LoteFueDespojado;
            anexoData.PARAM_TIPO_DESPOJADO = anexoView.DespojoTipo;
            anexoData.AUTOR_DESPOJADO = anexoView.DespojoQuien;
            anexoData.PARAM_SITUACION_ACT_TIERRA = anexoView.EstadoActualLote;
            anexoData.PARAM_SOL_PROTECCION = anexoView.SolicitaProteccionMuebles;
            anexoData.PROTECCION_PORQUE = anexoView.SolicitaProteccionMueblesPorque;

            #endregion

            #endregion

            anexoData.ACTIVO = (short)((anexoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        #endregion
        #endregion

        #region Obtener
        public static List<TBSINIESTROS_PERSONA> ObtenerSiniestros(int id_declaracion)
        {
            //Obtener los datos del encabezado del anexo
            entSiniestroPersona entSinPer = new entSiniestroPersona();
            List<TBSINIESTROS_PERSONA> siniestrosData = new List<TBSINIESTROS_PERSONA>();

            siniestrosData = entSinPer.getData(TipoAnexo, id_declaracion);
            return siniestrosData;
        }

        public static clsAnexo11 ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData)
        {
            entAnexo11 entAnexo = new entAnexo11();
            clsAnexo11 anexoView = new clsAnexo11();

            //Obtener las personas del anexo
            List<TBANEXO11> anexosData = entAnexo.getData(siniestroData.ID);

            foreach (TBANEXO11 anexoData in anexosData)
            {
                //Obtener datos del siniestro
                Anexo11.ParseDataToView_Siniestro(siniestroData, ref anexoView);
                
                //Obtener informacion extra del anexo11
                Anexo11.ParseDataToView_Anexo(anexoData, ref anexoView);

                //Reiniciar Estado
                anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener Muebles
                anexoView.BienesMuebles = Anexo11_Mueble.Obtener(anexoData.ID);
                
                //Obtener Inmuebles
                anexoView.BienesInmuebles = Anexo11_Inmueble.Obtener(anexoData.ID);

                anexoView.CreditosPasivos = Anexo11_Credito.Obtener(anexoData.ID);
                break;
            }
            return anexoView;
        }
         
        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData, ref clsAnexo11 anexoView)
        {
            //TBDECLARACIONES declaracionData, ref clsDeclaracion declaracionView
            anexoView.ID = siniestroData.ID;
             
            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            anexoView.JefeGrupoFamiliarId = siniestroData.TBREGISTROS_PERSONAS.ID;

            //A-1
            anexoView.FechaYLugar.HechosFecha = siniestroData.FECHASINIESTRO;
            anexoView.FechaYLugar.HechosDepartamento = siniestroData.ID_DEPARTAMENTO;
            anexoView.FechaYLugar.HechosMunicipio = siniestroData.ID_MUNICIPIO;
            //ENTORNO
            anexoView.FechaYLugar.TipoEntorno = (eTipoEntorno?)siniestroData.PARAM_TIPO_ENTORNO;
            anexoView.FechaYLugar.LocalidadCorregimientoId = siniestroData.PARAM_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.LocalidadCorregimientoNombre = siniestroData.OTRO_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.BarrioVeredaId = siniestroData.PARAM_BARRIO_VEREDA;
            anexoView.FechaYLugar.BarrioVeredaNombre = siniestroData.OTRO_BARRIO_VEREDA;

            //anexoView.EstadoRegistro = (Int16)siniestroData.ACTIVO;
        }

        public static void ParseDataToView_Anexo(TBANEXO11 anexoData, ref clsAnexo11 anexoView)
        {
            #region Common Anexos
            //anexoView.ID = anexoData.ID;
            anexoView.IdAnexo11 = anexoData.ID;
   
            #region Denuncia Previa
            anexoView.DenunciaPrevia.SePresento = anexoData.DECLARACIONPREV;
            anexoView.DenunciaPrevia.Entidad = anexoData.PARAM_ENTIDAD_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Fecha = anexoData.FECHA_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Pais = anexoData.ID_PAIS_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Departamento = anexoData.ID_DEPARTAMENTO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Municipio = anexoData.ID_MUNICIPIO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Codigo = anexoData.NUMERO_RADICADO_DENUNCIAPREV;
            #endregion

            #endregion


            #region Despojo
            anexoView.LoteFueDespojado = anexoData.PARAM_TIERRA_DESPOJADA;
            anexoView.DespojoTipo = anexoData.PARAM_TIPO_DESPOJADO;
            anexoView.DespojoQuien = anexoData.AUTOR_DESPOJADO;
            anexoView.EstadoActualLote = anexoData.PARAM_SITUACION_ACT_TIERRA;
            anexoView.SolicitaProteccionMuebles = anexoData.PARAM_SOL_PROTECCION;
            anexoView.SolicitaProteccionMueblesPorque = anexoData.PROTECCION_PORQUE;

            #endregion

            //anexoView.EstadoRegistro = anexoData.ACTIVO;
        }
        #endregion
    }
}
