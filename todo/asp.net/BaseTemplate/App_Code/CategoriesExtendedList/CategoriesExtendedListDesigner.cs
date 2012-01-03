using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Cms.Engine;
using Telerik.Cms.Web.UI;

/// <summary>
/// Summary description for CategoriesExtendedListDesigner
/// </summary>
public class CategoriesExtendedListDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Designers/CategoriesExtendedListDesigner.ascx"; }
    }

    private String NotInUseText;
    private List<String> ImageFlds;

    /// <summary>
    /// Executed when Control Designer is initialized.
    /// </summary>
    protected override void InitializeControls(System.Web.UI.Control viewContainer)
    {
        NotInUseText = ddlTextFld1.Items[0].Text;
        ImageFlds = new List<String>();
        ImageFlds.Add("Image");
        ImageFlds.Add("Picture");
        ImageFlds.Add("Thumb");
        ImageFlds.Add("Thumbnail");
        ImageFlds.Add("Testimonial_Picture");

        txtControlTitle.Text = ((CategoriesExtendedListControlBase)DesignedControl).ControlTitle;
        
        txtImageToDisplayWhenImageFldNotFilledIn.Text = ((CategoriesExtendedListControlBase)DesignedControl).ItemNoImageFilledInImage;

        cbDisplayCategoriesWithNoItems.Checked = ((CategoriesExtendedListControlBase)DesignedControl).CategoriesWithNoItems;

        cbDisplayNoOfItems.Checked = ((CategoriesExtendedListControlBase)DesignedControl).NoOfItems;

        cbDisplayItemNumber.Checked = ((CategoriesExtendedListControlBase)DesignedControl).ItemNumber;

        ListItem item = ddlNameOfTextAfterNoOfItems.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).NoOfItemsText);
        if (item != null)
        {
            ddlNameOfTextAfterNoOfItems.SelectedIndex = ddlNameOfTextAfterNoOfItems.Items.IndexOf(item);
        }

        txtItemContentUrl.Text = ((CategoriesExtendedListControlBase)DesignedControl).ItemLink;

        IDictionary<string, ContentProviderBase> providers = ContentManager.Providers;
        foreach (KeyValuePair<string, ContentProviderBase> provider in providers)
        {
            ddlDataProvider.Items.Add(provider.Key.ToString());
        }
        ddlDataProvider.SelectedIndexChanged += ddlDataProvider_SelectedIndexChanged;
        ddlDataProvider.AutoPostBack = true;
        
        String ProviderName = ((CategoriesExtendedListControlBase)DesignedControl).ProviderName;
        if (!String.IsNullOrEmpty(ProviderName))
        {
            item = ddlDataProvider.Items.FindByValue(ProviderName);
            if (item != null)
            {
                ddlDataProvider.SelectedIndex = ddlDataProvider.Items.IndexOf(item);
            }

            List<String> FldsToDisplay;
            List<String> ImageFldsToDisplay;
            DetermineFldsToDisplay(ProviderName, out FldsToDisplay, out ImageFldsToDisplay);

            ddlTextFld1.DataSource = FldsToDisplay;
            ddlTextFld1.DataBind();
            item = ddlTextFld1.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).ItemText1);
            if (item != null)
            {
                ddlTextFld1.SelectedIndex = ddlTextFld1.Items.IndexOf(item);
            }

            ddlTextFld2.DataSource = FldsToDisplay;
            ddlTextFld2.DataBind();
            item = ddlTextFld2.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).ItemText2);
            if (item != null)
            {
                ddlTextFld2.SelectedIndex = ddlTextFld2.Items.IndexOf(item);
            }

            ddlImage1.DataSource = ImageFldsToDisplay;
            ddlImage1.DataBind();
            item = ddlImage1.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).ItemImage1);
            if (item != null)
            {
                ddlImage1.SelectedIndex = ddlImage1.Items.IndexOf(item);
            }

            ddlTextFld3.DataSource = FldsToDisplay;
            ddlTextFld3.DataBind();
            item = ddlTextFld3.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).ItemText3);
            if (item != null)
            {
                ddlTextFld3.SelectedIndex = ddlTextFld3.Items.IndexOf(item);
            }

            ddlTextFld4.DataSource = FldsToDisplay;
            ddlTextFld4.DataBind();
            item = ddlTextFld4.Items.FindByValue(((CategoriesExtendedListControlBase)DesignedControl).ItemText4);
            if (item != null)
            {
                ddlTextFld4.SelectedIndex = ddlTextFld4.Items.IndexOf(item);
            }
        }
    }

    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((CategoriesExtendedListControlBase)DesignedControl).ControlTitle = txtControlTitle.Text;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemNoImageFilledInImage = txtImageToDisplayWhenImageFldNotFilledIn.Text;
        ((CategoriesExtendedListControlBase)DesignedControl).CategoriesWithNoItems = cbDisplayCategoriesWithNoItems.Checked;
        ((CategoriesExtendedListControlBase)DesignedControl).NoOfItems = cbDisplayNoOfItems.Checked;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemNumber = cbDisplayItemNumber.Checked;
        ((CategoriesExtendedListControlBase)DesignedControl).NoOfItemsText = ddlNameOfTextAfterNoOfItems.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemLink = txtItemContentUrl.Text;
        ((CategoriesExtendedListControlBase)DesignedControl).ProviderName = ddlDataProvider.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemText1 = ddlTextFld1.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemText2 = ddlTextFld2.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemImage1 = ddlImage1.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemText3 = ddlTextFld3.SelectedValue;
        ((CategoriesExtendedListControlBase)DesignedControl).ItemText4 = ddlTextFld4.SelectedValue;
    }

    private void DetermineFldsToDisplay(String ProviderName, out List<String> FldsToDisplay, out List<String> ImageFldsToDisplay)
    {
        ContentManager ContentManager = new ContentManager(ProviderName);
        IDictionary<String, IMetaInfo> MetaKeys = ContentManager.MetaKeys;
        FldsToDisplay = new List<String>();
        FldsToDisplay.Add(NotInUseText);
        ImageFldsToDisplay = new List<String>();
        ImageFldsToDisplay.Add(NotInUseText);
        foreach (String Key in MetaKeys.Keys)
        {
            if (!ImageFlds.Contains(Key))
            {
                if (Key != "Gallery")
                {
                    FldsToDisplay.Add(Key);
                }
            }
            else
            {
                ImageFldsToDisplay.Add(Key);
            }
        }
        if (ProviderName == "Libraries")
        {
            ImageFldsToDisplay.Add("Image");
            ImageFldsToDisplay.Add("Thumbnail");
        }
    }

    protected void ddlDataProvider_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string ProviderName = ddlDataProvider.SelectedValue;

        List<String> FldsToDisplay;
        List<String> ImageFldsToDisplay;
        DetermineFldsToDisplay(ProviderName, out FldsToDisplay, out ImageFldsToDisplay);

        ddlTextFld1.DataSource = FldsToDisplay;
        ddlTextFld1.DataBind();
        ListItem item = ddlTextFld1.Items.FindByValue(NotInUseText);
        if (item != null)
        {
            ddlTextFld1.SelectedIndex = ddlTextFld1.Items.IndexOf(item);
        }

        ddlTextFld2.DataSource = FldsToDisplay;
        ddlTextFld2.DataBind();
        item = ddlTextFld2.Items.FindByValue(NotInUseText);
        if (item != null)
        {
            ddlTextFld2.SelectedIndex = ddlTextFld2.Items.IndexOf(item);
        }

        ddlImage1.DataSource = ImageFldsToDisplay;
        ddlImage1.DataBind();
        item = ddlImage1.Items.FindByValue(NotInUseText);
        if (item != null)
        {
            ddlImage1.SelectedIndex = ddlImage1.Items.IndexOf(item);
        }

        ddlTextFld3.DataSource = FldsToDisplay;
        ddlTextFld3.DataBind();
        item = ddlTextFld3.Items.FindByValue(NotInUseText);
        if (item != null)
        {
            ddlTextFld3.SelectedIndex = ddlTextFld3.Items.IndexOf(item);
        }

        ddlTextFld4.DataSource = FldsToDisplay;
        ddlTextFld4.DataBind();
        item = ddlTextFld4.Items.FindByValue(NotInUseText);
        if (item != null)
        {
            ddlTextFld4.SelectedIndex = ddlTextFld4.Items.IndexOf(item);
        }
    } 

    /// <summary>
    /// Gets a reference to the text field for the txtControlTitle property.
    /// </summary>
    protected virtual TextBox txtControlTitle
    {
        get { return base.Container.GetControl<TextBox>("txtControlTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the data provider DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlDataProvider
    {
        get { return base.Container.GetControl<DropDownList>("ddlDataProvider", true); }
    }

    /// <summary>
    /// Gets a reference to the text field 1 DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlTextFld1
    {
        get { return base.Container.GetControl<DropDownList>("ddlTextFld1", true); }
    }

    /// <summary>
    /// Gets a reference to the text field 2 DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlTextFld2
    {
        get { return base.Container.GetControl<DropDownList>("ddlTextFld2", true); }
    }

    /// <summary>
    /// Gets a reference to the image field 1 DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlImage1
    {
        get { return base.Container.GetControl<DropDownList>("ddlImage1", true); }
    }

    /// <summary>
    /// Gets a reference to the text field 3 DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlTextFld3
    {
        get { return base.Container.GetControl<DropDownList>("ddlTextFld3", true); }
    }

    /// <summary>
    /// Gets a reference to the text field 4 DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlTextFld4
    {
        get { return base.Container.GetControl<DropDownList>("ddlTextFld4", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtImageToDisplayWhenImageFldNotFilledIn property.
    /// </summary>
    protected virtual TextBox txtImageToDisplayWhenImageFldNotFilledIn
    {
        get { return base.Container.GetControl<TextBox>("txtImageToDisplayWhenImageFldNotFilledIn", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the DisplayCategoriesWithNoItems property.
    /// </summary>
    protected virtual CheckBox cbDisplayCategoriesWithNoItems
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayCategoriesWithNoItems", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the DisplayNoOfItems property.
    /// </summary>
    protected virtual CheckBox cbDisplayNoOfItems
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayNoOfItems", true); }
    }

    /// <summary>
    /// Gets a reference to the checkbox for the DisplayItemNumber property.
    /// </summary>
    protected virtual CheckBox cbDisplayItemNumber
    {
        get { return base.Container.GetControl<CheckBox>("cbDisplayItemNumber", true); }
    }

    /// <summary>
    /// Gets a reference to the name of the text after the no of items DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlNameOfTextAfterNoOfItems
    {
        get { return base.Container.GetControl<DropDownList>("ddlNameOfTextAfterNoOfItems", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtItemContentUrl property.
    /// </summary>
    protected virtual TextBox txtItemContentUrl
    {
        get { return base.Container.GetControl<TextBox>("txtItemContentUrl", true); }
    }
}