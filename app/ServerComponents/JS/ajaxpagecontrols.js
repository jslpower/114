/*
JavaScript分页控件 汪奇志 2010-03-04
*/
if (!window.AjaxPageControls) {
    window.AjaxPageControls = (function() {
        var AjaxPageControls = {
            version: '0.0.1',
            author: 'wangqizhi',
            description: 'AJAX 客户端分页控件',
            config: {
                cssClassName: 'ajaxpagecontrols',
                pageSize: 10,
                pageIndex: 1,
                recordCount: 0,
                pageCount: 0,
                gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                showPrev: true,
                showNext: true,
                showNo: true,
                showText:true
            }
        };
        return AjaxPageControls;
    })();
}

AjaxPageControls.getObj = function(elementId) {
    return document.getElementById(elementId);
};

AjaxPageControls.replace = function(elementId, config) {
    this.createConfig(config);
    this.getObj(elementId).innerHTML = this.createHTML();
};

AjaxPageControls.createConfig = function(config) {
    //if (config == null || config == 'undefined') return;

    for (var key in config) {
        if (key == 'pageCount') { continue; }
        this.config[key] = config[key];
    }

    this.config.pageCount = Math.ceil(this.config.recordCount / this.config.pageSize);
    if (this.config.pageIndex < 1) this.config.pageIndex = 1;
    if (this.config.pageIndex > this.config.pageCount) this.config.pageIndex = this.config.pageCount;
};

AjaxPageControls.createDisplayText = function() {
    var s = new Array();

    s.push('<span>');
    s.push('每页显示' + this.config.pageSize + '条 共' + this.config.recordCount + '条信息');
    s.push(' 共' + this.config.pageCount + '页');
    s.push('</span>');

    return s.join('');
};

AjaxPageControls.createEllipsis = function() {
    return '....';
};

AjaxPageControls.createPrev = function() {
    if (this.config.pageIndex > 1) {
        var prevPageIndex = this.config.pageIndex - 1;
        return '<a href="javascript:void(0)" onclick="return ' + this.config.gotoPageFunctionName + '(' + prevPageIndex + ')"><nobr>上一页</nobr></a>';
    } else {
        return '<span class="disabled"><nobr>上一页</nobr></span>';
    }
};

AjaxPageControls.createPage = function(pageIndex) {
    var s = '';

    if (pageIndex != this.config.pageIndex) {
        s = '<a href="javascript:void(0)" onclick="return ' + this.config.gotoPageFunctionName + '(' + pageIndex + ')">' + pageIndex + '</a>';
    } else {
        s = '<span class="current">' + pageIndex + '</span>';
    }

    return s;
};

AjaxPageControls.createNext = function() {
    if (this.config.pageIndex != this.config.pageCount) {
        var nextPageIndex = this.config.pageIndex + 1;
        return '<a href="javascript:void(0)" onclick="return ' + this.config.gotoPageFunctionName + '(' + nextPageIndex + ')"><nobr>下一页</nobr></a>';
    } else {
        return '<span class="disabled"><nobr>下一页</nobr></span>';
    }
}

