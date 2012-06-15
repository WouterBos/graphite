/**
Simple Cookie Prompt
Idea: @panda_doodle - Coded: @michaelw90 - Edited by Wouter Bos
**/
var cPrompt = {
  stylesheet: '/internal/css/cPrompt.css',
  labels: {
    accept: 'Accepteer',
    decline: 'Weiger'
  },
  cookie: false,
  n: 3,
  hideOnAccept: true,
  minimisePrompt: false,
  prompts: [],
  bar: null,
  barSpacer: null,

  init: function (labels) {
    if (labels) {
      if (labels.accept) {
        this.labels.accept = labels.accept;
      }
      if (labels.decline) {
        this.labels.decline = labels.decline;
      }
    }
    var defaultAlert = '<span class="cPrompt_message">Deze website gebruikt cookies voor functionaliteit en statistieken.</span> <span class="cPrompt_buttons"><a class="cPrompt_button" href="javascript:void(0);" onclick="cPrompt.doClick(1);">' + this.labels.accept + '</a> <a class="cPrompt_button cPrompt_buttonSmall" href="javascript:void(0);" onclick="cPrompt.doClick(0)">' + this.labels.decline + '</a></span>';

    this.prompts = [
      // 0: The user has actively opted out of all cookies on the site
      ['', '<span class="cPrompt_message">Cookies zijn op deze website uitgeschakeld.</span> <span class="cPrompt_buttons"><a class="cPrompt_button" href="javascript:void(0);" onclick="cPrompt.doClick(1);">' + this.labels.accept + '</a></span>'],
  
      // 1: The user has seen a warning about cookies, but neither accepted nor declined, this is classed as inferred acception.
      ['', defaultAlert],
  
      // 2: The user has accepted all cookies to the site.
      ['', '<span class="cPrompt_message">U hebt het gebruik van cookies toegestaan.</span> <span class="cPrompt_buttons"><a class="cPrompt_button cPrompt_buttonSmall" href="javascript: void(0);" onclick="cPrompt.doClick(0);">' + this.labels.decline + '</a></span>'],
  
      // 3: The user's first visit to the site, no cookies accepted or declined.
      ['', defaultAlert]
    ];
    var cookie = this.checkCookie();
  
    if ((cookie == 2 && !this.hideOnAccept) || ((cookie == 2 || cookie == 0) && document.location.hash.indexOf('cPrompt=set') > 0) || (cookie != 2 && cookie != 0)) {
      this.loadPrompt(cookie);
      this.bar = document.getElementById('cPrompt_bar');
      this.barSpacer = document.getElementById('cPrompt_barSpacer');
      if (navigator.userAgent.indexOf('MSIE') > 0 && navigator.userAgent && document.compatMode == 'BackCompat') {
        this.bar.className += ' ieQuirks';
      }
      if (document.cookie.match(/cPrompt_hide=/) || this.minimisePrompt) {
        this.hidePrompt(null);
      }
    }
  },

  // Get cookie value
  checkCookie: function () {
    if (this.cookie === false) {
      if (!document.cookie.match(/cPrompt_useCookies=/)) {
        this.cookie = 3;
      } else if (document.cookie.match(/cPrompt_useCookies=(\d)($|;)/)) {
        this.cookie = parseInt(RegExp.$1);
      }
    }
    return this.cookie;
  },

  // Check if user allowed use of tracking cookies
  cookiesAllowed: function () {
    if (this.checkCookie() == 2) {
      return true;
    } else {
      return false;
    }
  },

  // Show dialogue
  loadPrompt: function (n) {
    if (n == 3) {
      this.saveCookie('useCookies', 1);
    }
    if (n > 3 || n < 0) {
      if (window.console) {
        console.log('Error: Undefined num (' + n + ')');
      }
    } else {
      this.n = n;
      var bar = document.createElement('div');
      var barSpacer = document.createElement('div');
      var pageBody = document.body;
      
      bar.innerHTML = this.prompts[n][1];
      bar.style.cssText = this.prompts[n][0];
      bar.className = 'cPrompt';
      bar.id = 'cPrompt_bar';
      pageBody.insertBefore(bar, pageBody.firstChild);
      
      barSpacer.id = 'cPrompt_barSpacer';
      pageBody.insertBefore(barSpacer, pageBody.firstChild);
      
      setHeight();
      this.loadCss(setHeight);
    }

    function setHeight() {
      barSpacer.style.height = bar.scrollHeight + 'px';
    }
  },

  // Store preference
  saveCookie: function (key, value) {
    document.cookie = 'cPrompt_' + key + '=' + value + '; path=/; expires=' + (new Date()).toGMTString().replace(/\d{4}/, '2050');
  },

  // Shows and hides prompt
  togglePrompt: function (e) {
    if (this.bar.className.indexOf('cPrompt_hide') == 0) {
      this.bar.className += ' cPrompt_hide';
      this.barSpacer.className += ' cPrompt_hide';
    } else {
      this.bar.className = this.bar.className.replace(/cPrompt_hide/g, '');
      this.barSpacer.className = ' cPrompt_hide';
    }
  },
  
  reshow: function () {
    this.bar.style.cssText = this.prompts[this.n][0];
    this.bar.innerHTML = this.prompts[this.n][1]; // +'<a class="cPrompt_button" onclick="cPrompt.hidePrompt(event)">X</a>';
  },

  doClick: function (type) {
    this.saveCookie('useCookies', (type == 0 ? 0 : 2));
    this.bar.style.display = 'none';
    var href = document.location.href;
    href = href.substr(0, href.indexOf('#'));
    if (type == 1 || type == 0) {
      href += '#cPrompt=set';
    }
    document.location.href = href;
    document.location.reload(true);
  },

  allowCookies: function () {
    return (cPrompt.checkCookie() == 1 || cPrompt.checkCookie() == 2);
  },

  loadCss: function (callback) {
    if (document.createStyleSheet) {
      document.createStyleSheet(this.stylesheet);
    }
    else {
      var newSS = document.createElement('link');
      newSS.rel = 'stylesheet';
      newSS.href = this.stylesheet;
      document.getElementsByTagName('head')[0].appendChild(newSS);
      newSS.onload = callback;
    }
  }
}

/*
if (document.addEventListener) {
  document.addEventListener('DOMContentLoaded', function () {
    cPrompt.init();
  }, false);
} else if (document.attachEvent) {
  document.attachEvent('onreadystatechange', function () {
    if (document.readyState === 'complete') {
      cPrompt.init();
    }
  });
};*/