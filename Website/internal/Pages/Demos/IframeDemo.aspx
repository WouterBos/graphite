<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Internal/Masterpages/Demo.master" AutoEventWireup="true" CodeFile="IframeDemo.aspx.cs" Inherits="IframeDemo" %>
<%@ Register TagPrefix="Internal" TagName="BlockDemo" Src="~/Internal/usercontrols/blockdemo2.ascx" %>



<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <Internal:BlockDemo runat="server" ID="BlockDemo" />
</asp:Content>
