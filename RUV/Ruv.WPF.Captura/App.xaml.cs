using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Ruv.WPF.Captura.Infrastructure.LocalStorage;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Seguridad;
using System.Management;

namespace Ruv.WPF.Captura
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Determinar el modo de ejecución.
            RUV.I.ModoEjecucion = (Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion)
              Ruv.WPF.Captura.Properties.Settings.Default.ModoEjecucion;

            base.OnStartup(e);

            // Lanzar la pre-ejecución.
            var PE = new clsPreEjecucion();
            PE.Ejecutar();

            // Este handler dá como válido el certificado que no se puede verificar, 
            // en caso de hacer requerimientos Request no-relacionados con WCF.
            System.Net.ServicePointManager.ServerCertificateValidationCallback
              += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };

            // Agregar el objeto principal a la colección de recursos.
            if (Resources.Contains("RUV"))
                Resources["RUV"] = RUV.I;
            else
                Resources.Add("RUV", RUV.I);

            DatabaseService.Start();

            // =================================================
            //Modificar el usuario que esta almacenado en la cola de procesos
            //var LaCola = Sipod.I.LocalDB.Query<Ruv.WPF.Captura.Infrastructure.ColaProcesos.clsProceso, string>().Select(x => x.LazyValue.Value).ToList();
            //Sipod.I.LocalDB.Truncate(typeof(Ruv.WPF.Captura.Infrastructure.ColaProcesos.clsProceso));
            //foreach (var item in LaCola)
            //{
            //    if (item.Id.Length > 36)
            //        item.Id = item.Id.Substring(0, 36) + "12769";
            //    Sipod.I.LocalDB.Save<Ruv.WPF.Captura.Infrastructure.ColaProcesos.clsProceso>(item);
            //}
            //Sipod.I.LocalDB.Flush();

            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Warning;


        }

        /// <summary>
        /// Tratar de coger todos los errores no manejados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Pasar el control a la ventana de error que registra la excepción.
            e.Handled = true;
            DesplegarException DE = new DesplegarException(e.Exception);
            DE.ShowDialog();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            DatabaseService.Stop();
        }

    }
}
