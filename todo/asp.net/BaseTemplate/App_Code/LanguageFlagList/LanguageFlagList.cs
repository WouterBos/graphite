using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Localization.WebControls;

/// <summary>
/// This class inherits from LanguageList to extend it and add ImageUrls to the hyperlinks.
/// It is used by LanguageBar to render the contents of the language selector control.
/// </summary>
public class LanguageFlagList : LanguageList
{
    private bool Active = false;
    
    public LanguageFlagList(bool active)
    {
        this.Active = active;
    }

    // a collection of all the controls that represents list items (Hyperlinks)
    private List<Control> listItems;

    // fill the collection - get all child controls with ID listItem recursively
    private void FillItems(Control root)
    {
        foreach(Control child in root.Controls)
        {
            if (child != null && child.ID == "listItem")
            {
                listItems.Add(child);
            }
            // recursive call
            FillItems(child);
        }
    }

    // override this method to fill the collection of HyperLinks
    protected override void CreateChildControls()
    {
        // call the base method
        base.CreateChildControls();
        // initialize and fill the collection recursively
        this.listItems = new List<Control>();
        this.FillItems(this);
    }

    // after the child controls have been created, go through the list items and set the image path.
    protected override void OnPreRender(EventArgs e)
    {
        // call the base method 
        base.OnPreRender(e);
        // loop through the items
        for (int i = 0; i < this.Items.Count; i++)
        {
            // check every item in the collection that was filled in CreateChildControls(). If it is a Hyperlink,
            // set the ImageURL property.
            
            ListItem item = this.Items[i];
            Control ctrl = this.listItems[i];
            if (ctrl is HyperLink)
            {
                HyperLink lnk = (HyperLink)ctrl;
                lnk.Enabled = this.Active;
                lnk.Text = this.Items[i].Value.ToUpper();
                // item.Value is the culture code, the full path for english would be ~/Images/en.gif, german ~/Images/de.gif
                // and so on. In order for the control to display flags, images should be already uploaded in the specified folder
                // This path can be changed as desired.
                //lnk.ImageUrl = "~/Images/" + item.Value + ".jpg";
            }
        }
    }
}

public class MyLanguageBar : LanguageBar
{
    public void Page_Load(object sender, EventArgs e)
    {
        this.LanguageChanged += new EventHandler<LanguageChangedEventArgs>(MyLanguageBar_LanguageChanged);
    }

    void MyLanguageBar_LanguageChanged(object sender, LanguageChangedEventArgs e)
    {
            //add logic for creating language cookie
    }
}

