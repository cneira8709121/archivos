using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ConstantesSesion
{
    public const string USUARIO = "Usuario";
    public const string USUARIO_APP = "UsuarioApp";
    public const string USUARIO_ID_LOGIN = "UsuarioId";
}

public class ConstantesAplicacion
{
    public const string ASIGNAR = "AsignarDeclaraciones";
}

public class ConstantesItems
{
    public const string USUARIO_ID = "UsuarioId";
    public const string PERFIL_ID = "PerfilId";
    public const string DECLARACION_ID = "DeclaracionId";
    public const string DECLARACIONES_NO_VAL = "DeclaracionesSinValorar";
    public const string DECLARACIONES_ASIGNADAS = "DeclaracionesAsignadas";
    public const string VALORADORES = "Valoradores";
    public const string TAREAS_VALORADOR = "TareasValorador";
    public const string VALORACION_ID = "ValoracionId";
    public const string VALORACION_ANEXO = "ValoracionAnexo";
    public const string PERSONA_ANEXO = "PersonaAnexo";
    public const string VALORACION_ETAPA = "EtapaValoracion";
    public const string VALORACION_ANEXO_GUARDADO = "PersonaGuardado";
    public const string VALORACION_PERSONA_GUARDADA = "PersonaGuardada";
    public const string VALORACION_ANEXO_ULTIMO = "UltimoAnexo";
    public const string VALORACION_PERSONA_ULTIMA = "UltimaPersona";
    public const string VALORACION_ANEXO_ID = "HechoId";
    public const string VALORACION_PERSONA_GRILLA = "Grilla";
    public const string VALORACION_AUTORES = "Autores";
    public const string HERRAMIENTAS = "Herramientas";
    public const string VALORACION = "Valoracion";
    public const string VALORACION_REPLICA = "Replica";
    public const string HECHO = "Hecho";
    public const string ACTOS_ADMIN = "ActosAdminis";
    public const string GENERALES_DATOS = "DatosGeneralesValoracion";
    public const string ERROR = "Error";
}
public class ConstatesFiltros
{
    public const string PRIMER_NOMBRE = "PrimerNombre";
    public const string SEGUNDO_NOMBRE = "SegundoNombre";
    public const string PRIMER_APELLIDO = "PrimerApellido";
    public const string SEGUNDO_APELLIDO = "SegundoApellido";
    public const string TIPO_DOCUMENTO = "TipoDocumento";
    public const string DOCUMENTO = "NumeroDocumento";
    public const string IDENTIFICACION = "Id";
}

public class ConstatesControlDocumentos
{
    public const string DOCUMENTOS_FORMULARIO = "DocumentosFormulario";
}

public class ConstantesTipoDocumento
{
    public const string Resoluciones = "06";
    public const string Recursos = "03";
}

public class ConstantesEstadoConceptoValoracion
{
    public const string INGRESADO = "INGRESADO";
    public const string ANULADO = "ANULADO";
}

public class ConstantesTipoGenero
{
    public const string Hombre = "Hombre";
    public const string Mujer = "Mujer";
}

public class ConstantesFuentesInformacion
{
    public const string ICBF = "VICTIMA_ICBF";
    public const string SIPOD = "SIPOD";
    public const string FONDOLIBERTAD = "VICTIMA_FONDOLIBERTAD";
}

public class ConstantesEstadoConcepto
{
    public const string Firmadas = "Firmadas";
    public const string NoFirmadas = "NoFirmadas";
}

public class ConstantesCodigoSipod
{
    public const string CodigoSipodNoInclusion = "CODIGO_SIPOD";
    public const string CodigoSipodExtemporaneidad = "CodigoSipod";
    public const string CodigoSipodViagubernativa = "CODIGO_SIPOD";
}

public class ConstantesListaTareas
{
    public const string ListasTareas = "LISTA_TAREAS";
}

