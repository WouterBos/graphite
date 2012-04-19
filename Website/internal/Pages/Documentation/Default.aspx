<%@ Page Title="Getting started" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="Internal" TagName="BreadCrumb" Src="~/Internal/usercontrols/breadcrumb.ascx" %>



<asp:Content ID="Head" ContentPlaceHolderID="Head" Runat="Server">
    <script src="/internal/javascript/graphite.js" type="text/javascript"></script>
    <script src="/internal/javascript/graphite.blocks.js" type="text/javascript"></script>
    <script src="/internal/javascript/graphite.blocks.widgets.js" type="text/javascript"></script>
    <script src="/internal/javascript/graphite.blocks.widgets.accordion.js" type="text/javascript"></script>
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
            <h1>How to use Graphite</h1>
            <h2>Introduction</h2>
            <p>Graphite is a website template that you can download and copy to an IIS server. The website contains a start page and a subsite where you can find and copy code of UI components like menus, grids and forms.</p>

            <h2>Install and deploy</h2>
            <div class="gp_accordion">
                <dl>
                    <dt>1. Download</dt>
                    <dd>
                        <div class="gp_block">Graphite is hosted on Github where you can <a href="https://github.com/WouterBos/graphite/downloads">download the most stable version</a>. Be aware: Graphite is still in alpha phase.</div>
                    </dd>
                    
                    <dt>2. Install</dt>
                    <dd>
                        <div class="gp_block">Unpack the compressed file. If you open the Graphite folder you'll see a folder called <code>Website</code>. That's the root of your website. Now you can run the website either by opening it with Visual Studio or by creating a new website in IIS and point to the <code>Website</code> directory as the root.</div>
                    </dd>
                    
                    <dt>3. Select a UI component</dt>
                    <dd>
                        <div class="gp_block">Open the Graphite website in the browser and you'll see the introduction page. This page brings you to the demos en documentation in the <code>Internal</code> folder. Look in the &ldquo;Blocks&rdquo; section for the &ldquo;Menu&rdquo; component.</div>
                    </dd>
                    
                    <dt>4. Use the UI component</dt>
                    <dd>
                        <div class="gp_block">
                            <p>Click on the &ldquo;.html&rdquo; button to copy the menu code. Then open <code>/Start.aspx</code> in your editor. This is your basic page. Paste the code in a <code>asp:Content</code>-section and load the page in your browser. You'll see an unstyled list of links.</p>
                            <p>Lets add come style to it. Go back to the menu page and click on the &ldquo;.css&rdquo; button. Now open <code>/Css/all.less</code> and add the code in your clipboard to the bottom of the file, correct the paths by removing the <code>/Css/</code> and remove the <code>@import</code> rules that may already exist (like <code>gp_reset.less</code>). Now open <code>/Start.aspx</code> in your browser. If everything went well you see a styled menu.</p>
                        </div>
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



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>
