using Graphite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI.WebControls;


public partial class GraphiteInternal_Folders : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MakeFolderList();
    }
    
    
    
    private void MakeFolderList()
    {
        string strScriptPath = Server.MapPath(Request.ServerVariables["SCRIPT_PATH"]);
        DirectoryInfo directory = new DirectoryInfo(strScriptPath);
        DirectoryInfo[] directories = directory.GetDirectories();
        rptFolders.DataSource = directories;
        rptFolders.DataBind();

        litFolderName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(directory.Name);
    }
    
    protected void rptFolders_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {
            HyperLink hlFolder = e.Item.FindControl("hlFolder") as HyperLink;
            DirectoryInfo dirItem = e.Item.DataItem as DirectoryInfo;
            string strFolderNameHuman = Graphite.Tools.GetXmlPath(dirItem.Name.ToLower());
            strFolderNameHuman = Graphite.Tools.GetHumanName(strFolderNameHuman);
            //strFolderNameHuman = strFolderNameHuman.Replace("-", " ");
            hlFolder.Text = strFolderNameHuman;
            hlFolder.Attributes["href"] = dirItem.Name;
        }
    }
}
