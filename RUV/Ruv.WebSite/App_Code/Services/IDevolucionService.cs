using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.General;

[ServiceContract]
public interface IDevolucionService
{
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="nIdDeclaracion"></param>
    ///// <param name="nIdRadicacion"></param>
    ///// <param name="cError"></param>
    ///// <returns></returns>
    //[OperationContract]
    //clsDevolucion ObtenerDevolucion(int nIdDeclaracion, int nIdRadicacion, ref string cError);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nIdDeclaracion"></param>
    /// <param name="cError"></param>
    /// <returns></returns>
    [OperationContract]
    clsDevolucion ObtenerDevolucion(int nIdDeclaracion, ref string cError);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dev"></param>
    /// <param name="cError"></param>
    /// <returns></returns>
    [OperationContract]
    bool SolicitarDevolucion(clsDevolucion dev, ref string cError);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dev"></param>
    /// <param name="cError"></param>
    /// <returns></returns>
    [OperationContract]
    bool ActualizarDevolucion(clsDevolucion dev, ref string cError);

    [OperationContract]
    byte[] GenerarDocumentoDevolucion(int nIdDevolucion, ref string cError);

    /// <summary>
    /// Obtener las causales de devolución depende de la interfaz Filtrar el tipo
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    List<clsCausal> ObtenerCausalesDevolucion(ref string cError);
}