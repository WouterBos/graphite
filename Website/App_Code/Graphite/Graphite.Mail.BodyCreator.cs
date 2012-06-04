using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;



namespace Graphite.Mail
{
    /// <summary>
    /// Creates a HTML or text body for an email.
    /// </summary>
    public class BodyCreator
    {
        #region "Private Methods"

        /// <summary>
        /// Loads the content of the file into a string
        /// </summary>
        /// <param name="templateLocation"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetHTMLTemplate(string templateLocation)
        {
            string BodyTemplate = System.IO.File.ReadAllText(templateLocation);
            return BodyTemplate;
        }

        /// <summary>
        /// Replaces placeholders in the template string
        /// </summary>
        /// <param name="bodyTemplate">String holding the template</param>
        /// <param name="bodyText">Value to replace the ##BODY## placeholder</param>
        /// <param name="mailSubject">Value to replace the ##SUBJECT## placeholder</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string FillTemplate(string bodyTemplate, string bodyText, string mailSubject)
        {
            string FilledTemplate = "";

            FilledTemplate = bodyTemplate.Replace("##BODY##", bodyText);
            FilledTemplate = FilledTemplate.Replace("##SUBJECT##", mailSubject);

            return FilledTemplate;
        }

        /// <summary>
        /// Formats values of the input controls on the control to HTML
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string FormatFormValues(Control control)
        {
            ArrayList FormFieldsCollection = new ArrayList();
            GetFormValuesFromPanel(control, ref FormFieldsCollection);
            string TableStyle = "border-bottom: 1px dotted #bababa";
            string ColumnStyleLeft = "border-top: 1px dotted #bababa; " +
                                     "vertical-align: top; " +
                                     "padding-top: 2px; " +
                                     "padding-right: 5px; " +
                                     "padding-bottom: 2px; " +
                                     "color: #000000; " +
                                     "white-space: nowrap; " +
                                     "font: normal 12px/18px Arial, Helvetica, sans-serif";
            string ColumnStyleRight = "border-top: 1px dotted #bababa; " +
                                      "padding-top: 2px; " +
                                      "padding-bottom: 2px; " +
                                      "vertical-align: top; " +
                                      "color: #000000; " +
                                      "font: normal 12px/18px Arial, Helvetica, sans-serif";
            System.Text.StringBuilder CormValuesFormatted = new System.Text.StringBuilder();
            string ItemValue = null;

            CormValuesFormatted.Append("<table border='0' cellspacing='0' cellpadding='0' style='" + TableStyle + "'>\r\n");
            foreach (UtilityObj Item in FormFieldsCollection)
            {
                ItemValue = Item.Value; //Estate.Common.Text.Texttools.HTML2Text(Item.Value);
                CormValuesFormatted.Append("<tr>");
                CormValuesFormatted.Append("<td style='" + ColumnStyleLeft + "'><strong>" + FormatFormFieldName(Item.Name) + "</strong></td>\r\n");
                CormValuesFormatted.Append("<td style='" + ColumnStyleRight + "'>" + ItemValue + "</td>\r\n");
                CormValuesFormatted.Append("</tr>");
            }
            CormValuesFormatted.Append("</table>");

            return CormValuesFormatted.ToString();
        }

        /// <summary>
        /// Replaces _ in the fieldname with spaces
        /// </summary>
        /// <param name="formFieldName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string FormatFormFieldName(string formFieldName)
        {
            string ReturnString = null;

            ReturnString = formFieldName;
            ReturnString = ReturnString.Replace("_", " ");

            return ReturnString;
        }

        /// <summary>
        /// Gets input values from the controls in the control
        /// </summary>
        /// <param name="control"></param>
        /// <returns>Arraylist(Of UtilityObj)</returns>
        /// <remarks></remarks>
        private void GetFormValuesFromPanel(Control control, ref ArrayList arrList)
		{
			foreach (Control ControlInForm in control.Controls)
            {
				if (ControlInForm is TextBox)
                {
                    if (ControlInForm.ID == "txtBotCheck")
                    {
                        continue;
                    }
					arrList.Add(new UtilityObj(ControlInForm.ID, ((TextBox)ControlInForm).Text));
				}
                else if (ControlInForm is DropDownList)
                {
                    arrList.Add(new UtilityObj(ControlInForm.ID, ((DropDownList)ControlInForm).SelectedValue));
                }
                else if (ControlInForm is RadioButtonList)
                {
                    arrList.Add(new UtilityObj(ControlInForm.ID, ((RadioButtonList)ControlInForm).SelectedValue));
                }
                else if (ControlInForm is CheckBoxList)
                {
                    CheckBoxList list = (CheckBoxList)ControlInForm;
                    String listValues = "";
                    foreach (ListItem boxItem in list.Items)
                    {
                        Console.WriteLine(boxItem.Text + " - " + boxItem.Selected.ToString());

                        if (boxItem.Selected == true)
                        {
                            if (listValues != "")
                            {
                                listValues += "<br />";
                            }
                            listValues += ((string)boxItem.Value);
                        }
                    }
                    arrList.Add(new UtilityObj(ControlInForm.ID, listValues));
                }
                else if (ControlInForm is CheckBox)
                {
					CheckBox oCheckBox = (CheckBox)ControlInForm;
					if (oCheckBox.Checked == true)
                    {
						arrList.Add(new UtilityObj(oCheckBox.ID, "Ja"));
					}
				}
				if (ControlInForm.HasControls())
                {
					GetFormValuesFromPanel(ControlInForm, ref arrList);
				}
			}
		}


        #endregion

        #region "Public Methods"
        /// <summary>
        /// Creates a mailbody
        /// </summary>
        /// <param name="formPanel"></param>
        /// <param name="templateLocation"></param>
        /// <param name="mailSubject"></param>
        /// <returns>The body of the mail</returns>
        /// <remarks></remarks>
        public string Create(Panel formPanel, string templateLocation, string mailSubject)
        {
            string BodyTemplate = null;
            string BodyText = null;
            string ReturnBody = null;

            BodyTemplate = GetHTMLTemplate(templateLocation);
            BodyText = FormatFormValues(formPanel);
            ReturnBody = FillTemplate(BodyTemplate, BodyText, mailSubject);

            return ReturnBody;
        }
        #endregion
    }

    #region "Key/valuepair object"
    public class UtilityObj
    {
        #region "Private Properties"
        private string _name;
        #endregion
        private string _value;

        #region "Constructor"
        public UtilityObj(string Name, string Value)
        {
            _name = Name;
            _value = Value;
        }
        #endregion

        #region "Public Properties"
        public string Name
        {
            get { return _name; }
            set { _name = Name; }
        }

        public string Value
        {
            get { return (_value); }
            set { _value = value; }
        }
        #endregion
    }

    #endregion

}
