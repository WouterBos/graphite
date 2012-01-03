using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Cms.Engine;

/// <summary>
/// Summary description for GoogleMapsDirectionsDesigner
/// </summary>
public class GoogleMapsDirectionsDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    private List<String> ImageFlds;
    
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/GoogleMaps/Designers/GoogleMapsDirectionsDesigner.ascx"; }
    }

    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        txtTitle.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).Title;
        txtAddress.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).Address;
        txtContentFields.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).ContentFields;
        txtGeomappingData.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).GeomappingData;
        txtWidth.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).Width;
        txtHeight.Text = ((GoogleMapsDirectionsControlBase)DesignedControl).Height;
        cbDisplayRouteInNewWindow.Checked = ((GoogleMapsDirectionsControlBase)DesignedControl).DisplayRouteInNewWindow;

        ListItem item = ddlMapControls.Items.FindByValue(((GoogleMapsDirectionsControlBase)DesignedControl).MapControls);
        if (item != null)
        {
            ddlMapControls.SelectedIndex = ddlMapControls.Items.IndexOf(item);
        }
    }
 
    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((GoogleMapsDirectionsControlBase)DesignedControl).Title = txtTitle.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).Address = txtAddress.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).ContentFields = txtContentFields.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).GeomappingData = txtGeomappingData.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).Width = txtWidth.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).Height = txtHeight.Text;
        ((GoogleMapsDirectionsControlBase)DesignedControl).DisplayRouteInNewWindow = cbDisplayRouteInNewWindow.Checked;
        ((GoogleMapsDirectionsControlBase)DesignedControl).MapControls = ddlMapControls.SelectedValue;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtAddress property.
    /// </summary>
    protected virtual TextBox txtAddress
    {
        get { return base.Container.GetControl<TextBox>("txtAddress", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtGeomappingData property.
    /// </summary>
    protected virtual TextBox txtGeomappingData
    {
        get { return base.Container.GetControl<TextBox>("txtGeomappingData", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtContentFields property.
    /// </summary>
    protected virtual TextBox txtContentFields
    {
        get { return base.Container.GetControl<TextBox>("txtContentFields", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtWidth property.
    /// </summary>
    protected virtual TextBox txtWidth
    {
        get { return base.Container.GetControl<TextBox>("txtWidth", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtHeight property.
    /// </summary>
    protected virtual TextBox txtHeight
    {
        get { return base.Container.GetControl<TextBox>("txtHeight", true); }
    }

    /// <summary>
    /// Gets a reference to the ddlMapControls DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlMapControls
    {
        get { return base.Container.GetControl<DropDownList>("ddlMapControls", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the cbDisplayRouteInNewWindow property.
    /// </summary>
    protected virtual CheckBox cbDisplayRouteInNewWindow
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayRouteInNewWindow", true); }
    }
}