public class ConstantesCorrecciones
{
    public const string DatosCorreccionActuales = "Correcciones.DatosCorreccionActuales";
    public const string DatosCorreccionNuevos = "Correcciones.DatosCorreccionNuevos";
    public const string CamposCorreccionNuevos = "Correcciones.CamposCorreccionNuevos";
    public const string IdCorreccion = "Correcciones.IdCorreccion";
    public const string IdRegPersona = "Correcciones.IdRegPersona";
}

public class Herramientas
{

}

public class Cargos
{
    public const string JefeRegistro = "JEFE REGISTRO";
}

public class ConstantesNotificaciones
{
    public const string Notificaciones = "NOTIFICACIONES";
    public const string TotalRegistros = "NOTIFICACIONES.TOTALREGISTROS";
    public const string IdsNotificaciones = "NOTIFICACIONES.IDSNOTIFICACIONES";
    public const string NotificacionesPaquete = "NOTIFICACIONES.NOTIFICACIONESPAQUETE";
}

public enum Correos
{
    RecordarContraseña = 1
}

public enum TiposDocumentos
{
    Recurso = 1,
    Tutela = 2,
    NoInclusion = 3,
    Revaloracion = 4
}

public enum CodigosPrograma
{
    RNI = 1,
    ViaGubernativa = 3,
    Valoracion = 4
}

public enum Poblar
{
    EstadosValoracion = 1,
    PrincipioValoracion = 2,
    ObservacionesValoracion = 3,
    Parametros = 4,
    Autores = 5,
    Infracciones = 6,
    Herramientas = 7,
    TipoHerramientas = 8,
    RegistrosAnteriores = 9,
    PreguntasRegAnteriores = 10,
    PersonasDeclaracion = 11,
    Geografia =12,
    DocumentosActosAd = 13,
    CausalesDevolucion = 14,
    EntidadesMunicipio = 15,
    Paises = 16,
    SubEtnias = 17,
    HechoEnmarcado = 18,
    DecretoLey = 19
}

public enum TipoGeografia
{
    Pais = 1,
    Departamento = 2,
    Municipio = 3,
    Entorno = 4,
    LocCorr = 5,
    BarrioVereda = 6
}

public enum NivelesGeografia : int
{
    Pais,
    Departamento,
    Municipio
}


public enum TipoEntorno
{
    Rural = 128,
    Urbano = 127
}

public enum Tratos
{
    Trato_1,
    Trato_2,
    Trato_3,
    Trato_4,
    Trato_5,
    Trato_6,
    Trato_7
}

public enum Filtros
{
    NombreDeclarante = 1,
    DocumentoDeclarante = 2,
    FechaRadicado = 3,
    NumeroFormulario = 4,
    TotalHv = 5,
    Departamento = 6,
    Municipio = 7,
    Entidad = 8,
    NombreDeclaranteReasignar = 9,
    DocumentoDeclaranteReasignar = 10,
    FechaRadicadoReasignar = 11,
    NumeroFormularioReasignar = 12,
    TotalHvReasignar = 13,
    DepartamentoReasignar = 14,
    MunicipioReasignar = 15,
    EntidadReasignar = 16,
    ValoradorReasignar = 17,
    DeclaranteValorar = 18,
    DocumentoDeclaranteValorar = 19,
    FechaRadicacionValoracion = 20,
    NumeroFormularioValoracion = 21,
    HechosVictimizantesValoracion = 22,
    TotalHvValoracion = 23,
    FechaAsignacionValoracion = 24,
    EstadoValoracion= 25
}

public enum Proceso
{
    Asignacion = 1,
    Reasignacion = 2,
    Valoracion = 3,
    ActoAdmin = 4,
    ListaTareas = 5,
    Notificaciones = 6
}

public enum ValoresDropDownList : int
{
    NoSeleccion = 0,
    OtroValor = Int32.MaxValue
}

public enum HerramientasNoAplica : int
{
    NoAplicaJuridica = 130,
    NoAplicaTecnica = 131,
    NoAplicaContexto = 132
}

