using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;

using System.Data.Objects.DataClasses;
using System.Data.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.Business.Captura.Declaracion
{
    public class RegistroPersona
    {
        #region Guardar Datos
        public static void Guardar(clsDeclaracion declaracionView, IPersonaAfectada personaView, int id_persona, ref int? id_declarante, ref int? id_jefeHogar, DbTransaction tran)
        {
            #region RegistroPersona
            int id_declaracion = (int)declaracionView.ID;
            Ruv.Data.TBREGISTROS_PERSONAS regPersonaData = new TBREGISTROS_PERSONAS();
            RegistroPersona.ParseViewToData(personaView, id_declaracion, id_persona, declaracionView.UsuarioId, declaracionView.UnidadTerritorialId, regPersonaData);

            if (declaracionView.TomaDeclaracion.DeclaranteId != null
                && declaracionView.TomaDeclaracion.DeclaranteId == personaView.ID)
            {
                Persona.ParseViewToData_Declarante(declaracionView.TomaDeclaracion, regPersonaData);
                regPersonaData.ESDECLARANTE = 1;
            }                
            else if (declaracionView.TomaDeclaracion.DeclaranteId != null //Se cargan los datos de contacto del jefe familiar para los anexos 13
                && personaView.FamiliaConsecutivo > 1
                && personaView.Relacion == (int)eRelacion.Jefe_de_hogar)
            {
                Persona.ParseViewToData_Declarante(declaracionView.TomaDeclaracion, regPersonaData);
            }
            //if (!id_declarante.HasValue)
            //    if (declaracionView.TomaDeclaracion.DeclaranteNumeroDocumento == personaView.NumeroDocumento
            //        && declaracionView.TomaDeclaracion.DeclaranteTipoDocumento == personaView.TipoDocumento)
            //    {
            //        Persona.ParseViewToData_Declarante(declaracionView.TomaDeclaracion, regPersonaData);
            //        regPersonaData.ESDECLARANTE = 1;
            //    }

            entRegistroPersona entRegPers = new entRegistroPersona();
            //Insertar/Actualizar base de datos
            switch (personaView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    regPersonaData.ACTIVO = 1;
                    entRegPers.setData(regPersonaData, tran);
                    RegistroPersona.ActualizarAnexosRegPersonas(declaracionView, (int)personaView.ID, regPersonaData.ID);
                    personaView.ID = regPersonaData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entRegPers.updateData(regPersonaData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    regPersonaData.ACTIVO = 0;
                    entRegPers.updateData(regPersonaData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    break;
            }

            //if (!id_declarante.HasValue && regPersonaData.ESDECLARANTE == 1)
            if (regPersonaData.ESDECLARANTE == 1)
            {
                declaracionView.TomaDeclaracion.DeclaranteId = regPersonaData.ID;
                id_declarante = (int)declaracionView.TomaDeclaracion.DeclaranteId;

                if (declaracionView.Firmas != null) { 
                    var firma = declaracionView.Firmas.FirstOrDefault(x => x.firmaOwner == FirmaOwner.DECLARANTE);
                    if (firma != null) {
                        if (personaView.EstadoRegistro == eEstadoRegistro.Insertar)
                            entRegPers.insertVictimaFirma(regPersonaData.ID, firma.firma, tran);
                        else
                            entRegPers.updateVictimaFirma(regPersonaData.ID, firma.firma, tran);
                    }
                }
            }

            //verificar si es JefeHogar
            if ((personaView.Relacion ?? -1) == (int)Common.eRelacion.JEFE_HOGAR)
                id_jefeHogar = regPersonaData.ID;

            #endregion
        }

        public static void ParseViewToData(IPersonaAfectada personaView,
                                            int id_declaracion, int id_persona,
                                            int? usuarioId, int? unidadTerritorialId,
                                            Ruv.Data.TBREGISTROS_PERSONAS regPersonaData)
        {
            regPersonaData.ID = personaView.ID ?? -1;

            regPersonaData.TBDECLARACIONES = new TBDECLARACIONES();
            regPersonaData.TBDECLARACIONES.ID = id_declaracion;

            regPersonaData.TBPERSONAS = new TBPERSONAS();
            regPersonaData.TBPERSONAS.ID = id_persona;

            regPersonaData.ACTIVO = (short)((personaView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
                        
            regPersonaData.PARAM_RELACION = personaView.Relacion;

            regPersonaData.CONSECUTIVO_PERSONA = (short)personaView.NumeroConsecutivo;

            regPersonaData.ESMUJERCABEZADEHOGAR = Common.ParseIntToShortNullable(personaView.MujerCabezaDeHogar);
            regPersonaData.PARAM_REGIMENESPECIAL = personaView.RegimenEspecial;
            regPersonaData.GESTANTE_LACTANTE = Common.ParseIntToShortNullable(personaView.GestanteLactante);

            //120131 - Adcionar a RegPersona Usuario y UnidadTerritorial
            regPersonaData.ID_USUARIO = usuarioId;
            regPersonaData.ID_UTERRITORIAL = Common.ParseIntToShortNullable(unidadTerritorialId);

            regPersonaData.CONSECUTIVO_FAMILIA = (short)personaView.FamiliaConsecutivo;

            regPersonaData.ID_NACIONALIDAD = personaView.Nacionalidad;

            regPersonaData.ID_NACIONALIDAD = personaView.Nacionalidad;

            regPersonaData.ESHOMBRECABEZADEHOGAR = Common.ParseIntToShortNullable(personaView.HombreCabezaDeHogar);
            regPersonaData.CAMPESINADO = Common.ParseIntToShortNullable(personaView.Campesinado);
            regPersonaData.PERSONA_BUSCADORA = Common.ParseIntToShortNullable(personaView.PersonaBuscadora);
        }

        private static void ActualizarAnexosRegPersonas(clsDeclaracion declaracionView, int idView, int idData)
        {
            #region ANEXO01:
            foreach (clsAnexo01 anexo in declaracionView.A01)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo01_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO02:
            foreach (clsAnexo02 anexo in declaracionView.A02)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo02_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO03:
            foreach (clsAnexo03 anexo in declaracionView.A03)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo03_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;

                if (anexo.NiñosNacidosPorAbusoSexual != null && anexo.NiñosNacidosPorAbusoSexual.Contains(idView))
                {
                    anexo.NiñosNacidosPorAbusoSexual.Remove(idView);
                    anexo.NiñosNacidosPorAbusoSexual.Add(idData);
                }
            }
            #endregion

            #region ANEXO04:
            foreach (clsAnexo04 anexo in declaracionView.A04)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;

                foreach (clsAnexo04_Victima victima in anexo.Victimas)
                {
                    if (victima.PersonaAfectadaId == idView)
                        victima.PersonaAfectadaId = idData;
                    if (victima.MenorDesprotegidoId == idView)
                        victima.MenorDesprotegidoId = idData;
                }
            }
            #endregion

            #region ANEXO05:
            foreach (clsAnexo05 anexo in declaracionView.A05)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo05_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO06:
            foreach (clsAnexo06 anexo in declaracionView.A06)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;

                foreach (clsAnexo06_Victima victima in anexo.Victimas)
                {
                    if (victima.PersonaAfectadaId == idView)
                        victima.PersonaAfectadaId = idData;
                    if (victima.MenorDesprotegidoId == idView)
                        victima.MenorDesprotegidoId = idData;
                }
            }
            #endregion

            #region ANEXO07:
            foreach (clsAnexo07 anexo in declaracionView.A07)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                foreach (clsAnexo07_Victima victima in anexo.Victimas)
                {
                    if (victima.PersonaAfectadaId == idView)
                        victima.PersonaAfectadaId = idData;
                    if (victima.MenorDesprotegidoId == idView)
                        victima.MenorDesprotegidoId = idData;
                }
            }
            #endregion

            #region ANEXO08:
            foreach (clsAnexo08 anexo in declaracionView.A08)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo08_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO09:
            foreach (clsAnexo09 anexo in declaracionView.A09)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo09_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO10:
            foreach (clsAnexo10 anexo in declaracionView.A10)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;
                var vicitmas = from v in anexo.Victimas
                               where v.PersonaAfectadaId == idView
                               select v;
                foreach (clsAnexo10_Victima victima in vicitmas)
                    victima.PersonaAfectadaId = idData;
            }
            #endregion

            #region ANEXO11:
            foreach (clsAnexo11 anexo in declaracionView.A11)
            {
                if (anexo.JefeGrupoFamiliarId == idView)
                    anexo.JefeGrupoFamiliarId = idData;

                var inmuebles = from b in anexo.BienesInmuebles
                                where b.PersonaAfectadaId == idView
                                select b;
                foreach (clsAnexo11_BienInmueble bien in inmuebles)
                    bien.PersonaAfectadaId = idData;

                var muebles = from b in anexo.BienesMuebles
                              where b.PersonaAfectadaId == idView
                              select b;
                foreach (clsAnexo11_BienMueble bien in muebles)
                    bien.PersonaAfectadaId = idData;

                var creditos = from c in anexo.CreditosPasivos
                               where c.PersonaAfectadaId == idView
                               select c;
                foreach (clsAnexo11_CreditoPasivo credito in creditos)
                    credito.PersonaAfectadaId = idData;
            }
            #endregion

            //#region ANEXO13:
            //foreach (clsAnexo13 anexo in declaracionView.A13)
            //{
            //    if (anexo.JefeGrupoFamiliarId == idView)
            //        anexo.JefeGrupoFamiliarId = idData;
            //    var vicitmas = from v in anexo.ListaPersonas
            //                   where v.ID == idView
            //                   select v;
            //    foreach (clsAnexo13_Victima victima in vicitmas)
            //    {
            //        victima.PersonaAfectadaId = idData;
            //        victima.ID = idData;
            //    }
            //}
            //#endregion
        }

        #endregion

        #region Obtener Datos
        public static List<TBREGISTROS_PERSONAS> ObtenerRegistrosPersona(int id_declaracion, int FamiliaConsecutivo)
        {
            //Obtener registros personas de la declaracion
            entRegistroPersona entRegPer = new entRegistroPersona();
            List<TBREGISTROS_PERSONAS> registrosPersonasData = entRegPer.getData(id_declaracion, FamiliaConsecutivo);

            return registrosPersonasData;
        }


        #endregion
    }
}
