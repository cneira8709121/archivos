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
  public partial class clsAnexo10_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo10_Victima()
    {      
      Afectacion = new clsAnexo_Afectacion() { Victima = this };
      DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre }; ;

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 10"; } }

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

    #region PREGUNTA 9 AL 11

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

            GrupoArmado = null;
        }
           * */
        ReportarCambioPropiedad("VictimaDeEsteHecho");
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

    #region PREGUNTA 12

    private string _GrupoArmado;
    [DataMember]
    public string GrupoArmado
    {
      get { return _GrupoArmado; }
      set
      {
        _GrupoArmado = value;
        if (string.IsNullOrWhiteSpace(value))
        {
            GrupoArmadoFechaDesvinculacion = null;
            AtendidoPorICBF = null;
            AtendidoPorOtraEntidad = null;
        }
        ReportarCambioPropiedad("GrupoArmado");
        ReportarCambioPropiedad("GrupoArmadoFechaDesvinculacion");
        ReportarCambioPropiedad("AtendidoPorICBF");
        ReportarCambioPropiedad("AtendidoPorOtraEntidad");
      }
    }

    private DateTime? _GrupoArmadoFechaDesvinculacion;
    [DataMember]
    public DateTime? GrupoArmadoFechaDesvinculacion
    {
      get { return _GrupoArmadoFechaDesvinculacion; }
      set
      {
        _GrupoArmadoFechaDesvinculacion = value;
        ReportarCambioPropiedad("GrupoArmadoFechaDesvinculacion");
        ReportarCambioPropiedad("GrupoArmado");
      }
    }

    private int? _AtendidoPorICBF;
    [DataMember]
    public int? AtendidoPorICBF
    {
      get { return _AtendidoPorICBF; }
      set
      {
        _AtendidoPorICBF = value;
          if(value != 1)FechaAtencionICBF = null;
        ReportarCambioPropiedad("AtendidoPorICBF");
        ReportarCambioPropiedad("FechaAtencionICBF");
      }
    }

    private DateTime? _FechaAtencionICBF;
    [DataMember]
    public DateTime? FechaAtencionICBF
    {
      get { return _FechaAtencionICBF; }
      set
      {
        _FechaAtencionICBF = value;
        ReportarCambioPropiedad("FechaAtencionICBF");
      }
    }

    private int? _AtendidoPorOtraEntidad;
    [DataMember]
    public int? AtendidoPorOtraEntidad
    {
      get { return _AtendidoPorOtraEntidad; }
      set
      {
        _AtendidoPorOtraEntidad = value;
        if (value != 1)
        {
            FechaAtencionOtraEntidad = null;
            NombreOtraEntidadQueAtendio = null;
        }
        ReportarCambioPropiedad("AtendidoPorOtraEntidad");
        ReportarCambioPropiedad("FechaAtencionOtraEntidad");
        ReportarCambioPropiedad("NombreOtraEntidadQueAtendio");
      }
    }

    private DateTime? _FechaAtencionOtraEntidad;
    [DataMember]
    public DateTime? FechaAtencionOtraEntidad
    {
      get { return _FechaAtencionOtraEntidad; }
      set
      {
        _FechaAtencionOtraEntidad = value;
        ReportarCambioPropiedad("FechaAtencionOtraEntidad");
      }
    }

    private string _NombreOtraEntidadQueAtendio;
    [DataMember]
    public string NombreOtraEntidadQueAtendio
    {
      get { return _NombreOtraEntidadQueAtendio; }
      set
      {
        _NombreOtraEntidadQueAtendio = value;
        ReportarCambioPropiedad("NombreOtraEntidadQueAtendio");
      }
    }


    #endregion
  }
}
