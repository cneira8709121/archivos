using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHerramientaAnexoPer : clsEntidadBase
    {

        #region Atributos

        private int id;
        private int herramientaId;
        private clsHerramientas herramienta;
        private bool usadoParaDesicion;
        private DateTime fecha;
        private string descripcion;
        private int anexoPerId;

        #endregion

        #region Propiedades

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DataMember]
        public int HerramientaId
        {
            get { return herramientaId; }
            set { herramientaId = value; }
        }
        [DataMember]
        public clsHerramientas Herramienta
        {
            get { return herramienta; }
            set { herramienta = value; }
        }
        [DataMember]
        public bool UsadoParaDesicion
        {
            get { return usadoParaDesicion; }
            set { usadoParaDesicion = value; }
        }
        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        [DataMember]
        public int AnexoPerId
        {
            get { return anexoPerId; }
            set { anexoPerId = value; }
        }

        #endregion

    }
}
