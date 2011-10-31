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
    // Demo data
    private string _demoSelector;
    public string demoSelector
    {
        get
        {
            return _demoSelector;
        }
        set
        {
            _demoSelector = value;
        }
    }
    private Graphite.Config config;
    Dictionary<string, Boolean> defaultCode;



    protected void Page_Load(object sender, EventArgs e)
    {
        CreateDemo();
    }

    private void CreateDemo()
    {
        config = new Graphite.Config(_demoSelector);
        defaultCode = config.DefaultCode(GetActiveIndex()); 
        
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
        string[] types = config.Types();
        for (int i = 0; i <= types.GetUpperBound(0); i++)
        {
            HyperLink link = new HyperLink();
            link.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[i]);
            link.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + i;
            if (i == menuItemActive)
            {
                link.CssClass = "active";
            }
            Types.Controls.Add(link);
        }
    }

    private string getSourceCode(string suffix, bool defaultCode)
    {
        string fileName = "default";
        if (defaultCode == false)
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
        string Description = getSourceCode("-description.html", defaultCode["description"]);

        if (Description == "")
        {
            litDescription.Text = "<p>There's no description available</p>";
        }
        litDescription.Text = Description;
    }

    private void GetDemoHTML()
    {
        // Set HTML CSS class
        // string HtmlCode = getSourceCode(".html", defaultCode["html"]); ES_TODO: fix this and remove line below
        string HtmlCode = getSourceCode(".html", true);
        HtmlCode = HtmlCode.Replace("##GP_BLOCK_TYPE##", config.CssClass(GetActiveIndex()));
        DemoHTMLCodeBlock.Text = HtmlCode;
    }

    private void GetDemoCss()
    {
        string CssCode = getSourceCode(".less", defaultCode["css"]);
        int menuItemActive = GetActiveIndex();

        string strCssLink = config.Type(menuItemActive) +".less";
        CSSLink.Attributes["href"] = strCssLink;

        CssCode = CssCode.Replace("'", "\\'");
        CssCode = CssCode.Replace("\n", "\\n");
        CssCode = CssCode.Replace("\r", "\\r");

        if (CssCode == "")
        {
            CodeLinksLess.Visible = false;
        }
        DemoCss.Text = CssCode;
    }

    private void GetDemoJavaScript()
    {
        bool defaultJavaScriptCode = false;
        // ES_TODO: check if dictionary item exists. Also deplay check in other "Get" methods.
        //if (Boolean.TryParse(defaultCode["javascript"] == true)
        //{
        //    defaultJavaScriptCode = defaultCode["javascript"];
        //}
        string JsCode = getSourceCode("-js.html", defaultJavaScriptCode);
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
