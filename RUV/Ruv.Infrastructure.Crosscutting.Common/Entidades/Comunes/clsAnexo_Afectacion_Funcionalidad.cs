using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo_Afectacion : clsEntidadBase, IDataErrorInfo
    {

        /// <summary>
        /// Retorna una copia de esta entidad.
        /// </summary>
        /// <returns></returns>
        public clsAnexo_Afectacion ObtenerCopia()
        {
            clsAnexo_Afectacion Resultado = new clsAnexo_Afectacion
            {
                Afectado = this.Afectado,
                Otro = this.Otro,
                ID = null,
                EstadoRegistro = this.EstadoRegistro,
                Victima = this.Victima
            };

            Resultado.TiposDeAfectacion.Clear();
            foreach (var item in this.TiposDeAfectacion)
                Resultado.TiposDeAfectacion.Add(item);

            return Resultado;
        }

        [XmlIgnore]
        public IVictima Victima { get; set; }
        
    }
}
