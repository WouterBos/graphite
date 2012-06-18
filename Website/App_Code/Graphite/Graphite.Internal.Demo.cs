using Graphite;
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
    public class Demo
    {
        // Returns source code of a demo file
        public static string GetSourceCode(string suffix, bool dicFiles, int intActiveIndex, string path, Graphite.Internal.Config config)
        {
            string strFileName = "default";
            if (dicFiles == false)
            {
                int intMenuItemActive = intActiveIndex;
                strFileName = config.Type(intMenuItemActive);
            }

            string strRoot = HttpContext.Current.Server.MapPath(path) + "\\";
            StringBuilder sbCode = new StringBuilder();

            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(strRoot + strFileName + suffix);
                string line;

                while (sr.Peek() != -1)
                {
                    line = sr.ReadLine();
                    sbCode.AppendLine(line);
                }

                sr.Close();
                sr.Dispose();
            }
            catch (Exception exp)
            {
                //
            }
            return sbCode.ToString();
        }

        public static string GetCodeBehind(string ascxSource, string cssClass, string path, Graphite.Internal.Config config, int activeIndex)
        {
            string strCode = "";

            // Get URL to Codebehind
            string strCodeBehindURL = "";
            int intCodeFileStart = ascxSource.IndexOf("CodeFile=", StringComparison.OrdinalIgnoreCase);
            int intIgnoreCodeFile = ascxSource.IndexOf("<!-- Graphite: Ignore Codefile -->", StringComparison.OrdinalIgnoreCase);
            int intStartQuote = ascxSource.IndexOf("\"", intCodeFileStart);
            int intEndQuote = ascxSource.IndexOf("\"", (intStartQuote + 1));
            if (intStartQuote >= 0)
            {
                strCodeBehindURL = ascxSource.Substring((intStartQuote + 1), (intEndQuote - intStartQuote - 1));
            }

            string strRoot = HttpContext.Current.Server.MapPath(path) + "\\";

            if (intIgnoreCodeFile >= 0)
            {
                // Get Codebehind code
                try
                {
                    StringBuilder sbCode = new StringBuilder();
                    System.IO.StreamReader sr = new System.IO.StreamReader(strRoot + strCodeBehindURL);

                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        sbCode.AppendLine(line);
                    }
                    sr.Close();
                    sr.Dispose();

                    strCode = sbCode.ToString();

                    // Insert class into private variable _strRootClass
                    int rootClassStart = strCode.IndexOf("_strRootClass", StringComparison.OrdinalIgnoreCase);
                    int rootClassValueStart = strCode.IndexOf("\"\"", rootClassStart);
                    strCode = strCode.Insert(rootClassValueStart + 1, config.CssClass(activeIndex));
                }
                catch (Exception exp)
                {
                    // No Codebehind available
                }
            }
            return strCode;
        }

    }
}