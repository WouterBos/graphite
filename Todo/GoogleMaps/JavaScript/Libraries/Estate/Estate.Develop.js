/**
 * @fileOverview Base namespace for all development functionality
 * @author Wouter Bos
 * @since 1.0 - 2008
 * @version 1.1 - 2010-09-29
 * @requires Estate.js
 */






/**
 * @namespace Namespace for developer helpers. On startup it will setup a root div
 * in the page and load some CSS files.  
 */
Estate.Develop = ( function() {
	var config = {
		rootElement: null,
		devtoolsPath: 'devtools',
		cssDevelop: "css/develop.css",
		cssHighlight: "css/develop-highlightBadHTML.css"
	}
	
	setup()
	
	function setup(){
		// Add root div
		config.rootElement = document.createElement("div");
		config.rootElement.className = "dev_root"
		document.body.appendChild(config.rootElement);
		
		// Add develop css files
		var css = document.createElement("link")
		css.rel = "stylesheet"
		css.type = "text/css"
		css.media = "screen, handheld"
		var cssPath = config.cssDevelop
		if (config.devtoolsPath != "") {
			cssPath = config.devtoolsPath +'/'+ cssPath
		}
		css.href = cssPath
		document.getElementsByTagName("head")[0].appendChild(css)

		var css2 = document.createElement("link")
		css2.rel = "stylesheet"
		css2.type = "text/css"
		css2.media = "screen, handheld"
		var css2Path = config.cssHighlight
		if (config.devtoolsPath != "") {
			css2Path = config.devtoolsPath +'/'+ css2Path
		}
		css2.href = css2Path
		document.getElementsByTagName("head")[0].appendChild(css2)
	}

	/* START PUBLIC */
	return {
		/**
		 * Returns root element that holds all development tools
		 * 
		 * @example
		 * Estate.Develop.GetRoot()
		 * @returns {Object} Root element that holds all development tools
		 */
		GetRoot: function() {
			return config.rootElement;
		},

		/**
		 * Return path to developers tool resources
		 * 
		 * @example
		 * Estate.Develop.GetDevtoolsPath()
		 * @returns {Object} Path to developers tool resources
		 */
		GetDevtoolsPath: function() {
			var path = config.devtoolsPath
			if (path != "") {
				path += "/"
			}
			return path
		}
	}
	/* END PUBLIC */
})();





/**
 * @namespace Timer is a stopwatch to check how long some operation costs in the browser.
 * 
 * @example
 * var oTimer = new Estate.Develop.Timer()
 * oTimer.SetID( 'Random' )
 * oTimer.Start()
 * oTimer.End()
 */
Estate.Develop.Timer = function() {
	/**
	 * Sets message that has to be printed in the tracebox when ending the timer
	 * 
	 * @param {String} [string] String that will be printed on the screen
	 * @usage
	 * Estate.Develop.Trace( 'Showthis' )
	 */
	this.SetID = function( newTimerID ) {
		this.TimerID = newTimerID;
	}

	/**
	 * Starts timer
	 */
	this.Start = function() {
		var TimeNow = new Date();
		this.TimeStart = TimeNow.getTime();
	}
		
	/**
	 * Ends timer
	 */
	this.End = function() {
		var TimeNow = new Date();
		this.TimeEnd = TimeNow.getTime();
		
		Estate.Develop.TraceBox.Create();
		document.getElementById( this.TracerID ).innerHTML = '<span>"'+ this.TimerID +'" runs in <strong>'+ (this.TimeEnd - this.TimeStart) +'</strong> milliseconds</span>' + document.getElementById( this.TracerID ).innerHTML;
	}
}






/**
 * @namespace TraceBox is an alternative for console.log: it wil show show debug
 * information on the webpage itself
 */
