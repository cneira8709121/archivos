using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel.Activation;
using Ionic.Zip;
using Ruv.Business.General;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.General;
using grb = Ruv.Business.Captura;

[AspNetCompatibilityRequirements(RequirementsMode
  = AspNetCompatibilityRequirementsMode.Required)]
public class GeneralService : IGeneralService
{

    const string ErrorGenerico = "Se presentó un inconveniente con el servicio";

    public GeneralService()
    {

    }

    #region GRABACIÓN DE UNA DECLARACIÓN

    /// <summary>
    /// Recibe una declaración y la graba en la base de datos.
    /// </summary>
    /// <param name="declaracion"></param>
    /// <returns></returns>
    public clsResultado DeclaracionAlmacenar(clsDeclaracion declaracion, string numeroDeclaracion, clsUsuario usuario)
    {
        var securityHandler = new clsSeguridad();
        string cErrorCredenciales = string.Empty;
        if (!securityHandler.CredencialesValidas(numeroDeclaracion, ref cErrorCredenciales))
            return new clsResultado() { ErroresDB = new List<string>() { "No se pudo verificar las credenciales de usuario, " + cErrorCredenciales } };

        // En este punto, el usuario actual es: Seguridad.Usuario;
        declaracion.UsuarioId = securityHandler.Usuario.Id;
        declaracion.UnidadTerritorialId = securityHandler.Usuario.UnidadTerritorialId;

        grb::Procesos processHandler = new grb::Procesos();
        processHandler.DeclaracionAlmacenar(declaracion, usuario);

        clsResultado processResult = new clsResultado { AdvertenciasDB = processHandler.Advertencias, Declaracion = declaracion, ErroresDB = processHandler.Errores };

        declaracion.DocumentoDigital = null;

        //Guardar la primera version de la declaración, en la cual esta la información digitada y el documento escaneado
        if (processResult.ErroresDB.Count == 0)
        {
            GrabarPrimeraVersion(declaracion, (int)declaracion.ID);
        }

        return processResult;
    }

    #endregion

    #region ENTREGA DE INFORMACIÓN DE PARÁMETROS

    const string ClaveZip = "7Np#  *!!!array*9823!* Qnt  ";

    static object Lock_ObtenerParametrosGenerales = "DummyValue";

    public byte[] ObtenerParametrosGenerales(string tipoParams)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        if (!Seguridad.CredencialesValidas(tipoParams)) return null;

