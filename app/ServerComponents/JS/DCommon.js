var LoginUrl = "";
/*
datatype：支持json,text
*/
jQuery.newAjax = function(options, noneedcheck) {
    var dataType = "text";
    if (options.dataType) {
        dataType = options.dataType;
    }
    var isPostType = false;
    if (options.type) {
        if (options.type.toUpperCase() == "POST") {
            isPostType = true;
        }
    }
    if (isPostType) {
        if (options.url.indexOf('?') == -1) {
            options.url = options.url + "?urltype=tab"
        } else {
            options.url = options.url + "&urltype=tab"
        }
        if (noneedcheck == true) {
            options.url += "&RequestSource=noneedcheck";
        }
    } else {
        options.data = $.extend({ urltype: "tab" }, options.data);
        if (noneedcheck == true) {
            options.data = $.extend({ RequestSource: "noneedcheck" }, options.data);
        }
    }
    var orisucess;
    if (options.success) {
        orisucess = options.success;
    }
    options.success = function(result) {
        var isLogin = false, isCheck = false;
        if (dataType == "text") {
            if (result != "{Islogin:false}") {
                isLogin = true;
            }
            if (result != "{isCheck:false}") {
                isCheck = true;
            }
        } else {
            if (result.Islogin === undefined) {
                isLogin = true;
            }
            if (result.isCheck === undefined) {
                isCheck = true;
            }
        }
        if (isLogin === false) {
            alert('对不起你未登录，请登录！');
            window.location.href = LoginUrl + "?returnurl=" + encodeURIComponent(window.location.href);
            return false;
        } else if (isCheck === false) {
            alert("对不起，您还未通过审核，不能进行操作！");
            return false;
        } else {
            if (orisucess) {
                orisucess(result);
            }
        }
    };
    jQuery.ajax(options);
};
var ChildTab = {
    getParentUrl: function(childUrl) {
        if (childUrl == "") {
            return "";
        }
        var self = this, p, url = "", i, isFind = false, data;
        for (p in self) {
            if (p != "getParentUrl") {
                data = self[p];
                for (i = 0; i < data.urls.length; i++) {
                    if (childUrl == data.urls[i] || childUrl.indexOf(data.urls[i]) != -1) {
                        isFind = true;
                        url = data.url;
                        break;
                    }
                }
                if (isFind == true) {
                    break;
                }
            }
        }
        return url;
    },
    a: {
        url: "/systemset/systemindex.aspx",
        urls: ["/systemset/personinfoset.aspx", "/systemset/companyinfoset.aspx", "/systemset/departmanage.aspx", "/systemset/sonusermanage.aspx", "/systemset/permitmanage.aspx"]
    },
    b: {
        url: "/supplyinformation/addsupplyinfo.aspx",
        urls: ["/supplyinformation/addsupplyinfo.aspx", "/supplyinformation/allsupplymanage.aspx", "/supplyinformation/hassupplyfavorites.aspx"]
    },
    d: {
        url: "/userorder/ordersreceived.aspx",
        urls: ["/userorder/orderhistory.aspx", "/userorder/orderprocessed.aspx", "/userorder/ordersreceived.aspx"]
    },
    f: {
        url: "/tongyeinfo/infolist.aspx",
        urls: ["/tongyeinfo/infolist.aspx", "/tongyeinfo/infoshow.aspx"]
    },
    g: {
        url: "/teamservice/scatterplanc.aspx",
        urls: ["/order/orderyytour.aspx"]
    },
    h: {
        url: "/routeagency/scatteredfightplan.aspx",
        urls: ["/order/routeagency/addorderbyroute.aspx"]
    },
    i: {
        url: "/teamservice/scatterplanc.aspx",
        urls: ["/order/orderbytour.aspx"]
    },
    j: {
        url: "/teamservice/linelibrarylist.aspx",
        urls: ["/teamservice/singlegrouppre.aspx"]
    },
    k: {
        url: "/routeagency/routemanage/routeview.aspx",
        urls: ["/routeagency/routemanage/addtourism.aspx"]
    },
    l: { url: "/teamservice/fitorders.aspx", urls: ["/teamservice/fitorders.aspx"] },
    m: { url: "/teamservice/teamorders.aspx", urls: ["/teamservice/teamorders.aspx"] },
    n: { url: "/routeagency/routemanage/routeview.aspx", urls: ["/routeagency/routemanage/routeview.aspx"] },
    o: { url: "/routeagency/routemanage/rmdefault.aspx", urls: ["/routeagency/routemanage/rmdefault.aspx"] },
    p: { url: "/order/routeagency/neworders.aspx", urls: ["/order/routeagency/orderstateupdate.aspx"] }
};
var FormTab = {
    isFormUrl: function(url) {
        var self = this, p, i, isFind = false, data;
        for (p in self) {
            if (p != "isFormUrl") {
                var data = self[p];
                for (i = 0; i < data.urls.length; i++) {
                    if (url.indexOf(data.urls[i]) != -1) {
                        isFind = true;
                        break;
                    }
                }
                if (isFind == true) {
                    break;
                }
            }
        }
        return isFind;
    },
    //    a: {
    //        urls: ["/routeagency/routemanage/rmdefault.aspx"]
    //    },
    //    b: {
    //        urls: ["/routeagency/routemanage/rmdefault.aspx?RouteSource=2"]
    //    }, 
    c: {
        urls: ["/teamservice/singlegrouppre.aspx"]
    }
    //    d: {
    //        urls: ["/routeagency/routemanage/routeview.aspx"]
    //    }
};
function TopTab(option) {
    this.option = $.extend({
        ftabOnClass: "option-tab-on-index",
        ftabUnClass: "option-tab-un-index",
        tabOnClass: "option-tab-on",
        tabUnClass: "option-tab-un",
        panelOnClass: "option-panel-on",
        panelUnClass: "option-panel-un",
        tabTemplate: "<div id='#{id}' class='#{cssname}'><span title='#{name}'><a href='#{href}' tabtype='#{tabtype}'>#{name}</a></span><a href='javascript:;' class='menuclose' title='关闭'>&nbsp;</a></div>",
        panelTemplate: "<div id='#{id}' class='#{cssname}'></div>",
        tabContainerId: "optionTab",
        panelContainerId: "optionPanel",
        tabIdPrefix: "tab_",
        panelIdPrefiex: "panel_",
        maxLength: 8,
        onSelect: null,
        onAdd: null
    }, option || {});

    this.tabCloseCss = "menuclose";
    this.tabCtn = $("#" + this.option.tabContainerId);
    this.panelCtn = $("#" + this.option.panelContainerId);
    this._tabify();
    this.activeTabIndex = 0;

    var self = this;

    //bind event to FirstTab.
    //Click Event.
    this.tabs.eq(0).click(function() {
        var i = self.tabs.index($(this));
        self.select(i);
        return false;
    });

    //ie6 backgroundimage hack.
    if ($.browser.msie) {
        if (parseFloat($.browser.version) <= 6) {
            try {
                document.execCommand('BackgroundImageCache', false, true);
            } catch (e) {
            }
        }
    }
};
TopTab.prototype.open = function(url, label, option) {
    backPageTop();
    //bug for singlecalendar
    clearSingleCalendar();

    option = $.extend({
        isRefresh: true, /*如果url对应的选项卡已经存在，该属性指定是否刷新选项卡的内容*/
        tabType: "list", /*tabType:[form,list]*/
        data: {}, /*要额外发送的key/value对象*/
        desUrl: "", /*指定实际上要获取内容的URL*/
        isOpen: true /*无视参数，只判断URL是否存在,false 表示不打开新标签*/
    }, option || {});
    for (var key in option.data) {
        option.isRefresh = true;
        break;
    }
    this.option.data = option.data;
    var self = this;
    url = self._filterUrl(url);
    if (option.desUrl == "") {
        option.desUrl = url;
    }

    if (option.isOpen == false) {
        if (url.indexOf("?") >= 0) {
            url = url.substring(0, url.indexOf("?"));
        }
    }
    var tabId = self._generateTabId(url);
    var panelId = self._generatePanelId(url);

    if (document.getElementById(tabId) == null || document.getElementById(panelId) == null) {
        return self.add(url, label, option.desUrl);
    } else {
        var index = self.getIndexByUrl(url);
        if (index != undefined) {
            self.select(index, option.isRefresh, option.desUrl);
        }
    }
};
/*
Add a new tab.
*/
TopTab.prototype.add = function(url, label, desUrl) {
    var self = this, o = this.option, tabType;
    url = self._filterUrl(url);
    var isOverLoadCurrentTab = false;
    var tmpTabIndex;

    if (self.length() >= o.maxLength) {
        //alert("最多创建"+o.maxLength+"个选项标签");
        //return false;
        tmpTabIndex = self.activeTabIndex;
        if (tmpTabIndex == 0) {
            tmpTabIndex = 7;
        }
        isOverLoadCurrentTab = true;
        if (self.anchors.eq(tmpTabIndex).attr("tabtype") == "form") {
            var panel = self._getPanelByTabIndex(tmpTabIndex);
            var isM = CheckFormIsChange.isFormChanaged(panel.find("form")[0]);
            if (isM) {
                var c = confirm("------------------------------\n提示：当前的线路信息尚未保存。\n是否舍弃该线路信息？\n------------------------------");
                if (!c) {
                    return false;
                }
            }
        }
    }

    if (url.indexOf("/routeagency/addquicktour.aspx") != -1 ||
        url.indexOf("/routeagency/addstandardtour.aspx") != -1 ||
        url.indexOf("/routeagency/routemanage/addquickroute.aspx") != -1 ||
        url.indexOf("/routeagency/routemanage/addstandardroute.aspx") != -1 ||
        url.indexOf("/localagency/localquickroute.aspx") != -1 ||
        url.indexOf("/localagency/localstandardroute.aspx") != -1) {
        tabType = "form";
    } else {
        tabType = "list";
    }

    //Set Active Tab option-un.
    self._resetActiveTab();

    //Create New Tab And Panel.
    var tabId = self._generateTabId(url);
    var panelId = self._generatePanelId(url);

    var $tab, $panel;

    if (isOverLoadCurrentTab) {
        $tab = self.tabs.eq(tmpTabIndex);
        $panel = self.panels.eq(tmpTabIndex);

        $tab.attr("class", o.tabOnClass);

        //        if(tmpTabIndex!=self.activeTabIndex){
        //            $tab.removeClass(o.tabUnClass).addClass(o.tabOnClass);
        //            $panel.removeClass(o.panelUnClass).addClass(o.panelOnClass);
        //        }
        $tab.attr("id", tabId);
        $tab = $(document.getElementById(tabId));
        $tab.find("span").attr("title", label);
        $tab.find("span a").attr("href", url).attr("tabtype", tabType).text(label);


        $panel.attr("id", panelId);
        $panel = $(document.getElementById(panelId));

        $panel.html("正在加载...");
        $panel.removeClass(o.panelUnClass).addClass(o.panelOnClass);
    } else {
        var tabhtml = o.tabTemplate.replace(/#\{cssname\}/g, o.tabOnClass).replace(/#\{name\}/g, label).replace(/#\{href\}/, url).replace(/#\{id\}/, tabId).replace(/#\{tabtype\}/, tabType);
        var panelhtml = o.panelTemplate.replace(/#\{cssname\}/, o.panelOnClass).replace(/#\{id\}/, panelId);

        $tab = $(tabhtml);
        $tab.appendTo(self.tabCtn);

        if (document.getElementById(panelId) == null) {
            $panel = $(panelhtml).html("正在加载...");
            $panel.appendTo(self.panelCtn);
        } else {
            $(document.getElementById(panelId)).html("正在加载...").removeClass(o.panelUnClass).addClass(o.panelOnClass);
        }
    }

    self._tabify();

    var index;
    if (isOverLoadCurrentTab) {
        index = tmpTabIndex;
    } else {
        index = self.length() - 1;
    }

    //Clear Tab Click Event.
    $tab.unbind();
    //Tab Click Event.
    $tab.click(function() {
        var i = self.tabs.index($(this));
        self.select(i);
        return false;
    });

    //Clear Tab CloseLink Event.
    $tab.find("a[class='" + self.tabCloseCss + "']").unbind();
    //Tab CloseLink[class='menuclose'] Click Event.
    $tab.find("a[class='" + self.tabCloseCss + "']").click(function(e) {
        e.stopPropagation();
        var i = self.tabs.index($(this).closest("div"));
        var tabType = self.anchors.eq(i).attr("tabtype");
        if (tabType == "form") {
            var panel = self._getPanelByTabIndex(i);
            var isM = CheckFormIsChange.isFormChanaged(panel.find("form")[0]);
            if (isM) {
                var c = confirm("------------------------------\n提示：当前的线路信息尚未保存。\n是否舍弃该线路信息？\n------------------------------");
                if (!c) {
                    return false;
                }
            }
        }
        self.remove(i);
        return false;
    });

    this.activeTabIndex = index;

    if (o.onAdd != null && o.onAdd != undefined) {
        setTimeout(function() {
            o.onAdd({
                index: index,
                url: self.getUrlByIndex(index),
                title: self.anchors.eq(index).text()
            });
        }, 100);
    }

    //Load Content.
    self.load(index, desUrl);

    return true;
};
/*
Remove a tab.
*/
TopTab.prototype.remove = function(index) {
    var o = this.option;
    var islast = false, isActive = false;
    if (index == (this.length() - 1)) {
        islast = true;
    }
    if (index == this.activeTabIndex) {
        isActive = true;
    }

    //bug for swfupload
    clearcurrentSwfuploadInstances(index);
    //bug for singlecalendar
    clearSingleCalendar();

    var opanel = this._getPanelByTabIndex(index);

    this.tabs.eq(index).find("iframe").attr("src", "").remove().end().remove();
    //this.tabs.eq(index).remove();

    opanel.removeClass(o.panelOnClass).addClass(o.panelUnClass);
    opanel.remove();

    this._tabify();

    if (isActive) {
        this.activeTabIndex = -1;
        if (islast) {
            this.select(this.length() - 1);
        } else {
            this.select(index);
        }
    } else {
        if (index < this.activeTabIndex) {
            this.activeTabIndex--;
        }
    }
};
/*
Remove all tabs expected firsttab.
*/
TopTab.prototype.removeAll = function() {

};
/*
Select a tab.
*/
TopTab.prototype.select = function(index, isreload, desUrl) {
    var o = this.option;

    //Set Active Tab option-un.
    this._resetActiveTab();

    //bug for singlecalendar
    clearSingleCalendar();

    if (index > 0) {
        this.tabs.eq(index).removeClass(o.tabUnClass).addClass(o.tabOnClass);
        var opanel = this._getPanelByTabIndex(index);
        if (opanel != null) {
            opanel.removeClass(o.panelUnClass).addClass(o.panelOnClass);
        }
    } else {
        this.tabs.eq(index).removeClass(o.ftabUnClass).addClass(o.ftabOnClass);
        this.panels.eq(index).removeClass(o.panelUnClass).addClass(o.panelOnClass);
    }

    var self = this;
    if (o.onSelect != null && o.onSelect != undefined) {
        o.onSelect({
            index: index,
            url: self.getUrlByIndex(index),
            title: self.anchors.eq(index).text()
        });
    }

    if (isreload == true) {
        TopMessage.show();
        if (desUrl != undefined && desUrl != null && desUrl != "") {
            this.load(index, desUrl);
        } else {
            this.load(index);
        }
    }

    this.activeTabIndex = index;
};
/*
Reload the content of an tab programmatically.
*/
TopTab.prototype.load = function(index, desUrl) {
    var self = this, o = this.option, a = this.anchors.eq(index), url = a.attr("href"),
    tab = this.tabs.eq(index), panel;
    if (index > 0) {
        panel = this._getPanelByTabIndex(index);
    } else {
        panel = this.panels.eq(index);
    }
    if (desUrl != undefined && desUrl != null && desUrl != "") {
        url = desUrl;
    }
    var customData = "";
    if (this.option.data != undefined && this.option.data != null) {
        customData = $.param(this.option.data);
    }
    //var customData = $.param(this.option.data);
    if (customData != "") {
        if (url.indexOf("?") != -1) {
            url += "&" + $.param(this.option.data);
        } else {
            url += "?" + $.param(this.option.data);
        }
    }

    $.newAjax({
        type: "GET",
        url: url,
        data: { t: Math.random(), RequestSource: "toptab" },
        success: function(r, s) {

            //bug for swfupload
            clearcurrentSwfuploadInstances(index);

            panel.html(r);
            TopMessage.hide();
            //panel[0].innerHTML = r;
        },
        error: function(xhr, s, errorThrow) {
            panel.html("未能成功获取响应结果");
        }
    });
};
/*
Change the url from which an tab will be loaded.
*/
TopTab.prototype.url = function(index, url, data) {
    backPageTop();

    this.option.data = $.extend({}, data || {});
    //bug for singlecalendar
    clearSingleCalendar();

    var panel = this._getPanelByTabIndex(index);
    if (panel == null || panel == undefined) {
        return;
    }

    TopMessage.show();

    var customData = $.param(this.option.data);
    if (customData != "") {
        if (url.indexOf("?") != -1) {
            url += "&" + $.param(this.option.data);
        } else {
            url += "?" + $.param(this.option.data);
        }
    }

    $.newAjax({
        type: "GET",
        url: url,
        data: { t: Math.random(), RequestSource: "toptab" },
        success: function(r, s) {

            //bug for swfupload
            clearcurrentSwfuploadInstances(index);


            panel.html(r);
            TopMessage.hide();
        },
        error: function(xhr, s, errorThrow) {
            panel.html("未能成功获取响应结果");
            TopMessage.hide();
        }
    });
};

TopTab.prototype.GetActivePanel = function(index) {
    return this._getPanelByTabIndex(index);
};

/*
Retrieve the number of tabs
*/
TopTab.prototype.length = function() {
    return this.tabs.length;
};
TopTab.prototype.getIndexByUrl = function(url) {
    var index;
    this.tabs.each(function(i) {
        if ($(this).find("a[href*='" + url + "']").length > 0) {
            index = i;
            return false;
        }
    });
    return index;
};
TopTab.prototype.getUrlByIndex = function(index) {
    var url;
    return this._filterUrl(this.anchors.eq(index).attr("href"));
};
/*判断选项卡页面中，是否有被修改过的表单*/
TopTab.prototype.isHaveFormChanged = function() {
    var isHaveFormChanged = false;
    var self = this;
    this.anchors.each(function(i) {
        var tabtype = $(this).attr("tabtype");
        if (tabtype == "form") {
            var isM = CheckFormIsChange.isFormChanaged(self._getPanelByTabIndex(i).find("form")[0]);
            if (isM) {
                isHaveFormChanged = true;
                return false;
            }
        }
    });

    return isHaveFormChanged;
};

/*
private method.
*/
TopTab.prototype._getPanelByTabIndex = function(tabIndex) {
    if (tabIndex == 0)
        return this.panels.eq(0);
    var url = this.getUrlByIndex(tabIndex);
    var panelId = this._generatePanelId(url);

    if (document.getElementById(panelId) != null) {
        return $(document.getElementById(panelId));
    } else {
        return null;
    }
};
TopTab.prototype._filterUrl = function(url) {
    //是否toLowerCase()。
    return url.replace(location.protocol + "//" + location.host, "");
};
TopTab.prototype._getTabId = function(index) {

};
TopTab.prototype._getPanelId = function(index) {

};
TopTab.prototype._generateTabId = function(url) {
    return this.option.tabIdPrefix + encodeURIComponent(url);
};
TopTab.prototype._generatePanelId = function(url) {
    return this.option.panelIdPrefiex + encodeURIComponent(url);
};
TopTab.prototype._tabify = function() {
    this.tabs = $(this.tabCtn).children("div:has(a[href])");
    this.anchors = this.tabs.map(function() { return $('a', this)[0]; });
    this.panels = $(this.panelCtn).children("div");
};
TopTab.prototype._resetActiveTab = function() {
    if (this.activeTabIndex == -1) {
        return;
    }
    var o = this.option;
    if (this.activeTabIndex > 0) {
        this.tabs.eq(this.activeTabIndex).removeClass(o.tabOnClass).addClass(o.tabUnClass);
        var opanel = this._getPanelByTabIndex(this.activeTabIndex);
        if (opanel != null) {
            opanel.removeClass(o.panelOnClass).addClass(o.panelUnClass);
        }
    } else {
        this.tabs.eq(this.activeTabIndex).removeClass(o.ftabOnClass).addClass(o.ftabUnClass);
        this.panels.eq(this.activeTabIndex).removeClass(o.panelOnClass).addClass(o.panelUnClass);
    }
};

/*
TopMessage
*/
var TopMessage = {
    show: function(option) {
        this.option = $.extend({
            message: "正在载入..."
        }, option || {});
        $("#topmessage span").text(this.option.message);
        $("#topmessage").css("visibility", "visible");
        // CenterMessage.show();
    },
    hide: function() {
        $("#topmessage").css("visibility", "hidden");
        // CenterMessage.hide();
    }
};

var CenterMessage = {
    dialog: null,
    show: function(option) {
        option = $.extend({
            message: "正在载入..."
        }, option || {});
        this.hide();
        this.dialog = new Boxy("<p>" + option.message + "</p>", { title: "ads", modal: true, closeable: true });
    },
    hide: function() {
        if (this.dialog) {
            this.dialog.hide();
        }
    }
};

/*
CheckFormIsChange,2010-5-18，判断表单是否改变
*/
var CheckFormIsChange = {
    data: [],
    recodeInitialDataForm: function(form) {
        //this.data=[]; 
        //记录表单的初始数据
        var oTable = $(form).children("table").eq(0);
        var FormData = $(form).serialize();
        this.data[oTable.attr("id")] = FormData;
    },
    isFormChanaged: function(form) {
        //检查表单是否已经被修改
        var isModified = false;
        var element;
        var oTable = $(form).children("table").eq(0);
        var FormData = $(form).serialize();
        var oldFormData = this.data[oTable.attr("id")];
        if (FormData != oldFormData) {
            isModified = true;
        }
        return isModified;
    }
};

/*
Clear Swfupload.
*/
function clearcurrentSwfuploadInstances(index) {
    var panel = topTab._getPanelByTabIndex(index);
    var isHaveUploadControl = false;
    if (panel.find("div[id$='divFileProgressContainer']").length > 0) {
        isHaveUploadControl = true;
    }
    if (isHaveUploadControl) {
        try {
            for (var i = 0; i < currentSwfuploadInstances.length; i++) {
                if (currentSwfuploadInstances[i]) {
                    currentSwfuploadInstances[i].destroy();
                    currentSwfuploadInstances[i] = null;
                }
            }
            currentSwfuploadInstances = [];
        } catch (ex) { }
    }
}

/*
Auto Close SingleCalendar.
*/
function clearSingleCalendar() {
    try {
        if (SingleCalendar) {
            if (SingleCalendar.CloseCalendar) {
                SingleCalendar.CloseCalendar();
            }
        }
    } catch (ex) { }
}

/*
back page top
*/
function backPageTop() {
    //document.body.scrollTop="0px"
    window.scrollTo(0, 0);
};


/*	dynamicCSS.js v1.0 <http://www.bobbyvandersluis.com/articles/dynamicCSS.php>
Copyright 2005 Bobby van der Sluis
This software is licensed under the CC-GNU LGPL <http://creativecommons.org/licenses/LGPL/2.1/>
*/
/* Cross-browser dynamic CSS creation
- Based on Bobby van der Sluis' solution: http://www.bobbyvandersluis.com/articles/dynamicCSS.php
*/
function createStyleRule(selector, declaration) {
    if (!document.getElementsByTagName || !(document.createElement || document.createElementNS)) return;
    var agt = navigator.userAgent.toLowerCase();
    var is_ie = ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1));
    var is_iewin = (is_ie && (agt.indexOf("win") != -1));
    var is_iemac = (is_ie && (agt.indexOf("mac") != -1));
    if (is_iemac) return; // script doesn't work properly in IE/Mac
    var head = document.getElementsByTagName("head")[0];
    var style = (typeof document.createElementNS != "undefined") ? document.createElementNS("http://www.w3.org/1999/xhtml", "style") : document.createElement("style");
    if (!is_iewin) {
        var styleRule = document.createTextNode(selector + " {" + declaration + "}");
        style.appendChild(styleRule); // bugs in IE/Win
    }
    style.setAttribute("type", "text/css");
    style.setAttribute("media", "screen");
    head.appendChild(style);
    if (is_iewin && document.styleSheets && document.styleSheets.length > 0) {
        var lastStyle = document.styleSheets[document.styleSheets.length - 1];
        if (typeof lastStyle.addRule == "object") { // bugs in IE/Mac and Safari
            lastStyle.addRule(selector, declaration);
        }
    }
}


function htmlDecode(text) {
    var oDiv = document.createElement("div");
    oDiv.innerHTML = text;
    var output = oDiv.innerText || oDiv.textContent;
    oDiv = null;
    return output;
}