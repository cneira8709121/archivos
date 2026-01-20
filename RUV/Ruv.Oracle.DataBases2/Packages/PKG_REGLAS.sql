-- Create PKG_REGLAS package
create or replace PACKAGE PKG_REGLAS AS
  
  --TIPOS PUNTO DE NOTIFICACION
  PUNTOATENCION  NUMBER  := 0;
  DIRECCIONTERRITORIAL  NUMBER  := 1;
  
  PROCEDURE sp_getContPtoNotificacion(p_municipio NUMBER, p_punto varchar2, p_tipoPto int, p_conteo OUT int);
  
  PROCEDURE sp_setPuntos(p_idPunto   in out int, 
                         p_nombre    varchar2,
                         p_direccion varchar2,
                         p_sede      varchar2,
                         p_municipio int,
                         p_tipoPto   int);
  
  PROCEDURE sp_setPuntoNotificacion(p_municipio NUMBER, 
                                    p_idPunto number, 
                                    p_tipoPto int);
                                    
  PROCEDURE sp_getVerificarPunto(p_municipio NUMBER, 
                                 p_idPunto number, 
                                 p_tipoPto int,
                                 verificar out int);
                                 
  PROCEDURE sp_setCitacion(p_idPunto   in out int, 
                           p_nombre    varchar2,
                           p_direccion varchar2,
                           p_depto     varchar2,
                           p_municipio varchar2,
                           p_tipoPto   int);
                           
  PROCEDURE sp_setReglaCitacion(p_idPunto   int, 
                                p_municipio int,
                                p_tipoPto   int);
                                
  PROCEDURE sp_getVerificarCitacion(p_municipio NUMBER, 
                                    p_idPunto number, 
                                    p_tipoPto int,
                                    verificar out int);           
END PKG_REGLAS;
/

