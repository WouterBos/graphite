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

    private string[] _demos;
    public string[] demos
    {
        get
        {
            return _demos;
        }
        set
        {
            _demos = value;
        }
    }
    
    private bool[,] _defaultCode;
    public bool[,] defaultCode
    {
        get
        {
            return _defaultCode;
        }
        set
        {
            _defaultCode = value;
        }
    }

    private string[] _demosClasses;
    public string[] demosClasses
    {
        get
        {
            return _demosClasses;
        }
        set
        {
            _demosClasses = value;
        }
    }

    private Dictionary<string, string> _supportedBrowsers;
    public Dictionary<string, string> supportedBrowsers
    {
        get
        {
            return _supportedBrowsers;
        }
        set
        {
            _supportedBrowsers = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CreateDemo();
    }


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



    private string getSourceCode(string suffix, bool defaultCode)
    {
        string fileName = "default";
        if (defaultCode == false)
        {
            int menuItemActive = GetActiveIndex();
            fileName = demos[menuItemActive].ToLower();
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
        string Description = getSourceCode("-description.html", defaultCode[GetActiveIndex(), 2]);

        if (Description == "")
        {
            litDescription.Text = "<p>There's no description available</p>";
        }
        litDescription.Text = Description;
    }



    private void GetDemoCss()
    {
        string CssCode = getSourceCode(".less", defaultCode[GetActiveIndex(), 0]);
        int menuItemActive = GetActiveIndex();

        string strCssLink = demos[menuItemActive].ToLower() + ".less";
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
        string JsCode = getSourceCode("-js.html", defaultCode[GetActiveIndex(), 1]);
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
