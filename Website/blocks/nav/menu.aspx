<%@ Page Title="Blocks › Navigation › Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>



<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <div class="graphite_demoStage">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/grids">Lorem ipsum dolor sit amet</a>
                    <ul>
                        <li>Div popup</li>
                        <li>Div popup movie</li>
                        <li>Google maps</li>
                        <li>Navigation lists
                            <ul>
                                <li class="graphite_inDevelopment"><a href="/blocks/nav/sitemap.aspx">Sitemap</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="/CSS/all-graphite-demo.less" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> Graphite
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_text">
        <p>Generates a list of links to all available pages in a website.</p>
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

