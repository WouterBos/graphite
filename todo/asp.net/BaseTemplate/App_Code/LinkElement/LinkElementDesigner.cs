using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for LinkElementDesigner
/// </summary>
public class LinkElementDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/LinkElementDesigner.ascx"; }
    }

     /// <summary>
    /// Executed when the child controls are created.
    /// </summary>
   protected override void CreateChildControls()
    {
        base.CreateChildControls();
        
        Intro.Content = ((LinkElementControlBase)DesignedControl).Intro;
    }
    
    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((LinkElementControlBase)DesignedControl).Title;
        txtSubtitle.Text = ((LinkElementControlBase)DesignedControl).Subtitle;
        txtThumbnail.Text = ((LinkElementControlBase)DesignedControl).Thumbnail;
        txtLinkUrl.Text = ((LinkElementControlBase)DesignedControl).LinkUrl;
        cbAddRowClickEvent.Checked = ((LinkElementControlBase)DesignedControl).AddRowClickEvent;
        cbDisplayVideoOverlay.Checked = ((LinkElementControlBase)DesignedControl).DisplayVideoOverlay;
        cbDisplayNewOverlay.Checked = ((LinkElementControlBase)DesignedControl).DisplayNewOverlay;

        ListItem item = ddlLinkTarget.Items.FindByValue(((LinkElementControlBase)DesignedControl).LinkTarget);
        if (item != null)
        {
            ddlLinkTarget.SelectedIndex = ddlLinkTarget.Items.IndexOf(item);
        }
    }
 
    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((LinkElementControlBase)DesignedControl).Title = txtTitle.Text;
        ((LinkElementControlBase)DesignedControl).Subtitle = txtSubtitle.Text;
        ((LinkElementControlBase)DesignedControl).Intro =  Intro.Content;
        ((LinkElementControlBase)DesignedControl).Thumbnail = txtThumbnail.Text;
        ((LinkElementControlBase)DesignedControl).LinkUrl = txtLinkUrl.Text;
        ((LinkElementControlBase)DesignedControl).LinkTarget = ddlLinkTarget.SelectedValue;
        ((LinkElementControlBase)DesignedControl).AddRowClickEvent = cbAddRowClickEvent.Checked;
        ((LinkElementControlBase)DesignedControl).DisplayVideoOverlay = cbDisplayVideoOverlay.Checked;
        ((LinkElementControlBase)DesignedControl).DisplayNewOverlay = cbDisplayNewOverlay.Checked;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtSubtitle property.
    /// </summary>
    protected virtual TextBox txtSubtitle
    {
        get { return base.Container.GetControl<TextBox>("txtSubtitle", true); }
    }

    /// <summary>
    /// Gets a reference to the html content for the Intro property.
    /// </summary>
    protected virtual RadEditor Intro
    {
        get { return base.Container.GetControl<RadEditor>("Intro", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtThumbnail property.
    /// </summary>
    protected virtual TextBox txtThumbnail
    {
        get { return base.Container.GetControl<TextBox>("txtThumbnail", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtLinkUrl property.
    /// </summary>
    protected virtual TextBox txtLinkUrl
    {
        get { return base.Container.GetControl<TextBox>("txtLinkUrl", true); }
    }

    /// <summary>
    /// Gets a reference to the DropDownList for the LinkTarget property.
    /// </summary>
    protected virtual DropDownList ddlLinkTarget
    {
        get { return base.Container.GetControl<DropDownList>("ddlLinkTarget", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the AddRowClickEvent property.
    /// </summary>
    protected virtual CheckBox cbAddRowClickEvent
    {
        get { return base.Container.GetControl<CheckBox>("cbAddRowClickEvent", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the DisplayVideoOverlay property.
    /// </summary>
    protected virtual CheckBox cbDisplayVideoOverlay
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayVideoOverlay", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the DisplayNewOverlay property.
    /// </summary>
    protected virtual CheckBox cbDisplayNewOverlay
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayNewOverlay", true); }
    }
}
