<%@ Control Language="C#" AutoEventWireup="true" CodeFile="default.ascx.cs" Inherits="Internal_Pages_Demos_Blocks_Navigation_ResultsFilter_default" %>



<div class="gp_searchFilter">
    <section>
        <div class="gp_textBlock gp_local_filterIntro">
            <p>The search results are filtered instantly if a value in this form below changes.</p>
        </div>

        <div class="gp_local_filter" data-filterType-era="and" data-filterType-members="or" data-ajaxurl="dummy.txt">
            <fieldset>
                <label>Keyword:</label>
                <input type="text" data-keyword="true" />

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> Era</legend>
                <ul>
                    <li><label for="a1">1960s</label> <input type="checkbox" id="a1" data-field="era" value="1960s" /></li>
                    <li><label for="a2">1990s</label> <input type="checkbox" id="a2" data-field="era" value="1990s" /></li>
                    <li><label for="a3">2000s</label> <input type="checkbox" id="a3" data-field="era" value="2000s" /></li>
                    <li><label for="a4">1890s - 1920s</label> <input type="checkbox" id="a4" data-field="era" value="1890s" /></li>
                </ul>
                
                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> Country</legend>
                <ul>
                    <li><label for="b1">UK</label> <input type="checkbox" id="b1" name="b" data-field="country" value="UK" /></li>
                    <li><label for="b2">Jamaica</label> <input type="checkbox" id="b2" name="b" data-field="country" value="Jamaica" /></li>
                    <li><label for="b3">USA</label> <input type="checkbox" id="b3" name="b" data-field="country" value="USA" /></li>
                    <li><label for="b4">France</label> <input type="checkbox" id="b4" name="b" data-field="country" value="France" /></li>
                    <li><label for="b5">Germany</label> <input type="checkbox" id="b5" name="b" data-field="country" value="Germany" /></li>
                    <li><label for="b6">Japan</label> <input type="checkbox" id="b6" name="b" data-field="country" value="Japan" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> Members</legend>
                <ul>
                    <li><label for="c1">1</label> <input type="radio" id="c1" data-field="members" value="1" /></li>
                    <li><label for="c2">2</label> <input type="radio" id="c2" data-field="members" value="2" /></li>
                    <li><label for="c3">3</label> <input type="radio" id="c3" data-field="members" value="3" /></li>
                    <li><label for="c4">4</label> <input type="radio" id="c4" data-field="members" value="4" /></li>
                </ul>

                <legend><%=GetGlobalResourceObject("Graphite", "Category")%> Genre</legend>
                <ul>
                    <li><label for="d1">Classic rock</label> <input type="checkbox" id="d1" data-field="genre" value="Classic rock" /></li>
                    <li><label for="d2">Britpop</label> <input type="checkbox" id="d2" data-field="genre" value="Britpop" /></li>
                    <li><label for="d3">Ska</label> <input type="checkbox" id="d3" data-field="genre" value="Ska" /></li>
                    <li><label for="d4">Folk rock</label> <input type="checkbox" id="d4" data-field="genre" value="Folk rock" /></li>
                    <li><label for="d5">Piano</label> <input type="checkbox" id="d5" data-field="genre" value="Piano" /></li>
                    <li><label for="d6">Rock</label> <input type="checkbox" id="d6" data-field="genre" value="Rock" /></li>
                    <li><label for="d7">Bollywood</label> <input type="checkbox" id="d7" data-field="genre" value="Bollywood" /></li>
                    <li><label for="d8">Electro</label> <input type="checkbox" id="d8" data-field="genre" value="Electro" /></li>
                    <li><label for="d9">Pop</label> <input type="checkbox" id="d9" data-field="genre" value="Pop" /></li>
                    <li><label for="d10">Rap</label> <input type="checkbox" id="d10" data-field="genre" value="Rap" /></li>
                    <li><label for="d11">Calypso</label> <input type="checkbox" id="d11" data-field="genre" value="Calypso" /></li>
                    <li><label for="d12">Alternative</label> <input type="checkbox" id="d12" data-field="genre" value="Alternative" /></li>
                </ul>
                
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources: Graphite, Search %>" CssClass="gp_local_submit"></asp:Button>
            </fieldset>
        </div>
        
        <div class="gp_local_resultsContainer">
            <div class="gp_local_controlOrder"></div>
            <div class="gp_local_controlBatch"></div>
            <div class="gp_local_results">
                <ul>
                    <li>Result #1</li>
                    <li>Result #2</li>
                    <li>Result #3</li>
                </ul>
            </div>
            <div class="gp_local_controlPaging"></div>
        </div>

        <div class="local_log gp_textAttention"></div>
    </section>
</div>