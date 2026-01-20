declare 
  maxid number := 0;
begin
  select max(id) into maxid from tbparametros;
  insert into tbparametros
  VALUES(maxid + 1, 2134, 'ninguno', 0, 0, 10868, 36, null);
  commit;
end;