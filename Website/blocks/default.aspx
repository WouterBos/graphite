<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="blocks_nav_menu" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/internal/usercontrols/breadcrumb.ascx" %>
<%@ Register TagPrefix="Internal" TagName="Folders" Src="~/internal/usercontrols/folders.ascx" %>



<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <Internal:Breadcrumb runat="server" ID="Breadcrumb" />
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Intro" Runat="Server">
    <Internal:Folders runat="server" ID="Folders" />
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Content6" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>



<asp:Content ID="Content7" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>

