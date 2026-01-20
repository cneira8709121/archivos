using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Ruv.Business.DTO.IdentidadPersona;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Ruv.Data.General
{
    public class entMensajeSMS
    {
        private string URL = ConfigurationManager.AppSettings["ServiciosOTI"];
        public entMensajeSMS()
        {

        }
        public bool EnviarSMS(clsMensajeSMS mensaje)
        {
            bool result = false;
            var client = new RestClient(URL + "/GestionSMS");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(mensaje), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Convert.ToBoolean(response.Content);
            }
            return result;
        }
        public bool EnviarCorreo(clsMensajeCorreo mensajeCorreo)
        {
            try
            {
                clsCodigoValidacion codigoValidacion = new clsCodigoValidacion();
                codigoValidacion.Cedula = mensajeCorreo.Cedula;
                codigoValidacion.Correo = mensajeCorreo.Correo;
                string codigo = string.Empty;
                var client = new RestClient(URL + "/CodigoVerificacion");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(mensajeCorreo), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    codigo= response.Content;
                }

                var clientSmtp = new SmtpClient();
                MailMessage message = new MailMessage();

                string[] sto = mensajeCorreo.Correo.Split(',', ';');
                foreach (string item in sto)
                {
                    MailAddress maTo = new MailAddress(item);
                    message.To.Add(maTo);
                }

                message.Subject = mensajeCorreo.Asunto;
                message.IsBodyHtml = true;
                message.Body = mensajeCorreo.Mensaje + codigo;

                clientSmtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ValidarCodigo(clsCodigoValidacion codigoValidacion)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(codigoValidacion.Celular))
            {
                var client = new RestClient(URL + "/GestionSMS");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(codigoValidacion), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = Convert.ToBoolean(response.Content);
                }
            }
            else
            {
                var client = new RestClient(URL + "/CodigoVerificacion");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(codigoValidacion), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = Convert.ToBoolean(response.Content);
                }
            }
            return result;
        }
    }
}
