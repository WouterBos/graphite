/**
 * @author wbos
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
      if (typeof(currentOffset) != 'undefined' &&
          currentOffset < item.offset().top) {
        item.addClass(config.fixClass);
      }
      currentOffset = item.offset().top;
    });
  }
})(jQuery);