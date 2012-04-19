/**
 * @fileOverview All Google Maps related code
 * @author Wouter Bos
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-03-17
 */

/*
TODO:
	- Stop using startlocation when multiple locations are loaded
	- Convert to google maps V3
	- When using xml locations, the script always uses config.mapConfig.zoom as default level even when multiple locations are loaded

 * Usage:
	// START JAVASCRIPT EXAMPLE 1 show one location
		Estate.Events.AddEvent (
			document.getElementsByTagName('input')[0],
			function(e) {
				var KeyID = (window.event) ? event.keyCode : e.which;
				if (KeyID == 13) {
					Estate.GoogleMaps.Route.GetRouteInGoogleMaps( 'Vredesplein 5, Waalwijk', document.getElementById('saddr').value );
					return false;
				}
			},
			"onkeypress"
		)
	// END JAVASCRIPT EXAMPLE 1 	
 */
 
/**
 * @namespace  Returns an object that you can use to create a Google map.
 *             you provide the object with a configuration object. You can
 *             choose to either add a single location (by setting the config
 *             object) or a bunch of locations by providing an URL to an
 *             xml file. If both are supplied the xml file rules out the
 *             single location. It is possible to have multiple maps on one
 *             page. In that case: make sure you have already loaded the library.
 * 			   ### WARNING ### Class has not been tested after last rewrite.
 * @requires Google Maps
 * @see ESDN for related HTML and CSS
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-03-16
 * @constructor
 * @param {Object} [newConfig] Configuration object.
 * @param {String} [newConfig.instanceName] The name of the instance that is going to be created. You *must* provide this value if you haven't already loaded the Google library
 * @param {String} [newConfig.mapId] The id of the div that should contain the map  
 * @param {String[]} [newConfig.mapKeys] Different keys per domain. Each array item is an array itself with a piece of the domain and the google maps key. A simple example: [ ['www.domain.com', 'gmap key'] ]
 * @param {String} [newConfig.mapConfig.zoomControl] The type of zoom control on the map. Options are: "large", "small", "smallest"
 * @param {Boolean} [newConfig.mapConfig.mapTypeControl] Adds buttons on the map to switch between different map layers
 * @param {Number} [newConfig.mapConfig.zoom] Overrides default behaviour. If you load multiple locations an autozoom is used. Static zoom level. If the value is not null, that zoom level overrides the auto zoom level.
 * @param {Number} [newConfig.mapConfig.zoomBounds.minimum] Minimum zoom level
 * @param {Number} [newConfig.mapConfig.zoomBounds.maximum] Maximum zoom level
 * @param {Number} [newConfig.startLocation.lng] The longitude for a single location
 * @param {Number} [newConfig.startLocation.lat] The latitude for a single location
 * @param {Number} [newConfig.startLocation.zoom] The zoom for a single location
 * @param {Number} [newConfig.startLocation.text] The text in the text balloon
 * @param {String} [newConfig.centerMapLocation.lng] The longitude for the map center for a single location
 * @param {String} [newConfig.centerMapLocation.lat] The latitude for the map center for a single location 
 * @param {String} [newConfig.locationsUrl] The location of the XML with multiple locations
 * @param {String} [newConfig.markerImage.url] The url of the marker image of the location
 * @param {String} [newConfig.markerImage.dimensions.width] The width of the marker image
 * @param {String} [newConfig.markerImage.dimensions.height] The height of the marker image 
 * @param {String} [newConfig.markerImage.anchor.xPosition] x-position in the image where it should position on the position
 * @param {String} [newConfig.markerImage.anchor.yPosition] y-position in the image where it should position on the position
 * @param {String} [newConfig.routeNoDestination] Alerts user that no location is selected, so it's not possible to calculate a route 
 * @param {String} [newConfig.textLoading] Text that the user sees while the route is loading.
 *   
 * @example
 * // Simple map with one marker
 *	var mapConfig = {
 *		mapId: 'Map',
 *		mapKeys: [
 *			['www.domain.com', 'insert gmap key here'],
 *			['.local', 'insert gmap key here']
 *		],
 *		startLocation: {
 *			lng: 5.09206,
 *			lat: 51.57459,
 *			zoom: 13,
 *			text: 'Estate Internet'
 *		}
 *	}
 *	
 *	oMap = new Estate.GoogleMaps( mapConfig, "oMap" )
 *	oMap.Init()
 *
 * @example
 * // Map with markers from an XML
 *	var mapConfig = {
 *		mapId: 'Map',
 *		mapKeys: {
 *			['www.domain.com', 'insert gmap key here'],
 *			['.local', 'insert gmap key here']
 *		},
 *		locationsUrl: '/scripts/data/googleMaps-locations.xml'
 *	}
 *	
 *	oMap = new Estate.GoogleMaps( mapConfig, "oMap" )
 *	oMap.Init()
 *
 */
