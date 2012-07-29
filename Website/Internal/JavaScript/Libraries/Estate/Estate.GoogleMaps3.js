/**
 * @fileOverview Google maps V3-related code
 * @author Wouter Bos
 * @since 0.1 - 2011-02-23
 * @version 0.2 - 2011-05-5
 */


/*
    Google maps V3
    - Map with one or more locations
        * With one pre-selected balloon
        * "You are here"
        * With polygons
        * With multiple levels
        * Custom icons
        * Default icons in sprite
        * Find location with address or lat/lng
    - Route

TODO: Handle marker icons with the method MarkerImage and include its anchoring point of the image

 */








/**
 * @namespace Creates Google map
 *
 * @requires Google Maps
 * @since 1.0 - 2011-02-23
 * @version 1.0 - 2011-02-23
 * @constructor
 *
 * @example
 * var map = new Estate.gmap3()
 */
Estate.gmap3 = function() {
  // Configuration object
  this._config = {
    mapOptions: {
      center: new google.maps.LatLng(58.99036, 6.00952),
      zoom: 10,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    },
    autoWrapMarkers: true,
    pointsOptions: {
      icon: 'http://gmaps-samples.googlecode.com/' +
            'svn/trunk/markers/pink/blank.png'
    },
    infoWindowOptions: {
      allowMultiple: false
    }
  };

  // Other local values
  this._overlays = {
      markers: new Array(),
      polygons: new Array()
  };
  this._map = null;
  this._infowindow = new google.maps.InfoWindow();
};



/**
 * Create map
 *
 * @ignore
 * @param {Object|String} mapBox Either the ID of the map container or the
 *      element itself.
 * @return {Object} map.
 */
Estate.gmap3.prototype._createMap = function(mapBox) {
  if (typeof (mapBox) == 'string') {
    // Supplied value is an id of an element
    return new google.maps.Map(
      document.getElementById(mapBox),
      this._config.mapOptions
    );
  } else if (typeof (mapBox) == 'object') {
    // Supplied value is a DOM element
    return new google.maps.Map(
      mapBox,
      this._config.mapOptions
    );
  }
};

/**
 * Creates infowindow
 *
 * @ignore
 * @param {Object} infoWindow An infoWindow object.
 * @param {Object} map The Google map.
 * @param {Object} overlay The object that should receive the click event.
 * @param {Object|Null} latLng latLng-object on which the infoWindow will
 *        be centered.
 */
Estate.gmap3.prototype._openInfoWindow = function(infoWindow,
                                                  map,
                                                  overlay,
                                                  latLng
                                                  ) {
  if (typeof (latLng) == 'undefined') {
    infoWindow.open(map, overlay);
  } else {
    infoWindow.open(map);
    infoWindow.setPosition(latLng);
  }
};

/**
 * Create infowindow
 *
 * @ignore
 * @param {Object} overlay The object that should receive the click event.
 * @param {String} popupText The text for the infowindow.
 * @param {Object} map The Google map.
 * @param {Boolean} allowMultiple (dis)Allow multiple infowindows.
 * @param {Object} arg_infoWindow Infowindow, stored as a private variable.
 * @param {Object|Null} latLng latLng-object on which the infoWindow will
 *        be centered.
 */
Estate.gmap3.prototype._setInfoWindowEvent = function(overlay,
                                                      popupText,
                                                      map,
                                                      allowMultiple,
                                                      arg_infoWindow,
                                                      latLng
                                                      ) {
  var openInfoWindow = this._openInfoWindow;
  google.maps.event.addListener(
    overlay,
    'click',
    function() {
      if (allowMultiple == true) {
        var infowindow = new ({content: popupText});
        openInfoWindow(infoWindow, map, overlay, latLng);
      } else {
        arg_infoWindow.setContent(popupText);
        openInfoWindow(arg_infoWindow, map, overlay, latLng);
      }
    }
  );
};

/**
 * Makes sure all markers are visible within the viewport
 *
 * @ignore
 */
Estate.gmap3.prototype._wrapMarkers = function() {
  // TODO: this method re-iterates every latlng on the map when one even just
  // one item is added. It's more efficient to add only new points to the bounds 
  if (this._config.autoWrapMarkers == true) {
    var bounds = new google.maps.LatLngBounds();
    var path;
    var aLatLng;

    // Get latlng from point
    for (var i = 0; i < this._overlays.markers.length; i++) {
        bounds.extend(this._overlays.markers[i].getPosition());
    }
    
    // Get latlng from polygon
    for (var i = 0; i < this._overlays.polygons.length; i++) {
      aLatLng = this._overlays.polygons[i].getPath();
      for (var ii = 0; ii < aLatLng.length; ii++) {
        var latLng = aLatLng.getAt(ii);
        bounds.extend(new google.maps.LatLng(
          latLng.lat(),
          latLng.lng()
        ));
      }
    }
    this._map.fitBounds(bounds);
  }
};

