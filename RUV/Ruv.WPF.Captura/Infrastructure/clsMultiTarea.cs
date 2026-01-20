using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Ruv.WPF.Captura.Infrastructure
{
    /// <summary>
    /// Facilita la ejecución de procesos en el background.
    /// </summary>
    public class clsMultiTarea
    {

        public void PosponerEjecucion(int milisegundos,
          Action metodo)
        {
            DispatcherTimer Temporizador = new DispatcherTimer();
            Temporizador.Tick += new EventHandler(PosponerEjecucion_Tick);
            Temporizador.Tag = metodo;
            Temporizador.Interval = new TimeSpan(0, 0, 0, 0, milisegundos);
            Temporizador.Start();
        }

        void PosponerEjecucion_Tick(object sender, EventArgs e)
        {
            DispatcherTimer Temporizador = sender as DispatcherTimer;
            Temporizador.Tick -= PosponerEjecucion_Tick;
            (Temporizador.Tag as Action)();
            Temporizador.Stop();
            Temporizador = null;
        }
        Exception BackgroundException;
        BackgroundWorker BW;
        public void EjecutarEnBackground(Action metodo, Action metodoAlFinalizar = null)
        {
            BW = new BackgroundWorker();
            BW.DoWork += EjecutarEnBackground_DoWork;
            if (metodoAlFinalizar != null)
                BW.RunWorkerCompleted += EjecutarEnBackground_RunWorkerCompleted;
            BW.RunWorkerAsync(new Tuple<Action, Action>(metodo, metodoAlFinalizar));
        }
        public void DetenerBackground()
        {
            if (BW != null)
            {
                try
                {
                    BW.Dispose();
                }
                catch
                {
                }
            }
        }
        void EjecutarEnBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (BackgroundException != null)
                MostrarMensajeExcepcion(BackgroundException);
            Action Accion = e.Result as Action;
            if (Accion != null)
                Accion();
        }

        void EjecutarEnBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            var Acciones = e.Argument as Tuple<Action, Action>;
            try
            {
                BackgroundException = null;
                Acciones.Item1();
            }
            catch (Exception ex)
            {
                BackgroundException = ex;
            }
            e.Result = Acciones.Item2;
        }

        void MostrarMensajeExcepcion(Exception ex)
        {
            if (BackgroundException is System.TimeoutException)
            {
                Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "", "Mensaje", "Agotado tiempo de espera del servicio, por favor intente nuevamente.");
                DE.ShowDialog();
            }
            else
            {
                if (ex.Message.IndexOf("RADICACION_REPETIDA") > -1)
                {
                    Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "Radicación", "Problemas almacenando radicación", "\"Número de formulario\" repedito, ya esta registrado. Verifique por favor.");
                    DE.ShowDialog();
                }
                else if (ex.Message.IndexOf("ORA-20001") > -1 && ex.Message.IndexOf("SP_SETRADICACION ERR-FECHAEXCEPCION") > -1)
                {
                    Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "Radicación", "Problemas almacenando radicación", "La fecha no puede ser mayor a la fecha actual del sistema");
                    DE.ShowDialog();
                }
                else
                {
                    Ruv.WPF.Captura.Seguridad.DesplegarException DE = new Ruv.WPF.Captura.Seguridad.DesplegarException(ex);
                    DE.EsControlada = true;
                    DE.ShowDialog();
                }
            }
        }

    }

}
