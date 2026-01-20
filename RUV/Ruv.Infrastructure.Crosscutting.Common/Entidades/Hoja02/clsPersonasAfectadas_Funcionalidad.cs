using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsPersonasAfectadas : clsEntidadBase, IDataErrorInfo
    {

        #region PROPIEDADES DE SOPORTE, NO SE REQUIERE ALMACENAMIENTO

        /// <summary>
        /// Código del declarante.
        /// Esto es una referencia a la propiedad 'Declaracion.TomaDeclaracion.DeclaranteId'.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int? DeclaranteId
        {
            get
            {
                if (Declaracion == null || Declaracion.TomaDeclaracion == null) return null;
                return Declaracion.TomaDeclaracion.DeclaranteId;
            }
            set
            {
                if (Declaracion == null || Declaracion.TomaDeclaracion == null) return;

                Declaracion.TomaDeclaracion.DeclaranteId = value;
                ReportarCambioPropiedad("DeclaranteId");

                // Reportar los cambios para que la interfase se actualice.
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclarantePrimerNombre");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclarantePrimerApellido");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteSegundoNombre");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteSegundoApellido");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteTipoDocumento");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteNacionalidad");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteNumeroDocumento");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteTipoDocumento");
                Declaracion.TomaDeclaracion.ReportarCambioPropiedad("DeclaranteFechaNacimiento");
            }
        }

        #endregion

        public void ReportarCambioPropiedadAlEditar()
        {
            ReportarCambioPropiedad("ListaPersonasOrdenada");
            ReportarCambioPropiedad("ListaPersonas");
        }

    }
}
