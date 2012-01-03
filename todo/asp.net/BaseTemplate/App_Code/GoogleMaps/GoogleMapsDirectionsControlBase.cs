using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;

/// <summary>
/// Summary description for GoogleMapsDirectionsControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("GoogleMapsDirectionsDesigner")]
public class GoogleMapsDirectionsControlBase : System.Web.UI.UserControl
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
    /// Gets or sets the title of the user control
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the address to get directions to
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Directions to")]
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the content of information window
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Content information window")]
    public string ContentFields { get; set; }

    /// <summary>
    /// Gets or sets if the route sgould be displayed in a new window
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Display route in new window")]
    public bool DisplayRouteInNewWindow { get; set; }
    
    #endregion
}
