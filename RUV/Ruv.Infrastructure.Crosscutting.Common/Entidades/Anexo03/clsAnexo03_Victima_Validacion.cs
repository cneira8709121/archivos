using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo03_Victima : clsEntidadBase, IDataErrorInfo
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
                    case "AtencionMedicaRecibioAtencionMedica":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaRecibioAtencionMedica.HasValue)
                            resultado = "Indique si la persona recibió atención médica";
                        break;
                    /* 20120703 Luis.Esteban 
                    * AtencionEntidadMedica, AtencionMedicaDepartamento y AtencionMedicaMunicipio, no son obligatorios aun cuando se haya marcado SI en AtencionMedicaRecibio 
                    case "AtencionEntidadMedica":
                          if (AtencionMedicaRecibio == 1 && string.IsNullOrWhiteSpace(AtencionEntidadMedica))
                            resultado = "Debe ingresar la Entidad Médica";
                          else
                              resultado = clsValidadorEntidades.ValidarAlfanumerico(AtencionEntidadMedica, "Entidad de Atención Médica");
                          break;
                     case "AtencionMedicaDepartamento":
                     if (AtencionMedicaRecibio == 1 && !AtencionMedicaDepartamento.HasValue)
                         resultado = "Debe ingresar el departamento de la Entidad Médica";
                     break;
                   case "AtencionMedicaMunicipio":
                     if (AtencionMedicaRecibio == 1 && !AtencionMedicaMunicipio.HasValue)
                         resultado = "Debe ingresar el municipio la Entidad Médica";
                     break;
                    */
                    case "AtencionMedicaSolicitoAyuda":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaSolicitoAyuda.HasValue)
                            resultado = "Indique si solicitó algún tipo de apoyo o ayuda";
                        break;
                    case "AtencionMedicaSolicitoAyudaEntidad":
                        if (!string.IsNullOrWhiteSpace(AtencionMedicaSolicitoAyudaEntidad))
                        {
                            if (AtencionMedicaSolicitoAyuda.HasValue)
                            {
                                if (AtencionMedicaSolicitoAyuda.Value == 0)
                                    resultado = "Ha indicado la entidad a la que solicitó apoyo o ayuda, así que debe marcar que solicitó apoyo o ayuda";
                            }
                            else
                                resultado = "Ha indicado la entidad a la que solicitó apoyo o ayuda, así que debe marcar que solicitó apoyo o ayuda";
                        }
                        else
                        {
                            if (AtencionMedicaSolicitoAyuda.HasValue)
                                if (AtencionMedicaSolicitoAyuda.Value == 1)
                                    resultado = "Debe indicar la entidad a la cual solicitó apoyo o ayuda";
                        }
                        break;
                    case "AtencionMedicaRecibioAyuda":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaRecibioAyuda.HasValue)
                            resultado = "Indique si recibió algún tipo de apoyo o ayuda";
                        break;
                    case "AtencionMedicaAyudaRecibida":
                        if (!string.IsNullOrWhiteSpace(AtencionMedicaAyudaRecibida))
                        {
                            if (AtencionMedicaRecibioAyuda.HasValue)
                            {
                                if (AtencionMedicaRecibioAyuda.Value == 0)
                                    resultado = "Ha indicado el tipo de apoyo o ayuda recibidos, así que debe marcar que recbió apoyo o ayuda";
                            }
                            else
                                resultado = "Ha indicado el tipo de apoyo o ayuda recibidos, así que debe marcar que recbió apoyo o ayuda";
                        }
                        else
                        {
                            if (AtencionMedicaRecibioAyuda.HasValue)
                                if (AtencionMedicaRecibioAyuda.Value == 1)
                                    resultado = "Debe indicar el tipo de apoyo o ayuda";
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
