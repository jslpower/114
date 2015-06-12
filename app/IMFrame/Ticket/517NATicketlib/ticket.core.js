/*
汪奇志 2010-03-14 517NATicket
*/
var searchFromValue = { city: '', lke: '' }; var searchToValue = { city: '', lke: '' }; var searchTime = '';

var ticketLKE = {
    cacheData: new Array(),
    config: { isInitTopCitys: false, setHotType: 1, hotCiystsIsShow: false, dateTimeControlId: 'dpTakeOffDate_dateTextBox' },
    //出发、抵达城市输入框自动完成功能 对jquery.autocomplete.js做了一点点修改
    initAutoComplete: function() {
        //出发城市
        $("#txtFromCity").autocomplete(ticketLKEdata, {
            minChars: 1,
            width: 117,
            matchContains: "text",
            autoFill: true,
            formatItem: function(row, i, max) {
                return row.cName;
            },
            formatMatch: function(row, i, max) {
                //return row.cName + "," + row.shortName + "," + row.eName;
                return row.shortName + "," + row.eName + "," + row.cName + "," + row.lke + "|" + row.lke;
            },
            formatResult: function(row) {
                return row.cName;
            },
            formatHidResult: function(row) {
                return { hidInputId: "txtFromCityLKE", value: row.lke };
            }
        });
        //抵达城市
        $("#txtToCity").autocomplete(ticketLKEdata, {
            minChars: 1,
            width: 117,
            matchContains: "text",
            autoFill: true,
            formatItem: function(row, i, max) {
                return row.cName;
            },
            formatMatch: function(row, i, max) {
                //return row.cName + "," + row.shortName + "," + row.eName;
                return row.shortName + "," + row.eName + "," + row.cName + "," + row.lke + "|" + row.lke;
            },
            formatResult: function(row) {
                return row.cName;
            },
            formatHidResult: function(row) {
                return { hidInputId: "txtToCityLKE", value: row.lke };
            }
        });

        $("#txtFromCity").bind("blur", function() { ticketLKE.setDefaultValue("txtFromCity"); });
        $("#txtToCity").bind("blur", function() { ticketLKE.setDefaultValue("txtToCity"); });

        $("#txtFromCity").val(searchFromValue.city);
        $("#txtFromCityLKE").val(searchFromValue.lke);
        $("#txtToCity").val(searchToValue.city);
        $("#txtToCityLKE").val(searchToValue.lke);

        if (searchTime != '') {
            $("#" + this.config.dateTimeControlId).val(searchTime);
        }
    },
    //表单验证
    webFormSubmit: function() {
        var fromValue = { city: $.trim($("#txtFromCity").val()), lke: $("#txtFromCityLKE").val() };
        var toValue = { city: $.trim($("#txtToCity").val()), lke: $("#txtToCityLKE").val() };
        var leaveTime = $("#" + this.config.dateTimeControlId).val();
        var isFrom = false;
        var isTo = false;
        if (fromValue.lke == "") { alert("请输入出发城市"); return false; }
        if (toValue.lke == "") { alert("请输入目的城市"); return false; }
        if (leaveTime == "") { alert("请输入时间"); return false; }

        for (var i = 0; i < ticketLKEdata.length; i++) {
            if (ticketLKEdata[i].lke == fromValue.lke) {
                if (ticketLKEdata[i].cName == fromValue.city) { isFrom = true; }
            }

            if (ticketLKEdata[i].lke == toValue.lke) {
                if (ticketLKEdata[i].cName == toValue.city) { isTo = true; }
            }
        }

        if (!isFrom) { alert("请输入正确的出发城市"); return false; }
        if (!isTo) { alert("请输入正确的目的城市"); return false; }

        return true;
    },
    //显示热点城市
    showHotCitys: function(setHotType) {
        if (!this.config.isInitTopCitys) { this.initHotCitys(); }
        if (!this.config.hotCiystsIsShow) { $("#divHotCitys").show(); }
        var elementId = (setHotType == 1 ? 'txtFromCity' : 'txtToCity');
        var jObjInput = $("#" + elementId);
        var offset = jObjInput.offset();
        var left = offset.left - 58;
        var top = offset.top + jObjInput.height();
        var jObj = $("#divHotCitys");
        jObj.css({ 'left': left, 'top': top });

        this.config.hotCiystsIsShow = true;
        this.config.setHotType = setHotType;
    },
    //隐藏热点城市
    hideHotCitys: function() {
        $("#divHotCitys").hide();
        this.config.hotCiystsIsShow = false;
    },
    //加载热点城市
    initHotCitys: function() {
        var hotCitysCount = 0;
        var s = '<li class="li1"><b><span style="color: #ff6600; margin-left:4px;">TOP<span id="spanHotCitysCount">0</span>热点城市</span><!--(直接输入可搜索' + ticketLKEdata.length + '个城市)--></b></li>';
        s += '<li class="li2"><a href="javascript:void(0)" onclick="ticketLKE.hideHotCitys()"><b>×</b></a></li>';
        s += '<li class="li3"></li>';
        var jObj = $("#ulHotCitys");
        jObj.append(s);
        for (var i = 0; i < ticketLKEdata.length; i++) {
            if (ticketLKEdata[i].isHot) {
                s = '<li class="list"><a href="javascript:void(0)" onclick="ticketLKE.setHotCity(\'' + ticketLKEdata[i].lke + '\',\'' + ticketLKEdata[i].cName + '\')">' + ticketLKEdata[i].cName + '</a></li>';
                jObj.append(s);
                hotCitysCount++;
            }
        }

        if (hotCitysCount == 0) {
            s = '<li class="liNotHotCitys"><b style="margin-left:4px;">暂时没有设置任何热点城市</b></li>';
            jObj.append(s);
        }

        s = '<li class="li3"></li>';
        jObj.append(s);
        $("#spanHotCitysCount").html(hotCitysCount + '');
        this.config.isInitTopCitys = true;
    },
    //设置热点城市
    setHotCity: function(cityLKE, cName) {
        if (this.config.setHotType == 1) {
            $("#txtFromCityLKE").val(cityLKE);
            $("#txtFromCity").val(cName);
        } else {
            $("#txtToCityLKE").val(cityLKE);
            $("#txtToCity").val(cName);
        }

        this.hideHotCitys();
    },
    //鼠标离开设定输入框值
    setDefaultValue: function(elementId) {
        var obj = $("#" + elementId);
        var objV = obj.val().toLowerCase();
        if ($.trim(objV).length < 1) {
            obj.next().val('');
            return;
        }
        if (this.cacheData.length > 0) {
            var arr1 = this.cacheData[0].split('|')

            if (arr1.length != 2) { return; }

            var arr2 = arr1[0].toLowerCase().split(",");

            if (arr2.length != 4) { return; }

            var isMatch = false;
            for (var i = 0; i < arr2.length; i++) {
                if (arr2[i].indexOf(objV) == 0) {
                    isMatch = true;
                    break;
                }
            }

            if (isMatch) {
                var obj = $("#" + elementId);
                obj.val(arr2[2]);
                obj.next().val(arr1[1]);
            }

            this.cacheData = new Array();
        }
    },
    //异步提交机票信息查询数据
    ajaxPost: function() {
        //表单验证
        var formValidateResult = this.webFormSubmit();
        if (!formValidateResult) { return false; }
        //取表单值
        var formValue = { fcity: $.trim($("#txtFromCity").val()), flke: $("#txtFromCityLKE").val(), tcity: $.trim($("#txtToCity").val()), tlke: $("#txtToCityLKE").val(), stime: $("#" + this.config.dateTimeControlId).val() };
        //提交数据
        $.ajax({
            type: "POST",
            url: "/TourAgency/Ticket/DefaultPost.ashx",
            data: formValue,
            dataType: 'json',
            success: function(result) {
                if (result.IsSucceed) {
                    $("#imgTicket").attr("href", result.RedirectUrl);
                    $("#imgTicket").attr("target", "_blank");
                } else {
                    alert(result.ErrorMsg);
                }
            }
        });
    },
	// 点击页面任何地方关闭div
	hiddenDiv: function()
	{
		$(document).click(function(e){
		    if(document.all)e = event;
			var ele = e.target == undefined ? event.srcElement : e.target;
			if(ele.id != "fromCity" && ele.id != "toCity")
			{
				ticketLKE.hideHotCitys();
			}
		});
	}
};
