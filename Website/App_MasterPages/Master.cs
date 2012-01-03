using Graphite;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Master : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Css oCss = new Graphite.Css();
        MasterPageBody.Attributes["class"] += oCss.UserAgentClasses();

        ShowDevelopCode(false);
        HideInvalidCss();
        //HideFontface();
    }

	/// <summary>
	/// Hides fontface CSS if OS of user is XP or similar
	/// </summary>
	private void HideFontface()
	{
		if (Request.UserAgent.IndexOf("Windows NT 5.") > 0)
		{
			phFontface.Visible = false;
		}
		else
		{
			phFontface.Visible = true;
		}
	}
	
    /// <summary>
    /// Hides Estate developer tools
    /// </summary>
    private void ShowDevelopCode(bool enabled)
    {
        if (enabled == true && Request.QueryString["cmspagemode"] != "edit")
        {
            DevelopCode.Visible = true;
        }
        else
        {
            DevelopCode.Visible = false;
        }
    }
}
