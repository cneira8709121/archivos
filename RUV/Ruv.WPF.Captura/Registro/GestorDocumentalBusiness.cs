using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.IO;

namespace Ruv.WPF.Captura.Registro
{
    class GestorDocumentalBusiness
    {

        public int aplicacionID { get; set; } = 3; //RUV
        public int tipDesrem { get; set; } = 1; //Set as default value..
        public string Direccion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Entidad { get; set; } = "";
        public string descAnexo { get; set; } = "";
        public string Asunto { get; set; } = "";
        public string Email { get; set; } = "";
        public string codDependencia { get; set; } = "111510";
        public string NroRadGDocumental { get; set; } = "";


        //Servicio Gestor Documental Fachada;
        FachadaGestorDocumental.ServicioGestorDocumentalClient  clienteGestorDocumental = new FachadaGestorDocumental.ServicioGestorDocumentalClient("BasicHttpBinding_IServicioGestorDocumental1");
        FachadaGestorDocumental.Credencial credenciales = new FachadaGestorDocumental.Credencial() { UsuarioWsFachada = "usrGestorUARIV", ContrasenaWsFachada = "csgduplaialv16*" };
        FachadaGestorDocumental.attachResponse attachresp = new FachadaGestorDocumental.attachResponse();
        FachadaGestorDocumental.status statusGestor = new FachadaGestorDocumental.status();
        FachadaGestorDocumental.radicadoResponse radResponse = new FachadaGestorDocumental.radicadoResponse();
        FachadaGestorDocumental.documento documentoradicado = new FachadaGestorDocumental.documento();
        FachadaGestorDocumental.b64File b64file = new FachadaGestorDocumental.b64File();

        //Servicio de Comunicacion RUV y SIRAV, guarda el radicado de entrada y el No de FUD. 
        WSComunicacionSiravRuv.ComunicacionSiravRuvClient objclient = new WSComunicacionSiravRuv.ComunicacionSiravRuvClient("BasicHttpBinding_IComunicacionSiravRuv");
        WSComunicacionSiravRuv.RadicadoSirav objRad = new WSComunicacionSiravRuv.RadicadoSirav();

        public int CrearRadicadoDeEntrada(clsRadicacion DatoRadicacion, string msg)
        {
            string numradicado = String.Empty;
            string nombreArchivo = String.Empty;
            string extensionArchivo = String.Empty;
            string archivoBase64str = String.Empty;
            string msgRadEntrada = String.Empty;
            string msgAttachDoc = String.Empty;
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            int successprocess = 0;
            byte[] archivoBinario = null;
            if (!DatoRadicacion.RUTAIMAGEN.Equals(""))
            {
                nombreArchivo = Path.GetFileNameWithoutExtension(DatoRadicacion.RUTAIMAGEN.ToString());
                extensionArchivo = Path.GetExtension(DatoRadicacion.RUTAIMAGEN.ToString());
                archivoBinario = File.ReadAllBytes(DatoRadicacion.RUTAIMAGEN.ToString()).ToArray();
                archivoBase64str = Convert.ToBase64String(archivoBinario);
            }


            Asunto = DatoRadicacion.NRO_FORMULARIO + "-" + Fecha + "-SVR";
            Direccion = String.IsNullOrEmpty(Direccion) ? "." : Direccion = Direccion;
            Telefono = String.IsNullOrEmpty(Telefono) ? "." : Telefono = Telefono;
            try
            {
                statusGestor = clienteGestorDocumental.radicarDeEntrada(
                    credenciales,
                    aplicacionID,
                    tipDesrem,
                    DatoRadicacion.PrimerNombre + " " + DatoRadicacion.SegundoNombre,
                    DatoRadicacion.PrimerApellido,
                    DatoRadicacion.SegundoApellido,
                    DatoRadicacion.NumeroDocumento,
                    Direccion,
                    Telefono,
                    Entidad,
                    Convert.ToInt32(DatoRadicacion.ID_PAIS.ToString()),
                    Convert.ToInt32(DatoRadicacion.ID_DEPARTAMENTO.ToString()),
                    Convert.ToInt32(DatoRadicacion.ID_MUNICIPIO.ToString()),
                    Email,
                    codDependencia,
                    descAnexo,
                    Asunto);

                //Almacena el Número de Radicado eSigma y Adjunta del FUD cargado desde RUV
                if (Int32.Parse(statusGestor.errorCodeField.ToString()) == 0)
                {
                    numradicado = statusGestor.messageField.ToString();
                    NroRadGDocumental = numradicado.ToString();
                    successprocess = 1;
                    //Adjunta el archivo creado desde toma en linea..
                    attachresp = clienteGestorDocumental.attachDocument(credenciales, numradicado, nombreArchivo, archivoBase64str, 1);
                    objRad = objclient.GuardarRadicadoEntradaBPM(statusGestor.messageField.ToString(), DatoRadicacion.NRO_FORMULARIO.ToString(), Fecha, 1, -1);
                }

            }
            catch (Exception e)
            {
                
                msgRadEntrada = statusGestor.codeField.ToString();
                msgAttachDoc = attachresp.codeField.ToString();
                msg = String.Format("Excepcion en el metodo: {0} Excepcion en el Radicado eSigma {1} Excepcion Adjuntando el FUD: {2}", e.Message.ToString(), msgRadEntrada, msgAttachDoc);
                successprocess = 0;
            }

            return successprocess;
        }



    }
}
