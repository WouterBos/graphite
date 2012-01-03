using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;

/// <summary>
/// Summary description for LinkElementControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("GoogleMapsListDesigner")]
public class GoogleMapsListControlBase : System.Web.UI.UserControl
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

    #region Properties - map

    /// <summary>
    /// Gets or sets the height of the map
    /// </summary>
    [Browsable(true),
    Category("Map"),
    DisplayName("Height")]
    public string Height { get; set; }

    /// <summary>
    /// Gets or sets the width of the map
    /// </summary>
    [Browsable(true),
    Category("Map"),
    DisplayName("Width")]
    public string Width { get; set; }

    /// <summary>
    /// Gets or sets the geomapping data of the map
    /// </summary>
    [Browsable(true),
    Category("Map"),
    DisplayName("Geomapping data")]
    public string GeomappingData { get; set; }

    /// <summary>
    /// Gets or sets the control(s) on the map
    /// </summary>
    [Browsable(true),
    Category("Map"),
    DisplayName("Available control(s)")]
    public string MapControls { get; set; }

    #endregion

    #region Properties - content

    /// <summary>
    /// Gets or sets the title of the list
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the module of the list
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Markers are retrieved from")]
    public string ProviderName { get; set; }
 
    /// <summary>
    /// Gets or sets the url link of markers
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Marker links to"),
    WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string LinkUrl { get; set; }

    /// <summary>
    /// Gets or sets the text of the url link of markers
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Text of the link")]
    public string LinkText { get; set; }

    /// <summary>
    /// Gets or sets the content of markers
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Marker content is made up from")]
    public string ContentFields { get; set; }
    
    #endregion
}
