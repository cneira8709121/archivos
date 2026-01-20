using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ruv.WPF.Captura.Infrastructure.ColaProcesos;
using System.Collections.Specialized;
using Ruv.WPF.Captura.Infrastructure;
using Microsoft.Win32;
using System.IO;
using Ruv.WPF.Captura.Controles;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.WPF.Captura.Registro.Colilla;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.ListaTareas
{
    /// <summary>
    /// Interaction logic for ColaProcesos.xaml
    /// </summary>
    public partial class ColaProcesos : Page
    {
        #region CONSTRUCTOR

        public ColaProcesos()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ColaProcesos_Loaded);
        }

        void ColaProcesos_Loaded(object sender, RoutedEventArgs e)
        {
            btnPuargarCola.Visibility = RUV.I.Configuraciones.ConfiguracionGeneral.PermitirPurgarColaProcesos ?
              Visibility.Visible : Visibility.Collapsed;

            // Llenar la lista de los años.
            var ListaAños = new List<Infrastructure.clsItem>();
            for (int i = 2011; i <= DateTime.Now.Year; i++)
                ListaAños.Add(new Ruv.WPF.Captura.Infrastructure.clsItem
                {
                    Id = i,
                    Nombre = i.ToString()
                });

            cbxAño.ItemsSource = ListaAños;

            // LLenar la lista de los meses.
            var Meses = (new System.Globalization.CultureInfo("es-co")).DateTimeFormat.MonthNames;
            var ListaMeses = new List<Infrastructure.clsItem>();
            for (int i = 1; i < 13; i++)
            {
                ListaMeses.Add(new Infrastructure.clsItem
                {
                    Id = i,
                    Nombre = Meses[i - 1]
                });
            }
            cbxMes.ItemsSource = ListaMeses;

            // Seleccionar el año y mes actual.
            var AñoActual = ListaAños.FirstOrDefault(x => x.Id == DateTime.Now.Year);
            var MesActual = ListaMeses.FirstOrDefault(x => x.Id == DateTime.Now.Month);

            cbxAño.SelectedItem = AñoActual;
            cbxMes.SelectedItem = MesActual;

            cbxAño.SelectionChanged += new SelectionChangedEventHandler(CambioFiltroHistorico);
            cbxMes.SelectionChanged += new SelectionChangedEventHandler(CambioFiltroHistorico);

            // Suscribirse a los cambios en la lista de procesos históricos.
            //Sipod.I.ColaProcesos.ListaProcesos.CollectionChanged +=
            //  ListaProcesos_CollectionChanged;

            ActualizarFiltroHistorico();


        }

        void ListaProcesos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ActualizarFiltroHistorico();
        }

        #endregion

        #region VER ERRORES Y ADVERTENCIAS

        private void VerErroresAdvertenciasDB(object sender, RoutedEventArgs e)
        {
            var Proceso = dgPendientes.SelectedItem as clsProceso;
            Proceso = Proceso ?? dgHistorico.SelectedItem as clsProceso;
            if (Proceso == null ||
              (Proceso.ErroresDB == null || Proceso.ErroresDB.Count == 0) &&
              (Proceso.AdvertenciasDB == null || Proceso.AdvertenciasDB.Count == 0))
                return;

            if (Ventana != null) Ventana.Close();

            if (Proceso.ErroresDB != null)
                Ventana = new Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion(
                  Proceso.ErroresDB.AsEnumerable().ToArray(), null);
            else if (Proceso.AdvertenciasDB != null)
                Ventana = new Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion(
                  null, Proceso.AdvertenciasDB.AsEnumerable().ToArray());


            Ventana.Topmost = true;
            Ventana.Show();
        }

        Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion Ventana;

        #endregion

        #region SELECCION DE DECLARACIÓN PARA EDICIÓN

        private void GrillaDobleClick(object sender, MouseButtonEventArgs e)
        {
            var Proceso = dgPendientes.SelectedItem as clsProceso;
            Proceso = Proceso ?? dgHistorico.SelectedItem as clsProceso;
            if (Proceso == null) return;

            switch ((eEstadoProcesoCola)Proceso.Estado)
            {
                case eEstadoProcesoCola.RequiereRevision:
                    // 1) Crear una copia de la declaración en la carpeta raiz.
                    RUV.I.ColaProcesos.LanzarEdicionProceso(Proceso);
                    break;

                case eEstadoProcesoCola.Transmitido:
                    // 2) Abrir la declaración en modo de sólo léctura.
                    RUV.I.ColaProcesos.LanzarEdicionProceso(Proceso);
                    break;
            }

        }

        void EditarDeclaracion(clsProceso proceso)
        {

        }

        #endregion

        #region PURGA DE LA COLA DE PROCESOS

        private void PurgarCola(object sender, RoutedEventArgs e)
        {
            if (RUV.I.UIPrincipal.UsuarioConfirmar(
              "¿Desea borrar completamente la cola de procesos?"))
                RUV.I.ColaProcesos.PurgarCola();
        }

        #endregion

        #region CAMBIO EN FILTRO DE HISTÓRICO

        void CambioFiltroHistorico(object sender, SelectionChangedEventArgs e)
        {
            ActualizarFiltroHistorico();
        }

        /// <summary>
        /// Actualizar el filtro de los históricos a mostrar.
        /// </summary>
        void ActualizarFiltroHistorico()
    {
      IEnumerable<clsProceso> Historico = null;

      var FiltroAño = cbxAño.SelectedItem as clsItem;
      var FiltroMes = cbxMes.SelectedItem as clsItem;

      if (RUV.I.LocalDB.Query<clsProceso, string>().Any())
        try
        {
          var ListaListr = from x in RUV.I.LocalDB.Query<clsProceso, string>()
                           where !string.IsNullOrWhiteSpace(x.Key)
                           select x.LazyValue.Value;

          var Lista2 = ListaListr.ToList();

          string usuarioId = RUV.I.Usuario.Id.ToString();

          Historico = ListaListr.ToList()
            .Where(x =>
              x.Estado == (int)eEstadoProcesoCola.Transmitido
              && x.FechaUltimaTransmision.HasValue
              && x.FechaUltimaTransmision.Value.Year == FiltroAño.Id
              && x.FechaUltimaTransmision.Value.Month == FiltroMes.Id

              // Diego Alvarez - 10/09/2013 - Filtrar historico por usuario logueado
              && x.Id.ToString().Substring(x.Id.Length - usuarioId.Length, usuarioId.Length) == usuarioId
              //&& ((x.Id.Length > 36 && x.Id.Substring(36).ToString() == Sipod.I.Usuario.Id.ToString()) || x.Id.Length == 36)  
              )
            .Select(x => x);
        }
        catch(Exception ex) 
        {
            string mensaje;
            mensaje = ex.Message; 
        }

      //Sipod.I.ColaProcesos.ListaHistoricos.Clear();
      if (Historico != null)
      {
          try
          {
              //Historico.OrderByDescending(x => x.FechaUltimaTransmision).ToList()
              //  .ForEach(x => Sipod.I.ColaProcesos.ListaHistoricos.Add(x));
              dgHistorico.ItemsSource = Historico
                .OrderByDescending(x => x.FechaUltimaTransmision).ToList();
          }
          catch(Exception ex) {  
          
          }

      }
      else
        dgHistorico.ItemsSource = null;
    }

        #endregion

        #region CERRAR ESTA PÁGINA

        private void Unload_ColaProcesos(object sender, RoutedEventArgs e)
        {
            RUV.I.ColaProcesos.ListaProcesos.CollectionChanged -=
              ListaProcesos_CollectionChanged;
        }

        #endregion

        #region CAMBIO HACIA EL TAB HISTÓRICO

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabHistorico.IsSelected)
            {
                ActualizarFiltroHistorico();
            }
        }

        #endregion

        private void Btn_ExportaExcel_Click(object sender, RoutedEventArgs e)
        {
            List<clsProceso> Historico = (List<clsProceso>)dgHistorico.ItemsSource;
            if (Historico == null || Historico.Count <= 0)
            {
                MessageBox.Show("No hay datos a exportar", resx::Globalization.Controles.Advertencia);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel file (2010)|*.xlsx";
            sfd.Title = "Save an Excel File";
            sfd.ShowDialog();

           // If the file name is not an empty string open it for saving.
           if (sfd.FileName != "")
           {
               RUV.I.UIPrincipal.BloquearInterfase = "Generando...";
               RUV.I.MultiTarea.EjecutarEnBackground((() =>
                   {
                       string cError = string.Empty;
                       byte[] ColaExcel = RUV.I.ColaProcesos.ExportarColaExcel(Historico, ref cError);

                       if (ColaExcel == null || !string.IsNullOrEmpty(cError))
                       {
                           MessageBox.Show(cError, resx::Globalization.Controles.Advertencia);
                           return;
                       }
                       using (FileStream fs = (FileStream)sfd.OpenFile())
                       {
                           foreach (byte b in ColaExcel)
                           {
                               fs.WriteByte(b);
                           }
                       }
                   }), () =>
                    {
                        Notificaciones notifica = new Notificaciones(sfd.FileName, resx::Globalization.Informacion.GeneradoCorrectamente);
                        RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                        RUV.I.UIPrincipal.BloquearInterfase = null;
                    });
           }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recibo colilla = new Recibo();

            clsProceso proceso = dgHistorico.SelectedItem as clsProceso;
            clsDeclaracion declaracion = null;
            if (RUV.I.ColaProcesos.PrepararInfoColilla(proceso, ref declaracion))
            {
                colilla.blkColilla.DataContext = declaracion;
                colilla.ShowDialog();
            }
        }

    }
}
