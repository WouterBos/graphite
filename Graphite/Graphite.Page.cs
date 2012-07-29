using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Graphite
{
    /// <summary>
    /// Summary description for Graphite
    /// </summary>
    public static class Page
    {
        /// <summary>
        /// Creates class string with browser name and version
        /// </summary>
        public static void AddRootAttributes(HtmlGenericControl htmlControl)
        {
            htmlControl.Attributes["class"] += Graphite.Css.UserAgentClasses();
            htmlControl.Attributes["lang"] = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToString();
        }
    }
}