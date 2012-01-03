using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for DownloadFileFromModuleDesigner
/// </summary>
public class DownloadFileFromModuleDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/DownloadFileFromModuleDesigner.ascx"; }
    }

     /// <summary>
    /// Executed when the child controls are created.
    /// </summary>
   protected override void CreateChildControls()
    {
        base.CreateChildControls();
        
        Intro.Content = ((DownloadFileFromModuleControlBase)DesignedControl).Intro;
    }
    
    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((DownloadFileFromModuleControlBase)DesignedControl).Title;
        txtButtonText.Text = ((DownloadFileFromModuleControlBase)DesignedControl).ButtonText;
        ItemsPicker.Text = ((DownloadFileFromModuleControlBase)DesignedControl).File;
    }
 
    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((DownloadFileFromModuleControlBase)DesignedControl).Title = txtTitle.Text;
        ((DownloadFileFromModuleControlBase)DesignedControl).ButtonText = txtButtonText.Text;
        ((DownloadFileFromModuleControlBase)DesignedControl).Intro =  Intro.Content;
        ((DownloadFileFromModuleControlBase)DesignedControl).File = ItemsPicker.Text;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtButtonText property.
    /// </summary>
    protected virtual TextBox txtButtonText
    {
        get { return base.Container.GetControl<TextBox>("txtButtonText", true); }
    }

    /// <summary>
    /// Gets a reference to the html content for the Intro property.
    /// </summary>
    protected virtual RadEditor Intro
    {
        get { return base.Container.GetControl<RadEditor>("Intro", true); }
    }
    
    protected virtual RelatedItemsPickerBase ItemsPicker
    {
        get { return base.Container.GetControl<RelatedItemsPickerBase>("ItemsPicker", true); }
    }
}