/**
* Clear map
*
* @ignore
* @param {Array} overlays An array with all the markers.
*/
Estate.gmap3.prototype._clear = function(overlays) {
  for (var i = 0; i < this._overlays.markers.length; i++) {
      overlays[i].setMap(null);
  }
  this._overlays.markers.length = 0;
};

/**
* Add polygons to map
*
* @ignore
* @param {Array} polygons An array containing all points and configuration
* for each polygon.
* @return {Boolean|Null} Either false or nothing.
*/
Estate.gmap3.prototype._addPolygons = function(polygons) {
  if (typeof (polygons) != 'object') {
    return false;
  }

  for (var i = 0; i < polygons.length; i++) {
    var points = new Array();
    var polygonConfig = {};
    var bounds = new google.maps.LatLngBounds();

    for (var ii = 0; ii < polygons[i].points.length; ii++) {
      points.push(
        new google.maps.LatLng(
          polygons[i].points[ii].lat,
          polygons[i].points[ii].lng
        )
      );
    }

    for (ii = 0; ii < points.length; ii++) {
      bounds.extend(points[ii]);
    }

    Estate.Check.UpdateLiteral(polygonConfig, polygons[i].options, true);
    polygonConfig.paths = points;
    polygonConfig.map = this._map;

    var polygon = new google.maps.Polygon(polygonConfig);
    this._overlays.polygons.push(polygon);

    // Add info window event
    if (polygons[i].infoWindow && polygons[i].infoWindow.popupText) {
      var latLng;
      if (typeof (bounds) != 'undefined') {
        latLng = bounds.getCenter();
      }

      Estate.gmap3.prototype._setInfoWindowEvent(
        polygon,
        polygons[i].infoWindow.popupText,
        this._map,
        this._config.infoWindowOptions.allowMultiple,
        this._infowindow,
        latLng
      );
    }
  }
};

/**
* Add markers to map
*
* @ignore
* @param {Array} points An array with all the markers.
* @return {Boolean|Null} Either false or nothing.
*/
Estate.gmap3.prototype._addPoints = function(points) {
  if (typeof (points) != 'object') {
    return false;
  }

  for (var i = 0; i < points.length; i++) {
    // Create marker
    var markerConfig = {
      position: new google.maps.LatLng(points[i].lat, points[i].lng),
      map: this._map
    };
    // Add default configuration
    if (this._config.pointsOptions) {
      Estate.Check.UpdateLiteral(
        markerConfig,
        this._config.pointsOptions,
        true
      );
    }
    // Add marker specific configuration
    if (points[i].markerOptions) {
      Estate.Check.UpdateLiteral(
        markerConfig,
        points[i].markerOptions,
        true);
    }
    // Add marker to map and store in array
    var newMarker = new google.maps.Marker(markerConfig);
    this._overlays.markers.push(newMarker);

    // Add info window event
    if (points[i].infoWindow && points[i].infoWindow.popupText) {
      Estate.gmap3.prototype._setInfoWindowEvent(
        newMarker,
        points[i].infoWindow.popupText,
        this._map,
        this._config.infoWindowOptions.allowMultiple,
        this._infowindow
      );
    }
  }
};

;

/**
 * Initiates creation map
 *
 * @param {Object|String} mapBox Either the ID of the map container or the
 *      element itself.
 * @param {Object} config A config object.
 * @param {Object} data Data with points, polygons, etc.
 * @example
    var map = new Estate.gmap3()
    map.init(
      {
        options: {
          mapId: "Map",
          zoom: 9
        }
      },
      {
        points: [
          [58.96913, 5.73486, {icon: "/images/icon.png"}],
          [59.02501, 6.02463]
        ],
        polygons: [
          [
            58.85709, 5.75272,
            58.96134, 5.82138,
            58.91103, 5.96970,
            58.87555, 5.85159
          ]
        ]
      }
    )
 */
Estate.gmap3.prototype.init = function(mapBox, config, data, directionsConfig) {
  if (typeof (config) != 'undefined') {
    Estate.Check.UpdateLiteral(this._config, config, true);
  }

  this._map = this._createMap(mapBox);

  if (typeof (data) != 'undefined') {
    if (data.points) {
      this.addItems(data);
    }
  }
  
  if (typeof(directionsConfig) == "object") {
    this._directions = new Estate.gmap3.directions(
      directionsConfig,
      this._map
    );
  }
};

