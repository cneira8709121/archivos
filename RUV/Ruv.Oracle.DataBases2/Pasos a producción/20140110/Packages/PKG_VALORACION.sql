-- Create PKG_VALORACION package
create or replace PACKAGE PKG_VALORACION AS

  -- Author  : GRAIG.LUQUE
  -- Created : 3/20/2012 10:05:11 AM
  -- Purpose : RUV Valoracion

  TYPE cursor_type IS REF CURSOR;

  --ESTADOS  RECONOCIMIENTO
  RADICADO_PENDIENTE_CAPTURA NUMBER := 704;
  RADICADO_INICIA_CAPTURA    NUMBER := 737;
  VALORACION_PEND_PORASIGNAR NUMBER := 702;
  VALORACION_PEND_PORVALORAR NUMBER := 10000;
  VALORACION_EN_VALORACION   NUMBER := 10001;
  --VALORACION_FINALIZA_VAL    NUMBER := 10002;
  VALORACION_PEND_REVISION   NUMBER := 10002;
  VALORACION_NOVAL_DEVUELTA  NUMBER := 10003;
  PROCESO_DECLARACION        NUMBER := 547;

  --ESTADOS VALORACION
  VALORACION_ASIGNADA        NUMBER := 1;
  VALORACION_EN_PROCESO      NUMBER := 2;
  VALORACION_FINALIZADA      NUMBER := 3;
  VALORACION_DEVUELTA        NUMBER := 4;
  VALORACION_DEVUELTA_ASI    NUMBER := 5;
  VALORACION_PENDIENTE_REV   NUMBER := 6;


  --Estado Valoracion Persona
  Incluido                   NUMBER := 1;
  NoIncluido                 NUMBER := 2;
  EnValoracion               NUMBER := 3;
  Excluido                   NUMBER := 4;
  NoValoradoDevuelto         NUMBER := 5;
  AfectadoNoValorado         NUMBER := 6;
  NoAfectadoNoValorado       NUMBER := 7;

  --Rol Valorador
  VALORADOR_ROL              NUMBER := 1002;
  LIDER_VALORACION           NUMBER := 1015;
  LIDER_NOTIFICACION         NUMBER := 1019;
  PREPARADOR_NOTIFICACION    NUMBER := 1022;

  --Hechos Victimizantes
  HECHO_VICTIMIZANTE         NUMBER := 2137;

  --Parametro Afectacion
  AFECTACIONES               NUMBER := 2155;

  --Parametro Preguntas Registros Anteriores
  PREGUNTA_REGISTRO_ANT      NUMBER := 2164;


  --ESTADOS ACTOS ADMINISTRATIVOS
  GENERADO  NUMBER  := 2;
  APROBADO  NUMBER  := 3;
  FIRMADO   NUMBER  := 4;
  RECHAZADO NUMBER  := 5;

  --ESTADOS DECLARACION POR VALORACION
  APROPENDNOTI NUMBER := 10031;
  VALPENDREV   NUMBER := 10029;
  VALPENDFIR   NUMBER := 10030;

  --ESTADO DE NOTIFICACION
  PENDIENTE_ENVIO NUMBER := 2;

  --HECHOS AGREGADOS EN VALORACION
  HECHOAGREGADOVALORACION NUMBER := 1;

  --Procedimientos
  PROCEDURE sp_getDeclaracionesSinValorar
  (
    cu_result  OUT cursor_type
  );

/*  DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
**  (NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
**  AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
**  FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
**  CAMBIOS:
**    20130312 - JAIRO VALDERRAMA
**    1. SE ADICIONAN PARAMETROS P_Criterio Y P_Valor CON EL FIN DE GARANTIZAR
**    EL FILTRO EN LA BUSQUEDA
*/
PROCEDURE  sp_getDeclaSinValorarPaginada
(
  P_FilaInicial    NUMBER,
  P_FilaFinal      NUMBER,
  P_Orden          VARCHAR2,
  P_Criterio       VARCHAR2 DEFAULT NULL,
  P_Valor          VARCHAR2 DEFAULT NULL,
  P_Result         OUT SYS_REFCURSOR
);

/*  DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
**  (NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
**  AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
**  FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
**  CAMBIOS:
**    20130312 - JAIRO VALDERRAMA
**    1. SE ADICIONAN PARAMETROS P_Criterio Y P_Valor CON EL FIN DE GARANTIZAR
**    EL FILTRO EN LA BUSQUEDA
*/
PROCEDURE  sp_getDeclaSinValorarCantidad
(
  P_Criterio       VARCHAR2 DEFAULT NULL,
  P_Valor          VARCHAR2 DEFAULT NULL,
  P_Cantidad       OUT NUMBER
) ;
  PROCEDURE sp_getDetallesDeclaracion
  (
      v_Declaracion IN NUMBER,
      c_result OUT SYS_REFCURSOR
  );

  PROCEDURE sp_getDeclaracionesValorando
  (
    cu_result  OUT cursor_type
  );

  PROCEDURE sp_GetDeclaracionesValPaginada(pi_Orden        IN VARCHAR2
                                         , pi_Filtro       IN VARCHAR2
                                         , pi_RegInicial   IN NUMBER
                                         , pi_TamanoPagina IN NUMBER
                                         , po_Resultado    OUT SYS_REFCURSOR);
                                         
  PROCEDURE sp_GetDeclaracionesValCantidad(pi_Filtro    IN VARCHAR2
                                         , po_Resultado OUT NUMBER);
  
  PROCEDURE sp_getValoracionPorID (p_IdVal   IN NUMBER
                                 , cu_Result OUT CURSOR_TYPE);
                                 
  PROCEDURE sp_getValoracionPorDeclaracion (p_IdVal   IN NUMBER
                                          , cu_Result OUT CURSOR_TYPE);
                                          
  PROCEDURE sp_getValoradores
  (
     cu_result  OUT cursor_type
  );

  PROCEDURE sp_AsignarValoracion
  (
    --Id=0, DeclaracionId= decla, EstadoId=1, ValoradorId=valoradorId, AsignadorId=asignadorId
     P_ID_Valoracion IN OUT NUMBER,
    P_ID_DECLARACION IN NUMBER,
    P_ID_VALORACION_ESTADO IN NUMBER,
    P_ID_VALORADOR IN NUMBER,
    P_ID_ASIGNADOR IN NUMBER
  );

  PROCEDURE sp_CrearValoracion(p_Id_Valoracion        IN OUT NUMBER
                             , p_Id_Declaracion       IN NUMBER
                             , p_Id_Valoracion_Estado IN NUMBER
                             , p_Id_Valorador         IN NUMBER
                             , p_Id_Asignador         IN NUMBER);

  PROCEDURE sp_DeterminarNotificacion(p_Id_Valoracion IN NUMBER);

 PROCEDURE sp_getValoracionesPorValorador
 (
   P_ValoradorId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 );

 PROCEDURE  sp_getDeclaraValoradorPaginado
  (
    Pi_ValoradorId  IN NUMBER,
    Pi_Orden        IN VARCHAR2,
    Pi_Filtro       IN VARCHAR2,
    Pi_RegInicial   IN NUMBER,
    Pi_TamanoPagina IN NUMBER,
    Po_Resultado    OUT SYS_REFCURSOR
  );

  PROCEDURE  sp_getDeclaraValoradorCantidad
  (
    Pi_ValoradorId  IN NUMBER,
    Pi_Filtro       IN VARCHAR2,
    Po_Resultado    OUT NUMBER
  );

PROCEDURE sp_getInfoDeclaracion
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);

PROCEDURE sp_getHechosPorValoracion
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;

 PROCEDURE sp_GetHerraPorAnexoPerId
 (
   P_ValAnexPerId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 );

  PROCEDURE sp_GetHerramientasPorTipoId
 (
   P_TipoId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 );
 PROCEDURE sp_GetHerramientas
 (
   P_Result OUT SYS_REFCURSOR
 );

 PROCEDURE sp_GetTiposHerramienta
 (
   P_Result OUT SYS_REFCURSOR
 );
 PROCEDURE sp_GetTipoHerramientaPorId
 (
   P_Id     IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 );

PROCEDURE sp_GetPersonasPorAnexo
(
  P_ValoracionAnexoId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);

PROCEDURE sp_GetEstadosValPersona
(
  P_Result OUT SYS_REFCURSOR
);
PROCEDURE sp_GetObservaciones
(
  P_Result OUT SYS_REFCURSOR
);
PROCEDURE sp_GetObservacionesPorEstadoId
(
  P_EstadValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);

PROCEDURE sp_GetPrincipiosPorEstado
(
  P_EstadoId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;
PROCEDURE sp_GetPrincipios
(
  P_Result OUT SYS_REFCURSOR
) ;
PROCEDURE sp_GetPrincipioPorVal
(
  P_ValId             IN NUMBER,
  P_Result            OUT SYS_REFCURSOR
);
PROCEDURE sp_GetPrincipioPorValAnexoPer
(
  P_ValAnexoPersonaId IN NUMBER,
  P_Result            OUT SYS_REFCURSOR
) ;

PROCEDURE sp_GetAfectacionesPorPersona
(
  P_PersonaId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);

PROCEDURE sp_GetAfectacionesPorId
(
  P_Id IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);

PROCEDURE sp_GetAutores
(
  P_Result OUT SYS_REFCURSOR
) ;

PROCEDURE sp_GetAutoresPorValAnexoPerId
(
  P_AnexoPersonaId IN NUMBER,
  P_Result         OUT SYS_REFCURSOR
);


PROCEDURE sp_GetInfracciones
(
  P_Result OUT SYS_REFCURSOR
) ;
PROCEDURE sp_GetInfraccionesPorAnexoPer
(
  P_ValAnexoPerId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;

PROCEDURE sp_GetInfraccionesValAnexoPer
(
  P_ValAnexoPerId  IN NUMBER,
  P_Result OUT SYS_REFCURSOR
);
  PROCEDURE sp_ActualizarValoracion (p_Id                     IN NUMBER
                                   , p_EstadoId               IN NUMBER
                                   , p_FechaAsignacion        IN DATE
                                   , p_ValoradorId            IN NUMBER
                                   , p_AsignadorId            IN NUMBER
                                   , p_FechaValoracion        IN DATE
                                   , p_FechaRealValoracion    IN DATE
                                   , p_Motivacion_Inclusion   IN CLOB
                                   , p_Motivacion_NoInclusion IN CLOB
                                   , p_ResuelveArticulo1      IN CLOB
                                   , p_ResuelveArticulo2      IN CLOB
                                   , p_EsDeclaracion          IN NUMBER
                                   , p_Observacion            IN CLOB
                                   , p_Finalizar              IN NUMBER
                                   , p_CantidadAfectadas      OUT NUMBER);
PROCEDURE sp_ActualizarValAnexo
(
  P_Id IN NUMBER,
  P_UltimaFechaEdicion IN DATE,
  P_CantidadAfecadas OUT NUMBER
) ;
PROCEDURE sp_GetValAnexoPorId
(
  P_Id IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;

PROCEDURE sp_InsertarTbValHerramienta
(
 P_ID_VAL_ANEXO_PER       IN NUMBER,
 P_ID_HERRAMIENTA         IN NUMBER,
 P_DETALLES               IN CLOB,
 P_FECHA                  IN DATE,
 P_USAPARADESICION        IN NUMBER,
 P_AFECTADAS              OUT NUMBER
);


PROCEDURE sp_EliminarTbHerrAnexo
(
  P_Id            IN NUMBER,
  P_Afectadas     OUT NUMBER
) ;

PROCEDURE  sp_InsertarTbHerrVal
(
  P_ID             OUT NUMBER,
  P_ID_TIPO_HERR   IN NUMBER,
  P_NOMBRE         IN VARCHAR2,
  P_TEXTO          IN VARCHAR2
);


PROCEDURE sp_ActualizarValAnexoPersona
(
  P_ID                    IN NUMBER,
  P_ID_REGPERSONA         IN NUMBER DEFAULT NULL,
  P_ID_OBSERVACION_VAL    IN NUMBER DEFAULT NULL,
  P_ID_ESTADO_VAL         IN NUMBER DEFAULT NULL,
  P_ESVICITMA             IN NUMBER DEFAULT NULL,
  P_ESAFECTADO            IN NUMBER DEFAULT NULL,
  P_ID_VAL_ANEXO          IN NUMBER DEFAULT NULL,
  P_OBSERVACION           IN CLOB,
  P_AFECTADAS             OUT NUMBER
);

PROCEDURE sp_GetValAnexoPersonaPorId
(
  P_ID                    IN NUMBER,
  P_RESULT                OUT SYS_REFCURSOR
);

PROCEDURE sp_InsertarTbAfectacionVal
(
  P_id_valanexoperson     IN NUMBER,
  P_param_afectacion      IN NUMBER,
  P_Afectadas             OUT NUMBER
);
PROCEDURE sp_EliminarTbAfectacionVal
(
  P_ID             IN NUMBER,
  P_AFECTADAS      OUT NUMBER
);
PROCEDURE  sp_InsertarTbAutorHvAnexoPer
(
  P_ID_AUTOR          IN NUMBER,
  P_ID_VAL_ANEXO_PER  IN NUMBER,
  P_Afectadas         OUT NUMBER
);

PROCEDURE  sp_EliminaTbAutorValAnexoPer
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
);

PROCEDURE  sp_InsertarTbInfraccionDIH
(
  P_ID_INFRACCIONDIH      IN NUMBER,
  P_ID_VAL_ANEXO_PER      IN NUMBER,
  P_AFECTADAS             OUT NUMBER
);
PROCEDURE  sp_EliminaTbInfraccionDIH
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
);
PROCEDURE  sp_InsertarTbPrincipioVal
(
  P_ID_PRINCIPIO          IN NUMBER,
  P_ID_VAL_ANEXO_PER      IN NUMBER,
  P_AFECTADAS             OUT NUMBER
);
PROCEDURE  sp_InsertarTbValoracionPri
(
  P_ID_PRINCIPIO          IN NUMBER,
  P_IDVALORACION          IN NUMBER,
  P_AFECTADAS             OUT NUMBER
);
PROCEDURE  sp_EliminaTbPrincipioVal
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
);
PROCEDURE  sp_EliminaTbCausalValoracion
(
  P_ID_Valoracion  IN NUMBER,
  P_AFECTADAS      OUT NUMBER
);
PROCEDURE sp_getRegistrosAnteriores
(
  P_Result OUT SYS_REFCURSOR
) ;

