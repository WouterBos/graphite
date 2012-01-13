using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;

namespace Graphite
{
    /// <summary>
    /// Summary description for Graphite
    /// </summary>
    public static class Css
    {
        /// <summary>
        /// Creates class string with browser name and version
        /// </summary>
        public static string UserAgentClasses()
        {
            string browserName = HttpContext.Current.Request.Browser.Browser;
            string browserNameAndVersion = HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.MajorVersion.ToString();
            if (HttpContext.Current.Request.UserAgent.Contains("Chrome"))
            {
                browserName = "chrome";
                browserNameAndVersion = browserName + HttpContext.Current.Request.Browser.MajorVersion.ToString();
            }
            else if (HttpContext.Current.Request.UserAgent.Contains("Safari"))
            {
                browserName = "safari";
                browserNameAndVersion = browserName + HttpContext.Current.Request.Browser.MajorVersion.ToString();
            }
            return browserName.ToLower() + " " + browserNameAndVersion.ToLower();
        }
    }
}