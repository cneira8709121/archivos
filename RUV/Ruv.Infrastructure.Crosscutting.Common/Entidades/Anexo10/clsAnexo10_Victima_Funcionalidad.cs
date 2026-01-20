using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo10_Victima : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo10_Victima R = Resultado as clsAnexo10_Victima;
      R.ID = this.ID;
      R.Afectacion = this.Afectacion.ObtenerCopia();
      R.AtendidoPorICBF = this.AtendidoPorICBF;
      R.AtendidoPorOtraEntidad = this.AtendidoPorOtraEntidad;
      R.DenunciaPrevia = this.DenunciaPrevia.ObtenerCopia();
      R.FechaAtencionICBF = this.FechaAtencionICBF;
      R.FechaAtencionOtraEntidad = this.FechaAtencionOtraEntidad;
      R.GrupoArmado = this.GrupoArmado;
      R.GrupoArmadoFechaDesvinculacion = this.GrupoArmadoFechaDesvinculacion;
      R.NombreOtraEntidadQueAtendio = this.NombreOtraEntidadQueAtendio;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.VictimaDeEsteHecho = this.VictimaDeEsteHecho;
      R.EstadoRegistro = this.EstadoRegistro;
      R.AnexoPadre = this.AnexoPadre;

      return Resultado;
    }

    clsAnexo10 _AnexoPadre;
    [System.Xml.Serialization.XmlIgnore]
    public clsAnexo10 AnexoPadre
    {
        get { return _AnexoPadre; }
        set { _AnexoPadre = value; }
    }
  }
}
