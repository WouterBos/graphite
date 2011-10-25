using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blocks_nav_menu : System.Web.UI.Page
{
    // Demo data

    // ###ES_TODO: How can I put this in an interface of some sorts? I'd like to tie "demos", "demosClasses" and "supportedBrowsers" to each other
    string[] demos = new string[]
    {
        "Horizontal",
        "Vertical",
        "Collapse"
    };
    bool[,] defaultCode = new bool[,]
    {
        {false, true, false},
        {false, true, false},
        {false, false, false}
    };
    string[] demosClasses = new string[]
    {
        "gp_menu_typeHorizontal",
        "gp_menu_typeVertical",
        "gp_menu_typeHorizontal gp_menu_typeCollapse"
    };
    Dictionary<string, string> supportedBrowsers = new Dictionary<string, string>();
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set page title
        PageTitle.InnerHtml = this.Title;
        
        BlockDemo.demos = demos;
        BlockDemo.defaultCode = defaultCode;
        BlockDemo.demosClasses = demosClasses;
        BlockDemo.supportedBrowsers = supportedBrowsers;
    }
}
