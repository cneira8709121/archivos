-- Create new package
create or replace PACKAGE PKG_REGISTROOFFLINE IS

  TYPE CURSOR_TYPE IS REF CURSOR;

  PROCEDURE CerrarSesion(p_Usuario VARCHAR);

  PROCEDURE AutenticarUsuario(p_Usuario VARCHAR, p_Clave VARCHAR, p_InterfaseRed VARCHAR, p_IP VARCHAR, p_Roles OUT CURSOR_TYPE);

  PROCEDURE CerrarSesionUsuario(p_Id NUMBER);

  PROCEDURE ObtenerInfoGeneral(p_Departamentos         OUT CURSOR_TYPE
                             , p_Municipios            OUT CURSOR_TYPE
                             , p_Parametros            OUT CURSOR_TYPE
                             , p_GruposParamDetalle    OUT CURSOR_TYPE
                             , p_GruposEtnicos         OUT CURSOR_TYPE
                             , p_ComunidadesEtnicas    OUT CURSOR_TYPE
                             , p_Poblaciones           OUT CURSOR_TYPE
                             , p_UnidadesTerritoriales OUT CURSOR_TYPE
                             , p_Paises                OUT CURSOR_TYPE
                             , p_Validaciones          OUT CURSOR_TYPE
                             , p_EntidadMunicipio      OUT CURSOR_TYPE
                             , p_CriticaN              OUT CURSOR_TYPE
                             , p_Causales              OUT CURSOR_TYPE);

  PROCEDURE AuditarSesion(p_Usuario VARCHAR, p_Codigo NUMBER, p_InterfaseRed VARCHAR, p_IP VARCHAR);

  ESESION_USUARIODESCONOCIDO NUMBER := 4759;
  ESESION_USUARIONOACTIVO    NUMBER := 5107;
  ESESION_BLOQUEOPORINTENTOS NUMBER := 4761;
  ESESION_CLAVEINCORRECTA    NUMBER := 4760;
  ESESION_SINPERMISOS        NUMBER := 5108;
  ESESION_INGESOEXITOSO      NUMBER := 4758;
  ESESION_MAXIMASSESIONES    NUMBER := 4994;
  ESESION_CERRARSESION       NUMBER := 4765;

END PKG_REGISTROOFFLINE;
/

