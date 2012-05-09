using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Graphite.Mail;

public partial class gp_blocks_forms_common : System.Web.UI.UserControl
{
    private string _strRootClass = "";
    public string strRootClass
    {
        get { return _strRootClass; }
        set { _strRootClass = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlRoot.CssClass += " " + _strRootClass;
        
        FormSentBack.HRef = Request.ServerVariables["URL"];
        FormErrorBack.HRef = Request.ServerVariables["URL"];
        
        if (TextBoxHumanCheck.Text == "")
        {
            // SEND FORM
        }
    }


    protected void SendMail(object sender, EventArgs e)
    {
        if (txtBotCheck.Text != "")
        {
            SendFailedPanel.Visible = true;
            pnlRoot.Visible = false;
            SendOkPanel.Visible = false;
            return;
        }
        HandleForm oMailSend = new Graphite.Mail.HandleForm();
        oMailSend.From = "wouter@estate.nl";
        //oMailSend.Cc = Email.Text;
        //oMailSend.Bcc = "w.bos@estate.nl";
        oMailSend.Subject = "Resultaat contactformulier op www.____.nl";
        oMailSend.BodyTemplatePath = "~\\App_Data\\Graphite\\Mail\\Template.html";
        oMailSend.FormPanel = pnlRoot;
        oMailSend.SendOkPanel = SendOkPanel;
        oMailSend.SendFailedPanel = SendFailedPanel;
        oMailSend.CreateBody();
        oMailSend.SendMail();
    }
}
