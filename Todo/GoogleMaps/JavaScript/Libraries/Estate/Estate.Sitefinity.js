/**
 * @fileOverview Sitefinity related functionalities 
 * @author Wouter Bos
 * @since 1.0 - 2010-05-26
 * @version 1.0 - 2010-05-26
 */






/**
 * @namespace Helper functions for Sitefinity
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.Sitefinity = (function() {
	/* Start public */
	return {
  		/**
		 * Indicates if a user is editing the page
		 *
		 * @since 1.0 - 2010-02-23
		 * @version 1.0 - 2010-02-23
		 * @example
		 * Estate.Sitefinity.IsInEditMode()
		 */
		IsInEditMode: function() {
			var pagemode = Estate.StringTools.GetQueryString("cmspagemode")
			if (pagemode == "edit") {
				return true;
			} else {
				return false;
			}
		}
	}
	/* End public */
})();






/**
 * @namespace Handling popups for the external video control
 * @class
 * @requires jQuery
 * @since 1.0 - 2010-05-26
 * @version 1.0 - 2010-05-26
 */
Estate.Sitefinity.MoviePopup = (function() {
	function showPopup(videoCode) {
		var popupHTML = ''
		if (jQuery("#DivPopupWindowMovie").size() == 0) {
			popupHTML += '<div id="DivPopupWindowMovie" class="divPopupWindow divPopupWindowMovie">'
			popupHTML += '	<div class="close"></div>'
			popupHTML += '	<div class="divPopupContent">'
			popupHTML += '		' + videoCode
			popupHTML += '	</div>'
			popupHTML += '</div>'
			popupHTML += '<div id="DivPopupBackgroundMovie" class="divPopupBackground divPopupBackgroundMovie"></div>'

			jQuery("body").append(popupHTML)
		} else {
			jQuery("#DivPopupWindowMovie div.divPopupContent").html(videoCode)
		}
	}



	/* Start public */
	return {
		/**
		 * Opens a div popup with the embedded video
		 *
		 * @example
		 * Estate.Sitefinity.MoviePopup.Popup(someVideoEmbedCode)
		 */
		Open: function(videoCode) {
			var error
			error = Estate.Check.ArgumentsCount(arguments.length, 1);
			if (error != "") throw new Error(error);

			while (videoCode.indexOf('|') > 0) {
				videoCode = videoCode.replace('|', '"')
			}
			showPopup(videoCode)

			var popupConfig = {
				scrollToWindow: false,
				animationSpeed: 250,
				closeText: "x",
				emptyOnClose: true,
				windowScrollOffsetY: 50,
				windowSelector: "#DivPopupWindowMovie",
				backgroundSelector: "#DivPopupBackgroundMovie",
				backgroundOpacity: 0.5
			}
			var popup = new Estate.Popup(popupConfig);
			popup.Open();
		}
	}
	/* End public */
})();
