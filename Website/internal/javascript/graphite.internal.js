/**
 * @fileOverview JavaScript for Graphite demos
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 * @since 0.1 - 2011-11-05
 * @version 0.1 - 2011-11-05
 */

 
 
 
 
 
/**
 * @namespace Root namespace for graphite
 */
graphite.internal = {};



/**
 * @namespace Extracts code from demo and presents to user.
 *
 * @param {Object} root The demo container.
 */
graphite.internal.demo = function(arg_config) {
  var config = {
    root: arg_config.root
  }
  
  sourceCode = graphite.internal.demo.data.sourceCode;
  
  function setEvents() {
    var getCodeHtml;
    var getCodeCss;
    var getCodeJs;
    var rootLinks = document.getElementsByTagName('a');
    for (var i = 0; i < rootLinks.length; i++) {
      if (rootLinks[i].className.indexOf('graphite_getCodeHtml') != -1) {
        getCodeHtml = rootLinks[i];
      }
      if (rootLinks[i].className.indexOf('graphite_getCodeCss') != -1) {
        getCodeCss = rootLinks[i];
      }
      if (rootLinks[i].className.indexOf('graphite_getCodeJs') != -1) {
        getCodeJs = rootLinks[i];
      }
    }
    
    setEvent(
      getCodeHtml,
      sourceCode.html
    );
    
    setEvent(
      getCodeCss,
      sourceCode.css
    );
    
    sourceCode.js = sourceCode.js.replace(/\#\#\#GRAPHITE\#\#\#SCRIPT/g, '<script');
    sourceCode.js = sourceCode.js.replace(/\#\#\#GRAPHITE\#\#\#\/SCRIPT/g, '</script');
    setEvent(
      getCodeJs,
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
  
        clipHtml.glue(copyLink);
      }
    }
  }
  		
	this.extractCode = function() {
	  // Setup cut 'n paste containers
    graphite.events.addEvent(
      window,
      setEvents,
      "load",
      true
    );
	}
}



graphite.internal.demo.data = {};