PROCEDURE sp_getPreguntasRegAnteriores
(
  P_Result         OUT SYS_REFCURSOR
) ;
PROCEDURE sp_getRegistrosAntPorValId
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;
PROCEDURE sp_getPersonasPorRegValId
(
  P_RegValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;
PROCEDURE sp_getPreguntasPorRegValId
(
  P_RegValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
) ;

  PROCEDURE sp_InsertarRegistroAnterior(p_RegistroId   IN NUMBER
                                      , p_ValoracionId IN NUMBER
                                      , p_Id           OUT NUMBER);

  PROCEDURE sp_ActualizarRegistroAnterior(p_Id           IN NUMBER
                                        , p_RegistroId   IN NUMBER
                                        , p_ValoracionId IN NUMBER);

  PROCEDURE sp_EliminarRegistroAnterior(p_Id IN NUMBER);
  
  PROCEDURE sp_InsertarRegistroAntPersona(p_RegistroAnteriorId IN NUMBER
                                        , p_RegPersonaId       IN NUMBER);
  
  PROCEDURE sp_InsertarRegistroAntPregunta(p_RegistroAnteriorId IN NUMBER
                                         , p_PreguntaId         IN NUMBER);

PROCEDURE sp_getResumenValoracion
(
  P_ValoracionID IN NUMBER,
  P_ResultDeclaracion OUT SYS_REFCURSOR,
  P_ResultHechos OUT SYS_REFCURSOR,
  P_ResultPersonas OUT SYS_REFCURSOR
);

PROCEDURE sp_GetGeografia
(
  P_Result  OUT SYS_REFCURSOR
) ;
PROCEDURE sp_CrearHecho
(
  P_TipoHecho             IN NUMBER,
  P_Fecha                 IN DATE,
  P_Departamento          IN NUMBER,
  P_Municipio             IN NUMBER,
  p_TipoEntorno           IN NUMBER,
  P_CorrLoc               IN NUMBER,
  P_BarrVer               IN NUMBER,
  p_OtroCorLoc            VARCHAR2,
  P_OtroBarVer            VARCHAR2,
  P_Victima1              IN NUMBER,
  P_Valoracion            IN NUMBER,
  P_ValAnexo              OUT NUMBER
);
PROCEDURE sp_CrearAnexo
(
  P_ValAnexoId             IN NUMBER,
  P_RegPersona             IN NUMBER,
  P_EstadoHecho            IN NUMBER
);
PROCEDURE sp_AsignarValoracionAutomatico
(
  P_ID_Declaracion        NUMBER,
  P_ID_UsuarioAsigna      NUMBER
);
PROCEDURE sp_GetValoracionFull
(
  Pi_IdValoracion           IN NUMBER,
  Po_DetalleDeclaracion     OUT SYS_REFCURSOR,
  Po_Principios             OUT SYS_REFCURSOR,
  Po_RegistrosAnteriores    OUT SYS_REFCURSOR,
  Po_Hechos                 OUT SYS_REFCURSOR,
  Po_Personas               OUT SYS_REFCURSOR
);
PROCEDURE SP_CONSULTAVALORADORES(PI_PAGENUMBER IN NUMBER,
                                 PI_PAGESIZE IN NUMBER,
                                 PO_CURSOR OUT CURSOR_TYPE);

PROCEDURE SP_CONSULTAVALORADORDETALLE(PI_VALORADORID IN NUMBER,
                                        PI_FECHASOLICITUD IN DATE,
                                        PI_PAGENUMBER IN NUMBER,
                                        PI_PAGESIZE IN NUMBER,
                                        PO_CURSOR OUT CURSOR_TYPE
                                       );

PROCEDURE SP_AUTOASIGNARVALORACION( PI_IDDECLARACION IN NUMBER);

PROCEDURE SP_CONSULTAVALORADORESCOUNT(PO_RECORDCOUNT OUT NUMBER);

 PROCEDURE SP_DETALLEVALORADORCOOUNT(PI_VALORADORID IN NUMBER,
                                      PI_FECHASOLICITUD IN DATE,
                                      PO_RECORDCOUNT OUT NUMBER);

PROCEDURE SP_IDVALDESDEIDDECLA(PI_IDDECLARACION IN NUMBER,
                               PO_IDVALORACION OUT NUMBER
                              );

/*  DESCRIPCION:
**  AUTOR:
**  FECHA:
**  CAMBIOS:
**    20121222 - JAIRO VALDERRAMA
**    1. SE AGREGA EL N脷MERO DEL FORMULARIO DE LA VALORACION A LA CONSULTA RESULTANTE
*/
PROCEDURE SP_RESUMENVALORACION(pi_IdDeclaracion IN NUMBER,
                               PO_CURSOR        OUT CURSOR_TYPE
                              );

PROCEDURE SP_APROBARVALORACION(  PI_IDUSUARIO IN NUMBER,
                                 PI_IDDECLARACION IN NUMBER,
                                 PI_OBSERVACION IN VARCHAR2
                              );

PROCEDURE SP_RECHAZARVALORACION( PI_IDUSUARIO IN NUMBER,
                                 PI_IDDECLARACION IN NUMBER,
                                 PI_OBSERVACION IN VARCHAR2
                               );

PROCEDURE SP_INSERTAHISTORICOVAL(PI_IDUSUARIO IN NUMBER,
                                 PI_IDVALORACION IN NUMBER,
                                 PI_OBSERVACION IN VARCHAR2
                                );

PROCEDURE SP_INSERTATIPOMOTIVACION(PI_IDVALORACION IN NUMBER,
                                     PI_TIPOMOTIVACION IN VARCHAR2 DEFAULT NULL);


  PROCEDURE sp_ObtieneTipoMotivacion(pi_IdValoracion   IN NUMBER
                                   , po_TipoMotivacion OUT VARCHAR2);


  PROCEDURE sp_AgregaPersona(pi_PrimerNombre         IN VARCHAR2
                           , pi_SegundoNombre        IN VARCHAR2 DEFAULT NULL
                           , pi_PrimerApellido       IN VARCHAR2
                           , pi_SegundoApellido      IN VARCHAR2 DEFAULT NULL
                           , pi_TipoDocumento        IN NUMBER   DEFAULT NULL
                           , pi_NumeroDocumento      IN VARCHAR2 DEFAULT NULL
                           , pi_Param_EstadoCivil    IN NUMBER   DEFAULT NULL
                           , pi_Param_Genero         IN NUMBER   DEFAULT NULL
                           , pi_Param_MinoriaEtnica  IN NUMBER   DEFAULT NULL
                           , pi_Gestante             IN NUMBER   DEFAULT NULL
                           , pi_FechaNacimiento      IN DATE     DEFAULT NULL
                           , pi_EsMujerCabezaDeHogar IN NUMBER   DEFAULT NULL
                           , pi_Comunidad            IN VARCHAR2 DEFAULT NULL
                           , pi_IdCreado             OUT NUMBER);

  PROCEDURE sp_AgregaRegPersona(pi_IdDeclaracion        IN NUMBER
                              , pi_IdPersona            IN NUMBER
                              , pi_CDireccion           IN VARCHAR2
                              , pi_NTelefono            IN VARCHAR2
                              , pi_Relacion             IN NUMBER
                              , pi_CorreoElectronico    IN VARCHAR2
                              , pi_EsMujerCabezaDeHogar IN NUMBER
                              , pi_RegimenEspecial      IN NUMBER
                              , pi_Gestante             IN NUMBER
                              , pi_Observacion          IN VARCHAR2
                              , pi_IdCreado             OUT NUMBER);

  PROCEDURE sp_AgregaDiscapacidadValora(pi_IdRegPersona IN NUMBER, pi_Discapacidad IN NUMBER);

  PROCEDURE SP_CARGAPERSONASASOCIADAS(PI_IDDECLARACION IN NUMBER,
                                    PO_CURSOR OUT CURSOR_TYPE);

  PROCEDURE SP_CARGAPERSONASASOCIADASCOUNT(PI_IDDECLARACION IN NUMBER,
                                           PO_RECORDCOUNT  OUT NUMBER);

  PROCEDURE SP_AGREGADISCAPACIDADVALORA(PI_IDREGPERSONA IN NUMBER,
                                        PI_IDDISCAPACIDAD IN NUMBER);

  PROCEDURE SP_IDVALORACIONPORDECLARACION(PI_IDDECLARACION IN NUMBER,
                                          PO_IDVALORACION OUT NUMBER);
                                        
  PROCEDURE SP_GETVALORACIONHISTORICO(PI_IDVALORACION IN NUMBER,
                                      PO_CURSOR OUT CURSOR_TYPE);
                                      
  PROCEDURE SP_GETMOTIVACIONVALORACION(pi_IdValoracion   IN NUMBER,
                                       PO_CURSOR OUT CURSOR_TYPE);

END PKG_VALORACION;
/

-- Create PKG_VALORACION package body
create or replace PACKAGE BODY PKG_VALORACION AS

  /* Obtener declaraciones sin valorar */
  PROCEDURE sp_getDeclaracionesSinValorar (
    cu_result OUT cursor_type
  ) AS
  BEGIN
    OPEN cu_result FOR
    SELECT
        D.ID
      , PKG_COMMON.f_getnombrecompletopersona(P.ID) AS Nombre_Persona
      , P.PARAM_TIPODOCUMENTO
      , P_TD.NOMBRE as TIPO_DOCUMENTO
      , P.NUMERODOCUMENTO
      , D.NUMEROFORMULARIO
      , D.ID_DEPARTAMENTODECLARACION
      , P_DPT.nombre AS DEPARTAMENTO
      , P_MUN.nombre AS MUNICIPIO
      , RAD.ID as RAD_ID
      , RAD.NRO_FORMULARIO
      , RAD.FECHALLEGADA AS FECHA_RADICACION
      , RAD.ID_UTERRITORIALRECIBE
      , UT.NOMBRE AS UTERRITORIAL
      , RAD.PARAM_TIPOENTIDAD
      , P_TE.nombre AS TIPOENTIDAD
      , (SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 ) AS Total_HV
    from TBDECLARACIONES D
    INNER join TBRADICACION RAD ON RAD.id_declaracion = d."ID"
    INNER join TBREGISTROS_PERSONAS D_RP ON D_RP.ID_DECLARACION = D.ID AND D_RP.ESDECLARANTE = 1
    INNER join TBPERSONAS P ON P.ID = D_RP.ID_PERSONA
    INNER join TBPARAMETROS P_TD ON P_TD.ID = P.PARAM_TIPODOCUMENTO
    LEFT JOIN tbunidadesterritoriales UT ON UT.ID = RAD.ID_UTERRITORIALRECIBE
    LEFT join tbentidadmunicipio P_TE ON P_TE.ID = RAD.id_entidadmunicipio
    LEFT join tbgeografia P_DPT ON P_DPT.ID = D.ID_DEPARTAMENTODECLARACION
    LEFT join tbgeografia P_MUN ON P_MUN.ID = D.ID_MUNICIPIODECLARACION
    WHERE D_RP.ACTIVO = 1 AND D.PARAM_ESTADO IN ( VALORACION_PEND_PORASIGNAR )
    ORDER BY RAD.FECHALLEGADA ASC;
  END;

  /***********************************************************
  * Procedure description: sp_getDeclaracionesSinValorar Paginado
  * Date:   06/07/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date 30/08/2013
  * Modified By  Diego Alvarez
  * Comments Se crea variable V_Inicial para solucionar problema en aginaci贸n
  ************************************************************
  *
  ************************************************************/
  PROCEDURE  sp_getDeclaSinValorarPaginada (
    P_FilaInicial    NUMBER,
    P_FilaFinal      NUMBER,
    P_Orden          VARCHAR2,
    P_Criterio       VARCHAR2 DEFAULT NULL,
    P_Valor          VARCHAR2 DEFAULT NULL,
    P_Result         OUT SYS_REFCURSOR
  ) AS
    V_Final       NUMBER;
    V_Inicial     NUMBER;
    V_Orden       VARCHAR2(1000);
    V_FILTRO      VARCHAR2(1000);
    LONGITUD      NUMBER;
    BUSQUEDA      VARCHAR2(1000);
    V_QUERY       VARCHAR2(4000);
  BEGIN
    V_Final := P_FilaInicial + P_FilaFinal;
    V_Inicial := P_FilaInicial + 1;
    IF P_Orden IS NULL THEN
       V_Orden := 'D.ID';
    END IF;
    IF P_Orden = 'NombreDeclarante' THEN
       V_Orden := 'PKG_COMMON.f_getnombrecompletopersona(P.ID)';
    END IF;
    IF P_Orden = 'DocumentoDeclarante' THEN
       V_Orden := 'P.NUMERODOCUMENTO';
    END IF;
    IF P_Orden = 'NumeroFormulario' THEN
       V_Orden := 'RAD.nro_formulario';
    END IF;
    IF P_Orden = 'TotalHv' THEN
       V_Orden := '(SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 )';
    END IF;
    IF P_Orden = 'Departamento' THEN
       V_Orden := 'P_DPT.NOMBRE';
    END IF;
    IF P_Orden = 'Municipio' THEN
       V_Orden := 'P_MUN.NOMBRE';
    END IF;
    IF P_Orden = 'Entidad' THEN
       V_Orden := 'P_TE.NOMBRE';
    END IF;
    IF P_Orden = 'FechaRadicado' THEN
       V_Orden := 'RAD.FECHALLEGADA';
    END IF;

    IF P_Orden = 'NombreDeclarante DESC' THEN
       V_Orden := 'PKG_COMMON.f_getnombrecompletopersona(P.ID) DESC';
    END IF;
    IF P_Orden = 'DocumentoDeclarante DESC' THEN
       V_Orden := 'P.NUMERODOCUMENTO DESC';
    END IF;
    IF P_Orden = 'NumeroFormulario DESC' THEN
       V_Orden := 'RAD.nro_formulario DESC';
    END IF;
    IF P_Orden = 'TotalHv DESC' THEN
       V_Orden := '(SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1) DESC';
    END IF;
    IF P_Orden = 'Departamento DESC' THEN
       V_Orden := 'P_DPT.departamento';
    END IF;
    IF P_Orden = 'Municipio DESC' THEN
       V_Orden := 'P_MUN.municipio DESC';
    END IF;
    IF P_Orden = 'Entidad DESC' THEN
       V_Orden := 'P_TE.NOMBRE DESC';
    END IF;
    IF P_Orden = 'FechaRadicado DESC' THEN
       V_Orden := 'RAD.FECHALLEGADA DESC';
    END IF;

    IF P_CRITERIO IS NOT NULL THEN
      V_FILTRO := 'AND ';
      SELECT LENGTH(P_VALOR) INTO LONGITUD FROM DUAL;
      select UPPER(SUBSTR(P_VALOR,2,(LONGITUD - 1))) into BUSQUEDA from DUAL;
      IF P_CRITERIO = 'FechaRadicado' THEN
          V_FILTRO := V_FILTRO || 'RAD.FECHALLEGADA BETWEEN TO_DATE(SUBSTR(''' || P_VALOR || ''',1,10),''dd/mm/yyyy'') AND TO_DATE(SUBSTR(''' || P_VALOR || ''',26,10),''dd/mm/yyyy'')';
      END IF;
      IF P_CRITERIO = 'NombreDeclarante' THEN
         V_FILTRO := V_FILTRO || 'PKG_COMMON.f_getnombrecompletopersona(P.ID) LIKE ''%' || P_VALOR || '%''';
      END IF;
      IF P_Criterio = 'DocumentoDeclarante' THEN
         V_FILTRO := V_FILTRO || 'P.NUMERODOCUMENTO = ''' || P_VALOR || '''';
      END IF;
      IF P_CRITERIO = 'NumeroFormulario' THEN
         V_FILTRO := V_FILTRO || 'RAD.nro_formulario LIKE ''%' || P_VALOR || '%''';
      END IF;
      IF P_Criterio = 'TotalHv' THEN
         V_FILTRO := V_FILTRO || '(SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 ) = ''' || P_VALOR || '''';
      END IF;
      IF P_CRITERIO = 'Departamento' THEN
         V_FILTRO := V_FILTRO || 'P_DPT.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
      IF P_CRITERIO = 'Municipio' THEN
         V_FILTRO := V_FILTRO || 'P_MUN.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
      IF P_CRITERIO = 'Entidad' THEN
         V_FILTRO := V_FILTRO || 'P_TE.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
    ELSE
      V_FILTRO := '';
    END IF;

    V_QUERY := 'SELECT * FROM (select
                ROW_NUMBER() OVER (ORDER BY '|| V_Orden ||') FILA
              , D.ID
              , PKG_COMMON.f_getnombrecompletopersona(P.ID) AS NombreDeclarante
              , P.PARAM_TIPODOCUMENTO
              , P_TD.NOMBRE as TIPO_DOCUMENTO
              , P.NUMERODOCUMENTO DocumentoDeclarante
              , D.ID_DEPARTAMENTODECLARACION
              , P_DPT.NOMBRE AS Departamento
              , P_MUN.NOMBRE AS Municipio
              , RAD.ID as RAD_ID
              , RAD.NRO_FORMULARIO NumeroFormulario
              , RAD.FECHALLEGADA AS FechaRadicado
              , RAD.ID_UTERRITORIALRECIBE
              , UT.NOMBRE AS UTERRITORIAL
              , RAD.PARAM_TIPOENTIDAD
              , P_TE.NOMBRE AS Entidad
              , (SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 ) AS TotalHv
             from TBDECLARACIONES D
             INNER join TBRADICACION RAD ON RAD.ID_declaracion = d.id
             INNER join TBREGISTROS_PERSONAS D_RP ON D_RP.ID_DECLARACION = D.ID AND D_RP.ESDECLARANTE = 1
             INNER join TBPERSONAS P ON P.ID = D_RP.ID_PERSONA
             INNER join TBPARAMETROS P_TD ON P_TD.ID = P.PARAM_TIPODOCUMENTO
             LEFT JOIN tbunidadesterritoriales UT ON UT.ID = RAD.ID_UTERRITORIALRECIBE
             LEFT join TBENTIDADMUNICIPIO P_TE ON P_TE.ID = RAD.ID_ENTIDADMUNICIPIO
             LEFT join tbgeografia P_DPT ON P_DPT.ID = D.ID_DEPARTAMENTODECLARACION
             LEFT join tbgeografia P_MUN ON P_MUN.ID = D.ID_MUNICIPIODECLARACION
            WHERE
             D_RP.ACTIVO = 1
             AND D.PARAM_ESTADO IN ('||VALORACION_PEND_PORASIGNAR ||' )
             '|| V_FILTRO ||') WHERE FILA BETWEEN '|| V_Inicial ||' AND '|| V_Final|| '';
    OPEN P_Result FOR V_QUERY;
  END;

/***********************************************************
* Procedure description: sp_getDeclaracionesSinValorar Cantidad
* Date:   06/07/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_getDeclaSinValorarCantidad
(
  P_Criterio       VARCHAR2 DEFAULT NULL,
  P_Valor          VARCHAR2 DEFAULT NULL,
  P_Cantidad       OUT NUMBER
)
AS
  V_FILTRO           VARCHAR2(1000);
  V_QUERY            VARCHAR2(4000);
  LONGITUD           NUMBER;
  BUSQUEDA           VARCHAR2(1000);
BEGIN
    IF P_CRITERIO IS NOT NULL THEN
      V_FILTRO := 'AND ';
      SELECT LENGTH(P_VALOR) INTO LONGITUD FROM DUAL;
      select UPPER(SUBSTR(P_VALOR,2,(LONGITUD - 1))) into BUSQUEDA from DUAL;
      IF P_CRITERIO = 'FechaRadicado' THEN
          V_FILTRO := V_FILTRO || 'RAD.FECHALLEGADA BETWEEN TO_DATE(SUBSTR(''' || P_VALOR || ''',1,10),''dd/mm/yyyy'') AND TO_DATE(SUBSTR(''' || P_VALOR || ''',26,10),''dd/mm/yyyy'')';
      END IF;
      IF P_CRITERIO = 'NombreDeclarante' THEN
         V_FILTRO := V_FILTRO || 'PKG_COMMON.f_getnombrecompletopersona(P.ID) LIKE ''%' || P_VALOR || '%''';
      END IF;
      IF P_Criterio = 'DocumentoDeclarante' THEN
         V_FILTRO := V_FILTRO || 'P.NUMERODOCUMENTO = ''' || P_VALOR || '''';
      END IF;
      IF P_CRITERIO = 'NumeroFormulario' THEN
         V_FILTRO := V_FILTRO || 'RAD.nro_formulario LIKE ''%' || P_VALOR || '%''';
      END IF;
      IF P_Criterio = 'TotalHv' THEN
         V_FILTRO := V_FILTRO || '(SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 ) = ''' || P_VALOR || '''';
      END IF;
      IF P_CRITERIO = 'Departamento' THEN
         V_FILTRO := V_FILTRO || 'P_DPT.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
      IF P_CRITERIO = 'Municipio' THEN
         V_FILTRO := V_FILTRO || 'P_MUN.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
      IF P_CRITERIO = 'Entidad' THEN
         V_FILTRO := V_FILTRO || 'P_TE.NOMBRE LIKE ''%' || BUSQUEDA || '%''';
      END IF;
    ELSE
      V_FILTRO := '';
    END IF;

    V_QUERY := 'SELECT  COUNT(*)
                from TBDECLARACIONES D
                INNER join TBRADICACION RAD ON RAD.ID_DECLARACION = D.ID
                INNER join TBREGISTROS_PERSONAS D_RP ON D_RP.ID_DECLARACION = D.ID AND D_RP.ESDECLARANTE = 1
                INNER join TBPERSONAS P ON P.ID = D_RP.ID_PERSONA
                INNER join TBPARAMETROS P_TD ON P_TD.ID = P.PARAM_TIPODOCUMENTO
                LEFT JOIN tbunidadesterritoriales UT ON UT.ID = RAD.ID_UTERRITORIALRECIBE
                LEFT join tbentidadmunicipio P_TE ON P_TE.ID = RAD.ID_ENTIDADMUNICIPIO
                LEFT join tbgeografia P_DPT ON P_DPT.ID = D.ID_DEPARTAMENTODECLARACION
                LEFT join tbgeografia P_MUN ON P_MUN.ID = D.ID_MUNICIPIODECLARACION
                WHERE D_RP.ACTIVO = 1
                 AND D.PARAM_ESTADO IN (' || VALORACION_PEND_PORASIGNAR ||')
                 '|| V_FILTRO;
      DBMS_OUTPUT.PUT_LINE(V_QUERY);
      EXECUTE IMMEDIATE V_QUERY INTO P_Cantidad;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;
 --Obtener detalles de una declaracion sin valorar
  PROCEDURE sp_getDetallesDeclaracion
  (
   v_Declaracion IN NUMBER,
   c_result OUT SYS_REFCURSOR
  )
  is
  BEGIN

      OPEN c_result FOR
      SELECT        rp."ID"
                ,   PKG_COMMON.f_getnombrecompletopersona(P.ID) AS Nombre_Persona
                ,   P_TD.NOMBRE as TIPO_DOCUMENTO
                ,   P.NUMERODOCUMENTO
                ,   P_REL.nombre AS RELACION
                ,   P_GEN.NOMBRE AS GENERO
                ,   P.FECHANACIMIENTO
                ,   trunc ( months_between( SYSDATE, P.FECHANACIMIENTO)/12 ) AS EDAD
                ,   P_ETN.NOMBRE AS ETNIA
                ,   (select CASE WHEN count(*) > 0 THEN  1 ELSE 0 END from TBDISCAPACIDAD_PERSONA DP where DP.ID_REGPERSONA = RP.ID) as Es_Discapacitado
                ,   replace(PKG_COMMON.f_gethechosvictimizantesper(rp."ID"), ';', '<br />') AS Hechos
        FROM        TBDECLARACIONES D
                    left join TBREGISTROS_PERSONAS RP ON RP.ID_DECLARACION = D.ID
                    left join TBPERSONAS P ON P.ID = RP.ID_PERSONA
                    left join TBPARAMETROS P_TD ON P_TD.ID = P.PARAM_TIPODOCUMENTO
                    left join TBPARAMETROS P_GEN ON P_GEN.ID = P.PARAM_GENERO
                    left join TBPARAMETROS P_ETN ON P_ETN.ID = P.PARAM_ETNIAPERTENECE
                    left JOIN tbparametros P_REL ON P_REL.ID = RP.param_relacion
        WHERE       RP.ACTIVO = 1
                    AND D."ID" = v_Declaracion
        ORDER BY    D.ID;


  end;


  --Obtener Valoraciones para Reasignar
  PROCEDURE sp_getDeclaracionesValorando
  (
    cu_result  OUT cursor_type
  )AS
  BEGIN
    OPEN cu_result FOR
    select
          VAL.ID
        , PKG_COMMON.f_getnombrecompletopersona(P.ID) AS Nombre_Persona
        , P_TD.NOMBRE as TIPO_DOCUMENTO
        , P.NUMERODOCUMENTO
        , P_DPT.NOMBRE AS DEPARTAMENTO
        , P_MUN.NOMBRE AS MUNICIPIO
        , RAD.ID as RAD_ID
        , RAD.NRO_FORMULARIO
        , RAD.FECHALLEGADA AS FECHA_RADICACION
        ,UT.NOMBRE AS UTERRITORIAL
        , P_TE.NOMBRE AS TIPOENTIDAD
        , (SELECT COUNT(*) FROM TBREGISTROS_PERSONAS RP
              inner join TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = RP.ID
              WHERE RP.ID_DECLARACION = D.ID AND RP.ACTIVO = 1 AND S.ACTIVO = 1 ) AS Total_HV
       , VAL.ID_VALORADOR
       ,  u.nombre AS VALORADOR
    from TBVALORACION VAL
    INNER join TBDECLARACIONES D ON D.ID = VAL.ID_DECLARACION
    INNER join tbusuarios U ON U.ID = VAL.ID_VALORADOR
    INNER join tbusuarios UA ON UA.ID = VAL.ID_ASIGNADOR
    INNER join TBRADICACION RAD ON RAD.ID_DECLARACION = D."ID"
    INNER join TBREGISTROS_PERSONAS D_RP ON D_RP.ID_DECLARACION = D.ID AND D_RP.ESDECLARANTE = 1
    INNER join TBPERSONAS P ON P.ID = D_RP.ID_PERSONA
    INNER join TBPARAMETROS P_TD ON P_TD.ID = P.PARAM_TIPODOCUMENTO
    left join tbunidadesterritoriales UT ON UT.ID = RAD.ID_UTERRITORIALRECIBE
    left join TBPARAMETROS P_TE ON P_TE.ID = RAD.PARAM_TIPOENTIDAD
    left join tbgeografia P_DPT ON P_DPT.ID = D.ID_DEPARTAMENTODECLARACION
    left join tbgeografia P_MUN ON P_MUN.ID = D.ID_MUNICIPIODECLARACION
    WHERE
    VAL.ID_ESTADO_VAL NOT IN(VALORACION_FINALIZADA,VALORACION_DEVUELTA, VALORACION_DEVUELTA_ASI) AND
    D_RP.ACTIVO = 1
    ORDER BY RAD.fechallegada ASC;

  END;

  PROCEDURE sp_GetDeclaracionesValPaginada(pi_Orden        IN VARCHAR2
                                         , pi_Filtro       IN VARCHAR2
                                         , pi_RegInicial   IN NUMBER
                                         , pi_TamanoPagina IN NUMBER
                                         , po_Resultado    OUT SYS_REFCURSOR) IS
    Consulta VARCHAR2(32767);
  BEGIN
    Consulta := 'SELECT * FROM ( ' ||
                '  SELECT Total.*, ROW_NUMBER() OVER (ORDER BY ' || pi_Orden || ') FILA FROM ( ' ||
                '    SELECT VAL.ID                AS ID ' ||
                '         , PKG_COMMON.f_GetNombreCompletoPersona(RRP.ID_PERSONA) AS NOMBRE_PERSONA ' ||
                '         , PKG_COMMON.f_GetDocumentoPersona(RRP.ID_PERSONA)      AS DOCUMENTO      ' ||
                '         , RAD.FECHALLEGADA      AS FECHA_RADICACION  ' ||
                '         , DCL.NUMEROFORMULARIO  AS FORMULARIO        ' ||
                '         , ''''                  AS HECHOVICTIMIZANTE ' ||
                '         , RRP.CANTIDADHECHOS    AS TOTAL_HV          ' ||
                '         , DPT.NOMBRE            AS DEPARTAMENTO      ' ||
                '         , MCP.NOMBRE            AS MUNICIPIO         ' ||
                '         , ENT.nombre            AS TIPOENTIDAD       ' ||
                '         , USV.NOMBRE            AS VALORADOR         ' ||
                '    FROM TBDECLARACIONES DCL ' ||
                '    /* Ultima Valoracion */  ' ||
                '    INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBVALORACION GROUP BY ID_DECLARACION) VVL ON VVL.ID_DECLARACION = DCL.ID ' ||
                '    INNER JOIN TBVALORACION VAL ON VAL.ID = VVL.ID ' ||
                '    /* Ultima Radicacion */  ' ||
                '    INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBRADICACION GROUP BY ID_DECLARACION) RRD ON RRD.ID_DECLARACION = DCL.ID ' ||
                '    INNER JOIN TBRADICACION RAD ON RAD.ID = RRD.ID ' ||
                '    /* Hash REGPERSONA */    ' ||
                '    INNER JOIN (SELECT R.ID_DECLARACION ' ||
                '                     , MAX(CASE WHEN R.ESDECLARANTE = 1 THEN R.ID_PERSONA ELSE 0 END) AS ID_PERSONA     ' ||
                '                     , SUM(CASE WHEN S.ID_REGPERSONA IS NOT NULL THEN 1 ELSE 0 END)   AS CANTIDADHECHOS ' ||
                '                FROM TBREGISTROS_PERSONAS R ' ||
                '                LEFT JOIN TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = R.ID           ' ||
                '                GROUP BY R.ID_DECLARACION) RRP ON RRP.ID_DECLARACION = DCL.ID        ' ||
                '    LEFT  JOIN TBUSUARIOS   USV ON USV.ID = VAL.ID_VALORADOR                   ' ||
                '    LEFT  JOIN TBGEOGRAFIA        DPT ON DPT.ID = DCL.ID_DEPARTAMENTODECLARACION     ' ||
                '    LEFT  JOIN TBGEOGRAFIA        MCP ON MCP.ID = DCL.ID_MUNICIPIODECLARACION        ' ||
                '    LEFT  JOIN TBENTIDADMUNICIPIO ENT ON ENT.ID = DCL.ID_ENTIDADMUNICIPIODECLARACION ' ||
                '    WHERE DCL.PARAM_ESTADO IN (' || VALORACION_PEND_PORVALORAR || ', ' || VALORACION_EN_VALORACION || ') AND VAL.ID_ESTADO_VAL IN (' || VALORACION_ASIGNADA || ', ' || VALORACION_EN_PROCESO || ')' ||
                ') Total ' || (CASE WHEN pi_Filtro IS NOT NULL THEN 'WHERE ' || pi_Filtro END) || ') ' ||
                'WHERE FILA BETWEEN ' || pi_RegInicial || ' AND ' || (pi_RegInicial + pi_TamanoPagina) || '';
    DBMS_OUTPUT.PUT_LINE(Consulta);
    OPEN po_Resultado FOR Consulta;
  END;

  PROCEDURE sp_GetDeclaracionesValCantidad(pi_Filtro    IN VARCHAR2
                                         , po_Resultado OUT NUMBER) IS
    Consulta VARCHAR2(32767);
  BEGIN
    Consulta := 'SELECT COUNT(1) FROM ( ' ||
                '  SELECT VAL.ID                AS ID ' ||
                '       , PKG_COMMON.f_GetNombreCompletoPersona(RRP.ID_PERSONA) AS NOMBRE_PERSONA ' ||
                '       , PKG_COMMON.f_GetDocumentoPersona(RRP.ID_PERSONA)      AS DOCUMENTO      ' ||
                '       , RAD.FECHALLEGADA      AS FECHA_RADICACION  ' ||
                '       , DCL.NUMEROFORMULARIO  AS FORMULARIO        ' ||
                '       , ''''                  AS HECHOVICTIMIZANTE ' ||
                '       , RRP.CANTIDADHECHOS    AS TOTAL_HV          ' ||
                '       , DPT.NOMBRE            AS DEPARTAMENTO      ' ||
                '       , MCP.NOMBRE            AS MUNICIPIO         ' ||
                '       , ENT.nombre            AS TIPOENTIDAD       ' ||
                '       , USV.NOMBRE            AS VALORADOR         ' ||
                '  FROM TBDECLARACIONES DCL ' ||
                '  /* Ultima Valoracion */  ' ||
                '  INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBVALORACION GROUP BY ID_DECLARACION) VVL ON VVL.ID_DECLARACION = DCL.ID ' ||
                '  INNER JOIN TBVALORACION VAL ON VAL.ID = VVL.ID ' ||
                '  /* Ultima Radicacion */  ' ||
                '  INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBRADICACION GROUP BY ID_DECLARACION) RRD ON RRD.ID_DECLARACION = DCL.ID ' ||
                '  INNER JOIN TBRADICACION RAD ON RAD.ID = RRD.ID ' ||
                '  /* Hash REGPERSONA */    ' ||
                '  INNER JOIN (SELECT R.ID_DECLARACION ' ||
                '                   , MAX(CASE WHEN R.ESDECLARANTE = 1 THEN R.ID_PERSONA ELSE 0 END) AS ID_PERSONA     ' ||
                '                   , SUM(CASE WHEN S.ID_REGPERSONA IS NOT NULL THEN 1 ELSE 0 END)   AS CANTIDADHECHOS ' ||
                '              FROM TBREGISTROS_PERSONAS R ' ||
                '              LEFT JOIN TBSINIESTROS_PERSONA S ON S.ID_REGPERSONA = R.ID           ' ||
                '              GROUP BY R.ID_DECLARACION) RRP ON RRP.ID_DECLARACION = DCL.ID        ' ||
                '  LEFT  JOIN TBUSUARIOS   USV ON USV.ID = VAL.ID_VALORADOR                   ' ||
                '  LEFT  JOIN TBGEOGRAFIA        DPT ON DPT.ID = DCL.ID_DEPARTAMENTODECLARACION     ' ||
                '  LEFT  JOIN TBGEOGRAFIA        MCP ON MCP.ID = DCL.ID_MUNICIPIODECLARACION        ' ||
                '  LEFT  JOIN TBENTIDADMUNICIPIO ENT ON ENT.ID = DCL.ID_ENTIDADMUNICIPIODECLARACION ' ||
                '  WHERE DCL.PARAM_ESTADO IN (' || VALORACION_PEND_PORVALORAR || ', ' || VALORACION_EN_VALORACION || ') AND VAL.ID_ESTADO_VAL IN (' || VALORACION_ASIGNADA || ', ' || VALORACION_EN_PROCESO || ')' ||
                ') Total ' || (CASE WHEN pi_Filtro IS NOT NULL THEN 'WHERE ' || pi_Filtro END) || '';
    DBMS_OUTPUT.PUT_LINE(Consulta);
    EXECUTE IMMEDIATE Consulta INTO po_Resultado;
  END;
  
  /***********************************************************
  * Procedure description:ObtenerValoradores
  * Date:   21/03/2012
  * Author: Cristian Alejandro neira
  * Obtiene la lista de valoradores para ser asignada una declaracion
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_getValoradores
  (
     cu_result  OUT cursor_type
  )
  AS
  BEGIN
    OPEN cu_result FOR
    SELECT  t."ID",
            t.nombre
    FROM    tbusuarios t
            INNER JOIN tbroles_usuario tu ON tu.id_usuario = t."ID"
    WHERE   tu.id_rol IN(VALORADOR_ROL)
            AND t.activo = 1
    ORDER BY t.nombre ASC;

  END;
  
  PROCEDURE sp_getValoracionPorID (p_IdVal   IN NUMBER
                                 , cu_Result OUT CURSOR_TYPE) AS
  BEGIN
    OPEN cu_Result FOR
      SELECT VAL.ID                     AS ID
           , VAL.ID_DECLARACION         AS ID_DECLARACION
           , VAL.ID_ESTADO_VAL          AS ID_ESTADO_VAL
           , VAL.FECHAASIGNACION        AS FECHAASIGNACION
           , VAL.ID_VALORADOR           AS ID_VALORADOR
           , VAL.ID_ASIGNADOR           AS ID_ASIGNADOR
           , VAL.FECHAVALORACION        AS FECHAVALORACION
           , VAL.FECHAVALORACIONREAL    AS FECHAVALORACIONREAL
           , MOT.TIPOMOTIVACION         AS TIPOMOTIVACION
           , MOT.MOTIVACION_INCLUSION   AS MOTIVACION_INCLUSION
           , MOT.MOTIVACION_NOINCLUSION AS MOTIVACION_NOINCLUSION
           , MOT.RESUELVE_ARTICULO1     AS RESUELVE_ARTICULO1
           , MOT.RESUELVE_ARTICULO2     AS RESUELVE_ARTICULO2
           , VAL.ESDECLARACION          AS ESDECLARACION
           , VAL.OBSERVACION            AS OBSERVACION
           , DCL.PARAM_ESTADO           AS PARAM_ESTADO
      FROM TBVALORACION VAL
      INNER JOIN TBDECLARACIONES         DCL ON DCL.ID = VAL.ID_DECLARACION
      LEFT  JOIN TBVALORACION_MOTIVACION MOT ON VAL.ID = MOT.ID_VALORACION
      WHERE VAL.ID = p_IdVal;
  END;

  PROCEDURE sp_getValoracionPorDeclaracion (p_IdVal   IN NUMBER
                                          , cu_Result OUT CURSOR_TYPE) AS
  BEGIN
    OPEN cu_Result FOR
      SELECT VAL.ID                     AS ID
           , VAL.ID_DECLARACION         AS ID_DECLARACION
           , VAL.ID_ESTADO_VAL          AS ID_ESTADO_VAL
           , VAL.FECHAASIGNACION        AS FECHAASIGNACION
           , VAL.ID_VALORADOR           AS ID_VALORADOR
           , VAL.ID_ASIGNADOR           AS ID_ASIGNADOR
           , VAL.FECHAVALORACION        AS FECHAVALORACION
           , VAL.FECHAVALORACIONREAL    AS FECHAVALORACIONREAL
           , VAL.MOTIVACION             AS MOTIVACION
           , VAL.ESDECLARACION          AS ESDECLARACION
           , VAL.OBSERVACION            AS OBSERVACION
           , DCL.PARAM_ESTADO           AS PARAM_ESTADO
      FROM TBDECLARACIONES DCL
      INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID_VALORACION
                  FROM TBVALORACION
                  GROUP BY ID_DECLARACION) VVL ON (VVL.ID_DECLARACION = DCL.ID)
      INNER JOIN TBVALORACION VAL ON VAL.ID = VVL.ID_VALORACION
      WHERE DCL.ID = p_IdVal;

  END;

  /***********************************************************
  * Procedure description:OCrear/Actualizar Valoracion
  * Date:   21/03/2012
  * Author: Cristian Alejandro neira
  * Obtiene la lista de valoradores para ser asignada una declaracion
  * Changes
   * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_AsignarValoracion
  (
    P_ID_Valoracion IN OUT NUMBER,
    P_ID_DECLARACION IN NUMBER,
    P_ID_VALORACION_ESTADO IN NUMBER,
    P_ID_VALORADOR IN NUMBER,
    P_ID_ASIGNADOR IN NUMBER
  ) AS
  V_CANTIDAD_ANT  NUMBER;
  BEGIN
      IF (P_ID_Valoracion < 1) THEN
         --Crear nueva valoraci髇

         SELECT  COUNT(1)
         INTO    V_CANTIDAD_ANT
         FROM    tbvaloracion V
         WHERE   V.id_declaracion = p_id_declaracion
                 AND V.id_estado_val NOT IN(VALORACION_DEVUELTA_ASI, VALORACION_DEVUELTA, VALORACION_FINALIZADA);

         IF V_CANTIDAD_ANT = 0 THEN

            PKG_VALORACION.sp_CrearValoracion(
                          P_ID_Valoracion,
                          P_ID_DECLARACION,
                          P_ID_VALORACION_ESTADO,
                          P_ID_VALORADOR ,
                          P_ID_ASIGNADOR);

            PKG_COMMON.sp_updestado_declaracion(P_ID_DECLARACION, P_ID_VALORADOR, VALORACION_PEND_PORVALORAR);

            INSERT INTO tbvaloracion_anexo
            (
              ID,
              id_valoracion,
              ultima_fechaedicion,
              tipo_anexo,
              id_siniestro
            )
            SELECT SEQ_ANEXO_VAL.NEXTVAL,
                   P_ID_Valoracion,
                   SYSDATE,
                   sp.param_tipohecho,
                   SP.ID
            FROM   tbregistros_personas tp
                   INNER JOIN tbsiniestros_persona sp ON sp.id_regpersona = tp.ID
            WHERE  tp.id_declaracion = P_ID_DECLARACION;

            --DBMS_OUTPUT.put_line('Guardo Anexo');

            DECLARE CURSOR v_cur_valanexo IS
            SELECT va."ID",
                   va.id_valoracion,
                   va.tipo_anexo,
                   va.id_siniestro
            FROM   tbvaloracion_anexo va
            WHERE  va.id_valoracion = P_ID_Valoracion;
            v_cur_valper sys_refcursor;
            v_regper NUMBER;
            v_idanexo NUMBER;
            v_query VARCHAR2(1000);
            BEGIN
            FOR rec IN v_cur_valanexo LOOP

                  v_query :=PKG_COMMON.f_getanexo_regper(rec.tipo_anexo, rec.id_siniestro);
                  --DBMS_OUTPUT.put_line(v_query);
                  OPEN v_cur_valper FOR v_query;
                  LOOP
                    FETCH v_cur_valper INTO v_regper, v_idanexo;
                    EXIT WHEN v_cur_valper%NOTFOUND;
                    INSERT INTO tbval_anexo_persona
                    (
                      "ID",
                      id_regpersona,
                      id_val_anexo,
                      id_anexo
                    )
                    VALUES
                    (
                      SEQ_VAL_ANEXO_PER.NEXTVAL,
                      v_regper,
                      rec.ID,
                      v_idanexo
                    );
                    --DBMS_OUTPUT.put_line('Guardo Persona' || v_regper);
                  END LOOP;
                  CLOSE v_cur_valper;
            END LOOP;
            END;
          END IF;
      ELSE
          UPDATE TBVALORACION VAL
                 SET   VAL.ID_VALORADOR = P_ID_VALORADOR
                     , VAL.ID_ASIGNADOR = P_ID_ASIGNADOR
          WHERE VAL.ID = P_ID_Valoracion;
      END IF;
 END;


 /***********************************************************
 * Procedure description: Lista de Valoraciones del Valorador
 * Date:   27/03/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_getValoracionesPorValorador
 (
   P_ValoradorId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN
   OPEN P_Result FOR
   SELECT        v.ID,
                 v.id_valorador,
                 pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Declarante,
                 pkg_common.f_getdocumentopersona(rp.id_persona) AS DocumentoDeclarante,
                 r.fechallegada AS FechaRadicacion,
                 r.nro_formulario AS Formulario,
                 replace(PKG_COMMON.f_gethechosvictimizantesdec(v.id_declaracion), ';', '<br />') AS Hechos,
                 (SELECT COUNT(*)
                  FROM   tbregistros_personas rp1
                         INNER JOIN tbsiniestros_persona sp1 ON sp1.id_regpersona = rp1.ID
                  WHERE  rp1.id_declaracion = v.id_declaracion
                         and rp1.activo = 1
                         AND sp1.activo = 1 ) AS TotalHV,
                 v.fechaasignacion AS FechaAsignacion,
                 est.nombre AS Estado
   FROM          tbvaloracion v
                 INNER JOIN tbdeclaraciones d ON d.ID = v.id_declaracion
                 INNER JOIN tbparametros est ON est.ID = d.param_estado
                 INNER JOIN tbregistros_personas rp ON rp.id_declaracion = d.ID
                 INNER JOIN tbradicacion r ON r.id_declaracion = D."ID"
                 INNER JOIN tbvaloracion_estado ve ON ve.ID = v.id_estado_val
   WHERE         v.id_valorador = P_ValoradorId
                 AND rp.esdeclarante = 1
                 AND v.id_estado_val NOT IN(VALORACION_DEVUELTA,VALORACION_DEVUELTA_ASI)
   ORDER BY      v.fechaasignacion ASC, r.fechallegada ASC;

 EXCEPTION
   WHEN OTHERS THEN
     RAISE;
 END;

  /***********************************************************
   * Procedure description: Declaraciones asignadas a valorador
   * Date:   19/09/2012
   * Author: Cristian Neira
   *
   * Changes
   * Date    Modified By     Comments
   ************************************************************
   * 03/01/2013 Johnatan Garc铆a: Restrict output records by state
   ************************************************************/
  PROCEDURE  sp_getDeclaraValoradorPaginado (
    Pi_ValoradorId  IN NUMBER,
    Pi_Orden        IN VARCHAR2,
    Pi_Filtro       IN VARCHAR2,
    Pi_RegInicial   IN NUMBER,
    Pi_TamanoPagina IN NUMBER,
    Po_Resultado    OUT SYS_REFCURSOR
  ) IS
    Filtro          VARCHAR2(1000);
    RegFinal        NUMBER;
    Consulta        VARCHAR2(4000);
  BEGIN
    RegFinal := Pi_RegInicial + Pi_TamanoPagina;
    IF PI_FILTRO IS NOT NULL THEN
      Filtro := ' AND ' || Pi_Filtro;
    END IF;
    Consulta := '
    SELECT * FROM (
      SELECT Total.*, ROW_NUMBER() OVER (ORDER BY '|| Pi_Orden ||') FILA FROM (
             SELECT        v.ID,
                           v.id_valorador,
                           pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Declarante,
                           pkg_common.f_getdocumentopersona(rp.id_persona) AS DocumentoDeclarante,
                           r.fechallegada AS FechaRadicacion,
                           r.nro_formulario AS Formulario,
                           replace(PKG_COMMON.f_gethechosvictimizantesdec(v.id_declaracion), '';'', ''<br />'') AS Hechos,
                           (SELECT COUNT(1)
                            FROM   tbregistros_personas rp1
                                   INNER JOIN tbsiniestros_persona sp1 ON sp1.id_regpersona = rp1.ID
                            WHERE  rp1.id_declaracion = v.id_declaracion
                                   and rp1.activo = 1
                                   AND sp1.activo = 1 ) AS TotalHV,
                           v.fechaasignacion AS FechaAsignacion,
                           est.nombre AS Estado,
                           VH.OBSERVACION,
                           VH.FECHAACTUALIZACION,
                           ROW_NUMBER() OVER (PARTITION BY V.ID ORDER BY VH.ID DESC) AS RN
             FROM          tbvaloracion v
                           INNER JOIN tbdeclaraciones d ON d.ID = v.id_declaracion
                           INNER JOIN tbparametros est ON est.ID = d.param_estado
                           INNER JOIN tbregistros_personas rp ON rp.id_declaracion = d.ID
                           INNER JOIN tbradicacion r ON r.id_declaracion = D."ID"
                           INNER JOIN tbvaloracion_estado ve ON ve.ID = v.id_estado_val
                           LEFT OUTER JOIN tbvaloracionhistorico VH ON V.ID = VH.IDVALORACION
             WHERE         v.id_valorador = '|| Pi_ValoradorId ||'
                           AND rp.esdeclarante = 1 AND v.id_estado_val NOT IN('||VALORACION_DEVUELTA||','||VALORACION_DEVUELTA_ASI||', '||VALORACION_FINALIZADA||','||VALORACION_PENDIENTE_REV||')
                           AND d.param_estado NOT IN '||VALORACION_PEND_PORASIGNAR||'
             ORDER BY      v.fechaasignacion ASC, r.fechallegada asc
      ) Total WHERE RN = 1
      '|| Filtro ||'
    )
    WHERE FILA BETWEEN ' || Pi_RegInicial || ' AND '|| RegFinal ||'';

    DBMS_OUTPUT.PUT_LINE(Consulta);

    OPEN Po_Resultado FOR Consulta;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error: '||SQLCODE||SQLERRM);
      RAISE;
  END;

  /***********************************************************
  * Procedure description: Declaraciones asignadas a valorador
  * Date:   19/09/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By     Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE  sp_getDeclaraValoradorCantidad (
    Pi_ValoradorId  IN NUMBER,
    Pi_Filtro       IN VARCHAR2,
    Po_Resultado    OUT NUMBER
  ) IS
    Filtro          VARCHAR2(1000);
    Consulta        VARCHAR2(4000);
  BEGIN

    IF PI_FILTRO IS NOT NULL THEN
      Filtro := ' AND ' || Pi_Filtro;
    END IF;

    Consulta := '
      SELECT  COUNT(1) FROM (
       SELECT       v.ID,
                     v.id_valorador,
                     pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Declarante,
                     pkg_common.f_getdocumentopersona(rp.id_persona) AS DocumentoDeclarante,
                     r.fechallegada AS FechaRadicacion,
                     r.nro_formulario AS Formulario,
                     replace(pkg_common.f_gethechosvictimizantesdec(v.id_declaracion), '';'', ''<br />'') AS Hechos,
                     (SELECT COUNT(1)
                      FROM   tbregistros_personas rp1
                             INNER JOIN tbsiniestros_persona sp1 ON sp1.id_regpersona = rp1.ID
                      WHERE  rp1.id_declaracion = v.id_declaracion
                             and rp1.activo = 1
                             AND sp1.activo = 1 ) AS TotalHV,
                     v.fechaasignacion AS FechaAsignacion,
                     est.nombre AS Estado,
                     VH.OBSERVACION,
                     VH.FECHAACTUALIZACION,
                     ROW_NUMBER() OVER (PARTITION BY V.ID ORDER BY VH.ID DESC) AS RN
       FROM          tbvaloracion v
                     INNER JOIN tbdeclaraciones d ON d.ID = v.id_declaracion
                     INNER JOIN tbparametros est ON est.ID = d.param_estado
                     INNER JOIN tbregistros_personas rp ON rp.id_declaracion = d.ID
                     INNER JOIN tbradicacion r ON r.id_declaracion = D."ID"
                     INNER JOIN tbvaloracion_estado ve ON ve.ID = v.id_estado_val
                     LEFT JOIN tbvaloracionhistorico VH ON V.ID = VH.IDVALORACION
       WHERE         v.id_valorador = '|| Pi_ValoradorId ||'
                     AND rp.esdeclarante = 1 AND v.id_estado_val NOT IN('||VALORACION_DEVUELTA||','||VALORACION_DEVUELTA_ASI||', '||VALORACION_FINALIZADA||','||VALORACION_PENDIENTE_REV||')
       ORDER BY      v.fechaasignacion ASC, r.fechallegada ASC
      ) Total WHERE RN = 1
      '|| Filtro ||'';

    DBMS_OUTPUT.PUT_LINE(Consulta);

    EXECUTE IMMEDIATE Consulta INTO Po_Resultado;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error: '||SQLCODE||SQLERRM);
      RAISE;
  END;

  PROCEDURE sp_CrearValoracion(p_Id_Valoracion        IN OUT NUMBER
                             , p_Id_Declaracion       IN NUMBER
                             , p_Id_Valoracion_Estado IN NUMBER
                             , p_Id_Valorador         IN NUMBER
                             , p_Id_Asignador         IN NUMBER) AS
  BEGIN
    -- Insertar registro de valoraci髇
    INSERT INTO TBVALORACION (ID
                            , ID_DECLARACION
                            , ID_ESTADO_VAL
                            , FECHAASIGNACION
                            , ID_VALORADOR
                            , ID_ASIGNADOR)
    VALUES (SEQ_TBVALORACION.NextVal
          , p_Id_Declaracion
          , p_Id_Valoracion_Estado
          , SYSDATE
          , p_Id_Valorador
          , p_Id_Asignador)
    RETURNING ID INTO p_Id_Valoracion;
    -- Insertar registro de motivaci髇
    INSERT INTO TBVALORACION_MOTIVACION (ID, ID_VALORACION) VALUES (SEQ_TBVALORACION_MOTIVACION.NextVal, p_Id_Valoracion);
    -- Actualizar valores de notificaci髇
    sp_DeterminarNotificacion(p_Id_Valoracion);
  END;

  PROCEDURE sp_DeterminarNotificacion(p_Id_Valoracion IN NUMBER) AS
    xIdDeclaracion        NUMBER;
    xMunicipioDeclaracion NUMBER;
    xMunicipioPorDefecto  NUMBER := f_MunicipioPorCodigoCodazzi('11001');
  BEGIN
    SELECT ID_DECLARACION INTO xIdDeclaracion FROM TBVALORACION WHERE ID = p_Id_Valoracion;
    -- Obtener el municipio de la declaraci髇 - se utiliza tanto para determinar el punto de notificacion como para citar a DT si la direcci髇 no es v醠ida
    SELECT ID_MUNICIPIODECLARACION INTO xMunicipioDeclaracion FROM TBDECLARACIONES WHERE ID = xIdDeclaracion;
    
    -- Failsafe. Si no existe municipio de declaracion (dato antiguo) obtener municipio de residencia de la v韈tima declarante
    IF xMunicipioDeclaracion IS NULL THEN
      BEGIN
        SELECT NVL(ID_MUNICIPIO, xMunicipioPorDefecto) INTO xMunicipioDeclaracion FROM TBREGISTROS_PERSONAS
        WHERE ID_DECLARACION = xIdDeclaracion AND ESDECLARANTE = 1;
      EXCEPTION WHEN OTHERS THEN
        NULL;
      END;
    END IF;
    
    IF xMunicipioDeclaracion IS NOT NULL THEN
      DECLARE
        xNPuntoAtencion        NUMBER;
        xNDireccionTerritorial NUMBER;
        xCPuntoAtencion        NUMBER;
        xCDireccionTerritorial NUMBER;
      BEGIN
        SELECT NTF.IDPUNTOATENCION, NTF.IDDIRECCIONTERRITORIAL, CIT.IDPUNTOATENCION, CIT.IDDIRECCIONTERRITORIAL INTO xNPuntoAtencion, xNDireccionTerritorial, xCPuntoAtencion, xCDireccionTerritorial
        FROM TBGEOGRAFIA MCP
        LEFT JOIN (SELECT IDPUNTOATENCION, IDDIRECCIONTERRITORIAL, ROWNUM AS R FROM TBREGLASNOTIFICACION
                   WHERE IDMUNICIPIO = xMunicipioDeclaracion ORDER BY IDMUNICIPIO, PESO DESC) NTF ON NTF.R = 1
        LEFT JOIN (SELECT IDPUNTOATENCION, IDDIRECCIONTERRITORIAL, ROWNUM AS R FROM TBREGLASCITACION
                   WHERE IDMUNICIPIO = xMunicipioDeclaracion ORDER BY IDMUNICIPIO, PESO DESC) CIT ON CIT.R = 1
        WHERE MCP.ID = xMunicipioDeclaracion;
        
        /* Fallback: Si no existe regla configurada de notificacion, se debe notificar en Bogot谩? */
        IF COALESCE(xNPuntoAtencion, xNDireccionTerritorial) IS NULL THEN
          SELECT NTF.IDPUNTOATENCION, NTF.IDDIRECCIONTERRITORIAL INTO xNPuntoAtencion, xNDireccionTerritorial
          FROM TBGEOGRAFIA MCP
          LEFT JOIN (SELECT IDPUNTOATENCION, IDDIRECCIONTERRITORIAL, ROWNUM AS R FROM TBREGLASNOTIFICACION
                     WHERE IDMUNICIPIO = xMunicipioPorDefecto ORDER BY IDMUNICIPIO, PESO DESC) NTF ON NTF.R = 1
          LEFT JOIN (SELECT IDPUNTOATENCION, IDDIRECCIONTERRITORIAL, ROWNUM AS R FROM TBREGLASCITACION
                     WHERE IDMUNICIPIO = xMunicipioPorDefecto ORDER BY IDMUNICIPIO, PESO DESC) CIT ON CIT.R = 1
          WHERE MCP.ID = xMunicipioPorDefecto;
        END IF;
        
        UPDATE TBVALORACION SET IDPUNTOATENCION                = xNPuntoAtencion
                              , IDDIRECCIONTERRITORIAL         = xNDireccionTerritorial
                              , IDDIRECCIONTERRITORIALCITACION = xCDireccionTerritorial
        WHERE ID = p_Id_Valoracion;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20055, 'No se encontr贸 el municipio ' || xMunicipioDeclaracion || ' asociado a la declaracion');
      END;
    END IF;
  EXCEPTION WHEN NO_DATA_FOUND THEN
    RAISE_APPLICATION_ERROR(-20054, 'No es posible crear el registro de valoraci贸n: El identificador de la declaraci贸n es inv谩lido');
  END;

