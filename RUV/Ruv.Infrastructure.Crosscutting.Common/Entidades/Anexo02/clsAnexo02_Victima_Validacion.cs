using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo02_Victima : clsEntidadBase, IDataErrorInfo
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
              case "ProteccionHaSolicitado":
                  if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !ProteccionHaSolicitado.HasValue)
                      resultado = "Indique si ha solicitado medidas proteccion";
                  break;
              case "ProteccionLeHanBrindado":
                  if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !ProteccionLeHanBrindado.HasValue)
                      resultado = "Indique si le han brindado medidas proteccion";
                  break;
              case "ProteccionTipoDeMedida":
                  if (!string.IsNullOrWhiteSpace(ProteccionTipoDeMedida))
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                      {
                          if (ProteccionLeHanBrindado.Value == 0)
                              resultado = "Ha indicado tipo de medida, así que debe marcar que le han brindado medidas de protección";
                      }
                      else
                          resultado = "Ha indicado tipo de medida, así que debe marcar que le han brindado medidas de protección";
                  }
                  else
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                          if (ProteccionLeHanBrindado.Value == 1)
                              resultado = "Debe indicar el tipo de medida";
                  }
                  break;
              case "ProteccionEntidad":
                  if (!string.IsNullOrWhiteSpace(ProteccionEntidad))
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                      {
                          if (ProteccionLeHanBrindado.Value == 0)
                              resultado = "Ha indicado entidad que brindó protección, así que debe marcar que le han brindado medidas de protección";
                      }
                      else
                          resultado = "Ha indicado entidad que brindó protección, así que debe marcar que le han brindado medidas de protección";
                  }
                  else
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                          if (ProteccionLeHanBrindado.Value == 1)
                              resultado = "Debe indicar la entidad que brindó la protección";
                  }
                  break;
              case "ProteccionFechaInicial":
                  if (ProteccionFechaInicial.HasValue)
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                      {
                          if (ProteccionLeHanBrindado.Value == 0)
                              resultado = "Ha indicado fecha de protección, así que debe marcar que le han brindado medidas de protección";
                      }
                      else
                          resultado = "Ha indicado fecha de protección, así que debe marcar que le han brindado medidas de protección";
                  }
                  else
                  {
                      if (ProteccionLeHanBrindado.HasValue)
                          if (ProteccionLeHanBrindado.Value == 1)
                              resultado = "Debe indicar la fecha desde cuando goza de dicha medida";
                  }
                  break;
              case "ProteccionVigencia":
                  if (!string.IsNullOrWhiteSpace(ProteccionVigencia))
                  {
                      if (!ProteccionLeHanBrindado.HasValue || ProteccionLeHanBrindado.Value == 0)
                          resultado = "Ha indicado la vigencia de la protección, así que debe marcar que le han brindado medidas de protección";
                  }
                  //else if (ProteccionLeHanBrindado.HasValue && ProteccionLeHanBrindado.Value == 1)
                  //            resultado = "Debe indicar la vigencia de la protección";
                  break;
              case "HaContinuadoConLasAmenzas":
                  if (VictimaDeEsteHecho.HasValue && VictimaDeEsteHecho.Value == 1 && !HaContinuadoConLasAmenzas.HasValue)
                      resultado = "Indique si han continuado con las amnenazas";
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
