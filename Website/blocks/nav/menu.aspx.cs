using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

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
        
        CreateDemo();
    }



    // ###ES_TODO: All this logic must be tucheck away in a single ASCX
    private void CreateDemo()
    {
        CreateMenu();
        GetDemoHTML();
        GetDemoCss();
        GetDemoJavaScript();
        GetDemoDescription();
        
        supportedBrowsers.Add("IE", "7+");
        supportedBrowsers.Add("Firefox", "3.5");
        CreateSupportedBrowsersList(supportedBrowsers);
    }


    
    private int GetActiveIndex()
    {
        int menuItemActive = 0;
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            Regex regxNum = new Regex(@"^\d+$");
            if (regxNum.Match(Request.QueryString["type"].ToString()).Success)
            {
                menuItemActive = Convert.ToInt32(Request.QueryString["type"]);
            }
        }
        return menuItemActive;
    }
    
    
    
    private void CreateMenu()
    {
        int menuItemActive = GetActiveIndex();
        for (int i = 0; i <= demos.GetUpperBound(0); i++)
        {
            HyperLink link = new HyperLink();
            link.Text = demos[i];
            link.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + i;
            if (i == menuItemActive)
            {
                link.CssClass = "active";
            }
            Types.Controls.Add(link);
        }
    }
    
    
    
    private void GetDemoHTML()
    {
        // Set HTML CSS class
        DemoHTMLCodeBlock.CssType = demosClasses[GetActiveIndex()];
    }



    private string getSourceCode(string suffix)
    {
        int menuItemActive = GetActiveIndex();
        string root = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        string less = demos[menuItemActive].ToLower();
        less = less.Replace("/", "\\");
        StringBuilder sbCode = new StringBuilder();

        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(root + less + suffix);
            string line;

            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                sbCode.AppendLine(line);
            }
        }
        catch (Exception exp)
        {
            //
        }
        return sbCode.ToString();
    }



    private void GetDemoDescription()
    {
        string Description = getSourceCode("-description.html");

        if (Description == "")
        {
            litDescription.Text = "<p>There's no description available</p>";
        }
        litDescription.Text = Description;
    }


    
    private void GetDemoCss()
    {
        string CssCode = getSourceCode(".less");
        int menuItemActive = GetActiveIndex();

        string strCssLink = demos[menuItemActive].ToLower() + ".less";
        CSSLink.Attributes["href"] = strCssLink;
        
        CssCode = CssCode.Replace("'", "\\'");
        CssCode = CssCode.Replace("\n", "\\n");
        CssCode = CssCode.Replace("\r", "\\r");
        
        if (CssCode == "") {
            CodeLinksLess.Visible = false;
        }
        DemoCss.Text = CssCode;
    }
    


    private void GetDemoJavaScript()
    {
        string JsCode = getSourceCode("-js.html");
        int menuItemActive = GetActiveIndex();

        JsCode = JsCode.Replace("'", "\\'");
        JsCode = JsCode.Replace("<script", "###GRAPHITE###SCRIPT");
        JsCode = JsCode.Replace("</script", "###GRAPHITE###/SCRIPT");
        JsCode = JsCode.Replace("\n", "\\n");
        JsCode = JsCode.Replace("\r", "\\r");

        if (JsCode == "")
        {
            CodeLinksJs.Visible = false;
        }
        DemoJavaScript.Text = JsCode;
    }



    private void CreateSupportedBrowsersList(IDictionary<string, string> supportedBrowsers)
    {
        string[] allBrowser = new string[] {
            "IE",
            "Firefox",
            "Chrome",
            "Safari",
            "Opera"
        };
        StringBuilder sbBrowserList = new StringBuilder();
        
        sbBrowserList.AppendLine("<ul class='graphite_browser'>");
        for (int i = 0; i <= allBrowser.GetUpperBound(0); i++)
        {
            string browserVersion = "";
            string unsupported = " class='graphite_browserUnsupported'";
            try
            {
                browserVersion = supportedBrowsers[allBrowser[i].ToString()];
                unsupported = "";
            }
            catch (Exception exp)
            {
            
            }
            sbBrowserList.AppendLine("  <li" + unsupported + ">");
            sbBrowserList.AppendLine("      <strong class='graphite_browserIcon graphite_browser" + allBrowser[i] + "'>" + allBrowser[i] + "</strong>");
            sbBrowserList.AppendLine("      <span class='graphite_browserVersion'>" + browserVersion + "</span>");
            sbBrowserList.AppendLine("  </li>");
        }
        sbBrowserList.AppendLine("</ul>");
        
        BrowserList.Text = sbBrowserList.ToString();
    }
}
