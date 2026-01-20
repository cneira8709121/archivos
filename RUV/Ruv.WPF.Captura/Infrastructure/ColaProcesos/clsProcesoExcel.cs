using System;
using System.Data.Linq.Mapping;

namespace Ruv.WPF.Captura.Infrastructure.ColaProcesos
{
    public class clsProcesoExcel
    {
        [Column(Name = "Número de FUD")]
        public string FUD { get; set; }
        [Column(Name = "Nombre declarante")]
        public string NombreDeclarante { get; set; }
        [Column(Name = "Fecha en cola")]
        public DateTime FechaEnCola { get; set; }
        [Column(Name = "Fecha última transmisión")]
        public DateTime? FechaUltimaTransmision { get; set; }
    }
}