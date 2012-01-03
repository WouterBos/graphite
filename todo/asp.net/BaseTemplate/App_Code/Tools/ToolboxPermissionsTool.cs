using System.Web;
using System.Web.UI;
using Telerik;

/// <summary>
/// Summary description for ToolboxPermissionsTool
/// </summary>
public class ToolboxPermissionsTool: ITool
{
    public ToolboxPermissionsTool()
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
            return path.LoadControl("~/Estate/UserControls/Tools/ToolboxPermissionsTool/ToolboxPermissionsTool.ascx");
        }
    }

    public string Description
    {
        get { return "Toolbox Permissions"; }
    }

    public string Name
    {
        get { return "Toolbox Permissions"; }
    }

    public string Title
    {
        get { return "Toolbox Permissions"; }
    }
}
