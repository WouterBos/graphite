/**
 * @fileOverview Graphite blocks.widgets.accordion code
 * @author Wouter Bos, Web developer at Estate Internet (www.estate.nl).
 *
 * @since 0.1 - 2011-11-10
 * @version 0.1 - 2011-11-10
 */







/**
 * @namespace Root namespace for Graphite blocks.widgets.accordion
 */
graphite.blocks.widgets.accordion = function(config) {
  var _root;
  var _config = {
    open: 750,
    close: 500,
    index: 0,
    allowAllClosed: false
  }
  jQuery.extend(_config, config)
  
  this.init = function(selector) {
    _root = jQuery(selector);
    _root.addClass('gp_accordionJavaScript');
    
    _root.find('dt:eq(' + _config.index + ')').addClass("gp_active");
    _root.find('dd').css('width', _root.find('dd').outerWidth() + 'px');
    _root.find('dd').hide();
    _root.find('dd:eq(' + _config.index + ')').show();
    
    _root.find('dt').click(function() {
      var dtClicked = jQuery(this);
      var ddClicked = dtClicked.next("dd");
      var isOpened = ddClicked.is(":visible");
      _root.find('dd').not(ddClicked).slideUp(_config.close);
      _root.find('dt').not(dtClicked).removeClass("gp_active");
      if (isOpened == true) {
        if (_config.allowAllClosed == true) {
          dtClicked.next("dd").slideUp(_config.close);
        }
      } else {
        dtClicked.addClass("gp_active");
        dtClicked.next("dd").slideDown(_config.open);
      }
    })
  }
};
