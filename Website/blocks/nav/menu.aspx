<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>
<%@ Register TagPrefix="UserControl" TagName="DemoHTML" Src="html.ascx" %>


<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <div id="graphite_demoStage">
        <asp:Panel ID="Types" runat="server"></asp:Panel>
        
        <div class="graphite_demoStage">
            <div class="graphite_demoStage_html">
<UserControl:DemoHTML runat="server" ID="DemoHTML1" />
            </div>

            <!--<script>
                // Lorem ipsum dolor
            </script>-->
        </div>
        
        <div class="codeBox js_codeBox gp_columns gp_columns_3">
            <ul class="gp_innerColumns">
                <li class="html gp_column gp_column1">
                    <div class="gp_block">
                        <strong class="localHeading">HTML</strong>
                        <pre class="brush: xml"><UserControl:DemoHTML runat="server" ID="DemoHTML2" /></pre>
                    </div>
                </li>
                <li class="css gp_column gp_column2">
                    <div class="gp_block">
                        <strong class="localHeading">CSS</strong>
                        <pre class="brush: css"></pre>
                    </div>
                </li>
                <li class="javascript gp_column gp_column3">
                    <div class="gp_block">
                        <strong class="localHeading">JavaScript</strong>
                        <pre class="brush: js"></pre>
                    </div>
                </li>
            </ul>
        </div>
   </div>

    <script>
        (function() {
            var demo = new graphite.demo(
                {
                    root: document.getElementById('graphite_demoStage'),
                    cssFiles: document.querySelector("link.graphite_demoStage_html")
                }
            );
            demo.extractCode();
        })()
    </script>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="all.less" class="graphite_demoStage_html" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> ‹ <a href="/blocks/nav">Navigation</a> ‹ <a href="/blocks">Blocks</a> ‹ <a href="/">Graphite</a>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_text">
        <p>The menu is used as the main navigation for the website.</p>
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

