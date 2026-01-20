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
  public partial class clsAnexo03_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo03_Victima()
    {
        DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };
      DelitosSexuales = new List<int>();
      Afectacion = new clsAnexo_Afectacion() { Victima = this };

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 03"; } }

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

    #region PREGUNTA 9 AL 12

    private int? _VictimaDeEsteHecho;
    [DataMember]
    public int? VictimaDeEsteHecho
    {
      get { return _VictimaDeEsteHecho; }
      set
      {
        _VictimaDeEsteHecho = value;
          /*FICHA CONTROL DE CAMBIOS RUV 27-03-12
                    No se debe bloquear ningun campo cuando se marque la persona como "no víctima" 
        if (value != 1)
        {
            //Afectacion.Afectado = 0;  Se solicitó que la afectacion no depende de si es victima del hecho
            //DenunciaPrevia.SePresento = 0;    Se solicitó que la denuncia previa no depende de si es victima del hecho
            DelitosSexuales = new List<int>();
            AtencionMedicaRecibioAtencionMedica = null;
            AtencionMedicaSolicitoAyuda = null;
            AtencionMedicaRecibioAyuda = null;
        }
           * */
        ReportarCambioPropiedad("VictimaDeEsteHecho");
        ReportarCambioPropiedad("DelitosSexuales");
        ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
        ReportarCambioPropiedad("AtencionMedicaSolicitoAyuda");
        ReportarCambioPropiedad("AtencionMedicaRecibioAyuda");
      }
    }

    private clsAnexo_Afectacion _Afectacion;
    [DataMember]
    public clsAnexo_Afectacion Afectacion
    {
      get { return _Afectacion; }
      set
      {
        _Afectacion = value;
        ReportarCambioPropiedad("Afectacion");
      }
    }

    private clsAnexo_DenunciaPrevia _DenunciaPrevia;
    [DataMember]
    public clsAnexo_DenunciaPrevia DenunciaPrevia
    {
      get { return _DenunciaPrevia; }
      set
      {
        _DenunciaPrevia = value;
        ReportarCambioPropiedad("DenunciaPrevia");
      }
    }

    #endregion

    #region PREGUNTA 13

    private List<int> _DelitosSexuales;
    [DataMember]
    public List<int> DelitosSexuales
    {
      get { return _DelitosSexuales; }
      set
      {
        _DelitosSexuales = value;
        ReportarCambioPropiedad("DelitosSexuales");
      }
    }

    #endregion

    #region PREGUNTA 14

    private int? _AtencionMedicaRecibioAtencionMedica;
    [DataMember]
    public int? AtencionMedicaRecibioAtencionMedica
    {
      get { return _AtencionMedicaRecibioAtencionMedica; }
      set
      {
        _AtencionMedicaRecibioAtencionMedica = value;
        if (value != 1)
        {
            AtencionMedicaEntidad = null;
            AtencionMedicaDepartamento = null;
            AtencionMedicaMunicipio = null;
        }
        ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
        ReportarCambioPropiedad("AtencionMedicaEntidad");
        ReportarCambioPropiedad("AtencionMedicaDepartamento");
        ReportarCambioPropiedad("AtencionMedicaMunicipio");        
      }
    }

    private string _AtencionMedicaEntidad;
    [DataMember]
    public string AtencionMedicaEntidad
    {
      get { return _AtencionMedicaEntidad; }
      set
      {
        _AtencionMedicaEntidad = value;
        ReportarCambioPropiedad("AtencionMedicaEntidad");
        ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
      }
    }

    // NUEVO: 27-abr-2012
    private Int64? _AtencionMedicaPais = 48L;
    [DataMember]
    public Int64? AtencionMedicaPais
    {
        get { return _AtencionMedicaPais; }
        set
        {
            _AtencionMedicaPais = value;
            ReportarCambioPropiedad("AtencionMedicaPais");
            ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
        }
    }

    // NUEVO: 21-dic-2011
    private Int64? _AtencionMedicaDepartamento;
    [DataMember]
    public Int64? AtencionMedicaDepartamento
    {
      get { return _AtencionMedicaDepartamento; }
      set
      {
        _AtencionMedicaDepartamento = value;
        ReportarCambioPropiedad("AtencionMedicaDepartamento");
        ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
      }
    }

    // NUEVO: 21-dic-2011
    private Int64? _AtencionMedicaMunicipio;
    [DataMember]
    public Int64? AtencionMedicaMunicipio
    {
      get { return _AtencionMedicaMunicipio; }
      set
      {
        _AtencionMedicaMunicipio = value;
        ReportarCambioPropiedad("AtencionMedicaMunicipio");
        ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
      }
    }


    private int? _AtencionMedicaSolicitoAyuda;
    [DataMember]
    public int? AtencionMedicaSolicitoAyuda
    {
      get { return _AtencionMedicaSolicitoAyuda; }
      set
      {
        _AtencionMedicaSolicitoAyuda = value;
        if (value != 1) AtencionMedicaSolicitoAyudaEntidad = null;
        ReportarCambioPropiedad("AtencionMedicaSolicitoAyuda");
        ReportarCambioPropiedad("AtencionMedicaSolicitoAyudaEntidad");        
      }
    }

    private string _AtencionMedicaSolicitoAyudaEntidad;
    [DataMember]
    public string AtencionMedicaSolicitoAyudaEntidad
    {
      get { return _AtencionMedicaSolicitoAyudaEntidad; }
      set
      {
        _AtencionMedicaSolicitoAyudaEntidad = value;
        ReportarCambioPropiedad("AtencionMedicaSolicitoAyudaEntidad");
        ReportarCambioPropiedad("AtencionMedicaSolicitoAyuda");
      }
    }

    private int? _AtencionMedicaRecibioAyuda;
    [DataMember]
    public int? AtencionMedicaRecibioAyuda
    {
      get { return _AtencionMedicaRecibioAyuda; }
      set
      {
        _AtencionMedicaRecibioAyuda = value;
        if (value != 1) AtencionMedicaAyudaRecibida = null;
        ReportarCambioPropiedad("AtencionMedicaRecibioAyuda");
        ReportarCambioPropiedad("AtencionMedicaAyudaRecibida");        
      }
    }

    private string _AtencionMedicaAyudaRecibida;
    [DataMember]
    public string AtencionMedicaAyudaRecibida
    {
      get { return _AtencionMedicaAyudaRecibida; }
      set
      {
        _AtencionMedicaAyudaRecibida = value;
        ReportarCambioPropiedad("AtencionMedicaAyudaRecibida");
        ReportarCambioPropiedad("AtencionMedicaRecibioAyuda");                
      }
    }

    #endregion
  }
}
