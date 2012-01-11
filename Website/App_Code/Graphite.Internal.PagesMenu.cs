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
            string href = GetMenuPath(arg_demo);
            foreach (XmlElement node in arg_demo.ChildNodes)
            {
                if (node.HasAttribute("url") == true)
                {
                    menuCode.AppendLine("<li>");
                    menuCode.Append("<a href='/Internal/Pages" + href + "/" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Attributes["title"].Value) + "/default.aspx'>");
                    menuCode.Append(node.Attributes["url"]);
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
        
        private string GetMenuPath(XmlElement arg_Element)
        {
            string path = "";
            XmlNode Element = arg_Element as XmlNode;
            while (Element.Name != "#document")
            {
                path = "/" + Element.Name + path;
                Element = Element.ParentNode;
            }
            return path;
        }
    }
}