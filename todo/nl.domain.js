/**
 * @fileOverview All JS-code for domain.nl
 * @author name
 * @since 1.0 - 2010-8-6
 * @version 1.0 - 2010-8-6
 */




if (typeof (nl) == "undefined") {
	/**
	 * @namespace Top Level Domain
	 */
	var nl = {}
}
if (typeof(nl.domain) == "undefined") {
	/**
	 * @namespace Domain name
	 */
	nl.domain = {}
}






/**
 * @namespace Boiler plate class
 */
nl.domain.namespace = (function() {
	var config = {
		foo: "bar"
	}

	function privateMethod() {
		//
	}



	/* Start public */
	return {
		/**
		 * Boiler plate public method
		 * @example
		 * nl.domain.namespace.PublicMethod()
		 */
		PublicMethod: function() {
			//
		}
	}
	/* End public */
})();
