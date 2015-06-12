//http://www.fwolf.com/tools/2009/js_packer_js.php
function EyouSoftSSO() {
    var sso = this;
    var _check = null;
    var actionUrl = null;
    var redirectUrl = null;
    var times = 20;
    this.addLogin = function(_url, _redirectUrl) {
        if (_url != null)
            actionUrl = new Array();
        for (var i = 0; i < _url.length; i++)
        { actionUrl.push(_url[i]); }
        redirectUrl = _redirectUrl;
    }
    this.customLogin = function(o) {
        var me = this;
        for (var i = 0; i < actionUrl.length; i++) {
            var d = document.getElementsByTagName("head")[0]
            var loginFrame = document.createElement("script");
            loginFrame.src = actionUrl[i];
            loginFrame.type = "text/javascript";
            loginFrame.charset = "utf-8";
            d.appendChild(loginFrame);
        }
        // _check = setInterval(function() { me.t() }, 500);

    }
    this.t = function() {
        if (times == 0) {
            document.write("login bad");
        }
        else {
            if (sso.fnLoadOk()) {
                clearInterval(_check);
                setTimeout("location.replace('" + redirectUrl + "')", 500)
            }
            times--;
        }
    }
    this.getIframeControlValue = function(iframeId) {
        var ie = document.getElementsByTagName("iframe")[iframeId].readyState;
        if (ie == "complete") {
            ie = document.getElementsByTagName("iframe")[iframeId].innerText;
        }
        return ie;
    }
    this.fnLoadOk = function() {
        var b = true;
        for (var i = 0; i < actionUrl.length; i++) {
            if (sso.getIframeControlValue(i) == "ok") {
                b = b && true;
            } else {
                b = b && false;
            }
        }
        return b;
    }
    
}
function SSOController() {
    var j = this;
    var k = null;
    var l = 1800;
    var m = 86400;
    var n = null;
    var o = null;
    var p = 3; //监听间隔秒
    var q = null;
    var r = "http://www.ty.com/sso/crossdomain.php";
    var s = "http://www.ty.com/sso/login.php";
    var t = "http://www.ty.com/sso/logout.php";
    var u = "http://www.ty.com/sso/updatetgt.php";
    var v = "http://www.ty.com/sso/gettime.php";
    var w = null;
    var x = "";
    var y = 1;
    var z = 2;
    this.https = 1;
    this.wsse = 2;
    this.name = "sinaSSOController";
    this.loginFormId = "ssoLoginForm";
    this.scriptId = "ssoLoginScript";
    this.ssoCrossDomainScriptId = "ssoCrossDomainScriptId";
    this.loginFrameName = "ssoLoginFrame";
    this.appLoginURL = { '51uc.com': "http://www.ty.com/sso/login.php" };
    this.setDomain = false;
    this.feedBackUrl = "";
    this.service = "sso";
    this.domain = "ty.com";
    this.from = "";
    this.pageCharset = "GB2312";
    this.useTicket = false;
    this.isCheckLoginState = false;
    this.isUpdateCookieOnLoad = true;
    this.useIframe = true;
    this.noActiveTime = 7200;
    this.autoUpdateCookieTime = 1800;
    this.loginType = 0;
    this.timeoutEnable = false;
    this.crossDomain = true;
    this.scriptLoginHttps = true;
    this.getVersion = function() { return "ssologin.js(v1.3.8) 2010-04-21"; };
    this.getEntry = function() { return j.entry; };
    this.getClientType = function() { return j.getVersion().split(" ")[0]; };
    this.init = function() { var a = window.sinaSSOConfig; if (typeof a != "object") { a = {}; } var b; for (b in a) { j[b] = a[b]; } if (!j.entry) { j.entry = j.service; } if (j.isUpdateCookieOnLoad) { setTimeout(j.name + ".updateCookie()", 10000); } if (j.isCheckLoginState) { C(window, "load", function() { j.checkLoginState(); }); } j.customInit(); };
    this.customInit = function() { };
    this.customUpdateCookieCallBack = function(a) { };
    this.customLogoutCallBack = function(a) { j.customLoginCallBack({ result: false }); };
    this.customLoginCallBack = function(a) { };
    this.login = function(b, c, d) { D.start("login", 5000, function() { D.clear("login"); j.customLoginCallBack({ result: false, reason: unescape("%u767B%u5F55%u8D85%u65F6%uFF0C%u8BF7%u91CD%u8BD5") }); }); d = d == undefined ? 0 : d; var e = function() { if (j.useIframe && (j.setDomain || j.feedBackUrl)) { if (j.setDomain) { document.domain = j.domain; if (!j.feedBackUrl && j.domain != "sina.com.cn") { j.feedBackUrl = M(j.appLoginURL[j.domain], { domain: 1 }); } } x = "post"; var a = G(b, c, d); if (!a) { x = "get"; if (j.scriptLoginHttps) { j.loginType = j.loginType | y; } F(b, c, d); } } else { x = "get"; F(b, c, d); } }; if (j.loginType & z) { var f = M(v, { entry: j.entry, callback: j.name + ".getTimeCallBack" }); j.getTimeCallBack = function(a) { if (a && a.retcode == 0) { j.servertime = a.time; } e(); }; E(j.scriptId, f); } else { e(); } return true; };
    this.logout = function() { try { var a = { entry: j.getEntry(), callback: j.name + ".ssoLogoutCallBack" }; var b = M(t, a); E(j.scriptId, b); } catch (e) { } return true; };
    this.ssoLogoutCallBack = function(a) { if (a.arrURL) { j.setCrossDomainUrlList(a); } j.crossDomainAction("logout", function() { j.customLogoutCallBack({ result: true }); }); };
    this.updateCookie = function() { try { if (j.autoUpdateCookieTime > 5) { if (k != null) { clearTimeout(k); } k = setTimeout(j.name + ".updateCookie()", j.autoUpdateCookieTime * 1000); } var a = j.getCookieExpireTime(); var b = (new Date).getTime() / 1000; var c = {}; if (a == null) { c = { retcode: 6102 }; } else if (a < b) { c = { retcode: 6203 }; } else if (a - m + l > b) { c = { retcode: 6110 }; } else if (a - b > j.noActiveTime) { c = { retcode: 6111 }; } if (c.retcode !== undefined) { j.customUpdateCookieCallBack(c); return false; } var d = M(u, { entry: j.getEntry(), callback: j.name + ".updateCookieCallBack" }); E(j.scriptId, d); } catch (e) { } return true; };
    this.setCrossDomainUrlList = function(a) { w = a; };
    this.callFeedBackUrl = function(a) { try { var b = { callback: j.name + ".feedBackUrlCallBack" }; if (a.ticket) { b.ticket = a.ticket; } if (a.retcode !== undefined) { b.retcode = a.retcode; } var c = M(j.feedBackUrl, b); E(j.scriptId, c); } catch (e) { } return true; };
    this.loginCallBack = function(a) { try { if (j.timeoutEnable && !D.isset("login")) { return; } D.clear("login"); var b = {}; var c = a.ticket; var d = a.uid; if (d) { b.result = true; b.retcode = 0; b.userinfo = { uniqueid: a.uid }; if (c) { b.ticket = c; } if (j.feedBackUrl) { if (j.crossDomain) { j.crossDomainAction("login", function() { j.callFeedBackUrl(b); }); } else { j.callFeedBackUrl(b); } } else { if (j.crossDomain) { j.crossDomainAction("login", function() { j.customLoginCallBack(b); }); } else { j.customLoginCallBack(b); } } } else { b.result = false; b.errno = a.retcode; b.reason = a.reason; j.customLoginCallBack(b); } } catch (e) { } return true; };
    this.updateCookieCallBack = function(a) { if (a.retcode == 0) { j.crossDomainAction("update", function() { j.customUpdateCookieCallBack(a); }); } else { j.customUpdateCookieCallBack(a); } };
    this.feedBackUrlCallBack = function(a) { if (x == "post" && j.timeoutEnable && !D.isset("login")) { return; } D.clear("login"); j.customLoginCallBack(a); I(j.loginFrameName); };
    this.doCrossDomainCallBack = function(a) { j.crossDomainCounter++; var b = j.$(a.scriptId); b.parentNode.removeChild(b); if (j.crossDomainCounter == j.crossDomainCount) { clearTimeout(o); j.crossDomainResult(); } };
    this.crossDomainCallBack = function(a) { var b = j.$(j.ssoCrossDomainScriptId); if (b) { b.parentNode.removeChild(b); } if (!a || a.retcode != 0) { return false; } var c = a.arrURL; var d, scriptId; var e = { callback: j.name + ".doCrossDomainCallBack" }; j.crossDomainCount = c.length; j.crossDomainCounter = 0; if (c.length == 0) { clearTimeout(o); j.crossDomainResult(); return true; } for (var i = 0; i < c.length; i++) { d = c[i]; scriptId = "ssoscript" + i; e.scriptId = scriptId; d = M(d, e); E(scriptId, d); } };
    this.crossDomainResult = function() { w = null; if (typeof n == "function") { n(); } };
    this.crossDomainAction = function(a, b) { o = setTimeout(j.name + ".crossDomainResult()", p * 1000); if (typeof b == "function") { n = b; } else { n = null; } if (w) { j.crossDomainCallBack(w); return false; } var c = j.domain; if (a == "update") { a = "login"; c = "sina.com.cn"; } var d = { scriptId: j.ssoCrossDomainScriptId, callback: j.name + ".crossDomainCallBack", action: a, domain: c }; var e = M(r, d); E(j.ssoCrossDomainScriptId, e); };
    this.checkLoginState = function(d) { if (d) { j.autoLogin(d); } else { j.autoLogin(function(a) { var b = {}; if (a !== null) { var c = { displayname: a.nick, uniqueid: a.uid, userid: a.user }; b.result = true; b.userinfo = c; } else { b.result = false; b.reason = ""; } j.customLoginCallBack(b); }); } };
    this.getCookieExpireTime = function() { return B(j.domain); };
    this.getSinaCookie = function(a) { var b = P(L("SUP")); if (!b && !P(L("ALF"))) { return null; } if (!b) { b = P(L("SUR")); } if (!b) { return null; } var c = R(b); if (a && c.et && (c.et * 1000 < (new Date).getTime())) { return null; } return c; };
    this.get51UCCookie = function() { return j.getSinaCookie(); };
    this.autoLogin = function(a) { if (j.domain == "sina.com.cn") { if (L("SUP") === null && L("ALF") !== null) { A(a); return true; } } else { if (L("SUP") === null && (L("SSOLoginState") !== null || L("ALF") !== null)) { A(a); return true; } } a(j.getSinaCookie()); return true; };
    this.autoLoginCallBack2 = function(a) { try { q(j.getSinaCookie()); } catch (e) { } return true; };
    this.autoLoginCallBack3 = function(a) { if (a.retcode != 0) { j.autoLoginCallBack2(a); return false; } var b = { callback: j.name + ".autoLoginCallBack2", retcode: a.retcode, ticket: a.ticket }; var c = j.appLoginURL[j.domain]; var d = M(c, b); E(j.scriptId, d, "gb2312"); return true; };
    var A = function(a) { q = a; var b = { entry: j.getEntry(), service: j.service, encoding: "UTF-8", gateway: 1, returntype: "TEXT", from: j.from }; if (j.domain == "sina.com.cn") { b.callback = j.name + ".autoLoginCallBack2"; b.useticket = 0; } else { b.callback = j.name + ".autoLoginCallBack3"; b.useticket = 1; } var c = M(s, b); E(j.scriptId, c, "gb2312"); return true; };
    var B = function(a) { var b = null; var c = null; switch (a) { case "sina.com.cn": c = j.getSinaCookie(); if (c) { b = c.et; } break; case "51uc.com": c = j.getSinaCookie(); if (c) { b = c.et; } break; default: ; } return b; };
    var C = function(a, b, c) { if (a.addEventListener) { a.addEventListener(b, c, false); } else if (a.attachEvent) { a.attachEvent("on" + b, c); } else { a["on" + b] = c; } };
    var D = new function() { this.start = function(a, b, c) { if (j.timeoutEnable) { this[a] = setTimeout(c, b); } }; this.clear = function(a) { if (j.timeoutEnable) { clearTimeout(this[a]); this[a] = false; } }; this.isset = function(a) { return this[a]; }; };
    var E = function(a, b, c) { var d = document.getElementsByTagName("head")[0]; var e = document.getElementById(a); if (e) { d.removeChild(e); } var f = document.createElement("script"); if (c) { f.charset = c; } else { f.charset = "gb2312"; } f.id = a; f.type = "text/javascript"; f.src = M(b, { client: j.getClientType(), _: (new Date).getTime() }); d.appendChild(f); };
    var F = function(a, b, c) { var d = { entry: j.getEntry(), encoding: "UTF-8", gateway: 1, callback: j.name + ".loginCallBack", returntype: "TEXT", from: j.from, savestate: c, useticket: j.useTicket ? 1 : 0 }; d.username = a; if (j.service) { d.service = j.service; } if (j.loginType & z && j.servertime && sinaSSOEncoder && sinaSSOEncoder.hex_sha1) { d.servertime = j.servertime; d.pwencode = "wsse"; b = sinaSSOEncoder.hex_sha1("" + sinaSSOEncoder.hex_sha1(b) + j.servertime); j.servertime = false; } d.password = b; var e = (j.loginType & y) ? s.replace(/^http:/, "https:") : s; e = M(e, d); E(j.scriptId, e, "gb2312"); };
    var G = function(a, b, c) { H(j.loginFrameName); var d = J(j.loginFormId); if (j.service) { d.addInput("service", j.service); } d.addInput("client", j.getClientType()); d.addInput("entry", j.getEntry()); d.addInput("encoding", j.pageCharset); d.addInput("gateway", 1); d.addInput("savestate", c); d.addInput("from", j.from); d.addInput("useticket", j.useTicket ? 1 : 0); d.addInput("username", a); if (j.loginType & z && j.servertime && sinaSSOEncoder && sinaSSOEncoder.hex_sha1) { d.addInput("servertime", j.servertime); d.addInput("pwencode", "wsse"); b = sinaSSOEncoder.hex_sha1("" + sinaSSOEncoder.hex_sha1(b) + j.servertime); j.servertime = false; } d.addInput("password", b); if (j.crossDomain == false) { d.addInput("crossdomain", 0); } if (j.feedBackUrl) { d.addInput("url", M(j.feedBackUrl, { framelogin: 1, callback: "parent." + j.name + ".feedBackUrlCallBack" })); d.addInput("returntype", "META"); } else { d.addInput("callback", "parent." + j.name + ".loginCallBack"); d.addInput("returntype", "IFRAME"); var f = 0; if (j.setDomain) { f = 1; } d.addInput("setdomain", f); } var g = (j.loginType & y) ? s.replace(/^http:/, "https:") : s; g = M(g, { client: j.getClientType() }); d.method = "post"; d.action = g; d.target = j.loginFrameName; var h = true; try { d.submit(); } catch (e) { I(j.loginFrameName); h = false; } d.parentNode.removeChild(d); return h; };
    var H = function(a, b) { if (b == null) { b = "javascript:false;"; } var c = j.$(a); if (c) { c.parentNode.removeChild(c); } c = document.createElement("iframe"); c.height = 0; c.width = 0; c.style.display = "none"; c.name = a; c.id = a; c.src = b; K(document.body, c); window.frames[a].name = a; return c; };
    var I = function(a) { var b = j.$(a); if (b) { b.parentNode.removeChild(b); } };
    var J = function(e, f) { if (f == null) { f = "none"; } var g = j.$(e); if (g) { g.parentNode.removeChild(g); } g = document.createElement("form"); g.height = 0; g.width = 0; g.style.display = f; g.name = e; g.id = e; K(document.body, g); document.forms[e].name = e; g.addInput = function(a, b, c) { if (c == null) { c = "text"; } var d = this.getElementsByTagName("input")[a]; if (d) { this.removeChild(d); } d = document.createElement("input"); this.appendChild(d); d.id = a; d.name = a; d.type = c; d.value = b; }; return g; };
    var K = function(a, b) { a.appendChild(b); };
    var L = function(a) { var b = eval("/" + a + "=([^;]+)/").exec(document.cookie); return b == null ? null : b[1]; };
    var M = function(a, b) { return a + N(a) + Q(b); };
    var N = function(a) { return /\?/.test(a) ? "&" : "?"; };
    var O = function(a) { return encodeURIComponent(a); };
    var P = function(a) { if (a == undefined) { return ""; } var b = decodeURIComponent(a); return b == "null" ? "" : b; };
    var Q = function(a) { if (typeof a != "object") { return ""; } var b = new Array; for (var c in a) { if (typeof a[c] == "function") { continue; } b.push(c + "=" + O(a[c])); } return b.join("&"); };
    var R = function(a) { var b = a.split("&"); var c; var d = {}; for (var i = 0; i < b.length; i++) { c = b[i].split("="); d[c[0]] = P(c[1]); } return d; };
    this.$ = function(a) { return document.getElementById(a); };
}

