using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
    public class clsDeclaracionesValoracion
    {
        /// <summary>
        /// El código de la declaración.
        /// </summary>
        [Column(Name="ID")]
        public int? ID { get; set; }


        /// <summary>
        /// Nombre del declarante.
        /// </summary>
        [Column(Name = "Nombre_Persona")]
        public string NombreDeclarante { get; set; }


        /// <summary>
        /// Documento del declarante.
        /// </summary>
        [Column(Name = "Documento")]
        public string DocumentoDeclarante { get; set; }


        /// <summary>
        /// Fecha de Radicación
        /// </summary>
        [Column(Name = "Fecha_Radicacion")]
        public DateTime FechaRadicado { get; set; }


        /// <summary>
        /// Numero de FOrmulario
        /// </summary>
        [Column(Name = "Formulario")]
        public string NumeroFormulario { get; set; }


        /// <summary>
        /// Tipo de Hecho Victimizante.
        /// </summary>
        [Column(Name = "HechoVictimizante")]
        public string HechoVictimizante { get; set; }


        /// <summary>
        /// TOtal Hv
        /// </summary>
        [Column(Name = "Total_Hv")]
        public int TotalHV { get; set; }


        /// <summary>
        /// Departamento
        /// </summary>
        [Column(Name = "Departamento")]
        public string Departamento { get; set; }


        /// <summary>
        /// Municipio
        /// </summary>
        [Column(Name = "Municipio")]
        public string Municipio { get; set; }


        /// <summary>
        /// Entidad
        /// </summary>
        [Column(Name = "TipoEntidad")]
        public string Entidad { get; set; }

        /// <summary>
        /// Valorador
        /// </summary>
        [Column(Name = "Valorador")]
        public string Valorador { get; set; }
    }
}
