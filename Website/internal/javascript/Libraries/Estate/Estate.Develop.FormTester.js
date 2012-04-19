/**
 * @fileOverview Code for automatically filling in form fields with test data
 * 
 * @author Wouter Bos
 * @since 1.0 - 2008
 * @version 1.1 - 2010-10-15
 * @requires Estate.js
 * @requires Estate.Develop.js
 */





/**
 * @namespace Holds all FormTester code.
 */
Estate.Develop.FormTester = ( function() {
	var menuButton
	var radioNamesCollection = new Array()
	var shortTestString = "#REPLACE# éالْعَرَبيّة汉语/漢語";
	var longTestString = "#REPLACE# Lorem ipsum dolor sit amet, consectetuer adipiscing elit."+
						 "Curabitur leo. <' \r\n\r\n"+
						 "Nullam nisi turpis, semper nec, dapibus quis, vestibulum ac, ipsum."+
						 "Vestibulum eleifend dignissim tortor. Nunc elementum tortor condimentum diam."+
						 "Morbi elementum viverra odio. Donec est turpis, porttitor in, varius"+
						 "at, dapibus ac, velit. `éالْعَرَبيّة汉语/漢語";
	var shortTestStringStrict = "#REPLACE# Lorem ipsum dolor sit amet."+
								"</div></td> <'\" `éالْعَرَبيّة汉语/漢語"+
								"<script>var alrt; if (typeof(alrt) == 'undefined') { alert('javascript injection'); alrt = 1 }</script>"+
								"' OR '1'='1 ";
	var longTestStringStrict = "#REPLACE# Lorem ipsum dolor sit amet, consectetuer adipiscing elit."+
							   "Curabitur leo. </span></div></td> <'\" \r\n\r\n"+
							   "Nullam nisi turpis, semper nec, dapibus quis, vestibulum ac, ipsum."+
							   "metus, sagittis non, egestas sed, fermentum sit amet, sem. Cras et massa. Ut"+
							   "dapibus ac, velit. `éالْعَرَبيّة汉语/漢語"+
							   "<script>var alrt; if (typeof(alrt) == 'undefined') { alert('javascript injection'); alrt = 1 }</script>"+
							   "' OR '1'='1 ";
	


	// Fills in "text" form fields or equivalent
	function fillText( element ) {
		try {
			element.value = shortTestString.replace("#REPLACE#", element.name)
		}
		catch (e) {}
	}
	
	// Fills in textarea form fields or equivalent
	function fillTextarea( element ) {
		try {
			if (document.all) {
				element.innerText = longTestString.replace("#REPLACE#", element.name)
			} else {
				element.innerHTML = longTestString.replace("#REPLACE#", element.name)
			}
		}
		catch (e) {}
	}

	// Finds all radiobuttons that belong to one group and returns it as an array
	function getRadioNames( element, newRadioNamesCollection ) {
		try {
			var radioNameFound = false
			
			for ( var i=0; i < newRadioNamesCollection.length; i++ ) {
				if ( newRadioNamesCollection[i] == element.name ) {
					radioNameFound = true
				}
			}
			
			if ( element.name != "" ) {
				if ( radioNameFound == false ) {
					newRadioNamesCollection.push( element.name )
				}
			}
			return newRadioNamesCollection
		}
		catch (e) {}
	}

	// Checks one item in a radio button list at random
	function fillRadioByName( lRadioNamesCollection ) {
		try {
			var radioGroupCollection = new Array
			var randomElement
			
			for ( var i=0; i < lRadioNamesCollection.length; i++ ) {
				radioGroupCollection = document.getElementsByName( lRadioNamesCollection[i] )
				
				randomElement = Math.round( Math.random() * ( radioGroupCollection.length - 1 ) )
				radioGroupCollection[randomElement].checked = true
			}
		}
		catch (e) {}
	}

	// Checks a checkbos item at random 
	function fillCheckBox( element ) {
		try {
			var randomChecked = Math.round( Math.random() )

			if ( randomChecked == 0 ) {
				element.checked = ""
			} else {
				element.checked = "checked"
			}
		}
		catch (e) {}
	}

	// Selects an option in a select box at random
	function fillSelect( element ) {
		try {
			var randomOption = Math.floor( Math.random() * ( element.length - 1 ) )
			
			if ( randomOption == 0 && element.length > 1 && element.options[0].value == "" ) {
				randomOption = 1
			}
			
			element.options[randomOption].selected = true
		}
		catch (e) {}
	}
	
	
	
	/* START PUBLIC */
	return {
		/**
		 * Sets FormTester event and button
		 * 
		 * @example
		 * Estate.Events.AddEvent(window, Estate.Develop.FormTester.Init, "onload")
		 * or simply:
		 * Estate.Develop.FormTester.Init()
		 */
		Init: function() {
			menuButton = Estate.Develop.Menu.AddMenuItem('Form', 'Press "="')
			Estate.Events.AddEvent( menuButton, Estate.Develop.FormTester.AddDataToFormFields, "onclick")
			Estate.Events.AddEvent(
				document, 
				function(e) {
					var KeyID = (window.event) ? event.keyCode : e.which;
					if ( KeyID == 61 ) {
						// Fill forms if an "=" is pressed
						Estate.Develop.FormTester.AddDataToFormFields()
					}
				},
				"onkeypress"
			)
		},

		/**
		 * Fills in all form fields on the page. This method can be called
		 * without calling Estate.Develop.FormTester.Init() first.
		 * 
		 * @example
		 * Estate.Develop.FormTester.AddDataToFormFields()
		 */
		AddDataToFormFields: function() {
			var formFieldsCollection = document.getElementsByTagName( 'input' )
			
			for ( var i=0; i < formFieldsCollection.length; i++ ) {
				switch(formFieldsCollection[i].type) {
					case "email":
					case "file":
					case "number":
					case "password":
					case "search":
					case "text":
					case "url":
						fillText( formFieldsCollection[i] )
						break;
					case "date":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "1970-01-01"
						}
						break;
					case "month":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "1970-01"
						}
						break;
					case "week":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "1970-W01"
						}
						break;
					case "time":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "12:00"
						}
						break;
					case "datetime":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "1970-01-01T12:00Z"
						}
						break;
					case "datetime-local":
						fillText( formFieldsCollection[i] )
						if (formFieldsCollection[i].value == "") {
							formFieldsCollection[i].value = "1970-01-01T12:00"
						}
						break;
					case "range":
						fillText( formFieldsCollection[i] )
						break;
					case "radio":
						radioNamesCollection = getRadioNames( formFieldsCollection[i], radioNamesCollection )
						break;
					case "checkbox":
						fillCheckBox( formFieldsCollection[i] )
						break;
				}
				dlog(formFieldsCollection[i].value)
			}
			fillRadioByName( radioNamesCollection )
			
			formFieldsCollection = document.getElementsByTagName( 'select' )
			for ( var i=0; i < formFieldsCollection.length; i++ ) {
				fillSelect( formFieldsCollection[i] )
			}

			formFieldsCollection = document.getElementsByTagName( 'textarea' )
			for ( var i=0; i < formFieldsCollection.length; i++ ) {
				fillTextarea( formFieldsCollection[i] )
			}
		}
	};
	/* END PUBLIC */
})();
