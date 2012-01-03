using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Libraries;
using System.Collections;

/// <summary>
/// Summary description for FlashGalleryDesigner
/// </summary>
public class FlashGalleryDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/FlashGalleryDesigner.ascx"; }
    }

    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((FlashGalleryControlBase)DesignedControl).Title;
        txtVideoplayer.Text = ((FlashGalleryControlBase)DesignedControl).Videoplayer;
        
        LibraryManager manager = new LibraryManager();
        IList libraries = manager.GetAllLibraries("Flash", true);
        ddlFvlGallery.AppendDataBoundItems = true;
        ddlFvlGallery.DataTextField = "Name";
        ddlFvlGallery.DataValueField = "ID";
        ddlFvlGallery.DataSource = libraries;
        ddlFvlGallery.DataBind();

        ListItem item = ddlFvlGallery.Items.FindByValue(((FlashGalleryControlBase)DesignedControl).FlvGallery);
        if (item != null)
        {
            ddlFvlGallery.SelectedIndex = ddlFvlGallery.Items.IndexOf(item);
        }
    }

    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((FlashGalleryControlBase)DesignedControl).Title = txtTitle.Text;
        ((FlashGalleryControlBase)DesignedControl).Videoplayer = txtVideoplayer.Text;
        ((FlashGalleryControlBase)DesignedControl).FlvGallery = ddlFvlGallery.SelectedValue;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtVideoplayer property.
    /// </summary>
    protected virtual TextBox txtVideoplayer
    {
        get { return base.Container.GetControl<TextBox>("txtVideoplayer", true); }
    }

    /// <summary>
    /// Gets a reference to the DropDownList field for the FvlGallery property.
    /// </summary>
    protected virtual DropDownList ddlFvlGallery
    {
        get { return base.Container.GetControl<DropDownList>("ddlFvlGallery", true); }
    }
}
