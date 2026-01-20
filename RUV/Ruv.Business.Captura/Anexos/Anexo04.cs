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
    public class Anexo04
    {
        public const int TipoAnexo = (int)Common.eTiposAnexo.Anexo04;

        #region Guardar
        public static void Guardar(clsAnexo04 anexoSiniestro, int idValoracion, DbTransaction tran)
        {
            int idValanexo = 0;
            Anexo04.GuardarSiniestro(anexoSiniestro, idValoracion, ref idValanexo, tran);

            //Obtener Anexo
            if (idValanexo == 0)
            {
                Anexo04.ObtenerValoracionAnexo(anexoSiniestro, idValoracion, ref idValanexo, tran);
            } 

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Guardar Anexos
            int id_siniestro = (int)anexoSiniestro.ID;
            //TBANEXO04
            foreach (clsAnexo04_Victima anexoView in anexoSiniestro.Victimas)
            {
                //Guardar Anexo
                Anexo04.GuardarAnexo(anexoView, id_siniestro, idValanexo, tran);

                //Si esta eliminado continua con el siguiente
                if (anexoView.EstadoRegistro == eEstadoRegistro.Eliminado)
                    continue;

                //Actualizar Afectaciones del Anexo
                Afectaciones.Guardar((int)anexoView.ID, TipoAnexo, anexoView.Afectacion, tran);

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

        public static void ObtenerValoracionAnexo(clsAnexo04 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdAnexo = new entSiniestroPersona();
            idValanexo = entBdAnexo.GetDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);

        }

        #region Siniestro
        private static void GuardarSiniestro(clsAnexo04 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo04.ParseViewToData_Siniestro(anexoSiniestro, siniestroData);

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

        public static void ParseViewToData_Siniestro(clsAnexo04 anexo, Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
        {
            siniestroData.ID = anexo.ID ?? -1;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            if (!anexo.JefeGrupoFamiliarId.HasValue)
                throw new InvalidOperationException("No se pudo determinar el jefe del grupo familiar para el anexo");

            siniestroData.TBREGISTROS_PERSONAS.ID = anexo.JefeGrupoFamiliarId.Value;
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
        private static void GuardarAnexo(clsAnexo04_Victima anexoView, int id_siniestro, int idValanexo, DbTransaction tran)
        {
            TBANEXO4 anexoData = new TBANEXO4();
            Anexo04.ParseViewToData_Anexo(anexoView, id_siniestro, anexoData);
            entAnexo4 entBd = new entAnexo4();
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();
            
            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo4(anexoData, tran);
                    anexoView.ID = anexoData.ID;
                    if (idValanexo > 0 && anexoView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)anexoView.PersonaAfectadaId, anexoData.ID, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo4(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo4(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_Anexo(clsAnexo04_Victima anexoView, int id_siniestro, TBANEXO4 anexoData)
        {
            #region Common Anexos
            anexoData.ID = anexoView.ID ?? -1;

            if (anexoData.TBSINIESTROS_PERSONA == null)
                anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            anexoData.TBSINIESTROS_PERSONA.ID = id_siniestro;
            anexoData.ID_REGPERSONA = (int)anexoView.PersonaAfectadaId;
            anexoData.VICTIMA = Common.ParseIntToShortNullable(anexoView.VictimaDeEsteHecho);

            anexoData.AFECTADO = Common.ParseIntToShortNullable(anexoView.Afectacion.Afectado);
            if (anexoView.Afectacion != null)
                anexoData.OTRA_AFECTACION = anexoView.Afectacion.Otro;
            
            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            anexoData.DECLARACIONPREV = Common.ParseIntToShortNullable(denunciaPreviaView.SePresento);
            anexoData.PARAM_ENTIDAD_DENUNCIAPREV = denunciaPreviaView.Entidad;
            anexoData.FECHA_DENUNCIAPREV = denunciaPreviaView.Fecha;
            anexoData.ID_DEPARTAMENTO_DENUNCIAPREV = denunciaPreviaView.Departamento;
            anexoData.ID_MUNICIPIO_DENUNCIAPREV = denunciaPreviaView.Municipio;
            anexoData.NUMERO_RADICADO_DENUNCIAPREV = denunciaPreviaView.Codigo;
            #endregion
            #endregion
            
            #region Antecedentes y Hechos Posteriores a la desaparición
            anexoData.DESAPARECIDA = Common.ParseIntToShortNullable(anexoView.VictimaDesaparecida);

            anexoData.PARAM_EVENTO_ANTES_HECHO = anexoView.SePresentoEventoAnterior;
            anexoData.PARAM_EVENTO_DESPUES_HECHO = anexoView.SePresentoEventoPosterior;
            anexoData.ACTIVIDAD_EN_DESAPARICION = anexoView.ActividadAlDesaparecer;

            anexoData.MENOR_DESPROTEGIDO = Common.ParseIntToShortNullable(anexoView.QuedoMenorDesprotegido);
            anexoData.ID_MENOR_DESPROTEGIDO = anexoView.MenorDesprotegidoId;

            anexoData.BUSQUEDA_VICTIMA = Common.ParseIntToShortNullable(anexoView.HaRealizadoBusquedaDeVictima);
            anexoData.ENTIDAD_BUSQUEDA = anexoView.HarealizadoBusquedaAnteEntidad;
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

        public static clsAnexo04 ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData)
        {
            entAnexo4 entAnexo = new entAnexo4();
            clsAnexo04 anexoView = new clsAnexo04();

            //Pasar datos a la vista
            Anexo04.ParseDataToView_Siniestro(siniestroData, ref anexoView);

            //Reiniciar Estado
            anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            //Obtener las personas del anexo
            List<TBANEXO4> anexosData = entAnexo.getData(siniestroData.ID);

            foreach (TBANEXO4 anexoData in anexosData)
            {

                clsAnexo04_Victima VictimaAnexoView = new clsAnexo04_Victima();

                Anexo04.ParseDataToView_Anexo(anexoData, VictimaAnexoView);

                VictimaAnexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener Afectaciones
                VictimaAnexoView.Afectacion.TiposDeAfectacion = Afectaciones.Obtener(anexoData.ID, TipoAnexo);

                //Reiniciar Estado
                VictimaAnexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Agregar personaAfectada a la declaración
                anexoView.Victimas.Add(VictimaAnexoView);
            }
            return anexoView;
        }

        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData, ref clsAnexo04 anexoView)
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

            //A-2
            anexoView.InformacionJefeGrupo.TieneInscritaCedulaParaVotar = siniestroData.PARAM_CCVOTAR;
            anexoView.InformacionJefeGrupo.InscripcionDepartamento = siniestroData.ID_DEPARTAMENTO_VOTAR;
            anexoView.InformacionJefeGrupo.InscripcionMunicipio = siniestroData.ID_MUNICIPIO_VOTAR;

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

        public static void ParseDataToView_Anexo(TBANEXO4 anexoData, clsAnexo04_Victima anexoView)
        {
            #region Common Anexos
            anexoView.ID = anexoData.ID;

            anexoView.PersonaAfectadaId = anexoData.ID_REGPERSONA;
            anexoView.VictimaDeEsteHecho = anexoData.VICTIMA;

            if (anexoView.Afectacion == null)
                anexoView.Afectacion = new clsAnexo_Afectacion();
            anexoView.Afectacion.Afectado = anexoData.AFECTADO;
            anexoView.Afectacion.Otro = anexoData.OTRA_AFECTACION;

            #region Denuncia Previa
            if (anexoView.DenunciaPrevia == null)
                anexoView.DenunciaPrevia = new clsAnexo_DenunciaPrevia();
            anexoView.DenunciaPrevia.SePresento = anexoData.DECLARACIONPREV;
            anexoView.DenunciaPrevia.Entidad = anexoData.PARAM_ENTIDAD_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Fecha = anexoData.FECHA_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Pais = anexoData.ID_PAIS_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Departamento = anexoData.ID_DEPARTAMENTO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Municipio = anexoData.ID_MUNICIPIO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Codigo = anexoData.NUMERO_RADICADO_DENUNCIAPREV;
            #endregion
            #endregion

            #region Antecedentes y Hechos Posteriores a la desaparición
            anexoView.VictimaDesaparecida = anexoData.DESAPARECIDA;

            anexoView.SePresentoEventoAnterior = anexoData.PARAM_EVENTO_ANTES_HECHO;
            anexoView.SePresentoEventoPosterior = anexoData.PARAM_EVENTO_DESPUES_HECHO;
            anexoView.ActividadAlDesaparecer = anexoData.ACTIVIDAD_EN_DESAPARICION;

            anexoView.QuedoMenorDesprotegido = anexoData.MENOR_DESPROTEGIDO;
            anexoView.MenorDesprotegidoId = anexoData.ID_MENOR_DESPROTEGIDO;

            anexoView.HaRealizadoBusquedaDeVictima = anexoData.BUSQUEDA_VICTIMA;
            anexoView.HarealizadoBusquedaAnteEntidad = anexoData.ENTIDAD_BUSQUEDA;
            #endregion

        }
        #endregion
    }
}
