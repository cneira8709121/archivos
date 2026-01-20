using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo01_Victima : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo01_Victima R = Resultado as clsAnexo01_Victima;
      R.ID = this.ID;
      R.AtencionEntidadMedica = this.AtencionEntidadMedica;
      R.AtencionMedicaPais = this.AtencionMedicaPais;
      R.AtencionMedicaDepartamento = this.AtencionMedicaDepartamento;
      R.AtencionMedicaMunicipio = this.AtencionMedicaMunicipio;
      R.AtencionMedicaRecibio = this.AtencionMedicaRecibio;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.AnexoPadre = this.AnexoPadre;

      R.Bienes.Clear();
      foreach (var UnBien in this.Bienes)
        R.Bienes.Add(UnBien.ObtenerCopia<clsAnexo01_Victima_Bien>());

      return Resultado;

    }

    /// <summary>
    /// Vista editable de los bienes.
    /// </summary>
    [System.Xml.Serialization.XmlIgnore()]
    public IEditableCollectionView BienesVistaEditable { get; set; }

    clsAnexo01 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo01 AnexoPadre {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
