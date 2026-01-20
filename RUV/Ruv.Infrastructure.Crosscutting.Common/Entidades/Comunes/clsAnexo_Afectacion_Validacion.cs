using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Clase genérica con información sobre la afectación.
    /// </summary>
    public partial class clsAnexo_Afectacion : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                string resultado = null;
                switch (columnName)
                {
                    case "Afectado":
                        if (!Afectado.HasValue)
                            resultado = "Indique si ha resultado afectados o no con ocasión a estos hechos";
                        break;
                    case "TiposDeAfectacion":
                        if (Afectado.HasValue)
                        {
                            if (!TiposDeAfectacion.Any() && Afectado.Value == 1)
                                resultado = "Indique el tipo de afectación";
                            else if (TiposDeAfectacion.Any() && Afectado.Value == 0)
                                resultado = "Ha marcado tipo de afectación, debe indicar que la persona resultó afectada";
                        }
                        else if (TiposDeAfectacion.Any())
                            resultado = "Ha marcado tipo de afectación, debe indicar que la persona resultó afectada";

                        if (TiposDeAfectacion.Any() && TiposDeAfectacion.Contains((int)eTiposDeAfectacion.Muerte))
                        {
                            var DA = clsDeclaracion.DeclaracionActual;
                            
                            if (DA != null)
                            {
                                if(DA.TomaDeclaracion.DeclaranteId == this.AfectadoId)
                                    resultado = "El declarante no puede estar marcado con la afectación muerte";

                                //Segun correo de Nasly Lopez 27Mar2012 este cambio queda pendiente hasta nuevo analisis
                                /*
                                var victima8 = Victima as clsAnexo08_Victima;

                                if (victima8 != null 
                                    && (victima8.SituacionActualVictima == (int)eSituacionVictimaSecuestro.CAUTIVA 
                                        || victima8.SituacionActualVictima == (int)eSituacionVictimaSecuestro.LIBRE))
                                    resultado = "AFECTACION: Marcó la víctima con la afectación 'Muerte', por lo tanto no puede marcarla como 'Libre' o 'Cautiva'";
                                */
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
