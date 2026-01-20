using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo04_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo04_Victima R = Resultado as clsAnexo04_Victima;
      R.ID = this.ID;
      R.ActividadAlDesaparecer = this.ActividadAlDesaparecer;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.HarealizadoBusquedaAnteEntidad = this.HarealizadoBusquedaAnteEntidad;
      R.HaRealizadoBusquedaDeVictima = this.HaRealizadoBusquedaDeVictima;
      R.MenorDesprotegidoId = this.MenorDesprotegidoId;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.QuedoMenorDesprotegido = this.QuedoMenorDesprotegido;
      R.SePresentoEventoAnterior = this.SePresentoEventoAnterior;
      R.SePresentoEventoPosterior = this.SePresentoEventoPosterior;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.VictimaDesaparecida = this.VictimaDesaparecida;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    clsAnexo04 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo04 AnexoPadre
    {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