        GenerarArchivoParametros();
        return ObtenerArchivoParametros();
    }

    /// <summary>
    /// El nombre del archivo local comprimido con los parámetros.
    /// </summary>
    string ArchivoLocalParametros
    {
        get
        {
            string Sufijo = System.Configuration.ConfigurationManager.AppSettings["UltimoArchivoParametros"];
            if (string.IsNullOrWhiteSpace(Sufijo))
                Sufijo = DateTime.Now.ToString("yyyyMMdd");

            return Path.Combine(Path.GetTempPath(),
             string.Format("SipodParametros_{0}.dat", Sufijo));
        }
    }

    /// <summary>
    /// Genera el archivo local de parámetros si no existe.
    /// </summary>
    private void GenerarArchivoParametros()
    {
        lock (Lock_ObtenerParametrosGenerales)
        {
            if (!File.Exists(ArchivoLocalParametros))
            {
                var ArchivoPlano =
                  Path.Combine(Path.GetTempPath(), "SipodParametros.xml");

                Ruv.WPF.Server.clsGeneral Gen = new Ruv.WPF.Server.clsGeneral();
                var Resultado = Gen.ObtenerParametrosGenerales();

                System.Xml.Serialization.XmlSerializer Serializador =
                  new System.Xml.Serialization.XmlSerializer(Resultado.GetType());

                using (StreamWriter SW = System.IO.File.CreateText(ArchivoPlano))
                {
                    Serializador.Serialize(SW, Resultado);
                }

                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    zip.Password = ClaveZip;
                    zip.AddFile(ArchivoPlano);
                    zip.Save(ArchivoLocalParametros);
                }
            }
        }
    }

    /// <summary>
    /// Retorna el archivo de paramaétros como un vector de bytes.
    /// </summary>
    /// <returns></returns>
    private byte[] ObtenerArchivoParametros()
    {
        byte[] VectorArchivo = null;

        // Open file for reading
        FileStream _FileStream = new FileStream(
          ArchivoLocalParametros, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
        long _TotalBytes = new System.IO.FileInfo(ArchivoLocalParametros).Length;
        VectorArchivo = _BinaryReader.ReadBytes((Int32)_TotalBytes);

        // Close file reader
        _FileStream.Close();
        _FileStream.Dispose();
        _BinaryReader.Close();

        return VectorArchivo;
    }

    #endregion

    #region BUSCAR UNA DECLARACIÓN

    /// <summary>
    /// Realiza una búsqueda de declaraciones que cumplen con los parámetros indicados.
    /// </summary>
    /// <param name="parametros"></param>
    public List<clsBusquedaDeclaracion> BuscarDeclaracion(clsBusquedaDeclaracion parametros, string tipoParams)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        if (!Seguridad.CredencialesValidas(tipoParams)) return null;

        // Retorno de resultados.
        var Resultado = new List<clsBusquedaDeclaracion>();

        Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();

        Resultado = process.BuscarDeclaracion(parametros);

        return Resultado;
    }

    /// <summary>
    /// Carga y retorna la declaración indicada.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipoDeclaracion"></param>
    /// <returns></returns>
    public clsDeclaracion ObtenerDeclaracion(int id, string tipoDeclaracion)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        if (!Seguridad.CredencialesValidas(tipoDeclaracion)) return null;
        clsDeclaracion Resultado = null;
        try
        {
            Ruv.Business.Captura.Procesos Pro = new Ruv.Business.Captura.Procesos();
            Resultado = Pro.ObtenerDeclaracion(id);
            string errorFile = string.Empty;
            string nombreArchivo = string.Empty;
            CriticaNService objCritica = new CriticaNService();
            if (!Resultado.RadicacionId.HasValue)
            {
                RegistroTraza.I.Registrar(string.Format("La declaración solicitada ({0}) no contiene información de radicación.", "Declaracion ID: " + id));
                throw new InvalidOperationException(string.Format("La declaración solicitada ({0}) no contiene información de radicación.", "Declaracion ID: " + id));
            }
            Resultado.DocumentoDigital = objCritica.ObtenerImagenRadicacion(Resultado.RadicacionId.Value, ref nombreArchivo, ref errorFile);
            Resultado.DocumentoAnexo = ObtenerDeclaracionXPS(Resultado.RadicacionId.Value);
            
            if (Resultado.DocumentoDigital != null)
                Resultado.DocumentoDigitalNombre = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(nombreArchivo);
            else
                //clsLog.Registrar(new InvalidOperationException(string.Format("No se pudo encontrar el documento digital asociado a la radicación solicitada ({0}).", "Radicacion ID: " + Resultado.RadicacionId.Value))); // Registrar el hecho que no hay imagen
                RegistroTraza.I.Registrar(new InvalidOperationException(string.Format("No se pudo encontrar el documento digital asociado a la radicación solicitada ({0}).", "Radicacion ID: " + Resultado.RadicacionId.Value))); // Registrar el hecho que no hay imagen
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
            throw ex;
        }

        return Resultado;
    }

    #endregion

    #region ALMACENAR UNA RADICACIÓN

    public decimal RadicacionAlmacenar(clsRadicacion radicacion)
    {
        return new Ruv.Business.Captura.GuardarDatos().ObtenerRadicacion(radicacion);
    }

    public decimal GuardarRadicacion(clsRadicacion radicacion)
    {
        return new Ruv.Business.Captura.GuardarDatos().GuardarRadicacion(radicacion);
    }

    public Boolean ActualizarRadicacion(clsRadicacion radicacion)
    {
        return new Ruv.Business.Captura.GuardarDatos().ActualizarRadicacion(radicacion);
    }

    public bool CargarImagen(byte[] imageData, string fileName)
    {
        grb::Procesos Pro = new grb::Procesos();
        return Pro.CargarPdf(imageData, fileName);
    }

    public bool CargarPdf(byte[] fileData, string fileName)
    {
        grb::Procesos Pro = new grb::Procesos();
        return Pro.CargarPdf(fileData, fileName);
    }

    private byte[] ObtenerDeclaracionXPS(int idRadicacion)
    {
        string extension = ".zip";
        string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosRadicacion"];
        string[] files = Directory.GetFiles(path, string.Format("{0}-XPS{1}", idRadicacion.ToString(), extension));

        if (files.Length > 0) return File.ReadAllBytes(files[0]);
        return null;
    }

    #endregion

    #region Lista Tareas

    public List<clsListaTareas> ObtenerListaTareas(int idUsuario, string tipoParams, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario, int? PageNumber, int? PageSize)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        if (!Seguridad.CredencialesValidas(tipoParams)) return null;
        // El usuario actual es: Seguridad.Usuario;

        List<clsListaTareas> Resultado = new List<clsListaTareas>();

        try
        {
            Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
            List<clsListaTareas> Listatareas = new List<clsListaTareas>();
            Listatareas = process.ObtenerListaTareas(idUsuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario, PageNumber, PageSize);

            return Listatareas;

        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }

        return Resultado;
    }

    public List<clsListaTareas> ObtenerListaTareasPaginado(int idUsuario, string tipoParams, int startRow, int pageSize, string sortColumns, string filterEx)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        //if (!Seguridad.CredencialesValidas(tipoParams)) return null;
        // El usuario actual es: Seguridad.Usuario;

        List<clsListaTareas> Listatareas = new List<clsListaTareas>();

        try
        {
            Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
            Listatareas = process.ObtenerListaTareasPaginado(idUsuario, startRow, pageSize, sortColumns, filterEx);
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }
        return Listatareas;
    }

    public int ObtenerListaTareasCantidad(int idUsuario)
    {
        int cantidad = 0;
        try
        {
            Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
            cantidad = process.ObtenerListaTareasCantidad(idUsuario);
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }
        return cantidad;
    }
    /// <summary>
    /// Obtiene Cantidad de resgistros de Lista Tareas WPF.
    /// </summary>
    /// <returns></returns>
    public int ObtenerListaTareasWPFCantidad(int idUsuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario)
    {
        int cantidad = 0;
        try
        {
            Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
            cantidad = process.ObtenerListaTareasWPFCantidad(idUsuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario);
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }
        return cantidad;
    }

    /// <summary>
    /// Recibe una declaración y la graba en la base de datos.
    /// </summary>
    /// <param name="declaracion"></param>
    /// <returns></returns>
    public clsResultado RadicacionActualizarEstado(int idRadicacion, int param_estado, string tipoParams, int idDeclaracion)
    {
        clsSeguridad Seguridad = new clsSeguridad();
        if (!Seguridad.CredencialesValidas(tipoParams)) return null;
        // El usuario actual es: Seguridad.Usuario;

        var Resultado = new clsResultado();

        try
        {
            Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
            process.RadicacionActualizarEstado(idRadicacion, param_estado);

            Resultado.ErroresDB = process.Errores;
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            Resultado.ErroresDB.Add("Error al actualizar el estado de la radicación: " + ex.Message);
        }

        return Resultado;
    }

    #endregion

    #region  OPERACIONES PARA MANEJO DE GLOSAS E INTENCIONES DE GLOSAS

    public ObservableCollection<clsGlosa> getGlosasxDeclaracion(clsDeclaracion laDeclaracion)
    {
        return new Ruv.Business.Captura.GestionGlosas().ObtenerGlosasxDec(laDeclaracion);
    }
    public ObservableCollection<clsGlosaIntencion> getIGlosasxDeclaracion(clsDeclaracion laDeclaracion)
    {
        return new Ruv.Business.Captura.GestionGlosas().ObtenerInGlosasxDec(laDeclaracion);
    }
    public clsGlosa setGlosas(clsGlosa myGlosa)
    {
        return null;
      //return new Ruv.Business.Captura.GestionGlosas().InsertarGlosa(myGlosa);
    }
    public clsGlosaIntencion setIntenGlosas(clsGlosaIntencion myIntencionGlosa)
    {
        return null;
      //return new Ruv.Business.Captura.GestionGlosas().InsertarIntencionGlosa(myIntencionGlosa);
    }


    #endregion

    #region Grabar Archivo

    /// <summary>
    /// Grabar el archivo binario en el servidor
    /// </summary>
    /// <param name="archivo"></param>
    /// <param name="nombreArchivo"></param>
    protected string guardaArchivo(byte[] archivo, string nombreArchivo)
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosDeclaracion"];
        string file = nombreArchivo;

        path = path + file;

        //Si no existe el archivo se guarda en el servidor
        //Si ya existe se pasa por alto y no se hace nada
        if (!System.IO.File.Exists(path))
        {

            try
            {
                FileStream archivo_fisico = new FileStream(path, FileMode.Create, FileAccess.Write);
                foreach (byte b in archivo)
                {
                    archivo_fisico.WriteByte(b);
                }
                archivo_fisico.Close();
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(ex);
                return ex.Message;
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// Grabar la declaración en el servidor
    /// </summary>
    void GrabarPrimeraVersion(clsDeclaracion Declaracion, int idDeclaracion)
    {
        clsUtil objUtil = new clsUtil();

        string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosDeclaracion"] + "PrimeraVersion\\Declaracion " + idDeclaracion.ToString() + ".tmp";
        objUtil.GrabarArchivoSerializado<clsDeclaracion>(
            path,
            Declaracion,
            ClaveZip,
            true);
    }

    #endregion

    #region Actualizar Estado Declaracion

    public clsResultado ActualizarEstadoDeclaracion(clsDeclaracion declaracion)
    {
        var Resultado = new clsResultado();
        Ruv.Business.Captura.Procesos process = new Ruv.Business.Captura.Procesos();
        try
        {
            process.ActualizarEstadoDeclaracion(declaracion);
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            Resultado.ErroresDB.Add("Ocurrio un error al actulizar el estado de la declaracion: " + ex.Message);
        }
        return Resultado;
    }

    #endregion
    
    #region Geografia

    public List<clsGeografiaCompleta> ObtenerGeografiaCompleta(ref string cError) {
        var GeneralBus = new GeografiasBusiness();
        return GeneralBus.ObtenerGeografiaCompleta(ref cError);
    }

    public List<clsGeografiaCompleta> ObtenerPaises(ref string cError) {
        var GeneralBus = new GeografiasBusiness();
        return GeneralBus.ObtenerPaises(ref cError);
    }

    public List<clsGeografiaCompleta> ObtenerDepartamentosPorPais(int idPais, ref string cError) {
        var GeneralBus = new GeografiasBusiness();
        return GeneralBus.ObtenerDepartamentosPorPais(idPais, ref cError);
    }

    public List<clsGeografiaCompleta> ObtenerMunicipiosPorDepartamento(int idDepartamento, ref string cError) {
        var GeneralBus = new GeografiasBusiness();
        return GeneralBus.ObtenerMunicipiosPorDepartamento(idDepartamento, ref cError);
    }

    public List<clsEntidadMunicipio> ObtenerEntidadesPorMunicipio(int idMunicipio, ref string cError) {
        var GeneralBus = new GeografiasBusiness();
        return GeneralBus.ObtenerEntidadesPorMunicipio(idMunicipio, ref cError);
    }

    public List<clsPuntoNotificacion> ObtenerPuntosAtencionyDTPorMunicipio(int idMunicipio) {
        return new GeografiasBusiness().ObtenerPuntosAtencionyDTPorMunicipio(idMunicipio);
    }

    /// <summary>
    /// Consulta que retorna la dirección del punto de notificación
    /// </summary>
    /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
    /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
    /// <returns>Dirección del punto de notificación</returns>
    /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
    public string ObtenerDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto)
    {
        return new GeografiasBusiness().ObtenerDireccionPuntoNotificacion(idPuntoNotificacion, tipoPunto);
    }

    /// <summary>
    /// Procedimiento que actualiza la dirección del punto de notificación
    /// </summary>
    /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
    /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
    /// <param name="direccion">Nueva dirección del punto de notificación</param>
    /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
    public void ActualizarDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto, string direccion, ref string cError)
    { 
        new GeografiasBusiness().ActualizarDireccionPuntoNotificacion(idPuntoNotificacion, tipoPunto, direccion, ref cError);
    }

    #endregion Geografia


    public List<clsParametroGeneral> ObtenerParametros(int tipoParametro, ref string cError)
    {
        return new ParametrosBusiness().ObtenerParametros(tipoParametro, ref cError);
    }
}

