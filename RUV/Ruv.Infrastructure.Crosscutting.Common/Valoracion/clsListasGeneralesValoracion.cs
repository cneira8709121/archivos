using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    public class clsListasGeneralesValoracion
    {
        List<clsEstadosValoracion> estados;
        List<clsPrincipioEstado> principios;
        List<clsObservacionEstado> observaciones;
        List<clsParametroGeneral> parametros;
        List<clsSubEtnias> subetnia;
        List<clsAutores> autores;
        List<clsInfracciones> infracciones;
        List<clsHerramientas> herramientas;
        List<clsTipoHerramienta> tipoHerramientas;
        List<clsRegistrosAnteriores> registros;
        List<clsPreguntasRegAnt> preguntasRegAnt;
        List<clsGeografia> geografias;
        List<clsCausal> causalesDevolucion;
        List<clsEntidadMunicipio> entidadesMunicipio;
        List<clsGeografiaCompleta> paises;
        List<clsGeografiaCompleta> departamentos;
        List<clsGeografiaCompleta> municipios;
        List<clsHechoEnmarcado> hechoEnmarcado;
        List<clsDecretoLey> decretoLey;

        public List<clsCausal> CausalesDevolucion
        {
            get { return causalesDevolucion; }
            set { causalesDevolucion = value; }
        }

        public List<clsEstadosValoracion> Estados
        {
            get { return estados; }
            set { estados = value; }
        }

        public List<clsPrincipioEstado> Principios
        {
            get { return principios; }
            set { principios = value; }
        }

        public List<clsObservacionEstado> Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public List<clsParametroGeneral> Parametros
        {
            get { return parametros; }
            set { parametros = value; }
        }

        public List<clsSubEtnias> sEtnias
        {
            get { return subetnia; }
            set { subetnia = value; }
        }
        
        public List<clsAutores> Autores
        {
            get { return autores; }
            set { autores = value; }
        }

        public List<clsInfracciones> Infracciones
        {
            get { return infracciones; }
            set { infracciones = value; }
        }

        public List<clsHerramientas> Herramientas
        {
            get { return herramientas; }
            set { herramientas = value; }
        }

        public List<clsTipoHerramienta> TipoHerramientas
        {
            get { return tipoHerramientas; }
            set { tipoHerramientas = value; }
        }

        public List<clsRegistrosAnteriores> Registros
        {
            get { return registros; }
            set { registros = value; }
        }

        public List<clsPreguntasRegAnt> PreguntasRegAnt
        {
            get { return preguntasRegAnt; }
            set { preguntasRegAnt = value; }
        }

        public List<clsGeografia> Geografias
        {
            get { return geografias; }
            set { geografias = value; }
        }

        public List<clsEntidadMunicipio> EntidadesMunicipio
        {
            get { return entidadesMunicipio; }
            set { entidadesMunicipio = value; }
        }

        public List<clsGeografiaCompleta> Paises
        {
            get { return paises; }
            set { paises = value; }
        }

        public List<clsGeografiaCompleta> Departamentos
        {
            get { return departamentos; }
            set { departamentos = value; }
        }

        public List<clsGeografiaCompleta> Municipios
        {
            get { return municipios; }
            set { municipios = value; }
        }

        public List<clsHechoEnmarcado> HechoEnmarcado
        {
            get { return hechoEnmarcado; }
            set { hechoEnmarcado = value; }
        }

        public List<clsDecretoLey> DecretoLey
        {
            get { return decretoLey; }
            set { decretoLey = value; }
        }
    }
}
