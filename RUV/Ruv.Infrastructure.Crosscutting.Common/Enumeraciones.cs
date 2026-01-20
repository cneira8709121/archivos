using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public enum eCodigoAutenticacion
    {
        AutenticacionExitosa = 0,
        UsuarioNoEncontrado = 1,
        UsuarioNoActivo = 2,
        UsuarioBloqueado = 3,
        MaximoDeSesionesPermitidas = 4,
        UsuarioClaveDesconocidos = 5,
        UsuarioSinPermisos = 6
    }

    public enum eTipoHerramientaValoracion
    {
        Juridica = 1,
        Tecnica = 2,
        Contexto = 3
    }

    public enum ePermisosUsuario
    {
        //RegistrarDeclaraciones = 1,
        //Valorar = 2,
        //MinisterioPublico = 3,
        //RadicarDeclaracion = 4

        RUV_Ingresar = 1000,
        Registrar_Declaraciones = 1001,
        Radicar_Declaracion = 1002,
        Valorar = 1003,
        Glosas = 1004,
        Cargar_Lugar_Declaracion = 1005,
        Firma_funcionario_declaracion = 1006,
        Validar_narración_hechos = 1007,
        validar_Enmendar_corregir_declaración = 1008,
        Asignar = 1009,
        Ingresa_Valoracion = 1014,
        Reasignar_Valoracion = 1010,
        Valoracion = 1011,
        Valorar_Declaracion = 1012,
        ObtenerDeclaracion = 1013,
        Ingresar_ActosAdmin = 1015,
        Listar_ActosAdmin = 1016,
        Nuevo_ActosAdmin = 1017,
        Editar_ActosAdmin = 1018,
        Anular_ActosAdmin = 1019,
        Firmar_ActosAdmin = 1020,
        Consultar = 1021,
        Consultar_Persona = 1022,
        Requerir_Validaciones_Obligatorias = 1023,
        Control_Documentos_Ingresar = 1024,
        Control_Documentos = 1025,
        Control_Documentos_WPF = 1026,
        Consulta_Documentos = 1027,
        Requerir_Validaciones_Flexibles = 1028,
        Requerir_Validaciones_Obcionales = 1029,
        Gestion = 1030,
        Gestion_Valoracion = 1031,
        AprobarRechazarValoracion = 1032,
        Solicitar_Correcion = 1033,
        Notificaciones = 1034,
        FirmaActoAdministrativo = 1035,
        NotificaiconesEntregadas = 1036,
        OPCIONES_COLA_PROCESOS = 1037,
        OPCIONES_CARGUE_PARAMETROS = 1038,
        OPCIONES_CONFIGURACION = 1039,
        ModificarPuntoNotificacion = 1040,
        DeclaracionGuardar = 1041,
        DeclaracionCargar = 1042,
        DeclaracionFinalizar = 1043,
        DeclaracionImprimir = 1044,
        FirmaDeclaracion = 1045,
        NotificacionesEntregadas = 1051,
        PaquetesNotificacion = 1046,
        ConfiguracionSistema = 1047,
        ConfiguracionCalendario = 1048,
        PreparadorNotificaciones = 1052,
        ConsultaCentroAtencion = 1053,
        GestionEdicto = 1054,
        TomaVirtual = 1055,
        Test_Lista_Tareas = 9999
    }

    public enum eErrores
    {
        Ninguno,
        Autenticacion,
        NoDeterminado
    }

    public enum eTipoEntorno
    {
        Urbano = 127,
        Rural = 128
    }

    public enum eTipoPoblacion
    {
        Urbano_Barrio = 653,
        Urbano_Localidad = 656,
        Rural_Corregimiento = 651,
        Rural_Vereda = 652
    }


    public enum eTipoParametros
    {
        Ninguno = -2,
        ActividadMomentoHecho = 2144,
        AfiliaciónASalud = 32,
        CausaDeDesplazamiento = 2153,
        DelitosSexuales = 2152,
        DeseoDelHogar = 72,
        DiscapacidadEnActividades = 2135,
        Entidades = 2154,
        EstadoActualLote = 2162,
        EstadoCivil = 22,
        EstadoVictima = 2142,
        EventosAntesDeDesaparición = 2138,
        EventosDespuésDeDesaparición = 2139,
        FinalidadSecuestro = 2146,
        Genero = 24,
        HechosVictimizantes = 2137,
        HuerfanoDe = 2141,
        LiberacionVictimaSecuestro = 2148,
        MinoriaEtnica = 31,
        RegimenEspecial = 2134,
        Relacion = 29,
        SiNo = -1,
        SiNoNsNr = 2160,
        SituacionActualVictimaSecuestro = 2147,
        TipoAcreedorEnDeudasPersona = 2111,
        TipoDeAccidente = 2156,
        TipoDeBien = 2163,
        TipoDeDesplazamiento = 2108,
        TipoDeDocumentoDeIdentidad = 21,
        TipoDeInmueble = 2157,
        TipoDespojo = 2161,
        TipoDeTenencia = 2158,
        TipoEncargado = 2149,
        TipoPerteneciaBienAfectadoAnexo1 = 2136,
        TiposDeAfectacion = 2155,
        TipoSecuestro = 2145,
        UnidadDeArea = 2159,
        GlosaIncompletaDeHogar = 1931,
        FechasIncoherentesInexistenteesOIncompletas = 1932,
        IdentificaciónIncompletaDeLaEntidad = 1933,
        TipoDeDesplazamientoNoCorresponde = 1934,
        NarraciónDeHechosInexistente = 1935,
        NarraciónDeHechosNoCorrespondeAlDeclarante = 1936,
        UbicaciónIncoherente = 1937,
        InconsistenciaDeProceso = 1938,
        CategoríasYConceptosDeGlosas = 1164,
        CategoríasDeIntentosDeGlosa = 1173,
        TipoAfiliacion = 32,
        TipoDeRadicacion = 2171,
        ResultadoValidacionRadicacion = 2172,
        CausalesTodos = 10027,
        CausalesCriticaN = 10026,
        CausalesLiderRadicacion = 10025,
        PreguntaCriticaN = 10028,
        CausalesGlosas = 10033,
        CausalesValoracion = 10034,
        TipoTomaDeclaracion = 2178,
        OrientacionSexual = 2180,
        IdentidadGenero = 2181,
        ConfiguracionRUV = 2179,
        NuevoTipoDesplazamiento = 2182,
        PaisesTransfronterizos = 2183
    }

    public enum eTipoConfiguracionRUV { 
        MensajesTomaEnLinea = 10127
    }

    public enum eTipoDesplazamientoNuevo
    {
        Intramunicipales = 10143,
        Intermunicipal=10144,
        Intraresguardo=10145,
        IntraConsejoCominitario=10146,
        Trasnsnacional=10147,
        Transfronterizo=10148
    }

    public enum eGruposParametros
    {
        Ninguna = -1,
        Entidades_Hoja_1_DP = 1,
        Entidades_Anexo_1_DP = 2,
        Entidades_Anexo_2_DP = 3,
        Entidades_Anexo_3_DP = 4,
        Entidades_Anexo_4_DP = 5,
        Entidades_Anexo_5_DP = 6,
        Entidades_Anexo_6_DP = 7,
        Entidades_Anexo_7_DP = 8,
        Entidades_Anexo_8_DP = 9,
        Entidades_Anexo_9_DP = 10,
        Entidades_Anexo_10_DP = 11,
        Entidades_Anexo_11_DP = 12,
        Afectacion_Anexo_1 = 13,
        Afectacion_Anexo_2 = 14,
        Afectacion_Anexo_3 = 15,
        Afectacion_Anexo_4 = 16,
        Afectacion_Anexo_6 = 17,
        Afectacion_Anexo_7 = 18,
        Afectacion_Anexo_8 = 19,
        Afectacion_Anexo_9 = 20,
        Afectacion_Anexo_10 = 21,
        Afectacion_Anexo_13 = 22
    }

    public enum eOpcionesOtros
    {
        Hechos = 9023,
        //Representante = 9092
        Representante = 5173
    }

    public enum eGaranteTipos
    {
        Tutor = 5172,
        FuncionarioAutoridadCompetente = 5173
    }

    public enum eEstadoRegistro
    {
        SinModificaciones = 0,
        Modificado = 1,
        Insertar = 2,
        Eliminado = 3
    }

    public enum eEstadoDeclaracion
    {
        Ninguno = 0,
        CapturaPendientePorValidar = 10011,
        ValoracionPendientePorAsignar = 702,
        Radicado = 770,
        IniciaCapturaSinRadicar = 694,
        FinalizaCapturaSinRadicar = 696,
        RadicadoPendienteCaptura = 704,
        ValoracionPendientePorValorar = 10000,
        ValoracionEnValoracion = 10001,
        ValoracionPendientePorRevision = 10002,
        NoValoradoDevuelto = 10005,
        RadicacionPendienteCritica5 = 10015,
        RadicacionPendientePorVerificar = 10016,
        DeclaracionDevuelta = 10023,
        DeclaracionPendientePorDevolucion = 10024,
        ValoracionPendientePorFirma = 10030,
        CapturaPendientePorValidarTomaEnLinea = 10108
    }

    public enum eEstadosValoracion
    {
        PendientePorValorar = 1,
        IniciaValoracion = 2,
        PendientePorNotificar = 3,
        NoValoradoDevuelto = 4,
        ValoracionDevueltaAsignacion = 5
    }

    public enum eEstadosValoracionPersona
    {
        Incluido = 1,
        NoIncluido = 2,
        EnValoración = 3,
        Excluido = 4,
        NoValoradoDevuelto = 5,
        NoValoradoAfectado = 6,
        NoValoradoNoAfectado = 7
    }

    public enum eEstadoDescicion
    {
        Incluido = 1,
        NoIncluido = 2,
        EnValoracion = 3,
        Excluido = 4,
        AsociadoAOtro = 5
    }


    public enum eTipoDocumento
    {
        CedulaCiudadania = 110,
        LibretaMilitar = 111,
        TarjetaIdentidad = 112,
        RegistroCivil = 113,
        NoInforma = 114,
        Indocumentado = 115,
        NUIP = 847,
        NIP = 853,
        CedulaExtranjeria = 4401,
        NoSabe = 5134,
        NoResponde = 5135,
        Visa = 10102,
        PermisoEspecialPermanencia = 10103,
        Pasaporte = 10104,
        SalvoconductoPermanencia = 10105,
        CarnetDiplomatico = 10100,
        IndocumentadoExtranjero = 10106,
        CedulaExtranjera = 10107,
        PermisoProteccionEspecial = 10110
    }


    /// <summary>
    /// Estos tipos de documento de identificación NO
    /// cuentan con el respectivo número.
    /// </summary>
    public enum eTipoDocumentoSinNumero
    {
        Indocumentado = 115,
        NoInforma = 114,
        NoSabe = 5134,
        NoResponde = 5135
    }

    public enum eTipoProceso
    {
        Declaracion = 547,
        Valoracion = 549,
        EventosMasivos = 548
    }

    public enum eEstadosGlosas
    {
        CreadaSinAtender = 1,
        AsignadaSinAtender = 2,
        Atendida = 3,
        GlosaPerdida = 4,
        GlosaEliminadaPorAutor = 5
    }

    public enum eModoEjecucion
    {
        Desarrollo = 0,
        Pruebas = 1,
        Produccion = 2,
        Capacitacion = 3
    }

    public enum eRelacion
    {
        Jefe_de_hogar = 143,
        Esposo_Compañero = 144,
        Hijo_Hijastro = 145,
        Yerno_Nuera = 146,
        Nieto = 147,
        Padre_Madre = 148,
        Suegros = 149,
        Hermanos_Cuñados = 150,
        OtrosParientes = 151,
        NoPariente = 152,
        NoSabe_NS = 5136,
        NoResponde_NR = 5137
    }

    /// <summary>
    /// Lista de parámetros que al ser seleccionados deben habilitar controles sobre la interfase.
    /// </summary>
    public enum eParametrosHabilitantes
    {
        SiNoNsNr_Si = 5557,
        DeseoDelHogar_Retornar = 370,
        DeseoDelHogar_Reubicarse = 371,
        CedulaCiudadania = 110,
        LibretaMilitar = 111,
        TarjetaIdentidad = 112,
        RegistroCivil = 113,
        NUIP = 847,
        NIP = 853,
        CedulaExtranjeria = 4401,
        Mujer = 126,
        LGBTI = 5088,
        Intersexual = 10043,
        LIBRE = 5165,
        Visa = 10102,
        PermisoEspecialPermanencia = 10103,
        Pasaporte = 10104,
        SalvoconductoPermanencia = 10105,
        CarnetDiplomatico = 10100,
        IndocumentadoExtranjero = 10106,
        CedulaExtranjera = 10107,
        NoSabe = 5134,
        NoResponde = 5135,
        PermisoProteccionTemporal = 10110,
        Hombre = 125,
        Trasnsnacional = 10147,
        Transfronterizo = 10148,
        Colombia = 48
    }

    public enum eGenero
    {
        Hombre = 125,
        Mujer = 126,
        LGBTI = 5088
    }

    public enum eTipoSecuestro
    {
        SIMPLE = 5160,
        EXTORSIVO = 5161
    }

    public enum eFinalidadSecuestroExtor
    {
        ECONOMICA = 5162,
        POLITICA = 5163,
        Otro = 5576
    }

    public enum eSituacionVictimaSecuestro
    {
        CAUTIVA = 5164,
        LIBRE = 5165,
        MUERTO = 5166
    }

    public enum eEstadoVictimaMinas
    {
        MUERTO = 5145,
        HERIDO = 5146
    }

    public enum eSiNoNsNr
    {
        Si = 5557,
        No = 5558,
        No_sabe = 5559,
        No_responde = 5560
    }

    public enum eDeseoDelHogar
    {
        Permanecer = 369,
        Retornar = 370,
        Reubicarse = 371,
        No_sabe = 5506,
        No_responde = 5507
    }

    public enum eTipoDesplazamiento
    {
        Amenazas_e_intimidaciones = 5494,
        Atentados_a_bienes_e_infraestructuras = 5495,
        Atentados_a_personas = 5496,
        Combates = 5497,
        Desapariciones_forzadas = 5498,
        Enfrentamientos = 5499,
        Homicidios = 5500,
        Masacres = 5501,
        Presencia_minas_antipersonal = 5502,
        Reclutamiento_forzado = 5503,
        Secuestro_toma_rehenes = 5504,
        Otra = 5505
    }

    public enum ePertenenciaEtnica
    {
        Ninguna = 166
    }

    public enum eHechosVictimizantes
    {
        Acto_terrorista_1 = 5116,
        Amenaza_2 = 5117,
        Delitos_contra_sexual_3 = 5118,
        DesaparicionForzada_4 = 5119,
        DesplazamientoForzado_5 = 5120,
        HomicidioMasacre_6 = 5121,
        MinasAntipersonal_7 = 5122,
        Secuestro_8 = 5123,
        Tortura_9 = 5124,
        VinculacionNiñosGruposArmados_10 = 5125,
        AbandonoDespojoForzadoTierras_11 = 5126,
        Otro_12 = 5127,
        NoVictima_13 = 5128
    }

    public enum eTiposAnexos
    {
        Acto_terrorista_1 = 1,
        Amenaza_2 = 2,
        Delitos_contra_sexual_3 = 3,
        DesaparicionForzada_4 = 4,
        DesplazamientoForzado_5 = 5,
        HomicidioMasacre_6 = 6,
        MinasAntipersonal_7 = 7,
        Secuestro_8 = 8,
        Tortura_9 = 9,
        VinculacionNiñosGruposArmados_10 = 10,
        AbandonoDespojoForzadoTierras_11 = 11,
        CensoMasivo_13 = 13
    }

    public enum eTipoAfiliacion
    {
        Regimen_Contributivo = 172,
        Regimen_Subsidiado = 173,
        NoAfiliado = 174
    }

    public enum eTipoDesplazamientoA05
    {
        Masivo = 4345,
        Individual = 4346
    }

    public enum eTiposDeAfectacion
    {
        DañosMueblesInmuebles = 5527,
        Fracturas = 5528,
        HeridasLaceraciones = 5529,
        Infección = 5530,
        Muerte = 5531,
        ParalisisTotalParcial = 5532,
        PerdidaAudicion = 5533,
        PerdidaVista = 5534,
        PerdidaHabla = 5535,
        PerdidaAmputación = 5536,
        PerdidaDiferenteVistaAudiciónHabla = 5537,
        Quemaduras = 5538,
        TrastornosPsicológicosPsiquiátricos = 5539,
        DisminucionPerdidaIngresos = 5540,
        Otro = 5541
    }

    public enum eDiscapacidades
    {
        PensarMemorizar = 5089,
        Percibir_la_luz_a_pesar_de_usar_lentes_o_gafas = 5090,
        Oír_aun_con_aparatos_especiales = 5091,
        DistinguirSaboresOlores = 5092,
        DificultadesParaHablarComunicarse = 5093,
        DesplazarseProblemasRespiratoriosCorazón = 5094,
        MasticarTransformarAlimentos = 5095,
        RetenerExpulsarOrinaTenerRelacionesSexualesTenerHijos = 5096,
        CaminarCorrerSaltar = 5097,
        MantenerPielUñasCabellosSanos = 5098,
        RelacionarseConLasDemasPersonasEntorno = 5099,
        LlevarMoverUtilizarObjetosConLasManos = 5100,
        CambiarMantenerLasPosicionesDelCuerpo = 5101,
        AlimentarseAsearseVestirsePorSiMismo = 5102,
        Otra = 5103,
        Ninguna = 5104,
        NoSabe_NS = 5105,
        NoResponde_NR = 5106,
        NoSabe_NS_NoResponde_NR = 2133
    }

    public enum ePaises
    {
        Colombia = 48
    }

    public enum eEntidadAtiende
    {
        Procuraduria = 5510,
        Defensoria = 5511,
        Personeria = 5512,
        Fiscalia = 5513,
        MedicinaLegal = 5514,
        InspeccionPolicía = 5515,
        Policia = 5516,
        PersoneriaMunicipal = 5517,
        DefensoriaDelPueblo = 5518,
        NoSabeONoResponde = 5520,
        MinisterioDelInterior = 5521,
        ComisiónNalBusquedaPersonasDesaparecidas = 5522,
        DespachoJudicial = 5523,
        Otro = 5524,
        PAICMA = 5525,
        DireccionOperativaDefensaLibertadPersonal = 5526,
        Consulado = 10005
    }


    public enum eEstadoActoAdmin
    {
        Generado = 10011,
        Firmado = 10012,
        Anulado = 10013
    }

    public enum eEstadoValidacion : int
    {
        NoAplica = 0,    // Parametro es opcional, para todos los roles
        Flexible = 1,    // Parametro que NO es obligatorio para digitadores
        Obligatoria = 2  // Parametro obligatorio para todos los roles
    }

    [DataContract]
    public enum eEstadoFormulario
    {
        [EnumMember]
        INACTIVO = 1,
        [EnumMember]
        ASIGNADO = 2,
        [EnumMember]
        IMPRENTA = 3,
        [EnumMember]
        GENERADO = 4,
        [EnumMember]
        RADICADO = 5,
        [EnumMember]
        DEVULETO = 6
    }

    public enum eAccionEnFormulario : int
    {
        Activar = 1,
        Inactivar = 2
    }

    public enum eTipoConsulta
    {
        Listado,
        Total
    }

    public enum eTipoListadoCaptura
    {
        CriticaNPregunta = 1,
        CriticaNCausal = 2
    }

    /// <summary>
    /// Causales de necesidad de verificacion de radicación 
    /// </summary>
    public enum eResultadoValidacionRadicacion
    {
        /// <summary>
        /// La radicación se encuentra completa
        /// </summary>
        validacionCorrecta = 10017,
        /// <summary>
        /// No tiene numero de formulario.
        /// </summary>
        faltaNumeroFormulario = 10018,
        /// <summary>
        /// Numero de formulario invalido.
        /// </summary>
        NumeroFormularioInvalido = 10019,
        /// <summary>
        /// Procedencia de formulario no corresponde con asignación.
        /// </summary>
        ProcedenciaErronea = 10020,
        /// <summary>
        /// El numero de formulario ya se encuentra radicado, por lo que hay que enviar al lider de Radicación
        /// </summary>
        NumeroFormularioRadicado = 10021,
        /// <summary>
        /// El numero de formulario se encuentra inactivo
        /// </summary>
        NumeroFormularioInactivo = 10022,
        /// <summary>
        /// El numero de formulario no ha sido asignado a un territorio
        /// </summary>
        NumeroFormularioNoAsignado = 10032
    }

    public enum eTipoCausal
    {
        Todos = 10027,
        CriticaN = 10026,
        LiderRadicacion = 10025,
        Glosas = 10033,
        Valoracion = 10034
    }

    public enum eTipoRadicacion : int
    {
        RadicacionDeclaracion = 10012,
        RadicacionDevolución = 10013
    }

    public enum eObservacionEstado : int
    {
        ActivoIncluido = 1,
        InactivoIncluido = 2,
        ActivoNoIncluido = 3,
        InactivoNoIncluido = 4
    }

    public enum eTipoDocumentoValoracion
    {
        [Description("Incluido")]
        Incluido = 1,
        [Description("NoIncluido")]
        Excluido = 2,
        [Description("Mixto")]
        Mixto = 3
    }

    public enum eCamposCorreccion
    {
        PrimerNombre = 1,
        SegundoNombre = 2,
        PrimerApellido = 3,
        SegundoApellido = 4,
        TipoDocumento = 5,
        Documento = 6,
        FechaNacimiento = 7,
        Genero = 8,
        Etnia = 9,
        Discapacidades = 10,
        Direccion = 11,
        Telefono = 12,
        CorreoElectronico = 13,
        SubEtnia = 14,
        Fallecido = 15
    }

    public enum eEstadosNotificacion
    {
        /* Estados de Notificaciones - Preparación y Envío */
        /// <summary>
        /// No se ha firmado por parte del jefe de registro
        /// </summary>
        EnFirmaAAdministrativo = 0,
        /// <summary>
        /// Cuando se valida la información para que pase al líder de notificaciones
        /// </summary>
        CorreccionInformacion = 1,
        /// <summary>
        /// La notificación está lista para agregarse un paquete
        /// </summary>
        PendienteEnvio = 2,
        /// <summary>
        /// Notificación en proceso (paquete), en espera de confirmación de envío
        /// </summary>
        EnvioPorConfirmar = 3,
        /// <summary>
        /// Se le envió el paquete a 4-72
        /// </summary>
        Enviado = 4,
        /* Estados de Notificaciones - Respuestas Courier */
        NotificacionEntregada = 5,
        NotificacionRechazada = 6,
        NotificacionEnProceso = 7,
        NotificacionEstadoPorValidar = 8,
        /* Estados de Notificaciones - Entrega y Términos */
        NotificadoPersonal = 10,
        PendientePublicacion = 11,
        EdictoPublicado = 12,
        PendienteDespublicacion = 13,
        NotificadoEdicto = 14,
        PendienteEnvioresolucion = 15,
        NotificadoResolucion = 16
    }

    public enum eRolesUsuario
    {
        RuvDigitador = 90,
        TomaEnLinea = 91,
        Glosas = 1008,
        LiderNotificaciones = 1019,
        PreparadorNotificaciones = 1022,
        TomaVirtual = 1025
    }

    public enum eTipoLey
    {
        LeyNueva = 1,
        LeyVieja = 0
    }

    public enum ePuntoNotificacion
    {
        PuntoAtencion = 0,
        DireccionTerritorial = 1,
        Personeria = 2
    }

    public enum eTipoAnexo11 : int
    {
        Inmueble = 1,
        Mueble = 2,
        Credito = 3
    }

    public enum eTipoTomaDeclaracion : int
    {
        Presencial = 10094,
        Virtual = 10095
    }
}
