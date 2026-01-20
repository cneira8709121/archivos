using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.WPF.Captura.Impresion;
using Ruv.WPF.Captura.Registro.Secciones;

namespace Ruv.WPF.Captura.Infrastructure
{
    /// <summary>
    /// En este archivo está la lógica de impresión global de la declaración.
    /// </summary>
    public partial class clsImpresion
    {
        #region VARIABLES Y OBJETOS

        public bool ErrorImpresion;

        //static Ruv.WPF.Captura.Impresion.Encabezado _Encabezado01Declaracion;
        /// <summary>
        /// Este es el escabezado de todas las páginas.
        /// </summary>
        public Ruv.WPF.Captura.Impresion.Encabezado Encabezado01Declaracion()
        {
            return new Ruv.WPF.Captura.Impresion.Encabezado();
        }

        PrintQueue ColaImpresion;
        PrintTicket TiqueteImpresion;
        FixedDocument fixedDocument;
        //Size TamañoPapelActual;
        /// <summary>
        /// La declaración que se está imprimiendo.
        /// </summary>
        public clsDeclaracion DeclaracionEnImpresion { get; set; }
        /// <summary>
        /// La página en proceso actualmente.
        /// </summary>
        public int PaginaActualGenerando { get { return ConteoPaginas; } }
        UserControl EncabezadoTabla;
        FixedPage fixedPage;
        PageContent pageContent;
        DockPanel ContenedorPagina;
        clsInformacionImpresion ResultadoPaso;
        Border BordePagina;
        ILogicaImpresion LogicaActual;
        Grid GrillaPagina;
        int ConteoPaginas;
        bool IsAnXPSAttached = false;

        /* Por falta de una arquitectura de la clase, se usa la lista de partes de impresión como una variable global */
        Dictionary<string, byte[]> XPSAttachments;

        #endregion

        #region CONSTRUIR EL DOCUMENTO A IMPRIMIR

        /// <summary>
        /// Envía una declaración a impresión.
        /// </summary>
        /// <param name="declaracion"></param>
        public void ImprimirDeclaracion(clsDeclaracion declaracion)
        {
            RUV.I.UIPrincipal.BloquearInterfase = "Imprimiendo";
            DeclaracionEnImpresion = null;
            RUV.I.MultiTarea.PosponerEjecucion(1,
              (() =>
              {
                  ImprimirDeclaracionAsync(declaracion);
                  RUV.I.UIPrincipal.BloquearInterfase = null;
              }));
        }

        public void ImprimirSeccion(ElementoImprimir elementoImprimir, clsDeclaracion declaracion)
        {
            RUV.I.UIPrincipal.BloquearInterfase = "Imprimiendo";
            DeclaracionEnImpresion = null;
            RUV.I.MultiTarea.PosponerEjecucion(1,
              (() =>
              {
                  ImprimirSeccionAsync(elementoImprimir, declaracion);
                  RUV.I.UIPrincipal.BloquearInterfase = null;
              }));
        }

        private void ImprimirSeccionAsync(ElementoImprimir elementoImprimir, clsDeclaracion declaracion)
        {
            ConteoPaginas = 0;

            if (DeclaracionEnImpresion == null)
            {
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
            }

            // Armar la lista de las secciones a imprimir.
            List<clsLogicaYDato> Secciones = new List<clsLogicaYDato>();


            if (elementoImprimir.Hoja1 != null)
            {
                Secciones.Add(new clsLogicaYDato(new H01_TomaDeclaracionImp(), DeclaracionEnImpresion.TomaDeclaracion));
            }
            if (elementoImprimir.Hoja2 != null)
            { Secciones.Add(new clsLogicaYDato(new H02_PersonasAfectadasImp(), DeclaracionEnImpresion.PersonasAfectadas)); }
            if (elementoImprimir.Hoja3 != null)
            {
                Secciones.Add(new clsLogicaYDato(new H03_DescripcionHechosImp(), DeclaracionEnImpresion.DescripcionHechos));
            }

            // Procesar cada Hoja y Anexo.
            //ProcesarImprimirSecciones(Secciones);

            // Continuar con la lista de los anexos.        
            foreach (var UnAnexo in DeclaracionEnImpresion.TodosLosAnexos)
            {
                //Secciones = new List<clsLogicaYDato>();

                //IniciarImpresionDeclaracion();
                //DeclaracionEnImpresion = declaracion;

                ILogicaImpresion LogicaAnexo = null;
                object Datos = null;

                switch (UnAnexo.Numero)
                {
                    case 1: LogicaAnexo = new A01_Imp(); Datos = UnAnexo as clsAnexo01; break;
                    case 2: LogicaAnexo = new A02_Imp(); Datos = UnAnexo as clsAnexo02; break;
                    case 3: LogicaAnexo = new A03_Imp(); Datos = UnAnexo as clsAnexo03; break;
                    case 4: LogicaAnexo = new A04_Imp(); Datos = UnAnexo as clsAnexo04; break;
                    case 5: LogicaAnexo = new A05_Imp(); Datos = UnAnexo as clsAnexo05; break;
                    case 6: LogicaAnexo = new A06_Imp(); Datos = UnAnexo as clsAnexo06; break;
                    case 7: LogicaAnexo = new A07_Imp(); Datos = UnAnexo as clsAnexo07; break;
                    case 8: LogicaAnexo = new A08_Imp(); Datos = UnAnexo as clsAnexo08; break;
                    case 9: LogicaAnexo = new A09_Imp(); Datos = UnAnexo as clsAnexo09; break;
                    case 10: LogicaAnexo = new A10_Imp(); Datos = UnAnexo as clsAnexo10; break;
                    case 11: LogicaAnexo = new A11_Imp(); Datos = UnAnexo as clsAnexo11; break;
                    case 13: LogicaAnexo = new A13_Imp(); Datos = UnAnexo as clsAnexo13; break;
                }
                if (LogicaAnexo != null && Datos != null)
                {
                    if (elementoImprimir.Anexo != null && (Datos as IAnexo).Numero == elementoImprimir.Anexo.Numero)
                        Secciones.Add(new clsLogicaYDato(LogicaAnexo, Datos));
                }
                //ProcesarImprimirSecciones(Secciones);
            }

            // La Hoja 4.
            //IniciarImpresionDeclaracion();
            //DeclaracionEnImpresion = declaracion;
            //Secciones = new List<clsLogicaYDato>();
            if (elementoImprimir.Hoja4 != null)
                Secciones.Add(new clsLogicaYDato(new H04_VerificacionProdedimientoImp(), DeclaracionEnImpresion.VerificacionProcedimiento));
            ProcesarImprimirSecciones(Secciones);

            this.ErrorImpresion = false;
            //DeclaracionEnImpresion = null;
        }

