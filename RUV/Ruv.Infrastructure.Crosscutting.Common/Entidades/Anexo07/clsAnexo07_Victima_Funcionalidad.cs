using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo07_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo07_Victima R = Resultado as clsAnexo07_Victima;
      R.ID = this.ID;
      R.ActividadAlMomentoDelHecho = this.ActividadAlMomentoDelHecho;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.AlgunMenorQuedoHuerfano = this.AlgunMenorQuedoHuerfano;
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.EstadoVictima = this.EstadoVictima;
      R.MenorQuedoHuerfanoDe = this.MenorQuedoHuerfanoDe;
      R.MenorDesprotegidoId = this.MenorDesprotegidoId;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.RecibioAtencionMedica = this.RecibioAtencionMedica;
      R.RecibioAtencionMedicaEntidad = this.RecibioAtencionMedicaEntidad;
      R.RecibioAtencionMedicaPais = this.RecibioAtencionMedicaPais;
      R.RecibioAtencionMedicaDepartamento = this.RecibioAtencionMedicaDepartamento;
      R.RecibioAtencionMedicaMunicipio = this.RecibioAtencionMedicaMunicipio;
      R.TipoAccidente = this.TipoAccidente;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    clsAnexo07 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo07 AnexoPadre
    {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
