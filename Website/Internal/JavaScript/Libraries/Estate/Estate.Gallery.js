/**
 * @fileOverview Code for generating an image gallery
 * @author Wouter Bos
 * @since 1.0 - 2010-04-7
 * @version 1.0 - 2010-04-7
 */






/**
 * @namespace Creates a gallery from a predefined list with images.
 * 			  ### WARNING ### Class has not been tested after last rewrite.
 * @requires jQuery
 * @see ESDN for related HTML and CSS
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-04-6
 * @constructor
 * @param {Object} [newConfig] Configuration object.
 * @param {String} [newConfig.containerSelector] The selector to the gallery container
 * @param {Number} [newConfig.popupAnimationSpeed] Duration of the div popup in milliseconds
 * @param {Boolean} [newConfig.showImageSizeMedium] If false, no popup is offered
 * @param {Boolean} [newConfig.showImageSizeLarge] If false, no popup is offered
 * @param {Number} [newConfig.thumbWidth] Width of a single thumbnail
 * @param {Number} [newConfig.thumbHeight] Height of a single thumbnail
 * @param {String} [newConfig.thumbScrollOrientation] Scrolling through the thumbnails is either 'horizontal' or 'vertical'
 * @param {Number} [newConfig.thumbScrollOffset]
 * @param {String} [newConfig.thumbLoopType] Looping through the thumbnails is either 'scrollback' or 'continuous'
 * @param {Number} [newConfig.thumbScrollSpeed] Duration of animating to the new thumb
 * @param {Number} [newConfig.backgroundOpacity] Opacity level of the layer below the popup that obscures the website
 * @param {String} [newConfig.navigationTemplate] HTML template of the navigation (arrow buttons)
 * @param {String} [newConfig.mediumSizeTemplate] HTML template of the medium size image
 * @example
 * var galleryConfig
 * galleryConfig.containerSelector = "#Gallery";
 * var gallery = new Estate.Gallery(newConfig);
 * gallery.Run();
 */
