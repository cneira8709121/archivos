using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.Orfeo
{
    public class Dignatario
    {
        #region Attributes

        private int _nTipoRadicado;
        private string _cNombreDeclarante;
        private string _cPrimerApellido;
        private string _cSegundoApellido;
        private string _cCedula;
        private string _cDireccion;
        private string _cTelefono;
        private string _cEntidad;
        private int _nIdDepartamento;
        private int _nIdMunicipio;
        private string _cEmail;

        #endregion
        #region Properties

        public int NTipoRadicado { get { return 2; } }
        public string CNombreDeclarante
        {
            get
            {
                return _cNombreDeclarante;
            }
            set
            {
                _cNombreDeclarante = value == null ? string.Empty : value;
            }
        }
        public string CPrimerApellido
        {
            get
            {
                return _cPrimerApellido;
            }
            set
            {
                _cPrimerApellido = value == null ? string.Empty : value;
            }
        }
        public string CSegundoApellido
        {
            get
            {
                return _cSegundoApellido;
            }
            set
            {
                _cSegundoApellido = value == null ? string.Empty : value;
            }
        }
        public string CCedula
        {
            get
            {
                return _cCedula;
            }
            set
            {
                _cCedula = value == null ? string.Empty : value;
            }
        }
        public string CDireccion
        {
            get
            {
                return _cDireccion;
            }
            set
            {
                _cDireccion = value == null ? string.Empty : value;
            }
        }
        public string CTelefono
        {
            get
            {
                return _cTelefono;
            }
            set
            {
                _cTelefono = value == null ? string.Empty : value;
            }
        }
        public string CEntidad
        {
            get
            {
                return _cEntidad;
            }
            set
            {
                _cEntidad = value == null ? string.Empty : value;
            }
        }
        public int NIdDepartamento
        {
            get
            {
                return _nIdDepartamento;
            }
            set
            {
                _nIdDepartamento = value;
            }
        }
        public int NIdMunicipio
        {
            get
            {
                return _nIdMunicipio;
            }
            set
            {
                _nIdMunicipio = value;
            }
        }
        public string CEmail
        {
            get
            {
                return _cEmail;
            }
            set
            {
                _cEmail = value == null ? string.Empty : value;
            }
        }

        #endregion
    }
}
