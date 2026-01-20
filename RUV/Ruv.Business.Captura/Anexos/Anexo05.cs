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
    public class Anexo05
    {
        public const int TipoAnexo = (int)Common.eTiposAnexo.Anexo05;

        #region Guardar
        public static void Guardar(clsAnexo05 anexoSiniestro, int idValoracion, DbTransaction tran)
        {
            int idValanexo = 0;
            Anexo05.GuardarSiniestro(anexoSiniestro, idValoracion, ref idValanexo, tran);

            //Guardar Anexo:  Info Extra del Siniestro para el Anexo5
            int idAnexo05 = -1;
            Anexo05.GuardarAnexo(anexoSiniestro, ref idAnexo05, tran);

            //Obtener Anexo
            if (idValanexo == 0)
            {
                Anexo05.ObtenerValoracionAnexo(anexoSiniestro, idValoracion, ref idValanexo, tran);
            }
            //Copnsulta el Anexo
            //int idAnex05 = -1;
            //Anexo05.GuardarAnexo(anexoSiniestro, ref idAnex05, tran);

            #region Afectaciones o causas del Desplazamiento

            clsAnexo_Afectacion afectacionAnexo05 = new clsAnexo_Afectacion();
            afectacionAnexo05.TiposDeAfectacion = anexoSiniestro.CausaDesplazamiento;
            
            afectacionAnexo05.Otro = anexoSiniestro.CausaDesplazamientoOtro;

            //Actualizar Afectaciones del Anexo
            Afectaciones.Guardar(idAnexo05, TipoAnexo, afectacionAnexo05, tran);

            #endregion

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Guardar Desplazados
            int id_siniestro = (int)anexoSiniestro.ID;
            //TBANEXO04
            int id_jefeHogar = (int)anexoSiniestro.JefeGrupoFamiliarId;
            foreach (clsAnexo05_Victima desplazadoView in anexoSiniestro.Victimas)
            {
                //Guardar Desplazado
                Anexo05_Desplazado.Guardar(desplazadoView, idAnexo05, id_jefeHogar, idValanexo, tran);

                //Reiniciar EstadoRegistro
                if (desplazadoView.EstadoRegistro != eEstadoRegistro.Eliminado)
                    desplazadoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
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

        public static void ObtenerValoracionAnexo(clsAnexo05 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdAnexo = new entSiniestroPersona();

            TBSINIESTROS_PERSONA AnexoData = new TBSINIESTROS_PERSONA();
            idValanexo = entBdAnexo.GetDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);

        }

        #region Siniestro
        private static void GuardarSiniestro(clsAnexo05 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo05.ParseViewToData_Siniestro(anexoSiniestro, siniestroData);

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

        public static void ParseViewToData_Siniestro(clsAnexo05 anexo, Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
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
        private static void GuardarAnexo(clsAnexo05 anexoView, ref int idAnexo05, DbTransaction tran)
        {
            TBANEXO5 anexoData = new TBANEXO5();
            Anexo05.ParseViewToData_Anexo(anexoView, ref anexoData);

            entAnexo5 entBd = new entAnexo5();
            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo5(anexoData, tran);
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo5(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo5(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }

            idAnexo05 = anexoData.ID;
        }

        public static void ParseViewToData_Anexo(clsAnexo05 anexoView, ref TBANEXO5 anexoData)
        {
            //anexoData.ID = -1;
            anexoData.ID = anexoView.IdAnexo5 ?? -1;

            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            anexoData.DECLARACIONPREV = Common.ParseIntToShortNullable(denunciaPreviaView.SePresento);
            anexoData.PARAM_ENTIDAD_DENUNCIAPREV = denunciaPreviaView.Entidad;
            anexoData.OTRA_ENTIDAD_DENUNCIAPREV =  denunciaPreviaView.OtraEntidad;
            anexoData.FECHA_DENUNCIAPREV = denunciaPreviaView.Fecha;
            anexoData.ID_PAIS_DENUNCIAPREV = denunciaPreviaView.Pais;
            anexoData.ID_DEPARTAMENTO_DENUNCIAPREV = denunciaPreviaView.Departamento;
            anexoData.ID_MUNICIPIO_DENUNCIAPREV = denunciaPreviaView.Municipio;
            anexoData.NUMERO_RADICADO_DENUNCIAPREV = denunciaPreviaView.Codigo;
            #endregion
                        
            anexoData.DESPLAZAMIENTO_OTRO = anexoView.CausaDesplazamientoOtro;

            anexoData.PARAM_TIPO_DESPLAZAMIENTO = anexoView.TipoDesplazamiento;
            anexoData.PARAM_NUEVO_TIPO_DESPLAZAMIENTO = anexoView.NuevoTipoDesplazamiento;
            anexoData.ESEXILIO = anexoView.EsExilio;

            #region Información de Arribo
            anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            anexoData.TBSINIESTROS_PERSONA.ID = (int)anexoView.ID;
            anexoData.FECHA_ARRIBO = anexoView.InformacionDeArribo.HechosFecha;
            anexoData.ID_PAIS_ARRIBO = anexoView.InformacionDeArribo.HechosPais;
            anexoData.ID_DEPARTAMENTO_ARRIBO = anexoView.InformacionDeArribo.HechosDepartamento;
            anexoData.ID_MUNICIPIO_ARRIBO = anexoView.InformacionDeArribo.HechosMunicipio;

            //ENTORNO
            anexoData.PARAM_TIPO_ENTORNO_ARRI = (int?)anexoView.InformacionDeArribo.TipoEntorno;
            anexoData.PARAM_LOCALIDAD_CORREG_ARRI = anexoView.InformacionDeArribo.LocalidadCorregimientoId;
            anexoData.OTRO_LOCALIDAD_CORREG_ARRI = anexoView.InformacionDeArribo.LocalidadCorregimientoNombre;
            anexoData.PARAM_BARRIO_VEREDA_ARRI = anexoView.InformacionDeArribo.BarrioVeredaId;
            anexoData.OTRO_BARRIO_VEREDA_ARRI = anexoView.InformacionDeArribo.BarrioVeredaNombre;
            #endregion

            #region Información de Retorno y Reubicación
            anexoData.PARAM_DESEOHOGAR = anexoView.DeseoDelHogar;
            anexoData.ID_PAIS_REUBICACION = anexoView.DeseaUbicarseEn.HechosPais;
            anexoData.ID_DPTO_REUBICACION = anexoView.DeseaUbicarseEn.HechosDepartamento;
            anexoData.ID_MUNICIPIO_REUBICACION = anexoView.DeseaUbicarseEn.HechosMunicipio;

            //ENTORNO
            anexoData.PARAM_TIPO_ENTORNO_REUB = (int?)anexoView.DeseaUbicarseEn.TipoEntorno;
            anexoData.PARAM_LOCALIDAD_CORREG_REUB = anexoView.DeseaUbicarseEn.LocalidadCorregimientoId;
            anexoData.OTRO_LOCALIDAD_CORREG_REUB = anexoView.DeseaUbicarseEn.LocalidadCorregimientoNombre;
            anexoData.PARAM_BARRIO_VEREDA_REUB = anexoView.DeseaUbicarseEn.BarrioVeredaId;
            anexoData.OTRO_BARRIO_VEREDA_REUB = anexoView.DeseaUbicarseEn.BarrioVeredaNombre;

            anexoData.TIEMPO_RESIDENCIA_ANOS = Common.ParseIntToShortNullable(anexoView.TiempoResidenciaEnLugarExpulsorAños);
            anexoData.TIEMPO_RESIDENCIA_DIAS = Common.ParseIntToShortNullable(anexoView.TiempoResidenciaEnLugarExpulsorDias);
            anexoData.TIEMPO_RESIDENCIA_MESES = Common.ParseIntToShortNullable(anexoView.TiempoResidenciaEnLugarExpulsorMeses);
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

        public static clsAnexo05 ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData)
        {
            entAnexo5 entAnexo = new entAnexo5();
            clsAnexo05 anexoView = new clsAnexo05();

            //Obtener las personas del anexo
            List<TBANEXO5> anexosData = entAnexo.getData(siniestroData.ID);

            foreach (TBANEXO5 anexoData in anexosData)
            {
                //Obtener datos del siniestro
                Anexo05.ParseDataToView_Siniestro(siniestroData, ref anexoView);

                //Reiniciar Estado
                anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener informacion extra del anexo05
                Anexo05.ParseDataToView_Anexo(anexoData, ref anexoView);
                
                int? id_jefe_hogar = null;
                //Obtener Desplazados
                anexoView.Victimas = Anexo05_Desplazado.Obtener(anexoData.ID, ref id_jefe_hogar);

                //Actualizar jefe de hogar de los desplazados
                if (id_jefe_hogar != null)
                    anexoView.JefeGrupoFamiliarId = (int?)id_jefe_hogar;
                break;
            }
            return anexoView;
        }

        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData, ref clsAnexo05 anexoView)
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

        public static void ParseDataToView_Anexo(TBANEXO5 anexoData, ref clsAnexo05 anexoView)
        {
            //anexoView.ID = anexoData.ID;
            anexoView.IdAnexo5 = anexoData.ID;

            #region Denuncia Previa
            if (anexoView.DenunciaPrevia == null)
                anexoView.DenunciaPrevia = new clsAnexo_DenunciaPrevia();
            anexoView.DenunciaPrevia.SePresento = anexoData.DECLARACIONPREV;
            anexoView.DenunciaPrevia.Entidad = anexoData.PARAM_ENTIDAD_DENUNCIAPREV;
            anexoView.DenunciaPrevia.OtraEntidad = anexoData.OTRA_ENTIDAD_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Fecha = anexoData.FECHA_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Pais = anexoData.ID_PAIS_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Departamento = anexoData.ID_DEPARTAMENTO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Municipio = anexoData.ID_MUNICIPIO_DENUNCIAPREV;
            anexoView.DenunciaPrevia.Codigo = anexoData.NUMERO_RADICADO_DENUNCIAPREV;
            #endregion

            anexoView.CausaDesplazamiento = Afectaciones.Obtener(anexoData.ID, TipoAnexo);
            anexoView.CausaDesplazamientoOtro= anexoData.DESPLAZAMIENTO_OTRO;

            anexoView.TipoDesplazamiento = anexoData.PARAM_TIPO_DESPLAZAMIENTO;
            anexoView.NuevoTipoDesplazamiento = anexoData.PARAM_NUEVO_TIPO_DESPLAZAMIENTO;
            anexoView.EsExilio = anexoData.ESEXILIO;

            #region Información de Arribo
            anexoView.InformacionDeArribo.HechosFecha = anexoData.FECHA_ARRIBO;
            anexoView.InformacionDeArribo.HechosPais = anexoData.ID_PAIS_ARRIBO;
            anexoView.InformacionDeArribo.HechosDepartamento = anexoData.ID_DEPARTAMENTO_ARRIBO;
            anexoView.InformacionDeArribo.HechosMunicipio = anexoData.ID_MUNICIPIO_ARRIBO;

            //ENTORNO
            anexoView.InformacionDeArribo.TipoEntorno = (eTipoEntorno?)anexoData.PARAM_TIPO_ENTORNO_ARRI;
            anexoView.InformacionDeArribo.LocalidadCorregimientoId = anexoData.PARAM_LOCALIDAD_CORREG_ARRI;
             anexoView.InformacionDeArribo.LocalidadCorregimientoNombre = anexoData.OTRO_LOCALIDAD_CORREG_ARRI ;
            anexoView.InformacionDeArribo.BarrioVeredaId = anexoData.PARAM_BARRIO_VEREDA_ARRI ;
            anexoView.InformacionDeArribo.BarrioVeredaNombre = anexoData.OTRO_BARRIO_VEREDA_ARRI;
            #endregion

            #region Información de Retorno y Reubicación
            anexoView.DeseoDelHogar = anexoData.PARAM_DESEOHOGAR;
            anexoView.DeseaUbicarseEn.HechosPais = anexoData.ID_PAIS_REUBICACION;
            anexoView.DeseaUbicarseEn.HechosDepartamento = anexoData.ID_DPTO_REUBICACION;
            anexoView.DeseaUbicarseEn.HechosMunicipio = anexoData.ID_MUNICIPIO_REUBICACION;

            //ENTORNO
            anexoView.DeseaUbicarseEn.TipoEntorno = (eTipoEntorno?)anexoData.PARAM_TIPO_ENTORNO_REUB;
            anexoView.DeseaUbicarseEn.LocalidadCorregimientoId = anexoData.PARAM_LOCALIDAD_CORREG_REUB;
            anexoView.DeseaUbicarseEn.LocalidadCorregimientoNombre = anexoData.OTRO_LOCALIDAD_CORREG_REUB;
            anexoView.DeseaUbicarseEn.BarrioVeredaId = anexoData.PARAM_BARRIO_VEREDA_REUB;
            anexoView.DeseaUbicarseEn.BarrioVeredaNombre = anexoData.OTRO_BARRIO_VEREDA_REUB;

            anexoView.TiempoResidenciaEnLugarExpulsorAños = Common.ParseIntToShortNullable(anexoData.TIEMPO_RESIDENCIA_ANOS);
            anexoView.TiempoResidenciaEnLugarExpulsorDias = Common.ParseIntToShortNullable(anexoData.TIEMPO_RESIDENCIA_DIAS);
            anexoView.TiempoResidenciaEnLugarExpulsorMeses = Common.ParseIntToShortNullable(anexoData.TIEMPO_RESIDENCIA_MESES);
            #endregion

        }
        #endregion
    }
}
