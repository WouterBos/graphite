using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Master : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowDevelopCode(false);
        HideInvalidCss();
		AddUserAgentBodyClass();
        ForceIeVersion(false, true);
        //HideFontface();
    }


    /// <summary>
    /// Hides CSS file with invalid code
    /// </summary>
    private void HideInvalidCss()
    {
        if (Request.Browser.EcmaScriptVersion.Major >= 1 && Request.Browser.Frames == false && Request.Browser.Type == "Unknown" && Request.Browser.Browser == "Unknown")
		{
			phHideInvalidCss.Visible = false;
		}
    }

    /// <summary>
    /// Toggles X-UA metatag
    /// </summary>
    private void ForceIeVersion(bool bForceInPublicPage, bool bForceInEditMode)
    {
        if (Request.QueryString["cmspagemode"] == "edit")
        {
            phForceIEVersion.Visible = bForceInEditMode;
        }
        else
        {
            phForceIEVersion.Visible = bForceInPublicPage;
        }
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

	/// <summary>
	/// Hides fontface CSS if OS of user is XP or similar
	/// </summary>
	private void AddUserAgentBodyClass()
	{
		string browserName = HttpContext.Current.Request.Browser.Browser;
        string browserVersion = HttpContext.Current.Request.Browser.MajorVersion.ToString();
		if (Request.UserAgent.Contains("Chrome"))
		{
            browserName = "chrome";
		}
		else if (Request.UserAgent.Contains("Safari"))
		{
            browserName = "safari";
		}
        Output.AddClass(this.MasterPageBody, browserName.ToLower() + " " + browserName.ToLower() + browserVersion);
	}
}
