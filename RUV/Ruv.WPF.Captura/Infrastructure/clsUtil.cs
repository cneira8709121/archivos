using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Ionic.Zip;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Registro.Secciones.Controles;
using System.Collections;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsUtil
    {
        #region INFORMACIÓN DE ALMACENAMIENTO LOCAL


        string _RutaArchivosLocales;

        /// <summary>
        /// La ruta a los archivos locales 
        /// </summary>
        public string RutaArchivosLocales
        {
            get
            {
                if (_RutaArchivosLocales == null)
                {
                    _RutaArchivosLocales = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "RUVWPF";
                    switch (RUV.I.ModoEjecucion)
                    {
                        case eModoEjecucion.Desarrollo:
                            _RutaArchivosLocales += "_Desarrollo/";
                            break;
                        case eModoEjecucion.Pruebas:
                            _RutaArchivosLocales += "_Pruebas/";
                            break;
                        case eModoEjecucion.Produccion:
                            _RutaArchivosLocales += "/";
                            break;
                        case eModoEjecucion.Capacitacion:
                            _RutaArchivosLocales += "_Capacitacion/";
                            break;
                    }
                }
                return _RutaArchivosLocales;
            }
        }

        /// <summary>
        /// Carga un archivo serializado, comprimido y con clave.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public T1 CargarArchivoSerializado<T1>(string rutaArchivo) where T1 : class
        {
            T1 Resultado = null;

            using (ZipFile zip = ZipFile.Read(rutaArchivo))
            {
                zip.Password = RUV.I.InfoGeneral.GZP;
                ZipEntry ArchivoEnZip = zip[0];

                System.Xml.Serialization.XmlSerializer x =
                   new System.Xml.Serialization.XmlSerializer(typeof(T1));

                clsDeclaracion.DesSerializando = true;
                using (var stream = ArchivoEnZip.OpenReader())
                    Resultado = x.Deserialize(stream) as T1;
                clsDeclaracion.DesSerializando = false;
            }

            return Resultado;
        }

        /// <summary>
        /// Graba un archivo serializado, comprimido y con clave.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="rutaArchivo"></param>
        /// <param name="objeto"></param>
        public void GrabarArchivoSerializado<T1>(string rutaArchivo, T1 objeto) where T1 : class
        {
            System.Xml.Serialization.XmlSerializer Serializador =
                       new System.Xml.Serialization.XmlSerializer(objeto.GetType());

            string ArchivoTemp = rutaArchivo + ".tmp";

            if (System.IO.File.Exists(rutaArchivo))
                System.IO.File.Delete(rutaArchivo);

            if (System.IO.File.Exists(ArchivoTemp))
                System.IO.File.Delete(ArchivoTemp);

            using (StreamWriter SW = System.IO.File.CreateText(ArchivoTemp))
            {
                Serializador.Serialize(SW, objeto);
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.Password = RUV.I.InfoGeneral.GZP;
                zip.AddFile(ArchivoTemp);
                zip.Save(rutaArchivo);
            }

            if (System.IO.File.Exists(ArchivoTemp))
                System.IO.File.Delete(ArchivoTemp);
        }


        #endregion

        #region EXTRAER EL TEXTO VISIBLE EN UN TEXTBLOCK

        /// <summary>
        /// Para un textblock se retorna el texto que es visible.
        /// El textblock debe implementar estar propiedades:
        /// TextTrimming="WordEllipsis"
        /// TextWrapping="Wrap"
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string ObtenerTextoVisible(Visual v)
        {
            Drawing textBlockDrawing = VisualTreeHelper.GetDrawing(v);
            var glyphs = new List<PositionedGlyphs>();

            ObtenerGlyphs(glyphs, Transform.Identity, textBlockDrawing);

            IEnumerable<PositionedGlyphs> Ordenados = (from glyph in glyphs
                                                       let roundedBaselineY = Math.Round(glyph.Position.Y, 1)
                                                       orderby roundedBaselineY ascending, glyph.Position.X ascending
                                                       select glyph);
            IEnumerable<PositionedGlyphs> LosGlyphs = null;

            var Ultimo = Ordenados.LastOrDefault();
            // Resulta que existe el caracter '3 puntos'.
            const int TresPuntos = 8230;
            if (Convert.ToInt32(Ultimo.Glyphs.GlyphRun.Characters[0]) == TresPuntos)
                LosGlyphs = Ordenados.Reverse().Skip(1).Reverse();
            else
                LosGlyphs = Ordenados;

            int ConteoGlyphs = LosGlyphs.Sum(x => x.Glyphs.GlyphRun.Characters.Count);

            StringBuilder SB = new StringBuilder();
            int Pos = -1;
            int Validos = 0;
            TextBlock containerText = v as TextBlock;

            while (Validos < ConteoGlyphs)
            {
                Validos++;
                do
                {
                    Pos++;
                    SB.Append(containerText.Text[Pos]);
                } while (containerText.Text[Pos] == '\r' || containerText.Text[Pos] == '\n');
            }

            return SB.ToString();
        }

        struct PositionedGlyphs
        {
            public PositionedGlyphs(System.Windows.Point position, GlyphRunDrawing grd)
            {
                this.Position = position;
                this.Glyphs = grd;
            }
            public readonly System.Windows.Point Position;
            public readonly GlyphRunDrawing Glyphs;
        }

        static void ObtenerGlyphs(List<PositionedGlyphs> glyphList, Transform tx, Drawing d)
        {
            var glyphs = d as GlyphRunDrawing;
            if (glyphs != null)
            {
                var textOrigin = glyphs.GlyphRun.BaselineOrigin;
                System.Windows.Point glyphPosition = tx.Transform(textOrigin);
                glyphList.Add(new PositionedGlyphs(glyphPosition, glyphs));
            }
            else
            {
                var g = d as DrawingGroup;
                if (g != null)
                {
                    // Drawing groups are allowed to transform their children, so we need to
                    // keep a running accumulated transform for where we are in the tree.
                    Matrix current = tx.Value;
                    if (g.Transform != null)
                    {
                        // Note, Matrix is a struct, so this modifies our local copy without
                        // affecting the one in the 'tx' Transforms.
                        current.Append(g.Transform.Value);
                    }
                    var accumulatedTransform = new MatrixTransform(current);
                    foreach (Drawing child in g.Children)
                    {
                        ObtenerGlyphs(glyphList, accumulatedTransform, child);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Borra un registro si está perviamente marcado como "Insertar" o 
        /// lo marca como borrado en los demás casos.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="coleccion"></param>
        /// <param name="entidad"></param>
        public void BorrarEntidad<T1>(ObservableCollection<T1> coleccion,
          T1 entidad) where T1 : clsEntidadBase
        {
            clsUtils.BorrarEntidad<T1>(coleccion, entidad);
        }

        /// <summary>
        /// Establece el ID de la entidad al siguiente para los casos
        /// de nuevos registros.
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="entidad"></param>
        public void EntidadEstablecerSiguienteId(
          IEnumerable<clsEntidadBase> lista,
          clsEntidadBase entidad)
        {
            int Siguiente = int.MinValue;
            if (lista == null)
                entidad.ID = Siguiente;
            else
            {
                Siguiente = (from num in lista
                             where num.ID.HasValue
                             && num.ID.Value < 0
                             select num.ID.Value).DefaultIfEmpty().Max();
                if (Siguiente == 0)
                    entidad.ID = int.MinValue;
                else
                    entidad.ID = Siguiente + 1;
            }
        }

        public void EntidadEstablecerSiguienteId_General(
          IEnumerable<clsEntidadBase> lista,
          clsEntidadBase entidad)
        {
            int Siguiente = int.MinValue;
            if (lista == null)
                entidad.ID = Siguiente;
            else
            {
                Siguiente = (from num in lista
                             where num.ID.HasValue
                             && num.ID.Value < 0
                             select num.ID.Value).DefaultIfEmpty().Max();
                if (Siguiente == 0)
                    entidad.ID = int.MinValue;
                else
                    entidad.ID = Siguiente + 1;
            }
        }

        /// <summary>
        /// Copia la información de este objeto en otro con las mismas propiedades, si existen.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public void AlimentarDesde<T1>(T1 origen, T1 destino) where T1 : class
        {
            // Propiedades origen, de este objeto.
            var PropSource = origen.GetType().GetProperties().Where(
                pi => pi.GetGetMethod() != null
                && (!pi.GetGetMethod().ReturnType.IsArray)
                && (!pi.PropertyType.ToString().Contains("Collections"))
                && pi.DeclaringType == pi.ReflectedType
                && pi.Name != "Item")
              .Select(pi => new
              {
                  Name = pi.Name,
                  Value = pi.GetGetMethod().Invoke(origen, null),
                  Tipo = pi.PropertyType.ToString()
              });

            foreach (var item in PropSource)
            {
                // Propiedades destino. El nuevo objeto.
                var PropTarget = destino.GetType().GetProperties().Where(
                    pi => pi.GetSetMethod() != null
                    && (!pi.GetGetMethod().ReturnType.IsArray)
                    && (!pi.PropertyType.ToString().Contains("Collections"))
                    && pi.Name == item.Name)
                  .Select(pi => new
                  {
                      Name = pi.Name,
                      Value = pi.GetGetMethod().Invoke(destino, null),
                      Tipo = pi.PropertyType.ToString()
                  }).FirstOrDefault();

                if (PropTarget != null)
                {
                    destino.GetType().GetProperty(item.Name).SetValue(destino, item.Value, null);
                }
            }
        }

        /// <summary>
        /// Copia la información de este objeto en otro con las mismas propiedades, si existen.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public T1 CrearCopia<T1>(object origen) where T1 : class
        {
            if (origen == null) return null;

            T1 Destino = Activator.CreateInstance<T1>();

            // Propiedades origen, de este objeto.
            var PropSource = origen.GetType().GetProperties().Where(
                pi => pi.GetGetMethod() != null
                && (!pi.GetGetMethod().ReturnType.IsArray)
                && (!pi.PropertyType.ToString().Contains("Collections"))
                && pi.DeclaringType == pi.ReflectedType
                && pi.Name != "Item")
              .Select(pi => new
              {
                  Name = pi.Name,
                  Value = pi.GetGetMethod().Invoke(origen, null),
                  Tipo = pi.PropertyType.ToString()
              });

            foreach (var item in PropSource)
            {
                // Propiedades destino. El nuevo objeto.
                var PropTarget = Destino.GetType().GetProperties().Where(
                    pi => pi.GetSetMethod() != null
                    && (!pi.GetGetMethod().ReturnType.IsArray)
                    && (!pi.PropertyType.ToString().Contains("Collections"))
                    && pi.Name == item.Name)
                  .Select(pi => new
                  {
                      Name = pi.Name,
                      Value = pi.GetGetMethod().Invoke(Destino, null),
                      Tipo = pi.PropertyType.ToString()
                  }).FirstOrDefault();

                if (PropTarget != null)
                {
                    Destino.GetType().GetProperty(item.Name).SetValue(Destino, item.Value, null);
                }
            }

            return Destino;
        }

        /// <summary>
        /// Crea una copia de una ObservableCollection para un tipo sencillo.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        public ObservableCollection<T1> CopiarObservableCollectionOf<T1>(
          ObservableCollection<T1> propiedad)
        {
            return Ruv.Infrastructure.Crosscutting.Common.clsUtils.CopiarObservableCollectionOf<T1>(propiedad);
        }

        /// <summary>
        /// Crea una copia de una List para un tipo genérico sencillo.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        public static List<T1> CopiarListOf<T1>(
          List<T1> propiedad)
        {
            return Ruv.Infrastructure.Crosscutting.Common.clsUtils.CopiarListOf<T1>(propiedad);
        }

        private clsValidadorEntidades _ValidadorEntidades;
        /// <summary>
        /// Clase que consulta el estado de validación de una entidad.
        /// </summary>
        public clsValidadorEntidades ValidadorEntidades
        {
            get
            {
                if (_ValidadorEntidades == null)
                    _ValidadorEntidades = new clsValidadorEntidades();
                return _ValidadorEntidades;
            }
        }

        #region FILTROS COMUNES

        /// <summary>
        /// El filtro para las entidades no-eliminadas.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Boolean FiltroEntidadNoEliminada(object entidad)
        {
            return (entidad as clsEntidadBase).EstadoRegistro != eEstadoRegistro.Eliminado;
        }

        #endregion

        /// <summary>
        /// Verdadero: Se está ejecutando y depurando.
        /// </summary>
        public bool EstaDentroDeVisualStudio
        {
            get
            {
                bool Resultado = false;
                if (System.Diagnostics.Debugger.IsAttached)
                    Resultado = true;
                return Resultado;
            }
        }

        /// <summary>
        /// Retorna verdadero si estamos en modo de diseño dentro de VS.
        /// </summary>
        public bool EnModoDeDiseño
        {
            get
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
                bool Resultado = process.ProcessName.ToLower().Trim() == "devenv";
                process.Dispose();
                return Resultado;
            }

        }

        /// <summary>
        /// Carga un archivo a un array de bytes.
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public byte[] CargarArchivo(string rutaArchivo)
        {
            byte[] Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream FS = new System.IO.FileStream(rutaArchivo, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader BR = new System.IO.BinaryReader(FS);

                // get total byte length of the file
                long BytesTotales = new System.IO.FileInfo(rutaArchivo).Length;

                // read entire file into buffer
                Buffer = BR.ReadBytes((Int32)BytesTotales);

                // close file reader
                FS.Close();
                FS.Dispose();
                BR.Close();
            }
            catch (Exception _Exception)
            {
                RUV.I.Log.Registrar("CargarArchivo", _Exception);
                throw _Exception;
            }

            return Buffer;
        }

        #region VALIDAR UNA DECLARACION

        ResumenValidacionDeclaracion VentanaValidacion = null;

        /// <summary>
        /// Verdadero: La declaración no tiene errores.
        /// False: Hay por lo menos un error y como consecuencia se abre la ventana de errores.
        /// </summary>
        /// <param name="declaracion"></param>
        /// <returns></returns>
        public eResultadoValidacion ValidarDeclaracion(clsDeclaracion declaracion)
        {
            if (VentanaValidacion != null)
            {
                VentanaValidacion.Close();
                VentanaValidacion = null;
            }

            List<eEstadoValidacion> Requeridas = ValidacionesRequeridas();
            int validacionesSaltadas = 0;

            if (Requeridas != null && Requeridas.Count > 0)
            {
                var listaErroresValidacion = declaracion.ValidarDeclaracion(Requeridas, ref validacionesSaltadas);
                if (listaErroresValidacion == null || !listaErroresValidacion.Any())
                    return validacionesSaltadas <= 0 ? eResultadoValidacion.PasaValoracion : eResultadoValidacion.PasaGlosa;
            }
            // Si hay errores se abre la ventana de errores y se retorna False.
            VentanaValidacion = new ResumenValidacionDeclaracion(declaracion);
            VentanaValidacion.Show();

            return eResultadoValidacion.NoPasaValidaciones;
        }

        public static List<eEstadoValidacion> ValidacionesRequeridas()
        {
            List<eEstadoValidacion> Requeridas = new List<eEstadoValidacion>();
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Obligatorias))
            {
                Requeridas.Add(eEstadoValidacion.Obligatoria);
            }
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Flexibles))
            {
                Requeridas.Add(eEstadoValidacion.Flexible);
            }
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Obcionales))
            {
                Requeridas.Add(eEstadoValidacion.NoAplica);
            }
            return Requeridas;
        }

        /// <summary>
        /// Cierra la ventana de validación en caso de estar abierta.
        /// </summary>
        public void CerrarVentanaValidacion()
        {
            if (VentanaValidacion == null) return;

            VentanaValidacion.Close();
            VentanaValidacion = null;
        }

        #endregion

        /// <summary>
        /// Retorna la lista de los discos USB conectados al sistema.
        /// </summary>
        public IEnumerable<string> ObtenerDiscosExtraibles
        {
            get
            {
                DriveInfo[] ListDrives = DriveInfo.GetDrives();
                var Resultado = ListDrives.Where(x => x.DriveType == DriveType.Removable);
                if (Resultado == null)
                    return null;
                else
                    return Resultado.Select(x => x.RootDirectory.Name);
            }
        }

        public string ConcatenaValoresSinDuplicados(ArrayList sourceList, string separator)
        {
            ArrayList list = new ArrayList();
            StringBuilder listn = new StringBuilder();
            foreach (string item in sourceList)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    listn.Append(separator);
                    listn.Append(item.ToString());                    
                }
            }            
            return listn.ToString().Substring(separator.Length);         
        }
    }
}
