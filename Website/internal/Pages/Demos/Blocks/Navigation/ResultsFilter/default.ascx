<%@ Control Language="C#" AutoEventWireup="true" CodeFile="default.ascx.cs" Inherits="Internal_Pages_Demos_Blocks_Navigation_ResultsFilter_default" %>



<div class="gp_searchFilter">
    <section>
        <div class="gp_local_filter">
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