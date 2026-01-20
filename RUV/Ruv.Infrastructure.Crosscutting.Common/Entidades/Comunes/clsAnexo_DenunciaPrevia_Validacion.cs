using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Clase genérica con información sobre una denuncia previa.
    /// </summary>

    public partial class clsAnexo_DenunciaPrevia : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                string resultado = null;
                switch (columnName)
                {
                    //Luis.Esteban 18 Ene 2012 Solo se deja la validacion de la fecha mayor a la actual, por solicitud de Alexander Holgin
                    case "SePresento":
                        if (!SePresento.HasValue)
                            resultado = "Indique si el(la) Declarante ya había realizado alguna denuncia o declaración por este mismo evento";
                        break;
                    //case "Entidad":
                    //    if (!Entidad.HasValue)
                    //    {
                    //        if (SePresento.HasValue)
                    //            if (SePresento.Value == 1)
                    //                resultado = "Debe indicar la entidad donde se realizó la denuncia o declaración";
                    //    }
                    //    else
                    //    {
                    //        if (!SePresento.HasValue)
                    //            resultado = "Ha marcado una entidad, debe marcar que presentó denuncia previa";
                    //        else if (SePresento.Value == 0)
                    //            resultado = "Ha marcado una entidad, debe marcar que presentó denuncia previa";
                    //    }                            
                    //    break;
                    case "Fecha":
                        if (Fecha.HasValue && Fecha.Value > DateTime.Today)
                            resultado = "La fecha de denuncia no puede ser posterior a la fecha actual";
                        //if (AnexoPadre is clsAnexo01)
                        //    if (Fecha.HasValue && Fecha.Value < ((clsAnexo01)this.AnexoPadre).FechaYLugar.HechosFecha)
                        //        resultado =string.Format("La fecha de denuncia ({0}) no puede ser anterior a la fecha de los hechos ({1})",
                        //            System.Convert.ToDateTime(Fecha.Value).ToString("dd MMM yyyy") ,
                        //            System.Convert.ToDateTime(((clsAnexo01)this.AnexoPadre).FechaYLugar.HechosFecha).ToString("dd MMM yyyy"));

                        if (AnexoPadre != null)
                        {
                            try
                            {
                                if (Fecha.HasValue && Fecha.Value < AnexoPadre.HechosFecha)
                                    resultado = string.Format("La fecha de denuncia ({0}) no puede ser anterior a la fecha de los hechos ({1})",
                                        System.Convert.ToDateTime(Fecha.Value).ToString("dd MMM yyyy"),
                                        System.Convert.ToDateTime(AnexoPadre.HechosFecha).ToString("dd MMM yyyy"));
                            }
                            catch (InvalidOperationException)
                            {
                                //resultado = "La fecha de denuncia no puede ser evaluada puesto que no se tiene la fecha de los hechos.";
                            }
                        }

                        //if(Fecha.HasValue && Fecha.Value < fechaHechos  
                        break;
                    //case "Departamento":
                    //    if (!Departamento.HasValue)
                    //    {
                    //        if (SePresento.HasValue)
                    //            if (SePresento.Value == 1)
                    //                resultado = "Debe indicar el departamento de la denuncia o declaración";
                    //    }
                    //    else
                    //    {
                    //        if (!SePresento.HasValue)
                    //            resultado = "Ha indicado el departamento de la denuncia, debe marcar que presentó denuncia previa";
                    //        else if (SePresento.Value == 0)
                    //            resultado = "Ha indicado el departamento de la denuncia, debe marcar que presentó denuncia previa";
                    //    }
                    //    break;
                    //case "Municipio":
                    //    if (!Municipio.HasValue)
                    //    {
                    //        if (SePresento.HasValue)
                    //            if (SePresento.Value == 1)
                    //                resultado = "Debe indicar el municipio de la denuncia o declaración";
                    //    }
                    //    else
                    //    {
                    //        if (!SePresento.HasValue)
                    //            resultado = "Ha indicado el municipio de la denuncia, debe marcar que presentó denuncia previa";
                    //        else if (SePresento.Value == 0)
                    //            resultado = "Ha indicado el municipio de la denuncia, debe marcar que presentó denuncia previa";
                    //    }
                    //    break;
                    //case "Codigo":
                    //    if (!string.IsNullOrWhiteSpace(Codigo))
                    //    {
                    //        if (SePresento.HasValue)
                    //            if (SePresento.Value == 0)
                    //                resultado = "Ha indicado el código de la denuncia, debe marcar que presentó denuncia previa";
                    //    }
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
