using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

public partial class ClavesUs : System.Web.UI.Page
{
    public string ClaveParaEsto { get; set; }
    public class Test
    {
        private int id;

        public int Id
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Cryptography.Cryptography.Encrypt oEncrypt = new Cryptography.Cryptography.Encrypt();
        TextBox2.Text = oEncrypt.DecryptData(TextBox1.Text);
    }

    protected void Guardar_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(txtValoracionId.Text)){
            int valoracionId = Convert.ToInt32(txtValoracionId.Text);
            ValoracionService objValoracion = new ValoracionService();
            clsValoracion valo = new clsValoracion();
            valo = objValoracion.ValoracionPorId(valoracionId, false);
            objValoracion.DeshacerAsignacion(valo);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ClaveParaEsto = "+CPVC123456+";
        if (txtClave.Text == ClaveParaEsto)
        {
            pnl.Visible = true;
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}