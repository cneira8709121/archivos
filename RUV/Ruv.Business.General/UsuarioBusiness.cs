using Ruv.Data.General;
using Ruv.Infrastructure.Crosscutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.General
{
    public class UsuarioBusiness
    {
        public USUARIO_BASICO ObtenerUsuarioPorId(int idUsuario, ref string cError)
        {
            var elements = new entUsuario().ObtenerUsuarioPorId(idUsuario, ref cError);
            USUARIO_BASICO usr = new USUARIO_BASICO();
            if (string.IsNullOrEmpty(cError) && elements != null)
            {
                usr.ID = elements.ID;
                usr.IDENTIFICACION = elements.IDENTIFICACION;
                usr.CLAVE = elements.CLAVE;
                usr.ACTIVO = elements.ACTIVO;
                usr.USERNAME = elements.USERNAME;
            }
            return usr;
        }
    }
}
