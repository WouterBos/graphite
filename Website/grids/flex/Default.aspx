<%@ Page Title="Grid &amp; Basic Page" Language="C#" MasterPageFile="~/grids/flex/Default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <link rel="stylesheet" media="all" href="/css/gp_all-graphite-demo-grid.less" />
    
    <style>
        div.gp_header div.gp_main {
            background: #C8C7E5;
        }
 
        div.gp_content div.gp_main {
            background: #C97CE2;
        }

        div.gp_column1 {
            background: #eee;
        }
        div.gp_column2 {
            background: #ddd;
        }
        div.gp_column3 {
            background: #ccc;
        }
        div.gp_column4 {
            background: #bbb;
        }
        
        div.gp_footer div.gp_main {
            background: #BAEAB8;
        }
    </style>
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> Graphite
</asp:Content>
