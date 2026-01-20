create or replace PACKAGE PKG_ACTOSADMIN AS

  TYPE CURSOR_TYPE IS REF CURSOR;
  HECHO_VICTIMIZANTE NUMBER := 2137;
  
  --ESTADOS ACTOS ADMINISTRATIVOS
  GENERADO  NUMBER  := 2;
  APROBADO  NUMBER  := 3;
  FIRMADO   NUMBER  := 4;
  RECHAZADO NUMBER  := 5;

  PROCEDURE sp_getActosAdminPaginado(p_FilaInicial NUMBER, p_FilaFinal NUMBER, p_Orden VARCHAR2, p_Result OUT SYS_REFCURSOR);

  PROCEDURE sp_getActosAdminFiltro(p_Filtro VARCHAR2, p_Valor VARCHAR2, p_Result OUT SYS_REFCURSOR);

  PROCEDURE sp_getActosAdminCantidad(p_Cantidad OUT NUMBER);

  PROCEDURE sp_getDocumentosPorArea(p_Id_Area NUMBER, p_Result OUT SYS_REFCURSOR);

  PROCEDURE sp_getCantidadFormulario(p_Formulario VARCHAR2, p_Cantidad OUT NUMBER);

  PROCEDURE sp_setActoAdministrativo(p_Documento NUMBER, p_Num_Interno VARCHAR2, p_Formulario VARCHAR2, p_Descripcion VARCHAR2, p_Dirigido VARCHAR2, p_Usuario NUMBER, p_Estado NUMBER, p_Id OUT NUMBER, p_Consecutivo OUT VARCHAR2);
  
  PROCEDURE sp_ActualizarTipoCodigo(pi_IdActoAdministrativo IN NUMBER, pi_TipoCodigo IN NUMBER);

  PROCEDURE sp_updActoAdministrativo(p_Documento NUMBER, p_Num_Interno VARCHAR2, p_Formulario VARCHAR2, p_Descripcion VARCHAR2, p_Dirigido VARCHAR2, p_Usuario NUMBER, p_Estado NUMBER, p_Id NUMBER, p_Consecutivo OUT VARCHAR2);
  
  PROCEDURE sp_GetActoAdminPorId(p_Id NUMBER, p_Result OUT SYS_REFCURSOR);

  PROCEDURE sp_Notificacion(pi_IdValoracion IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE sp_EstablecerActoAdmin(pi_IdValoracion IN NUMBER);
                         
PROCEDURE  sp_setActoAdministrativoRUV
(
  PI_IDDECLARACION NUMBER,
  P_DOCUMENTO      NUMBER,
  P_NUM_INTERNO    VARCHAR2,
  P_FORMULARIO     VARCHAR2,
  P_DESCRIPCION    VARCHAR2,
  P_DIRIGIDO       VARCHAR2,
  P_USUARIO        NUMBER,
  P_ESTADO         NUMBER,
  PI_TIPOACTOADMIN NUMBER,
  P_Id             OUT NUMBER,
  P_CONSECUTIVO    OUT VARCHAR2
);                         

/*	DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
**	(NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
**	AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
**	FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
**	CAMBIOS:
**		20130614 - JAIRO VALDERRAMA
**		1. SE ACTUALIZA EL CAMPO 'DFIRMA' CON LA FECHA DEL SISTEMA CUANDO EL
**    ESTADO A ACTUALIZAR ES 'FIRMADO'
*/
PROCEDURE SP_ACTESTADOACTOADMIN (PI_IDACTOADMIN IN NUMBER,
                                 PI_ESTADOACTOADMIN IN NUMBER,
                                 PI_IDUSUARIO IN NUMBER                                                              
                                );
                                
PROCEDURE SP_GETIDVALORACIONBYIDDEC(
                                    PI_IDDECLARACION IN NUMBER,
                                    PO_IDVALORACION OUT NUMBER
                                   );

FUNCTION f_getPrincipiosPorTipo
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPO IN NUMBER
)RETURN VARCHAR2;

FUNCTION f_getHechosPorTipo
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPO in number
)RETURN VARCHAR2;

FUNCTION F_GETHECHOSADDVALORDECLAR
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPOAGREGADOHECHO IN NUMBER
)RETURN VARCHAR2;

  FUNCTION f_GetUbicacionPuntoDeclaracion(pi_IdMunicipio IN NUMBER, pi_IdDepartamento IN NUMBER) RETURN VARCHAR2;
  
  FUNCTION f_GetEntidadTotalDeclaracion(pi_NombreEntidad VARCHAR2, pi_CodigoMunicipio VARCHAR2, pi_NombreDepartamento VARCHAR2, pi_NombreMunicipio VARCHAR2) RETURN VARCHAR2;

  FUNCTION f_EvaluarDireccion(pi_Direccion VARCHAR2) RETURN NUMBER;
  
  FUNCTION ObtenerNomPersoneria(pi_IdMunicipio IN NUMBER) RETURN VARCHAR2;
  
  FUNCTION ObtenerDirPersoneria(pi_IdMunicipio IN NUMBER) RETURN VARCHAR2;
  
  FUNCTION F_PROPEREXARTICULOS(PI_TEXTO IN VARCHAR2) RETURN VARCHAR2;

