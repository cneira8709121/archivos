using System.IO;
using Ruv.Business.DTO.Orfeo;

namespace Ruv.Business.Orfeo.Services
{
    public interface IManageOrfeo
    {
        int? NValorARelacionar { get; set; }
        string GeneraCodigoOrfeo(Dignatario dig, Radicado rad, Direccion dir, Evento evt, ref string cError);
        string CargarArchivoOrfeo(string numeroRadicado, byte[] fileContents, string fileName, int numeroPaginas, string usuarioDigitalizador);
        string ObtenerCodigoOrfeoPorIdVal(int idValoracion, ref string cError);
    }
}
