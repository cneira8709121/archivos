using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para SeleccionAnexo.xaml
    /// </summary>
    public partial class SeleccionAnexo : Window
    {
        #region CONSTRUCTOR

        public SeleccionAnexo()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SeleccionAnexo_Loaded);
        }

        void SeleccionAnexo_Loaded(object sender, RoutedEventArgs e)
        {
            AnexoSeleccionado = null;
            LlenarListaAnexos();
            AgregarListaAnexos();

            btnEliminar.IsEnabled = !RUV.I.DeclaracionActual.SoloLectura;
            btnAgregar.IsEnabled = !RUV.I.DeclaracionActual.SoloLectura;
        }

        #endregion

        #region ARMAR LISTA DE ANEXOS

        /// <summary>
        /// Llena la lista de los anexos.
        /// </summary>
        void LlenarListaAnexos()
        {
            var Hechos = RUV.I.DeclaracionActual.TomaDeclaracion.Hechos;
            clsValidadorEntidades Validador = new clsValidadorEntidades();
            List<ElementoAnexo> ListaAnexos = new List<ElementoAnexo>();

            string NombreJefeHogar = null;
            SolidColorBrush EstadoAnexo = null;

            for (int i = 0; i < RUV.I.DeclaracionActual.TodosLosAnexos.Count(); i++)
            {
                int NumeroAnexo = i + 1;
                var Decla = RUV.I.DeclaracionActual;
                var LosAnexos = Decla.TodosLosAnexos;
                IAnexo ElAnexo = null;

                ElAnexo = LosAnexos.ElementAt(i);
                var Persona = Decla.PersonasAfectadas.ListaPersonas
                  .Where(x => x.ID == (LosAnexos.ElementAt(i).JefeGrupoFamiliarId)
                    && x.ID.HasValue).FirstOrDefault();

                List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
                int validacionesSaltadas = 0;
                if (!RUV.I.ValidadorEntidades.EntidadEsValida(ElAnexo as clsEntidadBase, Requeridas, ref validacionesSaltadas))
                {
                    EstadoAnexo = EstadoSinComenzar;
                    if (Persona != null && !string.IsNullOrWhiteSpace(Persona.NombreCompleto))
                        NombreJefeHogar = string.Format("{0} (no completado)", Persona.NombreCompleto);
                    else
                        NombreJefeHogar = "(no completado)";
                }
                else
                {
                    if (Persona != null)
                    {
                        EstadoAnexo = EstadoComenzado;
                        NombreJefeHogar = Persona.NombreCompleto;
                    }
                    else
                    {
                        if (ElAnexo.Numero == 11)
                        {
                            EstadoAnexo = EstadoComenzado;
                            NombreJefeHogar = "(Anexo11)";
                        }
                        else if (ElAnexo.Numero == 13)
                        {
                            clsAnexo13 Anexo13 = ElAnexo as clsAnexo13;
                            Anexo13.Declaracion = Decla;
                            var PersonaA13 = Anexo13.ListaPersonas
                            .Where(x => x.Relacion == (int)eRelacion.Jefe_de_hogar).FirstOrDefault();

                            if (PersonaA13 != null)
                            {
                                EstadoAnexo = EstadoComenzado;
                                NombreJefeHogar = PersonaA13.NombreCompleto;
                            }
                            else
                            {
                                EstadoAnexo = EstadoSinComenzar;
                                NombreJefeHogar = "(no completado)";
                            }
                        }
                        else
                        {
                            EstadoAnexo = EstadoSinComenzar;
                            NombreJefeHogar = "(no completado)";
                        }
                    }
                }

                ListaAnexos.Add(new ElementoAnexo()
                {
                    NombreAnexo = string.Format("Anexo {0:D2}", LosAnexos.ElementAt(i).Numero),
                    JefeDeHogar = NombreJefeHogar,
                    ColorEstado = EstadoAnexo,
                    Anexo = ElAnexo,
                    NumeroAnexo = LosAnexos.ElementAt(i).Numero
                });
            }

            lbxAnexos.ItemsSource = ListaAnexos.OrderBy(x => x.NombreAnexo).ThenBy(x => x.JefeDeHogar);
        }

        #endregion

        #region ACEPTAR Y CANCELAR

        private void SeleccionDefinitiva_DobleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbxAnexos.SelectedItem == null) { return; }
            else
            {
                AnexoSeleccionado = lbxAnexos.SelectedItem as ElementoAnexo;
                this.Close();
            }
        }

        /// <summary>
        /// Aceptar la selección del usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAnexos.SelectedItem == null)
            {
                RUV.I.UIPrincipal.ReportarErrorDeUsuario("Debe seleccionar el anexo antes de continuar.");
                return;
            }

            AnexoSeleccionado = lbxAnexos.SelectedItem as ElementoAnexo;
            this.Close();
        }

        private ElementoAnexo _AnexoSeleccionado;
        /// <summary>
        /// El anexo seleccionado por el usuario.
        /// </summary>
        public ElementoAnexo AnexoSeleccionado
        {
            get { return _AnexoSeleccionado; }
            set { _AnexoSeleccionado = value; }
        }

        /// <summary>
        /// Cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            AnexoSeleccionado = null;
            this.Close();
        }

        #endregion

        #region VARIABLES

        static SolidColorBrush EstadoComenzado = new SolidColorBrush(Colors.Green);
        static SolidColorBrush EstadoSinComenzar = new SolidColorBrush(Colors.Red);

        public class ElementoAnexo
        {
            public string NombreAnexo { get; set; }
            public string JefeDeHogar { get; set; }
            public SolidColorBrush ColorEstado { get; set; }
            public IAnexo Anexo { get; set; }
            public int NumeroAnexo { get; set; }
        }

        #endregion

        #region AGREGAR ANEXO

        private void AgregarAnexo(object sender, RoutedEventArgs e)
        {
            var AnexoSeleccionado = cbxNuevoAnexo.SelectedItem as clsItem;
            var Decla = RUV.I.DeclaracionActual;

            switch (AnexoSeleccionado.Id)
            {
                case 1:
                    var anexoNuevo1 = new clsAnexo01();
                    anexoNuevo1.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo1);
                    Decla.A01.Add(anexoNuevo1);
                    break;
                case 2:
                    var anexoNuevo2 = new clsAnexo02();
                    anexoNuevo2.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo2);
                    Decla.A02.Add(anexoNuevo2);
                    break;
                case 3:
                    var anexoNuevo3 = new clsAnexo03();
                    anexoNuevo3.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo3);
                    Decla.A03.Add(anexoNuevo3);
                    break;
                case 4:
                    var anexoNuevo4 = new clsAnexo04();
                    if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                        MessageBox.Show("Recuerde que previamente debió ingresar en la hoja 2, los datos de la persona desaparecida para de esta manera asociarlo como víctima 1 (directa)");
                    anexoNuevo4.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo4);
                    Decla.A04.Add(anexoNuevo4);
                    break;
                case 5:
                    var anexoNuevo5 = new clsAnexo05();
                    anexoNuevo5.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo5);
                    Decla.A05.Add(anexoNuevo5);
                    break;
                case 6:
                    var anexoNuevo6 = new clsAnexo06();
                    if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                        MessageBox.Show("Recuerde que previamente debió ingresar en la hoja 2, los datos de la persona fallecida para de esta manera asociarlo como víctima 1(directa).");
                    anexoNuevo6.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo6);
                    Decla.A06.Add(anexoNuevo6);
                    break;
                case 7:
                    var anexoNuevo7 = new clsAnexo07();
                    anexoNuevo7.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo7);
                    Decla.A07.Add(anexoNuevo7);
                    break;
                case 8:
                    var anexoNuevo8 = new clsAnexo08();
                    anexoNuevo8.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo8);
                    Decla.A08.Add(anexoNuevo8);
                    break;
                case 9:
                    var anexoNuevo9 = new clsAnexo09();
                    anexoNuevo9.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo9);
                    Decla.A09.Add(anexoNuevo9);
                    break;
                case 10:
                    var anexoNuevo10 = new clsAnexo10();
                    anexoNuevo10.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo10);
                    Decla.A10.Add(anexoNuevo10);
                    break;
                case 11:
                    var anexoNuevo11 = new clsAnexo11();
                    anexoNuevo11.ID_Interno = Guid.NewGuid();
                    RUV.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo11);
                    Decla.A11.Add(anexoNuevo11);
                    break;
                case 13:
                    Decla.A13.Add(new clsAnexo13() { ID = int.MinValue, Declaracion = Decla, ID_Interno = Guid.NewGuid() }); break;
            }

            Decla.ReportarCambioPropiedad("NumeroDeAnexos");

            LlenarListaAnexos();
        }

        //private void CrearAnexo(IAnexo anexoNuevo )
        //{
        //  // var anexoNuevo = new clsAnexo01();
        //  Sipod.I.Util.EntidadEstablecerSiguienteId(Decla.A01, anexoNuevo);      
        //}

        void AgregarListaAnexos()
        {
            List<clsItem> Anexos = new List<clsItem>();
            Anexos.Add(new clsItem { Id = 1, Nombre = "1. Acto Terrorista / Atentados / Combates / Enfrentamientos / Hostigamientos" });
            Anexos.Add(new clsItem { Id = 2, Nombre = "2. Amenaza" });
            Anexos.Add(new clsItem { Id = 3, Nombre = "3. Delitos contra la libertad y la integridad sexual en desarrollo del conflicto armado" });
            Anexos.Add(new clsItem { Id = 4, Nombre = "4. Desaparición Forzada" });
            Anexos.Add(new clsItem { Id = 5, Nombre = "5. Desplazamiento Forzado" });
            Anexos.Add(new clsItem { Id = 6, Nombre = "6. Homicidio - Masacre" });
            Anexos.Add(new clsItem { Id = 7, Nombre = "7. Minas antipersonal, Munición sin explotar y Artefcto Explosivo Improvisado" });
            Anexos.Add(new clsItem { Id = 8, Nombre = "8. Secuestro" });
            Anexos.Add(new clsItem { Id = 9, Nombre = "9. Tortura" });
            Anexos.Add(new clsItem { Id = 10, Nombre = "10. Vinculación de Niños, Niñas y Adolescentes a actividades relacionadas con grupos armados" });
            Anexos.Add(new clsItem { Id = 11, Nombre = "11. Despojo y/o abandono forzado de bienes muebles e inmuebles" });
            Anexos.Add(new clsItem { Id = 13, Nombre = "13. Censo Evento Masivo" });
            cbxNuevoAnexo.ItemsSource = Anexos;
            cbxNuevoAnexo.SelectedIndex = 0;
        }

        #endregion

        #region ELIMINAR ANEXO

        private void EliminarAnexo(object sender, RoutedEventArgs e)
        {
            if (lbxAnexos.SelectedItem == null)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "Primero debe seleccionar el anexo a eliminar");
                return;
            }

            if (!RUV.I.UIPrincipal.UsuarioConfirmar(
              "¿Está seguro de eliminar toda la información\ndel anexo seleccionado?"))
                return;

            var Anexo = (lbxAnexos.SelectedItem as ElementoAnexo).Anexo as IAnexo;
            var Decla = RUV.I.DeclaracionActual;

            if ((Anexo as clsEntidadBase).EstadoRegistro == eEstadoRegistro.Insertar)
            {
                // Si el anexo es nuevo, quitarlo de forma definitiva.
                switch (Anexo.Numero)
                {
                    case 1: Decla.A01.Remove(Anexo as clsAnexo01); break;
                    case 2: Decla.A02.Remove(Anexo as clsAnexo02); break;
                    case 3: Decla.A03.Remove(Anexo as clsAnexo03); break;
                    case 4: Decla.A04.Remove(Anexo as clsAnexo04); break;
                    case 5: Decla.A05.Remove(Anexo as clsAnexo05); break;
                    case 6: Decla.A06.Remove(Anexo as clsAnexo06); break;
                    case 7: Decla.A07.Remove(Anexo as clsAnexo07); break;
                    case 8: Decla.A08.Remove(Anexo as clsAnexo08); break;
                    case 9: Decla.A09.Remove(Anexo as clsAnexo09); break;
                    case 10: Decla.A10.Remove(Anexo as clsAnexo10); break;
                    case 11: Decla.A11.Remove(Anexo as clsAnexo11); break;
                    case 13: Decla.A13.Remove(Anexo as clsAnexo13); break;
                }
            }
            else
            {
                // De lo contrario marcarlo como borrado.
                switch (Anexo.Numero)
                {
                    case 1:
                        var AN1 = Anexo as clsAnexo01;
                        AN1.Victimas.ToList().ForEach(x =>
                        {
                            MarcarComoBorrado<clsAnexo01_Victima_Bien>(x.Bienes);
                            x.EstadoRegistro = eEstadoRegistro.Eliminado;
                        });
                        AN1.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 2:
                        var AN2 = Anexo as clsAnexo02;
                        MarcarComoBorrado<clsAnexo02_Victima>(AN2.Victimas);
                        AN2.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 3:
                        var AN3 = Anexo as clsAnexo03;
                        MarcarComoBorrado<clsAnexo03_Victima>(AN3.Victimas);
                        AN3.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 4:
                        var AN4 = Anexo as clsAnexo04;
                        MarcarComoBorrado<clsAnexo04_Victima>(AN4.Victimas);
                        AN4.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 5:
                        var AN5 = Anexo as clsAnexo05;
                        MarcarComoBorrado<clsAnexo05_Victima>(AN5.Victimas);
                        AN5.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 6:
                        var AN6 = Anexo as clsAnexo06;
                        MarcarComoBorrado<clsAnexo06_Victima>(AN6.Victimas);
                        AN6.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 7:
                        var AN7 = Anexo as clsAnexo07;
                        MarcarComoBorrado<clsAnexo07_Victima>(AN7.Victimas);
                        AN7.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 8:
                        var AN8 = Anexo as clsAnexo08;
                        MarcarComoBorrado<clsAnexo08_Victima>(AN8.Victimas);
                        AN8.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 9:
                        var AN9 = Anexo as clsAnexo09;
                        MarcarComoBorrado<clsAnexo09_Victima>(AN9.Victimas);
                        AN9.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 10:
                        var AN10 = Anexo as clsAnexo10;
                        MarcarComoBorrado<clsAnexo10_Victima>(AN10.Victimas);
                        AN10.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 11:
                        var AN11 = Anexo as clsAnexo11;
                        MarcarComoBorrado<clsAnexo11_BienInmueble>(AN11.BienesInmuebles);
                        MarcarComoBorrado<clsAnexo11_BienMueble>(AN11.BienesMuebles);
                        MarcarComoBorrado<clsAnexo11_CreditoPasivo>(AN11.CreditosPasivos);
                        AN11.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;

                    case 13:
                        var AN13 = Anexo as clsAnexo13;
                        //TODO: Anexo 13  Revisar en ejecución
                        MarcarComoBorrado<clsAnexo13_Victima>(AN13.ListaPersonas);
                        AN13.EstadoRegistro = eEstadoRegistro.Eliminado;
                        break;
                }
            }

            Decla.ReportarCambioPropiedad("NumeroDeAnexos");

            LlenarListaAnexos();
        }

        /// <summary>
        /// Marca todos los elementos de una colección de entidades como borrados.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="coleccion"></param>
        public void MarcarComoBorrado<T1>(ObservableCollection<T1> coleccion) where T1 : clsEntidadBase
        {
            coleccion.ToList().ForEach(
              x => (x as clsEntidadBase).EstadoRegistro = eEstadoRegistro.Eliminado);
        }


        #endregion

        #region CERRAR ESTA VENTANA

        /// <summary>
        /// lanzar algunos procesos al cerrar esta ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CerrarVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Actualizar la lista de las hojas.
            RUV.I.DeclaracionActual.ActualizarConteoHechos();
        }

        #endregion

        #region Censo Masivo
        /*
         * 
        /// <summary>
        /// Agregar un anexo 13 para hacer un censo masivo
        /// Este anexo 13 debe ir asociado a un anexo del 1 al 11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CensoMasivo_Click(object sender, RoutedEventArgs e)
        {
          if (lbxAnexos.SelectedItem == null)
          {
            Sipod.I.UIPrincipal.ReportarErrorDeUsuario("Debe seleccionar el anexo al que se asociará el censo masivo antes de continuar.");
            return;
          }

          AnexoSeleccionado = lbxAnexos.SelectedItem as ElementoAnexo;

          var Decla = Sipod.I.DeclaracionActual;

          //Decla.A13.Add(new clsAnexo13() { ID = int.MinValue, idAnexoRelacionado = ((clsEntidadBase)AnexoSeleccionado.Anexo).ID });

          var anexoNuevo = new clsAnexo13() { ID = int.MinValue, idAnexoRelacionado = ((clsEntidadBase)AnexoSeleccionado.Anexo).ID };
          Sipod.I.Util.EntidadEstablecerSiguienteId_General(Decla.TodosLosAnexos.Cast<clsEntidadBase>(), anexoNuevo);
          Decla.A13.Add(anexoNuevo);

          AnexoSeleccionado.Anexo.idAnexoRelacionado = anexoNuevo.ID;

          Decla.ReportarCambioPropiedad("NumeroDeAnexos");

          LlenarListaAnexos();

        }
         * */
        #endregion
    }
}
