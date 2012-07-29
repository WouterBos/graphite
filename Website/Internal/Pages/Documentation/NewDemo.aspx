<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/Masterpages/DefaultAlt1.master" AutoEventWireup="true" CodeFile="NewDemo.aspx.cs" Inherits="Internal_Pages_Documentation_NewDemo" %>
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
            <h1>Making a new demo</h1>
            <p><big>Demos are the heart of Graphite. Each demo showcases a reusable component. That way its easy to test and improve such a component.</big></p>
            
            <p>You start making a new demo by adding one in <code>/App_Data/Graphite/Internal/Sitemaps/demos.xml</code>. Each demo is defined in this XML file. Its structure resembles the folder structure in <code>/Internal/Pages/Demos</code>. Each leaf ends with a <code>&lt;demo&gt;</code> node.</p>
            <p>The menu demos are a good example of how you can build your demos. Here's a piece of code from the XML file:</p>
            
            <p>
                <pre>
&lt;demo>
    &lt;horizontal cssclass="gp_menu_typeHorizontal">
        &lt;files>
            &lt;html use_default_code="true" />
            &lt;css />
        &lt;/files>
        &lt;browsers>
            &lt;msie supports="7+" />
            &lt;firefox supports="2+" />
            &lt;chrome supports="10+" />
            &lt;safari supports="4+" />
            &lt;ipad supports="iOS5+" />
            &lt;opera supports="9.27+" />
        &lt;/browsers>
    &lt;/horizontal>

    &lt;verticalcollapse humanname="vertical collapse" cssclass="gp_menu_typeVertical gp_menu_typeCollapse">
        &lt;files>
            &lt;html use_default_code="true" />
            &lt;css />
            &lt;javascript />
        &lt;/files>
        &lt;browsers>
            &lt;msie supports="7+" />
            &lt;firefox supports="3.5+" />
            &lt;chrome supports="10+" />
            &lt;safari supports="4+" />
            &lt;ipad supports="iOS5+" />
            &lt;opera supports="9.27+" />
        &lt;/browsers>
    &lt;/verticalcollapse>
    
    &lt;!-- More code... -->
&lt;/demo>
                </pre>
            </p>
            
            <p>The code above will generate 2 demos of the same component. The <code>files</code> node defines which files are needed for this particular demo. In the case of the <code>horizontal</code> node, you can see that the <code>files</code> node contains 2 child nodes: <code>html</code> and <code>css</code>. By adding such a node, the demo will look for a *.less file in the demo folder. In case of the <code>css</code> node, the demo page is triggered to look for a file called <code>horizontal.less</code> in <code>/Internal/Pages/Demos/Navigation/Menu</code>. In case of <code>html</code>, it will load a file called <code>default.html</code>.</p>
            <p>In this example both demos share the same html because of the attribute <code>use_default_code="true"</code>. You can do this with any file type. If such an attribute is used, the demo page will look for a file called <code>default.*</code>.</p>
            <p>After you built your demo, you can browser-check it and document it in the <code>browsers</code> node.</p>
        </article>
    </div>
</asp:Content>



<asp:Content ID="Column3_1" ContentPlaceHolderID="Column3_1" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_2" ContentPlaceHolderID="Column3_2" Runat="Server">
</asp:Content>



<asp:Content ID="Column3_3" ContentPlaceHolderID="Column3_3" Runat="Server">
</asp:Content>
