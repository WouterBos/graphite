/**
 * @fileOverview Code for generating a div popup
 * @author Wouter Bos
 * @since 1.0 - 2010-04-7
 * @version 1.0 - 2010-09-29
 */



/**
 * @requires jQuery
 * @see ESDN for related HTML and CSS
 * @class
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-09-29
 * @constructor
 * @param {Object} [newConfig] Configuration object.
 * @param {String} [newConfig.popupCookieName] The name of the cookie that determines if a popup should be shown or not
 * @param {Date} [newConfig.popupCookieExpire] The date and time  on which the cookie will expire
 * @param {Boolean} [newConfig.popupDeactivated] The value of the cookie that determines if a popup should be shown or not
 * @param {Boolean} [newConfig.scrollToWindow] If true, the page will be scrolled to the popup window
 * @param {Number} [newConfig.animationSpeed] Sets the animation speed in milliseconds
 * @param {String} [newConfig.closeText] Sets the text in the close button
 * @param {String} [newConfig.windowSelector] The selector of the popup div
 * @param {String} [newConfig.windowPosition] Positions the popup window vertically. Use either 'dynamic' (positions in the viewport) or 'static' (position determined by CSS)
 * @param {Number} [newConfig.windowScrollOffsetY] margin in pixels between popup and top window border
 * @param {String} [newConfig.backgroundSelector] The selector of the background div
 * @param {Number} [newConfig.backgroundOpacity] Opacity level of the layer below the popup that obscures the website
 * @example
 * var popupConfig = {
 *     animationSpeed: 250,
 *     windowSelector: "#DivPopupWindow1",
 *     backgroundSelector: "#DivPopupBackground2"
 * }
 * var popup = new Estate.Popup(popupConfig);
 * popup.Open();
 */
Estate.Popup = function(newConfig) {
	var error
	error = Estate.Check.ArgumentsCount(arguments.length, [0, 1]);
	if (error != "") throw new Error(error);



	var isIE6 = /MSIE 6/i.test(navigator.userAgent)
	var config = {
		popupCookieName: "deactivated",
		popupCookieExpire: new Date(2015, 12, 31, 23, 59, 59, 0),
		popupDeactivated: false,
		scrollToWindow: false,
		animationSpeed: 250,
		closeText: "x",
		windowSelector: "#DivPopupWindow",
		windowPosition: "dynamic",
		windowScrollOffsetY: 20,
		backgroundSelector: "#DivPopupBackground",
		backgroundOpacity: 0.5
	}
	var openCallback = {}
	var closeCallback = {}

	// Check a cookie to find out if the popup is disabled
	function popupIsDisabled() {
		if (Estate.Cookies.Get(config.popupCookieName) == null) {
			config.popupDeactivated = false;
		} else {
			config.popupDeactivated = true;
		}
	}
	
	function scrollToPopupWindow() {
		if (config.scrollToWindow == true && config.windowPosition == "static") {
			jQuery('html, body').animate({ scrollTop: (jQuery(config.windowSelector).offset().top - config.windowScrollOffsetY) }, 500)
		}
		if (typeof(openCallback) == "function") {
			openCallback()
		}
	}

	function setConfig(newConfig) {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, 1);
		if (error != "") throw new Error(error);
		error = Estate.Check.LiteralUpdatable(config, newConfig);
		if (error != "") throw new Error(error);

		if (newConfig != null) {
			Estate.Check.UpdateLiteral(config, newConfig)
		}
	}

	function close() {
		jQuery(config.windowSelector).slideUp(config.animationSpeed);
		jQuery(config.backgroundSelector).animate(
			{ opacity: 0 },
			config.animationSpeed,
			function() {
				jQuery(config.backgroundSelector).hide()
				if (typeof(closeCallback) == "function") {
					closeCallback()
				}
			}
		);
	}

	if (arguments.length > 0) {
		setConfig(newConfig)
	}

	/* Start public */
	/**
	 * Opens popup
	 * @param {Object} [argOpenCallback] Function that will be initiated when the popup starts the opening animation
	 * @param {Object} [argCloseCallback] Function that will be initiated when the popup ends the closing animation
	 */
	this.Open = function(argOpenCallback, argCloseCallback) {
		popupIsDisabled()
		
		// Store callbacks
		if (typeof(argOpenCallback) != undefined) {
			openCallback = argOpenCallback
		}
		if (typeof(argCloseCallback) != undefined) {
			closeCallback = argCloseCallback
		}
		
		// Open window if it's not disabled
		if (config.popupDeactivated == false) {
			// Set up popup
			jQuery(config.windowSelector).find("div.close").text(config.closeText);
			if (config.windowPosition == "dynamic") {
				jQuery(config.windowSelector).css("top", (jQuery(window).scrollTop() + config.windowScrollOffsetY) + "px")
			}
			jQuery(config.backgroundSelector).css("opacity", 0)
			
			// Animate popup
			jQuery(config.windowSelector).slideDown(config.animationSpeed, scrollToPopupWindow);
			jQuery(config.backgroundSelector).animate({ opacity: config.backgroundOpacity }, config.animationSpeed);
			jQuery(config.backgroundSelector).show()
			if (isIE6 == true) {
				jQuery(config.backgroundSelector).css("height", jQuery("body").height() + "px")
			}
			
			// Set events once
			if (jQuery(config.backgroundSelector).data('popupEventSet') != 'true') {
				jQuery(config.backgroundSelector + ", " + config.windowSelector + " div.close").bind('click.close', function() {
					close()
				})
				jQuery(config.backgroundSelector).data('popupEventSet', 'true')
			}
			if (jQuery(config.windowSelector + " input.tag_deactivate").data('popupEventSet') != 'true') {
				jQuery(config.windowSelector + " input.tag_deactivate").bind('change.popup', function() {
					if (jQuery(this).is(':checked')) {
						Estate.Cookies.Set(config.popupCookieName, "true", config.popupCookieExpire, "/", "", false);
					} else {
						Estate.Cookies.Delete(config.popupCookieName);
					}
				})
				jQuery(config.windowSelector + " input.tag_deactivate").data('popupEventSet', 'true')
			}
		}
	}
	
	/**
	 * Closes popup
	 */
	this.Close = function() {
		close();
	}
	/* End public */
};
