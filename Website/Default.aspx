<%@ Page Title="Start page" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
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
            <p class="gp_textAlert"><big><strong>The project is still very alpha. But there's a <a href="/blocks/navigation/menu/">preview</a> of the menu component.</strong></big></p>
            
            <h2>About this web page</h2>
            <p>This website contains many working code snippets ready to be copy-pasted into your own project.</p>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Todo" Runat="Server">
    <div class="gp_block gp_text">
        <article>
            <h1 class="heading2">Todo</h1>
            <ul>
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
            </ul>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/grids">Grids</a>
                    <ul>
                        <li><a href="/grids/flex">Flex</a></li>
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
                        <li>Navigation lists
                            <ul>
                                <li><a href="/blocks/navigation/menu/">Menu</a></li>
                                <li>Sitemap</li>
                            </ul>
                        </li>
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



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><span>Documentation</span>
                    <ul>
                        <li class="graphite_inDevelopment"><a href="/documentation/code-convention.aspx">Code convention</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