        public void ImprimirColilla(clsDeclaracion declaracion)
        {
            RUV.I.UIPrincipal.BloquearInterfase = "Imprimiendo";
            DeclaracionEnImpresion = null;
            RUV.I.MultiTarea.PosponerEjecucion(1,
              (() =>
              {
                  ImprimirColillaAsync(declaracion);
                  RUV.I.UIPrincipal.BloquearInterfase = null;
              }));
        }

        public byte[] GenerarXPS(clsDeclaracion declaracion)
        {
            DeclaracionEnImpresion = null;
            IsAnXPSAttached = true;
            XPSAttachments = new Dictionary<string, byte[]>();
            ImprimirDeclaracionAsync(declaracion);

            // Diego Alvarez - 05/09/2013
            // Se vuelve a inicializar variable en false para que permita imprimir más adelante
            IsAnXPSAttached = false;
            if (XPSAttachments != null && XPSAttachments.Count > 0)
            {
                var zipfile = FileHelper.CompressFiles(XPSAttachments);
                XPSAttachments = null;
                return zipfile;
            }
            return null;
        }

        /// <summary>
        /// Imprime la declaración proporcionada.
        /// </summary>
        /// <param name="declaracion"></param>
        public void ImprimirDeclaracionAsync_TodoEnBloque(clsDeclaracion declaracion)
        {
            ConteoPaginas = 0;

            if (DeclaracionEnImpresion == null)
            {
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
            }

            bool InicioSesionProcesado = false;

            // Armar la lista de las secciones a imprimir.
            List<clsLogicaYDato> Secciones = new List<clsLogicaYDato>();
            Secciones.Add(new clsLogicaYDato(new H01_TomaDeclaracionImp(), DeclaracionEnImpresion.TomaDeclaracion));
            Secciones.Add(new clsLogicaYDato(new H02_PersonasAfectadasImp(), DeclaracionEnImpresion.PersonasAfectadas));
            Secciones.Add(new clsLogicaYDato(new H03_DescripcionHechosImp(), DeclaracionEnImpresion.DescripcionHechos));

            // Continuar con la lista de los anexos.
            foreach (var UnAnexo in DeclaracionEnImpresion.TodosLosAnexos)
            {
                ILogicaImpresion LogicaAnexo = null;
                object Datos = null;

                switch (UnAnexo.Numero)
                {
                    case 1: LogicaAnexo = new A01_Imp(); Datos = UnAnexo as clsAnexo01; break;
                    case 2: LogicaAnexo = new A02_Imp(); Datos = UnAnexo as clsAnexo02; break;
                    case 3: LogicaAnexo = new A03_Imp(); Datos = UnAnexo as clsAnexo03; break;
                    case 4: LogicaAnexo = new A04_Imp(); Datos = UnAnexo as clsAnexo04; break;
                    case 5: LogicaAnexo = new A05_Imp(); Datos = UnAnexo as clsAnexo05; break;
                    case 6: LogicaAnexo = new A06_Imp(); Datos = UnAnexo as clsAnexo06; break;
                    case 7: LogicaAnexo = new A07_Imp(); Datos = UnAnexo as clsAnexo07; break;
                    case 8: LogicaAnexo = new A08_Imp(); Datos = UnAnexo as clsAnexo08; break;
                    case 9: LogicaAnexo = new A09_Imp(); Datos = UnAnexo as clsAnexo09; break;
                    case 10: LogicaAnexo = new A10_Imp(); Datos = UnAnexo as clsAnexo10; break;
                    case 11: LogicaAnexo = new A11_Imp(); Datos = UnAnexo as clsAnexo11; break;
                    case 13: LogicaAnexo = new A13_Imp(); Datos = UnAnexo as clsAnexo13; break;
                }
                if (LogicaAnexo != null && Datos != null)
                    Secciones.Add(new clsLogicaYDato(LogicaAnexo, Datos));
            }

            // La Hoja 4.
            Secciones.Add(new clsLogicaYDato(new H04_VerificacionProdedimientoImp(), DeclaracionEnImpresion.VerificacionProcedimiento));

            // Y la colilla, si aplica.
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea) && !string.IsNullOrEmpty(DeclaracionEnImpresion.DeclaracionNumero))
            {
                Secciones.Add(
                  new clsLogicaYDato(new ColillaRecibo_Imp(),
                  DeclaracionEnImpresion));
            }


