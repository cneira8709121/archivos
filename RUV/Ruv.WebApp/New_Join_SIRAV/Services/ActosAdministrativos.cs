using SIRAV.Entidades.ActosAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruv.WebApp.New_Join_SIRAV.Services
{
    public class ActosAdministrativos
    {
        SIRAV.Cliente.ActosAdmin.ClienteActosAdmin objAdmin;
        public ActosAdministrativos()
        {
            objAdmin = new SIRAV.Cliente.ActosAdmin.ClienteActosAdmin();
        }

        public SIRAV.Common.Resultado<KeyValuePair<int, string>> CrearActoAdministrativo(DECLARACION declaracion)
        {
            SIRAV.Common.Resultado<KeyValuePair<int, string>> result = objAdmin.GenerarActoAdministrativo(declaracion, HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());
            return result;
        }
    }
}