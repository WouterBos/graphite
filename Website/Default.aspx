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
            <p><big>Graphite is a frontend development framework for the ASP.NET platform. It will help kickstart your web project.</big></p>
            <p class="gp_textAlert"><big><strong>The project is still very alpha. But there's a <a href="/blocks/navigation/menu/">preview</a> of the menu component.</strong></big></p>

            <p><a href="/Internal/Pages/Documentation/GettingStarted.aspx">Get started</a></p>
        </article>
        
        <nav>
            <asp:Literal ID="DemosList" runat="server"></asp:Literal>
        </nav>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Todo" Runat="Server">
    <div class="gp_block gp_textBlock">
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
                <li><strong>PSD</strong> Set up two PSD templates for the two grids systems.</li>
            </ul>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block">
        <div class="gp_sitemap">
            <ul>
                <li><a href="/Internal/Pages/Demos/grids">Grids</a>
                    <ul>
                        <li><a href="/Internal/Pages/Demos/grids/flex">Flex</a></li>
                        <li><a href="/Internal/Pages/Demos/grids/fixed">Fixed (960)</a></li>
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
                <li><span>Plain HTML and ASP.NET</span>
                    <ul>
                        <li><a href="/Internal/Pages/Demos/blocks/text">Text content</a></li>
                        <li><a href="/Internal/Pages/Demos/blocks/forms">Forms</a>
                            <ul>
                                <li><a href="/Internal/Pages/Demos/blocks/forms/login">Login</a></li>
                                <li><a href="/Internal/Pages/Demos/blocks/forms/plain">Plain</a></li>
                            </ul>
                        </li>
                        <li><span>Div popup</span></li>
                        <li><span>Div popup movie</span></li>
                        <li><span>Google maps</span></li>
                        <li><span>Navigation lists</span>
                            <ul>
                                <li><a href="/Internal/Pages/Demos/blocks/navigation/menu/">Menu</a></li>
                                <li><span>Sitemap</span></li>
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
                <li><span>CMS related</span>
                    <ul>
                        <li><span>Sitefinity</span>
                            <ul>
                                <li><span>Pager</span></li>
                                <li><span>Captcha</span></li>
                                <li><span>Forms</span></li>
                                <li><span>CMS edit mode</span></li>
                            </ul>
                        </li>
                        <li><a href="/Internal/Pages/Demos/Sitecore">Sitecore</a>
                            <ul>
                                <li><a href="/Internal/Pages/Demos/Sitecore/Webforms-for-marketers">Webforms for marketers</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
