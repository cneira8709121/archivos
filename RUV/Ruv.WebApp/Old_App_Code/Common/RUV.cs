using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

/// <summary>
/// Descripción breve de RUV
/// </summary>
public class RUV
{

    private static readonly Lazy<RUV> Obj = new Lazy<RUV>(() => new RUV());
    public RUV()
    {
    }

    public SIRAV.Entidades.Administracion.USUARIO Usuario
    {
        get
        {
            return Varios.Usuario(HttpContext.Current);
        }
    }

    public clsListasGeneralesValoracion ListadosGeneralesValoracion
    {
        get
        {
            clsListasGeneralesValoracion objLis;
            if (HttpContext.Current.Session["DatosGeneralesValoracion"] == null)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ListadosGeneralesValoracion:::Session[\"DatosGeneralesValoracion\"] = null");
                objLis = new clsListasGeneralesValoracion();
                objLis = DataSourceGeneral.CargarDatosGenerales();
                HttpContext.Current.Session["DatosGeneralesValoracion"] = objLis;
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ListadosGeneralesValoracion:::Session[\"DatosGeneralesValoracion\"] Guardada");
            }
            else
            {
                objLis = (clsListasGeneralesValoracion)HttpContext.Current.Session["DatosGeneralesValoracion"];
            }
            return objLis;
        }
    }

    public static RUV Current
    {
        get
        {
            return Obj.Value;
        }
    }

}