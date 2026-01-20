-- Create new package
create or replace PACKAGE PKG_DEVOLUCION AS
  
  TYPE CURSOR_TYPE IS REF CURSOR;
  ROL_LIDER_DEVOLUCION NUMBER := 1013;
  DEVUELTA number := 10023; -- Devuelto
  DECLA_PENDE_DEVOL NUMBER := 10024;
  FORMULARIO_DEVUELTO         NUMBER := 6;
  ESTADO_DOCUMENTO_GEN NUMBER := 2;
  TIPO_DOCUMENTO_DEV NUMBER :=2;
  



  PROCEDURE SP_CARGARDECPENDDEV(
                                    PI_IDDECLARACION IN NUMBER, 
                                    PO_CURSOR OUT CURSOR_TYPE
                                  );

  /* Obtiene una devolucion por id_declaracion y id_radicacion  */                                  
  PROCEDURE sp_ObtenerDevolucion (
                                  PI_IDDECLARACION IN NUMBER,
                                  PO_CURSOR OUT CURSOR_TYPE
                                  );
                  
  /*  DESCRIPCION: OBTIENE LA LISTA DE CAUSALES DE UNA DEVOLUCION DADA
  **  AUTOR: JAIRO VALDERRAMA
  **  FECHA: 20121029
  **  CAMBIOS:
  **    1. FECHA Y NOMBRE DE QUIEN REALIZA EL CAMBIO EN FORMATO (YYYYMMDD - NOMBRE APELLIDO)
  **    DESCIPCIÓN DE LA MODIFICACION
  */
  PROCEDURE SP_OBTENERLISTACAUSALES(PI_IDDEVOLUCION IN NUMBER,
                                    PO_CURSOR       OUT CURSOR_TYPE);

  /*  DESCRIPCION: ACTUALIZA LA INFORMACION DE UNA DEVOLUCION
  **  AUTOR:
  **  FECHA:
  **  CAMBIOS:
  **    1. 20121108 - JAIRO VALDERRAMA
  **    SE ACTUALIZA EL FORMULARIO CON ESTADO DE DEVUELTO
  */
  PROCEDURE sp_ActualizarDevolucion (
                                      PI_IDDEVOLUCION IN NUMBER,
                                      PI_IDUSUARIO IN NUMBER,
                                      PI_PARTEEMOTIVAMOD IN VARCHAR2,
                                      PI_NUMEROGUIA IN VARCHAR2 DEFAULT NULL,
                                      PI_CDIRECCION IN VARCHAR2,
                                      PI_NTELEFONO IN NUMBER,
                                      PI_CFUNCIONARIO IN VARCHAR2
                                      );

  /* Inserta la informacion correspondiente a una solicitud de devolucion */
  PROCEDURE SP_SOLICITARDEVOLUCION(
                                  PI_IDDECLARACION IN NUMBER,
                                  PI_IDENTIDADMUNICIPIO IN NUMBER DEFAULT NULL,
                                  PI_IDUSUARIO IN NUMBER,
                                  P_OBSERVACIONES IN VARCHAR2 DEFAULT NULL,
                                  PI_IDSCAUSALES IN VARCHAR2
                                  );

  type string_array is table of varchar2(32767);
  function split_string(str in varchar2, delimiter in char default ',') return string_array;
  
  PROCEDURE SP_DATOSPARADEVOLUCION(PI_IDDEVOLUCION IN NUMBER,
                                    PO_CURSOR OUT CURSOR_TYPE
                                   );

  PROCEDURE  sp_ObtenerCausalesDevolucion
  (
    Po_Resultado OUT SYS_REFCURSOR
  ); 

END PKG_DEVOLUCION;
/

