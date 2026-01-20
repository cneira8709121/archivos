INSERT INTO TBESTADOSNOTIFICACION(ID, NOMBRE, DESCRIPCION) VALUES (7, 'Notificación en Proceso', 'El Courier aun esta intentando enviar la notificacion y no se sabe si es entregada o devuelta');
INSERT INTO TBESTADOSNOTIFICACION(ID, NOMBRE, DESCRIPCION) VALUES (8, 'Estado por Validar', 'Se ha cargado un estado al reporte del courier el cual no es identificado por el sistema y necesita ser validado por el usuario');
INSERT INTO TBESTADOSNOTIFICACION(ID, NOMBRE, DESCRIPCION) VALUES (15, 'Pendiente Envío Resolución', null);
INSERT INTO TBESTADOSNOTIFICACION(ID, NOMBRE, DESCRIPCION) VALUES (16, 'Notificado Resolución', null);

COMMIT;

UPDATE TBESTADOSNOTIFICACION SET NOMBRE = 'Pendiente Fijacion Edicto' WHERE ID = 11;
UPDATE TBESTADOSNOTIFICACION SET NOMBRE = 'Pendiente Desfijacion Edicto' WHERE ID = 13;

commit;