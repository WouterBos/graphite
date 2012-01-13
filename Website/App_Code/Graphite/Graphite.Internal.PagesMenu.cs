using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;

namespace Graphite.Internal
{
    /// <summary>
    /// Creates Unordered list of links to demos
    /// </summary>
    public class PagesMenu
    {
        private System.Xml.XmlElement demo;

        public PagesMenu()
	    {
            System.Xml.XmlDocument demos = new System.Xml.XmlDocument();
            demos.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\Graphite\Internal\Sitemaps\pages.xml"));
            demo = demos.SelectSingleNode("/pages") as XmlElement;
        }
        
        public string GetHTML()
        {
            return BuildMenu(demo);
        }
        
        private string BuildMenu(XmlElement arg_demo)
        {
            StringBuilder menuCode = new StringBuilder();
            menuCode.AppendLine("<ul>");
            string href = "/Internal/Pages/documentation" + GetMenuPath(arg_demo);
            foreach (XmlElement node in arg_demo.ChildNodes)
            {
                if (node.HasAttribute("url") == true)
                {
                    string nodeUrl = node.Attributes["url"].Value;
                    string active = "";
                    if (IsActive(nodeUrl, node.ParentNode.Name, node.Name) == true)
                    {
                        active = "gp_active";
                    }
                    menuCode.AppendLine("<li class='" + active + "'>");
                    menuCode.Append("<a href='" + href + "/" + nodeUrl + "'>");
                    menuCode.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Attributes["title"].Value));
                    menuCode.AppendLine("</a>");
                    if (node.ChildNodes.Count > 0)
                    {
                        menuCode.AppendLine(BuildMenu(node));
                    }
                    menuCode.AppendLine("</li>");
                }
            }
            menuCode.AppendLine("</ul>");

            return menuCode.ToString();
        }
        
        private bool IsActive(string nodeUrl, string parentName, string nodeName)
        {
            string loadedUrl = HttpContext.Current.Request.ServerVariables["url"].ToLower();
            string safeNodeUrl = nodeUrl;
            if (parentName != "pages")
            {
                safeNodeUrl = parentName + "/" + nodeUrl;
            }
            if (loadedUrl.IndexOf("/documentation/" + safeNodeUrl.ToLower()) > 0)
            {
                return true;
            }
            if (loadedUrl.IndexOf("/documentation/" + nodeName.ToLower()) > 0)
            {
                return true;
            }
            return false;
        }
        
        private string GetMenuPath(XmlElement arg_Element)
        {
            string path = "";
            XmlNode Element = arg_Element as XmlNode;
            while (Element.Name != "#document")
            {
                path = "/" + Element.Name + path;
                Element = Element.ParentNode;
            }
            return path.Replace("/pages", "");
        }
    }
}