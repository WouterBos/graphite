<%@ Page Title="Start page" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="/CSS/all.less" />
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> Graphite
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
    
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_text">
        <article>
            <h1>Welcome to Graphite</h1>
            <p><big>Graphite is a codebase for frontend development on the ASP.NET platform. It will help kickstart your web project. It contains code templates with HTML, CSS and JavaScript. It also has some templates for the popular CMS systems Sitefinity and Sitecore.</big></p>
            
            <h2>About this web page</h2>
            <p>This website contains many working code snippets ready to be copy-pasted into your own project.</p>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/grids">Grids</a>
                    <ul>
                        <li class="graphite_inDevelopment"><a href="/grids/flex">Flex</a></li>
                        <li>16 grid system</li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/grids">Blocks</a>
                    <ul>
                        <li>Div popup</li>
                        <li>Div popup movie</li>
                        <li>Google maps</li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/grids">CMS</a>
                    <ul>
                        <li>Sitefinity
                            <ul>
                                <li>Pager</li>
                                <li>Captcha</li>
                                <li>Forms</li>
                                <li>CMS edit mode</li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
