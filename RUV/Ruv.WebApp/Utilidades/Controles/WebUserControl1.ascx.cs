using Ruv.Infrastructure.Crosscutting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ruv.WebApp.Utilidades.Controles
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public event CambiaValor TextChanged;

        private bool requiereExpresion;
        [Bindable(true)]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool RequiereExpresion
        {
            get { return requiereExpresion; }
            set { requiereExpresion = value; }
        }

        private string _script;
        [DefaultValue("")]
        public string Script
        {
            set { _script = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (requiereExpresion)
            {
                rev_txt.Enabled = requiereExpresion;
            }
            if(!string.IsNullOrEmpty(_script))
                txt.Attributes.Add("onchange", _script);
        }

        protected void txt_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged(sender, e);
            }
        }
    }
}