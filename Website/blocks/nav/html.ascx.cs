using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blocks_nav_html : System.Web.UI.UserControl
{
    private bool _PrintAsCopyableCode = false;
    public bool PrintAsCopyableCode
    {
        get
        {
            return _PrintAsCopyableCode;
        }
        set
        {
            _PrintAsCopyableCode = value;
        }
    }

    private string _CssType = "";
    public string CssType
    {
        get
        {
            return _CssType;
        }
        set
        {
            _CssType = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string html = CodeHidden.Text;
        
        if (_PrintAsCopyableCode == true)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
        }
        
        if (_CssType == "")
        {
            html = html.Replace("##CSS_TYPE##", "");
        }
        else
        {
            html = html.Replace("##CSS_TYPE##", _CssType);
        }
        
        Code.Text = html;
        CodeHidden.Visible = false;
    }
}
