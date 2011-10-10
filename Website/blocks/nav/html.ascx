<%@ Control Language="C#" AutoEventWireup="true" CodeFile="html.ascx.cs" Inherits="blocks_nav_html" %>
<asp:Literal ID="Code" runat="server"></asp:Literal>

<asp:Literal ID="CodeHidden" runat="server">
    <div class="gp_nav_menu ##CSS_TYPE##">
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
</asp:Literal>