using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Internal_Pages_Demos_Blocks_Navigation_Menu_default : System.Web.UI.UserControl
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
}
