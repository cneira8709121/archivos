using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo_FechaYLugar : clsEntidadBase, IDataErrorInfo
    {

        private ValidacionesAlternasDelegate _MetodoAlternoValidacion;
        /// <summary>
        /// Método alterno a invocar para realizar las validaciones de esta clase.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public ValidacionesAlternasDelegate MetodoAlternoValidacion
        {
            get { return _MetodoAlternoValidacion; }
            set { _MetodoAlternoValidacion = value; }
        }


        /// <summary>
        /// Retorna una copia de esta entidad.
        /// </summary>
        /// <returns></returns>
        public clsAnexo_FechaYLugar ObtenerCopia()
        {
            clsAnexo_FechaYLugar Resultado = new clsAnexo_FechaYLugar
            {
                HechosFecha = this.HechosFecha,
                HechosPais = this.HechosPais,
                HechosDepartamento = this.HechosDepartamento,
                HechosMunicipio = this.HechosMunicipio,

                //EntornoId = this.EntornoId,
                //EntornoOtro = this.EntornoOtro,
                //TipoPoblacionId = this.TipoPoblacionId,

                TipoEntorno = this.TipoEntorno,
                BarrioVeredaId = this.BarrioVeredaId,
                BarrioVeredaNombre = this.BarrioVeredaNombre,
                LocalidadCorregimientoId = this.LocalidadCorregimientoId,
                LocalidadCorregimientoNombre = this.LocalidadCorregimientoNombre,

                ID = this.ID,
                EstadoRegistro = this.EstadoRegistro
            };

            return Resultado;
        }
    }
}
