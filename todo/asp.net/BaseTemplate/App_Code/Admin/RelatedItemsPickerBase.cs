using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Cms.Engine;
using System.Collections;

public partial class RelatedItemsPickerBase : System.Web.UI.UserControl, ITextControl
{
    /// <summary>
    /// Property mandated by ITextControl interface which is used by Sitefinity to set or
    /// get the value for the ListID metafield.
    /// </summary>
    public string Text
    {
        get
        {
            return this.hfSelectedItems.Text;
        }
        set
        {
            
            this.hfSelectedItems.Text = value;
        }
    }
    
    public string ProviderName
    {
        get
        {
            return providername;
        }
        set
        {
            
            providername = value;
        }
    }
    
    private string providername = string.Empty;

    /// <summary>
    /// Gets a reference to the text field for the txtControlTitle property.
    /// </summary>
    protected virtual TextBox hfSelectedItems
    {
        get { return (TextBox)this.FindControl("hfSelectedItems"); }
    }
}
