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
using System.Collections;

public partial class GraphiteInternal_BlockDemo : System.Web.UI.UserControl
{
    private Graphite.Internal.Config config;
    Dictionary<string, Boolean> dicFiles;
    private string strMenuItemName = "";



    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            strMenuItemName = Request.QueryString["type"];
        }
        CreateDemo();
    }

    private void CreateDemo()
    {
        // Getting configuration settings from XML
        config = new Graphite.Internal.Config(Graphite.Tools.GetXmlPath("demo"));
        dicFiles = config.Files(GetActiveIndex()); 
        
        // Run methods
        CreateMenu();
        GetDemoHTML();
        GetDemoCss();
        GetDemoJavaScript();
        GetDemoDescription();
        CreateSupportedBrowsersList();
        SetStageDimension();
    }

    private void SetStageDimension()
    {
        Dictionary<string, string> stageDimension = config.GetStageDimension(GetActiveIndex());
        pnlDemoHTMLCodeBlock.Attributes["style"] += "; width: " + stageDimension["width"];
        pnlDemoHTMLCodeBlock.Attributes["style"] += "; height: " + stageDimension["height"];
    }

    // Finds out which tab in the demo menu is selected. If no menu is visible above the demo, the index is always 0.
    private int GetActiveIndex()
    {
        int intMenuItemActive = 0;
        
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            intMenuItemActive = config.Index(strMenuItemName.ToLower());
        }
        
        if (intMenuItemActive == -1)
        {
            intMenuItemActive = 0;
            if (strMenuItemName.Length > 0)
            {
            litMessage.Text = @"<div class='gp_text'><span class='gp_textAlert'>" +
                @"Cannot find a type called  &ldquo;" + strMenuItemName + @"&rdquo;, showing default type instead." + 
                @"</span></div>";
            }
        }
        else
        {
            litMessage.Text = "";
        }
        
        return intMenuItemActive;
    }

    // Create tabs above demo.
    private void CreateMenu()
    {
        int intMenuItemActive = GetActiveIndex();
        ArrayList types = config.Types();
        if (types.Count > 0)
        {
            for (int i = 0; i < types.Count; i++)
            {
                HyperLink hlMenuLink = new HyperLink();
                ArrayList type = types[i] as ArrayList;
                hlMenuLink.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type[1].ToString());
                hlMenuLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + Server.HtmlEncode(type[0].ToString());
                if (strMenuItemName.ToLower() == type[0].ToString().ToLower())
                {
                    hlMenuLink.CssClass = "active";
                }
                Types.Controls.Add(hlMenuLink);
            }
        }
    }
    
    // Returns source code of a demo file
    private string GetSourceCode(string suffix, bool dicFiles)
    {
        string strFileName = "default";
        if (dicFiles == false)
        {
            int intMenuItemActive = GetActiveIndex();
            strFileName = config.Type(intMenuItemActive);
        }

        string strRoot = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        StringBuilder sbCode = new StringBuilder();

        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(strRoot + strFileName + suffix);
            string line;

            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                sbCode.AppendLine(line);
            }

            sr.Close();
            sr.Dispose();
        }
        catch (Exception exp)
        {
            //
        }
        return sbCode.ToString();
    }
    
    // Gets description from file and prints it below demo block.
    private void GetDemoDescription()
    {
        if (dicFiles.ContainsKey("description") == true)
        {
            string strDescription = GetSourceCode("-description.html", dicFiles["description"]);
            litDescription.Text = strDescription;
        }
        else
        {
            litDescription.Text = "<p>No description available.</p>";
        }
    }
    
    // Gets codebehind of ASCX if any available.
    private string GetCodeBehind(string ascxSource, string cssClass)
    {
        string strCode = "";
        
        // Get URL to Codebehind
        string strCodeBehindURL = "";
        int intCodeFileStart = ascxSource.IndexOf("CodeFile=", StringComparison.OrdinalIgnoreCase);
        int intIgnoreCodeFile = ascxSource.IndexOf("<!-- Graphite: Ignore Codefile -->", StringComparison.OrdinalIgnoreCase);
        int intStartQuote = ascxSource.IndexOf("\"", intCodeFileStart);
        int intEndQuote = ascxSource.IndexOf("\"", (intStartQuote + 1));
        if (intStartQuote >= 0)
        {
            strCodeBehindURL =  ascxSource.Substring((intStartQuote + 1), (intEndQuote - intStartQuote - 1));
        }

        string strRoot = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        
        if (intIgnoreCodeFile >= 0)
        {
            // Get Codebehind code
            try
            {
                StringBuilder sbCode = new StringBuilder();
                System.IO.StreamReader sr = new System.IO.StreamReader(strRoot + strCodeBehindURL);

                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    sbCode.AppendLine(line);
                }
                sr.Close();
                sr.Dispose();
                
                strCode = sbCode.ToString();

                // Insert class into private variable _strRootClass
                int rootClassStart = strCode.IndexOf("_strRootClass", StringComparison.OrdinalIgnoreCase);
                int rootClassValueStart = strCode.IndexOf("\"\"", rootClassStart);
                strCode = strCode.Insert(rootClassValueStart + 1, config.CssClass(GetActiveIndex()));
            }
            catch (Exception exp)
            {
                // No Codebehind available
            }
        }
        return strCode;
    }
    
    // Change string in order to store it as a string in JavaScript
    private string WrapInJsString(string str) {
        string strReturn = str;
        strReturn = strReturn.Replace("'", "\\'");
        strReturn = strReturn.Replace("\n", "\\n");
        strReturn = strReturn.Replace("\r", "\\r");
        return strReturn;
    }
    
    // Gets demo HTML which is either an HTML or an ASCX file (depends on config file).
    private void GetDemoHTML()
    {
        string strFileName = "default";

        if (dicFiles.ContainsKey("ascx") == true)
        {
            // File is ASCX
            if (dicFiles["ascx"] == false)
            {
                int intMenuItemActive = GetActiveIndex();
                strFileName = config.Type(intMenuItemActive);
            }
            
            // Load control
            Control ctrlControl = LoadControl(this.Parent.Page.TemplateSourceDirectory + "\\" + strFileName + ".ascx");
            PropertyInfo[] info = ctrlControl.GetType().GetProperties();
            
            // Set strRootClass property if available
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
            
            // Add ASCX control
            pnlDemoHTMLCodeBlock.Controls.Add(ctrlControl);
            string strHtmlCode = GetSourceCode(".ascx", dicFiles["ascx"]);
            litDemoAscx.Text = WrapInJsString(strHtmlCode);
            
            // Find codebehind
            string strCodeBehind = GetCodeBehind(strHtmlCode, config.CssClass(GetActiveIndex()));
            litDemoCodeBehind.Text = WrapInJsString(strCodeBehind);
            if (strCodeBehind == "") {
                phCodeLinksCodeBehind.Visible = false;
            }

            // Hide HTML placeholder
            phCodeLinksHtml.Visible = false;
        }
        else if (dicFiles.ContainsKey("html") == true)
        {
            // File is HTML
            string strHtmlCode = GetSourceCode(".html", dicFiles["html"]);
            strHtmlCode = strHtmlCode.Replace("###GP_BLOCK_TYPE###", config.CssClass(GetActiveIndex())); // Set HTML CSS class
            Literal litControl = new Literal();
            litControl.Text = strHtmlCode;
            pnlDemoHTMLCodeBlock.Controls.Add(litControl);
            
            // Get demo code for copy/paste
            if (dicFiles["externalDemo"] == true)
            {
                strHtmlCode = GetSourceCode("-external.html", dicFiles["html"]);
            }
            
            // Add HTML to demo block
            litDemoHtml.Text = WrapInJsString(strHtmlCode);
            
            // Hide ASCX and Codebehind block
            phCodeLinksAscx.Visible = false;
            phCodeLinksCodeBehind.Visible = false;
        }
        else
        {
            // There's no HTML. Hide all HTML-related blocks
            phCodeLinksHtml.Visible = false;
            phCodeLinksAscx.Visible = false;
            phCodeLinksCodeBehind.Visible = false;
        }
    }

    // Gets CSS code and prints it in demo if available
    private void GetDemoCss()
    {
        if (dicFiles.ContainsKey("css") == true)
        {
            string strCssCode = GetSourceCode(".less", dicFiles["css"]);
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
            string strStartBaseComment = "/* Start base */";
            string strEndBaseComment = "/* End base */";
            
            int intStartBase = strCssCode.IndexOf(strStartBaseComment);
            int intEndbase = strCssCode.IndexOf(strEndBaseComment);
            if (intStartBase >= 0 && intEndbase >= 0)
            {
                strCssCode = strCssCode.Substring(intEndbase + strStartBaseComment.Length);
            }

            litDemoCss.Text = WrapInJsString(strCssCode);
        }
        else
        {
            phCodeLinksLess.Visible = false;
        }
    }

    // Gets JavaScript code and prints it in demo if available
    private void GetDemoJavaScript()
    {
        if (dicFiles.ContainsKey("javascript") == true)
        {
            string strJsCode = GetSourceCode("-js.html", dicFiles["javascript"]);
            string strJsCodeCopyString = strJsCode;

            strJsCodeCopyString = WrapInJsString(strJsCodeCopyString);
            strJsCodeCopyString = strJsCodeCopyString.Replace("<script", "###GP###SCRIPT");
            strJsCodeCopyString = strJsCodeCopyString.Replace("</script", "###GP###/SCRIPT");

            DemoJavaScriptCodeBlock.Text = strJsCode;
            litDemoJavaScript.Text = strJsCodeCopyString;
        }
        else
        {
            phCodeLinksJs.Visible = false;
        }
    }

    // Creates list of browsers the block supports
    private void CreateSupportedBrowsersList()
    {
        Dictionary<string, string> dicSupportedBrowsers = config.SupportedBrowsers(GetActiveIndex()); 
        string[] strAllBrowsers = new string[] {
            "msie",
            "firefox",
            "chrome",
            "safari",
            "opera",
            "ipad",
            "iphone",
            "android",
            "windowsphone",
            "blackberry"
        };
        StringBuilder sbBrowserList = new StringBuilder();

        sbBrowserList.AppendLine("<ul class='graphite_browser'>");
        for (int i = 0; i <= strAllBrowsers.GetUpperBound(0); i++)
        {
            string strBrowserVersion = "";
            string strUnsupported = " class='graphite_browserUnsupported'";
            
            // ES_TODO: Remove this ugly "try"
            try
            {
                strBrowserVersion = dicSupportedBrowsers[strAllBrowsers[i].ToString()];
                strUnsupported = "";
            }
            catch (Exception exp)
            {

            }
            string browserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strAllBrowsers[i]);
            sbBrowserList.AppendLine("  <li" + strUnsupported + ">");
            sbBrowserList.AppendLine("      <strong class='graphite_browserIcon graphite_browser" + browserName + "'>" + browserName + "</strong>");
            sbBrowserList.AppendLine("      <span class='graphite_browserVersion'>" + strBrowserVersion + "</span>");
            sbBrowserList.AppendLine("  </li>");
        }
        sbBrowserList.AppendLine("</ul>");

        litBrowserList.Text = sbBrowserList.ToString();
    }
}
