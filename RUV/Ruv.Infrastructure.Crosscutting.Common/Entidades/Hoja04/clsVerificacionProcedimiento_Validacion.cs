using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsVerificacionProcedimiento : clsEntidadBase, IDataErrorInfo
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
                    case "EnmendarDeclaracion":
                        if (!EnmendarDeclaracion.HasValue)
                        {
                            var usuario = clsDeclaracion.UsuarioActual;


                            if (usuario != null && usuario.Permisos.Contains(ePermisosUsuario.validar_Enmendar_corregir_declaración))
                            {
                                resultado = "Debe marcar si quiere o no agregar, enmendar o corregir algo de la declaración";
                            }
                        }
                        break;
                    case "EnmendarDeclaracionTexto":
                        if (string.IsNullOrWhiteSpace(EnmendarDeclaracionTexto))
                        {
                            if (EnmendarDeclaracion.HasValue && EnmendarDeclaracion.Value == 1)
                                resultado = "Debe digitar el texto a agregar, enmendar o corregir";
                        }
                        break;
                    case "NumeroTotalSoportes":
                        if (NumeroTotalSoportes < 0)
                            resultado = "Debe indicar un numero entero positivo para el total de soportes";
                        break;
                    case "RealizoEntrevistaPrevia":
                        if (!RealizoEntrevistaPrevia.HasValue)
                            resultado = "Debe indicar si realizó o no entrevista previa";
                        break;
                    case "LeyoAlDeclaranteLaDeclaracion":
                        if (!LeyoAlDeclaranteLaDeclaracion.HasValue)
                            resultado = "Debe indicar si leyó o no al (a la) Declarante la declaración";
                        break;
                    case "SeIncluyeronCorrecciones":
                        if (!SeIncluyeronCorrecciones.HasValue)
                            resultado = "Debe indicar si se incluyeron o no correciones o enmendaduras";
                        break;
                    case "RealizoTomaJuramento":
                        if (!RealizoTomaJuramento.HasValue && clsDeclaracion.DeclaracionActual.VersionFUD == 1)
                            resultado = "Debe indicar si realizó o no la toma de juramento";
                        break;
                    case "HuboOrientacionParaCorregir":
                        if (!HuboOrientacionParaCorregir.HasValue)
                            resultado = "Debe indicar si hubo o no orientación para correir o enmendar";
                        break;
                    case "DeclaranteSabeFirmar":
                        if (!DeclaranteSabeFirmar.HasValue)
                            resultado = "Debe indicar si el declarante sabe o no firmar";
                        break;
                    case "UsoDatosPersonales":
                        if(clsDeclaracion.DeclaracionActual.VersionFUD == 2)
                        {
                            if (!UsoDatosPersonales.HasValue)
                                resultado = "Debe indicar si autoriza o no el uso de datos personales";
                        }
                        break;
                        //case "FuncionarioNombre":
                        //    if (string.IsNullOrWhiteSpace(FuncionarioNombre))
                        //        resultado = "El nombre del funcionario es obligatorio";
                        //    break;
                        //case "FuncionarioCargo":
                        //    if (string.IsNullOrWhiteSpace(FuncionarioCargo))
                        //        resultado = "El cargo del funcionario es obligatorio";
                        //    break;
                        //case "FuncionarioDocumentoIdentidad":
                        //    if (string.IsNullOrWhiteSpace(FuncionarioDocumentoIdentidad))
                        //        resultado = "El documento de identidad del funcionario es obligatorio";
                        //    break;


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
