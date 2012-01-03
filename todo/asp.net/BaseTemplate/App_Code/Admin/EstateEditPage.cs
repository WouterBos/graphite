using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin;
using Telerik.Cms.Web;
using System.Web.UI;
using Telerik.FileManager.WebControls;
using Telerik.Cms.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;

/// <summary>
/// Summary description for EstateEditPage
/// </summary>
public class EstateEditPage : EditPage
{
    /// <summary>
    /// Creates the child controls.
    /// </summary>
    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        SFEstateDataContext ctx = new SFEstateDataContext(ConfigurationManager.ConnectionStrings["Sitefinity"].ConnectionString);
        Control controlsLoader = this.GetControlsLoader();
        if (controlsLoader != null)
        {
            // Process panels
            foreach (Control c in controlsLoader.Controls)
            {
                DropDownPanel panel = c as DropDownPanel;
                if (panel != null)
                {
                    estate_ToolboxControl tbcPanel = null;
                    var pnlResult = from pnl in ctx.estate_ToolboxControls where pnl.name == panel.Text && pnl.parent == null select pnl;
                    if (pnlResult != null && pnlResult.Count() > 0)
                    {
                        tbcPanel = pnlResult.First();
                    }
                    else
                    {
                        tbcPanel = new estate_ToolboxControl()
                        {
                            id = Guid.NewGuid(),
                            name = panel.Text,
                            parent = null,
                            showControl = true
                        };
                        ctx.estate_ToolboxControls.InsertOnSubmit(tbcPanel);
                    }
                    panel.Visible = tbcPanel.showControl || User.IsInRole("administrators");

                    // Process subpanels
                    foreach (Control cc in panel.Controls)
                    {
                        HtmlGenericControl subPanel = cc as HtmlGenericControl;
                        if (subPanel != null)
                        {
                            estate_ToolboxControl tbcSubPanel = null;
                            var subPnlResult = from subPnl in ctx.estate_ToolboxControls where subPnl.parent == tbcPanel.id && subPnl.name == subPanel.InnerText select subPnl;
                            if (subPnlResult != null && subPnlResult.Count() > 0)
                            {
                                tbcSubPanel = subPnlResult.First();
                            }
                            else
                            {
                                tbcSubPanel = new estate_ToolboxControl()
                                {
                                    id = Guid.NewGuid(),
                                    name = subPanel.InnerText,
                                    parent = tbcPanel.id,
                                    showControl = true
                                };
                                ctx.estate_ToolboxControls.InsertOnSubmit(tbcSubPanel);
                            }
                            subPanel.Visible = tbcSubPanel.showControl || User.IsInRole("administrators");
                        }
                    }
                }
            }
            ctx.SubmitChanges();
        }
    }

    /// <summary>
    /// Gets the control loader.
    /// </summary>
    /// <returns></returns>
    private Control GetControlsLoader()
    {
        Control controlsLoader = null;
        Toolbox toolBox = null;
        ToolBar toolBar = this.Form.FindControl("toolBar") as ToolBar;
        foreach (Control c in toolBar.Controls)
            if (c is Toolbox)
            {
                toolBox = c as Toolbox;
                break;
            }

        if (toolBox != null)
        {
            Control cContainer = toolBox.FindControl("controlsContainer");
            controlsLoader = cContainer.FindControl("controlsLoader");
        }

        return controlsLoader;
    }
}
