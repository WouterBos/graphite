using System;
using System.Collections.Generic;
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
        private System.Xml.XmlNodeList demo;

        public DemoMenu()
	    {
            System.Xml.XmlDocument demos = new System.Xml.XmlDocument();
            demos.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\demos\root.xml"));
            demo = demos.SelectNodes("/demos");
        }
        
        public string GetHTML()
        {
            return BuildMenu(demo);
        }
        
        private string BuildMenu(XmlNodeList arg_demo)
        {
            StringBuilder menuCode = new StringBuilder();
            menuCode.AppendLine("<ul>");
            foreach (XmlElement node in arg_demo[0].ChildNodes)
            {
                menuCode.AppendLine("<li>");
                menuCode.Append("<a href='/'>");
                menuCode.Append(node.Name);
                menuCode.AppendLine("</a>");
                if (node.ChildNodes.Count > 0)
                {
                    System.Xml.XmlNodeList demo;
                    menuCode.AppendLine(BuildMenu(node.));
                }
                menuCode.AppendLine("</li>");
            }
            menuCode.AppendLine("</ul>");

            return menuCode.ToString();
        }
    }
}