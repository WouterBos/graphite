using System.Web;
using System.Web.UI;
using Telerik;

/// <summary>
/// Summary description for SettingsTool
/// </summary>
public class SettingsTool: ITool
{
    public SettingsTool()
    {
    }

    public Control CreateControlPanel(TemplateControl path)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated || !HttpContext.Current.User.IsInRole("Administrators"))
        {
            return path.LoadControl("~/Estate/UserControls/Tools/NoPermissions.ascx");
        }
        else
        {
            return path.LoadControl("~/Estate/UserControls/Tools/SettingsTool/SettingsTool.ascx");
        }
    }

    public string Description
    {
        get { return "Miscellaneous settings"; }
    }

    public string Name
    {
        get { return "Settings"; }
    }

    public string Title
    {
        get { return "Settings"; }
    }
}
