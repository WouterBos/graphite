using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Xml;
using System.Data;
using System.IO;
using Telerik.Framework;
using Telerik.Web;
using Telerik.Cms.Tools.WebControls;

namespace Telerik.Cms.Tools.ReplaceTool
{
    /// <summary>
    /// Summary description for CustomTool
    /// </summary>
    public class ReplaceTool : ITool
    {
        public ReplaceTool()
        {
        }

        public Control CreateControlPanel(TemplateControl path)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || !HttpContext.Current.User.IsInRole("Administrators"))
            {
                return path.LoadControl("~/Estate/UserControls/Tools/NoPermissions.ascx");
            }
            else
            {
                return path.LoadControl("~/Estate/UserControls/Tools/ReplaceTool/ReplaceTool.ascx");
            }
        } 

        public string Description
        {
            get { return "Search and replace for terms in your CMS Content"; }
        }

        public string Name
        {
            get { return "ReplaceTool"; }
        }

        public string Title
        {
            get { return "Replace Tool"; }
        }
    }
}
