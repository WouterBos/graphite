using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Framework.Search;
using Telerik.Cms.Engine;
using Telerik.Events;

namespace EventIndexer
{
    /// <summary>
    /// Summary description for CustomIndexProvider
    /// </summary>
    public class EventIndexProvider : IIndexingServiceClient
    {
        /// <summary>
        /// Defines the name of the provider. This name is used to mange providers within Indexing 
        /// Service.
        /// </summary>
        public string Name
        {
            get
            {
                return "EventIndexProvider";
            }
        }

        /// <summary>
        /// Provides detailed description of the client
        /// </summary>
        public string Description
        {
            get
            {
                return "Provides indexing for the events module.";
            }
        }

        /// <summary>
        /// Meta fields for this provider
        /// </summary>
        protected string[] MetaFields
        {
            get
            {
                return new string[]{
					"Title"
				};
            }
        }

        public IIndexerInfo[] GetContentToIndex()
        {
			// get current events
            EventsManager mgr = new EventsManager("Events");
			IMetaSearchInfo[] filters = new IMetaSearchInfo[2];
			filters[0] = new MetaSearchInfo(MetaValueTypes.DateTime, "Expiration_Date", DateTime.Now, SearchCondition.GreaterOrEqual);
			filters[1] = new MetaSearchInfo(MetaValueTypes.DateTime, "Publication_Date", DateTime.Now, SearchCondition.LessOrEqual);
			IList events = mgr.Content.GetContent("Event_Start ASC", filters);
            List<IIndexerInfo> list = new List<IIndexerInfo>();
            string ItemUrl = "";

			foreach (IContent ev in events)
			{
				Hashtable metaFields = new Hashtable();            
				foreach (string key in MetaFields)
				{
					metaFields.Add(key, "");
				}

				metaFields["Title"] = ev.GetMetaData("Title");

                ItemUrl = eventsPageUrl.Replace(".aspx", ev.Url + ".aspx");

				list.Add(
					new EventIndexerInfo(ItemUrl, metaFields, ev.Content.ToString()));
			}

			return list.ToArray();
		}

        public string[] GetUrlsToIndex()
        {
            return new string[0];
        }

        public event EventHandler<IndexEventArgs> Index;

        public void Initialize(System.Collections.Generic.IDictionary<string, string> indexsettings)
        {
            eventsPageUrl = indexsettings["EventPageUrl"];

            if (String.IsNullOrEmpty(eventsPageUrl))
                throw new ArgumentException(String.Format("CannotCreateAbsoluteUri"));
        }

        private string eventsPageUrl;
    }
}