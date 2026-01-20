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
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Infrastructure;
using System.Collections.ObjectModel;



namespace Ruv.WPF.Captura.Registro
{
    /// <summary>
    /// Lógica de interacción para manejoGlosas.xaml
    /// </summary>
    public partial class manejoGlosas : Window
    {
        #region ATRIBUTOS GENERALES DEl MANEJO DE GLOSAS

        ObservableCollection<clsGlosa> _ListaGlosas = new ObservableCollection<clsGlosa>();
        public ObservableCollection<clsGlosa> ListaGlosas
        {
            get { return _ListaGlosas; }
            set { _ListaGlosas = value; }
        }

        ObservableCollection<clsGlosaIntencion> _ListaIntecionesGlosas = new ObservableCollection<clsGlosaIntencion>();
        public ObservableCollection<clsGlosaIntencion> ListaIntecionesGlosas
        {
            get { return _ListaIntecionesGlosas; }
            set { _ListaIntecionesGlosas = value; }
        }

        clsDeclaracion _Declaracion;
        public clsDeclaracion Declaracion
        {
            get { return _Declaracion; }
            set { _Declaracion = value; }
        }
        clsGlosa objGlosas;

        Ruv.WPF.Captura.Utils.CapturaGlosa EntradaUsuario;

        #endregion
        public manejoGlosas(clsDeclaracion laDeclaracion)
        {


            InitializeComponent();
            objGlosas = new clsGlosa();
            DataContext = objGlosas;
            _Declaracion = laDeclaracion;

            laDeclaracion.Glosas = laDeclaracion.Glosas ?? new ObservableCollection<clsGlosa>();
            laDeclaracion.IGlosas = laDeclaracion.IGlosas ?? new ObservableCollection<clsGlosaIntencion>();

            this.ListaGlosas = laDeclaracion.Glosas;
            this.ListaIntecionesGlosas = laDeclaracion.IGlosas;
            this.cbTipoGlosa.ItemsSource = RUV.I.InfoGeneral.ListaTipoIntoGlosa;
            this.cbTipoGlosa.DisplayMemberPath = "Nombre";
            this.cbTipoGlosa.SelectedValuePath = "Id";

            GridGlosas.ItemsSource = ListaGlosas;
            GridIntenGlosas.ItemsSource = ListaIntecionesGlosas;

            EntradaUsuario = new Utils.CapturaGlosa();
            DataContext = EntradaUsuario;
        }

        private manejoGlosas()
        {
        }

        private void cbTipoGlosa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((int)cbTipoGlosa.SelectedValue)
            {
                case (1): // Glosa
                    cbCategoria.ItemsSource = RUV.I.InfoGeneral.ListaCategoriasGlosa;
                    break;
                case (2): // Intención de Glosa
                    cbCategoria.ItemsSource = RUV.I.InfoGeneral.ListaCategoriasIntentoGlosa;
                    break;
            }
            cbConcepto.DisplayMemberPath = "Nombre";
            cbConcepto.SelectedValuePath = "Id";
        }

