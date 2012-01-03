using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Cms.Web.UI;
using Telerik.Framework.Web.Design;

/// <summary>
/// Summary description for TickertapeControlBase
/// </summary>
[Telerik.Framework.Web.Design.ControlDesigner("TickertapeDesigner")]
public class TickertapeControlBase : System.Web.UI.UserControl
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
                return "Tickertape";
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
    [TypeConverter("TickerItemListConverter, App_Code")]
    public List<TickerItem> ListLinks
    {
        get
        {
            // if listLinks list is null, we'll create an empty list
            // to avoid dealing with null reference in code
            if(listLinks == null)
                listLinks = new List<TickerItem>();
            return listLinks;
        }
        set
        {
            listLinks = value;
        }
    }
    
    private string controlTitle;
    private List<TickerItem> listLinks;
   #endregion
}
