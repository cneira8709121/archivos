using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public interface IPersonaAfectada
    {

        //clsPersonasAfectadas PersonasAfectadas { get; set; }

        int? ID { get; set; }
        eEstadoRegistro EstadoRegistro { get; set; }

        #region CAMPOS OBLIGATORIOS
        int NumeroConsecutivo { get; set; }
        int FamiliaConsecutivo { get; set; }
        string PrimerNombre { get; set; }
        string SegundoNombre { get; set; }
        string PrimerApellido { get; set; }
        string SegundoApellido { get; set; }
        string NombreCompleto { get; set; }
        int? TipoDocumento { get; set; }
        string NumeroDocumento { get; set; }
        DateTime? FechaNacimiento { get; set; }
        int? Nacionalidad { get; set; }
        List<int> HechosVictimizantes { get; set; }
        int? Relacion { get; set; }
        int? EstadoCivil { get; set; }
        int? RegimenEspecial { get; set; }
        #endregion

        #region ENFOQUE DIFERENCIAL
        int? Genero { get; set; }
        int? OrientacionSexual { get; set; }
        int? IdentidadGenero { get; set; }
        List<int> Discapacidades { get; set; }
        string OtraDiscapacidad { get; set; }
        int? PertenenciaEtnica { get; set; }
        int? ComunidadEtnica1 { get; set; }
        int? ComunidadEtnica2 { get; set; }
        string OtraComunidadEtnica { get; set; }
        int? MujerCabezaDeHogar { get; set; }
        int? GestanteLactante { get; set; }

        int? HombreCabezaDeHogar { get; set; }
        int? Campesinado { get; set; }
        int? PersonaBuscadora { get; set; }
        #endregion
    }
}
