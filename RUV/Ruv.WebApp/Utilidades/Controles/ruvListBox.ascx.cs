using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class Utilidades_Controles_dpsListBox : System.Web.UI.UserControl
{
    
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataSourceID
    {
        get { return lbx.DataSourceID; }
        set { lbx.DataSourceID = value; }
    }


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string Skin
    {
        get { return lbx.SkinID; }
        set { lbx.SkinID = value; }
    }

    [DefaultValue(true)]
    public bool Enabled
    {
        get { return lbx.Enabled; }
        set { lbx.Enabled = value; }
    }



    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public ListSelectionMode SelectionMode
    {
        get { return lbx.SelectionMode; }
        set { lbx.SelectionMode = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataValueField
    {
        get { return lbx.DataValueField; }
        set { lbx.DataValueField = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextFormatString
    {
        get { return lbx.DataTextFormatString; }
        set { lbx.DataTextFormatString = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextField
    {
        get { return lbx.DataTextField; }
        set { lbx.DataTextField = value; }
    }

    [DefaultValue("")]
    public object DataSource
    {
        get { return lbx.DataSource; }
        set
        {
            lbx.DataSource = value;
        }
    }
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string SelectedValue
    {
        get { return lbx.SelectedValue; }
        set { lbx.SelectedValue = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public ListItem SelectedItem
    {
        get { return lbx.SelectedItem; }
    }

    [Bindable(true)]
    [DefaultValue(0)]
    [Localizable(true)]
    public List<int> Seleccionados
    {
        get 
        {
            List<int> sele = new List<int>();
            foreach (ListItem item in lbx.Items)
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
            foreach (int item in value)
            {
                foreach (ListItem it in lbx.Items)
                {
                    if (Convert.ToInt32(it.Value) == item)
                    {
                        it.Selected = true;
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
        get { return lbx.AutoPostBack; }
        set { lbx.AutoPostBack = value; }
    }

    [Bindable(true)]
    [DefaultValue(100)]
    [Localizable(true)]
    public Unit Width
    {
        get { return lbx.Width; }
        set { lbx.Width = value; }
    }

    [Bindable(true)]
    [DefaultValue(100)]
    [Localizable(true)]
    public Unit Height
    {
        get { return lbx.Height; }
        set { lbx.Height = value; }
    }

    [Bindable(true)]
    [DefaultValue(0)]
    [Localizable(true)]
    public int SelectedIndex
    {
        get { return lbx.SelectedIndex; }
        set { lbx.SelectedIndex = value; }
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
        get { return lbx.Items; }
    }

    [DefaultValue("")]
    [Bindable(true)]
    [Localizable(true)]
    public string Valor { get; set; }

    [Bindable(true)]
    [Localizable(true)]
    public Poblar Source
    {
        set
        {
            InsertarDatos(value, Valor);
        }
    }

    private void InsertarDatos(Poblar value, string valor)
    {
        object obj = (object)lbx;
        DataSourceGeneral.PoblarControl(ref obj, value, valor);
    }

}