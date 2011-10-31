<%@ Control Language="C#" AutoEventWireup="true" CodeFile="blockdemo.ascx.cs" Inherits="internal_usercontrols_blockdemo" %>



<link rel="stylesheet" media="all" href="" runat="server" id="CSSLink" />

<div id="graphite_demoStage">
    <asp:Panel ID="Types" runat="server" CssClass="graphite_demoStage_menu"></asp:Panel>
    
    <div class="graphite_demoStage">
        <div class="graphite_demoStage_html" id="sourcecodeHTML"><asp:Literal ID="DemoHTMLCodeBlock" runat="server"></asp:Literal></div>

        <asp:Literal ID="DemoJavaScriptCodeBlock" runat="server"></asp:Literal>
    </div>
    
    <div class="codeBox js_codeBox gp_columns gp_columns_3">
        <ul class="gp_innerColumns">
            <li class="gp_column gp_column1">
                <div class="gp_block">
                    <section>
                        <div class="gp_text">
                            <h1 class="gp_textHeading3">Copy to clipboard</h1>
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
                            <h1 class="gp_textHeading3">Browser compatibility</h1>
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
                        <h1 class="gp_textHeading3">Description</h1>
                        <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                    </div>
                </section>
            </li>
        </ul>
    </div>
</div>

<script>
    (function() {
        graphite.demo.data.sourceCode = {
            html: document.getElementById('sourcecodeHTML').innerHTML,
            js: '<asp:Literal ID="DemoJavaScript" runat="server"></asp:Literal>',
            css: '<asp:Literal ID="DemoCss" runat="server"></asp:Literal>'
        }

        var demo = new graphite.demo(
            {
                root: document.getElementById('graphite_demoStage')
            }
        );
        demo.extractCode();
    })()

</script>
