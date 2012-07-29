/*
 * Summary:
 * Converts PNG-images in <IMG>-tags to something IE6 supports
 * 
 * Usage:
   Estate.Hack.PNG4IE6.Run()
 */
Estate.Hack.PNG4IE6 = ( function() {
	/* START PUBLIC */
	return {
		Run: function(obj) {
			for(var i=0; i<document.images.length; i++){
				var img=document.images[i]
				var imgName=img.src.toUpperCase()
				if (imgName.indexOf("-ALPHA.PNG") >= 0 || imgName.indexOf("-ALPHA.SFLB.ASHX") >= 0){
					var imgID=(img.id) ? "id='"+img.id+"' " : ""
					var imgClass=(img.className) ? "class='"+img.className+"' " : ""
					var imgTitle=(img.title) ? "title='"+img.title+"' " : "title='"+img.alt+"' "
					var imgStyle="display:inline-block;"+img.style.cssText 
					var imgAttribs=img.attributes;
					for (var j=0; j<imgAttribs.length; j++){
						var imgAttrib=imgAttribs[j];
						if (imgAttrib.nodeName=="align"){          
							if (imgAttrib.nodeValue=="left") imgStyle="float:left;"+imgStyle
							if (imgAttrib.nodeValue=="right") imgStyle="float:right;"+imgStyle
							break
						}
					}
					var strNewHTML="<span "+imgID+imgClass+imgTitle
					strNewHTML+=" style=\"" + "width:"+img.width + "px; height:"+img.height+"px;"+imgStyle+";"
					strNewHTML+="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
					strNewHTML+="(src=\'"+img.src+"\', sizingMethod='scale');\"></span>" 
					img.outerHTML=strNewHTML
					i=i-1
				}
			}
		}
	}
	/* END PUBLIC */
})();






/*
 * Summary:
 * Makes certain incompatible CSS selectors and selectors work in IE6
 * 
 * Usage:
   Estate.Hack.IE6ClassGenerator.Run()
 */
