using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;
using Telerik.Cms.Engine.WebControls;
using Telerik.Web;

/// <summary>
/// Summary description for LinkElementControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("LinkElementDesigner")]
public class LinkElementControlBase : System.Web.UI.UserControl
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
    /// Gets or sets the title of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the subtitle of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Subtitle")]
    public string Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the intro of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Intro")]
    public string Intro
    {
        get
        {
            object obj = this.ViewState["Intro"];
            if (obj != null)
                return (string)obj;
            return "Enter some content here";
        }
        set
        {
            this.ViewState["Intro"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the thumbnail of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Thumbnail"),
    WebEditor("Telerik.Libraries.WebControls.ImageSelector, Telerik.Libraries")]
    public string Thumbnail { get; set; }
   
    /// <summary>
    /// Gets or sets if the new overlay should be displayed
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Display new overlay")]
    public bool DisplayNewOverlay { get; set; }

    /// <summary>
    /// Gets or sets if the video overlay should be displayed
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Display video overlay")]
    public bool DisplayVideoOverlay { get; set; }
 
    /// <summary>
    /// Gets or sets the url link of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Url to link to"),
    WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string LinkUrl { get; set; }

    /// <summary>
    /// Gets or sets the target of the url link of the element
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DefaultValue("_top"),
    DisplayName("Target of the link")]
    public string LinkTarget { get; set; }
    
    /// <summary>
    /// Gets or sets if a row click event should be added
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Add row click event")]
    public bool AddRowClickEvent { get; set; }
    
    #endregion
}
