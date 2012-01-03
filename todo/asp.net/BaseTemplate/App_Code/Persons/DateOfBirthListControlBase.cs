using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;
using Telerik.Cms.Engine.ContentViewFiltering;

/// <summary>
/// Summary description for DateOfBirthListControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("DateOfBirthListDesigner")]
public class DateOfBirthListControlBase : System.Web.UI.UserControl, IFilterableContentControl
{

    #region Properties - niet getoonde properties

    [Browsable(false)]
    public override bool EnableTheming
    {
        get
        {
            return base.EnableTheming;
        }
        set
        {
            base.EnableTheming = value;
        }
    }

    [Browsable(false)]
    public override bool EnableViewState
    {
        get
        {
            return base.EnableViewState;
        }
        set
        {
            base.EnableViewState = value;
        }
    }

    [Browsable(false)]
    public override bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
        }
    }
    
    [Browsable(false)]
    public string FilterExpression
    {
        get
        {
            return this.ItemFilterExpression;
        }
        set
        {
            this.ItemFilterExpression = value;
        }
    }
    
    [Browsable(false)]
    public string ProviderName
    {
        get
        {
            return "Persons";
        }
        set
        {
            this.ProviderName = value;
        }
    }
    #endregion

    #region Properties - Content

    /// <summary>
    /// Gets or sets the title of the control
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title of the control")]
    public string ControlTitle { get; set; }

    /// <summary>
    /// Gets or sets the maximum no of persons to display
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DefaultValue(999),
    DisplayName("Maximum number of persons")]
    public int MaximumNumber { get; set; }

    /// <summary>
    /// Gets or sets the filter expression for the persons
    /// </summary>
    [Browsable(false),
    Category("Content"),
    DefaultValue("Publication_Date <= \"#now\" AND Expiration_Date > \"#now\""),
    DisplayName("Filter expression")]
    public string ItemFilterExpression { get; set; }

    /// <summary>
    /// Gets or sets the field to use as Date of Birth field
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Date of birth field")]
    public string DateOfBirthField { get; set; }

    /// <summary>
    /// Gets or sets the date elements to display
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Date elements to display")]
    public string DateElementsToDisplay { get; set; }

    /// <summary>
    /// Gets or sets the display mode of the list
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Display mode")]
    public string DisplayMode { get; set; }

    /// <summary>
    /// Gets or sets the fields to display
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Fields to display in list")]
    public string FieldsToDisplay { get; set; }

    /// <summary>
    /// Gets or sets the link to the person page
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Item content url"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string ItemLink { get; set; }
    
    #endregion
}
