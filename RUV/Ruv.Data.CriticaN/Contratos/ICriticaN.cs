using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.CriticaN;
using System.Data.Common;

namespace Ruv.Data.CriticaN.Contratos
{
    public interface ICriticaN
    {
        /// <summary>
        /// Inserta en la tabla de Radica_criticaN los nuevos registros
        /// </summary>
        /// <param name="rc">Respuestas del usuario que se van a guardar </param>
        /// <param name="cError">Error que será personalizable al usuario </param>
        /// <returns>True si la inserción es exitosa, false si ocurre algún error en la operación</returns>
        bool GuardarValidacion(List<clsRespuestaCritica> lstRepuesta, DbTransaction tra, ref string cError);
    }

}
