using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo08_Victima : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo08_Victima R = Resultado as clsAnexo08_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.ComoSeProdujoLiberacion = this.ComoSeProdujoLiberacion;
      R.ContraprestacionPedida = this.ContraprestacionPedida;
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.FechaLiberacion = this.FechaLiberacion;
      R.FinalidadSecuestroExtorsivo = this.FinalidadSecuestroExtorsivo;
      R.HanPedidoContraprestacionPorLibertad = this.HanPedidoContraprestacionPorLibertad;
      R.OtraFinalidadSecuestroOtro = this.OtraFinalidadSecuestroOtro;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.SituacionActualVictima = this.SituacionActualVictima;
      R.TipoDeSecuestro = this.TipoDeSecuestro;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.EstadoRegistro = this.EstadoRegistro;
      R.PersonaEstaSecuestrada = this.PersonaEstaSecuestrada;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo08 AnexoPadre;


  }
}
