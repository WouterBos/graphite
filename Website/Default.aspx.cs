using Graphite.Internal;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DemoMenu oDemoMenu = new Graphite.Internal.DemoMenu();
        DemosList.Text = oDemoMenu.GetHTML(true);
    }
}
