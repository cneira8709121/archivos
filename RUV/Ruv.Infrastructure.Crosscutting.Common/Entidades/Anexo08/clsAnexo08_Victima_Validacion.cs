using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo08_Victima : clsEntidadBase, IDataErrorInfo
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
                    case "Afectacion":
                        Afectacion.ReportarCambioPropiedad("TiposDeAfectacion");
                        ReportarCambioPropiedad("SituacionActualVictima");
                        break;
                    case "VictimaDeEsteHecho":
                        if (!VictimaDeEsteHecho.HasValue)
                            resultado = "Debe marcar si la persona fue o no afectada víctima de este hecho";
                        break;
                    case "PersonaEstaSecuestrada":
                        if (!PersonaEstaSecuestrada.HasValue)
                            resultado = "Debe marcar si la persona fue o no secuestrada como resultado de este hecho";
                        break;
                    case "TipoDeSecuestro":
                        if(clsDeclaracion.DeclaracionActual.VersionFUD == 1)
                        {
                            if (PersonaEstaSecuestrada.HasValue && PersonaEstaSecuestrada.Value == 1 && !TipoDeSecuestro.HasValue)
                                resultado = "Debe indicar el tipo de secuestro";
                        }
                        break;
                    case "FinalidadSecuestroExtorsivo":
                        if (TipoDeSecuestro.HasValue && TipoDeSecuestro.Value == (int)eTipoSecuestro.EXTORSIVO
                            && !FinalidadSecuestroExtorsivo.HasValue)
                            resultado = "Debe indicar la finalidad del secuestro extorsivo, en caso de ser diferente a económica y política diligencie el campo 'otra'";
                        break;
                    case "OtraFinalidadSecuestroOtro":
                        if (FinalidadSecuestroExtorsivo.HasValue && FinalidadSecuestroExtorsivo.Value == (int)eFinalidadSecuestroExtor.Otro && string.IsNullOrWhiteSpace(OtraFinalidadSecuestroOtro))
                            resultado = "Debe indicar cual es la 'otra' finalidad del secuestro extorsivo";
                        break;
                    case "HanPedidoContraprestacionPorLibertad":
                        if (TipoDeSecuestro.HasValue && TipoDeSecuestro.Value == (int)eTipoSecuestro.EXTORSIVO
                            && !HanPedidoContraprestacionPorLibertad.HasValue)
                            resultado = "Debe indicar si le han pedido algún tipo de contraprestación a cambio de la libertad de la víctima";
                        break;
                    case "ContraprestacionPedida":
                        if (HanPedidoContraprestacionPorLibertad.HasValue && HanPedidoContraprestacionPorLibertad.Value == 1
                            && string.IsNullOrWhiteSpace(ContraprestacionPedida))
                            resultado = "Debe indicar que le han pedido";
                        break;
                    case "SituacionActualVictima":
                        if (PersonaEstaSecuestrada.HasValue && PersonaEstaSecuestrada.Value == 1
                            && !SituacionActualVictima.HasValue)
                            resultado = "Debe indicar la situación actual de la víctima";
                        //Segun correo de Nasly Lopez 27Mar2012 este cambio queda pendiente hasta nuevo analisis
                        /*
                        if (SituacionActualVictima.HasValue
                           && (SituacionActualVictima.Value == (int)eSituacionVictimaSecuestro.CAUTIVA || SituacionActualVictima.Value == (int)eSituacionVictimaSecuestro.LIBRE)
                           && Afectacion.TiposDeAfectacion.Contains((int)eDiscapacidades.Muerte))
                            resultado = "VICTIMA: Marcó la víctima con la afectación 'Muerte', por lo tanto no puede marcarla como 'Libre' o 'Cautiva'";
                        */
                        break;
                    case "ComoSeProdujoLiberacion":
                        if (SituacionActualVictima.HasValue && SituacionActualVictima.Value == (int)eSituacionVictimaSecuestro.LIBRE
                            && !ComoSeProdujoLiberacion.HasValue)
                            resultado = "Debe indicar como se produjo la liberación de la víctima";
                        break;
                    case "FechaLiberacion":
                        if (SituacionActualVictima.HasValue && SituacionActualVictima.Value == (int)eSituacionVictimaSecuestro.LIBRE
                            && !FechaLiberacion.HasValue)
                            resultado = "Debe indicar la fecha de la liberación de la víctima";
                        if (FechaLiberacion.HasValue && FechaLiberacion.Value > DateTime.Today)
                            resultado = "La fecha de la liberación de la víctima no puede ser mayor a la fecha actual";
                        if (FechaLiberacion.HasValue && AnexoPadre != null && FechaLiberacion.Value < AnexoPadre.FechaYLugar.HechosFecha)
                            resultado = "La fecha de la liberación de la víctima debe ser mayor o igual a la fecha de los hechos";
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
