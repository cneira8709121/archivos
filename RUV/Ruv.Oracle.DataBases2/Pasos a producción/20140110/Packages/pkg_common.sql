-- Create new package
create or replace package pkg_common as
  --Hechos Victimizantes
  HECHO_VICTIMIZANTE      NUMBER := 2137;
  DECLARACION_DEVUELTA    NUMBER := 10023;
  D_PENDIENTE_ASIGNAR_VAL NUMBER := 702;
  
  TYPE CURSOR_TYPE IS REF CURSOR;
	--Funciones
  FUNCTION f_getSiniestrosPersona
  (
     V_ID_REGPERSONA in NUMBER
  )return varchar2;

  FUNCTION f_getanexo_regper
  (
        anexo IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN VARCHAR2;

  FUNCTION f_getHechosVictimizantesDec
  (
    P_DeclaracionId IN INT
  )
  RETURN VARCHAR2;
  FUNCTION f_getCantidadPersonasPorHecho
  (
        anexo IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER;

  FUNCTION f_getHechosVictimizantesPer
  (
    P_RegPersona IN INT
  )RETURN VARCHAR2;
  FUNCTION f_PersonaAfectada
  (
        anexo IN NUMBER,
        id_regpersona IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER;
  FUNCTION f_PersonaVictma
  (
        anexo IN NUMBER,
        id_regpersona IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER;
 FUNCTION f_Principios( P_ValAnexoPerId NUMBER)
  RETURN VARCHAR2;
 function f_getNombreCompletoPersona ( V_ID_PERSONA NUMBER ) return varchar2;
 FUNCTION f_getDocumentoPersona( V_ID_PERSONA NUMBER ) RETURN varchar2;
 FUNCTION f_getDiscapacidades( P_IdRegPer NUMBER ) RETURN VARCHAR2;
 FUNCTION F_GETAFECTACIONES( P_ANEXOID NUMBER, P_IDTIPOANEXO NUMBER ) RETURN VARCHAR2;
  /*	DESCRIPCION: RETORNA LA LISTA DE TAREAS DE UN USUARIO EN ESPECIFICO
  **	AUTOR: 
  **	FECHA: 
  **	CAMBIOS:
  **    20121229 - JOHN HENAO
  **    1. SE CAMBIO LA MANERA DE REALIZAR EL COUNT DE DECLARACIONES POR PERSONA
  **		20130116 - JAIRO VALDERRAMA
  **		1. SE MODIFICA LA CONSULTA CON EL FIN DE EXCLUIR DEL CONTEO AQUELLAS DECLARACIONES
  **    QUE ESTAN EN ESTADO DECLARACION_DEVUELTA O D_PENDIENTE_ASIGNAR_VAL
  */
  FUNCTION F_USUARIOMENOSCARGA( P_ROL NUMBER ) RETURN NUMBER;
 
  /*Procedimientos*/
  PROCEDURE SP_SETDECLARACION_HISTORICO 
  (
   P_ID_DECLARACION    NUMBER,
   P_PARAM_ESTADO      NUMBER,
   P_ID_USUARIO        NUMBER
  );

  PROCEDURE SP_UPDESTADO_DECLARACION  
  (
   pi_IdDeclaracion  IN NUMBER,
   pi_IdUsuario      IN NUMBER,
   Pi_Param_Estado   IN NUMBER
  );

  /*	DESCRIPCION: RETORNA LA LISTA DE TAREAS DE UN USUARIO EN ESPECIFICO
  **	AUTOR: 
  **	FECHA: 
  **	CAMBIOS:
  **		20130116 - JAIRO VALDERRAMA
  **		1. SE MODIFICA LA CONSULTA DEBIDO A QUE LA SUBCONSULTA QUE TENIA GENERABA UN
  **    ERROR
  */
  PROCEDURE SP_LISTA_TAREAS 
  (
    P_ID_USUARIO IN NUMBER
   ,cu_result    out cursor_type
  );
  
 PROCEDURE SP_LISTATAREASCANTIDAD
  (
    P_ID_USUARIO        IN NUMBER,
    PO_RECORDCOUNT      OUT NUMBER
  );
                            
 PROCEDURE SP_LISTA_TAREAS_PAGINADO
  (
    P_ID_USUARIO        IN NUMBER,
    PI_PAGENUMBER       IN NUMBER,
    PI_PAGESIZE         IN NUMBER,
    PI_ORDER            IN VARCHAR2,
    PI_FILTER           IN VARCHAR2,
    CU_RESULT           OUT CURSOR_TYPE
  );
  
  /*	DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
  **	(NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
  **	AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
  **	FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
  **	CAMBIOS:
  **		20130608 - JAIRO VALDERRAMA
  **		1. SE MODIFICA EL SCRIPT DE TAL FORMA QUE TRAIGA LAS DECLARACIONES CON
  **    FORMULARIO VACÍO
  */
  PROCEDURE spListaTareasWPF(pi_IdUsuario           IN NUMBER   DEFAULT NULL
                           , pi_FechaRadicadoInicio IN DATE     DEFAULT NULL
                           , pi_FechaRadicadoFinal  IN DATE     DEFAULT NULL
                           , pi_NumeroFormulario    IN VARCHAR2 DEFAULT NULL                                   
                           , pi_PageNumber          IN NUMBER
                           , pi_PageSize            IN NUMBER
                           , po_Result              OUT CURSOR_TYPE);
                                    
  /*	DESCRIPCION: BREVE RESUMEN DEL PROCEDIMIENTO O FUNCION QUE AFECTA
  **	(NO USAR TILDES O CARACTERES ESPECIALES EN NINGUNA PARTE DE ESTOS COMENTARIOS)
  **	AUTOR: NOMBRE Y APELLIDO DE QUIEN REALIZA EL PROCEDIMIENTO O FUNCION
  **	FECHA: FECHA EN QUE SE REALIZA EL PROCEDIMIENTO O FUNCION
  **	CAMBIOS:
  **		20130608 - JAIRO VALDERRAMA
  **		1. SE MODIFICA EL SCRIPT DE TAL FORMA QUE TRAIGA LAS DECLARACIONES CON
  **    FORMULARIO VAC?O
  */
  PROCEDURE spListaTareasWPFCantidad(pi_IdUsuario           IN NUMBER
                                   , pi_FechaRadicadoInicio IN DATE     DEFAULT NULL
                                   , pi_FechaRadicadoFinal  IN DATE     DEFAULT NULL
                                   , pi_NumeroFormulario    IN VARCHAR2 DEFAULT NULL
                                   , po_RecordCount         OUT NUMBER);
  
  procedure SP_GETPARAMETROS
  (
    P_RESULT OUT SYS_REFCURSOR
  );

  /*-------------------------------------------------------
  ** Purpose : Procedimiento para Obtener Sub-Etnias
  ** Author  : John Henao
  ** Fecha   : 7/6/2013
  **-------------------------------------------------------
  */
  PROCEDURE SP_OBTIENESUBETNIAS(p_ID IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE SP_OBTENERGEOGRAFIACOMPLETA(PO_CURSOR OUT CURSOR_TYPE);
  
  PROCEDURE SP_OBTENERPAISES(PO_CURSOR OUT CURSOR_TYPE);
  
  PROCEDURE SP_OBTENERDEPARTAMENTOSPORPAIS(PI_IDPAIS NUMBER, PO_CURSOR OUT CURSOR_TYPE);
  
  PROCEDURE SP_OBTENERMUNIPORDEPARTAMENTO(pi_IdDepartamento NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE sp_ObtenerEntidadesPorMunicip(pi_IdMunicipio IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE sp_ObtenerPAyDTPorMunicipio(pi_IdMunicipio IN NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  PROCEDURE sp_ObtenerParametros(pi_TipoParametro NUMBER, po_Cursor OUT CURSOR_TYPE);
  
  /*-------------------------------------------------------
  ** Purpose : Procedimiento para Obtener la dirección del punto de notificación
  ** Author  : Ivan Suarez
  ** Fecha   : 12/09/2013
  ** TipoAtencion :  PuntoAtencion = 0,
                     DireccionTerritorial = 1,
                     Personeria = 2
  **-------------------------------------------------------
  */
  PROCEDURE sp_getDirPuntoNotificacion(PI_PUNTONOTIFICACION IN NUMBER,
                                       pi_TipoPunto         IN NUMBER,
                                       PI_CDIRECCION        OUT VARCHAR2);
                                       
  /*-------------------------------------------------------
  ** Purpose : Procedimiento para actualizar la dirección del punto de notificación
  ** Author  : Ivan Suarez
  ** Fecha   : 12/09/2013
  ** TipoAtencion :  PuntoAtencion = 0,
                     DireccionTerritorial = 1,
                     Personeria = 2
  --------------------------------------------------------
  */
  PROCEDURE sp_setDirPuntoNotificacion(PI_PUNTONOTIFICACION IN NUMBER,
                                       pi_TipoPunto         IN NUMBER,
                                       PI_CDIRECCION        IN VARCHAR2);

  TYPE string_array IS TABLE OF VARCHAR2(32767);
  FUNCTION SPLIT_STRING(STR IN VARCHAR2, DELIMITER IN CHAR DEFAULT ',') RETURN STRING_ARRAY;

end PKG_Common;
/

-- Create package body
create or replace PACKAGE BODY PKG_COMMON AS

/*******************************************
 * A Partir de aqui van las funciones
 *******************************************/

--Obtener hechos victimizantes/siniestros por persona
  FUNCTION f_getSiniestrosPersona (
    V_ID_REGPERSONA in NUMBER
  ) return varchar2 as
    c_anexos SYS_REFCURSOR;
    p_anexo varchar2(500);
    Result varchar2(500);
  begin
      OPEN c_anexos for
           select   P_S.Nombre
            FROM TBSINIESTROS_PERSONA S
            left join TBPARAMETROS P_S ON P_S.NUMERO = S.PARAM_TIPOHECHO AND P_S.ID_TIPOPARAMETRO = HECHO_VICTIMIZANTE
            WHERE
            S.Id_Regpersona =  V_ID_REGPERSONA
            AND S.ACTIVO = 1;

      LOOP
        FETCH c_anexos INTO p_anexo;
        EXIT WHEN c_anexos%NOTFOUND;
        dbms_output.put_line('p_anexo: '|| p_anexo);
        Result := Result || p_anexo || ', ';
     END LOOP;
     CLOSE c_anexos;

     if LENGTH(Result) > 0 then
       Result:= SUBSTR(Result, 0, LENGTH(Result) -2);
     end if;

    return(Result);
  end;


  /***********************************************************
  * Function description: Obtiene los hechos victimizantes por Persona
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_getanexo_regper
  (
        anexo IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN VARCHAR2
  AS
         v_query VARCHAR2(10000);
  BEGIN


    IF(anexo <> 5 AND anexo <> 11) THEN
        v_query := 'select   t.id_regpersona,
                             t.id as idAnexo
                    from     tbanexo' || anexo ||' t
                    where    t.id_siniestro = '|| siniestro ||'';
    END IF;
    IF (anexo = 5) THEN
      v_query := 'select   td.id_regpersona,
                           t.id as idAnexo
                  from     tbanexo5 t
                           inner JOIN tbanexo5_desplazados td ON td.id_anexo5 = t."ID"
                  where    t.id_siniestro = '|| siniestro ||'';
    END IF;
    IF (anexo = 11) THEN
      v_query := 'select   sp.id_regpersona,
                           ''''
                  from     tbsiniestros_persona sp
                  where    sp.ID = '|| siniestro ||'';
    END IF;

  RETURN(v_query);
  END;


  /***********************************************************
  * Function description: Obtiene los hechos victimizantes por Valoracion
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_getHechosVictimizantesDec(p_DeclaracionId IN INT) RETURN VARCHAR2 IS
    v_Personas SYS_REFCURSOR;
    v_Hecho    VARCHAR2(32767);
    v_Result   VARCHAR2(32767);
    v_Masivos  NUMBER := 0;
  BEGIN
    OPEN v_Personas FOR
      SELECT CASE SP.PARAM_TIPOHECHO WHEN 13 THEN 'Censo Masivo' ELSE HV.NOMBRE_HECHO_VICTIMIZANTE END
      FROM TBDECLARACIONES D
      INNER JOIN TBREGISTROS_PERSONAS RP ON RP.ID_DECLARACION = D.ID
      INNER JOIN TBSINIESTROS_PERSONA   SP ON SP.ID_REGPERSONA = RP.ID
      INNER JOIN TBHECHOS_VICTIMIZANTES HV ON HV.ID_HECHO_VICTIMIZANTE = SP.PARAM_TIPOHECHO
      WHERE D.ID = p_DeclaracionId;
    LOOP
      FETCH V_Personas INTO V_Hecho;
      EXIT WHEN V_Personas%NOTFOUND;
      IF v_Hecho <> 'Censo Masivo' THEN
        v_Result := v_Result || '-' || v_Hecho || ';';
      ELSE
        v_Masivos := v_Masivos + 1;
      END IF;
    END LOOP;
    CLOSE V_Personas;
    IF V_MASIVOS > 0 THEN
      v_Result := V_Result || ' ' || V_MASIVOS || ' Masivos ;';
    END IF;
    RETURN V_Result;
  END;
  
  /***********************************************************
  * Function description: Obtiene los hechos victimizantes por Persona
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_getHechosVictimizantesPer(p_RegPersona IN INT) RETURN VARCHAR2 IS
    v_Result VARCHAR2(32000);
    v_HechoTemp VARCHAR2(3200);
  BEGIN
    DECLARE CURSOR curHechos IS
      SELECT a.id_siniestro, a.id_regpersona, 1 Tipo FROM tbanexo1 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 2 Tipo FROM tbanexo2 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 3 Tipo FROM tbanexo3 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 4 Tipo FROM tbanexo4 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, ad.id_regpersona, 5 Tipo FROM tbanexo5 a INNER JOIN tbanexo5_desplazados ad ON ad.id_anexo5 = a.id WHERE ad.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 6 Tipo FROM tbanexo6 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 7 Tipo FROM tbanexo7 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 8 Tipo FROM tbanexo8 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 9 Tipo FROM tbanexo9 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 10 Tipo FROM tbanexo10 a WHERE a.id_regpersona = P_RegPersona
      UNION ALL
      SELECT a.id_siniestro, a.id_regpersona, 13 Tipo FROM tbanexo13 a WHERE a.id_regpersona = P_RegPersona;
    BEGIN
      FOR hecho IN curHechos LOOP
        IF hecho.Tipo < 11 THEN
          SELECT t.nombre INTO V_hechoTemp FROM tbsiniestros_persona tp
          INNER JOIN tbparametros t ON t.numero = tp.param_tipohecho AND t.id_tipoparametro = HECHO_VICTIMIZANTE
          WHERE  tp."ID" = hecho.id_siniestro;
        ELSE
          V_hechoTemp := 'Censo Masivo';
        END IF;
        V_Result := V_Result || '-' || V_hechoTemp ||';';
      END LOOP;
    END;
    RETURN V_Result;
  END;

  /***********************************************************
  * Function description: Obtiene La cantidad de personas por hecho
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_getCantidadPersonasPorHecho
  (
        anexo IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER
  AS
         v_query VARCHAR2(10000);
         v_resultado NUMBER;
  BEGIN
    v_query := 'select 0 from dual';
    IF(anexo <> 5 AND anexo <> 11 AND anexo <> 12 AND anexo > 0) THEN
      v_query := 'select   count(*)
                  from     tbanexo' || anexo ||' t
                  where    t.id_siniestro = '|| siniestro ||'';
    END IF;
    IF (anexo = 5) THEN
      v_query := 'select   count(*)
                  from     tbanexo5 t
                           inner JOIN tbanexo5_desplazados td ON td.id_anexo5 = t."ID"
                  where    t.id_siniestro = '|| siniestro ||'';
    END IF;
    IF (anexo = 11) THEN
      v_query := 'select 1 from dual';
    END IF;

    EXECUTE IMMEDIATE v_query INTO v_resultado;

  RETURN(v_resultado);
  END;
  /***********************************************************
  * Function description: Obtiene EstadoAfectacion
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_PersonaAfectada
  (
        anexo IN NUMBER,
        id_regpersona IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER
  AS
         v_query VARCHAR2(10000);
         v_resultado NUMBER;
  BEGIN

    IF(anexo <> 5 AND anexo <> 11) THEN
      v_query := 'select   t.afectado
                  from     tbanexo' || anexo ||' t
                  where    t.id_siniestro = '|| siniestro ||'
                           and t.id_regpersona = '|| id_regpersona ||' and rownum = 1';

    END IF;
    IF (anexo = 5) THEN
      v_query := 'select   td.se_desplazo
                  from     tbanexo5 t
                           inner JOIN tbanexo5_desplazados td ON td.id_anexo5 = t."ID"
                  where    t.id_siniestro = '|| siniestro ||'
                           and td.id_regpersona = '|| id_regpersona ||' and rownum = 1';


    END IF;
    IF (anexo >= 11) THEN
      v_query := 'select 0 from dual';
    END IF;

    DBMS_OUTPUT.PUT_LINE(v_query);

    EXECUTE IMMEDIATE v_query INTO v_resultado;

  RETURN(v_resultado);
  END;

  /***********************************************************
  * Function description: Obtiene EstadoVictima
  * Date:   27/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  FUNCTION f_PersonaVictma
  (
        anexo IN NUMBER,
        id_regpersona IN NUMBER,
        siniestro IN NUMBER
  )
  RETURN NUMBER
  AS
         v_query VARCHAR2(10000);
         v_resultado NUMBER;
  BEGIN


    IF(anexo <> 5 AND anexo <> 11) THEN
      v_query := 'select   t.victima
                  from     tbanexo' || anexo ||' t
                  where    t.id_siniestro = '|| siniestro ||'
                           and t.id_regpersona = '|| id_regpersona ||'
                           and rownum = 1';
    END IF;
    IF (anexo = 5) THEN
      v_query := 'select   td.se_desplazo
                  from     tbanexo5 t
                           inner JOIN tbanexo5_desplazados td ON td.id_anexo5 = t."ID"
                  where    t.id_siniestro = '|| siniestro ||'
                           and td.id_regpersona = '|| id_regpersona ||'
                           and rownum = 1';
    END IF;
    IF (anexo >= 11) THEN
      v_query := 'select 0 from dual';
    END IF;

    EXECUTE IMMEDIATE v_query INTO v_resultado;

  RETURN(v_resultado);
  END;


 FUNCTION f_Principios( P_ValAnexoPerId NUMBER)
 RETURN VARCHAR2 IS
        V_Result VARCHAR2(10000);
        V_Principios SYS_REFCURSOR;
        V_Principio VARCHAR2(10000);
 BEGIN
        OPEN V_Principios FOR
        SELECT    p.nombre
        FROM      tbprincipio p
                  INNER JOIN tbprincipio_val vp ON vp.id_principio = p."ID"
        WHERE     vp.id_val_anexo_per = P_ValAnexoPerId;


        LOOP
            FETCH V_Principios INTO V_Principio;
            EXIT WHEN V_Principios%NOTFOUND;
            V_Result := V_Result || V_Principio || ', ';
         END LOOP;

         CLOSE V_Principios;

      RETURN(V_Result);
 END;
 FUNCTION f_getNombreCompletoPersona( V_ID_PERSONA NUMBER )
 RETURN VARCHAR2 IS RESULT VARCHAR2(500);
 BEGIN
        SELECT  P.PRIMERNOMBRE || ' ' || P.SEGUNDONOMBRE  || ' ' ||  P.PRIMERAPELLIDO || ' ' ||  P.SEGUNDOAPELLIDO
        INTO   Result
        from    TBPERSONAS P where P.ID = V_ID_PERSONA;
      Result := replace(Result, '  ',  ' ');
      RETURN(RESULT);
 end;

 FUNCTION f_getDocumentoPersona( V_ID_PERSONA NUMBER )
 RETURN varchar2 IS Result varchar2(500);
 BEGIN
        SELECT     P.numerodocumento
        INTO       Result
        FROM       TBPERSONAS P
        WHERE      P.ID = V_ID_PERSONA;

      RETURN(Result);
 END;

 FUNCTION f_getDiscapacidades( P_IdRegPer NUMBER )
 RETURN VARCHAR2 IS
        V_Result VARCHAR2(10000);
        V_Discapasidades SYS_REFCURSOR;
        V_Discapasidad VARCHAR2(10000);
 BEGIN
        OPEN V_Discapasidades FOR
        SELECT      t.nombre
        FROM        tbdiscapacidad_persona tp
                    INNER JOIN tbparametros t ON t."ID" = tp.param_discapacidad
        WHERE       tp.id_regpersona = P_IdRegPer;

        LOOP
            FETCH V_Discapasidades INTO V_Discapasidad;
            EXIT WHEN V_Discapasidades%NOTFOUND;
            V_Result := V_Result || V_Discapasidad || ', ';
         END LOOP;

         CLOSE V_Discapasidades;

      RETURN(V_Result);
 END;

 FUNCTION f_getAfectaciones( P_AnexoId NUMBER, P_IdTipoAnexo NUMBER )
 RETURN VARCHAR2 IS
        V_Result VARCHAR2(10000);
        V_Afectaciones SYS_REFCURSOR;
        V_Afectacion VARCHAR2(10000);
 BEGIN
        OPEN V_Afectaciones FOR
        SELECT        par.nombre

        FROM          tbafectacion a
                      LEFT JOIN tbparametros par ON par."ID" = a.param_afectacion
        WHERE         a.id_anexo = P_AnexoId
                      AND a.param_tipo_hecho = P_IdTipoAnexo;


        LOOP
            FETCH V_Afectaciones INTO V_Afectacion;
            EXIT WHEN V_Afectaciones%NOTFOUND;
            V_Result := V_Result || V_Afectacion || ', ';
         END LOOP;

         CLOSE V_Afectaciones;

      RETURN(V_Result);
 END;


 FUNCTION F_USUARIOMENOSCARGA( P_Rol NUMBER )
  RETURN NUMBER IS Result NUMBER;
  BEGIN

    SELECT ID
    INTO   Result
    FROM   (SELECT ID
            FROM
              (
                SELECT Z.ID, Z.NUMERODECLARACIONES
                FROM
                (
                  SELECT NVL(X.ID, Y.ID) AS ID, NVL(X.NUMERODECLARACIONES, 0) AS NUMERODECLARACIONES
                  FROM
                  (
                    SELECT  RU.ID_USUARIO AS ID, COUNT(D.ID) AS NUMERODECLARACIONES
                    FROM    TBROLES_USUARIO RU INNER JOIN
                            TBUSUARIOS UU ON (UU.ID = RU.ID_USUARIO AND UU.ACTIVO = 1) LEFT JOIN
                            TBDECLARACIONES D ON RU.ID_USUARIO = D.ID_USUARIO_ACTUAL
                    WHERE   RU.ID_ROL = P_Rol AND
                            (D.PARAM_ESTADO <> DECLARACION_DEVUELTA AND
                            D.PARAM_ESTADO <> D_PENDIENTE_ASIGNAR_VAL OR
                            D.PARAM_ESTADO IS NULL)
                    GROUP BY RU.ID_USUARIO
                  ) X FULL OUTER JOIN
                  (
                    SELECT DISTINCT RU.ID_USUARIO AS ID
                    FROM  TBROLES_USUARIO RU 
                    INNER JOIN TBUSUARIOS UU ON (UU.ID = RU.ID_USUARIO AND UU.ACTIVO = 1) 
                    LEFT JOIN TBDECLARACIONES D ON RU.ID_USUARIO = D.ID_USUARIO_ACTUAL
                    WHERE  RU.ID_ROL = P_Rol AND
                          (D.PARAM_ESTADO = DECLARACION_DEVUELTA OR
                          D.PARAM_ESTADO = D_PENDIENTE_ASIGNAR_VAL)
                  ) Y ON X.ID = Y.ID
                ) Z
                ORDER BY Z.NUMERODECLARACIONES
              )
             WHERE ROWNUM = 1);
    RETURN (Result);
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

/*******************************************
 * A Partir de aqui van los Procedimientos
 *******************************************/

 /***********************************************************
 * Procedure description: Inserta en la tabla tbhistorial_estado
 * Date:   04/09/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date   Modified By     Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE SP_SETDECLARACION_HISTORICO
 (
      P_ID_DECLARACION    NUMBER,
      P_PARAM_ESTADO      NUMBER,
      P_ID_USUARIO        NUMBER
 )
 AS
      V_ID                NUMBER := 0;
 BEGIN
  V_ID := SEQ_TBDECLARACION_HISTORICO.NEXTVAL;
  INSERT INTO tbdeclaracion_historico
  (
    "ID",
    id_declaracion,
    param_estado,
    fecha_asignacion,
    id_usuario_responsable
  )
  VALUES
  (
    V_ID,
    P_ID_DECLARACION,
    P_PARAM_ESTADO,
    SYSDATE,
    P_ID_USUARIO
  );

 EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error :'||SQLCODE||SQLERRM);
    RAISE;
 END;

 /***********************************************************
 * Procedure description: Update Declaracion Status
 * Date:   07/09/2012
 * Author: Cristian Neira
 *
 * Changes
 * Date   Modified By     Comments
 ************************************************************
 *
 ************************************************************/
 PROCEDURE SP_UPDESTADO_DECLARACION
 (
  pi_IdDeclaracion  IN NUMBER,
  pi_IdUsuario      IN NUMBER,
  Pi_Param_Estado   IN NUMBER
 )
 AS
 BEGIN
   
   UPDATE tbdeclaraciones
   SET    PARAM_ESTADO = Pi_Param_Estado,
          id_usuario_actual = pi_IdUsuario
   WHERE  ID = pi_IdDeclaracion;
   PKG_COMMON.sp_setdeclaracion_historico(pi_IdDeclaracion, Pi_Param_Estado, pi_IdUsuario);
   
 EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error :'||SQLCODE||SQLERRM);
    RAISE;
 END;

  PROCEDURE SP_LISTA_TAREAS (
    P_ID_USUARIO IN NUMBER
   ,CU_RESULT    OUT CURSOR_TYPE
  ) AS
  -- RADICADO_PENDIENTE_CAPTURA   NUMBER := 704;
  -- Radicado_inicia_captura      number := 737;
  -- Captura_Pendiente_por_Validar number:= 10011;
  -- Radicado_Pendiente_Critica_N number:= 10015;
  BEGIN
    OPEN CU_RESULT FOR
        SELECT D.ID AS DECLARACION,
               (SELECT MAX(ID) FROM TBDECLARACION_HISTORICO WHERE ID_DECLARACION = D.ID AND PARAM_ESTADO = D.PARAM_ESTADO) ID,
               NVL((SELECT MAX(FECHA_ASIGNACION) FROM TBDECLARACION_HISTORICO WHERE ID_DECLARACION = D.ID AND PARAM_ESTADO = D.PARAM_ESTADO), D.FECHADECLARACION) FECHA,
               P.NOMBRE AS ACCION,
               D.NUMEROFORMULARIO AS FORMULARIO
        FROM TBDECLARACIONES D
        INNER JOIN TBPARAMETROS P ON P.ID = D.PARAM_ESTADO
        WHERE D.ID_USUARIO_ACTUAL = P_ID_USUARIO
          AND     P.ID <> DECLARACION_DEVUELTA
          AND     P.ID <> D_PENDIENTE_ASIGNAR_VAL
        ORDER BY FECHA, ACCION;
  END;
  
  PROCEDURE spListaTareasWPF(pi_IdUsuario           IN NUMBER   DEFAULT NULL
                           , pi_FechaRadicadoInicio IN DATE     DEFAULT NULL
                           , pi_FechaRadicadoFinal  IN DATE     DEFAULT NULL
                           , pi_NumeroFormulario    IN VARCHAR2 DEFAULT NULL
                           , pi_PageNumber          IN NUMBER
                           , pi_PageSize            IN NUMBER
                           , po_Result              OUT CURSOR_TYPE) IS 
  
   startRow NUMBER;
   endRow   NUMBER;
  BEGIN
    startRow := ((pi_PageNumber - 1) * pi_PageSize) + 1;
    endRow := (pi_PageNumber * pi_PageSize) + 1;
    OPEN po_Result FOR
      SELECT *
      FROM (SELECT INFO.*, ROWNUM AS R
            FROM (SELECT DCL.ID               AS DECLARACION
                       , DHS.ID               AS ID
                       , EST.NOMBRE           AS ACCION
                       , DCL.NUMEROFORMULARIO AS FORMULARIO
                       , COALESCE(DHS.FECHA_ASIGNACION, DCL.FECHADECLARACION, DCL.FECHAREGISTRO) AS FECHA
                       , RAD.FECHALLEGADA                                                        AS FECHARADICACION
                  FROM (SELECT D.ID
                             , D.NUMEROFORMULARIO
                             , D.PARAM_ESTADO
                             , D.ID_USUARIO_ACTUAL
                             , D.FECHADECLARACION
                             , D.FECHAREGISTRO
                             , R.ID_RADICACION
                             , H.ID_DECLARACIONHISTORICO
                        FROM TBDECLARACIONES D
                        INNER JOIN (SELECT ID_DECLARACION
                                         , MAX(ID) AS ID_RADICACION
                                    FROM TBRADICACION 
                                    GROUP BY ID_DECLARACION) R ON R.ID_DECLARACION = D.ID
                        LEFT JOIN (SELECT ID_DECLARACION
                                        , PARAM_ESTADO
                                        , ID_USUARIO_RESPONSABLE
                                        , MAX(ID) ID_DECLARACIONHISTORICO
                                   FROM TBDECLARACION_HISTORICO
                                   GROUP BY ID_DECLARACION, PARAM_ESTADO, ID_USUARIO_RESPONSABLE) H ON (H.ID_DECLARACION         = D.ID 
                                                                                                    AND H.PARAM_ESTADO           = D.PARAM_ESTADO
                                                                                                    AND H.ID_USUARIO_RESPONSABLE = D.ID_USUARIO_ACTUAL)
                        WHERE D.ID_USUARIO_ACTUAL = pi_IdUsuario
                          AND D.PARAM_ESTADO NOT IN (DECLARACION_DEVUELTA, D_PENDIENTE_ASIGNAR_VAL)) DCL
                  INNER JOIN TBRADICACION RAD ON RAD.ID = DCL.ID_RADICACION
                  INNER JOIN TBPARAMETROS EST ON EST.ID = DCL.PARAM_ESTADO
                  LEFT  JOIN TBDECLARACION_HISTORICO DHS ON DHS.ID = DCL.ID_DECLARACIONHISTORICO
                  /* Filtros */
                  WHERE DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_NumeroFormulario, DCL.NUMEROFORMULARIO) || '%'
                    AND RAD.FECHALLEGADA BETWEEN TRUNC(NVL(pi_FechaRadicadoInicio, RAD.FECHALLEGADA), 'DD') AND CASE WHEN pi_FechaRadicadoFinal IS NULL THEN RAD.FECHALLEGADA ELSE TRUNC(pi_FechaRadicadoFinal, 'DD') + 1 - (1 / (24 * 60 * 60)) END
                  ORDER BY FECHA DESC) INFO
            WHERE ROWNUM < endRow)
      WHERE R >= startRow;
  END;
  
  PROCEDURE spListaTareasWPFCantidad(pi_IdUsuario           IN NUMBER
                                   , pi_FechaRadicadoInicio IN DATE     DEFAULT NULL
                                   , pi_FechaRadicadoFinal  IN DATE     DEFAULT NULL
                                   , pi_NumeroFormulario    IN VARCHAR2 DEFAULT NULL
                                   , po_RecordCount         OUT NUMBER) IS
  BEGIN
    SELECT COUNT(1) INTO po_RecordCount
    FROM (SELECT D.ID
               , D.NUMEROFORMULARIO
               , D.PARAM_ESTADO
               , D.ID_USUARIO_ACTUAL
               , D.FECHADECLARACION
               , R.ID_RADICACION
          FROM TBDECLARACIONES D
          INNER JOIN (SELECT ID_DECLARACION
                           , MAX(ID) AS ID_RADICACION
                      FROM TBRADICACION 
                      GROUP BY ID_DECLARACION) R ON R.ID_DECLARACION = D.ID
          WHERE D.ID_USUARIO_ACTUAL = pi_IdUsuario
            AND D.PARAM_ESTADO NOT IN (DECLARACION_DEVUELTA, D_PENDIENTE_ASIGNAR_VAL)) DCL
    INNER JOIN TBRADICACION RAD ON RAD.ID = DCL.ID_RADICACION
    INNER JOIN TBPARAMETROS EST ON EST.ID = DCL.PARAM_ESTADO
    /* Filtros */
    WHERE DCL.NUMEROFORMULARIO LIKE '%' || NVL(pi_NumeroFormulario, DCL.NUMEROFORMULARIO) || '%'
      AND RAD.FECHALLEGADA BETWEEN TRUNC(NVL(pi_FechaRadicadoInicio, RAD.FECHALLEGADA), 'DD') AND CASE WHEN pi_FechaRadicadoFinal IS NULL THEN RAD.FECHALLEGADA ELSE TRUNC(pi_FechaRadicadoFinal, 'DD') + 1 - (1 / (24 * 60 * 60)) END;
  END;

 PROCEDURE SP_LISTATAREASCANTIDAD
  (
    P_ID_USUARIO        IN NUMBER,
    PO_RECORDCOUNT      OUT NUMBER
  ) AS

BEGIN

    SELECT COUNT(1) INTO PO_RECORDCOUNT  FROM(
                                              SELECT           'DECLARACION'         AS TIPO,
                                                               DH.FECHA_ASIGNACION   AS FECHA,
                                                               P.NOMBRE              AS ACCION,
                                                               P.ID                  AS IDACCION,
                                                               D.NUMEROFORMULARIO    AS FORMULARIO,
                                                               D.ID                  AS DECLARACION,
                                                               NULL                  AS CORRECCION,
                                                               NULL                  AS REGPERSONA
                                                        FROM TBDECLARACIONES D
                                                        INNER      JOIN TBPARAMETROS P ON (P.ID = D.PARAM_ESTADO)
                                                        LEFT OUTER JOIN (SELECT ID_DECLARACION, PARAM_ESTADO, ID_USUARIO_RESPONSABLE, MAX(ID) IDMAX
                                                                         FROM TBDECLARACION_HISTORICO
                                                                         GROUP BY ID_DECLARACION, PARAM_ESTADO, ID_USUARIO_RESPONSABLE) HH ON (HH.ID_DECLARACION         = D.ID
                                                                                                                                           AND HH.PARAM_ESTADO           = D.PARAM_ESTADO
                                                                                                                                           AND HH.ID_USUARIO_RESPONSABLE = D.ID_USUARIO_ACTUAL)
                                                        LEFT OUTER JOIN TBDECLARACION_HISTORICO DH ON (DH.ID = HH.IDMAX)
                                                        WHERE D.ID_USUARIO_ACTUAL = P_ID_USUARIO
                                                        UNION ALL
                                                        SELECT 'CORRECCION'                     AS TIPO,
                                                               CC.FECHASOLICITUD                AS FECHA,
                                                               'PENDIENTE APROBAR CORRECCION'   AS ACCION,
                                                               0                                AS IDACCION,
                                                               D.NUMEROFORMULARIO               AS FORMULARIO,
                                                               D.ID                             AS DECLARACION,
                                                               CC.ID                            AS CORRECCION,
                                                               CC.ID_REGPERSONA                 AS REGPERSONA
                                                        FROM TBCORRECCION CC
                                                        INNER JOIN TBREGISTROS_PERSONAS RP ON (RP.ID = CC.ID_REGPERSONA)
                                                        INNER JOIN TBDECLARACIONES      D  ON (D.ID = RP.ID_DECLARACION)
                                                        WHERE CC.ESTADO = 2 AND CC.ID_USUARIO = P_ID_USUARIO
                                            ) CONTADOR;

    END;

 PROCEDURE SP_LISTA_TAREAS_PAGINADO
  (
    P_ID_USUARIO        IN NUMBER,
    PI_PAGENUMBER       IN NUMBER,
    PI_PAGESIZE         IN NUMBER,
    PI_ORDER            IN VARCHAR2,
    PI_FILTER           IN VARCHAR2,
    CU_RESULT           OUT CURSOR_TYPE
  ) AS

    LOWERBOUND    INT;
    UPPERBOUND    INT;
    STR_SQL       VARCHAR2(10000);
    V_ORDER       VARCHAR2(50);
    V_FILTER      VARCHAR2(100);


  BEGIN

    LOWERBOUND := (PI_PAGENUMBER * PI_PAGESIZE) + 1;
    UPPERBOUND := ((PI_PAGENUMBER - 1) * PI_PAGESIZE) + 1;

    IF PI_ORDER IS NULL THEN
        V_ORDER := 'DECLARACION';
      ELSE
        V_ORDER := PI_ORDER;
    END IF;

    IF PI_FILTER IS NULL THEN
        V_FILTER := ' ';
      ELSE
        V_FILTER := 'WHERE ' || PI_FILTER;
    END IF;

    STR_SQL :=
     ' SELECT * FROM ( ' ||
     '  SELECT TOTAL.*, ROW_NUMBER() OVER (ORDER BY ' || V_ORDER || ' ) FILA FROM ( ' ||
     '   SELECT ''DECLARACION''       AS TIPO, ' ||
     '          NVL(TRUNC(DH.FECHA_ASIGNACION,''DD''), TRUNC(D.FECHADECLARACION,''DD''))   AS FECHA, ' ||
     '          NVL(DH.FECHA_ASIGNACION, D.FECHADECLARACION)   AS FECHALLEGADA, ' ||
     '          P.NOMBRE              AS ESTADO, ' ||
     '          P.ID                  AS IDACCION, ' ||
     '          D.NUMEROFORMULARIO    AS FORMULARIO, ' ||
     '          D.ID                  AS DECLARACION, ' ||
     '          NULL                  AS CORRECCION, ' ||
     '          NULL                  AS REGPERSONA ' ||
     '   FROM TBDECLARACIONES D ' ||
     '   INNER      JOIN TBPARAMETROS P ON (P.ID = D.PARAM_ESTADO) ' ||
     '   LEFT OUTER JOIN (SELECT ID_DECLARACION, PARAM_ESTADO, ID_USUARIO_RESPONSABLE, MAX(ID) IDMAX FROM TBDECLARACION_HISTORICO ' ||
     '                    GROUP BY ID_DECLARACION, PARAM_ESTADO, ID_USUARIO_RESPONSABLE) HH ON (HH.ID_DECLARACION = D.ID AND HH.PARAM_ESTADO = D.PARAM_ESTADO AND HH.ID_USUARIO_RESPONSABLE = D.ID_USUARIO_ACTUAL) ' ||
     '   LEFT OUTER JOIN TBDECLARACION_HISTORICO DH ON (DH.ID = HH.IDMAX) ' ||
     '   WHERE D.ID_USUARIO_ACTUAL = :P_ID_USUARIO ' ||
     '   UNION ALL ' ||
     '   SELECT ''CORRECCION''                   AS TIPO, ' ||
     '          TRUNC(CC.FECHASOLICITUD,''DD'')  AS FECHA, ' ||
     '          CC.FECHASOLICITUD                AS FECHALLEGADA, ' ||
     '          ''PENDIENTE APROBAR CORRECCION'' AS ESTADO, ' ||
     '          0                                AS IDACCION, ' ||
     '          D.NUMEROFORMULARIO               AS FORMULARIO, ' ||
     '          D.ID                             AS DECLARACION, ' ||
     '          CC.ID                            AS CORRECCION, ' ||
     '          CC.ID_REGPERSONA                 AS REGPERSONA ' ||
     '   FROM TBCORRECCION CC ' ||
     '   INNER JOIN TBREGISTROS_PERSONAS RP ON (RP.ID = CC.ID_REGPERSONA) ' ||
     '   INNER JOIN TBDECLARACIONES      D  ON (D.ID = RP.ID_DECLARACION) ' ||
     '   WHERE CC.ESTADO = 2 AND CC.ID_USUARIO = :P_ID_USUARIO) Total ' ||
     '   ' || V_FILTER || ' ' ||
     ' ) ' ||
     ' WHERE FILA BETWEEN :UPPERBOUND AND :LOWERBOUND';

    DBMS_OUTPUT.PUT_LINE(STR_SQL);

    OPEN CU_RESULT FOR (STR_SQL) USING P_ID_USUARIO, P_ID_USUARIO, UPPERBOUND, LOWERBOUND;

  END;


  /***********************************************************
  * Procedure description: Trae la lista de Parametros
  * Date:   28/03/2012
  * Author: Cristian Neira
  *
  * Changes
  * Date    Modified By      Comments
  ************************************************************
  *
  ************************************************************/
  PROCEDURE sp_GetParametros
  (
    P_Result OUT SYS_REFCURSOR
  )
  IS
  BEGIN

       OPEN P_Result FOR
        SELECT
          t."ID",
          t.nombre,
          t.id_tipoparametro
        FROM
          TBPARAMETROS T
        WHERE t.id_tipoparametro IN(2135,21,24,2137,123,2155,29,22,2134,31);
  EXCEPTION
    WHEN OTHERS THEN
      RAISE;
  end;

 /*-------------------------------------------------------
Purpose : Procedimiento para Obtener Sub-Etnias
Author  : John Henao
Fecha   : 7/6/2013
--------------------------------------------------------
*/

  PROCEDURE SP_OBTIENESUBETNIAS(p_ID IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT EC.ID, 
             EC.ETNIAGRUPOID,
             EC.NOMBRE,        
             EC.NUMERO
      FROM TBPARAMETROS ET
      INNER JOIN TBETNIACOMUNIDADES EC ON EC.ETNIAGRUPOID = ET.NUMERO
      WHERE ET.ID = p_ID
      ORDER BY EC.ETNIAGRUPOID;
  END;

  PROCEDURE sp_ObtenerGeografiaCompleta(po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID             AS ID
           , NOMBRE         AS NOMBRE
           , NIVEL          AS TIPO
           , PADREID        AS PADRE
           , REPRESENTACION
           , CODIGOCODAZZI
      FROM TBGEOGRAFIA ORDER BY ID;
  END;
  
  PROCEDURE SP_OBTENERPAISES(po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID             AS ID
           , NOMBRE         AS NOMBRE
           , NIVEL          AS TIPO
           , PADREID        AS PADRE
           , REPRESENTACION
           , CODIGOCODAZZI
      FROM TBGEOGRAFIA WHERE NIVEL = 1
      ORDER BY NOMBRE;
  END;
  
  PROCEDURE SP_OBTENERDEPARTAMENTOSPORPAIS(pi_IdPais NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID             AS ID
           , NOMBRE         AS NOMBRE
           , NIVEL          AS TIPO
           , PADREID        AS PADRE
           , REPRESENTACION
           , CODIGOCODAZZI
      FROM TBGEOGRAFIA WHERE NIVEL = 2 AND PADREID = pi_IdPais
      ORDER BY NOMBRE;
  END;
  
  PROCEDURE SP_OBTENERMUNIPORDEPARTAMENTO(pi_IdDepartamento NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID             AS ID
           , NOMBRE         AS NOMBRE
           , NIVEL          AS TIPO
           , PADREID        AS PADRE
           , REPRESENTACION
           , CODIGOCODAZZI
      FROM TBGEOGRAFIA WHERE NIVEL = 3 AND PADREID = pi_IdDepartamento
      ORDER BY NOMBRE;
  END;
  
  PROCEDURE sp_ObtenerEntidadesPorMunicip(pi_IdMunicipio IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID
           , ID_ENTIDAD
           , NOMBRE
           , ID_MUNICIPIO
           , DIRECCIONENTIDAD
      FROM TBENTIDADMUNICIPIO WHERE ID_MUNICIPIO = pi_IdMunicipio
      ORDER BY NOMBRE;         
  END;
  
  PROCEDURE sp_ObtenerPAyDTPorMunicipio(pi_IdMunicipio IN NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      select case when idpuntoatencion is null then iddireccionterritorial
                  when iddireccionterritorial is null then idpuntoatencion
                  end as ID,
             case when idpuntoatencion is null then 'DT-' || iddireccionterritorial
                  when iddireccionterritorial is null then 'PA-' || idpuntoatencion
                  end AS HASHID,
             case when idpuntoatencion is null then dt.direccion
                  when iddireccionterritorial is null then pa.direccion
                  end AS DIRECCION,
             case when idpuntoatencion is null then dt.nombre
                  when iddireccionterritorial is null then pa.nombre
                  end AS NOMBRE,
             rn.IDMUNICIPIO
      from tbreglasnotificacion rn
      left outer join tbpuntoatencion pa on pa.id = rn.idpuntoatencion 
      left outer join tbdireccionterritorial dt on dt.id = rn.iddireccionterritorial
      WHERE rn.IDMUNICIPIO = pi_IdMunicipio
      ORDER BY NOMBRE;
  END;

  PROCEDURE sp_ObtenerParametros(pi_TipoParametro NUMBER, po_Cursor OUT CURSOR_TYPE) IS
  BEGIN
    OPEN po_Cursor FOR
      SELECT ID, NOMBRE FROM TBPARAMETROS WHERE ID_TIPOPARAMETRO = pi_TipoParametro;
  END;
  
  /*-------------------------------------------------------
  Purpose : Procedimiento para Obtener la dirección del punto de notificación
  Author  : Ivan Suarez
  Fecha   : 12/09/2013
  TipoAtencion :  PuntoAtencion = 0,
                  DireccionTerritorial = 1,
                  Personeria = 2
  --------------------------------------------------------
  */
  PROCEDURE sp_getDirPuntoNotificacion(PI_PUNTONOTIFICACION IN NUMBER,
                                       pi_TipoPunto         IN NUMBER,
                                       PI_CDIRECCION        OUT VARCHAR2)                                    
  IS
  BEGIN
    IF pi_TipoPunto = 0 THEN
      SELECT DIRECCION INTO PI_CDIRECCION 
      FROM TBPUNTOATENCION
      WHERE ID = PI_PUNTONOTIFICACION;
    ELSIF pi_TipoPunto = 1 THEN
      SELECT DIRECCION INTO PI_CDIRECCION 
      FROM TBDIRECCIONTERRITORIAL
      WHERE ID = PI_PUNTONOTIFICACION;
    END IF;
  END sp_getDirPuntoNotificacion;

  /*-------------------------------------------------------
  Purpose : Procedimiento para actualizar la dirección del punto de notificación
  Author  : Ivan Suarez
  Fecha   : 12/09/2013
  TipoAtencion :  PuntoAtencion = 0,
                  DireccionTerritorial = 1,
                  Personeria = 2
  --------------------------------------------------------
  */
  PROCEDURE sp_setDirPuntoNotificacion(PI_PUNTONOTIFICACION IN NUMBER,
                                       pi_TipoPunto         IN NUMBER,
                                       PI_CDIRECCION        IN VARCHAR2) 
  IS
  BEGIN
    IF pi_TipoPunto = 0 THEN
      UPDATE TBPUNTOATENCION
        SET   DIRECCION = PI_CDIRECCION
            , TEXTODIRECCION = PI_CDIRECCION
      WHERE ID = PI_PUNTONOTIFICACION;
    ELSIF pi_TipoPunto = 1 THEN
      UPDATE TBDIRECCIONTERRITORIAL
        SET   DIRECCION = PI_CDIRECCION
            , TEXTODIRECCION = PI_CDIRECCION
      WHERE ID = PI_PUNTONOTIFICACION;
    END IF;
  END sp_setDirPuntoNotificacion;

END PKG_COMMON;
/