Estate.Gallery = function(newConfig) {
	var error
	error = Estate.Check.ArgumentsCount(arguments.length, [0, 1]);
	if (error != "") throw new Error(error);



	var config = {
		containerSelector: "#Gallery",
		popupAnimationSpeed: 250,
		showImageSizeMedium: true,
		showImageSizeLarge: true,
		thumbWidth: 80,
		thumbHeight: 80,
		thumbScrollOrientation: "horizontal",
		thumbScrollOffset: 75,				
		thumbLoopType: "continuous",
		thumbScrollSpeed: 200,
		backgroundOpacity: 0.5,
		navigationTemplate: "<div style='width: 100%; height: 0; clear: both;'></div> <span class='status'></span> <span class='back'></span> <span class='forward'></span>",
		mediumSizeTemplate: "<a class='mediumSize'><img alt=''></a>"
	}
	var pager;
	var isScrollable;

	/**
	 * Returns object literal with URL to medium and large size version of the thumbnail
	 * @ignore
	 */
	function getImageSet(el) {
		var imageSet = {
			medium: "",
			large: ""
		}
		var imageSetArray = jQuery(el).attr("rel").split(",");
		imageSet.medium = imageSetArray[0];
		imageSet.large = imageSetArray[1];

		return imageSet;
	}

	/**
	 * Shows selected medium size image & adds link to large size image
	 * @ignore
	 */
	function setMediumSize(gallery, imageSet) {
		if (config.showImageSizeMedium == true) {
			jQuery(gallery).find("a.mediumSize img").attr("src", imageSet.medium);
			if (config.showImageSizeLarge == true) {
				jQuery(gallery).find("a.mediumSize").attr("href", imageSet.large);
				jQuery(gallery).find("a.mediumSize").click(function() {
					showPopup(jQuery(this).attr("href"))
					return (false);
				})
			}
			jQuery(gallery).find("span.status").text((pager.ImageIndex() + 1) + " van " + pager.ImageCount())
		} else if (config.showImageSizeLarge == true) {
			showPopup(imageSet.large);
		}
	}

	/**
	 * Moves focus to an image in the thumblist
	 * @ignore
	 */
	function scroll(gallery, index, scrollType) {
		pager.ImageIndex(index, scrollType);

		var imageSet = getImageSet(jQuery(gallery).find("li:eq(" + (pager.ImageIndex()) + ") img"))
		setMediumSize(gallery, imageSet);

		if (isScrollable == true) {
			if (pager.ThumbBarIndexSkip() != false) {
				if (config.thumbScrollOrientation == 'horizontal') {
					jQuery(gallery).find("ul").css("margin-left", -((pager.ThumbBarIndexSkip() * config.thumbWidth) + 1) + "px")
				} else if (config.thumbScrollOrientation == 'vertical') {
					jQuery(gallery).find("ul").css("margin-top", -((pager.ThumbBarIndexSkip() * config.thumbHeight) + 1 + config.thumbScrollOffset) + "px")
				}
			}

			if (config.thumbScrollOrientation == 'horizontal') {
				if (Estate.IsIE6) {
					jQuery(gallery).find("ul").animate({ marginLeft: -((pager.ThumbBarIndex() * (config.thumbWidth / 2)) + 1) }, config.thumbScrollSpeed)
				} else {
					jQuery(gallery).find("ul").animate({ marginLeft: -((pager.ThumbBarIndex() * config.thumbWidth) + 1) }, config.thumbScrollSpeed)
				}
			} else if (config.thumbScrollOrientation == 'vertical') {
				jQuery(gallery).find("ul").animate({ marginTop: -((pager.ThumbBarIndex() * config.thumbHeight) + 1 + config.thumbScrollOffset) }, config.thumbScrollSpeed)
			}
		}
	}

	/**
	 * Shows large image in div popup
	 * @ignore
	 */
	function showPopup(url) {
		var popupHTML = ''
		var noEventsAttached = false;
		popupHTML += '<div id="DivPopupWindowGallery" class="divPopupWindow">'
		popupHTML += '	<span class="divPopupContent">'
		popupHTML += '		<span class="close">Sluiten <span>x</span></span>'
		popupHTML += '		<br />'
		popupHTML += '		<img src="" alt="" id="DivPopupImage">'
		popupHTML += '	</span>'
		popupHTML += '</div>'
		popupHTML += '<div id="DivPopupBackgroundGallery" class="divPopupBackground"></div>'

		if (jQuery("#DivPopupWindowGallery").size() == 0) {
			jQuery("body").append(popupHTML)
			noEventsAttached = true
		}
		jQuery("#DivPopupImage").attr("src", url)
		jQuery("#DivPopupWindowGallery").slideDown(config.popupAnimationSpeed, function() { jQuery('html, body').animate({ scrollTop: 0 }, 500) });
		jQuery("#DivPopupBackgroundGallery").css("opacity", 0)
		jQuery("#DivPopupBackgroundGallery").show()
		jQuery("#DivPopupBackgroundGallery").animate({ opacity: config.backgroundOpacity }, config.popupAnimationSpeed);
		
		if (noEventsAttached == true) {
			jQuery("#DivPopupWindowGallery span.close").click(function() {
				jQuery("#DivPopupWindowGallery").slideUp(config.popupAnimationSpeed);
				jQuery("#DivPopupBackgroundGallery").animate({ opacity: 0 }, config.popupAnimationSpeed, function() { jQuery("#DivPopupBackgroundGallery").hide() });
			})
		}
	}

	/**
	 * Checks if the content is larger than the container
	 * @ignore
	 */
	function setIsScrollable(gallery) {
		var thumbsList = jQuery(gallery).find('ul')
		
		if (config.thumbScrollOrientation == 'horizontal') {
			var thumbsListWidth = jQuery(thumbsList).width()
			var thumbsListContentWidth = jQuery(thumbsList).find('li').size() * config.thumbWidth
			if (thumbsListWidth < thumbsListContentWidth) {
				return true
			}
		} else if (config.thumbScrollOrientation == 'vertical') {
			var thumbsListHeight = jQuery(thumbsList).height()
			var thumbsListContentHeight = jQuery(thumbsList).find('li').size() * config.thumbHeight
			if (thumbsListHeight < thumbsListContentHeight) {
				return true
			}
		}
		return false;
	}

	/**
	 * updates the internal config literal
	 * @ignore
	 */
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



	/* Start public */
	if (arguments.length > 0) {
		setConfig(newConfig)
	}

	this.Run = function() {
		jQuery(config.containerSelector).each(function() {
			var gallery = this;
			var totalListItems = 0;
			isScrollable = setIsScrollable(gallery)

			if (config.thumbLoopType == "continuous") {
				if (isScrollable == true) {
					totalListItems = jQuery(gallery).find("ul").html()
					jQuery(gallery).find("ul").html(totalListItems + totalListItems)
				}
			}

			var imageSet = getImageSet(jQuery(gallery).find("li:first img"))
			jQuery(gallery).find("ul").width(jQuery(gallery).find("li").size() * config.thumbWidth);   // Set width thumbs container
			jQuery(gallery).prepend(config.mediumSizeTemplate)   // Shows medium size image
			jQuery(gallery).append(config.navigationTemplate)   // Add navigation to gallery

			// Create pager instance
			var pagerConfig = {
				imageCount: jQuery(gallery).find("li").size(),
				thumbLoopType: config.thumbLoopType
			}
			pager = new Estate.Gallery.Pager(pagerConfig)

			// Selects first item in the gallery
			setMediumSize(gallery, imageSet)
			scroll(gallery, pager.ImageIndex(), "")

			// Handles click on a thumbnail
			jQuery(gallery).find("li img").click(function() {
				var index = jQuery(gallery).find("ul li").index(jQuery(this).parent());
				scroll(gallery, index + 1, "goto")
				var imageSet2 = getImageSet(jQuery(this))
				setMediumSize(gallery, imageSet2)
			})

			if (isScrollable == true) {
				jQuery(gallery).addClass("galleryNoScroll")
				// Handles click on back button
				jQuery(gallery).find("span.back").click(function() {
					scroll(gallery, -1, "move")
				})

				// Handles click on forward button
				jQuery(gallery).find("span.forward").click(function() {
					scroll(gallery, 1, "move")
				})
			}
		})
	}
		/* End public */
};






