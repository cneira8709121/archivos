-- Create PKG_SERIALIZATION package
create or replace PACKAGE PKG_SERIALIZATION AS

  TYPE NumberVarray IS VARRAY(100) OF NUMBER;

  FUNCTION Serialize(p_VALUES NumberVarray) RETURN VARCHAR2;

  FUNCTION Deserialize(p_STRING VARCHAR2) RETURN NumberVarray;

END PKG_SERIALIZATION;
/

-- Create package body
create or replace PACKAGE BODY PKG_SERIALIZATION AS

  FUNCTION Serialize(p_VALUES NumberVarray) RETURN VARCHAR2 IS
    vSerialization VARCHAR2(2000);
  BEGIN
    vSerialization := '';
    IF p_VALUES IS NOT NULL THEN
      IF p_VALUES.Count > 0 THEN
        FOR i IN p_VALUES.First..p_VALUES.Last LOOP
          vSerialization := vSerialization || p_VALUES(i);
          IF i < p_VALUES.Last THEN vSerialization := vSerialization || '|'; END IF;
        END LOOP;
      END IF;
    END IF;
    RETURN vSerialization;
  END;
 
  FUNCTION Deserialize(p_STRING VARCHAR2) RETURN NumberVarray IS
    vDeserialization NumberVarray;
  BEGIN
    vDeserialization := NumberVarray();
    FOR splitElement IN (SELECT regexp_substr(p_STRING,'[^|]+', 1, level) AS SVALUE FROM DUAL
                         CONNECT BY regexp_substr(p_STRING, '[^|]+', 1, level) IS NOT NULL) LOOP
      vDeserialization.Extend(1);
      vDeserialization(vDeserialization.Count) := CAST(splitElement.SVALUE AS INT);
    END LOOP;
    RETURN vDeserialization;
  END;

END PKG_SERIALIZATION;
/
