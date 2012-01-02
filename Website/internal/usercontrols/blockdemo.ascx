<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlockDemo.ascx.cs" Inherits="GraphiteInternal_BlockDemo" %>



<link rel="stylesheet" media="all" href="" runat="server" id="CSSLink" />

<div id="graphite_demoStage">
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
    
    <asp:Panel ID="Types" runat="server" CssClass="graphite_demoStage_menu"></asp:Panel>
    
    <div class="graphite_demoStage">
        <asp:Panel cssclass="graphite_demoStage_html" ID="pnlDemoHTMLCodeBlock" runat="server">
            <asp:Literal ID="DemoHTMLCodeBlock" runat="server"></asp:Literal>
        </asp:Panel>

        <asp:Literal ID="DemoJavaScriptCodeBlock" runat="server"></asp:Literal>
    </div>
    
    <div class="gp_columns gp_columns_3 gp_columns_3DemoSpecs">
        <ul>
            <li class="gp_column gp_column1">
                <div class="gp_block">
                    <section>
                        <div class="gp_textBlock">
                            <h1 class="gp_textHeading3">Copy to clipboard</h1>
                        </div>
                        
                        <div class="graphite_getCode">
                            <asp:PlaceHolder ID="phCodeLinksHtml" runat="server">
                                <a href="#" class="graphite_getCodeHtml">.html</a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phCodeLinksAscx" runat="server">
                                <a href="#" class="graphite_getCodeAscx">.ascx</a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phCodeLinksCodeBehind" runat="server">
                                <a href="#" class="graphite_getCodeCodeBehind">codebehind</a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phCodeLinksLess" runat="server">
                                <a href="#" class="graphite_getCodeCss">.css</a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phCodeLinksJs" runat="server">
                                <a href="#" id="getJsCode" class="graphite_getCodeJs">.js</a>
                            </asp:PlaceHolder>
                            
                            <span class="graphite_getCodeFeedback"></span>
                        </div>
                    </section>
                </div>
            </li>
            <li class="gp_column gp_column2">
                <div class="gp_block">
                    <section>
                        <div class="gp_textBlock">
                            <h1 class="gp_textHeading3">Browser compatibility</h1>
                        </div>
                        
                        <asp:Literal ID="litBrowserList" runat="server"></asp:Literal>
                    </section>
                </div>
            </li>
            <li class="gp_column gp_column3">
                <section>
                    <div class="gp_block gp_textBlock">
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
        graphite.internal.demo.data.sourceCode = {
            html: '<asp:Literal ID="litDemoHtml" runat="server"></asp:Literal>',
            ascx: '<asp:Literal ID="litDemoAscx" runat="server"></asp:Literal>',
            codebehind: '<asp:Literal ID="litDemoCodeBehind" runat="server"></asp:Literal>',
            js: '<asp:Literal ID="litDemoJavaScript" runat="server"></asp:Literal>',
            css: '<asp:Literal ID="litDemoCss" runat="server"></asp:Literal>'
        }

        var demo = new graphite.internal.demo(
            {
                root: document.getElementById('graphite_demoStage')
            }
        );
        demo.extractCode();
    })()
</script>
