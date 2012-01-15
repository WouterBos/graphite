using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Web;

namespace Graphite
{
    /// <summary>
    /// Summary description for Graphite
    /// </summary>
    public static class Tools
    {
        public static string GetSourceCode(string file, string path)
        {
            StringBuilder sbCode = new StringBuilder();

            // GRAPHITE_TODO Check if file exists.
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(path + file);
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

        public static string GetXmlPath(string append)
        {
            string strPhysicalPath = HttpContext.Current.Request.ServerVariables["script_name"].Replace("default.aspx", "");
            strPhysicalPath = strPhysicalPath.ToLower();
            strPhysicalPath = strPhysicalPath.Replace("-", "");
            strPhysicalPath = strPhysicalPath.Replace("/internal/pages", "");
            return strPhysicalPath + append;
        }

        public static string GetHumanName(string path)
        {
            System.Xml.XmlDocument demos = new System.Xml.XmlDocument();
            demos.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\Graphite\Internal\Sitemaps\demos.xml"));
            XmlElement node = demos.SelectSingleNode(path) as XmlElement;
            if (node.HasAttribute("humanname") == true)
            {
                return node.Attributes["humanname"].Value;
            }
            else
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Name);
            }
            
        }        
        

    }
}