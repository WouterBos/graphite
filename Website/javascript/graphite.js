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
  var codeBox;
  
  
  // Gets HTML from demo and presents it as copy/paste code.
  function showHTML() {
    var pre = codeBox.querySelector('.html pre');

    var demoHTML = htmlRoot.innerHTML;
    if (demoHTML.match(/[\d\w]/) == null) {
      demoHTML = "No demo code found";
    } else {
      demoHTML = demoHTML.replace(/</g, "&lt;");
      demoHTML = demoHTML.replace(/>/g, "&gt;");
    }
    pre.innerHTML = demoHTML;
  }
  
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
  
  function prepareCodeBox() {
    codeBox = document.createElement('div');
    codeBox.className = 'codeBox gp_columns gp_columns_3';
    codeBox.innerHTML =
      '<ul class="gp_innerColumns">' +
      ' <li class="html gp_column gp_column1">' +
      '   <div class="gp_block">' +
      '     <strong class="localHeading">HTML</strong>' +
      '     <pre class="brush: xml"></pre>' +
      '   </div>' +
      ' </li>' +
      ' <li class="css gp_column gp_column2">' +
      '   <div class="gp_block">' +
      '     <strong class="localHeading">CSS</strong>' +
      '     <pre class="brush: css"></pre>' +
      '   </div>' +
      ' </li>' +
      ' <li class="javascript gp_column gp_column3">' +
      '   <div class="gp_block">' +
      '     <strong class="localHeading">JavaScript</strong>' +
      '     <pre class="brush: js"></pre>' +
      '   </div>' +
      ' </li>' +
      '</ul>';

      config.root.parentNode.insertBefore(codeBox, config.root.nextSibling);
  }
		
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
	  showCSS();
	  getJavaScript();
	}
}




