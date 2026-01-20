update tbgeografia set nombre = 'Berlin' where nombre = 'Berliini';
update tbgeografia set nombre = 'Buenos Aires' where nombre = 'Distrito Federal';
update tbgeografia set nombre = 'Canberra' where nombre = 'Capital Region';
update tbgeografia set nombre = 'Manaos' where nombre = 'Manaus';
update tbgeografia set nombre = 'Emiratos Arabes Unidos' where nombre = 'United Arab Emirates';
update tbgeografia set nombre = 'En Abu Dhabi' where nombre = 'Abu Dhabi';
update tbgeografia set nombre = 'Baleares' where nombre = 'Balears';
update tbgeografia set nombre = 'Nueva Jersey' where nombre = 'New Jersey';
update tbgeografia set nombre = 'Ile De France' where nombre = 'Azle de France';
update tbgeografia set nombre = 'Tegucigalpa' where nombre = 'Distrito Central';
update tbgeografia set nombre = 'Nueva Delhi' where nombre = 'New Delhi';
update tbgeografia set nombre = 'Mexico DF' where nombre = 'Distrito Federado';
update tbgeografia set nombre = 'Hong Kong' where nombre like 'Región Administrativa Especial de Hong Kong de la República Popular China';

INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7564,'Santo Domingo de los Tsáchilas', 2, 69, 0);
INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7565,'Santo Domingo', 3, 7564, 0);

INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7566,'Sucumbíos', 2, 69, 0);
INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7567,'Nueva Loja', 3, 7566, 0);

INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7568,'Darién', 2, 166, 0);
INSERT INTO TBGEOGRAFIA(ID,NOMBRE,NIVEL,PADREID,REPRESENTACION)VALUES(7569,'Jaque', 3, 7568, 0);

update TBGEOGRAFIA set representacion = 1 where id = 1177;

commit;