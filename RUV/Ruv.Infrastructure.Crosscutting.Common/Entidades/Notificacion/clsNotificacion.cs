using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion
{
    public class clsNotificacion
    {
        public int NID { get; set; }

        public string CID_DECLARACION { get; set; }

        public int NID_ESTADONOTIFICACION  { get; set; }

        public string CESTADONOTIFICACION { get; set; }

        public string CUBICACIONNOTIFICACION { get; set; }

        public int? ID_UBICACIONNOTIFICACION { get; set; }

        public string CDIRECCIONNOTIFICACION { get; set; }

        public string CTELEFONONOTIFICACION { get; set; }

        public int NID_USUARIO { get; set; }

        public int? NID_PAQUETENOTIFICACION { get; set; }

        public string CNOMBRECOMPLETO { get; set; }

        public string CTIPODOCUMENTO { get; set; }

        public string CNUMERODOCUMENTO { get; set; }

        public string CESTADOPROCESO { get; set; }

        public int NID_DEPARTAMENTO { get; set; }

        public string CNOMBREDEPARTAMENTO { get; set; }

        public int NID_MUNICIPIO { get; set; }

        public string CNOMBREMUNICIPIO { get; set; }

        public int NID_PAIS { get; set; }

        public string CNOMBREPAIS { get; set; }

        public string CNumeroFormulario { get; set; }

        public bool Aprobado { get; set; }

        public DateTime? FechaFinal { get; set; }

        public DateTime? FechaFirma { get; set; }

        public int? IdPaisPuntoNotificacion { get; set; }

        public int? IdDepartamentoPuntoNotificacion { get; set; }

        public int? IdMunicipioPuntoNotificacion { get; set; }

        public int? IdPuntoAtencion { get; set; }

        public int? IdDireccionTerritorial { get; set; }
    }
}
