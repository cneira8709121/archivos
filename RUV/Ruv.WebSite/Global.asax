<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {

        ///string ruta = AppDomain.CurrentDomain.BaseDirectory;
        List<string> Carpetas = new List<string>();

        string CarpetaRadicacion = System.Configuration.ConfigurationManager.AppSettings["PathArchivosRadicacion"];
        string CarpetaDeclaracion = System.Configuration.ConfigurationManager.AppSettings["PathArchivosDeclaracion"];
        string CarpetaPrimeraVersion = System.Configuration.ConfigurationManager.AppSettings["PathArchivosDeclaracion"] + "PrimeraVersion";
        string CarpetaActosAdmin = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
        string CarpetaCorrecciones = System.Configuration.ConfigurationManager.AppSettings["PathArchivosCorrecciones"];
        string CarpetaNotificaciones = System.Configuration.ConfigurationManager.AppSettings["PathArchivosNotificaciones"];
        Carpetas.Add(CarpetaRadicacion);
        Carpetas.Add(CarpetaDeclaracion);
        Carpetas.Add(CarpetaPrimeraVersion);
        Carpetas.Add(CarpetaActosAdmin);
        Carpetas.Add(CarpetaCorrecciones);
        Carpetas.Add(CarpetaNotificaciones);

        foreach (string carpeta in Carpetas)
        {
            if (!System.IO.Directory.Exists(carpeta))
            {
                System.IO.Directory.CreateDirectory(carpeta);
            }
        }

        RegistroTraza.I.Registrar("Inicio de clase de registro de trazas");
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta cuando se cierra la aplicación
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta al producirse un error no controlado
        Exception oException = Server.GetLastError();

        StringBuilder oTexto = new StringBuilder("RUV\n");
        int Level = 0;

        while (oException != null)
        {
            oTexto.AppendFormat("{0}-EXCEPCION: {1}\n", Level, oException.Message);
            oTexto.AppendFormat("{0}-STACK: {1}\n\n", Level++, oException.StackTrace);
            Elmah.ErrorSignal.FromCurrentContext().Raise(oException);
            oException = oException.InnerException;
        }
        try
        {
            //clsLog Log = new clsLog();
            //Log.Registrar(oTexto.ToString());
            RegistroTraza.I.Registrar(oTexto.ToString());
            Session[ConstantesItems.ERROR] = oTexto.ToString();
        }
        catch { }

        try
        {
            System.Diagnostics.EventLog.WriteEntry("RUV",
              oTexto.ToString(),
              System.Diagnostics.EventLogEntryType.Error);
        }
        catch { }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se inicia una nueva sesión
        Session["DUMMY"] = 1969;

    }

    void Session_End(object sender, EventArgs e)
    {
    }
       
</script>
