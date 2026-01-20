using System.Windows.Input;

namespace Ruv.WPF.Captura.Registro
{
    /// <summary>
    /// Operaciones realizadas a través de comandos.
    /// </summary>
    class ComandosRegistro
    {

        #region CONSTRUCTOR

        static ComandosRegistro()
        {
            // Registrar los atajos de teclado.
            CrearGesto(out _GrabarDeclaracion, "GrabarDeclaracion", Key.G, ModifierKeys.Control);
            CrearGesto(out _UbicarSiguienteError, "UbicarSiguienteError", Key.F8, ModifierKeys.None);
            CrearGesto(out _UbicarAnteriorError, "UbicarAnteriorError", Key.F7, ModifierKeys.None);
            CrearGesto(out _AbrirListaDeTareas, "AbrirListaDeTareas", Key.L, ModifierKeys.Control);
            CrearGesto(out _AbrirListaDeAnexos, "AbrirListaDeAnexos", Key.A, ModifierKeys.Control);
            CrearGesto(out _AbrirHoja01, "AbrirHoja01", Key.NumPad1, ModifierKeys.Control);
            CrearGesto(out _AbrirHoja02, "AbrirHoja02", Key.NumPad2, ModifierKeys.Control);
            CrearGesto(out _AbrirHoja03, "AbrirHoja03", Key.NumPad3, ModifierKeys.Control);
            CrearGesto(out _AbrirHoja04, "AbrirHoja04", Key.NumPad4, ModifierKeys.Control);
        }

        /// <summary>
        /// Crear un gesto de teclado para una comando.
        /// </summary>
        /// <param name="comando"></param>
        /// <param name="nombre"></param>
        /// <param name="tecla"></param>
        /// <param name="modificador"></param>
        static void CrearGesto(out RoutedUICommand comando, string nombre, Key tecla, ModifierKeys modificador)
        {
            InputGestureCollection IGC = new InputGestureCollection();
            IGC.Add(new KeyGesture(tecla, modificador));
            comando =
              new RoutedUICommand(nombre, nombre, typeof(ComandosRegistro), IGC);
        }

        #endregion

        private static RoutedUICommand _UbicarSiguienteError;
        /// <summary>
        /// Posiciona el foco en el siguiente error.
        /// </summary>
        public static RoutedUICommand UbicarSiguienteError
        {
            get { return _UbicarSiguienteError; }
            set { _UbicarSiguienteError = value; }
        }

        private static RoutedUICommand _UbicarAnteriorError;
        /// <summary>
        /// Posiciona el foco en el ANTERIOR error.
        /// </summary>
        public static RoutedUICommand UbicarAnteriorError
        {
            get { return _UbicarAnteriorError; }
            set { _UbicarAnteriorError = value; }
        }

        private static RoutedUICommand _GrabarDeclaracion;
        /// <summary>
        /// Graba la declaración actual.
        /// </summary>
        public static RoutedUICommand GrabarDeclaracion
        {
            get { return _GrabarDeclaracion; }
            set { _GrabarDeclaracion = value; }
        }

        private static RoutedUICommand _AbrirHoja01;
        /// <summary>
        /// Abre la hoja 01
        /// </summary>
        public static RoutedUICommand AbrirHoja01
        {
            get { return _AbrirHoja01; }
            set { _AbrirHoja01 = value; }
        }

        private static RoutedUICommand _AbrirHoja02;
        /// <summary>
        /// Abre la hoja 02
        /// </summary>
        public static RoutedUICommand AbrirHoja02
        {
            get { return _AbrirHoja02; }
            set { _AbrirHoja02 = value; }
        }

        private static RoutedUICommand _AbrirHoja03;
        /// <summary>
        /// Abre la hoja 03
        /// </summary>
        public static RoutedUICommand AbrirHoja03
        {
            get { return _AbrirHoja03; }
            set { _AbrirHoja03 = value; }
        }

        private static RoutedUICommand _AbrirHoja04;
        /// <summary>
        /// Abre la hoja 04
        /// </summary>
        public static RoutedUICommand AbrirHoja04
        {
            get { return _AbrirHoja04; }
            set { _AbrirHoja04 = value; }
        }

        private static RoutedUICommand _AbrirListaDeTareas;
        /// <summary>
        /// Abre la lista de tareas.
        /// </summary>
        public static RoutedUICommand AbrirListaDeTareas
        {
            get { return _AbrirListaDeTareas; }
            set { _AbrirListaDeTareas = value; }
        }

        private static RoutedUICommand _AbrirListaDeAnexos;
        /// <summary>
        /// Abre la ventana de anexos.
        /// </summary>
        public static RoutedUICommand AbrirListaDeAnexos
        {
            get { return _AbrirListaDeAnexos; }
            set { _AbrirListaDeAnexos = value; }
        }
    }
}