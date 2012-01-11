<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagesMenu.ascx.cs" Inherits="Internal_Usercontrols_PagesMenu" %>



<div class="gp_block">
    <div class="gp_menu gp_menu_typeVertical">
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
