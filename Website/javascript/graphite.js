/**
 * @fileOverview Basic functionality for Graphite
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2011-11-03
 * @version 0.1 - 2011-11-03
 */








if (typeof(graphite) == 'undefined') {
  /**
   * @namespace Root namespace for Graphite
   */
  var graphite = {};
} else {
  throw Error('Graphite error: root library may only be loaded once.');
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
          eval('element.on' + eventType +
               ' = function(e) { oldEvent(e); eventFunction(e); }');
        }
      }
    }
  };
  /* End public */
})();
