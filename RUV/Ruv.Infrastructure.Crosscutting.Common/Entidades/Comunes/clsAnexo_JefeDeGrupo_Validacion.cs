using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Genérico de información de víctima para uso en varios anexos.
    /// </summary>

    public partial class clsAnexo_JefeDeGrupo : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                string resultado = null;
                switch (columnName)
                {
                    case "TieneInscritaCedulaParaVotar":
                        if (TieneInscritaCedulaParaVotar < 0)
                            resultado = "Indique si tiene o no cédula inscrita para votar";
                        break;
                    case "EncuestaSisben":
                        if (EncuestaSisben < 0)
                            resultado = "Indique si le aplicaron la encuesta en el Sisben";
                        break;
                    case "InscritoEnPrograma":
                        if (InscritoEnPrograma < 0)
                            resultado = "Indique si estaba inscrito en el programa 'Familias en Acción'";
                        break;
                    case "VinculadoSistemaSalud":
                        if (VinculadoSistemaSalud < 0)
                            resultado = "Indique si se encontraba viculado al sistema de Salud";
                        break;
                    /*
                     * Por solicutud realizada el 25 Mayo 2012, en el documento OBSERVACIONES V103_PRUEBAS.docx
                     * Se quita esta validación el campo debe permitir numeros y letras.
                case "HijosEstudianInstitucion":
                    if (!string.IsNullOrWhiteSpace(HijosEstudianInstitucion))
                        resultado = clsValidadorEntidades.ValidarAlfanumerico(HijosEstudianInstitucion, "Institución Educativa");
                    break;
                    */
                    case "InscritoEnProgramaEntidadDondeLabora":
                        if (!string.IsNullOrWhiteSpace(InscritoEnProgramaEntidadDondeLabora))
                            resultado = clsValidadorEntidades.ValidarAlfanumerico(InscritoEnProgramaEntidadDondeLabora, "Entidad en la cual cobra");
                        break;

                    case "LugarLaboralEmpleador":
                        if (!string.IsNullOrWhiteSpace(LugarLaboralEmpleador))
                            resultado = clsValidadorEntidades.ValidarAlfanumerico(LugarLaboralEmpleador, "Nombre Empleador donde laboraba");
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
