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
 * Extracts code from demo and presents to user.
 *
 * @since 1.0 - 2010-12-18
 * @version 1.0 - 2010-12-18
 *
 * @constructor
 * @param {Object} arg_config Config object.
 *    .root: The demo container.
 */
graphite.internal.demo = function(arg_config) {
  var config = {
    root: arg_config.root
  };

  sourceCode = graphite.internal.demo.data.sourceCode;

  function setEvents() {
    var rootLinks = document.getElementsByTagName('a');

    for (var i = 0; i < rootLinks.length; i++) {
      // ASCX
      if (rootLinks[i].className.indexOf('graphite_getCodeAscx') != -1) {
        setEvent(
          rootLinks[i],
          sourceCode.ascx
        );
      }
      // Codebehind
      if (rootLinks[i].className.indexOf('graphite_getCodeCodeBehind') != -1) {
        setEvent(
          rootLinks[i],
          sourceCode.codebehind
        );
      }

      // HTML
      if (rootLinks[i].className.indexOf('graphite_getCodeHtml') != -1) {
        setEvent(
          rootLinks[i],
          sourceCode.html
        );
      }
      // CSS
      if (rootLinks[i].className.indexOf('graphite_getCodeCss') != -1) {
        setEvent(
          rootLinks[i],
          sourceCode.css
        );
      }
      // JavaScript
      if (rootLinks[i].className.indexOf('graphite_getCodeJs') != -1) {
        sourceCode.js = sourceCode.js.replace(/\#\#\#GP\#\#\#SCRIPT/g,
                                              '<script');
        sourceCode.js = sourceCode.js.replace(/\#\#\#GP\#\#\#\/SCRIPT/g,
                                              '</script');
        setEvent(
          rootLinks[i],
          sourceCode.js
        );
      }
    }

    function setEvent(copyLink, source) {
      if (copyLink && source) {
        var clipHtml = new ZeroClipboard.Client();
        clipHtml.setHandCursor(true);

        clipHtml.addEventListener('mouseup', function(client) {
          clipHtml.setText(source);
          var feedbackElement = jQuery('*.graphite_getCodeFeedback');
          feedbackElement.html('Code copied');
          feedbackElement.show();
          feedbackElement.css('opacity', 1);
          feedbackElement.delay(1500).animate(
            {
              opacity: 0
            },
            1000
          );
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
      'load',
      true
    );
  }
};



/**
 * Namespace branch to attach JSON data for the demo.
 */
graphite.internal.demo.data = {};
