/*
*
*在线客服-客服端脚本 author:汪奇志
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
        //ServerUrl: "" //一般处理程序服务器地址
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
        if (this.data.info.OId == 0) {
            this.setErrorsMsg('未能与服务器建立连接,请重新进入在线客服系统!');
            return;
        }

        //设置标题
        this.setTitles();
        //提示信息
        this.setTipsMsg('您好,欢迎进入' + this.data.CompanyName + '在线客服系统!');

        //获取在线用户信息
        this.getUList(true);

        //设置获取在线用户定时器
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
        if ($.trim(this.data.info.AcceptId) != "") {
            this.doms.messagesTitle.html('与<span style="color:#0041B6">' + this.data.info.AcceptName + '</span>的交谈');
        } else {
            this.doms.messagesTitle.html('您当前没有任何会话');
        }
    },
    //获取在线用户信息
    getUList: function(isFirst) {
        $.ajax({
            type: "GET",
            url:  "/OlServer/OlServerGetUList.ashx",
            data: "OlId=" + OLS.data.info.OId + "&service=2009",
            dataType: "json",
            cache: false,
            success: function(data) {
                //请求返回的信息包含自己当前正在服务的所有在线用户信息
                OLS.getUList_callback(data, isFirst);
            }
        });
    },
    //成功获取在线用户信息后的处理
    getUList_callback: function(data, isFirst) {
        //当前的在线用户列表
        var nowUList = this.data.uList;

        if (nowUList == null) { nowUList = []; }

        //去除当前的在线用户列表中不在线的用户
        for (var i = 0; i < nowUList.length; i++) {
            //是否要从当前的列表中删除
            var isDelete = true;
            for (var j = 0; j < data.length; j++) {
                if (nowUList[i].OId == data[j].OId) {
                    isDelete = false;
                    break;
                }
            }
            //从当前的列表中删除该在线用户
            if (isDelete) {
                this.delUser(nowUList[i]);
            }
        }

        //设置新的在线用户列表
        this.setUserList(data);
        this.data.uList = data;

        //如果是第一次发起请求
        if (isFirst) {
            //获取消息
            this.getMList(true);
            //设置获取消息定时器
            this.timer.getMList = setInterval(function() { OLS.getMList(false); }, this.data.config.GetUsersInterval * 1000);
        }
    },
    //从用户列表中删除用户
    delUser: function(uInfo) {
        $("#user_" + uInfo.OId).remove();
        OLS.setTipsMsg('提示信息:' + uInfo.OlName + '退出了在线客服系统!');

        if (this.data.info.AcceptId == uInfo.OId) {
            this.data.info.AcceptId = 0;
            this.data.info.AcceptName = 0;
            this.setTitles();
        }

    },
    //设置新的在线用户列表
    setUserList: function(uInfos) {
        $(uInfos).each(function() {
            //用户已经在列表中不做任何操作
            if ($("#user_" + this.OId).size() > 0) { return; }

            var s = '<li id="user_' + this.OId + '" class="online">';
            s += '<span>' + this.OlName + '</span>';
            s += '&nbsp;(<span id="user_' + this.OId + '_messagecount" style="color:red">0</span><span style="font-weight:100">条新消息</span>)';
            s += '</li>';
            OLS.doms.users.append(s);
            var uInfo = this;
            $("#user_" + this.OId).bind('click', function() { OLS.setAcceptInfo(uInfo); });

            if (OLS.data.uList != null) {
                OLS.setTipsMsg('提示信息:' + this.OlName + '进入了在线客服系统!');
            }
        });
    },
    //点击在线用户列表中的用户触发的事件
    setAcceptInfo: function(uInfo) {
        this.data.info.AcceptId = uInfo.OId;
        this.data.info.AcceptName = uInfo.OlName;
        this.setTitles();
        //隐藏所有会话消息
        $('ul[id^="message_"]').hide();
        //显示与当前用户会话的消息
        var ulsId = 'message_' + this.data.info.OId + '_' + uInfo.OId;
        $('ul[id^="' + ulsId + '"]').show();
        $('#user_' + uInfo.OId + '_messagecount').html('0');
    },
    //发送消息
    sendMessage: function() {
        if (!this.data.info.IsOnline) {
            this.setTipsMsg("提示信息:您与服务器的连接已断开,请重新进入在线客服系统");
            return;
        }

        if ($.trim(this.data.info.AcceptId) == "") {
            this.setTipsMsg("提示信息:请选择客户后再发送消息!");
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
            var messageDomId = 'message_' + this.data.mInfo.SendId + '_' + this.data.mInfo.AcceptId + '_' + sendResult.MessageId;
            var s = '<ul id="' + messageDomId + '"><li style="color:blue">';
            //s += this.jsonDateToDateTime(sendResult.SendTime).format("yyyy-mm-dd HH:MM:ss") + "&nbsp;&nbsp;"
            s += OLS.jsonDateToDateTime(sendResult.SendTime).format("yyyy-mm-dd HH:MM:ss", "isoDateTime") + "&nbsp;&nbsp;"
            s += '我对' + this.data.mInfo.AcceptName + '说';
            s += '</li>';
            s += '<li>';
            s += this.data.mInfo.Message;
            s += '</li>';
            s += '</ul>';
            OLS.doms.messages.append(s);
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
            data: "OlId=" + OLS.data.info.OId + "&service=2009",
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

        this.data.info.AcceptId = 0;
        this.data.info.AcceptName = '';
        this.data.info.IsOnline = false;
        this.setTitles();
        this.setTipsMsg("提示信息:成功退出");
        clearInterval(this.timer.getUList);
        clearInterval(this.timer.getMList);
        setTimeout(function() { OLS.exitGoto(); }, 2000);
    },
    //退出后跳转
    exitGoto: function() {
        topTab.remove(topTab.activeTabIndex); //window.top.location.href = "/default.aspx";
    },
    //获取消息
    getMList: function(isFirst) {
        $.ajax({
            type: "POST",
            url: "/OlServer/OlServerGetMList.ashx",
            data: "OlId=" + OLS.data.info.OId + "&lastTime=" + this.jsonDateToDateTime(this.data.lastTime).format("yyyy-mm-dd HH:MM:ss", "isoDateTime"),
            dataType: "json",
            cache: false,
            success: function(data) {
                OLS.getMlist_callback(data);
            }
        });
        if (isFirst) { OLS.data.lastTime = olserverinfos.uInfo.LastSendMessageTime; }
    },
    //成功获取消息后的处理
    getMlist_callback: function(data, isFirst) {
        if (data.length < 1) { return; }

        $(data).each(function() {
            //消息容器的id:message_客服编号_在线编号_消息编号
            var messageDomId = 'message';

            if (this.SendId != OLS.data.info.OId) {//客户发给客服的消息
                messageDomId += '_' + this.AcceptId + '_' + this.SendId + '_' + this.MessageId;
            } else {//客服发给客户的消息
                messageDomId += '_' + this.SendId + '_' + this.AcceptId + '_' + this.MessageId;
            }
            var infoColor = isFirst ? "#A9A9A9" : "blue";
            var s = '<ul id="' + messageDomId + '"><li style="color:' + infoColor + '">';
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

            if ($.trim(OLS.data.info.AcceptId) != "" && OLS.data.info.AcceptId != this.SendId) {
                //不是当前的会话隐藏消息
                $("#" + messageDomId).hide();
            }

            //设置消息计数
            if (this.SendId != OLS.data.info.OId && this.SendId != OLS.data.info.AcceptId) {
                var nCount = parseInt($("#user_" + this.SendId + "_messagecount").html()) + 1;
                $("#user_" + this.SendId + "_messagecount").html(nCount);
            }

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
            //window.open('/olserver/olserversavemessages.ashx?OlId' + this.data.info.OId + '&service=2009', '保存消息记录', "width=50,height=10,toolbar=no,scrollbars=no,menubar=no,left=0,top=0");
            window.open('/olserver/olserversavemessages.ashx?OlId=' + this.data.info.OId + '&service=2009');
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
