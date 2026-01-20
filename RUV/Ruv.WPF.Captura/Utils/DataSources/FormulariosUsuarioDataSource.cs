using System;
using System.Collections.Generic;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;

namespace Ruv.WPF.Captura.Utils.DataSources
{
    public class FormulariosUsuarioDataSource : IDataSourceBase
    {
        public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

        public FormulariosUsuarioDataSource()
        {
            
        }

        public eEstadoFormulario IdEstado { get; set; }
        public string CNumeroFormulario { get; set; }
        public int? NDesde { get; set; }
        public int? NHasta { get; set; }
        public DateTime? DGenerado { get; set; }

        public int VirtualItemCount()
        {
            string error = string.Empty;
            int cantidad = 0;
            cantidad = RUV.I.Red.ServicioGestionDocumentos.ObtenerCantidadFormulariosPorUsuarioEstado(
                new clsSolicitudFormularioEstado { CNumeroFormulario = this.CNumeroFormulario, NDesde = this.NDesde, NHasta = this.NHasta, DGenerado = this.DGenerado, IdEstado = this.IdEstado, NIdUsuario = RUV.I.Usuario.Id }, ref error);
            if(!string.IsNullOrEmpty(error))
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
            formularios = RUV.I.Red.ServicioGestionDocumentos.ObtenerFormulariosPorUsuarioEstadoPaginado(
                new clsSolicitudFormularioEstado { CNumeroFormulario = this.CNumeroFormulario, NDesde = this.NDesde, NHasta = this.NHasta, DGenerado = this.DGenerado, IdEstado = this.IdEstado, NIdUsuario = RUV.I.Usuario.Id, NPagina = startRow, NDatosPorPg = maxRows }, ref error);
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
