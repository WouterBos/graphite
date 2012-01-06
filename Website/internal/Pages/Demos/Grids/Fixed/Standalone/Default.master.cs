using Graphite;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class App_Master_Default : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPageBody.Attributes["class"] += Graphite.Css.UserAgentClasses();
    }
}
