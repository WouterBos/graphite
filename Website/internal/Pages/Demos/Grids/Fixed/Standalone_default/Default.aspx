<%@ Page Title="Fixed column size grid | Graphite" Language="C#" MasterPageFile="Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="../default.less" />
    
    <style>
        div.gp_header div.gp_main {
            background: #f96;
        }
 
        div.gp_content div.gp_main {
            background: #69f;
        }

        div.gp_footer div.gp_main {
            background: #9f6;
        }
    </style>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <asp:Literal ID="DemoHtml" runat="server"></asp:Literal>
</asp:Content>