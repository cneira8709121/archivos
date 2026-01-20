using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Business.Valoracion.Contratos
{
    public interface ILiderValoracion
    {
        bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError);
        bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError);
        List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError);
        string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError);
    }
}
