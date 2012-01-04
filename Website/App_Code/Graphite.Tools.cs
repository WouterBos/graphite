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
    public class Tools
    {
        public string getSourceCode(string file, string path)
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
    }
}