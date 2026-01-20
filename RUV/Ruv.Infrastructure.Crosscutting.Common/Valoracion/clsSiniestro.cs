using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsSiniestro
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int tipo;

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
