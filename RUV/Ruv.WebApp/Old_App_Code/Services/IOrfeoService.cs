using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo;

[ServiceContract]
public interface IOrfeoService
{
    [OperationContract]
    string GeneraCodigoOrfeo(Dignatario dig, Radicado rad, Direccion dir, Evento evt, ref string cError);
}
