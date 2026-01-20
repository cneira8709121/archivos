using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion;
using Ruv.Data.Valoracion.Asignacion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Asignacion
{
    public class AsignarValoradorBusiness
    {
        public AsignarValoradorBusiness()
        {

        }

        public List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorar()
        {
            return ListaDeclaracionesSinValorar.GetData();
        }

        public List<clsDeclaracionValoraracion> DeclaracionesEnValoracion()
        {
            return DeclaracionesValorando.GetData();
        }
           
        public List<clsValorador> ListarValoradores()
        {
            return ListaValoradores.GetData();
        }

        public List<clsPersona> ListarDetalleDeclaracionPorId(int declaracionId)
        {
            return DetalleDeclaracion.GetDetalleDeclaracionPorId(declaracionId);
        }

        public string ErrorMessage { get; private set; }
        public string StackTrace { get; private set; }

        public bool Guardar(List<clsValoracion> Valoraciones)
        {
            using (DbTransaction tra = Dao.InitTransaction())
            {
                try
                {
                    foreach (clsValoracion valoracion in Valoraciones)
                    {
                        TBVALORACION objValoracion = new TBVALORACION();
                        objValoracion.ID = valoracion.Id;
                        objValoracion.ID_DECLARACION = valoracion.DeclaracionId;
                        objValoracion.ID_ESTADO_VAL = valoracion.EstadoId;
                        objValoracion.ID_VALORADOR = valoracion.ValoradorId;
                        objValoracion.ID_ASIGNADOR = valoracion.AsignadorId;
                        entAsignacion objAsignacion = new entAsignacion();
                        objAsignacion.GuardarValoracion(objValoracion, valoracion.ValoradorRId, tra);
                    }
                    tra.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    StackTrace = ex.StackTrace;
                    tra.Rollback();
                    return false;
                }
            }
        }

        public List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorarPaginado(int Inicio, int Fin, string sortColumns, string filtro, string Valor)
        {
            return ListaDeclaracionesSinValorar.GetDataPaginado(Inicio, Fin, sortColumns, filtro, Valor);
        }

        public int CantidadDeclaracionesSinValorar(string filtro, string Valor)
        {
            return ListaDeclaracionesSinValorar.GetCantidad(filtro, Valor);
        }

        public bool Guardar(int usuarioId)
        {
            try
            {
                entAsignacion objAsignacion = new entAsignacion();
                objAsignacion.GuardarValoracion(usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                StackTrace = ex.StackTrace;
                return false;
            }
        }

        public void DeclaracionesEnValoracion(ref clsConsultaValoracion consulta, ref string error)
        {
            if (consulta.TipoConsulta == Infrastructure.Crosscutting.Common.eTipoConsulta.Listado)
            {
                DeclaracionesValorando.GetDataPaginado(ref consulta, ref error);
            }
            else
            {
                DeclaracionesValorando.GetDataCantidad(ref consulta, ref error);
            }
        }

        public bool AutoAsignaValorador(int IdDeclaracion, ref string cError)
        {
            try
            {
                entAsignacion objAsignacion = new entAsignacion();
                objAsignacion.AutoAsignarValoracion(IdDeclaracion, ref cError);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                StackTrace = ex.StackTrace;
                return false;
            }
        }

        
    }
}
