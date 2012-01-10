nl.kch.sitecore = (function() {
  /* START PUBLIC */
  return {
    DetectMode: function(selector) {
      if (document.location.href.indexOf('sc_mode=edit') > 0) {
        jQuery('body').addClass('sc_ModeEdit');
      }
    }
  }
  /* END PUBLIC */
})();
