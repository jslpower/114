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
function copyDate(date) {
    var d = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    //	d.setYear(date.getFullYear());
    //	d.setMonth(date.getMonth());
    //	d.setDate(date.getDate());
    return d;
}
function formatDate(date) {
    var d = new Date(date.getFullYear(), date.getMonth(), 1);
    return d;
}
function compareDate(DateOne, DateTwo) {
    var OneMonth = DateOne.getMonth() + 1;
    var OneDay = DateOne.getDate();
    var OneYear = DateOne.getFullYear();

    var TwoMonth = DateTwo.getMonth() + 1;
    var TwoDay = DateTwo.getDate();
    var TwoYear = DateTwo.getFullYear();

    if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) >
	Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
        return 1;
    }
    else if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) <
	Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
        return -1;
    } else {
        return 0;
    }

}
Array.prototype.remove = function(dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}
var QGD = {
    about: { Author: '汪奇志', DateTime: '2010-04-24', Description: '快速发布模板团队选择出团日期' },
    html: '<table class="calendarTable" width="100%" cellspacing="0" cellpadding="0">' +
  '<tbody><tr>' +
    '<td height="25" bgcolor="#def2fc" align="center" style="padding: 3px;">&nbsp;<a href="javascript:void(0);" id="linkPreMonth">上一月</a>&nbsp;&nbsp;</td>' +
    '<td height="25" bgcolor="#def2fc" align="center" style="padding: 3px;">&nbsp; <a href="javascript:void(0);" id="linkNextMonth">下一月</a>&nbsp;</td>' +
  '</tr>' +
  '<tr>' +
    '<td width="50%" valign="top"><div id="thisMonthCalendar"></div></td>' +
    '<td width="50%" valign="top"><div id="nextMonthCalendar"></div></td>' +
  '</tr>' +
'</tbody></table>',

    //初始化日历
    initCalendar: function(option) {
        this.option = $.extend({
            containerId: "calendarContainer",
            tourLeaveDate: "hidTourLeaveDate",
            areatype: 0,
            listcontainer: "divTourCodeHTML"
        }, option || {});
        this.currentDate = this.option.currentDate;
        this.maxDate = copyDate(this.option.currentDate);

        this.maxDate = formatDate(this.maxDate);

        if (this.option.areatype == 1)   // 国际线
        {
            this.maxDate.setMonth(this.maxDate.getMonth() + 11);
        } else {
            this.maxDate.setMonth(this.maxDate.getMonth() + 5);
        }

        this.SCD = formatDate(this.option.firstMonthDate);
        this.SND = formatDate(this.option.nextMonthDate);
        this.config.arrOldLeaveDate = eval($("#" + this.option.containerId).find("input[type=hidden][name$=" + this.option.oldLeaveDate + "]").val());
        this.config.arrOldTourCode = eval($("#" + this.option.containerId).find("input[type=hidden][name$=" + this.option.oldTourCode + "]").val());

        this.initCalendarBasic();
        this._createCalendarContainer();

        //初始化当前月日历
        this.createCalendarMonth(false);
        this.createCalendarDays(false);

        //初始化下月日历
        this.createCalendarMonth(true);
        this.createCalendarDays(true);

        //创建空行
        this.createEmptyRows();
        this.InitCheckedDate();
        if (this.config.arrOldLeaveDate != undefined && this.config.arrOldLeaveDate.length > 0) {
            this.createChildrenTourHTML(this.config.arrOldLeaveDate, this.config.arrOldTourCode);
        }
        /*
        //初始化基础数据
        this.initBasic();
        //选中子团的日期
        this.initCalendarChecked();
        */
    },
    updateCalendar: function(option) {
        this.option = $.extend(this.option, option || {});

        this.config.CR = 0;
        this.config.NR = 0;

        this.SCD = this.option.firstMonthDate;
        this.SND = this.option.nextMonthDate;

        this.initCalendarBasic();
        this._createCalendarContainer();

        //初始化当前月日历
        this.createCalendarMonth(false);
        this.createCalendarDays(false);

        //初始化下月日历
        this.createCalendarMonth(true);
        this.createCalendarDays(true);

        //创建空行
        this.createEmptyRows();
        /*
        //初始化基础数据
        this.initBasic();
        //选中子团的日期
        this.initCalendarChecked();
        */
    },
    p: parent,
    //页面元素配置信息
    elements: {
        //生成的当前月表格id
        cMTable: 'cMonthTable',
        //生成的下一个月表格id
        nMTable: 'nMonthTable',
        //存放当前月表格容器id
        cMTd: "thisMonthCalendar",
        //存放下一个月表格容器id
        nMTd: "nextMonthCalendar",
        //线路区域下拉表单id
        tourArea: "txtTourArea",
        // 父页面存放出团日期
        tourLeaveDate: "hidTourLeaveDate",
        //子团容器
        childrenTour: "ulChildrenTours",
        //正在生成团队的提示信息
        loading: "loading",
        //存放要新增的子团
        ITJ: "txtInsertChildrenTours",
        //存放要修改的子团
        UTJ: "txtUpdateChildrenTours",
        //存放要删除的子团
        DTJ: "txtDeleteChildrenTours"
    },
    maxDate: new Date(),
    //服务器当前日期
    currentDate: new Date(),
    //显示的第一个月的日期
    SCD: new Date(),
    //服务器下一个月日期
    SND: new Date(),
    //日历基础数据
    SDConfig: { CY: 2010, CM: 1, CD: 1, CDays: 31, NY: 2010, NM: 2, NDays: 28 },
    //配置信息
    config: {
        //当前月行数
        CR: 0,
        //下一个月行数
        NR: 0,
        //当前月1号在表格内td的index
        CI: 0,
        //下一个月1号在表格内td的index
        NI: 0,
        // 存放选中的出团日期
        arrLeaveDate: [],
        // 原来的子团出团日期
        arrOldLeaveDate: [],
        // 原来的子团团号
        arrOldTourCode: [],
        //模板团编号
        TI: 0,
        //原有子团信息
        TC: [],
        //当前子团信息
        CC: [],
        //父页面盛放子团的jQuery对象
        JPO: null,
        //日历当前月表格jQuery对象
        JCO: null,
        //日历下个月表格jQuery对象
        JNO: null,
        //父页面线路区域下拉列表的jQuery对象
        JPTAO: null,
        //是否有新增子团
        ISI: false,
        //是否有修改子团
        ISU: false,
        //是否有删除子团
        ISD: false,
        //empty template children tour info
        ETC: {}
    },
    //初始化日历基础数据
    initCalendarBasic: function() {
        this.SDConfig.CY = this.SCD.getFullYear();
        this.SDConfig.CM = this.SCD.getMonth() + 1;
        this.SDConfig.CD = this.SCD.getDate();
        this.SDConfig.CDays = new Date(this.SDConfig.CY, this.SDConfig.CM, 0).getDate();

        this.SDConfig.NY = this.SND.getFullYear();
        this.SDConfig.NM = this.SND.getMonth() + 1;
        this.SDConfig.NDays = new Date(this.SDConfig.NY, this.SDConfig.NM, 0).getDate();
    },
    /*private*/
    _createCalendarContainer: function() {
        if (!document.getElementById(this.option.containerId)) {
            alert("请指定日历容器");
            return false;
        }

        if (this.firstLoad == undefined || this.firstLoad == false) {
            $("#" + this.option.containerId).html(this.html);
            this.firstLoad = true;
        }
        var self = this;
        var cDate = copyDate(self.SCD);
        var nDate = copyDate(self.SND);
        var result = compareDate(self.SCD, this.currentDate);
        if (result == -1 || result == 0) {
            $("#linkPreMonth").css("display", "none");
        } else {
            $("#linkPreMonth").css("display", "").unbind().click(function() {
                var a = copyDate(cDate);
                cDate.setMonth(cDate.getMonth() - 1, 1);
                var b = copyDate(cDate);
                self.GetCheckedDate();
                self.updateCalendar({
                    nextMonthDate: a,
                    firstMonthDate: b
                });

                self.InitCheckedDate();
            });
        }
        result = compareDate(this.SND, this.maxDate);
        if (result == 1 || result == 0) {
            $("#linkNextMonth").css("display", "none");
        } else {
            $("#linkNextMonth").css("display", "").unbind().click(function() {
                var a = copyDate(nDate);
                nDate.setMonth(nDate.getMonth() + 1, 1)
                var b = copyDate(nDate);
                self.GetCheckedDate();
                self.updateCalendar({
                    firstMonthDate: a,
                    nextMonthDate: b
                });

                self.InitCheckedDate();
            });
        }
    },
    //创建日历表格
    createCalendarMonth: function(isNextMonth) {
        var myself = this;
        var tableId = isNextMonth ? this.elements.nMTable : this.elements.cMTable;
        var s = [];
        s.push('<table width="100%" border="1" cellpadding="0" cellspacing="0" id="' + tableId + '">');
        s.push('<tr><th colspan="8"><input type="checkbox" id="' + isNextMonth + 'ckAll"><label for="' + isNextMonth + 'ckAll">全选 ' + (isNextMonth ? this.SDConfig.NY : this.SDConfig.CY) + '年' + (isNextMonth ? this.SDConfig.NM : this.SDConfig.CM) + '月整月</label></th></tr>');
        s.push('<tr class="weektitle"><td style="width:16%"><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '0"><label for="' + isNextMonth + '0">日</label></td></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '1"><label for="' + isNextMonth + '1">一</label></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '2"><label for="' + isNextMonth + '2">二</label></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '3"><label for="' + isNextMonth + '3">三</label></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '4"><label for="' + isNextMonth + '4">四</label></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '5"><label for="' + isNextMonth + '5">五</label></td><td style="width: 12%"><input type="checkbox" id="' + isNextMonth + '6"><label for="' + isNextMonth + '6">六</label></td></tr>');
        s.push('</table>');
        $("#" + (isNextMonth ? this.elements.nMTd : this.elements.cMTd)).html(s.join(''));

        if (isNextMonth) {
            this.config.JNO = $("#" + tableId);
        } else {
            this.config.JCO = $("#" + tableId);
        }

        var obj = isNextMonth ? this.config.JNO : this.config.JCO;

        //年全选绑定事件
        obj.find('tr th input[type="checkbox"]').bind("click", function() { myself.selectMonth(this, isNextMonth) });
        //列全选绑定事件
        obj.find('tr:gt(0) input[type="checkbox"]').bind("click", function() { myself.selectColumn(this, isNextMonth) });
        //列mouseover mouseout事件
        obj.find('tr:eq(1) td').bind("mousemove", function() { myself.mouseoverColumn(this); }).bind("mouseout", function() { myself.mouseoutColumn(this); })

    },
    //创建前面的空白天数
    createStartEmptyDays: function(monthFirstDayOfWeek) {
        var s = [];
        for (var i = 0; i < monthFirstDayOfWeek; i++) {
            s.push('<td></td>')
        }
        return s.join('');
    },
    //创建空行
    createEmptyRows: function() {
        var s = '<tr style="height:21px;"><td>&nbsp;</td><td>&nbsp;</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>';
        if (this.config.CR > this.config.NR) {
            for (var i = 0; i < this.config.CR - this.config.NR; i++) {
                this.config.JNO.append(s);
            }
        } else {
            for (var i = 0; i < this.config.NR - this.config.CR; i++) {
                this.config.JCO.append(s);
            }
        }
    },
    //创建后面的空白天数
    createEndEmptyDays: function(days) {
        if (days == 7) return;
        var s = [];
        for (var i = 0; i < days; i++) {
            s.push('<td></td>')
        }
        s.push('</tr>')
        return s.join('');
    },
    //创建日历日期信息
    createCalendarDays: function(isNextMonth) {
        var myself = this;
        var obj = isNextMonth ? this.config.JNO : this.config.JCO;
        var tmpyear = isNextMonth ? this.SDConfig.NY : this.SDConfig.CY;
        var tmpmonth = isNextMonth ? this.SDConfig.NM : this.SDConfig.CM;
        var sd = 1;
        var fd = isNextMonth ? this.SDConfig.NDays : this.SDConfig.CDays;
        var sdOfWeek = isNextMonth ? new Date(this.SDConfig.NY, this.SDConfig.NM - 1, 1).getDay() : new Date(this.SDConfig.CY, this.SDConfig.CM - 1, 1).getDay();
        var s = [];
        var i = 1;
        var isCurrentMonth = false;
        if (this.SDConfig.CY == this.currentDate.getFullYear()
			&& this.SDConfig.CM == (this.currentDate.getMonth() + 1)
			&& isNextMonth == false) {
            isCurrentMonth = true;
        }

        do {
            if ((i) % (7) == 1) {

                if (isNextMonth) {
                    this.config.NR++;
                } else {
                    this.config.CR++;
                }

                s.push('<tr>');
                var inputid = isNextMonth ? 'cw_' + sd : 'nw_' + sd;
                s.push('<td><input type="checkbox" id="' + inputid + '"><label for="' + inputid + '">第' + parseInt((sd + sdOfWeek + 6) / 7) + '周</label></td>');
            }

            if (i == 1) {
                s.push(this.createStartEmptyDays(sdOfWeek));
                i = i + sdOfWeek;
            }

            var tmpDate = tmpyear + "-" + tmpmonth + "-" + sd;
            s.push('<td class="days"><input type="checkbox"' + (sd < this.currentDate.getDate() && isCurrentMonth ? ' disabled="disabled"' : '') + ' value="' + tmpDate + '"><span>' + sd + '</span></td>');

            if ((i) % (7) == 0) { s.push('</tr>'); }

            if (isNextMonth) {
                this.config.NI = 9 + sdOfWeek;
            } else {
                this.config.CI = 9 + sdOfWeek;
            }

            sd++;
            i++
        } while (sd <= fd)

        s.push(this.createEndEmptyDays(7 - (i - 1) % 7));
        obj.append(s.join(''));
        //行全选绑定事件
        obj.find('tr:gt(1)').find('td:eq(0) input[type=checkbox]').bind("click", function() { myself.selectRow(this, isNextMonth); });
        //天选中绑定事件
        obj.find('td.days input[type=checkbox]').bind("click", function() { myself.selectDay(this, isNextMonth); });
        //行mouseover mouseout事件
        obj.find("tr:gt(1)").find("td:eq(0)").bind("mousemove", function() { myself.mouseoverRow(this); }).bind("mouseout", function() { myself.mouseoutRow(this); })
    },
    //选中事件
    setChecked: function(obj, isNextMonth, checked) {
        if (checked) {
            obj.checked = checked;
        } else {
            obj.checked = checked;
        }
    },
    //月份全选
    selectMonth: function(obj, isNextMonth) {
        var myself = this;
        $(obj).closest("table").find("input[type=checkbox]:enabled").each(function() { myself.setChecked(this, isNextMonth, obj.checked); })
    },
    //列全选
    selectColumn: function(obj, isNextMonth) {
        var myself = this;
        $(obj).closest("table").find("tr:gt(1)").each(function() { $(this).find("td:eq(" + $(obj).closest("tr").find("td").index($(obj).parent()) + ") input:[type=checkbox]:enabled").each(function() { myself.setChecked(this, isNextMonth, obj.checked) }); });
    },
    //行全选
    selectRow: function(obj, isNextMonth) {
        var myself = this;
        $(obj).closest("tr").find("input[type=checkbox]:enabled:gt(0)").each(function() { myself.setChecked(this, isNextMonth, obj.checked); });
    },
    //天选中
    selectDay: function(obj, isNextMonth) {
        this.setChecked(obj, isNextMonth, obj.checked);
    },
    //onmouseover 星期 列
    mouseoverColumn: function(obj) {
        $(obj).closest("table").find("tr:gt(1)").each(function() { $(this).find("td:eq(" + $(obj).parent().find("td").index($(obj)) + ")").css({ background: '#e3d596' }) });
    },
    //onmouseout 星期 列
    mouseoutColumn: function(obj) {
        $(obj).closest("table").find("tr:gt(1)").each(function() { $(this).find("td:eq(" + $(obj).parent().find("td").index($(obj)) + ")").css({ background: '#ffffff' }) });
    },
    //onmouseover 第N周 行
    mouseoverRow: function(obj) {
        $(obj).parent().children("td").css({ background: '#e3d596' });
    },
    //onmouseout 第N周 行
    mouseoutRow: function(obj) {
        $(obj).parent().children("td").css({ background: '#ffffff' });
    },
    //选中子团的日期
    initCalendarChecked: function() {
        var tmp = (this.config.CC.length < 1) ? this.config.TC : this.config.CC;

        for (var i = 0; i < tmp.length; i++) {
            var arr = tmp[i].TravelPeriod.split("-");
            var m = arr[1];
            var d = arr[2];

            if (m == this.SDConfig.CM) {
                this.config.JCO.find('td.days input[type="checkbox"]:eq(' + (d - 1) + ')').attr("checked", true);
            } else {
                this.config.JNO.find('td.days input[type="checkbox"]:eq(' + (d - 1) + ')').attr("checked", true);
            }
        }
    },
    //获取星期
    getWeek: function(date) {
        var d = date.split("-");
        var dayOfWeek = new Date(d[0], d[1] - 1, d[2]).getDay();

        var s = "";

        switch (dayOfWeek) {
            case 0: s = "日"; break;
            case 1: s = "一"; break;
            case 2: s = "二"; break;
            case 3: s = "三"; break;
            case 4: s = "四"; break;
            case 5: s = "五"; break;
            case 6: s = "六"; break;
        }

        return s;
    },
    formatMD: function(num) {
        var tmp = 100 + num;
        return tmp.toString().substring(1);
    },
    //初始化基础数据
    initBasic: function() {
        var myself = this;
        this.config.JPO = QGD.config.JPO;

        this.config.JPTAO = QGD.config.JPTAO;
        this.config.ETC = QGD.config.ETC;

        this.config.CC = [];
        this.config.JPO.find('li').each(function() {
            var tmp = myself.getETC();
            tmp['ChildrenId'] = $(this).find('input[name="txtChildrenId"]').val();
            tmp['TravelPeriod'] = $(this).find('input[name="txtTravelPeriod"]').val();
            tmp['TourCode'] = $(this).find('input[name="txtTourCode"]').val();
            tmp["Checked"] = tmp['ChildrenId'] > 0 ? $(this).find('input[type="checkbox"]').attr("checked") : false;

            myself.config.CC.push(tmp);
        });
    },
    //生成子团HTML
    createChildrenTourHTML: function(tourLeaveDate, tourCode) {
        var s = [];
        if (tourLeaveDate.length > 0) {
            var y = 0, m = 0, j = 0;
            for (var i = 0; i < tourLeaveDate.length; i++) {
                var arr = tourLeaveDate[i].split('-');
                y = arr[0];
                var d = arr[2];
                if (d.length < 2)
                    d = '0' + d;
                if (arr[1] != m) {
                    j = 1;
                    if (m != 0) {
                        s.push('</fieldset>');
                    }
                    var tmpM = arr[1];
                    if (arr[1].length < 2)
                        tmpM = '0' + arr[1];
                    s.push('<fieldset style=\"font-size:12px;\">');
                    s.push('<legend>' + y + '年' + arr[1] + '月</legend>');
                    s.push('<label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + y + '/' + tmpM + '/' + d + '&nbsp;(' + this.getWeek(tourLeaveDate[i]) + ')&nbsp;[' + tourCode[i] + ']</label>');
                    m = arr[1];
                } else {
                    j++;
                    var tmpM = arr[1];
                    if (arr[1].length < 2)
                        tmpM = '0' + arr[1];
                    if (j != 0 && j % 3 == 0) {
                        s.push('<label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + y + '/' + tmpM + '/' + d + '&nbsp;(' + this.getWeek(tourLeaveDate[i]) + ')&nbsp;[' + tourCode[i] + ']</label><br />');
                    } else {
                        s.push('<label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + y + '/' + tmpM + '/' + d + '&nbsp;(' + this.getWeek(tourLeaveDate[i]) + ')&nbsp;[' + tourCode[i] + ']</label>');
                    }
                }
            }
        }
        s.push('</fieldset>');
        $("#" + this.option.listcontainer).html(s.join(''));
    },
    //子团全选
    selectAll: function(obj) {
        $("#" + this.elements.childrenTour).find("input[type=checkbox]").attr("checked", obj.checked);
    },
    // 获取选中的日期
    GetCheckedDate: function() {
        var myself = this;
        var objs = [{ obj: this.config.JCO, year: this.SDConfig.CY, month: this.SDConfig.CM }, { obj: this.config.JNO, year: this.SDConfig.NY, month: this.SDConfig.NM}];

        //获取选中的日期
        for (var i = 0; i < objs.length; i++) {
            objs[i].obj.find('td.days input[type="checkbox"]:enabled').each(function() {
                var tmpDate = $(this).val();
                if (QGD.config.arrLeaveDate.length > 0) {
                    var oid = -1;
                    for (var h = 0; h < QGD.config.arrLeaveDate.length; h++) {
                        if (tmpDate == QGD.config.arrLeaveDate[h]) {
                            if (!$(this).attr("checked")) {
                                QGD.config.arrLeaveDate.remove(h);
                            }
                            oid = h;
                            break;
                        }
                    }
                    if (oid == -1) {
                        if ($(this).attr("checked")) {
                            QGD.config.arrLeaveDate.push(tmpDate);
                        }
                    }
                } else {
                    if ($(this).attr("checked")) {
                        QGD.config.arrLeaveDate.push(tmpDate);
                    }
                }
            });
        }
    },
    // 初始化选中的日期
    InitCheckedDate: function() {
        var myself = this;
        var objs = [{ obj: this.config.JCO, year: this.SDConfig.CY, month: this.SDConfig.CM }, { obj: this.config.JNO, year: this.SDConfig.NY, month: this.SDConfig.NM}];

        if (QGD.config.arrLeaveDate.length == 0) {
            var InitDate = $("#" + this.option.parentContainerID).find("input[type=hidden][name$=" + this.option.tourLeaveDate + "]").val();
            if (InitDate && InitDate.length > 0) {
                QGD.config.arrLeaveDate = InitDate.split(',');
            }
        }

        if (QGD.config.arrLeaveDate.length > 0) {
            for (var i = 0; i < objs.length; i++) {
                objs[i].obj.find('td.days input[type="checkbox"]').each(function() {
                    var tmpDate = $(this).val();
                    for (var h = 0; h < QGD.config.arrLeaveDate.length; h++) {
                        if (tmpDate == QGD.config.arrLeaveDate[h]) {
                            $(this).attr("checked", "checked");
                            break;
                        }
                    }
                });
            }
        }

        if (this.config.arrOldLeaveDate != '' && this.config.arrOldLeaveDate != null) {
            var s = [];
            for (var j = 0; j < objs.length; j++) {
                for (var i = 0; i < this.config.arrOldLeaveDate.length; i++) {
                    var arr = this.config.arrOldLeaveDate[i].split('-');
                    var m = arr[1];
                    var d = arr[2];
                    if (m == objs[j].month) {
                        var day = d - 1;
                        objs[j].obj.find('td.days input[type=checkbox]:eq(' + day + ')').attr("checked", true).attr("disabled", "disabled");
                    }
                }
            }
        }
        //else{

        //}		
    },
    SubmitClick: function() {
        this.GetCheckedDate();
        if (QGD.config.arrLeaveDate.length > 0) {
            $("#" + this.option.parentContainerID).find("input[type=hidden][name$=" + this.option.tourLeaveDate + "]").val(QGD.config.arrLeaveDate.join(','));
            $("#" + this.option.parentContainerID).find("span[id=spanChildCount]").html('<strong>' + QGD.config.arrLeaveDate.length + '</strong>');
        } else {
            alert('请选择出团日期!');
            return;
        }
        var frameid = window.parent.Boxy.queryString("iframeId")
        window.parent.Boxy.getIframeDialog(frameid).hide();
    }
};

$(document).ready(function() {
    createStyleRule(".calendarTable table", "border-collapse:collapse;");
    createStyleRule(".calendarTable td", "font-size:12px; line-height:120%;");
    createStyleRule(".calendarTable .weektitle td", "background: #e3d596;text-align: left;");
    createStyleRule(".calendarTable .days", "text-align: left;");
    createStyleRule(".calendarTable th", "background: #0099cc;width: 50%;border-bottom: 1px solid #efefef;");
});
