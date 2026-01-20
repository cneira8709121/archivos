using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo06_Victima : clsEntidadBase, IDataErrorInfo
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
                    //INICIO PREGUNTA 9 AL 13
                    case "VictimaDeEsteHecho":
                        if (!VictimaDeEsteHecho.HasValue)
                            resultado = "Debe marcar si la persona fue o no afectada víctima de este hecho";
                        break;
                    case "VictimaFallecida":
                        if (!VictimaFallecida.HasValue)
                            resultado = "Debe marcar si la víctima falleció como resultado de este hecho";
                        else if(VictimaFallecida.Value == 1)
                        {
                            var DA = clsDeclaracion.DeclaracionActual;

                            if (DA != null)
                            {
                                if (DA.TomaDeclaracion.DeclaranteId == PersonaAfectadaId)
                                {
                                    resultado = "La persona no puede estar marcada como declarante y como fallecida, realice la corrección del caso";
                                }
                            }
                        }
                        break;
                    case "AlgunMenorQuedoHuerfano":
                        if (!AlgunMenorQuedoHuerfano.HasValue)
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

                            //if (PA != null && PA.FechaNacimiento.Value.CompareTo(DateTime.Today.AddYears(-18)) > 0)
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

                    case "RecuerdaNumeroPersonasMuertas":
                        if (!RecuerdaNumeroPersonasMuertas.HasValue)
                            resultado = "Debe marcar si recuerda el número de personas muertas por este hecho";
                        break;

                    case "NumeroPersonasMuertasEnEsteHecho":
                        if (!NumeroPersonasMuertasEnEsteHecho.HasValue)
                        {
                            if (RecuerdaNumeroPersonasMuertas.HasValue)
                                if (RecuerdaNumeroPersonasMuertas.Value == 1)
                                    resultado = "Debe indicar el numero de personas muertas";
                        }
                        else
                        {
                            if (RecuerdaNumeroPersonasMuertas.HasValue)
                            {
                                if (RecuerdaNumeroPersonasMuertas.Value == 0)
                                    resultado = "Debe seleccionar que 'recuerda el numero de personas muertas'";
                            }
                            else
                                resultado = "Debe seleccionar que 'recuerda el numero de personas muertas'";
                        }
                        break;
                    

                    //FIN PREGUNTA 9 AL 13
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
