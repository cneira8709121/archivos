
namespace Ruv.WPF.Captura.DesignTime
{
    /// <summary>
    /// Realiza algunas acciones en tiempo de diseño.
    /// </summary>
    class clsDesignTime
    {
        public clsDesignTime()
        {
            // Esta clase sólo se usa cuando se esté en tiempo de diseño.
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName != "devenv"
              || App.Current.Resources.Contains("Sipod"))
                return;
            App.Current.Resources.Add("Sipod", RUV.I);
        }
    }
}
