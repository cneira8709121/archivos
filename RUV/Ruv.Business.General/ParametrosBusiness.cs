using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data.General;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Business.DTO.General;

namespace Ruv.Business.General
{
    public class ParametrosBusiness
    {
        public List<clsParametroGeneral> ObtenerParametros(int tipoParametro, ref string cError)
        {
            var elements = new entParametro().ObtenerParametros(tipoParametro, ref cError);
            if (string.IsNullOrEmpty(cError) && elements != null)
                return elements.Select(x => new clsParametroGeneral { Id = x.Id, Nombre = x.Nombre }).ToList();
            return null;
        }
    }
}
