using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Libraries;
using System.Collections;

/// <summary>
/// Summary description for FlashVideoDesigner
/// </summary>
public class FlashVideoDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/FlashVideoDesigner.ascx"; }
    }

    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((FlashVideoControlBase)DesignedControl).Title;
        txtFlvFile.Text = ((FlashVideoControlBase)DesignedControl).FlvFile;
        txtSubtitlesFile.Text = ((FlashVideoControlBase)DesignedControl).SubtitlesFile;
        txtThumbnail.Text = ((FlashVideoControlBase)DesignedControl).Thumbnail;
    }

    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((FlashVideoControlBase)DesignedControl).Title = txtTitle.Text;
        ((FlashVideoControlBase)DesignedControl).FlvFile = txtFlvFile.Text;
        ((FlashVideoControlBase)DesignedControl).SubtitlesFile = txtSubtitlesFile.Text;
        ((FlashVideoControlBase)DesignedControl).Thumbnail = txtThumbnail.Text;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtFlvFile property.
    /// </summary>
    protected virtual TextBox txtFlvFile
    {
        get { return base.Container.GetControl<TextBox>("txtFlvFile", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtSubtitlesFile property.
    /// </summary>
    protected virtual TextBox txtSubtitlesFile
    {
        get { return base.Container.GetControl<TextBox>("txtSubtitlesFile", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtThumbnail property.
    /// </summary>
    protected virtual TextBox txtThumbnail
    {
        get { return base.Container.GetControl<TextBox>("txtThumbnail", true); }
    }
}
