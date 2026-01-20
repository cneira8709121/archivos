using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Radicacion;
using System.Data.Common;

namespace Ruv.Data.Radicacion.Contratos
{
    public interface ILiderRadicacion
    {
        clsRadicacion GetRadicacion(long nIdDeclaracion, ref string cError);
        clsRadicacion GetRadicacion(long nIdDeclaracion, string cNumeroFormulario, ref string cError);
        bool UpdateRadicacion(clsRadicacion rad, string cObservaciones, DbTransaction tra, ref string cError);
    }
}
