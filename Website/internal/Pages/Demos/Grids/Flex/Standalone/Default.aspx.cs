﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDemoCode();
    }

    private void GetDemoCode()
    {
        Graphite.Tools oGraphite = new Graphite.Tools();
        string path = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]) + "\\";
        path = path.Replace("\\standalone", "");
        string DemoHtmlSource = oGraphite.getSourceCode("default-external.html", path);
        DemoHtmlSource = DemoHtmlSource.Replace("###GP_TITLE###", this.Title);
        DemoHtml.Text = DemoHtmlSource;
    }
}