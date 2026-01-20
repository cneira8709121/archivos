using System;
using System.Collections.Generic;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Utils.DataSources
{
    public class ListaTareasDataSource : IDataSourceBase
    {
        public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

        public ListaTareasDataSource()
        {
            
        }

        public string NumeroFormulario { get; set; }
        public DateTime? FechaInicialRadicado { get; set; }
        public DateTime? FechaFinalRadicado { get; set; }

        public int VirtualItemCount()
        {
            string error = string.Empty;

            var cantidad = RUV.I.Red.ServicioGeneral.ObtenerListaTareasWPFCantidad(RUV.I.Usuario.Id, FechaInicialRadicado, FechaFinalRadicado, NumeroFormulario);
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
            string cError = string.Empty;
            var result = RUV.I.Red.ServicioGeneral.ObtenerListaTareas(RUV.I.Usuario.Id, RUV.I.Seguridad.LlaveUsuario, FechaInicialRadicado, FechaFinalRadicado, NumeroFormulario, startRow, maxRows).ToList<object>();

            return result;
        }

        public IList<object> GetData(int startRow, int maxRows, string sortColumns)
        {
            throw new NotImplementedException();
        }
    }
}
