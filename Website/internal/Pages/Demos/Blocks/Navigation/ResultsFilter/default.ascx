<%@ Control Language="C#" AutoEventWireup="true" CodeFile="default.ascx.cs" Inherits="Internal_Pages_Demos_Blocks_Navigation_ResultsFilter_default" %>



<div class="gp_searchFilter">
    <section>
        <div class="gp_local_filter" data-filterType-c="and" data-filterType-d="or" data-ajaxurl="dummy.txt">
            <fieldset>
                <label>Keyword:</label>
                <input type="text" data-keyword="true" />

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> A</legend>
                <ul>
                    <li><label for="a1">Lorem</label> <input type="checkbox" id="a1" data-field="a" value="Lorem" /></li>
                    <li><label for="a2">Ipsum</label> <input type="checkbox" id="a2" data-field="a" value="Ipsum" /></li>
                    <li><label for="a3">Dolor</label> <input type="checkbox" id="a3" data-field="a" value="Dolor" /></li>
                    <li><label for="a4">Sit amet</label> <input type="checkbox" id="a4" data-field="a" value="Site amet" /></li>
                </ul>
                
                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> B</legend>
                <ul>
                    <li><label for="b1">Un</label> <input type="radio" id="b1" name="b" data-field="b" value="Un" /></li>
                    <li><label for="b2">Deux</label> <input type="radio" id="b2" name="b" data-field="b" value="Deux" /></li>
                    <li><label for="b3">Trois</label> <input type="radio" id="b3" name="b" data-field="b" value="Trois" /></li>
                    <li><label for="b4">Quatre</label> <input type="radio" id="b4" name="b" data-field="b" value="Quatre" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> C</legend>
                <ul>
                    <li><label for="c1">If</label> <input type="checkbox" id="c1" data-field="c" value="If" /></li>
                    <li><label for="c2">The</label> <input type="checkbox" id="c2" data-field="c" value="The" /></li>
                    <li><label for="c3">Inverting</label> <input type="checkbox" id="c3" data-field="c" value="Inverting" /></li>
                    <li><label for="c4">Core</label> <input type="checkbox" id="c4" data-field="c" value="Core" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> D</legend>
                <ul>
                    <li><label for="d1">Foo</label> <input type="checkbox" id="d1" data-field="d" value="Foo" /></li>
                    <li><label for="d2">Bar</label> <input type="checkbox" id="d2" data-field="d" value="Bar" /></li>
                    <li><label for="d3">FooBar</label> <input type="checkbox" id="d3" data-field="d" value="FooBar" /></li>
                    <li><label for="d4">Baz</label> <input type="checkbox" id="d4" data-field="d" value="Baz" /></li>
                </ul>
                
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources: Graphite, Search %>"></asp:Button>
            </fieldset>
        </div>
        
        <div class="gp_local_results">
            <ul>
                <li>Result #1</li>
                <li>Result #2</li>
                <li>Result #3</li>
            </ul>
        </div>

        <div class="local_log gp_textAttention"></div>
    </section>
</div>