using Graphite;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Master : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPageHtml.Attributes["class"] += Graphite.Css.UserAgentClasses();
		MasterPageHtml.Attributes["lang"] = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToString();
    }
}
