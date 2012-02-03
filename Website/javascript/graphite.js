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
 * @namespace Methods to validate function arguments. Use these methods liberally in all
 *            code that is part of the Estate object.
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
graphite.check = (function() {
  function literalsAreCompatible(mainLiteral, updateLiteral) {
    for (prop in updateLiteral) {
      if (typeof (mainLiteral[prop]) == "undefined") {
        return "The variable '" + prop + "'in the object literal cannot be merged with the original object literal.";
      }

      if (typeof (updateLiteral[prop]) != typeof (mainLiteral[prop])) {
        return "The variable '" + prop + " is of the wrong type. It is '" + typeof (updateLiteral[prop]) + "' but it should be '" + typeof (mainLiteral[prop]) + "'";
      }
      
      if ( typeof( updateLiteral[prop] ) == "object" && typeof( updateLiteral[prop].tagName ) != "string" ) {
        // Descend down the object tree, but only if it's not a DOM element
        literalsAreCompatible( mainLiteral[prop], updateLiteral[prop] );
      }
    }
  }



  /* Start public */
  return {
    /**
    * Checks if the right amount of arguments are used when calling the function
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-03-22
    * @param {Number} CurrentArgumentsLength The number of arguments that have been supplied
    * @param {Number|Number[]} CorrectArgumentsLength The number of arguments that are required
    * @example
    * error = graphite.check.ArgumentsCount( arguments.length, [0, 1] );
    * if ( error != "" ) throw new Error( error );
    */
    ArgumentsCount: function(CurrentArgumentsLength, CorrectArgumentsLength) {
      var error
      if (arguments.length != 2) throw new Error("Arguments count must be 2");
      error = graphite.check.variableType(CurrentArgumentsLength, "number");
      if (error != "") throw new Error(error);

      var CorrectArgumentsCount = false;
      if (typeof (CorrectArgumentsLength) == "number") {
        if (CurrentArgumentsLength == CorrectArgumentsLength) {
          CorrectArgumentsCount = true;
        }
      } else if (typeof (CorrectArgumentsLength) == "array" || typeof (CorrectArgumentsLength) == "object") {
        for (var i = 0; i < CorrectArgumentsLength.length; i++) {
          if (CurrentArgumentsLength == CorrectArgumentsLength[i]) {
            CorrectArgumentsCount = true
          }
        }
      }

      if (CorrectArgumentsCount == false) {
        return "Wrong number of arguments. There argument count should be " + CorrectArgumentsLength + ", but it is " + CurrentArgumentsLength;
      } else {
        return ""
      }
    },

    /**
    * Checks if an element with a particular id exists
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-02-23
    * @param {String} ElementID The ID of the element that will be checked if it exists.
    * @param {String} [RequiredTagName] The tagname that the element should have.
    * @example
    * error = graphite.check.ElementById( 'elID', 'div' );
    * if ( error != "" ) throw new Error( error );
    */
    ElementById: function(ElementID, RequiredTagName) {
      if (typeof (ElementID) != "string") {
        return "Provided element id is not a string but  '" + typeof (ElementID) + "'.";
      }
      if (!document.getElementById(ElementID)) {
        return "Cannot find HTML element with the id '" + ElementID + "'";
      }
      if (arguments.length > 1 && typeof (RequiredTagName) == "string") {
        if (document.getElementById(ElementID).tagName.toLowerCase() != RequiredTagName && RequiredTagName != "") {
          return "HTML element with ID '" + ElementID + "' has the tagname '" + document.getElementById(ElementID).tagName + "' but it should be '" + RequiredTagName + "'";
        }
      }
      return ""
    },

    /**
    * Checks if the referenced object is an HTML element
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-02-23
    * @param {Object} element The element that will be checked if it exists.
    * @example
    * error = graphite.check.element( document.getElementsByTagName('a')[0] );
    * if ( error != "" ) throw new Error( error );
    */
    element: function(element) {
      if (typeof (element.tagName) == "undefined") {
        return "HTML element expected. Type of checked variable is " + typeof (element)
      }
      return ""
    },

    /**
    * Checks if argument is of the expected variable type
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-02-23
    * @param {anything} Variable The variable that will be checked if it has the right type
    * @param {String} ExpectedVariableType The variable type of the first argument has to be equal to this string
    * @example
    * error = graphite.check.variableType( id, "string" );
    * if ( error != "" ) throw new Error( error );
    */
    variableType: function(Variable, ExpectedVariableType) {
      if (typeof (Variable) != ExpectedVariableType) {
        return "Unexpected variable type. There variable type should be " + ExpectedVariableType + ", but it is " + typeof (Variable);
      }
      return ""
    },

    /**
    * Returns a value from an object literal. If that vaule does not exist
    * it will fallback on the value in the old object literal
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-02-23
    * @param {Object} oldVariable
    * @param {Object} newVariable
    * @param {String} arrayID Key of the object literal
    * @example
    * oLiteral.foo = graphite.check.setLiteralIfDefined( oLiteral, oNewLiteral, "foo" )
    */
    setLiteralIfDefined: function(oldVariable, newVariable, arrayID) {
      if (typeof (newVariable) == "undefined") {
        return oldVariable[arrayID]
      }

      if (typeof (newVariable[arrayID]) == "undefined") {
        return oldVariable[arrayID]
      } else {
        return newVariable[arrayID]
      }
    },

    /**
    * Updates object literal. The second argument is merged with the first. Please use
    * graphite.check.literalUpdatable if you want to be sure that this method
    * only updates variables and doesn't create new ones.
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2011-03-08
    * @param {Object} obj Main object
    * @param {Object} newObj Object that will be merged with the main object
    * @param [Boolean] recursive If true, the updating will be done recursively.
    * If false, values will be merged and/or updated. Objects will be replaced.
    * The default value is false. 
    * @example
    * graphite.check.updateLiteral(obj, newObj, true)
    */
    updateLiteral: function(obj, newObj, recursive) {
      if (typeof(recursive) == "undefined") {
        for (prop in newObj) {
          obj[prop] = newObj[prop]
          if (typeof (newObj[prop]) == "object") {
            graphite.check.UpdateLiteral(obj[prop], newObj[prop]);
          }
        }
      } else {
        for (var prop in newObj) {
          try {
            if (newObj[prop].constructor == Object) {
              obj[prop] = graphite.check.UpdateLiteral(obj[prop], newObj[prop], true);
            } else {
              obj[prop] = newObj[prop];
            }
          }
          catch (e) {
            obj[prop] = newObj[prop];
          }
        }
        return obj
      }
    },

    /**
    * Compares 2 object literals and checks if the 2nd argument can be merged
    * with the 1st. If there's a variable in the 1st argument that's not
    * been defined in the 2nd, the function returns the name of the variable.
    *
    * @since 1.0 - 2010-02-23
    * @version 1.0 - 2010-02-23
    * @param {Object} mainLiteral
    * @param {Object} updateLiteral
    * @example
    * error = graphite.check.literalUpdatable( mainLiteral, updatingLiteral );
    * if ( error != "" ) throw new Error( error );
    */
    literalUpdatable: function(mainLiteral, updateLiteral) {
      if (typeof (mainLiteral) != "object") {
        return "Cannot check literals: first argument is not an object"
      }
      if (typeof (updateLiteral) != "object") {
        return "Cannot check literals: second argument is not an object"
      }


      var isNotUpdatableVariable = literalsAreCompatible(mainLiteral, updateLiteral)

      if (typeof (isNotUpdatableVariable) == "undefined") {
        return ''
      } else {
        return isNotUpdatableVariable;
      }
    }
  }
  /* End public */
})();






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
     * @param {Object|Array} element The element or array of elements on which
     *    the event is placed.
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
        if (element instanceof Array) {
          for (var i = 0; i < element.length; i++) {
            addEvent(element[i]);
          }
        } else {
          addEvent(element);
        }
      }

      function addEvent(element) {
        try{
          element.addEventListener(eventType, eventFunction);
        }
        catch(e) {
          console.log(element, eventType, eventFunction, e);
        }
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
