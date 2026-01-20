using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Ruv.Business.DTO.Valoracion;
using Ruv.Business.Valoracion.Asignacion;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;


namespace Ruv.Business.Valoracion.Valoracion
{
    public class ValoracionesBusiness
    {

        public List<clsAutores> GetAutores()
        {
            return Autores.GetAutores();
        }

        public List<clsAutores> GetAutores(int valAnexoPerId)
        {
            return Autores.GetAutores(valAnexoPerId);
        }

        public List<clsValoradorTareas> getListaTareas(int ValoradorId)
        {
            return ListaTareasValoracion.GetListaValoraciones(ValoradorId);
        }

        public List<clsTipoHerramienta> GetTiposHerramienta()
        {
            return Herramientas.GetTiposHeramienta();
        }

        public clsTipoHerramienta GetTipoHerramientaPorId(int Id)
        {
            return Herramientas.GetTipoHeramientaPorId(Id);
        }

        public List<clsHerramientas> GetHerramientasPorTipo(int tipo)
        {
            return Herramientas.GetHeramientasPorTipo(tipo);
        }

        public List<clsEstadosValoracion> GetEstadosValoracion()
        {
            return Estados.GetEstadosValoracionPersona();
        }

        public List<clsObservacionEstado> GetObservacionesEstadoPorEstado(int estadoId)
        {
            return ObservacionesEstado.GetObsedervacionestadoPorestadoId(estadoId);
        }

        public List<clsPrincipioEstado> GetPrincipiosPorEstado(int estadoId)
        {
            return Principios.GetPrincipiosPorEstadoId(estadoId);
        }

        public List<clsInfracciones> GetInfracciones()
        {
            return InfraccionDHI.GetInfracciones();
        }

        public List<clsInfracciones> GetInfracciones(int valAnexoPerId)
        {
            return InfraccionDHI.GetInfracciones(valAnexoPerId);
        }

        public List<clsDeclaracionInfoValoracion> GetInforDeclaracionVal(int ValoracionId)
        {
            return Declaraciones.GetInfoDeclaracionPorId(ValoracionId);
        }

        public clsValoracion getValoracion(int idValoracion, bool completa) {
            using (DbTransaction transaction = Dao.InitTransaction()) {
                entValoracion entBd = new entValoracion();
                var valoracion = entBd.GetValoracionById(idValoracion);
                //var valoracion = ParseDataToView(entBd.GetValoracionById(idValoracion, transaction));
                if (completa) {
                    DataSet ValoracionFullds = entBd.GetValoracionByIdFull(idValoracion);
                    valoracion.PersonasDeclaracion = DetalleDeclaracion.GetDetalleDeclaracionPorId(ValoracionFullds.Tables[0]);
                    valoracion.CausalDevolucion = Principios.GetPrincipiosPorValoracion(ValoracionFullds.Tables[1]);
                    valoracion.RegistrosAnteriores = RegistrosAnteriores.GetRegistrosPorValoracion(ValoracionFullds.Tables[2]);
                    valoracion.Hechos = Hechos.GetHechosPorValoracion(ValoracionFullds.Tables[3]);
                    foreach (clsHechosValoracion hecho in valoracion.Hechos)
                    {
                        hecho.Personas = PersonasAnexo.GetPersonasPorAnexoId(hecho.Id, ValoracionFullds.Tables[4]);
                    }
                }
                return valoracion;
            }
        }

        public clsValoracion getValoracionByDeclaracionID(int idDeclaracion) {
            entValoracion entBd = new entValoracion();
            return ParseDataToView(entBd.GetValoracionByDeclaracionId(idDeclaracion));
        }

        private clsValoracion ParseDataToView(TBVALORACION data) {
            var view = new clsValoracion();
            view.Id = data.ID;
            view.DeclaracionId = data.ID_DECLARACION;
            view.EstadoId = (data.ID_ESTADO_VAL.HasValue) ? data.ID_ESTADO_VAL.Value : 0;
            view.FechaAsignacion = data.FECHAASIGNACION;
            view.ValoradorId = (data.ID_VALORADOR.HasValue) ? data.ID_VALORADOR.Value : 0;
            view.AsignadorId = (data.ID_ASIGNADOR.HasValue) ? data.ID_ASIGNADOR.Value : 0;
            view.FechaValoracion = (data.FECHAVALORACION.HasValue) ? data.FECHAVALORACION.Value : DateTime.Now;
            view.FechaRealValoracion = (data.FECHAVALORACIONREAL.HasValue) ? data.FECHAVALORACIONREAL.Value : DateTime.Now;
            if (data.TBVALORACION_MOTIVACION != null)
            {
                foreach (var item in data.TBVALORACION_MOTIVACION)
                {
                    view.Motivacion_Inclusion = item.MOTIVACION_INCLUSION;
                    view.Motivacion_NoInclusion = item.MOTIVACION_NOINCLUSION;
                    view.ResuelveArticulo1 = item.RESUELVE_ARTICULO1;
                    view.ResuelveArticulo2 = item.RESUELVE_ARTICULO2;
                    view.cIdTipoMotivo = item.TIPOMOTIVACION;
                }
            }
            view.Observacion = data.OBSERVACION;
            view.EsDeclaracion = Convert.ToBoolean(data.ESDECLARACION);
            return view;
        }


