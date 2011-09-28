<%@ Page Title="Grid &amp; Basic Page" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <!--<link rel="stylesheet" media="all" href="/CSS/reset.less" />
    <link rel="stylesheet" media="all" href="/CSS/font.less" />
    <link rel="stylesheet" media="all" href="/CSS/font-textBlock.less" />
    <link rel="stylesheet" media="all" href="/CSS/grid.less" />
    <link rel="stylesheet" media="all" href="/CSS/grid-print.less" />-->
    <link rel="stylesheet" media="all" href="/CSS/all.less" />
    
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



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
    
</asp:Content>



<asp:Content ID="Column2_1" ContentPlaceHolderID="Column2_1" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 1st of 2 columns</strong></p>
        <p>Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aliquam posuere lorem sed libero rutrum aliquam. Ut consequat bibendum urna, id ultrices velit volutpat a. Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit.</p>
    </div>
</asp:Content>



<asp:Content ID="Column2_2" ContentPlaceHolderID="Column2_2" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 2nd of 2 columns</strong></p>
        <p>Fusce sagittis eros vel tellus varius adipiscing. Donec at mauris tellus. Quisque varius viverra urna, nec commodo sem lacinia quis. Sed varius luctus massa, quis malesuada ipsum ornare non.</p>
        <p>Eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 1st of 2 columns</strong></p>
        <p>Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 2nd of 2 columns</strong></p>
        <p>Aliquam posuere lorem sed libero rutrum aliquam. Ut consequat bibendum urna, id ultrices velit volutpat a. Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 3rd of 3 columns</strong></p>
        <p>Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Column4_1" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 1st of 4 columns</strong></p>
        <p>Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="Column4_2" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 2nd of 4 columns</strong></p>
        <p>Aliquam posuere lorem sed libero rutrum aliquam. Ut consequat bibendum urna, id ultrices velit volutpat a. Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Column4_3" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 3rd of 4 columns</strong></p>
        <p>Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="Column4_4" Runat="Server">
    <div class="gp_block gp_text">
        <p><strong>RTE block in 4th of 4 columns</strong></p>
        <p>Proin pulvinar eros quis nulla vestibulum sit amet mattis velit suscipit. Nullam diam nisl, dignissim quis faucibus eget, dictum non lorem. Vivamus eleifend hendrerit bibendum. </p>
    </div>
</asp:Content>



<asp:Content ID="Footer" ContentPlaceHolderID="Footer" Runat="Server">
    <div class="gp_block gp_text">
        <p>This is a footer</p>
    </div>
</asp:Content>
