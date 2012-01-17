using Graphite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GraphiteInternal_BreadCrumb : System.Web.UI.UserControl
{
    private XmlElement xmlElement;
    private XmlDocument xmlDocument;

    protected void Page_Load(object sender, EventArgs e)
    {
        string xmlPath = Graphite.Tools.GetXmlPath("");
        string xmlFile = "";
        if (xmlPath.IndexOf("/demos") == 0)
        {
            xmlFile = "demos.xml";
        }
        else
        {
            xmlFile = "pages.xml";
        }
        //litBreadcrumb.Text = Graphite.Tools.GetXmlPath("");
        xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\Graphite\Internal\Sitemaps\" + xmlFile));
        xmlPath = xmlPath.Replace("default.aspx", "");
        xmlPath = xmlPath.Substring(0, xmlPath.Length - 1);
        Response.Write(xmlPath);
        //xmlElement = xmlDocument.SelectSingleNode(xmlPath) as XmlElement;
        //StringBuilder sbBreadCrumb = new StringBuilder();
        //XmlNode xmlNode = xmlElement as XmlNode;
        //int nodeLevel = 0;
        //while (xmlNode.Name != "#document")
        //{
        //    sbBreadCrumb.AppendLine(getLink(xmlNode, nodeLevel));
        //    xmlNode = xmlNode.ParentNode as XmlNode;
        //    nodeLevel++;
        //}
        //sbBreadCrumb.AppendLine(" ‹ <a href='/'>Start page</a>");
        //litBreadcrumb.Text = sbBreadCrumb.ToString();
    }

    private string getLink(XmlNode xmlNode, int nodeLevel)
    {
        XmlElement xmlElement = xmlNode as XmlElement;
        string linkName;
        StringBuilder sbReturn = new StringBuilder();
        if (xmlElement.HasAttribute("humanname"))
        {
            linkName = xmlElement.Attributes["humanname"].Value;
        }
        else
        {
            linkName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(xmlElement.Name);
        }
        if (nodeLevel > 0)
        {
            sbReturn.Append(" ‹ ");
        }
        string link = "<a href='/internal/pages" + GetXPathToNode(xmlNode) + "'>" + linkName + "</a>";
        if (nodeLevel == 0)
        {
            link = "<strong>" + link + "</strong>";
        }
        sbReturn.AppendLine(link);
        return sbReturn.ToString();
    }

    private string GetXPathToNode(XmlNode node)
    {
        if (node.NodeType == XmlNodeType.Attribute)
        {
            // attributes have an OwnerElement, not a ParentNode; also they have
            // to be matched by name, not found by position
            return String.Format(
                "{0}/@{1}",
                GetXPathToNode(((XmlAttribute)node).OwnerElement),
                node.Name
            );
        }
        if (node.ParentNode == null)
        {
            // the only node with no parent is the root node, which has no path
            return "";
        }
        //get the index
        int iIndex = 1;
        XmlNode xnIndex = node;
        while (xnIndex.PreviousSibling != null)
        {
            iIndex++;
            xnIndex = xnIndex.PreviousSibling;
        }
        // the path to a node is the path to its parent, plus "/node()[n]", where 
        // n is its position among its siblings.
        return String.Format(
            "{0}/{1}",
            GetXPathToNode(node.ParentNode),
            node.Name
        );
    }

    private void PrintBreadCrumb()
    {
        string strPhysicalPath = Request.ServerVariables["script_name"].Replace("default.aspx", "");
        char[] charSplitChar = { '/' };
        string[] strTree = strPhysicalPath.Split(charSplitChar);
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
        strBreadcrumb += "<a href='/'>Start page</a>";
        litBreadcrumb.Text = strBreadcrumb;
    }
    
    private string GetLink(string[] arg_tree, int intIndex)
    {
        string[] strTree = arg_tree;
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
