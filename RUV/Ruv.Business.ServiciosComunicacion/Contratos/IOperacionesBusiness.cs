using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;

namespace Ruv.Business.ServiciosComunicacion.Contratos
{
    public interface IOperacionesBusiness
    {
        List<Persona> ObtenerPersonas(int pagina, int tamano);

        Persona ObtenerPersonaPorId(int ID);

        Persona ObtenerPersonaPorDocumento(string documento);

        List<Siniestro> ObtenerSiniestrosPorIdPersona(int ID);

        List<GrupoFamiliar> ObtenerGrupoFamiliar(int ID);
    }
}
