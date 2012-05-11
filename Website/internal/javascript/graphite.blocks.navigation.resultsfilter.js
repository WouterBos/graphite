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
    jQuery.ajax({
      url: _config.url,
      success: handleQueryRequestSuccess,
      fail: handleQueryRequestFail
    })
    
    function handleQueryRequestSuccess(data) {
      var orderedArray = createOrderedArray(JSON.parse(_config.json()));
      console.log(orderedArray);
    }

    function handleQueryRequestFail(data) {
      throw Error('Something went wrong with ajax request.');
    }
    
    function createOrderedArray(json) {
      var arr = new Array();
      for (var i = 0; json.data.length; i++) {
        arr.push()
      }
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
    var filterType
    for (var i = 0; i < queryJSON.fields.length; i++) {
      if (jsonItemExists(queryJSON.fields[i].key) && jsonItemExists(queryJSON.fields[i].value)) {
        if (filters.length > 0) {
          filters += '~';
        }
        filterType = 'or';
        if (jsonItemExists(queryJSON.fields[i].filterType)) {
          filterType = queryJSON.fields[i].filterType;
        }
        filters += queryJSON.fields[i].key + '=' + filterType;
      }
    }
    get += 'filterType=' + filters;
    
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
