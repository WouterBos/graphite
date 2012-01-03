using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.News;
using System.Collections;
using Telerik.Cms.Engine;
using Telerik.Cms.Engine.ContentViewFiltering;
using Telerik.Web;
using System.ComponentModel;
using Telerik.Cms.Web;

/// <summary>
/// Summary description for LatestNewsView
/// </summary>
public class LatestNewsView : Telerik.News.WebControls.NewsView
{
    
    #region Properties
    
    public override string ItemListTemplatePath
    {
        get
        {
            return "~/Estate/UserControls/News/LatestNewsList.ascx";
        }
        set
        {
            base.ItemListTemplatePath = value;
        }
    }

    public override string SingleItemTemplatePath
    {
        get
        {
            return "~/Estate/UserControls/News/LatestNewsDetails.ascx";
        }
        set
        {
            base.SingleItemTemplatePath = value;
        }
    }

    [Browsable(true),
    Category("Misc"),
    Description("Redirect to this page when there is now news available"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string RedirectPage { get; set; }
    
    #endregion
    
    protected override void CreateChildControls()
    {
        var man = new NewsManager("News");  
        //get latest news item.  
        //IList listofLatest = man.Content.GetContent(this.SortExpression);
        IList listofLatest = null;
        if (!string.IsNullOrEmpty(this.FilterExpression))
        {
            ContentFilterBuilder itemFilters = new ContentFilterBuilder(this);
            
            foreach (IContentFilterStatement itemFilter in itemFilters.ParseInternal(true))
            {
                itemFilters.AddFilter(itemFilter);
            }
            
            listofLatest = man.Content.GetContent(0, 1, this.SortExpression, itemFilters.ParseMetaFieldsFilter());
        }
        else
        {
            listofLatest = man.Content.GetContent(this.SortExpression);
        }
        
        bool newsToDisplay = false;
        if (listofLatest.Count > 0)
        {
            var latestContent = listofLatest[0] as IContent;  
            if (latestContent != null)  
            {  
                //set the item ID
                if (Context.Items.Contains(this.Manager.Provider.ContentItemKey))
                    itemId = (Guid)((IUrlRewriteData)Context.Items[Manager.Provider.ContentItemKey]).Data;
                else if (!String.IsNullOrEmpty(Context.Request[ContentItemKey]))
                    itemId = new Guid(Context.Request[ContentItemKey]);
                else
                {
                    itemId = latestContent.ID;
                }
           
                if (this.itemId != Guid.Empty && SingleModeSupported)
                {
                    newsToDisplay = true;
                    var filterBuilder = new ContentFilterBuilder(this);  
                    filterBuilder.AddFilter(new ContentFilterStatement("ID", itemId.ToString(), ContentFilter.Condition.Equal, ContentFilter.JoinType.And));  
                    this.BehaviorMode = BehaviorModes.Detail;  
                    this.CreateSingleContent();  
                    Controls.Add(SingleContainer);
                    
                    // save item id for future use
                    Output.HttpContextValue("LatestNews", itemId.ToString());
			    }
            }  
        }
        
        if (!newsToDisplay && !string.IsNullOrEmpty(this.RedirectPage))
        {
            CmsPageBase cmsPage = (CmsPageBase)this.Page;
            if (cmsPage.PageMode == CmsPageMode.Live)
            {
                HttpContext.Current.Response.Redirect(this.RedirectPage, true);
            }
        }
        
        base.CreateChildControls();
    }

}
