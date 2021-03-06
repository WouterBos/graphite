/**
 * @fileOverview Graphite Results Filter
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2012-04-20
 * @version 0.8 - 2012-06-11
 */







/**
 * @namespace Results Filter
 * @class
 */
graphite.blocks.navigation.resultsFilter = (function() {
  /* Start public */
  return {
    /**
     * Description
     *
     * @param {Object} config Configuration object.
     * 
     * @param {String|Object} config.selector jQuery selector that selects the
     *    root element of the results filter
     * 
     * @param {String} config.listTemplate Mustache template needed for
     *    rendering the results list.
     * 
     * @param {Array} config.queryDataKeys Array of JSON selectors. If a
     *    server sends a JSON reply, JavaScript will only pick up the data
     *    that is defined in this array. The selectors are converted to a
     *    one-level variable. For example: if you supply the query
     *    ['properties.id'], the Mustache key will be properties_id. 
     * 
     * @param {Array} config.listOrderData Data by which a list can be sorted.
     *    The array is typically built up like this:
     *    [ {key: 'properties.id', name: 'Era'}, {...} ]
     * 
     * @param {Array} config.listOrderTemplate Mustache template to build the
     *    order links.
     * 
     * @param {Array} config.listBatchSizes Array of different options a user
     *    has set how many items will be shown per page.
     * 
     * @param {Array} config.listOrderTemplate Mustache template to build the
     *    paging links.
     * 
     * @param {Object} config.labels Object literal with text labels for the
     *    pager.
     * 
     * @param {String} config.labels.first Pager label.
     * @param {String} config.labels.last Pager label.
     * @param {String} config.labels.previous Pager label.
     * @param {String} config.labels.next Pager label.
     * 
     * @example
     * graphite.blocks.navigation.resultsFilter.init({});
     */
    init: function(config) {
      jQuery(config.selector).each(function() {
        var root = jQuery(this);
        var form = root.find('.gp_local_filter');
        var list = root.find('.gp_local_results');
        var controlOrder = root.find('.gp_local_controlOrder');
        var controlBatch = root.find('.gp_local_controlBatch');
        var controlPaging = root.find('.gp_local_controlPaging');
        var url = form.attr('data-ajaxurl');
        var listTemplate = config.listTemplate;
        

        
        // Creates Results Filter instance
        var resultsFilter = new graphite.blocks.navigation.resultsFilter.obj(
          jQuery.extend(
            {},
            {
              root: root,
              form: form,
              list: list,
              url: url,
              controlOrder: controlOrder,
              controlBatch: controlBatch,
              controlPaging: controlPaging,
              queryDataKeys: [],
              listOrderData: [],
              listBatchSizes: [10, 25, 50],
              listBatchTemplate: '',
              listTemplate: '',
              listOrderTemplate: ''
            },
            config
          )
        );
        resultsFilter.init(); // Runs filter
      });
    }
  };
  /* End public */
})();



/**
 * @namespace Results Filter object
 */
