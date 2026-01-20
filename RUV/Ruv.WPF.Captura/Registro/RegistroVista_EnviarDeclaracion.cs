using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common;
using System.IO;
using ServiceStack.Text;

namespace Ruv.WPF.Captura.Registro
{

    public partial class RegistroVista : Page
    {
        // Rutinas para transmisión de una declaración.
        GeneralService.clsResultado ResultadoEnvio = null;



        /// <summary>
        /// Enviar la declaración actual para registro.
        /// </summary>
        /// <param name="declaracion"></param>
        void EnviarDeclaracionAsync(clsDeclaracion declaracion)
        {
            declaracion.UsuarioId = RUV.I.Usuario.Id;

            if (!RUV.I.Configuraciones.ConfiguracionGeneral.OmitirValidacionesAlEnviar)
            {
                var pasaValidacion = RUV.I.Util.ValidarDeclaracion(declaracion);
                if (pasaValidacion == eResultadoValidacion.NoPasaValidaciones)
                    return;
                else if (pasaValidacion == eResultadoValidacion.PasaGlosa)
                    declaracion.PendienteGlosas = true;
                else
                    declaracion.PendienteGlosas = false;
            }
            CompletarInformacionDeFuncionario(declaracion);

            if (RUV.I.Configuraciones.ConfiguracionGeneral.TransmitirSinUsarColaDeprocesos)
            {
                // Hacer la declaración directamente sin pasar por la cola de procesos.
                EnviarDeclaracionDirecto(declaracion);
            }
            else
            {
                // Hacer la transmisión en la cola 
                // y pasar automáticamente a la cola de procesos.
                //if (string.IsNullOrWhiteSpace(declaracion.DeclaracionNumero))
                //    declaracion.EstadoDeclaracion =
                //      Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.FinalizaCapturaSinRadicar;
                //Jhon  20/02/2014 Por Inci GLO1 GLOSAS se comenta Codigo para enviarlo a cola de procesos
                //jh if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.validar_Enmendar_corregir_declaración))
                //jh {
                //jh    EnviarDeclaracionDirecto(declaracion);
                //jh
                //jhelse
                //jh{
                    RUV.I.ColaProcesos.AgregarProceso(declaracion);
                    RUV.I.UIPrincipal.NavegarA("ListaTareas/ColaProcesos");
                //jh}

            }

            // Borrar de la carpeta raiz el borrador.
            if (HayBorradorCargado
              && !RUV.I.Configuraciones.ConfiguracionGeneral.PreservarBorradorDespuesDeEnvio)
            {
                string ArchivoBorrador = Path.Combine(RUV.I.Util.RutaArchivosLocales, "DeclaracionBorrador.tmp");
                if (File.Exists(ArchivoBorrador))
                    File.Delete(ArchivoBorrador);

                HayBorradorCargado = false;
            }
        }

        /// <summary>
        /// Envía directamente la declaración sin pasar por la cola de procesos.
        /// </summary>
        /// <param name="declaracion"></param> 
        private void EnviarDeclaracionDirecto(clsDeclaracion declaracion)
        {
            RUV.I.UIPrincipal.BloquearInterfase = "Enviando";

            RUV.I.MultiTarea.EjecutarEnBackground(
              new Action(() =>
              {
                  ResultadoEnvio = null;
                  try
                  {
                      if (RUV.I.DeclaracionActual.DocumentoDigitalNombre == null)
                          throw new InvalidOperationException("No existe adjunto");

                      var ArchivoAdjunto =
                        System.IO.Path.Combine(
                        RUV.I.Util.RutaArchivosLocales,
                        RUV.I.DeclaracionActual.DocumentoDigitalNombre);
                      declaracion.DocumentoDigital = RUV.I.Util.CargarArchivo(ArchivoAdjunto);

                      ResultadoEnvio = EnviarDeclaracion(declaracion);

                      declaracion.DocumentoDigital = null;
                  }
                  catch (Exception ex)
                  {
                      ResultadoEnvio = new GeneralService.clsResultado();
                      ResultadoEnvio.ErroresDB = new string[1];
                      ResultadoEnvio.ErroresDB[0] = ex.Message;
                      RUV.I.Log.Registrar("EnviarDeclaracion", ex);
                  }
              }),
              new Action(() =>
              {
                  RUV.I.UIPrincipal.BloquearInterfase = null;
                  MostrarResumenEnvio();
              }
              ));
        }

        /// <summary>
        /// Prepara la declaración y la envía al servicio.
        /// </summary>
        Ruv.WPF.Captura.GeneralService.clsResultado EnviarDeclaracion(clsDeclaracion declaracion)
        {
            Ruv.WPF.Captura.GeneralService.clsResultado Resultado = null;

            //Adjuntar el archivo.
            if (!string.IsNullOrWhiteSpace(declaracion.DocumentoDigitalNombre))
            {
                try
                {
                    declaracion.DocumentoDigital = RUV.I.Util.CargarArchivo(
                      System.IO.Path.Combine(
                      RUV.I.Util.RutaArchivosLocales, declaracion.DocumentoDigitalNombre));

                }
                catch (Exception ex)
                {
                    string Mensaje = "No se pudo accesar el archivo escaneado.\n" + ex.Message;
                    throw new Exception(Mensaje);
                }
            }

            try
            {
                declaracion.IdValoracion = RUV.I.IdValoracion;
                var objetoSerializado = JsonSerializer.SerializeToString<clsDeclaracion>(declaracion);
                Resultado = RUV.I.Red.ServicioGeneral.DeclaracionAlmacenar(declaracion,
                  RUV.I.Seguridad.LlaveUsuario, RUV.I.Usuario);
            }
            catch (Exception ex)
            {
                string Mensaje = "No se pudo realizar la transmisión.\n" + ex.Message;
                throw new Exception(Mensaje);
            }

            return Resultado;
        }

        /// <summary>
        /// Muestra al usuario el resultado de la transmisión.
        /// </summary>
        void MostrarResumenEnvio()
        {
            if (ResultadoEnvio == null ||
                (
                  (ResultadoEnvio.AdvertenciasDB == null || !ResultadoEnvio.AdvertenciasDB.Any())
                  &&
                  (ResultadoEnvio.ErroresDB == null || !ResultadoEnvio.ErroresDB.Any())
                )
              )
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Transmisión exitosa");
                return;
            }
            else
            {
                var Ventana = new Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion(ResultadoEnvio);
                Ventana.ShowDialog();
            }
        }

        /// <summary>
        /// Agrega a la declaración la "firma" del funcionario, si hiciese falta.
        /// </summary>
        /// <param name="declaracion"></param>
        void CompletarInformacionDeFuncionario(clsDeclaracion declaracion)
        {
            // Completar alguna información si hace falta.
            if (string.IsNullOrWhiteSpace(declaracion.VerificacionProcedimiento.FuncionarioNombre)
                && RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Firma_funcionario_declaracion))
            {
                var VP = declaracion.VerificacionProcedimiento;
                VP.FuncionarioNombre = RUV.I.Usuario.Nombre;
                // El número del documento está cifrado.
                if (!string.IsNullOrWhiteSpace(RUV.I.Usuario.NumeroDocumento))
                    VP.FuncionarioDocumentoIdentidad =
                      RUV.I.Seguridad.Crypto.DecryptStringFixed(RUV.I.Usuario.NumeroDocumento);
                VP.FuncionarioCargo = RUV.I.Usuario.Cargo;
            }
        }

    }
}
