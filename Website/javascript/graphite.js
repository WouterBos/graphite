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
    cssFiles: arg_config.cssFiles
  }
    
  var htmlRoot = config.root.querySelector('div.graphite_demoStage_html');
  var codeBox = config.root.querySelector('.js_codeBox');
  
  
  // Gets Less code from demo and presents it as copy/paste code.
  function showCSS() {
    if (typeof(config.cssFiles) == "object") {
      var path = document.location.pathname;
      removeIndex = path.lastIndexOf('/');
      path = path.substring(0, removeIndex + 1);
      var fileName = config.cssFiles.getAttribute('href').replace('.less', '');
      getLessCode(path + fileName);
      
      // Also show plain CSS code
      var link = document.createElement('a');
      link.innerHTML = 'Get plain CSS';
      link.href = config.cssFiles.href;
      link.className = 'preLink';
      var title = codeBox.querySelector('.css pre');
      title.parentNode.insertBefore(link, title);
    };

    function getLessCode(lessLocation) {
      var xmlhttp = new XMLHttpRequest();
      xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
          var pre = codeBox.querySelector('.css pre');
          pre.innerHTML = xmlhttp.responseText;
          
          SyntaxHighlighter.defaults['gutter'] = false;
          SyntaxHighlighter.all();
        }
      }
      xmlhttp.open('GET', '/getless.aspx?less=' + lessLocation, true);
      xmlhttp.send();
    }
  }
  
  // Gets JavaScript code from demo and presents it as copy/paste code
  function getJavaScript() {
    var pre = codeBox.querySelector('.javascript pre');
    var script = config.root.querySelector('script');
    
    if (script == null) {
      pre.innerHTML = '//No JavaScript used.';
    } else {
      pre.innerHTML = script.innerHTML;
    }
  }
  		
	// JAVASCRIPT
	// Get JavaScript files that have been used from DOM
	// Get JavaScript code that has been used in the demo page
	// Present JavaScript in textarea
	// Add references to JavaScript libraries below textarea
	
	// Setup copy links for each textarea
	
	this.extractCode = function() {
	  // Setup cut 'n paste containers
	  
	  //getJavaScript();
	  //showCSS();
	}
}




