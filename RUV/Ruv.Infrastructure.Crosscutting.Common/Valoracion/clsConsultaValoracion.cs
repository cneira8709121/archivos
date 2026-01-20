using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    

    [DataContract]
    public class clsConsultaValoracion
    {
        private string filtro;
        private string ordenarPor;
        private int pagina;
        private int tamaño;
        private int total;
        private int valoradorId;
        private eTipoConsulta tipoConsulta;
        private List<clsDeclaracionValoraracion> declaraciones;
        private List<clsValoradorTareas> tareas;

        [DataMember]
        public List<clsValoradorTareas> Tareas
        {
            get { return tareas; }
            set { tareas = value; }
        }



        [DataMember]
        public List<clsDeclaracionValoraracion> Declaraciones
        {
            get { return declaraciones; }
            set { declaraciones = value; }
        }

        [DataMember]
        public int ValoradorId
        {
            get { return valoradorId; }
            set { valoradorId = value; }
        }

        [DataMember]
        public string Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }

        [DataMember]
        public string OrdenarPor
        {
            get { return ordenarPor; }
            set { ordenarPor = value; }
        }

        [DataMember]
        public int Pagina
        {
            get { return pagina; }
            set { pagina = value; }
        }

        [DataMember]
        public int Tamaño
        {
            get { return tamaño; }
            set { tamaño = value; }
        }

        [DataMember]
        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        [DataMember]
        public eTipoConsulta TipoConsulta
        {
            get { return tipoConsulta; }
            set { tipoConsulta = value; }
        }
        

        
    }
}