AjaxPageControls.createHTML = function(elementId) {
    var s = new Array();
    var startRecordIndex = (this.config.pageIndex - 1) * this.config.pageSize;
    var finishRecordIndex = startRecordIndex + this.config.pageSize;
    if (this.config.recordCount > 0) startRecordIndex++;
    if (finishRecordIndex > this.config.recordCount) finishRecordIndex = this.config.recordCount;

    if (this.config.showText) {
        s.push(this.createDisplayText());
    }

    if (this.config.showPrev) {
        s.push(this.createPrev());
    }

    if (this.config.showNo) {
        /*
        分页控件最多同时显示9个页面的页列表
        flag=0:分页显示所有的页列表
        flag=1:分页控件显示前面的7页列表+....+最后两页
        flag=2:分页控件显示前2页+...+当前页面前2页面至当前页后面2页页列表+....+最后2页    
        flag=3:分页控件显示前2页+...+最后7个页页列表
        */
        var flag = 0;

        if (this.config.pageCount <= 9) {
            flag = 0;
        } else if (this.config.pageIndex <= 5) {
            flag = 1;
        } else if (this.config.pageIndex + 4 < this.config.pageCount) {
            flag = 2;
        } else {
            flag = 3;
        }

        if (flag == 0) {
            for (var i = 1; i <= this.config.pageCount; i++) {
                s.push(this.createPage(i));
            }
        }

        if (flag == 1) {
            for (var i = 1; i <= 7; i++) {
                s.push(this.createPage(i));
            }
            s.push(this.createEllipsis());
            for (var i = this.config.pageCount - 1; i <= this.config.pageCount; i++) {
                s.push(this.createPage(i));
            }
        }

        if (flag == 2) {
            for (var i = 1; i <= 2; i++) {
                s.push(this.createPage(i));
            }

            s.push(this.createEllipsis());

            for (var i = this.config.pageIndex - 2; i <= this.config.pageIndex + 2; i++) {
                s.push(this.createPage(i));
            }

            s.push(this.createEllipsis());

            for (var i = this.config.pageCount - 1; i <= this.config.pageCount; i++) {
                s.push(this.createPage(i));
            }
        }

        if (flag == 3) {
            for (var i = 1; i <= 2; i++) {
                s.push(this.createPage(i));
            }

            s.push(this.createEllipsis());

            for (var i = this.config.pageCount - 6; i <= this.config.pageCount; i++) {
                s.push(this.createPage(i));
            }
        }
    }

    if (this.config.showNext) {
        s.push(this.createNext());
    }

    s.push(this.createHiddens(elementId));

    return s.join('');
};

AjaxPageControls.createUrl = function(url, params) {
    var isHaveParam = false;
    var isHaveQuestionMark = false;
    var questionMark = "?";
    var questionMarkIndex = url.indexOf(questionMark);
    var urlLength = url.length;

    if (questionMarkIndex == urlLength - 1) {
        isHaveQuestionMark = true;
    } else if (questionMarkIndex != -1) {
        isHaveParam = true;
    }

    if (isHaveParam == true) {
        for (var key in params) {
            url = url + "&" + key + "=" + params[key];
        }
    } else {
        if (isHaveQuestionMark == false) {
            url += questionMark;
        }
        for (var key in params) {
            url = url + key + "=" + params[key] + "&";
        }
        url = url.substr(0, url.length - 1);
    }

    return url;
};

AjaxPageControls.getUrlParms = function(removeParms) {
    var argsArr = new Object();
    var query = window.location.search;
    query = query.substring(1);
    var pairs = query.split("&");

    for (var i = 0; i < pairs.length; i++) {
        var sign = pairs[i].indexOf("=");
        if (sign == -1) {
            continue;
        }

        var aKey = pairs[i].substring(0, sign);
        var aValue = pairs[i].substring(sign + 1);

        //移除不需要要的键
        var isRemove = false
        for (var j = 0; j < removeParms.length; j++) {
            if (aKey.toLowerCase() == removeParms[j].toLowerCase()) {
                isRemove = true;
                break;
            }
        }

        if (isRemove) {
            continue;
        }

        argsArr[aKey] = aValue;
    }

    return argsArr;
};

AjaxPageControls.gotoPage = function(pageIndex) {
    var url = window.location.pathname;
    var urlParms = this.getUrlParms(["page"]);
    urlParms["Page"] = pageIndex;

    window.location.href = this.createUrl(url, urlParms);
    return false;
}

AjaxPageControls.createHiddens = function(elementId) {
    var s = new Array();
    var inputId = "AjaxPageControls_PageIndex_" + elementId;
    //当前页索引隐藏域
    s.push('<input type="hidden" name="' + inputId + '" id="' + inputId + '" value="' + this.config.pageIndex + '" />');
    return s.join('');
}

