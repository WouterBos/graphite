<%@ Page Title="Code conventions" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/Internal/usercontrols/breadcrumb.ascx" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <Internal:Breadcrumb runat="server" ID="Breadcrumb" />
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Column2_1" Runat="Server">
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Column2_2" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>Code conventions in Graphite</h1>
            <p><big>Consistent coding improves readability and the quality of Graphite in general. Here below you can find the code conventions it adheres to.</big></p>
        </article>
    </div>
</asp:Content>
