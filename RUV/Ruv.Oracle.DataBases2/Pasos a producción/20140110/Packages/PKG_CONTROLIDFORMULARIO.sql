-- Create new package
create or replace PACKAGE PKG_CONTROLIDFORMULARIO IS

  TYPE CURSOR_TYPE IS REF CURSOR;

  ESTADO_INACTIVO NUMBER := 1;
  ESTADO_ASIGNADO NUMBER := 2;
  ESTADO_IMPRENTA NUMBER := 3;
  ESTADO_GENERADO NUMBER := 4;
  ESTADO_RADICADO NUMBER := 5;
  ESTADO_DEVUELTO NUMBER := 6;

  PROCEDURE sp_ObtenerFormularios(po_Cursor OUT CURSOR_TYPE);

  PROCEDURE SP_GETFORMULARIOSNORADICADOS( PI_NUMEROFORMULARIO TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
                                          PI_IDPAIS TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
                                          PI_IDDEPARTAMENTO TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
                                          PI_IDMUNICIPIO TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
                                          PI_IDENTIDADMUNICIPIO TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
                                          PO_CURSOR OUT CURSOR_TYPE);

  PROCEDURE sp_ObtenerFormulariosPorEstado(pi_IdEstado NUMBER, po_Cursor OUT CURSOR_TYPE);

  PROCEDURE sp_AsignarFormulario(pi_NumeroFormulario VARCHAR2, pi_IdPais NUMBER, pi_IdDepartamento NUMBER, pi_IdMunicipio NUMBER, pi_IdEntidadMunicipio NUMBER, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER);

  PROCEDURE SP_ASIGNARFORMULARIOFILTRO
  (
    PI_NUMEROFORMULARIO   IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE             IN NUMBER DEFAULT NULL,
    PI_NHASTA             IN NUMBER DEFAULT NULL,
    PI_DGENERADO          IN DATE DEFAULT NULL,
    PI_IDUSUARIO          IN NUMBER,
    PI_IDPAIS             NUMBER,
    PI_IDDEPARTAMENTO     NUMBER,
    PI_IDMUNICIPIO        NUMBER,
    PI_IDENTIDADMUNICIPIO NUMBER,
    PO_RESULTADO          OUT SYS_REFCURSOR
  );
  
  PROCEDURE SP_INACTIVARFORMULARIO
  (
    PI_NIDFORMULARIO                TBIDENTIFICADORFORMULARIO.ID%TYPE,
    PI_OBSERVACION                  VARCHAR2,
    PO_IDIDENTIFICADORFORMULARIO    OUT TBIDENTIFICADORFORMULARIO.ID%TYPE
  );

  PROCEDURE sp_SepararImprenta(pi_NumeroFormulario VARCHAR2, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER);
  
  PROCEDURE SP_SEPARARIMPRENTAFILTRO
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    PI_IDUSUARIO        IN NUMBER,
    PO_RESULTADO        OUT SYS_REFCURSOR
  );

  PROCEDURE sp_MarcarGenerado(pi_NumeroFormulario VARCHAR2, pi_IdPais NUMBER, pi_IdDepartamento NUMBER, pi_IdMunicipio NUMBER, pi_IdEntidadMunicipio NUMBER, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER);
  
  PROCEDURE sp_MarcarRadicado(pi_NumeroFormulario VARCHAR2);
  
  PROCEDURE SP_UPDFORMULARIOFILTRO
  (
    PI_NUMEROFORMULARIO   IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE             IN NUMBER DEFAULT NULL,
    PI_NHASTA             IN NUMBER DEFAULT NULL,
    PI_DGENERADO          IN DATE DEFAULT NULL,
    PI_IDUSUARIO          IN NUMBER,
    PI_ESTADOESPERADO     IN NUMBER,
    PI_IDPAIS             NUMBER DEFAULT NULL,
    PI_IDDEPARTAMENTO     NUMBER DEFAULT NULL,
    PI_IDMUNICIPIO        NUMBER DEFAULT NULL,
    PI_IDENTIDADMUNICIPIO NUMBER DEFAULT NULL,
    PI_OBSERVACION        VARCHAR2 DEFAULT NULL,
    PO_RESULTADO          OUT SYS_REFCURSOR
  );

  PROCEDURE sp_IngresarActualizarForm
  (
    pi_NumeroFormulario           VARCHAR2,
    pi_IdPais                     NUMBER,
    pi_IdDepartamento             NUMBER,
    pi_IdMunicipio                NUMBER,
    pi_IdEntidadMunicipio         NUMBER,
    pi_IdEstadoIdFormulario       NUMBER,
    pi_IdUsuario                  NUMBER,
    PI_OBSERVACION                VARCHAR2,
    po_IdIdentificadorFormulario  OUT NUMBER
  );

  PROCEDURE sp_GenerarFormularios(pi_Cantidad NUMBER,
                                  pi_Serie VARCHAR2,
                                  pi_IdUsuario NUMBER,
                                  pi_IdEstado NUMBER,
                                  pi_IdPais NUMBER DEFAULT NULL,
                                  pi_IdDepartamento NUMBER DEFAULT NULL,
                                  pi_IdMunicipio NUMBER DEFAULT NULL,
                                  pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                  po_Formularios OUT CURSOR_TYPE);

  /*-------------------------------------------------------
    Purpose : Procedimiento para Gestion de Formularios WEB
    Author  : John Henao
    Fecha   : 7/6/2013
  ------------------------------------------------------- 
  */

   PROCEDURE sp_GenerarFormulariosWEB(pi_Cantidad NUMBER,
                                      pi_Serie VARCHAR2,
                                      pi_IdUsuario NUMBER,
                                      pi_IdEstado NUMBER,
                                      pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                      po_Formularios OUT CURSOR_TYPE);

  /*-------------------------------------------------------
      Purpose : Procedimiento para Obtener el pais que esta generando los formularios WEB
      Author  : John Henao
      Fecha   : 7/6/2013
  -------------------------------------------------------- 
  */
  PROCEDURE sp_ObtienePaisGenerFormuWEB(pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                        Po_IdPais OUT NUMBER);

  PROCEDURE sp_ObtenerFrmPorUsuario(pi_IdUsuario NUMBER,
                                    po_Formularios OUT CURSOR_TYPE);

  PROCEDURE sp_MarcarDescargado(PI_NIDFORMULARIO TBIDENTIFICADORFORMULARIO.ID%TYPE,
                                PO_IDIDENTIFICADORFORMULARIO OUT TBIDENTIFICADORFORMULARIO.ID%TYPE);

  PROCEDURE sp_ObtenerFrmPorNumero(pi_NumeroFormulario VARCHAR2, po_Cursor OUT CURSOR_TYPE);

  /*	DESCRIPCION: 
  **	FECHA: 
  **	CAMBIOS:
  **		20130125 - JAIRO VALDERRAMA
  **		1. SE ADICIONAN COLUMNAS PI_NUMEROFORMULARIO, PI_NDESDE, PI_NHASTA, PI_DGENERADO
  **    CON EL FIN DE REALIZAR FILTRO DE BUSQUEDA SOLICITDADO
  **    20130128 - JAIRO VALDERRAMA
  **    1. SE ADICIONA COLUMNA DGENERADO AL RESULTADO DE LA CONSULTA
  */
  PROCEDURE sp_ObtenerFrmsPaginado
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    pi_IdEstado         IN NUMBER,
    pi_IdUsuario        IN NUMBER,
    pi_PageNumber       IN NUMBER,
    pi_PageSize         IN NUMBER,
    Po_Resultado        OUT SYS_REFCURSOR
  );

  /*	DESCRIPCION: 
  **	FECHA: 
  **	CAMBIOS:
  **		20130125 - JAIRO VALDERRAMA
  **		1. SE ADICIONAN COLUMNAS PI_NUMEROFORMULARIO, PI_NDESDE, PI_NHASTA, PI_DGENERADO
  **    CON EL FIN DE CONTAR LA CANTIDAD DE RESULTADOS EN EL FILTRO DE BUSQUEDA
  */
  PROCEDURE sp_ObtenerFrmsCantidad
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    pi_IdEstado         IN NUMBER,
    pi_IdUsuario        IN NUMBER,
    po_RecordCount      OUT NUMBER
  );

  PROCEDURE sp_ObtenerFormulariosPaginado
  (
    pi_NumeroFormulario     TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
    pi_IdPais               TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
    pi_IdDepartamento       TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
    pi_IdMunicipio          TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
    pi_IdEntidadmunicipio   TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
    pi_Accion               NUMBER,
    pi_PageNumber           NUMBER,
    pi_PageSize             NUMBER,
    Po_Resultado            OUT SYS_REFCURSOR
  );

  PROCEDURE sp_ObtenerFormulariosCantidad
  (
    pi_NumeroFormulario     TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
    pi_IdPais               TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
    pi_IdDepartamento       TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
    pi_IdMunicipio          TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
    pi_IdEntidadmunicipio   TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
    pi_Accion               NUMBER,
    po_RecordCount          OUT NUMBER
  );

  function f_generaNroFrmGenerico
    (
         P_SERIE VARCHAR2
    )
    return varchar2;

  function f_reducir
    (
         P_CADENA varchar2
    )
    return number;