-- Create package body
create or replace PACKAGE BODY PKG_DEVOLUCION AS
  
  PROCEDURE sp_CargarDecPendDev(pi_IdDeclaracion IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT DECLA.ID ID_DECLARACION, 
             DECLA.NUMEROFORMULARIO NUMERO_FORMULARIO,
             DECLA.FECHADECLARACION FECHA_DECLARACION,
             RAD.FECHALLEGADA       FECHA_RADICACION,
             PER.PRIMERNOMBRE       PRIMER_NOMBRE,
             PER.SEGUNDONOMBRE      SEGUNDO_NOMBRE,
             PER.PRIMERAPELLIDO     PRIMER_APELLIDO,
             PER.SEGUNDOAPELLIDO    SEGUNDO_APELLIDO,
             PER.NUMERODOCUMENTO    NUMERO_DOCUMENTO
      FROM TBDECLARACIONES DECLA
      INNER JOIN TBRADICACION RAD ON RAD.ID_DECLARACION = DECLA.ID 
      LEFT  JOIN TBDECLARACION_HISTORICO DEC_H ON DEC_H.ID_DECLARACION = DECLA.ID AND DEC_H.PARAM_ESTADO = 10024
      INNER JOIN TBREGISTROS_PERSONAS REG_PER ON REG_PER.ID_DECLARACION = DECLA.ID AND REG_PER.ESDECLARANTE = 1
      INNER JOIN TBPERSONAS PER ON PER.ID = REG_PER.ID_PERSONA
      WHERE DECLA.ID = pi_IdDeclaracion;
  END;

  /* Obtiene una devolucion por id_declaracion y id_radicacion  */
  PROCEDURE sp_ObtenerDevolucion (pi_IdDeclaracion IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT  DEV.ID,
              DEV.ID_RADICACION,
              DEV.ID_DECLARACION,
              DEV.ID_ENTIDADMUNICIPIO,
              DEV.USUARIO,
              DEV.ESTADODECLANTERIOR,
              DEV.FECHASOLICITUD,
              DEV.FECHADEVOLUCION,
              DEV.OBSERVACOINDEVO,
              DEV.PARTEEMOTIVAMOD,
              DEV.NROGUIA,
              PER.PRIMERNOMBRE PRIMER_NOMBRE,
              PER.SEGUNDONOMBRE SEGUNDO_NOMBRE,
              PER.PRIMERAPELLIDO PRIMER_APELLIDO,
              PER.SEGUNDOAPELLIDO SEGUNDO_APELLIDO,
              P.NOMBRE AS PAIS,
              D.NOMBRE AS DEPARTAMENTO,
              M.NOMBRE AS MUNICIPIO,
              EM.NOMBRE AS ENTIDAD,
              EM.NOMBREFUNCIONARIO,
              EM.DIRECCIONENTIDAD,
              EM.TELEFONOENTIDAD,
              RAD.NRO_FORMULARIO,
              RAD.FECHAREGISTRO
      FROM    TBDEVOLUCION DEV
              INNER JOIN TBREGISTROS_PERSONAS REG_PER ON REG_PER.ID_DECLARACION = DEV.ID_DECLARACION AND REG_PER.ESDECLARANTE = 1
              INNER JOIN TBPERSONAS PER ON PER.ID = REG_PER.ID_PERSONA
              LEFT JOIN TBENTIDADMUNICIPIO EM ON EM.ID = DEV.ID_ENTIDADMUNICIPIO
              INNER JOIN TBRADICACION RAD ON RAD.ID = DEV.ID_RADICACION
              LEFT JOIN TBGEOGRAFIA M ON M.ID = EM.ID_MUNICIPIO
              LEFT JOIN TBGEOGRAFIA D ON D.ID = M.PADREID
              LEFT JOIN TBGEOGRAFIA P ON P.ID = D.PADREID
      WHERE   DEV.ID_DECLARACION   = pi_IdDeclaracion;
  END;
  
  PROCEDURE sp_ObtenerListaCausales(pi_IdDevolucion IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT CD.ID_CAUSAL FROM TBCAUSALESDEVOLUCION CD WHERE CD.ID_DEVOLUCION = pi_IdDevolucion;
  END;
  
  /* Actualiza la informacion de una devolucion */
  PROCEDURE sp_ActualizarDevolucion(pi_IdDevolucion    IN NUMBER,
                                    pi_IdUsuario       IN NUMBER,
                                    pi_ParteEmotivaMod IN VARCHAR2,
                                    pi_NumeroGuia      IN VARCHAR2 DEFAULT NULL,
                                    pi_CDireccion      IN VARCHAR2,
                                    pi_NTelefono       IN NUMBER,
                                    pi_CFuncionario    IN VARCHAR2) IS
    NIdDeclaracion      NUMBER;
    NIdEntidadMunicipio NUMBER;
    IdActoAdmin         NUMBER;
  BEGIN
    UPDATE TBDEVOLUCION SET FECHADEVOLUCION     = SYSDATE
                          , PARTEEMOTIVAMOD     = pi_ParteEmotivaMod
                          , NROGUIA             = pi_NumeroGuia
    WHERE ID = pi_IdDevolucion;
    
    SELECT IDACTOADMINISTRATIVO, ID_ENTIDADMUNICIPIO, ID_DECLARACION INTO IdActoAdmin, NIdEntidadMunicipio, NIdDeclaracion
    FROM TBDEVOLUCION WHERE ID = pi_IdDevolucion;
    
    -- Actualiza el estado de la declaracion a "devuelta"
    PKG_COMMON.sp_UpdEstado_Declaracion(NIdDeclaracion, pi_IdUsuario, DEVUELTA);
    
    -- Actualiza los datos ingresados de la entidad
    UPDATE TBENTIDADMUNICIPIO SET NOMBREFUNCIONARIO = pi_CFuncionario
                                , DIRECCIONENTIDAD  = pi_CDireccion
                                , TELEFONOENTIDAD   = pi_NTelefono
    WHERE ID = NIdEntidadMunicipio;
    
    -- Actualiza el estado del formulario a devuelto
    UPDATE TBIDENTIFICADORFORMULARIO SET ID_ESTADOIDFORMULARIO = FORMULARIO_DEVUELTO
    WHERE NUMEROFORMULARIO = (SELECT  NUMEROFORMULARIO FROM TBDECLARACIONES WHERE ID = NIdDeclaracion);
    
    -- Actualiza el usuario que aprueba la solicitud
    UPDATE TBDEVOLUCION SET USUARIO = pi_IdUsuario
    WHERE   ID = PI_IDDEVOLUCION;
              
    PKG_ACTOSADMIN.sp_ActEstadoActoAdmin(IdActoAdmin, 4, pi_IdUsuario);
  END;

  /* Inserta la informacion correspondiente a una solicitud de devolucion */
  PROCEDURE sp_SolicitarDevolucion(pi_IdDeclaracion      IN NUMBER,
                                   pi_IdEntidadMunicipio IN NUMBER DEFAULT NULL,
                                   pi_IdUsuario          IN NUMBER,
                                   p_Observaciones       IN VARCHAR2 DEFAULT NULL,
                                   pi_IdsCausales        IN VARCHAR2) IS
    v_NumeroFormulario    TBDECLARACIONES.NUMEROFORMULARIO%TYPE;
    v_EstadoDeclaracion   TBDECLARACIONES.PARAM_ESTADO%TYPE;
    v_IdActoAdmin         NUMBER;
    v_IdConsecutivoAdmin  VARCHAR2(500);
    v_IdRadicacion        NUMBER;
    v_IdEntidadMunicipio  NUMBER;
    v_IdsCausales         STRING_ARRAY;
    v_IdDevolucion        NUMBER;
    v_IdUsuarioDevolucion NUMBER;
  BEGIN 
    -- Valores actuales de la declaracion
    SELECT NUMEROFORMULARIO, PARAM_ESTADO INTO v_NumeroFormulario, v_EstadoDeclaracion FROM TBDECLARACIONES WHERE ID = pi_IdDeclaracion;
    -- Generar consecutivo y registro de acto administrativo devolución
    PKG_ACTOSADMIN.sp_SetActoAdministrativoRUV(pi_IdDeclaracion, 0, '', v_NumeroFormulario, '', '', pi_IdUsuario, ESTADO_DOCUMENTO_GEN, TIPO_DOCUMENTO_DEV, v_IdActoAdmin, v_IdConsecutivoAdmin);
      
    --Obtiene el numero de la radicacion correspondiente a esta devolucion
    SELECT ID INTO v_IdRadicacion FROM (SELECT ID FROM TBRADICACION WHERE ID_DECLARACION = pi_IdDeclaracion
                                        ORDER BY ID DESC) WHERE ROWNUM = 1;
    
    -- Dado el caso que el id de la entidad municipio sea nulo (ej. crítica n)
    -- entonces este es obtenido con el id de la radicación
    IF (pi_IdEntidadMunicipio IS NULL) THEN
      SELECT ID_ENTIDADMUNICIPIO INTO v_IdEntidadMunicipio FROM TBRADICACION WHERE ID = v_IdRadicacion;
    ELSE
      v_IdEntidadMunicipio := pi_IdEntidadMunicipio;
    END IF;  
    
    -- Insertar registro de devolución
    INSERT INTO TBDEVOLUCION (ID, ID_RADICACION, ID_DECLARACION, ID_ENTIDADMUNICIPIO, FECHASOLICITUD, OBSERVACOINDEVO, ESTADODECLANTERIOR, IDACTOADMINISTRATIVO, ID_USUARIOSOLICITANTE)
    VALUES (SEQ_TBDEVOLUCION.NextVal, v_IdRadicacion, pi_IdDeclaracion, v_IdEntidadMunicipio, SYSDATE, p_Observaciones, v_EstadoDeclaracion, v_IdActoAdmin, pi_IdUsuario)
    RETURNING ID INTO v_IdDevolucion;
    
    -- Separar causales de devolucion e insertar
    v_IdsCausales := SPLIT_STRING(pi_IdsCausales);
    IF v_IdsCausales.Count > 0 THEN
      FOR i IN 1..v_IdsCausales.Count LOOP
        INSERT INTO TBCAUSALESDEVOLUCION (ID_CAUSAL, ID_DEVOLUCION) VALUES (v_IdsCausales(i), v_IdDevolucion);
      END LOOP;
    END IF;
    
    -- Busca el lider de devolucion con menos carga  
    v_IdUsuarioDevolucion := PKG_COMMON.f_UsuarioMenosCarga(ROL_LIDER_DEVOLUCION);
    -- Actualiza el estado de la declaración y la asigna a el lider de devolucion
    PKG_COMMON.sp_UpdEstado_Declaracion(pi_IdDeclaracion, v_IdUsuarioDevolucion, DECLA_PENDE_DEVOL);
  END;

  PROCEDURE sp_DatosParaDevolucion(pi_IdDevolucion IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN                              
    OPEN po_Cursor FOR
      SELECT D.ID_DECLARACION
           , EM.NOMBRE AS NOMBREENTIDAD
           , G.NOMBRE AS NOMBREMUNICIPIO
           , D.PARTEEMOTIVAMOD
           , SYSDATE
           , PA.PRIMERNOMBRE || ' ' || PA.SEGUNDONOMBRE || ' ' || PA.PRIMERAPELLIDO || ' ' || PA.SEGUNDOAPELLIDO AS NOMBREDECLARANTE
           , PR.NOMBRE AS TIPODOCUMENTO
           , PA.NUMERODOCUMENTO
           , DE.FECHADECLARACION
           , AC.CONSECUTIVO
      FROM TBDEVOLUCION D
      INNER JOIN TBREGISTROS_PERSONAS RPA ON RPA.ID_DECLARACION = D.ID_DECLARACION
      INNER JOIN TBPERSONAS PA ON PA.ID = RPA.ID_PERSONA
      INNER JOIN TBDECLARACIONES DE ON DE.id = D.ID_DECLARACION
      INNER JOIN TBPARAMETROS PR ON PR.id = PA.PARAM_TIPODOCUMENTO
      INNER JOIN TBENTIDADMUNICIPIO EM ON EM.ID = D.ID_ENTIDADMUNICIPIO
      INNER JOIN TBGEOGRAFIA G ON G.ID = EM.ID_MUNICIPIO
      LEFT JOIN TBACTO_ADMINISTRATIVO AC ON AC.ID = D.IDACTOADMINISTRATIVO
      WHERE D.ID = pi_IdDevolucion AND RPA.ESDECLARANTE = 1;
  END;

  PROCEDURE sp_ObtenerCausalesDevolucion(po_Resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN po_Resultado FOR
      SELECT ID, NOMBRECAUSAL, PARTEEMOTIVA, TIPO FROM TBCAUSALES;
  END;
  
  FUNCTION split_string(str in varchar2, delimiter in char default ',') return string_array is
    return_value         string_array := string_array();
    split_str            long default str || delimiter;
    i                    number;
  BEGIN
    loop
      i := instr(split_str, delimiter);
      exit when nvl(i,0) = 0;
      return_value.extend;
      return_value(return_value.count) := trim(substr(split_str, 1, i-1));
      split_str := substr(split_str, i + length(delimiter));
    end loop;
    return return_value;
  END split_string;
    
END PKG_DEVOLUCION;
/