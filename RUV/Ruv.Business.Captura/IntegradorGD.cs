using Ionic.Zip;
using Ruv.Business.DTO.Radicacion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace Ruv.Business.Captura
{
    /// <summary>
    /// Clase que construye la entidad del gestor documental e invoca la integración
    /// </summary>
    public class IntegradorGD
    {

        /// <summary>
        /// Metodo que construye el objeto de entidad que requiere el integrador de Gestión Documental
        /// </summary>
        /// <param name="declaracion">Entidad Declaración</param>
        /// <param name="tipoAnexo">Tipo de Anexo</param>
        /// <param name="idUsuario">Identificador del Usuario</param>
        /// <param name="tra">Transacción de la declaración</param>
        /// <param name="cError">Mensaje de error en caso de Falla</param>
        public static void GuardarRadicacion(clsDeclaracion declaracion, string tipoAnexo, int idUsuario, DbTransaction tra, ref string cError)
        {
            Ruv.Data.Radicacion.IntegradorGDData datos = new Data.Radicacion.IntegradorGDData();

            clsRadicacionIntegradorGD rad = new clsRadicacionIntegradorGD();
            var declarante = declaracion.PersonasAfectadas.ListaPersonas.Where(x => x.ID == declaracion.TomaDeclaracion.DeclaranteId).First();

            string tmpPathXPS = Path.Combine(Path.GetTempPath(), $"{declaracion.DeclaracionNumero}-XPS.zip");
            string tmpPathOtros = Path.Combine(Path.GetTempPath(), $"{declaracion.DeclaracionNumero}-{declaracion.DocumentoDigitalNombre}");
            string tmpPathZIP = Path.Combine(Path.GetTempPath(), $"{declaracion.DeclaracionNumero}.zip");

            File.WriteAllBytes(tmpPathXPS, declaracion.DocumentoAnexo);

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                if (declaracion.DocumentoAnexo != null)
                {
                    File.WriteAllBytes(tmpPathXPS, declaracion.DocumentoAnexo);
                    zip.AddFile(tmpPathXPS);
                }
                if(declaracion.DocumentoDigital != null)
                {
                    File.WriteAllBytes(tmpPathOtros, declaracion.DocumentoDigital);
                    zip.AddFile(tmpPathOtros);
                }
                zip.Save(tmpPathZIP);
            }

            rad.ID_USUARIO = idUsuario;
            rad.ARCHIVO = Convert.ToBase64String(File.ReadAllBytes(tmpPathZIP));
            rad.NOMBRE_ARCHIVO = Path.GetFileName(tmpPathZIP);
            rad.NOMBRE = string.Format("{0} {1}", declarante.PrimerNombre, declarante.SegundoNombre);
            rad.PRIMER_APELIIDO = declarante.PrimerApellido;
            rad.SEGUNDO_APELLIDO = declarante.SegundoApellido;
            rad.CEDULA = declarante.NumeroDocumento;
            rad.NUM_DECLARACION = declaracion.DeclaracionNumero;
            rad.PAIS = Convert.ToInt32(declaracion.TomaDeclaracion.LugarDeclaracionPais.Value);
            rad.DEPARTAMENTO = Convert.ToInt32(declaracion.TomaDeclaracion.LugarDeclaracionDepartamento.Value);
            rad.MUNICIPIO = Convert.ToInt32(declaracion.TomaDeclaracion.LugarDeclaracionMunicipio.Value);
            rad.CORREO = declaracion.TomaDeclaracion.DatoContactoCorreoElectronico;
            rad.TELEFONO = (!string.IsNullOrEmpty(declaracion.TomaDeclaracion.DatoContactoTelefonoFijo))? declaracion.TomaDeclaracion.DatoContactoTelefonoFijo : "SIN TELEFONO";
            rad.DIRECCION = declaracion.TomaDeclaracion.DatoContactoDireccion;
            rad.ID_DECLARACION = declaracion.ID.Value;
            rad.DESCRIPCION_ANEXO = tipoAnexo;
            datos.RadicarEntrada(rad, tra, ref cError);
        }

        public static void GuardarLogIntegracionSGD(string numDeclaracion, string codIntegradorGD, string numExpedienteGD, int? idExpedienteGD, int idUsuario, DbTransaction tra, ref string cError)
        {
            Ruv.Data.Radicacion.IntegradorGDData datos = new Data.Radicacion.IntegradorGDData();
            if(!datos.ActualizarLogIntegrador(numDeclaracion, 0, codIntegradorGD, numExpedienteGD, idExpedienteGD, idUsuario, tra, ref cError))
                datos.GuardarLogIntegrador(numDeclaracion, 0, codIntegradorGD, numExpedienteGD, idExpedienteGD, idUsuario, tra, ref cError);
        }
    }
}
