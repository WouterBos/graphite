using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Cms.Web.UI;
using Telerik.Framework.Search;
using Telerik.Framework.Web;
using System;

namespace EventIndexer
{
    public class EventsIndexViewControl : CompositeControl, ISearchViewControl
    {
        /// <summary>
        /// Gets or sets path of the external template that defines child controls of this control.
        /// </summary>
        public string TemplatePath
        {
            get
            {
                object o = this.ViewState["TemplatePath"];
                if (o == null)
                    return "~/Sitefinity/Admin/ControlTemplates/Events/EventsIndexViewSettingsControl.ascx";
                return (string)o;
            }
            set
            {
                this.ViewState["TemplatePath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template of this control.
        /// </summary>
        public ITemplate ItemTemplate
        {
            get
            {
                if (this.itemTemplate == null)
                    this.itemTemplate = ControlUtils.GetTemplate<DefaultTemplate>(this.TemplatePath);
                return this.itemTemplate;
            }
        }

        /// <summary>
        /// Overriden. Called to populate the child control hierarchy. This is the main
        /// method to render the control's markup, since it is a CompositeControl and contains
        /// child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.container = new ControlContainer(this);
            this.ItemTemplate.InstantiateIn(this.container);

            this.container.ResultsPageUrl.Text = this.GetValue("EventPageUrl", this.settings["EventPageUrl"]);

            this.Controls.Add(this.container);

        }

        private string GetValue(string key, string value)
        {
            switch (key)
            {
                case "EventPageUrl":
                    return String.IsNullOrEmpty(value) ? "Not set" : value;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Initializes settings for the control
        /// </summary>
        /// <param name="settings">a dictionary of strings</param>
        public void InitializeSettings(IDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        #region Private Fields

        private ControlContainer container;
        private ITemplate itemTemplate;
        private IDictionary<string, string> settings;

        #endregion

        #region Default Template

        /// <summary>
        /// Implements the ITemplate interface
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

        #region Container

        /// <summary>
        /// The control container class
        /// </summary>
        protected class ControlContainer : GenericContainer<EventsIndexViewControl>
        {
            public ControlContainer(EventsIndexViewControl owner)
                : base(owner)
            {
            }

            public ITextControl ResultsPageUrl
            {
                get
                {
                    if (this.resultsPageUrl == null)
                        this.resultsPageUrl = (ITextControl)base.FindRequiredControl<Control>("resultsPageUrl");
                    return this.resultsPageUrl;
                }
            }

            private ITextControl resultsPageUrl;
        }

        #endregion
    }
}