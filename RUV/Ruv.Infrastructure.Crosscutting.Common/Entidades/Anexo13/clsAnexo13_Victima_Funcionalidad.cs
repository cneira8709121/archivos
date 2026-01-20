using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo13_Victima : clsEntidadBase, IDataErrorInfo
    {
        /// <summary>
        /// Alimenta las colecciones de esta entidad con la información de otra.
        /// </summary>
        /// <param name="origen"></param>
        public void CopiarColeccionesDesde(clsAnexo13_Victima origen)
        {
            HechosVictimizantes =
              clsUtils.CopiarListOf<int>(origen.HechosVictimizantes);
            Discapacidades =
              clsUtils.CopiarListOf<int>(origen.Discapacidades);
            TiposDeAfectacion =
                clsUtils.CopiarListOf<int>(origen.TiposDeAfectacion);
            AnexoPadre = origen.AnexoPadre;
        }

        /// <summary>
        /// Reporta hacia TomaDeclaración algún cambio en los datos del declarante.
        /// </summary>
        /// <param name="nombrePropiedad"></param>
        void ReportarHaciaElDeclarante(string nombrePropiedad)
        {
            if (ID != null
              && PersonasAfectadas != null
              && PersonasAfectadas.Declaracion != null
              && PersonasAfectadas.Declaracion.TomaDeclaracion != null
              && PersonasAfectadas.Declaracion.TomaDeclaracion.DeclaranteId == ID)
                PersonasAfectadas.Declaracion
                  .TomaDeclaracion.ReportarCambioPropiedad(nombrePropiedad);
        }

        clsAnexo13 _AnexoPadre;
        [System.Xml.Serialization.XmlIgnore]
        public clsAnexo13 AnexoPadre
        {
            get { return _AnexoPadre; }
            set { _AnexoPadre = value; }
        }
    }
}
