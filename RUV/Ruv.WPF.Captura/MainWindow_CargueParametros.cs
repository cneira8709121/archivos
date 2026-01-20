using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace Ruv.WPF.Captura
{
    /// <summary>
    ///  Rutinas para forzar el re-cargue de los parámetros fuera de línea.
    /// </summary>
    public partial class MainWindow : Window
    {
        bool DescargaExitosa = false;
        string CarpetaTemporal;

        /// <summary>
        /// Forzar el re-cargue de los parámetros.
        /// </summary>
        void ForzarCargueParametros()
        {
            // 0) Pedir confirmación.
            if (RUV.I.Red.EstadoRed != eEstadoRed.Disponible)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "No se puede realizar el cargue de los parámetros\nmientras que no exista conexión.");
                return;
            }

            if (!RUV.I.UIPrincipal.UsuarioConfirmar(
              "Recuerde salvar o finalizar cualquier trabajo que este realizando\n antes de proceder con esta función.\n¿Cargar los parámetros?"))
                return;

            BloquearInterfase = "Cargando parámetros";
            DescargaExitosa = true;
            CarpetaTemporal = Path.Combine(RUV.I.Util.RutaArchivosLocales, Guid.NewGuid().ToString());

            try
            {
                // 1) Respaldar los actuales.
                Directory.CreateDirectory(CarpetaTemporal);

                var Directorio = new DirectoryInfo(RUV.I.Util.RutaArchivosLocales);
                foreach (var UnArchivo in Directorio.GetFiles("*.dat"))
                {
                    UnArchivo.MoveTo(Path.Combine(CarpetaTemporal, UnArchivo.Name));
                }

                // 2) Tratar de obtener los parámetros.
                RUV.I.InfoGeneral.DescargaInformacionCompleted -= ReCargaParametros_Completed;
                RUV.I.InfoGeneral.DescargaInformacionCompleted += ReCargaParametros_Completed;
                RUV.I.InfoGeneral.Descargar();
            }
            catch (Exception ex)
            {
                DescargaExitosa = false;
                RUV.I.Log.Registrar("ForzarCargueParametros", ex);
            }

        }

        void ReCargaParametros_Completed(object sender, EventArgs e)
        {

            if (DescargaExitosa)
            {
                // 3) Si todo salió bien, volver al pantallazo vacío y borrar el respaldo.
                Directory.Delete(CarpetaTemporal, true);
                RUV.I.InfoGeneral.PrecargarParametros();
                BloquearInterfase = null;
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Descarga exitosa");
            }
            else
            {
                // 4) Si algo salió mal devolver todo como estaba.
                if (Directory.Exists(CarpetaTemporal))
                {
                    var Directorio = new DirectoryInfo(CarpetaTemporal);
                    foreach (var UnArchivo in Directorio.GetFiles())
                    {
                        UnArchivo.MoveTo(Path.Combine(RUV.I.Util.RutaArchivosLocales, UnArchivo.Name));
                    }
                    Directory.Delete(CarpetaTemporal);
                }
                BloquearInterfase = null;
                RUV.I.UIPrincipal.ReportarErrorDeUsuario(
                  "No fué posible descargar los parámetros\npor favor inténtelo más tarde");
            }
        }
    }
}