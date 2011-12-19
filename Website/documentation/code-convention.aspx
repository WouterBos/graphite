<%@ Page Title="Code Conventions" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="code-convention.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <strong runat="server" id="PageTitle"></strong> ‹ Documentation ‹ <a href="/">Graphite</a>
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
    
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>Code Conventions in Graphite</h1>
            <p><big>Consistent coding improves readability and the quality of Graphite in general. Here below you can find the code conventions it adheres to.</big></p>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Todo" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="gp_textHeading2">HTML</h1>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="gp_textHeading2">CSS</h1>
            <ul>
                <li>
                    <strong>All classes are prefixed with <code>gp_</code></strong><br />
                    This will minimize the risk of collission with other CSS code.
                </li>
                
                <li>
                    <strong>Only one level of nested styles</strong><br />
                    In order to make styling retracable through developer tools like Firebug.
                </li>
            </ul>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="gp_textHeading2">JavaScript</h1>
            <p>JavaScript code adheres to the <a href="http://google-styleguide.googlecode.com/svn/trunk/javascriptguide.xml">Google JavaScript Style Guide</a>. It is checked with Closure Linter. You can run a syntax check by executing the batch file <code>JavaScript - Lint.bat</code>. You can find this and other batch files in the <code>tools</code> folder of this project</p>
            <p>Some issues can be automatically resolved with <code>JavaScript - Autofix Lint issues.bat</code></p>
        </article>
    </div>
</asp:Content>