/***********************************************************
* Procedure description: Obtiene Informacion Basica de la declaracion
* Date:   27/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getInfoDeclaracion
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT  d.ID,
          r.nro_formulario,
          r.fechallegada,
          ut.nombre AS UnidadTerritorial,
          mun.nombre AS Municipio,
          dep.nombre AS Departamento,
          u.nombre AS Valorador,
          v.id_valorador AS ValoradorId,
          v.fechavaloracion AS FechaValoracion
  FROM    tbvaloracion v
          INNER JOIN tbdeclaraciones d ON d.ID = v.id_declaracion
          INNER JOIN tbradicacion r ON r.id_declaracion = D."ID"
          LEFT JOIN tbunidadesterritoriales ut ON ut.ID = r.id_uterritorialrecibe
          LEFT JOIN tbgeografia mun ON r.id_municipio = mun."ID"
          LEFT JOIN tbgeografia dep ON dep."ID" = mun.padreid
          INNER JOIN tbusuarios u ON u.ID = v.id_valorador
  WHERE    v.ID = P_ValoracionId;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;


/***********************************************************
* Procedure description: Obtiene los hechos por el id de la valoracion
* Date:   27/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getHechosPorValoracion
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT  ta."ID",
          v.id_declaracion,
          sp.param_tipohecho,
          CASE WHEN sp.param_tipohecho <= 11 THEN (SELECT ph.nombre FROM tbparametros ph WHERE ph.numero = sp.param_tipohecho AND ph.id_tipoparametro = HECHO_VICTIMIZANTE) ELSE 'Censo Evento Masivo' END TipoHecho,
          sp.fechasiniestro AS Fecha,
          (SELECT te.nombre FROM tbparametros te WHERE te.ID = sp.param_tipo_entorno) AS TipoEntorno,
          sp.otro_localidad_correg AS LocalidadCorregimiento,
          sp.otro_barrio_vereda AS BarrioVereda,
          dto.nombre AS Departamento,
          mun.nombre AS Municipio,
          ta.tipo_anexo AS TipoHechoId,
          PKG_COMMON.f_getcantidadpersonasporhecho(ta.tipo_anexo, ta.id_siniestro) AS TotalPersonas,
          pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Victima1
  FROM    tbvaloracion v
          INNER JOIN tbvaloracion_anexo ta ON ta.id_valoracion = v."ID"
          INNER JOIN tbsiniestros_persona sp ON sp."ID" = ta.id_siniestro
          INNER JOIN tbregistros_personas rp ON rp."ID" = sp.id_regpersona
          LEFT JOIN tbgeografia dto ON dto."ID" = sp.id_departamento
          LEFT JOIN tbgeografia mun ON mun.ID = sp.id_municipio
  WHERE   v."ID" = P_ValoracionId;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;


 /***********************************************************
 * Procedure description: Traer Herramientas por ValAnexoPersona
 * Date:   28/03/2012
 * Author: Cristian Alejandro Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_GetHerraPorAnexoPerId
 (
   P_ValAnexPerId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN
   OPEN P_Result FOR
   SELECT
     tap.id_valanexo_per,
     tap.id_herramienta,
     tap.detalle,
     tap.fecha,
     tap.usaparadesicion,
     t."ID",
     t.nombre,
     t.texto,
     t.id_tipo_herramienta,
     th."ID" AS IdTipo,
     th.nombre AS TipoNombre,
     th.texto AS TextoTipo
   FROM
     tbherramienta_anexo_per tap
     INNER JOIN tbherramientaval t ON t."ID" = tap.id_herramienta
     INNER JOIN tbtipo_herramientaval th ON th."ID" = t.id_tipo_herramienta
   WHERE         tap.id_valanexo_per = P_ValAnexPerId;
 EXCEPTION
   WHEN OTHERS THEN
     RAISE;
 END;



 /***********************************************************
 * Procedure description: Obtener sp_GetHerramientasPorTipoId
 * Date:   12/04/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_GetHerramientasPorTipoId
 (
   P_TipoId IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN
   OPEN P_Result FOR
   SELECT
     t."ID",
     t.id_tipo_herramienta,
     t.nombre,
     t.texto
   FROM
     tbherramientaval t
   WHERE t.id_tipo_herramienta = P_TipoId
   ORDER BY t.nombre ASC;

 EXCEPTION
   WHEN OTHERS THEN
     DBMS_OUTPUT.PUT_LINE('Error');
 END;

 /***********************************************************
 * Procedure description: Obtener sp_GetHerramientas
 * Date:   28/09/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_GetHerramientas
 (
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN
   OPEN P_Result FOR
   SELECT
     t."ID",
     t.id_tipo_herramienta,
     t.nombre,
     t.texto
   FROM
     tbherramientaval t
   ORDER BY t.nombre ASC;

 EXCEPTION
   WHEN OTHERS THEN
     DBMS_OUTPUT.PUT_LINE('Error');
 END;


 /***********************************************************
 * Procedure description:sp_GetTiposHerramienta
 * Date:   12/04/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_GetTiposHerramienta
 (
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN

   OPEN P_Result FOR
   SELECT
     th."ID",
     th.nombre,
     th.texto
   FROM
     tbtipo_herramientaval th
   ORDER BY th.nombre ASC;

 EXCEPTION
   WHEN OTHERS THEN
     DBMS_OUTPUT.PUT_LINE('Error');
 END;



 /***********************************************************
 * Procedure description:sp_GetTipoPorId
 * Date:   12/04/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE sp_GetTipoHerramientaPorId
 (
   P_Id     IN NUMBER,
   P_Result OUT SYS_REFCURSOR
 )
 IS
 BEGIN

   OPEN P_Result FOR
   SELECT
     th."ID",
     th.nombre,
     th.texto
   FROM
     tbtipo_herramientaval th
   WHERE th.ID = P_Id
   ORDER BY th.nombre ASC;

 EXCEPTION
   WHEN OTHERS THEN
     DBMS_OUTPUT.PUT_LINE('Error');
 END;



/***********************************************************
* Procedure description: Obtiene personas por valoracionanexo Id
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetPersonasPorAnexo
(
  P_ValoracionAnexoId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT        vap."ID",
                pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Persona,
                vap.id_regpersona,
                (SELECT td.nombre   FROM tbparametros td WHERE td."ID" = p.param_tipodocumento) AS TipoDocumento,
                p.numerodocumento,
                (SELECT rel.nombre FROM tbparametros rel WHERE rel."ID" = rp.param_relacion) AS Relacion,
                p.param_genero AS GeneroId,
                (SELECT gen.nombre FROM tbparametros gen WHERE gen."ID" = p.param_genero) AS Genero,
                trunc ( months_between( SYSDATE, p.fechanacimiento)/12 ) AS Edad,
                p.param_etniapertenece AS EtniaId,
                (SELECT et.nombre FROM tbparametros et WHERE et."ID" = p.param_etniapertenece) AS Etnia,
                (select CASE WHEN count(*) > 0 THEN  1 ELSE 0 END from TBDISCAPACIDAD_PERSONA DP where DP.ID_REGPERSONA = RP.ID) as Discapacitado,
                (SELECT  t.fallecida
                 FROM    tbanexo6 t
                 WHERE   t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona) AS Fallecida,
                (SELECT t.desaparecida
                 FROM   tbanexo4 t
                 WHERE  t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona) AS Desaparecida,
                (SELECT  t.secuestrado
                 FROM    tbanexo8 t
                 WHERE  t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona) AS Secuestrado,
                (SELECT  t2.nombre
                 FROM    tbanexo7 t
                         INNER JOIN tbparametros t2 ON t2."ID" = t.param_estadovictima
                 WHERE   t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona) AS EstadoPorMina,
                (SELECT td.se_desplazo
                 FROM   tbanexo5 t
                        INNER JOIN tbanexo5_desplazados td ON td.id_anexo5 = t.ID
                 WHERE  t.id_siniestro = va.id_siniestro AND td.id_regpersona = vap.id_regpersona) AS SeDesplazo,
                (CASE WHEN vap.esafectado IS NULL THEN PKG_COMMON.f_personaafectada(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) ELSE vap.esafectado END) AS esafectado,
                (CASE WHEN vap.esvicitma IS NULL THEN PKG_COMMON.f_personavictma(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) ELSE vap.esvicitma END) AS esvicitma,
                vap.id_val_anexo,
                vap.id_estado_val,
                ev.nombre AS estado_val,
                vap.id_observacion_val,
                vap.observacion
  FROM          tbval_anexo_persona vap
                INNER JOIN tbregistros_personas rp ON rp."ID" = vap.id_regpersona
                INNER JOIN tbpersonas p ON p."ID" = rp.id_persona
                INNER JOIN tbvaloracion_anexo va ON va."ID" = vap.id_val_anexo
                LEFT JOIN tbestado_val ev ON ev."ID" = vap.id_estado_val
  WHERE         vap.id_val_anexo = P_ValoracionAnexoId;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae los estados de una valoracion
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/

PROCEDURE sp_GetEstadosValPersona
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto
  FROM
    tbestado_val tv
  ORDER BY tv.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;



/***********************************************************
* Procedure description: Traaer las observaciones por Estado Id
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetObservacionesPorEstadoId
(
  P_EstadValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto,
    tv.id_estado_val
  FROM
    tbobservacion_val tv
  WHERE tv.id_estado_val = P_EstadValId;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Traaer las observaciones
* Date:   28/09/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetObservaciones
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto,
    tv.id_estado_val
  FROM
    tbobservacion_val tv;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae los principios por estado Id
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetPrincipiosPorEstado
(
  P_EstadoId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto,
    tv.id_estado_val
  FROM
    tbprincipio tv
  WHERE tv.id_estado_val = P_EstadoId
  ORDER BY tv.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae los principios
* Date:   28/09/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetPrincipios
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto,
    tv.id_estado_val
  FROM
    tbprincipio tv
  ORDER BY tv.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae los principios por val anexo per Id
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetPrincipioPorValAnexoPer
(
  P_ValAnexoPersonaId IN NUMBER,
  P_Result            OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT        T."ID",
                T.nombre,
                T.texto,
                T.id_estado_val
  FROM          tbprincipio_val tv
                INNER JOIN tbprincipio t ON t."ID" = tv.id_principio
  WHERE         tv.id_val_anexo_per = P_ValAnexoPersonaId
  ORDER BY t.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae los principios por val anexo per Id
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetPrincipioPorVal
(
  P_ValId             IN NUMBER,
  P_Result            OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT        C."ID",
                C.nombrecausal nombre,
                c.parteemotiva texto,
                p.id_estado_val
  FROM          tbcausalesdevolucion cd
                INNER JOIN tbcausales c ON c."ID" = cd.id_causal
                INNER JOIN tbcausaldevolucion_principio cdp ON cdp.id_causal = c."ID"
                INNER JOIN tbprincipio p ON p."ID" = cdp.id_principio
                INNER JOIN tbdevolucion d ON d."ID" = cd.id_devolucion
                INNER JOIN tbdeclaraciones dd ON dd."ID" = d.id_declaracion
                INNER JOIN tbvaloracion v ON v.id_declaracion = dd."ID"
  WHERE         v."ID" = P_ValId
  ORDER BY      c.nombrecausal ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;



/***********************************************************
* Procedure description: Trae las afectaciones
* Date:   29/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetAfectacionesPorPersona
(
  P_PersonaId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
AS
v_Encontradas  NUMBER :=0;
v_EnconCaptura NUMBER :=0;
BEGIN

  SELECT        COUNT(*)
  INTO          v_Encontradas
  FROM          tbafectacion_val pa
                INNER JOIN tbparametros par ON par."ID" = pa.param_afectacion
                INNER JOIN tbval_anexo_persona vap ON vap."ID" = pa.id_valanexoperson
  WHERE         vap."ID" = P_PersonaId;

  IF(v_Encontradas > 0) THEN
    DBMS_OUTPUT.PUT_LINE('Encontro en valoracion');
    OPEN P_Result FOR
    SELECT        par."ID",
                  par.nombre
    FROM          tbafectacion_val pa
                  INNER JOIN tbparametros par ON par."ID" = pa.param_afectacion
                  INNER JOIN tbval_anexo_persona vap ON vap."ID" = pa.id_valanexoperson
    WHERE         vap."ID"    = P_PersonaId;
  ELSE
    SELECT        COUNT(*)
    INTO          v_EnconCaptura
    FROM          tbvaloracion_anexo va
                  INNER JOIN tbval_anexo_persona vap ON vap.id_val_anexo = va."ID"
                  INNER JOIN tbafectacion a ON vap.id_anexo = a.id_anexo AND a.param_tipo_hecho = va.tipo_anexo
                  INNER JOIN tbparametros par ON par."ID" = a.param_afectacion
    WHERE         vap."ID" = P_PersonaId;

    IF(v_EnconCaptura > 0) THEN
      DBMS_OUTPUT.PUT_LINE('Encontro en captura');
      OPEN P_Result FOR
      SELECT        par."ID",
                    par.nombre
      FROM          tbvaloracion_anexo va
                    INNER JOIN tbval_anexo_persona vap ON vap.id_val_anexo = va."ID"
                    INNER JOIN tbafectacion a ON vap.id_anexo = a.id_anexo AND a.param_tipo_hecho = va.tipo_anexo
                    INNER JOIN tbparametros par ON par."ID" = a.param_afectacion
      WHERE         vap."ID" = P_PersonaId;
    ELSE
      DBMS_OUTPUT.PUT_LINE('Encontro en ningunga');
      OPEN P_Result FOR
      SELECT        5541, 'Otro' FROM dual d;
    END IF;
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;

/***********************************************************
* Procedure description: Trae las afectaciones Por Id
* Date:   29/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetAfectacionesPorId
(
  P_Id IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN p_Result FOR
  SELECT
    tv.id_valanexoperson,
    tv.param_afectacion
  FROM
    tbafectacion_val tv
  WHERE tv.id_valanexoperson = P_Id;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;


/***********************************************************
* Procedure description: Trae los autores
* Date:   28/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetAutores
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tv."ID",
    tv.nombre,
    tv.texto
  FROM
    tbautorhv tv
  ORDER BY tv.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;



/***********************************************************
* Procedure description: Trae los autores por Anexo Persona
* Date:   20/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetAutoresPorValAnexoPerId
(
  P_AnexoPersonaId IN NUMBER,
  P_Result         OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT        t."ID",
                t.nombre,
                t.fecha_creacion,
                t.fecha_desmovilizacion
  FROM          tbautorhv_val_anexo tva
                INNER JOIN tbautorhv t ON t.ID = tva.id_autorhv
  WHERE         tva.id_val_anexo_persona = P_AnexoPersonaId;


EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;


/***********************************************************
* Procedure description: Traer Infracciones DIH
* Date:   10/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetInfracciones
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    td."ID",
    td.nombre
  FROM
    tbinfraccion_dih td
  ORDER BY td.nombre ASC;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description: Traer Infracciones DIH
* Date:   10/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetInfraccionesPorAnexoPer
(
  P_ValAnexoPerId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    td."ID",
    td.nombre
  FROM
    tbinfraccion_dih td
    INNER JOIN tbinfraccion_dih_valanexoper tdv ON tdv.id_infracciondih = td."ID"
  WHERE tdv.id_val_anexo_per = P_ValAnexoPerId;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;


/***********************************************************
* Procedure description: Traer Infracciones DIH Por Val Anexo Persona
* Date:   20/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetInfraccionesValAnexoPer
(
  P_ValAnexoPerId  IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT td."ID",
         td.nombre
  FROM   tbinfraccion_dih_valanexoper tdv
         INNER JOIN tbinfraccion_dih td ON td."ID" = tdv.id_infracciondih
  WHERE  tdv.id_val_anexo_per = P_ValAnexoPerId;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

  PROCEDURE sp_ActualizarValoracion (p_Id                     IN NUMBER
                                   , p_EstadoId               IN NUMBER
                                   , p_FechaAsignacion        IN DATE
                                   , p_ValoradorId            IN NUMBER
                                   , p_AsignadorId            IN NUMBER
                                   , p_FechaValoracion        IN DATE
                                   , p_FechaRealValoracion    IN DATE
                                   , p_Motivacion_Inclusion   IN CLOB
                                   , p_Motivacion_NoInclusion IN CLOB
                                   , p_ResuelveArticulo1      IN CLOB
                                   , p_ResuelveArticulo2      IN CLOB
                                   , p_EsDeclaracion          IN NUMBER
                                   , p_Observacion            IN CLOB
                                   , p_Finalizar              IN NUMBER
                                   , p_CantidadAfectadas      OUT NUMBER) AS
    xIdDeclaracion        NUMBER;
    vMotivaciones         NUMBER;
    vNumeroFormulario     VARCHAR2(50);
    vTipoDocumentoAA      NUMBER := 2;
    vIdActoAdministrativo NUMBER;
    vIdConsecutivoAA      VARCHAR2(50);
  BEGIN
    SELECT ID_DECLARACION INTO xIdDeclaracion FROM TBVALORACION WHERE ID = p_Id;
    p_CantidadAfectadas := 0;
    
    -- Actualizar valores acto administrativo
    SELECT COUNT(*) INTO vMotivaciones FROM TBVALORACION_MOTIVACION VM WHERE VM.ID_VALORACION = p_Id;
    IF vMotivaciones > 0 THEN
      UPDATE TBVALORACION_MOTIVACION SET MOTIVACION_INCLUSION   = p_Motivacion_Inclusion
                                       , MOTIVACION_NOINCLUSION = p_Motivacion_NoInclusion
                                       , RESUELVE_ARTICULO1     = p_ResuelveArticulo1
                                       , RESUELVE_ARTICULO2     = p_ResuelveArticulo2
      WHERE ID_VALORACION = p_Id;
    ELSE
      INSERT INTO TBVALORACION_MOTIVACION (ID
                                         , ID_VALORACION
                                         , MOTIVACION_INCLUSION
                                         , MOTIVACION_NOINCLUSION
                                         , RESUELVE_ARTICULO1
                                         , RESUELVE_ARTICULO2)
      VALUES (SEQ_TBVALORACION_MOTIVACION.NextVal
            , P_Id
            , p_Motivacion_Inclusion
            , p_Motivacion_NoInclusion
            , p_ResuelveArticulo1
            , p_ResuelveArticulo2);
    END IF;
    
    -- Actualizar estados de declaracion de acuerdo al estado de valoracion
    IF p_EstadoId = VALORACION_DEVUELTA_ASI THEN
      PKG_COMMON.sp_UpdEstado_Declaracion(xIdDeclaracion, p_ValoradorId, VALORACION_PEND_PORASIGNAR);
    ELSIF p_EstadoId = VALORACION_ASIGNADA THEN
      PKG_COMMON.sp_UpdEstado_Declaracion(xIdDeclaracion, p_ValoradorId, VALORACION_PEND_PORVALORAR);
    ELSIF p_EstadoId = VALORACION_EN_PROCESO THEN
      PKG_COMMON.sp_UpdEstado_Declaracion(xIdDeclaracion, p_ValoradorId, VALORACION_EN_VALORACION);
    END IF;
    
    IF p_Finalizar = 1 THEN
      PKG_ACTOSADMIN.sp_SetActoAdministrativoRUV(xIdDeclaracion, 0, '', vNumeroFormulario, '', '', p_ValoradorId, GENERADO, vTipoDocumentoAA, vIdActoAdministrativo, vIdConsecutivoAA);
      -- TODO: Review. Se necesita crear el registro con valores sin inicializar?? Quien hizo esto?
      DECLARE
        vIdUsuarioLider NUMBER;
      BEGIN
        BEGIN
          SELECT ID_LIDERGRUPO INTO vIdUsuarioLider FROM TBGRUPOSVALORACION
          WHERE ID_VALORADOR = p_ValoradorId AND ROWNUM = 1;
        EXCEPTION WHEN NO_DATA_FOUND THEN
          vIdUsuarioLider := PKG_COMMON.f_UsuarioMenosCarga(LIDER_VALORACION);
        END;
        PKG_COMMON.sp_UpdEstado_Declaracion(xIdDeclaracion, vIdUsuarioLider, VALORACION_PEND_REVISION);
      END;
    END IF;
    
    -- Actualizar registro de Valoracion
    UPDATE TBVALORACION SET ID_ESTADO_VAL        = p_EstadoId,
                            FECHAASIGNACION      = p_FechaAsignacion,
                            ID_VALORADOR         = p_ValoradorId,
                            ID_ASIGNADOR         = p_AsignadorId,
                            FECHAVALORACION      = p_FechaValoracion,
                            FECHAVALORACIONREAL  = p_FechaRealValoracion,
                            ESDECLARACION        = p_EsDeclaracion,
                            OBSERVACION          = p_Observacion,
                            IDACTOADMINISTRATIVO = vIdActoAdministrativo
    WHERE ID = p_Id;

    p_CantidadAfectadas := SQL%ROWCOUNT;
    
    IF p_CantidadAfectadas < 1 THEN
      RAISE_APPLICATION_ERROR(-20054, 'No se pudo actualizar el registro de valoraci贸n con ID :' + p_Id + '. Se ha perdido la informaci贸n de sesi贸n. Por favor, intente de nuevo');
    END IF;

  END;

/***********************************************************
* Procedure description: Actualiza TbValoracion_Anexo
* Date:   29/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_ActualizarValAnexo
(
  P_Id IN NUMBER,
  P_UltimaFechaEdicion IN DATE,
  P_CantidadAfecadas OUT NUMBER
)
IS
BEGIN

  P_CantidadAfecadas := 0;

  UPDATE tbvaloracion_anexo
  SET
    ultima_fechaedicion = P_UltimaFechaEdicion
  WHERE ID = P_Id;

  P_CantidadAfecadas := SQL%ROWCOUNT;
END;



/***********************************************************
* Procedure description: trae TbValoracion_Anexo por Id
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetValAnexoPorId
(
  P_Id IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN

  OPEN P_Result FOR
  SELECT
    ta."ID",
    ta.id_valoracion,
    ta.ultima_fechaedicion,
    ta.tipo_anexo,
    ta.id_siniestro
  FROM
    tbvaloracion_anexo ta
  WHERE ta."ID" = P_Id;

EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;



/***********************************************************
* Procedure description: Ingresa una herramienta de valoracion
* Date:   29/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_InsertarTbValHerramienta
(
 P_ID_VAL_ANEXO_PER       IN NUMBER,
 P_ID_HERRAMIENTA         IN NUMBER,
 P_DETALLES               IN CLOB,
 P_FECHA                  IN DATE,
 P_USAPARADESICION        IN NUMBER,
 P_AFECTADAS              OUT NUMBER
)
IS
BEGIN
  INSERT INTO tbherramienta_anexo_per
  (
    id_valanexo_per,
    id_herramienta,
    detalle,
    fecha,
    usaparadesicion
  )
  VALUES
  (
    P_ID_VAL_ANEXO_PER,
    P_ID_HERRAMIENTA,
    P_DETALLES,
    P_FECHA,
    p_usaparadesicion
  );

  P_AFECTADAS := SQL%ROWCOUNT;
END;



/***********************************************************
* Procedure description: Eliminar TbHerramienta_Anexo por Id
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_EliminarTbHerrAnexo
(
  P_Id            IN NUMBER,
  P_Afectadas     OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbherramienta_anexo_per
  WHERE id_valanexo_per = P_Id;

  P_Afectadas := SQL%ROWCOUNT;
END;


/***********************************************************
* Procedure description: Insertar TbHerramienta_Val
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_InsertarTbHerrVal
(
  P_ID             OUT NUMBER,
  P_ID_TIPO_HERR   IN NUMBER,
  P_NOMBRE         IN VARCHAR2,
    P_TEXTO          IN VARCHAR2
)
IS
  v_cantidad NUMBER;
BEGIN

  SELECT COUNT(*) INTO v_cantidad FROM tbherramientaval t WHERE t.nombre = p_nombre;

  IF(v_cantidad = 0) THEN

    P_ID := SEQ_TBHERRAMIENTAVAL.NEXTVAL;

    INSERT INTO tbherramientaval
    (
      "ID",
      id_tipo_herramienta,
      nombre,
      texto
    )
    VALUES
    (
      p_id,
      p_id_tipo_herr,
      p_nombre,
      p_texto
    );
  ELSE
    SELECT t."ID" INTO P_ID FROM tbherramientaval t WHERE t.nombre = p_nombre;
  END IF;
END;






/***********************************************************
* Procedure description: Actualizar tbValAnexoPersona
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_ActualizarValAnexoPersona
(
  P_ID                    IN NUMBER,
  P_ID_REGPERSONA         IN NUMBER DEFAULT NULL,
  P_ID_OBSERVACION_VAL    IN NUMBER DEFAULT NULL,
  P_ID_ESTADO_VAL         IN NUMBER DEFAULT NULL,
  P_ESVICITMA             IN NUMBER DEFAULT NULL,
  P_ESAFECTADO            IN NUMBER DEFAULT NULL,
  P_ID_VAL_ANEXO          IN NUMBER DEFAULT NULL,
  P_OBSERVACION           IN CLOB,
  P_AFECTADAS             OUT NUMBER
)
IS
BEGIN
  UPDATE tbval_anexo_persona
  SET
    id_regpersona = P_ID_REGPERSONA,
    id_observacion_val = P_ID_OBSERVACION_VAL,
    id_estado_val = P_ID_ESTADO_VAL,
    esvicitma = P_ESVICITMA,
    esafectado = P_ESAFECTADO,
    id_val_anexo = P_ID_VAL_ANEXO,
    observacion = P_OBSERVACION
  WHERE "ID" = P_ID;

  P_AFECTADAS := SQL%ROWCOUNT;

END;

/***********************************************************
* Procedure description: Traer tbValAnexoPersona Por Id
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetValAnexoPersonaPorId
(
  P_ID                    IN NUMBER,
  P_RESULT                OUT SYS_REFCURSOR
)
IS
BEGIN

  OPEN p_result FOR
  SELECT
    tap."ID",
    tap.id_regpersona,
    tap.id_observacion_val,
    tap.id_estado_val,
    tap.esvicitma,
    tap.esafectado,
    tap.id_val_anexo,
    tap.observacion
  FROM
    tbval_anexo_persona tap
  WHERE tap."ID" = p_id;
END;


/***********************************************************
* Procedure description: Inserta en TbAfectacion_Val
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_InsertarTbAfectacionVal
(
  P_id_valanexoperson     IN NUMBER,
  P_param_afectacion      IN NUMBER,
  P_Afectadas             OUT NUMBER
)
IS
BEGIN


  INSERT INTO tbafectacion_val
  (
    id_valanexoperson,
    param_afectacion
  )
  VALUES
  (
    P_id_valanexoperson,
    P_param_afectacion
  );

  P_Afectadas := SQL%ROWCOUNT;

  COMMIT;


EXCEPTION
  WHEN OTHERS THEN
    RAISE;
END;


/***********************************************************
* Procedure description: Eliminar registro de TbAfectacion_Val
* Date:   30/03/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_EliminarTbAfectacionVal
(
  P_ID             IN NUMBER,
  P_AFECTADAS      OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbafectacion_val
  WHERE id_valanexoperson = p_id;

  P_AFECTADAS := SQL%ROWCOUNT;
END;




/***********************************************************
* Procedure description: Insertar en TbAutorHvValAnexo
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_InsertarTbAutorHvAnexoPer
(
  P_ID_AUTOR          IN NUMBER,
  P_ID_VAL_ANEXO_PER  IN NUMBER,
  P_Afectadas         OUT NUMBER
)
IS
BEGIN

  INSERT INTO tbautorhv_val_anexo
  (
    id_autorhv,
    id_val_anexo_persona
  )
  VALUES
  (
    P_ID_AUTOR,
    P_ID_VAL_ANEXO_PER
  );
  P_Afectadas := SQL%ROWCOUNT;
END;


/***********************************************************
* Procedure description: Eliminar de TbAutorHvValAnexo
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************  
*
************************************************************/
PROCEDURE  sp_EliminaTbAutorValAnexoPer
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbautorhv_val_anexo
  WHERE id_val_anexo_persona = P_ID_ValAnexoPer;

  P_AFECTADAS := SQL%ROWCOUNT;
