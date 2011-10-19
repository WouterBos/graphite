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


    
    private void GetDemoCss()
    {
        string CssCode = getSourceCode(".less");
        
        int menuItemActive = GetActiveIndex();
        string strCssLink = demos[menuItemActive].ToLower() + ".less";
        CSSLink.Attributes["href"] = strCssLink;
        
        CssCode = CssCode.Replace("'", "\'");
        
        if (CssCode == "") {
            CodeLinksLess.Visible = false;
        }
        //DemoCss.Text = CssCode;
    }
    


    private void GetDemoJavaScript()
    {
        string JsCode = getSourceCode(".js.html");
        //DemoJavaScript.Text = JsCode.Replace("'", "\'");
    }
}
