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
    public class Anexo08
    {
        public const int TipoAnexo = (int)Common.eTiposAnexo.Anexo08;

        #region Guardar
        public static void Guardar(clsAnexo08 anexoSiniestro, int idValoracion, DbTransaction tran)
        {
            int idValanexo = 0;
            Anexo08.GuardarSiniestro(anexoSiniestro, idValoracion, ref idValanexo, tran);

            //Obtener Anexo
            if (idValanexo == 0)
            {
                Anexo08.ObtenerValoracionAnexo(anexoSiniestro, idValoracion, ref idValanexo, tran);
            }

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Guardar Anexos
            int id_siniestro = (int)anexoSiniestro.ID;
            //TBANEXO04
            foreach (clsAnexo08_Victima anexoView in anexoSiniestro.Victimas)
            {
                //Guardar Anexo
                Anexo08.GuardarAnexo(anexoView, id_siniestro, idValanexo, tran);

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


        public static void ObtenerValoracionAnexo(clsAnexo08 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdAnexo = new entSiniestroPersona();
            idValanexo = entBdAnexo.GetDataValoracionAnexo(idValoracion, (int)anexoSiniestro.ID, tran);

        }
        #region Siniestro
        private static void GuardarSiniestro(clsAnexo08 anexoSiniestro, int idValoracion, ref int idValanexo, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo08.ParseViewToData_Siniestro(anexoSiniestro, siniestroData);

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

        public static void ParseViewToData_Siniestro(clsAnexo08 anexo, Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
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
        private static void GuardarAnexo(clsAnexo08_Victima anexoView, int id_siniestro, int idValanexo, DbTransaction tran)
        {
            TBANEXO8 anexoData = new TBANEXO8();
            Anexo08.ParseViewToData_Anexo(anexoView, id_siniestro, ref anexoData);
            entAnexo8 entBd = new entAnexo8();
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();
            
            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo8(anexoData, tran);
                    anexoView.ID = anexoData.ID;
                    if (idValanexo > 0 && anexoView.PersonaAfectadaId > 0)
                    {
                        entBdSiniestro.insDataValoracionAnexoPersona(idValanexo, (int)anexoView.PersonaAfectadaId, anexoData.ID, tran);
                    }
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo8(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo8(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }
        
        public static void ParseViewToData_Anexo(clsAnexo08_Victima anexoView,  int id_siniestro, ref TBANEXO8 anexoData)
        {
            #region Common Anexos
            anexoData.ID = anexoView.ID ?? -1;

            if (anexoData.TBSINIESTROS_PERSONA == null)
                anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            anexoData.TBSINIESTROS_PERSONA.ID = id_siniestro;

            if (anexoData.TBREGISTROS_PERSONAS == null)
                anexoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
            anexoData.TBREGISTROS_PERSONAS.ID = (int)anexoView.PersonaAfectadaId;
            
            anexoData.VICTIMA = Common.ParseIntToShortNullable(anexoView.VictimaDeEsteHecho);

            clsAnexo_Afectacion anexoAfectacionView = anexoView.Afectacion;
            anexoData.AFECTADO  = Common.ParseIntToShortNullable(anexoAfectacionView.Afectado);
            anexoData.OTRA_AFECTACION = anexoAfectacionView.Otro;
            
            #region Denuncia Previa
            clsAnexo_DenunciaPrevia denunciaPreviaView = anexoView.DenunciaPrevia;
            anexoData.DECLARACIONPREV = Common.ParseIntToShortNullable(denunciaPreviaView.SePresento);
            anexoData.PARAM_ENTIDAD_DENUNCIAPREV = denunciaPreviaView.Entidad;
            anexoData.FECHA_DENUNCIAPREV = denunciaPreviaView.Fecha;
            anexoData.ID_PAIS_DENUNCIAPREV = denunciaPreviaView.Pais;
            anexoData.ID_DEPARTAMENTO_DENUNCIAPREV = denunciaPreviaView.Departamento;
            anexoData.ID_MUNICIPIO_DENUNCIAPREV = denunciaPreviaView.Municipio;
            anexoData.NUMERO_RADICADO_DENUNCIAPREV = denunciaPreviaView.Codigo;
            #endregion

            #endregion

            #region Datos del Secuestro
            anexoData.SECUESTRADO = Common.ParseIntToShortNullable(anexoView.PersonaEstaSecuestrada);
            anexoData.PARAM_TIPO_SECUESTRO = Common.ParseIntToShortNullable(anexoView.TipoDeSecuestro);
            anexoData.PARAM_FINALIDAD_SECUESTRO = anexoView.FinalidadSecuestroExtorsivo;
            anexoData.OTRA_FINALIDAD_SECUESTRO = anexoView.OtraFinalidadSecuestroOtro;
            anexoData.CONTRAPRESTACION = Common.ParseIntToShortNullable(anexoView.HanPedidoContraprestacionPorLibertad);
            anexoData.QUE_CONTRAPRESTACION = anexoView.ContraprestacionPedida;
            anexoData.PARAM_SITUACIONACTUALVICTIMA = anexoView.SituacionActualVictima;
            #endregion

            #region Libertad de la Victima
            anexoData.PARAM_LIBERACION_VICTIMA = Common.ParseIntToShortNullable(anexoView.ComoSeProdujoLiberacion);
            anexoData.FECHA_LIBERACION = anexoView.FechaLiberacion;
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

        public static clsAnexo08 ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData)
        {
            entAnexo8 entAnexo = new entAnexo8();
            clsAnexo08 anexoView = new clsAnexo08();

            //Pasar datos a la vista
            Anexo08.ParseDataToView_Siniestro(siniestroData, ref anexoView);

            //Reiniciar Estado
            anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            //Obtener las personas del anexo
            List<TBANEXO8> anexosData = entAnexo.getData(siniestroData.ID);

            foreach (TBANEXO8 anexoData in anexosData)
            {

                clsAnexo08_Victima VictimaAnexoView = new clsAnexo08_Victima();

                Anexo08.ParseDataToView_Anexo(anexoData, VictimaAnexoView);

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

        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData, ref clsAnexo08 anexoView)
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

        public static void ParseDataToView_Anexo(TBANEXO8 anexoData, clsAnexo08_Victima anexoView)
        {
            #region Common Anexos

            anexoView.ID = anexoData.ID;

            if (anexoData.TBREGISTROS_PERSONAS != null)
                anexoView.PersonaAfectadaId = anexoData.TBREGISTROS_PERSONAS.ID;

            anexoView.VictimaDeEsteHecho = anexoData.VICTIMA;

            anexoView.PersonaEstaSecuestrada = anexoData.SECUESTRADO;

            anexoView.Afectacion.Afectado = anexoData.AFECTADO;
            anexoView.Afectacion.Otro = anexoData.OTRA_AFECTACION;


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

            #region Datos del Secuestro
            anexoView.TipoDeSecuestro = anexoData.PARAM_TIPO_SECUESTRO;
            anexoView.FinalidadSecuestroExtorsivo = anexoData.PARAM_FINALIDAD_SECUESTRO;
            anexoView.OtraFinalidadSecuestroOtro = anexoData.OTRA_FINALIDAD_SECUESTRO;
            anexoView.HanPedidoContraprestacionPorLibertad = anexoData.CONTRAPRESTACION;
            anexoView.ContraprestacionPedida = anexoData.QUE_CONTRAPRESTACION;
            anexoView.SituacionActualVictima = anexoData.PARAM_SITUACIONACTUALVICTIMA;
            #endregion

            #region Libertad de la Victima
            anexoView.ComoSeProdujoLiberacion = anexoData.PARAM_LIBERACION_VICTIMA;
            anexoView.FechaLiberacion = anexoData.FECHA_LIBERACION;
            #endregion

            //anexoView.EstadoRegistro = anexoData.ACTIVO;
        }
        #endregion
    }
}
