using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for ExternalMovieDesigner
/// </summary>
public class ExternalMovieDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/ExternalMovieDesigner.ascx"; }
    }
    
    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((ExternalMovieControlBase)DesignedControl).Title;
        txtVideoCode.Text = ((ExternalMovieControlBase)DesignedControl).VideoCode;
        chkPopup.Checked = ((ExternalMovieControlBase)DesignedControl).ShowInPopup;
    }
 
    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((ExternalMovieControlBase)DesignedControl).Title = txtTitle.Text;
        ((ExternalMovieControlBase)DesignedControl).VideoCode = txtVideoCode.Text;
        ((ExternalMovieControlBase)DesignedControl).ShowInPopup = chkPopup.Checked;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtVideoCode property.
    /// </summary>
    protected virtual TextBox txtVideoCode
    {
        get { return base.Container.GetControl<TextBox>("txtVideoCode", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the txtVideoCode property.
    /// </summary>
    protected virtual CheckBox chkPopup
    {
        get { return base.Container.GetControl<CheckBox>("chkPopup", true); }
    }
}
