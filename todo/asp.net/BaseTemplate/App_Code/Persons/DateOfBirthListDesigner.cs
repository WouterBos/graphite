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
/// Summary description for DateOfBirthListDesigner
/// </summary>
public class DateOfBirthListDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    private List<String> ImageFlds;
    private List<String> FldsToDisplay;
    
    /// <summary>
    /// Loads an external template that contains the controls & UI for the Control Designer.
    /// </summary>
    public override string LayoutTemplatePath
    {
        get { return "~/Estate/UserControls/Persons/Designers/DateOfBirthListDesigner.ascx"; }
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
        
        txtControlTitle.Text = ((DateOfBirthListControlBase)DesignedControl).ControlTitle;

        txtMaximumNumber.Text = ((DateOfBirthListControlBase)DesignedControl).MaximumNumber.ToString();
        
        FldsToDisplay = new List<String>();
        List<string> DateFldsToDisplay = DetermineFldsToDisplay("Persons");
        
        ddlDateOfBirthField.DataSource = DateFldsToDisplay;
        ddlDateOfBirthField.DataBind();

        ListItem item = ddlDateOfBirthField.Items.FindByValue(((DateOfBirthListControlBase)DesignedControl).DateOfBirthField);
        if (item != null)
        {
            ddlDateOfBirthField.SelectedIndex = ddlDateOfBirthField.Items.IndexOf(item);
        }

        item = ddlDisplayMode.Items.FindByValue(((DateOfBirthListControlBase)DesignedControl).DisplayMode);
        if (item != null)
        {
            ddlDisplayMode.SelectedIndex = ddlDisplayMode.Items.IndexOf(item);
        }

        txtDateElementsToDisplay.Text = ((DateOfBirthListControlBase)DesignedControl).DateElementsToDisplay;
        
        lbDateElementsToDisplayAvailable.Items.Add(new ListItem("Day","Day"));
        lbDateElementsToDisplayAvailable.Items.Add(new ListItem("Month","Month"));
        lbDateElementsToDisplayAvailable.Items.Add(new ListItem("Year","Year"));
        
        string dateElementsToDisplay = ((DateOfBirthListControlBase)DesignedControl).DateElementsToDisplay;
        if (!string.IsNullOrEmpty(dateElementsToDisplay))
        {
            txtDateElementsToDisplay.Text = dateElementsToDisplay;
            
            string[] arrDateElementsToDisplay = dateElementsToDisplay.Split(',');
            for (int i = 0; i < arrDateElementsToDisplay.Length; i++)
            {
                lbDateElementsToDisplay.Items.Add(new ListItem(arrDateElementsToDisplay[i], arrDateElementsToDisplay[i]));
            }
        }
        
        List<string> AvailableFlds = DetermineAvailableFlds(FldsToDisplay, ((DateOfBirthListControlBase)DesignedControl).FieldsToDisplay);
        
        lbFieldsToDisplayAvailable.DataSource = AvailableFlds;
        lbFieldsToDisplayAvailable.DataBind();
        
        string fieldsToDisplay = ((DateOfBirthListControlBase)DesignedControl).FieldsToDisplay;
        if (!String.IsNullOrEmpty(fieldsToDisplay))
        {
            txtFieldsToDisplay.Text = fieldsToDisplay;
            
            string[] arrFieldsToDisplay = fieldsToDisplay.Split(',');
            for (int i = 0; i < arrFieldsToDisplay.Length; i++)
            {
                lbFieldsToDisplay.Items.Add(new ListItem(arrFieldsToDisplay[i], arrFieldsToDisplay[i]));
            }
        }

        txtItemContentUrl.Text = ((DateOfBirthListControlBase)DesignedControl).ItemLink;
    }

    /// <summary>
    /// Executed automatically when the I'm done button is clicked in the Control Designer.
    /// </summary>
    public override void OnSaving()
    {
        ((DateOfBirthListControlBase)DesignedControl).ControlTitle = txtControlTitle.Text;
        
        int maxNo = 0;
        if (int.TryParse(txtMaximumNumber.Text, out maxNo))
        {
            ((DateOfBirthListControlBase)DesignedControl).MaximumNumber = maxNo;
        }
        else
        {
            ((DateOfBirthListControlBase)DesignedControl).MaximumNumber = 999;
        }
        
        ((DateOfBirthListControlBase)DesignedControl).DateOfBirthField = ddlDateOfBirthField.SelectedValue;
        ((DateOfBirthListControlBase)DesignedControl).DisplayMode = ddlDisplayMode.SelectedValue;
        ((DateOfBirthListControlBase)DesignedControl).DateElementsToDisplay = txtDateElementsToDisplay.Text;
        ((DateOfBirthListControlBase)DesignedControl).FieldsToDisplay = txtFieldsToDisplay.Text;
        ((DateOfBirthListControlBase)DesignedControl).ItemLink = txtItemContentUrl.Text;
    }

    private List<string> DetermineFldsToDisplay(String ProviderName)
    {
        List<string> DateFldsToDisplay = new List<string>();
        ContentManager ContentManager = new ContentManager(ProviderName);
        IDictionary<String, IMetaInfo> MetaKeys = ContentManager.MetaKeys;
        
        foreach (String Key in MetaKeys.Keys)
        {
            //if (!ImageFlds.Contains(Key))
            //{
                if (Key != "Gallery")
                {
                    FldsToDisplay.Add(Key);
                }
            //}
            
            if (MetaKeys[Key].ValueType == MetaValueTypes.DateTime)
            {
                DateFldsToDisplay.Add(Key);
            }
        }
        
        return DateFldsToDisplay;
    }

    private List<string> DetermineAvailableFlds(List<string> FldsToDisplay, string selectedFields)
    {
        List<string> AvailableFlds = new List<string>();
        
        foreach (String fld in FldsToDisplay)
        {
            if (("," + selectedFields + ",").IndexOf("," + fld + ",") == -1)
            {
                //if (!ImageFlds.Contains(fld))
                //{
                    if (fld != "Gallery")
                    {
                        AvailableFlds.Add(fld);
                    }
                //}
            }
        }
        return AvailableFlds;
    }

    /// <summary>
    /// Gets a reference to the text field for the txtControlTitle property.
    /// </summary>
    protected virtual TextBox txtControlTitle
    {
        get { return base.Container.GetControl<TextBox>("txtControlTitle", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtMaximumNumber property.
    /// </summary>
    protected virtual TextBox txtMaximumNumber
    {
        get { return base.Container.GetControl<TextBox>("txtMaximumNumber", true); }
    }

    /// <summary>
    /// Gets a reference to the ddlDateOfBirthField DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlDateOfBirthField
    {
        get { return base.Container.GetControl<DropDownList>("ddlDateOfBirthField", true); }
    }

    /// <summary>
    /// Gets a reference to the ddlDisplayMode DropDownList control.
    /// </summary>
    protected virtual DropDownList ddlDisplayMode
    {
        get { return base.Container.GetControl<DropDownList>("ddlDisplayMode", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtDateElementsToDisplay property.
    /// </summary>
    protected virtual TextBox txtDateElementsToDisplay
    {
        get { return base.Container.GetControl<TextBox>("txtDateElementsToDisplay", true); }
    }

    /// <summary>
    /// Gets a reference to the lbDateElementsToDisplayAvailable ListBox control.
    /// </summary>
    protected virtual ListBox lbDateElementsToDisplayAvailable
    {
        get { return base.Container.GetControl<ListBox>("lbDateElementsToDisplayAvailable", true); }
    }

    /// <summary>
    /// Gets a reference to the lbDateElementsToDisplay ListBox control.
    /// </summary>
    protected virtual ListBox lbDateElementsToDisplay
    {
        get { return base.Container.GetControl<ListBox>("lbDateElementsToDisplay", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtFieldsToDisplay property.
    /// </summary>
    protected virtual TextBox txtFieldsToDisplay
    {
        get { return base.Container.GetControl<TextBox>("txtFieldsToDisplay", true); }
    }

    /// <summary>
    /// Gets a reference to the lbFieldsToDisplayAvailable ListBox control.
    /// </summary>
    protected virtual ListBox lbFieldsToDisplayAvailable
    {
        get { return base.Container.GetControl<ListBox>("lbFieldsToDisplayAvailable", true); }
    }

    /// <summary>
    /// Gets a reference to the lbFieldsToDisplay ListBox control.
    /// </summary>
    protected virtual ListBox lbFieldsToDisplay
    {
        get { return base.Container.GetControl<ListBox>("lbFieldsToDisplay", true); }
    }

    /// <summary>
    /// Gets a reference to the text field for the txtItemContentUrl property.
    /// </summary>
    protected virtual TextBox txtItemContentUrl
    {
        get { return base.Container.GetControl<TextBox>("txtItemContentUrl", true); }
    }
}