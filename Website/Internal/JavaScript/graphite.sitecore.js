/**
 * @fileOverview graphite.sitecore code
 *
 * @since 1.0 - 2012-01-31
 * @version 1.0 - 2012-01-31
 */







/**
 * @namespace Root namespace for graphite.sitecore
 */
graphite.sitecore = (function() {
  /* Start public */
  return {
    /**
     * Detects if a Sitecore page is in edit mode and adds a class to html tag
     *    if true.
     *
     * @since 1.0 - 2012-01-31
     * @version 1.0 - 2012-01-31
     * @example
        graphite.sitecore.checkEditMode();
     */
    checkEditMode: function() {
      if (document.location.href.indexOf('sc_mode=edit') > 0) {
        var tag = document.getElementByTagName('html')
        if (tag.length > 0) {
          tag[0].className += ' gp_sitecore_pageEditMode';
        }
      }
    }
  };
  /* End public */
})();