-- Create PKG_REGLAS package body
create or replace PACKAGE BODY PKG_REGLAS AS
  
  PROCEDURE sp_getContPtoNotificacion(p_municipio NUMBER, p_punto varchar2, p_tipoPto int, p_conteo OUT int)IS
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      SELECT COUNT(*) INTO p_conteo FROM TBPUNTOATENCION
      WHERE IDMUNICIPIO = p_municipio AND
            UPPER(REEMPLAZATILDES(NOMBRE)) = UPPER(REEMPLAZATILDES(p_punto));
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      SELECT COUNT(*) INTO p_conteo FROM tbdireccionterritorial
      WHERE IDMUNICIPIO = p_municipio AND
            UPPER(REEMPLAZATILDES(NOMBRE)) = UPPER(REEMPLAZATILDES(p_punto));
    END IF;
  END;
  
  PROCEDURE sp_setPuntos(p_idPunto   in out int, 
                         p_nombre    varchar2,
                         p_direccion varchar2,
                         p_sede      varchar2,
                         p_municipio int,
                         p_tipoPto   int) IS
    p_adjetivo varchar2(10);
    conteo int := 0;
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      
      IF p_nombre LIKE 'CENTRO%' THEN
        p_adjetivo := 'del ';
        SELECT COUNT(1) into conteo FROM TBPUNTOATENCION WHERE ID = p_idPunto;
      ELSE
        p_adjetivo := 'de la ';
        SELECT COUNT(1) into conteo 
        FROM TBPUNTOATENCION 
        WHERE UPPER(REEMPLAZATILDES(nombre)) LIKE UPPER(REEMPLAZATILDES(p_nombre));
      END IF;
      
      IF conteo = 1 THEN
      
        IF p_nombre NOT LIKE 'CENTRO%' THEN
          SELECT ID into p_idPunto
          FROM TBPUNTOATENCION 
          WHERE UPPER(REEMPLAZATILDES(nombre)) LIKE UPPER(REEMPLAZATILDES(p_nombre));
        END IF;
        
        UPDATE TBPUNTOATENCION
          SET NOMBRE = p_nombre,
              TEXTONOMBRE = p_adjetivo || p_nombre,
              DIRECCION = p_direccion,
              TEXTODIRECCION = p_direccion,
              IDMUNICIPIO = p_municipio
        WHERE ID = p_idPunto;
      ELSIF conteo = 0 THEN
        INSERT INTO TBPUNTOATENCION(ID, NOMBRE, TEXTONOMBRE, IDMUNICIPIO, DIRECCION, TEXTODIRECCION)
        VALUES(p_idPunto, p_nombre, p_adjetivo || p_nombre, p_municipio, p_direccion, p_direccion);
      END IF;
      
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      SELECT COUNT(1) into conteo FROM TBDIRECCIONTERRITORIAL WHERE ID = p_idPunto;
      
      IF conteo = 1 THEN
        UPDATE TBDIRECCIONTERRITORIAL
          SET NOMBRE = p_nombre,
              TEXTONOMBRE = 'de la ' || p_nombre,
              DIRECCION = p_direccion,
              TEXTODIRECCION = p_direccion,
              IDMUNICIPIO = p_municipio
        WHERE ID = p_idPunto;
      ELSIF conteo = 0 THEN
        INSERT INTO TBDIRECCIONTERRITORIAL(ID, IDMUNICIPIO, NOMBRE, TEXTONOMBRE, DIRECCION, TEXTODIRECCION)
        VALUES(p_idPunto, p_municipio, p_nombre, 'de la ' || p_nombre, p_direccion, p_direccion);
      END IF;
    END IF;
    
    commit;
  END;
  
  PROCEDURE sp_setPuntoNotificacion(p_municipio NUMBER, 
                                    p_idPunto number, 
                                    p_tipoPto int)IS
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      INSERT INTO TBREGLASNOTIFICACION(ID,IDMUNICIPIO,IDPUNTOATENCION)
      VALUES(SEQ_TBREGLASNOTIFICACION.NEXTVAL, p_municipio, p_idPunto);
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      INSERT INTO TBREGLASNOTIFICACION(ID,IDMUNICIPIO,IDDIRECCIONTERRITORIAL)
      VALUES(SEQ_TBREGLASNOTIFICACION.NEXTVAL, p_municipio, p_idPunto);
    END IF;
    
    commit;
  END;
  
  PROCEDURE sp_getVerificarPunto(p_municipio NUMBER, 
                                 p_idPunto number, 
                                 p_tipoPto int,
                                 verificar out int)IS
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      SELECT COUNT(*) INTO verificar FROM TBREGLASNOTIFICACION
      WHERE IDMUNICIPIO = p_municipio AND
            IDPUNTOATENCION = p_idPunto;
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      SELECT COUNT(*) INTO verificar FROM TBREGLASNOTIFICACION
      WHERE IDMUNICIPIO = p_municipio AND
            IDDIRECCIONTERRITORIAL = p_idPunto;
    END IF;
  END;
  
  PROCEDURE sp_setCitacion(p_idPunto   in out int, 
                           p_nombre    varchar2,
                           p_direccion varchar2,
                           p_depto     varchar2,
                           p_municipio varchar2,
                           p_tipoPto   int)IS
    p_adjetivo varchar2(10) := 'de la ';
    conteo int := 0;
    idMunicipio number;
  BEGIN
    
    IF p_tipoPto = PUNTOATENCION THEN
      
      SELECT COUNT(1) into conteo 
      FROM TBPUNTOATENCION 
      WHERE UPPER(REEMPLAZATILDES(nombre)) LIKE UPPER(REEMPLAZATILDES(p_nombre));
      
      IF conteo = 1 THEN
      
        SELECT ID into p_idPunto 
        FROM TBPUNTOATENCION 
        WHERE UPPER(REEMPLAZATILDES(nombre)) LIKE UPPER(REEMPLAZATILDES(p_nombre));
        
        UPDATE TBPUNTOATENCION
          SET NOMBRE = p_nombre,
              TEXTONOMBRE = p_adjetivo || p_nombre,
              DIRECCION = p_direccion,
              TEXTODIRECCION = p_direccion
        WHERE ID = p_idPunto;
      ELSIF conteo = 0 THEN
        SELECT mcp.id into idMunicipio from tbgeografia mcp
        inner join tbgeografia dep on dep.id = mcp.padreid
        where upper(REEMPLAZATILDES(dep.nombre)) = upper(REEMPLAZATILDES(p_depto))
          AND upper(REEMPLAZATILDES(mcp.nombre)) = upper(REEMPLAZATILDES(p_municipio));
      
        INSERT INTO TBPUNTOATENCION(ID, NOMBRE, TEXTONOMBRE, IDMUNICIPIO, DIRECCION, TEXTODIRECCION)
        VALUES(p_idPunto, p_nombre, p_adjetivo || p_nombre, idMunicipio, p_direccion, p_direccion);
      END IF;
      
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      SELECT COUNT(1) into conteo FROM TBDIRECCIONTERRITORIAL WHERE ID = p_idPunto;
      
      IF conteo = 1 THEN
        UPDATE TBDIRECCIONTERRITORIAL
          SET NOMBRE = p_nombre,
              TEXTONOMBRE = p_adjetivo || p_nombre,
              DIRECCION = p_direccion,
              TEXTODIRECCION = p_direccion
        WHERE ID = p_idPunto;
      ELSIF conteo = 0 THEN
        SELECT mcp.id into idMunicipio from tbgeografia mcp
        inner join tbgeografia dep on dep.id = mcp.padreid
        where upper(REEMPLAZATILDES(dep.nombre)) = upper(REEMPLAZATILDES(p_depto))
          AND upper(REEMPLAZATILDES(mcp.nombre)) = upper(REEMPLAZATILDES(p_municipio));
          
        INSERT INTO TBDIRECCIONTERRITORIAL(ID, IDMUNICIPIO, NOMBRE, TEXTONOMBRE, DIRECCION, TEXTODIRECCION)
        VALUES(p_idPunto, idMunicipio, p_nombre, p_adjetivo || p_nombre, p_direccion, p_direccion);
      END IF;
    END IF;
    
    commit;
  END;

  PROCEDURE sp_setReglaCitacion(p_idPunto   int, 
                                p_municipio int,
                                p_tipoPto   int)IS
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      INSERT INTO TBREGLASCITACION(ID,IDMUNICIPIO,IDPUNTOATENCION)
      VALUES(SEQ_TBREGLASCITACION.NEXTVAL, p_municipio, p_idPunto);
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      INSERT INTO TBREGLASCITACION(ID,IDMUNICIPIO,IDDIRECCIONTERRITORIAL)
      VALUES(SEQ_TBREGLASCITACION.NEXTVAL, p_municipio, p_idPunto);
    END IF;
    
    commit;
  END;
  
  PROCEDURE sp_getVerificarCitacion(p_municipio NUMBER, 
                                    p_idPunto number, 
                                    p_tipoPto int,
                                    verificar out int) IS
  BEGIN
    IF p_tipoPto = PUNTOATENCION THEN
      SELECT COUNT(*) INTO verificar FROM TBREGLASCITACION
      WHERE IDMUNICIPIO = p_municipio AND
            IDPUNTOATENCION = p_idPunto;
    ELSIF p_tipoPto = DIRECCIONTERRITORIAL THEN
      SELECT COUNT(*) INTO verificar FROM TBREGLASCITACION
      WHERE IDMUNICIPIO = p_municipio AND
            IDDIRECCIONTERRITORIAL = p_idPunto;
    END IF;
  END;
                
END PKG_REGLAS;
/