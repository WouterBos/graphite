<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="AspNet.aspx.cs" Inherits="Internal_Pages_Documentation_CodeConventions_AspNet" %>
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
            <h1>ASP.NET</h1>
            <p>ASP.NET code adheres to the <a href="/Internal/downloads/Submain_DotNET_Coding_Guidelines.pdf" target="_blank">code conventions of Submain</a>. Apart from that, Graphite is not related in any way to the company Submain.</p>
        </article>
    </div>
</asp:Content>
