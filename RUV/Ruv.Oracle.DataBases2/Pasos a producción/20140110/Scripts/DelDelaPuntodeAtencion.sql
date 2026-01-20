update TBPUNTOATENCION set textonombre =
 concat('DEL CENTRO ', LTRIM(TEXTONOMBRE, 'DE LA CENTRO ')) where textonombre like 'DE LA CENTRO%';
update TBPUNTOATENCION set textonombre =
 concat('del Centro ', LTRIM(TEXTONOMBRE, 'de la Centro ')) where textonombre like 'de la Centro %';
update TBDIRECCIONTERRITORIAL set textonombre =
 concat('DEL CENTRO ', LTRIM(TEXTONOMBRE, 'DE LA CENTRO ')) where textonombre like 'DE LA CENTRO%';
update TBDIRECCIONTERRITORIAL set textonombre =
 concat('del Centro ', LTRIM(TEXTONOMBRE, 'de la Centro ')) where textonombre like 'de la Centro %';
 
COMMIT;