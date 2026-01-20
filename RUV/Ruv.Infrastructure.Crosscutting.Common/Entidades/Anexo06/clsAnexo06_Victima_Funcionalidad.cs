using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo06_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo06_Victima R = Resultado as clsAnexo06_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.AlgunMenorQuedoHuerfano = this.AlgunMenorQuedoHuerfano;
      R.MenorQuedoHuerfanoDe = this.MenorQuedoHuerfanoDe;
      R.MenorDesprotegidoId = this.MenorDesprotegidoId;
      R.NumeroPersonasMuertasEnEsteHecho = this.NumeroPersonasMuertasEnEsteHecho;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.RecuerdaNumeroPersonasMuertas = this.RecuerdaNumeroPersonasMuertas;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.VictimaFallecida = this.VictimaFallecida;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    clsAnexo06 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo06 AnexoPadre
    {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