Estate.Develop.TraceBox = ( function() {
	var config = {
		traceBox: null,
		traceBoxClass: 'traceBox',
		traceBoxContent: null,
		traceBoxContentClass: 'traceBox_content',
		traceBoxToggle: null,
		traceBoxToggleClass: 'traceBox_toggle',
		consoleEnabled: false
	}
	
	if (typeof(console) == "object") {
		if (typeof(console.log) == "function") {
			config.consoleEnabled = true
		}
	}
	
	function createTraceBox() {
		if (config.traceBox == null) {
			var root = Estate.Develop.GetRoot()
			config.traceBox = document.createElement("div");
			config.traceBox.className = config.traceBoxClass;
			root.appendChild(config.traceBox);

			config.traceBoxContent = document.createElement("div");
			config.traceBoxContent.className = config.traceBoxContentClass;
			config.traceBox.appendChild(config.traceBoxContent);
			
			config.traceBoxToggle = document.createElement("div");
			config.traceBoxToggle.className = config.traceBoxToggleClass;
			config.traceBoxToggle.innerHTML = "&laquo;";
			config.traceBox.appendChild(config.traceBoxToggle);
			
			Estate.Events.AddEvent(config.traceBoxToggle, function() { showHide() }, "onclick")
		}
	}
	
	function showHide() {
		if (config.traceBox.className.indexOf('hidden') < 0) {
			config.traceBox.className += " traceBox_hidden"
			config.traceBoxToggle.innerHTML = "&raquo;"
		} else {
			config.traceBox.className = config.traceBox.className.replace(" traceBox_hidden", "")
			config.traceBoxToggle.innerHTML = "&laquo;"
		}
	}

	function traceAttributesElement(element) {
		var error;
		error = Estate.Check.Element( element );
		if ( error != "" ) throw new Error( error );

		var attributes

		attributes = "&lt;" + element.tagName + "&gt;: "
		for (var i = 0; i < element.attributes.length; i++) {
			var attr = element.attributes[i];
			attributes += " "+ attr.name +"='"+ attr.value +"'"
		}

		return attributes
	}
	
	// Documentation of this global function at the bottom of this document
	window.dlog = function(printThis) {
		Estate.Develop.TraceBox.Trace( printThis, false );
	}
	
	// Documentation of this global function at the bottom of this document
	window.clog = function(string) {
		Estate.Develop.TraceBox.Trace( string, true )
	}



	/* START PUBLIC */
	return {
		/**
		 * Prints a givenstring n the tracebox.
		 * 
		 * @param {String|Object} printThis Either string that will be printed on the screen or a DOM element
		 * @param {Boolean} doConsole Use console.log if that exists
		 * @example
		 * Estate.Develop.TraceBox.Trace( 'Showthis', false )
		 */
		Trace: function( printThis, doConsole ) {
			var printString = printThis
			if (typeof(printThis) == "object") {
				if (typeof(printThis.tagName) == "string") {
					printString = traceAttributesElement(printThis)
				}
			}
			
			if (doConsole && config.consoleEnabled) {
				console.log(printString)
			} else {
				createTraceBox();
				config.traceBoxContent.innerHTML = '<span><em>&#8226;</em> ' + printString + '</span>' + config.traceBoxContent.innerHTML;
			}
		},
		
		/**
		 * Adds the tracebox to the DOM
		 * 
		 * @example
		 * Estate.Develop.TraceBox.CreateTraceBox()
		 */
		CreateTraceBox: function() {
			createTraceBox()
		}		
	}
	/* END PUBLIC */
})();




/**
 * @namespace Creates and manages the menu
 */
Estate.Develop.Menu = ( function() {
	var config = {
		menu: null,
		menuClass: 'dev_menu',	
		buttons: null,
		buttonsClass: 'dev_buttons'	
	}

	var menu = document.createElement("div");
	menu.className = config.menuClass;
	Estate.Develop.GetRoot().appendChild(menu);

	var buttons = document.createElement("div");
	buttons.className = config.buttonsClass;
	menu.appendChild(buttons);

	/* START PUBLIC */
	return {
		/**
		 * Adds a button to the menu
		 * 
		 * @returns {Object} The created button element
		 * @example
		 * Estate.Develop.Menu.AddMenuItem('design')
		 */
		AddMenuItem: function(buttontext, tooltipText) {
			var button = document.createElement("div");
			button.className = "dev_button"
			button.innerHTML = buttontext;
			buttons.appendChild(button);
			
			if (tooltipText != null) {
				var tooltip = document.createElement("div");
				tooltip.className = "dev_tooltip"
				tooltip.innerHTML = tooltipText;
				button.appendChild(tooltip);
			}
			
			var aButtons = buttons.childNodes
			var totalWidth = 0
			for (var i = 0; i < aButtons.length; i++) {
				totalWidth += aButtons[i].offsetWidth
			}
			menu.style.width = totalWidth + "px"
			
			return button;
		}
	}
	/* END PUBLIC */
})();






