using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;

/// <summary>
/// Descripción breve de DataSourceGeneral
/// </summary>
public class DataSourceGeneral
{
    public DataSourceGeneral()
    {

    }

    public static clsListasGeneralesValoracion CargarDatosGenerales()
    {
        string cError = string.Empty;

        ValoracionService objValService = new ValoracionService();
        clsListasGeneralesValoracion ObjGenerales = new clsListasGeneralesValoracion();
        DevolucionService objDevolucion = new DevolucionService();
        NotificacionService objNotificacion = new NotificacionService();
        GeneralService objGeneralSer = new GeneralService();

        ObjGenerales.Estados = objValService.ListarEstados();
        ObjGenerales.Principios = objValService.ListarPrincipios();
        ObjGenerales.Observaciones = objValService.ListarObservacion();
        ObjGenerales.Parametros = objValService.ListarParametros();
        ObjGenerales.sEtnias = new List<clsSubEtnias>();  //objValService.ListarSubEtnias();
        ObjGenerales.Autores = objValService.ListarAutores();
        ObjGenerales.Infracciones = objValService.ListarInfracciones();
        ObjGenerales.Herramientas = objValService.ListarHerramientas();
        ObjGenerales.TipoHerramientas = objValService.ListarTiposDeHerramienta();
        ObjGenerales.Registros = objValService.ListarRegistrosAnteriores();
        ObjGenerales.PreguntasRegAnt = objValService.ListarPreguntasRegAnt();
        ObjGenerales.Geografias = objValService.ListarGeografia();
        ObjGenerales.CausalesDevolucion = objDevolucion.ObtenerCausalesDevolucion(ref cError);
        ObjGenerales.EntidadesMunicipio = objValService.ObtenerEntidadesMunicipio(ref cError);
        ObjGenerales.Paises = new List<clsGeografiaCompleta>(); // objGeneralSer.ObtenerGeografiaCompleta(ref cError).Where(x => x.Tipo == 1).ToList();
        ObjGenerales.Departamentos = new List<clsGeografiaCompleta>(); // objGeneralSer.ObtenerGeografiaCompleta(ref cError).Where(x => x.Tipo == 2).ToList();
        ObjGenerales.Municipios = new List<clsGeografiaCompleta>(); // objGeneralSer.ObtenerGeografiaCompleta(ref cError).Where(x => x.Tipo == 3).ToList();
        ObjGenerales.Observaciones = objValService.ListarObservacion();
        ObjGenerales.HechoEnmarcado = objValService.HechoEnmarcado();
        ObjGenerales.DecretoLey = objValService.DecretoLey(); 
        RegistroTraza.I.Registrar("DataSourceGeneral:::CargarDatosGenerales:::Estados " + ObjGenerales.Estados.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Principios" + ObjGenerales.Estados.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Observaciones" + ObjGenerales.Observaciones.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Parametros" + ObjGenerales.Parametros.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::sEtnias" + ObjGenerales.sEtnias.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Autores" + ObjGenerales.Autores.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Infracciones" + ObjGenerales.Infracciones.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Herramientas" + ObjGenerales.Herramientas.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::TipoHerramientas" + ObjGenerales.TipoHerramientas.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Registros" + ObjGenerales.Registros.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::PreguntasRegAnt" + ObjGenerales.PreguntasRegAnt.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Geografias" + ObjGenerales.Geografias.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::CausalesDevolucion" + ObjGenerales.CausalesDevolucion.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::EntidadesMunicipio" + ObjGenerales.EntidadesMunicipio.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Paises" + ObjGenerales.Paises.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Departamentos" + ObjGenerales.Departamentos.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::Municipios" + ObjGenerales.Municipios.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::HechoEnmarcado" + ObjGenerales.HechoEnmarcado.Count + "\n" +
            "DataSourceGeneral:::CargarDatosGenerales:::DecretoLey" + ObjGenerales.DecretoLey.Count + "\n");
        return ObjGenerales;
    }

