using System.Collections;
using System.Text;
using Telerik.Framework.Search;

namespace EventIndexer
{
    /// <summary>
    /// Summary description for CustomIndexerInfo
    /// </summary>
    public class EventIndexerInfo : IIndexerInfo
    {
        private string _url;
        private Hashtable _metaFields;
        private string _content;

        public EventIndexerInfo(string url, Hashtable metaFields, string content)
        {
            this._url = url;
            this._metaFields = metaFields;
            this._content = content;
        }

        protected string GetMetaData()
        {
            StringBuilder sb = new StringBuilder();
            IDictionaryEnumerator meta = _metaFields.GetEnumerator();

            while (meta.MoveNext())
            {
                sb.AppendLine();
                sb.Append("<");
                sb.Append(meta.Key);
                sb.Append(">");
                sb.Append(meta.Value.ToString());
                sb.Append("</");
                sb.Append(meta.Key);
                sb.Append(">");
            }
            return sb.ToString();
        }

        #region IIndexerInfo Members

        public Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public byte[] GetData()
        {
            string text = (string)this._content;

            //If we have any meta data add it to the index content
            if (this._metaFields.Count > 0)
            {
                text += this.GetMetaData();
            }

            return this.Encoding.GetBytes(text);
        }

        public string MimeType
        {
            get { return "text/html"; }
        }

        public string Path
        {
            get { return this._url; }
        }

        #endregion
		
		#region IIndexerInfo Members

		public string Culture
		{
			get { return string.Empty; }
		}

		public System.Guid ItemID
		{
			get { return System.Guid.Empty; }
		}

        public string ResolveIndexPath()
        {
            return this.Path;
        }  

		#endregion
    }
}