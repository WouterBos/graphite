using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI.WebControls;

namespace Graphite
{
    /// <summary>
    /// Tools to use in Sitefinity
    /// </summary>
    public class Sitefinity
    {
        /// <summary>
        /// Toggles X-UA metatag if website is in edit mode
        /// </summary>
        public void ForceIE7InEditMode(Literal lit)
        {
            if (HttpContext.Current.Request.QueryString["cmspagemode"] == "edit")
            {
                lit.Text = "<meta http-equiv='X-UA-Compatible' content='IE=7' />";
            }
        }
    }
}