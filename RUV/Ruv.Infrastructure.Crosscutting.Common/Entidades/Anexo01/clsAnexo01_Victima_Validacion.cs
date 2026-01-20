using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo01_Victima : clsEntidadBase, IDataErrorInfo
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
                            resultado = "Marque si la persona fue víctima de Acto terrorista, Atentados, Combates, Enfrentamientos u Hostigamientos";
                        break;
                    case "AtencionMedicaRecibio":
                        if (!AtencionMedicaRecibio.HasValue)
                            resultado = "Indique si la persona afectada recibió atención médica";
                        break;
                    case "Bienes":
                        if (Bienes.Count > 0)
                        {
                            foreach (var item in Bienes)
                            {
                                if (!item.TipoBien.HasValue)
                                    resultado = "El 'tipo' de bien es obligatorio";
                                if (string.IsNullOrWhiteSpace(item.Descripcion))
                                    resultado = "La 'descripción' es obligatoria";
                                else if (item.Descripcion.Length > 500)
                                    resultado = "La descripcion no puede ser mayor a 500 caracteres";
                                if (!item.CalidadDeLaVictima.HasValue)
                                    resultado = "La 'calidad de la víctima' es obligatoria";
                            }
                        }
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
