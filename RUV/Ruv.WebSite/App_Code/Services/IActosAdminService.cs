using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common.General;

// NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "IActosAdminService" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IActosAdminService
{
    [OperationContract]
    List<clsActosAdminstrativos> GetActosAdminPaginado(int Inicio, int Fin, string sortColumns);
    [OperationContract]
    int CantidadActosAdmin();
    [OperationContract]
    List<clsParametroGeneral> GetDocumentosPorArea(int Area);
    [OperationContract]
    bool ExisteDeclaracion(string formulario);
    [OperationContract]
    string Guardar(clsActosAdminstrativos actoadmin);
    [OperationContract]
    clsActosAdminstrativos GetActoAdminPorId(int id);
    [OperationContract]
    List<clsActosAdminstrativos> GetActosAdminFitro(string tipoFiltro, string valorFiltro);
    [OperationContract]
    void GenerarDocumentoValoracion(int idValoracion, bool firmado, ref string cError);
}
