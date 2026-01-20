-- Create new package
create or replace PACKAGE PKG_CONSULTA AS

  TYPE CURSOR_TYPE IS REF CURSOR;

  PROCEDURE sp_BuscaDeclaracionesCount(pi_NumeroFormulario IN VARCHAR2,
                                       pi_PrimerNombre     IN VARCHAR2,
                                       pi_PrimerApellido   IN VARCHAR2,
                                       pi_NumeroDocumento  IN VARCHAR2,
                                       po_RecordCount      OUT NUMBER);

  PROCEDURE sp_BuscaDeclaraciones(pi_NumeroFormulario IN VARCHAR2,
                                  pi_PrimerNombre     IN VARCHAR2,
                                  pi_PrimerApellido   IN VARCHAR2,
                                  pi_NumeroDocumento  IN VARCHAR2,
                                  pi_PageNumber       IN NUMBER,
                                  pi_PageSize         IN NUMBER,
                                  po_Cursor           OUT CURSOR_TYPE);

  PROCEDURE sp_DetalleDeclaracion(pi_IdDeclaracion IN NUMBER,
                                  po_Cursor        OUT CURSOR_TYPE);

END;
/

-- Create package body
create or replace PACKAGE BODY PKG_CONSULTA AS

  /* Obtiene la cantidad de registros devueltos por la consulta */
  PROCEDURE sp_BuscaDeclaracionesCount(pi_NumeroFormulario IN VARCHAR2,
                                       pi_PrimerNombre     IN VARCHAR2,
                                       pi_PrimerApellido   IN VARCHAR2,
                                       pi_NumeroDocumento  IN VARCHAR2,
                                       po_RecordCount      OUT NUMBER) IS
  BEGIN
    SELECT COUNT(1) INTO po_RecordCount FROM TBDECLARACIONES DD
    INNER JOIN TBREGISTROS_PERSONAS RP ON (DD.ID = RP.ID_DECLARACION)
    INNER JOIN TBPERSONAS           PP ON (PP.ID = RP.ID_PERSONA)
    WHERE DD.NUMEROFORMULARIO = NVL(pi_NumeroFormulario, DD.NUMEROFORMULARIO)
      AND PP.NUMERODOCUMENTO  = NVL(pi_NumeroDocumento , PP.NUMERODOCUMENTO)
      AND UPPER(PP.PRIMERNOMBRE)   LIKE '%' || NVL(pi_PrimerNombre  , UPPER(PP.PRIMERNOMBRE)) || '%'
      AND UPPER(PP.PRIMERAPELLIDO) LIKE '%' || NVL(pi_PrimerApellido, UPPER(PP.PRIMERAPELLIDO)) || '%';
  END;

  /*Obtiene los registros de la consulta */
  PROCEDURE sp_BuscaDeclaraciones(pi_NumeroFormulario IN VARCHAR2,
                                  pi_PrimerNombre     IN VARCHAR2,
                                  pi_PrimerApellido   IN VARCHAR2,
                                  pi_NumeroDocumento  IN VARCHAR2,
                                  pi_PageNumber       IN NUMBER,
                                  pi_PageSize         IN NUMBER,
                                  po_Cursor           OUT CURSOR_TYPE) IS
    lowerBound NUMBER;
    upperBound NUMBER;
  BEGIN
    lowerBound := (pi_PageNumber * pi_PageSize) + 1;
    upperBound := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    OPEN po_Cursor FOR
      SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (SELECT PX.NUMEROFORMULARIO AS NUMEROFORMULARIO
                       , P2.NOMBRE           AS ESTADOPROCESO
                       , PX.FECHADECLARACION AS FECHADECLARACION
                       , SM.NOMBRE           AS MUNICIPIO
                       , SD.NOMBRE           AS DEPARTAMENTO
                       , PX.PRIMERNOMBRE     AS PRIMERNOMBRE
                       , PX.SEGUNDONOMBRE    AS SEGUNDONOMBRE
                       , PX.PRIMERAPELLIDO   AS PRIMERAPELLIDO
                       , PX.SEGUNDOAPELLIDO  AS SEGUNDOAPELLIDO
                       , P1.NOMBRE           AS TIPODOCUMENTO
                       , PX.NUMERODOCUMENTO  AS NUMERODOCUMENTO
                       , PX.ID               AS ID
                  FROM (SELECT DD.NUMEROFORMULARIO AS NUMEROFORMULARIO
                             , DD.FECHADECLARACION AS FECHADECLARACION
                             , PP.PRIMERNOMBRE     AS PRIMERNOMBRE
                             , PP.SEGUNDONOMBRE    AS SEGUNDONOMBRE
                             , PP.PRIMERAPELLIDO   AS PRIMERAPELLIDO
                             , PP.SEGUNDOAPELLIDO  AS SEGUNDOAPELLIDO
                             , PP.NUMERODOCUMENTO  AS NUMERODOCUMENTO
                             , DD.ID               AS ID
                             , PP.PARAM_TIPODOCUMENTO
                             , DD.PARAM_ESTADO
                             , DD.ID_MUNICIPIODECLARACION
                             , DD.ID_DEPARTAMENTODECLARACION
                        FROM TBDECLARACIONES DD
                        INNER JOIN TBREGISTROS_PERSONAS RP ON (DD.ID = RP.ID_DECLARACION)
                        INNER JOIN TBPERSONAS           PP ON (PP.ID = RP.ID_PERSONA)
                        WHERE DD.NUMEROFORMULARIO = NVL(pi_NumeroFormulario, DD.NUMEROFORMULARIO)
                          AND PP.NUMERODOCUMENTO  = NVL(pi_NumeroDocumento , PP.NUMERODOCUMENTO)
                          AND UPPER(PP.PRIMERNOMBRE)   LIKE '%' || NVL(pi_PrimerNombre  , UPPER(PP.PRIMERNOMBRE)) || '%'
                          AND UPPER(PP.PRIMERAPELLIDO) LIKE '%' || NVL(pi_PrimerApellido, UPPER(PP.PRIMERAPELLIDO)) || '%'
                  ) PX
                  INNER JOIN TBPARAMETROS P1 ON (P1.ID = PX.PARAM_TIPODOCUMENTO)
                  INNER JOIN TBPARAMETROS P2 ON (P2.ID = PX.PARAM_ESTADO)
                  LEFT  JOIN TBGEOGRAFIA  SM ON (SM.ID = PX.ID_MUNICIPIODECLARACION)
                  LEFT  JOIN TBGEOGRAFIA  SD ON (SD.ID = PX.ID_DEPARTAMENTODECLARACION)
            ) INFO
            WHERE ROWNUM < lowerBound)
      WHERE R >= upperBound;
  END;

  /* Obtiene el detalle de la consulta */
  PROCEDURE sp_DetalleDeclaracion(pi_IdDeclaracion IN NUMBER,
                                  po_Cursor        OUT CURSOR_TYPE) IS
    v_NombreDeclarante VARCHAR2(500);
    v_TipoDocumento    VARCHAR2(200);
    v_DocumentoDeclant VARCHAR2(200);
  BEGIN
    -- Obtener nombre del declarante
    SELECT NVL(PP.PRIMERNOMBRE, ' ') ||
           CASE WHEN PP.SEGUNDONOMBRE   IS NULL THEN '' ELSE ' ' || PP.SEGUNDONOMBRE   END ||
           CASE WHEN PP.PRIMERAPELLIDO  IS NULL THEN '' ELSE ' ' || PP.PRIMERAPELLIDO  END ||
           CASE WHEN PP.SEGUNDOAPELLIDO IS NULL THEN '' ELSE ' ' || PP.SEGUNDOAPELLIDO END
         , TD.NOMBRE
         , PP.NUMERODOCUMENTO
    INTO v_NombreDeclarante, v_TipoDocumento, v_DocumentoDeclant
    FROM TBDECLARACIONES DD
    INNER JOIN TBREGISTROS_PERSONAS RP ON (DD.ID = RP.ID_DECLARACION)
    INNER JOIN TBPERSONAS           PP ON (PP.ID = RP.ID_PERSONA)
    LEFT  JOIN TBPARAMETROS         TD ON (TD.ID = PP.PARAM_TIPODOCUMENTO)
    WHERE RP.ESDECLARANTE = 1
      AND DD.ID = pi_IdDeclaracion
      AND ROWNUM = 1;

    OPEN po_Cursor FOR
      SELECT DD.NUMEROFORMULARIO   AS NUMEROFORMULARIO
           , AX.ID                 AS ANEXOID
           , AX.TIPO               AS TIPOANEXO
           , AX.ID_SINIESTRO       AS ID_SINIESTRO
           , v_NombreDeclarante    AS NOMBREDECLARANTE
           , v_TipoDocumento       AS TIPODOCUMENTO
           , v_DocumentoDeclant    AS DOCUMENTOIDENTIDAD
           , (SELECT P.NOMBRE FROM TBPARAMETROS P WHERE P.ID = DD.PARAM_ESTADO) AS ESTADOACTUALPROCESO
           , (SELECT P.ID FROM TBPARAMETROS P WHERE P.ID = DD.PARAM_ESTADO) AS IDESTADOPROCESO
           , CAST(EV.NOMBRE AS VARCHAR2(500)) AS ESTADOVALORACION
           , VV.FECHAVALORACION     AS FECHAVALORACION
           , CAST(OV.NOMBRE AS VARCHAR2(500)) AS ESTADO
           , HV.NOMBRE_HECHO_VICTIMIZANTE
           , SP.FECHASINIESTRO AS FECHAHECHOS
           , PP.PRIMERNOMBRE || ' ' || PP.SEGUNDONOMBRE || ' ' || PP.PRIMERAPELLIDO || ' ' || PP.SEGUNDOAPELLIDO AS NOMBREVICTIMA
           , (SELECT P.NOMBRE FROM TBPARAMETROS P WHERE P.ID = PP.PARAM_TIPODOCUMENTO) AS TIPODOCUMENTO_VICTIMA
           , PP.NUMERODOCUMENTO AS DOCUMENTOVICTIMA
      FROM TBDECLARACIONES DD
      INNER JOIN TBREGISTROS_PERSONAS RP ON (DD.ID = RP.ID_DECLARACION)
      INNER JOIN TBPERSONAS           PP ON (PP.ID = RP.ID_PERSONA)
      LEFT  JOIN (SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 1 TIPO FROM TBANEXO1 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 2 TIPO FROM TBANEXO2 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 3 TIPO FROM TBANEXO3 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 4 TIPO FROM TBANEXO4 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, AD.ID_REGPERSONA, 5 TIPO FROM TBANEXO5 A
                  INNER JOIN TBANEXO5_DESPLAZADOS AD ON AD.ID_ANEXO5 = A.ID
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 6 TIPO FROM TBANEXO6 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 7 TIPO FROM TBANEXO7 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 8 TIPO FROM TBANEXO8 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 9 TIPO FROM TBANEXO9 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 10 TIPO FROM TBANEXO10 A
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, AM.ID_REGPERSONA, 11 TIPO FROM TBANEXO11 A
                  INNER JOIN TBANEXO11_MUEBLES AM ON AM.ID_ANEXO11 = A.ID
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, AI.ID_REGPERSONA, 11 TIPO FROM TBANEXO11 A
                  INNER JOIN TBANEXO11_INMUEBLES AI ON AI.ID_ANEXO11 = A.ID
                  UNION ALL
                  SELECT A.ID, A.ID_SINIESTRO, A.ID_REGPERSONA, 13 TIPO FROM TBANEXO13 A) AX ON AX.ID_REGPERSONA = RP.ID
      LEFT JOIN TBSINIESTROS_PERSONA   SP ON SP.ID = AX.ID_SINIESTRO
      LEFT JOIN TBHECHOS_VICTIMIZANTES HV ON HV.ID_HECHO_VICTIMIZANTE = SP.PARAM_TIPOHECHO
      LEFT JOIN TBVALORACION           VV ON DD.ID = VV.ID_DECLARACION
      LEFT JOIN TBVALORACION_ANEXO     VA ON VV.ID = VA.ID_VALORACION AND SP.ID = VA.ID_SINIESTRO
      LEFT JOIN TBVAL_ANEXO_PERSONA    VP ON VP.ID_VAL_ANEXO = VA.ID AND VP.ID_REGPERSONA = RP.ID
      LEFT JOIN TBESTADO_VAL           EV ON EV.ID = VP.ID_ESTADO_VAL
      LEFT JOIN TBOBSERVACION_VAL      OV on VP.ID_OBSERVACION_VAL = OV.ID
      WHERE DD.ID = pi_IdDeclaracion ORDER BY AX.ID_SINIESTRO;
  END;

END;
/