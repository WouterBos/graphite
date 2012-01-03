using System;
using System.Collections;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Telerik.Framework.Web;
using Telerik.Cms.Web.UI;
using Telerik.Web.UI;

/// <summary>
/// Summary description for SpotlightRotatorDesigner
/// </summary>
public class SpotlightRotatorDesigner : Telerik.Framework.Web.Design.ControlDesigner
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the template used by LinksListDesigner control
    /// </summary>
    public ITemplate LayoutTemplate
    {
        get
        {
            return layoutTemplate;
        }
        set
        {
            layoutTemplate = value;
        }
    }

    /// <summary>
    /// Gets or sets the path of the template used by LinksListDesigner control
    /// </summary>
    public virtual string LayoutTemplatePath
    {
        get
        {
            if (ViewState["LayoutTemplatePath"] == null)
                return "~/Estate/UserControls/Designers/SpotlightRotatorDesigner.ascx";
            return ViewState["LayoutTemplatePath"] as string;
        }
        set
        {
            ViewState["LayoutTemplatePath"] = value;
        }
    }

    #endregion

    #region Base Overrides

    /// <summary>
    /// Restores control state information from a previous page request that was saved by the SaveControlState
    /// method.
    /// </summary>
    /// <param name="savedState">Represents the control state to be restored.</param>
    /// <remarks>Notice that this method loads the state from the base class as well</remarks>
    protected override void LoadControlState(object savedState)
    {
        if (savedState != null)
        {
            object[] state = (object[])savedState;
            base.LoadControlState(state[0]);
            temporaryLinksList = (List<SpotlightRotatorItem>)state[1];
        }
    }

    /// <summary>
    /// Saves server control state changes.
    /// </summary>
    /// <returns>Array of objects to be saved with the control state</returns>
    /// <remarks>Notice that this method saves the state for the base class as well</remarks>
    protected override object SaveControlState()
    {
        return new object[] { 
				base.SaveControlState(),
                temporaryLinksList
			};
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
        //Do not render
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
        //Do not render
    }

    /// <summary>
    /// Creates the child controls in LinksListDesigner control.
    /// </summary>
    protected override void CreateChildControls()
    {
        Controls.Clear();

        InitializeTemplate();
        InitializeComponent();
        
        container.TxtTitle.Text = ((SpotlightRotatorControlBase)DesignedControl).ControlTitle;

        container.LinksGrid.MasterTableView.DataKeyNames = new string[] { "Id" };
        container.LinksGrid.DeleteCommand += new GridCommandEventHandler(LinksGrid_DeleteCommand);
        container.LinksGrid.EditCommand += new GridCommandEventHandler(LinksGrid_EditCommand);
        container.LinksGrid.CancelCommand += new GridCommandEventHandler(LinksGrid_CancelCommand);
        container.LinksGrid.UpdateCommand += new GridCommandEventHandler(LinksGrid_UpdateCommand);
        container.LinksGrid.InsertCommand += new GridCommandEventHandler(LinksGrid_InsertCommand);
        container.LinksGrid.NeedDataSource += new GridNeedDataSourceEventHandler(LinksGrid_NeedDataSource);
        container.LinksGrid.ItemCommand += new GridCommandEventHandler(LinksGrid_ItemCommand);
        container.LinksGrid.ItemDataBound += new GridItemEventHandler(LinksGrid_ItemDataBound);

        Controls.Add(container);
    }

    /// <summary>
    /// This method is called by the ControlEditor when the component is about to be saved.
    /// </summary>
    public override void OnSaving()
    {
        component.ControlTitle = container.TxtTitle.Text;
    }
    
    #endregion

    #region Protected Virtual Methods

    /// <summary>
    /// Initializes the template to use. The principle is very similar to how we do it in all the controls
    /// </summary>
    protected virtual void InitializeTemplate()
    {
        container = new SpotlightRotatorDesignerContainer(this);
        layoutTemplate = ControlUtils.GetTemplate<DefaultMyBulletedListDesignerTemplate>(LayoutTemplatePath);
        layoutTemplate.InstantiateIn(container);
    }

    /// <summary>
    /// Initializes the component which is our public control.
    /// </summary>
    /// <remarks>
    /// By "component" we understand the control for which designer is setting properties. By having a 
    /// reference to the "component" we can access or modify the properties of that control / component.
    /// </remarks>
    protected virtual void InitializeComponent()
    {
        if (DesignedControl != null)
        {
            component = (SpotlightRotatorControlBase)DesignedControl;
            properties = TypeDescriptor.GetProperties(component);
        }
    }

    #endregion

    #region Private methods

    private void BindGrid()
    {
        container.LinksGrid.DataSource = temporaryLinksList;
        container.LinksGrid.DataBind();
    }

    private void UpdateLinksList()
    {
        component.ListLinks = temporaryLinksList;
        base.OnPropertyChanged(EventArgs.Empty);
    }

    #endregion

    #region Event Handlers

    private void LinksGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (temporaryLinksList == null)
            temporaryLinksList = component.ListLinks;
        container.LinksGrid.DataSource = temporaryLinksList;
    }

    private void LinksGrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        Hashtable newValues = new Hashtable();
        newValues["Title"] = (userControl.FindControl("txtItemTitle") as TextBox).Text;
        newValues["Subtitle"] = (userControl.FindControl("txtItemSubtitle") as TextBox).Text;
        newValues["Summary"] = (userControl.FindControl("txtItemSummary") as TextBox).Text;
        newValues["Url"] = (userControl.FindControl("txtItemUrl") as TextBox).Text;
        newValues["Linktext"] = (userControl.FindControl("txtItemLinktext") as TextBox).Text;
        newValues["Image"] = (userControl.FindControl("txtItemImage") as TextBox).Text.Split('?')[0];

        temporaryLinksList.Add(new SpotlightRotatorItem(temporaryLinksList.Count + 1, newValues["Title"].ToString(), newValues["Subtitle"].ToString(), newValues["Summary"].ToString(), newValues["Url"].ToString(), newValues["Linktext"].ToString(), newValues["Image"].ToString()));
        UpdateLinksList();
        BindGrid();
    }

    private void LinksGrid_DeleteCommand(object source, GridCommandEventArgs e)
    {
        int id = int.Parse((e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
        if (temporaryLinksList.Exists(i => i.Id == id))
        {
            temporaryLinksList.Remove(temporaryLinksList.Find(i => i.Id == id));
        }
        UpdateLinksList();
        BindGrid();
    }

    private void LinksGrid_EditCommand(object source, GridCommandEventArgs e)
    {
        e.Item.OwnerTableView.Items[e.Item.ItemIndex].Edit = true;
        BindGrid();
    }

    private void LinksGrid_CancelCommand(object source, GridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > 0 && e.Item.OwnerTableView.Items[e.Item.ItemIndex].IsInEditMode)
            e.Item.OwnerTableView.Items[e.Item.ItemIndex].Edit = false;
        BindGrid();
    }

    private void LinksGrid_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        Hashtable newValues = new Hashtable();
        newValues["Id"] = (userControl.FindControl("txtItemId") as TextBox).Text;
        newValues["Title"] = (userControl.FindControl("txtItemTitle") as TextBox).Text;
        newValues["Subtitle"] = (userControl.FindControl("txtItemSubtitle") as TextBox).Text;
        newValues["Summary"] = (userControl.FindControl("txtItemSummary") as TextBox).Text;
        newValues["Url"] = (userControl.FindControl("txtItemUrl") as TextBox).Text;
        newValues["Linktext"] = (userControl.FindControl("txtItemLinktext") as TextBox).Text;
        newValues["Image"] = (userControl.FindControl("txtItemImage") as TextBox).Text;
        int id = int.Parse(newValues["Id"].ToString());
        
        List<SpotlightRotatorItem> updatedListLinks = new List<SpotlightRotatorItem>();        
        foreach (SpotlightRotatorItem link in temporaryLinksList)
        {
            if (link.Id == id)
            {
                updatedListLinks.Add(new SpotlightRotatorItem(int.Parse(newValues["Id"].ToString()), newValues["Title"].ToString(), newValues["Subtitle"].ToString(), newValues["Summary"].ToString(), newValues["Url"].ToString(), newValues["Linktext"].ToString(), newValues["Image"].ToString()));
            }
            else
            {
                updatedListLinks.Add(new SpotlightRotatorItem(link.Id, link.Title, link.Subtitle, link.Summary, link.Url, link.Linktext, link.Image));
            }
        }
        
        temporaryLinksList = updatedListLinks;
        UpdateLinksList();
        BindGrid();
    }

    protected void LinksGrid_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            if (e.CommandName == "Up" || e.CommandName == "Down")
            {
                int id = ((SpotlightRotatorItem)e.Item.DataItem).Id;
                int newid = id + 1;
                if (e.CommandName == "Up")
                {
                    newid = id - 1;
                }

                if (newid >= 0 && newid <= temporaryLinksList.Count - 1)
                { 
                    List<SpotlightRotatorItem> updatedListLinks = new List<SpotlightRotatorItem>();        
                    for (int i = 0; i < temporaryLinksList.Count; i++)
                    {
                        if (e.CommandName == "Down" && i == id)
                        {
                            updatedListLinks.Add(new SpotlightRotatorItem(i, temporaryLinksList[i + 1].Title, temporaryLinksList[i + 1].Subtitle, temporaryLinksList[i + 1].Summary, temporaryLinksList[i + 1].Url, temporaryLinksList[i + 1].Linktext, temporaryLinksList[i + 1].Image));
                            updatedListLinks.Add(new SpotlightRotatorItem(i + 1, temporaryLinksList[i].Title, temporaryLinksList[i].Subtitle, temporaryLinksList[i].Summary, temporaryLinksList[i].Url, temporaryLinksList[i].Linktext,temporaryLinksList[i].Image));

                        }
                        else if (e.CommandName == "Up" && i == id)
                        {
                            updatedListLinks.Add(new SpotlightRotatorItem(i - 1, temporaryLinksList[i].Title, temporaryLinksList[i].Subtitle, temporaryLinksList[i].Summary, temporaryLinksList[i].Url, temporaryLinksList[i].Linktext,temporaryLinksList[i].Image));
                            updatedListLinks.Add(new SpotlightRotatorItem(i, temporaryLinksList[i - 1].Title, temporaryLinksList[i - 1].Subtitle, temporaryLinksList[i - 1].Summary, temporaryLinksList[i - 1].Url, temporaryLinksList[i - 1].Linktext, temporaryLinksList[i - 1].Image));
                        }
                        else if (i != newid)
                        {
                            updatedListLinks.Add(new SpotlightRotatorItem(i, temporaryLinksList[i].Title, temporaryLinksList[i].Subtitle, temporaryLinksList[i].Summary, temporaryLinksList[i].Url, temporaryLinksList[i].Linktext, temporaryLinksList[i].Image));
                        }
                    }

                    temporaryLinksList = updatedListLinks;
                    UpdateLinksList();
                    BindGrid();
                }
            }
        }
    }
    
    protected void LinksGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            int id = ((SpotlightRotatorItem)e.Item.DataItem).Id;
            if (e.Item.ItemIndex == 0)
            {
                Control btn = (Control)dataItem["Up"].Controls[0];
                btn.Visible = false; 
            }
            else if (e.Item.ItemIndex == temporaryLinksList.Count - 1)
            {
                Control btn = (Control)dataItem["Down"].Controls[0];
                btn.Visible = false; 
            }
        }
    }

    #endregion

    #region Protected Fields

    /// <summary>
    /// Gets or sets the myBulletedList designer container.
    /// </summary>
    /// <value>The myBulletedList designer container.</value>
    protected SpotlightRotatorDesignerContainer Container
    {
        get
        {
            return container;
        }
        set
        {
            container = value;
        }
    }

    /// <summary>
    /// Gets or sets the component which is of SpotlightRotatorControlBase type.
    /// </summary>
    /// <value>The component which is of SpotlightRotatorControlBase type.</value>
    protected SpotlightRotatorControlBase Component
    {
        get
        {
            return component;
        }
        set
        {
            component = value;
        }
    }

    #endregion

    #region Private Fields

    private ITemplate layoutTemplate;
    private SpotlightRotatorDesignerContainer container;
    private SpotlightRotatorControlBase component;
    private PropertyDescriptorCollection properties;
    private List<SpotlightRotatorItem> temporaryLinksList;

    #endregion

    #region Default Template

    /// <summary>
    /// Default template for the LinksListDesigner control designer. NOT IMPLEMENTED!
    /// </summary>
    protected class DefaultMyBulletedListDesignerTemplate : ITemplate
    {
        /// <summary>
        /// When implemented by a class, defines the object that child controls and templates belong to. 
        /// These child controls are in turn defined within an inline template.
        /// </summary>
        /// <param name="container">The object to contain the instances of controls from the inline template.</param>
        public void InstantiateIn(Control container)
        {
            throw new NotImplementedException("Default control designer not implemented!");
        }
    }

    #endregion

    #region Container

    /// <summary>
    /// The container class for the LinksListDesigner control designer.
    /// </summary>
    protected class SpotlightRotatorDesignerContainer : GenericContainer<SpotlightRotatorDesigner>
    {
        /// <summary>
        /// Initializes a new instance of the LinksListDesignerContainer class.
        /// </summary>
        /// <param name="owner">The LinksListDesigner control.</param>
        public SpotlightRotatorDesignerContainer(SpotlightRotatorDesigner owner)
            : base(owner, true)
        {
        }

        public RadGrid LinksGrid
        {
            get
            {
                if (linksGrid == null)
                    linksGrid = FindRequiredControl<RadGrid>("linksGrid");
                return linksGrid;
            }
        }
        
        public TextBox TxtTitle
        {
            get
            {
                if (txtTitle == null)
                    txtTitle = FindRequiredControl<TextBox>("txtTitle");
                return txtTitle;
            }
        }
        
        private RadGrid linksGrid;
        private TextBox txtTitle;
    }

    #endregion
}