END;

/***********************************************************
* Procedure description: Insertar en TbInfraccionDIHValAnexo
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_InsertarTbInfraccionDIH
(
  P_ID_INFRACCIONDIH      IN NUMBER,
  P_ID_VAL_ANEXO_PER      IN NUMBER,
  P_AFECTADAS             OUT NUMBER
)
IS
BEGIN

  INSERT INTO tbinfraccion_dih_valanexoper
  (
    id_infracciondih,
    id_val_anexo_per
  )
  VALUES
  (
    P_ID_INFRACCIONDIH,
    P_ID_VAL_ANEXO_PER
  );

  P_AFECTADAS := SQL%ROWCOUNT;
END;


/***********************************************************
* Procedure description: Eliminar de TbAutorHvValAnexo
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_EliminaTbInfraccionDIH
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbinfraccion_dih_valanexoper
  WHERE id_val_anexo_per = P_ID_ValAnexoPer;

  P_AFECTADAS := SQL%ROWCOUNT;
END;


/***********************************************************
* Procedure description: Insertar en TbPrincipioVal
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_InsertarTbPrincipioVal
(
  P_ID_PRINCIPIO          IN NUMBER,
  P_ID_VAL_ANEXO_PER      IN NUMBER,
  P_AFECTADAS             OUT NUMBER
)
IS
  ESDEVOLUCION NUMBER := 0;
  IDPRINCIPIO NUMBER := 0;
  ESTADO_VAL NUMBER := 0;
BEGIN

  select id_estado_val INTO ESTADO_VAL 
  from tbval_anexo_persona
  where id = P_ID_VAL_ANEXO_PER;

  SELECT COUNT(1)
  INTO   ESDEVOLUCION
  FROM   tbcausales C
  WHERE  C."ID" = p_id_principio
         AND C.tipo = 10034;
  
  --Ademas de que no sea por devolucion se debe comparar que no sea por no inclusion
  IF ESDEVOLUCION > 0 AND ESTADO_VAL <> 2 THEN
    SELECT P."ID"
    INTO   IDPRINCIPIO
    FROM   tbprincipio P
           INNER JOIN tbcausaldevolucion_principio tp ON tp.id_principio = P."ID"
    WHERE  TP.id_causal = p_id_principio;
  ELSE
    IDPRINCIPIO := P_ID_PRINCIPIO;
  END IF;

  --SELECT ID INTO IDPRINCIPIO FROM TBPRINCIPIO WHERE NOMBRE = (SELECT NOMBRECAUSAL FROM TBCAUSALES WHERE ID = P_ID_PRINCIPIO);

  INSERT INTO tbprincipio_val
  (
    id_principio,
    id_val_anexo_per
  )
  VALUES
  (
    IDPRINCIPIO,
    P_ID_VAL_ANEXO_PER
  );

  P_AFECTADAS := SQL%ROWCOUNT;
END;
/***********************************************************
* Procedure description: Insertar en TbPrincipioCausal
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_InsertarTbValoracionPri
(
  P_ID_PRINCIPIO          IN NUMBER,
  P_IDVALORACION          IN NUMBER,
  P_AFECTADAS             OUT NUMBER
)
IS
BEGIN

  INSERT INTO tbvaloracion_principo
  (
    id_principio,
    id_valoracion
  )
  VALUES
  (
    p_id_principio,
    p_idvaloracion
  );

  P_AFECTADAS := SQL%ROWCOUNT;

  COMMIT;


EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;


/***********************************************************
* Procedure description: Eliminar de tbprincipio_val
* Date:   04/04/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_EliminaTbPrincipioVal
(
  P_ID_ValAnexoPer IN NUMBER,
  P_AFECTADAS      OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbprincipio_val
  WHERE id_val_anexo_per = P_ID_ValAnexoPer;

  P_AFECTADAS := SQL%ROWCOUNT;
END;

/***********************************************************
* Procedure description: Eliminar de Valoracion_principio
* Date:   14/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_EliminaTbCausalValoracion
(
  P_ID_Valoracion  IN NUMBER,
  P_AFECTADAS      OUT NUMBER
)
IS
BEGIN

  DELETE FROM tbvaloracion_principo
  WHERE id_valoracion = P_ID_Valoracion;

  P_AFECTADAS := SQL%ROWCOUNT;

  COMMIT;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description: Trae Registros Anteriores
* Date:   22/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getRegistrosAnteriores
(
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    ta."ID",
    ta.nombre,
    ta.descripcion
  FROM
    tbregistros_anteriores ta
  ORDER BY ta.nombre;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;



/***********************************************************
* Procedure description: Trae Preguntas de Registro anetrior
* Date:   22/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getPreguntasRegAnteriores
(
  P_Result         OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    t."ID",
    t.nombre
  FROM
    tbparametros t
  WHERE t.id_tipoparametro = PREGUNTA_REGISTRO_ANT
  ORDER BY t.nombre ASC;


EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description: Trae Registros Anteriores Por Valoracion
* Date:   23/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getRegistrosAntPorValId
(
  P_ValoracionId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT
    tr."ID",
    tr.id_registro,
    tr.id_valoracion
  FROM
    tbregistros_anteriores ta
    INNER JOIN tbvaloracion_registros tr ON tr.id_registro = ta."ID"
  WHERE tr.id_valoracion = P_ValoracionId
  ORDER BY ta.nombre;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description: Trae Personas Por RegValId
* Date:   23/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getPersonasPorRegValId
(
  P_RegValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT tp.id_regpersona
  FROM   tbvaloracion_registros tr
         INNER JOIN tbvalreg_persona tp ON tp.id_valreg = tr.ID
  WHERE tr.ID = P_RegValId;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description: Trae Preguntas Por RegValId
* Date:   23/05/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getPreguntasPorRegValId
(
  P_RegValId IN NUMBER,
  P_Result OUT SYS_REFCURSOR
)
IS
BEGIN
  OPEN P_Result FOR
  SELECT tp.param_pregunta
  FROM   tbvaloracion_registros tr
         INNER JOIN tbvalreg_pregunta tp ON tp.id_valreg = tr."ID"
  WHERE tr.ID = P_RegValId;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

  PROCEDURE sp_InsertarRegistroAnterior(p_RegistroId   IN NUMBER
                                      , p_ValoracionId IN NUMBER
                                      , p_Id           OUT NUMBER) IS
  BEGIN
    INSERT INTO TBVALORACION_REGISTROS (ID, ID_REGISTRO, ID_VALORACION) VALUES (SQL_TBVALORACION_REGISTRO.NextVal, p_RegistroId, p_ValoracionId)
    RETURNING ID INTO p_Id;
  END;

  PROCEDURE sp_ActualizarRegistroAnterior(p_Id           IN NUMBER
                                        , p_RegistroId   IN NUMBER
                                        , p_ValoracionId IN NUMBER) IS
  BEGIN
    UPDATE TBVALORACION_REGISTROS SET ID_REGISTRO   = p_RegistroId
                                    , ID_VALORACION = p_ValoracionId
    WHERE  ID = p_Id;
  END;


  PROCEDURE sp_EliminarRegistroAnterior(p_Id IN NUMBER) IS
  BEGIN
    DELETE FROM TBVALREG_PERSONA WHERE ID_VALREG = p_ID;
    DELETE FROM TBVALREG_PREGUNTA WHERE ID_VALREG = p_ID;
    DELETE FROM TBVALORACION_REGISTROS WHERE ID = p_ID;
  END;

  PROCEDURE sp_InsertarRegistroAntPersona(p_RegistroAnteriorId IN NUMBER
                                        , p_RegPersonaId       IN NUMBER) IS
    xCountRegistries INT;
  BEGIN
    SELECT COUNT(1) INTO xCountRegistries FROM TBVALREG_PERSONA WHERE ID_VALREG = p_RegistroAnteriorId AND ID_REGPERSONA = p_RegPersonaId;
    IF xCountRegistries = 0 THEN
      INSERT INTO TBVALREG_PERSONA (ID_VALREG, ID_REGPERSONA) VALUES (p_RegistroAnteriorId, p_RegPersonaId);
    END IF;
  END;

  PROCEDURE sp_InsertarRegistroAntPregunta(p_RegistroAnteriorId IN NUMBER
                                         , p_PreguntaId         IN NUMBER) IS
    xCountRegistries INT;
  BEGIN
    SELECT COUNT(1) INTO xCountRegistries FROM TBVALREG_PREGUNTA WHERE ID_VALREG = p_RegistroAnteriorId AND PARAM_PREGUNTA = p_PreguntaId;
    IF xCountRegistries = 0 THEN
      INSERT INTO TBVALREG_PREGUNTA (ID_VALREG, PARAM_PREGUNTA) VALUES (p_RegistroAnteriorId, p_PreguntaId);
    END IF;
  END;

/***********************************************************
* Procedure description:
* Date:   13/06/2012
* Author: luis.esteban
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_getResumenValoracion
(
  P_ValoracionID IN NUMBER,
  P_ResultDeclaracion OUT SYS_REFCURSOR,
  P_ResultHechos OUT SYS_REFCURSOR,
  P_ResultPersonas OUT SYS_REFCURSOR
)
AS
BEGIN

  OPEN P_ResultDeclaracion FOR
  SELECT  d."ID",
          d.fechadeclaracion,
          rad.fechallegada FechaRadicado,
          d.numeroformulario,
          (SELECT ut.nombre  FROM tbunidadesterritoriales ut WHERE ut."ID" = d.id_uterritorial) UnidadTerritorial,
          (SELECT dto.nombre FROM tbgeografia dto WHERE dto."ID" = d.id_departamentodeclaracion) Departamento,
          (SELECT mun.nombre FROM tbgeografia mun WHERE mun."ID" = d.id_municipiodeclaracion) Municipio,
          (SELECT est.nombre FROM tbparametros est WHERE est."ID"= d.param_estado) EstadoDeclaracion,
          (SELECT us.nombre FROM tbusuarios us WHERE us."ID" = v.id_valorador) Valorador,
          v.fechaasignacion,
          v.fechavaloracion
  FROM    tbdeclaraciones d
          INNER JOIN tbvaloracion v ON v.id_declaracion = d."ID"
          INNER JOIN tbestadoprocesos ep ON ep.id_proceso = d."ID" AND ep.param_estado = 770
          INNER JOIN tbradicacion rad ON rad."ID" = ep.id_detalle_radicacion
  WHERE   v."ID" = P_ValoracionID;

  OPEN P_ResultHechos FOR
  SELECT  va."ID" Id,
          (SELECT th.nombre FROM tbparametros th WHERE th."ID" = va.tipo_anexo) HechoVictimizante,
          sp.fechasiniestro,
          (SELECT te.nombre FROM tbparametros te WHERE te.ID = sp.param_tipo_entorno) TipoEntorno,
          sp.otro_localidad_correg AS LocalidadCorregimiento,
          sp.otro_barrio_vereda AS BarrioVereda,
          (SELECT dto.nombre FROM tbgeografia dto WHERE dto."ID" = sp.id_departamento) Departamento,
          (SELECT mun.nombre FROM tbgeografia mun WHERE mun."ID" = sp.id_municipio) Municipio
  FROM    tbdeclaraciones d
          INNER JOIN tbvaloracion v ON v.id_declaracion = d."ID"
          INNER JOIN tbvaloracion_anexo va ON va.id_valoracion = v."ID"
          INNER JOIN tbsiniestros_persona sp ON sp."ID" = va.id_siniestro
  WHERE   v."ID" = P_ValoracionID;

  OPEN P_ResultPersonas FOR
  SELECT  vap."ID" Id,
          vap.id_val_anexo,
          pkg_common.f_getnombrecompletopersona(rp.id_persona) Nombre,
          (SELECT tdoc.nombre FROM tbparametros tdoc WHERE tdoc."ID" = p.param_tipodocumento) TipoDocumento,
          p.numerodocumento,
          (SELECT rel.nombre FROM tbparametros rel WHERE rel."ID" = rp.param_relacion) Relacion,
          (SELECT gen.nombre FROM tbparametros gen WHERE gen."ID" = p.param_genero) Genero,
          trunc ( months_between( SYSDATE, p.fechanacimiento)/12 ) AS Edad,
          p.param_etniapertenece AS EtniaId,
          (SELECT et.nombre FROM tbparametros et WHERE et."ID" = p.param_etniapertenece) AS Etnia,
          (select CASE WHEN count(*) > 0 THEN 'SI' ELSE 'NO' END from TBDISCAPACIDAD_PERSONA DP where DP.ID_REGPERSONA = RP.ID) as Discapacitado,
          (SELECT  CASE t.fallecida WHEN 1 THEN 'SI' ELSE 'NO' END
           FROM    tbanexo6 t
           WHERE   t.ID = vap.id_anexo AND t.id_regpersona = vap.id_regpersona) AS Fallecida,
           (SELECT CASE t.desaparecida WHEN 1 THEN 'SI' ELSE 'NO' END
           FROM   tbanexo4 t
           WHERE  t.ID = vap.id_anexo AND t.id_regpersona = vap.id_regpersona) AS Desaparecida,
          (SELECT  CASE t.secuestrado WHEN 1 THEN 'SI' ELSE 'NO' END
           FROM    tbanexo8 t
           WHERE   t.ID = vap.id_anexo AND t.id_regpersona = vap.id_regpersona) AS Secuestrado,
          (SELECT  t2.nombre
           FROM    tbanexo7 t
                   INNER JOIN tbparametros t2 ON t2."ID" = t.param_estadovictima
           WHERE   t.ID = vap.id_anexo AND t.id_regpersona = vap.id_regpersona) AS EstadoPorMina,
          (SELECT CASE td.se_desplazo WHEN 1 THEN 'SI' ELSE 'NO' END
           FROM   tbanexo5 t
                  INNER JOIN tbanexo5_desplazados td ON td.id_anexo5 = t.ID
           WHERE  t.ID = vap.id_anexo AND td.id_regpersona = vap.id_regpersona) AS SeDesplazo,
          (CASE WHEN vap.esafectado IS NULL THEN CASE PKG_COMMON.f_personaafectada(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) WHEN 1 THEN 'SI' ELSE 'NO' END ELSE CASE vap.esafectado WHEN 1 THEN 'SI' ELSE 'NO' END END) AS esafectado,
          (CASE WHEN vap.esvicitma IS NULL THEN CASE PKG_COMMON.f_personavictma(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) WHEN 1 THEN 'SI' ELSE 'NO' END ELSE CASE vap.esvicitma WHEN 1 THEN 'SI' ELSE 'NO' END END) AS esvicitma,
          pkg_common.f_getafectaciones(vap.id_anexo, va.tipo_anexo) Afectaciones,
          (SELECT ev.nombre FROM tbestado_val ev WHERE ev."ID"= vap.id_estado_val) EstadoValoracion,
          (SELECT ov.nombre FROM tbobservacion_val ov WHERE ov."ID" = vap.id_observacion_val) ObservacionValoracion,
          PKG_COMMON.f_principios(vap."ID") Principios
  FROM    tbdeclaraciones d
          INNER JOIN tbvaloracion v ON v.id_declaracion = d."ID"
          INNER JOIN tbvaloracion_anexo va ON va.id_valoracion = v."ID"
          INNER JOIN tbval_anexo_persona vap ON vap.id_val_anexo = va."ID"
          INNER JOIN tbregistros_personas rp ON rp."ID" = vap.id_regpersona
          INNER JOIN tbpersonas p ON p."ID" = rp.id_persona
  WHERE   v."ID" = P_ValoracionID;


EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;


/***********************************************************
* Procedure description:sp_GetGeografia
* Date:   26/06/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_GetGeografia
(
  P_Result  OUT SYS_REFCURSOR
)
AS
BEGIN

  OPEN P_Result FOR
  SELECT g."ID",
         g.nombre Nombre,
         2 Tipo,
         0 Padre
         --3 Tipo,
         --g.padreid Padre
  FROM   tbgeografia g
  WHERE  g.padreid = 48
  UNION ALL
  SELECT g."ID",
         g.nombre Nombre,
         3 Tipo,
         g.padreid Padre
  FROM   tbgeografia g
  WHERE  g.padreid IN(
    SELECT g."ID"
    FROM   tbgeografia g
    WHERE  g.padreid = 48
    )
  UNION ALL
  SELECT tp."ID",
         tp.nombre Nombre,
         4 Tipo,
         0 Padre
  FROM   tbparametros tp
  WHERE  tp."ID" IN(127, 128)
  UNION ALL
  SELECT p."ID",
         p.poblado Nombre,
         5 Tipo,
         p.id_municipio Padre
  FROM   tbpoblados p
  WHERE  p.id_entorno IS NOT NULL
  UNION ALL
  SELECT p."ID",
         p.poblado Nombre,
         6 Tipo,
         p.id_municipio Padre
  FROM   tbpoblados p
  WHERE  p.id_entorno IS NOT NULL;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description:sp_CrearHecho
* Date:   26/06/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_CrearHecho
(
  P_TipoHecho             IN NUMBER,
  P_Fecha                 IN DATE,
  P_Departamento          IN NUMBER,
  P_Municipio             IN NUMBER,
  p_TipoEntorno           IN NUMBER,
  P_CorrLoc               IN NUMBER,
  P_BarrVer               IN NUMBER,
  p_OtroCorLoc            VARCHAR2,
  P_OtroBarVer            VARCHAR2,
  P_Victima1              IN NUMBER,
  P_Valoracion            IN NUMBER,
  P_ValAnexo              OUT NUMBER
)
AS
  V_NUMEROANEXO           NUMBER := 0;
  V_SiniestroId           NUMBER := 0;
BEGIN

    SELECT t.numero
    INTO   V_NUMEROANEXO
    FROM   tbparametros t
    WHERE  t."ID" = P_TipoHecho;

    V_SiniestroId := SEQ_SINIESTROPERSONA.NEXTVAL;

    INSERT INTO tbsiniestros_persona
    (
      ID,
      param_tipohecho,
      id_regpersona,
      fechasiniestro,
      id_departamento,
      id_municipio,
      activo,
      param_localidad_correg,
      param_barrio_vereda,
      otro_localidad_correg,
      otro_barrio_vereda,
      param_tipo_entorno,
      HECHOENVALORACION
    )
    VALUES
    (
      V_SiniestroId,
      V_NUMEROANEXO,
      P_Victima1,
      P_Fecha,
      P_Departamento,
      P_Municipio,
      1,
      P_CorrLoc,
      P_BarrVer,
      p_OtroCorLoc,
      P_OtroBarVer,
      p_TipoEntorno,
      HECHOAGREGADOVALORACION
    );

    P_ValAnexo := SEQ_ANEXO_VAL.NEXTVAL;

    INSERT INTO tbvaloracion_anexo
    (
      "ID",
      id_valoracion,
      ultima_fechaedicion,
      tipo_anexo,
      id_siniestro
    )
    VALUES
    (
      P_ValAnexo,
      P_Valoracion,
      P_Fecha,
      V_NUMEROANEXO,
      V_SiniestroId
    );

    COMMIT;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description:sp_CrearHecho
* Date:   26/06/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE sp_CrearAnexo
(
  P_ValAnexoId             IN NUMBER,
  P_RegPersona             IN NUMBER,
  P_EstadoHecho            IN NUMBER
)
AS
  V_TipoHecho              NUMBER := 0;
  V_TipoAnexo              NUMBER := 0;
  V_Siniestro              NUMBER := 0;
  V_AnexoId                NUMBER := 0;

BEGIN

  SELECT va.tipo_anexo,
         va.id_siniestro,
         tp.param_tipohecho
  INTO   V_TipoHecho,
         V_Siniestro,
         V_TipoAnexo
  FROM   tbvaloracion_anexo va
         INNER JOIN tbsiniestros_persona tp ON tp."ID" = va.id_siniestro
  WHERE  va."ID" = P_ValAnexoId;


  IF V_TipoAnexo = 1 THEN
      V_AnexoId := SEQ_ANEXO1.NEXTVAL;

      INSERT INTO tbanexo1
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        1
      );
  END IF;

  IF V_TipoAnexo = 2 THEN
      V_AnexoId := SEQ_ANEXO2.NEXTVAL;

      INSERT INTO tbanexo2
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        1
      );
  END IF;

  IF V_TipoAnexo = 3 THEN
      V_AnexoId := SEQ_ANEXO3.NEXTVAL;

      INSERT INTO tbanexo3
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        1
      );
  END IF;

  IF V_TipoAnexo = 4 THEN
      V_AnexoId := SEQ_ANEXO4.NEXTVAL;

      INSERT INTO tbanexo4
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        desaparecida,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        P_EstadoHecho,
        1
      );
  END IF;

  IF V_TipoAnexo = 5 THEN
      V_AnexoId := SEQ_ANEXO5.NEXTVAL;

      INSERT INTO tbanexo5
      (
        "ID",
        id_siniestro,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        1
      );

      INSERT INTO tbanexo5_desplazados
      (
        "ID",
        id_anexo5,
        id_regpersona,
        se_desplazo,
        activo
      )
      VALUES
      (
        SEQ_ANEXO5_DESPLAZADO.NEXTVAL,
        V_AnexoId,
        P_RegPersona,
        P_EstadoHecho,
        1
      );

  END IF;

  IF V_TipoAnexo = 6 THEN
      V_AnexoId := SEQ_ANEXO6.NEXTVAL;

      INSERT INTO tbanexo6
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        fallecida,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        P_EstadoHecho,
        1
      );
  END IF;

    IF V_TipoAnexo = 7 THEN
      V_AnexoId := SEQ_ANEXO7.NEXTVAL;

      INSERT INTO tbanexo7
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        param_estadovictima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        P_EstadoHecho,
        1
      );
  END IF;

  IF V_TipoAnexo = 8 THEN
      V_AnexoId := SEQ_ANEXO8.NEXTVAL;

      INSERT INTO tbanexo8
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        secuestrado,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        P_EstadoHecho,
        1
      );
  END IF;

  IF V_TipoAnexo = 9 THEN
      V_AnexoId := SEQ_ANEXO9.NEXTVAL;

      INSERT INTO tbanexo9
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        1
      );
  END IF;

  IF V_TipoAnexo = 10 THEN
      V_AnexoId := SEQ_ANEXO10.NEXTVAL;

      INSERT INTO tbanexo10
      (
        "ID",
        id_siniestro,
        id_regpersona,
        afectado,
        victima,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        P_RegPersona,
        0,
        0,
        1
      );
  END IF;

  IF V_TipoAnexo = 11 THEN
      V_AnexoId := SEQ_ANEXO11.NEXTVAL;

      INSERT INTO tbanexo11
      (
        "ID",
        id_siniestro,
        activo
      )
      VALUES
      (
        V_AnexoId,
        V_Siniestro,
        1
      );

  END IF;

  DBMS_OUTPUT.PUT_LINE('pASO1');

  INSERT INTO tbval_anexo_persona
  (
    "ID",
    id_regpersona,
    id_val_anexo,
    esvicitma,
    esafectado
  )
  VALUES
  (
    SEQ_VAL_ANEXO_PER.NEXTVAL,
    P_RegPersona,
    P_ValAnexoId,
    0,
    0
  );

  DBMS_OUTPUT.PUT_LINE('pASO2');
  COMMIT;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;
/***********************************************************
 * Procedure description:
 * Date:   02/08/2012
 * Author: Cristian Alejandro Neira
 * Changes
 * Date    Modified By      Comments
 ************************************************************
 *
 ************************************************************/
