<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Common.ascx.cs" Inherits="gp_blocks_forms_common" %>

<asp:Panel runat="server" Visible="true" ID="pnlRoot" cssclass="gp_form">
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Niet alle verplichte formuliervelden zijn volledig of correct ingevuld" ShowSummary="true" ShowMessageBox="false" CssClass="gp_formCheckFeedback" ValidationGroup="contact" />

    <span class="mandatory">* = Verplicht veld</span>
    <fieldset>
        <ol>
            <li class="gp_hideAccessible">
                <asp:Label ID="lblTextBoxHumanCheck" runat="server" AssociatedControlID="TextBoxHumanCheck">
                    Leave this form field empty
                </asp:Label>
                <asp:TextBox ID="TextBoxHumanCheck" TextMode="SingleLine" runat="server" CssClass="text" />
            </li>

            <li>
                <asp:Label ID="Label3" runat="server" AssociatedControlID="TextBox1">
                    Textbox without validation
                </asp:Label>
                <asp:TextBox ID="TextBox1" TextMode="SingleLine" runat="server" CssClass="text" ValidationGroup="contact" />
            </li>

            <li>
                <asp:Label ID="Label4" runat="server" AssociatedControlID="TextBox2">
                    Textbox with validation
                    <span>*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2" ErrorMessage="TextBox2" SetFocusOnError="True" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:TextBox ID="TextBox2" TextMode="SingleLine" runat="server" CssClass="text" ValidationGroup="contact" />
            </li>

            <li>
                <asp:Label ID="Label2" runat="server" AssociatedControlID="Regio">Dropdown</asp:Label>
                <asp:DropDownList ID="Regio" runat="server">
                    <asp:ListItem Value="">Choose an option</asp:ListItem>
                    <asp:ListItem>Option A</asp:ListItem>
                    <asp:ListItem>Option B</asp:ListItem>
                    <asp:ListItem>Option C</asp:ListItem>
                </asp:DropDownList>
            </li>

            <li>
                <asp:Label ID="Label1" runat="server" AssociatedControlID="CheckBoxList1">
                    Checkbox list
                </asp:Label>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    <asp:ListItem>Option A</asp:ListItem>
                    <asp:ListItem>Option B</asp:ListItem>
                    <asp:ListItem>Option C</asp:ListItem>
                </asp:CheckBoxList>
            </li>

            <li>
                <asp:CheckBox ID="Notifications" runat="server" Text="Lorem ipsum dolor sit amet" TextAlign="Right" />
            </li>

            <li>
                <asp:Label ID="Label19" runat="server" AssociatedControlID="RadioButtonList1">
                    <span>*</span>
                    Radio button list (horizontal)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="RadioButtonList1" SetFocusOnError="True" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Option A" />
                    <asp:ListItem Text="Option B" />
                </asp:RadioButtonList>
            </li>

            <li>
                <asp:Label ID="Label10" runat="server" AssociatedControlID="Email">
                    Email adress
                    <span>*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Email" ErrorMessage="Email adress" SetFocusOnError="True" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
	                    ControlToValidate="Email"
	                    ValidationExpression=".*@.*\..*"
	                    ValidationGroup="contact"
	                    ErrorMessage="Email adress (incorrect)">*</asp:RegularExpressionValidator>
                </asp:Label>
                <asp:TextBox ID="Email" ToolTip="E-mailadres" TextMode="SingleLine" runat="server" CssClass="text" ValidationGroup="contact" />
            </li>

            <li>
                <asp:Label ID="Label12" runat="server" AssociatedControlID="TextBox3">
                    Textarea:
                    <span>*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox3" ErrorMessage="TextBox3" SetFocusOnError="True" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:TextBox ID="TextBox3" TextMode="MultiLine" runat="server" CssClass="text" ValidationGroup="contact" />
            </li>
        </ol>
    </fieldset>

    <fieldset class="submit">
        <asp:Button runat="server" ID="LinkButton1" ValidationGroup="contact" Text="Verzenden" />
    </fieldset>
</asp:Panel>

<asp:Panel runat="server" ID="SendOkPanel" Visible="false">
    <strong id="FormSentTitle" runat="server" innerhtml="<%$ Resources: FormSentTitle %>"></strong>
    <p id="FormSentText" runat="server" innerhtml="<%$ Resources: FormSentText %>"></p>
    <p><a id="FormSentBack" runat="server" innerhtml="<%$ Resources: Graphite, Back %>"></a></p>
</asp:Panel>

<asp:Panel runat="server" ID="SendFailedPanel" Visible="false">
    <strong id="FormErrorTitle" runat="server" innerhtml="<%$ Resources: FormErrorTitle %>"></strong>
    <p id="FormErrorText" runat="server" innerhtml="<%$ Resources: FormErrorText %>"></p>
    <p><a id="FormErrorBack" runat="server" innerhtml="<%$ Resources: Graphite, Back %>"></a></p>
</asp:Panel>
