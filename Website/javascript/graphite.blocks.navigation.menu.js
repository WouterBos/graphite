/**
 * @fileOverview Graphite basic menu
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2011-11-10
 * @version 0.1 - 2011-11-10
 */







/**
 * @namespace Menu functionality
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
graphite.blocks.navigation.menu = ( function() {
  /* Start public */
  return {
    /**
     * Connects a function to an event of a single element
     *
     * @since 1.0 - 2011-11-03
     * @version 1.0 - 2011-11-03
     * @example
        graphite.events.addevent(
          document.getElementById('elementId'),
          function() {alert('foo')},
          "click",
          true
        );
     */
    initCollapse: function() {
      // Get the menu root element. We'll use jQuery for brevity
      var menuListItems = jQuery('.gp_menu.gp_menu_typeCollapse li');
      var cssDelay = new graphite.css.cssDelay(true);
      
      menuListItems.each(function() {
        cssDelay.addItem(
          {
            element: jQuery(this)[0],
            cssclass: 'gp_hover',
            eventType: 'mouseover',
            action: 'add',
            time: 0
          }
        );
        cssDelay.addItem(
          {
            element: jQuery(this)[0],
            cssclass: 'gp_hover',
            eventType: 'mousemove',
            action: 'add',
            time: 0
          }
        );
        cssDelay.addItem(
          {
            element: jQuery(this)[0],
            cssclass: 'gp_hover',
            eventType: 'mouseout',
            action: 'remove',
            time: 1000
          }
        );
      });
      
      cssDelay.createEvents();
    }
  }
  /* End public */
})();





graphite.css = {};





/**
 * @namespace ...
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
graphite.css.cssDelay = function(arg_singleSibling) {
  var _arr = new Array();
  var _singleSibling = arg_singleSibling || false;
  
  
  
  /* Start public */
  /**
   * Adds and removes class with delay
   *
   * @since 1.0 - 2011-11-03
   * @version 1.0 - 2011-11-03
   */
  this.addItem = function(arrItem) {
    _arr.push(arrItem);
  }

  this.createEvents = function() {
    // Loop through array and create events.
    for (var i = 0; i < _arr.length; i++) {
      graphite.events.addEvent(
        _arr[i].element,
        (function(arrItem) {
          return function() {
              toggleClass(arrItem);
          }
        })(_arr[i]),
        _arr[i].eventType,
        true
      );
    }

    // The event handler.
    function toggleClass(arrItem) {
      // Create timeout object on element.
      if (typeof(arrItem.element.gp_timeout) == 'undefined') {
        arrItem.element.gp_timeout = {};
      }
      
      // Clear possible existing timeouts when removing classes.
      if (typeof(arrItem.element.gp_timeout[arrItem.action + '_' + arrItem.cssclass]) != 'undefined') {
        if (arrItem.action == 'remove') {
          clearTimeout(arrItem.element.gp_timeout[arrItem.action + '_' + arrItem.cssclass]);
        }
      }
      
      // The actual adding or removing of classes.
      arrItem.element.gp_timeout[arrItem.action + '_' + arrItem.cssclass] = setTimeout(
        function() {
          // Add class.
          if (arrItem.action == 'add') {
            if (arrItem.element.className.indexOf(arrItem.cssclass) == -1) {
              arrItem.element.className += ' ' + arrItem.cssclass;
            }
            // Make sure only one sibling has that class.
            if (_singleSibling == true && arrItem.eventType == "mouseover") {
              var parent = arrItem.element.parentNode;
              for (var i = 0; i < parent.childNodes.length; i++) {
                if (parent.childNodes[i].nodeType == 1) {
                  var classNameRegEx = new RegExp(arrItem.cssclass, 'g');
                  parent.childNodes[i].className = parent.childNodes[i].className.replace(classNameRegEx, '');
                }
              }
            }
          }
          // Remove class.
          if (arrItem.action == "remove") {
            var classNameRegEx = new RegExp(arrItem.cssclass, 'g');
            arrItem.element.className = arrItem.element.className.replace(classNameRegEx, '');
          }
        },
        arrItem.time
      );
    }
  }
  /* End public */
};
