-- Create package CARACTERIZACION
create or replace package PKG_CARACTERIZACION AS
  TYPE cursor_type IS REF CURSOR;
  
  Function fonetica(CAD IN VARCHAR2) RETURN varchar2;

end PKG_CARACTERIZACION;
/

-- Create package body
create or replace package body PKG_CARACTERIZACION AS
  
  FUNCTION cadenaalfa(CAD IN VARCHAR2) RETURN VARCHAR2
  AS
  --reemplaza caracteres especiales, elimina espacios y validar reglas de G�
  varCadenaAlfa VARCHAR2(100);
  BEGIN
       --Convertir caracteres especiales si tienen un reemplazo
       varCadenaAlfa := TRANSLATE( replace(CAD, ' ') ,
                                '�������()1!|0�����:�����??e??????@' ,
                                'AAAEEIIIIIIIOOOOUUULL�ECRROEEAUAEAA' );
                              
       --caso especial en castellano para la �     
       varCadenaAlfa :=  REPLACE( varCadenaAlfa, 'G�E', 'WE');
       varCadenaAlfa :=  REPLACE( varCadenaAlfa, 'G�I', 'WI');
       varCadenaAlfa :=  REPLACE( varCadenaAlfa, '�', 'U');
       varCadenaAlfa :=  REPLACE( varCadenaAlfa, 'GUA', 'WA');
       varCadenaAlfa :=  REPLACE( varCadenaAlfa, 'GUO', 'WO');
       --varCadenaAlfa :=  REPLACE( varCadenaAlfa, 'GUU', 'GU');
                        
       --Quitar caracteres especiales que no tienen un reemplazo 
       varCadenaAlfa :=   REGEXP_REPLACE (varCadenaAlfa, '[^[:alpha:]]+' ); --[:alnum:]
       
       RETURN varCadenaAlfa;
  END;

  Function sonido1(CAD IN Varchar2) RETURN Varchar2
  AS
  --aplica reglas foneticas sencillas de un solo cambio
  varSONIDO1 varchar2(100);
  BEGIN        
       --Convertir caracteres con su similar fonetico
       
       varSONIDO1 := TRANSLATE ( CAD ,
                                 'Z�YVWX' ,
                                 'SNIBUS' );
        RETURN varSONIDO1;
   End;

Function sonido2(CAD IN Varchar2) RETURN Varchar2
  AS	
  --verificar la LL
  varSONIDO2 Varchar2(100);
  BEGIN
        --quitar mas de dos L repetidas
        varSONIDO2 := REGEXP_REPLACE( CAD, 'LLL+', 'LL');
        
        --reemplazar LL por I cuando hay vocales
        varSONIDO2 := replace(varSONIDO2, 'LLH', 'LL');  --si hay una vocal al final H es muda con LL
        
        varSONIDO2 := replace(varSONIDO2, 'LLA', 'IA');
        varSONIDO2 := replace(varSONIDO2, 'LLE', 'IE');
        varSONIDO2 := replace(varSONIDO2, 'LLI', 'I');
        varSONIDO2 := replace(varSONIDO2, 'LLO', 'IO');
        varSONIDO2 := replace(varSONIDO2, 'LLU', 'IU');
         
        RETURN varSONIDO2;
  End;
  
  Function sonido3(CAD IN Varchar2) RETURN Varchar2
  AS	
  --remover letras duplicadas
  varSONIDO3 Varchar2(100);
  BEGIN      
        --remover las letras repetidas
        varSONIDO3 :=  REGEXP_REPLACE(CAD, '(.)\1+', '\1');  
              
        RETURN varSONIDO3;
  End;

  Function sonido4(CAD in varchar2) RETURN varchar2
  AS
  --Aplicar reglas foneticas personalizadas
  varSONIDO4  varchar2(100);
  BEGIN

      	varSONIDO4 := CAD;

        varSONIDO4 := Replace(varSONIDO4, 'SHA', 'CHA');
        varSONIDO4 := Replace(varSONIDO4, 'SHE', 'CHE');
        varSONIDO4 := Replace(varSONIDO4, 'SHI', 'CHI');
        varSONIDO4 := Replace(varSONIDO4, 'SHO', 'CHO');
        varSONIDO4 := Replace(varSONIDO4, 'SHU', 'CHU');

        varSONIDO4 := Replace(varSONIDO4, 'QUA', 'KUA');
        varSONIDO4 := Replace(varSONIDO4, 'QUE', 'KE');
        varSONIDO4 := Replace(varSONIDO4, 'QUI', 'KI');
        varSONIDO4 := Replace(varSONIDO4, 'QUO', 'KUO');
        varSONIDO4 := Replace(varSONIDO4, 'QUU', 'KU');

        varSONIDO4 := Replace(varSONIDO4, 'CE', 'SE');
        varSONIDO4 := Replace(varSONIDO4, 'CI', 'SI');

        --Reemplzar CH por �
        varSONIDO4 := replace(varSONIDO4, 'CHA', '�A'); 
        varSONIDO4 := replace(varSONIDO4, 'CHE', '�E'); 
        varSONIDO4 := replace(varSONIDO4, 'CHI', '�I'); 
        varSONIDO4 := replace(varSONIDO4, 'CHO', '�O'); 
        varSONIDO4 := replace(varSONIDO4, 'CHU', '�U'); 
        
        --Quitar CH con consonantes, por ejemplo CHL por KHL
        varSONIDO4 := replace(varSONIDO4, 'C', 'K');        
        
        --Quitar Q sueltas por K
        varSONIDO4 := replace(varSONIDO4, 'Q', 'K');
        
        --Quitar H muda
        varSONIDO4 := replace(varSONIDO4, 'H');
        
        --Devolver el valor � a CH
        varSONIDO4 := replace(varSONIDO4, '�', 'CH'); 
        
        --Reemplazar sonido G por J cuando suenan igual
        varSONIDO4 := Replace(varSONIDO4, 'GE', 'JE');
        varSONIDO4 := Replace(varSONIDO4, 'GI', 'JI');
            
        RETURN varSONIDO4;
  end;

  Function fonetica(CAD IN VARCHAR2) RETURN varchar2
  AS
  varFonetica varchar2(100);
  BEGIN
    
    varFonetica := UPPER(TRIM(NVL(CAD, '')));
    IF varFonetica IS NULL THEN
      RETURN varFonetica;
    END IF;
    
     varFonetica := sonido3(SONIDO4(sonido3(SONIDO2(sonido1((CADENAALFA(varFonetica)))))));
     
     RETURN varFonetica;
     EXCEPTION
       WHEN OTHERS THEN
         RETURN CAD;
  END;
  
end PKG_CARACTERIZACION;
/
