<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="Css.aspx.cs" Inherits="Internal_Pages_Documentation_CodeConventions_Css" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/Internal/usercontrols/breadcrumb.ascx" %>



<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <Internal:Breadcrumb runat="server" ID="Breadcrumb" />
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Column2_1" Runat="Server">
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Column2_2" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>CSS</h1>
            <ul>
                <li>
                    <strong>All classes are prefixed with <code>gp_</code></strong><br />
                    This will minimize the risk of collission with other CSS code.
                </li>
                
                <li>
                    <strong>Each block has one only root class</strong><br />
                    All other selectors are nested within the root selector. Example:<br />
<pre>*.gp_login {
    table.gp_loginRoot {
        margin: 0 auto;
        width: 245px;
    }
    /* more nested styles */
}</pre>
                </li>

                <li>
                    <strong>The prefix is followed by one word</strong><br />
                    Like this: <code>gp_menu</code>. All blocks that use that class as a base will be appended to the existing class name like this: <code>gp_menu_typeHorizontal</code>.<br />
                    The selector in the CSS will use both classes: <code>.gp_menu.gp_menu_typeHorizontal { /* code */ }</code>.
                </li>
            </ul>
        </article>
    </div>
</asp:Content>