-- Create package body
create or replace PACKAGE BODY PKG_REGISTROOFFLINE IS

  PROCEDURE CerrarSesion(p_Usuario VARCHAR) AS
  BEGIN

    UPDATE TBUSUARIOS U SET U.SESIONES_RUV = 0, U.INTENTOSERRADOS = 0
    WHERE U.USUARIO = p_Usuario;

    INSERT INTO TBAUDITORIA_SESION VALUES (SYSDATE, p_Usuario, ESESION_CERRARSESION, NULL, NULL);

  END;

  PROCEDURE AutenticarUsuario(p_Usuario VARCHAR, p_Clave VARCHAR, p_InterfaseRed VARCHAR, p_IP VARCHAR, p_Roles OUT CURSOR_TYPE) AS
    v_IdMenuAutenticacion NUMBER := 1000;
    v_UsuarioEncontrado   NUMBER;
    v_Resultado           NUMBER := NULL;
    v_ResultadoMensaje    VARCHAR(255);
    -- Usuario
    v_Id                  NUMBER;
    v_Clave               VARCHAR(255);
    v_Nombre              VARCHAR(255);
    v_IntentosErrados     NUMBER;
    v_Activo              NUMBER;
    v_Bloqueado           NUMBER;
    v_SesionesRUV         NUMBER;
    v_Cuenta              VARCHAR(255);
    v_Identificacion      VARCHAR(20);
    v_Cargo               VARCHAR(255);
    v_FirmaDigital        NUMBER;
    v_UTerritorial        NUMBER;
    v_IdDepartamento      NUMBER;
    v_IdMunicipio         NUMBER;
    v_IdEntidadMunicipio  NUMBER;
    v_ConteoPermisos      NUMBER;
  BEGIN
    p_Roles := NULL;

    -- Determinar existencia de usuario
    SELECT COUNT(*) INTO v_UsuarioEncontrado FROM TBUSUARIOS U WHERE LOWER(p_Usuario) = LOWER(U.USUARIO);
    IF v_UsuarioEncontrado = 0 THEN
      v_Resultado := 1;
      v_ResultadoMensaje := 'Usuario o clave no coinciden.';
      AuditarSesion(p_Usuario, ESESION_USUARIODESCONOCIDO, p_InterfaseRed, p_IP);
      GOTO failed_authentication;
    END IF;

    -- Obtener usuario
    SELECT U.ID, U.CLAVE, U.NOMBRE, NVL(U.INTENTOSERRADOS, 0), NVL(U.ACTIVO, 0), NVL(U.BLOQUEADO, 0), NVL(U.SESIONES_RUV, 0), U.USUARIO, U.IDENTIFICACION, U.CARGO, NVL(U.APLICA_FIRMADIGITAL, 0), U.ID_UTRERRITORIAL, U.ID_DEPARTAMENTO, U.ID_MUNICIPIO, U.ID_ENTIDADMUNICIPIO
    INTO v_Id, v_Clave, v_Nombre, v_IntentosErrados, v_Activo, v_Bloqueado, v_SesionesRUV, v_Cuenta, v_Identificacion, v_Cargo, v_FirmaDigital, v_UTerritorial, v_IdDepartamento, v_IdMunicipio, v_IdEntidadMunicipio
    FROM TBUSUARIOS U
    WHERE LOWER(p_Usuario) = LOWER(U.USUARIO);

    -- Usuario debe estar activo
    IF v_Activo = 0 THEN
      v_Resultado := 2;
      v_ResultadoMensaje := 'Usuario no activo';
      AuditarSesion(p_Usuario, ESESION_USUARIONOACTIVO, p_InterfaseRed, p_IP);
      GOTO failed_authentication;
    END IF;

    IF v_Bloqueado = 1 THEN
      v_Resultado := 3;
      v_ResultadoMensaje := 'Usuario bloqueado';
      AuditarSesion(p_Usuario, ESESION_USUARIONOACTIVO, p_InterfaseRed, p_IP);
      GOTO failed_authentication;
    END IF;

    /* IF v_SesionesRUV > 0 THEN
      v_Resultado := 4;
      v_ResultadoMensaje := 'Maximo de sesiones utilizadas';
      AuditarSesion(p_Usuario, EVENTOSESION_MAXIMASSESIONES, p_InterfaseRed, p_IP);
      GOTO failed_authentication;
    END IF; */

    IF v_Clave != p_Clave THEN
      v_Resultado := 5;
      v_ResultadoMensaje := 'Usuario o clave no coinciden';
      v_IntentosErrados := v_IntentosErrados + 1;
      IF v_IntentosErrados >= 3 THEN
        UPDATE TBUSUARIOS U SET U.BLOQUEADO = 1, U.SESIONES_RUV = 0, U.INTENTOSERRADOS = 0 WHERE U.ID = v_Id;
        AuditarSesion(p_Usuario, ESESION_BLOQUEOPORINTENTOS, p_InterfaseRed, p_IP);
      ELSE
        UPDATE TBUSUARIOS U SET U.INTENTOSERRADOS = v_IntentosErrados WHERE U.ID = v_Id;
        AuditarSesion(p_Usuario, ESESION_CLAVEINCORRECTA, p_InterfaseRed, p_IP);
      END IF;
      GOTO failed_authentication;
    END IF;

    -- Permisos de ingreso
    SELECT COUNT(*) INTO v_ConteoPermisos FROM TBROLES_USUARIO RU
    INNER JOIN TBROLES R ON R.ID = RU.ID_ROL
    INNER JOIN TBROLES_OPCIONESACCIONES ROA ON ROA.ID_ROL = R.ID AND ROA.CONSULTAR = 1
    --LEFT  JOIN SIPODPRUEBAS.TBPROCESOS_REPORTES P ON P.ID_PROCESO = ROA.ID_OPCIONACCION /* Count is not affected by a left join */
    WHERE RU.ID_USUARIO = v_Id;

    IF v_ConteoPermisos = 0 THEN
      v_Resultado := 6;
      v_ResultadoMensaje := 'No tiene permisos para utilizar esta aplicacion';
      AuditarSesion(p_Usuario, ESESION_SINPERMISOS, p_InterfaseRed, p_IP);
      GOTO failed_authentication;
    END IF;

    -- Permitir ingreso.
    UPDATE TBUSUARIOS U SET U.SESIONES_RUV = 1, U.INTENTOSERRADOS = 0 WHERE U.ID = v_Id;
    v_Resultado := 0;
    v_ResultadoMensaje := 'Autenticacion exitosa';
    AuditarSesion(p_Usuario, ESESION_INGESOEXITOSO, p_InterfaseRed, p_IP);

    OPEN p_Roles FOR
      SELECT P.ID_PROCESO         AS PERMISO
           , RU.ID_ROL            AS ID_ROL
           , v_Id                 AS ID
           , v_Nombre             AS NOMBRE
           , v_Resultado          AS RESULTADO
           , v_ResultadoMensaje   AS MENSAJE
           , v_Cuenta             AS CUENTA
           , v_Cargo              AS CARGO
           , v_Identificacion     AS IDENTIFICACION
           , v_FirmaDigital       AS FIRMADIGITAL
           , v_UTerritorial       AS UNIDADTERRITORIAL
           , 48                   AS ID_PAIS
           , v_IdDepartamento     AS ID_DEPARTAMENTO
           , v_IdMunicipio        AS ID_MUNICIPIO
           , v_IdEntidadMunicipio AS ID_ENTIDADMUNICIPIO
           , FF.FIRMADIGITAL      AS IMAGENFIRMADIGITAL
      FROM TBROLES_USUARIO RU
      INNER JOIN TBROLES R ON R.ID = RU.ID_ROL AND RU.ID_USUARIO = v_Id
      INNER JOIN TBROLES_OPCIONESACCIONES ROA ON ROA.ID_ROL = R.ID AND ROA.CONSULTAR = 1
      INNER JOIN TBPROCESOS_REPORTES P ON P.ID_PROCESO = ROA.ID_OPCIONACCION AND P.ID_MENU = v_IdMenuAutenticacion
      LEFT  JOIN TBFUNCIONARIOFIRMA FF ON FF.ID_FUNCIONARIO = RU.ID_USUARIO;
    RETURN;

    <<failed_authentication>>
    OPEN p_Roles FOR SELECT 0 AS PERMISO, v_Id AS ID, v_Nombre AS NOMBRE, v_Resultado AS RESULTADO, v_ResultadoMensaje AS MENSAJE FROM DUAL;
  END;

  PROCEDURE CerrarSesionUsuario(P_ID NUMBER) AS
  BEGIN
    UPDATE TBUSUARIOS U SET U.SESIONES_RUV = 0, U.INTENTOSERRADOS = 0 WHERE U.ID = p_Id;
  END;

  PROCEDURE ObtenerInfoGeneral(p_Departamentos         OUT CURSOR_TYPE
                             , p_Municipios            OUT CURSOR_TYPE
                             , p_Parametros            OUT CURSOR_TYPE
                             , p_GruposParamDetalle    OUT CURSOR_TYPE
                             , p_GruposEtnicos         OUT CURSOR_TYPE
                             , p_ComunidadesEtnicas    OUT CURSOR_TYPE
                             , p_Poblaciones           OUT CURSOR_TYPE
                             , p_UnidadesTerritoriales OUT CURSOR_TYPE
                             , p_Paises                OUT CURSOR_TYPE
                             , p_Validaciones          OUT CURSOR_TYPE
                             , p_EntidadMunicipio      OUT CURSOR_TYPE
                             , p_CriticaN              OUT CURSOR_TYPE
                             , p_Causales              OUT CURSOR_TYPE) AS
  BEGIN
    /* Información Departamentos (Unicamente tabla TBGEOGRAFIA, Información Migrada de SIPOD) */
    OPEN p_Departamentos FOR
      SELECT ID, UPPER(NOMBRE) NOMBRE, PADREID, REPRESENTACION FROM TBGEOGRAFIA
      WHERE NIVEL = 2 ORDER BY NOMBRE;

    /* Información Municipios (Unicamente tabla TBGEOGRAFIA, Información Migrada de SIPOD) */
    OPEN p_Municipios FOR
      SELECT ID, UPPER(NOMBRE) NOMBRE, PADREID, CODTEL CODIGOTELEFONO, REPRESENTACION FROM TBGEOGRAFIA
      WHERE NIVEL = 3 ORDER BY NOMBRE;

    /* Información de parametros utilizados para RUV unicamente */
    OPEN p_Parametros FOR
      SELECT q1.*
        FROM (SELECT p.id,
                     p.id_tipoparametro "Tipo",
                     case when p.id_tipoparametro in(21,22,24,29,31,32,72,2134,2156,2160,2162)
                       then  CAST(p.numero AS VARCHAR2(3)) || ' ' || p.nombre
                         else p.nombre
                     end nombre,
                     nvl(p.otro, 0) "otro",
                     nvl(p.numero, 0) "numero"
                FROM tbparametros p
               WHERE p.id_tipoparametro IN (21,
                                            22,
                                            24,
                                            29,
                                            31,
                                            32,
                                            72,
                                            2108,
                                            2111,
                                            2134,
                                            2135,
                                            2136,
                                            2137,
                                            2138,
                                            2139,
                                            2141,
                                            2142,
                                            2144,
                                            2145,
                                            2146,
                                            2147,
                                            2148,
                                            2149,
                                            2152,
                                            2153,
                                            2154,
                                            2155,
                                            2156,
                                            2157,
                                            2158,
                                            2159,
                                            2160,
                                            2161,
                                            2162,
                                            2163,
                                            1164,
                                            1173,
                                            1931,
                                            1932,
                                            1933,
                                            1934,
                                            1935,
                                            1936,
                                            1937,
                                            1938,
                                            2171,
                                            2172)
                   AND ((p.id_tipoparametro = 21 AND P.NUMERO NOT IN(6,7,8,9))
                       OR p.id_tipoparametro <> 21)
                   ) q1
       ORDER BY q1.nombre;

    /* Información de las agrupaciones de parametros existentes para RUV */
    OPEN p_GruposParamDetalle fOR
      SELECT GRUPOPARAMETROID, PARAMETROID, ORDEN FROM TBGRUPOSPARAMDETALLE ORDER BY GRUPOPARAMETROID, PARAMETROID;

    /* Información de los grupos etnicos */
    OPEN p_GruposEtnicos FOR
      SELECT ID, ETNIAID, NOMBRE FROM TBETNIAGRUPOS ORDER BY NOMBRE;

    /* Información de las comunidades etnicas */
    OPEN p_ComunidadesEtnicas FOR
      SELECT ID, ETNIAGRUPOID GRUPOETNICOID, NUMERO|| '-' || NOMBRE NOMBRE, NUMERO FROM TBETNIACOMUNIDADES ORDER BY NUMERO;

    /* Información de Poblaciones (Barrios, localidades, veredas, corregimientos) Actualmente unicamente BogotÃ¡
     * Se encuentran actualmente en Sipod Pendiente Migrar a RUV */
    OPEN p_Poblaciones FOR
      SELECT ID, ID_ENTORNO, POBLADO NOMBRE, ID_MUNICIPIO, ID_POBLADOPADRE FROM TBPOBLADOS
      WHERE ID_ENTORNO IS NOT NULL AND ID_MUNICIPIO IS NOT NULL ORDER BY POBLADO;

    /* Información de las unidades territoriales (Actualmente no se utilizan) Se usaran cuando requieran
     * Información por unidad regional, posiblemnte para distribuciÃ³n y estadisticas (Pendiente migrar a RUV) */
    OPEN p_UnidadesTerritoriales FOR
      SELECT ID, NOMBRE FROM TBUNIDADESTERRITORIALES ORDER BY NOMBRE;

    /* Información de los paises */
    OPEN p_Paises FOR
      SELECT ID, UPPER(NOMBRE) NOMBRE, CODTEL CODIGOTELEFONO, REPRESENTACION FROM TBGEOGRAFIA
      WHERE NIVEL = 1 ORDER BY NOMBRE;

    /* Información del listado de validaciones de la Captura Unicamente */
    OPEN p_Validaciones FOR
      SELECT NOMBREHOJA, PROPIEDAD, VALOR FROM TBVALIDACIONESESTADO ORDER BY NOMBREHOJA;

    /* Información de Entidades relacionadas con los municipios (Información de distribuciÃ³n de formularios) */
    OPEN p_EntidadMunicipio FOR
      SELECT EM.ID, EM.ID_ENTIDAD, EM.ID_MUNICIPIO, E.NOMBRE, EM.NOMBRE AS NOMBRE_OTROS, EM.NOMBREENCARGADO
      FROM TBENTIDADMUNICIPIO EM
      INNER JOIN TBENTIDAD E ON EM.ID_ENTIDAD = E.ID;

    /* Preguntas para Critica 5 */
    OPEN p_CriticaN FOR
      SELECT ID, NOMBREPREGUNTA, ID_CAUSAL FROM TBCRITICAN;

    /* Informacion de causales de inclusion / no inclusion / devolucion */
    OPEN p_Causales FOR
      SELECT ID, NOMBRECAUSAL, PARTEEMOTIVA, TIPO FROM TBCAUSALES WHERE BHABILITADO = 1;

  END;

  PROCEDURE AuditarSesion(p_Usuario VARCHAR, p_Codigo NUMBER, p_InterfaseRed VARCHAR, p_IP VARCHAR) AS
  BEGIN
    IF NOT ((p_InterfaseRed IS NULL) AND (p_IP IS NULL)) THEN
      INSERT INTO TBAUDITORIA_SESION VALUES (SYSDATE, p_Usuario, p_Codigo, p_InterfaseRed, p_IP);
    END IF;
  END;

END PKG_REGISTROOFFLINE;
/
