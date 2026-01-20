using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public class clsAgregarPersonaValoracion
    {
        public string cPrimerNombre { get; set; }

        public string cSegundoNombre { get; set; }

        public string cPrimerApellido { get; set; }

        public string cSegundoApellido { get; set; }

        public int nTipoDocumento { get; set; }

        public string cNumeroDocumento { get; set; }

        public DateTime? cFechanacimiento { get; set; }

        public string cDireccion { get; set; }

        public string cTelefono { get; set; }

        public string cCorreoelectronico { get; set; }

        public int nRelacion { get; set; }

        public int nEstadoCivil { get; set; }

        public int nRegimenEspecial { get; set; }

        public int nGenero { get; set; }

        public int nEtnia { get; set; }

        public string cComunidad { get; set; }

        public List<int> lnDiscapacidad { get; set; }

        public int nCabezaHogar { get; set; }

        public int nGestante { get; set; }

        public string cComentarios { get; set; } 

        public string cFechaAgregado { get; set; }

        public int nIdDeclaracion { get; set; }
    }
}
