using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class internal_usercontrols_folders : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MakeFolderList();
    }
    
    
    
    private void MakeFolderList()
    {
        DirectoryInfo[] dir = new DirectoryInfo(Server.MapPath(Request.ServerVariables["SCRIPT_PATH"])).GetDirectories();
        rptFolders.DataSource = dir;
        rptFolders.DataBind();
    }
    
    protected void rptFolders_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {
            HyperLink hlFolder = e.Item.FindControl("hlFolder") as HyperLink;
            DirectoryInfo dirItem = e.Item.DataItem as DirectoryInfo;
            hlFolder.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dirItem.Name);
            hlFolder.Attributes["href"] = dirItem.Name;
        }
    }
}
