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
    }
}