using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Ruv.WebSite.Common
{
    public static class Controls
    {
        public static Control FindControlRecursively(this Control root, string id)
        {
            if (root.ID == id)
                return root;

            foreach (Control control in root.Controls) {
                Control probe = FindControlRecursively(control, id);
                if (probe != null)
                    return probe;
            }

            return null;
        }

    }

}