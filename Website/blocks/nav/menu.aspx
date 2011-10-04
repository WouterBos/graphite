<%@ Page Title="Blocks › Navigation › Menu" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="blocks_nav_menu" %>



<asp:Content ID="Content8" ContentPlaceHolderID="DemoStage" Runat="Server">
    <div class="graphite_demoStage">
        <div class="gp_nav_menu gp_type1">
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
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="/CSS/blocks-nav-menu.less" />
    <link rel="stylesheet" media="all" href="/CSS/blocks-nav-menu-type1.less" />
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

