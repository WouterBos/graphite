<%@ Page Title="Start page" Language="C#" MasterPageFile="~/Internal/Masterpages/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <strong>Graphite</strong>
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>Welcome to Graphite</h1>
            <p><big>Graphite is a frontend development framework for ASP.NET websites with <a href="http://dotless.org">.Less</a>. It will help to kickstart your web project.</big></p>
            <p><big><strong><a href="/Internal/Pages/Documentation/GettingStarted.aspx">How to use Graphite</a></big></strong></p>
            <p class="gp_textAlert"><big><strong>The project is still very alpha. But there's a <a href="/Internal/Pages/demos/blocks/navigation/menu/default.aspx">preview</a> of the menu component.</strong></big></p>
        </article>
    </div>
    
    <div class="gp_block">
        <nav>
            <div class="gp_textBlock">
                <h2>Components</h2>
            </div>
            <div class="gp_sitemap">
                <asp:Literal ID="DemosList" runat="server"></asp:Literal>
            </div>
        </nav>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Todo" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="heading2">Todo</h1>
            <ul>
                <li><strong>Standards</strong> Default "form" for designers to define their grid</li>
                <li><strong>Themes</strong> managed with just a few Less variables</li>
                <li><strong>Generic blocks</strong> multi-purpose elements like for example a list with just title, sub title and a short text</li>
                <li><strong>Fine-grained spacing</strong> different values for horizontal and vertical spacing</li>
                <li><strong>Export</strong> less import file (and possibly other stuff) by selecting graphite components</li>
                <li><strong>Locale</strong> How to create multilangual demo website</li>
                <li><strong>Sections</strong> Graphite must be extendedable so that organisations can create their own modules without "polluting" Graphite with functionality that's too specific for the general user.</li>
                <li><strong>dev/publish toggle</strong> Based on web.config's "default=true" some stuff is toggled on or of like:
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
