using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo10_Victima : clsEntidadBase, IDataErrorInfo
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

                //=========================================================================

            case "GrupoArmado":
                if (GrupoArmadoFechaDesvinculacion.HasValue && string.IsNullOrWhiteSpace(GrupoArmado))
                    resultado = "Indique a qué grupo armado perteneció";
                break;
            case "GrupoArmadoFechaDesvinculacion":
                if (!string.IsNullOrWhiteSpace(GrupoArmado))
                {
                    if (!GrupoArmadoFechaDesvinculacion.HasValue)
                        resultado = "Indique la fecha de desvinculación";
                    else
                    {
                        if (GrupoArmadoFechaDesvinculacion.Value.CompareTo(DateTime.Now) > 0)
                            resultado = "La fecha de desvinculación no puede ser mayor a la fecha actual";
                    }
                }
                break;

            case "AtendidoPorICBF":
                if (!string.IsNullOrWhiteSpace(GrupoArmado) && !AtendidoPorICBF.HasValue)
                        resultado = "Indique si fue o no atendido por el ICBF";
                break;

            case "FechaAtencionICBF":
                if (!string.IsNullOrWhiteSpace(GrupoArmado))
                {
                    if (AtendidoPorICBF.HasValue && AtendidoPorICBF.Value == 1)
                    {
                        if (!FechaAtencionICBF.HasValue)
                            resultado = "Indique a la fecha de atención en el ICBF";
                        else
                        {
                            if (FechaAtencionICBF.Value.CompareTo(DateTime.Now) > 0)
                                resultado = "La fecha de atención en el ICBF, no puede ser mayor a la fecha actual";
                            if (FechaAtencionICBF.Value.CompareTo(GrupoArmadoFechaDesvinculacion) < 0)
                                resultado = "La fecha de atención en el ICBF, no puede ser menor a la fecha de desvinculación";
                        }
                    }
                    else
                    {
                        if (FechaAtencionICBF.HasValue)
                            resultado = "Si indicó la fecha de atención en el ICBF, debe marcar que fue atendido en el ICBF";
                    }
                }
                break;

            case "AtendidoPorOtraEntidad":
                if (!string.IsNullOrWhiteSpace(GrupoArmado) && !AtendidoPorOtraEntidad.HasValue)
                        resultado = "Indique si fue o no atendido por alguna entidad diferente al ICBF";
                break;

            case "FechaAtencionOtraEntidad":
                if (!string.IsNullOrWhiteSpace(GrupoArmado))
                {
                    if (AtendidoPorOtraEntidad.HasValue && AtendidoPorOtraEntidad.Value == 1)
                    {
                        if (!FechaAtencionOtraEntidad.HasValue)
                            resultado = "Indique la fecha de atención en la otra entidad";
                        else
                        {
                            if (FechaAtencionOtraEntidad.Value.CompareTo(DateTime.Now) > 0)
                                resultado = "La fecha de atención en  otra entidad, no puede ser mayor a la fecha actual";
                            if (FechaAtencionOtraEntidad.Value.CompareTo(GrupoArmadoFechaDesvinculacion) < 0)
                                resultado = "La fecha de atención en otra entidad, no puede ser menor a la fecha de desvinculación";
                        }
                    }
                    else
                    {
                        if (FechaAtencionOtraEntidad.HasValue)
                            resultado = "Si indicó la fecha de atención en otra entidad, debe marcar que fue atendido en otra entidad diferente al ICBF";
                    }
                }
                break;

            case "NombreOtraEntidadQueAtendio":
                if (!string.IsNullOrWhiteSpace(GrupoArmado))
                {
                    if (AtendidoPorOtraEntidad.HasValue && AtendidoPorOtraEntidad.Value == 1)
                    {
                        if (string.IsNullOrWhiteSpace(NombreOtraEntidadQueAtendio))
                            resultado = "Indique la entidad en la que fue atendido";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(NombreOtraEntidadQueAtendio))
                            resultado = "Si indicó la entidad en que fue atendido, debe marcar que fue atendido en otra entidad diferente al ICBF";
                    }
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
