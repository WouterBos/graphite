using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Reflection;
using Telerik.Cms.Engine.WebControls;
using Telerik.Cms.Engine;
using System.Diagnostics;

namespace RandomSFCode.MetaMaster2008
{
    /// <summary>
    /// This class represents single mapping of meta-field value to an arbitrary property
    /// of specified control inside of a ContentView template
    /// </summary>
    public class MetaMapping : CompositeControl, IStateManager
    {
        #region Properties

        /// <summary>
        /// Gets or sets the key of meta-field for which the value should be mapped
        /// </summary>
        public string MetaKey
        {
            get
            {
                return this.metaKey;
            }
            set
            {
                this.metaKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the id of the control whose property will be set to the value
        /// of meta-field
        /// </summary>
        public string TargetControlID
        {
            get
            {
                return this.targetControlID;
            }
            set
            {
                this.targetControlID = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the property which value will be set to the value of
        /// meta-field
        /// </summary>
        public string TargetProperty
        {
            get
            {
                return this.targetProperty;
            }
            set
            {
                this.targetProperty = value;
            }
        }

        public string TargetPropertyStringFormat
        {
            get
            {
                return this.targetPropertyStringFormat;
            }
            set
            {
                this.targetPropertyStringFormat = value;
            }
        }

        #endregion

        #region Overriden Methods

        protected override void CreateChildControls()
        {
            // do not create child controls
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method find the target control and sets the target property to the value
        /// of specified meta key
        /// </summary>
        /// <param name="contentView"></param>
        public virtual void MapValue(ContentView contentView)
        {
            
            ContentManager cntManager = new ContentManager(contentView.ProviderName);
            if (cntManager.MetaKeys.ContainsKey(this.MetaKey))
            {
                IContent cnt = cntManager.GetContent(contentView.SelectedItemId);
                string metaValue = string.Empty;
                object objVal = cnt.GetMetaData(this.MetaKey);
                if (objVal != null) metaValue = objVal.ToString();

                // find the target control
                if (!String.IsNullOrEmpty(this.TargetControlID) && contentView != null)
                {
                    Control ctrl = FindControlRecursive(contentView, this.TargetControlID);
                    if (ctrl != null)
                    {
                        Type ctrlType = ctrl.GetType();
                        PropertyInfo pInfo = ctrlType.GetProperty(this.TargetProperty);
                        if (pInfo != null)
                            pInfo.SetValue(ctrl, String.Format(this.targetPropertyStringFormat, metaValue), null);
                    }
                }
            }
            else
            {
                throw new Exception("Meta field does not exist. Please ensure that the field was typed in properly and added to the Web.config");
            }
        }
        
        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }

        internal void SetDirty()
        {
            ViewState.SetDirty(true);
        }

        #endregion

        #region Private fields

        private string metaKey;
        private string targetControlID;
        private string targetProperty;
        private string targetPropertyStringFormat;

        #endregion

        #region IStateManager Members

        public new bool IsTrackingViewState
        {
            get
            {
                return base.IsTrackingViewState;
            }
        }

        public new void LoadViewState(object state)
        {
            base.LoadViewState(state);
        }

        public new object SaveViewState()
        {
            return base.SaveViewState();
        }

        public new void TrackViewState()
        {
            base.TrackViewState();
        }

        #endregion
    }
}
