using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ruv.WPF.Captura.Infrastructure.Impresion
{
    public class clsConfiguracionImpresion
    {
        public clsConfiguracionImpresion()
        {
            NumeroCopias = 1;
        }


        

        public int Id { get; set; }

        private string _ImpresoraPreferida;
        public string ImpresoraPreferida
        {
            get { return _ImpresoraPreferida; }
            set { _ImpresoraPreferida = value; }
        }

        private eTipoPapel _TipoPapel;
        /// <summary>
        /// El tipo del papel: Cart u Oficio.
        /// </summary>
        public eTipoPapel TipoPapel
        {
            get { return _TipoPapel; }
            set
            {
                _TipoPapel = value;
                // Establecer el tamaño del papel.
                switch (value)
                {
                    case eTipoPapel.Carta:
                        PapelLadoLargo = 1056d;
                        PapelLadoPequeño = 816d;
                        break;
                    case eTipoPapel.Oficio:
                        PapelLadoLargo = 1248d;
                        PapelLadoPequeño = 816d;
                        break;
                    default:
                        PapelLadoLargo = 1056d;
                        PapelLadoPequeño = 816d;
                        break;
                }
            }
        }

        /// <summary>
        /// El lado largo del papel.
        /// </summary>
        public double PapelLadoLargo { get; set; }

        /// <summary>
        /// El lado peuqeño del papel.
        /// </summary>
        public double PapelLadoPequeño { get; set; }

        /// <summary>
        /// El margen del papel.
        /// </summary>
        public Thickness MargenPapel { get; set; }

        /// <summary>
        /// El número de copias por defecto.
        /// </summary>
        public int NumeroCopias { get; set; }

        /// <summary>
        /// Graba localmente esta configuración.
        /// </summary>
        public void Grabar()
        {
            RUV.I.LocalDB.Save<clsConfiguracionImpresion>(this);
            RUV.I.LocalDB.Flush();
        }

    }
}
