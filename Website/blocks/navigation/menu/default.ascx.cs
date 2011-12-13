﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gp_blocks_navigation_menu : System.Web.UI.UserControl
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
