<%@ Page Title="Blocks › Navigation › Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>



<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <div class="graphite_demoStage" id="graphite_demoStage">
        <div class="graphite_demoStage_html">
            <div class="gp_nav_menu gp_horizontal">
                <ul>
                    <li><a href="#">Short</a>
                        <ul>
                            <li><a href="#">Dolor</a></li>
                            <li><a href="#">Consectetur</a></li>
                            <li><a href="#">Adipiscing</a></li>
                            <li><a href="#">Suspendisse</a>
                                <ul>
                                    <li><a href="#">Ipsum</a></li>
                                    <li><a href="#">Placerat</a></li>
                                    <li><a href="#">Porta</a></li>
                                    <li><a href="#">Imperdiet</a></li>
                                    <li><a href="#">Vestibulum</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#">Long</a>
                        <ul>
                            <li><a href="#">Vestibulum imperdiet, felis eu ultricies facilisis</a></li>
                            <li><a href="#">Aliquam eu odio arcu, quis scelerisque nulla</a></li>
                            <li><a href="#">Porta</a></li>
                            <li><a href="#">Aliquam porttitor neque</a>
                                <ul>
                                    <li><a href="#">Felis eu ultricies facilisis</a></li>
                                    <li><a href="#">Aliquam eu odio arcu, quis scelerisque nulla</a>
                                        <ul>
                                            <li><a href="#">Ipsum</a></li>
                                            <li><a href="#">Placerat</a></li>
                                            <li><a href="#">Porta</a></li>
                                            <li><a href="#">Imperdiet</a></li>
                                            <li><a href="#">Vestibulum</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="#">Porta</a></li>
                                </ul>
                            </li>
                            <li><a href="#">Contact</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Dolor</a></li>
                    <li><a href="#">Consectetur</a></li>
                    <li><a href="#">Adipiscing</a></li>
                    <li><a href="#">Suspendisse</a></li>
                </ul>
            </div>
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
    <strong runat="server" id="PageTitle"></strong> Graphite
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

