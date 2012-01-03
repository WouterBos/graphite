using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Cms.Engine;

/// <summary>
/// Summary description for GoogleMapsListDesigner
/// </summary>
public class GoogleMapsListDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    private List<String> ImageFlds;
    
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/GoogleMaps/Designers/GoogleMapsListDesigner.ascx"; }
    }

    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        ImageFlds = new List<String>();
        ImageFlds.Add("Image");
        ImageFlds.Add("Picture");
        ImageFlds.Add("Thumb");
        ImageFlds.Add("Thumbnail");
        ImageFlds.Add("Testimonial_Picture");
        
        txtTitle.Text = ((GoogleMapsListControlBase)DesignedControl).Title;
        txtLinkUrl.Text = ((GoogleMapsListControlBase)DesignedControl).LinkUrl;
        txtLinkText.Text = ((GoogleMapsListControlBase)DesignedControl).LinkText;

        IDictionary<string, ContentProviderBase> providers = ContentManager.Providers;
        foreach (KeyValuePair<string, ContentProviderBase> provider in providers)
        {
            ContentManager tempContentManager = new ContentManager(provider.Key.ToString());
            if (tempContentManager.MetaKeys.ContainsKey("Geomapping_Data"))
            {
                ddlDataProvider.Items.Add(provider.Key.ToString());
            }
        }
        ddlDataProvider.SelectedIndexChanged += ddlDataProvider_SelectedIndexChanged;
        ddlDataProvider.AutoPostBack = true;
        
        ListItem item = null;
        String ProviderName = ((GoogleMapsListControlBase)DesignedControl).ProviderName;
        if (!String.IsNullOrEmpty(ProviderName))
        {
            item = ddlDataProvider.Items.FindByValue(ProviderName);
            if (item != null)
            {
                ddlDataProvider.SelectedIndex = ddlDataProvider.Items.IndexOf(item);
            }

            List<string> FldsToDisplay;
            List<string> SelectedFlds = DetermineFldsToDisplay(ProviderName, out FldsToDisplay, ((GoogleMapsListControlBase)DesignedControl).ContentFields);
            
            List<string> AvailableFlds = DetermineAvailableFlds(FldsToDisplay, ((GoogleMapsListControlBase)DesignedControl).ContentFields);

            lbContentFieldsAvailable.DataSource = AvailableFlds;
            lbContentFieldsAvailable.DataBind();

            lbContentFields.DataSource = SelectedFlds;
            lbContentFields.DataBind();
        }
        
        txtContentFields.Text = ((GoogleMapsListControlBase)DesignedControl).ContentFields;
        txtGeomappingData.Text = ((GoogleMapsListControlBase)DesignedControl).GeomappingData;
        txtWidth.Text = ((GoogleMapsListControlBase)DesignedControl).Width;
        txtHeight.Text = ((GoogleMapsListControlBase)DesignedControl).Height;

        item = ddlMapControls.Items.FindByValue(((GoogleMapsListControlBase)DesignedControl).MapControls);
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
        ((GoogleMapsListControlBase)DesignedControl).Title = txtTitle.Text;
        ((GoogleMapsListControlBase)DesignedControl).LinkUrl = txtLinkUrl.Text;
        ((GoogleMapsListControlBase)DesignedControl).LinkText = txtLinkText.Text;
        ((GoogleMapsListControlBase)DesignedControl).ProviderName = ddlDataProvider.SelectedValue;;
        ((GoogleMapsListControlBase)DesignedControl).ContentFields = txtContentFields.Text;
        ((GoogleMapsListControlBase)DesignedControl).GeomappingData = txtGeomappingData.Text;
        ((GoogleMapsListControlBase)DesignedControl).Width = txtWidth.Text;
        ((GoogleMapsListControlBase)DesignedControl).Height = txtHeight.Text;
        ((GoogleMapsListControlBase)DesignedControl).MapControls = ddlMapControls.SelectedValue;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtTitle property.
    /// </summary>
    protected virtual TextBox txtTitle
    {
        get { return base.Container.GetControl<TextBox>("txtTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtLinkUrl property.
    /// </summary>
    protected virtual TextBox txtLinkUrl
    {
        get { return base.Container.GetControl<TextBox>("txtLinkUrl", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtLinkText property.
    /// </summary>
    protected virtual TextBox txtLinkText
    {
        get { return base.Container.GetControl<TextBox>("txtLinkText", true); }
    }

    /// <summary>
    /// Gets a reference to the data provider DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlDataProvider
    {
        get { return base.Container.GetControl<DropDownList>("ddlDataProvider", true); }
    }

    /// <summary>
    /// Gets a reference to the lbContentFields ListBox control.
    /// </summary>
    protected virtual ListBox lbContentFields
    {
        get { return base.Container.GetControl<ListBox>("lbContentFields", true); }
    }

    /// <summary>
    /// Gets a reference to the lbContentFieldsAvailable ListBox control.
    /// </summary>
    protected virtual ListBox lbContentFieldsAvailable
    {
        get { return base.Container.GetControl<ListBox>("lbContentFieldsAvailable", true); }
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

    protected void ddlDataProvider_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string ProviderName = ddlDataProvider.SelectedValue;

        List<String> FldsToDisplay;
        DetermineFldsToDisplay(ProviderName, out FldsToDisplay, string.Empty);

        lbContentFieldsAvailable.DataSource = FldsToDisplay;
        lbContentFieldsAvailable.DataBind();
    }

    private List<string> DetermineFldsToDisplay(String ProviderName, out List<String> FldsToDisplay, string selectedFields)
    {
        List<string> SelectedFlds = new List<string>();
        ContentManager ContentManager = new ContentManager(ProviderName);
        IDictionary<String, IMetaInfo> MetaKeys = ContentManager.MetaKeys;
        FldsToDisplay = new List<String>();
        foreach (String Key in MetaKeys.Keys)
        {
            if (!ImageFlds.Contains(Key))
            {
                if (Key != "Gallery")
                {
                    FldsToDisplay.Add(Key);
                }
            }
            
            if (("," + selectedFields + ",").IndexOf("," + Key + ",") > -1)
            {
                if (!ImageFlds.Contains(Key))
                {
                    if (Key != "Gallery")
                    {
                        SelectedFlds.Add(Key);
                    }
                }
            }
        }
        
        return SelectedFlds;
    }

    private List<string> DetermineAvailableFlds(List<string> FldsToDisplay, string selectedFields)
    {
        List<string> AvailableFlds = new List<string>();
        
        foreach (String fld in FldsToDisplay)
        {
            if (("," + selectedFields + ",").IndexOf("," + fld + ",") == -1)
            {
                if (!ImageFlds.Contains(fld))
                {
                    if (fld != "Gallery")
                    {
                        AvailableFlds.Add(fld);
                    }
                }
            }
        }
        return AvailableFlds;
    }
}
