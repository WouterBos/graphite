/**
 * @fileOverview Collection of base functionalities
 * @author Wouter Bos
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */



if (typeof(Estate) != "undefined") {
  throw new Error("Estate object must be loaded only once")
}


 
/**
 * @namespace Root namespace
 * @since 1.0 - 2010-02-23
 * @version 1.1 - 2010-10-10
 */
var Estate = {};






/**
 * @namespace Methods to validate function arguments. Use these methods liberally in all
 *            code that is part of the Estate object.
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Check = (function() {
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
		* error = Estate.Check.ArgumentsCount( arguments.length, [0, 1] );
		* if ( error != "" ) throw new Error( error );
		*/
		ArgumentsCount: function(CurrentArgumentsLength, CorrectArgumentsLength) {
			var error
			if (arguments.length != 2) throw new Error("Arguments count must be 2");
			error = Estate.Check.VariableType(CurrentArgumentsLength, "number");
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
		* error = Estate.Check.ElementById( 'elID', 'div' );
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
		* @param {Object} Element The element that will be checked if it exists.
		* @example
		* error = Estate.Check.Element( document.getElementsByTagName('a')[0] );
		* if ( error != "" ) throw new Error( error );
		*/
		Element: function(Element) {
			if (typeof (Element.tagName) == "undefined") {
				return "HTML element expected. Type of checked variable is " + typeof (Element)
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
		* error = Estate.Check.VariableType( id, "string" );
		* if ( error != "" ) throw new Error( error );
		*/
		VariableType: function(Variable, ExpectedVariableType) {
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
		* oLiteral.foo = Estate.Check.SetLiteralIfDefined( oLiteral, oNewLiteral, "foo" )
		*/
		SetLiteralIfDefined: function(oldVariable, newVariable, arrayID) {
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
		* Estate.Check.LiteralUpdatable if you want to be sure that this method
		* only updates variables and doesn't create new ones.
		*
		* @since 1.0 - 2010-02-23
		* @version 1.0 - 2011-03-08
		* @param {Object} obj Main object
		* @param {Object} newObj Object that will be merged with the main object
		* @param [Boolean] recursive If true, the updating will be done recursively.
		*	If false, values will be merged and/or updated. Objects will be replaced.
		*	The default value is false. 
		* @example
		* Estate.Check.UpdateLiteral(obj, newObj, true)
		*/
		UpdateLiteral: function(obj, newObj, recursive) {
			if (typeof(recursive) == "undefined") {
				for (prop in newObj) {
					obj[prop] = newObj[prop]
					if (typeof (newObj[prop]) == "object") {
						Estate.Check.UpdateLiteral(obj[prop], newObj[prop]);
					}
				}
			} else {
				for (var prop in newObj) {
					try {
						if (newObj[prop].constructor == Object) {
							obj[prop] = Estate.Check.UpdateLiteral(obj[prop], newObj[prop], true);
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
		* error = Estate.Check.LiteralUpdatable( mainLiteral, updatingLiteral );
		* if ( error != "" ) throw new Error( error );
		*/
		LiteralUpdatable: function(mainLiteral, updateLiteral) {
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
 * @namespace Helper methods for getting, setting and deleting cookies
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Cookies = (function() {
    /* Start public */
    return {
		/**
		 * Sets a cookie
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {String} name Name of the cookie
		 * @param {String} value Value of the cookie
		 * @param {Date} expires Expire date
		 * @param {String} path Path where the cookie is valid (default: path of calling document)
		 * @param {String} domain
		 * @param {Boolean} secure Indicating if the cookie transmission requires a secure transmission
		 * @example
		 * Estate.Cookies.Set( "cookieName", "cookieValue", new Date(2015, 12, 31, 23, 59, 59, 0), "/", "www.domain.com", "")
		 */
		Set: function(name,
					   value,
					   expires,
					   path,
					   domain,
					   secure
					   ) {
			document.cookie = name + "=" + encodeURIComponent(value) +
				((expires) ? "; expires=" + expires.toGMTString() : "") +
				((path) ? "; path=" + path : "") +
				((domain) ? "; domain=" + domain : "") +
				((secure) ? "; secure" : "");
		},

 		/**
		 * Gets cookie data
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @example
		 * Estate.Cookies.Get()
		 */
        Get: function(name) {
            var dc = document.cookie;
            var prefix = name + "=";
            var begin = dc.indexOf("; " + prefix);
            if (begin == -1) {
                begin = dc.indexOf(prefix);
                if (begin != 0) return null;
            } else {
                begin += 2;
            }
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
            return decodeURIComponent(dc.substring(begin + prefix.length, end));
        },

  		/**
		 * Deletes cookie data
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.1 - 2011-01-28
		 * @param {String} name Name of the cookie
		 * @param {String} path Path where the cookie is valid (default: path of calling document)
		 * @param {String} domain
		 * @example
		 * Estate.CookiesDelete()
		 */
        Delete: function(name,
						  path,
						  domain
						  ) {
            if (Estate.Cookies.Get(name)) {
                document.cookie = name + "=" +
				((path) ? "; path=" + path : "") +
				((domain) ? "; domain=" + domain : "") +
				"; expires=Thu, 01-Jan-1970 00:00:01 GMT";
            }
        }
    }
    /* End public */
})();






/**
 * @namespace CSS related methods
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.CSSTools = ( function() {
	/* Start public */
	return {
		/**
		 * Detects what the client supports. If something is supported, it
		 * is added as a class to the BODY element.
		 *
		 * @since 1.0 - 2010-08-12
		 * @version 1.0 - 2010-08-12
		 * @example
		 * Estate.CSSTools.AddSupportsClasses()
		 */
		AddSupportsClasses: function() {
			Estate.CSSTools.AddClass(document.body, "HasJavaScript")
		},

  		/**
		 * Calling the method adds or removes a class from an element
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} el An HTML element
		 * @param {String} className The name of the class that has to be added or removed
		 * @example
		 * Estate.CSSTools.ClassToggle( document.getElementById('elementId'), 'className')
		 */
		ClassToggle: function( el, className ) {
			var error;
			error = Estate.Check.Element( el );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( className, "string" )
			if ( error != "" ) throw new Error( error );


			if ( el.className.indexOf( className ) < 0) {
				el.className += " "+ className
			} else {
				while ( el.className.indexOf( className ) >= 0) {
					el.className = el.className.replace( " "+ className, "" )
					el.className = el.className.replace( className, "" )
				}
			}
		},

  		/**
		 * Center image in a box with a fixed width and height
		 *
		 * @since 1.0 - 2011-01-31
		 * @version 1.0 - 2011-01-31
		 * @param {Object} jQuery selector. Selects image container
		 * @example
		 * Estate.CSSTools.CenterImage('ul.gallery li')
		 */
		CenterImage: function( selector ) {
			var error;
			error = Estate.Check.ArgumentsCount(arguments.length, 1);
			if (error != "") throw new Error(error);


			jQuery(selector).each( function(index) {
				var containerWidth = jQuery(this).innerWidth()
				var containerHeight = jQuery(this).innerHeight()
				
				var imageOverflowX = jQuery(this).find('img').innerWidth() - jQuery(this).innerWidth()
				var imageOverflowY = jQuery(this).find('img').innerHeight() - jQuery(this).innerHeight()
				if (imageOverflowX > 0) {
					imageOverflowX = -Math.round( (imageOverflowX / 2) )
					jQuery(this).find('img').css('margin-left', imageOverflowX +"px")
				}
				if (imageOverflowY > 0) {
					imageOverflowY = -Math.round( (imageOverflowY / 2) )
					jQuery(this).find('img').css('margin-top', imageOverflowY +"px")
				}
			})
		},

  		/**
		 * Adds class to an element. This function is used for components in the Estate object that musn't use jQuery. If that's not the case, just use jQuery
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} el An HTML element
		 * @param {String} className The name of the class that has to be added
		 * @example
		 * Estate.CSSTools.AddClass( document.getElementById('elementId'), 'className')
		 */
		AddClass: function( el, className ) {
			var error;
			error = Estate.Check.Element( el );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( className, "string" )
			if ( error != "" ) throw new Error( error );


			if ( el.className.indexOf( className ) < 0) {
				el.className += " "+ className
			}
		},

  		/**
		 * Removes class to an element. This function is used for components in the Estate object that musn't use jQuery. If that's not the case, just use jQuery
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} el An HTML element
		 * @param {String} className The name of the class that has to be removed
		 * @example
		 * Estate.CSSTools.RemoveClass( document.getElementById('elementId'), 'className')
		 */
		RemoveClass: function( el, className ) {
			var error;
			error = Estate.Check.Element( el );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( className, "string" )
			if ( error != "" ) throw new Error( error );


			while ( el.className.indexOf( className ) >= 0) {
				el.className = el.className.replace( " "+ className, "" )
				el.className = el.className.replace( className, "" )
			}
		},
		

  		/**
		 * Toggles class on the body between "fontSize1" and and empty string.
		 * The class can be recoded to cater
		 * more font sizes: "fontSize1", "fontSize2", etc.
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} el An HTML element
		 * @example
		 * Estate.CSSTools.ToggleFontSize(linkTextHolder)
		 */
        ToggleFontSize: function(linkTextHolderId) {
            if (arguments.length > 0) {
                var error
                error = Estate.Check.ElementById(linkTextHolderId);
                if (error != "") throw new Error(error);
            }


            var fontSizeClassName = "FontSize"
            var smallerTextLink = "Kleinere letters"
            var largerTextLink = "Grotere letters"

            if (Estate.Cookies.Get("fontSize") == null) {
                Estate.Cookies.Set("fontSize", "1")
                Estate.CSSTools.AddClass(document.body, fontSizeClassName + '1')
                if (linkTextHolderId != undefined) {
                    document.getElementById(linkTextHolderId).innerHTML = smallerTextLink
                }
            } else {
                Estate.Cookies.Delete("fontSize")
                Estate.CSSTools.RemoveClass(document.body, fontSizeClassName + '1')
                if (linkTextHolderId != undefined) {
                    document.getElementById(linkTextHolderId).innerHTML = largerTextLink
                }
            }
        },
		
  		/**
		 * Sets events on links that work in conjunction with
		 *
		 * @since 1.0 - 2011-07-26
		 * @version 1.0 - 2011-07-26
		 * @param {Object} selector jQuery collection of font size links
		 * @example
		 * Estate.CSSTools.SetFontSizeHandler(jQuery('a.fontSizeButton'));
		 */
        SetFontSizeHandler: function(selector) {
			var fontSizeLinks = jQuery(selector)
            fontSizeLinks.click( function(event) {
				event.preventDefault();
				event.stopPropagation();

				var link = jQuery(this);
				var fontClassSelected = Estate.StringTools.GetQueryString("fontClass", link.attr('href'));
				var fontClass;
				
				// Set classes links
				fontSizeLinks.removeClass('active');
				link.addClass('active');
				
				// Set body class
				fontSizeLinks.each(function() {
					fontClass = Estate.StringTools.GetQueryString("fontClass", jQuery(this).attr('href'))
					jQuery('body').removeClass(fontClass);
				});
				jQuery('body').addClass(fontClassSelected);
				
				// Store preference
				var expireDate = new Date();
				expireDate.setDate(expireDate.getDate() + 1000);
				Estate.Cookies.Set("fontClass", fontClassSelected, expireDate, "/");
            })
        },
		
       /**
        * Accepts a jQuery selector and sets the height of all selected
		* elements equal to the height of the heighest block in the list
        *
        * @since 1.0 - 2010-06-11
        * @version 1.0 - 2010-06-11
		* @param {String} jQuery selector
		* @requires jQuery
        * @example
        * Estate.CSSTools.EqualizeBlockHeight("ul.news li")
        */
		EqualizeBlockHeight: function(selector) {
			var error
			error = Estate.Check.ArgumentsCount(arguments.length, 1);
			if (error != "") throw new Error(error);

			if (jQuery(selector).size() > 1) {
				var maxHeight = 0
				jQuery(selector).each( function(index) {
					if (maxHeight < jQuery(this).height()) {
						maxHeight = jQuery(this).height()
					}
				})
				jQuery(selector).height(maxHeight)
			}
		},

		/**
		 * Sets class to floating images in generic content blocks
		 * @requires jQuery
		 * @example
		 * Estate.CSSTools.ImageFloatClass()
		 */
		ImageFloatClass: function() {
			  jQuery('*.genericContent img[style*="float"][style*="left"]').addClass('floatLeft')
			  jQuery('*.genericContent img[style*="float"][style*="right"]').addClass('floatRight')
		},
		
       /**
        * Add classes to elements with CSS selectors that are not supported
		* in the browser.
        *
        * @since 1.0 - 2010-06-11
        * @version 1.0 - 2010-06-11
		* @param {Array} staticSelectorArray Each nested array holds 2 items. The classname in the
		* second array item will be added to all elements that are selected
		* with the jQuery selector in the first array item. Example:
		* [["jQuery selector", "classname"], ["jQuery selector 2", "classname2"]]
		* @requires jQuery
        * @example
        * Estate.CSSTools.SelectorHelper( [["ul li:lt(4)", "firstFive"], ["ul li:gt(9)", "afterTen"]] )
        */
		SelectorHelper: function(staticSelectorArray) {
			for (var i = 0; i < staticSelectorArray.length; i++) {
				jQuery(staticSelectorArray[i][0]).addClass(staticSelectorArray[i][1]);
			}
		},

       /**
        * Cross-browser alternative for IE's "text-overflow: ellipsis". It
		* cuts the text after a specified string length.
        *
        * @since 1.0 - 2010-06-11
        * @version 1.0 - 2010-06-11
		* @param {Array} arrSelectors
		* Example: [["jQuery selector", maxStringSize], ["jQuery selector 2", maxStringSize]]
		* @param [String] altAppendString String that will be appended after the truncated string
		* @requires jQuery
        * @example
        * Estate.CSSTools.ManualTextOverflow( [["ul.news li", 50], ["ul.agenda li", 100]] )
        */
		ManualTextOverflow: function(arrSelectors, altAppendString) {
			var appendString = "...";
			if (typeof(altAppendString) !== "undefined") {
				appendString = altAppendString
			}
			
			for (var i = 0; i < arrSelectors.length; i++) {
				var selector = arrSelectors[i][0]
				var maxStringSize = arrSelectors[i][1]
				jQuery(selector).each( function() {
					var stringLength = jQuery(this).html().length
					if (stringLength > maxStringSize) {
						jQuery(this).html(jQuery(this).html().substr(0, maxStringSize) + appendString)
					}
				})
			}
		}

	}
	/* END PUBLIC */
})();






/**
 * @namespace Detects browser support and adds corresponding body classes.
 * It indicates wether JavaScript and Flash are available. It alse removes
 * the class "JSDisabled" if appropriate
 * @class
 *
 * @since 1.0 - 2010-09-29
 * @version 1.0 - 2010-09-29
 */
Estate.CSSTools.FeatureDetection = (function() {
	//  JavaScript detection
	function hasJavaScript() {
		var classValue = document.body.className
		classValue = classValue.replace(/JSDisabled/i, "") // Removes body "JSDisabled"
		classValue += " JSEnabled"
		document.body.className = classValue
	}
	
	// Flash detection
    // Flash detection
    function hasFlash() {
        var classes = new Array();
        var className = "HasFlash";
        var flashPlayerVersion = 0;
        var minFlashPlayerVersion = 6;
        var plugin = (navigator.mimeTypes && navigator.mimeTypes["application/x-shockwave-flash"]) ? navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin : 0;

        // Detect Flash
        if (plugin) {
            // Flash detection in Non-IE
            var words = navigator.plugins["Shockwave Flash"].description.split(" ");
            for (var i = 0; i < words.length; ++i) {
                if (isNaN(parseInt(words[i]))) {
                    continue;
                }
                flashPlayerVersion = words[i];
            }
        }
        else if (navigator.userAgent && navigator.userAgent.indexOf("MSIE") >= 0 && (navigator.appVersion.indexOf("Win") != -1)) {
            // Flash detection in IE
            for (var i = 0; i < 15; i++) {
                IEflashPlayerVersion = false;

                document.write('<script language="VBScript">\n');
                document.write('on error resume next \n');
                document.write('IEflashPlayerVersion = ( IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash.' + i + '")))\n');
                document.write('</script> \n');

                if (IEflashPlayerVersion) {
                    flashPlayerVersion = i;
                }
            }
        }
		
        // Create array with relevant classes
        flashPlayerVersion = parseInt(flashPlayerVersion)
        if (minFlashPlayerVersion != 0) {
            classes[classes.length] = className;
            try { // Create body class names of major versions
            	for (i = minFlashPlayerVersion; i <= parseInt(flashPlayerVersion); i++) {
            		classes[classes.length] = className + i;
            	}
            }
            catch (e) { }
            flashPlayerVersion += ""
            classes[classes.length] = className + flashPlayerVersion.replace(".", "dot");
        } else {
            document.body.className += " NoFlash"
        }

        // Write class names to body element
        for (i = 0; i < classes.length; i++) {
            document.body.className += " " + classes[i];
        }
    }
	
	// Checks if page is in edit mode of some CMS
	function inEditMode() {
		if (document.getElementById('scWebEditRibbon')) {
			document.body.className += " InSitecoreEditmode";
		}
	}


	
	/* Start public */
	return {
       /**
        * Runs browser support detection
        *
        * @since 1.0 - 2010-03-29
        * @version 1.0 - 2010-03-29
        * @example
        * Estate.CSSTools.FeatureDetection.Run()
        */
       Run: function() {
			var error
			error = Estate.Check.ArgumentsCount(arguments.length, 0);
			if (error != "") throw new Error(error);
			
			hasJavaScript()
			hasFlash()
			inEditMode()
		}
	}
	/* End public */
})();







/**
 * @namespace Adds a function to an event of a single element. Use this if
 * you don't want to use jQuery
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Events = ( function() {
	/* Start public */
	return {
  		/**
		 * Adds a function to an event of a single element
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} element The element on which the event is placed
		 * @param {Function} newFunction The function that has to be linked to the event
		 * @param {String} eventType Name of the event
		 * @example
		 * Estate.Events.AddEvent( document.getElementById('elementId'), functionName, "onclick" )
		 */
		AddEvent: function( element,
						   	newFunction,
							eventType
							  ) {
			var error;
			error = Estate.Check.VariableType( element, "object" )
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( newFunction, "function" )
			if ( error != "" ) throw new Error( error );
			
			if (element instanceof Array) {
				for (var i = 0; i < element.length; i++) {
					addEvent(element[i]);
				}
			} else {
				addEvent(element)
			}
			
			function addEvent(element) {
				var oldEvent = eval("element." + eventType);
				var eventContentType = eval("typeof element." + eventType)
				
				if ( eventContentType != 'function' ) {
					eval("element." + eventType + " = newFunction")
				} else {
					eval("element." + eventType + " = function(e) { oldEvent(e); newFunction(e); }")
				}
			}
		}
	}
	/* End public */
})();






/**
 * @namespace Holder for form related functions
 */
Estate.Forms = {}


/**
 * @namespace Applies a character limit to form elements, including feedback
 *            to user on how much characters are left
 * @requires jQuery
 * @since 1.0 - 2010-06-16
 * @version 1.0 - 2010-06-16
 */
Estate.Forms.MaxLength = (function() {
	var config = {
		feedbackText: " tekens over",
		feedbackCss: "maxSizeFeedback",
		feedbackCssNearLimit: "maxSizeFeedbackNearLimit",
		NearLimitAlert: 20
	}



	function addFeedbackContainer(selector) {
		jQuery(selector).after("<span class='" + config.feedbackCss + "'></span>")
	}

	function onCharactersAdded(element, maxChars) {
		var stringSize = jQuery(element).val().length
		if (stringSize > 0) {
			
			if ((maxChars - stringSize) < config.NearLimitAlert) {
				jQuery(element).next("span." + config.feedbackCss).addClass(config.feedbackCssNearLimit)
			} else {
				jQuery(element).next("span." + config.feedbackCss).removeClass(config.feedbackCssNearLimit)
			}
			if ((maxChars - stringSize) < 0) {
				jQuery(element).val(jQuery(element).val().substr(0, maxChars))
				jQuery(element).next("span." + config.feedbackCss).html(0 + config.feedbackText);
			}

			stringSize = jQuery(element).val().length
			jQuery(element).next("span." + config.feedbackCss).html((maxChars - stringSize) + config.feedbackText);
		}
	}
	
	
	
	/* Start public */
	return {
  		/**
		 * @since 1.0 - 2010-06-16
		 * @version 1.0 - 2010-06-16
		 * @param {String} selector jQuery selector
		 * @param {Number} maxChars Maximum number of allowed characters
		 * @example
		 * Estate.Forms.MaxLength.Set( "div.form textarea", 1000 )
		 */
		Set: function(selector, maxChars) {
			var error
			error = Estate.Check.ArgumentsCount(arguments.length, 2);
			if (error != "") throw new Error(error);
			error = Estate.Check.VariableType( className, "string" )
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( maxChars, "number" )
			if ( error != "" ) throw new Error( error );

			if (jQuery(selector).size() > 0) {
				addFeedbackContainer(selector)
				jQuery(selector).keyup(function() { onCharactersAdded(this, maxChars) })
				jQuery(selector).change(function() { onCharactersAdded(this, maxChars) })
				jQuery(selector).mouseout(function() { onCharactersAdded(this, maxChars) })
				jQuery(selector).mouseover(function() { onCharactersAdded(this, maxChars) })
			}
		}
	}
	/* End public */
})();






/**
 * @namespace Adds watermark to an input box
 */
Estate.Forms.Watermark = (function() {
    function addWatermark(selector) {
        jQuery(selector).val(jQuery(selector).data("watermarkText"))
        jQuery(selector).addClass("watermark")
    }

    /* Start public */
    return {
        /**
         * Initialize watermark functionality
         * @param {String|Object} selector jQuery selector
         * @param {String} watermarkText The default text in the input field
         * @requires jQuery
         * @example
         * Estate.Forms.Watermark.Init("input.searchBox", "Search")
         */
        Init: function(selector, watermarkText) {
			jQuery(selector).data("watermarkText", watermarkText)
            addWatermark(selector)

            jQuery(selector).focus(function() {
                if (jQuery(this).val() == jQuery(selector).data("watermarkText")) {
                    jQuery(this).val("")
                    jQuery(this).removeClass("watermark")
                }
            })

            jQuery(selector).blur(function() {
                if (jQuery(this).val() == "") {
                    addWatermark(this)
                }
            })
        }
    }
    /* End public */
})();






/**
* @namespace Automatically add placeholders to all form fields in browsers that do not support placeholders
*/
Estate.Forms.Watermark2 = (function () {
    /* Start public */
    return {
        /**
        * Initialize watermark functionality
        * @requires jQuery
        * @example
        * Estate.Forms.Watermark2.Init();
        */
        Init: function () {
            if (!("placeholder" in document.createElement("input"))) {
                jQuery('*[placeholder]').each(function () {
                    $this = jQuery(this);
                    var placeholder = jQuery(this).attr('placeholder');
                    if (jQuery(this).val() === '') {
                        $this.val(placeholder);
                        jQuery(this).addClass('gp_showsPlaceholder');
                    }
                    $this.bind('focus',
					function () {
					    if (jQuery(this).val() === placeholder) {
					        this.plchldr = placeholder;
					        jQuery(this).val('');
					        jQuery(this).removeClass('gp_showsPlaceholder');
					    }
					});
                    $this.bind('blur',
					function () {
					    if (jQuery(this).val() === '' && jQuery(this).val() !== this.plchldr) {
					        jQuery(this).val(this.plchldr);
					        jQuery(this).addClass('gp_showsPlaceholder');
					    }
					});
                });
            }
        }
    }
    /* End public */
})();






/**
 * @namespace Helper methods for placing absolute positioned boxes
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Layers = ( function() {
	/* Start public */
	return {
  		/**
		 * Returns the position of the element on the y-axis
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} obj The element on which the event is placed
		 * @example
		 * Estate.Layers.GetPositionX( document.getElementById('elementId') )
		 */
		GetPositionX: function(obj) {
			var error
			error = Estate.Check.Element( obj );
			if ( error != "" ) throw new Error( error );


			var x = 0
			if (obj.offsetParent) {
				while (obj.offsetParent) {
					x += obj.offsetLeft
					obj = obj.offsetParent
				}
			} else 	if (obj.x) {
				x += obj.x
			}
			return x
		},
		
  		/**
		 * Returns the position of the element on the x-axis
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {Object} obj The element on which the event is placed
		 * @example
		 * Estate.Layers.GetPositionX( document.getElementById('elementId') )
		 */
		GetPositionY: function(obj) {
			var error
			error = Estate.Check.Element( obj );
			if ( error != "" ) throw new Error( error );


			var y = 0
			if (obj.offsetParent) {
				while (obj.offsetParent) {
					y += obj.offsetTop
					obj = obj.offsetParent
				}
			} else 	if (obj.y) {
				y += obj.y
			}
			return y
		}
	}
	/* End public */
})();






/**
 * @namespace Navigation related methods
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Navigation = ( function() {
	/* Start public */
	return {
  		/**
		 * Navigates one step backwards in browser history. If no browser
		 * history is available the static link in the href is used
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @example
		 * <a href="contact.asp" onclick="return Estate.Navigation.LinkBack()">Terug</a>
		 */
		LinkBack: function () {
			if ( history.length > 0 ) {
				history.go( -1 )
				return false
			} else {
				return true
			}
		}
	}
	/* End public */
})();






/**
 * @namespace Some string manipulation methods
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.StringTools = ( function() {
	function TranslateScrambledAddress( string ) {
		var returnString = "";
		var aCharacters;

		string = string.replace( "scrambled:", "" )
		string = string.substring( 1, (string.length - 1) )
		aCharacters = string.split("][")
		for ( var i = 0; i < aCharacters.length; i++ ) {
			returnString += String.fromCharCode( aCharacters[i] )
		}

		return returnString
	}

	/* Start public */
	return {
		/**
		 * Adds some data to a GET string
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @example
		 * var url = Estate.StringTools.addGetData(url, key, value)
		 */
		addGetData: function(arg_url, key, value) {
			var error;
			error = Estate.Check.ArgumentsCount( arguments.length, 3 );
			if ( error != "" ) throw new Error( error );

			
			var url = arg_url
	        if (url.indexOf('?') >= 0) {
				url += '&'
	        } else {
				url += '?'
	        }
	        url += key + '=' + value
	        
	        return url
		},

		/**
		 * Returns the filename is it exists in the address bar
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @example
		 * var filename = Estate.StringTools.GetFilenameFromUrl()
		 */
		GetFilenameFromUrl: function() {
			var error;
			error = Estate.Check.ArgumentsCount( arguments.length, 0 );
			if ( error != "" ) throw new Error( error );


	        var file_name = document.location.href;
	        var end = ( file_name.indexOf("?") == -1 ) ? file_name.length : file_name.indexOf("?");
	        return file_name.substring( file_name.lastIndexOf("/")+1, end );
		},

		/**
		 * Removes extension from a filename
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {String} eventType Filename with attachment
		 * @example
		 * var filename = Estate.StringTools.GetFilenameFromUrl()
		 */
		GetFilenameWithoutExtension: function( str ) {
			var str = str.replace(/^\s|\s$/g, "");
			if (/\.\w+$/.test(str)) {
				var m = str.match(/([^\/\\]+)\.(\w+)$/);
				if (m)
					return m[1];
				else
					return "no file name";
			} else {
				var m = str.match(/([^\/\\]+)$/);
				if (m)
					return m[1];
				else
					return "no file name";
			}
		},

		/**
		 * Removes measurement unit from a string. So '14px' and '1em' become '14' and '1'
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @param {String} SuppliedString
		 * @example
		 * width = Estate.StringTools.RemoveMeasurement( width )
		 */
		RemoveMeasurement: function( SuppliedString ) {
			var error;
			error = Estate.Check.ArgumentsCount( arguments.length, 1 );
			if ( error != "" ) throw new Error( error );

			if (typeof(SuppliedString) == "string") {
				var StringWithoutMeasure = SuppliedString
				StringWithoutMeasure = StringWithoutMeasure.replace( "px", "" )
				StringWithoutMeasure = StringWithoutMeasure.replace( "em", "" )
				StringWithoutMeasure = StringWithoutMeasure.replace( "pt", "" )
			}
			
			return StringWithoutMeasure
		},

		/**
		 * Returns the value of a variable in a GET url by supplying its key
		 *
		 * @since 1.0 - 2010-09-29
		 * @version 1.0 - 2010-09-29
		 * @param {String} key The GET key
		 * @param {String} [custom_url] Extract value from supplied url instead of the currently loaded page
		 * @example
		 * value = Estate.StringTools.GetQueryString("foo")
		 * or
		 * value = Estate.StringTools.GetQueryString("foo", "http://www.customdomain.com/index.aspx?foo=bar&foo2=bar2")
		 */
		GetQueryString: function(key, custom_url) {
			error = Estate.Check.ArgumentsCount( arguments.length, [1, 2] );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( key, "string" );
			if ( error != "" ) throw new Error( error );
			if (arguments.length == 2) {
				error = Estate.Check.VariableType( custom_url, "string" );
				if ( error != "" ) throw new Error( error );
			}

			var query
			if (typeof(custom_url) == "string") {
				query = custom_url.substring(custom_url.indexOf('?')+1)
			} else {
				query = window.location.search.substring(1)
			}
			var vars = query.split("&");
			
			for (var i = 0; i < vars.length; i++) {
				var pair = vars[i].split("=");
				if (pair[0] == key) {
					return pair[1];
				}
			}
		}
	}
	/* End public */
})();
