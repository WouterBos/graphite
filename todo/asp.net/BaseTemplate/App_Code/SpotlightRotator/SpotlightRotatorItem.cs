using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SpotlightRotatorItem
/// </summary>
    [Serializable]
    public class SpotlightRotatorItem
    {
        private int id;

        private string linktext;

        private string subtitle;
        
        private string summary;

        private string title;

        private string url;

        private string image;
        
        public SpotlightRotatorItem(int id, string title, string subtitle, string summary, string url, string linktext, string image)
        {
            this.id = id;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Summary = summary;
            this.Url = url;
            this.Linktext = linktext;
            this.Image = image;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Linktext
        {
            get { return linktext; }
            set { linktext = value; }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
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

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
    }