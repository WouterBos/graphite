<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/all-login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 100px;">
        <asp:Panel ID="Panel1" runat="server" CssClass="gp_login">
            <asp:Login ID="Login1" runat="server" MembershipProvider="Forms" onauthenticate="Login1_Authenticate" CssClass="gp_loginRoot" CheckBoxStyle-CssClass="gp_loginCheckbox" FailureTextStyle-CssClass="gp_textAlert" InstructionTextStyle-CssClass="gp_loginInstruction" LabelStyle-CssClass="gp_loginLabel" LoginButtonStyle-CssClass="gp_loginLoginButton" TextBoxStyle-CssClass="gp_loginTextBox" TitleTextStyle-CssClass="gp_loginTitle" ValidatorTextStyle-CssClass="gp_loginValidator">
            </asp:Login>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