/**
 * Adds all sort of items to the map
 *
 * @param {Object} data Data with points, polygons, etc.
 * @param {Boolean|Null} clearMap If true, the map will be cleared
 *      of all overlays.
 * @example
    instance.addItems(
      {
        points: [
          [58.96913, 5.73486, {icon: "/images/icon.png"}],
          [59.02501, 6.02463]
        ],
        polygons: [
          [
            58.85709, 5.75272,
            58.96134, 5.82138,
            58.91103, 5.96970,
            58.87555, 5.85159
          ]
        ]
      }
    )
 */
Estate.gmap3.prototype.addItems = function(data, clearMap) {
  if (clearMap == true) {
      this._clear(this._overlays.markers);
  }

  this._addPoints(data.points);
  this._addPolygons(data.polygons);
  this._wrapMarkers();
};

/**
 * Updates the map config
 *
 * @param {Object} config A config object.
 * @example
    instance.updateConfig(
      {
        mapOptions: {
          center: new google.maps.LatLng(58.99036, 6.00952),
          zoom: 12,
        }
      }
    )
 */
Estate.gmap3.prototype.updateConfig = function(config) {
    Estate.Check.UpdateLiteral(this._config, config);
};





var directionDisplay;
var geocoder = null;
Estate.gmap3.directions = function(newConfig, map) {
  var config = {
    directionsPanel: null,
    origin: "",
    destination: "",
    travelMode: google.maps.DirectionsTravelMode.DRIVING,
    submit: null
  };
  
  Estate.Check.UpdateLiteral(config, newConfig, true);
  
  if (config.directionsPanel) {
    showRouteInPage();
  } else {
    openRoutePopup();
  }
  
  
  function openRoutePopup() {
    Estate.Events.AddEvent(
      [config.origin,config.destination],
      function(event) {
        if (event.keyCode == 13) {
          event.preventDefault();
          event.stopPropagation();
        }
      },
      "onkeydown"
    );

    Estate.Events.AddEvent(
      [config.origin,config.destination],
      function(event) {
        if (event.keyCode == 13) {
          event.preventDefault();
          event.stopPropagation();
          openPopup();
        }
      },
      "onkeyup"
    );

    if (config.submit) {
      Estate.Events.AddEvent(
        config.submit,
        function(event) {
          event.preventDefault();
          event.stopPropagation();
        },
        "onclick"
      );
    }

    if (config.submit) {
      Estate.Events.AddEvent(
        config.submit,
        function(event) {
          event.preventDefault();
          event.stopPropagation();
          openPopup();
        },
        "onmousedown"
      );
    }
    
    function openPopup() {
      window.open("http://maps.google.nl/maps?"+
                  "saddr="+ config.origin.value +
                  "&daddr="+ config.destination.value +
                  "&hl=nl");
    }
  }
  
  function showRouteInPage() {
    var directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionsDisplay.setMap(map);
    directionsDisplay.setPanel(config.directionsPanel);
  
    if (config.submit) {
      Estate.Events.AddEvent(
        config.submit,
        function(event) {
          event.preventDefault();
          event.stopPropagation();
          _calcRoute();
        },
        "onclick"
      );
    }
    
    Estate.Events.AddEvent(
      [config.origin,config.destination],
      function(event) {
        event.preventDefault();
        event.stopPropagation();
        _calcRoute();
      },
      "onsubmit"
    );
    
    if (typeof(geo_position_js) != "undefined") {
  	  if (geo_position_js.init()) {
  		geo_position_js.getCurrentPosition(
  		  _setPosition,
  		  _handleError,
  		  { enableHighAccuracy: true }
  		);
  	  }
    }
  }

  function _setPosition(position){
    if (geocoder == null) {
      geocoder = new google.maps.Geocoder();
    }
    
    var startlocation = new google.maps.LatLng(parseFloat(position.coords.latitude), parseFloat(position.coords.longitude));
    
    config.origin.value = "Bezig met ophalen van adres..."
    geocoder.geocode(
      {
        'latLng': startlocation,
        'language': 'nl'
      },
      function(results, status){
        var content = '';
        if (status == google.maps.GeocoderStatus.OK) {
          if (results.length > 0) {
            if (results[0].formatted_address) {
              content += results[0].formatted_address;
            }
          }
        } else {
          content = "Voer een adres in";
        }
        config.origin.value = content
      }
    );
    //config.origin.value = position.coords.latitude + "," +
      //position.coords.longitude;
  }  

  function _handleError() {
    //
  }  
  
  function _calcRoute() {
    var request = {
      origin: config.origin.value,
      destination: config.destination.value,
      travelMode: google.maps.DirectionsTravelMode.DRIVING
    };
    directionsService.route(request, function(response, status){
      if (status == google.maps.DirectionsStatus.OK) {
        directionsDisplay.setDirections(response);
      }
    });
  }
};


Estate.gmap3.directions.prototype.calcRoute = function(){
}

//Estate.gmap3.directions.prototype.getDirectionsDisplay = function(){
//  return this._directionsDisplay;
//}