Estate.Hack.IE6Fix = ( function() {
	/* START PRIVATE */
	var httpRequest
	if (window.ActiveXObject) {
		httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
	}
	var newRules = new Array
	var config = {
		classNumber: 0,
		className: "IE6Fix",
		regExIncompatibleSelectors: {
			attribute: /\[/,
			adjacent: /\+/,
			child: /\>/,
			firstchild: /\:first-child/
		},
		regExIncompatibleStyles: {
			minHeight: /\min-height/,
			alphaPNG: /\alpha.png/
		},
		regExUnQueriable: {
			link: /\:link/,
			hover: /\:hover/,
			active: /\:active/,
			visited: /\:visited/,
			focus: /\:focus/,
			after: /\:after/,
			IE7Fix: /\*\:first-child\+html/
		},
		regExDelete: {
			comment: /\/\*([\s\S]*?)\*\//g
		}
	}
	
	var fileCache = {};
	function loadFile(href) {
		try {
			if (!fileCache[href]) {
				httpRequest.open("GET", href, false);
				httpRequest.send();
				if (httpRequest.status == 0 || httpRequest.status == 200) {
					fileCache[href] = httpRequest.responseText;
				}
			}
		} catch (e) {
			// ignore errors
		} finally {
			return fileCache[href] || "";
		}
	}
	
	function GetNewClassName() {
		return config.className + config.classNumber++
	}

	function IsIE6Incompatible( cssFileSource ) {
		for (regEx in config.regExIncompatibleSelectors) {
			if ( config.regExIncompatibleSelectors[regEx].test( cssFileSource ) == true ) {
				return true
			}
		}
		for (regEx in config.regExIncompatibleStyles) {
			if ( config.regExIncompatibleStyles[regEx].test( cssFileSource ) == true ) {
				return true
			}
		}
		return false
	}

	function HasIE6IncompatibleStyles( styles ) {
		for (regEx in config.regExIncompatibleStyles) {
			if ( config.regExIncompatibleStyles[regEx].test( styles ) == true ) {
				return true
			}
		}
		return false
	}

	function IsQueriable( cssFileSource ) {
		for (regEx in config.regExUnQueriable) {
			if ( config.regExUnQueriable[regEx].test( cssFileSource ) == true ) {
				return false
			}
		}
		return true
	}

	function RemoveComments( cssFileSource ) {
		var _cssFileSource = cssFileSource;
		while ( config.regExDelete.comment.test(_cssFileSource) == true ) {
			_cssFileSource = _cssFileSource.replace( config.regExDelete.comment, "")
		}
		return _cssFileSource
	}

	function AddRule( selectors, styles ) {
		var regExCharacter = /\S/;
		if ( typeof selectors == "string" && typeof styles == "string" ) {
			if ( regExCharacter.test( selectors ) == true && regExCharacter.test( styles ) == true ) {
				var newRulesLength = newRules.length++
				newRules[newRulesLength] = new Array;
				newRules[newRulesLength][0] = selectors;
				newRules[newRulesLength][1] = styles;
			}
		}
	}

	function FixStyles( styles ) {
		var fixedStyles = styles
		
		if ( config.regExIncompatibleStyles.minHeight.test( fixedStyles ) == true ) {
			fixedStyles = fixedStyles.replace( config.regExIncompatibleStyles.minHeight, "height" )
		}
		if ( config.regExIncompatibleStyles.alphaPNG.test( fixedStyles ) == true ) {
			var regExURL = /\/[\S]*alpha.png/
			var regExBackgroundStyle = /background[\s\S]*;/
			var imageUrl = fixedStyles.match( regExURL )
			fixedStyles = fixedStyles.replace( regExBackgroundStyle, "background: none !important; filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+ imageUrl +"', sizingMethod='crop');" )
		}
		
		return fixedStyles
	}

	function Fixselector( selector, newClassName ) {
		var _selector = selector
		var regExInvalidSelector = /\* html/;
		
		if ( regExInvalidSelector.test( _selector ) == true ) {
			_selector = _selector.replace( regExInvalidSelector, "" )
		}
		
		return _selector
	}

	function FixCssRule( CssRule ) {
		var aCssRule = CssRule.split("{");
		var aSelectors = aCssRule[0].split(",")
		var styles = aCssRule[1]
		var stylesNeedFix = false
		var newClassName = GetNewClassName()
		
		stylesNeedFix = HasIE6IncompatibleStyles( styles )
		styles = FixStyles( styles )
		for ( var i in aSelectors ) {
			if ( (IsIE6Incompatible( aSelectors[i] ) == true || stylesNeedFix == true) && IsQueriable( aSelectors[i] ) == true ) {
				//Estate.Trace( Fixselector(aSelectors[i]) +" - "+ styles +" >> "+ newClassName )
				try {
					jQuery( Fixselector(aSelectors[i]) ).addClass( newClassName )
				}
				catch(error) {
					Estate.Trace(
						"IE6Fix: could not fix selector '"+ Fixselector(aSelectors[i]) +"'<br />"+ 
						"&ensp; Error message: "+ error.description
					)
				}
			}
		}
		AddRule( "." + newClassName, styles )
	}

	function FixCss( cssFileSource ) {
		var _cssFileSource = RemoveComments( cssFileSource );
		
		if (IsIE6Incompatible(_cssFileSource)) {
			var aCssRules = _cssFileSource.split("}")
			for (var i = 0; i < aCssRules.length; i++) {
				if (IsIE6Incompatible(aCssRules[i]) == true) {
					FixCssRule(aCssRules[i])
				}
			}
		}
	}

	function CreateNewCssWithFix() {
		var newCss = document.createStyleSheet()
		
		for (var i = 0; i < newRules.length; i++) {
			try {
				newCss.addRule( newRules[i][0] , newRules[i][1])
			}
			catch(e) {
				//
			}
		}
	}
	/* END PRIVATE */

	/* START PUBLIC */
	return {
		Run: function() {
			var CSSDocRules;
			// Load source code of all stylesheets
			for ( CSSDoc = 0; CSSDoc < document.styleSheets.length; CSSDoc++ ) {
				if ( document.styleSheets[CSSDoc].href.indexOf( "develop") < 0 ) {
					if (httpRequest) {
						FixCss( loadFile( document.styleSheets[CSSDoc].href ) );
					}
				}
			}
			CreateNewCssWithFix()
		}
	}
	/* END PUBLIC */
})();
