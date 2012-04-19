<%@ Page Title="Start page" Language="C#" MasterPageFile="~/Internal/Masterpages/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <strong>Start page</strong>
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>Welcome to Graphite</h1>
            <p class="intro">Graphite is a frontend development framework for ASP.NET websites with <a href="http://dotlesscss.org">.Less</a>. It will help to kickstart your web project. Graphite aims to provide:</p>
            <ul class="intro">
                <li>Various templates (some ASP.NET-specific)</li>
                <li>2 grid systems</li>
                <li>Sites ready for phones and print</li>
                <li>Accessible webites</li>
            </ul>
            
            <ul class="intro">
                <li><strong><a href="/Internal/Pages/Documentation/">How to use Graphite</a></strong></li>
                <li><strong><a href="/start.aspx">Go edit the start page</a></strong></li>
            </ul>
        </article>
    </div>
    
    <div class="gp_block">
        <nav>
            <p class="gp_textAlert"><big><strong>The project is still in alpha. But there's a <a href="/Internal/Pages/demos/blocks/navigation/menu/default.aspx">preview</a> of the menu component.</strong></big></p>
        </nav>
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
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>