            // Procesar cada Hoja y Anexo.
            foreach (var Elemento in Secciones)
            {
                Elemento.Logica.ImpresionIniciar();
                Elemento.Logica.PasarPagina();
                LogicaActual = Elemento.Logica;
                ResultadoPaso = null;

                InicioSesionProcesado = false;

                do
                {
                    // Obtener los objetos desde la lógica de la sección.
                    ResultadoPaso = Elemento.Logica.ProcesarImpresion(Elemento.FuenteDeDatos);
                    if (!InicioSesionProcesado)
                    {
                        PrepararInicioPagina();
                        InicioSesionProcesado = true;
                    }

                    if (ResultadoPaso != null &&
                      (ResultadoPaso.ObjetoCuerpo != null
                      || ResultadoPaso.TipoContenido == eTipoContenido.FinalSeccion))
                        switch (ResultadoPaso.TipoContenido)
                        {
                            case eTipoContenido.TextoLargo:
                                TextBlock TB = new TextBlock()
                                {
                                    Text = ResultadoPaso.ObjetoCuerpo.ToString(),
                                    TextWrapping = TextWrapping.Wrap,
                                    TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                                    FontWeight = FontWeights.Bold,
                                    Margin = new Thickness(10d, 0d, 10d, 10d),
                                };
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(TB);
                                break;

                            case eTipoContenido.BloqueIndependiente:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as FrameworkElement);
                                break;

                            case eTipoContenido.EncabezadoLista:
                                EncabezadoTabla = ResultadoPaso.ObjetoCuerpo as UserControl;
                                AgregarContenidoAPagina(EncabezadoTabla);
                                break;

                            case eTipoContenido.EncajarEnPagina:
                                var VB = new Viewbox()
                                {
                                    StretchDirection = StretchDirection.Both,
                                    Stretch = System.Windows.Media.Stretch.Fill,
                                    Child = ResultadoPaso.ObjetoCuerpo as FrameworkElement,
                                    Tag = "Encajar"
                                };
                                AgregarContenidoAPagina(VB);
                                break;

                            case eTipoContenido.DetalleLista:
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as UserControl);
                                break;

                            case eTipoContenido.FinalSeccion:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(
                                  new Ruv.WPF.Captura.Impresion.General.MarcaFinalSeccion() { NombreSeccion = Elemento.Logica.NombreEntidad });
                                break;

                            case eTipoContenido.FinalSeccionSinMarca:
                                EncabezadoTabla = null;
                                break;
                        }
                } while (ResultadoPaso != null
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccion
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccionSinMarca);
            }


            // Ya se obtuvo todo el documento, grabarlo localmente.
            //GrabarXPS(
            //  System.IO.Path.Combine(
            //    Sipod.I.Util.RutaArchivosLocales,
            //    "Impresion.xps"), fixedDocument);

            EnviarDocumentoAImpresora(fixedDocument);

            //DeclaracionEnImpresion = null;
        }

        public void ImprimirDeclaracionAsync_OK(clsDeclaracion declaracion)
        {
            ConteoPaginas = 0;

            if (DeclaracionEnImpresion == null)
            {
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
            }

            bool InicioSesionProcesado = false;

            // Armar la lista de las secciones a imprimir.
            List<clsLogicaYDato> Secciones = new List<clsLogicaYDato>();
            Secciones.Add(new clsLogicaYDato(new H01_TomaDeclaracionImp(), DeclaracionEnImpresion.TomaDeclaracion));
            Secciones.Add(new clsLogicaYDato(new H02_PersonasAfectadasImp(), DeclaracionEnImpresion.PersonasAfectadas));
            Secciones.Add(new clsLogicaYDato(new H03_DescripcionHechosImp(), DeclaracionEnImpresion.DescripcionHechos));


            // Y la colilla, si aplica.
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea) && !string.IsNullOrEmpty(DeclaracionEnImpresion.DeclaracionNumero))
            {
                Secciones.Add(
                  new clsLogicaYDato(new ColillaRecibo_Imp(),
                  DeclaracionEnImpresion));
            }


            // Procesar cada Hoja y Anexo.
            foreach (var Elemento in Secciones)
            {
                Elemento.Logica.ImpresionIniciar();
                Elemento.Logica.PasarPagina();
                LogicaActual = Elemento.Logica;
                ResultadoPaso = null;

                InicioSesionProcesado = false;

                do
                {
                    // Obtener los objetos desde la lógica de la sección.
                    ResultadoPaso = Elemento.Logica.ProcesarImpresion(Elemento.FuenteDeDatos);
                    if (!InicioSesionProcesado)
                    {
                        PrepararInicioPagina();
                        InicioSesionProcesado = true;
                    }

                    if (ResultadoPaso != null &&
                      (ResultadoPaso.ObjetoCuerpo != null
                      || ResultadoPaso.TipoContenido == eTipoContenido.FinalSeccion))
                        switch (ResultadoPaso.TipoContenido)
                        {
                            case eTipoContenido.TextoLargo:
                                TextBlock TB = new TextBlock()
                                {
                                    Text = ResultadoPaso.ObjetoCuerpo.ToString(),
                                    TextWrapping = TextWrapping.Wrap,
                                    TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                                    FontWeight = FontWeights.Bold,
                                    Margin = new Thickness(10d, 0d, 10d, 10d),
                                };
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(TB);
                                break;

                            case eTipoContenido.BloqueIndependiente:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as FrameworkElement);
                                break;

                            case eTipoContenido.EncabezadoLista:
                                EncabezadoTabla = ResultadoPaso.ObjetoCuerpo as UserControl;
                                AgregarContenidoAPagina(EncabezadoTabla);
                                break;

                            case eTipoContenido.EncajarEnPagina:
                                var VB = new Viewbox()
                                {
                                    StretchDirection = StretchDirection.Both,
                                    Stretch = System.Windows.Media.Stretch.Fill,
                                    Child = ResultadoPaso.ObjetoCuerpo as FrameworkElement,
                                    Tag = "Encajar"
                                };
                                AgregarContenidoAPagina(VB);
                                break;

                            case eTipoContenido.DetalleLista:
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as UserControl);
                                break;

                            case eTipoContenido.FinalSeccion:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(
                                  new Ruv.WPF.Captura.Impresion.General.MarcaFinalSeccion() { NombreSeccion = Elemento.Logica.NombreEntidad });
                                break;

                            case eTipoContenido.FinalSeccionSinMarca:
                                EncabezadoTabla = null;
                                break;
                        }
                } while (ResultadoPaso != null
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccion
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccionSinMarca);
            }

            // Ya se obtuvo todo el documento, grabarlo localmente.
            //GrabarXPS(
            //  System.IO.Path.Combine(
            //    Sipod.I.Util.RutaArchivosLocales,
            //    "Impresion.xps"), fixedDocument);

            EnviarDocumentoAImpresora(fixedDocument);


            // Continuar con la lista de los anexos.

            foreach (var UnAnexo in DeclaracionEnImpresion.TodosLosAnexos)
            {
                Secciones = new List<clsLogicaYDato>();

                //if (DeclaracionEnImpresion == null)
                //{
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
                //}

                ILogicaImpresion LogicaAnexo = null;
                object Datos = null;

                switch (UnAnexo.Numero)
                {
                    case 1: LogicaAnexo = new A01_Imp(); Datos = UnAnexo as clsAnexo01; break;
                    case 2: LogicaAnexo = new A02_Imp(); Datos = UnAnexo as clsAnexo02; break;
                    case 3: LogicaAnexo = new A03_Imp(); Datos = UnAnexo as clsAnexo03; break;
                    case 4: LogicaAnexo = new A04_Imp(); Datos = UnAnexo as clsAnexo04; break;
                    case 5: LogicaAnexo = new A05_Imp(); Datos = UnAnexo as clsAnexo05; break;
                    case 6: LogicaAnexo = new A06_Imp(); Datos = UnAnexo as clsAnexo06; break;
                    case 7: LogicaAnexo = new A07_Imp(); Datos = UnAnexo as clsAnexo07; break;
                    case 8: LogicaAnexo = new A08_Imp(); Datos = UnAnexo as clsAnexo08; break;
                    case 9: LogicaAnexo = new A09_Imp(); Datos = UnAnexo as clsAnexo09; break;
                    case 10: LogicaAnexo = new A10_Imp(); Datos = UnAnexo as clsAnexo10; break;
                    case 11: LogicaAnexo = new A11_Imp(); Datos = UnAnexo as clsAnexo11; break;
                    case 13: LogicaAnexo = new A13_Imp(); Datos = UnAnexo as clsAnexo13; break;
                }
                if (LogicaAnexo != null && Datos != null)
                    Secciones.Add(new clsLogicaYDato(LogicaAnexo, Datos));

                foreach (var Elemento in Secciones)
                {
                    Elemento.Logica.ImpresionIniciar();
                    Elemento.Logica.PasarPagina();
                    LogicaActual = Elemento.Logica;
                    ResultadoPaso = null;

                    InicioSesionProcesado = false;

                    do
                    {
                        // Obtener los objetos desde la lógica de la sección.
                        ResultadoPaso = Elemento.Logica.ProcesarImpresion(Elemento.FuenteDeDatos);
                        if (!InicioSesionProcesado)
                        {
                            PrepararInicioPagina();
                            InicioSesionProcesado = true;
                        }

                        if (ResultadoPaso != null &&
                          (ResultadoPaso.ObjetoCuerpo != null
                          || ResultadoPaso.TipoContenido == eTipoContenido.FinalSeccion))
                            switch (ResultadoPaso.TipoContenido)
                            {
                                case eTipoContenido.TextoLargo:
                                    TextBlock TB = new TextBlock()
                                    {
                                        Text = ResultadoPaso.ObjetoCuerpo.ToString(),
                                        TextWrapping = TextWrapping.Wrap,
                                        TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                                        FontWeight = FontWeights.Bold,
                                        Margin = new Thickness(10d, 0d, 10d, 10d),
                                    };
                                    EncabezadoTabla = null;
                                    AgregarContenidoAPagina(TB);
                                    break;

                                case eTipoContenido.BloqueIndependiente:
                                    EncabezadoTabla = null;
                                    AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as FrameworkElement);
                                    break;

                                case eTipoContenido.EncabezadoLista:
                                    EncabezadoTabla = ResultadoPaso.ObjetoCuerpo as UserControl;
                                    AgregarContenidoAPagina(EncabezadoTabla);
                                    break;

                                case eTipoContenido.EncajarEnPagina:
                                    var VB = new Viewbox()
                                    {
                                        StretchDirection = StretchDirection.Both,
                                        Stretch = System.Windows.Media.Stretch.Fill,
                                        Child = ResultadoPaso.ObjetoCuerpo as FrameworkElement,
                                        Tag = "Encajar"
                                    };
                                    AgregarContenidoAPagina(VB);
                                    break;

                                case eTipoContenido.DetalleLista:
                                    AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as UserControl);
                                    break;

                                case eTipoContenido.FinalSeccion:
                                    EncabezadoTabla = null;
                                    AgregarContenidoAPagina(
                                      new Ruv.WPF.Captura.Impresion.General.MarcaFinalSeccion() { NombreSeccion = Elemento.Logica.NombreEntidad });
                                    break;

                                case eTipoContenido.FinalSeccionSinMarca:
                                    EncabezadoTabla = null;
                                    break;
                            }



                    } while (ResultadoPaso != null
                      && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccion
                      && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccionSinMarca);
                }
                EnviarDocumentoAImpresora(fixedDocument);
            }


            // La Hoja 4.
            IniciarImpresionDeclaracion();
            DeclaracionEnImpresion = declaracion;
            Secciones = new List<clsLogicaYDato>();
            Secciones.Add(new clsLogicaYDato(new H04_VerificacionProdedimientoImp(), DeclaracionEnImpresion.VerificacionProcedimiento));

            foreach (var Elemento in Secciones)
            {
                Elemento.Logica.ImpresionIniciar();
                Elemento.Logica.PasarPagina();
                LogicaActual = Elemento.Logica;
                ResultadoPaso = null;

                InicioSesionProcesado = false;

                do
                {
                    // Obtener los objetos desde la lógica de la sección.
                    ResultadoPaso = Elemento.Logica.ProcesarImpresion(Elemento.FuenteDeDatos);
                    if (!InicioSesionProcesado)
                    {
                        PrepararInicioPagina();
                        InicioSesionProcesado = true;
                    }

                    if (ResultadoPaso != null &&
                      (ResultadoPaso.ObjetoCuerpo != null
                      || ResultadoPaso.TipoContenido == eTipoContenido.FinalSeccion))
                        switch (ResultadoPaso.TipoContenido)
                        {
                            case eTipoContenido.TextoLargo:
                                TextBlock TB = new TextBlock()
                                {
                                    Text = ResultadoPaso.ObjetoCuerpo.ToString(),
                                    TextWrapping = TextWrapping.Wrap,
                                    TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                                    FontWeight = FontWeights.Bold,
                                    Margin = new Thickness(10d, 0d, 10d, 10d),
                                };
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(TB);
                                break;

                            case eTipoContenido.BloqueIndependiente:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as FrameworkElement);
                                break;

                            case eTipoContenido.EncabezadoLista:
                                EncabezadoTabla = ResultadoPaso.ObjetoCuerpo as UserControl;
                                AgregarContenidoAPagina(EncabezadoTabla);
                                break;

                            case eTipoContenido.EncajarEnPagina:
                                var VB = new Viewbox()
                                {
                                    StretchDirection = StretchDirection.Both,
                                    Stretch = System.Windows.Media.Stretch.Fill,
                                    Child = ResultadoPaso.ObjetoCuerpo as FrameworkElement,
                                    Tag = "Encajar"
                                };
                                AgregarContenidoAPagina(VB);
                                break;

                            case eTipoContenido.DetalleLista:
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as UserControl);
                                break;

                            case eTipoContenido.FinalSeccion:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(
                                  new Ruv.WPF.Captura.Impresion.General.MarcaFinalSeccion() { NombreSeccion = Elemento.Logica.NombreEntidad });
                                break;

                            case eTipoContenido.FinalSeccionSinMarca:
                                EncabezadoTabla = null;
                                break;
                        }



                } while (ResultadoPaso != null
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccion
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccionSinMarca);
            }

            EnviarDocumentoAImpresora(fixedDocument);

            //DeclaracionEnImpresion = null;
        }

        public void ImprimirDeclaracionAsync(clsDeclaracion declaracion)
        {
            ConteoPaginas = 0;

            if (DeclaracionEnImpresion == null)
            {
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
            }

            // Armar la lista de las secciones a imprimir.
            List<clsLogicaYDato> Secciones = new List<clsLogicaYDato>();
            Secciones.Add(new clsLogicaYDato(new H01_TomaDeclaracionImp(), DeclaracionEnImpresion.TomaDeclaracion));
            Secciones.Add(new clsLogicaYDato(new H02_PersonasAfectadasImp(), DeclaracionEnImpresion.PersonasAfectadas));
            Secciones.Add(new clsLogicaYDato(new H03_DescripcionHechosImp(), DeclaracionEnImpresion.DescripcionHechos));


            // Y la colilla, si aplica.
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea) && !string.IsNullOrEmpty(DeclaracionEnImpresion.DeclaracionNumero))
            {
                Secciones.Add(
                  new clsLogicaYDato(new ColillaRecibo_Imp(),
                  DeclaracionEnImpresion));
            }


            // Procesar cada Hoja y Anexo.
            ProcesarImprimirSecciones(Secciones);

            // Continuar con la lista de los anexos.        
            foreach (var UnAnexo in DeclaracionEnImpresion.TodosLosAnexos)
            {
                Secciones = new List<clsLogicaYDato>();

                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;

                ILogicaImpresion LogicaAnexo = null;
                object Datos = null;

                switch (UnAnexo.Numero)
                {
                    case 1: LogicaAnexo = new A01_Imp(); Datos = UnAnexo as clsAnexo01; break;
                    case 2: LogicaAnexo = new A02_Imp(); Datos = UnAnexo as clsAnexo02; break;
                    case 3: LogicaAnexo = new A03_Imp(); Datos = UnAnexo as clsAnexo03; break;
                    case 4: LogicaAnexo = new A04_Imp(); Datos = UnAnexo as clsAnexo04; break;
                    case 5: LogicaAnexo = new A05_Imp(); Datos = UnAnexo as clsAnexo05; break;
                    case 6: LogicaAnexo = new A06_Imp(); Datos = UnAnexo as clsAnexo06; break;
                    case 7: LogicaAnexo = new A07_Imp(); Datos = UnAnexo as clsAnexo07; break;
                    case 8: LogicaAnexo = new A08_Imp(); Datos = UnAnexo as clsAnexo08; break;
                    case 9: LogicaAnexo = new A09_Imp(); Datos = UnAnexo as clsAnexo09; break;
                    case 10: LogicaAnexo = new A10_Imp(); Datos = UnAnexo as clsAnexo10; break;
                    case 11: LogicaAnexo = new A11_Imp(); Datos = UnAnexo as clsAnexo11; break;
                    case 13: LogicaAnexo = new A13_Imp(); Datos = UnAnexo as clsAnexo13; break;
                }
                if (LogicaAnexo != null && Datos != null)
                    Secciones.Add(new clsLogicaYDato(LogicaAnexo, Datos));

                ProcesarImprimirSecciones(Secciones);
            }

            // La Hoja 4.
            IniciarImpresionDeclaracion();
            DeclaracionEnImpresion = declaracion;
            Secciones = new List<clsLogicaYDato>();
            Secciones.Add(new clsLogicaYDato(new H04_VerificacionProdedimientoImp(), DeclaracionEnImpresion.VerificacionProcedimiento));
            ProcesarImprimirSecciones(Secciones);

            this.ErrorImpresion = false;
            DeclaracionEnImpresion = null;
        }

        private void ImprimirColillaAsync(clsDeclaracion declaracion)
        {
            if (DeclaracionEnImpresion == null)
            {
                IniciarImpresionDeclaracion();
                DeclaracionEnImpresion = declaracion;
            }

            // Armar la lista de las secciones a imprimir.
            List<clsLogicaYDato> Secciones = new List<clsLogicaYDato>();

            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea) && !string.IsNullOrEmpty(DeclaracionEnImpresion.DeclaracionNumero))
            {
                Secciones.Add(
                  new clsLogicaYDato(new ColillaRecibo_Imp(),
                  DeclaracionEnImpresion));
            }

            // Procesar la colilla.
            ProcesarImprimirSecciones(Secciones);

            this.ErrorImpresion = false;
        }

        void ProcesarImprimirSecciones(List<clsLogicaYDato> Secciones)
        {
            bool InicioSesionProcesado = false;

            // Procesar cada Hoja y Anexo.
            foreach (var Elemento in Secciones)
            {
                Elemento.Logica.ImpresionIniciar();
                Elemento.Logica.PasarPagina();
                LogicaActual = Elemento.Logica;
                ResultadoPaso = null;

                InicioSesionProcesado = false;

                do
                {
                    // Obtener los objetos desde la lógica de la sección.
                    ResultadoPaso = Elemento.Logica.ProcesarImpresion(Elemento.FuenteDeDatos);
                    if (!InicioSesionProcesado)
                    {
                        PrepararInicioPagina();
                        InicioSesionProcesado = true;
                    }

                    if (ResultadoPaso != null &&
                      (ResultadoPaso.ObjetoCuerpo != null
                      || ResultadoPaso.TipoContenido == eTipoContenido.FinalSeccion))
                        switch (ResultadoPaso.TipoContenido)
                        {
                            case eTipoContenido.TextoLargo:
                                TextBlock TB = new TextBlock()
                                {
                                    Text = ResultadoPaso.ObjetoCuerpo.ToString(),
                                    TextWrapping = TextWrapping.Wrap,
                                    TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                                    FontWeight = FontWeights.Bold,
                                    Margin = new Thickness(10d, 0d, 10d, 10d),
                                };
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(TB);
                                break;

                            case eTipoContenido.BloqueIndependiente:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as FrameworkElement);
                                break;

                            case eTipoContenido.EncabezadoLista:
                                EncabezadoTabla = ResultadoPaso.ObjetoCuerpo as UserControl;
                                AgregarContenidoAPagina(EncabezadoTabla);
                                break;

                            case eTipoContenido.EncajarEnPagina:
                                var VB = new Viewbox()
                                {
                                    StretchDirection = StretchDirection.Both,
                                    Stretch = System.Windows.Media.Stretch.Fill,
                                    Child = ResultadoPaso.ObjetoCuerpo as FrameworkElement,
                                    Tag = "Encajar"
                                };
                                AgregarContenidoAPagina(VB);
                                break;

                            case eTipoContenido.DetalleLista:
                                AgregarContenidoAPagina(ResultadoPaso.ObjetoCuerpo as UserControl);
                                break;

                            case eTipoContenido.FinalSeccion:
                                EncabezadoTabla = null;
                                AgregarContenidoAPagina(
                                  new Ruv.WPF.Captura.Impresion.General.MarcaFinalSeccion() { NombreSeccion = Elemento.Logica.NombreEntidad });
                                break;

                            case eTipoContenido.FinalSeccionSinMarca:
                                EncabezadoTabla = null;
                                break;
                        }
                } while (ResultadoPaso != null
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccion
                  && ResultadoPaso.TipoContenido != eTipoContenido.FinalSeccionSinMarca);
            }

            if (IsAnXPSAttached)
            {
                if (XPSAttachments == null) XPSAttachments = new Dictionary<string, byte[]>();
                XPSAttachments.Add(string.Format("Adjunto-{0}.xps", XPSAttachments.Count + 1), GenerateMemoryXPS(fixedDocument));
            }
            else
            {
                EnviarDocumentoAImpresora(fixedDocument);
            }

        }

        /// <summary>
        /// Agrega contenido a la página que se está procesando.
        /// </summary>
        /// <param name="contenido"></param>
        void AgregarContenidoAPagina(FrameworkElement contenido)
        {
            if (fixedPage == null)
                PrepararInicioPagina();

            // Agregar el contenido en cuestión.
            if (!AgregarObjetoAContenedorPagina(contenido))
            {
                // Si el objeto es un texblock que puede extenderse por varias páginas, procesarlo por aparte.
                if (contenido is TextBlock)
                    ProcesarTextoLargo(contenido as TextBlock);
                else
                {
                    //Luis.Esteban 13Jun12 Si se genera una nueva pagina se imprime y se genera un nuevo documento, ya que en algunos casos imprime la siguiente hoja en blanco 
                    if (!IsAnXPSAttached)
                    {
                        EnviarDocumentoAImpresora(fixedDocument);
                    }
                    else
                    {
                        XPSAttachments.Add(string.Format("Adjunto-{0}.xps", XPSAttachments.Count + 1), GenerateMemoryXPS(fixedDocument));
                    }
                    IniciarImpresionDeclaracion();

                    // Iniciar una nueva página.
                    PrepararInicioPagina();

                    // Evitar que el encabezado de una tabla se repita el principio de la página.
                    if (contenido is IEncabezadoImpresion)
                    {
                        var Encabe = contenido as IEncabezadoImpresion;
                        var YaAgregado = ContenedorPagina.Children.OfType<IEncabezadoImpresion>()
                          .Any(x => x.Orden == Encabe.Orden);
                        if (!YaAgregado)
                            AgregarObjetoAContenedorPagina(contenido);
                    }
                    else
                        AgregarObjetoAContenedorPagina(contenido);
                }
            }
        }

        /// <summary>
        /// FontSize para el texto largo.
        /// </summary>
        double TamañoFuenteTextoLargo
        {
            get
            {
                return (double)App.Current.Resources["TamañoTextoPequeño"];
            }
        }

        /// <summary>
        /// Procesar un textblock que puede extenderse por varias páginas.
        /// </summary>
        /// <param name="contenido"></param>
        void ProcesarTextoLargo(TextBlock contenido)
        {
            StringBuilder SB = new StringBuilder(contenido.Text);

            do
            {
                TextBlock TB = new TextBlock()
                {
                    Text = SB.ToString(),
                    TextWrapping = TextWrapping.Wrap,
                    TextTrimming = System.Windows.TextTrimming.WordEllipsis,
                    FontStyle = FontStyles.Italic,
                    Margin = new Thickness(10d, 0d, 10d, 10d),
                    FontSize = TamañoFuenteTextoLargo
                };

                ContenedorPagina.Children.Add(TB);
                DockPanel.SetDock(TB, Dock.Top);

                // Determinar si el objeto CUPO en la página.
                CalcularDimensionesControl(TB, ContenedorPagina, GrillaPagina, BordePagina);
                bool Cupo = TB.DesiredSize.Height < TB.ActualHeight;

                //if (!Cupo)
                //{
                // Obtener el texto que SI cabe.
                var Texto = RUV.I.Util.ObtenerTextoVisible(TB);
                TB.Text = Texto.Trim();

                // Quitar el texto desplegado.
                SB.Remove(0, Texto.Length);

                // Agregar una nueva página?
                if (SB.Length > 0)
                    PrepararInicioPagina();
                //}
                //else
                //  SB.Clear();
            } while (SB.Length > 0);

        }

        /// <summary>
        /// Inicia una nueva página con sus encabezados.
        /// </summary>
        private void PrepararInicioPagina()
        {
            // Se está procesando el inicio de una página.
            ConteoPaginas++;
            ContenedorPagina = new DockPanel();
            double MargenBorde = RUV.I.Configuraciones.Impresion.Configuracion.MargenPapel.Bottom / 2d;
            GrillaPagina = new Grid();
            BordePagina = new Border()
            {
                Child = GrillaPagina,
                Padding = RUV.I.Configuraciones.Impresion.Configuracion.MargenPapel,
                Width = (ResultadoPaso.OrientacionPapel == eOrientacionPapel.Portrait ?
                  RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoPequeño : RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo) - MargenBorde,
                Height = (ResultadoPaso.OrientacionPapel == eOrientacionPapel.Portrait ?
                  RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo : RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoPequeño) - MargenBorde
            };

            // Si la orientación es Landscape, girar el contenido.
            if (ResultadoPaso.OrientacionPapel == eOrientacionPapel.Landscape)
            {
                System.Windows.Media.RotateTransform RT = new System.Windows.Media.RotateTransform()
                {
                    Angle = 270d
                };
                BordePagina.LayoutTransform = RT;
            }

            GrillaPagina.Children.Add(ContenedorPagina);
            Border Reborde = new Border()
            {
                BorderBrush = new System.Windows.Media.SolidColorBrush(
                  System.Windows.Media.Colors.Black),
                BorderThickness = new Thickness(1d)
            };
            GrillaPagina.Children.Add(Reborde);

            // Agregar el encabezado del reporte.
            AgregarObjetoAContenedorPagina(new Ruv.WPF.Captura.Impresion.Encabezado());

            LogicaActual.PasarPagina();

            // Agregar los encabezados requeridos de la sección.
            if (ResultadoPaso.Encabezados != null)
                foreach (UserControl UnEncabezado in ResultadoPaso.Encabezados)
                {
                    IEncabezadoImpresion EI = UnEncabezado as IEncabezadoImpresion;
                    AgregarObjetoAContenedorPagina(LogicaActual.ObtenerBloque(EI.Orden) as UserControl);
                }

            // Agregar un posible encabezado de tabla.
            if (EncabezadoTabla != null)
            {
                IEncabezadoImpresion EI = EncabezadoTabla as IEncabezadoImpresion;
                AgregarObjetoAContenedorPagina(LogicaActual.ObtenerBloque(EI.Orden) as UserControl);
            }

            // Demás plomería del documento XPS.
            pageContent = new PageContent();
            //fixedPage = new FixedPage()
            //{
            //  Width = (ResultadoPaso.OrientacionPapel == eOrientacionPapel.Portrait ?
            //    RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoPequeño : RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo),
            //  Height = (ResultadoPaso.OrientacionPapel == eOrientacionPapel.Portrait ?
            //    RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo : RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoPequeño)
            //};

            // La orientación de la página siempre es la misma: Portrait.
            // Al momento de imprimir, el contenido landscape es rotado.
            fixedPage = new FixedPage()
            {
                Width = RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoPequeño,
                Height = RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo
            };

            fixedDocument.Pages.Add(pageContent);
            fixedPage.Children.Add(BordePagina);
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
        }

        /// <summary>
        /// Agrega un objeto al contenedor DockPanel de la página actual,
        /// en la parte superior.
        /// Verdadero: El objeto cupo en la página. Si el objeto no cabe, entonces no se ingresa.
        /// </summary>
        /// <param name="controlContenido"></param>
        bool AgregarObjetoAContenedorPagina(FrameworkElement controlContenido)
        {
            clsPersonaAfectada PA = controlContenido.DataContext as clsPersonaAfectada;
            ContenedorPagina.Children.Add(controlContenido);
            DockPanel.SetDock(controlContenido, Dock.Top);

            if (controlContenido.DataContext is clsAnexo13_Victima &&
                (controlContenido.DataContext as clsAnexo13_Victima).NumeroConsecutivo == 11)
                System.Diagnostics.Debugger.Break();

            // Determinar si el objeto CUPO en la página.
            bool Cupo = true;
            if (controlContenido.Tag == null || controlContenido.Tag.ToString() != "Encajar")
            {
                CalcularDimensionesControl(controlContenido, ContenedorPagina, GrillaPagina, BordePagina);
                //Luis.Esteban 8 Jun 2012 Se agrega la condicion que el tamaño deseado sea diferente de CERO
                Cupo = controlContenido.DesiredSize.Height < controlContenido.ActualHeight && controlContenido.DesiredSize.Height != 0;
                if (!Cupo)
                    ContenedorPagina.Children.Remove(controlContenido);
            }
            else
            {
                ContenedorPagina.LastChildFill = true;
            }
            return Cupo;
        }

        /// <summary>
        /// Inicializar la impresión de la declaración.
        /// </summary>
        /// <param name="declaracion"></param>
        void IniciarImpresionDeclaracion()
        {
            if (!IsAnXPSAttached)
            {
                // Obtener la cola y el tiquete de la impresiona.
                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueueCollection printQueuesOnLocalServer =
                  printServer.GetPrintQueues(new[] {
          EnumeratedPrintQueueTypes.Local,
          EnumeratedPrintQueueTypes.Connections });

                ColaImpresion = printQueuesOnLocalServer
                  .Where(x => x.Name == RUV.I.Configuraciones.Impresion.Configuracion.ImpresoraPreferida).FirstOrDefault();
                TiqueteImpresion = ColaImpresion.UserPrintTicket;

                // Crear el escritor XPS y el Documento en blanco.
                XpsDocumentWriter documentWriter =
                  PrintQueue.CreateXpsDocumentWriter(ColaImpresion);
            }
            fixedDocument = new FixedDocument();
            fixedDocument.DocumentPaginator.PageSize =
              new Size(Configuracion.PapelLadoPequeño, RUV.I.Configuraciones.Impresion.Configuracion.PapelLadoLargo);
            pageContent = new PageContent();
            fixedDocument.Pages.Add(pageContent);
            EncabezadoTabla = null;
            fixedPage = null;
            //ConteoPaginas = 0;   LED: Omitir el reseteo de la variable
        }

        /// <summary>
        /// Informa a los objetos que deben calcular su tamaño.
        /// </summary>
        /// <param name="objetos"></param>
        void CalcularDimensionesControl(params UIElement[] objetos)
        {
            foreach (UIElement item in objetos)
            {
                item.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                item.Arrange(new Rect(item.DesiredSize));
            }
        }

        #region CLASE UTILITARIA

        class clsLogicaYDato
        {
            public clsLogicaYDato(ILogicaImpresion logica, object fuenteDeDatos)
            {
                Logica = logica;
                FuenteDeDatos = fuenteDeDatos;
            }
            public ILogicaImpresion Logica { get; set; }
            public object FuenteDeDatos { get; set; }
        }

        #endregion

        #endregion

        #region UTILITARIOS FINALES

        /// <summary>
        /// Envía el documentio creado a la impresora.
        /// </summary>
        /// <param name="documento"></param>
        void EnviarDocumentoAImpresora(FixedDocument documento)
        {
            LocalPrintServer ServidorImpresion = new LocalPrintServer();

            var Cola = ServidorImpresion.GetPrintQueues(
              new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections }
              ).FirstOrDefault(x => x.Name ==
                RUV.I.Configuraciones.Impresion.Configuracion.ImpresoraPreferida);

            if (Cola == null)
            {
                RUV.I.UIPrincipal.ReportarErrorDeUsuario(
                  string.Format("La impresora favorita '{0}' ya no se encuentra en el sistema\nIngrese por el menú de opciones para seleccionar otra impresora.",
                  RUV.I.Configuraciones.Impresion.Configuracion.ImpresoraPreferida));
                return;
            }

            PrintDialog DialogoImpresion = new PrintDialog();
            DialogoImpresion.PrintQueue = Cola;

            // Diego Alvarez - 21/11/2013 - Configuración para impresión en hojas tamaño oficio
            if (RUV.I.Configuraciones.Impresion.Configuracion.TipoPapel == eTipoPapel.Oficio)
            {
                PageMediaSize pmsOficio = new PageMediaSize(PageMediaSizeName.NorthAmericaLegal, 816, 1248);
                PrintTicket ptOficio = new PrintTicket();
                ptOficio.PageMediaSize = pmsOficio;
                Cola.UserPrintTicket = ptOficio;
            }

            DialogoImpresion.PrintTicket = Cola.UserPrintTicket;
            DialogoImpresion.PrintTicket.PageOrientation = PageOrientation.Portrait;
            DialogoImpresion.PrintTicket.CopyCount = RUV.I.Configuraciones.Impresion.Configuracion.NumeroCopias;

            if (string.IsNullOrEmpty(RUV.I.Configuraciones.Impresion.Configuracion.ImpresoraPreferida))
            {
                return;
            }
            //Jhon vargas 26/02/2014 TL3
            try
            {
                XpsDocumentWriter documentWriter = PrintQueue.CreateXpsDocumentWriter(Cola);
                documentWriter.Write(documento, Cola.UserPrintTicket);
                this.ErrorImpresion = false;
            }
            catch (Exception ex)
            {
                if (!this.ErrorImpresion)
                {
                    this.ErrorImpresion = true;
                    RUV.I.UIPrincipal.ReportarErrorDeUsuario(
                      string.Format("Error en la impresion. Por favor valide la impresora seleccionada \ndesde el menu Opciones/Configuración/Impresora.\n\nError sistema: {0}", ex.Message));
                    return;
                }
            }
        }

        /// <summary>
        /// Graba un FixedDocument a un archivo XPS.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="document"></param>
        void GrabarXPS(string fileName, FixedDocument document, bool isAPart = false)
        {
            //Delete any existing file.
            File.Delete(fileName);

            if (isAPart)
            {
                string extension = ".xps";
                string fullName = fileName.Replace(extension, string.Empty);
                string[] fullNameParts = fullName.Split(new char[] { '\\' });
                List<string> path = new List<string>();
                for (int i = 0; i < fullNameParts.Length; i++)
                {
                    if (i < fullNameParts.Length - 1) path.Add(fullNameParts[i]);
                }
                string[] files = Directory.GetFiles(string.Join(@"\", path.ToArray()), string.Format("{0}??{1}", fullNameParts.Last(), extension));

                if (files.Length > 0) fullName = string.Concat(new object[] { fullName, 0, files.Length + 1, extension });
                else fullName = string.Concat(new object[] { fullName, 0, 1, extension });

                fileName = fullName;

                //Delete any existing file.
                File.Delete(fileName);
            }

            //Create a new XpsDocument at the given location.
            XpsDocument xpsDocument =
              new XpsDocument(fileName, FileAccess.ReadWrite,
              CompressionOption.NotCompressed);
            //Create a new XpsDocumentWriter for the XpsDocument object.
            XpsDocumentWriter xdw = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            //Write the document to the Xps file.
            xdw.Write(document);
            //Close down the saved document.
            xpsDocument.Close();
        }

        public byte[] GenerateMemoryXPS(FixedDocument document)
        {
            //using (MemoryStream stream = new MemoryStream(2048))
            //using (Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite)) {
            //    PackageStore.AddPackage(new Uri(string.Format("pack://{0}-{1}.xps", "TemporaryFile", Guid.NewGuid())), package);
            //    XpsDocument xpsDocument = new XpsDocument(package);
            //    XpsDocumentWriter xdw = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            //    xdw.Write(document);
            //    xpsDocument.Close();
            //    return stream.ToArray();
            //}

            // No pude escribir el xps en memoria. Utilizando archivo temporal y borrandolo
            var temporaryFileName = Path.Combine(RUV.I.Util.RutaArchivosLocales, string.Format("{0}.xps", Guid.NewGuid()));
            using (XpsDocument xpsDocument = new XpsDocument(temporaryFileName, FileAccess.ReadWrite, CompressionOption.NotCompressed))
            {
                XpsDocumentWriter xdw = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                xdw.Write(document);
            }
            var file = File.ReadAllBytes(temporaryFileName);
            File.Delete(temporaryFileName);
            return file;
            //Create a new XpsDocumentWriter for the XpsDocument object.

        }

        #endregion
    }
}
