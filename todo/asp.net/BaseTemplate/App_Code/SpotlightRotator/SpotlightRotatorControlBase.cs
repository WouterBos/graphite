using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Cms.Web.UI;
using Telerik.Framework.Web.Design;

/// <summary>
/// Summary description for SpotlightRotatorControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("SpotlightRotatorDesigner")]
public class SpotlightRotatorControlBase : System.Web.UI.UserControl
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
                return "SpotlightRotator";
            return controlTitle;
        }
        set
        {
            controlTitle = value;
        }
    }

    /// <summary>
    /// Gets or sets the list of items to be displayed by the bulleted list control
    /// </summary>
    [Category("Content")]
    [TypeConverter("SpotlightRotatorItemListConverter, App_Code")]
    public List<SpotlightRotatorItem> ListLinks
    {
        get
        {
            // if listLinks list is null, we'll create an empty list
            // to avoid dealing with null reference in code
            if(listLinks == null)
                listLinks = new List<SpotlightRotatorItem>();
            return listLinks;
        }
        set
        {
            listLinks = value;
        }
    }
    
    #endregion
    
    #region Properties - Appearance
    
    /// <summary>
    /// Direction option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Direction")]
    public DirectionEnum Direction { get; set; }

    /// <summary>
    /// RotatorType option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Rotator type")]
    public Telerik.Web.UI.RotatorType RotatorType { get; set; }

    /// <summary>
    /// ScrollDuration option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Scroll duration (milliseconds)")]
    public int ScrollDuration { get; set; }

    /// <summary>
    /// FrameDuration option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    Description("This option is ignored when the RotatorType is set to a Buttons type."), 
    DisplayName("Frame duration (milliseconds)")]
    public int FrameDuration { get; set; }

    /// <summary>
    /// Width option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Width (px)")]
    public int Width { get; set; }

    /// <summary>
    /// ItemWidth option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Item width (px)")]
    public int ItemWidth { get; set; }

    /// <summary>
    /// Height option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Height (px)")]
    public int Height { get; set; }

    /// <summary>
    /// ItemHeight option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Item height (px)")]
    public int ItemHeight { get; set; }

    /// <summary>
    /// WrapFrames option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Wrap frames")]
    public bool WrapFrames { get; set; }

    /// <summary>
    /// ScrollDirection option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Scroll direction")]
    public Telerik.Web.UI.RotatorScrollDirection ScrollDirection { get; set; }

    /// <summary>
    /// FromCodeButtons option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    Description("This option is only applied when the RotatorType is set to FromCode."), 
    DisplayName("From code buttons")]
    public FromCodeButtonsEnum FromCodeButtons { get; set; }

    /// <summary>
    /// DisplayImageAsBackground option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Display image as background")]
    public bool DisplayImageAsBackground { get; set; }

    /// <summary>
    /// IncludeTitleLink option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Include link on (spotlight) title")]
    public bool IncludeTitleLink { get; set; }

    /// <summary>
    /// CssClass option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    DisplayName("Css class container")]
    public string CssClassContainer { get; set; }

    /// <summary>
    /// AnimationType option
    /// </summary>
    [Browsable(true),
    Category("Appearance"),
    Description("This option is only applied when RotatorType is set to SlideShow or SlideShowButtons."), 
    DisplayName("Animation type")]
    public Telerik.Web.UI.Rotator.AnimationType AnimationType { get; set; }
    
    #endregion
    
    #region Properties - Pager
    
    /// <summary>
    /// DisplayPager option
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    DisplayName("Display pager")]
    public bool DisplayPager { get; set; }

    /// <summary>
    /// PagerIndexChange option
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    Description("This option is only applied when DisplayPager is set to true."), 
    DisplayName("Pager changes the index on")]
    public PagerIndexChangeEnum PagerIndexChange { get; set; }

    /// <summary>
    /// PagerIndexChange option
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    Description("This option is only applied when DisplayPager is set to true."), 
    DisplayName("What to display on the pager buttons")]
    public PagerButtonTextEnum PagerButtonText { get; set; }

    /// <summary>
    /// PagerIndexChange option
    /// </summary>
    [Browsable(true),
    Category("Pager"),
    Description("This option is only applied when PagerIndexChange is set to OnMouseOver."), 
    DisplayName("Include link on pager options")]
    public bool IncludePagerLink { get; set; }
   
   #endregion
    
    private string controlTitle;
    private List<SpotlightRotatorItem> listLinks;
}
