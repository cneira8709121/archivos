-- Create new package
create or replace PACKAGE PKG_NOTIFICACION AS

  TYPE CURSOR_TYPE IS REF CURSOR;
  
  /* Estados de Notificaciones - Preparación y Envío */
  PREP_EnFirmaAAdministrativo  NUMBER := 0;
  PREP_CorreccionInformacion   NUMBER := 1;
  PREP_PendienteEnvio          NUMBER := 2;
  PREP_EnvioEnProceso          NUMBER := 3;
  PREP_Enviado                 NUMBER := 4;
  
  /* Estados de Notificaciones - Respuestas Courier */
  COUR_NotificacionEntregada   NUMBER := 5;
  COUR_NotificacionRechazada   NUMBER := 6;
  
  /* Estados de Notificaciones - Entrega y Términos */
  ENTR_NotificadoPersonal      NUMBER := 10;
  ENTR_PendientePublicacion    NUMBER := 11;
  ENTR_EdictoPublicado         NUMBER := 12;
  ENTR_PendienteDespublicacion NUMBER := 13;
  ENTR_NotificadoEdicto        NUMBER := 14;
  ENTR_PendientEnvioResolucion NUMBER := 15;
  ENTR_NotificadoResolucion    NUMBER := 16;

 /*ROLES NOTIFICACIONES*/
  LIDERNOTIFICACIONES NUMBER := 1019;

  /* Estados del Paquete de Notificaciones */
  GENERADO NUMBER := 1;
  
  PROCEDURE sp_InsertaNotificacion(pi_IdDeclaracion IN NUMBER);

  /*	DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
  **	(NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
  **	AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
  **	FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
  **	CAMBIOS:
  **		20130614 - JAIRO VALDERRAMA
  **		1. SE ADICIONA LA FECHA DE LA FIRMA A LA CONSULTA
  */
  PROCEDURE sp_ConsultaNotificaciones(pi_IdUsuario                IN NUMBER   DEFAULT NULL
                                    , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                    , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                    , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                    , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                    , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                    , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                    , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                    , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                    , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                    , pi_SoloAsignaciones         IN NUMBER
                                    , pi_Orden                    IN VARCHAR2
                                    , pi_PageNumber               IN NUMBER
                                    , pi_PageSize                 IN NUMBER
                                    , po_Resultado                OUT CURSOR_TYPE);

  PROCEDURE sp_ConsultaNotificacionesCount(pi_IdUsuario                IN NUMBER   DEFAULT NULL
                                         , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                         , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                         , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                         , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                         , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                         , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                         , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                         , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                         , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                         , pi_SoloAsignaciones         IN NUMBER
                                         , po_RecordCount              OUT NUMBER);
  
  PROCEDURE sp_ConsultaNotificacionPorId(pi_IdNotificacion IN NUMBER, po_Resultado OUT CURSOR_TYPE);
                                         
  PROCEDURE sp_CrearPaqueteDesdeFiltro(pi_IdUsuario                IN NUMBER
                                     , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                     , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                     , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                     , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                     , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                     , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                     , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                     , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                     , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                     , pi_SoloAsignaciones         IN NUMBER
                                     , po_IdPaqueteNotifica        OUT NUMBER
                                     , po_RecordCount              OUT NUMBER);

  PROCEDURE sp_DetalleNotificacion(pi_IdNotificacion IN NUMBER
                                 , po_Cursor         OUT CURSOR_TYPE);

PROCEDURE SP_ACTUALIZARNOTIFICACION(
                                    PI_IDNOTIFICACION IN NUMBER, 
                                    PI_DIRECCIONENVIO IN VARCHAR2
                                    );
                            
  PROCEDURE sp_ActualizarPuntoNotificacion(pi_IdNotificacion IN NUMBER, pi_IdPais IN NUMBER, pi_IdDepartamento IN NUMBER, pi_IdMunicipio IN NUMBER, pi_DireccionEnvio IN VARCHAR2, pi_IdPuntoAtencion IN NUMBER DEFAULT NULL, pi_IdDireccionTerritorial IN NUMBER DEFAULT NULL);

  PROCEDURE sp_CrearPaqueteNotificacion(pi_IdUsuario         IN NUMBER
                                      , po_IdPaqueteNotifica OUT NUMBER);
                                      
  PROCEDURE sp_AsociarNotificacionAPaquete(pi_IdNotificacion    IN NUMBER
                                         , pi_IdPaqueteNotifica IN NUMBER);

  PROCEDURE sp_ActualizarEstadoCourier(pi_IdNotificacion      IN NUMBER
                                     , pi_EstadoNotificacion IN NUMBER
                                     , pi_EstadoCourier      IN VARCHAR2
                                     , pi_Fecha              IN DATE DEFAULT NULL
                                     , pi_FechaFinal         IN DATE DEFAULT NULL);
                                  
  PROCEDURE sp_ConsultaNtfEntregadas(pi_IdUsuario           IN NUMBER
                                   , pi_BusquedaGlobal      IN NUMBER
                                   , pi_Declaracion         IN VARCHAR2 DEFAULT NULL
                                   , pi_TipoDocumento       IN NUMBER   DEFAULT NULL
                                   , pi_Documento           IN VARCHAR2 DEFAULT NULL
                                   , pi_NombreDeclarante    IN VARCHAR2 DEFAULT NULL
                                   , pi_EstadoNotificacion  IN NUMBER   DEFAULT NULL
                                   , pi_Orden               IN VARCHAR2
                                   , pi_PageNumber          IN NUMBER
                                   , pi_PageSize            IN NUMBER
                                   , po_Resultado           OUT CURSOR_TYPE);
  
  PROCEDURE sp_ConsultaNtfEntregadasCount(pi_IdUsuario            IN NUMBER
                                        , pi_BusquedaGlobal       IN NUMBER
                                        , pi_Declaracion          IN VARCHAR2 DEFAULT NULL
                                        , pi_TipoDocumento        IN NUMBER   DEFAULT NULL
                                        , pi_Documento            IN VARCHAR2 DEFAULT NULL
                                        , pi_NombreDeclarante     IN VARCHAR2 DEFAULT NULL
                                        , pi_EstadoNotificacion   IN NUMBER   DEFAULT NULL
                                        , po_RecordCount          OUT NUMBER);

  PROCEDURE sp_GetPuntosNotificacion(po_Cursor OUT CURSOR_TYPE);

PROCEDURE SP_GETPUNTOSNOTBYDIRTER(
                                  PI_IDDIRECCIONTERRITORIAL NUMBER,
                                  PO_CURSOR OUT CURSOR_TYPE
                                  );

