using Graphite;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class internal_usercontrols_blockdemo : System.Web.UI.UserControl
{
    private Graphite.Config config;
    Dictionary<string, Boolean> dicFiles;



    protected void Page_Load(object sender, EventArgs e)
    {
        CreateDemo();
    }

    private void CreateDemo()
    {
        string physicalPath = Request.ServerVariables["script_name"].Replace("default.aspx", "");
        string xmlPath = "/demos" + physicalPath + "demo";
        //Response.Write(xmlPath);
        config = new Graphite.Config(xmlPath);
        dicFiles = config.Files(GetActiveIndex()); 
        
        CreateMenu();
        GetDemoHTML();
        GetDemoCss();
        GetDemoJavaScript();
        GetDemoDescription();
        CreateSupportedBrowsersList();
    }

    private int GetActiveIndex()
    {
        int menuItemActive = 0;
        string menuItemName = "";
        
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            menuItemName = Request.QueryString["type"].ToString();
            menuItemActive = config.Index(menuItemName.ToLower());
        }
        
        if (menuItemActive == -1)
        {
            menuItemActive = 0;
            if (menuItemName.Length > 0)
            {
            litMessage.Text = @"<div class='gp_text'><span class='gp_textAlert'>" +
                @"Cannot find a type called  &ldquo;" + menuItemName + @"&rdquo;, showing default type instead." + 
                @"</span></div>";
            }
        }
        else
        {
            litMessage.Text = "";
        }
        
        return menuItemActive;
    }

    private void CreateMenu()
    {
        int menuItemActive = GetActiveIndex();
        string[] types = config.Types();
        for (int i = 0; i <= types.GetUpperBound(0); i++)
        {
            HyperLink link = new HyperLink();
            link.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[i]);
            link.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + Server.HtmlEncode(types[i]);
            if (i == menuItemActive)
            {
                link.CssClass = "active";
            }
            Types.Controls.Add(link);
        }
    }

    private string getSourceCode(string suffix, bool dicFiles)
    {
        string fileName = "default";
        if (dicFiles == false)
        {
            int menuItemActive = GetActiveIndex();
            fileName = config.Type(menuItemActive);
        }

        string root = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        StringBuilder sbCode = new StringBuilder();

        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(root + fileName + suffix);
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
        if (dicFiles.ContainsKey("description") == true)
        {
            string Description = getSourceCode("-description.html", dicFiles["description"]);
            litDescription.Text = Description;
        }
        else
        {
            litDescription.Text = "<p>No description available.</p>";
        }
    }

    private void GetDemoHTML()
    {
        if (dicFiles.ContainsKey("html") == true)
        {
            string HtmlCode = getSourceCode(".html", dicFiles["html"]);
            HtmlCode = HtmlCode.Replace("###GP_BLOCK_TYPE###", config.CssClass(GetActiveIndex())); // Set HTML CSS class
            DemoHTMLCodeBlock.Text = HtmlCode;
        }
        else
        {
            CodeLinksHtml.Visible = false;
        }
    }

    private void GetDemoCss()
    {
        if (dicFiles.ContainsKey("css") == true)
        {
            string CssCode = getSourceCode(".less", dicFiles["css"]);
            string strCssLink = config.Type(GetActiveIndex()) + ".less";
            CSSLink.Attributes["href"] = strCssLink;

            CssCode = CssCode.Replace("'", "\\'");
            CssCode = CssCode.Replace("\n", "\\n");
            CssCode = CssCode.Replace("\r", "\\r");

            DemoCss.Text = CssCode;
        }
        else
        {
            CodeLinksLess.Visible = false;
        }
    }

    private void GetDemoJavaScript()
    {
        if (dicFiles.ContainsKey("javascript") == true)
        {
            string JsCode = getSourceCode("-js.html", dicFiles["javascript"]);

            JsCode = JsCode.Replace("'", "\\'");
            JsCode = JsCode.Replace("<script", "###GRAPHITE###SCRIPT");
            JsCode = JsCode.Replace("</script", "###GRAPHITE###/SCRIPT");
            JsCode = JsCode.Replace("\n", "\\n");
            JsCode = JsCode.Replace("\r", "\\r");

            DemoJavaScript.Text = JsCode;
        }
        else
        {
            CodeLinksJs.Visible = false;
        }
    }

    private void CreateSupportedBrowsersList()
    {
        Dictionary<string, string> supportedBrowsers = config.SupportedBrowsers(GetActiveIndex()); 
        string[] allBrowsers = new string[] {
            "msie",
            "firefox",
            "chrome",
            "safari",
            "opera"
        };
        StringBuilder sbBrowserList = new StringBuilder();

        sbBrowserList.AppendLine("<ul class='graphite_browser'>");
        for (int i = 0; i <= allBrowsers.GetUpperBound(0); i++)
        {
            string browserVersion = "";
            string unsupported = " class='graphite_browserUnsupported'";
            
            // ES_TODO: Remove this ugly "try"
            try
            {
                browserVersion = supportedBrowsers[allBrowsers[i].ToString()];
                unsupported = "";
            }
            catch (Exception exp)
            {

            }
            string browserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(allBrowsers[i]);
            sbBrowserList.AppendLine("  <li" + unsupported + ">");
            sbBrowserList.AppendLine("      <strong class='graphite_browserIcon graphite_browser" + browserName + "'>" + browserName + "</strong>");
            sbBrowserList.AppendLine("      <span class='graphite_browserVersion'>" + browserVersion + "</span>");
            sbBrowserList.AppendLine("  </li>");
        }
        sbBrowserList.AppendLine("</ul>");

        BrowserList.Text = sbBrowserList.ToString();
    }
}