END PKG_ACTOSADMIN;
/

-- Create package body
create or replace PACKAGE BODY PKG_ACTOSADMIN AS

  PROCEDURE sp_getActosAdminPaginado(p_FilaInicial NUMBER, p_FilaFinal NUMBER, p_Orden VARCHAR2, p_Result OUT SYS_REFCURSOR) IS
    V_Query          VARCHAR2(4000);
    V_Final          NUMBER;
    V_Orden          VARCHAR2(100);
  BEGIN
    V_Final := P_FilaInicial + P_FilaFinal;

    IF P_Orden IS NULL THEN
      V_Orden := 'ad.ID';
    ELSE
      V_Orden := P_Orden;
    END IF;

    V_Query := '
    SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY '|| V_Orden ||') FILA,
           ad.id,
           ad.consecutivo,
           ad.fecha,
           param.nombre Documento,
           u.nombre Solicitante,
           d.numeroformulario NroFormulario,
           ad.id_usuario UsuarioId,
           u.nombre Usuario,
           ad.dirigido Dirigido,
           pestado.nombre Estado
    FROM   tbacto_administrativo ad
           INNER JOIN tbparametros param ON param."ID" = ad.param_documento
           INNER JOIN tbparametros pestado ON pestado."ID" = ad.param_estado
           INNER JOIN tbusuarios u ON u."ID" = ad.id_usuario
           LEFT JOIN tbdeclaraciones d ON d."ID" = ad.id_declaracion
    ) WHERE FILA BETWEEN '|| P_FilaInicial ||' AND '|| V_Final|| '';

    DBMS_OUTPUT.PUT_LINE(V_QUERY);
    OPEN P_Result FOR V_Query;
    
  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error');
  END;

  PROCEDURE sp_getActosAdminFiltro(p_Filtro VARCHAR2, p_Valor VARCHAR2, p_Result OUT SYS_REFCURSOR) IS
    V_FILTRO         VARCHAR2(4000);
    V_Query          VARCHAR2(4000);
  BEGIN

    IF P_Filtro = 'fecha' THEN
      V_FILTRO := P_Filtro || ' BETWEEN ' || P_Valor;
    ELSE
      V_FILTRO := P_Filtro || ' LIKE ''' || P_Valor || '''';
    END IF;

    IF P_Filtro = 'NroFormulario' THEN
      V_FILTRO := 'd.numeroformulario LIKE ''' || P_Valor || '''';
    END IF;
    IF P_Filtro = 'Documento' THEN
      V_FILTRO := 'param.nombre LIKE ''' || P_Valor || '''';
    END IF;
    IF P_Filtro = 'Solicitante' THEN
      V_FILTRO := 'u.nombre LIKE ''' || P_Valor || '''';
    END IF;
    IF P_Filtro = 'Usuario' THEN
      V_FILTRO := 'u.nombre LIKE ''' || P_Valor || '''';
    END IF;
    IF P_Filtro = 'Dirigido' THEN
      V_FILTRO := 'u.nombre LIKE ''' || P_Valor || '''';
    END IF;
    IF P_Filtro = 'Estado' THEN
      V_FILTRO := 'pestado.nombre LIKE ''' || P_Valor || '''';
    END IF;

    V_Query := '
    SELECT ad.id,
           ad.consecutivo,
           ad.fecha,
           param.nombre Documento,
           u.nombre Solicitante,
           d.numeroformulario NroFormulario,
           ad.id_usuario UsuarioId,
           u.nombre Usuario,
           ad.dirigido Dirigido,
           pestado.nombre Estado
    FROM   tbacto_administrativo ad
           INNER JOIN tbparametros param ON param."ID" = ad.param_documento
           INNER JOIN tbparametros pestado ON pestado."ID" = ad.param_estado
           INNER JOIN tbusuarios u ON u."ID" = ad.id_usuario
           LEFT JOIN tbdeclaraciones d ON d."ID" = ad.id_declaracion
    WHERE  '|| V_FILTRO;

    DBMS_OUTPUT.PUT_LINE(V_QUERY);
    OPEN P_Result FOR V_Query;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error');
  END;

  PROCEDURE sp_getActosAdminCantidad(p_Cantidad OUT NUMBER) IS
  BEGIN

    SELECT COUNT(*)
    INTO   P_Cantidad
    FROM   tbacto_administrativo ad
           INNER JOIN tbparametros param ON param."ID" = ad.param_documento
           INNER JOIN tbusuarios u ON u."ID" = ad.id_usuario
           LEFT JOIN tbdeclaraciones d ON d."ID" = ad.id_declaracion;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error');
  END;

  PROCEDURE sp_getDocumentosPorArea(p_Id_Area NUMBER, p_Result OUT SYS_REFCURSOR) IS
  BEGIN

    OPEN P_Result FOR
    SELECT PARAM."ID",
           PARAM.nombre
    FROM   tbarea_documento AD
           INNER JOIN tbparametros PARAM ON PARAM."ID" = AD.param_documento
    WHERE  AD.param_area = P_Id_Area;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error');
  END;

  PROCEDURE sp_getCantidadFormulario(p_Formulario VARCHAR2, p_Cantidad OUT NUMBER) IS
  BEGIN

    SELECT COUNT(*)
    INTO   P_Cantidad
    FROM   tbdeclaraciones d
           INNER JOIN tbestadoprocesos ep ON ep.id_proceso = d."ID"
           INNER JOIN tbradicacion r ON r."ID" = ep.id_detalle_radicacion
    WHERE  d.numeroformulario = P_Formulario
           OR r.nro_formulario = P_Formulario;

  EXCEPTION
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Error');
  END;

  PROCEDURE sp_setActoAdministrativo(p_Documento NUMBER, p_Num_Interno VARCHAR2, p_Formulario VARCHAR2, p_Descripcion VARCHAR2, p_Dirigido VARCHAR2, p_Usuario NUMBER, p_Estado NUMBER, p_Id OUT NUMBER, p_Consecutivo OUT VARCHAR2) IS
    V_DECLARACION NUMBER;
   -- ECODE VARCHAR2(500);
  BEGIN

    P_Id := SEQ_ACTO_ADMIN.NEXTVAL;

    SELECT  TO_CHAR(SYSDATE, 'YYYY') || '-' || CAST(COUNT(*) + 1 AS VARCHAR2(4000))
    INTO    P_Consecutivo
    FROM    tbacto_administrativo Ad;

    IF P_FORMULARIO IS NOT NULL THEN
      SELECT        d."ID"
      INTO          V_DECLARACION
      FROM          tbdeclaraciones d
                    INNER JOIN tbestadoprocesos ep ON ep.id_proceso = d."ID"
                    INNER JOIN tbradicacion r ON r."ID" = ep.id_detalle_radicacion
      WHERE         r.nro_formulario = P_FORMULARIO;

   
       
    END IF;
    
   

    INSERT INTO tbacto_administrativo
    (
      "ID",
      consecutivo,
      fecha,
      param_documento,
      num_interno,
      id_declaracion,
      descripcion,
      dirigido,
      id_usuario,
      param_estado
    )
    VALUES
    (
      P_Id,
      P_Consecutivo,
      SYSDATE,
      P_DOCUMENTO,
      P_NUM_INTERNO,
      V_DECLARACION,
      P_DESCRIPCION,
      P_DIRIGIDO,
      P_USUARIO,
      P_ESTADO
    );

    COMMIT;

  /*EXCEPTION
    when OTHERS then
      ECODE := SQLCODE;
      DBMS_OUTPUT.PUT_LINE(ECODE);*/
  END;
  
  PROCEDURE sp_ActualizarTipoCodigo(pi_IdActoAdministrativo IN NUMBER, pi_TipoCodigo IN NUMBER) AS
  BEGIN
    UPDATE TBACTO_ADMINISTRATIVO SET TIPOCODIGOACTO = pi_TipoCodigo WHERE ID = pi_IdActoAdministrativo;
  END;

/***********************************************************
* Procedure description:
* Date:   24/07/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_updActoAdministrativo
(
  P_DOCUMENTO      NUMBER,
  P_NUM_INTERNO    VARCHAR2,
  P_FORMULARIO     VARCHAR2,
  P_DESCRIPCION    VARCHAR2,
  P_DIRIGIDO       VARCHAR2,
  P_USUARIO        NUMBER,
  P_ESTADO         NUMBER,
  P_Id             NUMBER,
  P_Consecutivo    OUT VARCHAR2
)
AS
  V_DECLARACION NUMBER;
BEGIN

  IF P_FORMULARIO IS NOT NULL THEN
    SELECT        d."ID"
    INTO          V_DECLARACION
    FROM          tbdeclaraciones d
                  INNER JOIN tbestadoprocesos ep ON ep.id_proceso = d."ID"
                  INNER JOIN tbradicacion r ON r."ID" = ep.id_detalle_radicacion
    WHERE         r.nro_formulario = P_FORMULARIO;

  END IF;

  UPDATE tbacto_administrativo
  SET     param_documento = P_DOCUMENTO,
         num_interno = P_NUM_INTERNO,
         id_declaracion = V_DECLARACION,
         descripcion = P_DESCRIPCION,
         dirigido = P_DIRIGIDO,
         param_estado = P_ESTADO,
         id_usuariomod = P_USUARIO,
         fecha_modifica = SYSDATE
  WHERE  "ID" = P_Id;

  SELECT AD.consecutivo
  INTO   P_Consecutivo
  FROM   tbacto_administrativo AD
  WHERE  ad."ID" = P_Id;

  COMMIT;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

/***********************************************************
* Procedure description:
* Date:   24/07/2012
* Author: Cristian Neira
*
* Changes
* Date    Modified By      Comments
************************************************************
*
************************************************************/
PROCEDURE  sp_GetActoAdminPorId
(
  P_Id             NUMBER,
  P_Result         OUT SYS_REFCURSOR
)
AS
BEGIN

  OPEN P_Result FOR
  SELECT
    doc.nombre Documento,
    ta.param_documento,
    ta.num_interno,
    d.numeroformulario,
    ta.descripcion,
    ta.id_usuario,
    ta.param_estado,
    ta.fecha,
    ta.dirigido,
    ta."ID",
    ta.consecutivo
  FROM
    tbacto_administrativo ta
    LEFT JOIN tbdeclaraciones d ON d."ID" = ta.id_declaracion
    INNER JOIN tbarea_documento ad ON ad.param_documento = ta.param_documento
    INNER JOIN tbparametros doc ON doc."ID" = ta.param_documento
  WHERE ta."ID" = P_Id;

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error');
END;

  PROCEDURE sp_Notificacion(pi_IdValoracion IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
    v_CodigoOrfeo           VARCHAR2(30);
    v_IdUsuarioValorador    NUMBER;
    v_IdDeclaracion         NUMBER;    
    v_ContadorIncluidos     NUMBER;
    v_ContadorNoIncluidos   NUMBER;
    v_TipoDocumento         NUMBER;
    v_HechosIncluidos       VARCHAR2(1000);
    v_HechosNoIncluidos     VARCHAR2(1000);
    v_PrincipiosInclusion   VARCHAR2(1000);
    v_PrincipiosNoInclusion VARCHAR2(1000);
    v_HechosDeclaracion     VARCHAR2(1000);
    CODIGOINEXISTENTE EXCEPTION;
  BEGIN
    /* Determinar existencia de código ORFEO para el acto administrativo */
    BEGIN
      SELECT NUMERO INTO v_CodigoOrfeo FROM TBCODIGOORFEO WHERE NIDVALORACION = pi_IdValoracion AND ROWNUM = 1;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      RAISE CODIGOINEXISTENTE;
    END;
    
    SELECT ID_VALORADOR, ID_DECLARACION INTO v_IdUsuarioValorador, v_IdDeclaracion FROM TBVALORACION WHERE ID = pi_IdValoracion;
    -- PKG_NOTIFICACION.INSERTANOTIFICACION(V_IDDECLARACION);
    
    /* Determinar tipo de acto administrativo a partir de contadores */
    SELECT SUM(CASE WHEN VP.ID_ESTADO_VAL = 1 THEN 1 ELSE 0 END), SUM(CASE WHEN VP.ID_ESTADO_VAL = 2 THEN 1 ELSE 0 END)
      INTO v_ContadorIncluidos, v_ContadorNoIncluidos
    FROM TBVAL_ANEXO_PERSONA VP
    INNER JOIN TBVALORACION_ANEXO VA ON VA.ID = VP.ID_VAL_ANEXO
    INNER JOIN TBVALORACION       VL ON VL.ID = VA.ID_VALORACION
    WHERE VL.ID = pi_IdValoracion;
    IF v_ContadorIncluidos > 0 AND v_ContadorNoIncluidos <= 0 THEN
      v_TipoDocumento:= 1; -- Incluido
    ELSIF v_ContadorIncluidos <= 0 AND v_ContadorNoIncluidos > 0 THEN
      v_TipoDocumento:= 2; -- No Incluido
    ELSE
      v_TipoDocumento:= 3; -- Mixto
    END IF;
    
    /* Hechos incluidos, hechos no incluidos, principios de inclusion y no inclusion */
    v_HechosIncluidos   := REPLACE(f_GetHechosPorTipo(pi_IdValoracion, 1),';',',');
    v_HechosNoIncluidos := REPLACE(f_GetHechosPorTipo(pi_IdValoracion, 2),';',',');
    v_PrincipiosInclusion   := REPLACE(f_GetPrincipiosPorTipo(pi_IdValoracion, 1),';',',');
    v_PrincipiosNoInclusion := REPLACE(f_GetPrincipiosPorTipo(pi_IdValoracion, 2),';',',');
    
    /* Hechos Adicionados en Valoracion(1) y en Declaracion(0) */
    -- v_HechosValoracion := REPLACE(F_GETHECHOSADDVALORDECLAR(pi_IdValoracion,1),';',',');
    v_HechosDeclaracion := REPLACE(F_GETHECHOSADDVALORDECLAR(pi_IdValoracion,0),';',',');
    /* FAILSAFE: Verificacion de existencia de Aacto Administrativo para poblar documentos */
    sp_EstablecerActoAdmin(pi_IdValoracion);
    
    OPEN po_Cursor FOR
      SELECT DCL.NUMEROFORMULARIO       AS NUMEROFORMULARIO
           , DCL.FECHADECLARACION       AS FECHADECLARACION
           , EMC.NOMBRE                 AS NOMBREENTIDAD
           , DDP.NOMBRE                 AS NOMBREDEPARTAMENTO
           , DMC.NOMBRE                 AS NOMBREMUNICIPIO
           , f_GetEntidadTotalDeclaracion(EMC.NOMBRE
                                        , NVL(DDP.CODIGOCODAZZI, '') || NVL(DMC.CODIGOCODAZZI, '')
                                        , DDP.NOMBRE
                                        , DMC.NOMBRE) AS ENTIDADCOMPLETADECLARACION
           , PRS.PRIMERNOMBRE ||
             CASE WHEN PRS.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PRS.SEGUNDONOMBRE END ||
             CASE WHEN PRS.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PRS.PRIMERAPELLIDO END ||
             CASE WHEN PRS.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PRS.SEGUNDOAPELLIDO END AS NOMBREDECLARANTE
           , TDC.NOMBRE                 AS TIPODOCUMENTO
           , PRS.NUMERODOCUMENTO        AS DOCUMENTOIDENTIDAD
           , VAL.FECHAVALORACION        AS FECHAVALORACION
           , VAL.FECHAVALORACIONREAL    AS FECHAVALORACIONREAL
           , VAL.MOTIVACION             AS MOTIVACION
           , NVL(NTF.DIRECCIONNOTIFICACION, CASE WHEN f_EvaluarDireccion(RGP.DIRECCION) = 1 THEN RGP.DIRECCION ELSE DTC.DIRECCION END) AS DIRECCION
           , NVL(RGP.TELEFONO, '') ||
             CASE WHEN RGP.TELEFONO IS NOT NULL AND RGP.MOVIL IS NOT NULL THEN ' / ' ELSE '' END ||
             NVL(RGP.MOVIL, '')         AS TELEFONO
           , CASE WHEN f_EvaluarDireccion(RGP.DIRECCION) = 1 THEN VDP.NOMBRE ELSE DTD.NOMBRE END AS DEPARTAMENTO
           , CASE WHEN f_EvaluarDireccion(RGP.DIRECCION) = 1 THEN VMC.NOMBRE ELSE DTM.NOMBRE END AS MUNICIPIO
           , UVL.NOMBRE                 AS VALORADOR
           , ULV.NOMBRE                 AS LIDER
           , REPLACE(v_HechosIncluidos  , '-', ' ')      AS HECHOVICTIMIZANTEIN
           , REPLACE(v_HechosNoIncluidos, '-', ' ')      AS HECHOVICTIMIZANTENOIN
           , REPLACE(v_HechosDeclaracion, '-', ' ')      AS HECHOAGREGADODECLARACION
           , REPLACE(v_PrincipiosInclusion  , '-', ' ')  AS PRINCIPIOSIN
           , REPLACE(v_PrincipiosNoInclusion, '-', ' ')  AS PRINCIPIOSNOIN
           , ADM.ID                     AS IDACTOADMIN
           , ADM.CONSECUTIVO            AS CONSECUTIVO
           , ADM.TIPOCODIGOACTO         AS TIPOCODIGOACTO
           , COALESCE(ADM.FECHA_MODIFICA, ADM.FECHA) AS FECHAACTOADMINISTRATIVO
           , RAD.FECHALLEGADA           AS FECHARADICACION
           , v_CodigoOrfeo              AS CODIGOORFEO
           , v_TipoDocumento            AS TIPODOCUMENTOVALORACION
           , MOT.MOTIVACION_INCLUSION   AS MOTIVACIONINCLUSION
           , MOT.MOTIVACION_NOINCLUSION AS MOTIVACIONNOINCLUSION
           , MOT.RESUELVE_ARTICULO1     AS RESUELVEARTICULO1
           , MOT.RESUELVE_ARTICULO2     AS RESUELVEARTICULO2
           , UPR.USUARIO                AS USUARIOPROYECTO
           , URV.USUARIO                AS USUARIOREVISO
           --, PNT.NOMBRE                 AS NOMBREPUNTO
           --, PNT.DIRECCIONENTIDAD       AS DIRECCIONPUNTO
           , COALESCE(F_PROPEREXARTICULOS(PAT.TEXTONOMBRE), F_PROPEREXARTICULOS(DTE.TEXTONOMBRE), 'NO REGISTRA') AS NOMBREPUNTO
           , COALESCE(F_PROPEREXARTICULOS(PAT.TEXTODIRECCION), F_PROPEREXARTICULOS(DTE.TEXTODIRECCION), 'NO REGISTRA') AS DIRECCIONPUNTO
      FROM TBDECLARACIONES DCL
      INNER JOIN (SELECT ID_DECLARACION, MIN(ID) AS ID FROM TBREGISTROS_PERSONAS
                  WHERE ESDECLARANTE = 1 GROUP BY ID_DECLARACION) RDP ON RDP.ID_DECLARACION = DCL.ID
      INNER JOIN TBREGISTROS_PERSONAS    RGP ON RGP.ID = RDP.ID
      INNER JOIN TBPERSONAS              PRS ON PRS.ID = RGP.ID_PERSONA
      INNER JOIN TBVALORACION            VAL ON DCL.ID = VAL.ID_DECLARACION
      INNER JOIN TBACTO_ADMINISTRATIVO   ADM ON ADM.ID = VAL.IDACTOADMINISTRATIVO
      LEFT  JOIN TBRADICACION            RAD ON DCL.ID = RAD.ID_DECLARACION
      LEFT  JOIN TBVALORACION_MOTIVACION MOT ON VAL.ID = MOT.ID_VALORACION
      LEFT  JOIN TBENTIDADMUNICIPIO      EMC ON EMC.ID = DCL.ID_ENTIDADMUNICIPIODECLARACION
      LEFT JOIN TBNOTIFICACION           NTF ON NTF.ID_DECLARACION = DCL.ID
      LEFT  JOIN TBPARAMETROS            TDC ON TDC.ID = PRS.PARAM_TIPODOCUMENTO
      LEFT  JOIN TBGEOGRAFIA             DMC ON DMC.ID = NTF.ID_MUNICIPIO
      LEFT  JOIN TBGEOGRAFIA             DDP ON DDP.ID = DMC.PADREID
      LEFT  JOIN TBGEOGRAFIA             VDP ON VDP.ID = NTF.ID_DEPARTAMENTO
      LEFT  JOIN TBGEOGRAFIA             VMC ON VMC.ID = NTF.ID_MUNICIPIO
      LEFT  JOIN TBUSUARIOS              UVL ON UVL.ID = VAL.ID_VALORADOR
      LEFT  JOIN TBUSUARIOS              ULV ON ULV.ID = DCL.ID_USUARIO_ACTUAL
      LEFT  JOIN TBUSUARIOS              UPR ON UPR.ID = VAL.ID_VALORADOR
      LEFT  JOIN TBUSUARIOS              URV ON URV.ID = ADM.ID_USUARIOAPRUEBA
      --LEFT  JOIN TBENTIDADMUNICIPIO      PNT ON PNT.ID = VAL.IDENTIDADMUNICIPIO
      LEFT JOIN TBPUNTOATENCION          PAT ON PAT.ID = NTF.ID_PUNTOATENCION
      LEFT JOIN TBDIRECCIONTERRITORIAL   DTE ON DTE.ID = NTF.ID_DIRECCIONTERRITORIAL
      LEFT JOIN TBDIRECCIONTERRITORIAL   DTC ON DTC.ID = VAL.IDDIRECCIONTERRITORIALCITACION
      LEFT JOIN TBGEOGRAFIA              DTM ON DTM.ID = DTC.IDMUNICIPIO
      LEFT JOIN TBGEOGRAFIA              DTD ON DTD.ID = DTM.PADREID
      WHERE VAL.ID = pi_IdValoracion;
  EXCEPTION
    WHEN CODIGOINEXISTENTE THEN
      RAISE_APPLICATION_ERROR(-20000, 'No existe un código orfeo relacionado a la valoración ingresada.');
  END;

  PROCEDURE sp_EstablecerActoAdmin(pi_IdValoracion IN NUMBER) IS
    xIdActoAdministrativo NUMBER;
    xIdDeclaracion NUMBER;
    xIdValorador NUMBER;
    xNumeroFormulario TBDECLARACIONES.NUMEROFORMULARIO%TYPE;
  BEGIN
    SELECT ID_DECLARACION, ID_VALORADOR INTO xIdDeclaracion, xIdValorador FROM TBVALORACION WHERE ID = pi_IdValoracion;
    SELECT NUMEROFORMULARIO INTO xNumeroFormulario FROM TBDECLARACIONES WHERE ID = xIdDeclaracion;
    
    DECLARE
      xo_Consecutivo VARCHAR2(30);
    BEGIN
      SELECT ID INTO xIdActoAdministrativo FROM TBACTO_ADMINISTRATIVO WHERE ID_DECLARACION = xIdDeclaracion;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN 
        PKG_ACTOSADMIN.SP_SETACTOADMINISTRATIVORUV(xIdDeclaracion, 0, '', xNumeroFormulario, '', '', xIdValorador, 2, 2, xIdActoAdministrativo, xo_Consecutivo);
      WHEN TOO_MANY_ROWS THEN
        NULL;
    END;
    UPDATE TBVALORACION SET IDACTOADMINISTRATIVO = xIdActoAdministrativo WHERE ID = pi_IdValoracion;
  END;

PROCEDURE  sp_setActoAdministrativoRUV
(
  PI_IDDECLARACION NUMBER,
  P_DOCUMENTO      NUMBER,
  P_NUM_INTERNO    VARCHAR2,
  P_FORMULARIO     VARCHAR2,
  P_DESCRIPCION    VARCHAR2,
  P_DIRIGIDO       VARCHAR2,
  P_USUARIO        NUMBER,
  P_ESTADO         NUMBER,
  PI_TIPOACTOADMIN NUMBER,
  P_Id             OUT NUMBER,
  P_Consecutivo    OUT VARCHAR2
)
AS
 
 -- ECODE VARCHAR2(500);
BEGIN

  P_Id := SEQ_ACTO_ADMIN.NEXTVAL;

  SELECT  TO_CHAR(SYSDATE, 'YYYY') || '-' || CAST(COUNT(*) + 1 AS VARCHAR2(4000))
  INTO    P_Consecutivo
  FROM    tbacto_administrativo Ad;

  INSERT INTO tbacto_administrativo
  (
    "ID",
    consecutivo,
    fecha,
    param_documento,
    num_interno,
    id_declaracion,
    descripcion,
    dirigido,
    ID_USUARIO,
    PARAM_ESTADO,
    TIPODOCACTOADMIN,
    AREADOCACTOADMIN
  )
  VALUES
  (
    P_Id,
    P_Consecutivo,
    SYSDATE,
    P_DOCUMENTO,
    P_NUM_INTERNO,
    PI_IDDECLARACION,
    P_DESCRIPCION,
    P_DIRIGIDO,
    P_USUARIO,
    P_ESTADO,
    PI_TIPOACTOADMIN,
    2
  );

  COMMIT;

/*EXCEPTION
  when OTHERS then
    ECODE := SQLCODE;
    DBMS_OUTPUT.PUT_LINE(ECODE);*/
END;

PROCEDURE SP_ACTESTADOACTOADMIN (PI_IDACTOADMIN IN NUMBER,
                                 PI_ESTADOACTOADMIN IN NUMBER,
                                 PI_IDUSUARIO IN NUMBER                                                              
                                ) IS

BEGIN                                

      UPDATE TBACTO_ADMINISTRATIVO
      SET PARAM_ESTADO = PI_ESTADOACTOADMIN,
          ID_USUARIOMOD = PI_IDUSUARIO,
          FECHA_MODIFICA = SYSDATE,
          DFIRMA =  CASE
                      WHEN PI_ESTADOACTOADMIN = FIRMADO THEN SYSDATE
                      ELSE NULL
                    END  
      WHERE ID =  PI_IDACTOADMIN;       

END;

PROCEDURE SP_GETIDVALORACIONBYIDDEC(
                                    PI_IDDECLARACION IN NUMBER,
                                    PO_IDVALORACION OUT NUMBER
                                   ) IS
      V_IDDECLARACION NUMBER;                                     
BEGIN

  PO_IDVALORACION := 0;
  
  SELECT ID_DECLARACION, MAX(ID) INTO V_IDDECLARACION, PO_IDVALORACION FROM TBVALORACION
  WHERE  ID_DECLARACION = PI_IDDECLARACION
  GROUP BY ID_DECLARACION;

END;

FUNCTION f_getPrincipiosPorTipo
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPO IN NUMBER
)
RETURN VARCHAR2
IS
  V_PRINCIPIOS SYS_REFCURSOR;
  V_PRINCIPIO VARCHAR2(10000);
  V_RESULT VARCHAR2(10000);
BEGIN

  OPEN V_PRINCIPIOS FOR
    SELECT DISTINCT PRI.TEXTO FROM TBVAL_ANEXO_PERSONA VAP
      INNER JOIN TBVALORACION_ANEXO VA ON VA.ID = VAP.ID_VAL_ANEXO
      INNER JOIN TBVALORACION V ON V.ID = VA.ID_VALORACION
      LEFT JOIN TBPRINCIPIO_VAL PRVAL ON PRVAL.ID_VAL_ANEXO_PER = VAP.ID
      LEFT JOIN TBPRINCIPIO PRI ON PRI.ID = PRVAL.ID_PRINCIPIO
    WHERE V.ID = PI_IDVALORACION AND VAP.ID_ESTADO_VAL = PI_TIPO;

  LOOP
      FETCH V_PRINCIPIOS INTO V_PRINCIPIO;
      EXIT WHEN V_PRINCIPIOS%NOTFOUND;
      V_RESULT := V_RESULT || '-' ||V_PRINCIPIO || ';';
   END LOOP;

   CLOSE V_PRINCIPIOS;

  RETURN V_RESULT;
END;

FUNCTION f_getHechosPorTipo
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPO IN NUMBER
)
RETURN VARCHAR2
IS
  V_HECHOS SYS_REFCURSOR;
  V_HECHO VARCHAR2(10000);
  V_RESULT VARCHAR2(10000);
BEGIN

  OPEN V_HECHOS FOR
    SELECT DISTINCT(PAR.NOMBRE) FROM TBVALORACION_ANEXO VA
      INNER JOIN TBSINIESTROS_PERSONA SP ON SP.ID = VA.ID_SINIESTRO
      INNER JOIN TBVAL_ANEXO_PERSONA VAP ON VAP.ID_VAL_ANEXO = VA.ID  
      INNER JOIN TBPARAMETROS PAR ON PAR.NUMERO = SP.PARAM_TIPOHECHO AND PAR.ID_TIPOPARAMETRO = HECHO_VICTIMIZANTE AND SP.PARAM_TIPOHECHO <= 12
    WHERE VA.ID_VALORACION = PI_IDVALORACION AND VAP.ID_ESTADO_VAL = PI_TIPO;

  LOOP
      FETCH V_HECHOS INTO V_HECHO;
      EXIT WHEN V_HECHOS%NOTFOUND;
      V_RESULT := V_RESULT || '-' ||V_HECHO || ';';
   END LOOP;

   CLOSE V_HECHOS;

  RETURN V_RESULT;
END;
/**/
FUNCTION F_GETHECHOSADDVALORDECLAR
(
  PI_IDVALORACION IN NUMBER,
  PI_TIPOAGREGADOHECHO IN NUMBER
)
RETURN VARCHAR2
IS
  V_HECHOS SYS_REFCURSOR;
  V_HECHO VARCHAR2(10000);
  V_RESULT VARCHAR2(10000);
BEGIN

  OPEN V_HECHOS FOR
    SELECT DISTINCT(PAR.NOMBRE)
    FROM TBVALORACION_ANEXO VA
      INNER JOIN TBSINIESTROS_PERSONA SP ON SP.ID = VA.ID_SINIESTRO
      INNER JOIN TBVAL_ANEXO_PERSONA VAP ON VAP.ID_VAL_ANEXO = VA.ID  
      INNER JOIN TBPARAMETROS PAR ON PAR.NUMERO = SP.PARAM_TIPOHECHO AND PAR.ID_TIPOPARAMETRO = HECHO_VICTIMIZANTE AND SP.PARAM_TIPOHECHO <= 12
    WHERE VA.ID_VALORACION = PI_IDVALORACION AND SP.HECHOENVALORACION = PI_TIPOAGREGADOHECHO;

  LOOP
      FETCH V_HECHOS INTO V_HECHO;
      EXIT WHEN V_HECHOS%NOTFOUND;
      V_RESULT := V_RESULT || '-' ||V_HECHO || ';';
   END LOOP;

   CLOSE V_HECHOS;

  RETURN V_RESULT;
END;

  FUNCTION f_GetUbicacionPuntoDeclaracion(pi_IdMunicipio IN NUMBER, pi_IdDepartamento IN NUMBER) RETURN VARCHAR2 IS
    xCodigoBogota VARCHAR2(10) := '11001';
    xResult       VARCHAR2(500);
  BEGIN
    SELECT CASE WHEN NVL(DPT.CODIGOCODAZZI, '') || NVL(MCP.CODIGOCODAZZI, '') = xCodigoBogota
           THEN 'de ' || MCP.NOMBRE
           ELSE 'del municipio de ' || MCP.NOMBRE || ' del departamento de ' || DPT.NOMBRE END
           INTO xResult
    FROM TBGEOGRAFIA MCP, TBGEOGRAFIA DPT
    WHERE MCP.ID = pi_IdMunicipio AND DPT.ID = pi_IdDepartamento;

    RETURN xResult;
  END;
  
  FUNCTION f_GetEntidadTotalDeclaracion(pi_NombreEntidad VARCHAR2, pi_CodigoMunicipio VARCHAR2, pi_NombreDepartamento VARCHAR2, pi_NombreMunicipio VARCHAR2) RETURN VARCHAR2 IS
    xCodigoBogota VARCHAR2(10) := '11001';
    xResult       VARCHAR2(500);
  BEGIN
    IF UPPER(pi_NombreEntidad) LIKE '%MUNICIPAL%' THEN
      IF pi_CodigoMunicipio = xCodigoBogota THEN
        xResult := pi_NombreEntidad || ' de ' || pi_NombreMunicipio;
      ELSE
        xResult := pi_NombreEntidad || ' de ' || pi_NombreMunicipio || ' del departamento de ' || pi_NombreDepartamento;
      END IF;
    ELSE
      IF pi_CodigoMunicipio = xCodigoBogota THEN
        xResult := pi_NombreEntidad || ' de ' || pi_NombreMunicipio;
      ELSE
        xResult := pi_NombreEntidad || ' del municipio de ' || pi_NombreMunicipio || ' del departamento de ' || pi_NombreDepartamento;
      END IF;
    END IF;
    RETURN xResult;
  END;
  
  FUNCTION f_EvaluarDireccion(pi_Direccion VARCHAR2) RETURN NUMBER IS
  BEGIN
  
    IF pi_Direccion IS NULL OR LTRIM(RTRIM(UPPER(pi_Direccion))) = 'SIN NOMENCLATURA' OR LTRIM(RTRIM(UPPER(pi_Direccion))) = 'NO INFORMA' THEN
      RETURN 0;
    ELSE
      RETURN 1;
    END IF;
    
  END;
  
  FUNCTION ObtenerNomPersoneria(pi_IdMunicipio IN NUMBER) RETURN VARCHAR2 IS
    xResult VARCHAR2(500);
  BEGIN
    SELECT (EM.NOMBREENCARGADO || ' DE ' || MUN.NOMBRE || ' ' || DEP.NOMBRE) NOMBRE INTO xResult 
    FROM TBENTIDADMUNICIPIO EM 
    INNER JOIN TBGEOGRAFIA MUN ON MUN.ID = EM.ID_MUNICIPIO 
    INNER JOIN TBGEOGRAFIA DEP ON DEP.ID = MUN.PADREID
    WHERE EM.ID_MUNICIPIO = pi_IdMunicipio AND ID_ENTIDAD = 3 AND ROWNUM = 1;
    RETURN xResult;
  END;
  
  FUNCTION ObtenerDirPersoneria(pi_IdMunicipio IN NUMBER) RETURN VARCHAR2 IS
    xResult VARCHAR2(500);
  BEGIN
    SELECT DIRECCIONENTIDAD INTO xResult 
    FROM TBENTIDADMUNICIPIO
    WHERE ID_MUNICIPIO = pi_IdMunicipio AND ID_ENTIDAD = 3 AND ROWNUM = 1;
    RETURN xResult;
  END;
  
  FUNCTION F_PROPEREXARTICULOS(PI_TEXTO IN VARCHAR2) RETURN VARCHAR2 IS
    xResult VARCHAR2(500);
  BEGIN
    IF (PI_TEXTO IS NULL) THEN
      RETURN NULL;
    END IF;  
  
    xResult := INITCAP(PI_TEXTO);
    xResult := REPLACE(xResult, 'De ', 'de ');
    xResult := REPLACE(xResult, 'Del ', 'del ');
    xResult := REPLACE(xResult, 'De La ', 'de la ');
    
    RETURN xResult;
  END;

END PKG_ACTOSADMIN;
/