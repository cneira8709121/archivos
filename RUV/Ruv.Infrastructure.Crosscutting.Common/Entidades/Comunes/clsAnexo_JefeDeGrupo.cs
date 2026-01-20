using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Genérico de información de víctima para uso en varios anexos.
    /// </summary>
    [DataContract]
    public partial class clsAnexo_JefeDeGrupo : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {
        public clsAnexo_JefeDeGrupo()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;

            ////Iniciar campos
            //TieneInscritaCedulaParaVotar = -1;
            //EncuestaSisben = -1;
            //InscritoEnPrograma = -1;
            //VinculadoSistemaSalud = -1;
        }

        #region PREGUNTA 2

        private int? _TieneInscritaCedulaParaVotar;
        [DataMember]
        public int? TieneInscritaCedulaParaVotar
        {
            get { return _TieneInscritaCedulaParaVotar; }
            set
            {
                _TieneInscritaCedulaParaVotar = value;
                ReportarCambioPropiedad("TieneInscritaCedulaParaVotar");
            }
        }

        private Int64? _InscripcionPais=48L;
        [DataMember]
        public Int64? InscripcionPais
        {
            get { return _InscripcionPais; }
            set
            {
                    _InscripcionPais = value;
                ReportarCambioPropiedad("InscripcionPais");
            }
        }

        private Int64? _InscripcionDepartamento;
        [DataMember]
        public Int64? InscripcionDepartamento
        {
            get { return _InscripcionDepartamento; }
            set
            {
                _InscripcionDepartamento = value;
                ReportarCambioPropiedad("InscripcionDepartamento");
            }
        }

        private Int64? _InscripcionMunicipio;
        [DataMember]
        public Int64? InscripcionMunicipio
        {
            get { return _InscripcionMunicipio; }
            set
            {
                _InscripcionMunicipio = value;
                ReportarCambioPropiedad("InscripcionMunicipio");
            }
        }

        #endregion

        #region PREGUNTA 3

        private Int64? _HijosEstudianPais=48L;
        [DataMember]
        public Int64? HijosEstudianPais
        {
            get { return _HijosEstudianPais; }
            set
            {
                _HijosEstudianPais = value;
                ReportarCambioPropiedad("HijosEstudianPais");
            }
        }

        private Int64? _HijosEstudianDepartamento;
        [DataMember]
        public Int64? HijosEstudianDepartamento
        {
            get { return _HijosEstudianDepartamento; }
            set
            {
                _HijosEstudianDepartamento = value;
                ReportarCambioPropiedad("HijosEstudianDepartamento");
            }
        }

        private Int64? _HijosEstudianMunicipio;
        [DataMember]
        public Int64? HijosEstudianMunicipio
        {
            get { return _HijosEstudianMunicipio; }
            set
            {
                _HijosEstudianMunicipio = value;
                ReportarCambioPropiedad("HijosEstudianMunicipio");
            }
        }

        private string _HijosEstudianInstitucion;
        [DataMember]
        public string HijosEstudianInstitucion
        {
            get { return _HijosEstudianInstitucion; }
            set
            {
                _HijosEstudianInstitucion = value;
                ReportarCambioPropiedad("HijosEstudianInstitucion");
            }
        }

        #endregion

        #region PREGUNTA 4

        private int? _EncuestaSisben;
        /// <summary>
        /// Si/No/No sabe
        /// </summary>
        [DataMember]
        public int? EncuestaSisben
        {
            get { return _EncuestaSisben; }
            set
            {
                _EncuestaSisben = value;
                ReportarCambioPropiedad("EncuestaSisben");
            }
        }

        private Int64? _EncuestaSisbenPais= 48L;
        [DataMember]
        public Int64? EncuestaSisbenPais
        {
            get { return _EncuestaSisbenPais; }
            set
            {
                _EncuestaSisbenPais = value;
                ReportarCambioPropiedad("EncuestaSisbenPais");
            }
        }

        private Int64? _EncuestaSisbenDepartamento;
        [DataMember]
        public Int64? EncuestaSisbenDepartamento
        {
            get { return _EncuestaSisbenDepartamento; }
            set
            {
                _EncuestaSisbenDepartamento = value;
                ReportarCambioPropiedad("EncuestaSisbenDepartamento");
            }
        }

        private Int64? _EncuestaSisbenMunicipio;
        [DataMember]
        public Int64? EncuestaSisbenMunicipio
        {
            get { return _EncuestaSisbenMunicipio; }
            set
            {
                _EncuestaSisbenMunicipio = value;
                ReportarCambioPropiedad("EncuestaSisbenMunicipio");
            }
        }

        private int? _EncuestaSisbenNivel;
        [DataMember]
        public int? EncuestaSisbenNivel
        {
            get { return _EncuestaSisbenNivel; }
            set
            {
                _EncuestaSisbenNivel = value;
                ReportarCambioPropiedad("EncuestaSisbenNivel");
            }
        }

        #endregion

        #region PREGUNTA 5

        private int? _InscritoEnPrograma;
        /// <summary>
        /// Si/No/No sabe
        /// </summary>
        [DataMember]
        public int? InscritoEnPrograma
        {
            get { return _InscritoEnPrograma; }
            set
            {
                _InscritoEnPrograma = value;
                ReportarCambioPropiedad("InscritoEnPrograma");
            }
        }

        private Int64? _InscritoEnProgramaPais = 48L;
        [DataMember]
        public Int64? InscritoEnProgramaPais
        {
            get { return _InscritoEnProgramaPais; }
            set
            {
                _InscritoEnProgramaPais = value;
                ReportarCambioPropiedad("InscritoEnProgramaPais");
            }
        }

        private Int64? _InscritoEnProgramaDepartamento;
        [DataMember]
        public Int64? InscritoEnProgramaDepartamento
        {
            get { return _InscritoEnProgramaDepartamento; }
            set
            {
                _InscritoEnProgramaDepartamento = value;
                ReportarCambioPropiedad("InscritoEnProgramaDepartamento");
            }
        }

        private Int64? _InscritoEnProgramaMunicipio;
        [DataMember]
        public Int64? InscritoEnProgramaMunicipio
        {
            get { return _InscritoEnProgramaMunicipio; }
            set
            {
                _InscritoEnProgramaMunicipio = value;
                ReportarCambioPropiedad("InscritoEnProgramaMunicipio");
            }
        }

        private string _InscritoEnProgramaEntidadDondeLabora;
        [DataMember]
        public string InscritoEnProgramaEntidadDondeLabora
        {
            get { return _InscritoEnProgramaEntidadDondeLabora; }
            set
            {
                _InscritoEnProgramaEntidadDondeLabora = value;
                ReportarCambioPropiedad("InscritoEnProgramaEntidadDondeLabora");
            }
        }

        #endregion

        #region PREGUNTA 6

        private int? _VinculadoSistemaSalud;
        /// <summary>
        /// Si/No/No sabe
        /// </summary>
        [DataMember]
        public int? VinculadoSistemaSalud
        {
            get { return _VinculadoSistemaSalud; }
            set
            {
                _VinculadoSistemaSalud = value;
                ReportarCambioPropiedad("VinculadoSistemaSalud");
            }
        }

        private Int64? _VinculadoSistemaSaludPais = 48L;
        [DataMember]
        public Int64? VinculadoSistemaSaludPais
        {
            get { return _VinculadoSistemaSaludPais; }
            set
            {
                _VinculadoSistemaSaludPais = value;
                ReportarCambioPropiedad("VinculadoSistemaSaludPais");
            }
        }

        private Int64? _VinculadoSistemaSaludDepartamento;
        [DataMember]
        public Int64? VinculadoSistemaSaludDepartamento
        {
            get { return _VinculadoSistemaSaludDepartamento; }
            set
            {
                _VinculadoSistemaSaludDepartamento = value;
                ReportarCambioPropiedad("VinculadoSistemaSaludDepartamento");
            }
        }

        private Int64? _VinculadoSistemaSaludMunicipio;
        [DataMember]
        public Int64? VinculadoSistemaSaludMunicipio
        {
            get { return _VinculadoSistemaSaludMunicipio; }
            set
            {
                _VinculadoSistemaSaludMunicipio = value;
                ReportarCambioPropiedad("VinculadoSistemaSaludMunicipio");
            }
        }

        private int? _VinculadoSistemaSaludTipoAfiliacion;
        [DataMember]
        public int? VinculadoSistemaSaludTipoAfiliacion
        {
            get { return _VinculadoSistemaSaludTipoAfiliacion; }
            set
            {
                _VinculadoSistemaSaludTipoAfiliacion = value;
                ReportarCambioPropiedad("VinculadoSistemaSaludTipoAfiliacion");
            }
        }

        #endregion

        #region PREGUNTA 7

        private Int64? _LugarLaboralPais = 48L;
        [DataMember]
        public Int64? LugarLaboralPais
        {
            get { return _LugarLaboralPais; }
            set
            {
                _LugarLaboralPais = value;
                ReportarCambioPropiedad("LugarLaboralPais");
            }
        }

        private Int64? _LugarLaboralDepartamento;
        [DataMember]
        public Int64? LugarLaboralDepartamento
        {
            get { return _LugarLaboralDepartamento; }
            set
            {
                _LugarLaboralDepartamento = value;
                ReportarCambioPropiedad("LugarLaboralDepartamento");
            }
        }

        private Int64? _LugarLaboralMunicipio;
        [DataMember]
        public Int64? LugarLaboralMunicipio
        {
            get { return _LugarLaboralMunicipio; }
            set
            {
                _LugarLaboralMunicipio = value;
                ReportarCambioPropiedad("LugarLaboralMunicipio");
            }
        }

        private string _LugarLaboralEmpleador;
        [DataMember]
        public string LugarLaboralEmpleador
        {
            get { return _LugarLaboralEmpleador; }
            set
            {
                _LugarLaboralEmpleador = value;
                ReportarCambioPropiedad("LugarLaboralEmpleador");
            }
        }

        #endregion

        public string Scope
        {
            get { return "Jefe de Grupo"; }
        }
    }
}
