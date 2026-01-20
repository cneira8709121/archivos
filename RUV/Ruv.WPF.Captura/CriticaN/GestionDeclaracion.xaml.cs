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
using Microsoft.Win32;
using System.IO;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.CriticaN;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using c = Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.WPF.Captura.Utils;
using Ruv.WPF.Captura.Controles;
using resxGeneral = Ruv.Infrastructure.Crosscutting.Resources;
namespace Ruv.WPF.Captura.CriticaN
{
    /// <summary>
    /// Interaction logic for GestionDeclaracion.xaml
    /// </summary>
    public partial class GestionDeclaracion : Page
    {
        #region Propiedades

        public int NIdDeclaracion { get; set; }

        public int NIdRadicacion {
            private get
            {
                int nid = gContenedor.DataContext == null ? -1 : ((clsCriticaN)gContenedor.DataContext).NId;
                return nid;
            }
            set
            {
                clsCriticaN critica = new clsCriticaN
                {
                    NId = value
                };

                gContenedor.DataContext = critica;
            }
        }

        public clsDeclaracion Declaracion { get; set; }

        #endregion

        public GestionDeclaracion()
        {
            InitializeComponent();
        }

        #region Metodos Privados

        private bool GuardarPregunta(List<c::General.clsPreguntaCriticaN> iePreguntas, ref string cError)
        {
            clsCriticaN critica = (clsCriticaN)gContenedor.DataContext;
            CriticaNServiceReference.ICriticaNService sCritica = RUV.I.Red.ServicioCriticaN;

            List<clsRespuestaCritica> lstRespuesta = iePreguntas.Select(pgPregunta => new clsRespuestaCritica
            {
                NIdCriticaN = pgPregunta.NId,
                NIdRadicacion = this.NIdRadicacion,
                NIdUsuario = RUV.I.Usuario.Id,
                NRespuesta = critica.LstValidacion.Contains(pgPregunta.NId) ? 1 : 0
            }).ToList();

            return sCritica.InsertaCriticaN(lstRespuesta.ToArray(), ref cError);
        }

        #region Eventos

        private void Button_Causales(object sender, RoutedEventArgs e)
        {
            List<int> lstResuesta = loCausal.ValoresUsuario;
            string cObservacion = ObservacionCriticaN.Text;

            c.Entidades.Devolucion.clsDevolucion DecDevol = new c.Entidades.Devolucion.clsDevolucion
            {
                NIdRadicacion = this.NIdRadicacion,
                NIdDeclaracion = this.NIdDeclaracion,
                NIdUsuario = RUV.I.Usuario.Id,
                LstCausalesDevolucion = lstResuesta,
                CObservaciones = this.ObservacionCriticaN.Text
            };

            DevolucionServiceReference.DevolucionServiceClient dev = RUV.I.Red.ServicioDevolucion;
            string cError = string.Empty;
            dev.SolicitarDevolucion(DecDevol, ref cError);
            MessageBox.Show(resx::Informacion.RadicacionADevolucion, resx::Controles.Informacion);
            RUV.I.UIPrincipal.NavegarAListaDeTareas();
        }

        private void Button_Validacion(object sender, RoutedEventArgs e)
        {
            List<int> lstResuesta = loValidacion.ValoresUsuario;

            string cError = string.Empty;
            if (!(GuardarPregunta(RUV.I.InfoGeneral.PreguntasCriticaN, ref cError) || string.IsNullOrEmpty(cError)))
                MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
            else
            {
                IEnumerable<int> eIdPreguntaNoSeleccionada = loValidacion.ListaTBs.Where(x => x.IsChecked.HasValue ? !x.IsChecked.Value : true)
                    .Select(x => ((clsElementoSeleccionable)x.Tag).Id);


                if (eIdPreguntaNoSeleccionada.Count() > 0)
                {
                    c.Entidades.Devolucion.clsDevolucion DecDevol = new c.Entidades.Devolucion.clsDevolucion
                    {
                        NIdRadicacion = this.NIdRadicacion,
                        NIdDeclaracion = this.NIdDeclaracion,
                        NIdUsuario = RUV.I.Usuario.Id
                    };

                    IEnumerable<int> eCausales = RUV.I.InfoGeneral.PreguntasCriticaN.Where(x => x.NId == eIdPreguntaNoSeleccionada.ToList().Find(y => y == x.NId))
                        .Select(x => RUV.I.InfoGeneral.ListaCausales.Find(y => y.NId == x.NIdCausal).NId);

                    DecDevol.LstCausalesDevolucion = eCausales.ToList();
                    DevolucionServiceReference.DevolucionServiceClient dev = RUV.I.Red.ServicioDevolucion;

                    if (!(dev.SolicitarDevolucion(DecDevol, ref cError) || string.IsNullOrEmpty(cError)))
                        MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                    else
                        MessageBox.Show(resx::Informacion.RadicacionADevolucion, resx::Controles.Informacion);

                }

                else
                {
                    MessageBox.Show(resx::Informacion.RadicacionFinalizadaPendCaptura, resx::Controles.Informacion);
                }
            }

            RUV.I.UIPrincipal.NavegarAListaDeTareas();
        }
        

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            RUV.I.UIPrincipal.NavegarAListaDeTareas();
        }

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            CriticaNServiceReference.ICriticaNService sCritica = RUV.I.Red.ServicioCriticaN;
                SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = resxGeneral::General.FiltrosCargasDescargas;
            saveFile.InitialDirectory = RUV.I.Configuraciones.Ubicaciones.DestinoDescargas;
            saveFile.FileName = System.IO.Path.GetFileName(Declaracion.DocumentoDigitalNombre);
            if (string.IsNullOrEmpty(saveFile.FileName)) { MessageBox.Show(resx::Informacion.NoDocumentoEscaneado, resx::Informacion.Titulo); return; }
                if (saveFile.ShowDialog() == true)
                {
                    try
                    {
                    File.WriteAllBytes(saveFile.FileName, Declaracion.DocumentoDigital);
                    Notificaciones notifica = new Notificaciones(saveFile.FileName, resx::Informacion.DescargadoCorrectamente);
                    RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(resx::Errores.General, ex.Message), resx::Controles.Error);
                    }
                }
            }

        #endregion

        #endregion


    }
}
