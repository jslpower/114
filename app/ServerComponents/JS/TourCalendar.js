
function copyDate(date){
	var d = new Date(date.getFullYear(),date.getMonth(),date.getDate());
//	d.setYear(date.getFullYear());
//	d.setMonth(date.getMonth());
//	d.setDate(date.getDate());
	return d;
}
function compareDate(DateOne,DateTwo)  
{   
	var OneMonth = DateOne.getMonth()+1; 
	var OneDay = DateOne.getDate();
	var OneYear = DateOne.getFullYear();  
	  
	var TwoMonth = DateTwo.getMonth()+1;  
	var TwoDay = DateTwo.getDate();
	var TwoYear = DateTwo.getFullYear();

	var date1 = Date.parse(OneMonth + "/" + OneDay + "/" + OneYear);

	var date2 = Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear);

	if (Date.parse(OneMonth+"/"+OneDay+"/"+OneYear) >  
	Date.parse(TwoMonth+"/"+TwoDay+"/"+TwoYear)){  
		return 1;  
	}  
	else if(Date.parse(OneMonth+"/"+OneDay+"/"+OneYear) <  
	Date.parse(TwoMonth+"/"+TwoDay+"/"+TwoYear)){  
		return -1;  
	}else{
		return 0;
	}
}
function formatDate(date){
    var d = new Date(date.getFullYear(),date.getMonth(),1);
//	d.setYear(date.getFullYear());
//	d.setMonth(date.getMonth());
//	d.setDate(1);
	return d;
}
function jsonDateToDateTime(jsonDate) {
    var reg = /\/Date\((-?[0-9]+)(\+[0-9]+)?\)\//g;
    if (reg.test(jsonDate)) {
        var ticks = Number(jsonDate.replace(reg, '$1'));
        if (!isNaN(ticks)) {
            jsonDate = new Date(Number(jsonDate.replace(reg, '$1')));
        } else {
            jsonDate = new Date();
        }
    }
    return jsonDate;
}