END PKG_CONTROLIDFORMULARIO;
/

-- Create package body
create or replace PACKAGE BODY PKG_CONTROLIDFORMULARIO IS

  PROCEDURE sp_ObtenerFormularios(po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN PO_Cursor FOR
      SELECT IT.ID                    AS ID
           , IT.NUMEROFORMULARIO      AS NUMEROFORMULARIO
           , IT.ID_PAIS               AS ID_PAIS
           , PP.NOMBRE                AS NOMBREPAIS
           , IT.ID_DEPARTAMENTO       AS ID_DEPARTAMENTO
           , DP.NOMBRE                AS NOMBREDEPARTAMENTO
           , IT.ID_MUNICIPIO          AS ID_MUNICIPIO
           , MP.NOMBRE                AS NOMBREMUNICIPIO
           , IT.ID_ENTIDADMUNICIPIO   AS ID_ENTIDADMUNICIPIO
           , EP.NOMBRE                AS NOMBREENTIDADMUNICIPIO
           , IT.ID_ESTADOIDFORMULARIO AS ID_ESTADOIDFORMULARIO
           , ET.NOMBRE                AS NOMBREESTADOIDFORMULARIO
           , IT.ID_USUARIO            AS ID_USUARIO
           , UM.USUARIO               AS NOMBREUSUARIO
           , UM.NOMBRE                AS NOMBRECOMPLETOUSUARIO
      FROM TBIDENTIFICADORFORMULARIO IT
      INNER JOIN      TBESTADOIDFORMULARIO         ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
      LEFT OUTER JOIN TBGEOGRAFIA                  PP ON PP.ID = IT.ID_PAIS
      LEFT OUTER JOIN TBGEOGRAFIA                  DP ON DP.ID = IT.ID_DEPARTAMENTO
      LEFT OUTER JOIN TBGEOGRAFIA                  MP ON MP.ID = IT.ID_MUNICIPIO
      LEFT OUTER JOIN TBENTIDADMUNICIPIO           EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
      LEFT OUTER JOIN TBUSUARIOS                   UM ON UM.ID = IT.ID_USUARIO;
  END;

  PROCEDURE SP_GETFORMULARIOSNORADICADOS
  (
    PI_NUMEROFORMULARIO TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
                                          PI_IDPAIS TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
                                          PI_IDDEPARTAMENTO TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
                                          PI_IDMUNICIPIO TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
                                          PI_IDENTIDADMUNICIPIO TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
                                          PO_CURSOR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN PO_CURSOR FOR
      SELECT IT.ID
           , IT.NUMEROFORMULARIO
           , IT.ID_PAIS
           , PP.NOMBRE                AS NOMBREPAIS
           , IT.ID_DEPARTAMENTO
           , DP.NOMBRE                AS NOMBREDEPARTAMENTO
           , IT.ID_MUNICIPIO
           , MP.NOMBRE                AS NOMBREMUNICIPIO
           , IT.ID_ENTIDADMUNICIPIO
           , EP.NOMBRE                AS NOMBREENTIDADMUNICIPIO
           , IT.ID_ESTADOIDFORMULARIO
           , ET.NOMBRE                AS NOMBREESTADOIDFORMULARIO
           , IT.ID_USUARIO
           , UM.USUARIO               AS NOMBREUSUARIO
           , UM.NOMBRE                AS NOMBRECOMPLETOUSUARIO
      FROM TBIDENTIFICADORFORMULARIO IT
      INNER JOIN      TBESTADOIDFORMULARIO         ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
      LEFT OUTER JOIN TBGEOGRAFIA                  PP ON PP.ID = IT.ID_PAIS
      LEFT OUTER JOIN TBGEOGRAFIA                  DP ON DP.ID = IT.ID_DEPARTAMENTO
      LEFT OUTER JOIN TBGEOGRAFIA                  MP ON MP.ID = IT.ID_MUNICIPIO
      LEFT OUTER JOIN TBENTIDADMUNICIPIO           EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
      LEFT OUTER JOIN TBUSUARIOS                   UM ON UM.ID = IT.ID_USUARIO
      WHERE   UPPER(IT.NUMEROFORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(IT.NUMEROFORMULARIO)) || '%'
        AND   NVL(IT.ID_PAIS, -1) = NVL(PI_IDPAIS, NVL(IT.ID_PAIS, -1))
        AND   NVL(IT.ID_DEPARTAMENTO, -1) = NVL(PI_IDDEPARTAMENTO, NVL(IT.ID_DEPARTAMENTO, -1))
        AND   NVL(IT.ID_MUNICIPIO, -1) = NVL(PI_IDMUNICIPIO, NVL(IT.ID_MUNICIPIO, -1))
        AND   NVL(IT.ID_ENTIDADMUNICIPIO, -1) = NVL(PI_IDENTIDADMUNICIPIO, NVL(IT.ID_ENTIDADMUNICIPIO, -1))
        AND   IT.NUMEROFORMULARIO NOT IN(
                  SELECT  NRO_FORMULARIO
                  FROM    TBRADICACION
                  WHERE   UPPER(NRO_FORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(NRO_FORMULARIO)) || '%'
              );
  END;

  PROCEDURE sp_ObtenerFormulariosPorEstado(pi_IdEstado NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN PO_Cursor FOR
      SELECT IT.ID                    AS ID
           , IT.NUMEROFORMULARIO      AS NUMEROFORMULARIO
           , IT.ID_PAIS               AS ID_PAIS
           , PP.NOMBRE                AS NOMBREPAIS
           , IT.ID_DEPARTAMENTO       AS ID_DEPARTAMENTO
           , DP.NOMBRE                AS NOMBREDEPARTAMENTO
           , IT.ID_MUNICIPIO          AS ID_MUNICIPIO
           , MP.NOMBRE                AS NOMBREMUNICIPIO
           , IT.ID_ENTIDADMUNICIPIO   AS ID_ENTIDADMUNICIPIO
           , EP.NOMBRE                AS NOMBREENTIDADMUNICIPIO
           , IT.ID_ESTADOIDFORMULARIO AS ID_ESTADOIDFORMULARIO
           , ET.NOMBRE                AS NOMBREESTADOIDFORMULARIO
           , IT.ID_USUARIO            AS ID_USUARIO
           , UM.USUARIO               AS NOMBREUSUARIO
           , UM.NOMBRE                AS NOMBRECOMPLETOUSUARIO
      FROM TBIDENTIFICADORFORMULARIO IT
      INNER JOIN      TBESTADOIDFORMULARIO         ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
      LEFT OUTER JOIN TBGEOGRAFIA                  PP ON PP.ID = IT.ID_PAIS
      LEFT OUTER JOIN TBGEOGRAFIA                  DP ON DP.ID = IT.ID_DEPARTAMENTO
      LEFT OUTER JOIN TBGEOGRAFIA                  MP ON MP.ID = IT.ID_MUNICIPIO
      LEFT OUTER JOIN TBENTIDADMUNICIPIO           EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
      LEFT OUTER JOIN TBUSUARIOS                   UM ON UM.ID = IT.ID_USUARIO
      WHERE ID_ESTADOIDFORMULARIO = PI_IdEstado;
  END;

  PROCEDURE sp_AsignarFormulario(pi_NumeroFormulario VARCHAR2, pi_IdPais NUMBER, pi_IdDepartamento NUMBER, pi_IdMunicipio NUMBER, pi_IdEntidadMunicipio NUMBER, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER) IS
    vIdIdentificadorFormulario NUMBER;
  BEGIN
    sp_IngresarActualizarForm(pi_NumeroFormulario, pi_IdPais, pi_IdDepartamento, pi_IdMunicipio, pi_IdEntidadMunicipio, PKG_CONTROLIDFORMULARIO.ESTADO_ASIGNADO, pi_IdUsuario, NULL, vIdIdentificadorFormulario);
    po_IdIdentificadorFormulario := vIdIdentificadorFormulario;
  END;

  PROCEDURE SP_ASIGNARFORMULARIOFILTRO
  (
    PI_NUMEROFORMULARIO   IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE             IN NUMBER DEFAULT NULL,
    PI_NHASTA             IN NUMBER DEFAULT NULL,
    PI_DGENERADO          IN DATE DEFAULT NULL,
    PI_IDUSUARIO          IN NUMBER,
    PI_IDPAIS             NUMBER,
    PI_IDDEPARTAMENTO     NUMBER,
    PI_IDMUNICIPIO        NUMBER,
    PI_IDENTIDADMUNICIPIO NUMBER,
    PO_RESULTADO          OUT SYS_REFCURSOR
  ) IS
  BEGIN
    SP_UPDFORMULARIOFILTRO(PI_NUMEROFORMULARIO, PI_NDESDE, PI_NHASTA, PI_DGENERADO, PI_IDUSUARIO, ESTADO_ASIGNADO, PI_IDPAIS, PI_IDDEPARTAMENTO, PI_IDMUNICIPIO, PI_IDENTIDADMUNICIPIO, NULL, PO_RESULTADO);
  END;

  PROCEDURE SP_INACTIVARFORMULARIO
  (
    PI_NIDFORMULARIO                TBIDENTIFICADORFORMULARIO.ID%TYPE,
    PI_OBSERVACION                  VARCHAR2,
    PO_IDIDENTIFICADORFORMULARIO    OUT TBIDENTIFICADORFORMULARIO.ID%TYPE
  )
  IS
    CNUMEROFORMULARIO           TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE;
    NIDUSUARIO                  TBIDENTIFICADORFORMULARIO.ID_USUARIO%TYPE;
    VIDIDENTIFICADORFORMULARIO  TBIDENTIFICADORFORMULARIO.ID%TYPE;
    V_ID_PAIS                       TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE;
    V_ID_DEPARTAMENTO               TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE;
    V_ID_MUNICIPIO                  TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE;
    V_ID_ENTIDADMUNICIPIO           TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE;
    v_ID_ESTADOFORMULARIO           TBIDENTIFICADORFORMULARIO.ID_ESTADOIDFORMULARIO%TYPE;
  BEGIN
    SELECT  NUMEROFORMULARIO,
            ID_USUARIO,
            ID_PAIS,
            ID_DEPARTAMENTO,
            ID_MUNICIPIO,
            ID_ENTIDADMUNICIPIO,
            ID_ESTADOIDFORMULARIO
    INTO    CNUMEROFORMULARIO,
            NIDUSUARIO,
            V_ID_PAIS,
            V_ID_DEPARTAMENTO,
            V_ID_MUNICIPIO,
            V_ID_ENTIDADMUNICIPIO,
            v_ID_ESTADOFORMULARIO
    FROM    TBIDENTIFICADORFORMULARIO
    WHERE   ID = PI_NIDFORMULARIO;

    IF v_ID_ESTADOFORMULARIO = PKG_CONTROLIDFORMULARIO.ESTADO_INACTIVO THEN
      IF V_ID_ENTIDADMUNICIPIO IS NULL THEN
        v_ID_ESTADOFORMULARIO := PKG_CONTROLIDFORMULARIO.ESTADO_GENERADO;
      ELSE
        v_ID_ESTADOFORMULARIO := PKG_CONTROLIDFORMULARIO.ESTADO_ASIGNADO;
      END IF;
    ELSE
      v_ID_ESTADOFORMULARIO := PKG_CONTROLIDFORMULARIO.ESTADO_INACTIVO;
    END IF;

    SP_INGRESARACTUALIZARFORM(CNUMEROFORMULARIO, V_ID_PAIS, V_ID_DEPARTAMENTO, V_ID_MUNICIPIO, V_ID_ENTIDADMUNICIPIO, v_ID_ESTADOFORMULARIO, NIDUSUARIO, PI_OBSERVACION, VIDIDENTIFICADORFORMULARIO);

    PO_IDIDENTIFICADORFORMULARIO := VIDIDENTIFICADORFORMULARIO;
  END;

  PROCEDURE sp_SepararImprenta(pi_NumeroFormulario VARCHAR2, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER) IS
    vIdIdentificadorFormulario NUMBER;
  BEGIN
    sp_IngresarActualizarForm(pi_NumeroFormulario, NULL, NULL, NULL, NULL, PKG_CONTROLIDFORMULARIO.ESTADO_IMPRENTA, pi_IdUsuario, NULL, vIdIdentificadorFormulario);
    po_IdIdentificadorFormulario := vIdIdentificadorFormulario;
  END;

  PROCEDURE SP_SEPARARIMPRENTAFILTRO
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    PI_IDUSUARIO        IN NUMBER,
    PO_RESULTADO        OUT SYS_REFCURSOR
  ) IS
  BEGIN
    SP_UPDFORMULARIOFILTRO(PI_NUMEROFORMULARIO, PI_NDESDE, PI_NHASTA, PI_DGENERADO, PI_IDUSUARIO, ESTADO_IMPRENTA, NULL, NULL, NULL, NULL, NULL, PO_RESULTADO);
  END;

  PROCEDURE sp_MarcarGenerado(pi_NumeroFormulario VARCHAR2, pi_IdPais NUMBER, pi_IdDepartamento NUMBER, pi_IdMunicipio NUMBER, pi_IdEntidadMunicipio NUMBER, pi_IdUsuario NUMBER, po_IdIdentificadorFormulario OUT NUMBER) IS
    vIdIdentificadorFormulario NUMBER;
  BEGIN
    sp_IngresarActualizarForm(pi_NumeroFormulario, pi_IdPais, pi_IdDepartamento, pi_IdMunicipio, pi_IdEntidadMunicipio, PKG_CONTROLIDFORMULARIO.ESTADO_GENERADO, pi_IdUsuario, NULL, vIdIdentificadorFormulario);
    po_IdIdentificadorFormulario := vIdIdentificadorFormulario;
  END;

  PROCEDURE SP_UPDFORMULARIOFILTRO
  (
    PI_NUMEROFORMULARIO   IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE             IN NUMBER DEFAULT NULL,
    PI_NHASTA             IN NUMBER DEFAULT NULL,
    PI_DGENERADO          IN DATE DEFAULT NULL,
    PI_IDUSUARIO          IN NUMBER,
    PI_ESTADOESPERADO     IN NUMBER,
    PI_IDPAIS             NUMBER DEFAULT NULL,
    PI_IDDEPARTAMENTO     NUMBER DEFAULT NULL,
    PI_IDMUNICIPIO        NUMBER DEFAULT NULL,
    PI_IDENTIDADMUNICIPIO NUMBER DEFAULT NULL,
    PI_OBSERVACION        VARCHAR2 DEFAULT NULL,
    PO_RESULTADO          OUT SYS_REFCURSOR
  ) IS
    VFORMULARIOS        NUMBERARRAY;
    I                   NUMBER;
    FECHA_INVALIDA      EXCEPTION;
    RANGO_INVALIDO      EXCEPTION;
    GEOGRAFIA_INVALIDA  EXCEPTION;
  BEGIN
    IF PI_DGENERADO > SYSDATE THEN
      RAISE FECHA_INVALIDA;
    END IF;

    IF (PI_NDESDE IS NULL AND PI_NHASTA IS NOT NULL) OR PI_NDESDE > PI_NHASTA THEN
      RAISE RANGO_INVALIDO;
    END IF;

    VFORMULARIOS := NUMBERARRAY();

    FOR C IN
    (
        SELECT  IT.ID,
                IT.ID_ESTADOIDFORMULARIO
        FROM    TBIDENTIFICADORFORMULARIO IT
        WHERE   IT.NUMEROFORMULARIO LIKE NVL(PI_NUMEROFORMULARIO || '%', IT.NUMEROFORMULARIO)
              AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3)) >= NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3)))
              AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3)) <= NVL(PI_NHASTA, NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3))))
              AND IT.DGENERADO = NVL(PI_DGENERADO, IT.DGENERADO)
              AND IT.ID_USUARIO = PI_IDUSUARIO
    ) LOOP
      /* SOLO ACTUALIZAR SI SU ESTADO ES GENERADO */
      IF C.ID_ESTADOIDFORMULARIO = ESTADO_GENERADO THEN
        VFORMULARIOS.EXTEND;
        VFORMULARIOS(VFORMULARIOS.COUNT) := C.ID;
      END IF;
    END LOOP;

    /* ACCION */
    IF PI_ESTADOESPERADO = ESTADO_IMPRENTA THEN
      UPDATE TBIDENTIFICADORFORMULARIO
      SET ID_ESTADOIDFORMULARIO = ESTADO_IMPRENTA
      WHERE ID IN (SELECT * FROM TABLE(VFORMULARIOS));

      /* ACTUALIZAR EL HISTORICO DEL FORULARIO */
      I := VFORMULARIOS.FIRST;
      LOOP
        INSERT INTO TBIDFORMULARIOHISTORICO (ID, ID_IDENTIFICADORFORMULARIO, ID_ESTADOANTERIOR, ID_ESTADONUEVO, ID_USUARIO)
        VALUES (SEQ_TBIDFORMULARIOHISTORICO.NextVal, VFORMULARIOS(I), ESTADO_GENERADO, ESTADO_IMPRENTA, PI_IDUSUARIO);
        I := VFORMULARIOS.NEXT(I);
        EXIT WHEN I IS NULL;
      END LOOP;
    ELSIF PI_ESTADOESPERADO = ESTADO_ASIGNADO THEN
      IF  PI_IDPAIS IS NULL OR
          PI_IDDEPARTAMENTO IS NULL OR
          PI_IDMUNICIPIO IS NULL OR
          PI_IDENTIDADMUNICIPIO IS NULL THEN
        RAISE GEOGRAFIA_INVALIDA;
      ELSE
        UPDATE TBIDENTIFICADORFORMULARIO
        SET ID_ESTADOIDFORMULARIO = ESTADO_ASIGNADO,
            ID_PAIS               = PI_IDPAIS,
            ID_DEPARTAMENTO       = PI_IDDEPARTAMENTO,
            ID_MUNICIPIO          = PI_IDMUNICIPIO,
            ID_ENTIDADMUNICIPIO   = PI_IDENTIDADMUNICIPIO
        WHERE ID IN (SELECT * FROM TABLE(VFORMULARIOS));

        /* ACTUALIZAR EL HISTORICO DEL FORULARIO */
        I := VFORMULARIOS.FIRST;
        LOOP
          INSERT INTO TBIDFORMULARIOHISTORICO (ID, ID_IDENTIFICADORFORMULARIO, ID_ESTADOANTERIOR, ID_ESTADONUEVO, ID_ENTIDADMUNICIPIONUEVO, ID_USUARIO)
          VALUES (SEQ_TBIDFORMULARIOHISTORICO.NextVal, VFORMULARIOS(I), ESTADO_GENERADO, ESTADO_ASIGNADO, PI_IDENTIDADMUNICIPIO, PI_IDUSUARIO);
          I := VFORMULARIOS.NEXT(I);
          EXIT WHEN I IS NULL;
        END LOOP;
      END IF;
    END IF;

    /* DEVOLVER LISTA */
    OPEN PO_RESULTADO FOR
      SELECT IT.ID,
             IT.NUMEROFORMULARIO
      FROM TBIDENTIFICADORFORMULARIO IT
      WHERE IT.ID IN (SELECT * FROM TABLE(VFORMULARIOS));

    EXCEPTION
    WHEN FECHA_INVALIDA THEN
      RAISE_APPLICATION_ERROR(-20000, 'La fecha de generación del formulario a consultar no puede ser mayor que la actual');
    WHEN RANGO_INVALIDO THEN
      RAISE_APPLICATION_ERROR(-20001, 'El rango ingresado para la búsqueda es inválido');
    WHEN GEOGRAFIA_INVALIDA THEN
      RAISE_APPLICATION_ERROR(-20002, 'No se pueden distribuir los documentos sin una ubicación geográfica ingresada');
  END;

  PROCEDURE sp_IngresarActualizarForm
  (
    pi_NumeroFormulario           VARCHAR2,
    pi_IdPais                     NUMBER,
    pi_IdDepartamento             NUMBER,
    pi_IdMunicipio                NUMBER,
    pi_IdEntidadMunicipio         NUMBER,
    pi_IdEstadoIdFormulario       NUMBER,
    pi_IdUsuario                  NUMBER,
    PI_OBSERVACION                VARCHAR2,
    po_IdIdentificadorFormulario  OUT NUMBER
  )
  IS
    vIdFormulario NUMBER;
  BEGIN

    SELECT  ID
    INTO    vIdFormulario
    FROM    TBIDENTIFICADORFORMULARIO
    WHERE   NUMEROFORMULARIO = pi_NumeroFormulario;

    IF (vIdFormulario IS NULL) THEN
      INSERT INTO TBIDENTIFICADORFORMULARIO (ID, NUMEROFORMULARIO, ID_PAIS, ID_DEPARTAMENTO, ID_MUNICIPIO, ID_ENTIDADMUNICIPIO, ID_ESTADOIDFORMULARIO, ID_USUARIO)
      VALUES (SEQ_TBIDENTIFICADORFORMULARIO.NextVal, pi_NumeroFormulario, pi_IdPais, pi_IdDepartamento, pi_IdMunicipio, pi_IdEntidadMunicipio, pi_IdEstadoIdFormulario, pi_IdUsuario)
      RETURNING ID INTO vIdFormulario;

      INSERT INTO TBIDFORMULARIOHISTORICO (ID, ID_IDENTIFICADORFORMULARIO, ID_ESTADONUEVO, ID_ENTIDADMUNICIPIONUEVO, ID_USUARIO, OBSERVACION)
      VALUES (SEQ_TBIDFORMULARIOHISTORICO.NextVal, vIdFormulario, pi_IdEstadoIdFormulario, pi_IdEntidadMunicipio, pi_IdUsuario, PI_OBSERVACION);

    ELSE
      DECLARE
        vEstadoActual NUMBER;
        vEntidadMunicipioActual NUMBER;
      BEGIN

        SELECT  ID_ESTADOIDFORMULARIO, ID_ENTIDADMUNICIPIO
        INTO    vEstadoActual, vEntidadMunicipioActual
        FROM    TBIDENTIFICADORFORMULARIO
        WHERE ID = vIdFormulario;

        INSERT INTO TBIDFORMULARIOHISTORICO (ID, ID_IDENTIFICADORFORMULARIO, ID_ESTADOANTERIOR, ID_ENTIDADMUNICIPIOANTERIOR, ID_ESTADONUEVO, ID_ENTIDADMUNICIPIONUEVO, ID_USUARIO, OBSERVACION)
        VALUES (SEQ_TBIDFORMULARIOHISTORICO.NextVal, vIdFormulario, vEstadoActual, vEntidadMunicipioActual, pi_IdEstadoIdFormulario, pi_IdEntidadMunicipio, pi_IdUsuario, PI_OBSERVACION);

        UPDATE  TBIDENTIFICADORFORMULARIO
        SET     ID_PAIS               = PI_IdPais,
                ID_DEPARTAMENTO       = PI_IdDepartamento,
                ID_MUNICIPIO          = PI_IdMunicipio,
                ID_ENTIDADMUNICIPIO   = PI_IdEntidadMunicipio,
                ID_ESTADOIDFORMULARIO = pi_IdEstadoIdFormulario
        WHERE   ID = vIdFormulario;

      END;
    END IF;
    po_IdIdentificadorFormulario := vIdFormulario;
  END;

  PROCEDURE sp_GenerarFormularios(pi_Cantidad NUMBER,
                                  pi_Serie VARCHAR2,
                                  pi_IdUsuario NUMBER,
                                  pi_IdEstado NUMBER,
                                  pi_IdPais NUMBER DEFAULT NULL,
                                  pi_IdDepartamento NUMBER DEFAULT NULL,
                                  pi_IdMunicipio NUMBER DEFAULT NULL,
                                  pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                  po_Formularios OUT CURSOR_TYPE) IS

    vContador NUMBER;
    vfirstId NUMBER;
    vCurrentId NUMBER;
    vFinalId NUMBER;
  BEGIN


    vfirstId := seq_tbidentificadorformulario.nextval;
    vCurrentId := vfirstId;

    for vContador in 1..pi_Cantidad loop
        --Inserta el registro en la tabla tbidentificadorformulario, marcado como generado
        INSERT INTO tbidentificadorformulario
            (id,
             NUMEROFORMULARIO,
             id_pais,
             id_departamento,
             id_municipio,
             id_entidadmunicipio,
             id_estadoidformulario,
             id_usuario)
        VALUES
            (vCurrentId,
             (SELECT f_generaNroFrmGenerico(pi_Serie) FROM dual),
             pi_IdPais,
             pi_IdDepartamento,
             pi_IdMunicipio,
             pi_IdEntidadmunicipio,
             pi_IdEstado,
             pi_IdUsuario);

        vCurrentId := seq_tbidentificadorformulario.nextval;
    END LOOP;

    vFinalId := vCurrentId;

    --Retorna los registros de los formularios recien creados
    OPEN po_Formularios FOR
         select
            idf.id,
            idf.numeroformulario,
            idf.id_pais,
            idf.id_departamento,
            idf.id_municipio,
            idf.id_entidadmunicipio,
            idf.id_estadoidformulario,
            idf.id_usuario
         from tbidentificadorformulario idf
         WHERE idf.id >= vfirstId and idf.id <= vFinalId;

    --No se atrapa excepci?n para que si hay alguna, esta se propague a la aplicaci?n y all? se controle e informe
    --EXCEPTION
    --WHEN OTHERS THEN
    --  ROLLBACK;
  END;

  /*-------------------------------------------------------
    Purpose : Procedimiento para Gestion de Formularios WEB
    Author  : John Henao
    Fecha   : 7/6/2013
  --------------------------------------------------------
  */

   PROCEDURE sp_GenerarFormulariosWEB(pi_Cantidad NUMBER,
                                      pi_Serie VARCHAR2,
                                      pi_IdUsuario NUMBER,
                                      pi_IdEstado NUMBER,
                                      pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                      po_Formularios OUT CURSOR_TYPE) IS

    vContador NUMBER;
    vfirstId NUMBER;
    vCurrentId NUMBER;
    vFinalId NUMBER;
    VPAIS NUMBER;
    VIDDEPARTAMENTO NUMBER;
    VIDMUNICIPIO NUMBER;
  BEGIN


    vfirstId := seq_tbidentificadorformulario.nextval;
    vCurrentId := vfirstId;
    SELECT ID_MUNICIPIO INTO VIDMUNICIPIO FROM TBENTIDADMUNICIPIO
    WHERE ID = pi_IdEntidadmunicipio;
    SELECT PADREID INTO VIDDEPARTAMENTO FROM TBGEOGRAFIA WHERE ID = VIDMUNICIPIO;
    SELECT PADREID INTO VPAIS FROM TBGEOGRAFIA WHERE ID = VIDDEPARTAMENTO;

    for vContador in 1..pi_Cantidad loop
        --Inserta el registro en la tabla tbidentificadorformulario, marcado como generado
        INSERT INTO tbidentificadorformulario
            (id,
             NUMEROFORMULARIO,
             id_pais,
             id_departamento,
             id_municipio,
             id_entidadmunicipio,
             id_estadoidformulario,
             id_usuario)
        VALUES
            (vCurrentId,
             (SELECT f_generaNroFrmGenerico(pi_Serie) FROM dual),
             VPAIS,
             VIDDEPARTAMENTO,
             VIDMUNICIPIO,
             pi_IdEntidadmunicipio,
             pi_IdEstado,
             pi_IdUsuario);

        vCurrentId := seq_tbidentificadorformulario.nextval;
    END LOOP;

    vFinalId := vCurrentId;

    --Retorna los registros de los formularios recien creados
    OPEN po_Formularios FOR
         select
            idf.id,
            idf.numeroformulario,
            idf.id_pais,
            idf.id_departamento,
            idf.id_municipio,
            idf.id_entidadmunicipio,
            idf.id_estadoidformulario,
            idf.id_usuario
         from tbidentificadorformulario idf
         WHERE idf.id >= vfirstId and idf.id <= vFinalId;

    --No se atrapa excepci?n para que si hay alguna, esta se propague a la aplicaci?n y all? se controle e informe
    --EXCEPTION
    --WHEN OTHERS THEN
    --  ROLLBACK;
  END;


