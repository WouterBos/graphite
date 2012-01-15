using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GraphiteInternal_BreadCrumb : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintBreadCrumb();
    }
    
    private void PrintBreadCrumb()
    {
        string strPhysicalPath = Request.ServerVariables["script_name"].Replace("default.aspx", "");
        char[] charSplitChar = { '/' };
        string[] strTree = strPhysicalPath.Split(charSplitChar);
        strTree = strTree.Reverse().ToArray();
        string strBreadcrumb = "";
        for (int i = 0; i < strTree.Length; i++)
        {
            if (strTree[i] != "" && strTree[i] != "Internal" && strTree[i] != "Pages")
            {
                if (strBreadcrumb == "")
                {
                    string HumanName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strTree[i]);
                    HumanName = HumanName.Replace("-", " ");
                    HumanName = HumanName.Replace(".Aspx", "");
                    strBreadcrumb += "<strong>" + HumanName + "</strong>";
                }
                else
                {
                    strBreadcrumb += " ‹ <a href='" + GetLink(strTree, (strTree.Length - i)) + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strTree[i]) + "</a>";
                }
            }
        }
        if (strBreadcrumb != "")
        {
            strBreadcrumb += " ‹ ";
        }
        strBreadcrumb += "<a href='/'>Homepage</a>";
        litBreadcrumb.Text = strBreadcrumb;
    }
    
    private string GetLink(string[] arg_tree, int intIndex)
    {
        string[] strTree = arg_tree.Reverse().ToArray();
        string strLink = "";
        for (int i = 0; i < intIndex; i++)
        {
            if (strTree[i] == "")
            {
                strLink += "/";
            }
            else
            {
                strLink += strTree[i] + "/";
            }
        }
        return strLink;
    }
}
