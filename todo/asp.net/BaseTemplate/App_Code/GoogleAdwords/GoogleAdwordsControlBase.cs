using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

/// <summary>
/// Summary description for GoogleAdwordsControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("GoogleAdwordsDesigner")]
public class GoogleAdwordsControlBase : System.Web.UI.UserControl
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
    /// Gets or sets the Google Adwords code
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Code")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the activation date of the Google Adwords code
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Activation date")]
    public DateTime ActivationDate { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the Google Adwords code
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Expiration date")]
    public DateTime ExpirationDate { get; set; }
    
    /// <summary>
    /// Gets or sets if the Google Adwords code active is
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("Active")]
    public bool Active { get; set; }
    
    #endregion
}
