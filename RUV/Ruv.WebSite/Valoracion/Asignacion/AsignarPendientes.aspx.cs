using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

public partial class Valoracion_Asignacion_AsignarPendientes : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Cargar();
    }
    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        Application[ConstantesAplicacion.ASIGNAR] = true;
        Asignar objAsignar = new Asignar();
        objAsignar.UsuarioId = Varios.UsuarioId();
        objAsignar.Context = HttpContext.Current;
        ThreadStart inicio = new ThreadStart(objAsignar.AsignarDeclaraciones);
        Thread hilo = new Thread(inicio);
        hilo.Start();
        Cargar();
    }

    private void Cargar(){
        if (Application[ConstantesAplicacion.ASIGNAR] != null)
        {
            lblMensajeAsignar.Text = "Se estan asignando las declaraciones...";
            imgCargar.Visible = true;
            btnAsignar.Visible = false;
        }
        else
        {
            lblMensajeAsignar.Text = "Asignar todas las declaraciones pendientes a valoración";
            imgCargar.Visible = false;
            btnAsignar.Visible = true;
        }

    }
}