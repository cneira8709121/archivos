using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.ServiceModel.Activation;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common.General;
using rex = Ruv.Infrastructure.Crosscutting.Common.Entidades;
using val = Ruv.Business.DTO.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
// NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "IValoracionService" en el código y en el archivo de configuración a la vez.

[ServiceContract]
public interface IValoracionService
{
    [OperationContract]
    List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorar();
    [OperationContract]
    List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorarPaginada(int Inicio, int Fin, string sortColumns, string filtro, string Valor);
    [OperationContract]
    void ListaDeclaracionesEnValPaginada(ref clsConsultaValoracion consulta, ref string error);
    [OperationContract]
    void ListaDeclaracionesEnValTotal(ref clsConsultaValoracion consulta, ref string error);
    [OperationContract]
    int CantidadDeclaracionesSinValorar(string filtro, string Valor);
    [OperationContract]
    List<clsDeclaracionValoraracion> ListarDeclaracionesAsignadas();
    [OperationContract]
    List<clsValorador> ListarValoradoresDisponibles();
    [OperationContract]
    bool Asignar(List<clsValoracion> asignaciones);
    [OperationContract]
    bool Reasignar(List<clsValoracion> asignaciones);
    [OperationContract]
    List<clsValoradorTareas> ListarValoracionesPorValoradorId(int valoradorId);
    [OperationContract]
    string GuardarValoracion(clsValoracion valoracion, bool finalizar);
    [OperationContract]
    clsValoracion ValoracionPorId(int ValoracionId, bool Completa);
    [OperationContract]
    List<clsDeclaracionInfoValoracion> InformacionDeclaracionPorId(int DeclaracionId);
    [OperationContract]
    List<clsHechosValoracion> HechosPorDeclaracionId(int declaracionId);
    [OperationContract]
    List<clsPersonaAnexo> ListarPersonasPorHecho(int hechoId, int TipohechoId);
    [OperationContract]
    List<clsEstadosValoracion> ListarEstados();
    [OperationContract]
    List<clsObservacionEstado> ListarObservacionEstadoPorEstadoId(int estadoId);
    [OperationContract]
    List<clsPrincipioEstado> ListarPrincipioEstadoPorEstadoId(int estadoId);
    [OperationContract]
    List<clsAutores> ListarAutores();
    [OperationContract]
    List<clsInfracciones> ListarInfracciones();
    [OperationContract]
    List<clsHerramientas> ListarHerramientasPorTipo(int tipo);
    [OperationContract]
    clsHerramientas HerramientaPorId(int id);
    [OperationContract]
    List<clsPersona> ListarPersonasPorDeclaracion(int declaracionId);
    [OperationContract]
    List<clsTipoHerramienta> ListarTiposDeHerramienta();
    [OperationContract]
    clsTipoHerramienta TipoHerramientaPorId(int Id);
    [OperationContract]
    List<clsAutores> ListarAutoresPorAnexo(int ValAnexoPer);
    [OperationContract]
    List<clsInfracciones> ListarInfraccionesPorValPerId(int valAnexoPerId);
    [OperationContract]
    List<clsRegistrosAnteriores> ListarRegistrosAnteriores();
    [OperationContract]
    List<clsPreguntasRegAnt> ListarPreguntasRegAnt();
    [OperationContract]
    bool DeshacerAsignacion(clsValoracion valoracion);
    [OperationContract]
    DataSet getInforme();
    [OperationContract]
    DataSet getResumenPorId(int valId);
    [OperationContract]
    List<clsParametroGeneral> ListarParametros();
    [OperationContract]
    List<clsGeografia> ListarGeografia(int? nivel, int? tipo, int? padre);
    [OperationContract]
    bool AsignarTodos(int usuarioId);
    [OperationContract]
    void ListaTareasValorador(ref clsConsultaValoracion eConsulta, ref string error);
    [OperationContract]
    void ListaTareasValoradorCantidad(ref clsConsultaValoracion eConsulta, ref string error);
    [OperationContract]
    List<clsPrincipioEstado> ListarPrincipios();
    [OperationContract]
    List<clsObservacionEstado> ListarObservacion();
    [OperationContract]
    List<clsHerramientas> ListarHerramientas();
    [OperationContract]
    bool AgregarPersonaService(rex::clsAgregarPersonaValoracion AgregaPerso, ref string cError);
    [OperationContract]
    List<val::clsCargaPersonasAsociadasDeclaracion> CargaDatosPersonasAsociadas(int nIddeclaracion, ref string cError);
    [OperationContract]
    int CargaDatosPersonasAsociadasCount(int nIdDelcaracion, ref string cError);
    [OperationContract]
    int ObtenerIdValoracionporIdDeclaracionServ(int nIdDeclaracion, ref string cError);
    [OperationContract]
    List<clsSubEtnias> ListarSubEtnias(int etniaId);
    [OperationContract]
    USUARIO_BASICO UsuarioPorId(int IdUsuario, ref string error);
    [OperationContract]
    clsConceptoDeclaracion ObtenerConceptoDeclaracion(int idDeclaracion);
    [OperationContract]
    bool InsertaConceptoDeclaracion(clsConceptoDeclaracion conceptoDeclaracion);
}
