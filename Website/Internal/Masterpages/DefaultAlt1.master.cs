using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class App_Master_Default : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
		AddUserAgentBodyClass();
    }


	/// <summary>
	/// Hides fontface CSS if OS of user is XP or similar
	/// </summary>
	private void AddUserAgentBodyClass()
	{
		string browserClass = HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.MajorVersion.ToString();
		if (Request.UserAgent.Contains("Chrome"))
		{
			browserClass = "chrome" + HttpContext.Current.Request.Browser.MajorVersion.ToString();
		}
		else if (Request.UserAgent.Contains("Safari"))
		{
			browserClass = "safari" + HttpContext.Current.Request.Browser.MajorVersion.ToString();
		}
        MasterPageBody.Attributes["class"] += browserClass.ToLower();
	}
}