var sinaSSOEncoder = sinaSSOEncoder || {};
(function() { var n = 0; var o = 8; this.hex_sha1 = function(s) { return A(p(z(s), s.length * o)); }; var p = function(x, f) { x[f >> 5] |= 128 << 24 - f % 32; x[((f + 64 >> 9) << 4) + 15] = f; var w = Array(80); var a = 1732584193; var b = -271733879; var c = -1732584194; var d = 271733878; var e = -1009589776; for (var i = 0; i < x.length; i += 16) { var g = a; var h = b; var k = c; var l = d; var m = e; for (var j = 0; j < 80; j++) { if (j < 16) { w[j] = x[i + j]; } else { w[j] = v(w[j - 3] ^ w[j - 8] ^ w[j - 14] ^ w[j - 16], 1); } var t = u(u(v(a, 5), q(j, b, c, d)), u(u(e, w[j]), r(j))); e = d; d = c; c = v(b, 30); b = a; a = t; } a = u(a, g); b = u(b, h); c = u(c, k); d = u(d, l); e = u(e, m); } return Array(a, b, c, d, e); }; var q = function(t, b, c, d) { if (t < 20) { return (b & c) | (~b) & d; } if (t < 40) { return b ^ c ^ d; } if (t < 60) { return (b & c) | b & d | c & d; } return b ^ c ^ d; }; var r = function(t) { return (t < 20) ? 1518500249 : (t < 40) ? 1859775393 : (t < 60) ? -1894007588 : -899497514; }; var u = function(x, y) { var a = (x & 65535) + (y & 65535); var b = (x >> 16) + (y >> 16) + (a >> 16); return (b << 16) | a & 65535; }; var v = function(a, b) { return (a << b) | a >>> 32 - b; }; var z = function(a) { var b = Array(); var c = (1 << o) - 1; for (var i = 0; i < a.length * o; i += o) { b[i >> 5] |= (a.charCodeAt(i / o) & c) << 24 - i % 32; } return b; }; var A = function(a) { var b = n ? "0123456789ABCDEF" : "0123456789abcdef"; var c = ""; for (var i = 0; i < a.length * 4; i++) { c += b.charAt((a[i >> 2] >> (3 - i % 4) * 8 + 4) & 15) + b.charAt((a[i >> 2] >> (3 - i % 4) * 8) & 15); } return c; }; } .call(sinaSSOEncoder));
sinaSSOController = new SSOController;
sinaSSOController.init();