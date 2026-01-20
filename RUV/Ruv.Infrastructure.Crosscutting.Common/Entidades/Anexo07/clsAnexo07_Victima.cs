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
  public partial class clsAnexo07_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo07_Victima()
    {
        Afectacion = new clsAnexo_Afectacion() { Victima = this };
        DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 07"; } }

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

    #region PREGUNTA 9 AL 13

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
            EstadoVictima = null;
            //Afectacion.Afectado = 0;  Se solicitó que la afectacion no depende de si es victima del hecho
            //DenunciaPrevia.SePresento = 0;    Se solicitó que la denuncia previa no depende de si es victima del hecho
            ActividadAlMomentoDelHecho = null;
            TipoAccidente = null;
            AlgunMenorQuedoHuerfano = null;
            RecibioAtencionMedica = null;
        }
           * */
        ReportarCambioPropiedad("VictimaDeEsteHecho");

        var DA = clsDeclaracion.DeclaracionActual;
        if (DA != null)
        {
            var EsteAnexo07 = (from A in DA.A07.AsEnumerable()
                               where A.Victimas.Any(x => x.PersonaAfectadaId == this.PersonaAfectadaId)
                               select A).FirstOrDefault();
            if (EsteAnexo07 != null)
                EsteAnexo07.ReportarCambioPropiedad("Victimas");
        }
        ReportarCambioPropiedad("EstadoVictima");
        ReportarCambioPropiedad("Afectacion");
        ReportarCambioPropiedad("ActividadAlMomentoDelHecho");
        ReportarCambioPropiedad("TipoAccidente");
        ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
        ReportarCambioPropiedad("RecibioAtencionMedica");
      }
    }

    private int? _EstadoVictima;
    [DataMember]
    public int? EstadoVictima
    {
      get { return _EstadoVictima; }
      set
      {
        _EstadoVictima = value;
        ReportarCambioPropiedad("EstadoVictima");
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
        ReportarCambioPropiedad("SePresento");
      }
    }

    #endregion 

    #region PREGUNTAS 14 A 17
    
    private int? _TipoAccidente;
    [DataMember]
    public int? TipoAccidente
    {
      get { return _TipoAccidente; }
      set
      {
        _TipoAccidente = value;
        ReportarCambioPropiedad("TipoAccidente");
      }
    }

    private int? _ActividadAlMomentoDelHecho;
    [DataMember]
    public int? ActividadAlMomentoDelHecho
    {
      get { return _ActividadAlMomentoDelHecho; }
      set
      {
        _ActividadAlMomentoDelHecho = value;
        ReportarCambioPropiedad("ActividadAlMomentoDelHecho");        
      }
    }

    private int? _AlgunMenorQuedoHuerfano;
    [DataMember]
    public int? AlgunMenorQuedoHuerfano
    {
      get { return _AlgunMenorQuedoHuerfano; }
      set
      {
        _AlgunMenorQuedoHuerfano = value;
        if (value != 1)
        {
            MenorDesprotegidoId = null;
            MenorQuedoHuerfanoDe = null;
        }
        ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
        ReportarCambioPropiedad("MenorDesprotegidoId");
        ReportarCambioPropiedad("MenorQuedoHuerfanoDe");        
      }
    }

    private int? _MenorDesprotegidoId;
    [DataMember]
    public int? MenorDesprotegidoId
    {
      get { return _MenorDesprotegidoId; }
      set
      {
        _MenorDesprotegidoId = value;
        ReportarCambioPropiedad("MenorDesprotegidoId");
        ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
      }
    }

    private int? _MenorQuedoHuerfanoDe;
    /// <summary>
    /// Padre/Madre/Padre y madre
    /// </summary>
    [DataMember]
    public int? MenorQuedoHuerfanoDe
    {
      get { return _MenorQuedoHuerfanoDe; }
      set
      {
        _MenorQuedoHuerfanoDe = value;
        ReportarCambioPropiedad("MenorQuedoHuerfanoDe");
        ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
      }
    }

    private int? _RecibioAtencionMedica;
    [DataMember]
    public int? RecibioAtencionMedica
    {
      get { return _RecibioAtencionMedica; }
      set
      {
        _RecibioAtencionMedica = value;
        if (value != 1)
        { 
        RecibioAtencionMedicaEntidad = null;
        RecibioAtencionMedicaDepartamento = null;
        RecibioAtencionMedicaMunicipio = null;
        }
        ReportarCambioPropiedad("RecibioAtencionMedica");
        ReportarCambioPropiedad("RecibioAtencionMedicaEntidad");
        ReportarCambioPropiedad("RecibioAtencionMedicaDepartamento");
        ReportarCambioPropiedad("RecibioAtencionMedicaMunicipio");
      }
    }

    private string _RecibioAtencionMedicaEntidad;
    [DataMember]
    public string RecibioAtencionMedicaEntidad
    {
      get { return _RecibioAtencionMedicaEntidad; }
      set
      {
        _RecibioAtencionMedicaEntidad = value;
        ReportarCambioPropiedad("RecibioAtencionMedicaEntidad");
        ReportarCambioPropiedad("RecibioAtencionMedica");
      }
    }

    // Nuevo: 27-Abr-2012
    private Int64? _RecibioAtencionMedicaPais = 48L;
    [DataMember]
    public Int64? RecibioAtencionMedicaPais
    {
        get { return _RecibioAtencionMedicaPais; }
        set
        {
            _RecibioAtencionMedicaPais = value;
            ReportarCambioPropiedad("RecibioAtencionMedicaPais");
            ReportarCambioPropiedad("RecibioAtencionMedica");
        }
    }

    // Nuevo: 21-Dic-2011
    private Int64? _RecibioAtencionMedicaDepartamento;
    [DataMember]
    public Int64? RecibioAtencionMedicaDepartamento
    {
      get { return _RecibioAtencionMedicaDepartamento; }
      set
      {
        _RecibioAtencionMedicaDepartamento = value;
        ReportarCambioPropiedad("RecibioAtencionMedicaDepartamento");
        ReportarCambioPropiedad("RecibioAtencionMedica");
      }
    }

    // Nuevo: 21-Dic-2011
    private Int64? _RecibioAtencionMedicaMunicipio;
    [DataMember]
    public Int64? RecibioAtencionMedicaMunicipio
    {
      get { return _RecibioAtencionMedicaMunicipio; }
      set
      {
        _RecibioAtencionMedicaMunicipio = value;
        ReportarCambioPropiedad("RecibioAtencionMedicaMunicipio");
        ReportarCambioPropiedad("RecibioAtencionMedica");
      }
    }


    #endregion


  }
}
