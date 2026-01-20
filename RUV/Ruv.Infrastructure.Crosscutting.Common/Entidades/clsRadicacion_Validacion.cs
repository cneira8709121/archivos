using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsRadicacion : clsEntidadBase, IDataErrorInfo
  {
    #region VALIDACIONES
    /// <summary>
    /// Fecha minima para una radicacion en RUV: 02 de Enero de 2012
    /// </summary>
    public readonly static DateTime FechaMinima = new DateTime(2012, 1, 2);

    public string this[string columnName]
    {
      get
      {
        string resultado = null;
        switch (columnName)
        {
          case "FECHALLEGADA":
            if (FECHALLEGADA.HasValue)
              if (FECHALLEGADA.Value > DateTime.Now)
                resultado = "La fecha de llegada no puede ser mayor a la fecha actual";
              else if (FECHALLEGADA.Value < clsRadicacion.FechaMinima)
                resultado = "La fecha de llegada no puede ser menor a " + clsRadicacion.FechaMinima.ToShortDateString();
            break;
          //=====================================
          case "PrimerNombre":
            if (MODOFORMULARIO == true)
                resultado = clsValidadorEntidades.ValidarN1(PrimerNombre, "Primer Nombre", true);
            break;
          case "SegundoNombre":
            resultado = clsValidadorEntidades.ValidarN2A1A2(SegundoNombre, "Segundo Nombre", false);
            break;
          case "PrimerApellido":
            if (MODOFORMULARIO == true)
                resultado = clsValidadorEntidades.ValidarN2A1A2(PrimerApellido, "Primer Apellido", true);
            break;
          case "SegundoApellido":
            resultado = clsValidadorEntidades.ValidarN2A1A2(SegundoApellido, "Segundo Apellido", false);
            break;
          case "TipoDocumento":
            if (MODOFORMULARIO == true)
                if (!TipoDocumento.HasValue)
                  resultado = "El tipo de documento es obligatorio";
            break;
          case "NumeroDocumento":
            if (MODOFORMULARIO == true)
                // Para los tipos que no tienen numero de documento no se hace la validacion
                if (TipoDocumento != (int)eTipoDocumento.NoInforma
                    && TipoDocumento != (int)eTipoDocumento.Indocumentado
                    && TipoDocumento != (int)eTipoDocumento.NoSabe
                    && TipoDocumento != (int)eTipoDocumento.NoResponde)
                {
                  var ValidacionNumeroDocumento =
                  clsValidadorEntidades.ValidarDocumentoIdentificacion(true, TipoDocumento, NumeroDocumento);
                  if (!ValidacionNumeroDocumento.Item1)
                    resultado = ValidacionNumeroDocumento.Item2;
                }
                else
                {
                  if (!string.IsNullOrWhiteSpace(NumeroDocumento))
                    resultado = "El tipo de documento seleccionado no permite digitar numero de documento";
                }
            break;
          //case "PARAM_TIPOENTIDAD":
          //  if (!PARAM_TIPOENTIDAD.HasValue)
          //    resultado = "La entidad que atiende es obligatoria";
          //  else
          //  {
          //    if (ID_PAIS == (int)ePaises.Colombia && PARAM_TIPOENTIDAD.Value == (int)eEntidadAtiende.Consulado)
          //    {
          //      resultado = string.Format("La entidad '{0}' no puede ser seleccionada para '{1}'", eEntidadAtiende.Consulado, (ePaises)ID_PAIS);
          //    }
          //    else if (ID_PAIS != null && ID_PAIS != (int)ePaises.Colombia && PARAM_TIPOENTIDAD.Value != (int)eEntidadAtiende.Consulado)
          //    {
                  
          //      resultado = string.Format("La entidad '{0}' no puede ser seleccionada para este Pais solo se permite '{1}'", (eEntidadAtiende)PARAM_TIPOENTIDAD.Value, eEntidadAtiende.Consulado);
          //    }
          //  }
          //  break;
          case "NRO_FORMULARIO":
            if (MODOFORMULARIO == true)
            {
                if (string.IsNullOrEmpty(NRO_FORMULARIO))
                {
                    resultado = "El número de formulario es obligatorio";
                }
                else
                {
                    //var validarFormulario = formatoNoFormularioValido(NRO_FORMULARIO);
                    //if (!validarFormulario.Item1)
                    //    resultado = validarFormulario.Item2;
                }
            }
            break;
          case "ID_TIPORADICACION":
              if (!ID_TIPORADICACION.HasValue)
                resultado = "Debe seleccionar el tipo de radicación que se está diligenciando";
              break;
          case "RUTAIMAGEN":
              if (string.IsNullOrEmpty(RUTAIMAGEN))
                  resultado = "Debe seleccionar el arhivo de imagen para la radicación";
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
