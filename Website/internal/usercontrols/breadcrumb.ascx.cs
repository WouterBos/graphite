using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class internal_usercontrols_breadcrumb : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        printBreadCrumb();
    }
    
    private void printBreadCrumb()
    {
        string physicalPath = Request.ServerVariables["script_name"].Replace("default.aspx", "");
        char[] splitChar = { '/' };
        string[] tree = physicalPath.Split(splitChar);
        tree = tree.Reverse().ToArray();
        string breadcrumb = "";
        for (int i = 0; i < tree.Length; i++)
        {
            if (tree[i] != "")
            {
                if (breadcrumb == "")
                {
                    breadcrumb += " <strong>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tree[i]) + "</strong>";
                }
                else
                {
                    breadcrumb += " ‹ <a href='" + getLink(tree, (tree.Length - i)) + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tree[i]) + "</a>";
                }
            }
        }
        if (breadcrumb != "")
        {
            breadcrumb += " ‹ ";
        }
        breadcrumb += "<a href='/'>Graphite</a>";
        litBreadcrumb.Text = breadcrumb;
    }
    
    private string getLink(string[] arg_tree, int index)
    {
        string[] tree = arg_tree.Reverse().ToArray();
        string link = "";
        for (int i = 0; i < index; i++)
        {
            if (tree[i] == "")
            {
                link += "/";
            }
            else
            {
                link += tree[i] + "/";
            }
        }
        return link;
    }
}
