using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.CriticaN;

[ServiceContract]
public interface ICriticaNService
{
    /// <summary>
    /// Obtiene la imagen de la radicación
    /// </summary>
    /// <param name="nId">Id de la radicación de la que será obtenida la imagen</param>
    /// <param name="cNombreImagen">Nombre de la imagen o archivo obtenido</param>
    /// <param name="cError">Error que será personalizable al usuario</param>
    /// <returns>Arreglo de bytes de la imagen</returns>
    [OperationContract]
    byte[] ObtenerImagenRadicacion(long nId, ref string cNombreImagen, ref string cError);

    /// <summary>
    /// Inserta las respuestas de Critica N
    /// </summary>
    /// <param name="lstRespuesta">Respuestas del usuario que se va a insertar</param>
    /// <param name="cError">Error que será personalizable al usuario</param>
    /// <returns>true si la insercion fue correcta, falso en caso contrario</returns>
    [OperationContract]
    bool InsertaCriticaN(List<clsRespuestaCritica> lstRespuesta, ref string cError);
}
