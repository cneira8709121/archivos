using System;
using System.Data.Common;
using System.IO;
using Ruv.Business.DTO.Orfeo;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Utilities;
using data = Ruv.Data.Orfeo;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Business.Orfeo
{
    public class General : Services.IManageOrfeo
    {
        #region Properties

        public int? NValorARelacionar { get; set; }

        #endregion
        #region Public methods

        #region Services implementation

        public string GeneraCodigoOrfeo(Dignatario dig, Radicado rad, Direccion dir, Evento evt, ref string cError)
        {
            Secuencia secDignatario = InsertaDignatario(dig, ref cError);
            if (secDignatario == null || secDignatario.Estado != "OK" || !string.IsNullOrEmpty(cError)) return null;

            Secuencia secRadicado = InsertaRadicado(rad, ref cError);
            if (secRadicado == null || secRadicado.Estado != "OK" || !string.IsNullOrEmpty(cError)) return null;

            dir.coddir = secDignatario.SecuenciaMensaje;
            dir.numradicado = secRadicado.SecuenciaMensaje;
            dir.coddpto = dig.NIdDepartamento;
            dir.codmpio = dig.NIdMunicipio;
            dir.direccion = dig.CDireccion;
            dir.dirtelefono = dig.CTelefono;
            Secuencia secDireccion = InsertaDireccion(dir, ref cError);
            if (secDireccion == null || secDireccion.Estado != "OK" || !string.IsNullOrEmpty(cError)) return null;

            evt.numradicado = secRadicado.SecuenciaMensaje;
            Secuencia secEvento = InsertaEvento(evt, ref cError);
            if (secEvento == null || secEvento.Estado != "OK" || !string.IsNullOrEmpty(cError)) return null;

            string cOrfeo = secRadicado.SecuenciaMensaje;

            if (NValorARelacionar.HasValue)
            {
                if (!(RelacionarOrfeoValoracion(cOrfeo, NValorARelacionar.Value, ref cError) && string.IsNullOrEmpty(cError))) return null;
            }
            return cOrfeo;
        }

        public string CargarArchivoOrfeo(string numeroRadicado, byte[] fileContents, string fileName, int numeroPaginas, string usuarioDigitalizador)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            var resultadoArchivo = iOrfeo.ObtenerNombreAnexo(numeroRadicado, fileName);
            var resultadoPublicar = iOrfeo.CargarArchivoRemoto(numeroRadicado, Convert.ToBase64String(fileContents), resultadoArchivo.Mensaje);
            var resultadoVincular = iOrfeo.VincularArchivoCargado(resultadoPublicar, numeroRadicado);
            var resultadoEvento = iOrfeo.RegistrarEventoCargaArchivo(resultadoPublicar, numeroPaginas, numeroRadicado, usuarioDigitalizador);
            return resultadoPublicar;
        }

        public string ObtenerCodigoOrfeoPorIdVal(int idValoracion, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            return iOrfeo.ObtenerCodigoOrfeoPorIdVal(idValoracion, ref cError);
        }

        #endregion

        #endregion
        #region Private methods

        private Secuencia InsertaDignatario(Dignatario dig, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            return iOrfeo.InsertaDignatario(dig, ref cError);
        }


        private Secuencia InsertaRadicado(Radicado rad, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            return iOrfeo.InsertaRadicado(rad, ref cError);
        }

        private Secuencia InsertaDireccion(Direccion dir, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            return iOrfeo.InsertaDireccion(dir, ref cError);
        }

        private Secuencia InsertaEvento(Evento evt, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            return iOrfeo.InsertaEvento(evt, ref cError);
        }

        private bool RelacionarOrfeoValoracion(string cOrfeo, int nValoracion, ref string cError)
        {
            data::Services.IManageOrfeo iOrfeo = (data::Services.IManageOrfeo)Spring.GetService(resx::Dependencias.Objetos.OrfeoData);
            using (DbTransaction tra = Dao.InitTransaction())
            {
                if (iOrfeo.RelacionarOrfeoValoracion(cOrfeo, nValoracion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        #endregion
    }
}
