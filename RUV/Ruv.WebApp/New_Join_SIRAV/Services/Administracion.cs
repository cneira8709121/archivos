using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIRAV.Cliente.Administracion;
using SIRAV.Common.Administracion;
using Ruv.Infrastructure.Crosscutting.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIRAV.Entidades.Administracion;
using SIRAV.Entidades.ActosAdmin;
namespace Ruv.WebApp.New_Join_SIRAV.Services
{
    public class Administracion
    {
        SIRAV.Cliente.Administracion.ClienteUsuario objCliente;


        public Administracion()
        {
            objCliente = new ClienteUsuario();

        }

        private string token1 { get; set; }

        public string Autenticar(string usuario, string contraseña)
        {
            return objCliente.AutenticarUsuario(usuario, contraseña);
        }

        public USUARIO UsuarioPorToken(string token)
        {
            return objCliente.ObtenerUsuarioPorToken(token);
        }

        public void CerrarSesion(string token)
        {
            objCliente.Logout(token);
        }

        public List<MENU> UsuarioMenu(int idUsuario)
        {

            List<MENU> lstmenusuario = new List<MENU>();
            SIRAV.Cliente.Administracion.ClienteRol objRol = new ClienteRol();
            lstmenusuario = objRol.ObtenerMenusPorIdUsuario(idUsuario, HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());

            return lstmenusuario;
        }

        public bool permisosPagina (string url,int usuarioId){

            bool resultado = false;
            SIRAV.Cliente.Administracion.ClienteRol objRol = new ClienteRol();
            resultado = objRol.RolesPermisosEnPagina(url, usuarioId, HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());
            return resultado;
        }

        public List<MENU> ObtenerMenuUsuario(int usuarioId) {

            List<MENU> lstmenu = new List<MENU>();
            SIRAV.Cliente.Administracion.ClienteRol objRol = new ClienteRol();
            lstmenu = objRol.ObtenerMenusPorIdUsuario(usuarioId, HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());
            return lstmenu;
        
        }

        public List<INFORMACION_USUARIO> obtenerUsuariosMenu(string menuId) {
          List<INFORMACION_USUARIO> usuariosMenu = new List<INFORMACION_USUARIO>();
          SIRAV.Cliente.Administracion.ClienteUsuario objusuarios = new ClienteUsuario();
          usuariosMenu = objusuarios.ObtenerUsuariosPorMenu(menuId, HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());
          return usuariosMenu;
        }

        
    }
}