graphite.blocks.navigation.resultsFilter.obj = function(config) {
  // Local variables, extend config objects, create instances
  var _root;
  var _config = {};
  jQuery.extend(_config, config)
  _config.json = {};
  _config.log = false;
  if (_config.root.find('.local_log').size() > 0) {
    _config.log = true;
  }
  var _pager = new graphite.blocks.navigation.resultsFilter.pager(
    _config.controlPaging,
    _config.labels
  );
  var _resultsList = new graphite.blocks.navigation.resultsFilter.list(
    _config,
    _pager
  );
  _pager.addListBuilder(_resultsList);
  
  // Set events on all form fields
  function setFilterEvents() {
    // Event: form field change
    _config.form.find(':radio, :checkbox, :text').bind('change', filterList);
  }

  // initiate the filtering proces of the existing results list
  function filterList() {
    var queryJSON = getQueryJSON();
    urlUpdate(queryJSON);
    updateList(queryJSON);
  }
  
  // Store query as hash to address bar of browser
  function urlUpdate(queryJSON) {
    var JsonString = JSON.stringify(queryJSON);
    document.location.hash = 'query=' + JsonString;
  }
  
  // Initiates query and sends Ajax request to get new data.
  function updateList(queryJSON) {
    var urlQuery = createURLQuery(queryJSON);
    if (_config.log == true) {
      _config.root.find('.local_log').html('REQUEST: ' + _config.url + '?' +
        urlQuery + '<br /><br />');
    }

    _config.list.stop(true, true).fadeTo(500, 0.1);
    _config.list.html('<strong class="gp_local_loading">Bezig met laden...</strong>' + _config.list.html())

    jQuery.ajax({
      url: _config.url + '?' + urlQuery,
      success: handleQueryRequestSuccess,
      fail: handleQueryRequestFail
    })
    
    // If the response of the server was successful
    function handleQueryRequestSuccess(data) {
      if (_config.log == true) {
        _config.root.find('.local_log').html('REPLY SERVER: ' + data +
          '<br /><br />' + _config.root.find('.local_log').html());
      }
      _config.json = JSON.parse(data);
      
      // Create controls
      var controls = graphite.blocks.navigation.resultsFilter.list.controls;
      controls.order(
        _resultsList,
        _config
      );
      controls.pageSize(
        _resultsList,
        _config
      );
      
      // Create search results list
      _resultsList.build(_config.json.data);
      
      // Disable form fields if needed
      toggleFieldDisabler(_config.json.disable);
      
      _config.list.fadeTo(500, 1);
    }
    
    // If the server was unable to response correctly
    function handleQueryRequestFail(data) {
      throw Error('Something went wrong with ajax request.');
    }
  }
  
  // Enables or disables form fields in filter form
  function toggleFieldDisabler(disable) {
    _config.root.find('input').removeAttr('disabled');
    _config.root.find('label').removeClass('disabled');
    for (var key in disable) {
      for (var i = 0; i < disable[key].length; i++) {
        var field = _config.root.find('input[data-field="' + key +
          '"][value="' + disable[key][i] + '"]');
        field.attr('disabled', 'disabled');
        _config.root.find('label[for="' + field.attr('id') +
          '"]').addClass('disabled');
      }
    }
    _config.root.find(':disabled').removeAttr('checked');
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
  
  // Rebuild checked state of search form if data is available
  function restoreState() {
    var hash = document.location.hash;
    var query = {};
    if (hash.indexOf('#query=') > -1) {
      hash = hash.replace('#query=', '');
      hash = decodeURIComponent(hash);
      query = JSON.parse(hash);
    }
    
    if (typeof(query.keywords) != 'undefined') {
      _config.root.find('input[data-keyword]').val(query.keywords);
    }
    
    if (typeof(query.fields) != 'undefined') {
      for (var i = 0; i < query.fields.length; i++) {
        var values = query.fields[i].value.split('~');
        for (var ii = 0; ii < values.length; ii++) {
          _config.root.find('input[data-field="' + query.fields[i].key + '"][value="' + values[ii] + '"]').attr('checked', 'checked');
        }
      }
    }
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
    _config.root.addClass('gp_hasJs');
    restoreState(); // Rebuild checked state of search form if data is available
    setFilterEvents(); // Set form events
    filterList(); // Create results list
  }
};






/**
 * @namespace Results Filter list object
 * 
 * @param {Object} config The config object that is provided when calling
 *    graphite.blocks.navigation.resultsFilter.init.
 * @param {Object} pager Pager instance, created with
 *    graphite.blocks.navigation.resultsFilter.pager
 */
graphite.blocks.navigation.resultsFilter.list = function(config, pager) {
  var _queryData;
  var _sortKey = '';
  var _sortOrder = '';
  var _pageSize = 10;
  var _pageIndex = 0;
  var _config = config;
  var _pager = pager;
  var _queryDataKeys = config.queryDataKeys   // The data that has to be fetched
                                             // from the queryData
  if (config.listBatchSizes.length > 0) {
    _pageSize = config.listBatchSizes[0];
  }
  
  // Creates an ordered index of the JSON
  function createOrderedIndex() {
    var arr = new Array();
    var sortKey;
    for (var i = 0; i < _queryData.length; i++) {
      if (typeof(_queryData[i]['properties'][_sortKey]) != 'undefined') {
        sortKey = _queryData[i]['properties'][_sortKey];
      } else if (typeof(_queryData[i]['values'][_sortKey]) != 'undefined') {
        sortKey = _queryData[i]['values'][_sortKey];
      } else {
        sortKey = '';
      }
      arr.push([i, sortKey]);
    }
    if (_sortKey != '') {
      arr.sort(sortMultiDimensionalArray);
    }
    if (_sortOrder == 'desc') {
      arr.reverse();
    }
    return arr;
  }
  
  // Sort multi-dimensional array by its second item
  function sortMultiDimensionalArray(a, b) {
    return ((a[1] < b[1]) ? -1 : ((a[1] > b[1]) ? 1 : 0));
  }
  
  // Returns an object with relevant search results, based on the JSON reply
  // from the server.
  function getResultsData(orderedIndex) {
    var resultBatch = {};
    var data;
    var item;
    var evalStr;
    
    resultBatch.items = new Array();
    for (var i = 0; i < orderedIndex.length; i++) {
      data = {};
      item = _queryData[orderedIndex[i][0]];
      // Set default values
      data.id = (item.values.id) ? item.values.id : '';
      data.title = (item.values.title) ? item.values.title : '';
      for (var ii = 0; ii < _queryDataKeys.length; ii++) {
        // Search for specific items in the JSON from the server and store
        // them in the results batch.
        try {
          evalStr = 'data.' + getKey(_queryDataKeys[ii]) + ' = item.' +
                    _queryDataKeys[ii];
          eval(evalStr);
        }
        catch(e) {};
      }
      resultBatch.items.push(data);
    }
    
    return resultBatch;
    
    function getKey(str) {
      return str.replace('.', '_');
    }
  }
  
  // Creates the results list HTML and generates the paging.
  function buildList() {
    // Sort search results and get the right page batch
    var orderedIndex = createOrderedIndex();
    var sliceStart = (_pageIndex * _pageSize);
    orderedIndex = orderedIndex.slice(sliceStart, sliceStart + _pageSize);
    
    // Create JSON that will get rendered with Mustache template
    var results = getResultsData(orderedIndex);
    results.total = _queryData.length;
    results.totalRender = function() {
      return function(text) {
        if (_queryData.length > 1) {
          return text + " " + _config.labels.totalPlural;
        } else if (_queryData.length <= 1) {
         return text + " " + _config.labels.totalSingular;
        }
      }
    }
    var listHtml = Mustache.render(_config.listTemplate, results);
    _config.list.html(listHtml);
    
    // Make sure screen readers will update users of changes in the DOM
    _config.list.attr('aria-live', 'assertive');
    
    // Create pager
    _pager.page(_pageIndex, _queryData.length, _pageSize);
  }
  
  // Set defaults if not set already
  function setDefaultVariables() {
    var controls
    if (!_sortOrder) {
      _sortOrder = 'asc';
    }
    if (!_sortKey) {
      if (config.listOrderData.length > 0) {
        _sortKey = graphite.blocks.navigation.resultsFilter.list.controls
                     .orderKeyName(config.listOrderData[0].key);
      }
    }
    if (!_pageSize) {
      if (config.listBatchSizes.length > 0) {
        _pageSize = config.listBatchSizes[0];
      }
    }
  }

  
  
  
  /**
   * Builds new list of search results based on the queryData and stored filter
   * preferences.
   * 
   * @param {Object} queryData A JSON with the search results in raw JSON.
   * @param {String} sortKey A key name of items in the raw JSON. That key
   *    is used to sort the list.
   * @param {String} sortOrder Sets order type: 'asc' or 'desc'.
   * @param {String} pageIndex Sets the paging index.
   */
  this.build = function(queryData, sortKey, sortOrder, pageSize, pageIndex) {
    // Store arguments as local variable
    if (queryData) {
      _queryData = queryData;
    }
    if (sortOrder) {
      _sortOrder = sortOrder;
    }
    if (sortKey) {
      _sortKey = sortKey;
    }
    if (pageSize) {
      _pageSize = pageSize;
    }
    _pageIndex = (pageIndex) ? pageIndex : 0;
    
    setDefaultVariables();
    
    // Build the results list
    buildList(_queryData);
  }
};






/**
 * @namespace Results Filter list object
 * 
 * @param {Object} resultsList The list object.
 * @param {Array} config Global config object.
 */
graphite.blocks.navigation.resultsFilter.list.controls = (function() {
  // Return the text after the last dot of a string. That way you get the last
  // key of a JSON selector Example: function('root.trunk.leaf') will return
  // the key 'leaf'.
  function orderKeyName(str) {
    var key = str;
    if (key.lastIndexOf('.') >= 0) {
      return key.substring(key.lastIndexOf('.') + 1);
    }
    return key;
  }

  // Make one list order link appear selected.  
  function orderSetActive(selected, config) {
    // Deselect all other items
    config.controlOrder.find('a').not(selected).removeClass('gp_active')
                                               .removeAttr('data-sort');
    // Make selected item active
    selected.addClass('gp_active');
    // Set correct data sorting
    var sort = selected.attr('data-sort');
    if (sort == undefined || sort == 'desc') {
      selected.attr('data-sort', 'asc');
    } else if (sort == 'asc') {
      selected.attr('data-sort', 'desc');
    }
  }
  
  // Make one page size link appear selected.
  function pageSizeSetActive(selected, config) {
    // Deselect all other items
    config.controlBatch.find('a').not(selected).removeClass('gp_active');
    
    // Make selected item active
    selected.addClass('gp_active');
  }
  
  return {
    /**
     * Create list order links
     * 
     * @param {Object} resultsList Instance of
     *    graphite.blocks.navigation.resultsFilter.list.
     * 
     * @param {Object} config Config object of
     *    graphite.blocks.navigation.resultsFilter.
     */
    order: function(resultsList, config) {
      var activeIndex = 0;
      config.controlOrder.find('a[data-key]').each(function(index) {
        if  (jQuery(this).hasClass('gp_active') == true) {
          activeIndex = index;
        }
      });
      
      config.controlOrder.html(Mustache.render(config.listOrderTemplate, config));
      config.controlOrder.find('a').on('click', function() {
        var selected = jQuery(this);
        orderSetActive(selected, config);
        
        // Rebuild results list with new filter
        resultsList.build(
          false,
          orderKeyName(jQuery(this).attr('data-key')),
          selected.attr('data-sort')
        );
      });
      
      orderSetActive(config.controlOrder.find('a:eq(' + activeIndex + ')'), config);
    },
    
    /**
     * Make one list order link appear selected.
     * 
     * @param {Number} index Index of selected link in set of order links
     * 
     * @param {Object} config Config object of
     *    graphite.blocks.navigation.resultsFilter.
     */
    orderSetActive: function(index, config) {
      orderSetActive(config.controlOrder.find('a:eq(' + index + ')'), config);
    },

    /**
     * Create page size links
     * 
     * @param {Object} resultsList Instance of
     *    graphite.blocks.navigation.resultsFilter.list.
     * 
     * @param {Object} config Config object of
     *    graphite.blocks.navigation.resultsFilter.
     */
    pageSize: function(resultsList, config) {
      var activeIndex = 0;
      config.controlBatch.find('a[data-size]').each(function(index) {
        if  (jQuery(this).hasClass('gp_active') == true) {
          activeIndex = index;
        }
      });

      config.controlBatch.html(Mustache.render(config.listBatchTemplate, config));
      config.controlBatch.find('a').on('click', function() {
        var selected = jQuery(this);
        pageSizeSetActive(selected, config);

        // Rebuild results list with new filter
        resultsList.build(false, false, false, parseInt(selected.attr('data-size')));
      });
      
      pageSizeSetActive(config.controlBatch.find('a:eq(' + activeIndex + ')'), config);
    },

    /**
     * Make one page size link appear selected.
     * 
     * @param {Number} index Index of selected link in set of paging size links.
     * 
     * @param {Object} config Config object of
     *    graphite.blocks.navigation.resultsFilter.
     */
    pageSizeSetActive: function(index, config) {
      pageSizeSetActive(config.controlBatch.find('a:eq(' + index + ')'), config);
    },
    
    /**
     * Return the text after the last dot of a string. That way you get the last
     * key of a JSON selector Example: function('root.trunk.leaf') will return
     * the key 'leaf'.
     * 
     * @param {String} str The JSON selector as string.
     */
    orderKeyName: function(str) {
      return orderKeyName(str);
    }
  }
})();






/**
 * @namespace Pager manager for the results list
 * 
 * @param {Object} pagerContainer The element (as jQuery object) where the
 *    pager will be created in.
 * 
 * @param {Object} labels Object with text labels.
 * 
 * @param {String} config.labels.first Pager label.
 * @param {String} config.labels.last Pager label.
 * @param {String} config.labels.previous Pager label.
 * @param {String} config.labels.next Pager label.
 */
graphite.blocks.navigation.resultsFilter.pager = function(pagerContainer, labels) {
  var _resultsList = {};
  var _index = 0;
  var _total = 0;
  var _pageSize = 2;
  var visibleRange = 2;
  var _class = {
   first: 'gp_local_first', 
   last: 'gp_local_last',
   previous: 'gp_local_previous', 
   next: 'gp_local_next',
   number: 'gp_local_number' 
  }
  var _labels = {
    first: 'first',
    last: 'last',
    previous: 'previous',
    next: 'next',
    totalSingular: 'item',
    totalPlural: 'items'
  };
  jQuery.extend(_labels, labels);
  var _pagerContainer = pagerContainer;
  
  // Generate the HTML for the pager and set the events afterwards.
  function buildPager() {
    if (maxPage() == 1) {
      _pagerContainer.html('');
      return; 
    }
    
    var html = '';
    var backHtml = '';
    var forwardHtml = '';
    var hrefVoid = 'href="javascript:void(0);"'
    
    var firstClass = '';
    var lastClass = '';
    if (_index == 0) {
      firstClass = ' gp_local_disabled';
    }
    if (_index >= maxPage() - 1) {
      lastClass = ' gp_local_disabled';
    }
    var numbersData = numbers(hrefVoid);
    html = numbersData.html;
    backHtml += '<li class="' + _class.previous + firstClass + '"><a ' + hrefVoid + '><span></span>' + _labels.previous + '</a>\n';
    backHtml += '<li class="' + _class.first + firstClass + '"><a ' + hrefVoid + '><span>' + _labels.first + '</span></a>\n';
    if (numbersData.start > 0) {
      backHtml += '<li class="fontys_pager_ellipsis">...</li>\n';
    }
    if (numbersData.end < maxPage()) {
      forwardHtml += '<li class="fontys_pager_ellipsis">...</li>\n';
    }
    forwardHtml += '<li class="' + _class.last + lastClass + '"><a ' + hrefVoid + '><span>' + _labels.last + '</span></a>\n';
    forwardHtml += '<li class="' + _class.next + lastClass + '"><a ' + hrefVoid + '>' + _labels.next + '<span></span></a>\n';
    html = '<ul>' + backHtml + html + forwardHtml + '</ul>';
    
    _pagerContainer.html(html);
    setEvents();
  }

  // Set events for the pager.  
  function setEvents() {
    _pagerContainer.find('.' + _class.first + ' a').click(function() {
      if (jQuery(this).closest('.gp_local_disabled').size() == 0) {
        _resultsList.build(false, false, false, false, 0);
        _pagerContainer.find('.' + _class.first + ' a').focus();
      }
    });
    _pagerContainer.find('.' + _class.last + ' a').click(function() {
      if (jQuery(this).closest('.gp_local_disabled').size() == 0) {
        _resultsList.build(false, false, false, false, Math.ceil(_total / _pageSize) - 1);
        _pagerContainer.find('.' + _class.last + ' a').focus();
      }
    });
    _pagerContainer.find('.' + _class.previous + ' a').click(function() {
      if (jQuery(this).closest('.gp_local_disabled').size() == 0) {
        _resultsList.build(false, false, false, false, _index - 1);
        _pagerContainer.find('.' + _class.previous + ' a').focus();
      }
    });
    _pagerContainer.find('.' + _class.next + ' a').click(function() {
      if (jQuery(this).closest('.gp_local_disabled').size() == 0) {
        _resultsList.build(false, false, false, false, _index + 1);
        _pagerContainer.find('.' + _class.next + ' a').focus();
      }
    });
    _pagerContainer.find('.' + _class.number + ' a').click(function() {
      var index = parseInt(jQuery(this).html()) - 1;
      _resultsList.build(false, false, false, false, index);
      var newLink = _pagerContainer.find('a[data-index="' + index + '"]');
      newLink.focus();
    });
  }

  // Returns the maximum number of pages
  function maxPage() {
    return Math.ceil(_total/_pageSize);
  }

  // Generates the HTML for the numbers in the paging widget. It returns both
  // the HTML as well as the start and end index.  
  function numbers(hrefVoid) {
    var html = '';
    var start = _index - visibleRange;
    if (start < 0) {
      start = 0;
    }
    var end = start + (visibleRange * 2) + 1;
    if (end >= maxPage()) {
      end = maxPage();
      if (end - start < (visibleRange * 2) + 1) {
        start = end - ((visibleRange * 2) + 1);
      }
      if (start < 0) {
        start = 0;
      }
    }
    for (i = start; i < end; i++) {
      html += '<li class="' + _class.number + '"><a ' + hrefVoid;
      if (i == _index) {
        html += ' class="gp_active"';
      }
      html += ' data-index="' + i + '">' + (i + 1) + '</a></li>\n';
    }
    return {
      html: html,
      start: start,
      end: end
    }
  }
  
  
  /**
   * The page command
   * 
   * @param {Number} index The page index.
   * 
   * @param {Number} total Result itmes count.
   * 
   * @param {Number} pageSize Sets the size of the batch size for paging.
   * 
   * @param {Number} relative Defines if the index value should considered
   *    absolute (the provided argument is the new index) OR relative (the
   *    provided argument is a modifier for the current index).
   */
  this.page = function(index, total, pageSize, relative) {
    if (relative == true) {
      _index += index;
    } else {
      _index = index;
    }
    if (pageSize) {
      _pageSize = pageSize;
    }
    _total = total;
    
    buildPager();
  }
  
  /**
   * The passed list instance (graphite.blocks.navigation.resultsFilter.list)
   *    will be stored in the pager object.  
   */
  this.addListBuilder = function(resultsList) {
    if (resultsList) {
      _resultsList = resultsList;
    }
  }
}








