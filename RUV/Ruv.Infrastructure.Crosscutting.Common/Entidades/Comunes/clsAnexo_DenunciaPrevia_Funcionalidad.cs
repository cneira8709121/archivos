using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo_DenunciaPrevia : clsEntidadBase, IDataErrorInfo
  {

    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public clsAnexo_DenunciaPrevia ObtenerCopia()
    {
      clsAnexo_DenunciaPrevia Resultado = new clsAnexo_DenunciaPrevia
      {
        SePresento = this.SePresento,
        Entidad = this.Entidad,
        Fecha = this.Fecha,
        Pais = this.Pais,
        Departamento = this.Departamento,
        Municipio = this.Municipio,
        Codigo = this.Codigo,
        ID = null,
        EstadoRegistro = this.EstadoRegistro,
        AnexoPadre = this.AnexoPadre 
      };

      return Resultado;
    }

    [XmlIgnore]
    public IAnexo  AnexoPadre { get; set; }
  }
}
