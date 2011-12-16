using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class GraphiteBlocksFormsLogin : System.Web.UI.UserControl
{
    private string _strRootClass = "";
    public string strRootClass
    {
        get { return _strRootClass; }
        set { _strRootClass = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlRoot.CssClass += " " + _strRootClass;
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (Login1.UserName == "test" && Login1.Password == "test")
        {
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
        }
    }
}
