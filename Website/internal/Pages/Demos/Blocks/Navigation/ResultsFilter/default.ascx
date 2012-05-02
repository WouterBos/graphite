<%@ Control Language="C#" AutoEventWireup="true" CodeFile="default.ascx.cs" Inherits="Internal_Pages_Demos_Blocks_Navigation_ResultsFilter_default" %>



<div class="gp_searchFilter">
    <section>
        <fieldset>
            <legend><%=GetGlobalResourceObject("Graphite", "Category")%> A</legend>
            <ul>
                <li><label for="a1">Lorem</label> <input type="checkbox" id="a1" /></li>
                <li><label for="a2">Ipsum</label> <input type="checkbox" id="a2" /></li>
                <li><label for="a3">Dolor</label> <input type="checkbox" id="a3" /></li>
                <li><label for="a4">Sit amet</label> <input type="checkbox" id="a4" /></li>
            </ul>
            
            <legend><%=GetGlobalResourceObject("Graphite", "Category")%> B</legend>
            <ul>
                <li><label for="b1">Lorem</label> <input type="radio" id="b1" name="catb" /></li>
                <li><label for="b2">Ipsum</label> <input type="radio" id="b2" name="catb" /></li>
                <li><label for="b3">Dolor</label> <input type="radio" id="b3" name="catb" /></li>
                <li><label for="b4">Sit amet</label> <input type="radio" id="b4" name="catb" /></li>
            </ul>
            
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources: Graphite, Search %>"></asp:Button>
        </fieldset>
        
        RESULTS
    </section>
</div>