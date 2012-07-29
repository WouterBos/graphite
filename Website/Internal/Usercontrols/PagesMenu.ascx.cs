using Graphite.Internal;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Internal_Usercontrols_PagesMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PagesMenu oDemoMenu = new Graphite.Internal.PagesMenu();
        litMenuList.Text = oDemoMenu.GetHTML();
    }
}
