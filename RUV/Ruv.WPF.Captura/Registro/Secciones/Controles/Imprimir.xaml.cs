using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
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
using System.Windows.Shapes;
using static Ruv.WPF.Captura.Registro.Secciones.Controles.SeleccionAnexo;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para .xaml
    /// </summary>
    public partial class Imprimir : Window
    {
        public Imprimir()
        {
            InitializeComponent();
        }


        static SolidColorBrush EstadoComenzado = new SolidColorBrush(Colors.Green);
        static SolidColorBrush EstadoSinComenzar = new SolidColorBrush(Colors.Red);

        public void LlenarListado()
        {
            var Hechos = RUV.I.DeclaracionActual.TomaDeclaracion.Hechos;
            List<ElementoImprimir> listaElementos = new List<ElementoImprimir>();

            

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

                listaElementos.Add(new ElementoImprimir()
                {
                    NombreAnexo = string.Format("Anexo {0:D2}", LosAnexos.ElementAt(i).Numero),
                    JefeDeHogar = NombreJefeHogar,
                    ColorEstado = EstadoAnexo,
                    Anexo = ElAnexo,
                    NumeroAnexo = LosAnexos.ElementAt(i).Numero
                });
            }
            listaElementos.Add(new ElementoImprimir()
            {
                NombreAnexo = string.Format("Hoja 1"),
                ColorEstado = new SolidColorBrush(Colors.Green),
                Hoja1 = RUV.I.DeclaracionActual.TomaDeclaracion
            });
            listaElementos.Add(new ElementoImprimir()
            {
                NombreAnexo = string.Format("Hoja 2"),
                ColorEstado = new SolidColorBrush(Colors.Green),
                Hoja2 = RUV.I.DeclaracionActual.PersonasAfectadas
            });
            listaElementos.Add(new ElementoImprimir()
            {
                NombreAnexo = string.Format("Hoja 3"),
                ColorEstado = new SolidColorBrush(Colors.Green),
                Hoja3 = RUV.I.DeclaracionActual.DescripcionHechos
            });
            listaElementos.Add(new ElementoImprimir()
            {
                NombreAnexo = string.Format("Hoja 4"),
                ColorEstado = new SolidColorBrush(Colors.Green),
                Hoja4 = RUV.I.DeclaracionActual.VerificacionProcedimiento
            });

            lbxAnexos.ItemsSource = listaElementos.OrderBy(x => x.NombreAnexo).ThenBy(x => x.JefeDeHogar);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LlenarListado();
        }

        
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var Elemento = lbxAnexos.SelectedItem as ElementoImprimir;
            var Decla = RUV.I.DeclaracionActual;
            if (Elemento == null)
            {
                MessageBox.Show("por favor seleccione la hoja que desea imprimir");
            }
            else
            {
                if (!chkTodo.IsChecked.Value)
                {
                    RUV.I.Configuraciones.Impresion.ImprimirSeccion(Elemento, Decla);
                }
                else
                {
                    RUV.I.Configuraciones.Impresion.ImprimirDeclaracion(Decla);
                }
            }
        }
    }
}
