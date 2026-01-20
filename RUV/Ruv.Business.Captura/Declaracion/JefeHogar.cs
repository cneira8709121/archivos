using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;

using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Ruv.Business.Captura.Declaracion
{
    public class JefeHogar
    {
        #region Guardar Datos
        public static void Actualizar(int id_declaracion, int id_JefeHogar, int FamiliaConsecutivo, DbTransaction tran)
        {
            entDeclaraciones entDecl = new entDeclaraciones();
            entDecl.actualizarJefeHogar(id_declaracion, id_JefeHogar, FamiliaConsecutivo, tran);
        }
        #endregion

        #region Obtener Datos
        
        #endregion
    }
}
