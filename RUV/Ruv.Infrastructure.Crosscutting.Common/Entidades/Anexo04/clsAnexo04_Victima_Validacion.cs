using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo04_Victima : clsEntidadBase, IDataErrorInfo
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
                            resultado = "Marque si la persona fue víctima de este hecho";
                        break;
                    case "VictimaDesaparecida":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !VictimaDesaparecida.HasValue)
                            resultado = "Marque si la persona se encuentra desaparecida";

                        if (VictimaDesaparecida.HasValue && VictimaDesaparecida.Value == 1)
                        {
                            var DA = clsDeclaracion.DeclaracionActual;

                            if (DA != null)
                            {
                                if (clsDeclaracion.DeclaracionActual.TomaDeclaracion.DeclaranteId == PersonaAfectadaId)
                                {
                                    resultado = "La persona no puede estar marcada como declarante y como desaparecida, realice la corrección del caso";
                                }
                            }
                        }
                        break;
                    case "ActividadAlDesaparecer":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && string.IsNullOrWhiteSpace(ActividadAlDesaparecer))
                            resultado = "Debe indicar la actividad que la persona desaparecida estaba realizando justo al momento de la desaparición";
                        break;                        
                    case "QuedoMenorDesprotegido":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !QuedoMenorDesprotegido.HasValue)
                            resultado = "Marque si algún menor quedo desprotegido con ocasión de estos hechos";
                        break;
                    case "MenorDesprotegidoId":
                        if (!MenorDesprotegidoId.HasValue)
                        {
                            if (QuedoMenorDesprotegido.HasValue)
                                if (QuedoMenorDesprotegido.Value == 1)
                                    resultado = "Debe seleccionar el menor que quedo desprotegido con ocasión de estos hechos";
                        }
                        else
                        {   
                            //20120528 Luis.Esteban Se solicita quitar la restricción, el sistema debe permitir seleccionar la persona sin importar si es menor de edad o mayor de edad.
                            //var DA = clsDeclaracion.DeclaracionActual;
                            //var PA = DA.PersonasAfectadas.ListaPersonas.FirstOrDefault(x => x.ID == MenorDesprotegidoId);


                            //if (PA.FechaNacimiento.Value.CompareTo(DateTime.Today.AddYears(-18)) > 0)
                            //{
                                if (QuedoMenorDesprotegido.HasValue)
                                {
                                    if (QuedoMenorDesprotegido.Value == 0)
                                        resultado = "Ha seleccionado un menor desprotegido, así que debe marcar que algún menor quedo desprotegido";
                                }
                                else
                                    resultado = "Ha seleccionado un menor desprotegido, así que debe marcar que algún menor quedo desprotegido";
                            //}
                            //else
                            //{
                            //    resultado = "La persona seleccionada no es menor de edad";
                            //}
                        }
                        break;
                        //*20120307 Luis.Esteban Se solicitó que omitir esta validación.
                        case "HaRealizadoBusquedaDeVictima":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 &
                            !(VictimaDesaparecida.HasValue && VictimaDesaparecida.Value == 1) && 
                            !HaRealizadoBusquedaDeVictima.HasValue)
                            resultado = "Marque si ha realizado la búsqueda de la víctima";
                        //if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !HaRealizadoBusquedaDeVictima.HasValue)
                        //    resultado = "Marque si ha realizado la búsqueda de la víctima";
                        break;
                        
                        
                    case "HarealizadoBusquedaAnteEntidad":
                        if (!string.IsNullOrWhiteSpace(HarealizadoBusquedaAnteEntidad))
                        {
                            if (HaRealizadoBusquedaDeVictima.HasValue)
                            {
                                if (HaRealizadoBusquedaDeVictima.Value == 0)
                                    resultado = "Ha indicado la entidad mediante la cual ha realizado la búsqueda de la víctima, así que debe marcar que ha realizado la búsqueda de la víctima";
                            }
                            else
                                resultado = "Ha indicado la entidad mediante la cual ha realizado la búsqueda de la víctima, así que debe marcar que ha realizado la búsqueda de la víctima";
                        }
                        else
                        {
                            if (HaRealizadoBusquedaDeVictima.HasValue)
                                if (HaRealizadoBusquedaDeVictima.Value == 1)
                                    resultado = "Debe indicar la entidad mediante la cual ha realizado la búsqueda de la víctima";
                        }
                        break;
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
