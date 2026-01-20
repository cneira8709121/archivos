using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Ruv.WPF.Captura.Utils
{
    public class clsTextBoxFilterBehavior : Behavior<TextBox>
    {
        #region ATTACH AND DETACH

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }

        /// <summary>
        /// Ejecutar el filtro de teclado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (ConjuntoDeCaracteres)
            {
                case eConjuntoDeCaracteres.OmitirDigitos:
                    if (!FiltroOmitirDigitos(e))
                        e.Handled = true;
                    break;

                case eConjuntoDeCaracteres.LetrasEspacios:
                    if (!FiltroSoloLetrasEspacios(e))
                        e.Handled = true;
                    break;

                case eConjuntoDeCaracteres.SoloDigitos:
                    if (!FiltroSoloDigitos(e))
                        e.Handled = true;
                    break;

                case eConjuntoDeCaracteres.SoloDigitosExcluyendoCero:
                    if (!FiltroSoloDigitosExcluyendoCero(e))
                        e.Handled = true;
                    break;

                case eConjuntoDeCaracteres.LetrasNumeros:
                    if (!FiltroSoloLetrasNumeros(e))
                        e.Handled = true;
                    break;
            }
        }

        #endregion

        private eConjuntoDeCaracteres _ConjuntoCaracteres = eConjuntoDeCaracteres.OmitirDigitos;
        /// <summary>
        /// El conjunto de caracteres que se pueden o no utilizar.
        /// </summary>
        public eConjuntoDeCaracteres ConjuntoDeCaracteres
        {
            get { return _ConjuntoCaracteres; }
            set { _ConjuntoCaracteres = value; }
        }

        #region FILTROS

        /// <summary>
        /// Cualquier caracter excepto dígitos.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Verdadero: Cumple con el filtro.</returns>
        public static bool FiltroOmitirDigitos(System.Windows.Input.KeyEventArgs e)
        {
            return !(e.KeyboardDevice.Modifiers == ModifierKeys.None
                  && (e.Key >= Key.D0 && e.Key <= Key.D9)
                  || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9));
        }

        /// <summary>
        /// Sólo son válidas las letras y los espacios.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool FiltroSoloLetrasEspacios(System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z)
              || e.Key == Key.Oem3 // Letra Ñ.
              || e.Key == Key.Space)
                return true;

            if (TeclasEdicion.Contains(e.Key))
                return true;

            // Los acentos.
            if (e.Key == Key.Oem1 || e.Key == Key.DeadCharProcessed)
                return true;

            return false;
        }

        /// <summary>
        /// Sólo son válidas los digitos [0-9].
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool FiltroSoloDigitos(System.Windows.Input.KeyEventArgs e)
        {
            return (e.KeyboardDevice.Modifiers == ModifierKeys.None
                && (TeclasEdicion.Any(x => x == e.Key)
                || (e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)));
        }

        /// <summary>
        /// Sólo son válidas los digitos [1-9].
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool FiltroSoloDigitosExcluyendoCero(System.Windows.Input.KeyEventArgs e)
        {
            return (e.KeyboardDevice.Modifiers == ModifierKeys.None
                && (TeclasEdicion.Any(x => x == e.Key)
                || (e.Key >= Key.D1 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad1 && e.Key <= Key.NumPad9)));
        }

        /// <summary>
        /// Sólo son válidas las letras y numeros.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool FiltroSoloLetrasNumeros(System.Windows.Input.KeyEventArgs e)
        {
            //Validar si es una letra
            if ((e.Key >= Key.A && e.Key <= Key.Z)
              || e.Key == Key.Oem3 // Letra Ñ.
              )
                return true;

            //Validar si es un Numero
            if (e.KeyboardDevice.Modifiers == ModifierKeys.None
                  && (TeclasEdicion.Any(x => x == e.Key)
                  || (e.Key >= Key.D0 && e.Key <= Key.D9)
                  || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
                return true;

            if (TeclasEdicion.Contains(e.Key))
                return true;

            return false;
        }

        static System.Windows.Input.Key[] _TeclasEdicion;

        /// <summary>
        /// Retorna lista de teclas estándares de edición.
        /// </summary>
        static System.Windows.Input.Key[] TeclasEdicion
        {
            get
            {
                if (_TeclasEdicion == null)
                {
                    _TeclasEdicion = new Key[] { 
            Key.Back, Key.Delete, Key.Down, Key.End, Key.Home, 
            Key.Insert, Key.Left, Key.LeftCtrl, Key.LeftShift, 
            Key.PageDown, Key.PageUp, Key.Right, Key.RightCtrl,
            Key.LeftAlt, Key.RightAlt, Key.RightShift, Key.Up,
            Key.Capital, Key.Tab, Key.F1, Key.F2, Key.F3, Key.F4,
            Key.F5, Key.F6, Key.F7, Key.F8,Key.F9,
            Key.F10, Key.F11, Key.F12, Key.Return, Key.Enter,
            Key.LeftShift, Key.RightShift
          };
                }

                return _TeclasEdicion;
            }
        }

        #endregion

    }

    public enum eConjuntoDeCaracteres
    {
        /// <summary>
        /// Todos los caracteres son permitidos excepto los dígitos.
        /// </summary>
        OmitirDigitos,
        /// <summary>
        /// Sólo letras y espacios.
        /// </summary>
        LetrasEspacios,
        /// <summary>
        /// Solo los ígitos [0-9] son admintidos.
        /// </summary>
        SoloDigitos,
        /// <summary>
        /// Solo los ígitos [1-9] son admintidos.
        /// </summary>
        SoloDigitosExcluyendoCero,
        /// <summary>
        /// Sólo Letras y numeros son admitidos
        /// </summary>
        LetrasNumeros
    }
}