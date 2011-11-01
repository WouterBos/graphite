using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Web;

namespace Graphite
{
    /// <summary>
    /// Summary description for Graphite
    /// </summary>
    public class Config
    {
        private System.Xml.XmlNodeList demo;
        
        public Config(string demosClasses)
	    {
            System.Xml.XmlDocument demos = new System.Xml.XmlDocument();
            demos.Load(HttpContext.Current.Server.MapPath(@"~\App_Data\demos\root.xml"));
            demo = demos.SelectNodes(demosClasses);
        }

        public int Index(string name)
        {
            int index = -1;
            int count = 0;
            foreach (XmlNode node in demo[0].ChildNodes)
            {
                if (name == node.Name)
                {
                    index = count;
                }
                count++;
            }
            return index;
        }
        
        public string[] Types()
	    {
	        string[] types = new string[demo[0].ChildNodes.Count];
	        int count = 0;
            foreach (XmlNode node in demo[0].ChildNodes)
            {
                types[count] = node.Name;
                count++;
            }
            return types;
	    }

        public Dictionary<string, Boolean> Files(int index)
        {
            Dictionary<string, Boolean> defaultCode = new Dictionary<string, Boolean>();
            foreach (XmlElement node in demo[0].ChildNodes[index].SelectSingleNode("files"))
            {
                bool defaultBool = false;
                if (node.HasAttribute("use_default_code"))
                {
                    defaultBool = true;
                }
                defaultCode.Add(node.Name, defaultBool);
            }
            return defaultCode;
        }

        public Dictionary<string, string> SupportedBrowsers(int index)
        {
            Dictionary<string, string> supportedBrowsers = new Dictionary<string, string>();
            foreach (XmlElement node in demo[0].ChildNodes[index].SelectSingleNode("browsers"))
            {
                string browserVersion = "";
                if (node.HasAttribute("supports"))
                {
                    browserVersion = node.Attributes["supports"].Value;
                }
                supportedBrowsers.Add(node.Name, browserVersion);
            }
            return supportedBrowsers;
        }

        public string Type(int index)
        {
            return demo[0].ChildNodes[index].Name;
        }

        public string CssClass(int index)
        {
            return demo[0].ChildNodes[index].Attributes["cssclass"].Value;
        }
    }
}