namespace AdminControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Telerik.Libraries.WebControls;

    /// <summary>
    /// Summary description for FilesSelector
    /// </summary>
    public class FilesSelector: ItemSelector
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.container.TabStrip.Tabs[0].Visible = false;
            this.container.TabStrip.Tabs[1].Visible = false;
            this.container.TabStrip.SelectedIndex = 2;
            this.container.TabStrip.MultiPage.SelectedIndex = 2;
        }         
    }
}