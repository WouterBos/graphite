<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>
<%@ Register TagPrefix="UserControl" TagName="DemoHTML" Src="html.ascx" %>
<%@ Register TagPrefix="Internal" TagName="BlockDemo" Src="/internal/usercontrols/blockdemo.ascx" %>


<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <Internal:BlockDemo runat="server" ID="BlockDemo" />

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
                            
                            <div class="graphite_getCode">
                                <a href="#" class="graphite_getCodeHtml">.html</a>
                                <asp:PlaceHolder ID="CodeLinksLess" runat="server">
                                    <a href="#" class="graphite_getCodeCss">.css</a>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="CodeLinksJs" runat="server">
                                    <a href="#" id="getJsCode" class="graphite_getCodeJs">.js</a>
                                </asp:PlaceHolder>
                            </div>
                        </section>
                    </div>
                </li>
                <li class="gp_column gp_column2">
                    <div class="gp_block">
                        <section>
                            <div class="gp_text">
                                <h1 class="heading3">Browser compatibility</h1>
                            </div>
                            
                            <asp:Literal ID="BrowserList" runat="server"></asp:Literal>
                            
                            <!--<ul class="graphite_browser">
                                <li>
                                    <strong class="graphite_browserIcon graphite_browserMsie">IE</strong>
                                    <span class="graphite_browserVersion">7+</span>
                                </li>
                                <li>
                                    <strong class="graphite_browserIcon graphite_browserFf">Firefox</strong>
                                    <span class="graphite_browserVersion">3.5+</span>
                                </li>
                                <li>
                                    <strong class="graphite_browserIcon graphite_browserChrome">Chrome</strong>
                                    <span class="graphite_browserVersion">5+</span>
                                </li>
                                <li>
                                    <strong class="graphite_browserIcon graphite_browserSafari">Safari</strong>
                                    <span class="graphite_browserVersion">3+</span>
                                </li>
                                <li class="graphite_browserUnsupported">
                                    <strong class="graphite_browserIcon graphite_browserOpera">Opera</strong>
                                    <span class="graphite_browserVersion"></span>
                                </li>
                            </ul>-->
                        </section>
                    </div>
                </li>
                <li class="gp_column gp_column3">
                    <section>
                        <div class="gp_block gp_text">
                            <h1 class="heading3">Description</h1>
                            <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                        </div>
                    </section>
                </li>
            </ul>
        </div>
    </div>

    <script>
        var sourceCode = {
            html: document.getElementById('sourcecodeHTML').innerHTML,
            js: '<asp:Literal ID="DemoJavaScript" runat="server"></asp:Literal>',
            css: '<asp:Literal ID="DemoCss" runat="server"></asp:Literal>'
        }
        
        
    </script>

    <script>
        (function() {
            var demo = new graphite.demo(
                {
                    root: document.getElementById('graphite_demoStage')
                }
            );
            demo.extractCode();
        })()

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

