--Se valida que la operación de glosas este asociada al perfil de Digitación
--de ser así se elimina la relación, para que el boton "Devolver" no se 
--habilitado en el modulo de Digitación.
DECLARE
  CONTADOR int := 0;
  IDROL varchar2(25);
  IDPROCESO varchar2(25);
BEGIN
    SELECT ID_PROCESO INTO IDPROCESO FROM TBPROCESOS_REPORTES WHERE UPPER(NOMBRE_PROCESO) LIKE 'GLOSAS';
    DBMS_OUTPUT.put_line ('Id del proceso GLOSAS es ' || IDPROCESO);
    SELECT ID INTO IDROL FROM TBROLES WHERE UPPER(NOMBRE) LIKE 'RUV DIGITADOR';
    DBMS_OUTPUT.put_line ('Id del Rol RUV DIGITADOR es ' || IDROL);
    SELECT COUNT(*) INTO CONTADOR 
    FROM TBROLES_OPCIONESACCIONES
    WHERE ID_ROL = IDROL AND ID_OPCIONACCION = IDPROCESO;
    IF CONTADOR > 0 THEN
      DBMS_OUTPUT.put_line ('La operacón Glosas se encuentra relacionada con el perfil Digitación');
      SELECT ID_PROCESO INTO IDPROCESO FROM TBPROCESOS_REPORTES WHERE UPPER(NOMBRE_PROCESO) LIKE 'GLOSAS';
      SELECT ID INTO IDROL FROM TBROLES WHERE UPPER(NOMBRE) LIKE 'RUV DIGITADOR';
      DELETE FROM TBROLES_OPCIONESACCIONES WHERE ID_ROL = IDROL AND ID_OPCIONACCION = IDPROCESO;
      DBMS_OUTPUT.put_line ('Relación Eliminada');
    ELSE 
      DBMS_OUTPUT.put_line ('No existe relación entre Glosas y Digitación');
    END IF;
END;