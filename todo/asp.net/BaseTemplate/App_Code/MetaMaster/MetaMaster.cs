using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Collections;
using Telerik.Cms.Engine.WebControls;

namespace RandomSFCode.MetaMaster2008
{
    /// <summary>
    /// Represents a custom control builder for MetaMaster
    /// </summary>
    public class MetaMasterControlBuilder : ControlBuilder
    {
        public override Type GetChildControlType(String tagName, IDictionary attributes)
        {
            if (String.Compare(tagName, "MetaMapping", true) == 0)
            {
                return typeof(MetaMapping);
            }
            return null;
        }
    }

    /// <summary>
    /// Control holds all settings for a particular mode of ContentView control
    /// </summary>
    [ControlBuilderAttribute(typeof(MetaMasterControlBuilder))]
    [ParseChildren(true)]
    public class MetaMaster : Control
    {
        #region Properties

        /// <summary>
        /// Collection of MetaMappings
        /// </summary>
        public MetaMappingsCollection Mappings
        {
            get
            {
                if (this.mappings == null)
                    this.mappings = new MetaMappingsCollection();
                return this.mappings;
            }
            set
            {
                this.mappings = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds objects of MetaMapping type to the mappings collection
        /// </summary>
        /// <param name="obj"></param>
        protected override void AddParsedSubObject(Object obj)
        {
            if (obj is MetaMapping)
                mappings.Add((MetaMapping)obj);
        }

        /// <summary>
        /// Overriden. Called to populate the child control hierarchy. This is the main
        /// method to render the control's markup, since it is a CompositeControl and contains
        /// child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            // add all controls from Mappings collection to control collection of this control
            foreach (MetaMapping mm in Mappings)
                this.Controls.Add(mm);
        }

        /// <summary>
        /// Overriden. Called immediately before the page renders.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Find the ContentView control in which this control resides
            ContentView parentContentView = FindParentContentView(this);

            // Call MapValue on each MetaMapping control and pass it the reference to ContentView
            // control in which this control resides
            foreach (MetaMapping mm in Mappings)
                mm.MapValue(parentContentView);

        }

        // Finds the first parent control of type ContentView
        private ContentView FindParentContentView(Control ctrl)
        {
            ContentView ctrlCV = ctrl as ContentView;
            if (ctrlCV == null)
            {
                while (ctrlCV == null && ctrl.Parent != null)
                {
                    ctrlCV = ctrl.Parent as ContentView;
                    if (ctrlCV != null)
                        return ctrlCV;
                    ctrl = ctrl.Parent;
                }
                return ctrlCV;
            }
            else
                return ctrlCV;
        }

        #endregion

        #region Private fields

        private MetaMappingsCollection mappings;

        #endregion

    }

    /// <summary>
    /// Represents a collection of MetaMapping objects
    /// </summary>
    public class MetaMappingsCollection : StateManagedCollection
    {
        public MetaMappingsCollection()
        {

        }

        public MetaMapping this[int index]
        {
            get
            {
                return (MetaMapping)((IList)this)[index];
            }
            set
            {
                ((IList)this)[index] = value;
            }
        }

        public int Add(MetaMapping value)
        {
            return ((IList)this).Add(value);
        }

        public int IndexOf(MetaMapping value)
        {
            return ((IList)this).IndexOf(value);
        }

        public void Insert(int index, MetaMapping value)
        {
            ((IList)this).Insert(index, value);
        }

        public void CopyTo(MetaMapping[] mMappingsArray, int index)
        {
            base.CopyTo(mMappingsArray, index);
        }

        protected override object CreateKnownType(int index)
        {
            return new MetaMapping();
        }

        public void Remove(MetaMapping value)
        {
            ((IList)this).Remove(value);
        }

        public void RemoveAt(int index)
        {
            ((IList)this).RemoveAt(index);
        }

        public bool Contains(MetaMapping value)
        {
            return ((IList)this).Contains(value);
        }

        protected override void OnValidate(Object value)
        {
            if (!value.GetType().IsAssignableFrom(typeof(MetaMapping)))
                throw new ArgumentException("value must be of type MetaMapping.", "value");
        }

        protected override void SetDirtyObject(object o)
        {
            if (o is MetaMapping)
            {
                ((MetaMapping)o).SetDirty();
            }
        }
    }

}
