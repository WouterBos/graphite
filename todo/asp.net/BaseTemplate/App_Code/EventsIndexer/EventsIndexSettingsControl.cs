using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Cms.Web.UI;
using Telerik.Framework.Search;
using Telerik.Framework.Web;
using System;
using System.ComponentModel;

namespace EventIndexer
{
    public class EventsIndexSettingsControl : CompositeControl, ISettingsControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Control Template path
        /// </summary>
        public string ControlTemplatePath
        {
            get
            {
                object o = ViewState["ControlTemplatePath"];
                if (o == null)
                    return "~/Sitefinity/Admin/ControlTemplates/Events/EventsIndexSettingsControl.ascx";
                return (string)o;
            }
            set
            {
                ViewState["ControlTemplatePath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the control template
        /// </summary>
        public ITemplate ControlTemplate
        {
            get
            {
                if (controlTemplate == null)
                    controlTemplate = ControlUtils.GetTemplate<DefaultTemplate>(ControlTemplatePath);
                return controlTemplate;
            }
            set
            {
                controlTemplate = value;
            }
        }

        /// <summary>
        /// Gets or Sets the name of the provider
        /// </summary>
        public string ProviderName
        {
            get
            {
                object o = ViewState["ProviderName"];
                if (o == null)
                    return String.Empty;
                return (string)o;
            }
            set
            {
                ViewState["ProviderName"] = value;
            }
        }


        /// <summary>
        /// Gets or Sets the page Url for displaying the events item
        /// </summary>
        public string SingleEventUrl
        {
            get
            {
                if (ctrlContainer.SingleEventUrl != null)
                    return ctrlContainer.SingleEventUrl.Text;
                return string.Empty;
            }
            set
            {
                ctrlContainer.SingleEventUrl.Text = value;
            }
        }

        private PropertyDescriptorCollection properties;
        private ITemplate controlTemplate;
        private ControlContainer ctrlContainer;
        private IDictionary<string, string> settings;
        private PropertyEditorDialog editorDialog;

        #endregion

        #region Methods

        /// <summary>
        /// Overriden. Cancels the rendering of a beginning HTML tag for the control.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Overriden. Cancels the rendering of an ending HTML tag for the control.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation 
        /// to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            ctrlContainer = new ControlContainer(this);
            ControlTemplate.InstantiateIn(ctrlContainer);

            properties = TypeDescriptor.GetProperties(this);
            PropertyDescriptor desc = properties.Find("SingleEventUrl", false);

            editorDialog = new PropertyEditorDialog();
            editorDialog.TypeContainer = this;
            editorDialog.PropertyChanged += editorDialog_PropertyChanged;
            Controls.Add(editorDialog);

            ctrlContainer.SelectEventUrl.CommandName = "Telerik.Cms.Web.UI.PageIndexUrlWebEditor, Telerik.Cms";
            ctrlContainer.SelectEventUrl.CommandArgument = desc.Name;
            ctrlContainer.SelectEventUrl.Command += SelectSinglePersonUrl_Command;

            if (settings != null && settings.Count > 0)
            {
                ctrlContainer.SingleEventUrl.Text = settings["EventPageUrl"];
            }

            Controls.Add(ctrlContainer);
        }

        void SelectSinglePersonUrl_Command(object sender, CommandEventArgs e)
        {
            object data;
            string name = (string)e.CommandArgument;

            PropertyDescriptor desc = properties.Find(name, false);
            data = desc.Converter.ConvertToInvariantString(this);

            editorDialog.Show(name, e.CommandName, data, this);
        }

        void editorDialog_PropertyChanged(object source, PropertyValueChangedEventArgs e)
        {
            string[] resultValue = ((string)e.PropertyValue).Split(';');
            SetProperty(this, properties, e.PropertyName, resultValue[1]);
        }

        private void SetProperty(object component, PropertyDescriptorCollection _properties, string name, object value)
        {
            PropertyDescriptor desc = _properties.Find(name, false);
            desc.SetValue(component, value);
        }

        /// <summary>
        /// It initializes the _settings.
        /// </summary>
        /// <param name="_settings"> 
        ///   The parameter contains the default values when a new provider is created (in IRssProviderModule implementation)
        ///   If the provider has already been created, the parameter contains the values saved in the database.
        /// </param>
        public void InitSettings(IDictionary<string, string> _settings)
        {
            settings = _settings;
        }

        /// <summary>
        /// Gets the newly edited settings.
        /// </summary>
        /// <returns> an object of type Dictionary that contains strings</returns>
        public IDictionary<string, string> GetSettings()
        {
            settings = new Dictionary<string, string>();
            settings["EventPageUrl"] = ctrlContainer.SingleEventUrl.Text;
            return settings;
        }

        #endregion

        #region Classes

        /// <summary>
        /// The control container class
        /// </summary>
        protected class ControlContainer : GenericContainer<EventsIndexSettingsControl>
        {
            /// <summary>
            /// Constructs the control container
            /// </summary>
            /// <param name="owner">accepts a parameter of type RssSettingsControl</param>
            public ControlContainer(EventsIndexSettingsControl owner)
                : base(owner)
            {
            }

            /// <summary>
            /// Gets the control which displays the single product url which could be edited.
            /// </summary>
            public IEditableTextControl SingleEventUrl
            {
                get
                {
                    if (singleEventUrl == null)
                        singleEventUrl = (IEditableTextControl)FindControl(typeof(IEditableTextControl), "singleEventUrl", true);
                    return singleEventUrl;
                }
            }

            /// <summary>
            /// Gets the control which displays the post url in a link button which could be selected.
            /// </summary>
            public IButtonControl SelectEventUrl
            {
                get
                {
                    if (selectEventUrl == null)
                        selectEventUrl = (IButtonControl)FindControl(typeof(IButtonControl), "selectSingleEventUrl", true);
                    return selectEventUrl;
                }
            }

            private IButtonControl selectEventUrl;
            private IEditableTextControl singleEventUrl;
        }

        /// <summary>
        /// The default template class
        /// </summary>
        protected class DefaultTemplate : ITemplate
        {
            /// <summary>
            /// Instantiates the specified control in the default template
            /// </summary>
            /// <param name="container">accepts a parameter of type Control</param>
            public void InstantiateIn(Control container)
            {

            }
        }

        #endregion
    }
}