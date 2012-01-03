using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;
using Telerik.Cms.Engine.WebControls;
using Telerik.Web;

/// <summary>
/// Summary description for ExternalMovieControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("ExternalMovieDesigner")]
public class ExternalMovieControlBase : System.Web.UI.UserControl
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
    /// Gets or sets the title
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }
 
    /// <summary>
    /// Gets or sets the embedded video code
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("YouTube embed code")]
    public string VideoCode { get; set; }   

    /// <summary>
    /// Display option
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Show in popup")]
    public bool ShowInPopup { get; set; }
 
    /// <summary>
    /// Gets or sets the thumbnail link
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Thumbnail")]
    public string Thumbnail { get; set; }   
 
    #endregion
}
