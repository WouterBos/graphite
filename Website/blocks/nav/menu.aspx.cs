using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blocks_nav_menu : System.Web.UI.Page
{
    // Demo data
    string[] demos = new string[]
    {
        "horizontal",
        "vertical"
    };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set page title
        PageTitle.InnerHtml = this.Title;
        
        CreateDemo();
    }

    private void CreateDemo()
    {
        CreateMenu();
    }
    
    private void CreateMenu()
    {
        // Create menu
        for (int i = 0; i <= demos.GetUpperBound(0); i++)
        {
            HyperLink link = new HyperLink();
            link.Text = demos[i];
            link.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + i;
            Types.Controls.Add(link);
        }
    }
}
