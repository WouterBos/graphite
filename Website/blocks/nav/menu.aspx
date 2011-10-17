<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>
<%@ Register TagPrefix="UserControl" TagName="DemoHTML" Src="html.ascx" %>


<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <div id="graphite_demoStage">
        <asp:Panel ID="Types" runat="server" CssClass="graphite_demoStage_menu"></asp:Panel>
        
        <div class="graphite_demoStage">
            <div class="graphite_demoStage_html" id="sourcecodeHTML">
<UserControl:DemoHTML runat="server" ID="DemoHTMLCodeBlock" CssType="horizontal" />
            </div>

            <asp:Literal ID="DemoJavaScriptCodeBlock" runat="server"></asp:Literal>
        </div>
        
        <div class="codeBox js_codeBox gp_columns gp_columns_3">
            <ul class="gp_innerColumns">
                <li class="gp_column gp_column1">
                    <div class="gp_block">
                        <section>
                            <div class="gp_text">
                                <h1 class="heading3">Copy to clipboard</h1>
                            </div>
                            
                            <div class="getCode">
                                <a href="#">.html</a>
                                <asp:PlaceHolder ID="CodeLinksLess" runat="server">
                                    <a href="#">.less</a>
                                    <a href="#" runat="server" ID="aCssPlainLink">.css</a>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="CodeLinksCss" runat="server">
                                    <a href="#">.js</a>
                                </asp:PlaceHolder>
                            </div>
                        </section>
                    </div>
                </li>
                <li class="gp_column gp_column2">
                    <div class="gp_block gp_text">
                        <p>Browser compatibility.</p>
                    </div>
                </li>
                <li class="gp_column gp_column3">
                    <div class="gp_block gp_text">
                        <p>The menu is used as the main navigation for the website.</p>
                    </div>
                </li>
            </ul>
        </div>
    </div>

    <script>
        var sourceCode = {
            html: document.getElementById('sourcecodeHTML').innerHTML,
            css: '<asp:Literal ID="DemoCss" runat="server"></asp:Literal>',
            js: '<asp:Literal ID="DemoJavaScript" runat="server"></asp:Literal>'
        }
    </script>

    <script>
        /*
        (function() {
            var demo = new graphite.demo(
                {
                    root: document.getElementById('graphite_demoStage'),
                    cssFiles: document.querySelector("link.graphite_demoStage_html")
                }
            );
            demo.extractCode();
        })()
        
		SyntaxHighlighter.all();
		*/
    </script>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="all.less" runat="server" id="CSSLink" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> ‹ <a href="/blocks/nav">Navigation</a> ‹ <a href="/blocks">Blocks</a> ‹ <a href="/">Graphite</a>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Intro" Runat="Server">
    
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Content6" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>



<asp:Content ID="Content7" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>