PROCEDURE sp_AsignarValoracionAutomatico
(
  P_ID_Declaracion        NUMBER,
  P_ID_UsuarioAsigna      NUMBER
)
AS
  V_ValoracionID NUMBER := 0;
  V_CantidadPersonas NUMBER := 0;
  v_Cantidad NUMBER := 0;
  V_MenorCantidad NUMBER := 0;
  V_UsMenorCantidad  NUMBER := 0;
 BEGIN

  IF P_ID_Declaracion IS NULL THEN
     DECLARE CURSOR cur_Declaraciones IS
     SELECT d.ID
     FROM   tbdeclaraciones d
            INNER JOIN tbestadoprocesos ep ON ep.id_proceso = d."ID"
     WHERE  d.param_estado = 702
            AND ep.param_estado = 770
            AND ROWNUM <= 100;
     BEGIN
       FOR decla IN cur_Declaraciones LOOP

           DECLARE CURSOR cur_Hechos IS
           SELECT  sp."ID",
                   sp.param_tipohecho
          FROM    tbregistros_personas rp
                  INNER JOIN tbsiniestros_persona sp ON sp.id_regpersona = rp."ID"
          WHERE   rp.id_declaracion = decla.ID;

          BEGIN
              FOR hecho IN cur_Hechos LOOP
                    V_CantidadPersonas := V_CantidadPersonas + PKG_COMMON.f_getCantidadPersonasPorHecho(hecho.param_tipohecho, hecho.ID);
              END LOOP;
          END;

          DECLARE CURSOR cur_Usuarios IS
          SELECT u."ID"
          FROM   tbusuarios u
                 INNER JOIN tbroles_usuario ru ON ru.id_usuario = u."ID"
          WHERE  ru.id_rol = VALORADOR_ROL
                 and u.id <> 19326;

          BEGIN
            v_Cantidad := 0;
                FOR us IN cur_Usuarios LOOP
                  SELECT COUNT(*)
                  INTO   v_Cantidad
                  FROM   tbvaloracion v
                         INNER JOIN tbvaloracion_anexo va ON va.id_valoracion = v."ID"
                         INNER JOIN tbval_anexo_persona vap ON vap.id_val_anexo = va."ID"
                  WHERE  v.id_valorador = us.ID;
                  V_ValoracionID := 0;

                  IF V_MenorCantidad = 0 THEN
                     V_UsMenorCantidad := us.ID;
                     --V_MenorCantidad := v_Cantidad;
                     DBMS_OUTPUT.PUT_LINE(us.ID);
                     PKG_VALORACION.sp_asignarvaloracion(V_ValoracionID, decla.ID, VALORACION_ASIGNADA, V_UsMenorCantidad, P_ID_UsuarioAsigna);
                     EXIT;
                  END IF;
                  IF v_Cantidad < V_MenorCantidad THEN
                    V_UsMenorCantidad := us.ID;
                  END IF;
                END LOOP;
                PKG_VALORACION.sp_asignarvaloracion(V_ValoracionID, decla.ID, VALORACION_ASIGNADA, V_UsMenorCantidad, P_ID_UsuarioAsigna);
          END;
      END LOOP;
    END;
  ELSE
    DECLARE CURSOR cur_Usuarios IS
    SELECT u."ID"
    FROM   tbusuarios u
           INNER JOIN tbroles_usuario ru ON ru.id_usuario = u."ID"
    WHERE  ru.id_rol = VALORADOR_ROL;

    BEGIN
          FOR us IN cur_Usuarios LOOP
            SELECT COUNT(*)
            INTO   v_Cantidad
            FROM   tbvaloracion v
                   INNER JOIN tbvaloracion_anexo va ON va.id_valoracion = v."ID"
                   INNER JOIN tbval_anexo_persona vap ON vap.id_val_anexo = va."ID"
            WHERE  v.id_valorador = us.ID;

            IF V_MenorCantidad = 0 THEN
               V_UsMenorCantidad := us.ID;
               V_MenorCantidad := v_Cantidad;
            END IF;
            IF v_Cantidad < V_MenorCantidad THEN
              V_UsMenorCantidad := us.ID;
            END IF;
          END LOOP;
    END;
    --PKG_VALORACION.sp_asignarvaloracion(V_ValoracionID, P_ID_Declaracion, VALORACION_ASIGNADA, V_UsMenorCantidad, P_ID_UsuarioAsigna);
  END IF;
 EXCEPTION
  WHEN OTHERS THEN
     DBMS_OUTPUT.PUT_LINE(SQLERRM);