/**
 * @namespace Helper class for Estate.Gallery. Manages the logic behind the
 * navigation through the thumbs.
 * ### WARNING ### Class has not been tested after last rewrite.
 * @class
 * @constructor
 * @ignore
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-04-6
 */
Estate.Gallery.Pager = function(newConfig) {
	var error
	error = Estate.Check.ArgumentsCount(arguments.length, [0, 1]);
	if (error != "") throw new Error(error);



	var config = {
		imageCount: 0,
		imageIndex: 1,
		imagePreviousIndex: 1,
		thumbLoopType: "undefined"
	}
	
	setConfig(newConfig)
	
	if (arguments.length > 0) {
		if (config.thumbLoopType == "continuous") {
			config.imageCount = config.imageCount / 2
			config.imageIndex = config.imageCount
		}
	}


	/**
	 * @ignore
	 * @param {Object} newConfig
	 */
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
	
	/**
	 * @ignore
	 */
	function getImageIndex() {
		if (config.thumbLoopType == "continuous") {
			var imageIndex = config.imageIndex - config.imageCount
			if (imageIndex < 0) {
				imageIndex = (config.imageCount - 1)
			}
			return imageIndex;
		} else if (config.thumbLoopType == "scrollback") {
			return config.imageIndex;
		}
	}
	
	/**
	 * @ignore
	 * @param {Number} index
	 * @param {String} moveCursorType Type of movement through the thumbnail list. Choose between "move" or "goto" 
	 */
	function setImageIndex(index, moveCursorType) {
		config.imagePreviousIndex = config.imageIndex;
		if (moveCursorType == "move") {
			config.imageIndex += index; // Moves cursor
		} else if (moveCursorType == "goto") {
			config.imageIndex = index - 1; // Sets cursor
		}

		if (config.thumbLoopType == "scrollback") {
			if (config.imageIndex < 0) {
				// scrolls from first to last
				config.imageIndex = config.imageCount - 1;
			}
			if (config.imageIndex >= config.imageCount) {
				// scrolls from last to first
				config.imageIndex = 0;
			}
		} else if (config.thumbLoopType == "continuous") {
			if (config.imageIndex < (config.imageCount - 2)) {
				// scrolls from first to last
				config.imageIndex = (config.imageCount * 2) - 3;
			}
			if ((config.imageIndex + 1) >= (config.imageCount * 2)) {
				// scrolls from last to first
				config.imageIndex = config.imageCount - 1;
			}
		}
	}
	
	/**
	 * @ignore
	 */
	function thumbBarIndex() {
		var moveIndex = config.imageIndex - 1;

		if (config.thumbLoopType == "scrollback") {
			if (moveIndex < 1) {
				moveIndex = 0;
			}
			if (moveIndex > config.imageCount - 3) {
				moveIndex = config.imageCount - 3;
			}
		}
		return moveIndex;
	}



	/* Start public */
	/**
	 * Image count getter
	 * @ignore
	 */
	this.ImageCount = function() {
		return config.imageCount
	}

	/**
	 * Returns value of what the new selected image index should be
	 * @ignore
	 */
	this.ThumbBarIndex = function() {
		return thumbBarIndex();
	}

	/**
	 * Indicates if there has to be a jump when a user is moving through the image index.
	 * @ignore
	 */
	this.ThumbBarIndexSkip = function() {
		var difference = config.imagePreviousIndex - config.imageIndex;
		if (difference > 1) {
			return thumbBarIndex() - 1;
		} else if (difference < -1) {
			return thumbBarIndex() + 1;
		} else {
			return false;
		}
	}

	/**
	 * Either gets or sets the index of the selected image
	 * @ignore
	 */
	this.ImageIndex = function(index, moveCursorType) {
		if (arguments.length == 0) {
			return getImageIndex();
		} else {
			setImageIndex(index, moveCursorType);
		}
	}
	/* End public */
};
