-- Start with function signature
create or replace FUNCTION REEMPLAZATILDES(q IN VARCHAR2) RETURN VARCHAR2 IS
  texto  VARCHAR2(4000);
BEGIN
  texto := TRANSLATE(q, 'ביםףתאטלעשדץגךמפפהכןצüחֱֹֽ׃ÚְָּׂÙֳױֲÊ־װÛִֻֿײÜַ',
                        'aeiouaeiouaoaeiooaeioucAEIOUAEIOUAOAEIOOAEIOUC');
  RETURN texto;
END;
/