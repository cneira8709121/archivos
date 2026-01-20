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
using Ruv.WPF.Captura.Infrastructure;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para InfoVictima_Imp.xaml
    /// </summary>
    public partial class InfoVictima_Imp : UserControl
    {
        public InfoVictima_Imp()
        {
            InitializeComponent();
            if(RUV.I.DeclaracionActual.VersionFUD == 2){
                Titulos1_6.Height = new GridLength(0);
                SubTitulos1_6.Height = new GridLength(0);
                Valores1_6.Height = new GridLength(0);
            }
            
        }

        /// <summary>
        /// El número de la pregunta.
        /// </summary>
        public int Numero
        {
            set
            {
                seCedula.Numero = (value++).ToString();
                seEstudio.Numero = (value++).ToString();
                seSisben.Numero = (value++).ToString();
                seInscrito.Numero = (value++).ToString();
                seSalud.Numero = (value++).ToString();
                seActividades.Numero = (value++).ToString();
            }
        }

        /// <summary>
        /// La información del jefe de grupo.
        /// </summary>
        public clsAnexo_JefeDeGrupo JefeDeGrupo
        {
            get { return (clsAnexo_JefeDeGrupo)GetValue(JefeDeGrupoProperty); }
            set { SetValue(JefeDeGrupoProperty, value); }
        }

        public static readonly DependencyProperty JefeDeGrupoProperty =
            DependencyProperty.Register("JefeDeGrupo", typeof(clsAnexo_JefeDeGrupo),
            typeof(InfoVictima_Imp), new UIPropertyMetadata(null, JefeDeGrupoChanged));


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T typedChild && typedChild != null)
                    {
                        yield return typedChild;
                    }

                    foreach (var nestedChild in FindVisualChildren<T>(child))
                    {
                        yield return nestedChild;
                    }
                }
            }
        }

        public static void JefeDeGrupoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var IV = d as InfoVictima_Imp;
            var JDG = e.NewValue as clsAnexo_JefeDeGrupo;

            if (JDG == null) return;

            

            IV.EstablecerSiNo(IV.txtCedulaSiNo, JDG.TieneInscritaCedulaParaVotar);
            IV.EstablecerDepartamento(IV.txtCedulaDepartamento, JDG.InscripcionDepartamento);
            IV.EstablecerMunicipio(IV.txtCedulaMunicipio, JDG.InscripcionMunicipio);

            IV.EstablecerDepartamento(IV.txtEstudioDepartamento, JDG.HijosEstudianDepartamento);
            IV.EstablecerMunicipio(IV.txtEstudioMunicipio, JDG.HijosEstudianMunicipio);
            IV.txtEstudioInstitucion.Text = JDG.HijosEstudianInstitucion;

            IV.EstablecerSiNo(IV.txtSisbenSiNo, JDG.EncuestaSisben);
            IV.EstablecerDepartamento(IV.txtSisbenDepartamento, JDG.EncuestaSisbenDepartamento);
            IV.EstablecerMunicipio(IV.txtSisbenMunicipio, JDG.EncuestaSisbenMunicipio);
            if (JDG.EncuestaSisbenNivel.HasValue)
                IV.txtSisbenNivel.Text = JDG.EncuestaSisbenNivel.Value.ToString();

            IV.EstablecerSiNo(IV.txtInscritoSiNo, JDG.InscritoEnPrograma);
            IV.EstablecerDepartamento(IV.txtInscritoDepartamento, JDG.InscritoEnProgramaDepartamento);
            IV.EstablecerMunicipio(IV.txtInscritoMunicipio, JDG.InscritoEnProgramaMunicipio);
            IV.txtInscritoEntidad.Text = JDG.InscritoEnProgramaEntidadDondeLabora;

            IV.EstablecerSiNo(IV.txtSaludSiNo, JDG.VinculadoSistemaSalud);
            IV.EstablecerDepartamento(IV.txtSaludDepartamento, JDG.VinculadoSistemaSaludDepartamento);
            IV.EstablecerMunicipio(IV.txtSaludMunicipio, JDG.VinculadoSistemaSaludMunicipio);
            if (JDG.VinculadoSistemaSaludTipoAfiliacion.HasValue)
            {
                IV.txtSaludAfiliacion.Text = RUV.I.InfoGeneral.ListaTipoAfiliacionSalud.
                  FirstOrDefault(x => x.Id == JDG.VinculadoSistemaSaludTipoAfiliacion.Value).Nombre;
            }

            IV.EstablecerDepartamento(IV.txtActividadesDepartamento, JDG.LugarLaboralDepartamento);
            IV.EstablecerMunicipio(IV.txtActividadesMunicipio, JDG.LugarLaboralMunicipio);
            IV.txtActividadesEmpleador.Text = JDG.LugarLaboralEmpleador;
        }

        void EstablecerDepartamento(TextBlock textblock, Int64? departamentoId)
        {
            if (!departamentoId.HasValue) return;

            textblock.Text = RUV.I.InfoGeneral.ListaDepartamentosTodos
              .FirstOrDefault(x => x.Id == departamentoId).Nombre;
        }

        void EstablecerMunicipio(TextBlock textblock, Int64? municipioId)
        {
            if (!municipioId.HasValue) return;

            textblock.Text = RUV.I.InfoGeneral.ListaMunicipiosTodos
              .FirstOrDefault(x => x.Id == municipioId).Nombre;
        }

        void EstablecerSiNo(TextBlock textblock, int? valor)
        {
            if (!valor.HasValue)
                textblock.Text = "Ns/Nr";
            else if (valor.Value == 0)
                textblock.Text = "No";
            else
                textblock.Text = "Si";
        }

    }
}
