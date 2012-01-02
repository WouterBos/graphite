<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="gp_blocks_navigation_menu" %>



<asp:Panel ID="pnlRoot" runat="server" CssClass="gp_menu">
    <ul>
        <li><a href="#">Short words</a>
            <ul>
                <li><a href="#">Dolor</a>
                    <ul>
                        <li><a href="#">Ipsum</a></li>
                        <li><a href="#">Placerat</a></li>
                        <li><a href="#">Porta</a></li>
                        <li><a href="#">Imperdiet</a></li>
                        <li><a href="#">Vestibulum</a></li>
                    </ul>
                </li>
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
        <li class="gp_active"><a href="#">Active item</a>
            <ul>
                <li><a href="#">Vestibulum imperdiet, felis eu ultricies facilisis</a></li>
                <li><a href="#">Aliquam eu odio arcu, quis scelerisque nulla</a></li>
                <li><a href="#">Porta</a></li>
                <li class="gp_active"><a href="#">Aliquam porttitor neque</a>
                    <ul>
                        <li><a href="#">Felis eu ultricies facilisis</a></li>
                        <li class="gp_active"><a href="#">Aliquam eu odio arcu, quis scelerisque nulla</a>
                            <ul>
                                <li><a href="#">Ipsum</a></li>
                                <li><a href="#">Placerat</a></li>
                                <li class="gp_active gp_activePage"><a href="#">Porta</a></li>
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
</asp:Panel>