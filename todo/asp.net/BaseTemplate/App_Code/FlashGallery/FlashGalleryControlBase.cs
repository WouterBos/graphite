using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;

/// <summary>
/// Summary description for FlashGalleryControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("FlashGalleryDesigner")]
public class FlashGalleryControlBase : System.Web.UI.UserControl
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
    /// Gets or sets the title of the gallery
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the flash player to be used by the gallery
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Videoplayer to use")]
    public string Videoplayer { get; set; }

    /// <summary>
    /// Gets or sets the gallery containing the flv files
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Gallery containing the flv files")]
    public string FlvGallery { get; set; }

    #endregion
}
