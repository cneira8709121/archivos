-- Actualización Secuencias de Anexos
declare 
  numero integer := 0;
  strsql varchar2(255);
begin
  FOR I IN 1..13
  LOOP  
    IF I <> 12 THEN
      DBMS_OUTPUT.put_line('ANEXO' || I);
      strsql := 'select max(id) from tbanexo' || I;
      execute immediate strsql INTO numero;
      numero := numero + 1;
    
      DBMS_OUTPUT.put_line('Se elimina Secuencia');
        strsql := 'DROP SEQUENCE SEQ_ANEXO' || I;
      execute immediate strsql;
  
      DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
      strsql := 'CREATE SEQUENCE SEQ_ANEXO' || I || ' MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';
      execute immediate strsql;
    END IF;
  END LOOP;
  
  DBMS_OUTPUT.put_line('SEQ_ANEXO11_CREDITO');
  strsql := 'select max(id) from TBANEXO11_CREDITOS';
  execute immediate strsql INTO numero;
  numero := numero + 1;
    
  DBMS_OUTPUT.put_line('Se elimina Secuencia');
  strsql := 'DROP SEQUENCE SEQ_ANEXO11_CREDITO';
  execute immediate strsql;
  
  DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
  strsql := 'CREATE SEQUENCE SEQ_ANEXO11_CREDITO MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';
  execute immediate strsql;
  
  /* ------ */
  DBMS_OUTPUT.put_line('SEQ_ANEXO11_I');
  strsql := 'select max(id) from TBANEXO11_INMUEBLES';
  execute immediate strsql INTO numero;
  numero := numero + 1;
    
  DBMS_OUTPUT.put_line('Se elimina Secuencia');
  strsql := 'DROP SEQUENCE SEQ_ANEXO11_I';
  execute immediate strsql;
  
  DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
  strsql := 'CREATE SEQUENCE SEQ_ANEXO11_I MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';		
  execute immediate strsql;    
      
  /* ------ */
  DBMS_OUTPUT.put_line('SEQ_ANEXO11_M');
  strsql := 'select max(id) from TBANEXO11_MUEBLES';
  execute immediate strsql INTO numero;
  numero := numero + 1;
    
  DBMS_OUTPUT.put_line('Se elimina Secuencia');
  strsql := 'DROP SEQUENCE SEQ_ANEXO11_M';
  execute immediate strsql;
  
  DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
  strsql := 'CREATE SEQUENCE SEQ_ANEXO11_M MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';		
  execute immediate strsql;
  
  /* ------ */
  DBMS_OUTPUT.put_line('SEQ_ANEXO5_DESPLAZADO');
  strsql := 'select max(id) from TBANEXO5_DESPLAZADOS';
  execute immediate strsql INTO numero;
  numero := numero + 1;
    
  DBMS_OUTPUT.put_line('Se elimina Secuencia');
  strsql := 'DROP SEQUENCE SEQ_ANEXO5_DESPLAZADO';
  execute immediate strsql;
  
  DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
  strsql := 'CREATE SEQUENCE SEQ_ANEXO5_DESPLAZADO MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';		
  execute immediate strsql;

  /* ------ */  
  DBMS_OUTPUT.put_line('SEQ_ANEXO7_LUGAR');
  strsql := 'select max(id) from TBANEXO7_LUGARACCIDENTE';
  execute immediate strsql INTO numero;
  numero := numero + 1;
    
  DBMS_OUTPUT.put_line('Se elimina Secuencia');
  strsql := 'DROP SEQUENCE SEQ_ANEXO7_LUGAR';
  execute immediate strsql;
  
  DBMS_OUTPUT.put_line('Se crea Secuencia iniciando desde ' || numero);
  strsql := 'CREATE SEQUENCE SEQ_ANEXO7_LUGAR MINVALUE 1 START WITH ' || numero || ' INCREMENT BY 1';
  execute immediate strsql;
end;