Estate.GoogleMaps = function(newConfig, instanceName) {
	var error
	error = Estate.Check.ArgumentsCount(arguments.length, 2);
	if (error != "") throw new Error(error);

	var map;
	var bounds
	var aMarkers = new Array(0);
	var selectedMarker = {
		lat: 0,
		lng: 0
	};
	var config = {
		instanceName: '', /* private */
		mapId: 'Map',
		mapKeys: [
			['', ''],
		],
		mapConfig: {
			zoomControl: 'large',
			mapTypeControl: true,
			zoom: null,
			zoomBounds: {
				minimum: 1,
				maximum: 17
			}
		},
		startLocation: {
			lng: 5.09206,
			lat: 51.57459,
			zoom: 17,
			text: ''
		},
		centerMapLocation: {
			lng: undefined,
			lat: undefined
		},
		locationsUrl: '',
		markerImage: {
			url: '',
			dimensions: {
				width: '37',
				height: '31'
			},
			anchor: {
				xPosition: -1,
				yPosition: -1
			}
		},
		routeNoDestination: "Selecteer eerst een bestemming voordat u een route kunt berekenen",
		textLoading: 'Bezig met laden kaart...',
		textBrowserIncompatible: 'Uw browser ondesteunt geen Google Maps of is zodanig ingesteld dat het niet ondersteunt.'
	}

	error = Estate.Check.LiteralUpdatable(config, newConfig);
	if (error != "") throw new Error(error);
	setConfig(newConfig, instanceName);


	/* start getters, setters and checkers */
	function setConfig(newConfig, instanceName) {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, [1, 2]);
		if (error != "") throw new Error(error);

		config.instanceName = instanceName;
		Estate.Check.UpdateLiteral(config, newConfig)

		Estate.GoogleMaps.Check.CheckConfig(config)
	}

	function getKey() {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 0);
		if (error != "") throw new Error(error);

		var url = document.location.href;
		for (var i = 0; i < config.mapKeys.length; i++) {
			if (url.indexOf(config.mapKeys[i][0]) >= 0) {
				return config.mapKeys[i][1]
			}
		}
		return ''
	}

	function mapKeysExists() {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, 0);
		if (error != "") throw new Error(error);

		if (getKey() == '' && document.location.href.indexOf('//localhost') < 0) {
			return false;
		}
		return true
	}

	function isLibraryLoaded() {
		if (typeof (GBrowserIsCompatible) == 'undefined') {
			return false;
		}
		return true;
	}
	/* end getters, setters and checkers */



	function loadGoogleMapsLibrary() {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 0);
		if (error != "") throw new Error(error);
		if (mapKeysExists() == false) {
			throw new Error("Cannot find an appropriate Google Maps key for this domain.");
		}

		var script = document.createElement("script");
		script.type = "text/javascript";
		script.src = "http://maps.google.com/maps?file=api&v=2.x&key=" + getKey() + "&async=2&callback=" + config.instanceName + ".Init";
		document.body.appendChild(script);
	}

	function mapAddMarker(oGLatLng, locationText, configMarkerImage, locationsType) {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 4);
		if (error != "") throw new Error(error);

		var newMarker = createMarker(oGLatLng, configMarkerImage);
		newMarker.LocationText = locationText;
		map.addOverlay(newMarker);
		var newMarkerLatLng = newMarker.getLatLng()
		if (locationsType == "multiple locations") {
			GEvent.addListener(
				newMarker,
				"click",
				function() {
					if (locationText != '') {
						newMarker.openInfoWindowHtml(locationText);
					}
					map.panTo(newMarkerLatLng);
					selectedMarker.lat = newMarkerLatLng.lat();
					selectedMarker.lng = newMarkerLatLng.lng();
				}
			);
		} else {
			selectedMarker.lat = newMarkerLatLng.lat();
			selectedMarker.lng = newMarkerLatLng.lng();
			if (locationText != '') {
				newMarker.openInfoWindowHtml(locationText);
			}
		}
		var newMarkerId = aMarkers.length
		if (typeof (aMarkers[newMarkerId]) != "undefined") {
			newMarkerId += 1;
		}
		aMarkers[newMarkerId] = newMarker;
	}

	function createMarker(oGLatLng, configMarkerImage) {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 2);
		if (error != "") throw new Error(error);

		var newMarker;
		if (configMarkerImage.url != '') {
			// Create custom marker if available
			var markerIcon = new GIcon();
			markerIcon.image = configMarkerImage.url;
			markerIcon.iconSize = new GSize(configMarkerImage.dimensions.width, configMarkerImage.dimensions.height);
			if (configMarkerImage.anchor.xPosition >= 0 && configMarkerImage.anchor.yPosition >= 0) {
				markerIcon.iconAnchor = new GPoint(configMarkerImage.anchor.xPosition, configMarkerImage.anchor.yPosition);
			} else {
				markerIcon.iconAnchor = new GPoint(Math.floor(configMarkerImage.dimensions.width / 2), configMarkerImage.dimensions.height);
			}
			markerIcon.infoWindowAnchor = new GPoint(Math.floor(configMarkerImage.dimensions.width / 2), 0);
			return new GMarker(oGLatLng, markerIcon);
		} else {
			return new GMarker(oGLatLng);
		}
	}

	function mapSetZoomBounds() {
		var mapTypes = map.getMapTypes();
		for (var i = 0; i < mapTypes.length; i++) {
			mapTypes[i].getMinimumResolution = function() { return config.mapConfig.zoomBounds.minimum; }
			mapTypes[i].getMaximumResolution = function() { return config.mapConfig.zoomBounds.maximum; }
		}
	}

	function loadExternalData(locationsUrl) {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 1);
		if (error != "") throw new Error(error);

		if (locationsUrl != '') {
			var point;
			var locationTextCollection;
			var location;
			bounds = new GLatLngBounds();
			var markerImage = {};
			Estate.GoogleMaps.Check.CheckURL(locationsUrl)
			GDownloadUrl(locationsUrl, function(data, responseCode) {
				var xml = GXml.parse(data);
				var markers = xml.documentElement.getElementsByTagName("marker");
				if (markers.length > 0) {
					for (var i = 0; i < markers.length; i++) {
						Estate.Check.UpdateLiteral(markerImage, config.markerImage);
						point = new GLatLng(parseFloat(markers[i].getAttribute("lat")), parseFloat(markers[i].getAttribute("lng")));
						bounds.extend(point);
						locationText = GXml.value(markers[i].getElementsByTagName("locationText")[0]);
						if (markers[i].getAttribute("imageUrl") != null) {
							markerImage.url = markers[i].getAttribute("imageUrl")
						}
						if (markers[i].getAttribute("width") != null) {
							markerImage.dimensions.width = markers[i].getAttribute("width")
						}
						if (markers[i].getAttribute("height") != null) {
							markerImage.dimensions.height = markers[i].getAttribute("height")
						}
						if (markers[i].getAttribute("anchorX") != null) {
							markerImage.anchor.xPosition = markers[i].getAttribute("anchorX")
						}
						if (markers[i].getAttribute("anchorY") != null) {
							markerImage.anchor.yPosition = markers[i].getAttribute("anchorY")
						}

						mapAddMarker(point, locationText, markerImage, "multiple locations");
					}
					zoomAndCenterOnAllMarkers();
				}
			});
		}
	}

	function zoomAndCenterOnAllMarkers() {
		map.setCenter(bounds.getCenter());
		Estate.Events.AddEvent(
			window,
			function() {
				map.setCenter(bounds.getCenter());
			},
			"onresize"
		)

		if (typeof (config.mapConfig.zoom) == "number") {
			map.setZoom(config.mapConfig.zoom);
		} else {
			map.setZoom(map.getBoundsZoomLevel(bounds));
			Estate.Events.AddEvent(
				window,
				function() {
					map.setZoom(map.getBoundsZoomLevel(bounds));
					map.setCenter(bounds.getCenter());
				},
				"onresize"
			)
		}
	}

	function setCenter(startLocation) {
		var centerPoint = startLocation
		if (typeof (config.centerMapLocation.lng) != "undefined" && typeof (config.centerMapLocation.lat) != "undefined") {
			centerPoint = new GLatLng(config.centerMapLocation.lat, config.centerMapLocation.lng);
		}
		map.setCenter(centerPoint, config.startLocation.zoom);
	}

	function addControls() {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 0);
		if (error != "") throw new Error(error);


		if (typeof (config.mapConfig.zoomControl) == "string") {
			switch (config.mapConfig.zoomControl) {
				case "large":
					map.addControl(new GLargeMapControl());
					break;
				case "small":
					map.addControl(new GSmallMapControl());
					break;
				case "smallest":
					map.addControl(new GSmallZoomControl());
					break;
			}
		}
		if (typeof (config.mapConfig.mapTypeControl) == "boolean") {
			if (config.mapConfig.mapTypeControl == true) {
				map.addControl(new GMapTypeControl());
			}
		}
	}

	function createMap() {
		var error;
		error = Estate.Check.ArgumentsCount(arguments.length, 0);
		if (error != "") throw new Error(error);

		if (GBrowserIsCompatible()) {
			Estate.Events.AddEvent(window, GUnload, "onunload")
			var startLocation = new GLatLng(config.startLocation.lat, config.startLocation.lng);

			map = new GMap2(document.getElementById(config.mapId));
			map.setCenter(startLocation, config.startLocation.zoom);
			addControls()
			map.enableScrollWheelZoom();
			mapSetZoomBounds();

			if (config.locationsUrl != '') {
				loadExternalData(config.locationsUrl);
			} else {
				mapAddMarker(startLocation, config.startLocation.text, config.markerImage, "single location");
			}
		} else {
			alert(config.textBrowserIncompatible);
		}
	}



	/* Start public */
	/**
	* Creates the Google maps and shows it on the page
	* @since 1.0 - 2010-02-23
	* @version 1.0 - 2010-02-23
	* @example
	* instance.Init()
	*/
	this.Init = function() {
		if (isLibraryLoaded() == false) {
			loadGoogleMapsLibrary();
		} else {
			Estate.Events.AddEvent(document, GUnload, "onunload")
			createMap();
		}
	}

	/**
	* Reloads external data througha new config object
	* @since 1.0 - 2010-02-23
	* @version 1.0 - 2010-02-23
	* @param
	* @example newConfig {Object} newConfig Holds new config object with at least a new URL to the XML
	* instance.LoadExternalData(newConfig)
	*/
	this.LoadExternalData = function(newConfig) {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, 1);
		if (error != "") throw new Error(error);
		error = Estate.Check.VariableType(newConfig, "object");
		if (error != "") throw new Error(error);
		error = Estate.Check.VariableType(newConfig.locationsUrl, "string");
		if (error != "") throw new Error(error);
		if (config.locationsUrl == '') {
			throw new Error("URL string is empty!")
		}

		setConfig(newConfig)
		map.clearOverlays()
		loadExternalData(config.locationsUrl)
	}

	/**
	* Center map on a specific marker that was loaded from the external XML 
	* @since 1.0 - 2010-02-23
	* @version 1.0 - 2010-02-23
	* @param
	* @example newConfig {Number} index The index of the item in the XML
	* instance.Goto(5)
	*/
	this.Goto = function(index) {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, 1);
		if (error != "") throw new Error(error);
		error = Estate.Check.VariableType(index, "number");
		if (error != "") throw new Error(error);

		map.panTo(aMarkers[index].getLatLng())
		aMarkers[index].openInfoWindowHtml(aMarkers[index].LocationText);
	}

	/**
	* Get route to the selected marker 
	* @since 1.0 - 2010-02-23
	* @version 1.0 - 2010-02-23
	* @param {String} routeId The id of the box the route will be loaded in. 
	* @param {String} startLocation The address of the start location. This is normally a value you get from a input form field.
	* @example 
	* instance.GetRoute("divId", "Fratershof 16, Oss")
	*/
	this.GetRoute = function(routeId, startLocation) {
		var error
		error = Estate.Check.ArgumentsCount(arguments.length, 2);
		if (error != "") throw new Error(error);
		error = Estate.Check.ElementById(routeId);
		if (error != "") throw new Error(error);
		error = Estate.Check.VariableType(startLocation, "string");
		if (error != "") throw new Error(error);

		if (selectedMarker.lat == 0 || selectedMarker.lng == 0) {
			alert(config.routeNoDestination)
		}

		map.closeInfoWindow()
		Estate.GoogleMaps.Route.Init(routeId, startLocation, map, selectedMarker.lat, selectedMarker.lng)

		return false
	}
	/* End public */
};






