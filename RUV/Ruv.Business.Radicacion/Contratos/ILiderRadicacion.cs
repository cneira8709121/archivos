using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Radicacion;

namespace Ruv.Business.Radicacion.Contratos
{
    public interface ILiderRadicacion
    {
        clsLiderRadicacion CargarDatos(long nIdDeclaracion, ref string cError);
        bool ActualizarRadicacion(clsRadicacion rad, string cObservaciones, ref string cError);
    }
}
