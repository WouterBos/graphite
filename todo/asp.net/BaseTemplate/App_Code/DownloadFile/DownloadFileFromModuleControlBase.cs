using System.ComponentModel;

/// <summary>
/// Summary description for DownloadFileFromModuleControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("DownloadFileFromModuleDesigner")]
public class DownloadFileFromModuleControlBase : System.Web.UI.UserControl
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
    public string ProviderName
    {
        get
        {
            return "Downloads";
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
    /// Gets or sets the buttontext
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("ButtonText")]
    public string ButtonText { get; set; }

    /// <summary>
    /// Gets or sets the intro
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
    /// Gets or sets the file
    /// </summary>
    [Browsable(true),
    Category("Content"),
    DisplayName("File")]
    public string File { get; set; }
    
    #endregion
}
