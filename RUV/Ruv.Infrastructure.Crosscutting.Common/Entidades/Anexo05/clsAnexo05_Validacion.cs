using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo05 : clsEntidadBase, IDataErrorInfo
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
                    case "JefeGrupoFamiliarId":
                        if (!JefeGrupoFamiliarId.HasValue)
                            resultado = "Debe seleccionar víctima 1";
                        break;
                    case "TipoDesplazamiento":
                        if (!TipoDesplazamiento.HasValue)
                          resultado = "Debe seleccionar el tipo de desplazamiento";
                        else if(TipoDesplazamiento.Value == (int)eTipoDesplazamientoA05.Masivo )
                        {
                          var DA = clsDeclaracion.DeclaracionActual;

                          if (DA != null)
                          {
                            if (DA.A13.Count == 0)
                            {
                              resultado = "Se ha marcado el evento como masivo, por lo tanto debe diligenciar al menos un anexo 13";
                            }
                          }
                        }
                        break;
                    case "TiempoResidenciaEnLugarExpulsorAños":
                        if (!TiempoResidenciaEnLugarExpulsorAños.HasValue)
                            resultado = "Debe indicar el tiempo (años) de residencia en el lugar expulsor";
                        break;
                    case "TiempoResidenciaEnLugarExpulsorMeses":
                        if (!TiempoResidenciaEnLugarExpulsorMeses.HasValue)
                            resultado = "Debe indicar el tiempo (meses) de residencia en el lugar expulsor";
                        if (TiempoResidenciaEnLugarExpulsorMeses.HasValue && TiempoResidenciaEnLugarExpulsorMeses.Value > 12)
                            resultado = "El tiempo en (meses) debe ser máximo 12";
                        break;
                    case "TiempoResidenciaEnLugarExpulsorDias":
                        if (!TiempoResidenciaEnLugarExpulsorDias.HasValue)
                            resultado = "Debe indicar el tiempo (días) de residencia en el lugar expulsor";
                        if(TiempoResidenciaEnLugarExpulsorDias.HasValue && TiempoResidenciaEnLugarExpulsorDias.Value > 31)
                            resultado = "El tiempo en (días) debe ser máximo 31";
                        break;
                    case "CausaDesplazamiento":
                        if (!CausaDesplazamiento.Any())
                            resultado = "Debe indicar la causa del desplazamiento";
                        break;
                    case "CausaDesplazamientoOtro":
                        if (CausaDesplazamiento.Any() && CausaDesplazamiento.Contains((int)eTipoDesplazamiento.Otra)
                                && string.IsNullOrWhiteSpace(CausaDesplazamientoOtro))
                            resultado = "Especifique la causa del desplazamiento, en el campo indicado (otro)";
                        break;
                    case "DeseoDelHogar":
                        if (!DeseoDelHogar.HasValue)
                            resultado = "Especifique el deseo del hogar";
                        break;
                    case "InformacionDeArribo":
                        if (InformacionDeArribo.HechosFecha < FechaYLugar.HechosFecha)
                            resultado = "La fecha de arribo no puede ser menor a la fecha en que ocurrieron los hechos";
                        break;
                    case "NuevoTipoDesplazamiento":
                        if (NuevoTipoDesplazamiento.HasValue && 
                            (NuevoTipoDesplazamiento.Value == 10147 || NuevoTipoDesplazamiento.Value == 10148)
                            && !EsExilio.HasValue)
                            resultado = "Al seleccionar un tipo de desplazamiento, debe especificar si el motivo es por exilio";
                        break;
                    case "EsExilio":
                        if (NuevoTipoDesplazamiento.HasValue &&
                            (NuevoTipoDesplazamiento.Value == 10147 || NuevoTipoDesplazamiento.Value == 10148)
                            && !EsExilio.HasValue)
                            resultado = "Debe marcar si el motivo del desplazamiento es por exilio";
                            break;
                }
                return resultado;
            }
        }

        public string ValidacionesDeseaUbicarseEn(string columnName)
        {
            string resultado = null;
            if (_DeseoDelHogar == (int)eDeseoDelHogar.Reubicarse)
            {
            switch (columnName)
            {
                case "HechosDepartamento":
                    if (DeseoDelHogar.HasValue
                        && (DeseoDelHogar == (int)eDeseoDelHogar.Reubicarse)
                        && !DeseaUbicarseEn.HechosDepartamento.HasValue)
                        resultado = "Registre el departamento en el que desea reubicarse";
                    break;
                case "HechosMunicipio":
                    if (DeseoDelHogar.HasValue
                        && ( DeseoDelHogar == (int)eDeseoDelHogar.Reubicarse)
                        && !DeseaUbicarseEn.HechosMunicipio.HasValue)
                        resultado = "Registre el municipio en el que desea reubicarse";
                    break;
            }
                
            }
            return resultado;
            
        }

        #region Evaluar FechaYLugar
        public void RaiseReportarCambioFechas(clsEntidadBase children)
        {
            if (children == InformacionDeArribo)
            {
                FechaYLugar.RaiseReportarCambioPropiedad("HechosFecha");
            }
            else if (children == FechaYLugar)
            {
                InformacionDeArribo.RaiseReportarCambioPropiedad("HechosFecha");
            }
        }

        public string EvaluarFechas(clsEntidadBase children)
        {
            if (FechaYLugar != null && InformacionDeArribo != null &&
                              FechaYLugar.HechosFecha.HasValue && InformacionDeArribo.HechosFecha.HasValue)
            {
                if (InformacionDeArribo.HechosFecha.Value < FechaYLugar.HechosFecha.Value)
                    if (children == InformacionDeArribo)
                        return "La fecha de arribo no puede ser menor a la fecha de desplazamiento";
                    else if (children == FechaYLugar)
                        return "La fecha de desplazamiento debe ser menor a la fecha de arribo";
            }
            return null;
        }
        #endregion


        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
