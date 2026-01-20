using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Datos genéricos para algunos anexos.
    /// </summary>

    public partial class clsAnexo_FechaYLugar : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                if (MetodoAlternoValidacion != null)
                    return MetodoAlternoValidacion(columnName);

                string sTitulo = (string.IsNullOrWhiteSpace(Titulo)) ? string.Empty : " " + Titulo;

                string resultado = null;
                switch (columnName)
                {
                    case "HechosFecha":
                        var DA = clsDeclaracion.DeclaracionActual;
                        if (Titulo != "SoloLugar")
                        {
                            if (!HechosFecha.HasValue)
                                resultado = string.Format("Registre la fecha{0} en que ocurrieron los hechos", sTitulo);
                            else if (HechosFecha.HasValue && Convert.ToDateTime(HechosFecha).Date > DateTime.Today)
                                resultado = string.Format("La fecha{0} de los hechos no puede ser mayor a la fecha actual", sTitulo);
                            else if (HechosFecha.HasValue && DA != null && DA.TomaDeclaracion != null
                                && Convert.ToDateTime(HechosFecha).Date > Convert.ToDateTime(DA.TomaDeclaracion.FechaDeclaracion).Date)
                                resultado = string.Format("La fecha{0} de los hechos no puede ser mayor a la fecha de declaracion {1}", sTitulo, DA.TomaDeclaracion.FechaDeclaracion.ToString());
                            else if (this.Contenedor is clsAnexo05)
                                resultado = ((clsAnexo05)this.Contenedor).EvaluarFechas(this);
                        }
                        break;                         

                    case "HechosDepartamento":
                        if (!HechosDepartamento.HasValue && !SkipValidation)
                            resultado = string.Format("Registre el departamento{0} en el que ocurrieron", sTitulo);
                        break;
                    case "HechosMunicipio":
                        if (!HechosMunicipio.HasValue && !SkipValidation)
                            resultado = string.Format("Registre el municipio{0} en el que ocurrieron", sTitulo);
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
