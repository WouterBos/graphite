/**
 * @fileOverview Basic functionality for Graphite
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2011-11-03
 * @version 0.1 - 2011-11-03
 */








/**
 * @namespace Root namespace for Graphite
 */
if (typeof(graphite) == 'undefined') {
  // Only create Graphite object once
  var graphite = {};
} else {
  if (!document.getElementById('GraphiteDemoSite')) {
    throw Error('Graphite error: root library may only be loaded once.');
  }
}





/**
 * @namespace Event manager
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
graphite.events = (function() {
  /* Start public */
  return {
    /**
     * Connects a function to an event of a single element
     *
     * @since 1.0 - 2011-11-03
     * @version 1.0 - 2011-11-03
     * @param {Object} element The element on which the event is placed.
     * @param {Function} eventFunction The function that has to be linked to
     *    the event.
     * @param {String} eventType Name of the event.
     * @param {Boolean} legacy If this value is false or does not exist,
     *    the event will be created with "addEventHandler".
     * @example
        graphite.events.addEvent(
          document.getElementById('elementId'),
          function() {alert('foo')},
          "click",
          true
        );
     */
    addEvent: function(element, eventFunction, eventType, legacy) {
      //var error;
      //error = Estate.Check.VariableType( element, "object" )
      //if (error != "") throw new Error( error );
      //error = Estate.Check.VariableType( eventFunction, "function" )
      //if (error != "") throw new Error( error );

      if (legacy && legacy == true) {
        if (element instanceof Array) {
          for (var i = 0; i < element.length; i++) {
            addLegacyEvent(element[i]);
          }
        } else {
          addLegacyEvent(element);
        }
      } else {
        element.addEventListener(eventType, eventFunction);
      }

      function addLegacyEvent(element) {
        var oldEvent = eval('element.on' + eventType);
        var eventContentType = eval('typeof element.on' + eventType);

        if (eventContentType != 'function') {
          eval('element.on' + eventType + ' = eventFunction');
        } else {
          eval('element.on' + eventType + ' = function(e) { oldEvent(e); eventFunction(e); }');
        }
      }
    }
  };
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
            if (_singleSibling == true && arrItem.eventType == 'mouseover') {
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
          if (arrItem.action == 'remove') {
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
