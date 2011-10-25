using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class internal_usercontrols_blockdemo : System.Web.UI.UserControl
{
    // Demo data

    private string[] _demos;
    public string[] demos
    {
        get
        {
            return _demos;
        }
        set
        {
            _demos = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //litDebug.Text = this.Page.demos[0];
        litDebug.Text = "[" + _demos[0] + "]";
    }
}