/**
 * @namespace Forces a CSS reload without reloading the whole page. Very
 * handy when the DOM is drastically changed by JavaScript. This code runs
 * automatically at startup
 */
Estate.Develop.ReloadCSS = ( function() {
	// Creates button in menu
	var button = Estate.Develop.Menu.AddMenuItem('Reload CSS', 'Press "|"')
	
	// Adds click event to button
	Estate.Events.AddEvent(button, function(event) { reload(event) }, "onclick")
	
	// Adds keypress event that triggers CSS reload
	Estate.Events.AddEvent(
		document,
		function(e) {
			var KeyID = (window.event) ? event.keyCode : e.which;
			if (KeyID == 124) {
				reload(e)									
			}
		},
		"onkeypress"
	)
	
	// Forcing CSS reload by changing the href
	function reload(event) {
		var links = document.getElementsByTagName('link')
		for ( CSSDoc = 0; CSSDoc < links.length; CSSDoc++ ) {
			if (links[CSSDoc].href) {
				// The link has a href
				if (links[CSSDoc].href.indexOf('.css') > 0) {
					// The href refers to a CSS file so we can update the attribute value
					var newHref = links[CSSDoc].href
					if (newHref.indexOf("reload=") < 0) {
						if (newHref.indexOf("?") < 0) {
							newHref += "?"
						} else {
							newHref += "&"
						}
						links[CSSDoc].href = newHref + "reload=1"
					} else {
						var value = parseInt(Estate.StringTools.GetQueryString("reload", links[CSSDoc].href))
						newHref = links[CSSDoc].href
						newHref = newHref.substring(0, newHref.indexOf('?reload='))
						newHref += '?reload=' + (value + 1)
						links[CSSDoc].href = newHref
					}
				}
			}
		}
	}
})();






/**
 * @namespace Adds button and keyboard shortcut to toggle the visibility of
 * the Estate developer tools, so you can see the website without the
 * developer tools floating around. This code runs automatically at startup
 */
Estate.Develop.ToggleVisibility = ( function() {
	// Creates button in menu
	var button = Estate.Develop.Menu.AddMenuItem('Show/hide', 'Press "~"')
	
	// Adds click event to button
	Estate.Events.AddEvent(button, function(event) { showHide(event) }, "onclick")
	
	// Adds keypress event that toggles the visibility
	Estate.Events.AddEvent(
		document,
		function(e) {
			var KeyID = (window.event) ? event.keyCode : e.which;
			if (KeyID == 126) {
				showHide(e)									
			}
		},
		"onkeypress"
	)
	
	// Toggles the visibility of the developer toos
	function showHide(event) {
		var devRoot = Estate.Develop.GetRoot()
		
		if (devRoot.style.display == '') {
			devRoot.style.display = 'none';
		} else {
			devRoot.style.display = '';
		}
	}
})();






/**
 * Prints the argument in the tracebox.
 * 
 * @param {String|Object} printThis The argument will always be printed in the tracebox
 * @example
 * dlog('Lorem ipsum dolor sit amet')
 */
/* The documentation above refers to a global function that is created in the
 * namespace Estate.Develop.TraceBox. The documentation is placed outside the 
 * namespace so that the documentation tool JSDoc-Toolkit can recognize it
 * as a global function
 */
function dlog() {}

/**
 * Prints the argument in the browsers' developer console if available. If not, it will print it in the tracebox in the page.
 * 
 * @param {String|Object} printThis The argument that will be printed in either the console or the tracebox
 * @example
 * clog('Lorem ipsum dolor sit amet')
 */
/* The documentation above refers to a global function that is created in the
 * namespace Estate.Develop.TraceBox. The documentation is placed outside the 
 * namespace so that the documentation tool JSDoc-Toolkit can recognize it
 * as a global function
 */
function clog() {}