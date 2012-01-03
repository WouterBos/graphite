using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Cms.Web.UI;
using Telerik.Framework.Web.Design;
using System.Web.UI;
using Telerik.Cms.Engine.ContentViewFiltering;

/// <summary>
/// Summary description for RelatedItemsControlBase
/// </summary>
///[Telerik.Framework.Web.Design.ControlDesigner("SpotlightRotatorDesigner")]
public class RelatedItemsControlBase : System.Web.UI.UserControl, IFilterableContentControl
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

    #endregion

    #region Properties - content

    /// <summary>
    /// Gets or sets the title of the control
    /// </summary>
    [Category("Content")]
    public string ControlTitle
    {
        get
        {
            // if controlTitle is empty, return default title
            if (controlTitle == null)
                return "RelatedItems";
            return controlTitle;
        }
        set
        {
            controlTitle = value;
        }
    }

    /// <summary>
    /// Gets or sets the no related items text of the control
    /// </summary>
    [Category("Content")]
    public string NoItemsText
    {
        get
        {
            // if text is empty, return default text
            if (noItemsText == null)
                return "No releated items found";
            return noItemsText;
        }
        set
        {
            noItemsText = value;
        }
    }

    /// <summary>
    /// Gets or sets the name of the provider
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Name of data provider")]
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that holds the related items
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Name of field to hold the related items")]
    public string Field { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider of the items
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Name of data provider for the items")]
    public string ItemProviderName { get; set; }

    /// <summary>
    /// Gets or sets the sort expression
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DefaultValue("Title ASC"),
    DisplayName("Sort expression")]
    public string SortExpression { get; set; }

    /// <summary>
    /// Gets or sets the filter expression
    /// </summary>
    [Browsable(false),
    Category("Content"),
    DefaultValue("Publication_Date <= \"#now\" AND Expiration_Date > \"#now\""),
    DisplayName("Filter expression")]
    public string FilterExpression { get; set; }

    /// <summary>
    /// Gets or sets the link to the item page
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Item content url"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string ItemLink { get; set; }
    
    #endregion
    
    #region Properties - Appearance

    /// <summary>
    /// Gets or sets the additional css classes for the user control
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Additional css class(es) for user control")]
    public string CssClass { get; set; }
    
    [PersistenceMode(PersistenceMode.InnerProperty),
    TemplateContainer(typeof(PlaceHolder)),
    Browsable(true),
    Category("Appearance"),
    DefaultValue("~/Estate/UserControls/RelatedItems/ItemTemplate.ascx"),
    DisplayName("Template to use for displaying an item")]
    public string ItemTemplatePath { get; set; }
   
    #endregion

    #region Properties - Pager

    /// <summary>
    /// Gets or sets the no of items to display in the list
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    DefaultValue(10),
    DisplayName("No of items in list")]
    public int NoOfItemsInList { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether the pager should be displayed
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    DisplayName("Display Pager")]
    public bool DisplayPager { get; set; }
 
    /// <summary>
    /// Gets or sets a boolean indicating whether the pager should be rendered as a link
    /// </summary>
    [Browsable(true),
    Category("Pager")]
    public bool RenderPagerAsLink { get; set; }
   
    #endregion
    
    private string controlTitle;
    private string noItemsText;
}
