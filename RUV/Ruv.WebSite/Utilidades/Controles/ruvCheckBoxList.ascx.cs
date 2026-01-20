using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_dpsCheckBoxList : System.Web.UI.UserControl
{
    public event SelectIndexChanged SelectIndexChange;

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataSourceID
    {
        get { return chkL.DataSourceID; }
        set { chkL.DataSourceID = value; }
    }


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string Skin
    {
        get { return chkL.SkinID; }
        set { chkL.SkinID = value; }
    }

    

    [Bindable(true)]
    [DefaultValue("chkL")]
    [Localizable(true)]
    public string IdLista
    {
        get { return chkL.ID; }
        set
        {
            chkL.ID = value;
        }
    }

    [DefaultValue(true)]
    public bool Enabled
    {
        get { return chkL.Enabled; }
        set { chkL.Enabled = value; }
    }

    [DefaultValue(1)]
    public int RepeatColumns
    {
        get { return chkL.RepeatColumns; }
        set { chkL.RepeatColumns = value; }
    }


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataValueField
    {
        get { return chkL.DataValueField; }
        set { chkL.DataValueField = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextFormatString
    {
        get { return chkL.DataTextFormatString; }
        set { chkL.DataTextFormatString = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextField
    {
        get { return chkL.DataTextField; }
        set { chkL.DataTextField = value; }
    }

    [DefaultValue("")]
    public object DataSource
    {
        get { return chkL.DataSource; }
        set
        {
            chkL.DataSource = value;
        }
    }
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string SelectedValue
    {
        get { return chkL.SelectedValue; }
        set { chkL.SelectedValue = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public ListItem SelectedItem
    {
        get { return chkL.SelectedItem; }
    }

    [Bindable(true)]
    //[DefaultValue(RepeatLayout.Table)]
    [Localizable(true)]
    public RepeatLayout RepeatLayout {
        get { return chkL.RepeatLayout; }
        set { chkL.RepeatLayout = value; }
    }

    [Bindable(true)]
    [DefaultValue(0)]
    [Localizable(true)]
    public List<int> Seleccionados
    {
        get 
        {
            List<int> sele = new List<int>();
            foreach (ListItem item in chkL.Items)
            {
                if (item.Selected)
                {
                    sele.Add(Convert.ToInt32(item.Value));
                }
            }
            return sele;
        }
        set
        {
            foreach (ListItem it in chkL.Items)
            {
                it.Selected = false;
                foreach (int item in value)
                {
                    if (Convert.ToInt32(it.Value) == item)
                    {
                        it.Selected = true;
                    }
                }
            }
        }
    }


    public List<int> Ocultar
    {
        set
        {
            foreach (ListItem it in chkL.Items)
            {
                it.Selected = false;
                foreach (int item in value)
                {
                    if (Convert.ToInt32(it.Value) == item)
                    {
                        it.Attributes.CssStyle["visibility"] = "collapse";
                    }
                }
            }
        }
    }

    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool AutoPostBack
    {
        get { return chkL.AutoPostBack; }
        set { chkL.AutoPostBack = value; }
    }

    [Bindable(true)]
    [DefaultValue(100)]
    [Localizable(true)]
    public Unit Width
    {
        get { return chkL.Width; }
        set { chkL.Width = value; }
    }

    [Bindable(true)]
    [DefaultValue(0)]
    [Localizable(true)]
    public int SelectedIndex
    {
        get { return chkL.SelectedIndex; }
        set { chkL.SelectedIndex = value; }
    }


    private string mensajeError;
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string MensajeError
    {
        get { return mensajeError; }
        set { mensajeError = value; }
    }


    [Bindable(true)]
    [Localizable(true)]
    public ListItemCollection Items
    {
        get { return chkL.Items; }
    }

    [DefaultValue("")]
    public string Valor { get; set; }

    public Poblar Source
    {
        set
        {
            InsertarDatos(value);
        }
    }

    private void InsertarDatos(Poblar value)
    {
        object obj = (object)chkL;
        DataSourceGeneral.PoblarControl(ref obj, value, Valor);
    }

    public void LimpiarSelecciones() 
    {
        foreach (ListItem item in chkL.Items)
        {
            item.Selected = false;
        }
    }
    protected void chkL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectIndexChange != null)
        {
            SelectIndexChange(sender, e);
        }
    }
}