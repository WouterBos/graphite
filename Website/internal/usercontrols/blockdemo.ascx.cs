using Graphite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Reflection;

public partial class internal_usercontrols_blockdemo : System.Web.UI.UserControl
{
    private Graphite.Config config;
    Dictionary<string, Boolean> dicFiles;



    protected void Page_Load(object sender, EventArgs e)
    {
        CreateDemo();

        //Control Test = LoadControl("~\\internal\\usercontrols\\test.ascx");
        //TestPanel.Controls.Add(Test);
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
        string[,] types = config.Types();
        if (types.GetUpperBound(0) > 1)
        {
            for (int i = 0; i <= types.GetUpperBound(0); i++)
            {
                HyperLink link = new HyperLink();
                link.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[i,1]);
                link.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + Server.HtmlEncode(types[i,0]);
                if (i == menuItemActive)
                {
                    link.CssClass = "active";
                }
                Types.Controls.Add(link);
            }
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
    
    private string GetCodeBehind(string ascxSource, string cssClass)
    {
        string strCode = "";
        
        // Get URL to Codebehind
        string codeBehindURL = "";
        int CodeFileStart = ascxSource.IndexOf("CodeFile=", StringComparison.OrdinalIgnoreCase);
        int StartQuote = ascxSource.IndexOf("\"", CodeFileStart);
        int EndQuote = ascxSource.IndexOf("\"", (StartQuote + 1));
        if (StartQuote >= 0)
        {
            codeBehindURL =  ascxSource.Substring((StartQuote + 1), (EndQuote - StartQuote - 1));
        }

        string root = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";

        // Get Codebehind code
        try
        {
            StringBuilder sbCode = new StringBuilder();
            System.IO.StreamReader sr = new System.IO.StreamReader(root + codeBehindURL);

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                sbCode.AppendLine(line);
            }
            strCode = sbCode.ToString();
            
            int rootClassStart = strCode.IndexOf("_strRootClass", StringComparison.OrdinalIgnoreCase);
            int rootClassValueStart = strCode.IndexOf("\"\"", rootClassStart);
            Response.Write(strCode.Substring(rootClassValueStart, 2));
        }
        catch (Exception exp)
        {
            // No Codebehind available
        }
        return strCode;
    }

    private string WrapInJsString(string str) {
        string returnStr = str;
        returnStr = returnStr.Replace("'", "\\'");
        returnStr = returnStr.Replace("\n", "\\n");
        returnStr = returnStr.Replace("\r", "\\r");
        return returnStr;
    }
    
    private void GetDemoHTML()
    {
        string fileName = "default";

        if (dicFiles.ContainsKey("ascx") == true)
        {
            if (dicFiles["ascx"] == false)
            {
                int menuItemActive = GetActiveIndex();
                fileName = config.Type(menuItemActive);
            }

            Control ctrlControl = LoadControl(this.Parent.Page.TemplateSourceDirectory + "\\" + fileName + ".ascx");
            PropertyInfo[] info = ctrlControl.GetType().GetProperties();
            foreach (PropertyInfo item in info)
            {
                if (item.CanWrite)
                {
                     switch (item.Name)
                     {
                         case "strRootClass":
                             item.SetValue(ctrlControl, config.CssClass(GetActiveIndex()), null); 
                         break;
                     }
                }
            }
            pnlDemoHTMLCodeBlock.Controls.Add(ctrlControl);
            string HtmlCode = getSourceCode(".ascx", dicFiles["ascx"]);
            string codeBehind = GetCodeBehind(HtmlCode, config.CssClass(GetActiveIndex()));
            DemoCodeBehind.Text = WrapInJsString(codeBehind);

            DemoAscx.Text = WrapInJsString(HtmlCode);
            CodeLinksHtml.Visible = false;
        }
        else if (dicFiles.ContainsKey("html") == true)
        {
            string HtmlCode = getSourceCode(".html", dicFiles["html"]);
            HtmlCode = HtmlCode.Replace("###GP_BLOCK_TYPE###", config.CssClass(GetActiveIndex())); // Set HTML CSS class
            Literal litControl = new Literal();
            litControl.Text = HtmlCode;
            pnlDemoHTMLCodeBlock.Controls.Add(litControl);
            
            // Get demo code for copy/paste
            if (dicFiles["externalDemo"] == true)
            {
                HtmlCode = getSourceCode("-external.html", dicFiles["html"]);
            }

            DemoHtml.Text = WrapInJsString(HtmlCode);
            CodeLinksAscx.Visible = false;
        }
        else
        {
            CodeLinksHtml.Visible = false;
            CodeLinksAscx.Visible = false;
        }
    }

    private void GetDemoCss()
    {
        if (dicFiles.ContainsKey("css") == true)
        {
            string CssCode = getSourceCode(".less", dicFiles["css"]);
            if (dicFiles["externalDemo"] == false)
            {
                string strCssLink;
                if (dicFiles["css"] == true)
                {
                    strCssLink = "default.less";
                }
                else
                {
                    strCssLink = config.Type(GetActiveIndex()) + ".less";
                }
                
                CSSLink.Attributes["href"] = strCssLink;
            }

            DemoCss.Text = WrapInJsString(CssCode);
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
            string JsCodeCopyString = JsCode;

            JsCodeCopyString = WrapInJsString(JsCodeCopyString);
            JsCodeCopyString = JsCodeCopyString.Replace("<script", "###GP###SCRIPT");
            JsCodeCopyString = JsCodeCopyString.Replace("</script", "###GP###/SCRIPT");

            DemoJavaScriptCodeBlock.Text = JsCode;
            DemoJavaScript.Text = JsCodeCopyString;
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
