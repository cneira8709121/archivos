using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public class clsVincularAnexos
    {
        [DataMember]
        public clsAnexo13 Anexo13 { get; set; }
        [DataMember]
        public clsAnexo01 Anexo01 { get; set; }
        [DataMember]
        public clsAnexo02 Anexo02 { get; set; }
        [DataMember]
        public clsAnexo03 Anexo03 { get; set; }
        [DataMember]
        public clsAnexo04 Anexo04 { get; set; }
        [DataMember]
        public clsAnexo05 Anexo05 { get; set; }
        [DataMember]
        public clsAnexo06 Anexo06 { get; set; }
        [DataMember]
        public clsAnexo07 Anexo07 { get; set; }
        [DataMember]
        public clsAnexo08 Anexo08 { get; set; }
        [DataMember]
        public clsAnexo09 Anexo09 { get; set; }
        [DataMember]
        public clsAnexo10 Anexo10 { get; set; }
        [DataMember]
        public clsAnexo11 Anexo11 { get; set; }

    }
}
