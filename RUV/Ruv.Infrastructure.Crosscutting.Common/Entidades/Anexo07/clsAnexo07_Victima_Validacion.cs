using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo07_Victima : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                if (!ValidationManager.ValidateProperty(clsDeclaracion.ConfiguracionValidaciones, Scope, columnName))
                    return null;
                string resultado = null;
                switch (columnName)
                {
                    case "VictimaDeEsteHecho":
                        if (!VictimaDeEsteHecho.HasValue)
                            resultado = "Debe marcar si la persona fue o no afectada víctima de este hecho";
                        break;
                    case "EstadoVictima":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !EstadoVictima.HasValue)
                            resultado = "Debe indicar el estado de la víctima luego del accidente";
                        else if (EstadoVictima.HasValue && EstadoVictima.Value == (int)eEstadoVictimaMinas.MUERTO)
                        {
                            var DA = clsDeclaracion.DeclaracionActual;

                            if (DA != null)
                            {
                                if (DA.TomaDeclaracion.DeclaranteId == PersonaAfectadaId)
                                {
                                    resultado = "La persona no puede estar marcada como declarante y como muerto, realice la corrección del caso";
                                }
                            }
                        }
                        break;
                    case "TipoAccidente":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !TipoAccidente.HasValue)
                            resultado = "Debe indicar el tipo de accidente";
                        break;
                    case "ActividadAlMomentoDelHecho":
                        if (clsDeclaracion.DeclaracionActual.VersionFUD == 1)
                        {
                            if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !ActividadAlMomentoDelHecho.HasValue)
                                resultado = "Debe indicar la actividad al momento del hecho";
                        }
                        break;
                    case "AlgunMenorQuedoHuerfano":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AlgunMenorQuedoHuerfano.HasValue)
                            resultado = "Debe indicar si algún menor quedó huérfano por estos hechos";
                        break;

                    case "MenorDesprotegidoId":
                        if (!MenorDesprotegidoId.HasValue)
                        {
                            if (AlgunMenorQuedoHuerfano.HasValue)
                                if (AlgunMenorQuedoHuerfano.Value == 1)
                                    resultado = "Debe seleccionar el menor que quedo huérfano con ocasión de estos hechos";
                        }
                        else
                        {
                            //20120528 Luis.Esteban Se solicita quitar la restricción, el sistema debe permitir seleccionar la persona sin importar si es menor de edad o mayor de edad.
                            //var DA = clsDeclaracion.DeclaracionActual;
                            //var PA = DA.PersonasAfectadas.ListaPersonas.FirstOrDefault(x => x.ID == MenorDesprotegidoId);


                            //if (PA.FechaNacimiento.Value.CompareTo(DateTime.Today.AddYears(-18)) > 0)
                            //{
                            if (AlgunMenorQuedoHuerfano.HasValue)
                            {
                                if (AlgunMenorQuedoHuerfano.Value == 0)
                                    resultado = "Ha seleccionado un menor huérfano, así que debe marcar que algún menor quedo huérfano";
                            }
                            else
                                resultado = "Ha seleccionado un menor huérfano, así que debe marcar que algún menor quedo huérfano";
                            //}
                            //else
                            //{
                            //    resultado = "La persona seleccionada no es menor de edad";
                            //}
                        }
                        break;
                    case "MenorQuedoHuerfanoDe":
                        if (AlgunMenorQuedoHuerfano == 1)
                        {
                            if (!MenorQuedoHuerfanoDe.HasValue)
                                resultado = "Debe indicar si el menor quedo huérfano de PADRE, MADRE O PADRE Y MADRE";
                        }
                        else
                        {
                            if (MenorQuedoHuerfanoDe.HasValue)
                                resultado = "Si indicó que el menor quedo huérfano de PADRE, MADRE O PADRE Y MADRE, debe marcar que el menor quedo huérfano";
                        }
                        break;

                    case "RecibioAtencionMedica":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !RecibioAtencionMedica.HasValue)
                            resultado = "Debe marcar si recibio atención médica";
                        break;
                        /* 20120703 Luis.Esteban 
                            * RecibioAtencionMedicaEntidad, RecibioAtencionMedicaDepartamento y RecibioAtencionMedicaMunicipio, no son obligatorios aun cuando se haya marcado SI en AtencionMedicaRecibio 
                    case "RecibioAtencionMedicaEntidad":
                        if (RecibioAtencionMedica.HasValue && RecibioAtencionMedica == 1 && string.IsNullOrWhiteSpace(RecibioAtencionMedicaEntidad))
                            resultado = "Debe ingresar la Entidad Médica";
                        else
                            resultado = clsValidadorEntidades.ValidarAlfanumerico(RecibioAtencionMedicaEntidad, "Entidad de Atención Médica");
                        break;
                    case "RecibioAtencionMedicaDepartamento":
                        if (RecibioAtencionMedica.HasValue && RecibioAtencionMedica == 1 && !RecibioAtencionMedicaDepartamento.HasValue)
                            resultado = "Debe ingresar el departamento de la Entidad Médica";
                        break;
                    case "RecibioAtencionMedicaMunicipio":
                        if (RecibioAtencionMedica.HasValue && RecibioAtencionMedica == 1 && !RecibioAtencionMedicaMunicipio.HasValue)
                            resultado = "Debe ingresar el municipio la Entidad Médica";
                        break;
                         * */

                }
                return resultado;
            }
        }

        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
