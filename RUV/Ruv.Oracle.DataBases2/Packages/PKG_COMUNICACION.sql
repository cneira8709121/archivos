-- Create new package
create or replace PACKAGE PKG_COMUNICACION AS 

  PROCEDURE SP_OBTENERPERSONAS
  (
    PI_PAGENUMBER           NUMBER,
    PI_PAGESIZE             NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  );
  
  PROCEDURE SP_OBTENERPERSONAPORID
  (
    P_ID                    NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  );
  
  PROCEDURE SP_OBTENERPERSONAPORDOCUMENTO 
  (    
    PI_NUMERODOCUMENTO      VARCHAR2,
    PO_RESULTADO            OUT SYS_REFCURSOR
  );

  PROCEDURE SP_OBTENERHECHOSPORIDPERSONA
  (    
    P_ID                   NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  );
  
  PROCEDURE SP_OBTENERGRUPOFAMILIAR
  (    
    P_ID                    NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  );
  
END PKG_COMUNICACION;
/

-- Create package body
create or replace PACKAGE BODY PKG_COMUNICACION IS

  PROCEDURE SP_OBTENERPERSONAS
  (    
    pi_PageNumber           NUMBER,
    pi_PageSize             NUMBER,
    Po_Resultado            OUT SYS_REFCURSOR
  ) IS
                                  
  LOWERBOUND INT;
  UPPERBOUND INT;

  BEGIN
  
  LOWERBOUND := (PI_PAGENUMBER * PI_PAGESIZE) + 1;
  UPPERBOUND := ((PI_PAGENUMBER - 1) * PI_PAGESIZE) + 1;

  OPEN Po_Resultado FOR
    SELECT *
    FROM
      (SELECT A.*,
        ROWNUM RNUM
      FROM
         (SELECT  ID,
                  PRIMERNOMBRE,
                  SEGUNDONOMBRE,
                  PRIMERAPELLIDO,
                  SEGUNDOAPELLIDO,
                  FECHANACIMIENTO,
                  NUMERODOCUMENTO,
                  PARAM_TIPODOCUMENTO,
                  ESTAFALLECIDO,
                  PARAM_ESTADOCIVIL,
                  PARAM_GENERO,
                  PARAM_ETNIAPERTENECE,
                  ID_DEPARTAMENTO,
                  ID_MUNICIPIO
          FROM TBPERSONAS ORDER BY ID, ROWID
         )A
      WHERE ROWNUM < LOWERBOUND
      )
    WHERE RNUM >= UPPERBOUND;
    
  END;

  PROCEDURE SP_OBTENERPERSONAPORID
  (    
    P_ID                    NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  ) IS

  BEGIN

  OPEN PO_RESULTADO FOR
    SELECT  ID,
            PRIMERNOMBRE,
            SEGUNDONOMBRE,
            PRIMERAPELLIDO,
            SEGUNDOAPELLIDO,
            FECHANACIMIENTO,
            NUMERODOCUMENTO,
            PARAM_TIPODOCUMENTO
    FROM TBPERSONAS 
    WHERE ID = P_ID;
    
  END;

  PROCEDURE SP_OBTENERPERSONAPORDOCUMENTO 
  (    
    PI_NUMERODOCUMENTO      VARCHAR2,
    PO_RESULTADO            OUT SYS_REFCURSOR
  ) IS

  BEGIN

  OPEN PO_RESULTADO FOR
    SELECT  ID,
            PRIMERNOMBRE,
            SEGUNDONOMBRE,
            PRIMERAPELLIDO,
            SEGUNDOAPELLIDO,
            FECHANACIMIENTO,
            NUMERODOCUMENTO,
            PARAM_TIPODOCUMENTO
    FROM TBPERSONAS 
    WHERE NUMERODOCUMENTO = PI_NUMERODOCUMENTO;
    
  END;

  PROCEDURE SP_OBTENERHECHOSPORIDPERSONA
  (    
    P_ID                   NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  ) IS

  BEGIN

  OPEN PO_RESULTADO FOR
    /*
    SELECT  pkg_common.f_getnombrecompletopersona(rp.id_persona) AS victima,
            v.id_declaracion,
            sp.param_tipohecho,
            CASE WHEN sp.param_tipohecho <= 11 THEN (SELECT ph.nombre FROM tbparametros ph WHERE ph.numero = sp.param_tipohecho AND ph.id_tipoparametro = 2137) ELSE 'Censo Evento Masivo' END tipohecho,
            sp.fechasiniestro AS fecha,
            (SELECT te.nombre FROM tbparametros te WHERE te.ID = sp.param_tipo_entorno) AS tipoentorno,
            sp.otro_localidad_correg AS localidadcorregimiento,
            sp.otro_barrio_vereda AS barriovereda,
            dto.nombre AS departamento,
            mun.nombre AS municipio
    FROM    tbvaloracion v
            INNER JOIN tbvaloracion_anexo ta ON ta.id_valoracion = v."ID"
            INNER JOIN tbsiniestros_persona sp ON sp."ID" = ta.id_siniestro
            INNER JOIN tbregistros_personas rp ON rp."ID" = sp.id_regpersona
            LEFT JOIN tbgeografia dto ON dto."ID" = sp.id_departamento
            LEFT JOIN tbgeografia mun ON mun.ID = sp.id_municipio
    WHERE   RP.ID_PERSONA = PI_ID;
    */

    SELECT DISTINCT sp.fechasiniestro fecha_siniestro,
                    p.nombre nombre_hecho,
                    SP.PARAM_TIPOHECHO,
                    d."ID" id_declaracion,
                    d.numeroformulario numero_formulario,
                    sp.otro_localidad_correg AS localidadcorregimiento,
                    sp.otro_barrio_vereda AS barriovereda,
                    dto."ID" AS departamento,
                    mun."ID" AS municipio 
    FROM            TBREGISTROS_PERSONAS RP
      LEFT JOIN  TBSINIESTROS_PERSONA SP  ON SP.ID_REGPERSONA = RP."ID" AND RP.ESDECLARANTE = 1
      LEFT JOIN   TBPARAMETROS P          ON P.NUMERO = SP.PARAM_TIPOHECHO AND P.ID_TIPOPARAMETRO = 2137
      INNER JOIN  TBDECLARACIONES D       ON D."ID" = RP.ID_DECLARACION
      LEFT JOIN   TBGEOGRAFIA DTO         ON DTO."ID" = SP.ID_DEPARTAMENTO
      LEFT JOIN   TBGEOGRAFIA MUN         ON MUN.ID = SP.ID_MUNICIPIO
    WHERE   RP."ID_PERSONA" = P_ID;
    
  END;

  PROCEDURE SP_OBTENERGRUPOFAMILIAR
  (    
    P_ID                    NUMBER,
    PO_RESULTADO            OUT SYS_REFCURSOR
  ) IS

  BEGIN
    
  OPEN PO_RESULTADO FOR
  
    SELECT  RP.ID_DECLARACION IdDeclaracion, 
            D.numeroformulario numero_formulario,
            SP.fechasiniestro fecha_siniestro,
            RP.ID_PERSONA IdPersona,  
            PKG_COMMON.F_GETNOMBRECOMPLETOPERSONA(RP.ID_PERSONA) AS NombrePersona,
            PE.FECHANACIMIENTO FechaNacimiento,
            PA.NOMBRE PARENTESCO
    FROM TBREGISTROS_PERSONAS RP
      INNER JOIN TBPERSONAS PE    ON PE."ID" = RP.ID_PERSONA
      INNER JOIN TBPARAMETROS PA  ON PA.ID = RP.PARAM_RELACION
      INNER JOIN  TBDECLARACIONES D       ON D."ID" = RP.ID_DECLARACION
      LEFT OUTER JOIN  TBSINIESTROS_PERSONA SP  ON SP.ID_REGPERSONA = RP."ID" AND RP.ESDECLARANTE = 1
    WHERE RP.ID_DECLARACION IN 
    (
      SELECT ID_DECLARACION
      FROM TBREGISTROS_PERSONAS
      WHERE ID_PERSONA = P_ID
    );
    
  END;

END PKG_COMUNICACION;
/