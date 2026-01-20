using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using com = Ruv.Infrastructure.Crosscutting.Common.Entidades;

[ServiceContract]
public interface IRadicacionService
{
	[OperationContract]
    com::LiderRadicacion.clsLiderRadicacion CargarDatos(long nIdDeclaracion, ref string cError);
    [OperationContract]
    bool ActualizarRadicacion(com::clsRadicacion rad, string cObservaciones, ref string cError);
    [OperationContract]
    Int32 RadicarDevolucion(com::clsRadicacion rad, ref string cError);
}
