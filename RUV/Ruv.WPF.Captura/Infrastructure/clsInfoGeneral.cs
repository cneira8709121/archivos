using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Ionic.Zip;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Captura.Infrastructure
{
    /// <summary>
    /// Administra información de caracter general.
    /// </summary>
    public class clsInfoGeneral : DependencyObject
    {
        #region CONSTRUCTOR

        public clsInfoGeneral()
        {
            //Random R = new Random();
            //foreach (var item in "7Np#  *!!!array*9823!* Qnt  ")
            //{
            //  System.Diagnostics.Debug.Write(string.Format("{0:D3}{1:D4}",
            //    R.Next(255),
            //    Convert.ToByte(item)));
            //}
        }

        #endregion

        #region VARIABLES

        const string GZPStr = "0890055025007822701122070035104003202600322110042198003301600331350033143009705501141800114001009703501210390042156005700200561850050013005112700331220042231003225300812530110048011603600320940032";

        /// <summary>
        /// Retorna la clave del archivo zip.
        /// </summary>
        internal string GZP
        {
            get
            {
                StringBuilder SB = new StringBuilder();
                for (int i = 0; i < (GZPStr.Length / 7); i++)
                {
                    SB.Append(Convert.ToChar(Convert.ToInt32(GZPStr.Substring(i * 7 + 3, 4))));
                }
                return SB.ToString();
            }
        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// Verdadero: Se ha descargado la última versión de la información fuera de línea.
        /// </summary>
        /// <returns></returns>
        public bool HayInformacion()
        {
            return System.IO.File.Exists(ArchivoLocalParametros);
        }

        /// <summary>
        /// Ruta completa del archivo de parámetros
        /// </summary>
        string ArchivoLocalParametros
        {
            get
            {
                return System.IO.Path.Combine(
                  RUV.I.Util.RutaArchivosLocales,
                  string.Format("Param_{0}.dat", RUV.I.Usuario.VersionArchivoParametros));
            }
        }

        #endregion

        #region DESCARGA DE LA INFORMACIÓN GENERAL

        /// <summary>
        /// Realizar la descarga de la información general en el background.
        /// </summary>
        public void Descargar()
        {
            BackgroundWorker BW = new BackgroundWorker();
            BW.DoWork += Descargar_DoWork;
            BW.RunWorkerCompleted += Descargar_RunWorkerCompleted;
            BW.RunWorkerAsync();
            EstaOcupado = true;
        }

        void Descargar_DoWork(object sender, DoWorkEventArgs e)
        {
            const int PasosTotales = 3;
            int PasoActual = 1;
            const string FormatoMensaje = "Paso {0} de {1} : {2}";
            string LlaveUsuario = string.Empty;
            try
            {
                // 1) Invocar el servicio.
                EstadoDescarga =
                  string.Format(FormatoMensaje, PasoActual++, PasosTotales, "Obteniendo datos");
                LlaveUsuario = RUV.I.Seguridad.LlaveUsuario;
                var VectorInformacion = RUV.I.Red.ServicioGeneral.ObtenerParametrosGenerales(LlaveUsuario);

                // 2) Truncar la información actual si existiese.
                EstadoDescarga =
                   string.Format(FormatoMensaje, PasoActual++, PasosTotales, "Limpieza");
                if (System.IO.File.Exists(ArchivoLocalParametros))
                    System.IO.File.Delete(ArchivoLocalParametros);

                // 3.0) Descomprimir en memoria para tomar la lista de las poblaciones.
                EstadoDescarga =
                   string.Format(FormatoMensaje, PasoActual++, PasosTotales, "Almacenamiento");

                clsDatosGenerales DGTemp = null;
                using (ZipFile zip = ZipFile.Read(VectorInformacion))
                {
                    zip.Password = GZP;
                    ZipEntry ArchivoEnZip = zip[0];
                    clsDatosGenerales Resultado = new clsDatosGenerales();

                    System.Xml.Serialization.XmlSerializer x =
                       new System.Xml.Serialization.XmlSerializer(Resultado.GetType());

                    using (var stream = ArchivoEnZip.OpenReader())
                    {
                        DGTemp = x.Deserialize(stream) as clsDatosGenerales;
                    }

                    //List<clsEntidadMunicipio> entidadesLista = new List<clsEntidadMunicipio>();
                    //entidadesLista = DGTemp.EntidadesMunicipios.ToList();
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\EntidadesPrecargadasNuevo.txt"))
                    //{
                    //    file.WriteLine("CNombreEncargado" + "," + "CNombreEntidad" + "," + "CNombreOtraEntidad" + "," + "NId" + "," + "NIdEntidad" + "," + "NIdMunicipio");
                    //    foreach (clsEntidadMunicipio item in entidadesLista)
                    //    {
                    //        file.WriteLine(item.CNombreEncargado.ToString() + "," + item.CNombreEntidad.ToString() + "," + item.CNombreOtraEntidad.ToString() + "," + item.NId.ToString() + "," + item.NIdEntidad.ToString() + "," + item.NIdMunicipio.ToString());
                    //    }
                    //    file.Close();
                    //}
                }

                // 3.1) Grabar las poblaciones en formato Sterling.
                double QtyPoblaciones = DGTemp.Poblaciones.Count;
                double Conteo = 0;

                RUV.I.LocalDB.Truncate(typeof(clsPoblacion));

                foreach (var UnaPoblacion in DGTemp.Poblaciones)
                {
                    Conteo++;
                    if (Conteo % 5 == 0)
                        this.Dispatcher.Invoke(
                        new Action(() =>
                          {
                              PorcentajeDescarga = Conteo / QtyPoblaciones * 100d;
                          }
                          ), System.Windows.Threading.DispatcherPriority.Normal, null);
                    RUV.I.LocalDB.Save<clsPoblacion>(UnaPoblacion);
                }

                RUV.I.LocalDB.Flush();

                // 3.2) Quitarla de los parámetros en memoria.
                DGTemp.Poblaciones = null;

                // 3.2) Grabar localmente los demás parámetros.
                RUV.I.Util.GrabarArchivoSerializado<clsDatosGenerales>(
                  ArchivoLocalParametros, DGTemp);

                //using (System.IO.FileStream _FileStream = new System.IO.FileStream(
                //  ArchivoLocalParametros, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                //{
                //  _FileStream.Write(VectorInformacion, 0, VectorInformacion.Length);
                //  _FileStream.Close();
                //}

                // 4) Fin.
                EstadoDescarga = "Terminado";
            }
            catch (Exception ex)
            {
                string Texto = ex.Message;
                Texto += "\n" + ex.StackTrace;
                MessageBox.Show(Texto);
            }


        }

        void Descargar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EstaOcupado = false;
            if (DescargaInformacionCompleted != null)
                DescargaInformacionCompleted(this, null);
        }

        /// <summary>
        /// Se lanza cuando se completa la descarga de la información general.
        /// </summary>
        public event EventHandler DescargaInformacionCompleted;

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// Verdadero: Se está haciendo una descarga de los parámetros.
        /// </summary>
        public bool EstaOcupado
        {
            get { return (bool)GetValue(EstaOcupadoProperty); }
            set { SetValue(EstaOcupadoProperty, value); }
        }

        public static readonly DependencyProperty EstaOcupadoProperty =
            DependencyProperty.Register("EstaOcupado", typeof(bool),
            typeof(clsInfoGeneral), new UIPropertyMetadata(false));

        /// <summary>
        /// Mensaje informativo sobre el estado de la descarga.
        /// </summary>
        public string EstadoDescarga
        {
            get { return (string)GetValue(EstadoDescargaProperty); }
            set
            {
                this.Dispatcher.Invoke(
                  new Action(() =>
                    SetValue(EstadoDescargaProperty, value)
                    ),
                  DispatcherPriority.Normal, null);
            }
        }

        public static readonly DependencyProperty EstadoDescargaProperty =
            DependencyProperty.Register("EstadoDescarga", typeof(string),
            typeof(clsInfoGeneral), new UIPropertyMetadata(null));


        /// <summary>
        /// Porcentaje de un trabajo de descarga.
        /// </summary>
        public double PorcentajeDescarga
        {
            get { return (double)GetValue(PorcentajeDescargaProperty); }
            set { SetValue(PorcentajeDescargaProperty, value); }
        }

        public static readonly DependencyProperty PorcentajeDescargaProperty =
            DependencyProperty.Register("PorcentajeDescarga", typeof(double),
            typeof(clsInfoGeneral), new UIPropertyMetadata(0d));



        #endregion

        #region CONSULTA DE LA INFORMACIÓN

        /// <summary>
        /// Retorna una lista con una parámetro nulo.
        /// Se utiliza para que las demás listas ofrezcan la selección nula.
        /// </summary>
        public IEnumerable<clsParametroGeneral> ParametroNulo
        {
            get
            {
                var ListaNulo = new List<clsParametroGeneral>();
                ListaNulo.Add(new clsParametroGeneral
                {
                    Id = null,
                    Nombre = "Sin información",
                    EsOtro = false,
                    Tipo = eTipoParametros.Ninguno
                });
                return ListaNulo.AsEnumerable();
            }
        }

        /// <summary>
        /// Precarga en memoria los parámetros desde el archivo local.
        /// </summary>
        public void PrecargarParametros()
        {
            using (ZipFile zip = ZipFile.Read(ArchivoLocalParametros))
            {
                zip.Password = GZP;
                ZipEntry ArchivoEnZip = zip[0];
                clsDatosGenerales Resultado = new clsDatosGenerales();
                System.Xml.Serialization.XmlSerializer x =
                   new System.Xml.Serialization.XmlSerializer(Resultado.GetType());

                using (var stream = ArchivoEnZip.OpenReader())
                {
                    DatosGenerales = x.Deserialize(stream) as clsDatosGenerales;
                }

                //List<clsEntidadMunicipio> entidadesLista = new List<clsEntidadMunicipio>();
                //entidadesLista = DatosGenerales.EntidadesMunicipios.ToList();
                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\EntidadesPrecargadas.txt"))
                //{
                //    file.WriteLine("CNombreEncargado" + "," + "CNombreEntidad" + "," + "CNombreOtraEntidad" + "," + "NId" + "," + "NIdEntidad" + "," + "NIdMunicipio");
                //    foreach (clsEntidadMunicipio item in entidadesLista) {
                //        file.WriteLine(item.CNombreEncargado.ToString() + "," + item.CNombreEntidad.ToString() + "," + item.CNombreOtraEntidad.ToString() + "," + item.NId.ToString() + "," + item.NIdEntidad.ToString() + "," + item.NIdMunicipio.ToString());
                //    }
                //    file.Close();
                //}

                }
            }

        /// <summary>
        /// Contenedor de los datos generales que en todo momento están presentes en memoria.
        /// </summary>
        clsDatosGenerales DatosGenerales;

        List<clsParametroPais> PaisNulo;
        List<clsParametroDepartamento> DepartamentoNulo;
        List<clsParametroMunicipio> MunicipioNulo;
        List<clsEntidadMunicipio> EntidadesMunicipiosNulo;

        /// <summary>
        /// La lista de los paises.
        /// </summary>
        public List<clsParametroPais> ListaPaises
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                if (PaisNulo == null)
                {
                    var DeptoNulo = new clsParametroPais() { Id = null, Nombre = "", TieneRepresentacion = true };
                    PaisNulo = new List<clsParametroPais>();
                    PaisNulo.Add(DeptoNulo);
                }
                return PaisNulo.Concat(DatosGenerales.Paises).ToList();
            }
        }

        /// <summary>
        /// La lista de los departamentos.
        /// </summary>
        public List<clsParametroDepartamento> ListaDepartamentos(Int64 paisID)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return null;

            if (DepartamentoNulo == null)
            {
                var DeptoNulo = new clsParametroDepartamento() { Id = null, Nombre = "", TieneRepresentacion = true };
                DepartamentoNulo = new List<clsParametroDepartamento>();
                DepartamentoNulo.Add(DeptoNulo);
            }

            var Resultado = DatosGenerales.Departamentos
            .Where(x => x.PaisId == paisID)
            .Select(x => x).ToList();

            //return DepartamentoNulo.Concat(DatosGenerales.Departamentos).ToList();
            return DepartamentoNulo.Concat(Resultado).ToList();
        }

        /// <summary>
        /// La lista de los departamentos.
        /// </summary>
        public List<clsParametroDepartamento> ListaDepartamentosTodos
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                if (DepartamentoNulo == null)
                {
                    var DeptoNulo = new clsParametroDepartamento() { Id = null, Nombre = "", TieneRepresentacion = true };
                    DepartamentoNulo = new List<clsParametroDepartamento>();
                    DepartamentoNulo.Add(DeptoNulo);
                }

                return DepartamentoNulo.Concat(DatosGenerales.Departamentos).ToList();
            }
        }

        /// <summary>
        /// La lista de los municipios para un departamento.
        /// </summary>
        public List<clsParametroMunicipio> ListaMunicipios(Int64 departamentoId)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return null;

            if (MunicipioNulo == null)
            {
                var McpioNulo = new clsParametroMunicipio() { Id = null, Nombre = "", TieneRepresentacion = true };
                MunicipioNulo = new List<clsParametroMunicipio>();
                MunicipioNulo.Add(McpioNulo);
            }

            var Resultado = DatosGenerales.Municipios
              .Where(x => x.DepartamentoId == departamentoId)
              .Select(x => x).ToList();

            return MunicipioNulo.Concat(Resultado).ToList();
        }

        /// <summary>
        /// La lista total de los municipios.
        /// </summary>
        public List<clsParametroMunicipio> ListaMunicipiosTodos
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                return DatosGenerales.Municipios;
            }
        }

        /// <summary>
        /// Lista de nacionalidades
        /// </summary>
        public List<clsParametroNacionalidad> ListaNacionalidades 
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                return DatosGenerales.Nacionalidades;
            }
        }

        public List<clsEntidadMunicipio> ListaEntidadesMunicipiosTodos
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.EntidadesMunicipios;
            }
        }

        public List<clsEntidadMunicipio> ListaEntidadesMunicipios(long? nIdMunicipio)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return null;

            if (EntidadesMunicipiosNulo == null)
            {
                clsEntidadMunicipio em = new clsEntidadMunicipio();
                //em.NId = 3923;
                //em.NIdEntidad = 3;
                //em.CNombreOtraEntidad = "YY";
                //em.CNombreEncargado = "ZZ";
                //em.CNombreEntidad = "XX";
                //em.NIdMunicipio = 6516;
                EntidadesMunicipiosNulo = new List<clsEntidadMunicipio>();
                EntidadesMunicipiosNulo.Add(em);
            }

            return EntidadesMunicipiosNulo
                .Concat(DatosGenerales.EntidadesMunicipios
                          .Where(x => x.NIdMunicipio == nIdMunicipio)
                          .ToList())
                .ToList();
            //.Concat(DatosGenerales.EntidadesMunicipios
            //          .Where(x => x.NId == 3923)
            //          .ToList())
            //.ToList();
        }

        /// <summary>
        /// Todos los parámetros disponibles.
        /// </summary>
        public List<clsParametroGeneral> ListaParametros
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                return DatosGenerales.Parametros;
            }
        }

        /// <summary>
        /// La lista de parámetros para un conjunto.
        /// </summary>
        /// <param name="conjunto"></param>
        /// <returns></returns>
        public List<clsParametroGeneral> ListaDetallesGrupoParam(eGruposParametros conjunto)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return null;

            return (from p in ListaParametros
                    join c in DatosGenerales.GrupoParamDetalle on p.Id equals c.ParametroId
                    orderby p.EsOtro, c.Orden, p.Nombre
                    where c.Conjunto == conjunto
                    select p).ToList();
        }

        /// <summary>
        /// La lista de los tipos de parámetros
        /// </summary>
        public List<clsParametroGeneral> ListaTiposDocumentos
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.TipoDeDocumentoDeIdentidad).ToList();

            }
        }

        /// <summary>
        /// La lista de las relaciones.
        /// </summary>
        public List<clsParametroGeneral> ListaRelaciones
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.Relacion).ToList();
            }
        }

        /// <summary>
        /// La lista de los estados civiles.
        /// </summary>
        public List<clsParametroGeneral> ListaEstadoCivil
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.EstadoCivil).ToList();
            }
        }

        /// <summary>
        /// La lista de los regímenes especiales.
        /// </summary>
        public List<clsParametroGeneral> ListaRegimenEspecial
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.RegimenEspecial).ToList();
            }
        }

        /// <summary>
        /// La lista de los géneros.
        /// </summary>
        public List<clsParametroGeneral> ListaGeneros
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.Genero).ToList();
            }
        }

        /// <summary>
        /// La lista de tipos de afiliación al sector salud.
        /// </summary>
        public List<clsParametroGeneral> ListaTipoAfiliacionSalud
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.AfiliaciónASalud).ToList();
            }
        }

        /// <summary>
        /// La lista de las activades para las que se está incapacitado.
        /// </summary>
        public List<clsParametroGeneral> ListaDiscapacidadParaActividades
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.DiscapacidadEnActividades).ToList();
            }
        }

        /// <summary>
        /// La lista de las etnias.
        /// </summary>
        public List<clsParametroGeneral> ListaEtnias
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.MinoriaEtnica).ToList();
            }
        }

        public List<clsParametroGeneral> ListaTipoEntorno
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                var Resultado = new List<clsParametroGeneral>();

                foreach (var UnTipoEntorno in Enum.GetValues(typeof(eTipoEntorno)))
                {
                    Resultado.Add(new clsParametroGeneral
                    {
                        Id = (int)UnTipoEntorno,
                        EsOtro = false,
                        Nombre = UnTipoEntorno.ToString()
                    });
                }

                return ParametroNulo.Concat(Resultado).ToList();
            }
        }

        List<clsParametroGeneral> _ListaTiposDeBienesAnexo01;
        /// <summary>
        /// La lista de los tipos de bienes para el anexo 01.
        /// Estos son valores fijos que no vienen de la base de datos.
        /// </summary>
        public List<clsParametroGeneral> ListaTiposDeBienesAnexo01
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                if (_ListaTiposDeBienesAnexo01 == null)
                {
                    _ListaTiposDeBienesAnexo01 = new List<clsParametroGeneral>();
                    _ListaTiposDeBienesAnexo01.Add(new clsParametroGeneral { Id = 0, Nombre = "Mueble" });
                    _ListaTiposDeBienesAnexo01.Add(new clsParametroGeneral { Id = 1, Nombre = "Inmueble" });
                }
                return _ListaTiposDeBienesAnexo01;
            }
        }

        List<clsParametroGeneral> _ListaCalidadVictimaAnexo01;
        /// <summary>
        /// La lista de las calidad es de la víctimas con respecto a los bienes, para el anexo 01.
        /// </summary>
        public List<clsParametroGeneral> ListaCalidadVictimaAnexo01
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                //TODO: Obtener esta lista desde la base de datos.
                if (_ListaCalidadVictimaAnexo01 == null)
                {
                    _ListaCalidadVictimaAnexo01 = new List<clsParametroGeneral>();
                    _ListaCalidadVictimaAnexo01.Add(new clsParametroGeneral { Id = 1, Nombre = "Propietario" });
                    _ListaCalidadVictimaAnexo01.Add(new clsParametroGeneral { Id = 2, Nombre = "Poseedor" });
                    _ListaCalidadVictimaAnexo01.Add(new clsParametroGeneral { Id = 3, Nombre = "Arrendatario o tenedor" });
                    _ListaCalidadVictimaAnexo01.Add(new clsParametroGeneral { Id = 4, Nombre = "Conductor" });
                }
                return _ListaCalidadVictimaAnexo01;
            }
        }

        /// <summary>
        /// Todos los grupos étnicos.
        /// </summary>
        public List<clsGrupoEtnica> ListaGruposEtnicos
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.GruposEtnicos.ToList();
            }
        }

        /// <summary>
        /// Todas las validaciones
        /// </summary>
        public List<clsValidaciones> ListaValidaciones
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.Validaciones.ToList();
            }
        }

        /// <summary>
        /// Todas las comunidades étnicas
        /// </summary>
        public List<clsComunidadEtnica> ListaComunidadesEtnicas
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.ComunidadesEtnicas.ToList();
            }
        }

        List<clsItem> _ListaTiposPoblaciones;
        /// <summary>
        /// Todos los tipos de poblaciones... estos datos son constantes.
        /// </summary>
        public List<clsItem> ListaTiposPoblaciones
        {
            get
            {
                if (_ListaTiposPoblaciones == null)
                {
                    _ListaTiposPoblaciones =
                      (from x in
                           Enum.GetValues(typeof(eTipoPoblacion)).Cast<eTipoPoblacion>()
                       select new clsItem { Id = Convert.ToInt32(x), Nombre = x.ToString().Replace("_", " ") }
                      ).ToList();
                }

                return _ListaTiposPoblaciones;
            }
        }

        /// <summary>
        /// Los nombres de las poblaciones.
        /// OJO: Este es un tipo-query y no una lista de objetos.
        /// </summary>
        public List<Wintellect.Sterling.Keys.TableKey<Ruv.Infrastructure.Crosscutting.Common.General.clsPoblacion, int>>
          ListaPoblaciones
        {
            get
            {
                return RUV.I.LocalDB.Query<Ruv.Infrastructure.Crosscutting.Common.General.clsPoblacion, int>();
            }
        }

        /// <summary>
        /// Los nombres de las poblaciones, indexados por Municipio y Tipo de Población.
        /// OJO: Este es un tipo-query y no una lista de objetos.
        /// </summary>
        public List<Wintellect.Sterling.Indexes.TableIndex<Ruv.Infrastructure.Crosscutting.Common.General.clsPoblacion, Tuple<int, int>, int>>
          ListaPoblacionesPorIndice
        {
            get
            {
                return RUV.I.LocalDB.Query<Ruv.Infrastructure.Crosscutting.Common.General.clsPoblacion, int, int, int>("MunicipioTipoPoblacion");
            }
        }


        /// <summary>
        /// la lista de las unidades territoriales.
        /// </summary>
        public List<clsParametroUT> ListaUnidadesTerritoriales
        {
            get
            {
                return DatosGenerales.UnidadesTerritoriales;
            }
        }

        public List<clsCausal> ListaCausales
        {
            get
            {
                return DatosGenerales.Causales;
            }
        }

        #region Info de Glosas
        /// <summary>
        /// Tipo de Glosas o Intención deGlosa.
        /// </summary>
        public List<clsParametroGeneral> ListaTipoIntoGlosa
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                //TODO: Obtener esta lista desde la base de datos.
                if (_ListaTipoIntoGlosa == null)
                {
                    _ListaTipoIntoGlosa = new List<clsParametroGeneral>();
                    _ListaTipoIntoGlosa.Add(new clsParametroGeneral { Id = 1, Nombre = "Glosa" });
                    _ListaTipoIntoGlosa.Add(new clsParametroGeneral { Id = 2, Nombre = "Intención de Glosa" });
                }
                return _ListaTipoIntoGlosa;
            }
        }
        private List<clsParametroGeneral> _ListaTipoIntoGlosa;

        /// <summary>
        /// La lista de los Tipos de categorias de Glosas
        /// </summary>
        public List<clsParametroGeneral> ListaCategoriasGlosa
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.CategoríasYConceptosDeGlosas).ToList();
            }
        }
        /// <summary>
        /// La lista de los Tipos de categorias de intentos de Glosa
        /// </summary>
        public List<clsParametroGeneral> ListaCategoriasIntentoGlosa
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;
                return DatosGenerales.Parametros.
                  Where(x => x.Tipo == eTipoParametros.CategoríasDeIntentosDeGlosa).ToList();
            }
        }

        /// <summary>
        /// Listados De CriticaN
        /// </summary>
        public List<clsPreguntaCriticaN> PreguntasCriticaN
        {
            get
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return null;

                return DatosGenerales.PreguntasCriticaN.ToList();
            }
        }
        #endregion

        #endregion

        #region VERSION DE LA APLICACIÓN

        /// <summary>
        /// El número de versión de la aplicación.
        /// </summary>
        public string VersionAplicacion
        {
            get
            {
                try
                {
                    System.Deployment.Application.ApplicationDeployment Publicacion =
                      System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                    Version Version = Publicacion.CurrentVersion;
                    //return string.Format("Versión {0}.{1}.{2}.{3}",
                    //  Version.Major,
                    //  Version.MajorRevision,
                    //  Version.Minor,
                    //  Version.MinorRevision);

                    return string.Format("Versión {0}.{1}.{2}.{3}",
                     Version.Major,
                     Version.Minor,
                     Version.Build,
                     Version.Revision);
                }
                catch { }

                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        #endregion

        #region INFORMACION DEL ANEXO ACTUALMENTE EN EDICION

        int? _NumeroAnexoActual;
        /// <summary>
        /// Retorna el número del anexo que se está editando, de lo contrario
        /// retorna null.
        /// </summary>
        public int? NumeroAnexoActual
        {
            get { return _NumeroAnexoActual; }
            set
            {
                _NumeroAnexoActual = value;
            }
        }


        #endregion

    }
}