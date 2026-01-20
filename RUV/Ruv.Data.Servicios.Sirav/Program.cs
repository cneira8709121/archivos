using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRAV.Cliente.Administracion;

namespace Ruv.Data.Servicios.Sirav
{
    class Program
    {
        public void AutenticarUsuario() {

            ClienteUsuario objusuario = new ClienteUsuario();
            string token = objusuario.AutenticarUsuario("valoracion1","Liqonujo+4");
        
        }
    }
}
