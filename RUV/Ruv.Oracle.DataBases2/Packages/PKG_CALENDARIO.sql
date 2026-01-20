-- Create Package CALENDARIO
create or replace PACKAGE PKG_CALENDARIO AS

  PROCEDURE sp_InsertarFestivo(pi_Fecha IN DATE, pi_Nombre IN NVARCHAR2, pi_Descripcion IN NVARCHAR2, pi_Recurrente IN NUMBER, po_Resultado OUT NUMBER);

  PROCEDURE sp_BorrarFestivo(p_Id IN NUMBER);

  PROCEDURE sp_CalcularDiasHabiles(pi_FechaInicio IN DATE, pi_NumeroDias IN NUMBER, pi_ContarCero IN NUMBER, po_Resultado OUT DATE);

  PROCEDURE sp_ConsultarFestivos(pi_Ano IN VARCHAR2, po_Cursor OUT SYS_REFCURSOR);
  
  FUNCTION fn_DiaDeSemana(pi_Fecha IN DATE) RETURN NUMBER;

END PKG_CALENDARIO;
/

-- Create package body
create or replace PACKAGE BODY PKG_CALENDARIO AS

  PROCEDURE sp_InsertarFestivo(pi_Fecha IN DATE, pi_Nombre IN NVARCHAR2, pi_Descripcion IN NVARCHAR2, pi_Recurrente IN NUMBER, po_Resultado OUT NUMBER) IS
  BEGIN
    -- Sobreescribir registros actuales
    DELETE FROM TBFESTIVO
    WHERE (DIA = TO_CHAR(pi_Fecha, 'DD') AND MES = TO_CHAR(pi_Fecha, 'MM') AND RECURRENTE = 1)
       OR (FECHA = pi_Fecha)
       OR (DIA = TO_CHAR(pi_Fecha, 'DD') AND MES = TO_CHAR(pi_Fecha, 'MM') AND pi_Recurrente = 1);
      
    INSERT INTO TBFESTIVO (ID, DIA, MES, ANO, FECHA, NOMBRE, COMENTARIO, RECURRENTE)
    VALUES (SEQ_TTBFESTIVO.NextVal, TO_CHAR(pi_Fecha, 'DD'), TO_CHAR(pi_Fecha, 'MM'), TO_CHAR(pi_Fecha, 'YYYY'), TRUNC(pi_Fecha, 'DD'), pi_Nombre, pi_Descripcion, pi_Recurrente)
    RETURNING ID INTO po_Resultado;
  END;
  
   PROCEDURE sp_BorrarFestivo(p_Id IN NUMBER) IS
  BEGIN
    DELETE FROM TBFESTIVO WHERE ID = p_Id;
  END;
 
  PROCEDURE sp_CalcularDiasHabiles(pi_FechaInicio IN DATE, pi_NumeroDias IN NUMBER, pi_ContarCero IN NUMBER, po_Resultado OUT DATE) IS
    -- Configuracion
    xSkipSaturdays NUMBER := 1;
    xSkipSundays   NUMBER := 1;
    xMaxYearRange  NUMBER := 5;
    -- Variables de operacion
    TYPE DATEARRAY IS VARRAY(10000) OF DATE;
    xRangeHolidays DATEARRAY;
    xDayCounter    NUMBER := 0;
  BEGIN
    -- Obtener :xMaxYearRange años de festivos 
    SELECT DISTINCT(FECHA) BULK COLLECT INTO xRangeHolidays FROM (
      -- Festivos no recurrentes
      SELECT FECHA FROM TBFESTIVO WHERE RECURRENTE = 0 AND FECHA <= TRUNC(ADD_MONTHS(pi_FechaInicio, xMaxYearRange * 12), 'YYYY') - 1
      UNION ALL
      -- Festivos Recurrentes
      SELECT TO_DATE(FST.DIA || '/' || FST.MES || '/' || REC.RANGEYEAR, 'DD/MM/YYYY') FROM (
        SELECT TO_NUMBER(TO_CHAR(pi_FechaInicio, 'YYYY')) + (ROWNUM - 1) AS RANGEYEAR FROM DUAL
        CONNECT BY LEVEL < xMaxYearRange) REC, TBFESTIVO FST
        WHERE FST.RECURRENTE = 1
    ) WHERE FECHA >= pi_FechaInicio
    ORDER BY FECHA;
    
    po_Resultado := TRUNC(pi_FechaInicio, 'DD');
    xDayCounter := CASE WHEN pi_ContarCero <> 0 THEN -1 ELSE 0 END; -- Contar un dia mas (se cuenta el dia cero desde el siguiente día habil)
    
    WHILE xDayCounter < pi_NumeroDias LOOP
      DECLARE
        xIsWeekend BOOLEAN := FALSE;
        xIsHoliday BOOLEAN := FALSE;
      BEGIN
        po_Resultado := po_Resultado + 1;
        xIsWeekend := CASE WHEN (fn_DiaDeSemana(po_Resultado) = 6 AND xSkipSaturdays <> 0)
                             OR (fn_DiaDeSemana(po_Resultado) = 7 AND xSkipSundays   <> 0) THEN TRUE ELSE FALSE END;
        
        IF NOT xIsWeekend THEN
          IF xRangeHolidays.Count > 0 THEN
            FOR i IN xRangeHolidays.First..xRangeHolidays.Last LOOP
              IF xRangeHolidays(i) = po_Resultado THEN
                xIsHoliday := TRUE;
              ELSIF xRangeHolidays(i) > po_Resultado THEN
                EXIT;
              END IF;
            END LOOP;
          END IF;
        END IF;
        
        IF NOT xIsHoliday AND NOT xIsWeekend THEN
          xDayCounter := xDayCounter + 1;
        END IF;
      END;
    END LOOP;
    
  END;
  
  PROCEDURE sp_ConsultarFestivos(pi_Ano IN VARCHAR2, po_Cursor OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID, DIA, MES, ANO, FECHA, NOMBRE, COMENTARIO, RECURRENTE FROM TBFESTIVO
      WHERE ANO = pi_Ano AND RECURRENTE = 0
      UNION ALL
      SELECT ID, DIA, MES, pi_Ano AS ANO, TO_DATE(DIA || '/' || MES || '/' || pi_Ano, 'DD/MM/YYYY') AS FECHA, NOMBRE, COMENTARIO, RECURRENTE FROM TBFESTIVO
      WHERE RECURRENTE = 1 and ANO <= pi_Ano
      ORDER BY FECHA;
  END;

  FUNCTION fn_DiaDeSemana(pi_Fecha IN DATE) RETURN NUMBER IS
  BEGIN
    RETURN 1 + TRUNC(pi_Fecha) - TRUNC(pi_Fecha, 'IW');
  END;

END;
/