/**
 * @namespace Methods for generating routes
 * @see ESDN for related HTML and CSS
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.GoogleMaps.Route = ( function() {
	var routePanel;

	
	
	/* Start public */
	return {
	/**
	 * Generates route
	 *
	 * @since 1.0 - 2010-02-23
	 * @version 1.0 - 2010-02-23
	 * @param {String} routeId The Id of the element in which the route information will be loaded
	 * @param {String} startLocation The address of the start of the route
	 * @param {Object} map An instance of Estate.GoogleMaps
	 * @param {Number|String} lat The latitude of the destination 
	 * @param {Number|String} lng The longitude of the destination
	 * @example
	 * Estate.GoogleMaps.Route.Init("routeId", "Fratershof 16, Oss", oMap, 51.0, 51,0)
	 */
	Init: function( routeId, startLocation, map, lat, lng ) {
			var error
			error = Estate.Check.ArgumentsCount( arguments.length, 5 );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.ElementById( routeId );
			if ( error != "" ) throw new Error( error );
			
			_routeId = routeId
			routePanel = document.getElementById(routeId);
			routePanel.innerHTML = ""
			Estate.CSSTools.RemoveClass( routePanel, "displayNone" )
			var directions = new GDirections(map, routePanel);
			GEvent.addListener(directions, "error", Estate.GoogleMaps.Route.ShowStatusError );
        	directions.clear()
			directions.load("from: "+ startLocation +" to: " + lat + ", " + lng);
		},

		/**
		 * Handles error if Google maps cannot generate route
		 * @private
		 */
		ShowStatusError: function() {
			alert('Route kan niet geladen worden. Misschien heeft u geen geldig adres ingevoerd.')
			Estate.CSSTools.AddClass( routePanel, "displayNone" )
		},

		/**
		 * Loads route in the Google maps website in a new window
		 * @param {String} destinationAddress The address of the arrival location
		 * @param {String} startAddress The address of the start location
		 * @example
		 * Estate.GoogleMaps.Route.GetRouteInGoogleMaps()
		 */
		GetRouteInGoogleMaps: function(destinationAddress, startAddress) {
			window.open("http://maps.google.nl/maps?daddr="+ escape(destinationAddress) +"&geocode=&dirflg=&saddr="+ escape(startAddress) +"&f=d&sspn=0.010732,0.022488&ie=UTF8")
		}
	}
	/* End public */
})();






