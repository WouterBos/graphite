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
graphite.blocks.navigation.menu = (function() {
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
  };
  /* End public */
})();





/**
 * Container for CSS related functionality
 *
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
graphite.css = {};





/**
 * Adds and removes classes with a certain delay
 *
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 * @constructor
 *
 * @param {Boolean} arg_singleSibling If true, only one sibling can have the
 *    supplied class. Default value is false.
 *
 * @example
   var cssDelay = new graphite.css.cssDelay(true);
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
       eventType: 'mouseout',
       action: 'remove',
       time: 1000
     }
   );
 */
graphite.css.cssDelay = function(arg_singleSibling) {
  var _arr = new Array();
  var _singleSibling = arg_singleSibling || false;



  /* Start public */
  /**
   * Adds and removes class with delay
   *
   * @param {Object} classEvent Configuration object with DOM element, class,
   *    event, etc.
   * @example
     var cssDelay = new graphite.css.cssDelay(true);
     cssDelay.addItem(
       {
         element: jQuery(this)[0],
         cssclass: 'gp_hover',
         eventType: 'mouseover',
         action: 'add',
         time: 0
       }
     );
   */
  this.addItem = function(classEvent) {
    _arr.push(classEvent);
  }

  this.createEvents = function() {
    // Loop through array and create events.
    for (var i = 0; i < _arr.length; i++) {
      graphite.events.addEvent(
        _arr[i].element,
        (function(classEvent) {
          return function() {
              toggleClass(classEvent);
          }
        })(_arr[i]),
        _arr[i].eventType,
        true
      );
    }

    // The event handler.
    function toggleClass(classEvent) {
      var timeoutName = classEvent.action + '_' + classEvent.cssclass;

      // Create timeout object on element.
      if (typeof(classEvent.element.gp_timeout) == 'undefined') {
        classEvent.element.gp_timeout = {};
      }

      // Clear possible existing timeouts when removing classes.
      if (typeof(classEvent.element.gp_timeout[timeoutName]) != 'undefined') {
        if (classEvent.action == 'remove') {
          clearTimeout(classEvent.element.gp_timeout[timeoutName]);
        }
      }

      // The actual adding or removing of classes.
      classEvent.element.gp_timeout[timeoutName] = setTimeout(
        function() {
          // Add class.
          if (classEvent.action == 'add') {
            var className = classEvent.element.className;
            if (className.indexOf(classEvent.cssclass) == -1) {
              classEvent.element.className += ' ' + classEvent.cssclass;
            }
            // Make sure only one sibling has that class.
            if (_singleSibling == true && classEvent.eventType == 'mouseover') {
              var parent = classEvent.element.parentNode;
              for (var i = 0; i < parent.childNodes.length; i++) {
                if (parent.childNodes[i].nodeType == 1) {
                  var classNameRegEx = new RegExp(classEvent.cssclass, 'g');
                  var className = parent.childNodes[i].className;
                  var newClassName = className.replace(classNameRegEx, '');
                  parent.childNodes[i].className = newClassName;
                }
              }
            }
          }
          // Remove class.
          if (classEvent.action == 'remove') {
            var classNameRegEx = new RegExp(classEvent.cssclass, 'g');
            var className = classEvent.element.className;
            var newClassName = className.replace(classNameRegEx, '');
            classEvent.element.className = newClassName;
          }
        },
        classEvent.time
      );
    }
  }
  /* End public */
};
