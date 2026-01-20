using Wintellect.Sterling;

namespace Ruv.WPF.Captura.Infrastructure.LocalStorage
{
    public static class DatabaseService
    {
        private static SterlingEngine _engine;
        private static ISterlingDatabaseInstance _databaseInstance;

        public static void Start()
        {
            _engine = new SterlingEngine();
            // Registrar los serializadores.
            //_engine.SterlingDatabase.RegisterSerializer<clsSerializador>();
            _engine.Activate();

            var folder = RUV.I.Util.RutaArchivosLocales;
            var driver = new Wintellect.Sterling.Server.FileSystem.FileSystemDriver(folder);
            _databaseInstance = _engine.SterlingDatabase.RegisterDatabase<SterlingDatabaseInstance>(driver);

        }

        public static ISterlingDatabaseInstance Current
        {
            get { return _databaseInstance; }
        }

        public static void Stop()
        {
            _engine.Dispose();
        }

    }
}