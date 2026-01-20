using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.ServiciosComunicacion;

namespace Ruv.Data.ServiciosComunicacion.Contratos
{
    public interface IOperacionesData
    {
        List<clsPersona> ObtenerPersonas(int pagina, int tamano);

        clsPersona ObtenerPersonaPorId(int ID);

        clsPersona ObtenerPersonaPorDocumento(string documento);

        List<clsSiniestro> ObtenerSiniestrosPorIdPersona(int ID);

        List<clsGrupoFamiliar> ObtenerGrupoFamiliar(int ID);
    }
}
