using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Seguridad
{
    /// <summary>
    /// Ejecuta acciones la primera vez que se lanza una versión específica.
    /// </summary>
    class clsPreEjecucion
    {
        string SubLlave = "DAPS";
        int NumeroMarca = 8;

        public void Ejecutar()
        {
            // 1) Verificar las condiciones.
            if (!HayQueEjecutar()) return;

            // 2) Ejecutar las tareas.
            Tarea02();

            // 3) Poner la marca.
            PonerMarca();
        }

        /// <summary>
        /// True: La tarea de pre-ejecución debe ejecutarse.
        /// </summary>
        /// <returns></returns>
        bool HayQueEjecutar()
        {
            // 1) Verificar las condiciones.
            RegistryKey HKCU = Registry.CurrentUser;
            RegistryKey DAPS = HKCU.OpenSubKey("Software").OpenSubKey(SubLlave);
            //bool HayUsuarios = Sipod.I.LocalDB.Query<Ruv.Infrastructure.Crosscutting.Common.clsUsuario, string>().Any();
            string Verificacion = null;

            if (DAPS != null)
            {
                Verificacion = Convert.ToString(DAPS.GetValue(
                  string.Format("PreEjecucion{0:D2}", NumeroMarca), null));
            }
            else
            {
                // Crear la subkey, que no existe.
                HKCU.OpenSubKey("Software", true).CreateSubKey(SubLlave);
            }

            return Verificacion != "1"; //&& HayUsuarios;
        }

        /// <summary>
        /// La tarea de pre-ejecución:
        /// Limpiar completamente la carpeta de la aplicación.
        /// </summary>
        void Tarea02()
        {
            RUV.I.Log.Registrar("Inicio tarea limpieza carpeta temporal");

            // Borrar todo el contenido de la carpeta temporal
            if (!Directory.Exists(RUV.I.Util.RutaArchivosLocales))
                return;

            // Borrar los archivos.

            // Evitar que se borre el archivo de borrador si existe el archivo de configuración.
            string[] PreservarArchivos = null;
            if (File.Exists(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "RUVConfig.txt")))
            {
                PreservarArchivos = new string[] { "DeclaracionBorrador.tmp" };
            }

            foreach (var UnArchivo in Directory.GetFiles(RUV.I.Util.RutaArchivosLocales))
                if (PreservarArchivos == null ||
                  !PreservarArchivos.Any(x => x.ToLower() == Path.GetFileName(UnArchivo).ToLower()))
                    File.Delete(UnArchivo);

            // Borrar las carpetas
            foreach (var UnArchivo in Directory.GetDirectories(RUV.I.Util.RutaArchivosLocales))
                if (!UnArchivo.EndsWith("Log"))
                    Directory.Delete(UnArchivo, true);


            RUV.I.Log.Registrar("Fin tarea limpieza carpeta temporal");
        }

        /// <summary>
        /// Poner la marca de ejecución del pre-proceso.
        /// </summary>
        void PonerMarca()
        {
            Registry.CurrentUser
              .OpenSubKey("Software").OpenSubKey(SubLlave, true)
              .SetValue(string.Format("PreEjecucion{0:D2}", NumeroMarca), "1");
        }

        #region TAREAS

        /// <summary>
        /// La tarea de pre-ejecución.
        /// </summary>
        void Tarea01()
        {

            // Deshabilitar el switch de encriptacion.
            RUV.I.Configuraciones.ConfiguracionGeneral.SiempreEncriptarContraseña = false;

            // Cargar la lista de usuarios en memoria.
            var ListaUsuarios =
              RUV.I.LocalDB.Query<Ruv.Infrastructure.Crosscutting.Common.clsUsuario, string>()
              .Where(x => x.Key != null).Select(x => x.LazyValue.Value).ToList
              ();

            // Encriptar usuario y clave.
            ListaUsuarios.ForEach(x =>
              x.Contraseña = RUV.I.Seguridad.Crypto.EncryptStringFixed(x.Contraseña));

            // Truncar la tabla.
            RUV.I.LocalDB.Truncate(typeof(clsUsuario));

            // Grabar los usuarios en disco.
            ListaUsuarios.ForEach(x => RUV.I.LocalDB.Save<Ruv.Infrastructure.Crosscutting.Common.clsUsuario>(x));
            RUV.I.LocalDB.Flush();

            // Habilitar el switch.
            RUV.I.Configuraciones.ConfiguracionGeneral.SiempreEncriptarContraseña = true;
        }


        #endregion
    }
}
