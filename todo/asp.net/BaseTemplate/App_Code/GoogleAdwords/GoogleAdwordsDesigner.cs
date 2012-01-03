using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for GoogleAdwordsDesigner
/// </summary>
public class GoogleAdwordsDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/GoogleAdwordsDesigner.ascx"; }
    }
    
    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtCode.Text = ((GoogleAdwordsControlBase)DesignedControl).Code;
        rdpActivationDate.SelectedDate = ((GoogleAdwordsControlBase)DesignedControl).ActivationDate;
        if (((GoogleAdwordsControlBase)DesignedControl).ExpirationDate > rdpExpirationDate.MaxDate)
        {
            rdpExpirationDate.SelectedDate = rdpExpirationDate.MaxDate;
        }
        else
        {
            rdpExpirationDate.SelectedDate = ((GoogleAdwordsControlBase)DesignedControl).ExpirationDate;
        }
        cbActive.Checked = ((GoogleAdwordsControlBase)DesignedControl).Active;
    }
 
    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((GoogleAdwordsControlBase)DesignedControl).Code = txtCode.Text;
        ((GoogleAdwordsControlBase)DesignedControl).ActivationDate = (DateTime)rdpActivationDate.SelectedDate;
        ((GoogleAdwordsControlBase)DesignedControl).ExpirationDate = (DateTime)rdpExpirationDate.SelectedDate;
        ((GoogleAdwordsControlBase)DesignedControl).Active = cbActive.Checked;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtCode property.
    /// </summary>
    protected virtual TextBox txtCode
    {
        get { return base.Container.GetControl<TextBox>("txtCode", true); }
    }

    /// <summary>
    /// Gets a reference to the rdpActivationDate field.
    /// </summary>
    protected virtual RadDateTimePicker rdpActivationDate
    {
        get { return base.Container.GetControl<RadDateTimePicker>("rdpActivationDate", true); }
    }

    /// <summary>
    /// Gets a reference to the rdpExpirationDate field.
    /// </summary>
    protected virtual RadDateTimePicker rdpExpirationDate
    {
        get { return base.Container.GetControl<RadDateTimePicker>("rdpExpirationDate", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the Active property.
    /// </summary>
    protected virtual CheckBox cbActive
    {
        get { return base.Container.GetControl<CheckBox>("cbActive", true); }
    }
}
