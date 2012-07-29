<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="Todo.aspx.cs" Inherits="Internal_Pages_Documentation_Todo" %>
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
            <h1>Todo</h1>
            <ul>
                <li><strong>Documentation</strong> Targeted for people who start with Graphite.</li>
                <li><strong>Standards</strong> Default "form" for designers to define their grid</li>
                <li><strong>Themes</strong> managed with just a few Less variables</li>
                <li><strong>Generic blocks</strong> multi-purpose elements like for example a list with just title, sub title and a short text</li>
                <li><strong>Export</strong> less import file (and possibly other stuff) by selecting graphite components</li>
                <li><strong>Locale</strong> How to create multilangual demo website</li>
                <li><strong>Sections</strong> Graphite must be extendedable so that organisations can create their own modules without "polluting" Graphite with functionality that's too specific for the general user.</li>
                <li><strong>dev/publish toggle</strong> Based on web.config's "default=true" or some basic switch in the web.config some stuff must be toggled on or of like:
                    <ul>
                        <li>index/follow</li>
                        <li>dynamic or static css generation</li>
                    </ul>
                </li>
                <li><strong>PSD</strong> Set up two PSD templates for the two grids systems.</li>
            </ul>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>
