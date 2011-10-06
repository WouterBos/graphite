/**
 * @fileOverview JavaScript for Graphite demos
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 * @since 0.1 - 2011-11-05
 * @version 0.1 - 2011-11-05
 */

 
 
 
 
 
/**
 * @namespace Root namespace for graphite
 */
var graphite = {};



/**
 * @namespace Extracts code from demo and presents to user.
 *
 * @param {Object} root The demo container.
 */
graphite.demo = function(arg_config) {
  var config = {
    root: arg_config.root,
    cssFiles: []
  }
  
  if (typeof(arg_config.cssFiles) == 'object') {
    for (var i = 0; i < arg_config.cssFiles.length; i++){
    	config.cssFiles.push(arg_config.cssFiles[i]);
    };
  }
  
  var htmlRoot = config.root.querySelector('div.graphite_demoStage_html');
  var codeBox;
  
  
  // Gets HTML from demo and presents it as copy/paste code.
  function showHTML() {
    var pre = codeBox.querySelector('.html pre');

    var demoHTML = htmlRoot.innerHTML;
    demoHTML = demoHTML.replace(/</g, "&lt;");
    demoHTML = demoHTML.replace(/>/g, "&gt;");
    pre.innerHTML = demoHTML;
  }
  
  function prepareCodeBox() {
    codeBox = document.createElement('div');
    codeBox.className = 'codeBox';
    codeBox.innerHTML =
      '<ul>' +
      ' <li class="html">' +
      '   <strong class="localHeading">HTML</strong>' +
      '   <pre></pre>' +
      ' </li>' +
      ' <li class="css">' +
      '   <strong class="localHeading">CSS</strong>' +
      '   <pre></pre>' +
      ' </li>' +
      ' <li class="javascript">' +
      '   <strong class="localHeading">JavaScript</strong>' +
      '   <pre></pre>' +
      ' </li>' +
      '</ul>';

      config.root.parentNode.insertBefore(codeBox, config.root.nextSibling);
  }
	
	// HTML
	// If demo HTML is found
		// Get HTML source from DOM
		// Present HTML in textarea
	// If no demo HTML was found
		// Show text: "No HTML used"
	
	// CSS
	// If CSS is used
		// Get CSS files that have been used  from DOM
		// Make CSS readable
		// Present CSS in textarea
		// Add references to .less files below textarea
	// If no CSS is used
		// Show text: "No CSS used"
	
	// JAVASCRIPT
	// Get JavaScript files that have been used from DOM
	// Get JavaScript code that has been used in the demo page
	// Present JavaScript in textarea
	// Add references to JavaScript libraries below textarea
	
	// Setup copy links for each textarea
	
	this.extractCode = function() {
	  // Setup cut 'n paste containers
	  prepareCodeBox();
	  
	  showHTML();
	}
}