PROCEDURE SP_FINALIZANOTIFICACION(PI_IDNOTIFICACION IN NUMBER);

  /* Cambia el estado de una notificacion */
  PROCEDURE SP_CAMBIAESTADONOTIFICACION(PI_IDNOTIFICACION IN NUMBER, PI_IDESTADO IN NUMBER, PI_FECHAFIN IN DATE DEFAULT NULL, PI_OBSERVACION IN VARCHAR2);

  /* Conteo de registros de paquetes de notificaciones */
  PROCEDURE sp_ConsultaPaquetesCount(pi_IdUsuario IN NUMBER, pi_OrdenServicio IN VARCHAR2 DEFAULT NULL, pi_FechaInicio IN DATE DEFAULT NULL, pi_FechaFin IN DATE DEFAULT NULL, po_RecordCount OUT NUMBER);
  
  /* Registros de paquetes de notificaciones */
  PROCEDURE sp_ConsultaPaquetes(pi_IdUsuario IN NUMBER, pi_OrdenServicio IN VARCHAR2 DEFAULT NULL, pi_FechaInicio IN DATE DEFAULT NULL, pi_FechaFin IN DATE DEFAULT NULL, pi_PageNumber IN NUMBER, pi_PageSize IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  /* Registro de paquete de notificaciones por id */
  PROCEDURE sp_ConsultaPaquetePorId(p_Id IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  /* Conteo detalle (notificaciones) de paquete */  
  PROCEDURE sp_ConsultaDetallePaqueteCount(pi_IdPaqueteNotifica IN NUMBER, po_RecordCount OUT NUMBER);

  /* Detalle (notificaciones) de paquete */  
  PROCEDURE sp_ConsultaDetallePaquete(pi_IdPaqueteNotifica IN NUMBER, pi_PageNumber IN NUMBER, pi_PageSize IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE sp_AgregarOrdenServicio(pi_IdPaqueteNotifica IN NUMBER
                                  , pi_OrdenServicio     IN VARCHAR2);

  PROCEDURE sp_AprobarNotificacion(pi_IdNotificacion IN NUMBER);
                                  
 PROCEDURE SP_GUARDAOBSERVACION(PI_IDNOTIFICACION IN NUMBER,PI_OBSERVACION IN VARCHAR2);
 
 /*  DESCRIPCION: TRAE TODA LAS NOTIFICACIONES QUE ESTEN EN ENVIADO O ENVIO
  **  RECHAZADO PARA COMPARARLOS CON LOS ESTADOS DEL REPORTE COURIER
  **  FECHA: 20130411
  **  CAMBIOS:
  */
 PROCEDURE SP_GETALLNOTIFICACIONES(PO_CURSOR OUT CURSOR_TYPE);
 
  /*  DESCRIPCION: ACTUALIZA EL ESTADO DE UNA NOTIFICACION, CUANDO OCURRE UN VENCIMIENTO
  **  DE TERMINOS. ESTE PROCEDIMIENTO ES INVOCADO POR EL JOB "Job_CambiaEstadoNotificacion"
  **  FECHA: 20130411
  **  CAMBIOS:
  */
 PROCEDURE SP_PROCESARNOTIFICACIONES;
 
 PROCEDURE sp_confirmaEnvionNotificacion(PO_IDPAQUETENOTIFICA IN TBNOTIFICACION.ID_PAQUETENOTIFICACION%TYPE);

 /*------------------------------------------------------------------
  Purpose : Asocia codigos de guia a las notificaciones de un paquete
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_ASOCIARCODIGUIANOTIFICACION (PI_NID IN NUMBER,
                                           PI_IDCODIGOGUIA IN VARCHAR2);

 /*------------------------------------------------------------------
  Purpose : Consulta las notificaciones que tienen asociadas los puntos de atencion(DireccionTerritorial y PuntoNotifica)
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_CONSULTACENTROATENCIONOTIF (  pi_IdPais         IN NUMBER DEFAULT NULL
                                          , pi_IdDepartamento IN NUMBER DEFAULT NULL
                                          , pi_IdMunicipio    IN NUMBER DEFAULT NULL
                                          , pi_PageNumber     IN NUMBER
                                          , pi_PageSize       IN NUMBER
                                          , PO_CURSOR         OUT CURSOR_TYPE);                                      
                                          

  /*------------------------------------------------------------------
  Purpose : Consulta la cantidad de registros que trae el procedimiento SP_CONSULTACENTROATENCIONOTIF
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_CONSULTACENTROATENCIONCOUNT(  pi_IdPais         IN NUMBER DEFAULT NULL
                                          , pi_IdDepartamento IN NUMBER DEFAULT NULL
                                          , pi_IdMunicipio    IN NUMBER DEFAULT NULL
                                          , PO_RECORDCOUNT OUT NUMBER);

 /*------------------------------------------------------------------
  Purpose : Consulta en detalle las notificaciones que tienen asociadas los puntos de atencion(DireccionTerritorial y PuntoNotifica)
  Author  : John Henao
  Date    : 24/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE sp_DetalleCentroAtencion(  PI_IDCENTROATENCION     IN NUMBER
                                  , PI_TipoCentro            IN NUMBER
                                  , pi_PageNumber           IN NUMBER
                                  , pi_PageSize             IN NUMBER
                                  , PO_CURSOR OUT CURSOR_TYPE);

  /*------------------------------------------------------------------
  Purpose : Consulta la cantidad de registros que trae el procedimiento SP_CONSULTACENTROATENCIONOTIF
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_DetalleCentroAtencionCOUNT(PI_IDCENTROATENCION IN NUMBER
                                         ,PI_TipoCentro      IN NUMBER
                                         ,PO_RECORDCOUNT OUT NUMBER);
                                  
  /*------------------------------------------------------------------
   Purpose : Guarda el registro de trazabilidad para TBNOTIFICACION
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_AgregarHistorico(pi_IdNotificacion IN TBNOTIFICACION.ID%TYPE);
  
  /*------------------------------------------------------------------
   Purpose : Obtiene los registros historicos para una notificacion
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_ObtenerHistorico(pi_IdNotificacion IN TBNOTIFICACION.ID%TYPE, po_Resultado OUT CURSOR_TYPE);
  
  /*------------------------------------------------------------------
   Purpose : Obtiene los registros historicos para un paquete de notificaciones
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_ObtenerHistoricoPaquete(pi_IdPaqueteNotificacion IN TBPAQUETENOTIFICACION.ID%TYPE, po_Resultado OUT CURSOR_TYPE);

  /*------------------------------------------------------------------
   Purpose : verifica y obtiene si una notificacion es ley nueva o ley vieja
   Author  : John Henao
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_NOTIFICACIONLEYNUEVAOVIEJA(PI_IDNOTIFICACION IN NUMBER
                                          ,PO_RECORDCOUNT OUT NUMBER);
                                          
  /*------------------------------------------------------------------
   Purpose : Retorna los encargados que tiene una entidad
   Author  : Ivan Camilo Suarez
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_GETENCARGADOSPORENTIDAD(  PI_IDCENTROATENCION IN NUMBER
                                       , PI_TipoCentro       IN NUMBER
                                       , pi_PageNumber       IN NUMBER
                                       , pi_PageSize         IN NUMBER
                                       , po_Cursor           OUT CURSOR_TYPE);

  /*------------------------------------------------------------------
   Purpose : Consulta la cantidad de registros que trae el procedimiento SP_GETENCARGADOSPORENTIDAD
   Author  : Ivan Camilo Suarez
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_ENCARGADOSPORENTIDADCOUNT(  PI_IDCENTROATENCION IN NUMBER
                                         , PI_TipoCentro       IN NUMBER
                                         , PO_RECORDCOUNT      OUT NUMBER);
                                         
                                         
  /*------------------------------------------------------------------
   Purpose : Retorna los estados de notificacion
   Author  : Ivan Camilo Suarez
   Date    : 06/07/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_GETESTADOSDENOTIFICACION(po_Cursor OUT CURSOR_TYPE);
 
END PKG_NOTIFICACION;
/

-- Create package body
create or replace PACKAGE BODY PKG_NOTIFICACION AS

  PROCEDURE sp_InsertaNotificacion(pi_IdDeclaracion IN NUMBER) IS
    -- Valores referencia
    vIdValoracion            NUMBER;
    vPuntoAtencionNtf        NUMBER;
    vDireccionTerritorialNtf NUMBER;
    vDireccionTerritorialCit NUMBER;
    xCitadoAEntidad          NUMBER := 0;
    vEstadoActoAdmin         NUMBER;
    vTipoCodigoActo          NUMBER;
    vEstadoNotificacion      NUMBER;
    rIdNotificacion          NUMBER;
    -- Informacion de Envio
    vDireccion              VARCHAR2(100) := NULL;
    vTelefono               VARCHAR2(20)  := NULL;
    vPais                   NUMBER := NULL;
    vDepartamento           NUMBER := NULL;
    vMunicipio              NUMBER := NULL;
  BEGIN
    /* Valores de referencia */
    BEGIN
      SELECT VAL.ID, VAL.IDPUNTOATENCION, VAL.IDDIRECCIONTERRITORIAL, VAL.IDDIRECCIONTERRITORIALCITACION, ACT.PARAM_ESTADO, ACT.TIPOCODIGOACTO INTO vIdValoracion, vPuntoAtencionNtf, vDireccionTerritorialNtf, vDireccionTerritorialCit, vEstadoActoAdmin, vTipoCodigoActo
      FROM (SELECT ID_DECLARACION, MAX(ID) AS ID FROM TBVALORACION GROUP BY ID_DECLARACION) VRF
      INNER JOIN TBVALORACION          VAL ON VAL.ID = VRF.ID
      LEFT  JOIN TBACTO_ADMINISTRATIVO ACT ON ACT.ID = VAL.IDACTOADMINISTRATIVO
      WHERE VRF.ID_DECLARACION = pi_IdDeclaracion;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      RAISE_APPLICATION_ERROR(-20054, 'No es posible encontrar la valoracion correspondiente a la declaracion');
    END;
    
    /* Obtener Informacion de Envio utilizando las mismas reglas de la generacion de Actos Administrativos */
    BEGIN
      SELECT DIRECCION, NVL(TELEFONO, MOVIL), ID_PAIS, ID_DEPARTAMENTO, ID_MUNICIPIO INTO vDireccion, vTelefono, vPais, vDepartamento, vMunicipio
      FROM TBREGISTROS_PERSONAS
      WHERE ID_DECLARACION = pi_IdDeclaracion AND ESDECLARANTE = 1;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      NULL;
    END;
    
    IF vPais IS NULL OR vDepartamento IS NULL OR vMunicipio IS NULL OR PKG_ACTOSADMIN.f_EvaluarDireccion(vDireccion) = 0 THEN
      -- Pendiente. Deber?amos guardar el numero de telefono de la direccion territorial
      SELECT DTR.DIRECCION, NULL, PAI.ID, DPT.ID, MCP.ID INTO vDireccion, vTelefono, vPais, vDepartamento, vMunicipio FROM TBDIRECCIONTERRITORIAL DTR
      INNER JOIN TBGEOGRAFIA MCP ON MCP.ID = DTR.IDMUNICIPIO
      INNER JOIN TBGEOGRAFIA DPT ON DPT.ID = MCP.PADREID
      INNER JOIN TBGEOGRAFIA PAI ON PAI.ID = DPT.PADREID
      WHERE DTR.ID = vDireccionTerritorialCit;
      xCitadoAEntidad := 1;
    END IF;
    
    /* Si ya esta firmado, el estado es pendiente correccion de datos envio */
    vEstadoNotificacion := CASE WHEN vEstadoActoAdmin IS NOT NULL AND vEstadoActoAdmin = 4
                                THEN PREP_CorreccionInformacion ELSE PREP_EnFirmaAAdministrativo END;
                                
    IF COALESCE(vPuntoAtencionNtf, vDireccionTerritorialNtf) IS NULL THEN
      RAISE_APPLICATION_ERROR(-20055, 'Imposible crear registro de notificacion. No existen reglas de ubicaci?n de notificaci?n configuradas para el municipio.');
    END IF;
    
    BEGIN 
      SELECT ID INTO rIdNotificacion
      FROM (SELECT ID FROM TBNOTIFICACION WHERE ID_DECLARACION = pi_IdDeclaracion ORDER BY ID DESC) WHERE ROWNUM = 1;
      PKG_NOTIFICACION.sp_AgregarHistorico(rIdNotificacion);
      UPDATE TBNOTIFICACION SET ESTADO                  = vEstadoNotificacion
                              , DIRECCIONNOTIFICACION   = vDireccion
                              , TELEFONONOTIFICACION    = vTelefono
                              , ID_PAIS                 = vPais
                              , ID_DEPARTAMENTO         = vDepartamento
                              , ID_MUNICIPIO            = vMunicipio
                              , ID_PUNTOATENCION        = vPuntoAtencionNtf
                              , ID_DIRECCIONTERRITORIAL = vDireccionTerritorialNtf
                              , CITACIONAENTIDAD        = xCitadoAEntidad
                              , TIPOCODIGOACTO          = vTipoCodigoActo
      WHERE ID = rIdNotificacion;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      INSERT INTO TBNOTIFICACION (ID, ID_DECLARACION, ESTADO, DIRECCIONNOTIFICACION, TELEFONONOTIFICACION, ID_PAIS, ID_DEPARTAMENTO, ID_MUNICIPIO, ID_PUNTOATENCION, ID_DIRECCIONTERRITORIAL, CITACIONAENTIDAD, TIPOCODIGOACTO)
      VALUES (SEQ_TBNOTIFICACION.NextVal, pi_IdDeclaracion, vEstadoNotificacion, vDireccion, vTelefono, vPais, vDepartamento, vMunicipio, vPuntoAtencionNtf, vDireccionTerritorialNtf, xCitadoAEntidad, vTipoCodigoActo)
      RETURNING ID INTO rIdNotificacion;
    END;
  END;

  PROCEDURE sp_ConsultaNotificaciones(pi_IdUsuario                IN NUMBER   DEFAULT NULL
                                    , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                    , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                    , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                    , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                    , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                    , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                    , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                    , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                    , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                    , pi_SoloAsignaciones         IN NUMBER
                                    , pi_Orden                    IN VARCHAR2
                                    , pi_PageNumber               IN NUMBER
                                    , pi_PageSize                 IN NUMBER
                                    , po_Resultado                OUT CURSOR_TYPE) IS
    startRow NUMBER;
    endRow   NUMBER;
    xfTipoPuntoAtencion VARCHAR2(2) := NULL;
    xfIdPuntoAtencion   NUMBER      := NULL;
  BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow   := (pi_PageNumber * pi_PageSize) + 1;
    
    IF pi_EntidadNotificacion IS NOT NULL THEN
      DECLARE
        xfIndex NUMBER;
      BEGIN
        xfIndex := INSTR(pi_EntidadNotificacion, '-');
        xfTipoPuntoAtencion := SUBSTR(pi_EntidadNotificacion, 0, xfIndex - 1);
        xfIdPuntoAtencion   := CAST(SUBSTR(pi_EntidadNotificacion, xfIndex + 1) AS NUMBER);
      EXCEPTION WHEN OTHERS THEN
        NULL;
      END;
    END IF;
    
    OPEN po_Resultado FOR
      SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (SELECT NTF.ID                     AS ID
                       , NTF.ID_DECLARACION         AS ID_DECLARACION
                       , NTF.ESTADO                 AS ID_ESTADONOTIFICACION
                       , ESN.NOMBRE                 AS ESTADONOTIFICACION
                       , AAD.DFIRMA
                       , NTF.ESTADOCOURIER          AS ESTADOCOURIER
                       , NTF.FECHAESTADOCOURIER     AS FECHAESTADOCOURIER
                       , NTF.DIRECCIONNOTIFICACION  AS DIRECCIONNOTIFICACION
                       , NTF.ID_DEPARTAMENTO        AS ID_DEPARTAMENTO
                       , DPT.NOMBRE                 AS NOMBREDEPARTAMENTO
                       , NTF.ID_MUNICIPIO           AS ID_MUNICIPIO
                       , MCP.NOMBRE                 AS NOMBREMUNICIPIO
                       , NTF.ID_PAIS                AS ID_PAIS
                       , PAI.NOMBRE                 AS NOMBREPAIS
                       , NTF.TELEFONONOTIFICACION   AS TELEFONONOTIFICACION
                       , NTF.ID_USUARIO             AS ID_USUARIO
                       , NTF.ID_PAQUETENOTIFICACION AS ID_PAQUETENOTIFICACION
                       , PTD.NOMBRE                 AS TIPODOCUMENTO
                       , DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
                       , PRS.PRIMERNOMBRE ||
                         CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE   END ||
                         CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO  END ||
                         CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRECOMPLETO
                       , PRS.NUMERODOCUMENTO        AS NUMERODOCUMENTO
                       , PAR.NOMBRE                 AS ESTADOPROCESO
                       , EPG.ID                     AS ID_PAISPUNTO
                       , EDG.ID                     AS ID_DEPARTAMENTOPUNTO
                       , EMG.ID                     AS ID_MUNICIPIOPUNTO
                       , PAT.ID                     AS ID_PUNTOATENCION
                       , DTR.ID                     AS ID_DIRECCIONTERRITORIAL
                       , COALESCE(PAT.ID, DTR.ID)   AS ID_UBICACIONNOTIFICACION
                       , COALESCE(PAT.NOMBRE, DTR.NOMBRE) || CASE WHEN EMG.NOMBRE IS NOT NULL THEN ' (' || EMG.NOMBRE || ')' ELSE NULL END
                                                    AS UBICACIONNOTIFICACION
                       , NTF.APROBADO               AS APROBADO
                  FROM TBNOTIFICACION NTF
                  INNER JOIN TBDECLARACIONES        DCL ON DCL.ID = NTF.ID_DECLARACION
                  INNER JOIN TBACTO_ADMINISTRATIVO  AAD ON AAD.ID_DECLARACION = NTF.ID_DECLARACION
                  INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                              WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
                  INNER JOIN TBPERSONAS             PRS ON PRS.ID = RGP.ID_PERSONA
                  INNER JOIN TBPARAMETROS           PAR ON PAR.ID = DCL.PARAM_ESTADO
                  INNER JOIN TBPARAMETROS           PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
                  INNER JOIN TBESTADOSNOTIFICACION  ESN ON ESN.ID = NTF.ESTADO
                  LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
                  LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
                  LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
                  -- Ubicacion de Notificacion
                  LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
                  LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
                  LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
                  LEFT  JOIN TBGEOGRAFIA            EDG ON EDG.ID = EMG.PADREID
                  LEFT  JOIN TBGEOGRAFIA            EPG ON EPG.ID = EDG.PADREID
                  /* pi_SoloAsignaciones es 1 para L?der de Notificaciones, 0 para Preparador de Notificaciones */
                  WHERE (NTF.ESTADO              = CASE WHEN pi_SoloAsignaciones = 1 THEN PREP_CorreccionInformacion ELSE PREP_PendienteEnvio END
                      OR NTF.ESTADO              = CASE WHEN pi_SoloAsignaciones = 1 THEN PREP_CorreccionInformacion ELSE ENTR_PendientEnvioResolucion END)
                    AND NVL(NTF.ID_USUARIO, -1) = CASE WHEN pi_SoloAsignaciones = 1 THEN pi_IdUsuario ELSE NVL(NTF.ID_USUARIO, -1) END 
                    AND DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_Declaracion, DCL.NUMEROFORMULARIO)  || '%'
                    AND PRS.NUMERODOCUMENTO  LIKE '%' || NVL(pi_Documento, PRS.NUMERODOCUMENTO)     || '%'
                    AND (PRS.PRIMERNOMBRE    LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERNOMBRE) || '%' OR PRS.PRIMERAPELLIDO LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERAPELLIDO) || '%')
                    AND PRS.PARAM_TIPODOCUMENTO = NVL(pi_TipoDocumento, PRS.PARAM_TIPODOCUMENTO)
                    AND COALESCE(EPG.ID, -1) = COALESCE(pi_PaisNotificacion        , EPG.ID, -1)
                    AND COALESCE(EDG.ID, -1) = COALESCE(pi_DepartamentoNotificacion, EDG.ID, -1)
                    AND COALESCE(EMG.ID, -1) = COALESCE(pi_MunicipioNotificacion   , EMG.ID, -1)
                    AND COALESCE(PAT.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'PA' THEN xfIdPuntoAtencion ELSE COALESCE(PAT.ID, -1) END
                    AND COALESCE(DTR.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'DT' THEN xfIdPuntoAtencion ELSE COALESCE(DTR.ID, -1) END
                    AND COALESCE(NTF.DIRECCIONNOTIFICACION, '.') LIKE '%' || COALESCE(pi_DireccionCitacion, NTF.DIRECCIONNOTIFICACION, '.') || '%'
                    AND DFIRMA IS NOT NULL
                  ORDER BY DFIRMA DESC, ESTADONOTIFICACION, FECHAESTADOCOURIER DESC) INFO
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
  END;

  PROCEDURE sp_ConsultaNotificacionesCount(pi_IdUsuario                IN NUMBER   DEFAULT NULL
                                         , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                         , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                         , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                         , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                         , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                         , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                         , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                         , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                         , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                         , pi_SoloAsignaciones         IN NUMBER
                                         , po_RecordCount              OUT NUMBER) IS
    xfTipoPuntoAtencion VARCHAR2(2) := NULL;
    xfIdPuntoAtencion   NUMBER      := NULL;
  BEGIN
    IF pi_EntidadNotificacion IS NOT NULL THEN
      DECLARE
        xfIndex NUMBER;
      BEGIN
        xfIndex := INSTR(pi_EntidadNotificacion, '-');
        xfTipoPuntoAtencion := SUBSTR(pi_EntidadNotificacion, 0, xfIndex - 1);
        xfIdPuntoAtencion   := CAST(SUBSTR(pi_EntidadNotificacion, xfIndex + 1) AS NUMBER);
      EXCEPTION WHEN OTHERS THEN
        NULL;
      END;
    END IF;
    
    SELECT COUNT(1) INTO po_RecordCount
    FROM TBNOTIFICACION NTF
    INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
    INNER JOIN TBACTO_ADMINISTRATIVO  AAD ON AAD.ID_DECLARACION = NTF.ID_DECLARACION
    INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
    INNER JOIN TBPERSONAS             PRS ON PRS.ID = RGP.ID_PERSONA
    INNER JOIN TBPARAMETROS           PAR ON PAR.ID = DCL.PARAM_ESTADO
    INNER JOIN TBPARAMETROS           PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
    INNER JOIN TBESTADOSNOTIFICACION  ESN ON ESN.ID = NTF.ESTADO
    LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
    LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
    LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
    LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
    LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
    LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
    LEFT  JOIN TBGEOGRAFIA            EDG ON EDG.ID = EMG.PADREID
    LEFT  JOIN TBGEOGRAFIA            EPG ON EPG.ID = EDG.PADREID
    /* pi_SoloAsignaciones es 1 para L?der de Notificaciones, 0 para Preparador de Notificaciones */
    WHERE (NTF.ESTADO              = CASE WHEN pi_SoloAsignaciones = 1 THEN PREP_CorreccionInformacion ELSE PREP_PendienteEnvio END
        OR NTF.ESTADO              = CASE WHEN pi_SoloAsignaciones = 1 THEN PREP_CorreccionInformacion ELSE ENTR_PendientEnvioResolucion END)
      AND NVL(NTF.ID_USUARIO, -1) = CASE WHEN pi_SoloAsignaciones = 1 THEN pi_IdUsuario ELSE NVL(NTF.ID_USUARIO, -1) END 
      AND DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_Declaracion, DCL.NUMEROFORMULARIO)  || '%'
      AND PRS.NUMERODOCUMENTO  LIKE '%' || NVL(pi_Documento, PRS.NUMERODOCUMENTO)     || '%'
      AND (PRS.PRIMERNOMBRE    LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERNOMBRE) || '%' OR PRS.PRIMERAPELLIDO LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERAPELLIDO) || '%')
      AND PRS.PARAM_TIPODOCUMENTO = NVL(pi_TipoDocumento, PRS.PARAM_TIPODOCUMENTO)
      AND COALESCE(EPG.ID, -1) = COALESCE(pi_PaisNotificacion        , EPG.ID, -1)
      AND COALESCE(EDG.ID, -1) = COALESCE(pi_DepartamentoNotificacion, EDG.ID, -1)
      AND COALESCE(EMG.ID, -1) = COALESCE(pi_MunicipioNotificacion   , EMG.ID, -1)
      AND COALESCE(PAT.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'PA' THEN xfIdPuntoAtencion ELSE COALESCE(PAT.ID, -1) END
      AND COALESCE(DTR.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'DT' THEN xfIdPuntoAtencion ELSE COALESCE(DTR.ID, -1) END
      AND COALESCE(NTF.DIRECCIONNOTIFICACION, '.') LIKE '%' || COALESCE(pi_DireccionCitacion, NTF.DIRECCIONNOTIFICACION, '.') || '%';
  END;
  
  PROCEDURE sp_ConsultaNotificacionPorId(pi_IdNotificacion IN NUMBER, po_Resultado OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Resultado FOR
      SELECT NTF.ID                     AS ID
           , NTF.ID_DECLARACION         AS ID_DECLARACION
           , NTF.ESTADO                 AS ID_ESTADONOTIFICACION
           , ESN.NOMBRE                 AS ESTADONOTIFICACION
           , NTF.ESTADOCOURIER          AS ESTADOCOURIER
           , NTF.FECHAESTADOCOURIER     AS FECHAESTADOCOURIER
           , NTF.DIRECCIONNOTIFICACION  AS DIRECCIONNOTIFICACION
           , NTF.ID_DEPARTAMENTO        AS ID_DEPARTAMENTO
           , DPT.NOMBRE                 AS NOMBREDEPARTAMENTO
           , NTF.ID_MUNICIPIO           AS ID_MUNICIPIO
           , MCP.NOMBRE                 AS NOMBREMUNICIPIO
           , NTF.ID_PAIS                AS ID_PAIS
           , PAI.NOMBRE                 AS NOMBREPAIS
           , NTF.TELEFONONOTIFICACION   AS TELEFONONOTIFICACION
           , NTF.ID_USUARIO             AS ID_USUARIO
           , NTF.ID_PAQUETENOTIFICACION AS ID_PAQUETENOTIFICACION
           , PTD.NOMBRE                 AS TIPODOCUMENTO
           , DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
           , PRS.PRIMERNOMBRE ||
             CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE END ||
             CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO END ||
             CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRECOMPLETO
           , PRS.NUMERODOCUMENTO        AS NUMERODOCUMENTO
           , PAR.NOMBRE                 AS ESTADOPROCESO
           , PAT.ID                     AS ID_PUNTOATENCION
           , DTR.ID                     AS ID_DIRECCIONTERRITORIAL
           , COALESCE(PAT.ID, DTR.ID)   AS ID_UBICACIONNOTIFICACION
           , COALESCE(PAT.NOMBRE, DTR.NOMBRE) || CASE WHEN EMG.NOMBRE IS NOT NULL THEN ' (' || EMG.NOMBRE || ')' ELSE NULL END
                                        AS UBICACIONNOTIFICACION
           , NTF.APROBADO               AS APROBADO
      FROM TBNOTIFICACION NTF
      INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
      INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                  WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
      INNER JOIN TBPERSONAS             PRS ON PRS.ID = RGP.ID_PERSONA
      INNER JOIN TBPARAMETROS           PAR ON PAR.ID = DCL.PARAM_ESTADO
      INNER JOIN TBPARAMETROS           PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
      INNER JOIN TBESTADOSNOTIFICACION  ESN ON ESN.ID = NTF.ESTADO
      LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
      LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
      LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
      -- Ubicacion de Notificacion
      LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
      LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
      LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
      WHERE NTF.ID = pi_IdNotificacion;
  END;
  
  PROCEDURE sp_CrearPaqueteDesdeFiltro(pi_IdUsuario                IN NUMBER
                                     , pi_Declaracion              IN VARCHAR2 DEFAULT NULL
                                     , pi_TipoDocumento            IN NUMBER   DEFAULT NULL
                                     , pi_Documento                IN VARCHAR2 DEFAULT NULL
                                     , pi_NombreDeclarante         IN VARCHAR2 DEFAULT NULL
                                     , pi_PaisNotificacion         IN NUMBER   DEFAULT NULL
                                     , pi_DepartamentoNotificacion IN NUMBER   DEFAULT NULL
                                     , pi_MunicipioNotificacion    IN NUMBER   DEFAULT NULL
                                     , pi_EntidadNotificacion      IN VARCHAR2 DEFAULT NULL
                                     , pi_DireccionCitacion        IN VARCHAR2 DEFAULT NULL
                                     , pi_SoloAsignaciones         IN NUMBER
                                     , po_IdPaqueteNotifica        OUT NUMBER
                                     , po_RecordCount              OUT NUMBER) IS
    xListaNotificaciones NUMBERARRAY;
    xfTipoPuntoAtencion VARCHAR2(2) := NULL;
    xfIdPuntoAtencion   NUMBER      := NULL;
  BEGIN
    IF pi_EntidadNotificacion IS NOT NULL THEN
      DECLARE
        xfIndex NUMBER;
      BEGIN
        xfIndex := INSTR(pi_EntidadNotificacion, '-');
        xfTipoPuntoAtencion := SUBSTR(pi_EntidadNotificacion, 0, xfIndex - 1);
        xfIdPuntoAtencion   := CAST(SUBSTR(pi_EntidadNotificacion, xfIndex + 1) AS NUMBER);
      EXCEPTION WHEN OTHERS THEN
        NULL;
      END;
    END IF;
    
    SELECT NTF.ID BULK COLLECT INTO xListaNotificaciones
    FROM TBNOTIFICACION NTF
    INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
    INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
    INNER JOIN TBPERSONAS             PRS ON PRS.ID = RGP.ID_PERSONA
    INNER JOIN TBPARAMETROS           PAR ON PAR.ID = DCL.PARAM_ESTADO
    INNER JOIN TBPARAMETROS           PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
    INNER JOIN TBESTADOSNOTIFICACION  ESN ON ESN.ID = NTF.ESTADO
    LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
    LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
    LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
    -- Ubicacion de Notificacion
    LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
    LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
    LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
    /* pi_SoloAsignaciones es 1 para L?der de Notificaciones, 0 para Preparador de Notificaciones */
    WHERE NTF.ESTADO              = CASE WHEN pi_SoloAsignaciones = 1 THEN PREP_CorreccionInformacion ELSE PREP_PendienteEnvio END
      AND NVL(NTF.ID_USUARIO, -1) = CASE WHEN pi_SoloAsignaciones = 1 THEN pi_IdUsuario ELSE NVL(NTF.ID_USUARIO, -1) END 
      AND DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_Declaracion, DCL.NUMEROFORMULARIO)  || '%'
      AND PRS.NUMERODOCUMENTO  LIKE '%' || NVL(pi_Documento, PRS.NUMERODOCUMENTO)     || '%'
      AND (PRS.PRIMERNOMBRE    LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERNOMBRE) || '%' OR PRS.PRIMERAPELLIDO LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERAPELLIDO) || '%')
      AND PRS.PARAM_TIPODOCUMENTO = NVL(pi_TipoDocumento, PRS.PARAM_TIPODOCUMENTO)
      AND COALESCE(PAI.ID, -1) = COALESCE(pi_PaisNotificacion        , PAI.ID, -1)
      AND COALESCE(DPT.ID, -1) = COALESCE(pi_DepartamentoNotificacion, DPT.ID, -1)
      AND COALESCE(MCP.ID, -1) = COALESCE(pi_MunicipioNotificacion   , MCP.ID, -1)
      AND COALESCE(PAT.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'PA' THEN xfIdPuntoAtencion ELSE COALESCE(PAT.ID, -1) END
      AND COALESCE(DTR.ID, -1) = CASE WHEN NVL(xfTipoPuntoAtencion, '') = 'DT' THEN xfIdPuntoAtencion ELSE COALESCE(DTR.ID, -1) END
      AND COALESCE(NTF.DIRECCIONNOTIFICACION, '.') LIKE '%' || COALESCE(pi_DireccionCitacion, NTF.DIRECCIONNOTIFICACION, '.') || '%';
    
    po_RecordCount := xListaNotificaciones.Count;
    
    IF po_RecordCount > 0 THEN
      
      INSERT INTO TBPAQUETENOTIFICACION (ID, ID_USUARIOGENERACION, FECHA, ESTADO)
      VALUES (SEQ_TBPAQUETENOTIFICACION.NextVal, pi_IdUsuario, SYSDATE, GENERADO)
      RETURNING ID INTO po_IdPaqueteNotifica;
      
      FOR ix in xListaNotificaciones.First..xListaNotificaciones.Last LOOP
        PKG_NOTIFICACION.sp_AgregarHistorico(xListaNotificaciones(ix));
        UPDATE TBNOTIFICACION SET ESTADO = PREP_Enviado, ID_PAQUETENOTIFICACION = po_IdPaqueteNotifica
        WHERE ID = xListaNotificaciones(ix);
      END LOOP;
      
    END IF;
  END;

  PROCEDURE sp_DetalleNotificacion(pi_IdNotificacion IN NUMBER
                                 , po_Cursor         OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT NTF.ID                     AS ID
           , NTF.ID_DECLARACION         AS ID_DECLARACION
           , DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
           , STD.NOMBRE                 AS ESTADODECLARACION
           , NVL(PRS.PRIMERNOMBRE, ' ') ||
             CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE   END ||
             CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO  END ||
             CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END
                                        AS NOMBREDECLARANTE
           , PRS.NUMERODOCUMENTO        AS DOCUMENTOIDENTIDAD
           , TDC.NOMBRE                 AS TIPODOCUMENTO
           , STN.NOMBRE                 AS ESTADONOTIFICACION
           , STN.ID                     AS IDESTADONOTIFICACION
           , NTF.DIRECCIONNOTIFICACION  AS DIRECCIONNOTIFICACION
           , NTF.TELEFONONOTIFICACION   AS TELEFONONOTIFICACION
           , PAI.NOMBRE                 AS PAIS
           , DPT.NOMBRE                 AS DEPARTAMENTO
           , MCP.NOMBRE                 AS MUNICIPIO
           , COALESCE(PAT.NOMBRE, DTR.NOMBRE) || CASE WHEN EMG.NOMBRE IS NOT NULL THEN ' (' || EMG.NOMBRE || ')' ELSE NULL END
                                        AS UBICACIONNOTIFICACION
           , NTF.ID_USUARIO             AS ID_USUARIO
           , NTF.ID_PAQUETENOTIFICACION AS ID_PAQUETENOTIFICACION
           , NTF.APROBADO               AS APROBADO
      FROM TBNOTIFICACION NTF
      INNER JOIN TBDECLARACIONES        DCL ON NTF.ID_DECLARACION = DCL.ID
      INNER JOIN TBREGISTROS_PERSONAS   RGP ON RGP.ID_DECLARACION = DCL.ID AND RGP.ESDECLARANTE = 1
      INNER JOIN TBPERSONAS             PRS ON RGP.ID_PERSONA = PRS.ID
      INNER JOIN TBESTADOSNOTIFICACION  STN ON STN.ID = NTF.ESTADO
      LEFT  JOIN TBPARAMETROS           TDC ON PRS.PARAM_TIPODOCUMENTO = TDC.ID
      LEFT  JOIN TBPARAMETROS           STD ON DCL.PARAM_ESTADO = STD.ID
      LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
      LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
      LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
      -- Ubicacion de Notificacion
      LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
      LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
      LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
      --LEFT  JOIN TBGEOGRAFIA            EDG ON EDG.ID = EMG.PADREID
      --LEFT  JOIN TBGEOGRAFIA            EPG ON EPG.ID = EDG.PADREID
      WHERE NTF.ID = pi_IdNotificacion;
  END;

PROCEDURE SP_ACTUALIZARNOTIFICACION(
                                    PI_IDNOTIFICACION IN NUMBER, 
                                    PI_DIRECCIONENVIO IN VARCHAR2                                   
                                    ) IS

BEGIN

  UPDATE TBNOTIFICACION
    SET DIRECCIONNOTIFICACION  = PI_DIRECCIONENVIO
  WHERE ID = PI_IdNotificacion;

END;

  PROCEDURE sp_ActualizarPuntoNotificacion(pi_IdNotificacion IN NUMBER, pi_IdPais IN NUMBER, pi_IdDepartamento IN NUMBER, pi_IdMunicipio IN NUMBER, pi_DireccionEnvio IN VARCHAR2, pi_IdPuntoAtencion IN NUMBER DEFAULT NULL, pi_IdDireccionTerritorial IN NUMBER DEFAULT NULL) IS
  BEGIN
    UPDATE TBNOTIFICACION SET ID_PAIS                 = pi_IdPais
                            , ID_DEPARTAMENTO         = pi_IdDepartamento
                            , ID_MUNICIPIO            = pi_IdMunicipio
                            , DIRECCIONNOTIFICACION   = pi_DireccionEnvio
                            , ID_PUNTOATENCION        = pi_IdPuntoAtencion
                            , ID_DIRECCIONTERRITORIAL = pi_IdDireccionTerritorial
    WHERE ID = pi_IdNotificacion;
  END;

  PROCEDURE sp_CrearPaqueteNotificacion(pi_IdUsuario         IN NUMBER
                                      , po_IdPaqueteNotifica OUT NUMBER) IS
  BEGIN
    INSERT INTO TBPAQUETENOTIFICACION (ID
                                     , ID_USUARIOGENERACION
                                     , FECHA
                                     , ESTADO)
    VALUES (SEQ_TBPAQUETENOTIFICACION.NextVal
          , pi_IdUsuario
          , SYSDATE
          , GENERADO)
    RETURNING ID INTO po_IdPaqueteNotifica;
  END;

  PROCEDURE sp_AsociarNotificacionAPaquete(pi_IdNotificacion    IN NUMBER
                                         , pi_IdPaqueteNotifica IN NUMBER) IS
  BEGIN
    PKG_NOTIFICACION.sp_AgregarHistorico(pi_IdNotificacion);
    UPDATE TBNOTIFICACION SET ESTADO = PREP_EnvioEnProceso
                            , ID_PAQUETENOTIFICACION = pi_IdPaqueteNotifica
    WHERE ID = pi_IdNotificacion;
  END;
 
  PROCEDURE sp_ActualizarEstadoCourier(pi_IdNotificacion      IN NUMBER
                                     , pi_EstadoNotificacion IN NUMBER
                                     , pi_EstadoCourier      IN VARCHAR2
                                     , pi_Fecha              IN DATE DEFAULT NULL
                                     , pi_FechaFinal         IN DATE DEFAULT NULL) IS
  BEGIN
    PKG_NOTIFICACION.sp_AgregarHistorico(pi_IdNotificacion);
    UPDATE TBNOTIFICACION SET ESTADO             = pi_EstadoNotificacion
                            , ESTADOCOURIER      = pi_EstadoCourier
                            , FECHAESTADOCOURIER = pi_Fecha
                            , FECHAFINAL         = pi_FechaFinal
    WHERE ID = pi_IdNotificacion;
  END;
  
  PROCEDURE sp_ConsultaNtfEntregadas(pi_IdUsuario           IN NUMBER
                                   , pi_BusquedaGlobal      IN NUMBER
                                   , pi_Declaracion         IN VARCHAR2 DEFAULT NULL
                                   , pi_TipoDocumento       IN NUMBER   DEFAULT NULL
                                   , pi_Documento           IN VARCHAR2 DEFAULT NULL
                                   , pi_NombreDeclarante    IN VARCHAR2 DEFAULT NULL
                                   , pi_EstadoNotificacion  IN NUMBER   DEFAULT NULL
                                   , pi_Orden               IN VARCHAR2
                                   , pi_PageNumber          IN NUMBER
                                   , pi_PageSize            IN NUMBER
                                   , po_Resultado           OUT CURSOR_TYPE) IS
    startRow NUMBER;
    endRow NUMBER;
  BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow := (pi_PageNumber * pi_PageSize) + 1;
    OPEN po_Resultado FOR
      SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (SELECT NTF.ID                     AS ID
                       , NTF.ID_DECLARACION         AS ID_DECLARACION
                       , NTF.ESTADO                 AS ID_ESTADONOTIFICACION
                       , ESN.NOMBRE                 AS ESTADONOTIFICACION
                       , NTF.ESTADOCOURIER          AS ESTADOCOURIER
                       , NTF.FECHAESTADOCOURIER     AS FECHAESTADOCOURIER
                       , NTF.DIRECCIONNOTIFICACION  AS DIRECCIONNOTIFICACION
                       , NTF.ID_DEPARTAMENTO        AS ID_DEPARTAMENTO
                       , DPT.NOMBRE                 AS NOMBREDEPARTAMENTO
                       , NTF.ID_MUNICIPIO           AS ID_MUNICIPIO
                       , MCP.NOMBRE                 AS NOMBREMUNICIPIO
                       , NTF.ID_PAIS                AS ID_PAIS
                       , PAI.NOMBRE                 AS NOMBREPAIS
                       , NTF.TELEFONONOTIFICACION   AS TELEFONONOTIFICACION
                       , NTF.ID_USUARIO             AS ID_USUARIO
                       , NTF.ID_PAQUETENOTIFICACION AS ID_PAQUETENOTIFICACION
                       , PTD.NOMBRE                 AS TIPODOCUMENTO
                       , DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
                       , PRS.PRIMERNOMBRE ||
                         CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE END ||
                         CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO END ||
                         CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRECOMPLETO
                       , PRS.NUMERODOCUMENTO        AS NUMERODOCUMENTO
                       , PAR.NOMBRE                 AS ESTADOPROCESO
                       , COALESCE(PAT.NOMBRE, DTR.NOMBRE) ||
                         CASE WHEN COALESCE(GPA.NOMBRE, GDT.NOMBRE) IS NOT NULL 
                              THEN ' (' || COALESCE(GPA.NOMBRE, GDT.NOMBRE) || ')' ELSE '' END
                                                    AS UBICACIONNOTIFICACION
                       , NTF.APROBADO               AS APROBADO
                       , NTF.FECHAFINAL             AS FECHAFINAL
                  FROM TBNOTIFICACION NTF
                  INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
                  INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                              WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
                  INNER JOIN TBPERSONAS             PRS ON PRS.ID = RGP.ID_PERSONA
                  INNER JOIN TBPARAMETROS           PAR ON PAR.ID = DCL.PARAM_ESTADO
                  INNER JOIN TBPARAMETROS           PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
                  INNER JOIN TBESTADOSNOTIFICACION  ESN ON ESN.ID = NTF.ESTADO
                  LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
                  LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
                  LEFT  JOIN TBGEOGRAFIA            PAI ON PAI.ID = NTF.ID_PAIS
                  LEFT  JOIN TBGEOGRAFIA            DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
                  LEFT  JOIN TBGEOGRAFIA            MCP ON MCP.ID = NTF.ID_MUNICIPIO
                  LEFT  JOIN TBGEOGRAFIA            GPA ON GPA.ID = PAT.IDMUNICIPIO
                  LEFT  JOIN TBGEOGRAFIA            GDT ON GDT.ID = DTR.IDMUNICIPIO
                  WHERE ((EXISTS (SELECT * FROM TBENCARGADOENTIDAD EEN WHERE EEN.ID_ENCARGADO = pi_IdUsuario AND (EEN.ID_PUNTOATENCION = NTF.ID_PUNTOATENCION OR EEN.ID_DIRECCIONTERRITORIAL = NTF.ID_DIRECCIONTERRITORIAL)) 
                          AND NTF.ESTADO >= COUR_NotificacionEntregada)
                         OR 
                         pi_BusquedaGlobal = 1)
                    AND DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_Declaracion, DCL.NUMEROFORMULARIO) || '%'
                    AND PRS.PARAM_TIPODOCUMENTO = NVL(pi_TipoDocumento, PRS.PARAM_TIPODOCUMENTO)
                    AND PRS.NUMERODOCUMENTO LIKE '%' || NVL(pi_Documento, PRS.NUMERODOCUMENTO) || '%'
                    AND (PRS.PRIMERNOMBRE LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERNOMBRE) || '%' OR PRS.PRIMERAPELLIDO LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERAPELLIDO) || '%')
                    AND NTF.ESTADO = NVL(pi_EstadoNotificacion, NTF.ESTADO)
                  ORDER BY NTF.ESTADO, NTF.FECHAFINAL DESC) INFO
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
  END;

  PROCEDURE sp_ConsultaNtfEntregadasCount(pi_IdUsuario            IN NUMBER
                                        , pi_BusquedaGlobal       IN NUMBER
                                        , pi_Declaracion          IN VARCHAR2 DEFAULT NULL
                                        , pi_TipoDocumento        IN NUMBER   DEFAULT NULL
                                        , pi_Documento            IN VARCHAR2 DEFAULT NULL
                                        , pi_NombreDeclarante     IN VARCHAR2 DEFAULT NULL
                                        , pi_EstadoNotificacion   IN NUMBER   DEFAULT NULL
                                        , po_RecordCount          OUT NUMBER) IS
  BEGIN
    SELECT COUNT(1) INTO po_RecordCount FROM TBNOTIFICACION NTF
    INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
    INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
    INNER JOIN TBPERSONAS            PRS ON PRS.ID = RGP.ID_PERSONA
    INNER JOIN TBPARAMETROS          PAR ON PAR.ID = DCL.PARAM_ESTADO
    INNER JOIN TBPARAMETROS          PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
    INNER JOIN TBESTADOSNOTIFICACION ESN ON ESN.ID = NTF.ESTADO
    LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
    LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
    WHERE ((EXISTS (SELECT * FROM TBENCARGADOENTIDAD EEN WHERE EEN.ID_ENCARGADO = pi_IdUsuario AND (EEN.ID_PUNTOATENCION = NTF.ID_PUNTOATENCION OR EEN.ID_DIRECCIONTERRITORIAL = NTF.ID_DIRECCIONTERRITORIAL)) 
            AND NTF.ESTADO >= COUR_NotificacionEntregada)
           OR 
           pi_BusquedaGlobal = 1)
      AND DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_Declaracion, DCL.NUMEROFORMULARIO) || '%'
      AND PRS.PARAM_TIPODOCUMENTO = NVL(pi_TipoDocumento, PRS.PARAM_TIPODOCUMENTO)
      AND PRS.NUMERODOCUMENTO LIKE '%' || NVL(pi_Documento, PRS.NUMERODOCUMENTO) || '%'
      AND (PRS.PRIMERNOMBRE LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERNOMBRE) || '%' OR PRS.PRIMERAPELLIDO LIKE '%' || NVL(pi_NombreDeclarante, PRS.PRIMERAPELLIDO) || '%')
      AND NTF.ESTADO = NVL(pi_EstadoNotificacion, NTF.ESTADO);
  END;

  PROCEDURE sp_GetPuntosNotificacion(po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID                               
           , NOMBRE
           , ID_ENTIDAD
           , ID_MUNICIPIO
           , DIRECCIONENTIDAD
      FROM TBENTIDADMUNICIPIO
      WHERE ESPUNTONOTIFICACION = 1;
  END;

PROCEDURE SP_GETPUNTOSNOTBYDIRTER(
                                  PI_IDDIRECCIONTERRITORIAL NUMBER,
                                  PO_CURSOR OUT CURSOR_TYPE
                                  ) IS
BEGIN
  OPEN PO_CURSOR FOR
  SELECT * FROM DUAL;
    /*SELECT  PN.ID,
            PN.NOMBRE,
            DT.NOMBRE AS NOMBREDT,
            DT.ID_MUNICIPIO
    FROM TBPUNTONOTIFICACION PN
    INNER JOIN TBDIRECCIONTERRITORIAL DT ON DT.ID = PN.ID_DIRECCIONTERRITORIAL
    WHERE DT.ID = PI_IDDIRECCIONTERRITORIAL;*/
END;

  PROCEDURE SP_FINALIZANOTIFICACION(PI_IDNOTIFICACION IN NUMBER) IS
  BEGIN
    UPDATE TBNOTIFICACION SET ESTADO = ENTR_NotificadoPersonal
    WHERE ID = PI_IDNOTIFICACION;
  END;

/* Cambia el estado de una notificacion */
PROCEDURE SP_CAMBIAESTADONOTIFICACION(PI_IDNOTIFICACION IN NUMBER, PI_IDESTADO IN NUMBER, PI_FECHAFIN IN DATE DEFAULT NULL, PI_OBSERVACION IN VARCHAR2) IS

BEGIN
    sp_AgregarHistorico(PI_IDNOTIFICACION);
    UPDATE TBNOTIFICACION SET 
      ESTADO = PI_IDESTADO,
      FECHAFINAL = PI_FECHAFIN,
      OBSERVACIONNOTIFICACION = PI_OBSERVACION
    WHERE ID = PI_IDNOTIFICACION;
END;

  /* Conteo de registros de paquetes de notificaciones */
  PROCEDURE sp_ConsultaPaquetesCount(pi_IdUsuario IN NUMBER, pi_OrdenServicio IN VARCHAR2 DEFAULT NULL, pi_FechaInicio IN DATE DEFAULT NULL, pi_FechaFin IN DATE DEFAULT NULL, po_RecordCount OUT NUMBER) IS
  BEGIN
    SELECT COUNT(1) INTO po_RecordCount
    FROM TBPAQUETENOTIFICACION PNT
    WHERE COALESCE(UPPER(PNT.ORDENSERVICIO), '.') LIKE '%' || COALESCE(pi_OrdenServicio, UPPER(PNT.ORDENSERVICIO), '.') || '%'
      AND TRUNC(PNT.FECHA, 'DD') >= TRUNC(NVL(pi_FechaInicio, PNT.FECHA), 'DD')
      AND TRUNC(PNT.FECHA, 'DD') <= TRUNC(NVL(pi_FechaFin, PNT.FECHA), 'DD');
  END;

  /* Registros de paquetes de notificaciones */
  PROCEDURE sp_ConsultaPaquetes(pi_IdUsuario IN NUMBER, pi_OrdenServicio IN VARCHAR2 DEFAULT NULL, pi_FechaInicio IN DATE DEFAULT NULL, pi_FechaFin IN DATE DEFAULT NULL, pi_PageNumber IN NUMBER, pi_PageSize IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
    startRow NUMBER;
    endRow NUMBER;
  BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow := (pi_PageNumber * pi_PageSize) + 1;
    -- Parameter pi_IdUsuario can be used to determine ownership over objects, as well as permissions
    OPEN po_Cursor FOR
      SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (SELECT PNT.ID               AS ID
                       , PNT.FECHA            AS FECHA
                       , PNT.ORDENSERVICIO    AS ORDENSERVICIO
                       , USR.NOMBRE           AS NOMBRE
                       , NVL(NTF.CANTIDAD, 0) AS CANTIDAD
                       , rowconcat('SELECT CANTIDAD || '' en '' || NOMBRE
                                    FROM ( SELECT NTF.ESTADO, COUNT(*) AS CANTIDAD, ENT.NOMBRE
                                    FROM TBESTADOSNOTIFICACION ENT
                                    INNER JOIN TBNOTIFICACION NTF ON ENT.ID = NTF.ESTADO
                                    WHERE ID_PAQUETENOTIFICACION = ' || PNT.ID || ' AND 
                                          ((NTF.ESTADO >= ' || PREP_EnvioEnProceso || ' AND NTF.ESTADO <= ' || COUR_NotificacionRechazada || ' ) OR 
                                           (NTF.ESTADO >= ' || ENTR_NotificadoPersonal || ' AND NTF.ESTADO <= ' || ENTR_NotificadoResolucion || '))
                                    GROUP BY NTF.ESTADO, ENT.NOMBRE ORDER BY NTF.ESTADO)') AS RESUMEN
                  FROM TBPAQUETENOTIFICACION PNT
                  LEFT OUTER JOIN (SELECT ID_PAQUETENOTIFICACION
                                        , COUNT(1) AS CANTIDAD
                                   FROM TBNOTIFICACION
                                   WHERE ((ESTADO >= PREP_EnvioEnProceso AND ESTADO <= COUR_NotificacionRechazada) OR 
                                          (ESTADO >= ENTR_NotificadoPersonal AND ESTADO <= ENTR_NotificadoResolucion))
                                   GROUP BY ID_PAQUETENOTIFICACION) NTF ON NTF.ID_PAQUETENOTIFICACION = PNT.ID
                  LEFT OUTER JOIN TBUSUARIOS USR ON USR.ID = PNT.ID_USUARIOGENERACION
                  WHERE COALESCE(UPPER(PNT.ORDENSERVICIO), '.') LIKE '%' || COALESCE(pi_OrdenServicio, UPPER(PNT.ORDENSERVICIO), '.') || '%'
                    AND TRUNC(PNT.FECHA, 'DD') >= TRUNC(NVL(pi_FechaInicio, PNT.FECHA), 'DD')
                    AND TRUNC(PNT.FECHA, 'DD') <= TRUNC(NVL(pi_FechaFin, PNT.FECHA), 'DD')
                    ORDER BY PNT.FECHA DESC) INFO
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
  END;
  
  /* Registro de paquete de notificaciones por id */
  PROCEDURE sp_ConsultaPaquetePorId(p_Id IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT PNT.ID               AS ID
           , PNT.FECHA            AS FECHA
           , PNT.ORDENSERVICIO    AS ORDENSERVICIO
           , USR.NOMBRE           AS NOMBRE
           , NVL(NTF.CANTIDAD, 0) AS CANTIDAD
           , rowconcat('SELECT CANTIDAD || '' en '' || NOMBRE
                        FROM ( SELECT NTF.ESTADO, COUNT(*) AS CANTIDAD, ENT.NOMBRE
                        FROM TBESTADOSNOTIFICACION ENT
                        INNER JOIN TBNOTIFICACION NTF ON ENT.ID = NTF.ESTADO
                        WHERE ID_PAQUETENOTIFICACION = ' || p_Id || ' AND 
                              ((NTF.ESTADO >= ' || PREP_EnvioEnProceso || ' AND NTF.ESTADO <= ' || COUR_NotificacionRechazada || ' ) OR 
                               (NTF.ESTADO >= ' || ENTR_NotificadoPersonal || ' AND NTF.ESTADO <= ' || ENTR_NotificadoResolucion || '))
                        GROUP BY NTF.ESTADO, ENT.NOMBRE ORDER BY NTF.ESTADO)') AS RESUMEN
      FROM TBPAQUETENOTIFICACION PNT
      LEFT OUTER JOIN (SELECT ID_PAQUETENOTIFICACION
                            , COUNT(1) AS CANTIDAD
                       FROM TBNOTIFICACION
                       WHERE ((ESTADO >= PREP_EnvioEnProceso AND ESTADO <= COUR_NotificacionRechazada) OR 
                              (ESTADO >= ENTR_NotificadoPersonal AND ESTADO <= ENTR_NotificadoResolucion))
                       GROUP BY ID_PAQUETENOTIFICACION) NTF ON NTF.ID_PAQUETENOTIFICACION = PNT.ID
      LEFT OUTER JOIN TBUSUARIOS USR ON USR.ID = PNT.ID_USUARIOGENERACION
      WHERE PNT.ID = p_Id;
  END;
  
  /* Conteo detalle (notificaciones) de paquete */  
  PROCEDURE sp_ConsultaDetallePaqueteCount(pi_IdPaqueteNotifica IN NUMBER, po_RecordCount OUT NUMBER) IS
  BEGIN
    SELECT COUNT(1) INTO po_RecordCount
    FROM TBNOTIFICACION
    WHERE ID_PAQUETENOTIFICACION = pi_IdPaqueteNotifica;
  END;

  /* Detalle (notificaciones) de paquete */  
  PROCEDURE sp_ConsultaDetallePaquete(pi_IdPaqueteNotifica IN NUMBER, pi_PageNumber IN NUMBER, pi_PageSize IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
   
    OPEN po_Cursor FOR
      SELECT NTF.ID                    AS ID
           , NTF.ID_DECLARACION        AS ID_DECLARACION
           , DCL.NUMEROFORMULARIO      AS NUMEROFORMULARIO
           , VAL.ID_VALORACION         AS ID_VALORACION
           , ENT.ID                    AS CODIGOESTADONOTIFICACION
           , ENT.NOMBRE                as ESTADONOTIFICACION
           , NTF.ESTADOCOURIER         AS ESTADOCOURIER
           , PRS.PRIMERNOMBRE ||
             CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE END ||
             CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO END ||
             CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRECOMPLETO
           , TDC.NOMBRE                AS TIPODOCUMENTO
           , PRS.NUMERODOCUMENTO       AS NUMERODOCUMENTO
           , PAI.NOMBRE                AS NOMBREPAIS
           , PAI.NOMBREALTERNO         AS NOMBREPAISALTERNO
           , DPT.NOMBRE                AS NOMBREDEPARTAMENTO
           , DPT.NOMBREALTERNO         AS NOMBREDEPARTAMENTOALTERNO
           , MCP.NOMBRE                AS NOMBREMUNICIPIO
           , MCP.NOMBREALTERNO         AS NOMBREMUNICIPIOALTERNO
           , NTF.DIRECCIONNOTIFICACION AS DIRECCIONNOTIFICACION
           , CODOR.NUMERO              AS CODIGOORFEO
           , NTF.IDCODIGOGUIA
           , NTF.ENVIORESOLUCION
           , PAQ.ORDENSERVICIO
           , COALESCE(PAT.NOMBRE, DTR.NOMBRE) || 
             CASE WHEN EMG.NOMBRE IS NOT NULL THEN ' (' || EMG.NOMBRE || ')' ELSE NULL END
              AS UBICACIONNOTIFICACION
      FROM TBNOTIFICACION NTF
      INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
      INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                  WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
      -- TODO: Cambiar a valoracion referenciada por notificacion
      INNER JOIN (SELECT ID_DECLARACION, MAX(ID) AS ID_VALORACION FROM TBVALORACION
                  GROUP BY ID_DECLARACION) VAL ON VAL.ID_DECLARACION = DCL.ID
      INNER JOIN TBPERSONAS            PRS ON PRS.ID = RGP.ID_PERSONA
      INNER JOIN TBPARAMETROS          TDC ON TDC.ID = PRS.PARAM_TIPODOCUMENTO
      INNER JOIN TBESTADOSNOTIFICACION ENT ON ENT.ID = NTF.ESTADO
      INNER JOIN TBPAQUETENOTIFICACION PAQ ON PAQ.ID = NTF.ID_PAQUETENOTIFICACION
      LEFT  JOIN TBPUNTOATENCION        PAT ON PAT.ID = NTF.ID_PUNTOATENCION
      LEFT  JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = NTF.ID_DIRECCIONTERRITORIAL
      LEFT  JOIN TBGEOGRAFIA            EMG ON EMG.ID = COALESCE(PAT.IDMUNICIPIO, DTR.IDMUNICIPIO)
      LEFT  JOIN TBGEOGRAFIA           PAI ON PAI.ID = NTF.ID_PAIS
      LEFT  JOIN TBGEOGRAFIA           DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
      LEFT  JOIN TBGEOGRAFIA           MCP ON MCP.ID = NTF.ID_MUNICIPIO 
      LEFT  JOIN  TBCODIGOORFEO        CODOR ON CODOR.NIDVALORACION = VAL.ID_VALORACION
      WHERE NTF.ID_PAQUETENOTIFICACION = pi_IdPaqueteNotifica;
  END;

  PROCEDURE sp_AgregarOrdenServicio(pi_IdPaqueteNotifica IN NUMBER
                                  , pi_OrdenServicio     IN VARCHAR2) IS                                  
  BEGIN 
    UPDATE TBPAQUETENOTIFICACION SET ORDENSERVICIO = PI_ORDENSERVICIO
    WHERE ID = pi_IdPaqueteNotifica;
  END;

  PROCEDURE sp_AprobarNotificacion(pi_IdNotificacion IN NUMBER) AS
  BEGIN
    PKG_NOTIFICACION.sp_AgregarHistorico(pi_IdNotificacion);
    UPDATE TBNOTIFICACION SET APROBADO = 1
                            , ESTADO   = PREP_PendienteEnvio
    WHERE ID = pi_IdNotificacion;
  END;

PROCEDURE SP_GUARDAOBSERVACION(PI_IDNOTIFICACION IN NUMBER,PI_OBSERVACION IN VARCHAR2) IS

BEGIN
UPDATE TBNOTIFICACION SET OBSERVACIONNOTIFICACION = PI_OBSERVACION
WHERE ID = PI_IDNOTIFICACION;
END;

PROCEDURE SP_GETALLNOTIFICACIONES(PO_CURSOR OUT CURSOR_TYPE) IS
BEGIN
    OPEN PO_CURSOR FOR
        SELECT NTF.ID                     AS ID
             , NTF.ID_DECLARACION         AS ID_DECLARACION
             , NTF.ESTADO                 AS ID_ESTADONOTIFICACION
             , ESN.NOMBRE                 AS ESTADONOTIFICACION
             , NTF.ESTADOCOURIER          AS ESTADOCOURIER
             , NTF.FECHAESTADOCOURIER     AS FECHAESTADOCOURIER
             , NTF.DIRECCIONNOTIFICACION  AS DIRECCIONNOTIFICACION
             , NTF.ID_DEPARTAMENTO        AS ID_DEPARTAMENTO
             , DPT.NOMBRE                 AS NOMBREDEPARTAMENTO
             , NTF.ID_MUNICIPIO           AS ID_MUNICIPIO
             , MCP.NOMBRE                 AS NOMBREMUNICIPIO
             , NTF.ID_PAIS                AS ID_PAIS
             , PAI.NOMBRE                 AS NOMBREPAIS
             , NTF.TELEFONONOTIFICACION   AS TELEFONONOTIFICACION
             , NTF.ID_USUARIO             AS ID_USUARIO
             , NTF.ID_PAQUETENOTIFICACION AS ID_PAQUETENOTIFICACION
             , PTD.NOMBRE                 AS TIPODOCUMENTO
             , DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
             , PRS.PRIMERNOMBRE ||
               CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE END ||
               CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO END ||
               CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBRECOMPLETO
             , PRS.NUMERODOCUMENTO        AS NUMERODOCUMENTO
             , PAR.NOMBRE                 AS ESTADOPROCESO
             , EMC.NOMBRE || CASE WHEN EMG.NOMBRE IS NOT NULL THEN ' (' || EMG.NOMBRE || ')' ELSE NULL END
                                          AS UBICACIONNOTIFICACION
             , NTF.APROBADO               AS APROBADO
        FROM TBNOTIFICACION NTF
        INNER JOIN TBDECLARACIONES DCL ON DCL.ID = NTF.ID_DECLARACION
        INNER JOIN (SELECT ID_DECLARACION, MIN(ID_PERSONA) AS ID_PERSONA FROM TBREGISTROS_PERSONAS
                    WHERE TBREGISTROS_PERSONAS.ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RGP ON RGP.ID_DECLARACION = DCL.ID
        INNER JOIN TBPERSONAS            PRS ON PRS.ID = RGP.ID_PERSONA
        INNER JOIN TBPARAMETROS          PAR ON PAR.ID = DCL.PARAM_ESTADO
        INNER JOIN TBPARAMETROS          PTD ON PTD.ID = PRS.PARAM_TIPODOCUMENTO
        INNER JOIN TBESTADOSNOTIFICACION ESN ON ESN.ID = NTF.ESTADO
        LEFT  JOIN TBGEOGRAFIA           PAI ON PAI.ID = NTF.ID_PAIS
        LEFT  JOIN TBGEOGRAFIA           DPT ON DPT.ID = NTF.ID_DEPARTAMENTO
        LEFT  JOIN TBGEOGRAFIA           MCP ON MCP.ID = NTF.ID_MUNICIPIO
        LEFT  JOIN TBENTIDADMUNICIPIO    EMC ON EMC.ID = 1126 -- NTF.ID_ENTIDADMUNICIPIO
        LEFT  JOIN TBGEOGRAFIA           EMG ON EMG.ID = EMC.ID_MUNICIPIO
        WHERE NTF.ESTADO = PREP_Enviado OR NTF.ESTADO = COUR_NotificacionRechazada;
END;

  PROCEDURE SP_PROCESARNOTIFICACIONES AS
      lideresNotificacion NUMBERARRAY;
  BEGIN  
    -- Obtener en una lista los identificadores de los Lideres de Notificaciones
    SELECT ID_USUARIO BULK COLLECT INTO lideresNotificacion FROM TBROLES_USUARIO WHERE ID_ROL = 1019;
    
    FOR I IN (SELECT  N.ID, 
                      N.ID_DECLARACION, 
                      N.ESTADO, 
                      N.ID_PAIS, 
                      N.FECHAFINAL, 
                      N.OBSERVACIONNOTIFICACION,
                      N.ID_USUARIO,
                      N.ID_PUNTOATENCION,
                      N.ID_DIRECCIONTERRITORIAL,
                      N.TIPOCODIGOACTO,
                      CO.NUMERO                      
              FROM TBNOTIFICACION N
              INNER JOIN TBDECLARACIONES D on d.id = n.id_declaracion
              inner join (SELECT ID_DECLARACION, MAX(ID) AS ID_VALORACION FROM TBVALORACION
                          GROUP BY ID_DECLARACION) VAL ON VAL.ID_DECLARACION = D.ID
              inner join tbcodigoorfeo co on co.NIDVALORACION = VAL.ID_VALORACION     
              WHERE FECHAFINAL IS NOT NULL AND
                    CURRENT_DATE > FECHAFINAL) LOOP
      DECLARE
          listaFuncionarios NUMBERARRAY;
      BEGIN
        -- if para comparar si es ley nueva o ley vieja
       IF I.TIPOCODIGOACTO = 0 THEN
              -- Obtener la lista de funcionarios para la notificacion (encargados de la DT o el PA asociado a la notificacion
              SELECT ID_ENCARGADO BULK COLLECT INTO listaFuncionarios
              FROM (SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_PUNTOATENCION IS NOT NULL AND ID_PUNTOATENCION = I.ID_PUNTOATENCION
                    UNION
                    SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_DIRECCIONTERRITORIAL IS NOT NULL AND ID_DIRECCIONTERRITORIAL = I.ID_DIRECCIONTERRITORIAL);
              
               /* Vencimiento de terminos PLAN A (al dia 6 se cambia el estado a pendiente publicacion) */
              IF I.ESTADO = COUR_NotificacionEntregada AND TRUNC(CURRENT_DATE, 'DD') >= TRUNC(I.FECHAFINAL, 'DD') + 1 THEN
                UPDATE TBNOTIFICACION 
                   SET ESTADO = ENTR_PendientEnvioResolucion,
                       IDCODIGOGUIA = '' 
                WHERE ID = I.ID;
                -- Alerte a los encargados
                PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'Notificacion pendiente de publicacion edicto', 
                                                                                                                'Senior Usuario, la notificacion con el codigo orfeo ' || I.NUMERO ||
                                                                                                                ' esta lista para publicar edicto');
              END IF;
              
              /* Alerta para que el Lider le recuerde al encargado de la publicaci?n de edicto pendiente */
              IF I.ESTADO = ENTR_PendientePublicacion AND TRUNC(CURRENT_DATE, 'DD') = TRUNC(I.FECHAFINAL, 'DD') + 2 THEN
                -- Alerte a LN y Encargado
                PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, lideresNotificacion, 'Notificacion pendiente de publicacion edicto', 
                                                                                                                  'Senior Usuario, la notificacion con el codigo orfeo ' || I.NUMERO ||
                                                                                                                  ' esta lista para publicar edicto');
                PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'Notificacion pendiente de publicacion edicto', 
                                                                                                                'Senior Usuario, la notificacion con el codigo orfeo ' || I.NUMERO ||
                                                                                                                ' esta lista para publicar edicto');
              END IF;
     ELSE 
         -- Obtener la lista de funcionarios para la notificacion (encargados de la DT o el PA asociado a la notificacion
            SELECT ID_ENCARGADO BULK COLLECT INTO listaFuncionarios
            FROM (SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_PUNTOATENCION IS NOT NULL AND ID_PUNTOATENCION = I.ID_PUNTOATENCION
                  UNION
                  SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_DIRECCIONTERRITORIAL IS NOT NULL AND ID_DIRECCIONTERRITORIAL = I.ID_DIRECCIONTERRITORIAL);
            
            
            IF I.ESTADO = COUR_NotificacionEntregada AND TRUNC(CURRENT_DATE, 'DD') >= TRUNC(I.FECHAFINAL, 'DD') + 1 THEN
              UPDATE TBNOTIFICACION SET ENVIORESOLUCION = 1,ESTADO = ENTR_PendientEnvioResolucion, APROBADO = 1, ID_USUARIO = PKG_Common.F_USUARIOMENOSCARGA(LIDERNOTIFICACIONES) WHERE ID = I.ID;
              -- Alerte a LN y Encargado
              PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, lideresNotificacion, 'Notificacion pendiente Envio de Resolucion', 
                                                                                                                'Senior Usuario, la notificacion con el codigo orfeo ' || I.NUMERO ||
                                                                                                                ' esta lista para Envio de Resolucion');             
            END IF;
       END IF;
      END;
    END LOOP;
    
    FOR I IN (SELECT  N.ID, 
                      N.ID_DECLARACION, 
                      N.ESTADO, 
                      N.ID_PAIS, 
                      N.FECHAFINAL, 
                      N.OBSERVACIONNOTIFICACION,
                      N.ID_USUARIO,
                      N.ID_PUNTOATENCION,
                      N.ID_DIRECCIONTERRITORIAL,
                      N.TIPOCODIGOACTO,
                      CO.NUMERO                      
              FROM TBNOTIFICACION N
              INNER JOIN TBDECLARACIONES D on d.id = n.id_declaracion
              inner join (SELECT ID_DECLARACION, MAX(ID) AS ID_VALORACION FROM TBVALORACION
                          GROUP BY ID_DECLARACION) VAL ON VAL.ID_DECLARACION = D.ID
              inner join tbcodigoorfeo co on co.NIDVALORACION = VAL.ID_VALORACION     
              WHERE N.ESTADO = ENTR_EdictoPublicado AND 
                    FECHAFINAL IS NOT NULL AND
                    TRUNC(CURRENT_DATE, 'DD') <= TRUNC(FECHAFINAL, 'DD')) LOOP
      DECLARE
          listaFuncionarios NUMBERARRAY;
      BEGIN
          -- Obtener la lista de funcionarios para la notificacion (encargados de la DT o el PA asociado a la notificacion
          SELECT ID_ENCARGADO BULK COLLECT INTO listaFuncionarios
          FROM (SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_PUNTOATENCION IS NOT NULL AND ID_PUNTOATENCION = I.ID_PUNTOATENCION
                UNION
                SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_DIRECCIONTERRITORIAL IS NOT NULL AND ID_DIRECCIONTERRITORIAL = I.ID_DIRECCIONTERRITORIAL);
          /* Vencimiento de t?rminos PLAN B (al día 8 se notifica que el plazo esta a punto de vencer */
          IF TRUNC(CURRENT_DATE, 'DD') = TRUNC(I.FECHAFINAL, 'DD') - 2 THEN
            -- Alerte a Encargado
            PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'NotificaciÃ³n pendiente de publicaciÃ³n edicto', 
                                                                                                            'Señor Usuario, la notificaciÃ³n con el cÃ³digo orfeo ' || I.NUMERO ||
                                                                                                            ' esta lista para publicar edicto');
          END IF;
      
          /* Vencimiento de t?rminos PLAN B (al d?a 10 se cambia el estado a pendiente despublicacion */
          IF TRUNC(CURRENT_DATE, 'DD') >= TRUNC(I.FECHAFINAL, 'DD') THEN
            UPDATE TBNOTIFICACION SET ESTADO = ENTR_PendienteDespublicacion WHERE ID = I.ID;
            -- Alerte a LN y Encargado
            PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, lideresNotificacion, 'NotificaciÃ³n pendiente de desfijaciÃ³n edicto', 
                                                                              'Señor Usuario, la notificaciÃ³n con el cÃ³digo orfeo ' || I.NUMERO ||
                                                                              ' esta lista para desfijar edicto');
            PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'NotificaciÃ³n pendiente de desfijaciÃ³n edicto', 
                                                                              'Señor Usuario, la notificaciÃ³n con el cÃ³digo orfeo ' || I.NUMERO ||
                                                                              ' esta lista para desfijar edicto');
          END IF;
      END;
    END LOOP;
    
    -- Iterar las notificaciones que cumplan con la condicion (Plan A)
    FOR I IN (SELECT N.ID, N.FECHAFINAL, N.ID_PUNTOATENCION, N.ID_DIRECCIONTERRITORIAL, N.ID_USUARIO, CO.NUMERO
              FROM TBNOTIFICACION N
              INNER JOIN TBDECLARACIONES D on d.id = n.id_declaracion
              inner join (SELECT ID_DECLARACION, MAX(ID) AS ID_VALORACION FROM TBVALORACION
                          GROUP BY ID_DECLARACION) VAL ON VAL.ID_DECLARACION = D.ID
              inner join tbcodigoorfeo co on co.NIDVALORACION = VAL.ID_VALORACION              
              WHERE N.ESTADO = COUR_NotificacionEntregada
                AND N.FECHAFINAL is not null
                AND (TRUNC(N.FECHAFINAL, 'DD') - 1 = TRUNC(SYSDATE, 'DD') OR TRUNC(N.FECHAFINAL, 'DD') - 2 = TRUNC(SYSDATE, 'DD'))) 
    LOOP
      DECLARE
        listaFuncionarios NUMBERARRAY;
      BEGIN
        -- Obtener la lista de funcionarios para la notificacion (encargados de la DT o el PA asociado a la notificacion
        SELECT ID_ENCARGADO BULK COLLECT INTO listaFuncionarios
        FROM (SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_PUNTOATENCION IS NOT NULL AND ID_PUNTOATENCION = I.ID_PUNTOATENCION
              UNION
              SELECT ID_ENCARGADO FROM TBENCARGADOENTIDAD WHERE ID_DIRECCIONTERRITORIAL IS NOT NULL AND ID_DIRECCIONTERRITORIAL = I.ID_DIRECCIONTERRITORIAL);
          
        IF TRUNC(I.FECHAFINAL, 'DD') - 2 = TRUNC(SYSDATE, 'DD') THEN
          PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'Notificacion pendiente por revisión', 
                                                                          'Senior Usuario, la notificacion con el codigo orfeo ' || I.NUMERO ||
                                                                          ' esta lista para publicar edicto, llamado de 3 dias');
        ELSIF TRUNC(I.FECHAFINAL, 'DD') - 1 = TRUNC(SYSDATE, 'DD') THEN
          -- Notifico al punto de atencion y al lider de notificaciones
          PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, lideresNotificacion, 'Notificación pendiente por revisión', 
                                                                          'Senior Usuario, la notificaciÃ³n con el cÃ³digo orfeo ' || I.NUMERO ||
                                                                          ' esta lista para publicar edicto, llamado de 4 dÃ­as');
          PKG_NOTIFICACIONINTERNA.SP_GENERANOTIFICACIONMULTIPLE(I.ID_USUARIO, I.ID, 0, listaFuncionarios, 'NotificaciÃ³n pendiente por revisión', 
                                                                          'Senior Usuario, la notificaci?n con el cÃ³digo orfeo ' || I.NUMERO ||
                                                                          ' esta lista para publicar edicto, llamado de 4 dÃ­as');
        END IF;
      END;
    END LOOP;
    
    COMMIT;
  END SP_PROCESARNOTIFICACIONES;
  
  PROCEDURE sp_confirmaEnvionNotificacion(PO_IDPAQUETENOTIFICA IN TBNOTIFICACION.ID_PAQUETENOTIFICACION%TYPE) 
  AS
  BEGIN
    UPDATE TBNOTIFICACION
    SET ESTADO  = PREP_Enviado
    WHERE ID_PAQUETENOTIFICACION = PO_IDPAQUETENOTIFICA;
  END;
  
 /*------------------------------------------------------------------
  Purpose : Asocia codigos de guia a las notificaciones de un paquete
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_ASOCIARCODIGUIANOTIFICACION (PI_NID IN NUMBER,
                                           PI_IDCODIGOGUIA IN VARCHAR2) IS

 BEGIN
  UPDATE TBNOTIFICACION SET IDCODIGOGUIA = PI_IDCODIGOGUIA
  WHERE ID = PI_NID;
 END;

 /*------------------------------------------------------------------
  Purpose : Consulta las notificaciones que tienen asociadas los puntos de atencion(DireccionTerritorial y PuntoNotifica)
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_CONSULTACENTROATENCIONOTIF (  pi_IdPais         IN NUMBER DEFAULT NULL
                                          , pi_IdDepartamento IN NUMBER DEFAULT NULL
                                          , pi_IdMunicipio    IN NUMBER DEFAULT NULL
                                          , pi_PageNumber     IN NUMBER
                                          , pi_PageSize       IN NUMBER
                                          , PO_CURSOR         OUT CURSOR_TYPE) IS
  startRow NUMBER;
  endRow   NUMBER;
 BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow   := (pi_PageNumber * pi_PageSize) + 1;
  OPEN PO_CURSOR FOR
   SELECT *
      FROM (
            SELECT DATOS.*,ROWNUM AS R
            FROM (SELECT P.ID AS IDCENTRO
                        ,1 as tipo
                        ,COUNT(N.ID) AS CANTIDADASIGNADA
                        ,P.NOMBRE AS NOMBRE
                        ,M.ID AS ID_MUNICIPIO
                        ,M.NOMBRE AS MUNICIPIO 
                        ,D.ID AS ID_DEPTO
                        ,D.NOMBRE AS DEPARTAMENTO
                        ,PA.ID AS ID_PAIS
                        ,PA.NOMBRE AS PAIS
                  FROM TBPUNTOATENCION P
                  INNER JOIN TBGEOGRAFIA M ON M.ID = P.IDMUNICIPIO
                  INNER JOIN TBGEOGRAFIA D ON D.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = P.IDMUNICIPIO)
                  INNER JOIN TBGEOGRAFIA PA ON PA.ID = (SELECT DPT.PADREID FROM TBGEOGRAFIA DPT WHERE DPT.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = P.IDMUNICIPIO))
                  LEFT JOIN TBNOTIFICACION N ON N.ID_PUNTOATENCION = P.ID
                  GROUP BY P.ID, P.NOMBRE, M.NOMBRE, D.NOMBRE, PA.NOMBRE, M.ID, D.ID, PA.ID
                  UNION ALL
                  SELECT DT.ID AS IDCENTRO
                        ,2 as tipo
                        ,COUNT(N.ID) AS CANTIDADASIGNADA
                        ,DT.NOMBRE AS NOMBRE
                        ,M.ID AS ID_MUNICIPIO
                        ,M.NOMBRE AS MUNICIPIO 
                        ,D.ID AS ID_DEPTO
                        ,D.NOMBRE AS DEPARTAMENTO
                        ,PA.ID AS ID_PAIS
                        ,PA.NOMBRE AS PAIS
                  FROM TBDIRECCIONTERRITORIAL DT 
                  INNER JOIN TBGEOGRAFIA M ON M.ID = DT.IDMUNICIPIO
                  INNER JOIN TBGEOGRAFIA D ON D.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = DT.IDMUNICIPIO)
                  INNER JOIN TBGEOGRAFIA PA ON PA.ID = (SELECT DPT.PADREID FROM TBGEOGRAFIA DPT WHERE DPT.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = DT.IDMUNICIPIO))
                  LEFT JOIN TBNOTIFICACION N ON N.ID_DIRECCIONTERRITORIAL = DT.ID
                  GROUP BY DT.ID, DT.NOMBRE, M.NOMBRE, D.NOMBRE, PA.NOMBRE, M.ID, D.ID, PA.ID) DATOS
            WHERE DATOS.ID_PAIS = CASE WHEN pi_IdPais IS NULL THEN DATOS.ID_PAIS ELSE pi_IdPais END AND
                  DATOS.ID_DEPTO = CASE WHEN pi_IdDepartamento IS NULL THEN DATOS.ID_DEPTO ELSE pi_IdDepartamento END AND
                  DATOS.ID_MUNICIPIO = CASE WHEN pi_IdMunicipio IS NULL THEN DATOS.ID_MUNICIPIO ELSE pi_IdMunicipio END AND
                  ROWNUM < endRow)
      WHERE R >= startRow;
 END;
  /*------------------------------------------------------------------
  Purpose : Consulta la cantidad de registros que trae el procedimiento SP_CONSULTACENTROATENCIONOTIF
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_CONSULTACENTROATENCIONCOUNT(  pi_IdPais         IN NUMBER DEFAULT NULL
                                          , pi_IdDepartamento IN NUMBER DEFAULT NULL
                                          , pi_IdMunicipio    IN NUMBER DEFAULT NULL
                                          , PO_RECORDCOUNT OUT NUMBER) IS
 BEGIN
   SELECT COUNT(*) INTO PO_RECORDCOUNT FROM(SELECT IDCENTRO,CANTIDADASIGNADA,NOMBRE,MUNICIPIO,DEPARTAMENTO FROM
    (SELECT P.ID AS IDCENTRO
                        ,1 as tipo
                        ,COUNT(N.ID) AS CANTIDADASIGNADA
                        ,P.NOMBRE AS NOMBRE
                        ,M.ID AS ID_MUNICIPIO
                        ,M.NOMBRE AS MUNICIPIO 
                        ,D.ID AS ID_DEPTO
                        ,D.NOMBRE AS DEPARTAMENTO
                        ,PA.ID AS ID_PAIS
                        ,PA.NOMBRE AS PAIS
                  FROM TBPUNTOATENCION P
                  INNER JOIN TBGEOGRAFIA M ON M.ID = P.IDMUNICIPIO
                  INNER JOIN TBGEOGRAFIA D ON D.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = P.IDMUNICIPIO)
                  INNER JOIN TBGEOGRAFIA PA ON PA.ID = (SELECT DPT.PADREID FROM TBGEOGRAFIA DPT WHERE DPT.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = P.IDMUNICIPIO))
                  LEFT JOIN TBNOTIFICACION N ON N.ID_PUNTOATENCION = P.ID
                  GROUP BY P.ID, P.NOMBRE, M.NOMBRE, D.NOMBRE, PA.NOMBRE, M.ID, D.ID, PA.ID
                  UNION ALL
                  SELECT DT.ID AS IDCENTRO
                        ,2 as tipo
                        ,COUNT(N.ID) AS CANTIDADASIGNADA
                        ,DT.NOMBRE AS NOMBRE
                        ,M.ID AS ID_MUNICIPIO
                        ,M.NOMBRE AS MUNICIPIO 
                        ,D.ID AS ID_DEPTO
                        ,D.NOMBRE AS DEPARTAMENTO
                        ,PA.ID AS ID_PAIS
                        ,PA.NOMBRE AS PAIS
                  FROM TBDIRECCIONTERRITORIAL DT 
                  INNER JOIN TBGEOGRAFIA M ON M.ID = DT.IDMUNICIPIO
                  INNER JOIN TBGEOGRAFIA D ON D.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = DT.IDMUNICIPIO)
                  INNER JOIN TBGEOGRAFIA PA ON PA.ID = (SELECT DPT.PADREID FROM TBGEOGRAFIA DPT WHERE DPT.ID = (SELECT MU.PADREID FROM TBGEOGRAFIA MU WHERE MU.ID = DT.IDMUNICIPIO))
                  LEFT JOIN TBNOTIFICACION N ON N.ID_DIRECCIONTERRITORIAL = DT.ID
                  GROUP BY DT.ID, DT.NOMBRE, M.NOMBRE, D.NOMBRE, PA.NOMBRE, M.ID, D.ID, PA.ID) DATOS
            WHERE DATOS.ID_PAIS = CASE WHEN pi_IdPais IS NULL THEN DATOS.ID_PAIS ELSE pi_IdPais END AND
                  DATOS.ID_DEPTO = CASE WHEN pi_IdDepartamento IS NULL THEN DATOS.ID_DEPTO ELSE pi_IdDepartamento END AND
                  DATOS.ID_MUNICIPIO = CASE WHEN pi_IdMunicipio IS NULL THEN DATOS.ID_MUNICIPIO ELSE pi_IdMunicipio END);
 END;
 /*------------------------------------------------------------------
  Purpose : Consulta en detalle las notificaciones que tienen asociadas los puntos de atencion(DireccionTerritorial y PuntoNotifica)
  Author  : John Henao
  Date    : 24/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE sp_DetalleCentroAtencion(  PI_IDCENTROATENCION     IN NUMBER
                                  , PI_TipoCentro            IN NUMBER
                                  , pi_PageNumber           IN NUMBER
                                  , pi_PageSize             IN NUMBER
                                  , PO_CURSOR OUT CURSOR_TYPE) IS
startRow NUMBER;
  endRow   NUMBER;
 BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow   := (pi_PageNumber * pi_PageSize) + 1;
  
   IF PI_TipoCentro = 1 THEN
       OPEN PO_CURSOR FOR
           SELECT *
            FROM (SELECT DATOS.*,ROWNUM AS R
                   FROM (SELECT E.NOMBRE
                              , N.DIRECCIONNOTIFICACION
                              , N.TELEFONONOTIFICACION
                              , N.ESTADOCOURIER
                              , N.FECHAFINAL
                              , N.IDCODIGOGUIA 
                         FROM TBNOTIFICACION N
                         INNER JOIN TBESTADOSNOTIFICACION  E  ON E.ID = N.ESTADO
                         LEFT  JOIN TBPUNTOATENCION        PA ON N.ID_PUNTOATENCION = PA.ID
                         LEFT  JOIN TBDIRECCIONTERRITORIAL DT ON DT.ID = N.ID_DIRECCIONTERRITORIAL
                         WHERE N.ID_PUNTOATENCION = PI_IDCENTROATENCION
                        )DATOS
             WHERE ROWNUM < endRow)
        WHERE R >= startRow;
   ELSE 
    OPEN PO_CURSOR FOR
      SELECT *
            FROM (SELECT DATOS.*,ROWNUM AS R
                   FROM (SELECT E.NOMBRE
                              , N.DIRECCIONNOTIFICACION
                              , N.TELEFONONOTIFICACION
                              , N.ESTADOCOURIER
                              , N.FECHAFINAL
                              , N.IDCODIGOGUIA 
                         FROM TBNOTIFICACION N
                         INNER JOIN TBESTADOSNOTIFICACION  E  ON E.ID = N.ESTADO
                         LEFT  JOIN TBPUNTOATENCION        PA ON N.ID_PUNTOATENCION = PA.ID
                         LEFT  JOIN TBDIRECCIONTERRITORIAL DT ON DT.ID = N.ID_DIRECCIONTERRITORIAL
                         WHERE N.ID_DIRECCIONTERRITORIAL = PI_IDCENTROATENCION
                        )DATOS
             WHERE ROWNUM < endRow)
        WHERE R >= startRow;
   END IF;
 END;

 /*------------------------------------------------------------------
  Purpose : Consulta la cantidad de registros que trae el procedimiento SP_CONSULTACENTROATENCIONOTIF
  Author  : John Henao
  Date    : 21/06/2013
 ------------------------------------------------------------------
 */
 PROCEDURE SP_DetalleCentroAtencionCOUNT(PI_IDCENTROATENCION IN NUMBER
                                         ,PI_TipoCentro      IN NUMBER
                                         ,PO_RECORDCOUNT OUT NUMBER) IS
 BEGIN
   IF PI_TIPOCENTRO = 1 THEN
     SELECT COUNT(*) INTO PO_RECORDCOUNT FROM(SELECT E.NOMBRE
                            , N.DIRECCIONNOTIFICACION
                            , N.TELEFONONOTIFICACION
                            , N.ESTADOCOURIER
                            , N.FECHAFINAL
                            , N.IDCODIGOGUIA 
                       FROM TBNOTIFICACION N
                       INNER JOIN TBESTADOSNOTIFICACION  E  ON E.ID = N.ESTADO
                       LEFT  JOIN TBPUNTOATENCION        PA ON N.ID_PUNTOATENCION = PA.ID
                       LEFT  JOIN TBDIRECCIONTERRITORIAL DT ON DT.ID = N.ID_DIRECCIONTERRITORIAL
                       WHERE N.ID_PUNTOATENCION = PI_IDCENTROATENCION 
        )DATOS; 
   ELSE
      SELECT COUNT(*) INTO PO_RECORDCOUNT FROM(SELECT E.NOMBRE
                            , N.DIRECCIONNOTIFICACION
                            , N.TELEFONONOTIFICACION
                            , N.ESTADOCOURIER
                            , N.FECHAFINAL
                            , N.IDCODIGOGUIA 
                       FROM TBNOTIFICACION N
                       INNER JOIN TBESTADOSNOTIFICACION  E  ON E.ID = N.ESTADO
                       LEFT  JOIN TBPUNTOATENCION        PA ON N.ID_PUNTOATENCION = PA.ID
                       LEFT  JOIN TBDIRECCIONTERRITORIAL DT ON DT.ID = N.ID_DIRECCIONTERRITORIAL
                       WHERE N.ID_DIRECCIONTERRITORIAL = PI_IDCENTROATENCION
                       
        )DATOS; 
   END IF;
 END;
  /*------------------------------------------------------------------
   Purpose : Guarda el registro de trazabilidad para TBNOTIFICACION
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_AgregarHistorico(pi_IdNotificacion IN TBNOTIFICACION.ID%TYPE) IS
  BEGIN
    INSERT INTO TBHISTORICONOTIFICACION (ID
                                       , ID_NOTIFICACION
                                       , ID_DECLARACION
                                       , ID_PAIS
                                       , ID_DEPARTAMENTO
                                       , ID_MUNICIPIO
                                       , DIRECCIONNOTIFICACION
                                       , TELEFONONOTIFICACION
                                       , ID_PAQUETENOTIFICACION
                                       , ID_USUARIO
                                       , ESTADO
                                       , ESTADOCOURIER
                                       , FECHAESTADOCOURIER
                                       , FECHAFINAL
                                       , OBSERVACIONNOTIFICACION
                                       , ID_PUNTOATENCION
                                       , ID_DIRECCIONTERRITORIAL
                                       , APROBADO
                                       , IDCODIGOGUIA)
    SELECT SEQ_TBHISTORICONOTIFICACION.NextVal
         , ID
         , ID_DECLARACION
         , ID_PAIS
         , ID_DEPARTAMENTO
         , ID_MUNICIPIO
         , DIRECCIONNOTIFICACION
         , TELEFONONOTIFICACION
         , ID_PAQUETENOTIFICACION
         , ID_USUARIO
         , ESTADO
         , ESTADOCOURIER
         , FECHAESTADOCOURIER
         , FECHAFINAL
         , OBSERVACIONNOTIFICACION
         , ID_PUNTOATENCION
         , ID_DIRECCIONTERRITORIAL
         , APROBADO
         , IDCODIGOGUIA
    FROM TBNOTIFICACION WHERE ID = pi_IdNotificacion;
  END;
  
  /*------------------------------------------------------------------
   Purpose : Obtiene los registros historicos para una notificacion
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_ObtenerHistorico(pi_IdNotificacion IN TBNOTIFICACION.ID%TYPE, po_Resultado OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Resultado FOR
      SELECT HST.ID                      AS ID
           , HST.ID_NOTIFICACION         AS ID_NOTIFICACION
           , HST.ID_PAIS                 AS ID_PAIS
           , PAI.NOMBRE                  AS PAIS
           , HST.ID_DEPARTAMENTO         AS ID_DEPARTAMENTO
           , DPT.NOMBRE                  AS DEPARTAMENTO
           , HST.ID_MUNICIPIO            AS ID_MUNICIPIO
           , MCP.NOMBRE                  AS MUNICIPIO
           , HST.DIRECCIONNOTIFICACION   AS DIRECCIONNOTIFICACION
           , HST.TELEFONONOTIFICACION    AS TELEFONONOTIFICACION
           , HST.ID_PAQUETENOTIFICACION  AS ID_PAQUETENOTIFICACION
           , PAQ.ORDENSERVICIO           AS ORDENSERVICIO
           , HST.ID_USUARIO              AS ID_USUARIO
           , USR.USUARIO                 AS USUARIO
           , EST.NOMBRE                  AS ESTADO
           , HST.ESTADOCOURIER           AS ESTADOCOURIER
           , HST.FECHAESTADOCOURIER      AS FECHAESTADOCOURIER
           , HST.FECHAFINAL              AS FECHAFINAL
           , HST.OBSERVACIONNOTIFICACION AS OBSERVACIONNOTIFICACION
           , HST.ID_PUNTOATENCION        AS ID_PUNTOATENCION
           , PAT.NOMBRE                  AS PUNTOATENCION
           , HST.ID_DIRECCIONTERRITORIAL AS ID_DIRECCIONTERRITORIAL
           , DTR.NOMBRE                  AS DIRECCIONTERRITORIAL
           , HST.APROBADO                AS APROBADO
           , HST.IDCODIGOGUIA            AS IDCODIGOGUIA
           , HST.FECHAMODIFICACION       AS FECHAMODIFICACION
      FROM TBHISTORICONOTIFICACION HST
      LEFT JOIN TBGEOGRAFIA            PAI ON PAI.ID = HST.ID_PAIS
      LEFT JOIN TBGEOGRAFIA            DPT ON DPT.ID = HST.ID_DEPARTAMENTO
      LEFT JOIN TBGEOGRAFIA            MCP ON MCP.ID = HST.ID_MUNICIPIO
      LEFT JOIN TBPAQUETENOTIFICACION  PAQ ON PAQ.ID = HST.ID_PAQUETENOTIFICACION
      LEFT JOIN TBUSUARIOS             USR ON USR.ID = HST.ID_USUARIO
      LEFT JOIN TBESTADOSNOTIFICACION  EST ON EST.ID = HST.ESTADO
      LEFT JOIN TBPUNTOATENCION        PAT ON PAT.ID = HST.ID_PUNTOATENCION
      LEFT JOIN TBDIRECCIONTERRITORIAL DTR ON DTR.ID = HST.ID_DIRECCIONTERRITORIAL
      WHERE HST.ID_NOTIFICACION = pi_IdNotificacion
      ORDER BY HST.FECHAMODIFICACION DESC;
  END;
  
  /*------------------------------------------------------------------
   Purpose : Obtiene los registros historicos para un paquete de notificaciones
   Author  : 
   Date    : 
  ------------------------------------------------------------------
  */
  PROCEDURE sp_ObtenerHistoricoPaquete(pi_IdPaqueteNotificacion IN TBPAQUETENOTIFICACION.ID%TYPE, po_Resultado OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Resultado FOR
      SELECT HST.ID                      AS ID
           , HST.ID_NOTIFICACION         AS ID_NOTIFICACION
           , HST.ID_PAIS                 AS ID_PAIS
           , HST.ID_DEPARTAMENTO         AS ID_DEPARTAMENTO
           , HST.ID_MUNICIPIO            AS ID_MUNICIPIO
           , HST.DIRECCIONNOTIFICACION   AS DIRECCIONNOTIFICACION
           , HST.TELEFONONOTIFICACION    AS TELEFONONOTIFICACION
           , HST.ID_PAQUETENOTIFICACION  AS ID_PAQUETENOTIFICACION
           , PAQ.ORDENSERVICIO           AS ORDENSERVICIO
           , HST.ID_USUARIO              AS ID_USUARIO
           , USR.USUARIO                 AS USUARIO
           , EST.NOMBRE                  AS ESTADO
           , HST.ESTADOCOURIER           AS ESTADOCOURIER
           , HST.FECHAESTADOCOURIER      AS FECHAESTADOCOURIER
           , HST.FECHAFINAL              AS FECHAFINAL
           , HST.OBSERVACIONNOTIFICACION AS OBSERVACIONNOTIFICACION
           , HST.ID_PUNTOATENCION        AS ID_PUNTOATENCION
           , PAT.NOMBRE                  AS PUNTOATENCION
           , HST.ID_DIRECCIONTERRITORIAL AS ID_DIRECCIONTERRITORIAL
           , DTR.NOMBRE                  AS DIRECCIONTERRITORIAL
           , HST.APROBADO                AS APROBADO
           , HST.IDCODIGOGUIA            AS IDCODIGOGUIA
           , HST.FECHAMODIFICACION       AS FECHAMODIFICACION
      FROM TBNOTIFICACION NTF
      INNER JOIN TBHISTORICONOTIFICACION HST ON HST.ID_NOTIFICACION = NTF.ID
      LEFT  JOIN TBPAQUETENOTIFICACION   PAQ ON PAQ.ID = NTF.ID_PAQUETENOTIFICACION
      LEFT  JOIN TBUSUARIOS              USR ON USR.ID = HST.ID_USUARIO
      LEFT  JOIN TBESTADOSNOTIFICACION   EST ON EST.ID = HST.ESTADO
      LEFT  JOIN TBPUNTOATENCION         PAT ON PAT.ID = HST.ID_PUNTOATENCION
      LEFT  JOIN TBDIRECCIONTERRITORIAL  DTR ON DTR.ID = HST.ID_DIRECCIONTERRITORIAL
      WHERE NTF.ID_PAQUETENOTIFICACION = pi_IdPaqueteNotificacion
      ORDER BY HST.FECHAMODIFICACION DESC;
  END;
  /*------------------------------------------------------------------
   Purpose : verifica y obtiene si una notificacion es ley nueva o ley vieja
   Author  : John Henao
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
 PROCEDURE SP_NOTIFICACIONLEYNUEVAOVIEJA(PI_IDNOTIFICACION IN NUMBER
                                         ,PO_RECORDCOUNT OUT NUMBER) IS

 BEGIN
  SELECT N.TIPOCODIGOACTO INTO PO_RECORDCOUNT FROM TBNOTIFICACION N 
   WHERE N.ID = PI_IDNOTIFICACION;
 END;
 
  /*------------------------------------------------------------------
   Purpose : Retorna los encargados que tiene una entidad
   Author  : Ivan Camilo Suarez
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_GETENCARGADOSPORENTIDAD(  PI_IDCENTROATENCION IN NUMBER
                                       , PI_TipoCentro       IN NUMBER
                                       , pi_PageNumber       IN NUMBER
                                       , pi_PageSize         IN NUMBER
                                       , po_Cursor           OUT CURSOR_TYPE) IS
  startRow NUMBER;
  endRow   NUMBER;
 BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow   := (pi_PageNumber * pi_PageSize) + 1;
    IF PI_TIPOCENTRO = 1 THEN
      OPEN po_Cursor FOR
      SELECT *
      FROM (SELECT DATOS.*,ROWNUM AS R
            FROM (SELECT ENC.ID
                       , ENC.NOMBRE    AS NOMBRE
                       , ENC.CARGO     AS CARGO
                       , ENC.DIRECCION AS DIRECCION
                       , ENC.TELEFONO  AS TELEFONO
                  FROM TBENCARGADOENTIDAD EENT
                  LEFT OUTER JOIN TBUSUARIOS ENC ON EENT.ID_ENCARGADO = ENC.ID
                  WHERE EENT.ID_PUNTOATENCION IS NOT NULL AND EENT.ID_PUNTOATENCION = PI_IDCENTROATENCION) DATOS
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
    ELSIF PI_TIPOCENTRO = 2 THEN
      OPEN po_Cursor FOR
      SELECT *
      FROM (SELECT DATOS.*,ROWNUM AS R
            FROM (SELECT ENC.ID
                       , ENC.NOMBRE    AS NOMBRE
                       , ENC.CARGO     AS CARGO
                       , ENC.DIRECCION AS DIRECCION
                       , ENC.TELEFONO  AS TELEFONO
                  FROM TBENCARGADOENTIDAD EENT
                  LEFT OUTER JOIN TBUSUARIOS ENC ON EENT.ID_ENCARGADO = ENC.ID
                  WHERE EENT.ID_DIRECCIONTERRITORIAL IS NOT NULL AND EENT.ID_DIRECCIONTERRITORIAL = PI_IDCENTROATENCION) DATOS
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
    END IF;
  END; 
  
  /*------------------------------------------------------------------
   Purpose : Consulta la cantidad de registros que trae el procedimiento SP_GETENCARGADOSPORENTIDAD
   Author  : Ivan Camilo Suarez
   Date    : 26/06/2013
  ------------------------------------------------------------------
  */
 PROCEDURE SP_ENCARGADOSPORENTIDADCOUNT( PI_IDCENTROATENCION IN NUMBER
                                        ,PI_TipoCentro       IN NUMBER
                                        ,PO_RECORDCOUNT      OUT NUMBER) IS
 BEGIN
  IF PI_TIPOCENTRO = 1 THEN
     SELECT COUNT(*) INTO PO_RECORDCOUNT 
     FROM(SELECT *
          FROM TBENCARGADOENTIDAD EENT
          LEFT OUTER JOIN TBUSUARIOS ENC ON EENT.ID_ENCARGADO = ENC.ID
          WHERE EENT.ID_PUNTOATENCION IS NOT NULL AND EENT.ID_PUNTOATENCION = PI_IDCENTROATENCION); 
  ELSIF PI_TIPOCENTRO = 2 THEN
     SELECT COUNT(*) INTO PO_RECORDCOUNT 
     FROM(SELECT *
          FROM TBENCARGADOENTIDAD EENT
          LEFT OUTER JOIN TBUSUARIOS ENC ON EENT.ID_ENCARGADO = ENC.ID
          WHERE EENT.ID_DIRECCIONTERRITORIAL IS NOT NULL AND EENT.ID_DIRECCIONTERRITORIAL = PI_IDCENTROATENCION); 
  END IF;
 END;
 
 /*------------------------------------------------------------------
   Purpose : Retorna los estados de notificacion
   Author  : Ivan Camilo Suarez
   Date    : 06/07/2013
  ------------------------------------------------------------------
  */
  PROCEDURE SP_GETESTADOSDENOTIFICACION(po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID, NOMBRE FROM tbestadosnotificacion;
  END;
 
  
END PKG_NOTIFICACION;
/