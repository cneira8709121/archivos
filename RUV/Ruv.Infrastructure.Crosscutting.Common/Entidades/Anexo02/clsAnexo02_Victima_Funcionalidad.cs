using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo02_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo02_Victima R = Resultado as clsAnexo02_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.HaContinuadoConLasAmenzas = this.HaContinuadoConLasAmenzas;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.ProteccionEntidad = this.ProteccionEntidad;
      R.ProteccionFechaInicial = this.ProteccionFechaInicial;
      R.ProteccionLeHanBrindado = this.ProteccionLeHanBrindado;
      R.ProteccionHaSolicitado = this.ProteccionHaSolicitado;
      R.ProteccionTipoDeMedida = this.ProteccionTipoDeMedida;
      R.ProteccionVigencia = this.ProteccionVigencia;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

      clsAnexo02 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo02 AnexoPadre {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
