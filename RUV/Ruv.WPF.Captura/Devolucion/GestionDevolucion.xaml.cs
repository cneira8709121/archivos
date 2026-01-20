using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Microsoft.Win32;
using System.IO;
using Ruv.WPF.Captura.Controles;
using System.Linq;
using ent = Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura
{
	public partial class GestionDevolucion
	{
		public GestionDevolucion()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
        }

        public GestionDevolucion(int Declaracion)
        {
            this.InitializeComponent(); 
            DevolucionServiceReference.DevolucionServiceClient cliDevolucion = RUV.I.Red.ServicioDevolucion;
            string cError = string.Empty;
            
            //ent::Devolucion.clsDevolucion dev = cliDevolucion.ObtenerDevolucion(Decla.ID.Value, Decla.RadicacionId.Value, ref cError);
            ent::Devolucion.clsDevolucion dev = cliDevolucion.ObtenerDevolucion(Declaracion, ref cError);
            if (dev == null)
                cError = resx::Errores.NoExisteDevolucionParaDeclaracion;

            if (!string.IsNullOrEmpty(cError))
                MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
            else
            {
                StringBuilder strParteEmotiva = new StringBuilder();
                for (int i = 0; i < dev.LstCausalesDevolucion.Count; i++)
                {
                    strParteEmotiva.AppendLine(string.Format("{0}.\t{1}.", i + 1,
                        RUV.I.InfoGeneral.ListaCausales.Where(x => x.NId == dev.LstCausalesDevolucion[i])
                        .Select(x => x.CParteEmotiva)
                        .SingleOrDefault()));
                }
                dev.CParteEmotivaModificada = strParteEmotiva.ToString();
                DataContext = dev;
            }
                                  
        }

        #region Private methods

        #region Events

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            pCausales.IsOpen = false;
        }

        private void BtnEditar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pCausales.IsOpen = true;
        }

        private void BtnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            clsDevolucion dev = (clsDevolucion)DataContext;
            if (string.IsNullOrEmpty(dev.CParteEmotivaModificada) ||
                string.IsNullOrEmpty(dev.CDireccion) ||
                string.IsNullOrEmpty(dev.CFuncionario) ||
                dev.NTelefono < 0)
            {
                MessageBox.Show(resx::Advertencia.CamposRequeridosDevolucion, resx::Controles.Advertencia);
                return;
            }

            dev.NIdUsuario = RUV.I.Usuario.Id;

            string cError = string.Empty;
            DevolucionServiceReference.DevolucionServiceClient devService = RUV.I.Red.ServicioDevolucion;
            if (!devService.ActualizarDevolucion(dev, ref cError) || !string.IsNullOrEmpty(cError))
            {
                MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
            }
            else
            {
                byte[] pdf = devService.GenerarDocumentoDevolucion(dev.NId.Value, ref cError);
                if (cError != string.Empty) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                else
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "PDF files (*.pdf)|*.pdf";
                    if (saveFile.ShowDialog() == true)
                    {
                        using (FileStream fs = File.Create(saveFile.FileName))
                        {
                            foreach (byte bPart in pdf)
                            {
                                fs.WriteByte(bPart);
                            }

                            Notificaciones notifica = new Notificaciones(saveFile.FileName, resx::Informacion.GeneradoCorrectamente);
                            RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                        }
                    }

                    MessageBox.Show(resx::Informacion.CambiosGuardados, resx::Controles.Informacion);
                    RUV.I.UIPrincipal.NavegarAListaDeTareas();
                }
            }
        }

        private void BtnAceptarParteEmotiva(object sender, System.Windows.RoutedEventArgs e)
        {
            pCausales.IsOpen = false;
            BindingExpression be = txbParteEmotiva.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void BtnCancelarParteEmotiva(object sender, System.Windows.RoutedEventArgs e)
        {
            pCausales.IsOpen = false;
        }

        #endregion

        #endregion
    }
}