END;

  PROCEDURE sp_GetValoracionFull (pi_IdValoracion IN NUMBER
                                , po_DetalleDeclaracion  OUT SYS_REFCURSOR
                                , po_Principios          OUT SYS_REFCURSOR
                                , po_RegistrosAnteriores OUT SYS_REFCURSOR
                                , po_Hechos              OUT SYS_REFCURSOR
                                , po_Personas            OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN po_DetalleDeclaracion FOR
      SELECT RGP.ID AS ID
           , NVL(PRS.PRIMERNOMBRE, ' ') ||
             CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE   END ||
             CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO  END ||
             CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRE_PERSONA
           , TDC.NOMBRE          AS TIPO_DOCUMENTO
           , PRS.NUMERODOCUMENTO AS NUMERODOCUMENTO
           , REL.NOMBRE          AS RELACION
           , GNR.NOMBRE          AS GENERO
           , PRS.FECHANACIMIENTO AS FECHANACIMIENTO
           , TRUNC(MONTHS_BETWEEN(SYSDATE, PRS.FECHANACIMIENTO) /12) AS EDAD
           , ETN.NOMBRE          AS ETNIA
           , CASE WHEN NVL(DSC.DISCAPACIDADES, 0) > 0 THEN 1 ELSE 0 END AS ES_DISCAPACITADO
           , replace(PKG_COMMON.f_gethechosvictimizantesper(RGP."ID"), ';', '<br />') AS Hechos
           --, VCT.HECHOSVICTIMIZANTES AS HECHOS
      FROM TBDECLARACIONES DCL
      INNER JOIN TBVALORACION         VAL ON DCL.ID = VAL.ID_DECLARACION
      LEFT  JOIN TBREGISTROS_PERSONAS RGP ON DCL.ID = RGP.ID_DECLARACION AND RGP.ACTIVO = 1
      LEFT  JOIN TBPERSONAS           PRS ON PRS.ID = RGP.ID_PERSONA
      LEFT  JOIN TBPARAMETROS         TDC ON TDC.ID = PRS.PARAM_TIPODOCUMENTO
      LEFT  JOIN TBPARAMETROS         GNR ON GNR.ID = PRS.PARAM_GENERO
      LEFT  JOIN TBPARAMETROS         ETN ON ETN.ID = PRS.PARAM_ETNIAPERTENECE
      LEFT  JOIN TBPARAMETROS         REL ON REL.ID = RGP.PARAM_RELACION
      LEFT  JOIN (SELECT ID_REGPERSONA, COUNT(1) AS DISCAPACIDADES
                  FROM TBDISCAPACIDAD_PERSONA
                  WHERE PARAM_DISCAPACIDAD <> 5104 GROUP BY ID_REGPERSONA) DSC ON DSC.ID_REGPERSONA = RGP.ID
      /*LEFT  JOIN (/*+ PUSH_PRED(ANX)  SELECT ANX.ID_REGPERSONA, LISTAGG('- ' || HVC.NOMBRE_HECHO_VICTIMIZANTE || ' (' || CANTIDAD || ')', '<br />') WITHIN GROUP (ORDER BY HVC.ID_HECHO_VICTIMIZANTE) AS HECHOSVICTIMIZANTES
                  FROM (SELECT ID_REGPERSONA, TIPO, COUNT(1) AS CANTIDAD
                        FROM (SELECT  A.ID_REGPERSONA, 1  TIPO FROM TBANEXO1 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 2  TIPO FROM TBANEXO2 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 3  TIPO FROM TBANEXO3 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 4  TIPO FROM TBANEXO4 A
                              UNION ALL
                              SELECT AD.ID_REGPERSONA, 5  TIPO FROM TBANEXO5_DESPLAZADOS AD
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 6  TIPO FROM TBANEXO6 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 7  TIPO FROM TBANEXO7 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 8  TIPO FROM TBANEXO8 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 9  TIPO FROM TBANEXO9 A
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 10 TIPO FROM TBANEXO10 A
                              UNION ALL
                              SELECT AM.ID_REGPERSONA, 11 TIPO FROM TBANEXO11_MUEBLES AM
                              UNION ALL
                              SELECT AI.ID_REGPERSONA, 11 TIPO FROM TBANEXO11_INMUEBLES AI
                              UNION ALL
                              SELECT  A.ID_REGPERSONA, 13 TIPO FROM TBANEXO13 A) GROUP BY ID_REGPERSONA, TIPO) ANX
                  INNER JOIN TBHECHOS_VICTIMIZANTES HVC ON HVC.ID_HECHO_VICTIMIZANTE = ANX.TIPO
                  GROUP BY ANX.ID_REGPERSONA) VCT ON VCT.ID_REGPERSONA = RGP.ID*/
      WHERE VAL.ID = pi_IdValoracion;

    OPEN po_Principios FOR
      SELECT PRI.ID
           , PRI.NOMBRE
           , PRI.TEXTO
           , PRI.ID_ESTADO_VAL
      FROM TBVALORACION_PRINCIPO PCP
      INNER JOIN TBPRINCIPIO PRI ON PRI.ID = PCP.ID_PRINCIPIO
      WHERE PCP.ID_VALORACION = pi_IdValoracion
      ORDER BY PRI.NOMBRE ASC;

    OPEN po_RegistrosAnteriores FOR
      SELECT VRA.ID
           , VRA.ID_REGISTRO
           , VRA.ID_VALORACION
      FROM TBREGISTROS_ANTERIORES RGA
      INNER JOIN TBVALORACION_REGISTROS VRA ON VRA.ID_REGISTRO = RGA.ID
      WHERE VRA.ID_VALORACION = pi_IdValoracion
      ORDER BY RGA.NOMBRE;

    OPEN po_Hechos FOR
      SELECT VAX.ID                    AS ID
           , VAL.ID_DECLARACION        AS ID_DECLARACION
           , SNP.PARAM_TIPOHECHO       AS PARAM_TIPOHECHO
           , CASE WHEN SNP.PARAM_TIPOHECHO <= 11 THEN PHV.NOMBRE WHEN SNP.PARAM_TIPOHECHO = 12 THEN 'Otro' ELSE 'Censo Evento Masivo' END AS TipoHecho
           , SNP.FECHASINIESTRO        AS Fecha
           , ENT.NOMBRE                AS TipoEntorno
           , SNP.OTRO_LOCALIDAD_CORREG AS LocalidadCorregimiento
           , SNP.OTRO_BARRIO_VEREDA    AS BarrioVereda
           , DTO.NOMBRE                AS Departamento
           , MUN.NOMBRE                AS Municipio
           , VAX.TIPO_ANEXO            AS TipoHechoId
           , PKG_COMMON.f_GetCantidadPersonasPorHecho(VAX.TIPO_ANEXO, VAX.ID_SINIESTRO) AS TotalPersonas
           , PKG_COMMON.f_GetNombreCompletoPersona(RGP.ID_PERSONA)                      AS Victima1
      FROM TBVALORACION VAL
      INNER JOIN TBVALORACION_ANEXO   VAX ON VAL.ID = VAX.ID_VALORACION
      INNER JOIN TBSINIESTROS_PERSONA SNP ON SNP.ID = VAX.ID_SINIESTRO
      INNER JOIN TBREGISTROS_PERSONAS RGP ON RGP.ID = SNP.ID_REGPERSONA
      LEFT  JOIN TBGEOGRAFIA          DTO ON DTO.ID = SNP.ID_DEPARTAMENTO
      LEFT  JOIN TBGEOGRAFIA          MUN ON MUN.ID = SNP.ID_MUNICIPIO
      LEFT  JOIN TBPARAMETROS         PHV ON PHV.NUMERO = SNP.PARAM_TIPOHECHO AND PHV.ID_TIPOPARAMETRO = 2137
      LEFT  JOIN TBPARAMETROS         ENT ON ENT.ID = SNP.PARAM_TIPO_ENTORNO
      WHERE VAL.ID = Pi_IdValoracion;

    OPEN po_Personas FOR
      SELECT  vap."ID",
              pkg_common.f_getnombrecompletopersona(rp.id_persona) AS Persona,
              vap.id_regpersona,
              (SELECT td.nombre   FROM tbparametros td WHERE td."ID" = p.param_tipodocumento) AS TipoDocumento,
              p.numerodocumento,
              (SELECT rel.nombre FROM tbparametros rel WHERE rel."ID" = rp.param_relacion) AS Relacion,
              p.param_genero AS GeneroId,
              (SELECT gen.nombre FROM tbparametros gen WHERE gen."ID" = p.param_genero) AS Genero,
              trunc ( months_between( SYSDATE, p.fechanacimiento)/12 ) AS Edad,
              p.param_etniapertenece AS EtniaId,
              (SELECT et.nombre FROM tbparametros et WHERE et."ID" = p.param_etniapertenece) AS Etnia,
              (select CASE WHEN count(*) > 0 THEN  1 ELSE 0 END from TBDISCAPACIDAD_PERSONA DP where DP.ID_REGPERSONA = RP.ID) as Discapacitado,
              (SELECT  t.fallecida
               FROM    tbanexo6 t
               WHERE   t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona and rownum = 1) AS Fallecida,
              (SELECT t.desaparecida
               FROM   tbanexo4 t
               WHERE  t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona and rownum = 1) AS Desaparecida,
              (SELECT  t.secuestrado
               FROM    tbanexo8 t
               WHERE  t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona and rownum = 1) AS Secuestrado,
              (SELECT  t2.nombre
               FROM    tbanexo7 t
                       INNER JOIN tbparametros t2 ON t2."ID" = t.param_estadovictima
               WHERE   t.id_siniestro = va.id_siniestro AND t.id_regpersona = vap.id_regpersona and rownum = 1) AS EstadoPorMina,
              (SELECT td.se_desplazo
               FROM   tbanexo5 t
                      INNER JOIN tbanexo5_desplazados td ON td.id_anexo5 = t.ID
               WHERE  t.id_siniestro = va.id_siniestro AND td.id_regpersona = vap.id_regpersona and rownum = 1) AS SeDesplazo,
              (CASE WHEN vap.esafectado IS NULL THEN PKG_COMMON.f_personaafectada(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) ELSE vap.esafectado END) AS esafectado,
              (CASE WHEN vap.esvicitma IS NULL THEN PKG_COMMON.f_personavictma(va.tipo_anexo, vap.id_regpersona, va.id_siniestro) ELSE vap.esvicitma END) AS esvicitma,
              vap.id_val_anexo,
              vap.id_estado_val,
              ev.nombre AS estado_val,
              vap.id_observacion_val,
              vap.observacion
      FROM    tbval_anexo_persona vap
              INNER JOIN tbregistros_personas rp ON rp."ID" = vap.id_regpersona
              INNER JOIN tbpersonas p ON p."ID" = rp.id_persona
              INNER JOIN tbvaloracion_anexo va ON va."ID" = vap.id_val_anexo
              LEFT JOIN tbestado_val ev ON ev."ID" = vap.id_estado_val
      WHERE   va.id_valoracion = Pi_IdValoracion;

  END;

 PROCEDURE SP_CONSULTAVALORADORES(PI_PAGENUMBER IN NUMBER,
                                  PI_PAGESIZE IN NUMBER,
                                  PO_CURSOR OUT CURSOR_TYPE) IS

  LOWERBOUND NUMBER;
  UPPERBOUND NUMBER;

  BEGIN
  LOWERBOUND:=(PI_PAGENUMBER * PI_PAGESIZE) + 1;
  UPPERBOUND := ((PI_PAGENUMBER - 1) * PI_PAGESIZE) + 1;
  OPEN PO_CURSOR FOR
    SELECT *
    FROM(
    SELECT VALORADORES.*, ROWNUM AS R
    FROM(SELECT U.NOMBRE AS USUARIO, V.ID_VALORADOR,
          ROUND(AVG(V.FECHAVALORACIONREAL - V.FECHAASIGNACION),3) "PROMEDIO DE TIEMPO VALORACION",
          SUM(CASE WHEN V.ID_ESTADO_VAL = 4 THEN 1 ELSE 0 END) AS VALORACIONDEVUELTA,
          SUM(CASE WHEN V.ID_ESTADO_VAL = 3 THEN 1 ELSE 0 END) AS VALORACIONFINALIZADA,
          SUM(CASE WHEN V.ID_ESTADO_VAL = 2 THEN 1 ELSE 0 END) AS VALORACIONENPROCESO,
          SUM(CASE WHEN V.ID_ESTADO_VAL = 1 THEN 1 ELSE 0 END) AS VALORACIONASIGNADA,
          SUM(CASE WHEN V.ID_ESTADO_VAL = 5 THEN 1 ELSE 0 END) AS VALORACIONDEVUELTAASIGNACION
        FROM TBUSUARIOS U
        INNER JOIN TBVALORACION V ON V.ID_VALORADOR = U.ID
        INNER JOIN TBRADICACION R ON V.ID_DECLARACION = R.ID_DECLARACION
        GROUP BY U.NOMBRE, V.ID_VALORADOR
        ORDER BY V.ID_VALORADOR) VALORADORES
        WHERE ROWNUM <LOWERBOUND)
        WHERE R >= UPPERBOUND;
  END;

  PROCEDURE SP_CONSULTAVALORADORDETALLE(PI_VALORADORID IN NUMBER,
                                        PI_FECHASOLICITUD IN DATE,
                                        PI_PAGENUMBER IN NUMBER,
                                        PI_PAGESIZE IN NUMBER,
                                        PO_CURSOR OUT CURSOR_TYPE
                                       ) IS

  LOWERBOUND NUMBER;
  UPPERBOUND NUMBER;

  BEGIN

  LOWERBOUND:=(PI_PAGENUMBER * PI_PAGESIZE) + 1;
  UPPERBOUND := ((PI_PAGENUMBER - 1) * PI_PAGESIZE) + 1;

    OPEN po_cursor FOR
       SELECT *
       FROM(SELECT FECHAS.* , ROWNUM AS R
       FROM(SELECT V.FECHAVALORACION, COUNT(V.FECHAVALORACIONREAL) DECLARACIONESVALORADAS
        FROM TBVALORACION V
        INNER JOIN TBDECLARACIONES DE ON DE.ID = V.ID_DECLARACION
        WHERE TRUNC(V.FECHAVALORACION,'MM') = TRUNC(PI_FECHASOLICITUD,'MM')
        AND V.ID_VALORADOR = PI_VALORADORID
        AND DE.PARAM_ESTADO = 10002
        GROUP BY V.ID_VALORADOR,V.FECHAVALORACION
        ORDER BY V.FECHAVALORACION) FECHAS
        WHERE ROWNUM <LOWERBOUND)
        WHERE R >= UPPERBOUND;

  END;


  PROCEDURE SP_AUTOASIGNARVALORACION( PI_IDDECLARACION IN NUMBER) IS
    USUARIOASIGNADO NUMBER;
    V_ID NUMBER;
  BEGIN
    USUARIOASIGNADO := PKG_COMMON.F_USUARIOMENOSCARGA(VALORADOR_ROL);
    dbms_output.put_line(USUARIOASIGNADO);
    UPDATE TBDECLARACIONES SET PARAM_ESTADO = VALORACION_PEND_PORVALORAR WHERE ID = PI_IDDECLARACION;
    PKG_VALORACION.SP_ASIGNARVALORACION(V_ID,PI_IDDECLARACION,1,USUARIOASIGNADO,NULL);
  END;

  PROCEDURE SP_CONSULTAVALORADORESCOUNT(PO_RECORDCOUNT OUT NUMBER) IS

  BEGIN
     SELECT COUNT(1) INTO PO_RECORDCOUNT FROM (
      SELECT V.ID_VALORADOR
      FROM TBUSUARIOS U
      INNER JOIN TBVALORACION V ON V.ID_VALORADOR = U.ID
      INNER JOIN TBRADICACION R ON V.ID_DECLARACION = R.ID_DECLARACION
      GROUP BY V.ID_VALORADOR);
  END;

  PROCEDURE SP_DETALLEVALORADORCOOUNT(PI_VALORADORID    IN NUMBER,
                                      PI_FECHASOLICITUD IN DATE,
                                      PO_RECORDCOUNT    OUT NUMBER) IS

  BEGIN
       SELECT COUNT(1) INTO PO_RECORDCOUNT
       FROM(SELECT V.FECHAVALORACION, COUNT(V.FECHAVALORACIONREAL) DECLARACIONESVALORADAS
        FROM TBVALORACION V
        INNER JOIN TBDECLARACIONES DE ON DE.ID = V.ID_DECLARACION
        WHERE TRUNC(V.FECHAVALORACION,'MM') = TRUNC(PI_FECHASOLICITUD,'MM')
        AND V.ID_VALORADOR = PI_VALORADORID
        AND DE.PARAM_ESTADO = 10002
        GROUP BY V.ID_VALORADOR,V.FECHAVALORACION
        ORDER BY V.FECHAVALORACION);
  END;

  PROCEDURE SP_IDVALDESDEIDDECLA(PI_IDDECLARACION IN NUMBER,
                                 PO_IDVALORACION OUT NUMBER
                                ) IS
  BEGIN
  SELECT V.ID INTO PO_IDVALORACION FROM TBVALORACION V
  WHERE V.ID_DECLARACION = PI_IDDECLARACION AND V.ID_ESTADO_VAL = 2;

  END;

  PROCEDURE SP_RESUMENVALORACION(pi_IdDeclaracion IN NUMBER,
                                 po_Cursor        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN PO_CURSOR for
      SELECT DCL.ID               AS ID,
             DCL.NUMEROFORMULARIO AS NUMEROFORMULARIO,
             DCL.FECHADECLARACION AS FECHADECLARACION,
             USU.NOMBRE           AS NOMBREVALORADOR,
             PAA.PRIMERNOMBRE || ' ' || PAA.SEGUNDONOMBRE || ' ' || PAA.PRIMERAPELLIDO || ' ' || PAA.SEGUNDOAPELLIDO AS NOMBREDECLARANTE,
             P01.NOMBRE           AS TIPODOCUMENTO,
             PAA.NUMERODOCUMENTO  AS DOCUMENTOIDENTIDAD,
             P02.NOMBRE           AS ESTADOACTUALPROCESO,
             VEV.NOMBRE           AS ESTADOVALORACION,
             VAL.FECHAVALORACION  AS FECHAVALORACION,
             OBV.NOMBRE           AS ESTADO,
             HVC.NOMBRE_HECHO_VICTIMIZANTE AS NOMBRE_HECHO_VICTIMIZANTE,
             INF.INFRACCIONES     AS INFRACCIONDERECHOHUMAN,
             PRC.PRINCIPIOS       AS PRINCIPIO,
             PVS.PRIMERNOMBRE || ' ' || PVS.SEGUNDONOMBRE || ' ' || PVS.PRIMERAPELLIDO || ' ' || PVS.SEGUNDOAPELLIDO AS NOMBREVICTIMA,
             P03.NOMBRE           AS TIPODOCUMENTO_VICTIMA,
             PVS.NUMERODOCUMENTO  AS DOCUMENTOVICTIMA
      FROM TBDECLARACIONES DCL
      /* Informacion Declarante */
      INNER JOIN TBREGISTROS_PERSONAS   RGP ON RGP.ID_DECLARACION = DCL.ID
      INNER JOIN TBPERSONAS             PVS ON RGP.ID_PERSONA     = PVS.ID
      /* Informaci贸n Valoracion (脷ltima) */
      INNER JOIN (SELECT ID, ROWNUM AS R FROM (SELECT ID FROM TBVALORACION WHERE ID_DECLARACION = pi_IdDeclaracion ORDER BY FECHAVALORACIONREAL DESC)) VID ON VID.R = 1
      INNER JOIN TBVALORACION           VAL ON VAL.ID_DECLARACION = DCL.ID AND VAL.ID = VID.ID
      INNER JOIN TBVALORACION_ANEXO     VAX ON VAX.ID_VALORACION  = VAL.ID
      INNER JOIN TBVAL_ANEXO_PERSONA    VAP ON VAP.ID_VAL_ANEXO   = VAX.ID AND VAP.ID_REGPERSONA = RGP.ID
      INNER JOIN TBESTADO_VAL           VEV ON VAP.ID_ESTADO_VAL  = VEV.ID
      INNER JOIN TBSINIESTROS_PERSONA   SPP ON VAX.ID_SINIESTRO   = SPP.ID
      INNER JOIN TBHECHOS_VICTIMIZANTES HVC ON HVC.ID_HECHO_VICTIMIZANTE = SPP.PARAM_TIPOHECHO
      INNER JOIN TBREGISTROS_PERSONAS   RPA ON SPP.ID_REGPERSONA      = RPA.ID
      INNER JOIN TBPERSONAS             PAA ON RPA.ID_PERSONA         = PAA.ID
      INNER JOIN TBOBSERVACION_VAL      OBV ON VAP.ID_OBSERVACION_VAL = OBV.ID
      LEFT JOIN (SELECT IDV.ID_VAL_ANEXO_PER, LISTAGG(CAST(IDI.NOMBRE AS VARCHAR2(500)), ', ') WITHIN GROUP (ORDER BY IDI.NOMBRE) AS INFRACCIONES
                 FROM TBINFRACCION_DIH_VALANEXOPER IDV
                 INNER JOIN TBINFRACCION_DIH IDI ON IDI.ID = IDV.ID_INFRACCIONDIH
                 GROUP BY IDV.ID_VAL_ANEXO_PER) INF ON INF.ID_VAL_ANEXO_PER = VAP.ID
      LEFT JOIN (SELECT PRV.ID_VAL_ANEXO_PER, LISTAGG(CAST(PRI.NOMBRE AS VARCHAR2(500)), ', ') WITHIN GROUP (ORDER BY PRI.NOMBRE) AS PRINCIPIOS
                 FROM TBPRINCIPIO_VAL PRV
                 INNER JOIN TBPRINCIPIO      PRI ON PRI.ID = PRV.ID_PRINCIPIO
                 GROUP BY PRV.ID_VAL_ANEXO_PER) PRC ON PRC.ID_VAL_ANEXO_PER = VAP.ID
      LEFT JOIN TBUSUARIOS USU ON USU.ID = VAL.ID_VALORADOR
      LEFT JOIN TBPARAMETROS P01 ON P01.ID = PAA.PARAM_TIPODOCUMENTO
      LEFT JOIN TBPARAMETROS P02 ON P02.ID = DCL.PARAM_ESTADO
      LEFT JOIN TBPARAMETROS P03 ON P03.ID = PVS.PARAM_TIPODOCUMENTO
      WHERE DCL.ID = pi_IdDeclaracion
      ORDER BY PVS.ID, HVC.ID_HECHO_VICTIMIZANTE;
  END;

  PROCEDURE sp_AprobarValoracion(pi_IdUsuario     IN NUMBER,
                                 pi_IdDeclaracion IN NUMBER,
                                 pi_Observacion   IN VARCHAR2) IS
    TienePermiso       NUMBER;
    v_IdValoracion     NUMBER;
    v_IdActoAdmin      NUMBER;
    ID_USUARIOASIGNADO NUMBER;
  BEGIN
    /* Obtener Valores de Valoracion */
    SELECT MAX(ID), IDACTOADMINISTRATIVO INTO v_IdValoracion, v_IdActoAdmin FROM TBVALORACION WHERE ID_DECLARACION = pi_IdDeclaracion GROUP BY IDACTOADMINISTRATIVO;

    /* Determinar si el usuario actual es jefe */
    SELECT COUNT(1) INTO TienePermiso FROM TBROLES_USUARIO WHERE ID_ROL = 1016 AND ID_USUARIO = pi_IdUsuario;

    IF TienePermiso > 0 THEN
      -- Actualizar Acto Administrativo
      PKG_ACTOSADMIN.SP_ACTESTADOACTOADMIN(V_IDACTOADMIN, FIRMADO, PI_IDUSUARIO);
      UPDATE TBACTO_ADMINISTRATIVO SET ID_USUARIOFIRMA = PI_IDUSUARIO WHERE ID = V_IDACTOADMIN;
      -- Cambiar estado y actualizar hist贸rico. Se asigna a null
      ID_USUARIOASIGNADO := PKG_COMMON.F_USUARIOMENOSCARGA(PREPARADOR_NOTIFICACION);
      PKG_COMMON.SP_UPDESTADO_DECLARACION(PI_IDDECLARACION, ID_USUARIOASIGNADO, APROPENDNOTI);
      PKG_VALORACION.SP_INSERTAHISTORICOVAL(pi_IdUsuario, v_IdValoracion, pi_Observacion);
      UPDATE TBNOTIFICACION SET ID_USUARIO = ID_USUARIOASIGNADO WHERE ID_DECLARACION = pi_IdDeclaracion;
    ELSE
      -- Actualizar Acto Administrativo
      PKG_ACTOSADMIN.SP_ACTESTADOACTOADMIN(V_IDACTOADMIN, APROBADO, PI_IDUSUARIO);
      UPDATE TBACTO_ADMINISTRATIVO SET ID_USUARIOAPRUEBA = PI_IDUSUARIO WHERE ID = V_IDACTOADMIN;
      -- Asignar a JEFE
      ID_USUARIOASIGNADO := PKG_Common.F_USUARIOMENOSCARGA(1016);
      -- Cambiar estado y actualizar hist贸rico
      PKG_COMMON.SP_UPDESTADO_DECLARACION(PI_IDDECLARACION, ID_USUARIOASIGNADO, VALPENDFIR);
      PKG_VALORACION.SP_INSERTAHISTORICOVAL(pi_IdUsuario, v_IdValoracion, pi_Observacion);
    END IF;

  END;

  PROCEDURE SP_RECHAZARVALORACION(
                                 PI_IDUSUARIO IN NUMBER,
                                 PI_IDDECLARACION IN NUMBER,
                                 PI_OBSERVACION IN VARCHAR2
                                ) IS
  V_IDVALORACION NUMBER;
  V_IDACTOADMIN NUMBER;
  V_IDUSUARIOVAL NUMBER;
  BEGIN

      SELECT MAX(ID),IDACTOADMINISTRATIVO,ID_VALORADOR INTO V_IDVALORACION,V_IDACTOADMIN,V_IDUSUARIOVAL
      FROM TBVALORACION
      WHERE ID_DECLARACION = PI_IDDECLARACION
      GROUP BY IDACTOADMINISTRATIVO,ID_VALORADOR;

      PKG_ACTOSADMIN.SP_ACTESTADOACTOADMIN(V_IDACTOADMIN,RECHAZADO,PI_IDUSUARIO);

      PKG_COMMON.SP_UPDESTADO_DECLARACION(PI_IDDECLARACION,V_IDUSUARIOVAL,VALORACION_EN_VALORACION);

      PKG_VALORACION.SP_INSERTAHISTORICOVAL(PI_IDUSUARIO,V_IDVALORACION,PI_OBSERVACION);

      UPDATE TBVALORACION SET ID_ESTADO_VAL = VALORACION_ASIGNADA
      WHERE ID = V_IDVALORACION;

  END;

  PROCEDURE SP_INSERTAHISTORICOVAL(PI_IDUSUARIO IN NUMBER,
                                   PI_IDVALORACION IN NUMBER,
                                   PI_OBSERVACION IN VARCHAR2
                                  ) IS
  BEGIN
    INSERT INTO TBVALORACIONHISTORICO(ID,OBSERVACION,IDUSUARIO,IDVALORACION,FECHAACTUALIZACION)
    VALUES(SEQ_TBVALNHISTORICO.NEXTVAL,PI_OBSERVACION,PI_IDUSUARIO,PI_IDVALORACION,SYSDATE);
  END;


  PROCEDURE SP_INSERTATIPOMOTIVACION(PI_IDVALORACION IN NUMBER,
                                     PI_TIPOMOTIVACION IN VARCHAR2 DEFAULT NULL) IS

  BEGIN
    UPDATE TBVALORACION_MOTIVACION SET TIPOMOTIVACION = PI_TIPOMOTIVACION
    WHERE ID_VALORACION = PI_IDVALORACION;
  END;

  PROCEDURE sp_ObtieneTipoMotivacion(pi_IdValoracion   IN NUMBER
                                   , po_TipoMotivacion OUT VARCHAR2) IS
  BEGIN
   SELECT TIPOMOTIVACION INTO po_TipoMotivacion FROM TBVALORACION_MOTIVACION WHERE ID_VALORACION = PI_IDVALORACION;
  END;

  PROCEDURE sp_AgregaPersona(pi_PrimerNombre         IN VARCHAR2
                           , pi_SegundoNombre        IN VARCHAR2 DEFAULT NULL
                           , pi_PrimerApellido       IN VARCHAR2
                           , pi_SegundoApellido      IN VARCHAR2 DEFAULT NULL
                           , pi_TipoDocumento        IN NUMBER   DEFAULT NULL
                           , pi_NumeroDocumento      IN VARCHAR2 DEFAULT NULL
                           , pi_Param_EstadoCivil    IN NUMBER   DEFAULT NULL
                           , pi_Param_Genero         IN NUMBER   DEFAULT NULL
                           , pi_Param_MinoriaEtnica  IN NUMBER   DEFAULT NULL
                           , pi_Gestante             IN NUMBER   DEFAULT NULL
                           , pi_FechaNacimiento      IN DATE     DEFAULT NULL
                           , pi_EsMujerCabezaDeHogar IN NUMBER   DEFAULT NULL
                           , pi_Comunidad            IN VARCHAR2 DEFAULT NULL
                           , pi_IdCreado             OUT NUMBER) IS
  BEGIN
    INSERT INTO TBPERSONAS (ID
                          , PRIMERNOMBRE
                          , SEGUNDONOMBRE
                          , PRIMERAPELLIDO
                          , SEGUNDOAPELLIDO
                          , PARAM_TIPODOCUMENTO
                          , NUMERODOCUMENTO
                          , PARAM_ESTADOCIVIL
                          , PARAM_GENERO
                          , PARAM_MINORIAETNICA
                          , GESTANTE
                          , FECHANACIMIENTO
                          , ESMUJERCABEZADEHOGAR
                          , COMUNIDAD)
    VALUES (SEQ_PERSONAS.NEXTVAL
          , UPPER(TRIM(pi_PrimerNombre))
          , UPPER(TRIM(pi_SegundoNombre))
          , UPPER(TRIM(pi_PrimerApellido))
          , UPPER(TRIM(pi_SegundoApellido))
          , pi_TipoDocumento
          , pi_NumeroDocumento
          , pi_Param_EstadoCivil
          , pi_Param_Genero
          , pi_Param_MinoriaEtnica
          , pi_Gestante
          , pi_FechaNacimiento
          , pi_EsMujerCabezaDeHogar
          , pi_Comunidad)
    RETURNING ID INTO pi_IdCreado;
  END;

  PROCEDURE sp_AgregaRegPersona(pi_IdDeclaracion        IN NUMBER
                              , pi_IdPersona            IN NUMBER
                              , pi_CDireccion           IN VARCHAR2
                              , pi_NTelefono            IN VARCHAR2
                              , pi_Relacion             IN NUMBER
                              , pi_CorreoElectronico    IN VARCHAR2
                              , pi_EsMujerCabezaDeHogar IN NUMBER
                              , pi_RegimenEspecial      IN NUMBER
                              , pi_Gestante             IN NUMBER
                              , pi_Observacion          IN VARCHAR2
                              , pi_IdCreado             OUT NUMBER) IS
    vJefeFamiliar   NUMBER;
    vIdPais         NUMBER;
    vIdDepartamento NUMBER;
    vIdMunicipio    NUMBER;
    vConsecutivoPersona NUMBER;
    vIdValoracion       NUMBER;
  BEGIN
    SELECT MIN(ID) INTO vJefeFamiliar FROM TBREGISTROS_PERSONAS WHERE ID_DECLARACION = pi_IdDeclaracion AND ESDECLARANTE = 1;
    SELECT ID_PAIS, ID_DEPARTAMENTO, ID_MUNICIPIO INTO vIdPais, vIdDepartamento, vIdMunicipio FROM TBREGISTROS_PERSONAS WHERE ID = vJefeFamiliar;
    SELECT MAX(ID) INTO vIdValoracion FROM TBVALORACION WHERE ID_DECLARACION = pi_IdDeclaracion;
    SELECT (MAX(CONSECUTIVO_PERSONA) + 1) INTO vConsecutivoPersona FROM TBREGISTROS_PERSONAS WHERE ID_DECLARACION = pi_IdDeclaracion;
    
    INSERT INTO TBREGISTROS_PERSONAS (ID
                                    , ID_DECLARACION
                                    , ID_PERSONA
                                    , ACTIVO
                                    , ID_MIJEFEHOGAR
                                    , ID_USUARIO
                                    , ID_UTERRITORIAL
                                    , DIRECCION
                                    , TELEFONO
                                    , PARAM_RELACION
                                    , EMAIL
                                    , CONSECUTIVO_PERSONA
                                    , ESMUJERCABEZADEHOGAR
                                    , PARAM_REGIMENESPECIAL
                                    , GESTANTE_LACTANTE
                                    , ID_PAIS
                                    , ID_DEPARTAMENTO
                                    , ID_MUNICIPIO
                                    , CONSECUTIVO_FAMILIA)
    VALUES (SEQ_REGISTRO_PERSONAS.NextVal
          , pi_IdDeclaracion
          , pi_IdPersona
          , 1
          , vJefeFamiliar
          , 14878
          , 36
          , pi_CDireccion
          , PI_NTELEFONO
          , pi_Relacion
          , pi_CorreoElectronico
          , vConsecutivoPersona
          , pi_EsMujerCabezaDeHogar
          , pi_RegimenEspecial
          , pi_Gestante
          , vIdPais
          , vIdDepartamento
          , vIdMunicipio
          , 1)
    RETURNING ID INTO pi_IdCreado;

    INSERT INTO TBREGPERSONASCOMENTARIO (ID_REGPERSONA, COMENTARIO) VALUES (pi_IdCreado, pi_Observacion);
  END;

  PROCEDURE sp_AgregaDiscapacidadValora(pi_IdRegPersona IN NUMBER, pi_Discapacidad IN NUMBER) IS
  BEGIN
    INSERT INTO TBDISCAPACIDAD_PERSONA (ID_REGPERSONA, PARAM_DISCAPACIDAD, ACTIVO)
    VALUES (pi_IdRegPersona, pi_Discapacidad, 1);
  END;

PROCEDURE SP_CARGAPERSONASASOCIADAS(PI_IDDECLARACION IN NUMBER,
                                    PO_CURSOR OUT CURSOR_TYPE) IS

 V_JEFEHOGAR  NUMBER(10,0);
 BEGIN
  SELECT R.ID_MIJEFEHOGAR INTO V_JEFEHOGAR FROM TBREGISTROS_PERSONAS R
  WHERE R.ID_DECLARACION = PI_IDDECLARACION AND R.ESDECLARANTE = 1;

  OPEN PO_CURSOR FOR

    SELECT P.PRIMERNOMBRE ||
             CASE WHEN P.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || P.SEGUNDONOMBRE END ||
             CASE WHEN P.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || P.PRIMERAPELLIDO END ||
             CASE WHEN P.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || P.SEGUNDOAPELLIDO END AS NOMBREDECLARANTE,
             TDC.NOMBRE                 AS TIPODOCUMENTO,
             P.NUMERODOCUMENTO,
             TRP.NOMBRE AS RELACION
    FROM TBPERSONAS P
    LEFT JOIN TBREGISTROS_PERSONAS RP ON RP.ID_PERSONA = P.ID
    LEFT JOIN TBPARAMETROS TDC ON TDC.ID = P.PARAM_TIPODOCUMENTO
    LEFT JOIN TBPARAMETROS TRP ON TRP.ID = RP.PARAM_RELACION
    WHERE RP.ID_DECLARACION = PI_IDDECLARACION AND RP.ID_MIJEFEHOGAR = V_JEFEHOGAR;

 END;

PROCEDURE SP_CARGAPERSONASASOCIADASCOUNT(PI_IDDECLARACION IN NUMBER,
                                          PO_RECORDCOUNT  OUT NUMBER) IS

 V_JEFEHOGAR  NUMBER(10,0);
 BEGIN
  SELECT R.ID_MIJEFEHOGAR INTO V_JEFEHOGAR FROM TBREGISTROS_PERSONAS R
  WHERE R.ID_DECLARACION = PI_IDDECLARACION AND R.ESDECLARANTE = 1;


    SELECT COUNT(1) INTO PO_RECORDCOUNT
    FROM TBPERSONAS P
    LEFT JOIN TBREGISTROS_PERSONAS RP ON RP.ID_PERSONA = P.ID
    LEFT JOIN TBPARAMETROS TDC ON TDC.ID = P.PARAM_TIPODOCUMENTO
    LEFT JOIN TBPARAMETROS TRP ON TRP.ID = RP.PARAM_RELACION
    WHERE RP.ID_DECLARACION = PI_IDDECLARACION AND RP.ID_MIJEFEHOGAR = V_JEFEHOGAR;

 END;

PROCEDURE SP_AGREGADISCAPACIDADVALORA(PI_IDREGPERSONA IN NUMBER,
                                      PI_IDDISCAPACIDAD IN NUMBER) IS

BEGIN
INSERT
INTO TBDISCAPACIDAD_PERSONA
  (
    ID_REGPERSONA,
    PARAM_DISCAPACIDAD,
    ACTIVO
  )
  VALUES
  (
    PI_IDREGPERSONA,
    PI_IDDISCAPACIDAD,
    1
  );
end;

PROCEDURE SP_IDVALORACIONPORDECLARACION(PI_IDDECLARACION IN NUMBER,
                                        PO_IDVALORACION OUT NUMBER) IS

BEGIN
    SELECT  T."ID"  INTO PO_IDVALORACION
    FROM TBDECLARACIONES DE
    INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBVALORACION GROUP BY ID_DECLARACION) VAL ON (VAL.ID_DECLARACION = DE.ID)
    INNER JOIN TBVALORACION T ON T.ID = VAL.ID
    WHERE DE.ID = PI_IDDECLARACION;
END;

PROCEDURE SP_GETVALORACIONHISTORICO(PI_IDVALORACION IN NUMBER,
                                    PO_CURSOR OUT CURSOR_TYPE) IS
BEGIN
  OPEN PO_CURSOR FOR
    SELECT  V.ID,
            V.OBSERVACION,
            U.USUARIO,
            V.IDVALORACION,
            V.FECHAACTUALIZACION,
            SUBSTR(MOTIVACION, 0, 100)||'...' AS MOTIVACION
    FROM TBVALORACIONHISTORICO V
    INNER JOIN TBUSUARIOS U ON V.IDUSUARIO = U.ID
    INNER JOIN TBVALORACION VAL ON VAL.ID = V.IDVALORACION
    WHERE IDVALORACION = PI_IDVALORACION;

END;

PROCEDURE SP_GETMOTIVACIONVALORACION(pi_IdValoracion IN NUMBER,
                                     PO_CURSOR OUT CURSOR_TYPE) IS
BEGIN
    OPEN PO_CURSOR FOR
      --SELECT DBMS_LOB.substr(motivacion,DBMS_LOB.getlength(motivacion),1)
      SELECT motivacion
      FROM TBVALORACION
      WHERE ID = pi_IdValoracion; 
END;

END PKG_VALORACION;
/