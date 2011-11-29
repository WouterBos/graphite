<%@ Control Language="C#" AutoEventWireup="true" CodeFile="folders.ascx.cs" Inherits="internal_usercontrols_folders" %>



<div class="gp_block">
    <div class="gp_textBlock">
        <h1>Contents of {BLOCKS}</h1>
    </div>

    <div class="gp_sitemap">
        <asp:Repeater ID="rptFolders" runat="server" onitemdatabound="rptFolders_ItemDataBound">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="hlFolder" runat="server">HyperLink</asp:HyperLink>
                </li>
            </ItemTemplate>
            
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
