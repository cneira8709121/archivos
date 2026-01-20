using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Data.Valoracion.Valoracion.Contratos
{
    public interface ILiderValoracion
    {
        bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, DbTransaction tra, ref string cError);
        bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, DbTransaction tra, ref string cError);
        List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError);
        string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError);
    }
}
