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
    }

    private void CreateDemo()
    {
        string strPhysicalPath = Request.ServerVariables["script_name"].Replace("default.aspx", "");
        string xmlPath = "/demos" + strPhysicalPath + "demo";
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
        int intMenuItemActive = 0;
        string strMenuItemName = "";
        
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            strMenuItemName = Request.QueryString["type"].ToString();
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

    private void CreateMenu()
    {
        int intMenuItemActive = GetActiveIndex();
        string[,] types = config.Types();
        if (types.GetUpperBound(0) > 1)
        {
            for (int i = 0; i <= types.GetUpperBound(0); i++)
            {
                HyperLink hlMenuLink = new HyperLink();
                hlMenuLink.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(types[i,1]);
                hlMenuLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?type=" + Server.HtmlEncode(types[i,0]);
                if (i == intMenuItemActive)
                {
                    hlMenuLink.CssClass = "active";
                }
                Types.Controls.Add(hlMenuLink);
            }
        }
    }

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
            string strDescription = GetSourceCode("-description.html", dicFiles["description"]);
            litDescription.Text = strDescription;
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
        string strCodeBehindURL = "";
        int intCodeFileStart = ascxSource.IndexOf("CodeFile=", StringComparison.OrdinalIgnoreCase);
        int intStartQuote = ascxSource.IndexOf("\"", intCodeFileStart);
        int intEndQuote = ascxSource.IndexOf("\"", (intStartQuote + 1));
        if (intStartQuote >= 0)
        {
            strCodeBehindURL =  ascxSource.Substring((intStartQuote + 1), (intEndQuote - intStartQuote - 1));
        }

        string strRoot = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";

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
        return strCode;
    }

    private string WrapInJsString(string str) {
        string strReturn = str;
        strReturn = strReturn.Replace("'", "\\'");
        strReturn = strReturn.Replace("\n", "\\n");
        strReturn = strReturn.Replace("\r", "\\r");
        return strReturn;
    }
    
    private void GetDemoHTML()
    {
        string strFileName = "default";

        if (dicFiles.ContainsKey("ascx") == true)
        {
            if (dicFiles["ascx"] == false)
            {
                int intMenuItemActive = GetActiveIndex();
                strFileName = config.Type(intMenuItemActive);
            }

            Control ctrlControl = LoadControl(this.Parent.Page.TemplateSourceDirectory + "\\" + strFileName + ".ascx");
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
            string strHtmlCode = GetSourceCode(".ascx", dicFiles["ascx"]);
            string strCodeBehind = GetCodeBehind(strHtmlCode, config.CssClass(GetActiveIndex()));
            litDemoCodeBehind.Text = WrapInJsString(strCodeBehind);
            if (strCodeBehind == "") {
                CodeLinksCodeBehind.Visible = false;
            }

            litDemoAscx.Text = WrapInJsString(strHtmlCode);
            CodeLinksHtml.Visible = false;
        }
        else if (dicFiles.ContainsKey("html") == true)
        {
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

            litDemoHtml.Text = WrapInJsString(strHtmlCode);
            CodeLinksAscx.Visible = false;
            CodeLinksCodeBehind.Visible = false;
        }
        else
        {
            CodeLinksHtml.Visible = false;
            CodeLinksAscx.Visible = false;
            CodeLinksCodeBehind.Visible = false;
        }
    }

    private void GetDemoCss()
    {
        if (dicFiles.ContainsKey("css") == true)
        {
            string CssCode = GetSourceCode(".less", dicFiles["css"]);
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

            litDemoCss.Text = WrapInJsString(CssCode);
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
            string JsCode = GetSourceCode("-js.html", dicFiles["javascript"]);
            string JsCodeCopyString = JsCode;

            JsCodeCopyString = WrapInJsString(JsCodeCopyString);
            JsCodeCopyString = JsCodeCopyString.Replace("<script", "###GP###SCRIPT");
            JsCodeCopyString = JsCodeCopyString.Replace("</script", "###GP###/SCRIPT");

            DemoJavaScriptCodeBlock.Text = JsCode;
            litDemoJavaScript.Text = JsCodeCopyString;
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
            "ipad",
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
