using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
   public  class clsCargaPersonasAsociadasDeclaracion
    {
       [Column(Name = "NOMBREDECLARANTE")]
       public string cNombreDeclarante { get; set; }
       [Column(Name = "TIPODOCUMENTO")]
       public string cTipoDocumento { get; set; }
       [Column(Name = "NUMERODOCUMENTO")]
       public string cNumeroDocumento { get; set; }
       [Column(Name = "RELACION")]
       public string cRelacionDeclarante { get; set; }
    }
}
