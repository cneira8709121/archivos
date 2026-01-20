using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Data.Common;


  [ServiceContract]
  public interface IGeneralService
  {
    [OperationContract]
    byte[] ObtenerParametrosGenerales(string tipoParams);

    [OperationContract]
    clsResultado DeclaracionAlmacenar(clsDeclaracion declaracion, string numeroDeclaracion, clsUsuario usuario);

    [OperationContract]
    List<clsBusquedaDeclaracion> BuscarDeclaracion(clsBusquedaDeclaracion parametros, string tipoParams);

    [OperationContract]
    clsDeclaracion ObtenerDeclaracion(int id, string tipoDeclaracion);

    [OperationContract]
    decimal RadicacionAlmacenar(clsRadicacion radicacion);
      
    [OperationContract]
    decimal GuardarRadicacion(clsRadicacion radicacion);

    [OperationContract]
    Boolean ActualizarRadicacion(clsRadicacion radicacion);

    [OperationContract]
    bool CargarImagen(byte[] imageData, string fileName);

    [OperationContract]
    bool CargarPdf(byte[] fileData, string fileName);

    [OperationContract]
    List<clsListaTareas> ObtenerListaTareas(int idUsuario, string tipoParams, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario, int? PageNumber, int? PageSize);

    [OperationContract]
    List<clsListaTareas> ObtenerListaTareasPaginado(int idUsuario, string tipoParams, int startRow, int pageSize, string sortColumns, string filterEx);

    [OperationContract]
    int ObtenerListaTareasCantidad(int idUsuario);

    [OperationContract]
    int ObtenerListaTareasWPFCantidad(int idUsuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario);

    [OperationContract]
    clsResultado RadicacionActualizarEstado(int idRadicacion, int param_estado, string tipoParams, int idDeclaracion);
    [OperationContract]
    ObservableCollection<clsGlosa> getGlosasxDeclaracion(clsDeclaracion laDeclaracion);
    [OperationContract]
    ObservableCollection<clsGlosaIntencion> getIGlosasxDeclaracion(clsDeclaracion laDeclaracion);
    [OperationContract]
    clsGlosa setGlosas(clsGlosa myGlosa);
    [OperationContract]
    clsGlosaIntencion setIntenGlosas(clsGlosaIntencion myIntencionGlosa);
    [OperationContract]
    clsResultado ActualizarEstadoDeclaracion(clsDeclaracion declaracion);

    [OperationContract]
    List<clsGeografiaCompleta> ObtenerGeografiaCompleta(ref string cError);

    [OperationContract]
    List<clsGeografiaCompleta> ObtenerPaises(ref string cError);

    [OperationContract]
    List<clsGeografiaCompleta> ObtenerDepartamentosPorPais(int idPais, ref string cError);

    [OperationContract]
    List<clsGeografiaCompleta> ObtenerMunicipiosPorDepartamento(int idDepartamento, ref string cError);

    [OperationContract]
    string ObtenerDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto);

    [OperationContract]
    void ActualizarDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto, string direccion, ref string cError);

  }
