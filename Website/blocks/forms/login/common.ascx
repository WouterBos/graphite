<%@ Control Language="C#" AutoEventWireup="true" CodeFile="common.ascx.cs" Inherits="GraphiteBlocksFormsLogin" %>


<asp:Panel ID="pnlRoot" runat="server" CssClass="gp_login">
    <asp:Login ID="Login1" runat="server" MembershipProvider="Forms" onauthenticate="Login1_Authenticate" CssClass="gp_loginRoot" CheckBoxStyle-CssClass="gp_loginCheckbox" FailureTextStyle-CssClass="gp_loginFailure" InstructionTextStyle-CssClass="gp_loginInstruction" LabelStyle-CssClass="gp_loginLabel" LoginButtonStyle-CssClass="gp_loginLoginButton" TextBoxStyle-CssClass="gp_loginTextBox" TitleTextStyle-CssClass="gp_loginTitle" ValidatorTextStyle-CssClass="gp_loginValidator">
    </asp:Login>
</asp:Panel>