/**
 * @fileOverview Float Box Fixer. Adds a CSS class to each floating element
 *    that starts a new line.
 * @since 1 - 2012-2-8
 */





(function($){
  jQuery.fn.floatBoxFixer = function(arg_config) {
    var currentOffset;
    var config = {
      fixClass: 'newLineFloat'
    }
    config = $.extend(true, config, arg_config);
    
    
    
    return this.each(function() {
      var item = $(this);
      if (typeof(currentOffset) == 'undefined' ||
          currentOffset < item.offset().top) {
        item.addClass(config.fixClass);
      }
      currentOffset = item.offset().top;
    });
  }
})(jQuery);