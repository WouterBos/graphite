using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TickerItem
/// </summary>
    [Serializable]
    public class TickerItem
    {
        private int id;
        
        private string summary;

        private string title;

        private string url;
        
        public TickerItem(int id, string title, string summary, string url)
        {
            this.id = id;
            this.Title = title;
            this.Summary = summary;
            this.Url = url;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }