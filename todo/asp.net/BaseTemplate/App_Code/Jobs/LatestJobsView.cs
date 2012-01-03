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
public class LatestJobsView : Estate.SitefinityModules.Jobs.WebControls.JobsPublicView
{
    
    #region Properties
    
    public override string ItemListTemplatePath
    {
        get
        {
            return "~/Estate/UserControls/Jobs/LatestJobsList.ascx";
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
            return "~/Estate/UserControls/Jobs/LatestJobsDetails.ascx";
        }
        set
        {
            base.SingleItemTemplatePath = value;
        }
    }

    [Browsable(true),
    Category("Misc"),
    Description("Redirect to this page when there is no job available"),
    Telerik.Cms.Web.UI.WebEditor("Telerik.Cms.Web.UI.CmsUrlWebEditor, Telerik.Cms")]
    public string RedirectPage { get; set; }
    
    #endregion
    
    protected override void CreateChildControls()
    {
        var man = new ContentManager("Jobs");
        //IList listofLatest = man.GetContent(this.SortExpression);
        IList listofLatest = null;
        if (!string.IsNullOrEmpty(this.FilterExpression))
        {
            ContentFilterBuilder itemFilters = new ContentFilterBuilder(this);
            
            foreach (IContentFilterStatement itemFilter in itemFilters.ParseInternal(true))
            {
                itemFilters.AddFilter(itemFilter);
            }
            
            listofLatest = man.GetContent(0, 1, this.SortExpression, itemFilters.ParseMetaFieldsFilter());
        }
        else
        {
            listofLatest = man.GetContent(this.SortExpression);
        }
        
        bool jobToDisplay = false;
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
                    jobToDisplay = true;
                    var filterBuilder = new ContentFilterBuilder(this);  
                    filterBuilder.AddFilter(new ContentFilterStatement("ID", itemId.ToString(), ContentFilter.Condition.Equal, ContentFilter.JoinType.And));  
                    this.BehaviorMode = BehaviorModes.Detail;  
                    this.CreateSingleContent();  
                    Controls.Add(SingleContainer);
                    
                    // save item id for future use
                    Output.HttpContextValue("LatestJobs", itemId.ToString());
                }
            }
        }
        
        if (!jobToDisplay && !string.IsNullOrEmpty(this.RedirectPage))
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
