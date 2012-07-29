using Graphite;
using Graphite.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI.WebControls;


public partial class GraphiteInternal_Folders : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DemoMenu oDemoMenu = new Graphite.Internal.DemoMenu();
        DemosList.Text = oDemoMenu.GetHTML(false);
    }
}
