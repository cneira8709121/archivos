using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using Ruv.WPF.Captura.Infrastructure.Configuracion;
using Ruv.WPF.Captura.Infrastructure.Impresion;

namespace Ruv.WPF.Captura.Infrastructure
{
    /// <summary>
    /// Toda la funcionalidad de impresión.
    /// </summary>
    public partial class clsImpresion
    {
        public clsImpresion()
        {

        }

        #region CONFIGURACION

        /// <summary>
        /// Retorna la lista de las impresoras en el sistema.
        /// </summary>
        /// <returns></returns>
        public List<string> ListaImpresoras
        {
            get
            {
                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueueCollection printQueuesOnLocalServer =
                  printServer.GetPrintQueues(
                  new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
                return printQueuesOnLocalServer.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Aqui se establecen los parámetros de impresión iniciales.
        /// </summary>
        public void ConfiguracionInicialImpresora()
        {
            RUV.I.MultiTarea.PosponerEjecucion(100,
              new Action(() => ConfiguracionInicialImpresoraAsync()));
        }

        /// <summary>
        /// Aqui se establecen los parámetros de impresión iniciales (asíncrono).
        /// </summary>
        void ConfiguracionInicialImpresoraAsync()
        {
            clsConfiguracion Config = RUV.I.Configuraciones;

            try
            {
                if (RUV.I.LocalDB.Query<clsConfiguracion, int>().Any())
                {
                    Config = RUV.I.LocalDB.Query<clsConfiguracion, int>()
                      .Select(x => x.LazyValue.Value).FirstOrDefault();
                }
            }
            catch { }

            if (Config == null)
            {
                Config = new clsConfiguracion { Id = 1 };
                // Si no hay impresora preferida, usar la que está por defecto.
                EstablecerDefectoComoPreferida(ref Config);
            }

            // Verificar si la impresora preferida aun existe en el sistema.
            if (!ImpresorasDelSistema.Contains(Config.Impresion.Configuracion.ImpresoraPreferida))
                EstablecerDefectoComoPreferida(ref Config);


            RUV.I.Configuraciones = Config;
            // Almacenar los cambios en la configuración.
            RUV.I.LocalDB.Save<clsConfiguracion>(Config);
            RUV.I.LocalDB.Flush();

            //Configuracion = Config;
        }

        private clsConfiguracionImpresion _configuracion;
        /// <summary>
        /// Información de configuración de impresión.
        /// </summary>
        public clsConfiguracionImpresion Configuracion
        {
            get
            {
                if (_configuracion == null) _configuracion = new clsConfiguracionImpresion();
                return _configuracion;
            }
        }

        /// <summary>
        /// Establece la preferida como la impresora por defecto, y el papel en tamaño carta,
        /// </summary>
        /// <param name="config"></param>
        void EstablecerDefectoComoPreferida(ref clsConfiguracion config)
        {
            var Cola = ImpresoraPorDefectoDelSistema;
            config.Impresion.Configuracion.ImpresoraPreferida = Cola.Name;
            config.Impresion.Configuracion.TipoPapel = eTipoPapel.Carta;
            config.Impresion.Configuracion.MargenPapel = new System.Windows.Thickness(27d);
        }

        /// <summary>
        /// Retorna la impresora por defecto del sistema.
        /// </summary>
        /// <returns></returns>
        public PrintQueue ImpresoraPorDefectoDelSistema
        {
            get
            {
                PrintQueue ColaPorDefecto = LocalPrintServer.GetDefaultPrintQueue();
                return ColaPorDefecto;
            }
        }

        /// <summary>
        /// Los nombres de todas las impresoras del sistema.
        /// </summary>
        public List<string> ImpresorasDelSistema
        {
            get
            {
                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueueCollection printQueuesOnLocalServer =
                  printServer.GetPrintQueues(
                  new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
                return printQueuesOnLocalServer.Select(x => x.Name).ToList();
            }
        }

        public List<int> ListadoNumeroCopias
        {
            get
            {
                return new List<int>() { 1, 2, 3, 4 };
            }
        }

        #endregion
    }
}