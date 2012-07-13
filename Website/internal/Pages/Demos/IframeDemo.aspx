<%@ Page Title="Menu" Language="C#" AutoEventWireup="true" CodeFile="IframeDemo.aspx.cs" Inherits="IframeDemo" %>
<%@ Register TagPrefix="Internal" TagName="BlockDemo" Src="~/Internal/usercontrols/blockdemo2.ascx" %>



<!DOCTYPE html>
<html lang="nl">
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	</head>

<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <Internal:BlockDemo runat="server" ID="BlockDemo" />
</asp:Content>
