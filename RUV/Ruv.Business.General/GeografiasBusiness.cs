using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Ruv.Data.General;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Business.General
{
    public class GeografiasBusiness
    {
        public List<clsGeografiaCompleta> ObtenerGeografiaCompleta(ref string cError) {
            var collection = new entGeografia().ObtenerGeografiaCompleta(ref cError);
            if (string.IsNullOrEmpty(cError) && collection != null)
            {
                return collection.Select(x => new clsGeografiaCompleta { Id = x.Id, Nombre = x.Nombre, Padre = x.Padre, Tipo = x.Tipo }).ToList();
            }
            return null;
        }

        public List<clsGeografiaCompleta> ObtenerPaises(ref string cError) {
            var collection = new entGeografia().ObtenerPaises(ref cError);
            if (string.IsNullOrEmpty(cError) && collection != null) {
                return collection.Select(x => new clsGeografiaCompleta { Id = x.Id, Nombre = x.Nombre, Padre = x.Padre, Tipo = x.Tipo }).ToList();
            }
            return null;
        }

        public List<clsGeografiaCompleta> ObtenerDepartamentosPorPais(int idPais,ref string cError) {
            var collection = new entGeografia().ObtenerDepartamentosPorPais(idPais, ref cError);
            if (string.IsNullOrEmpty(cError) && collection != null) {
                return collection.Select(x => new clsGeografiaCompleta { Id = x.Id, Nombre = x.Nombre, Padre = x.Padre, Tipo = x.Tipo }).ToList();
            }
            return null;
        }

        public List<clsGeografiaCompleta> ObtenerMunicipiosPorDepartamento(int idDepartamento, ref  string cError) {
            var collection = new entGeografia().ObtenerMunicipiosPorDepartamento(idDepartamento, ref cError);
            if (string.IsNullOrEmpty(cError) && collection != null) {
                return collection.Select(x => new clsGeografiaCompleta { Id = x.Id, Nombre = x.Nombre, Padre = x.Padre, Tipo = x.Tipo }).ToList();
            }
            return null;
        }

        public List<clsEntidadMunicipio> ObtenerEntidadesPorMunicipio(int idMunicipio, ref string cError) { 
            var collection = new entGeografia().ObtenerEntidadesPorMunicipio(idMunicipio, ref cError);
            if (string.IsNullOrEmpty(cError) && collection != null) {
                return collection.Select(x => new clsEntidadMunicipio { NId = x.Id, CNombreEntidad = x.Nombre, NIdEntidad = (short)x.IdEntidad, NIdMunicipio = (long)x.IdMunicipio }).ToList();
            }
            return null;
        }

        public List<clsPuntoNotificacion> ObtenerPuntosAtencionyDTPorMunicipio(int idMunicipio) { 
            var collection = new entGeografia().ObtenerPuntosAtencionyDTPorMunicipio(idMunicipio);
            if (collection != null) {
                return collection.Select(x => new clsPuntoNotificacion { Id = x.Id, HashId = x.HashId, IdMunicipio = x.IdMunicipio, Nombre = x.Nombre, Direccion = x.Direccion }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Consulta que retorna la dirección del punto de notificación
        /// </summary>
        /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
        /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
        /// <returns>Dirección del punto de notificación</returns>
        /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
        public string ObtenerDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto)
        {
            return new entGeografia().ObtenerDireccionPuntoNotificacion(idPuntoNotificacion, tipoPunto);
        }

        /// <summary>
        /// Procedimiento que actualiza la dirección del punto de notificación
        /// </summary>
        /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
        /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
        /// <param name="direccion">Nueva dirección del punto de notificación</param>
        /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
        public bool ActualizarDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto, string direccion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                if(new entGeografia().ActualizarDireccionPuntoNotificacion(idPuntoNotificacion, tipoPunto, direccion, tra, ref cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

    }
}
