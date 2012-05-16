/**
 * @fileOverview Graphite Results Filter
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2012-04-20
 * @version 0.1 - 2012-04-20
 */







/**
 * @namespace Menu Results Filter
 * @class
 */
graphite.blocks.navigation.resultsFilter = (function() {
  /* Start public */
  return {
    /**
     * Description
     *
     * @example
     * graphite.blocks.navigation.resultsFilter.init('.gp_searchFilter');
     */
    init: function(selector) {
      jQuery(selector).each(function() {
        var root = jQuery(this);
        var form = root.find('.gp_local_filter');
        var list = root.find('.gp_local_results');
        var url = form.attr('data-ajaxurl');
        var resultsFilter = new graphite.blocks.navigation.resultsFilter.obj({root: root, form: form, list: list, url: url});
        resultsFilter.init();
      });
    }
  };
  /* End public */
})();



/**
 * @namespace Root namespace for Graphite blocks.widgets.accordion
 */
graphite.blocks.navigation.resultsFilter.obj = function(config) {
  var _root;
  var _config = {};
  jQuery.extend(_config, config)
  _config.json = {};
  _config.log = false;
  if (_config.root.find('.local_log').size() > 0) {
    _config.log = true;
  }
  
  // Set events on all form fields
  function setFilterEvents() {
    // Event: form field change
    _config.form.find(':radio, :checkbox, :text').bind('change', filterList);
  }

  // initiate the filtering proces of the existing results list
  function filterList() {
    var queryJSON = getQueryJSON();
    updateList(queryJSON);
  }
  
  // Initiates query and sends Ajax request to get new data.
  function updateList(queryJSON) {
    var urlQuery = createURLQuery(queryJSON);
    console.log('REQUEST: ' + _config.url + '?' + urlQuery + '<br /><br />')
    if (_config.log == true) {
      _config.root.find('.local_log').html('REQUEST: ' + _config.url + '?' + urlQuery + '<br /><br />');
    }
    jQuery.ajax({
      url: _config.url + '?' + urlQuery,
      success: handleQueryRequestSuccess,
      fail: handleQueryRequestFail
    })
    
    // If the response of the server was successful
    function handleQueryRequestSuccess(data) {
      if (_config.log == true) {
        _config.root.find('.local_log').html('REPLY SERVER: ' + data + '<br /><br />' + _config.root.find('.local_log').html());
      }
      _config.json = JSON.parse(data);
      var orderedArray = createOrderedArray(_config.json);
      //_config.root.find('.local_log').html(orderedArray + '<br />' + _config.root.find('.log').html());
    }
    
    // If the server was unable to response correctly
    function handleQueryRequestFail(data) {
      throw Error('Something went wrong with ajax request.');
    }
    
    // Creates an ordered representation of the JSON
    function createOrderedArray(json) {
      var sortKey = 'a';
      var arr = new Array();
      window.json = json;
      for (var i = 0; i < json.data.length; i++) {
        arr.push([i, json.data[i]['properties'][sortKey]]);
      }
      arr.sort(sortMultiDimensionalArray);
      console.log(arr);
      return arr;
    }
  }
  
  function sortMultiDimensionalArray(a, b) {
    return ((a[1] < b[1]) ? -1 : ((a[1] > b[1]) ? 1 : 0));
  }
  
  // Checks if some item in JSON exists
  function jsonItemExists(item) {
    if (typeof(item) == 'string' && item !== '') {
      return true;
    }
    return false;
  }
  
  // Creates GET query with JSON object
  function createURLQuery(queryJSON) {
    var get = '';
    var filters = '';
    var filterType;
    
    get += 'keywords=' + queryJSON.keywords;
    
    for (var i = 0; i < queryJSON.fields.length; i++) {
      if (jsonItemExists(queryJSON.fields[i].key) && jsonItemExists(queryJSON.fields[i].value)) {
        filterType = 'or';
        if (jsonItemExists(queryJSON.fields[i].filterType)) {
          filterType = queryJSON.fields[i].filterType;
        }
        filters += '&filtertype_' + queryJSON.fields[i].key + '=' + filterType;
      }
    }
    get += filters;
    
    for (var i = 0; i < queryJSON.fields.length; i++) {
      if (jsonItemExists(queryJSON.fields[i].key) && jsonItemExists(queryJSON.fields[i].value)) {
        get += '&' + queryJSON.fields[i].key + '=' + encodeURIComponent(queryJSON.fields[i].value);
      }
    }
    
    return get;
  }

  // Sifts through all form fields and builds JSON object of query
  function getQueryJSON() {
    var query = {};
    query.fields = [];
    query.keywords = '';
    
    // Get keyword search
    if (_config.root.find('input[data-keyword]').size() > 0) {
      query.keywords = _config.root.find('input[data-keyword]').val();
    }
    
    // Loop through all checked form fields    
    _config.form.find(':checked').each(function() {
      var item = jQuery(this);
      var key = item.attr('data-field');
      var value = item.val();
      
      var appended = appendToExistingKey(key, value);
      
      if (appended == false) {
        var filterType = 'or';
        var filterType = _config.form.attr('data-filterType-' + key);
        if (filterType === 'and' || filterType === 'or') {
          filterType = filterType;
        }
        query.fields.push({
          key: key,
          value: value,
          filterType: filterType
        });
      }

      // Append value to matching key if it exists
      function appendToExistingKey(key, value) {
        for (var i = 0; i < query.fields.length; i++) {
          if (query.fields[i].key == key) {
            query.fields[i].value += '~' + value;
            return true;
          }
        }
        return false;
      }
    });

    return query;
  }
  
  this.init = function() {
    setFilterEvents();
    
      
      // Server: returns JSON
      // Builds filtered list with HTML template (moustache?)
      // Handles paging and sorting
    // Event: form field onmouseover (optional)
      // Sifts through all form fields
      // Builds JSON object of query
      // Based on that JSON, items will be greyed out that would be hidden after the user toggles the filter
    // After list has been built
      // Event: onclick filter
        // Click toggles ASC and DESC
        // Builds JSON object of query
        // Creates GET query with JSON object
        // Sends Ajax request
        // Server: returns JSON
        // Builds filtered list with HTML template (moustache?)
      // Event: Set paging size
        // Rebuild filtered list
        // Rebuild paging
  }
};