    public static void PoblarControl(ref object obj, Poblar tipo, string valor)
    {
        ValoracionService objValService = new ValoracionService();
        ListControl objList = (ListControl)obj;
        switch (tipo)
        {
            case Poblar.EstadosValoracion:

                List<clsEstadosValoracion> estados = RUV.Current.ListadosGeneralesValoracion.Estados; //objValService.ListarEstados();
                foreach (clsEstadosValoracion item in estados)
                {
                    ListItem li = new ListItem();
                    li.Text = item.Nombre;
                    li.Value = item.Id.ToString();
                    objList.Items.Add(li);
                }
                break;
            case Poblar.PrincipioValoracion:
                if (valor != null && valor != "0")
                {
                    int Estado = Convert.ToInt32(valor);
                    List<clsPrincipioEstado> principios = RUV.Current.ListadosGeneralesValoracion.Principios.Where(x => x.EstadoId == Estado).ToList();//objValService.ListarPrincipioEstadoPorEstadoId(Convert.ToInt32(valor));
                    foreach (clsPrincipioEstado item in principios)
                    {
                        ListItem li = new ListItem();
                        string texto = string.Empty;
                        if (item.Nombre.Length > 100)
                            texto = item.Nombre.Substring(0, 100) + "...";
                        else
                            texto = item.Nombre;
                        li.Text = texto;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    int Estado = Convert.ToInt32(valor);
                    List<clsPrincipioEstado> principios = RUV.Current.ListadosGeneralesValoracion.Principios;
                    foreach (clsPrincipioEstado item in principios)
                    {
                        ListItem li = new ListItem();
                        string texto = string.Empty;
                        if (item.Nombre.Length > 100)
                            texto = item.Nombre.Substring(0, 100) + "...";
                        else
                            texto = item.Nombre;
                        li.Text = texto;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.ObservacionesValoracion:
                if (valor != null && valor != "0")
                {
                    int Estado = Convert.ToInt32(valor);
                    List<clsObservacionEstado> observaciones = RUV.Current.ListadosGeneralesValoracion.Observaciones.Where(x => x.EstadoId == Estado).ToList();//objValService.ListarObservacionEstadoPorEstadoId(Convert.ToInt32(valor));
                    foreach (clsObservacionEstado item in observaciones)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    List<clsObservacionEstado> observaciones = RUV.Current.ListadosGeneralesValoracion.Observaciones;
                    foreach (clsObservacionEstado item in observaciones)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.HechoEnmarcado:
                if (valor != null && valor != "0")
                {
                    int idHechoEnmarcado = Convert.ToInt32(valor);
                    List<clsHechoEnmarcado> observaciones = RUV.Current.ListadosGeneralesValoracion.HechoEnmarcado.Where(x => x.Id == idHechoEnmarcado).ToList();
                    foreach (clsHechoEnmarcado item in observaciones)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    List<clsHechoEnmarcado> hechoEnmarcado = RUV.Current.ListadosGeneralesValoracion.HechoEnmarcado;
                    foreach (clsHechoEnmarcado item in hechoEnmarcado)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.DecretoLey:
                if (valor != null && valor != "0")
                {
                    int idDecretoLey = Convert.ToInt32(valor);
                    List<clsDecretoLey> Decreto = RUV.Current.ListadosGeneralesValoracion.DecretoLey.Where(x => x.Id == idDecretoLey).ToList();
                    foreach (clsDecretoLey item in Decreto)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    List<clsDecretoLey> decretoLey = RUV.Current.ListadosGeneralesValoracion.DecretoLey;
                    foreach (clsDecretoLey item in decretoLey)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;

            case Poblar.Parametros:
                if (valor != null && valor != "0")
                {
                    eTipoParametros TipoParametro = (eTipoParametros)Enum.ToObject(typeof(eTipoParametros), Convert.ToInt32(valor));
                    List<clsParametroGeneral> Afectaciones = RUV.Current.ListadosGeneralesValoracion.Parametros.Where(x => x.Tipo == TipoParametro).ToList();
                    foreach (clsParametroGeneral item in Afectaciones)
                    {
                        ListItem li = new ListItem();
                        string texto = string.Empty;
                        if (item.Nombre.Length > 100)
                            texto = item.Nombre.Substring(0, 100) + "...";
                        else
                            texto = item.Nombre;
                        li.Text = texto;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.SubEtnias:
                if (valor != null && valor != "0") // Valor recibe el id de la etnia
                {
                    //List<clsSubEtnias> SubEtnia = RUV.Current.ListadosGeneralesValoracion.SubEtnias.Where(x => x.Tipo == TipoParametro).ToList();
                    List<clsSubEtnias> SubEtnia = objValService.ListarSubEtnias(int.Parse(valor)).ToList();
                    foreach (clsSubEtnias item in SubEtnia)
                    {
                        ListItem li = new ListItem();
                        string texto = string.Empty;
                        if (item.Nombre.Length > 100)
                            texto = item.Nombre.Substring(0, 100) + "...";
                        else
                            texto = item.Nombre;
                        li.Text = texto;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.Autores:
                if (valor != null && valor != "0")
                {
                    List<clsAutores> Autores = objValService.ListarAutoresPorAnexo(Convert.ToInt32(valor));
                    foreach (clsAutores item in Autores)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    List<clsAutores> Autores = RUV.Current.ListadosGeneralesValoracion.Autores;
                    foreach (clsAutores item in Autores)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.Infracciones:
                if (valor != null && valor != "0")
                {
                    List<clsInfracciones> AutoresInfra = objValService.ListarInfraccionesPorValPerId(Convert.ToInt32(valor));
                    foreach (clsInfracciones item in AutoresInfra)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                else
                {
                    List<clsInfracciones> AutoresInfra = RUV.Current.ListadosGeneralesValoracion.Infracciones;//objValService.ListarInfracciones();
                    foreach (clsInfracciones item in AutoresInfra)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.Herramientas:
                if (valor != null && valor != "0")
                {
                    int Tipo = Convert.ToInt32(valor);
                    List<clsHerramientas> Herramientas = RUV.Current.ListadosGeneralesValoracion.Herramientas.Where(x => x.TipoId == Tipo).ToList(); //objValService.ListarHerramientasPorTipo(Convert.ToInt32(valor));
                    foreach (clsHerramientas item in Herramientas)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.TipoHerramientas:
                List<clsTipoHerramienta> TipoHerramientas = RUV.Current.ListadosGeneralesValoracion.TipoHerramientas;//objValService.ListarTiposDeHerramienta();
                foreach (clsTipoHerramienta item in TipoHerramientas)
                {
                    ListItem li = new ListItem();
                    li.Text = item.Nombre;
                    li.Value = item.Id.ToString();
                    objList.Items.Add(li);
                }
                break;
            case Poblar.RegistrosAnteriores:
                List<clsRegistrosAnteriores> Registros = RUV.Current.ListadosGeneralesValoracion.Registros;//objValService.ListarRegistrosAnteriores();
                foreach (clsRegistrosAnteriores item in Registros)
                {
                    ListItem li = new ListItem();
                    li.Text = item.Nombre;
                    li.Value = item.Id.ToString();
                    objList.Items.Add(li);
                }
                break;
            case Poblar.PreguntasRegAnteriores:
                List<clsPreguntasRegAnt> Preguntas = RUV.Current.ListadosGeneralesValoracion.PreguntasRegAnt;//objValService.ListarPreguntasRegAnt();
                foreach (clsPreguntasRegAnt item in Preguntas)
                {
                    ListItem li = new ListItem();
                    li.Text = item.Pregunta;
                    li.Value = item.Id.ToString();
                    objList.Items.Add(li);
                }
                break;
            case Poblar.DocumentosActosAd:
                if (valor != null && valor != "0")
                {
                    ActosAdminService objActosAdmin = new ActosAdminService();
                    List<clsParametroGeneral> Documentos = objActosAdmin.GetDocumentosPorArea(Convert.ToInt32(valor));
                    foreach (clsParametroGeneral item in Documentos)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.Nombre;
                        li.Value = item.Id.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.CausalesDevolucion:
                if (valor != null && valor != "0")
                {
                    List<clsCausal> causales = RUV.Current.ListadosGeneralesValoracion.CausalesDevolucion.Where(x => x.EParametroTipoCausal == (eTipoParametros)Convert.ToInt32(valor)).ToList();
                    foreach (clsCausal item in causales)
                    {
                        ListItem li = new ListItem();
                        li.Text = item.CNombre;
                        li.Value = item.NId.ToString();
                        objList.Items.Add(li);
                    }
                }
                break;
            case Poblar.EntidadesMunicipio:
                List<clsEntidadMunicipio> entidadesMunicipio = RUV.Current.ListadosGeneralesValoracion.EntidadesMunicipio;
                foreach (var item in entidadesMunicipio)
                {
                    ListItem li = new ListItem();
                    li.Text = item.CNombreEntidad;
                    li.Value = item.NId.ToString();
                    objList.Items.Add(li);
                }
                break;
            case Poblar.Paises:
                //List<clsGeografiaCompleta> paises = RUV.Current.ListadosGeneralesValoracion.Paises;
                GeneralService objGeneralSer = new GeneralService();
                string cError = string.Empty;
                List<clsGeografiaCompleta> paises = objGeneralSer.ObtenerPaises(ref cError);
                if (string.IsNullOrEmpty(cError) && paises != null)
                    paises.ForEach(x =>
                    {
                        ListItem li = new ListItem();
                        li.Text = x.Nombre;
                        li.Value = x.Id.ToString();
                        objList.Items.Add(li);
                    });
                break;            
            default:
                break;
        }
        obj = objList;
    }


    public static void PoblarFiltroPorProceso(Proceso proceso, ref DropDownList ddl)
    {
        List<clsTipoFiltro> tipos = new List<clsTipoFiltro>();

        DataSet ds = new DataSet();
        ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "/Utilidades/XMLs/ValoresDefault.xml");
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables["Filtro"];
            foreach (DataRow dr in dt.Select(string.Format("Proceso = {0}", proceso.GetHashCode())))
            {
                clsTipoFiltro filtro = new clsTipoFiltro();
                filtro.Id = Convert.ToInt32(dr["Id"]);
                filtro.Nombre = dr["Nombre"].ToString();
                filtro.Descripcion = dr["Descripcion"].ToString();
                TypeCode tipo = (TypeCode)Enum.Parse(typeof(TypeCode), dr["TipoDato"].ToString());
                filtro.TipoDato = tipo;
                Proceso proc = (Proceso)Enum.Parse(typeof(Proceso), dr["Proceso"].ToString());
                filtro.Proceso = proc;
                tipos.Add(filtro);
            }
        }
        ddl.DataSource = tipos;
        ddl.DataBind();
    }

    public static clsTipoFiltro ObtenerFiltroPorId(int Id, Proceso proceso)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "/Utilidades/XMLs/ValoresDefault.xml");
        clsTipoFiltro filtro = new clsTipoFiltro();
        if (ds.Tables.Count > 0)
        {
            List<clsTipoFiltro> tipos = new List<clsTipoFiltro>();
            DataTable dt = ds.Tables["Filtro"];
            foreach (DataRow dr in dt.Select(string.Format("Proceso = {0}", proceso.GetHashCode())))
            {
                if (Convert.ToInt32(dr["Id"]) == Id)
                {
                    filtro.Id = Convert.ToInt32(dr["Id"]);
                    filtro.Nombre = dr["Nombre"].ToString();
                    filtro.Descripcion = dr["Descripcion"].ToString();
                    TypeCode tipo = (TypeCode)Enum.Parse(typeof(TypeCode), dr["TipoDato"].ToString());
                    filtro.TipoDato = tipo;
                    Proceso proc = (Proceso)Enum.Parse(typeof(Proceso), dr["Proceso"].ToString());
                    filtro.Proceso = proc;
                }
            }
        }
        return filtro;
    }

}