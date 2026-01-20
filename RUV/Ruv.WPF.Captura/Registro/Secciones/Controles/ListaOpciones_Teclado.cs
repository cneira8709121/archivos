using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Registro.Secciones
{
  /// <summary>
  /// Lógica de interacción con el teclado.
  /// Manejo la selección de opciones por el número respectivo.
  /// </summary>
    public partial class ListaOpciones : UserControl
    {
        private void sp01_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (CajaOtroTexto != null && CajaOtroTexto.IsFocused) return;

            // Sólo está habilitado al mostrar los números.
            if (!IncluirNumero) return;

            var Digito = ObtenerDigito(e.Key);

            if (Digito.HasValue)
            {
                // SI es un dígito.
                ArmarNumero(Digito);
                var ElControl = BuscarElementoCoincidente();
                //var ElControl = BuscarElementoUnico();
                if (ElControl != null)
                {
                    txtNumero.Text = NumeroDigitado;
                    MarcarDesmarcar(ElControl);
                    ResetearNumero();
                    return;
                }

                if (NumeroTieneDosDigitos())
                {
                    ResetearNumero();
                    return;
                }
                else
                {
                    // Si tiene un dígito, hacerlo visible.
                    txtNumero.Text = NumeroDigitado;
                    borNumero.Tag = 1;
                }

                return;
            }

            if (e.Key == Key.Delete)
            {
                // Return o Delete resetean la operación.
                ResetearNumero();
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Back)
            {
                // Borrar un dígito del número digitado.
                QuitarUltimoDigito();
                if (NumeroSinCifras())
                {
                    ResetearNumero();
                }
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Return)
            {
                // Al presionar Enter, tratar de seleccionar el elemento que se pueda encontrar.
                if (NumeroTieneUnDigito())
                {
                    var Item = BuscarElementoUnico();
                    if (Item != null)
                    {
                        MarcarDesmarcar(Item);
                    }
                }
                ResetearNumero();
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Tab)
            {
                // La tecla tab resetea la operación y deja seguir el foco.
                ResetearNumero();
            }

            if (e.Key == Key.Escape)
            {
                // La tecla escape cancela la edición.
                if (!NumeroSinCifras())
                    e.Handled = true;
                ResetearNumero();
            }

            return;
        }

        #region UTILITARIOS

        string NumeroDigitado = null;

        /// <summary>
        /// Retorna el único elemento coincidente, si existe.
        /// </summary>
        /// <returns></returns>
        ToggleButton BuscarElementoCoincidente()
        {
            var ListaAciertos = from x in wp01.Children.OfType<ToggleButton>()
                                where (x.Tag as clsElementoSeleccionable).Texto.EndsWith(")")
                                select new
                                {
                                    ElControl = x,
                                    Número = ExtraerNumero((x.Tag as clsElementoSeleccionable).Texto)
                                };


            if (!ListaAciertos.Any()) return null;

            var Encontrados = ListaAciertos.Where(x => x.Número.StartsWith(NumeroDigitado));

            if (Encontrados.Count() == 1)
                return Encontrados.ElementAt(0).ElControl;

            return null;
        }

        /// <summary>
        /// Marca o desmarca el control.
        /// </summary>
        /// <param name="elControl"></param>
        void MarcarDesmarcar(ToggleButton elControl)
        {
            elControl.IsChecked = !elControl.IsChecked;
            elControl.Focus();
        }

        /// <summary>
        /// Extraer de la cadena el número correspondiente al código del item.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        string ExtraerNumero(string texto)
        {
            var Espacio = texto.LastIndexOf(' ');
            if (Espacio == -1) return null;
            texto = texto.Substring(Espacio + 2, (texto.Length - Espacio) - 2);
            texto = texto.Substring(0, texto.Length - 1);
            return texto;
        }

        /// <summary>
        /// Resetea la operación de ingreso de número.
        /// </summary>
        void ResetearNumero()
        {
            NumeroDigitado = null;
            borNumero.Tag = null;
        }

        /// <summary>
        /// True: El número digitado cuenta con dos dígitos.
        /// </summary>
        /// <returns></returns>
        bool NumeroTieneDosDigitos()
        {
            return NumeroDigitado.Length == 2;
        }

        /// <summary>
        /// True: El número digitado cuenta con un dígito y el item existe.
        /// </summary>
        /// <returns></returns>
        bool NumeroTieneUnDigito()
        {
            return NumeroDigitado != null && NumeroDigitado.Length == 1;
        }

        /// <summary>
        /// Retorna el elemento con el número exacto.
        /// </summary>
        /// <returns></returns>
        ToggleButton BuscarElementoUnico()
        {
            var ListaAciertos = from x in wp01.Children.OfType<ToggleButton>()
                                where (x.Tag as clsElementoSeleccionable).Texto.EndsWith(")")
                                select new
                                {
                                    ElControl = x,
                                    Número = ExtraerNumero((x.Tag as clsElementoSeleccionable).Texto)
                                };

            var Acierto = ListaAciertos.FirstOrDefault(x => x.Número == NumeroDigitado);
            if (Acierto == null)
                return null;
            else
                return Acierto.ElControl;
        }

        /// <summary>
        /// Agrega el dígito al número.
        /// </summary>
        /// <param name="digito"></param>
        void ArmarNumero(int? digito)
        {
            NumeroDigitado += digito.ToString();
        }

        /// <summary>
        /// Verdadero: el número digitado no tiene digitos.
        /// </summary>
        /// <returns></returns>
        bool NumeroSinCifras()
        {
            return string.IsNullOrWhiteSpace(NumeroDigitado);
        }

        /// <summary>
        /// Quita el último digito del número digitado.
        /// </summary>
        void QuitarUltimoDigito()
        {
            if (NumeroDigitado == null || NumeroDigitado.Any()) return;
            if (NumeroDigitado.Length == 2)
                NumeroDigitado = NumeroDigitado[0].ToString();
            else
                NumeroDigitado = null;
        }

        /// <summary>
        /// Obtiene el dígito representado por la tecla.
        /// </summary>
        /// <param name="tecla"></param>
        /// <returns></returns>
        int? ObtenerDigito(Key tecla)
        {
            if ((tecla >= Key.D0 && tecla <= Key.D9) || (tecla >= Key.NumPad0 && tecla <= Key.NumPad9))
            {
                string NumeroTxt = tecla.ToString();
                NumeroTxt = NumeroTxt.Substring(NumeroTxt.Length - 1, 1);
                return Convert.ToInt32(NumeroTxt);
            }
            else
                return null;
        }

        #endregion
    }
}
