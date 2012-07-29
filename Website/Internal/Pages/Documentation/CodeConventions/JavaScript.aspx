<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="JavaScript.aspx.cs" Inherits="Internal_Pages_Documentation_CodeConventions_JavaScript" %>
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
            <h1 class="gp_textHeading2">JavaScript</h1>
            <p>JavaScript code adheres to the <a href="http://google-styleguide.googlecode.com/svn/trunk/javascriptguide.xml">Google JavaScript Style Guide</a>. It is checked with Closure Linter. You can run a syntax check by executing the batch file <code>JavaScript - Lint.bat</code>. You can find this and other batch files in the <code>tools</code> folder of this project</p>
            <p>Some issues can be automatically resolved with <code>JavaScript - Autofix Lint issues.bat</code>.</p>
        </article>
    </div>
</asp:Content>
