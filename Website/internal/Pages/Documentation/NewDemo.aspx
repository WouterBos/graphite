<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="NewDemo.aspx.cs" Inherits="Internal_Pages_Documentation_NewDemo" %>
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
            <h1>Making a new demo</h1>
            <p><big>Demos are the heart of Graphite. Each demo showcases a reusable block.</big></p>
            
            <h2>Make a github account</h2>
            <p>Github is a social network for developers. It offers version control for your project and the ability for others to join in with a project.</p>
            <p>The Graphite repository is hosted on Github. To get started you first need to create an account on Github.</p>
            
            <h2>Install stuff</h2>
            <p>Github requires the following software:</p>
            <ul>
                <li>TortoiseGIT</li>
                <li>PuTTY</li>
                <li></li>
            </ul>
            
            <h2>Get your own working copy</h2>
            <p>Making your own working copy is called &ldquo;forking&rdquo;. You can do anything with your fork.</p>
            
            <h2>Contribute to the project</h2>
            <p>If you like to share your improvements, you have to commit your work first (of course). After that you do a &ldquo;Pull request&rdquo; by clicking the similar named button on the Github website.</p>
            
            <h2>Updating your fork</h2>
            <p>GP_TODO</p>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>
