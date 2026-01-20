using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Business.DTO.Valoracion;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAprobarValoracionService" in both code and config file together.
[ServiceContract]
public interface ILiderValoracionService
{
    [OperationContract]
    bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError);
    
    [OperationContract]
    bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError);

    [OperationContract]
    List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError);

    [OperationContract]
    string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError);

}
