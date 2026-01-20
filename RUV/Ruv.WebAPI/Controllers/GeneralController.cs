using Elmah;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Ruv.WebAPI.Controllers
{
    public class GeneralController : ApiController
    {

        // POST api/<controller>
        [HttpPost]
        [Route("GuardarRadicado")]
        public long Post([FromBody] clsRadicacion radicacion)
        {
            if (!string.IsNullOrEmpty(radicacion.ARCHIVO_BASE64))
            {
                radicacion.DocumentoDigital = Convert.FromBase64String(radicacion.ARCHIVO_BASE64);
            }
            else
            {
                byte[] archivo = null;
                try
                {
                    archivo = ((byte[])radicacion.DocumentoDigital);
                }
                catch (Exception ex)
                {
                    radicacion.DocumentoDigital = Convert.FromBase64String(radicacion.DocumentoDigital.ToString());
                }
            }
            radicacion.ARCHIVO_BASE64 = null;
            long numero = new Ruv.Business.Captura.GuardarDatos().GuardarRadicacion(radicacion);
            return numero;
        }
    }
}