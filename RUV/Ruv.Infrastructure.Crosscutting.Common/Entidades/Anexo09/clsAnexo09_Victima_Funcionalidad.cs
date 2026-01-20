using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo09_Victima : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo09_Victima R = Resultado as clsAnexo09_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.AtencionMedicaRecibioAtencionMedica = this.AtencionMedicaRecibioAtencionMedica;
      R.AtencionMedicaEntidad = this.AtencionMedicaEntidad;
      R.RecibioAtencionMedicaPais = this.RecibioAtencionMedicaPais;
      R.RecibioAtencionMedicaDepartamento = this.RecibioAtencionMedicaDepartamento;
      R.RecibioAtencionMedicaMunicipio = this.RecibioAtencionMedicaMunicipio;
      R.AtencionMedicaRecibioAyuda = this.AtencionMedicaRecibioAyuda;
      R.AtencionMedicaAyudaRecibida = this.AtencionMedicaAyudaRecibida;
      R.AtencionMedicaSolicitoAyuda = this.AtencionMedicaSolicitoAyuda;
      R.AtencionMedicaSolicitoAyudaEntidad = this.AtencionMedicaSolicitoAyudaEntidad;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    clsAnexo09 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo09 AnexoPadre
    {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
