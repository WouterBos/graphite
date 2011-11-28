<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="blocks_nav_menu" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/internal/usercontrols/breadcrumb.ascx" %>



<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <Internal:Breadcrumb runat="server" ID="Breadcrumb" />
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block">
        <div class="gp_text">
            <h1>Contents of {BLOCKS}</h1>
            <div class="gp_sitemap">
                <ul>
                    <li>
                        <a href="navigation">Navigation</a>
                    </li>
                    <li>
                        <a href="text">Text</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Content6" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>



<asp:Content ID="Content7" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>