        private void ParseViewToData(ref TBVALORACION data, clsValoracion view)
        {

            TBVALORACION_MOTIVACION motivaciones = new TBVALORACION_MOTIVACION();
            
            data.ID = view.Id;
            data.ID_DECLARACION = view.DeclaracionId;
            data.ID_ESTADO_VAL = view.EstadoId;
            data.FECHAASIGNACION = view.FechaAsignacion;
            data.ID_VALORADOR = view.ValoradorId;
            data.ID_ASIGNADOR = view.AsignadorId;
            data.FECHAVALORACION = view.FechaValoracion;
            data.FECHAVALORACIONREAL = view.FechaRealValoracion;
            if (data.TBVALORACION_MOTIVACION != null && data.TBVALORACION_MOTIVACION.Count > 0)
            {
                motivaciones = data.TBVALORACION_MOTIVACION.FirstOrDefault();
                motivaciones.MOTIVACION_INCLUSION = view.Motivacion_Inclusion;
                motivaciones.MOTIVACION_NOINCLUSION = view.Motivacion_NoInclusion;
                motivaciones.RESUELVE_ARTICULO1 = view.ResuelveArticulo1;
                motivaciones.RESUELVE_ARTICULO2 = view.ResuelveArticulo2;
            }
            else
            {
                data.TBVALORACION_MOTIVACION = new System.Data.Objects.DataClasses.EntityCollection<TBVALORACION_MOTIVACION>();
                motivaciones.MOTIVACION_INCLUSION = view.Motivacion_Inclusion;
                motivaciones.MOTIVACION_NOINCLUSION = view.Motivacion_NoInclusion;
                motivaciones.RESUELVE_ARTICULO1 = view.ResuelveArticulo1;
                motivaciones.RESUELVE_ARTICULO2 = view.ResuelveArticulo2;
                data.TBVALORACION_MOTIVACION.Add(motivaciones);
            }
            data.OBSERVACION = view.Observacion;
            data.ESDECLARACION = Convert.ToInt16(view.EsDeclaracion);

            if (view.EstadoId == (int)eEstadosValoracion.IniciaValoracion)
            {
                data.TBDECLARACIONES.PARAM_ESTADO = (int)eEstadoDeclaracion.ValoracionEnValoracion;
            }
            if (view.EstadoId == (int)eEstadosValoracion.NoValoradoDevuelto)
            {
                data.TBDECLARACIONES.PARAM_ESTADO = (int)eEstadoDeclaracion.NoValoradoDevuelto;
            }
            if (view.EstadoId == (int)eEstadosValoracion.PendientePorNotificar)
            {
                data.TBDECLARACIONES.PARAM_ESTADO = (int)eEstadoDeclaracion.ValoracionPendientePorRevision;
            }

        }

        public bool EsFueraDeColombia(int declaracionId)
        {
            var valoracionData = new entDeclaracion();
            return valoracionData.EsFueraDeColombia(declaracionId);
        }

