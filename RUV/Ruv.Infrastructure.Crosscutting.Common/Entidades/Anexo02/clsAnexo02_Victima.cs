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
  public partial class clsAnexo02_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo02_Victima()
    {      
      Afectacion = new clsAnexo_Afectacion() { Victima = this };
      DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 02"; } }

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
                ProteccionHaSolicitado = 0;
                ProteccionLeHanBrindado = 0;
                HaContinuadoConLasAmenzas = 0;
            }
           * */
            ReportarCambioPropiedad("VictimaDeEsteHecho");
            ReportarCambioPropiedad("ProteccionHaSolicitado");
            ReportarCambioPropiedad("ProteccionLeHanBrindado");
            ReportarCambioPropiedad("HaContinuadoConLasAmenzas");
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

    #region PREGUNTAS 12 Y 13

    private int? _ProteccionHaSolicitado;
    [DataMember]
    public int? ProteccionHaSolicitado
    {
      get { return _ProteccionHaSolicitado; }
      set
      {
        _ProteccionHaSolicitado = value;
        ReportarCambioPropiedad("ProteccionHaSolicitado");
      }
    }

    private int? _ProteccionLeHanBrindado;
    [DataMember]
    public int? ProteccionLeHanBrindado
    {
      get { return _ProteccionLeHanBrindado; }
      set
      {
        _ProteccionLeHanBrindado = value;
        if (value != 1)
        {
            ProteccionTipoDeMedida = null;
            ProteccionEntidad = null;
            ProteccionFechaInicial = null;
            ProteccionVigencia = null;
        }
        ReportarCambioPropiedad("ProteccionLeHanBrindado");
        ReportarCambioPropiedad("ProteccionTipoDeMedida");
        ReportarCambioPropiedad("ProteccionEntidad");
        ReportarCambioPropiedad("ProteccionFechaInicial");
        ReportarCambioPropiedad("ProteccionVigencia");
      }
    }

    private string _ProteccionTipoDeMedida;
    [DataMember]
    public string ProteccionTipoDeMedida
    {
      get { return _ProteccionTipoDeMedida; }
      set
      {
        _ProteccionTipoDeMedida = value;
        ReportarCambioPropiedad("ProteccionTipoDeMedida");
        ReportarCambioPropiedad("ProteccionLeHanBrindado");
      }
    }

    private string _ProteccionEntidad;
    [DataMember]
    public string ProteccionEntidad
    {
      get { return _ProteccionEntidad; }
      set
      {
        _ProteccionEntidad = value;
        ReportarCambioPropiedad("ProteccionEntidad");
        ReportarCambioPropiedad("ProteccionLeHanBrindado");
      }
    }

    private DateTime? _ProteccionFechaInicial;
    [DataMember]
    public DateTime? ProteccionFechaInicial
    {
      get { return _ProteccionFechaInicial; }
      set
      {
        _ProteccionFechaInicial = value;
        ReportarCambioPropiedad("ProteccionFechaInicial");
        ReportarCambioPropiedad("ProteccionLeHanBrindado");
      }
    }

    private string _ProteccionVigencia;
    [DataMember]
    public string ProteccionVigencia
    {
      get { return _ProteccionVigencia; }
      set
      {
        _ProteccionVigencia = value;
        ReportarCambioPropiedad("ProteccionVigencia");
        ReportarCambioPropiedad("ProteccionLeHanBrindado");
      }
    }

    private int? _HaContinuadoConLasAmenzas;
    [DataMember]
    public int? HaContinuadoConLasAmenzas
    {
      get { return _HaContinuadoConLasAmenzas; }
      set
      {
        _HaContinuadoConLasAmenzas = value;
        ReportarCambioPropiedad("HaContinuadoConLasAmenzas");
      }
    }

    #endregion
  }
}
