/**
* @namespace Adds highlighting to container
*/
nl.groenhorst.Highlighting = (function() {

	function DoHighlight(bodyText, searchTerm, highlightStartTag, highlightEndTag) {

		// the highlightStartTag and highlightEndTag parameters are optional
		if ((!highlightStartTag) || (!highlightEndTag)) {
			highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
			highlightEndTag = "</font>";
		}

		// find all occurences of the search term in the given text,
		// and add some "highlight" tags to them (we're not using a
		// regular expression search, because we want to filter out
		// matches that occur within HTML tags and script blocks, so
		// we have to do a little extra validation)
		var newText = "";
		var i = -1;
		var lcSearchTerm = " " + searchTerm.toLowerCase();
		var lcBodyText = " " + bodyText.toLowerCase();

		searchTerm = " " + searchTerm;
		bodyText = " " + bodyText;

		while (bodyText.length > 0) {
			i = lcBodyText.indexOf(lcSearchTerm, i + 1);
			if (i < 0) {
				newText += bodyText;
				bodyText = "";
			} else {
				// skip anything inside an HTML tag
				if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
					// skip anything inside a <script> block
					if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
						newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
						bodyText = bodyText.substr(i + searchTerm.length);
						lcBodyText = bodyText.toLowerCase();
						i = -1;
					}
				}
			}
		}

		return newText;
	}

	/* Start public */
	return {
		/**
		* Highlight Search Terms
		* @param {String} search text
		* @param {Boolean} treat as phrase
		* @param {String} highlight start tag
		* @param {String} highlight end tag
		* @param {String} container class
		* @requires jQuery
		* @example
		* nl.groenhorst.Highlighting.HighlightSearchTerms("zoekterm", false, "<strong>", "</strong>", "zoekresultaatSummary")";
		*/
		HighlightSearchTerms: function(searchText, treatAsPhrase, highlightStartTag, highlightEndTag, containerClass) {

			// if the treatAsPhrase parameter is true, then we should search for 
			// the entire phrase that was entered; otherwise, we will split the
			// search string so that each word is searched for and highlighted
			// individually
			if (treatAsPhrase) {
				var searchArray = [searchText];
			} else {
				var searchArray = searchText.split(" ");
			}

			// loop through all the containers
			jQuery("." + containerClass).each(function() {
				var bodyText = jQuery(this).html();

				// loop through all search terms
				for (var i = 0; i < searchArray.length; i++) {
					bodyText = DoHighlight(bodyText, searchArray[i], highlightStartTag, highlightEndTag);
				};

				jQuery(this).html(bodyText);
			});

			return true;
		}
	}
	/* End public */
})();