        private void cbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((int)cbTipoGlosa.SelectedValue)
            {
                case (1): // Glosa
                    if (cbCategoria.SelectedItem != null)
                        cbConcepto.ItemsSource = RUV.I.InfoGeneral.ListaParametros.
                            Where(x => (int)x.Tipo == (int)cbCategoria.SelectedValue).ToList();
                    cbConcepto.DisplayMemberPath = "Nombre";
                    cbConcepto.SelectedValuePath = "Id";
                    break;
                case (2): // Intención de Glosa
                    cbConcepto.ItemsSource = null;
                    break;
            }
        }

        private void btnCrearGlosa_Click(object sender, RoutedEventArgs e)
        {
            

            if (this.Declaracion != null)
            {
                switch ((int)cbTipoGlosa.SelectedValue)
                {
                    case (1): // Glosa
                        clsGlosa glosaNueva = new clsGlosa();
                        if (ListaGlosas.Count == 0)
                            glosaNueva.ID = 1;
                        else
                            glosaNueva.ID = (ListaGlosas.Max(r => r.ID))+1;
                        glosaNueva.DESCRIPCIONGLOSA = tbDescripcion.Text;
                        glosaNueva.DEVOLUCION = 0;
                        glosaNueva.FECHAESPERADAATEN = dtpFechaAten.SelectedDate;
                        glosaNueva.FECHAGLOSA = DateTime.Now;
                        glosaNueva.ID_PROCESO = Declaracion.ID;
                        glosaNueva.ID_USUARIO = RUV.I.Usuario.Id;
                        glosaNueva.ID_USUARIOCREA = RUV.I.Usuario.Id;
                        glosaNueva.PARAM_CATEGORIAGLOSA = Convert.ToInt32(cbCategoria.SelectedValue);
                        glosaNueva.PARAM_CONCEPTOGLOSA = Convert.ToInt32(cbConcepto.SelectedValue);
                        glosaNueva.PARAM_PROCESO = Convert.ToInt32(Ruv.Infrastructure.Crosscutting.Common.eTipoProceso.Declaracion);
                        glosaNueva.PARAM_ESTADOGLOSA =Convert.ToInt32( Ruv.Infrastructure.Crosscutting.Common.eEstadosGlosas.CreadaSinAtender);
                        glosaNueva.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;
                        this.ListaGlosas.Add(glosaNueva);
                        break;
                    case (2): // Intención de Glosa
                        clsGlosaIntencion IglosaNueva = new clsGlosaIntencion();
                        if (ListaIntecionesGlosas.Count == 0)
                            IglosaNueva.ID = 1;
                        else
                            IglosaNueva.ID = (ListaIntecionesGlosas.Max(r => r.ID)) + 1;

                        IglosaNueva.DESCRIPCIONINGLOSA = tbDescripcion.Text;
                        IglosaNueva.FECHAESPERADAATEN = dtpFechaAten.SelectedDate;
                        IglosaNueva.FECHAINGLOSA = DateTime.Now;
                        IglosaNueva.ID_PROCESO = Declaracion.ID;
                        IglosaNueva.ID_USUARIO = RUV.I.Usuario.Id;
                        IglosaNueva.ID_USUARIOCREA = RUV.I.Usuario.Id;
                        IglosaNueva.PARAM_CATEGORIAINGLOSA = Convert.ToInt32(cbCategoria.SelectedValue);
                        IglosaNueva.PARAM_PROCESO = (int)Ruv.Infrastructure.Crosscutting.Common.eTipoProceso.Declaracion;
                        IglosaNueva.PARAM_ESTADOGLOSA = (int)Ruv.Infrastructure.Crosscutting.Common.eEstadosGlosas.CreadaSinAtender;
                        IglosaNueva.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;
                        this.ListaIntecionesGlosas.Add(IglosaNueva);
                        break;
                }
            }
            else
            {
                MessageBox.Show("No se Pueden Crear Glosas sino se posee una Declaración Asociada.");
            }
        }

        private void Borrar(object sender, RoutedEventArgs e)
        {
            int ID = (int)((Button)sender).CommandParameter;
            clsGlosa myGlosa= ListaGlosas.FirstOrDefault(x => x.ID == ID);
            myGlosa.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado;
            myGlosa.PARAM_ESTADOGLOSA = (int)Ruv.Infrastructure.Crosscutting.Common.eEstadosGlosas.GlosaEliminadaPorAutor;
        }

        private void BorrarI(object sender, RoutedEventArgs e)
        {
            int ID = (int)((Button)sender).CommandParameter;
            clsGlosaIntencion myIntencionGlosa = ListaIntecionesGlosas.FirstOrDefault(x => x.ID == ID);
            myIntencionGlosa.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado;
            myIntencionGlosa.PARAM_ESTADOGLOSA = (int)Ruv.Infrastructure.Crosscutting.Common.eEstadosGlosas.GlosaEliminadaPorAutor;
        }

    }
}
