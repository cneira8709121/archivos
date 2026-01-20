using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Business.ActosAdmin
{
    public class ActosAdminBusiness
    {
        public List<clsActosAdminstrativos> GetActosAdministrativosPaginado(int Inicio, int Fin, string sortColumns)
        {
            return ActosAdministrativos.GetDatosPaginado(Inicio, Fin, sortColumns);
        }

        public int GetCantidadActosAdmin()
        {
            return ActosAdministrativos.GetCantidad();
        }

        public List<clsParametroGeneral> GetDocumentosPorArea(int Area)
        {
            return DocumentosActoAdmin.GetDocumentosPorArea(Area);
        }


        public bool ExisteDeclaracion(string formulario)
        {
            return ActosAdministrativos.ExisteDeclaracion(formulario);
        }

        public string Guardar(clsActosAdminstrativos actoadmin)
        {
            return ActosAdministrativos.Guardar(actoadmin);
        }

        public clsActosAdminstrativos GetActoAdministrativoPorId(int id)
        {
            return ActosAdministrativos.GetPorId(id);
        }

        public List<clsActosAdminstrativos> GetActosAdministrativosFiltro(string tipoFiltro, string valorFiltro)
        {
            return ActosAdministrativos.GetDatosFiltro(tipoFiltro, valorFiltro);
        }
    }
}
