using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;
using Telerik.Cms.Engine.ContentViewFiltering;

/// <summary>
/// Summary description for CategoriesExtendedListControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("CategoriesExtendedListDesigner")]
public class CategoriesExtendedListControlBase : System.Web.UI.UserControl, IFilterableContentControl
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
    #endregion

    #region Properties - Categories

    /// <summary>
    /// Gets or sets the title of the control
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue("CategoriesExtendedList"),
    DisplayName("Title of the control")]
    public string ControlTitle { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider to supply the data
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DisplayName("Name of data provider")]
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the maximum no of categories to display
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue(999),
    DisplayName("Maximum number of categories")]
    public int MaximumNumber { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for the categories
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue("CategoryName ASC"),
    DisplayName("Sort expression")]
    public string SortExpression { get; set; }

    /// <summary>
    /// Gets or sets if the categories with no items should be included in the list
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue(true),
    DisplayName("Display categories with no items")]
    public bool CategoriesWithNoItems { get; set; }

    /// <summary>
    /// Gets or sets if the number of items should be displayed after the category name
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue(true),
    DisplayName("Display number of items")]
    public bool NoOfItems { get; set; }

    /// <summary>
    /// Gets or sets the name of the text in the local resource file to use after the number of items
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DefaultValue("litNoOfItems"),
    DisplayName("Name of text to be used after no of items")]
    public string NoOfItemsText { get; set; }

    /// <summary>
    /// Gets or sets the link to the category list page
    /// </summary>
    [Browsable(true),
    Category("Categories"),
    DisplayName("Category list page"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string CategoryLink { get; set; }

    #endregion

    #region Properties - Items

    /// <summary>
    /// Gets or sets if a item number should be display before every item
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DefaultValue(true),
    DisplayName("Display item number")]
    public bool ItemNumber { get; set; }

    /// <summary>
    /// Gets or sets the filter expression for the items
    /// </summary>
    [Browsable(false),
    Category("Items"),
    DefaultValue("Publication_Date <= \"#now\" AND Expiration_Date > \"#now\""),
    DisplayName("Filter expression")]
    public string ItemFilterExpression { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for the items
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DefaultValue("Publication_Date DESC"),
    DisplayName("Sort expression")]
    public string ItemSortExpression { get; set; }

    /// <summary>
    /// Gets or sets the maximum no of items to display per category
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DefaultValue(999),
    DisplayName("Maximum number of items")]
    public int ItemMaximumNumber { get; set; }

    /// <summary>
    /// Gets or sets the field to display in text field 1
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Content for text field 1 determined by")]
    public string ItemText1 { get; set; }

    /// <summary>
    /// Gets or sets the field to display in text field 2
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Content for text field 2 determined by")]
    public string ItemText2 { get; set; }

    /// <summary>
    /// Gets or sets the field to display in image field 1
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Content of image field determined by")]
    public string ItemImage1 { get; set; }

    /// <summary>
    /// Gets or sets the image to display if image field 1 is not filled in
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Image when image field not filled in"),
    WebEditor("Telerik.Libraries.WebControls.ImageSelector, Telerik.Libraries")]
    public string ItemNoImageFilledInImage { get; set; }

    /// <summary>
    /// Gets or sets the field to display in text field 3
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Content for text field 3 determined by")]
    public string ItemText3 { get; set; }

    /// <summary>
    /// Gets or sets the field to display in text field 4
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Content for text field 4 determined by")]
    public string ItemText4 { get; set; }

    /// <summary>
    /// Gets or sets the link to the item page
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Item content url"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string ItemLink { get; set; }

    /// <summary>
    /// Gets or sets the field in the item containing the item url
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Item field containing link url")]
    public string ItemLinkField { get; set; }

    /// <summary>
    /// Gets or sets the field in the item containing the item url target
    /// </summary>
    [Browsable(true),
    Category("Items"),
    DisplayName("Item field containing link url target")]
    public string ItemLinkTargetField { get; set; }
    
    #endregion
}
