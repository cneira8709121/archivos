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
    public class Anexo01 
    {
        public const int TipoAnexo =  (int)Common.eTiposAnexo.Anexo01;

        #region Guardar
        public static void Guardar(clsAnexo01 anexoSiniestro, int idValoracion, DbTransaction tran)
        {
            int idValanexo = 0;
            Anexo01.Guardar_Siniestro(anexoSiniestro, idValoracion, ref idValanexo, tran);

            if (idValanexo == 0) {
                Anexo01.obtenerValoracionAnexo(anexoSiniestro,idValoracion,ref idValanexo,tran);
            }

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Guardar Anexos
            int id_siniestro = (int)anexoSiniestro.ID;
            //TBANEXO01
            foreach (clsAnexo01_Victima anexoView in anexoSiniestro.Victimas)
            {
                Anexo01.GuardarAnexo(anexoView, id_siniestro, idValanexo, tran);

                //Si esta eliminado continua con el siguiente
                if (anexoView.EstadoRegistro == eEstadoRegistro.Eliminado)
                    continue;

                //Actualizar Afectaciones del Anexo
                Afectaciones.Guardar((int)anexoView.ID, TipoAnexo, anexoView.Afectacion, tran);

                #region Guardar BienAfectado Anexo01
                int id_anexo = (int)anexoView.ID;
                foreach (clsAnexo01_Victima_Bien Victima_Bien in anexoView.Bienes)
                {
                    Anexo01_BienAfectado.Guardar(Victima_Bien, id_anexo, tran);

                    //Reiniciar EstadoRegistro
                    if (Victima_Bien.EstadoRegistro != eEstadoRegistro.Eliminado)
                        Victima_Bien.EstadoRegistro = eEstadoRegistro.SinModificaciones;
                }
                #endregion

                //Eliminar de la vista
                var bienesBorrados = from a in anexoView.Bienes
                                     where a.EstadoRegistro == eEstadoRegistro.Eliminado
                                     select a;
                foreach (clsAnexo01_Victima_Bien Victima_Bien in bienesBorrados)
                    anexoView.Bienes.Remove(Victima_Bien);

                //Reiniciar EstadoRegistro
                if (anexoView.EstadoRegistro != eEstadoRegistro.Eliminado)
                    anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
            }
            #endregion

            // Eliminar víctimas de la vista
            var deletedVictimsIds = anexoSiniestro.Victimas.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
            if (deletedVictimsIds != null && deletedVictimsIds.Count > 0) {
                for (int i = 0; i < anexoSiniestro.Victimas.Count; i++) { 
                    if (deletedVictimsIds.Contains(anexoSiniestro.Victimas[i].ID))
                        anexoSiniestro.Victimas.RemoveAt(i);
                }
            }
        }

        public static void obtenerValoracionAnexo(clsAnexo01 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBDAnexo = new entSiniestroPersona();
            TBSINIESTROS_PERSONA AnexoData = new TBSINIESTROS_PERSONA();
            idValanexo = entBDAnexo.GetDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);
        }

        #region Siniestro
        private static void Guardar_Siniestro(clsAnexo01 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo01.ParseViewToData_Siniestro(anexoSiniestro, siniestroData);

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

        private static void ParseViewToData_Siniestro(clsAnexo01 anexo, Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
        {
            siniestroData.ID = anexo.ID ?? -1;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            if (!anexo.JefeGrupoFamiliarId.HasValue)
                throw new InvalidOperationException("No se pudo determinar el jefe del grupo familiar para el anexo");

            siniestroData.TBREGISTROS_PERSONAS.ID = anexo.JefeGrupoFamiliarId.Value;

            //A-1
            siniestroData.FECHASINIESTRO = anexo.FechaYLugar.HechosFecha;
            siniestroData.ID_DEPARTAMENTO = anexo.FechaYLugar.HechosDepartamento;
            siniestroData.ID_MUNICIPIO = anexo.FechaYLugar.HechosMunicipio;
            //ENTORNO
            siniestroData.PARAM_TIPO_ENTORNO = (int?)anexo.FechaYLugar.TipoEntorno;
            siniestroData.PARAM_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoId;
            siniestroData.OTRO_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoNombre;
            siniestroData.PARAM_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaId;
            siniestroData.OTRO_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaNombre;

            //A-2
            siniestroData.PARAM_CCVOTAR = anexo.InformacionJefeGrupo.TieneInscritaCedulaParaVotar;
            if (siniestroData.PARAM_CCVOTAR < 0)
                siniestroData.PARAM_CCVOTAR = null;
            siniestroData.ID_DEPARTAMENTO_VOTAR = anexo.InformacionJefeGrupo.InscripcionDepartamento;
            siniestroData.ID_MUNICIPIO_VOTAR = anexo.InformacionJefeGrupo.InscripcionMunicipio;

            //A-3
            siniestroData.ID_DPTO_ESTUDIO_HIJOS = anexo.InformacionJefeGrupo.HijosEstudianDepartamento;
            siniestroData.ID_MPIO_ESTUDIO_HIJOS = anexo.InformacionJefeGrupo.HijosEstudianMunicipio;
            siniestroData.INSTITUCION_EDUCATIVA = anexo.InformacionJefeGrupo.HijosEstudianInstitucion;

            //A-4
            siniestroData.PARAM_ENCUESTASISBEN = anexo.InformacionJefeGrupo.EncuestaSisben;
            if (siniestroData.PARAM_ENCUESTASISBEN < 0)
                siniestroData.PARAM_ENCUESTASISBEN = null;
            siniestroData.ID_DPTO_ENCUESTASISBEN = anexo.InformacionJefeGrupo.EncuestaSisbenDepartamento;
            siniestroData.ID_MPIO_ENCUESTASISBEN = anexo.InformacionJefeGrupo.EncuestaSisbenMunicipio;
            siniestroData.NIVEL_SISBEN = (anexo.InformacionJefeGrupo.EncuestaSisbenNivel.HasValue) ? (short)anexo.InformacionJefeGrupo.EncuestaSisbenNivel : Common.ShortNull;

            //A-5
            siniestroData.PARAM_FAMILIASACCION = anexo.InformacionJefeGrupo.InscritoEnPrograma;
            if (siniestroData.PARAM_FAMILIASACCION < 0)
                siniestroData.PARAM_FAMILIASACCION = null;
            siniestroData.ID_DPTO_FAMILIASACCION = anexo.InformacionJefeGrupo.InscritoEnProgramaDepartamento;
            siniestroData.ID_MPIO_FAMILIASACCION = anexo.InformacionJefeGrupo.InscritoEnProgramaMunicipio;
            siniestroData.ENTIDADCOBRA = anexo.InformacionJefeGrupo.InscritoEnProgramaEntidadDondeLabora;
            
            //A-6            
            siniestroData.PARAM_SISTEMASALUD = anexo.InformacionJefeGrupo.VinculadoSistemaSalud;
            if (siniestroData.PARAM_SISTEMASALUD < 0)
                siniestroData.PARAM_SISTEMASALUD = null;
            siniestroData.ID_DPTO_SISTEMASALUD = anexo.InformacionJefeGrupo.VinculadoSistemaSaludDepartamento;
            siniestroData.ID_MPIO_SISTEMASALUD = anexo.InformacionJefeGrupo.VinculadoSistemaSaludMunicipio;
            siniestroData.PARAM_SISTEMASALUD = anexo.InformacionJefeGrupo.VinculadoSistemaSaludTipoAfiliacion;

            //A-7
            siniestroData.ID_DPTO_TRABAJO = anexo.InformacionJefeGrupo.LugarLaboralDepartamento;
            siniestroData.ID_MPIO_TRABAJO = anexo.InformacionJefeGrupo.LugarLaboralMunicipio;
            siniestroData.NOMBRE_EMPLEADOR = anexo.InformacionJefeGrupo.LugarLaboralEmpleador;

            siniestroData.ACTIVO = (short)((anexo.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }
        #endregion

        #region Anexo
        private static void GuardarAnexo(clsAnexo01_Victima anexoView, int id_siniestro, int idValanexo, DbTransaction tran)
        {
            entAnexo1 entBd = new entAnexo1();
            TBANEXO1 anexoData = new TBANEXO1();
            Anexo01.ParseViewToData_Anexo(anexoView, id_siniestro, anexoData);
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo1(anexoData, tran);
                    anexoView.ID = anexoData.ID;
                    if (idValanexo > 0 && anexoView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)anexoView.PersonaAfectadaId, anexoData.ID, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo1(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo1(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_Anexo(clsAnexo01_Victima anexoView, int id_siniestro, TBANEXO1 anexoData)
        {
            #region Common Anexos
            anexoData.ID = anexoView.ID ?? -1;

            if (anexoData.TBSINIESTROS_PERSONA == null)
                anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            anexoData.TBSINIESTROS_PERSONA.ID = id_siniestro;

            if (anexoData.TBREGISTROS_PERSONAS == null)
                anexoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            anexoData.TBREGISTROS_PERSONAS.ID = (int)anexoView.PersonaAfectadaId;

            anexoData.VICTIMA = (anexoView.VictimaDeEsteHecho.HasValue) ? (short)anexoView.VictimaDeEsteHecho : Common.ShortNull;

            clsAnexo_Afectacion anexoAfectacionView = anexoView.Afectacion;
            anexoData.AFECTADO = (anexoAfectacionView.Afectado.HasValue) ? (short)anexoAfectacionView.Afectado : Common.ShortNull; ;
            anexoData.OTRA_AFECTACION = anexoAfectacionView.Otro;
            
            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            anexoData.DECLARACIONPREV = (denunciaPreviaView.SePresento.HasValue) ? (short)denunciaPreviaView.SePresento : Common.ShortNull;
            anexoData.PARAM_ENTIDAD_DENUNCIAPREV = denunciaPreviaView.Entidad;
            anexoData.FECHA_DENUNCIAPREV = denunciaPreviaView.Fecha;
            anexoData.ID_PAIS_DENUNCIAPREV = denunciaPreviaView.Pais;
            anexoData.ID_DEPARTAMENTO_DENUNCIAPREV = denunciaPreviaView.Departamento;
            anexoData.ID_MUNICIPIO_DENUNCIAPREV = denunciaPreviaView.Municipio;
            anexoData.NUMERO_RADICADO_DENUNCIAPREV = denunciaPreviaView.Codigo;
            #endregion

            #endregion

            //A13 Atencion Medica
            #region Denuncia Previa
            anexoData.ATENCION_MEDICA = (anexoView.AtencionMedicaRecibio.HasValue) ? (short)anexoView.AtencionMedicaRecibio : Common.ShortNull;
            anexoData.DETALLE_ATENCION_MEDICA = anexoView.AtencionEntidadMedica;
            anexoData.ID_DEPARTAMENTO_ATENCIONMED = anexoView.AtencionMedicaDepartamento;
            anexoData.ID_MUNICIPIO_ATENCIONMED = anexoView.AtencionMedicaMunicipio;
            #endregion


            if (anexoView.Afectacion != null)
                anexoData.OTRA_AFECTACION = anexoView.Afectacion.Otro;

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


        public static clsAnexo01 ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData)
        {
            clsAnexo01 anexoView = new clsAnexo01();
            entAnexo1 entAnexo = new entAnexo1();

            //Pasar datos a la vista
            Anexo01.ParseDataToView_Siniestro(siniestroData, ref anexoView);

            //Reiniciar Estado
            anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            //Obtener las personas del anexo
            List<TBANEXO1> anexosData = entAnexo.getData(siniestroData.ID);

            foreach (TBANEXO1 anexoData in anexosData)
            {
                clsAnexo01_Victima VictimaAnexoView = new clsAnexo01_Victima();

                Anexo01.ParseDataToView_Anexo(anexoData, VictimaAnexoView);
                VictimaAnexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener Afectaciones
                VictimaAnexoView.Afectacion.TiposDeAfectacion = Afectaciones.Obtener(anexoData.ID, TipoAnexo);
                
                //Reiniciar Estado
                VictimaAnexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener Bienes
                VictimaAnexoView.Bienes = Anexo01_BienAfectado.Obtener(anexoData.ID);

                //Agregar personaAfectada a la declaración
                anexoView.Victimas.Add(VictimaAnexoView);
            }
            return anexoView;
        }

        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData, ref clsAnexo01 anexoView)
        {
            //TBDECLARACIONES declaracionData, ref clsDeclaracion declaracionView
            anexoView.ID = siniestroData.ID;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            anexoView.JefeGrupoFamiliarId = siniestroData.TBREGISTROS_PERSONAS.ID;

            //A-1
            anexoView.FechaYLugar.HechosFecha = siniestroData.FECHASINIESTRO;
            anexoView.FechaYLugar.HechosDepartamento = siniestroData.ID_DEPARTAMENTO ;
            anexoView.FechaYLugar.HechosMunicipio = siniestroData.ID_MUNICIPIO;
            //ENTORNO
            anexoView.FechaYLugar.TipoEntorno = (eTipoEntorno?)siniestroData.PARAM_TIPO_ENTORNO;
            anexoView.FechaYLugar.LocalidadCorregimientoId = siniestroData.PARAM_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.LocalidadCorregimientoNombre = siniestroData.OTRO_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.BarrioVeredaId = siniestroData.PARAM_BARRIO_VEREDA;
            anexoView.FechaYLugar.BarrioVeredaNombre = siniestroData.OTRO_BARRIO_VEREDA;
            
            //A-2
            anexoView.InformacionJefeGrupo.TieneInscritaCedulaParaVotar = siniestroData.PARAM_CCVOTAR;
            anexoView.InformacionJefeGrupo.InscripcionDepartamento = siniestroData.ID_DEPARTAMENTO_VOTAR;
            anexoView.InformacionJefeGrupo.InscripcionMunicipio =siniestroData.ID_MUNICIPIO_VOTAR;

            //A-3
            anexoView.InformacionJefeGrupo.HijosEstudianDepartamento = siniestroData.ID_DPTO_ESTUDIO_HIJOS;
            anexoView.InformacionJefeGrupo.HijosEstudianMunicipio = siniestroData.ID_MPIO_ESTUDIO_HIJOS;
            anexoView.InformacionJefeGrupo.HijosEstudianInstitucion = siniestroData.INSTITUCION_EDUCATIVA;

            //A-4
            anexoView.InformacionJefeGrupo.EncuestaSisben = siniestroData.PARAM_ENCUESTASISBEN;
            anexoView.InformacionJefeGrupo.EncuestaSisbenDepartamento = siniestroData.ID_DPTO_ENCUESTASISBEN;
            anexoView.InformacionJefeGrupo.EncuestaSisbenMunicipio = siniestroData.ID_MPIO_ENCUESTASISBEN;
            anexoView.InformacionJefeGrupo.EncuestaSisbenNivel = siniestroData.NIVEL_SISBEN;

            //A-5
            anexoView.InformacionJefeGrupo.InscritoEnPrograma = siniestroData.PARAM_FAMILIASACCION;
            anexoView.InformacionJefeGrupo.InscritoEnProgramaDepartamento = siniestroData.ID_DPTO_FAMILIASACCION;
            anexoView.InformacionJefeGrupo.InscritoEnProgramaMunicipio = siniestroData.ID_MPIO_FAMILIASACCION;
            anexoView.InformacionJefeGrupo.InscritoEnProgramaEntidadDondeLabora = siniestroData.ENTIDADCOBRA;

            //A-6            
            anexoView.InformacionJefeGrupo.VinculadoSistemaSalud = siniestroData.PARAM_SISTEMASALUD;
            anexoView.InformacionJefeGrupo.VinculadoSistemaSaludDepartamento = siniestroData.ID_DPTO_SISTEMASALUD;
            anexoView.InformacionJefeGrupo.VinculadoSistemaSaludMunicipio = siniestroData.ID_MPIO_SISTEMASALUD;
            anexoView.InformacionJefeGrupo.VinculadoSistemaSaludTipoAfiliacion = siniestroData.PARAM_SISTEMASALUD;

            //A-7
            anexoView.InformacionJefeGrupo.LugarLaboralDepartamento = siniestroData.ID_DPTO_TRABAJO;
            anexoView.InformacionJefeGrupo.LugarLaboralMunicipio = siniestroData.ID_MPIO_TRABAJO;
            anexoView.InformacionJefeGrupo.LugarLaboralEmpleador = siniestroData.NOMBRE_EMPLEADOR;

            //anexoView.EstadoRegistro = (Int16)siniestroData.ACTIVO;
        }
        
        public static void ParseDataToView_Anexo(TBANEXO1 anexoData, clsAnexo01_Victima anexoView)
        {
            #region Common Anexos

            anexoView.ID = anexoData.ID;

            //A9
            anexoView.VictimaDeEsteHecho = anexoData.VICTIMA;


            //A10 TODO: Afectacion             
            anexoView.Afectacion.Afectado = anexoData.AFECTADO;
            anexoView.Afectacion.Otro = anexoData.OTRA_AFECTACION;

            //A11 Denuncia Previa
            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            denunciaPreviaView.SePresento = anexoData.DECLARACIONPREV;
            denunciaPreviaView.Entidad = anexoData.PARAM_ENTIDAD_DENUNCIAPREV;
            denunciaPreviaView.Fecha = anexoData.FECHA_DENUNCIAPREV;
            denunciaPreviaView.Pais = anexoData.ID_PAIS_DENUNCIAPREV;
            denunciaPreviaView.Departamento = anexoData.ID_DEPARTAMENTO_DENUNCIAPREV;
            denunciaPreviaView.Municipio = anexoData.ID_MUNICIPIO_DENUNCIAPREV;
            denunciaPreviaView.Codigo = anexoData.NUMERO_RADICADO_DENUNCIAPREV;
            #endregion

            if (anexoData.TBREGISTROS_PERSONAS != null)
                anexoView.PersonaAfectadaId = anexoData.TBREGISTROS_PERSONAS.ID;

            #endregion

             //A13 Atencion Medica
            #region Denuncia Previa
            anexoView.AtencionMedicaRecibio = anexoData.ATENCION_MEDICA;
            anexoView.AtencionEntidadMedica = anexoData.DETALLE_ATENCION_MEDICA;
            anexoView.AtencionMedicaDepartamento = anexoData.ID_DEPARTAMENTO_ATENCIONMED;
            anexoView.AtencionMedicaMunicipio = anexoData.ID_MUNICIPIO_ATENCIONMED;
            #endregion

            //anexoView.EstadoRegistro = anexoData.ACTIVO;
        }
        #endregion
    }
}