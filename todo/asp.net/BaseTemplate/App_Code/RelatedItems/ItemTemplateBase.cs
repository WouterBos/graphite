using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemTemplateBase
/// </summary>
public partial class ItemTemplateBase<T> : System.Web.UI.UserControl where T : class
{
    public T CurrentItem { get; set; }
    
    public string ItemLink { get; set; }
}
