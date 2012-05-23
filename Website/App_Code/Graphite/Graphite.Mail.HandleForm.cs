using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Net.Mail;



namespace Graphite.Mail
{
    public class HandleForm
    {
        private string _mailFrom = "";
        private string _mailTo = "";
        private string _mailCc = "";
        private string _mailBcc = "";
        private string _mailSubject = "";
        private string _mailBody = "";
        private string _mailBodyTemplatePath = HttpContext.Current.Server.MapPath("~\\App_Data\\Grapite\\Mail\\Template.html");
        private string _Attachment;
        private Panel _FormPanel;
        private Panel _SendOkPanel;
        private Panel _SendFailedPanel;

        public string Attachment
        {
            get { return _Attachment; }
            set { _Attachment = value; }
        }
        public string From
        {
            get { return _mailFrom; }
            set { _mailFrom = value; }
        }

        public string To
        {
            get { return _mailTo; }
            set { _mailTo = value; }
        }

        public string Cc
        {
            get { return _mailCc; }
            set { _mailCc = value; }
        }

        public string Bcc
        {
            get { return _mailBcc; }
            set { _mailBcc = value; }
        }

        public string Subject
        {
            get { return _mailSubject; }
            set { _mailSubject = value; }
        }

        public string Body
        {
            get { return _mailBody; }
            set { _mailBody = value; }
        }

        public string BodyTemplatePath
        {
            get { return _mailBodyTemplatePath; }
            set { _mailBodyTemplatePath = HttpContext.Current.Server.MapPath(value); }
        }

        public Panel FormPanel
        {
            get { return _FormPanel; }
            set { _FormPanel = value; }
        }

        public Panel SendOkPanel
        {
            get { return _SendOkPanel; }
            set { _SendOkPanel = value; }
        }

        public Panel SendFailedPanel
        {
            get { return _SendFailedPanel; }
            set { _SendFailedPanel = value; }
        }
        /// <summary>
        /// Gets default e-mail address from database
        /// </summary>
        /// <param name="datacontext"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetDefaultMailAddress()
        {
            return "w.bos@estate.nl";
        }

        /// <summary>
        /// Fills _mailbody with the formatted mailbody
        /// </summary>
        /// <remarks></remarks>
        public void CreateBody()
        {
            BodyCreator MyBodyCreator = new BodyCreator();
            _mailBody = MyBodyCreator.Create(_FormPanel, _mailBodyTemplatePath, _mailSubject);
        }

        /// <summary>
        /// Sends e-mail
        /// </summary>
        /// <remarks></remarks>
        public void SendMail()
        {
            MailMessage Mail = new MailMessage();

            if (string.IsNullOrEmpty(_mailTo))
            {
                _mailTo = "wouter@estate.nl";
            }
            if (!string.IsNullOrEmpty(_mailCc))
            {
                Mail.CC.Add(_mailCc);
            }
            if (!string.IsNullOrEmpty(_mailBcc))
            {
                Mail.Bcc.Add(_mailBcc);
            }

            Mail.From = new MailAddress(_mailFrom);
            if (_mailTo.IndexOf(";") == -1)
            {
                Mail.To.Add(_mailTo);
            }
            else
            {
                char[] separator = new char[] { ';' };
                string[] addresses = _mailTo.Split(separator);
                foreach (string address in addresses)
                {
                    Mail.To.Add(address);
                } 
            }
            Mail.Subject = _mailSubject;
            Mail.Body = _mailBody;
            Mail.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(_Attachment) & System.IO.File.Exists(_Attachment))
            {
                Attachment attachment = new Attachment(_Attachment);
                //create the attachment
                Mail.Attachments.Add(attachment);
                //add the attachment
            }

            _FormPanel.Visible = false;
            _SendOkPanel.Visible = true;

            try
            {
                SmtpClient SMTP = new SmtpClient();
                SMTP.Host = "192.168.1.3";
                SMTP.Send(Mail);
            }
            catch (Exception ex)
            {
                _SendOkPanel.Visible = false;
                _SendFailedPanel.Visible = true;
            }
            finally
            {
                //
            }
        }
    }
}
