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
graphite.blocks.widgets.accordion = function() {
  var _root;
  var _speed = {
    open: 500,
    close: 250
  }
  
  this.init = function(selector) {
    _root = jQuery(selector);
    _root.addClass('gp_accordionJavaScript');
    
    _root.find('dt:eq(0)').addClass("gp_active");
    _root.find('dd').css('width', _root.find('dd').innerWidth() + 'px');
    _root.find('dd:gt(0)').hide();
    
    _root.find('dt').click(function() {
      var dtClicked = jQuery(this);
      var isOpened = dtClicked.next("dd").is(":visible");
      _root.find('dd').slideUp(_speed.close);
      _root.find('dt').removeClass("gp_active");
      if (isOpened == true) {
        dtClicked.next("dd").slideUp(_speed.close);
      } else {
        dtClicked.addClass("gp_active");
        dtClicked.next("dd").slideDown(_speed.open);
      }
    })
  }
};
