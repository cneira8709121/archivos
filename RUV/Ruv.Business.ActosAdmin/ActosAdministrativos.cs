using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Data.ActosAdmin;
using System.Data;
using Ruv.Data;

namespace Ruv.Business.ActosAdmin
{
    public class ActosAdministrativos
    {
        internal static List<clsActosAdminstrativos> GetDatosPaginado(int Inicio, int Fin, string sortColumns)
        {
            List<clsActosAdminstrativos> view = new List<clsActosAdminstrativos>();
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            DataTable actoAdmin = objActosAdmin.GetActosAdministrativosPaginado(Inicio, Fin, sortColumns);
            foreach (DataRow drActos in actoAdmin.Rows)
            {
                clsActosAdminstrativos vActos = new clsActosAdminstrativos();
                ParseDataToView(drActos, ref vActos);
                view.Add(vActos);
            }
            return view;
        }

        private static void ParseDataToView(DataRow drActos, ref clsActosAdminstrativos vActos)
        {
            vActos.ID = Convert.ToInt32(drActos["Id"]);
            vActos.Consecutivo = drActos["consecutivo"].ToString();
            vActos.Fecha = Convert.ToDateTime(drActos["fecha"]);
            vActos.Documento = drActos["Documento"].ToString();
            vActos.Persona = drActos["Solicitante"].ToString();
            vActos.NroFormulario = drActos["NroFormulario"].ToString();
            vActos.UsuarioId = Convert.ToInt32(drActos["UsuarioId"]);
            vActos.Usuario = drActos["Usuario"].ToString();
            vActos.Estado = drActos["Estado"].ToString();
            vActos.Dirigido = drActos["dirigido"].ToString();
        }

        internal static int GetCantidad()
        {
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            return objActosAdmin.GetCantidad();
        }


        internal static bool ExisteDeclaracion(string formulario)
        {
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            return objActosAdmin.ExisteDeclaracion(formulario);
        }

        internal static string Guardar(clsActosAdminstrativos actoadmin)
        {
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            object[] data = null;
            string result = string.Empty;
            ParseViewToData(actoadmin, ref data);
            switch (actoadmin.EstadoRegistro)
            {
                case Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Modificado:
                    result = objActosAdmin.Actualizar(data);
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar:
                    result = objActosAdmin.Insertar(data);
                    break;
                default:
                    break;
            }
            return result;
        }

        private static void ParseViewToData(clsActosAdminstrativos actoadmin, ref object[] data)
        {
            data = new object[]{
                actoadmin.DocumentoId,
                actoadmin.Num_interno,
                actoadmin.NroFormulario,
                actoadmin.Descripcion,
                actoadmin.Dirigido,
                actoadmin.UsuarioId,
                actoadmin.EstadoId,
                actoadmin.ID,
                actoadmin.Consecutivo
            };
        }

        internal static clsActosAdminstrativos GetPorId(int id)
        {
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            TBACTO_ADMINISTRATIVO data = objActosAdmin.GetPorId(id);
            clsActosAdminstrativos view = new clsActosAdminstrativos();
            ParseDataToView(data, view);
            return view;
        }

        private static void ParseDataToView(TBACTO_ADMINISTRATIVO data, clsActosAdminstrativos view)
        {
            //view.TipoDocumento = data.TBPARAMETROS.NOMBRE;
            view.DocumentoId = data.PARAM_DOCUMENTO;
            view.Num_interno = data.NUM_INTERNO;
            view.NroFormulario = data.TBDECLARACIONES.NUMEROFORMULARIO;
            view.Descripcion = data.DESCRIPCION;
            view.Dirigido = data.DIRIGIDO;
            view.UsuarioId = data.ID_USUARIO;
            //view.EstadoId = data.PARAM_ESTADO;
            view.Fecha = data.FECHA;
            view.ID = data.ID;
            view.Consecutivo = data.CONSECUTIVO;
        }

        internal static List<clsActosAdminstrativos> GetDatosFiltro(string tipoFiltro, string valorFiltro)
        {
            List<clsActosAdminstrativos> view = new List<clsActosAdminstrativos>();
            entActosAdministrativos objActosAdmin = new entActosAdministrativos();
            DataTable actoAdminData = objActosAdmin.GetActosAdministrativosFiltro(tipoFiltro, valorFiltro);
            foreach (DataRow drActos in actoAdminData.Rows)
            {
                clsActosAdminstrativos vActos = new clsActosAdminstrativos();
                ParseDataToView(drActos, ref vActos);
                view.Add(vActos);
            }
            return view;
        }
    }
}
