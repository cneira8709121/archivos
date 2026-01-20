using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Collections.Specialized;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Almacena todos los datos que puede contener una declaración.
    /// </summary>
    public partial class clsDeclaracion : clsEntidadBase
    {
        static clsDeclaracion _DeclaracionActual;

        /// <summary>
        /// Crea algunos enlaces entre entidades, que no se preservan al 
        /// des-serializar.
        /// </summary>
        public void CrearEnlacesPostCargue()
        {
            // Enlazar las validaciones del anexo 5.
            foreach (clsAnexo05 item in A05)
            {
                item.DeseaUbicarseEn.MetodoAlternoValidacion = item.ValidacionesDeseaUbicarseEn;
            }

            foreach (clsAnexo01 item in A01)
                foreach (clsAnexo01_Victima vic in item.Victimas)
                {
                    vic.AnexoPadre = item;
                    vic.DenunciaPrevia.AnexoPadre = item;
                }

            CrearEnlacesAfectaciones();

            ReportarCambioPropiedad("NumeroDeAnexos");
        }

        /// <summary>
        /// Crear los enlaces entre las victimas y las afectaciones
        /// </summary>
        private void CrearEnlacesAfectaciones()
        {
            foreach (clsAnexo01 anexo in A01)
            {
                foreach (clsAnexo01_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo02 anexo in A02)
            {
                foreach (clsAnexo02_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo03 anexo in A03)
            {
                foreach (clsAnexo03_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo04 anexo in A04)
            {
                foreach (clsAnexo04_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo06 anexo in A06)
            {
                foreach (clsAnexo06_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo07 anexo in A07)
            {
                foreach (clsAnexo07_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo08 anexo in A08)
            {
                foreach (clsAnexo08_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo09 anexo in A09)
            {
                foreach (clsAnexo09_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

            foreach (clsAnexo10 anexo in A10)
            {
                foreach (clsAnexo10_Victima item in anexo.Victimas)
                {
                    item.Afectacion.Victima = item;
                }
            }

        }

        /// <summary>
        /// Permite acceso en todo momento a la declaración que se edita actulmente.
        /// </summary>
        [XmlIgnore()]
        public static clsDeclaracion DeclaracionActual
        {
            set { _DeclaracionActual = value; }
            get
            { return _DeclaracionActual; }
        }

        public static clsUsuario _UsuarioActual;

        /// <summary>
        /// Permite acceso en todo momento al usuario actual
        /// </summary>
        [XmlIgnore()]
        public static clsUsuario UsuarioActual
        {
            set { _UsuarioActual = value; }
            get
            { return _UsuarioActual; }
        }

        public static IList<clsValidaciones> _ConfiguracionValidaciones;

        /// <summary>
        /// Permite acceso en todo momento al usuario actual
        /// </summary>
        [XmlIgnore()]
        public static IList<clsValidaciones> ConfiguracionValidaciones
        {
            set { _ConfiguracionValidaciones = value; }
            get
            { return _ConfiguracionValidaciones; }
        }

        /// <summary>
        /// Actualiza la lista temporal que conserva el conteo de los anexos utilizados.
        /// </summary>
        public void ActualizarConteoHechos()
        {
            // No modificar el estado de la entidad.
            var EstadoAnterior = EstadoRegistro;

            if (TomaDeclaracion.Hechos != null)
            {
                for (int i = 0; i < TomaDeclaracion.Hechos.Count; i++)
                    TomaDeclaracion.Hechos[i] = 0;

                foreach (IAnexo UnAnexo in TodosLosAnexos)
                {
                    if (UnAnexo.Numero == 13)
                        TomaDeclaracion.Hechos[UnAnexo.Numero - 2]++;   //Se restan 2 ya que no existe el anexo 12
                    else
                        TomaDeclaracion.Hechos[UnAnexo.Numero - 1]++;

                }
            }
            EstadoRegistro = EstadoAnterior;
        }

        /// <summary>
        /// retorna una lista con todos los anexos.
        /// </summary>
        public IEnumerable<IAnexo> TodosLosAnexos
        {
            get
            {
                IEnumerable<clsEntidadBase> Resultado =
                  A01.Cast<clsEntidadBase>()
                  .Concat(A02.Cast<clsEntidadBase>())
                  .Concat(A03.Cast<clsEntidadBase>())
                  .Concat(A04.Cast<clsEntidadBase>())
                  .Concat(A05.Cast<clsEntidadBase>())
                  .Concat(A06.Cast<clsEntidadBase>())
                  .Concat(A07.Cast<clsEntidadBase>())
                  .Concat(A08.Cast<clsEntidadBase>())
                  .Concat(A09.Cast<clsEntidadBase>())
                  .Concat(A10.Cast<clsEntidadBase>())
                  .Concat(A11.Cast<clsEntidadBase>())
                  .Concat(A13.Cast<clsEntidadBase>());
                return Resultado.Where(x => x.EstadoRegistro != eEstadoRegistro.Eliminado).Cast<IAnexo>();
            }
        }

        /// <summary>
        /// El número total de anexos en esta declaración.
        /// </summary>
        [XmlIgnore]
        private int _NumeroDeAnexos;
        public int NumeroDeAnexos
        {
            get { 
                _NumeroDeAnexos = TodosLosAnexos.Count();
                return _NumeroDeAnexos;
            }
            set
            {
                _NumeroDeAnexos = value;
                ReportarCambioPropiedad("NumeroDeAnexos");
                ReportarCambioPropiedad("NumeroTotalFolios");
            }
        }

        private string _CodigoProceso;
        /// <summary>
        /// Código de procesamiento para transmisión.
        /// No requiere almacenamiento.
        /// </summary>
        public string CodigoProceso
        {
            get { return _CodigoProceso; }
            set { _CodigoProceso = value; }
        }

        private bool _SoloLectura = false;
        /// <summary>
        /// Veradero: La declaración no puede modificarse.
        /// </summary>
        public bool SoloLectura
        {
            get { return _SoloLectura; }
            set { _SoloLectura = value; }
        }

        #region INFORMACIÓN DE AUDITORIA
        [DataMember]
        public int? UsuarioId { get; set; }
        public int? UnidadTerritorialId { get; set; }

        #endregion

        #region VALIDACIÓN COMPLETA DE LA DECLARACION

        /// <summary>
        /// Retorna una lista de las secciones y las validaciones que tienen pendientes.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, List<string>>> ValidarDeclaracion(List<eEstadoValidacion> validacionesRequeridas, ref int validacionesSaltadas)
        {
            var Resultado = new List<Tuple<string, List<string>>>();

            VE = new clsValidadorEntidades();
            string Titulo;
            // Validar las Hojas.

            Titulo = "Hoja 1 de 4";
            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, TomaDeclaracion, Resultado);
            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, TomaDeclaracion.Encargado, Resultado);

            Titulo = "Hoja 2 de 4";
            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, PersonasAfectadas, Resultado);
            foreach (var x in PersonasAfectadas.ListaPersonas)
            {
                ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, x, Resultado, x.NombreCompleto);
            }

            List<string> ErroresHechos = new List<string>();

            // Hacer las validaciones y los errores agregarlos a ErroresHechos.
            Titulo = "Hoja 2 de 4";
            ErroresHechos = ValidarHechoVictimizante_vs_anexo();
            if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));
            /*
            //Luis.Esteban 26Jun06 Por solicitud de alexander Holgin se quita esta validación. El anexo 13 se puede diligenciar con cualquie otro anexo
              // Validar anexo 13 vs anexo5.
              Titulo = "Anexo 5";
              ErroresHechos = ValidarAnexo13_vs_anexo5();
              if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));
            */

            Titulo = "Anexo 5";
            ErroresHechos = ValidarAnexo13_vs_anexo5_Masivo();
            if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));

            //Se valida que para cada hecho victimizante marcado en cada anexo13 exista su correspondiente anexo. 
            Titulo = "Anexo 13";
            ErroresHechos = ValidarHechoVictimizante_vs_anexo13();
            if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));

            //Luis.Esteban 26Junio12 Validar anexo 13 vs anexos, si hay al menos un anexo 13 debe haber al menos un anexo de otro tipo
            Titulo = "Anexo 13";
            ErroresHechos = ValidarAnexo13_vs_anexos();
            if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));

            // Validar personas repetidas en anexo 13.
            Titulo = "Anexo 13";
            ErroresHechos = ValidarPersonasRepetidasAnexo13();
            if (ErroresHechos.Count > 0)
                Resultado.Add(new Tuple<string, List<string>>(Titulo, ErroresHechos));

            Titulo = "Hoja 3 de 4";
            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, DescripcionHechos, Resultado);

            Titulo = "Hoja 4 de 4";
            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, VerificacionProcedimiento, Resultado);

            // Seleccionar sólo los anexos que no se marcaron como borrados.
            var AnexosNoBorrados = from x in TodosLosAnexos.Cast<clsEntidadBase>()
                                   where x.EstadoRegistro != eEstadoRegistro.Eliminado
                                   select (IAnexo)x;

            foreach (var UnAnexo in AnexosNoBorrados)
            {
                Titulo = string.Format("Anexo {0}", UnAnexo.Numero);
                ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnAnexo, Resultado);

                switch (UnAnexo.Numero)
                {
                    case 1:
                        clsAnexo01 A01 = UnAnexo as clsAnexo01;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A01.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A01.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A01.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                            foreach (var UnBien in UnaVictima.Bienes)
                            {
                                ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnBien, Resultado, UnaVictima, "Bien");
                            }
                        }
                        break;

                    case 2:
                        clsAnexo02 A02 = UnAnexo as clsAnexo02;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A02.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A02.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A02.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 3:
                        clsAnexo03 A03 = UnAnexo as clsAnexo03;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A03.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A03.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A03.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 4:
                        clsAnexo04 A04 = UnAnexo as clsAnexo04;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A04.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A04.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A04.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 5:
                        clsAnexo05 A05 = UnAnexo as clsAnexo05;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A05.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A05.DenunciaPrevia, Resultado);
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A05.InformacionDeArribo, Resultado, "Arribo");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A05.DeseaUbicarseEn, Resultado, "Deseo ubicación");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A05.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A05.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                        }
                        break;

                    case 6:
                        clsAnexo06 A06 = UnAnexo as clsAnexo06;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A06.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A06.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A06.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 7:
                        clsAnexo07 A07 = UnAnexo as clsAnexo07;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A07.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A07.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A07.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 8:
                        clsAnexo08 A08 = UnAnexo as clsAnexo08;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A08.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A08.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A08.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 9:
                        clsAnexo09 A09 = UnAnexo as clsAnexo09;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A09.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A09.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A09.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 10:
                        clsAnexo10 A10 = UnAnexo as clsAnexo10;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A10.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A10.InformacionJefeGrupo, Resultado, "Jefe de grupo");
                        foreach (var UnaVictima in A10.Victimas)
                        {
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        }
                        break;

                    case 11:
                        clsAnexo11 A11 = UnAnexo as clsAnexo11;
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A11.FechaYLugar, Resultado, "Fecha y lugar de los hechos");
                        ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, A11.DenunciaPrevia, Resultado, "Denuncia previa");

                        foreach (var UnBienInmueble in A11.BienesInmuebles)
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnBienInmueble, Resultado, UnBienInmueble, "Bien inmueble");

                        foreach (var UnBienMueble in A11.BienesMuebles)
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnBienMueble, Resultado, UnBienMueble, "Bien mueble");

                        foreach (var UnCreditoPasivo in A11.CreditosPasivos)
                            ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, Titulo, UnCreditoPasivo, Resultado, "Crédito o pasivo");

                        break;
                    case 13:
                        //TODO: Anexo 13 
                        //clsAnexo13_Victima A13 = UnAnexo as clsAnexo13_Victima;
                        //ValidarEntidad(Titulo, A13.FechaYLugar, Resultado, "Fecha y lugar de los hechos");                        
                        //ValidarEntidad(Titulo, A13.InformacionJefeGrupo, Resultado, "Jefe de grupo");                        
                        //foreach (var UnaVictima in A13.Victimas)
                        //{
                        //    ValidarEntidad(Titulo, UnaVictima, Resultado, UnaVictima);
                        //    ValidarEntidad(Titulo, UnaVictima.Afectacion, Resultado, UnaVictima);
                        //    ValidarEntidad(Titulo, UnaVictima.DenunciaPrevia, Resultado, UnaVictima);
                        //}
                        break;
                }
            }


            //====================
            VE = null;
            return Resultado;
        }

        clsValidadorEntidades VE;

        /// <summary>
        /// Valida la entidad asignando como prefijo el nombre de la víctima.
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="entidad"></param>
        /// <param name="listaErrores"></param>
        /// <param name="victima"></param>
        void ValidarEntidad(List<eEstadoValidacion> validacionesRequeridas, ref int validacionesSaltadas, string titulo, object entidad, List<Tuple<string, List<string>>> listaErrores, IVictima victima, string prefijo = null)
        {
            if (entidad == null || (entidad as clsEntidadBase).EstadoRegistro == eEstadoRegistro.Eliminado)
                return;

            // Diego Alvarez - 26/12/2013 - Validación para que no permita pasar si no hay una persona afectada en anexo 11
            if (!victima.PersonaAfectadaId.HasValue)
            {
                string Prefijo = string.Empty;
                if (prefijo != null)
                    Prefijo += " >> " + prefijo;
                ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, titulo, entidad, listaErrores, Prefijo);
            }
            else
            {
                var affectedPerson = PersonasAfectadas.ListaPersonas.FirstOrDefault(x => x.ID == victima.PersonaAfectadaId.Value);
                if (affectedPerson != null)
                {
                    string Prefijo = affectedPerson.NombreCompleto;
                    if (prefijo != null)
                        Prefijo += " >> " + prefijo;
                    ValidarEntidad(validacionesRequeridas, ref validacionesSaltadas, titulo, entidad, listaErrores, Prefijo);
                }
            }
        }

        /// <summary>
        /// Valida una entidad y agrega su resultado a una lista de errores de validación.
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="entidad"></param>
        /// <param name="listaErrores"></param>
        void ValidarEntidad(List<eEstadoValidacion> validacionesRequeridas, ref int validacionesSaltadas, string titulo, object entidad, List<Tuple<string, List<string>>> listaErrores, string prefijo = null)
        {
            if (entidad == null || (entidad as clsEntidadBase).EstadoRegistro == eEstadoRegistro.Eliminado)
                return;


            var Detecciones = VE.ObtenerErroresEntidad(entidad, validacionesRequeridas, ref validacionesSaltadas);
            if (Detecciones != null)
            {
                if (prefijo != null)
                    titulo = string.Format("{0} > {1}", titulo, prefijo);

                // Ya reportado?
                /// Ivan Suarez 19 Diciembre del 2013: Se valida que la lista de errores no venga null y tampoco tenga 0 elementos
                if (listaErrores != null)
                {
                    var Existente = listaErrores.FirstOrDefault(x => x.Item1 == titulo);
                    if (Existente != null)
                        Existente.Item2.AddRange(Detecciones);
                    else
                        listaErrores.Add(new Tuple<string, List<string>>(titulo, Detecciones));
                }
            }
        }

        /// <summary>
        /// Validar que si en la hoja dos se marco un hecho victimizante este diligenciado el anexo correspondiente
        /// </summary>
        /// <returns></returns>
        List<string> ValidarHechoVictimizante_vs_anexo()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();
            string mensajeFaltaLlenarAnexo = "Para '{0}' se marco el hecho víctimizante {1} para el cual no se ha diligenciado el anexo correspondiente";

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Acto_terrorista_1)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo01>().Any(
                      x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 1));

            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Amenaza_2)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo02>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 2));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Delitos_contra_sexual_3)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo03>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 3));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.DesaparicionForzada_4)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo04>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 4));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.DesplazamientoForzado_5)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo05>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 5));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.HomicidioMasacre_6)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo06>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 6));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.MinasAntipersonal_7)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo07>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 7));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Secuestro_8)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo08>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 8));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Tortura_9)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo09>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 9));
            }

            foreach (clsPersonaAfectada persona in DA.PersonasAfectadas.ListaPersonas.Where(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.VinculacionNiñosGruposArmados_10)))
            {
                var Ok = DA.TodosLosAnexos.OfType<clsAnexo10>().Any(
                    x => x.Victimas.Any(y => y.PersonaAfectadaId == persona.ID));

                if (!Ok)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo, persona.NombreCompleto, 10));
            }

            return ErroresHechos;
        }

        private string mensajeFaltaLlenarAnexo13 = "Para '{0}' se marco el hecho víctimizante {1} en un Anexo13 para el cual no se ha diligenciado el anexo correspondiente";
        /// <summary>
        /// Validar que si en algun anexo 13 se marco un hecho victimizante exista el anexo correspondiente en la declaración
        /// Es decir: si para la persona X se marco el anexo 1 'Acto terrorista' debe existir al menos un anexo 1 en la declaración.
        /// </summary>
        /// <returns></returns>
        List<string> ValidarHechoVictimizante_vs_anexo13()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();

            foreach (clsAnexo13 anexo13 in DA.A13)
            {
                clsAnexo13_Victima persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Acto_terrorista_1));

                if (persona != null && DA.A01.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 1));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Amenaza_2));
                if (persona != null && DA.A02.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 2));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Delitos_contra_sexual_3));
                if (persona != null && DA.A03.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 3));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.DesaparicionForzada_4));
                if (persona != null && DA.A04.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 4));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.DesplazamientoForzado_5));
                if (persona != null && DA.A05.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 5));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.HomicidioMasacre_6));
                if (persona != null && DA.A06.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 6));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.MinasAntipersonal_7));
                if (persona != null && DA.A07.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 7));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Secuestro_8));
                if (persona != null && DA.A08.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 8));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.Tortura_9));
                if (persona != null && DA.A09.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 9));

                persona = anexo13.ListaPersonas.FirstOrDefault(x => x.HechosVictimizantes.Contains((int)eHechosVictimizantes.VinculacionNiñosGruposArmados_10));
                if (persona != null && DA.A10.Count == 0)
                    ErroresHechos.Add(string.Format(mensajeFaltaLlenarAnexo13, persona.NombreCompleto, 10));
            }

            return ErroresHechos;
        }

        /// <summary>
        /// Luis.Esteban 14Jun2012
        /// Validar que si se diligencio un anexo 13, se debe diligenciar el anexo 5 y marcar la opcion de "MASIVO"
        /// </summary>
        /// <returns></returns>
        List<string> ValidarAnexo13_vs_anexo5()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();

            if (DA.A13.Count > 0)
            {
                clsAnexo05 anexo5;
                anexo5 = DA.TodosLosAnexos.OfType<clsAnexo05>().FirstOrDefault(x => x.TipoDesplazamiento == (int)eTipoDesplazamientoA05.Masivo);
                if (anexo5 == null)
                    ErroresHechos.Add("Se ha diligenciado el Anexo 13, por lo tanto debe diligenciar el anexo 5 y marcar la opcion desplazamiento masivo.");
            }

            return ErroresHechos;
        }

        /// <summary>
        /// Luis.Esteban 02Jul2012
        /// Validar que si se diligencio un anexo 13 y un anexo 5, el anexo 5 debe estar marcado como "MASIVO"
        /// </summary>
        /// <returns></returns>
        List<string> ValidarAnexo13_vs_anexo5_Masivo()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();

            if (DA.A13.Count > 0 && DA.A05.Count > 0)
            {
                clsAnexo05 anexo5;
                anexo5 = DA.TodosLosAnexos.OfType<clsAnexo05>().FirstOrDefault(x => x.TipoDesplazamiento == (int)eTipoDesplazamientoA05.Masivo);
                if (anexo5 == null)
                    ErroresHechos.Add("Se ha diligenciado el Anexo 13, por lo tanto debe diligenciar el anexo 5 y marcar la opcion desplazamiento masivo.");
            }

            return ErroresHechos;
        }

        /// <summary>
        /// Luis.Esteban 26Jun2012
        /// Se valida que si existe uno o más anexos 13, debe existir al menos un anexo de otro tipo.
        /// Se valida el numero total de anexo contra el numero total de anexos13
        /// </summary>
        /// <returns></returns>
        List<string> ValidarAnexo13_vs_anexos()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();

            if (DA.A13.Count > 0 && DA.NumeroDeAnexos == DA.A13.Where(x => x.EstadoRegistro != eEstadoRegistro.Eliminado).Count())
            {
                ErroresHechos.Add("Se ha diligenciado el Anexo 13, por lo tanto debe diligenciar 'EL ANEXO CORRESPONDIENTE AL EVENTO MASIVO'.");
            }

            return ErroresHechos;
        }

        /// <summary>
        /// Luis.Esteban 19Jun2012
        /// Validar que no existan personas repetidas en diferentes anexos 13.
        /// </summary>
        /// <returns></returns>
        List<string> ValidarPersonasRepetidasAnexo13()
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            List<string> ErroresHechos = new List<string>();
            List<clsAnexo13_Victima> personaRepetida = new List<clsAnexo13_Victima>();

            foreach (clsAnexo13 A13 in DA.A13)
            {
                foreach (clsAnexo13_Victima persona in A13.ListaPersonas)
                {
                    int cont;
                    cont = A13.ListaPersonas.Count(x =>
                            x.NumeroDocumento == persona.NumeroDocumento
                            && x.TipoDocumento == persona.TipoDocumento);
                    if (cont > 1)
                    {
                        if (!personaRepetida.Any(x => x.NumeroDocumento == persona.NumeroDocumento
                                                && x.TipoDocumento == persona.TipoDocumento)
                                                && !Enum.IsDefined(typeof(Ruv.Infrastructure.Crosscutting.Common.eTipoDocumentoSinNumero), persona.TipoDocumento))
                        {
                            ErroresHechos.Add(string.Format(
                                    "El documento '{0}' con el numero '{1}' existe '{2}' veces en esta declaración lo cual no esta permitido.",
                                    Enum.GetName(typeof(eTipoDocumento), persona.TipoDocumento), persona.NumeroDocumento, cont));

                            personaRepetida.Add(persona);
                        }

                    }
                }
            }

            return ErroresHechos;
        }
        #endregion


    }
}