/*-------------------------------------------------------
Purpose : Procedimiento para Obtener el pais que esta generando los formularios WEB
Author  : John Henao
Fecha   : 7/6/2013
--------------------------------------------------------
*/
PROCEDURE sp_ObtienePaisGenerFormuWEB(pi_IdEntidadmunicipio NUMBER DEFAULT NULL,
                                      Po_IdPais OUT NUMBER) IS



    VIDDEPARTAMENTO NUMBER;
    VIDMUNICIPIO NUMBER;
  BEGIN
    SELECT ID_MUNICIPIO INTO VIDMUNICIPIO FROM TBENTIDADMUNICIPIO
    WHERE ID = pi_IdEntidadmunicipio;
    SELECT PADREID INTO VIDDEPARTAMENTO FROM TBGEOGRAFIA WHERE ID = VIDMUNICIPIO;
    SELECT PADREID INTO Po_IdPais FROM TBGEOGRAFIA WHERE ID = VIDDEPARTAMENTO;
 END;


  PROCEDURE sp_ObtenerFrmPorUsuario(pi_IdUsuario NUMBER,
                                            po_Formularios OUT CURSOR_TYPE) IS
  BEGIN

    --Retorna los registros de los formularios recien creados
    OPEN po_Formularios FOR
          SELECT IT.ID                    AS ID
               , IT.NUMEROFORMULARIO      AS NUMEROFORMULARIO
               , IT.ID_PAIS               AS ID_PAIS
               , PP.NOMBRE                AS NOMBREPAIS
               , IT.ID_DEPARTAMENTO       AS ID_DEPARTAMENTO
               , DP.NOMBRE                AS NOMBREDEPARTAMENTO
               , IT.ID_MUNICIPIO          AS ID_MUNICIPIO
               , MP.NOMBRE                AS NOMBREMUNICIPIO
               , IT.ID_ENTIDADMUNICIPIO   AS ID_ENTIDADMUNICIPIO
               , EP.NOMBRE                AS NOMBREENTIDADMUNICIPIO
               , IT.ID_ESTADOIDFORMULARIO AS ID_ESTADOIDFORMULARIO
               , ET.NOMBRE                AS NOMBREESTADOIDFORMULARIO
               , IT.ID_USUARIO            AS ID_USUARIO
               , UM.USUARIO               AS NOMBREUSUARIO
               , UM.NOMBRE                AS NOMBRECOMPLETOUSUARIO
               , IT.DESCARGADO            AS DESCARGADO
          FROM TBIDENTIFICADORFORMULARIO IT
          INNER JOIN      TBESTADOIDFORMULARIO           ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
          LEFT OUTER JOIN TBGEOGRAFIA                    PP ON PP.ID = IT.ID_PAIS
          LEFT OUTER JOIN TBGEOGRAFIA                    DP ON DP.ID = IT.ID_DEPARTAMENTO
          LEFT OUTER JOIN TBGEOGRAFIA                    MP ON MP.ID = IT.ID_MUNICIPIO
          LEFT OUTER JOIN TBENTIDADMUNICIPIO             EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
          LEFT OUTER JOIN TBUSUARIOS                     UM ON UM.ID = IT.ID_USUARIO
          WHERE IT.id_usuario = pi_IdUsuario
          ORDER BY IT.DGENERADO DESC, IT.NUMEROFORMULARIO;

    --No se atrapa excepci?n para que si hay alguna, esta se propague a la aplicaci?n y all? se controle e informe
    --EXCEPTION
    --WHEN OTHERS THEN
    --  ROLLBACK;
  END;

  PROCEDURE sp_MarcarDescargado(PI_NIDFORMULARIO TBIDENTIFICADORFORMULARIO.ID%TYPE,
                                PO_IDIDENTIFICADORFORMULARIO OUT TBIDENTIFICADORFORMULARIO.ID%TYPE) IS

  BEGIN
    UPDATE tbidentificadorformulario idf SET idf.descargado = 1
    WHERE idf.id = PI_NIDFORMULARIO;
    SELECT ID INTO PO_IDIDENTIFICADORFORMULARIO FROM TBIDENTIFICADORFORMULARIO WHERE ID = PI_NIDFORMULARIO;
  END;

  PROCEDURE sp_MarcarRadicado(pi_NumeroFormulario VARCHAR2) IS
  BEGIN
    UPDATE TBIDENTIFICADORFORMULARIO SET ID_ESTADOIDFORMULARIO = ESTADO_RADICADO
    WHERE NUMEROFORMULARIO = pi_NumeroFormulario;
  END;

  PROCEDURE sp_ObtenerFrmPorNumero(pi_NumeroFormulario VARCHAR2, po_Cursor OUT CURSOR_TYPE) IS

  BEGIN

    OPEN po_Cursor FOR
         select
            idf.id,
            idf.numeroformulario,
            idf.id_pais,
            idf.id_departamento,
            idf.id_municipio,
            idf.id_entidadmunicipio,
            idf.id_estadoidformulario,
            idf.id_usuario,
            idf.descargado
         from tbidentificadorformulario idf
         WHERE idf.numeroformulario = pi_NumeroFormulario;

    --No se atrapa excepci?n para que si hay alguna, esta se propague a la aplicaci?n y all? se controle e informe
    --EXCEPTION
    --WHEN OTHERS THEN
    --  ROLLBACK;
  END;


  /***********************************************************
  * Procedure description: Obtiene los formularios paginados, por estado y/o usuario
  * Date:   30/10/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By     Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_ObtenerFrmsPaginado
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    pi_IdEstado         IN NUMBER,
    pi_IdUsuario        IN NUMBER,
    pi_PageNumber       IN NUMBER,
    pi_PageSize         IN NUMBER,
    Po_Resultado        OUT SYS_REFCURSOR
  )
  AS
    lowerBound INT;
    upperBound INT;
    FECHA_INVALIDA EXCEPTION;
    RANGO_INVALIDO EXCEPTION;
  BEGIN
    IF PI_DGENERADO > SYSDATE THEN
      RAISE FECHA_INVALIDA;
    END IF;

    IF (PI_NDESDE IS NULL AND PI_NHASTA IS NOT NULL) OR PI_NDESDE > PI_NHASTA THEN
      RAISE RANGO_INVALIDO;
    END IF;

    lowerBound := (pi_PageNumber * pi_PageSize) + 1;
    upperBound := ((pi_PageNumber - 1) * pi_PageSize) + 1;

    OPEN Po_Resultado FOR
    SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (
              SELECT       IT.ID AS ID,
                           IT.NUMEROFORMULARIO AS NUMEROFORMULARIO,
                           IT.ID_PAIS               AS ID_PAIS,
                           PP.NOMBRE                AS NOMBREPAIS,
                           IT.ID_DEPARTAMENTO       AS ID_DEPARTAMENTO,
                           DP.NOMBRE                AS NOMBREDEPARTAMENTO,
                           IT.ID_MUNICIPIO          AS ID_MUNICIPIO,
                           MP.NOMBRE                AS NOMBREMUNICIPIO,
                           IT.ID_ENTIDADMUNICIPIO   AS ID_ENTIDADMUNICIPIO,
                           EP.NOMBRE                AS NOMBREENTIDADMUNICIPIO,
                           IT.ID_ESTADOIDFORMULARIO AS ID_ESTADOIDFORMULARIO,
                           ET.NOMBRE                AS NOMBREESTADOIDFORMULARIO,
                           IT.ID_USUARIO            AS ID_USUARIO,
                           UM.USUARIO               AS NOMBREUSUARIO,
                           UM.NOMBRE                AS NOMBRECOMPLETOUSUARIO,
                           IT.DGENERADO
              FROM         TBIDENTIFICADORFORMULARIO IT
                           INNER JOIN TBESTADOIDFORMULARIO ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
                           LEFT OUTER JOIN TBGEOGRAFIA                    PP ON PP.ID = IT.ID_PAIS
                           LEFT OUTER JOIN TBGEOGRAFIA                    DP ON DP.ID = IT.ID_DEPARTAMENTO
                           LEFT OUTER JOIN TBGEOGRAFIA                    MP ON MP.ID = IT.ID_MUNICIPIO
                           LEFT OUTER JOIN TBENTIDADMUNICIPIO             EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
                           LEFT OUTER JOIN TBUSUARIOS                     UM ON UM.ID = IT.ID_USUARIO
              WHERE        IT.NUMEROFORMULARIO LIKE NVL(PI_NUMEROFORMULARIO || '%', IT.NUMEROFORMULARIO)
                           AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)) >= NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)))
                           AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)) <= NVL(PI_NHASTA, NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2))))
                           AND IT.DGENERADO = NVL(PI_DGENERADO, IT.DGENERADO)
                           AND IT.id_usuario = pi_IdUsuario
                           AND IT.id_estadoidformulario = pi_IdEstado
              ORDER BY DGENERADO DESC
              ) INFO
            WHERE ROWNUM < lowerBound)
      WHERE R >= upperBound;
  EXCEPTION
    WHEN FECHA_INVALIDA THEN
      RAISE_APPLICATION_ERROR(-20000, 'La fecha de generación del formulario a consultar no puede ser mayor que la actual');
    WHEN RANGO_INVALIDO THEN
      RAISE_APPLICATION_ERROR(-20001, 'El rango ingresado para la búsqueda es inválido');
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('SQLERR');
  END;

  /***********************************************************
  * Procedure description: Obtiene la cantidad de formularios, por estado y/o usuario
  * Date:   30/10/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By     Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_ObtenerFrmsCantidad
  (
    PI_NUMEROFORMULARIO IN VARCHAR2 DEFAULT NULL,
    PI_NDESDE           IN NUMBER DEFAULT NULL,
    PI_NHASTA           IN NUMBER DEFAULT NULL,
    PI_DGENERADO        IN DATE DEFAULT NULL,
    pi_IdEstado         IN NUMBER,
    pi_IdUsuario        IN NUMBER,
    po_RecordCount      OUT NUMBER
  )
  AS
    FECHA_INVALIDA EXCEPTION;
    RANGO_INVALIDO EXCEPTION;
  BEGIN
    IF PI_DGENERADO > SYSDATE THEN
      RAISE FECHA_INVALIDA;
    END IF;

    IF (PI_NDESDE IS NULL AND PI_NHASTA IS NOT NULL) OR PI_NDESDE > PI_NHASTA THEN
      RAISE RANGO_INVALIDO;
    END IF;

    SELECT       COUNT(1)
    INTO         po_RecordCount
    FROM         TBIDENTIFICADORFORMULARIO IT
                 INNER JOIN TBESTADOIDFORMULARIO ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
                 LEFT OUTER JOIN TBGEOGRAFIA                    PP ON PP.ID = IT.ID_PAIS
                 LEFT OUTER JOIN TBGEOGRAFIA                    DP ON DP.ID = IT.ID_DEPARTAMENTO
                 LEFT OUTER JOIN TBGEOGRAFIA                    MP ON MP.ID = IT.ID_MUNICIPIO
                 LEFT OUTER JOIN TBENTIDADMUNICIPIO             EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
                 LEFT OUTER JOIN TBUSUARIOS                     UM ON UM.ID = IT.ID_USUARIO
    WHERE        IT.NUMEROFORMULARIO LIKE NVL(PI_NUMEROFORMULARIO || '%', IT.NUMEROFORMULARIO)
                 AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)) >= NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)))
                 AND TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2)) <= NVL(PI_NHASTA, NVL(PI_NDESDE, TO_NUMBER(SUBSTR(IT.NUMEROFORMULARIO,3,LENGTH(IT.NUMEROFORMULARIO)-2))))
                 AND IT.DGENERADO = NVL(PI_DGENERADO, IT.DGENERADO)
                 AND IT.id_usuario = pi_IdUsuario
                 AND IT.id_estadoidformulario = pi_IdEstado;

  EXCEPTION
    WHEN FECHA_INVALIDA THEN
      RAISE_APPLICATION_ERROR(-20000, 'La fecha de generación del formulario a consultar no puede ser mayor que la actual');
    WHEN RANGO_INVALIDO THEN
      RAISE_APPLICATION_ERROR(-20001, 'El rango ingresado para la búsqueda es inválido');
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('SQLERR');
  END;

  /***********************************************************
  * Procedure description: Obtiene Los formularios para activar o inactivar (SP_GETFORMULARIOSNORADICADOS)
  * Date:   31/10/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By     Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_ObtenerFormulariosPaginado
  (
    pi_NumeroFormulario     TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
    pi_IdPais               TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
    pi_IdDepartamento       TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
    pi_IdMunicipio          TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
    pi_IdEntidadmunicipio   TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
    pi_Accion               NUMBER,
    pi_PageNumber           NUMBER,
    pi_PageSize             NUMBER,
    Po_Resultado            OUT SYS_REFCURSOR
  )
  AS
    lowerBound INT;
    upperBound INT;
  BEGIN
    lowerBound := (pi_PageNumber * pi_PageSize) + 1;
    upperBound := ((pi_PageNumber - 1) * pi_PageSize) + 1;

    OPEN Po_Resultado FOR
    SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (
                  SELECT  X.*, NVL(FH.DMODIFICACION, X.DGENERADO) DULTIMAMOD, FH.OBSERVACION
                  FROM
                  (
                      SELECT       IT.ID,
                                   IT.NUMEROFORMULARIO,
                                   IT.DGENERADO,
                                   IT.ID_PAIS,
                                   PP.NOMBRE AS NOMBREPAIS,
                                   IT.ID_DEPARTAMENTO,
                                   DP.nombre AS NOMBREDEPARTAMENTO,
                                   IT.ID_MUNICIPIO,
                                   MP.nombre AS NOMBREMUNICIPIO,
                                   IT.ID_ENTIDADMUNICIPIO,
                                   EP.NOMBRE AS NOMBREENTIDADMUNICIPIO,
                                   IT.ID_ESTADOIDFORMULARIO,
                                   ET.NOMBRE AS NOMBREESTADOIDFORMULARIO,
                                   IT.ID_USUARIO,
                                   UM.USUARIO AS NOMBREUSUARIO,
                                   UM.NOMBRE  AS NOMBRECOMPLETOUSUARIO,
                                   (SELECT MAX(ID) FROM TBIDFORMULARIOHISTORICO WHERE ID_IDENTIFICADORFORMULARIO = IT.ID) NIDULTIMAMOD
                      FROM         TBIDENTIFICADORFORMULARIO IT
                                   INNER JOIN TBESTADOIDFORMULARIO ET ON ET.ID = IT.ID_ESTADOIDFORMULARIO
                                   LEFT OUTER JOIN TBGEOGRAFIA PP ON PP.ID = IT.ID_PAIS
                                   LEFT OUTER JOIN TBGEOGRAFIA DP ON DP.ID = IT.ID_DEPARTAMENTO
                                   LEFT OUTER JOIN TBGEOGRAFIA MP ON MP.ID = IT.ID_MUNICIPIO
                                   LEFT OUTER JOIN TBENTIDADMUNICIPIO EP ON EP.ID = IT.ID_ENTIDADMUNICIPIO
                                   LEFT OUTER JOIN TBUSUARIOS  UM ON UM.ID = IT.ID_USUARIO
                      WHERE        UPPER(IT.NUMEROFORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(IT.NUMEROFORMULARIO)) || '%'
                                   AND   NVL(IT.ID_PAIS, -1) = NVL(PI_IDPAIS, NVL(IT.ID_PAIS, -1))
                                   AND   NVL(IT.ID_DEPARTAMENTO, -1) = NVL(PI_IDDEPARTAMENTO, NVL(IT.ID_DEPARTAMENTO, -1))
                                   AND   NVL(IT.ID_MUNICIPIO, -1) = NVL(PI_IDMUNICIPIO, NVL(IT.ID_MUNICIPIO, -1))
                                   AND   NVL(IT.ID_ENTIDADMUNICIPIO, -1) = NVL(PI_IDENTIDADMUNICIPIO, NVL(IT.ID_ENTIDADMUNICIPIO, -1))
                                   AND   IT.NUMEROFORMULARIO NOT IN(
                                         SELECT  NRO_FORMULARIO
                                         FROM    TBRADICACION
                                         WHERE   UPPER(NRO_FORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(NRO_FORMULARIO)) || '%'
                                   )
                                   AND   ((it.id_estadoidformulario = ESTADO_INACTIVO AND pi_accion = 1) OR (IT.id_estadoidformulario IN (ESTADO_ASIGNADO,ESTADO_IMPRENTA, ESTADO_GENERADO) AND pi_accion = 2 ))
                  ) X LEFT JOIN TBIDFORMULARIOHISTORICO FH ON FH.ID = X.NIDULTIMAMOD
                  ORDER BY DULTIMAMOD DESC
              ) INFO
            WHERE ROWNUM < lowerBound)
      WHERE R >= upperBound;
  END;

  /***********************************************************
  * Procedure description: Cantidad Formularios Para activar
  * Date:   31/10/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By     Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_ObtenerFormulariosCantidad
  (
    pi_NumeroFormulario     TBIDENTIFICADORFORMULARIO.NUMEROFORMULARIO%TYPE DEFAULT NULL,
    pi_IdPais               TBIDENTIFICADORFORMULARIO.ID_PAIS%TYPE DEFAULT NULL,
    pi_IdDepartamento       TBIDENTIFICADORFORMULARIO.ID_DEPARTAMENTO%TYPE DEFAULT NULL,
    pi_IdMunicipio          TBIDENTIFICADORFORMULARIO.ID_MUNICIPIO%TYPE DEFAULT NULL,
    pi_IdEntidadmunicipio   TBIDENTIFICADORFORMULARIO.ID_ENTIDADMUNICIPIO%TYPE DEFAULT NULL,
    pi_Accion               NUMBER,
    po_RecordCount          OUT NUMBER
  )
  AS
  BEGIN
    SELECT       COUNT(1)
    INTO         po_RecordCount
    FROM         TBIDENTIFICADORFORMULARIO IT
    WHERE        UPPER(IT.NUMEROFORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(IT.NUMEROFORMULARIO)) || '%'
                 AND   NVL(IT.ID_PAIS, -1) = NVL(PI_IDPAIS, NVL(IT.ID_PAIS, -1))
                 AND   NVL(IT.ID_DEPARTAMENTO, -1) = NVL(PI_IDDEPARTAMENTO, NVL(IT.ID_DEPARTAMENTO, -1))
                 AND   NVL(IT.ID_MUNICIPIO, -1) = NVL(PI_IDMUNICIPIO, NVL(IT.ID_MUNICIPIO, -1))
                 AND   NVL(IT.ID_ENTIDADMUNICIPIO, -1) = NVL(PI_IDENTIDADMUNICIPIO, NVL(IT.ID_ENTIDADMUNICIPIO, -1))
                 AND   IT.NUMEROFORMULARIO NOT IN(
                       SELECT  NRO_FORMULARIO
                       FROM    TBRADICACION
                       WHERE   UPPER(NRO_FORMULARIO) LIKE '%' || NVL(UPPER(PI_NUMEROFORMULARIO), UPPER(NRO_FORMULARIO)) || '%'
                 )
                 AND   ((it.id_estadoidformulario = ESTADO_INACTIVO AND pi_accion = 1) OR (IT.id_estadoidformulario IN (ESTADO_ASIGNADO,ESTADO_IMPRENTA, ESTADO_GENERADO) AND pi_accion = 2 ));
  END;

  FUNCTION f_generaNroFrmGenerico
  (
    P_SERIE VARCHAR2
  )
  RETURN varchar2
  IS
      O_Result varchar2(50);
      v_reduccion NUMBER;
      v_car varchar2(50);
      v_id varchar2(50);
      vNext NUMBER;
  BEGIN

    SELECT (to_number(MAX(SUBSTR(numeroformulario, 3))) + 1) INTO vNext FROM TBIDENTIFICADORFORMULARIO WHERE numeroformulario LIKE P_SERIE || '%';

    IF vNext IS NULL THEN
      vNext := 1;
    END IF;

     v_reduccion := f_reducir(vNext);
     IF v_reduccion >= 10 THEN
         v_reduccion := f_reducir(v_reduccion);
     END IF;

      if (v_reduccion = 1) then
        v_car := 'M';
      elsif (v_reduccion = 2) then
        v_car := 'L';
      elsif (v_reduccion = 3) then
        v_car := 'K';
      elsif (v_reduccion = 4) then
        v_car := 'J';
      elsif (v_reduccion = 5) then
        v_car := 'I';
      elsif (v_reduccion = 6) then
        v_car := 'H';
      elsif (v_reduccion = 7) then
        v_car := 'G';
      elsif (v_reduccion = 8) then
        v_car := 'F';
      elsif (v_reduccion = 9) then
        v_car := 'E';
      elsif (v_reduccion = 10) then
        v_car := 'D';
      elsif (v_reduccion = 11) then
        v_car := 'C';
      elsif (v_reduccion = 12) then
        v_car := 'B';
      elsif (v_reduccion = 13) then
        v_car := 'A';
      else
        v_car := 'Z';
      end if;

      v_id := LPAD(vNext, 9, '0');

      O_Result := P_SERIE || v_car || v_id;

     return O_Result;
  end;

  FUNCTION f_reducir( P_CADENA varchar2 )
    RETURN number IS O_Result number;

        v_sum NUMBER;
        v_contador NUMBER;
        v_tam NUMBER;

  begin

     v_tam := length(P_CADENA);
     v_sum := 0;

     for v_contador in 1..v_tam loop
         v_sum := v_sum + substr(P_CADENA, v_contador, 1);
     end loop;

     O_Result := v_sum;
     return O_Result;
  end;

END PKG_CONTROLIDFORMULARIO;
/