        public void GuardarValoracion(clsValoracion valoracion, bool finalizar, DbTransaction transaction) {
            var valoracionData = new entValoracion();

            var valoracionObject = valoracionData.GetValoracionByIdOld(valoracion.Id, transaction);
            ParseViewToData(ref valoracionObject, valoracion);

            // Guardar registros anteriores
            foreach (clsRegistrosValoracion registroAnterior in RegistrosAnteriores.GetRegistrosPorValoracion(valoracion.Id)) {
                if (!valoracion.RegistrosAnteriores.Exists(x => x.Id == registroAnterior.Id)) {
                    RegistrosAnteriores.Eliminar(registroAnterior, transaction);
                }
            }
            if (valoracion.RegistrosAnteriores != null) {
                foreach (clsRegistrosValoracion registro in valoracion.RegistrosAnteriores) {
                    if (registro.Id > 0) {
                        RegistrosAnteriores.Actualizar(registro, transaction);
                    }
                    else {
                        RegistrosAnteriores.Nuevo(registro, transaction);
                    }
                }
            }

            valoracionData.Actualizar(valoracionObject, finalizar, transaction);
            if (valoracion.Hechos != null)
            {
                foreach (clsHechosValoracion hecho in valoracion.Hechos)
                {
                    Hechos.Actualizar(hecho, transaction);
                    if (hecho.Personas != null)
                    {
                        foreach (clsPersonaAnexo person in hecho.Personas)
                        {
                            PersonasAnexo.Actualizar(person, transaction);
                            Autores.Eliminar(person.Id, transaction);

                            if (person.Autores != null)
                            {
                                foreach (clsAutores autor in person.Autores)
                                {
                                    Autores.Insertar(autor.Id, person.Id, transaction);
                                }
                            }
                            InfraccionDHI.Eliminar(person.Id, transaction);
                            if (person.InfraccionesDHI != null)
                            {
                                foreach (clsInfracciones infraccion in person.InfraccionesDHI)
                                {
                                    InfraccionDHI.Insertar(infraccion.Id, person.Id, transaction);
                                }
                            }
                            Herramientas.EliminarPorAnexoId(person.Id, transaction);
                            if (person.Herramietas != null)
                            {
                                foreach (clsHerramientaAnexoPer herramienta in person.Herramietas)
                                {
                                    herramienta.AnexoPerId = person.Id;
                                    Herramientas.Insertar(herramienta, transaction);
                                }
                            }
                            Principios.Eliminar(person.Id, transaction);
                            if (person.Principios != null)
                            {
                                foreach (int principio in person.Principios)
                                {
                                    Principios.Insertar(principio, person.Id, transaction);
                                }
                            }

                            Afectaciones.Eliminar(person.Id, transaction);
                            if (person.AfectacionesDetectadas != null)
                            {
                                foreach (int afecta in person.AfectacionesDetectadas)
                                {
                                    Afectaciones.Insertar(afecta, person.Id, transaction);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<clsRegistrosAnteriores> GetRegistrosAnteriores()
        {
            return RegistrosAnteriores.GetRegistrosAnteriores();
        }

        public List<clsPreguntasRegAnt> GetPreguntasRegAnt()
        {
            return PreguntasRegistrosAnteriores.GetPreguntasRegAnt();
        }

        public bool DeshacerAsignacion(clsValoracion valoracion)
        {
            entValoracion Objvaloracion = new entValoracion();
            TBVALORACION val = Objvaloracion.GetValoracionByIdOld(valoracion.Id, null);

            ParseViewToData(ref val, valoracion);
            Objvaloracion.Actualizar(val, false, null);
            return true;
        }

        public DataSet GetInformeValoracion()
        {
            entValoracion Objvaloracion = new entValoracion();
            return Objvaloracion.GetInforme();
        }

        public DataSet getReportePorId(int valId)
        {
            entValoracion Objvaloracion = new entValoracion();
            return Objvaloracion.GetResumenPorId(valId);
        }

        public List<clsParametroGeneral> GetParametros()
        {
            return Parametros.GetParametros();
        }

        public List<clsSubEtnias> GetSubEtnias(int etniaId)
        {
            return SubEtniasB.GetSubEtnias(etniaId);
        }


        public List<Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsGeografia> GetGeografia(int? nivel, int? tipo, int? padre)
        {
            return Geografia.ObtenerGeografia(nivel, tipo, padre);
        }

        public void NuevoAnexo(clsHecho HechoVictimizante)
        {
            Hechos.NuevoHecho(HechoVictimizante);
        }

        public void ListaTareasValorador(ref clsConsultaValoracion eConsulta, ref string error)
        {
            ListaTareasValoracion.GetListaPaginada(ref eConsulta, ref error);
        }

        public void ListaTareasValoradorCantidad(ref clsConsultaValoracion eConsulta, ref string error)
        {
            ListaTareasValoracion.GetListaCantidad(ref eConsulta, ref error);
        }

        public List<clsPrincipioEstado> GetPrincipios()
        {
            return Principios.GetPrincipios();
        }

        public List<clsObservacionEstado> GetObservacionesEstado()
        {
            return ObservacionesEstado.GetObsevacionEstado();
        }

        public List<clsHerramientas> GetHerramientas()
        {
            return Herramientas.GetHeramientas();
        }

        public List<Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsGeografia> GetGeografia()
        {
            return Geografia.ObtenerGeografia();
        }

        public void InsertaTipoMotivacion(int nIdValoracion, string cTipoMotivacion, DbTransaction transaction) { 
            var valoracionData = new entValoracion();
            valoracionData.InsertaTipoMotivacion(nIdValoracion, cTipoMotivacion, transaction);
        }

         public string ObtieneIDTipoMotivacionB(int nidValoracion, ref string cError) {
             try { 
                 entValoracion objvalor = new entValoracion();
                 return objvalor.ObtieneTipoMotivacion(nidValoracion, null);
             }
             catch (Exception ex) {
                 cError = ex.Message;
                 return null;
             }
         }

         public List<clsEntidadMunicipio> ObtenerEntidadesMunicipio(ref string cError)
         {
             var listaEntidadesMunicipio = new entValoracion().ObtenerEntidadesMunicipio(ref cError).Select(x => new clsEntidadMunicipio { NId = x.Id, CNombreEntidad = x.Nombre, NIdEntidad = (short?)x.IdEntidad, NIdMunicipio = x.IdMunicipio }).ToList();
             return listaEntidadesMunicipio;
         }
         public bool AgregaPersonaValoracion(clsAgregarPersonaValoracion AgregaPersona, ref string cError)
         {
             using (DbTransaction tra = Dao.InitTransaction()) {
                 var repository = new entValoracion();

                 var idPersona = repository.AgregaPersonaValoracion(AgregaPersona, tra, ref cError);
                 if (string.IsNullOrEmpty(cError)) {
                     var idRegistroPersona = repository.AgregaRegPersonaValoracion(AgregaPersona, idPersona, tra, ref cError);
                     if (string.IsNullOrEmpty(cError) && AgregaPersona.lnDiscapacidad != null && AgregaPersona.lnDiscapacidad.Count > 0) {
                         var result = repository.AgregaDiscapacidadValoracion(AgregaPersona, idRegistroPersona, tra, ref cError);
                     }
                 }

                 if (string.IsNullOrEmpty(cError)) {
                     tra.Commit();
                     return true;
                 }
                 else {
                     tra.Rollback();
                     return false;
                 }
             }
             
         }
         public List<clsCargaPersonasAsociadasDeclaracion> CargaDatosPersonasAsociadas(int nIddeclracion, ref string cError)
         {
             entValoracion objvalora = new entValoracion();
             List<clsCargaPersonasAsociadasDeclaracion> PersonasAsociadasVal = new List<clsCargaPersonasAsociadasDeclaracion>();
             PersonasAsociadasVal = objvalora.CargaPersonasAsociadasVal(nIddeclracion, ref cError);

             if (string.IsNullOrEmpty(cError)&&(PersonasAsociadasVal!=null))
             {
                 return PersonasAsociadasVal;
             }
             else
                 return null;
         }

         public int CargaPersonasAsociadasCount(int nIdDeclaracion, ref string cError)
         {
             
                 entValoracion objvalora = new entValoracion();
                 int PersonasAsociadasCount ;
                 PersonasAsociadasCount = objvalora.CargaPersonasAsociadasCount(nIdDeclaracion, ref cError);
                 if (PersonasAsociadasCount != 0 && string.IsNullOrEmpty(cError))
                 {
                     return PersonasAsociadasCount;
                 }
                 return 0;
         }

         public int ObtenerIdValoracionporIdDeclaracionB(int nIdDeclaracion, ref string cError)
         {
             entValoracion objvalora = new entValoracion();
             int nIdValoracion = objvalora.ObtenerIdValoracionporIdDeclaracion(nIdDeclaracion,ref cError);
             return nIdValoracion;
         }

        public List<clsHechoEnmarcado> GetHechoEnmarcado()
        {
            return HechoEnmarcado.GetHechoEnmarcado();           
        }

        public List<clsDecretoLey> GetDecretoLey()
        {
            return DecretoLey.GetDecretoLey();
        }

    }

        //public bool Guardar(List<string> nombres)
        //{
        //    DbConnection con = entidadRUV.GetInstance().CreateConnection();
        //    con.Open();
        //    DbTransaction tran = con.BeginTransaction();
        //    try
        //    {
        //        foreach (string item in nombres)
        //        {
        //            entValoracion objVal = new entValoracion();
        //            objVal.GuarPrueba(item, tran);
        //        }

        //        tran.Commit();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        tran.Rollback();
        //        return false;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

       
    }

