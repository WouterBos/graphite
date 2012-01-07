<%@ Page Title="Getting started" Language="C#" MasterPageFile="~/Internal/Masterpages/Default.master" AutoEventWireup="true" CodeFile="GettingStarted.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/Internal/usercontrols/breadcrumb.ascx" %>

<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <script src="/JavaScript/graphite.js" type="text/javascript"></script>
    <script src="/JavaScript/graphite.blocks.js" type="text/javascript"></script>
    <script src="/JavaScript/graphite.blocks.widgets.js" type="text/javascript"></script>
    <script src="/JavaScript/graphite.blocks.widgets.accordion.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Title" ContentPlaceHolderID="Title" Runat="Server">
    <Internal:Breadcrumb runat="server" ID="Breadcrumb" />
</asp:Content>



<asp:Content ID="Header" ContentPlaceHolderID="Header" Runat="Server">
</asp:Content>



<asp:Content ID="Intro" ContentPlaceHolderID="Intro" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1>How to use Graphite</h1>
            <h2>Introduction</h2>
            <p>Graphite is a website template that you can download and copy to an IIS server. The website contains a start page and a subsite where you can find and copy code of UI components like menus, grids and forms.</p>

            <h2>Install and deploy</h2>
            <div class="gp_accordion">
                <dl>
                    <dt>1. Download</dt>
                    <dd>Graphite is hosted on Github where you can <a href="https://github.com/WouterBos/graphite/downloads">download the most stable version</a>. Be aware: Graphite is still in alpha phase.</dd>
                    
                    <dt>2. Install</dt>
                    <dd>Unpack the compressed file. If you open the Graphite folder you'll see a folder called <code>Website</code>. That's the root of your website. Now you can run the website either by opening it with Visual Studio or by creating a new website in IIS and point to the <code>Website</code> directory as the root.</dd>
                    
                    <dt>3. Select a UI component</dt>
                    <dd>Open the Graphite website in the browser and you'll see the introduction page. This page brings you to the demos en documentation in the <code>Internal</code> folder. Look in the &ldquo;Blocks&rdquo; section for the &ldquo;Menu&rdquo; component.</dd>
                    <dt>4. Use the UI component</dt>
                    <dd>
                        Click on the &ldquo;.html&rdquo; button to copy the menu code. Then open <code>/Start.aspx</code> in your editor. This is your basic page. Paste the code in a <code>asp:Content</code>-section and load the page in your browser. You'll see an unstyled list of links.<br />
                        Lets add come style to it. Go back to the menu page and click on the &ldquo;.css&rdquo; button. Now open <code>/Css/all.less</code> and add the code in your clipboard to the bottom of the file, correct the paths by removing the <code>/Css/</code> and remove the <code>@import</code> rules that may already exist (like <code>gp_reset.less</code>). Now open <code>/Start.aspx</code> in your browser. If everything went well you see a styled menu.
                    </dd>
                </dl>
            </div>
            <script>
                var accordion = new graphite.blocks.widgets.accordion();
                accordion.init('div.gp_accordion');
            </script>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="Todo" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="gp_textHeading2">ASP.NET</h1>
            <p>ASP.NET code adheres to the <a href="/Internal/downloads/Submain_DotNET_Coding_Guidelines.pdf" target="_blank">code conventions of Submain</a>. Apart from that, Graphite is not related in any way to the company Submain.</p>
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



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
    <div class="gp_block gp_textBlock">
        <article>
            <h1 class="gp_textHeading2">JavaScript</h1>
            <p>JavaScript code adheres to the <a href="http://google-styleguide.googlecode.com/svn/trunk/javascriptguide.xml">Google JavaScript Style Guide</a>. It is checked with Closure Linter. You can run a syntax check by executing the batch file <code>JavaScript - Lint.bat</code>. You can find this and other batch files in the <code>tools</code> folder of this project</p>
            <p>Some issues can be automatically resolved with <code>JavaScript - Autofix Lint issues.bat</code>.</p>
        </article>
    </div>
</asp:Content>
