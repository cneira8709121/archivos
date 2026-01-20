using Ruv.WPF.Captura.Controles;
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
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para H03_DescripcionHechos.xaml
    /// </summary>
    public partial class H03_DescripcionHechos : UserControl, ISeccionRegistro
    {
        public H03_DescripcionHechos()
        {
            InitializeComponent();
        }

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.H03_DescripcionHechos; } }



        public bool RequireScrollBars { get { return true; } }

        public void MostrarEnInterfase()
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                if (!string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.TomaDeclaracion.HechosOtrosCual))
                {
                    // Si existe el adorner, utilizarlo.
                    if (RUV.I.UIPrincipal.AdornoFocoValidacion != null)
                    {
                        RUV.I.UIPrincipal.AdornoFocoValidacion.MostrarFoco(txtNarracion);
                    }
                    else
                    {

                        txtNarracion.Focus();
                        if (txtNarracion is TextBox)
                            (txtNarracion as TextBox).SelectAll();
                    }
                    tpNarracion.IsOpen = true;
                    tpNarracion.Content = string.Format("Registrar las circunstancias de tiempo, modo y lugar, del hecho victimizante {0} marcado en la casilla ¿Otro? de la Hoja 1", RUV.I.DeclaracionActual.TomaDeclaracion.HechosOtrosCual);
                    lblMensajeOtroHecho.Content = string.Format("Registrar las circunstancias de tiempo, modo y lugar, del hecho victimizante {0} marcado en la casilla ¿Otro? de la Hoja 1", RUV.I.DeclaracionActual.TomaDeclaracion.HechosOtrosCual);
                }
                InformacionHechos();
            }
        }

        private void InformacionHechos()
        {
            StringBuilder sbInfo = new StringBuilder();
            var declaracion = RUV.I.DeclaracionActual;
            int contA1 = 1;
            foreach (var item in declaracion.A01)
            {
                sbInfo.AppendLine(string.Format("Acto terrorista / Atentados / Combates / Enfrentamientos / Hostigamientos {0}", contA1));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA1++;
            }
            int contA2 = 1;
            foreach (var item in declaracion.A02)
            {
                sbInfo.AppendLine(string.Format("Amenaza {0}", contA2));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA2++;
            }
            int contA3 = 1;
            foreach (var item in declaracion.A03)
            {
                sbInfo.AppendLine(string.Format("Delitos contra la libertad y la integridad sexual en desarrollo del conflicto armado {0}", contA3));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA3++;
            }

            int contA4 = 1;
            foreach (var item in declaracion.A04)
            {
                sbInfo.AppendLine(string.Format("Desaparicion Forzada {0}", contA4));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA4++;
            }
            int contA5 = 1;
            foreach (var item in declaracion.A05)
            {
                sbInfo.AppendLine(string.Format("Desplazamiento Forzado {0}", contA5));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                if (item.InformacionDeArribo.HechosDepartamento.HasValue && item.InformacionDeArribo.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar de arribo: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.InformacionDeArribo.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.InformacionDeArribo.HechosMunicipio).Nombre));
                if (item.InformacionDeArribo.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha de arribo: {0}", item.InformacionDeArribo.HechosFecha.Value.ToShortDateString()));
                sbInfo.AppendLine("");
                contA5++;
            }
            int contA6 = 1;
            foreach (var item in declaracion.A06)
            {
                sbInfo.AppendLine(string.Format("Homicidio / Masacre {0}", contA6));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA6++;
            }
            int contA7 = 1;
            foreach (var item in declaracion.A07)
            {
                sbInfo.AppendLine(string.Format("Minas Antipersonal, Municion sin explotar, y artefacto explosivo improvisado {0}", contA7));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA7++;
            }

            int contA8 = 1;
            foreach (var item in declaracion.A08)
            {
                sbInfo.AppendLine(string.Format("Secuestro {0}", contA8));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA8++;
            }
            int contA9 = 1;
            foreach (var item in declaracion.A09)
            {
                sbInfo.AppendLine(string.Format("Tortura {0}", contA9));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA9++;
            }
            int contA10 = 1;
            foreach (var item in declaracion.A10)
            {
                sbInfo.AppendLine(string.Format("Vinculacion de Niños, Niñas y Adolescentes a actividades relacionadas con Grupos Armados {0}", contA10));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA10++;
            }
            int contA11 = 1;
            foreach (var item in declaracion.A11)
            {
                sbInfo.AppendLine(string.Format("Abandono o despojo forzado de tierras {0}", contA11));
                if (item.FechaYLugar.HechosFecha.HasValue)
                    sbInfo.AppendLine(string.Format("Fecha del hecho: {0}", item.FechaYLugar.HechosFecha.Value.ToShortDateString()));
                if (item.FechaYLugar.HechosDepartamento.HasValue && item.FechaYLugar.HechosMunicipio.HasValue)
                    sbInfo.AppendLine(string.Format("Lugar del hecho: {0} - {1}", RUV.I.InfoGeneral.ListaDepartamentosTodos.First(x => x.Id == item.FechaYLugar.HechosDepartamento).Nombre, RUV.I.InfoGeneral.ListaMunicipiosTodos.First(x => x.Id == item.FechaYLugar.HechosMunicipio).Nombre));
                sbInfo.AppendLine("");
                contA11++;
            }

            RUV.I.DeclaracionActual.DescripcionHechos.InfoHechos = sbInfo.ToString();

            if (!string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DescripcionHechos.InfoHechos))
                spInfoHechos.Visibility = Visibility.Visible;
        }


        #endregion


        private static T FindVisualChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T && (child as FrameworkElement).Name == name)
                    {
                        return child as T;
                    }
                    else
                    {
                        T childOfChild = FindVisualChild<T>(child, name);
                        if (childOfChild != null)
                        {
                            return childOfChild;
                        }
                    }
                }
            }
            return null;
        }

        private void txtNarracion_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ToolTip toolTip = FindVisualChild<ToolTip>(sender as Button, "myToolTip");
            if (toolTip != null)
            {
                toolTip.IsOpen = true;
                e.Handled = true;
            }
        }
    }
}
