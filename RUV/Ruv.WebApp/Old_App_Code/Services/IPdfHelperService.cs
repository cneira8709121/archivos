using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Business.DTO.Devolucion;

[ServiceContract]
public interface IPdfHelperService
{
    /// <summary>
    /// Genera un archivo PDF con el código ingresado.
    /// </summary>
    /// <param name="codigo">Código que llevará el archivo PDF</param>
    /// <param name="cError">Mensaje de error personalizado a mostrar al usuairo.</param>
    /// <returns>Arreglo de bytes con el archivo PDF generado</returns>
    [OperationContract]
    byte[] GenerateOnePdfFile(string codigo, ref string cError);
    
    /// <summary>
    /// Genera un archivo ZIP con múltiples archivos PDF, cada uno de ellos con el código ingresado.
    /// </summary>
    /// <param name="codigos">Lista de códigos que llevará cada archivo PDF</param>
    /// <param name="cError">Mensaje de error personalizado a mostrar al usuairo.</param>
    /// <returns>Arreglo de bytes con el archivo ZIP generado</returns>
    [OperationContract]
    byte[] GenerateManyPdfFilesAsZip(Dictionary<string,bool> codigos, ref string cError);

    [OperationContract]
    byte[] GeneratePdf(string contenido, ref string cError);

    [OperationContract]
    byte[] GeneratePdfDevolucion(clsDatosparaDevolucion datosparaDevolucion, ref string cError);

    [OperationContract]
    byte[] GenerateOnePdfFileConNacional(string codigo, ref string cError);

    //[OperationContract]
    //byte[] GenerateManyPdfFilesConNacionalAsZip(IList<string> codigos, ref string cError);
}
