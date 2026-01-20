using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;
using Ruv.Business.DTO.ServiciosComunicacion;
using Ruv.Business.ServiciosComunicacion.Contratos;

namespace Ruv.Business.ServiciosComunicacion
{
    public class OperacionesBusiness : IOperacionesBusiness
    {
        public List<Persona> ObtenerPersonas(int pagina, int tamano)
        {
            Data.ServiciosComunicacion.Contratos.IOperacionesData iOperacionesData = (Data.ServiciosComunicacion.Contratos.IOperacionesData)new Data.ServiciosComunicacion.OperacionesData();
            List<clsPersona> listClsPersona = iOperacionesData.ObtenerPersonas(pagina, tamano);
            
            //List<Persona> lstRespuesta = null;
            //if (listClsPersona != null)
            //{
            //    lstRespuesta = listClsPersona.Select(x => new Persona
            //    { 
            //        FechaNacimiento = (DateTime)x.FechaNacimiento,
            //        Id = x.Id,
            //        PrimerApellido = x.PrimerApellido,
            //        PrimerNombre = x.PrimerNombre,
            //        SegundoApellido = x.SegundoApellido,
            //        SegundoNombre = x.SegundoNombre
            //    }).ToList();
            //}
            //return lstRespuesta;

            return ListPersonaFromListClsPersona(listClsPersona);
        }

        public Persona ObtenerPersonaPorId(int ID)
        {
            Data.ServiciosComunicacion.Contratos.IOperacionesData iOperacionesData = (Data.ServiciosComunicacion.Contratos.IOperacionesData)new Data.ServiciosComunicacion.OperacionesData();
            clsPersona clsPersona = iOperacionesData.ObtenerPersonaPorId(ID);
            
            return PersonaFromClsPersona(clsPersona);
        }

        public Persona ObtenerPersonaPorDocumento(string documento)
        {
            Data.ServiciosComunicacion.Contratos.IOperacionesData iOperacionesData = (Data.ServiciosComunicacion.Contratos.IOperacionesData)new Data.ServiciosComunicacion.OperacionesData();
            clsPersona clsPersona = iOperacionesData.ObtenerPersonaPorDocumento(documento);

            return PersonaFromClsPersona(clsPersona);
        }

        public List<Siniestro> ObtenerSiniestrosPorIdPersona(int ID)
        {
            Data.ServiciosComunicacion.Contratos.IOperacionesData iOperacionesData = (Data.ServiciosComunicacion.Contratos.IOperacionesData)new Data.ServiciosComunicacion.OperacionesData();
            List<clsSiniestro> listClsSiniestro = iOperacionesData.ObtenerSiniestrosPorIdPersona(ID);

            return ListSiniestroFromListClsSiniestro(listClsSiniestro);
        }

        public List<GrupoFamiliar> ObtenerGrupoFamiliar(int ID)
        {
            Data.ServiciosComunicacion.Contratos.IOperacionesData iOperacionesData = (Data.ServiciosComunicacion.Contratos.IOperacionesData)new Data.ServiciosComunicacion.OperacionesData();
            List<clsGrupoFamiliar> listClsGrupoFamiliar = iOperacionesData.ObtenerGrupoFamiliar(ID);

            return ListGrupoFamiliarFromListClsGrupoFamiliar(listClsGrupoFamiliar);
        }

        #region Private Methods

        private Persona PersonaFromClsPersona(clsPersona clsPersona)
        {
            Persona respuesta = null;
            if (clsPersona != null)
            {
                respuesta = new Persona
                {
                    FechaNacimiento = clsPersona.FechaNacimiento == null ? DateTime.Now : clsPersona.FechaNacimiento,
                    Id = clsPersona.Id,
                    PrimerApellido = clsPersona.PrimerApellido,
                    PrimerNombre = clsPersona.PrimerNombre,
                    SegundoApellido = clsPersona.SegundoApellido,
                    SegundoNombre = clsPersona.SegundoNombre,
                    NumeroDocumento = clsPersona.NumeroDocumento
                };
            }
            return respuesta;
        }

        private List<Persona> ListPersonaFromListClsPersona(List<clsPersona> listClsPersona)
        {
            List<Persona> lstRespuesta = new List<Persona>();
            foreach (clsPersona clsPersona in listClsPersona)
            {
                lstRespuesta.Add(PersonaFromClsPersona(clsPersona));
            }

            return lstRespuesta;
        }

        private Siniestro SiniestroFromClsSiniestro(clsSiniestro clsSiniestro)
        {
            Siniestro respuesta = null;
            if (clsSiniestro != null)
            {
                respuesta = new Siniestro
                {
                    Barriovereda = clsSiniestro.BariioVereda,
                    Departamento = clsSiniestro.Departamento,
                    Fecha = clsSiniestro.Fecha,
                    Id_declaracion = clsSiniestro.IdDeclaracion,
                    Localidadcorregimiento = clsSiniestro.LocalidadCorregimiento,
                    Municipio = clsSiniestro.Municipio,
                    Nombre_hecho = clsSiniestro.NombreHecho,
                    Numero_formulario = clsSiniestro.NumeroFormulario
                };
            }
            return respuesta;
        }

        private List<Siniestro> ListSiniestroFromListClsSiniestro(List<clsSiniestro> listClsSiniestro)
        {
            List<Siniestro> lstRespuesta = new List<Siniestro>();
            foreach (clsSiniestro clsSiniestro in listClsSiniestro)
            {
                lstRespuesta.Add(SiniestroFromClsSiniestro(clsSiniestro));
            }

            return lstRespuesta;
        }

        private GrupoFamiliar GrupoFamiliarFromClsGrupoFamiliar(clsGrupoFamiliar clsGrupoFamiliar)
        {
            GrupoFamiliar respuesta = null;
            if (clsGrupoFamiliar != null)
            {
                respuesta = new GrupoFamiliar
                {
                    FechaNacimiento = clsGrupoFamiliar.FechaNacimiento,
                    IdDeclaracion = clsGrupoFamiliar.IdDeclaracion,
                    IdPersona = clsGrupoFamiliar.IdPersona,
                    NombrePersona = clsGrupoFamiliar.NombrePersona,
                    Parentesco = clsGrupoFamiliar.Parentesco
                };
            }
            return respuesta;
        }

        private List<GrupoFamiliar> ListGrupoFamiliarFromListClsGrupoFamiliar(List<clsGrupoFamiliar> listClsGrupoFamiliar)
        {
            List<GrupoFamiliar> lstRespuesta = new List<GrupoFamiliar>();
            foreach (clsGrupoFamiliar clsGrupoFamiliar in listClsGrupoFamiliar)
            {
                lstRespuesta.Add(GrupoFamiliarFromClsGrupoFamiliar(clsGrupoFamiliar));
            }

            return lstRespuesta;
        }

        #endregion Private Methods
    }
}
