ALTER TABLE TBIDENTIFICADORFORMULARIO DROP CONSTRAINT FK_TBIDFORMULARIO_ENTIDMUNICIP;
ALTER TABLE TBIDFORMULARIOHISTORICO DROP CONSTRAINT FK_TBIDFORMHIST_ENTIDADANTERIO;

declare      
begin
  FOR I IN (SELECT COUNT(*), id_entidad, id_municipio, nombre
            FROM tbentidadmunicipio
            GROUP BY id_entidad, id_municipio, nombre
            HAVING COUNT(*)>1)
  LOOP
    DECLARE
      IDMIN NUMBER;
    BEGIN
      SELECT MIN(ID) INTO IDMIN FROM tbentidadmunicipio 
      WHERE I.id_entidad = id_entidad 
        AND I.id_municipio = id_municipio
        AND I.nombre = nombre;
      DBMS_OUTPUT.put_line('Se selecciona el menos ID de la duplicidad ' || IDMIN);
      
      update TBIDENTIFICADORFORMULARIO 
         set ID_ENTIDADMUNICIPIO = IDMIN
      where ID_ENTIDADMUNICIPIO in (SELECT ID FROM tbentidadmunicipio
                                    WHERE I.id_entidad = id_entidad 
                                      AND I.id_municipio = id_municipio
                                      AND I.nombre = nombre
                                      AND ID <> IDMIN);
      DBMS_OUTPUT.put_line('Se actualizan las ID en la tabla TBIDENTIFICADORFORMULARIO');
                                      
      update TBIDFORMULARIOHISTORICO 
         set ID_ENTIDADMUNICIPIOANTERIOR = IDMIN
       where ID_ENTIDADMUNICIPIOANTERIOR in (SELECT ID FROM tbentidadmunicipio
                                             WHERE I.id_entidad = id_entidad 
                                               AND I.id_municipio = id_municipio
                                               AND I.nombre = nombre
                                               AND ID <> IDMIN);
      DBMS_OUTPUT.put_line('Se actualizan las ID en la tabla TBIDFORMULARIOHISTORICO');
      
      DELETE FROM tbentidadmunicipio WHERE ID IN (SELECT ID FROM tbentidadmunicipio
                                                  WHERE I.id_entidad = id_entidad 
                                                    AND I.id_municipio = id_municipio
                                                    AND I.nombre = nombre
                                                    AND ID <> IDMIN);
      DBMS_OUTPUT.put_line('Se elimina la duplicidad');
      
      COMMIT;
    END;
  end loop;
end;

ALTER TABLE TBIDENTIFICADORFORMULARIO ADD 
CONSTRAINT FK_TBIDFORMULARIO_ENTIDMUNICIP FOREIGN KEY (ID_ENTIDADMUNICIPIO)
REFERENCES TBENTIDADMUNICIPIO (ID) ENABLE;

ALTER TABLE TBIDFORMULARIOHISTORICO ADD 
CONSTRAINT FK_TBIDFORMHIST_ENTIDADANTERIO FOREIGN KEY (ID_ENTIDADMUNICIPIOANTERIOR)
REFERENCES TBENTIDADMUNICIPIO (ID) ENABLE;