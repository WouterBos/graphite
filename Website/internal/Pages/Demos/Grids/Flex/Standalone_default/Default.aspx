<%@ Page Title="Flex grid | Graphite" Language="C#" MasterPageFile="~/Internal/Pages/Demos/Grids/Flex/Standalone/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="../default.less" />
    
    <style>
        div.gp_header div.gp_main {
            background: #C8C7E5;
        }
 
        div.gp_content div.gp_main {
            background: #C97CE2;
        }

        div.gp_footer div.gp_main {
            background: #BAEAB8;
        }
    </style>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <asp:Literal ID="DemoHtml" runat="server"></asp:Literal>
</asp:Content>