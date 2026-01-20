CREATE OR REPLACE FUNCTION f_MunicipioPorCodigoCodazzi(pi_CodigoCodazzi VARCHAR2) RETURN NUMBER IS
  xResult NUMBER;
BEGIN
  SELECT MCP.ID INTO xResult FROM TBGEOGRAFIA DPT, TBGEOGRAFIA MCP
  WHERE DPT.ID = MCP.PADREID AND NVL(DPT.CODIGOCODAZZI, '') || NVL(MCP.CODIGOCODAZZI, '') = pi_CodigoCodazzi;
  RETURN xResult;
EXCEPTION WHEN NO_DATA_FOUND THEN
  RAISE_APPLICATION_ERROR(-20054, 'No se encontró el municipio con el código ' || pi_CodigoCodazzi);
END;
/

-- Asegurar que no exista notificacion sin valores en PAIS, DEPARTAMENTO, y MUNICIPIO
BEGIN
  FOR t IN (SELECT ID, ID_DECLARACION FROM TBNOTIFICACION) LOOP
    DECLARE
      xPais         NUMBER;
      xDepartamento NUMBER;
      xMunicipio    NUMBER;
    BEGIN
      SELECT RGP.ID_PAIS, RGP.ID_DEPARTAMENTO, RGP.ID_MUNICIPIO INTO xPais, xDepartamento, xMunicipio
      FROM TBREGISTROS_PERSONAS RGP WHERE RGP.ID_DECLARACION = t.ID_DECLARACION AND RGP.ESDECLARANTE = 1;
      IF xPais IS NULL OR xDepartamento IS NULL OR xMunicipio IS NULL THEN
        xMunicipio := f_MunicipioPorCodigoCodazzi('11001');
        SELECT PAI.ID, DPT.ID INTO xPais, xDepartamento FROM TBGEOGRAFIA MCP
        INNER JOIN TBGEOGRAFIA DPT ON DPT.ID = MCP.PADREID
        INNER JOIN TBGEOGRAFIA PAI ON PAI.ID = DPT.PADREID
        WHERE MCP.ID = xMunicipio;
      END IF;
      UPDATE TBNOTIFICACION SET ID_PAIS = xPais, ID_DEPARTAMENTO = xDepartamento, ID_MUNICIPIO = xMunicipio
      WHERE ID = t.ID;
    END;
  END LOOP;
END;
/

-- Asegurar / Corregir identificador de punto de notificacion para cada una de las notificaciones anteriores
BEGIN
  FOR t IN (SELECT ID, ID_DECLARACION FROM TBNOTIFICACION) LOOP
    DECLARE
      xIdValoracion           NUMBER;
      xIdPuntoAtencion        NUMBER;
      xIdDireccionTerritorial NUMBER;
    BEGIN
      SELECT MAX(ID) INTO xIdValoracion FROM TBVALORACION WHERE ID_DECLARACION = t.ID_DECLARACION;
      SELECT IDPUNTOATENCION, IDDIRECCIONTERRITORIAL INTO xIdPuntoAtencion, xIdDireccionTerritorial FROM TBVALORACION WHERE ID = xIdValoracion;
      IF xIdPuntoAtencion IS NULL AND xIdDireccionTerritorial IS NULL THEN
        DBMS_OUTPUT.PUT_LINE('La valoracion con ID ' || xIdValoracion || ' tiene informacion incompleta');
        PKG_VALORACION.sp_DeterminarNotificacion(xIdValoracion);
      END IF;
      UPDATE TBNOTIFICACION SET ID_PUNTOATENCION = xIdPuntoAtencion, ID_DIRECCIONTERRITORIAL = xIdDireccionTerritorial
      WHERE ID = t.ID;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      DBMS_OUTPUT.PUT_LINE('La notificación ' || t.ID || ' no contiene un identificador de declaracion válido');
      DELETE FROM TBNOTIFICACION WHERE ID = t.ID;
    END;
  END LOOP;
  COMMIT;
END;
/

/* Corregir valoraciones */
BEGIN
  FOR valoracion IN (SELECT ID FROM TBVALORACION WHERE COALESCE(IDPUNTOATENCION, IDDIRECCIONTERRITORIAL) IS NULL ORDER BY ID DESC) LOOP
    BEGIN
      PKG_VALORACION.sp_DeterminarNotificacion(valoracion.ID);
    EXCEPTION WHEN OTHERS THEN
      NULL;
    END;
  END LOOP;
  COMMIT;
END;
/

/* Actualizar registros de TBACTO_ADMINISTRATIVO con el tipo de codigo */
BEGIN
  FOR t IN (SELECT N.ID, D.FECHADECLARACION FROM TBNOTIFICACION N, TBDECLARACIONES D
            WHERE D.ID = N.ID_DECLARACION) LOOP
     DECLARE
       xCodigo NUMBER(1);
     BEGIN
       IF TRUNC(t.FECHADECLARACION, 'DD') >= TRUNC(TO_DATE('02/07/2012', 'DD/MM/YYYY'), 'DD') THEN
         xCodigo := 1;
       ELSE
         xCodigo := 0;
       END IF;
       UPDATE TBNOTIFICACION SET TIPOCODIGOACTO = xCodigo WHERE ID = t.ID;
     END;
  END LOOP;
  COMMIT;
END;
/

/* Actualizar registros de TBNOTIFICACION con el tipo de codigo */
BEGIN
  FOR t IN (SELECT N.ID, D.FECHADECLARACION FROM TBNOTIFICACION N, TBDECLARACIONES D
            WHERE D.ID = N.ID_DECLARACION) LOOP
     DECLARE
       xCodigo NUMBER(1);
     BEGIN
       IF TRUNC(t.FECHADECLARACION, 'DD') >= TRUNC(TO_DATE('02/07/2012', 'DD/MM/YYYY'), 'DD') THEN
         xCodigo := 1;
       ELSE
         xCodigo := 0;
       END IF;
       UPDATE TBNOTIFICACION SET TIPOCODIGOACTO = xCodigo WHERE ID = t.ID;
     END;
  END LOOP;
  COMMIT;
END;
/

BEGIN
  DBMS_SCHEDULER.DROP_JOB('"Job_CambiaEstadoNotificacion"');
  DBMS_SCHEDULER.CREATE_JOB('"Job_CambiaEstadoNotificacion"'
                          , JOB_TYPE => 'STORED_PROCEDURE'
                          , JOB_ACTION => '"RUV"."PKG_NOTIFICACION"."SP_PROCESARNOTIFICACIONES"'
                          , NUMBER_OF_ARGUMENTS => 0
                          , START_DATE => TRUNC(SYSDATE, 'DD')
                          , REPEAT_INTERVAL => 'FREQ=Daily'
                          , END_DATE => NULL
                          , JOB_CLASS => '"DEFAULT_JOB_CLASS"'
                          , ENABLED => TRUE
                          , AUTO_DROP => FALSE
                          , COMMENTS => 'Procesar notificaciones vencidas de acuerdo a los estados');
  COMMIT;
END;
/