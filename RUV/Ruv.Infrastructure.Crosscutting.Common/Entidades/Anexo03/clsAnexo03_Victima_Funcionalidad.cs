using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo03_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo03_Victima R = Resultado as clsAnexo03_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.AtencionMedicaAyudaRecibida = this.AtencionMedicaAyudaRecibida;
      R.AtencionMedicaEntidad = this.AtencionMedicaEntidad;
      R.AtencionMedicaPais = this.AtencionMedicaPais;
      R.AtencionMedicaDepartamento = this.AtencionMedicaDepartamento;
      R.AtencionMedicaMunicipio = this.AtencionMedicaMunicipio;
      R.AtencionMedicaRecibioAtencionMedica = this.AtencionMedicaRecibioAtencionMedica;
      R.AtencionMedicaRecibioAyuda = this.AtencionMedicaRecibioAyuda;
      R.AtencionMedicaSolicitoAyuda = this.AtencionMedicaSolicitoAyuda;
      R.AtencionMedicaSolicitoAyudaEntidad = this.AtencionMedicaSolicitoAyudaEntidad;
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;

      R.DelitosSexuales.Clear();
      this.DelitosSexuales.ForEach(x => R.DelitosSexuales.Add(x));

      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

      clsAnexo03 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo03 AnexoPadre {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
