/**
Simple Cookie Prompt
Idea: @panda_doodle - Coded: @michaelw90 - Edited by Wouter Bos
**/
var cPrompt = {
    stylesheet: '/internal/css/cPrompt.css',
    cookie: false,
    n: 3,
    hideOnAccept: true,
    minimisePrompt: false,
    cookieLink: '',
    prompts: [],
    p: null,
    p2: null,
    defaultAlert: '<span class="cPrompt_message">Deze website gebruikt cookies voor functionaliteit en statistieken.</span> <span class="cPrompt_buttons"><a class="cPrompt_button" href="javascript:void(0);" onclick="cPrompt.doClick(1);">Accepteer</a> <a class="cPrompt_button"href="javascript:void(0);" class="cPrompt_Close" onclick="cPrompt.doClick(0)">Weiger</a></span>',

    init: function () {
        this.prompts = [
        // 0: The user has actively opted out of all cookies on the site
			['', '<span class="cPrompt_message">Cookies zijn op deze website uitgeschakeld.</span> <span class="cPrompt_buttons"><a class="cPrompt_button" href="javascript:void(0);" onclick="cPrompt.doClick(1);">Accepteer</a></span>'],

        // 1: The user has seen a warning about cookies, but neither accepted nor declined, this is classed as inferred acception.
			['', this.defaultAlert],

        // 2: The user has accepted all cookies to the site.
			['', '<span class="cPrompt_message">U hebt het gebruik van cookies toegestaan.</span> <span class="cPrompt_buttons"><a class="cPrompt_button"href="javascript: void(0);" onclick="cPrompt.doClick(0);">Weiger</a></span>'],

        // 3: The user's first visit to the site, no cookies accepted or declined.
			['', this.defaultAlert]
		];
        var cookie = this.checkCookie();

        //console.log((cookie == 2 && !this.hideOnAccept), ((cookie == 2 || cookie == 0) && document.location.hash.indexOf('cPrompt=set') > 0), (cookie != 2 && cookie != 0))

        if ((cookie == 2 && !this.hideOnAccept) || ((cookie == 2 || cookie == 0) && document.location.hash.indexOf('cPrompt=set') > 0) || (cookie != 2 && cookie != 0)) {
            this.loadPrompt(cookie);
            this.p = document.getElementById('cPrompt_bar');
            this.p2 = document.getElementById('cPrompt_barSpacer');
            if (navigator.userAgent.indexOf('MSIE') > 0 && navigator.userAgent && document.compatMode == 'BackCompat') {
                this.p.className += ' ieQuirks';
            }
            if (document.cookie.match(/cPrompt_hide=/) || this.minimisePrompt) {
                this.hidePrompt(null);
            }
        }

        //console.log(cookie);
    },

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

    cookiesAllowed: function () {
        if (this.checkCookie() == 2) {
            return true;
        } else {
            return false;
        }
    },

    loadPrompt: function (n) {
        if (n == 3) {
            this.saveCookie('useCookies', 1);
        }
        if (n > 3 || n < 0) {
            if (window.console) {
                console.log('Error: Undefined num (' + n + ')');
            }
        } else {
            this.loadCss();
            this.n = n;
            var h = document.createElement('div');
            var b = document.body;
            with (h) {
                innerHTML = this.prompts[n][1];
                style.cssText = this.prompts[n][0];
                className = 'cPrompt';
                id = 'cPrompt_bar';
            }
            b.insertBefore(h, b.firstChild);

            var h2 = document.createElement('div');
            h2.id = 'cPrompt_barSpacer';
            h2.style.height = h.scrollHeight + 'px';
            setTimeout(function () { h2.style.height = h.scrollHeight + 'px'; }, 500);
            b.insertBefore(h2, b.firstChild);
        }
    },

    saveCookie: function (c, v) {
        document.cookie = 'cPrompt_' + c + '=' + v + '; path=/; expires=' + (new Date()).toGMTString().replace(/\d{4}/, '2050');
    },

    hidePrompt: function (e) {
        if (e != null) {
            this.saveCookie('hide', 1);
            e.stopPropagation();
        }
        var h = this.p;
        with (h) {
            style.cssText = this.prompts[this.n][0];
            innerHTML = '';
            h.onclick = function () {
                cPrompt.reshow();
            }
        }
        document.body.appendChild(h);
    },

    reshow: function () {
        with (this.p) {
            style.cssText = this.prompts[this.n][0];
            innerHTML = this.prompts[this.n][1]; // +'<a class="cPrompt_button"class="cPrompt_Close" onclick="cPrompt.hidePrompt(event)">X</a>';
        }
    },

    doClick: function (type) {
        this.saveCookie('useCookies', (type == 0 ? 0 : 2));
        this.p.style.display = 'none';
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

    loadCss: function () {
        if (document.createStyleSheet) {
            document.createStyleSheet(this.stylesheet);
        }
        else {
            var newSS = document.createElement('link');
            newSS.rel = 'stylesheet';
            newSS.href = this.stylesheet;
            document.getElementsByTagName('head')[0].appendChild(newSS);
        }
    }
}

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
};