using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Telerik.Cms.Web.UI;

/// <summary>
/// Summary description for FlashVideoControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("FlashVideoDesigner")]
public class FlashVideoControlBase : System.Web.UI.UserControl
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

    /// <summary>
    /// Gets or sets the flash player to be used
    /// </summary>
    [Browsable(false)]
    public string Videoplayer { get; set; }

    #endregion

    #region Properties - content

    /// <summary>
    /// Gets or sets the title of the video
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the flv file
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("The Flash video file")]
    public string FlvFile { get; set; }

    /// <summary>
    /// Gets or sets the subtitles file
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("The subtitles file")]
    public string SubtitlesFile { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("The thumbnail")]
    public string Thumbnail { get; set; }

    #endregion
}