var TourIdArr = new Array();
var TourChildArr = new Array();
var SingleCalendar = {
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
        //当前子团信息
        CC: [],
        //日历当前月表格jQuery对象
        JCO: null,
        //是否登陆
        isLogin: false,
        //子团集合
        Childrens: [],
        //端口(日历上的查看详细报价文件在网站首页项目中)
        stringPort: ""
    },
    _initDate: function(currentDate) {
        this.maxDate = formatDate(this.option.currentDate);
        this.currentDate = formatDate(this.option.currentDate);
        this.SCD = formatDate(this.option.currentDate);
        this.SND = formatDate(this.option.currentDate);
    },
    /*外部调用方法 */
    //初始化日历
    initCalendar: function(option) {
        this.option = $.extend({
            containerId: "divSingleCalendar",
            width: "400px",
            areatype: 1,  //线路区域类型 
            srcElement: null,
            TourId: 0, //模板团ID
            AddOrder: null  //预定方法
        }, option || {});
        this.option.currentDate=formatDate(this.option.currentDate);
        this.option.firstMonthDate=formatDate(this.option.firstMonthDate);
        this._initDate(this.option.currentDate);

        var TourId = this.option.TourId;
        var strUrl = this.config.stringPort + "/TourManage/GetTourChildrens.ashx" + "?RouteId=" + this.option.TourId;
        var Child = [];
        var self = this;

        var oSrc = $(self.option.srcElement);
        var offset = oSrc.offset();
        var srcElementWidth = oSrc.outerWidth(true);
        var srcElementHeight = oSrc.outerHeight(true);
        var top = offset.top + srcElementHeight;
        var left = offset.left;
        $("<div id='" + self.option.containerId + "' style='position:absolute;top:-1000px;left:-1000px;width:" + self.option.width + "'></div>").appendTo("body");
        $("#" + self.option.containerId).css({
            top: top,
            left: left
        });

        $("#" + self.option.containerId).html('<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3" style="background-color:white;font-size:12px;;text-align:center;"><tr><td height="20px">正在加载中....</td></tr></table>');

        var intCount = 0;
        var fun = function(data) {
            self.config.Childrens = data;


            var stratrLeaveMonth = "00";

            for (var j = 0; j < self.config.Childrens.length; j++) {
                var strMonth = self.config.Childrens[j]["LeaveDate"].split("-")[1];
                var TourState = self.config.Childrens[j]["TourState"];
                if (TourState == "1") {
                    if (self.config.Childrens[j]["LeaveDate"].split("-")[0] == self.option.currentDate.getFullYear()) {
                        stratrLeaveMonth = strMonth;
                    } else {
                        stratrLeaveMonth = parseInt(strMonth[0] == "0" ? strMonth[1] : strMonth) + 12;
                    }
                    break;
                }
            }
            var intstartMonth = stratrLeaveMonth[0] == "0" ? stratrLeaveMonth[1] : stratrLeaveMonth;
            intCount = intstartMonth - (self.option.currentDate.getMonth() + 1);
            if (intCount > 0) {
//                if (self.option.areatype == 1)   // 国际线
//                {
                    self.currentDate.setMonth((self.option.currentDate.getMonth() + intCount));
                    self.maxDate = copyDate(self.currentDate);
                    self.maxDate.setMonth(self.currentDate.getMonth() + (5 - intCount));
//                } else {
//                    self.maxDate = copyDate(self.option.currentDate);
//                    self.maxDate.setMonth(self.option.currentDate.getMonth() + (2 - intCount));
//                    self.currentDate.setMonth((self.option.currentDate.getMonth() + intCount));
//                }

            } else {
//                if (self.option.areatype == 1)   // 国际线
//                {
                    self.currentDate = self.option.currentDate;
                    self.maxDate = copyDate(self.option.currentDate);
                    self.maxDate.setMonth(self.maxDate.getMonth() + 5);
//                } else {
//                    self.currentDate = self.option.currentDate;
//                    self.maxDate = copyDate(self.option.currentDate);
//                    self.maxDate.setMonth(self.maxDate.getMonth() + 1);
//                }
            }

            self.SCD = self.currentDate;
            self._initCalendarBasic();
            self._createCalendarContainer();


            //初始化当前月日历
            self.createCalendarMonth();
            self.createCalendarDays();

            self.MouseoutOrOnMessage();

            createFrame($("#" + self.option.containerId)[0]);
        };
        var Index = $.inArray(TourId, TourIdArr);
        if (Index == -1) { //未加载过
            $.ajax({
                cache: false,
                dataType: "jsonp",
                url: strUrl,
                success: function(data) {
                    Child = data;
                    TourIdArr.push(TourId);
                    TourChildArr.push(Child);
                    fun(data);
                }

            });
        } else {
            fun(TourChildArr[Index]);
        }

    },
    updateCalendar: function(option) {
        this.option = $.extend(this.option, option || {});

        this.SCD = this.option.firstMonthDate;

        this._initCalendarBasic();

        //初始化当前月日历
        this.createCalendarMonth();
        this.createCalendarDays();

        this.MouseoutOrOnMessage();
    },
    /*以下为内部私有方法 */
    html: '<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3" style="background-color:white;font-size:12px;;text-align:center;">' +
    '<tr>' +
      '<td colspan="7"><table width="100%" border="0" cellspacing="0" bordercolor="#ffffff" cellpadding="0">' +
          '<tr>' +
            '<td align="center"><span  style="float:left;">' +
              '<select id="selMonths">' +
              '</select>' +
              '</span><a href="javascript:void(0);" style="float:right;" class="close">关闭</a></td>' +
         '</tr>' +
        '</table></td>' +
    '</tr>' +
    '<tr>' +
     ' <td colspan="7" id="thisMonthCalendar">正在加载中....</td>' +
    '</tr>' +
  '</table>',
    p: parent,
    //页面元素配置信息
    elements: {
        //生成的当前月表格id
        cMTable: 'cMonthTable',
        //存放当前月表格容器id
        cMTd: "thisMonthCalendar"
    },

    //初始化日历基础数据
    _initCalendarBasic: function() {
        this.SDConfig.CY = this.SCD.getFullYear();
        this.SDConfig.CM = this.SCD.getMonth() + 1;
        this.SDConfig.CD = this.SCD.getDate();
        this.SDConfig.CDays = new Date(this.SDConfig.CY, this.SDConfig.CM, 0).getDate();
    },
    /*private*/
    _createCalendarContainer: function() {
        //初始化日历位置
        var oSrc = $(this.option.srcElement);
        var offset = oSrc.offset();
        var srcElementWidth = oSrc.outerWidth(true);
        var srcElementHeight = oSrc.outerHeight(true);
        var top = offset.top + srcElementHeight;
        var left = offset.left;
        if (!document.getElementById(this.option.containerId)) {
            $("<div id='" + this.option.containerId + "' style='position:absolute;top:-1000px;left:-1000px;width:" + this.option.width + "'></div>").appendTo("body");
            $("#" + this.option.containerId).css({
                top: top,
                left: left
            });
        } else {
            $("#" + this.option.containerId).css({
                top: top,
                left: left
            });
        }

        $("#" + this.option.containerId).html(this.html);

        var self = this;
        $("#" + this.option.containerId).find("a[class='close']").click(function() {
            $("#" + self.option.containerId).css({
                top: "-1000px",
                left: "-1000px"
            });
            closeFrame();
            return false;
        });

        var self = this;
        var cDate = copyDate(self.currentDate);
        var mDate = copyDate(self.maxDate);
        var oSelect = $("#selMonths");
        var optionHtml = "";
        while (compareDate(cDate, mDate) == -1 || compareDate(cDate, mDate) == 0) {
            var year = cDate.getFullYear(), month = cDate.getMonth() + 1;
            var value = year + "," + month;
            optionHtml += "<option value='" + value + "'>" + year + "年" + month + "月</option>";
            cDate.setMonth(cDate.getMonth() + 1);
        }
        oSelect.html(optionHtml);
        oSelect.change(function() {
            var v = $(this).val();
            var arr = v.split(",");
            var a = new Date(arr[0], arr[1], 0);
            self.updateCalendar({
                firstMonthDate: a
            });
        });
    },
    //创建日历表格
    createCalendarMonth: function() {
        var myself = this;
        var tableId = this.elements.cMTable;
        var s = [];
        s.push('<table id="' + tableId + '" width="100%"  cellpadding="0" border="1"  bordercolor="#E3E3E3" cellspacing="1" bgcolor="#ffffff">');
        s.push("<tr>");
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>日</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>一</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>二</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>三</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>四</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>五</strong></th>');
        s.push('<th width="55" align="center" bgcolor="#E3D596"><strong>六</strong></th>');
        s.push('</tr>');
        s.push('</table>');

        $("#" + this.elements.cMTd).html(s.join(''));

        this.config.JCO = $("#" + tableId);
    },
    //创建前面的空白天数
    createStartEmptyDays: function(monthFirstDayOfWeek) {
        var s = [];
        for (var i = 0; i < monthFirstDayOfWeek; i++) {
            s.push('<td bgcolor="#FFFFFF"></td>')
        }
        return s.join('');
    },
    //创建后面的空白天数
    createEndEmptyDays: function(days) {
        if (days == 7) return;
        var s = [];
        for (var i = 0; i < days; i++) {
            s.push('<td bgcolor="#FFFFFF"></td>')
        }
        s.push('</tr>')
        return s.join('');
    },
    //创建日历日期信息
    createCalendarDays: function() {
        var myself = this;
        var obj = this.config.JCO;
        var sd = 1;
        var fd = this.SDConfig.CDays;
        var sdOfWeek = new Date(this.SDConfig.CY, this.SDConfig.CM - 1, 1).getDay();
        var s = [];
        var i = 1;

        do {
            if ((i) % (7) == 1) {

                s.push('<tr>');
            }

            if (i == 1) {
                s.push(this.createStartEmptyDays(sdOfWeek));
                i = i + sdOfWeek;
            }
            var thisDate = new Date(this.SDConfig.CY, this.SDConfig.CM, sd);
            var thisY = this.SDConfig.CY;
            var thisM = this.SDConfig.CM;

            if (thisM <= 9) {
                thisM = "0" + thisM;
            }
            var thisD = sd <= 9 ? "0" + sd : sd;
            var thisDate = thisY + "-" + thisM + "-" + thisD;

            var tdTourstr = '<td height="39" bgcolor="#ffffff" align="center" ><font class="sizdate">' + sd + '</font></td>';
            for (var j = 0; j < this.config.Childrens.length; j++) {
                var LeaveDate = this.config.Childrens[j]["LeaveDate"];
                var TourId = this.config.Childrens[j]["TourId"];
                var TourState = this.config.Childrens[j]["PowderTourStatus"];
                var NoLimit=this.config.Childrens[j]["IsLimit"];
                var RemnantNumber=this.config.Childrens[j]["MoreThan"];
                var TourNum=this.config.Childrens[j]["TourNum"];
                if(NoLimit==true)
                {
                     RemnantNumber =TourNum;
                }
                if (thisDate == LeaveDate) {
                    var strColor = "#c3c0b1";
                    //停收/客满 颜色
                    if (TourState == 0 || TourState == 3) {
                        strColor = "#9be38e";
                    }
                    if (TourState == 2 || TourState == 4) {
                        strColor = "#f29f94";
                    }
                    var strPriceList = "";
                    //显示信息
                    var Price = this.config.Childrens[j]["RetailAdultPrice"];
                    var strleaveDate = this.SDConfig.CM + "," + sd;
                    var stringPrice = '<span>' + sd + '</span><br /><a target="_blank" title="预定此团队" rel="addorder" href="javascript:addorder('+TourId+');" id="' + TourId + '" >剩' + RemnantNumber + '</a><br /><a style="padding:1px" rel="price" href="javascript:;" tourid="' + TourId + '" leaveDate=' + strleaveDate + ' RemnantNumber=' + RemnantNumber + ' title="">' + Price + '元起</a><div style="display:none"></div>';
                    //停收
                    if (TourState == 2) {
                        stringPrice = '<font class="sizdate">' + sd + '</font><br><font >停收</font><br><span>' + Price + '元起</span>'
                    }
                    //客满
                    if (TourState == 1) {
                        stringPrice = '<font class="sizdate">' + sd + '</font><br><font >客满</font><br><span>' + Price + '元起</span>'
                    }
                    tdTourstr = '<td height="39" "' + strPriceList + '" width="55" bgcolor="' + strColor + '" align="center">' + stringPrice + '</td>';
                    break;
                }
            }
            s.push(tdTourstr);

            if ((i) % (7) == 0) { s.push('</tr>'); }

            sd++;
            i++
        } while (sd <= fd)

        s.push(this.createEndEmptyDays(7 - (i - 1) % 7));
        obj.append(s.join(''));
        var self = this;
        obj.find("a[rel='addorder']").click(function() {
            if (self.option.AddOrder) {
                self.option.AddOrder($(this).attr("id"));
            }
            return false;
        });

    },
    //查看子团的报价明细
    MouseoutOrOnMessage: function() {
        var stringPort = this.config.stringPort;
        var self = this;
        if (this.config.isLogin == "True") {
            $("#" + this.option.containerId).find("a[rel='price']").mouseover(function() {

                var oLink = this;
                $(oLink).addClass("show");
                var html = $(oLink).siblings("div").eq(0).html();
                if (html != "") {
                    var classN = $(oLink).attr("class");
                    if (classN == "show") {
                        wsug(oLink, html);
                    }
                } else {
                    var tourId = $(oLink).attr("tourid");
                    var RemnantNumber = $(oLink).attr("RemnantNumber");
                    var leaveDate = $(oLink).attr("leaveDate");
                    var strUrl = stringPort + "/TourManage/GetTourPriceInfo.ashx" + "?RouteId=" + tourId + "&callback=?" + "&RemnantNumber=" + RemnantNumber + "&leaveDate=" + leaveDate;
                    var classN = $(oLink).attr("class");

                    if (classN == "show") {
                        $.ajax({
                            type: "GET",
                            dataType: 'json',
                            url: strUrl,
                            cache: false,
                            success: function(data) {
                                $(oLink).siblings("div").eq(0).html(data[0].PriceInfo);
                                if ($(oLink).attr("class") == "show") {
                                    wsug(oLink, data[0].PriceInfo);
                                }
                            }
                        });
                    }
                }
            }).mouseout(function() {
                $(this).removeClass("show");
                wsug(this, 0);
            });
        }
    },
    CloseCalendar: function() {
        $("#" + this.option.containerId).css({
            top: "-1000px",
            left: "-1000px"
        });
        closeFrame();
        return false;
    }
};

function closeFrame() {
    var oFrame = document.getElementById("pframe");
    if (oFrame) {
        oFrame.style.display = "none";
    }
}
function createFrame(oDiv) {
    var isIe6 = false;
    if ($.browser.msie && $.browser.version == "6.0") {
        isIe6 = true;
    }
    if (isIe6 == false) { return; }
    var oFrame = document.getElementById("pframe");
    if (oFrame == null) {
        oFrame = document.createElement("iframe");
        oFrame.id = "pframe"
        oFrame.style.cssText = "border:none;display:none;position:absolute;";
        document.body.appendChild(oFrame);
    }
    var offset = $(oDiv).offset();
    oDiv.style.zIndex = 500;
    oFrame.style.top = offset.top + 2 + "px";
    oFrame.style.left = offset.left + "px";
    oFrame.style.width = $(oDiv).width() + "px";
    oFrame.style.height = $(oDiv).height() + "px";
    oFrame.style.zIndex = 499;
    oFrame.style.display = "block";
}

