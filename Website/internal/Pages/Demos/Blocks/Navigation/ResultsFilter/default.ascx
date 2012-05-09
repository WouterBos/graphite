<%@ Control Language="C#" AutoEventWireup="true" CodeFile="default.ascx.cs" Inherits="Internal_Pages_Demos_Blocks_Navigation_ResultsFilter_default" %>



<div class="gp_searchFilter">
    <section>
        <div class="gp_local_filter" data-filterType-c="and" data-filterType-d="or" data-ajaxurl="?dummyAjaxRequestHandler.aspx">
            <fieldset>
                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> A</legend>
                <ul>
                    <li><label for="a1">Lorem</label> <input type="checkbox" id="a1" data-field="a" value="1" /></li>
                    <li><label for="a2">Ipsum</label> <input type="checkbox" id="a2" data-field="a" value="2" /></li>
                    <li><label for="a3">Dolor</label> <input type="checkbox" id="a3" data-field="a" value="3" /></li>
                    <li><label for="a4">Sit amet</label> <input type="checkbox" id="a4" data-field="a" value="4" /></li>
                </ul>
                
                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> B</legend>
                <ul>
                    <li><label for="b1">Lorem</label> <input type="radio" id="b1" name="b" data-field="b" value="1" /></li>
                    <li><label for="b2">Ipsum</label> <input type="radio" id="b2" name="b" data-field="b" value="2" /></li>
                    <li><label for="b3">Dolor</label> <input type="radio" id="b3" name="b" data-field="b" value="3" /></li>
                    <li><label for="b4">Sit amet</label> <input type="radio" id="b4" name="b" data-field="b" value="4" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> C</legend>
                <ul>
                    <li><label for="c1">Lorem</label> <input type="checkbox" id="c1" data-field="c" value="1" /></li>
                    <li><label for="c2">Ipsum</label> <input type="checkbox" id="c2" data-field="c" value="2" /></li>
                    <li><label for="c3">Dolor</label> <input type="checkbox" id="c3" data-field="c" value="3" /></li>
                    <li><label for="c4">Sit amet</label> <input type="checkbox" id="c4" data-field="c" value="4" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> D</legend>
                <ul>
                    <li><label for="d1">Lorem</label> <input type="checkbox" id="d1" data-field="d" value="1" /></li>
                    <li><label for="d2">Ipsum</label> <input type="checkbox" id="d2" data-field="d" value="2" /></li>
                    <li><label for="d3">Dolor</label> <input type="checkbox" id="d3" data-field="d" value="3" /></li>
                    <li><label for="d4">Sit amet</label> <input type="checkbox" id="d4" data-field="d" value="4" /></li>
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
    </section>
</div>