using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsAnexo13_Victima : clsEntidadBase, IDataErrorInfo ,IPertenenciaEtnica, IPersonaAfectada,IValidationEntity 
    {
        public clsAnexo13_Victima()
        {
            Discapacidades = new List<int>();
            HechosVictimizantes = new List<int>();
            TiposDeAfectacion = new List<int>();

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        /// <summary>
        /// Acceso al padre (Persona Afectada - Hoja 2).
        /// No requiere almacenamiento.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public clsAnexo13 PersonasAfectadas { get; set; }
     
        private int? _PersonaAfectadaId;
        [DataMember]
        public int? PersonaAfectadaId
        {
            get { return _PersonaAfectadaId; }
            set
            {
                _PersonaAfectadaId = value;
                ReportarCambioPropiedad("PersonaAfectadaId");
            }
        }

        #region CAMPOS OBLIGATORIOS

        private int _NumeroConsecutivo;
        /// <summary>
        /// Se calcula automáticamente.
        /// </summary>
        [DataMember]
        public int NumeroConsecutivo
        {
            get { return _NumeroConsecutivo; }
            set
            {
                _NumeroConsecutivo = value;
                ReportarCambioPropiedad("NumeroConsecutivo");
            }
        }

        private int _FamiliaConsecutivo;
        /// <summary>
        /// Se calcula automáticamente.
        /// </summary>
        [DataMember]
        public int FamiliaConsecutivo
        {
            get { return _FamiliaConsecutivo; }
            set
            {
                _FamiliaConsecutivo = value;
                ReportarCambioPropiedad("FamiliaConsecutivo");
            }
        }

        #region Pregunta 2

        private string _PrimerNombre;
        [DataMember]
        public string PrimerNombre
        {
            get { return _PrimerNombre; }
            set
            {
                string oldValue = _PrimerNombre;
                _PrimerNombre = (value != null) ? value.ToUpper() : null;
                if (_PrimerNombre != oldValue)
                {
                    ReportarCambioPropiedad("PrimerNombre");
                    ReportarCambioPropiedad("NombreCompleto");

                    ReportarHaciaElDeclarante("PrimerNombre");
                }
            }
        }

        private string _SegundoNombre;
        [DataMember]
        public string SegundoNombre
        {
            get { return _SegundoNombre; }
            set
            {
                _SegundoNombre = value;
                ReportarCambioPropiedad("SegundoNombre");
                ReportarCambioPropiedad("NombreCompleto");

                ReportarHaciaElDeclarante("SegundoNombre");
            }
        }

        private string _PrimerApellido;
        [DataMember]
        public string PrimerApellido
        {
            get { return _PrimerApellido; }
            set
            {
                string oldValue = _PrimerApellido;
                _PrimerApellido = (value != null) ? value.ToUpper() : null;
                if (_PrimerApellido != oldValue)
                {
                    ReportarCambioPropiedad("PrimerApellido");
                    ReportarCambioPropiedad("NombreCompleto");

                    ReportarHaciaElDeclarante("PrimerApellido");
                }
            }
        }

        private string _SegundoApellido;
        [DataMember]
        public string SegundoApellido
        {
            get { return _SegundoApellido; }
            set
            {
                string oldValue = _SegundoApellido;
                _SegundoApellido = (value != null) ? value.ToUpper() : null;
                if (_SegundoApellido != oldValue)
                {
                    ReportarCambioPropiedad("SegundoApellido");
                    ReportarCambioPropiedad("NombreCompleto");

                    ReportarHaciaElDeclarante("SegundoApellido");
                }
            }
        }

        /// <summary>
        /// Campo calculado, no requiere almacenamiento.
        /// </summary>
        [DataMember]
        public string NombreCompleto
        {
            get
            {
                string cadena = "";

                if (ID.HasValue)
                    return cadena.UnirCadenas(
                      PrimerNombre, SegundoNombre,
                      PrimerApellido, SegundoApellido,
                      string.Format(" ({0})", NumeroConsecutivo));
                else
                    return null;
            }
            set { }
        }

        #endregion

        #region Pregunta3
        private int? _TipoDocumento;
        [DataMember]
        public int? TipoDocumento
        {
            get { return _TipoDocumento; }
            set
            {
                _TipoDocumento = value;
                if (value == null)
                    NumeroDocumento = null;
                else
                {
                    if (Enum.GetValues(typeof(eTipoDocumentoSinNumero)).Cast<int>().Contains((int)value)) NumeroDocumento = null;

                    if ((int)value == (int)eTipoDocumento.CedulaCiudadania ||
                            (int)value == (int)eTipoDocumento.LibretaMilitar ||
                            (int)value == (int)eTipoDocumento.TarjetaIdentidad ||
                            (int)value == (int)eTipoDocumento.RegistroCivil ||
                            (int)value == (int)eTipoDocumento.NUIP ||
                            (int)value == (int)eTipoDocumento.NIP ||
                            (int)value == (int)eTipoDocumento.Indocumentado)
                    {
                        Nacionalidad = 48;
                    }
                    else
                    {
                        Nacionalidad = 0;
                    }
                }
                    


                ReportarCambioPropiedad("TipoDocumento");
                ReportarCambioPropiedad("NumeroDocumento");
                ReportarCambioPropiedad("FechaNacimiento");
                ReportarCambioPropiedad("Genero");

                ReportarHaciaElDeclarante("TipoDocumento");
            }
        }

        private string _NumeroDocumento;
        [DataMember]
        public string NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set
            {
                _NumeroDocumento = value;
                ReportarCambioPropiedad("NumeroDocumento");
                ReportarCambioPropiedad("TipoDocumento");

                ReportarHaciaElDeclarante("NumeroDocumento");
            }
        }

        #endregion

        #region Pregunta 4

        private DateTime? _FechaNacimiento;
        [DataMember]
        public DateTime? FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set
            {
                _FechaNacimiento = value;
                ReportarCambioPropiedad("FechaNacimiento");

                ReportarHaciaElDeclarante("FechaNacimiento");
            }
        }

        #endregion
        private int? _Nacionalidad;
        [DataMember]
        public int? Nacionalidad
        {
            get { return _Nacionalidad; }
            set
            {
                _Nacionalidad = value;
                ReportarCambioPropiedad("Nacionalidad");

                ReportarHaciaElDeclarante("Nacionalidad");
            }
        }
        
        #region Pregunta 5

        private List<int> _HechosVictimizantes;
        [DataMember]
        public List<int> HechosVictimizantes
        {
            get { return _HechosVictimizantes; }
            set
            {
                _HechosVictimizantes = value;
                ReportarCambioPropiedad("HechosVictimizantes");
            }
        }

        #endregion

        #region PREGUNTA 6

        private List<int> _TiposDeAfectacion;
        [DataMember]
        public List<int> TiposDeAfectacion
        {
            get { return _TiposDeAfectacion; }
            set
            {
                _TiposDeAfectacion = value;
                ReportarCambioPropiedad("TiposDeAfectacion");
                ReportarCambioPropiedad("Afectado");
            }
        }

        private string _Otro;
        [DataMember]
        public string Otro
        {
            get { return _Otro; }
            set
            {
                _Otro = value;
                ReportarCambioPropiedad("Otro");
            }
        }

        #endregion

        #region Pregunta 7
        private int? _Relacion;
        [DataMember]
        public int? Relacion
        {
            get { return _Relacion; }
            set
            {
                _Relacion = value;
                ReportarCambioPropiedad("Relacion");
            }
        }
        #endregion

        #region Pregunta 8
        private int? _EstadoCivil;
        [DataMember]
        public int? EstadoCivil
        {
            get { return _EstadoCivil; }
            set
            {
                _EstadoCivil = value;
                ReportarCambioPropiedad("EstadoCivil");
            }
        }
        #endregion

        #region Pregunta 9
        private int? _RegimenEspecial;
        [DataMember]
        public int? RegimenEspecial
        {
            get { return _RegimenEspecial; }
            set
            {
                _RegimenEspecial = value;
                ReportarCambioPropiedad("RegimenEspecial");
            }
        }

        #endregion

        #endregion

        #region ENFOQUE DIFERENCIAL

        #region Pregunta 10
        private int? _Genero;
        [DataMember]
        public int? Genero
        {
            get { return _Genero; }
            set
            {
                _Genero = value;
                if (value == (int)eGenero.Hombre)
                {
                    GestanteLactante = null;
                    MujerCabezaDeHogar = null;
                }

                ReportarCambioPropiedad("Genero");
                ReportarCambioPropiedad("GestanteLactante");
                ReportarCambioPropiedad("MujerCabezaDeHogar");
                ReportarCambioPropiedad("TipoDocumento");
            }
        }

        private int? _OrientacionSexual;
        [DataMember]
        public int? OrientacionSexual
        {
            get
            {
                return _OrientacionSexual;
            }
            set
            {
                _OrientacionSexual = value;
                ReportarCambioPropiedad("OrientacionSexual");
            }
        }

        private int? _IdentidadGenero;
        [DataMember]
        public int? IdentidadGenero
        {
            get
            {
                return _IdentidadGenero;
            }
            set
            {
                _IdentidadGenero = value;
                ReportarCambioPropiedad("IdentidadGenero");
            }
        }
        #endregion

        #region Pregunta 11
        private List<int> _Discapacidades;
        [DataMember]
        public List<int> Discapacidades
        {
            get { return _Discapacidades; }
            set
            {
                _Discapacidades = value;
                ReportarCambioPropiedad("Discapacidades");
            }
        }

        private string _OtraDiscapacidad;
        [DataMember]
        public string OtraDiscapacidad
        {
            get { return _OtraDiscapacidad; }
            set
            {
                _OtraDiscapacidad = value;
                ReportarCambioPropiedad("OtraDiscapacidad");
            }
        }
        #endregion

        #region Pregunta 12
        private int? _PertenenciaEtnica;
        [DataMember]
        public int? PertenenciaEtnica
        {
            get { return _PertenenciaEtnica; }
            set
            {
                _PertenenciaEtnica = value;
                if (_PertenenciaEtnica == (int)ePertenenciaEtnica.Ninguna)
                    OtraComunidadEtnica = null;
                ReportarCambioPropiedad("PertenenciaEtnica");
            }
        }

        private int? _ComunidadEtnica1;
        [DataMember]
        public int? ComunidadEtnica1
        {
            get { return _ComunidadEtnica1; }
            set
            {
                _ComunidadEtnica1 = value;
                ReportarCambioPropiedad("ComunidadEtnica1");
            }
        }

        private int? _ComunidadEtnica2;
        [DataMember]
        public int? ComunidadEtnica2
        {
            get { return _ComunidadEtnica2; }
            set
            {
                _ComunidadEtnica2 = value;
                ReportarCambioPropiedad("ComunidadEtnica2");
            }
        }

        private string _OtraComunidadEtnica;
        [DataMember]
        public string OtraComunidadEtnica
        {
            get { return _OtraComunidadEtnica; }
            set
            {
                _OtraComunidadEtnica = value;
                ReportarCambioPropiedad("OtraComunidadEtnica");
            }
        }
        #endregion

        #region Pregunta 13

        private int? _MujerCabezaDeHogar;
        [DataMember]
        public int? MujerCabezaDeHogar
        {
            get { return _MujerCabezaDeHogar; }
            set
            {
                _MujerCabezaDeHogar = value;
                ReportarCambioPropiedad("MujerCabezaDeHogar");
            }
        }

        private int? _HombreCabezaDeHogar;
        [DataMember]
        public int? HombreCabezaDeHogar
        {
            get { return _HombreCabezaDeHogar; }
            set
            {
                _HombreCabezaDeHogar = value;
                ReportarCambioPropiedad("HombreCabezaDeHogar");
            }
        }
        #endregion

        #region Pregunta 14
        private int? _GestanteLactante;
        [DataMember]
        public int? GestanteLactante
        {
            get { return _GestanteLactante; }
            set
            {
                _GestanteLactante = value;
                ReportarCambioPropiedad("GestanteLactante");
            }
        }

        private int? _Campesinado;
        [DataMember]
        public int? Campesinado
        {
            get
            {
                return _Campesinado;
            }
            set
            {
                _Campesinado = value;
                ReportarCambioPropiedad("Campesinado");
            }
        }
        private int? _PersonaBuscadora;
        [DataMember]
        public int? PersonaBuscadora
        {
            get
            {
                return _PersonaBuscadora;
            }
            set
            {
                _PersonaBuscadora = value;
                ReportarCambioPropiedad("PersonaBuscadora");
            }
        }
        #endregion
        #endregion



        public string Scope
        {
            get { return "Anexo 13"; }
        }

    }
}
