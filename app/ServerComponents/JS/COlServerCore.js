/*
*
*在线客服-客户端脚本 author:汪奇志
*
*/
var OLS = {
    //数据
    data: {
        info: null,
        uList: null,
        mList: null,
        config: null,
        mInfo: null,
        lastTime: "", //最后提交时间
        tipsId: "",
        CompanyName: "" //当前公司名称
        //ServerUrl: ""//一般处理程序服务器地址
    },
    //定时器 
    timer: {
        getUList: null,
        getMList: null
    },
    //页面DOM元素的jquery对象
    doms: {
        messagesTitle: null,
        usersTitle: null,
        messages: null,
        users: null,
        txtmsg: null
    },
    //加载 
    init: function() {
        //初始基本数据 
        this.data.config = olserverinfos.configInfo;
        this.data.info = olserverinfos.uInfo;
        this.data.mInfo = olserverinfos.mInfo;
        this.data.CompanyName = olserverinfos.CompanyName;
        //this.data.ServerUrl = olserverinfos.ServerUrl;
        this.data.lastTime = olserverinfos.uInfo.LastSendMessageTime;

        //未登录系统
        if ($.trim(this.data.info.OId) == "") {
            this.setErrorsMsg('未能与服务器建立连接,请重新进入在线客服系统!');
            return;
        }

        //设置标题
        this.setTitles();
        //提示信息
        this.setTipsMsg('您好,欢迎进入' + this.data.CompanyName + '在线客服系统!');

        if ($.trim(this.data.info.AcceptId) == "") {
            this.setTipsMsg('提示信息：请选择您要会话的客服!');
        }
        //获取客服信息
        this.getUList(true);

        //设置获取客服定时器
        this.timer.getUList = setInterval(function() { OLS.getUList(false); }, this.data.config.GetUsersInterval * 1000);

        //设置CTRL+ENTER发送消息
        $(document).keydown(function(event) {
            if (event.ctrlKey && event.keyCode == 13) {
                OLS.sendMessage();
            }
        });

    },
    //设置错误信息 
    setErrorsMsg: function(msg) {
        this.doms.messages.append('<ul><li class="errors">' + msg + '</li></ul>');
    },
    //设置提示信息
    setTipsMsg: function(msg) {
        var tipsId = 'tips_' + this.data.tipsId;
        this.doms.messages.append('<ul><li class="tips" id="' + tipsId + '">' + msg + '</li></ul>');
        this.moveScroll();
        this.data.tipsId++;

        window.setTimeout(function() { OLS.removeTips(tipsId) }, 3000);
    },
    //移除提示信息
    removeTips: function(tipsId) {
        $('#' + tipsId).hide('slow');
    },
    //设置会话标题
    setTitles: function() {
        if (this.data.info.AcceptId != "") {
            this.doms.messagesTitle.html('与' + this.data.CompanyName + '客服<span style="color:#0041B6">' + this.data.info.AcceptName + '</span>的交谈');
        } else {
            this.doms.messagesTitle.html('您当前没有任何会话');
        }
    },
    //获取客服信息
    getUList: function(isFirst) {
        $.ajax({
            type: "GET",
            url: "/OlServer/OlServerGetUList.ashx",
            data: "olid=" + OLS.data.info.OId + "&service=0",
            dataType: "json",
            cache: false,
            success: function(data) {
                //请求返回的信息包含所有客服+当前用户信息
                OLS.getUList_callback(data, isFirst);
            }
        });
    },
    //成功获取客服信息后的处理
    getUList_callback: function(data, isFirst) {
        if (data.length < 1) { return; }
        //获取信息前当前用户信息
        var oinfo = this.data.info;
        //从请求返回的信息中删除最后一个元素并返回该元素,最后一个元素为当前用户信息
        var cinfo = data[data.length - 1];
        $(data).each(function(i) {
            if (i == data.length)
                return false;
            var tmpservice = this;

            //判断客服信息是否加载过
            if ($("#service_" + tmpservice.OId).size() > 0) {
                $("#service_" + tmpservice.OId).unbind("click");
            } else {
                OLS.doms.users.append('<li id="service_' + tmpservice.OId + '"><span style="color:#333333">客服 </span>' + tmpservice.OlName + '</li>');
            }

            //客服不在线且当前用户正在与其会话
            /*
            if (!tmpservice.IsOnline && oinfo.AcceptId == tmpservice.OId) {
            oinfo.AcceptId = 0;
            OLS.setTitles();
            OLS.setTipsMsg('提示信息:与您会话的客服[<b>' + tmpservice.OlName + '</b>]已经离开,请选择其它在线客服');
            }
            */
            //在线与不在线样式处理
            if (this.IsOnline) {
                $("#service_" + tmpservice.OId).attr("class", "online");
            } else {
                $("#service_" + tmpservice.OId).attr("class", "notonline");
            }

            //绑定点击事件
            $("#service_" + tmpservice.OId).bind("click", function() { OLS.setService(tmpservice); });
        });

        if (!cinfo.IsOnline) {
            this.setTipsMsg("提示信息:您与服务器的连接已断开,请重新进入在线客服系统.");
            clearInterval(this.timer.getUList);
            clearInterval(this.timer.getMList);
            this.data.info.IsOnline = false;
            this.data.info.AcceptId = 0;
            this.data.info.AcceptName = '';
            this.setTitles();
        }

        if (isFirst) {
            //获取消息
            this.getMList(true);
            //设置获取消息定时器
            this.timer.getMList = setInterval(function() { OLS.getMList(false); }, this.data.config.GetUsersInterval * 1000);
        }
    },
    //点击客服事件
    setService: function(serviceInfo) {
        if (!this.data.info.IsOnline) { this.setTipsMsg("提示信息:您与服务器的连接已断开,请重新进入在线客服系统."); return; }
        //if (!serviceInfo.IsOnline) { this.setTipsMsg('提示信息:客服[<b>' + serviceInfo.OlName + '</b>]当前不在线上,请选择其它客服.'); return; }
        if (this.data.info.OId == serviceInfo.OId) { return; }


        this.data.info.AcceptId = serviceInfo.OId;
        this.data.info.AcceptName = serviceInfo.OlName;
        this.setTitles();
        this.setTipsMsg('提示信息:您选择了客服[<b>' + serviceInfo.OlName + '</b>]与您会话.')

        $.ajax({
            type: "POST",
            url: "/OlServer/OlServerSetService.ashx",
            data: "olid=" + OLS.data.info.OId + "&serviceid=" + serviceInfo.OId + "&servicename=" + serviceInfo.OlName,
            dataType: "text",
            cache: false
        });
    },
    //发送消息
    sendMessage: function() {
        if (!this.data.info.IsOnline) {
            this.setTipsMsg("提示信息:您与服务器的连接已断开,请重新进入在线客服系统");
            return;
        }

        if ($.trim(this.data.info.AcceptId) == "") {
            this.setTipsMsg("提示信息:请选择客服后再发送消息!");
            return;
        }

        if ($.trim(OLS.doms.txtmsg.val()).length < 1) {
            this.setTipsMsg("提示信息:请输入消息内容!");
            return;
        }

        if ($.trim(this.doms.txtmsg.val()).length >= 50) {
            this.setTipsMsg("提示信息:消息内容不能超过50个字符!");
            return;
        }


        this.data.mInfo.SendId = encodeURIComponent(this.data.info.OId);
        this.data.mInfo.SendName = encodeURIComponent(this.data.info.OlName);
        this.data.mInfo.AcceptId = encodeURIComponent(this.data.info.AcceptId);
        this.data.mInfo.AcceptName = encodeURIComponent(this.data.info.AcceptName);
        this.data.mInfo.Message = encodeURIComponent($.trim(this.doms.txtmsg.val()));
        var currdate = this.data.mInfo.SendTime;
        this.data.mInfo.SendTime = encodeURIComponent(this.data.mInfo.SendTime);

        var msginfo = JSON.stringify(this.data.mInfo);

        this.data.mInfo.SendTime = currdate;
        this.data.mInfo.SendId = this.data.info.OId;
        this.data.mInfo.SendName = this.data.info.OlName;
        this.data.mInfo.AcceptId = this.data.info.AcceptId;
        this.data.mInfo.AcceptName = this.data.info.AcceptName;
        this.data.mInfo.Message = $.trim(this.doms.txtmsg.val());
        $.ajax({
            type: "POST",
            url: "/olserver/OlServerSendMessages.ashx",
            data: "msginfo=" + msginfo,
            dataType: "json",
            cache: false,
            success: function(sendResult) {
                OLS.sendMessage_callback(sendResult);
            }
        });

        this.doms.txtmsg.val('');
    },
    //成功发送消息成功后的处理
    sendMessage_callback: function(sendResult) {
        if (sendResult.IsSuccess && $.trim(sendResult.MessageId) != "") {
            //消息容器的id:message_客服编号_在线编号_消息编号
            var messageDomId = 'message_' + this.data.mInfo.AcceptId + '_' + this.data.mInfo.SendId + '_' + sendResult.MessageId;
            var s = '<ul id="' + messageDomId + '"><li style="color:blue">';
            s += this.jsonDateToDateTime(sendResult.SendTime).format("yyyy-mm-dd HH:MM:ss", "isoDateTime") + "&nbsp;&nbsp;"
            s += '我对' + this.data.mInfo.AcceptName + '说';
            s += '</li>';
            s += '<li>';
            s += this.data.mInfo.Message;
            s += '</li>';
            s += '</ul>';
            this.doms.messages.append(s);

            this.moveScroll();
            OLS.data.lastTime = sendResult.SendTime;
        }
    },
    //移动消息显示区域的滚动条
    moveScroll: function() {
        OLS.doms.messages.get(0).scrollTop = OLS.doms.messages.get(0).scrollHeight;
    },
    //退出
    exit: function() {
        if (!this.data.info.IsOnline) { this.setTipsMsg("提示信息:您与服务器的连接已断开"); this.exitGoto(); return; }

        $.ajax({
            type: "POST",
            url: "/OlServer/OlserverExit.ashx",
            data: "olid=" + OLS.data.info.OId + "&service=0",
            dataType: "text",
            cache: false,
            success: function(data) {
                OLS.exit_callback(parseInt(data));
            }
        });
    },
    //成功发出退出请求后的处理
    exit_callback: function(exitResult) {
        if (exitResult == 0) { return; }

        this.data.info.AcceptId = '';
        this.data.info.AcceptName = '';
        this.data.info.IsOnline = false;
        this.setTitles();
        this.setTipsMsg("提示信息:成功退出");
        clearInterval(this.timer.getUList);
        clearInterval(this.timer.getMList);
        this.exitGoto();
    },
    //退出后跳转
    exitGoto: function() {
        window.close(); //location.href = "/default.aspx";
    },
    //获取消息
    getMList: function(isFirst) {
        $.ajax({
            type: "POST",
            url: "/OlServer/OlServerGetMList.ashx",
            data: "olid=" + OLS.data.info.OId + "&lastTime=" + this.jsonDateToDateTime(OLS.data.lastTime).format("yyyy-mm-dd HH:MM:ss", "isoDateTime"),
            dataType: "json",
            cache: false,
            success: function(data) {
                OLS.getMlist_callback(data, isFirst);
            }
        });
        if (isFirst) { this.data.lastTime = olserverinfos.uInfo.LastSendMessageTime; }
    },
    //成功获取消息后的处理
    getMlist_callback: function(data) {
        if (data.length < 1) { return; }

        $(data).each(function() {
            //消息容器的id:message_客服编号_在线编号_消息编号
            var messageDomId = 'message';

            //客户发给客服的消息
            if (this.SendId == OLS.data.info.OId) {
                messageDomId += '_' + this.AcceptId + '_' + this.SendId + '_' + this.MessageId;
            } else {
                messageDomId += '_' + this.SendId + '_' + this.AcceptId + '_' + this.MessageId;
            }
            var s = '<ul id="' + messageDomId + '"><li style="color:blue">';
            s += OLS.jsonDateToDateTime(this.SendTime).format("yyyy-mm-dd HH:MM:ss", "isoDateTime") + "&nbsp;&nbsp;"
            if (this.SendId == OLS.data.info.OId) {
                s += '我对' + this.AcceptName + '说';
            } else {
                s += this.SendName + '对我说';
            }
            s += '</li>';
            s += '<li>';
            s += this.Message;
            s += '</li>';
            s += '</ul>';
            OLS.doms.messages.append(s);
            OLS.moveScroll();
            OLS.data.lastTime = this.SendTime;
        });
    },
    //JSON日期转换成日期对象
    jsonDateToDateTime: function(jsonDate) {
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
    },
    //保存聊天记录按钮事件 
    saveMessages: function() {
        if ($('ul[id^="message_"]').size() > 0) {
            //window.open('/olserver/olserversavemessages.ashx?olid=' + this.data.info.OlId + '&service=0', '保存消息记录', "width=50,height=10,toolbar=no,scrollbars=no,menubar=no,left=0,top=0");
            window.open('/olserver/olserversavemessages.ashx?olid=' + this.data.info.OId + '&service=0');
        } else {
            this.setTipsMsg("提示信息:暂时还没有任何消息记录用于保存!");
        }
    }
};
//页面加载
$(document).ready(function() {
    OLS.doms.messagesTitle = $("#olmessagestitle");
    OLS.doms.usersTitle = $("#oluserstitle");
    OLS.doms.messages = $("#olmessages");
    OLS.doms.users = $("#olusers");
    OLS.doms.txtmsg = $("#txtmessage");

    //设置会话结束按钮事件
    $("#btnexit").bind('click', function() { OLS.exit(); });
    //设置发送按钮事件
    $("#btnsend").bind("click", function() { OLS.sendMessage(); });
    //设置保存聊天记录按钮事件
    $("#btnsavemessages").bind("click", function() { OLS.saveMessages(); });
   
    OLS.init();
});
