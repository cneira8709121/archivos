using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;

namespace Ruv.WPF.Captura.Utils.DataSources
{
    public class FormulariosActivarDataSource : IDataSourceBase
    {
        public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

        public Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario.clsFormularioSolicitudNoRadicados Filtro { get; set; }

        public int VirtualItemCount()
        {
            string error = string.Empty;
            int cantidad = 0;
            if (Filtro != null)
            {
                cantidad = RUV.I.Red.ServicioGestionDocumentos.ObtenerCantidadFormulariosActivar(Filtro, ref error);
            }
            if (!string.IsNullOrEmpty(error))
            {
                OnError(null, new ErrorEventArgs(error));
            }
            return cantidad;
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            if (ErrorConsulta != null)
            {
                ErrorConsulta(sender, e);
            }
        }


        public IList<object> GetData(int startRow, int maxRows)
        {
            string error = string.Empty;
            List<object> objResult = new List<object>();
            clsFormulario[] formularios = null;
            if (Filtro != null)
            {
                formularios = RUV.I.Red.ServicioGestionDocumentos.ObtenerFormulariosActivar(Filtro, startRow, maxRows, ref error);
            }
            if (!string.IsNullOrEmpty(error))
            {
                OnError(null, new ErrorEventArgs(error));
            }
            if (formularios != null)
            {
                objResult = formularios.ToList<object>();
            }
            return objResult;
        }

        public IList<object> GetData(int startRow, int maxRows, string sortColumns)
        {
            throw new NotImplementedException();
        }
    }
}