using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo09_Victima : clsEntidadBase, IDataErrorInfo
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
                    case "AtencionMedicaRecibioAtencionMedica":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaRecibioAtencionMedica.HasValue)
                            resultado = "Debe marcar si recibio atención médica";
                        break;
                    /* 20120703 Luis.Esteban 
                * AtencionMedicaEntidad, RecibioAtencionMedicaDepartamento y RecibioAtencionMedicaMunicipio, no son obligatorios aun cuando se haya marcado SI en AtencionMedicaRecibio 
                case "AtencionMedicaEntidad":
                    if (AtencionMedicaRecibioAtencionMedica.HasValue && AtencionMedicaRecibioAtencionMedica == 1 && string.IsNullOrWhiteSpace(AtencionMedicaEntidad))
                        resultado = "Debe ingresar la Entidad Médica";
                    else
                        resultado = clsValidadorEntidades.ValidarAlfanumerico(AtencionMedicaEntidad, "Entidad de Atención Médica");
                    break;
                case "RecibioAtencionMedicaDepartamento":
                    if (AtencionMedicaRecibioAtencionMedica.HasValue && AtencionMedicaRecibioAtencionMedica == 1 && !RecibioAtencionMedicaDepartamento.HasValue)
                        resultado = "Debe ingresar el departamento de la Entidad Médica";
                    break;
                case "RecibioAtencionMedicaMunicipio":
                    if (AtencionMedicaRecibioAtencionMedica.HasValue && AtencionMedicaRecibioAtencionMedica == 1 && !RecibioAtencionMedicaMunicipio.HasValue)
                        resultado = "Debe ingresar el municipio la Entidad Médica";
                    break;
                    */
                    case "AtencionMedicaSolicitoAyuda":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaSolicitoAyuda.HasValue)
                            resultado = "Debe indicar si solicitó algún tipo de apoyo o ayuda";
                        break;
                    case "AtencionMedicaSolicitoAyudaEntidad":
                        if (AtencionMedicaSolicitoAyuda.HasValue && AtencionMedicaSolicitoAyuda.Value == 1
                            && string.IsNullOrWhiteSpace(AtencionMedicaSolicitoAyudaEntidad))
                            resultado = "Debe indicar la entidad a la que solicitó apoyo o ayuda";
                        break;
                    case "AtencionMedicaRecibioAyuda":
                        if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !AtencionMedicaRecibioAyuda.HasValue)
                            resultado = "Debe indicar si recibió algún tipo de apoyo o ayuda";
                        break;
                    case "AtencionMedicaAyudaRecibida":
                        if (AtencionMedicaRecibioAyuda.HasValue && AtencionMedicaRecibioAyuda.Value == 1
                            && string.IsNullOrWhiteSpace(AtencionMedicaAyudaRecibida))
                            resultado = "Debe indicar el tipo de apoyo o ayuda recibidos";
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
