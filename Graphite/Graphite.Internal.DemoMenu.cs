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
    public class DemoMenu
    {
        private System.Xml.XmlElement demo;
        private System.Xml.XmlDocument demos;

        public DemoMenu()
	    {
            demos = new System.Xml.XmlDocument();
            demos.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\Graphite\Internal\Sitemaps\demos.xml"));
            demo = demos.SelectSingleNode("/demos") as XmlElement;
        }
        
        public string GetHTML(bool tree)
        {
            if (tree == false)
            {
                string selector = HttpContext.Current.Request.ServerVariables["url"];
                int cutStart = selector.IndexOf("/demos/");
                int cutEnd = selector.LastIndexOf("/");
                selector = selector.Substring(cutStart, cutEnd - cutStart);
                demo = demos.SelectSingleNode(selector) as XmlElement;
            }
            return BuildMenu(demo, tree);
        }

        private string BuildMenu(XmlElement arg_demo, bool tree)
        {
            StringBuilder menuCode = new StringBuilder();
            menuCode.AppendLine("<ul>");
            string href = GetMenuPath(arg_demo);
            foreach (XmlElement node in arg_demo.ChildNodes)
            {
                if (node.Name != "demo")
                {
                    menuCode.AppendLine("<li>");
                    menuCode.Append("<a href='/Internal/Pages" + href + "/" + node.Name + "/default.aspx'>");
                    if (node.HasAttribute("humanname") == true)
                    {
                        menuCode.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Attributes["humanname"].Value));
                    }
                    else
                    {
                        menuCode.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Name));
                    }
                    menuCode.AppendLine("</a>");
                    if (tree == true && node.ChildNodes.Count > 0)
                    {
                        menuCode.AppendLine(BuildMenu(node, tree));
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