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
    string[] demos = new string[]
    {
        "Horizontal",
        "Vertical",
        "Collapse"
    };
    string[] demosClasses = new string[]
    {
        "gp_horizontal",
        "gp_vertical",
        "gp_horizontal gp_collapse"
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
        GetDemoHTML();
        GetDemoCss();
        GetDemoJavaScript();
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
        DemoHTMLCodeBlock.CssType = demosClasses[0];
        if (String.IsNullOrEmpty(Request.QueryString["type"]) == false)
        {
            Regex regxNum = new Regex(@"^\d+$");
            bool isNumeric = regxNum.Match(Request.QueryString["type"].ToString()).Success;
            
            if (isNumeric == true)
            {
                int type = Convert.ToInt32(Request.QueryString["type"]);
                DemoHTMLCodeBlock.CssType = demosClasses[type];
            }
        }
        DemoHTML.CssType = DemoHTMLCodeBlock.CssType;
    }


    
    private void GetDemoCss()
    {
        int menuItemActive = GetActiveIndex();
        string strCssLink = demos[menuItemActive].ToLower() + ".less";
        CSSLink.Attributes["href"] = strCssLink;
        aCssPlainLink.Attributes["href"] = strCssLink;
        
        string root = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        string less = demos[menuItemActive].ToLower();
        less = less.Replace("/", "\\");
        StringBuilder sbDemoCss = new StringBuilder();

        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(root + less + ".less");
            string line;
            sbDemoCss.AppendLine("<pre class='brush: css'>");

            while(sr.Peek() != -1)
            {
               line = sr.ReadLine();
               sbDemoCss.AppendLine(line);
            }
            sbDemoCss.AppendLine("</pre>");
        }
        catch(Exception exp) {
            sbDemoCss.AppendLine("<p>No CSS used</p>");
            aCssPlainLink.Visible = false;
        }

        DemoCss.Text = sbDemoCss.ToString();
    }
    


    private void GetDemoJavaScript()
    {
        int menuItemActive = GetActiveIndex();
        string root = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        string javaScript = demos[menuItemActive].ToLower();
        javaScript = javaScript.Replace("/", "\\");
        StringBuilder sbDemoJavaScript = new StringBuilder();

        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(root + javaScript + ".js");
            string line;
            sbDemoJavaScript.AppendLine("<pre class='brush: js'>");

            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                sbDemoJavaScript.AppendLine(line);
            }
            sbDemoJavaScript.AppendLine("</pre>");
        }
        catch (Exception exp)
        {
            sbDemoJavaScript.AppendLine("<p>No JavaScript used</p>");
            aCssPlainLink.Visible = false;
        }

        DemoJavaScript.Text = sbDemoJavaScript.ToString();
        
        DemoJavaScriptCodeBlock.Text = sbDemoJavaScript.ToString();
    }
}
