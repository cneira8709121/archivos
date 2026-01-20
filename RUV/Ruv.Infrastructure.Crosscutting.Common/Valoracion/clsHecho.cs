using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHecho
    {
        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int tipoHecho;
        [DataMember]
        public int TipoHecho
        {
            get { return tipoHecho; }
            set { tipoHecho = value; }
        }
        private DateTime fecha;
        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private int departamento;
        [DataMember]
        public int Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        private int municipio;
        [DataMember]
        public int Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }
        private int tipoentorno;
        [DataMember]
        public int Tipoentorno
        {
            get { return tipoentorno; }
            set { tipoentorno = value; }
        }
        private int? corrLocId;
        [DataMember]
        public int? CorrLocId
        {
            get { return corrLocId; }
            set { corrLocId = value; }
        }
        private int? barrVerId;
        [DataMember]
        public int? BarrVerId
        {
            get { return barrVerId; }
            set { barrVerId = value; }
        }
        private string otraLocCorrId;
        [DataMember]
        public string OtraLocCorrId
        {
            get { return otraLocCorrId; }
            set { otraLocCorrId = value; }
        }
        private string otroBarVerId;
        [DataMember]
        public string OtroBarVerId
        {
            get { return otroBarVerId; }
            set { otroBarVerId = value; }
        }
        private int victima1;
        [DataMember]
        public int Victima1
        {
            get { return victima1; }
            set { victima1 = value; }
        }
        private int valorEspecifico;
        [DataMember]
        public int ValorEspecifico
        {
            get { return valorEspecifico; }
            set { valorEspecifico = value; }
        }
        private clsValoracion valoracion;

        public clsValoracion Valoracion
        {
            get { return valoracion; }
            set { valoracion = value; }
        }
        private List<clsPersonaNuevoHecho> personas;
        [DataMember]
        public List<clsPersonaNuevoHecho> Personas
        {
            get { return personas; }
            set { personas = value; }
        }


        private int _ValInmuebleAbandono;
        [DataMember]
        public int ValInmuebleAbandono
        {
            get { return _ValInmuebleAbandono; }
            set { _ValInmuebleAbandono = value; }
        }

        private int _ValInmuebleDespojo;
        [DataMember]
        public int ValInmuebleDespojo
        {
            get { return _ValInmuebleDespojo; }
            set { _ValInmuebleDespojo = value; }
        }


        private DateTime? _FechaDespojo;
        [DataMember]
        public DateTime? FechaDespojo
        {
            get { return _FechaDespojo; }
            set { _FechaDespojo = value; }
        }

        private int? _TipoHechoOtro;
        [DataMember]
        public int? TipoHechoOtro
        {
            get { return _TipoHechoOtro; }
            set { _TipoHechoOtro = value; }
        }
        private DateTime? _FechaAbandono;
        [DataMember]
        public DateTime? FechaAbandono
        {
            get { return _FechaAbandono; }
            set { _FechaAbandono = value; }
        }
    }
}
