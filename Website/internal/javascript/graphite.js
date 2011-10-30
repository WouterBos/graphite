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
  }
  
  sourceCode = graphite.demo.data.sourceCode;
  
  function setEvents() {
    setEvent(
      config.root.querySelector('*.graphite_getCodeHtml'),
      sourceCode.html
    );
    
    setEvent(
      config.root.querySelector('*.graphite_getCodeCss'),
      sourceCode.css
    );
    
    sourceCode.js = sourceCode.js.replace(/\#\#\#GRAPHITE\#\#\#SCRIPT/g, '<script');
    sourceCode.js = sourceCode.js.replace(/\#\#\#GRAPHITE\#\#\#\/SCRIPT/g, '</script');
    setEvent(
      config.root.querySelector('*.graphite_getCodeJs'),
      sourceCode.js
    );
    
    
    function setEvent(copyLink, source) {
      if (copyLink && source) {
        var clipHtml = new ZeroClipboard.Client();
        clipHtml.setHandCursor( true );
        
        clipHtml.addEventListener('mouseup', function (client) {
          clipHtml.setText(source);
          if (console && console.log) {
            console.log('copied');
          }
        });
  
        clipHtml.glue( copyLink );
      }
    }
  }
  		
	this.extractCode = function() {
	  // Setup cut 'n paste containers
	  window.addEventListener('load', function() {
	    setEvents();
	  })
	}
}



graphite.demo.data = {};