/**
 * @namespace Some helper functions for checking
 * @ignore
 * @since 1.0 - 2010-02-23
 * @version 1.0 - 2010-02-23
 */
Estate.GoogleMaps.Check = ( function() {
	function httpRequest() {
		var obj;
		if (window.XMLHttpRequest) obj= new XMLHttpRequest(); 
		else if (window.ActiveXObject){
			try{
				obj= new ActiveXObject('MSXML2.XMLHTTP.3.0');
			}
			catch(er){
				try{
					obj= new ActiveXObject("Microsoft.XMLHTTP");
				}
				catch(er){
					obj= false;
				}
			}
		}
		return obj;
	}

	/* Start public */
	return {
		// Does some basic checking to see if the config variable is correct
		CheckConfig: function( config ) {
			var error
			error = Estate.Check.ArgumentsCount( arguments.length, 1 );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( config, "object" );
			if ( error != "" ) throw new Error( error );
			
			error = Estate.Check.ElementById( config.mapId );
			if ( error != "" ) throw new Error( error );			
			error = Estate.Check.VariableType( config.startLocation.lng, "number" );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( config.startLocation.lat, "number" );
			if ( error != "" ) throw new Error( error );
		},
		
		// Checks if an URL exists. This is used to check if the path to the XML is valid
		CheckURL: function( locationsUrl ) {
			var oHttpRequest = new httpRequest();
			oHttpRequest.open("HEAD", locationsUrl, false);
			oHttpRequest.send(null);		
			if (oHttpRequest.status != 200) {
				throw new Error( "Received an " + oHttpRequest.status +"-error while loading the URL '"+ locationsUrl +"'" )
			}
		}
	/* End public */
	}
})();
