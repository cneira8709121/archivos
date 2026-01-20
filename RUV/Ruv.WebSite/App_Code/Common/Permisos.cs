using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Menus
{
    public List<Permisos> ObtenerPermisos()
    {
        List<Permisos> permisos = new List<Permisos>();
        permisos.Add(new Permisos() { Id = "1014", Nombre = "Valoración", Url = string.Empty, Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1009", Nombre = "Asignar", Url = "/Valoracion/Asignacion/AsignarValoraciones.aspx", Padre = "1014", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "100901", Nombre = "Guardar", Url = null, Tipo = 3, CausaValidacion = true, Padre = "1009", Imagen = "~/Utilidades/Imagenes/Guardar.png" });
        permisos.Add(new Permisos() { Id = "100902", Nombre = "Exportar", Url = null, Tipo = 3, CausaValidacion = false, Padre = "1009", Imagen = "~/Utilidades/Imagenes/Descargar.png", ClientScript = "ShowModConsult()" });
        permisos.Add(new Permisos() { Id = "100903", Nombre = "Atras", Url = null, Tipo = 3, CausaValidacion = false, Padre = "1009", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1010", Nombre = "Reasignar Valoracion", Url = "/Valoracion/Asignacion/ReasignarValoraciones.aspx", Padre = "1014", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "101001", Nombre = "Guardar", Url = null, Tipo = 3, CausaValidacion = true, Padre = "1010", Imagen = "~/Utilidades/Imagenes/Guardar.png" });
        permisos.Add(new Permisos() { Id = "101002", Nombre = "Atras", Url = null, Tipo = 3, CausaValidacion = false, Padre = "1010", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1011", Nombre = "Valorar", Url = "/Valoracion/Valoracion/Default.aspx", Padre = "1014", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1012", Nombre = "Valorar", Url = "/Valoracion/Valoracion/Nueva.aspx", Tipo = 3, Padre = "1011", CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Editar.png" });
        permisos.Add(new Permisos() { Id = "101201", Nombre = "Resumen", Url = "/Valoracion/Valoracion/Resumen.aspx", Tipo = 3, Padre = "1011", CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Editar.png" });
        permisos.Add(new Permisos() { Id = "10120101", Nombre = "Atras", Url = null, Tipo = 4, CausaValidacion = false, Padre = "101201", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "101203", Nombre = "Ver Declaración", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Descargar.png", ClientScript = "ShowModConsult()" });
        permisos.Add(new Permisos() { Id = "101204", Nombre = "Nuevo Hecho Victimizante", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Nuevo.png", ClientScript = "return ShowModConsult('mpopUpNHechoBehavior');", ServerCode = false });
        permisos.Add(new Permisos() { Id = "101207", Nombre = "Personas Asociadas a la Declaracion", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/PersonasAsociadas.png", ClientScript = "", ServerCode = true });
        permisos.Add(new Permisos() { Id = "101205", Nombre = "Valores Actos Administrativos", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Editar.png", ClientScript = "return displayASPNETControl('mpopUpValoresAABehavior', function() { ruv.valoracion_valoresactos.initialize(); });", ServerCode = false });
        permisos.Add(new Permisos() { Id = "101201", Nombre = "Guardar", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Guardar.png", ClientScript="ShowModConsult(null, 'Guardando Valoración')" });
        permisos.Add(new Permisos() { Id = "101202", Nombre = "Finalizar", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Finalizar.png", ClientScript="return ShowModConsult('mpopGuardarBehavior')" });
        //permisos.Add(new Permisos() { Id = "101206", Nombre = "Modificar Punto Notificación", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/PuntoNotificacion.png", ClientScript = "return ShowModConsult('mpopUpPuntosNotBehavior');", ServerCode = false });
        permisos.Add(new Permisos() { Id = "101204", Nombre = "Atras", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1012", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1015", Nombre = "Actos Administrativos", Url = string.Empty, Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1016", Nombre = "Actos Administrativos", Url = "/ActosAdmin/Default.aspx", Padre = "1015", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1017", Nombre = "Nuevo", Url = "/ActosAdmin/Nuevo.aspx", Padre = "1016", Tipo = 3, CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Nuevo.png" });
        permisos.Add(new Permisos() { Id = "101701", Nombre = "Guardar", Url = null, Tipo = 4, CausaValidacion = true, Padre = "1017", Imagen = "~/Utilidades/Imagenes/Guardar.png" });
        permisos.Add(new Permisos() { Id = "101702", Nombre = "Atras", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1017", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1018", Nombre = "Editar", Url = "/ActosAdmin/Editar.aspx", Padre = "1016", Tipo = 3, CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Editar.png" });
        permisos.Add(new Permisos() { Id = "101801", Nombre = "Guardar", Url = null, Tipo = 4, CausaValidacion = true, Padre = "1018", Imagen = "~/Utilidades/Imagenes/Guardar.png" });
        permisos.Add(new Permisos() { Id = "101802", Nombre = "Atras", Url = null, Tipo = 4, CausaValidacion = false, Padre = "1018", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1019", Nombre = "Anular", Url = string.Empty, Padre = "1016", Tipo = 3, CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Anular.png" });
        permisos.Add(new Permisos() { Id = "1020", Nombre = "Firmar", Url = string.Empty, Padre = "1016", Tipo = 3, CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Firmar.png" });
        permisos.Add(new Permisos() { Id = "1021", Nombre = "Consulta", Url = string.Empty, Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1022", Nombre = "Consulta Persona", Url = "/Consultas/ConsultaPersona.aspx", Padre = "1021", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "102201", Nombre = "Atras", Url = "/Consultas/ConsultaPersona.aspx", Padre = "1022", Tipo = 3, CausaValidacion = false, Imagen = "~/Utilidades/Imagenes/Atras.png"});
        permisos.Add(new Permisos() { Id = "1024", Nombre = "Control de documentos", Url = string.Empty, Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1025", Nombre = "Control de documentos", Url = "/ControlDocumentos/ControlDocumentos.aspx", Padre = "1024", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1027", Nombre = "Consultar documentos", Url = "/ControlDocumentos/CodigosAsignados.aspx", Padre = "1024", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1030", Nombre = "Gestion", Url = string.Empty,Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1031", Nombre = "Valoracion", Url = "/Gestion/Valoracion/GestionValoracion.aspx", Padre = "1030", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "103101", Nombre = "Atras", Url = "/Gestion/Valoracion/GestionValoracion.aspx", Tipo = 3, CausaValidacion = false, Padre = "1031", Imagen = "~/Utilidades/Imagenes/Atras.png" });
        permisos.Add(new Permisos() { Id = "1033", Nombre = "Consultar personas", Url = "/Correcciones/ConsultaPersona.aspx", Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1033", Nombre = "Solicitar correccion", Url = "/Correcciones/SolicitudCorreccion.aspx", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1034", Nombre = "Notificacion", Url = "/Notificaciones/ConsultarNotificaciones.aspx", Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1036", Nombre = "Notificaciones Entregadas", Url = "/Notificaciones/NotificacionesEntregadas.aspx", Padre = "1034", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1051", Nombre = "Notificaciones Entregadas", Url = "/Notificaciones/NotificacionesEntregadas.aspx", Padre = "1034", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1046", Nombre = "Paquetes Generados", Url = "/Notificaciones/PaquetesNotificacion.aspx", Padre = "1034", Tipo = 2, CausaValidacion = false });

        permisos.Add(new Permisos() { Id = "1047", Nombre = "Configuracion", Url = string.Empty, Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1048", Nombre = "Calendario", Url = "/Configuracion/Calendario.aspx", Padre = "1047", Tipo = 2, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1052", Nombre = "Preparación Noti.", Url = "/Notificaciones/PrepararNotificaciones.aspx", Tipo = 1, CausaValidacion = false, Imagen = null });
        permisos.Add(new Permisos() { Id = "1053", Nombre = "Consulta Centro Atencion", Url = "/Notificaciones/ConsultaDatosCentrosAtencion.aspx", Padre = "1034",Tipo = 2, CausaValidacion = false });
        //permisos.Add(new Permisos() { Id = "1054", Nombre = "Gestion Edicto", Url = "/Notificaciones/EdictoPublicadoNotificacionesDetalle.aspx", Tipo = 1, CausaValidacion = false });

        permisos.Add(new Permisos() { Id = "9999", Nombre = "Test Reportes", Url = "/Test/TestPage.aspx", Tipo = 1, CausaValidacion = false, Imagen = null });

        return permisos;
    }
}


public class Permisos
{
    private string id;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    private string nombre;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    private string url;

    public string Url
    {
        get { return url; }
        set { url = value; }
    }

    private string padre;

    public string Padre
    {
        get { return padre; }
        set { padre = value; }
    }

    private int tipo;
    public int Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }
    private string imagen;
    public string Imagen
    {
        get { return imagen; }
        set { imagen = value; }
    }
    private bool causaValidacion;
    public bool CausaValidacion
    {
        get { return causaValidacion; }
        set { causaValidacion = value; }
    }

    private string clientScript;

    public string ClientScript
    {
        get { return clientScript; }
        set { clientScript = value; }
    }

    private bool? _ServerCode = true;

    public bool? ServerCode
    {
        get { return _ServerCode; }
        set { _ServerCode = value; }
    }
    
}
