<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="IMFrame.WEB.IM.RouteAgency.Main" %>

<%@ Register Src="../WebControls/IMTop.ascx" TagName="IMTop" TagPrefix="uc2" %>
<%@ Register Src="../WebControls/SubAccount.ascx" TagName="SubAccount" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批发商</title>
    <style type="text/css">
        /*FENYE*/DIV.digg
        {
            padding-right: 3px;
            padding-left: 3px;
            padding-bottom: 3px;
            margin: 3px;
            margin-top: 10px;
            padding-top: 3px;
            text-align: center;
        }
        DIV.digg A
        {
            border: #54A11C 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 2px;
            margin: 2px;
            color: #54A11C;
            padding-top: 2px;
            text-decoration: none;
        }
        DIV.digg A:hover
        {
            border: #54A11C 1px solid;
            background: #54A11C;
            color: #fff;
        }
        DIV.digg A:active
        {
            border: #54A11C 1px solid;
            color: #000;
        }
        DIV.digg SPAN.current
        {
            border: #54A11C 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            font-weight: bold;
            padding-bottom: 2px;
            margin: 2px;
            color: #fff;
            padding-top: 2px;
            background-color: #54A11C;
        }
        DIV.digg SPAN.disabled
        {
            border: #eee 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 2px;
            margin: 2px;
            color: #ddd;
            padding-top: 2px;
        }
        /*end*/BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #2F1004;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        a
        {
            color: #2F1004;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        a.red
        {
            color: #cc0000;
        }
        a.red:visited
        {
            color: #cc0000;
        }
        a.red:hover
        {
            color: #ff0000;
        }
        a.bbl
        {
            color: #000066;
        }
        a.bbl:visited
        {
            color: #000066;
        }
        a.bbl:hover
        {
            color: #ff0000;
        }
        .bar_on_comm
        {
            width: 80px;
            height: 21px;
            float: left;
            border: 1px solid #E7C994;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 100px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(<%=ImageServerUrl %>/IM/images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(<%=ImageServerUrl %>/IM/images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
        .toolbj
        {
            background: url(<%=ImageServerUrl %>/IM/images/toolbj.gif) repeat-x;
            height: 31px;
        }
        .toolbj a
        {
            margin-left: 2px;
        }
        .toolbj a img
        {
            margin-bottom: -3px;
            padding-right: 2px;
        }
        #mainMenu a
        {
            margin: 0;
            list-style-type: none;
        }
        #mainMenu ul li
        {
            margin: 0;
            padding: 0;
            list-style-type: none;
            width: 208px;
        }
        #menuList
        {
            margin: 0px;
            padding: 0px;
        }
        #menuList ul, li
        {
            margin: 0px;
            padding: 0px;
            float: left;
            text-align: left;
        }
        li.menubarname ul
        {
            width: 208px;
            border-left: 1px solid #dfdfdf;
        }
        li.menubarname a
        {
            color: #0A356D;
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
            margin: 0;
            padding: 6px 0px 4px 0px;
            list-style-type: none;
            background: url(<%=ImageServerUrl %>/IM/images/leftmenubar.gif) no-repeat;
            display: block;
        }
        li.menubarname a:visited
        {
            color: #0A356D;
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
            margin: 0;
            padding: 6px 0px 4px 0px;
            list-style-type: none;
            background: url(<%=ImageServerUrl %>/IM/images/leftmenubar.gif) no-repeat;
            display: block;
        }
        li.menubarname a:hover
        {
            background-color: #f00; /*text-decoration: underline;*/
            background: url(<%=ImageServerUrl %>/IM/images/leftmenubar.gif) no-repeat;
            list-style-type: none;
        }
        li.menubarname a.lineon
        {
            color: #f00;
            border: 0px solid #D37130; /*background: #FFEBCC;*/
        }
        li.menubarname a.lineon:visited
        {
            color: #f00;
            border: 0px solid #D37130;
            background: #FFEBCC;
        }
        li.menubarname a img
        {
            margin-left: 10px;
            margin-right: 5px;
        }
        .menubarname li a
        {
            color: #0A356D;
            font-size: 12px;
            font-weight: normal;
            text-decoration: none;
            height: 25px;
            padding-top: 5px;
            background: none;
            border-bottom: 0px solid #C3C9DE;
            width: 99%;
            display: block;
        }
        .menubarname li a:visited
        {
            color: #0A356D;
            font-size: 12px;
            font-weight: normal;
            text-decoration: none;
            height: 25px;
            padding-top: 5px;
            background: none;
            border-bottom: 1px solid #C3C9DE;
            width: 99%;
            display: block;
        }
        .menubarname li a:hover
        {
            color: #f00;
            font-size: 12px;
            font-weight: normal;
            text-decoration: none;
            height: 25px;
            padding-top: 5px;
            background: #FFE2B4;
            text-decoration: none;
            width: 99%;
            display: block;
        }
        .menu, .submenu
        {
            display: none;
            margin: 0px;
            padding: 0px;
        }
        .menu li, .submenu li
        {
            list-style: none outside;
        }
        li.actuator a
        {
            color: #5998d5;
            text-decoration: none;
            height: 14px;
            margin: 0;
            border-bottom: 1px solid #dfdfdf;
            border-top-color: #dfdfdf;
        }
    </style>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("Loading") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("IMCommon") %>"></script>

    <script language="javascript" type="text/javascript">
        $(function() {
            $("#divOpenMQ").hide();
            $("#OrderDiv").find("a").each(function(i) {
                $("#OrderDiv").find("a").eq(i).click(function() {
                    $("#OrderDiv").find("a").each(function() {
                        $(this).css({ "color": "#2F1004" });
                    });
                    if ($.trim($(this).text()) == "刷新") {
                        $("#OrderDiv").find("a").eq(1).css("color", "#cc0000");
                    } else {
                        $(this).css("color", "#cc0000");
                    }
                });
            });
        })
        function showMQVIP(obj) {

            $("#divOpenMQ").hide();
            $("#divSubAccount").show();

        }
        function CheckMuen(obj) {
            $("#order,#tour").css({ "font-size": "12px", "color": "#2F1004", "width": "106px" }).hover(
                  function() {
                      $(this).css("color", "#cc0000");
                  },
                  function() {
                  }
                );
            $(obj).css({ "font-size": "14px", "color": "#cc0000", "width": "105px" });
            var OperatorId = "<%= OperatorId %>";  //默认为当前用户的id
            var divOperator = document.getElementById("divOperator");   //子帐号的div 
            if (divOperator != null) {
                divOperator.style.display = "block";
            }

            //子帐号的下拉框
            var SubAccount1_ddlOperator = document.getElementById("SubAccount1_ddlOperator");
            if (SubAccount1_ddlOperator != null)   //若存在子帐号的时候,默认取下拉框中的选择的项的值
                OperatorId = SubAccount1_ddlOperator.value;

            $("#MuenTable").find(".MuenA").each(function() {
                if (this == obj) {
                    this.parentNode.style.background = 'url(<%=ImageServerUrl %>/IM/images/ztopbttttcs2.gif) no-repeat center';
                    this.style.fontWeight = 'bold';
                    if (this.id == "order") {
                        $("#OrderDiv").find("a").each(function() {
                            $(this).css({ "color": "#2F1004" });
                        });
                        $("#OrderDiv").find("a").eq(1).css("color", "#cc0000");
                        //if (SubAccount1_ddlOperator != null) {
                        //SubAccount1_ddlOperator.onchange = function() {
                        //CheckMuen(document.getElementById('order'))
                        //};
                        //}
                        $("#divSubAccount").hide();
                        document.getElementById("OrderDiv").style.display = 'block';
                        document.getElementById("TourDiv").style.display = 'none';
                        var OrderType = document.getElementById("hidOrderType").value;
                        $("#divOpenMQ").hide();
                        $("#main").html("正在加载...");
                        $.ajax
                        ({
                            url: "TourOrder/Default.aspx?userid=" + OperatorId,
                            cache: false,
                            success: function(html) {
                                $("#main").html(html);
                            }
                        });
                    }
                    else if (this.id == "tour") {
                        $("#divSubAccount").show();
                        if (SubAccount1_ddlOperator != null) {
                            SubAccount1_ddlOperator.onchange = function() {
                                CheckMuen(document.getElementById('tour'))
                            };
                        }
                        document.getElementById("OrderDiv").style.display = 'none';
                        document.getElementById("TourDiv").style.display = 'block';
                        $("#divOpenMQ").hide();
                        $("#main").html("正在加载...");
                        $.ajax
                        ({
                            url: "TourManger/Default.aspx?OperatorId=" + OperatorId,
                            cache: false,
                            dataType: "html",
                            success: function(html) {
                                $("#main").html(html);
                            }
                        });
                    }
                }
                else {

                    this.style.fontWeight = 'bold';

                    this.parentNode.style.background = 'url(<%=ImageServerUrl %>/IM/images/ztopbttttcs.gif) no-repeat center';
                }
            });
        }

        //切换到消息设置
        function MessageReminder() {

            var divOperator = document.getElementById("divOperator");
            if (divOperator != null) {
                divOperator.style.display = "none";
            }
            $("#main").html("正在加载...");
            $.ajax
            ({
                url: "TourManger/SetUser.aspx?rad=" + Math.random(),
                cache: false,
                success: function(html) {
                    $("#main").html(html);
                    SetMQMsgUser.Load();
                }
            });
        }

    </script>

    <script type="text/javascript">
        //设置结束
        function SetEnd() {
            window.location.href = "/RouteAgency/Main.aspx";
        }

        var SetMQMsgUser = {
            Config: {
                //当前公司ID
                CompanyId: "",
                //所有的用户信息列表,数据为:用户ID,用户名称
                UserList: [],
                //线路区域信息列表,数据为:区域ID,区域名称
                AreaList: [],
                //已选中的用户列表,数据为:用户ID,区域ID
                SelectedUserList: []
            },
            //加载初始化数据
            Load: function() {
                if (this.Config.AreaList != null && this.Config.AreaList.length > 0 && this.Config.CompanyId != "" && this.Config.UserList != null && this.Config.UserList.length > 0) {
                    for (var i = 0; i < this.Config.AreaList.length; i++) {
                        var html = "";
                        for (var j = 0; j < this.Config.UserList.length; j++) {
                            //input的id规则:ck_AreaId_UserId
                            var name = this.Config.UserList[j]["ContactInfo"]["ContactName"];
                            if (name == "" || name == null)
                                name = this.Config.UserList[j]["UserName"];
                            html = html + "<li><nobr><input id=\"ck_" + this.Config.AreaList[i]["AreaId"] + "_" + this.Config.UserList[j]["ID"] + "\" type=\"checkbox\" name=\"checkbox\" value=\"checkbox\" onclick=\"SetMQMsgUser.Set(this,'" + this.Config.CompanyId + "', " + this.Config.AreaList[i]["AreaId"] + ",'" + this.Config.UserList[j]["ID"] + "');\" />" + name + "</nobr></nobr></li>";
                        }

                        $("#div_User_" + this.Config.AreaList[i]["AreaId"]).html(html);
                    }

                    this.InitSelected();
                }
            },
            //初始化checked
            InitSelected: function() {
                if (this.Config.SelectedUserList != null && this.Config.SelectedUserList.length > 0) {
                    for (var i = 0; i < this.Config.SelectedUserList.length; i++) {
                        $("#ck_" + this.Config.SelectedUserList[i]["TourAreaId"] + "_" + this.Config.SelectedUserList[i]["OperatorId"]).attr("checked", "true");
                    }
                }
            },
            //设置操作
            Set: function(obj, companyId, areaId, userId) {
                var isCheck = 0;
                var msg = "取消";
                if (obj.checked) {
                    isCheck = 1;
                    msg = "设置";
                }
                $.ajax({ url: "/RouteAgency/TourManger/AjaxSetUser.aspx?CompanyId=" + companyId + "&AreaId=" + areaId + "&UserId=" + userId + "&IsCheck=" + isCheck,
                    cache: false,
                    success: function(html) {
                        if (html == "1")
                            ShowMsg(msg + "成功!", areaId);
                        else
                            ShowMsg(msg + "失败!", areaId);
                    }
                });
            }
        }


        //显示提示信息
        function ShowMsg(msg, areaId) {
            $("#table_msg_" + areaId).find("td").html(msg);
            $("#table_msg_" + areaId).show();
            setTimeout("$('#table_msg_" + areaId + "').hide();", 800);
        }
    </script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            CheckMuen(document.getElementById("tour"));
        });  
    </script>

</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <center>
        <div style="position: relative; width: 215px;">
            <uc2:IMTop ID="IMTop1" PageType="true" runat="server" />
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top" background="<%=ImageServerUrl %><%=ImageServerUrl %>/IM/images/ztopbjcs.gif">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="33" valign="bottom" background="<%=ImageServerUrl %>/IM/images/ztopbjcs.gif">
                                <table id="MuenTable" width="211" height="32" border="0" align="center" cellpadding="0"
                                    cellspacing="0" style="margin-top: 0px;">
                                    <tr>
                                        <td align="center" style="background: url(<%=ImageServerUrl %>/IM/images/ztopbttttcs.gif) no-repeat center;
                                            height: 30px;">
                                            <img src="<%=ImageServerUrl %>/IM/images/group.gif" width="16" height="16" style="margin-bottom: -3px;" /><a
                                                class="MuenA" onclick="javascript:CheckMuen(this)" id="tour" style="cursor: pointer;">
                                                产品管理</a>
                                        </td>
                                        <td align="center" style="height: 26px;">
                                            <asp:PlaceHolder runat="server" ID="plnOrder">
                                                <img src="<%=ImageServerUrl %>/IM/images/shop.gif" width="16" height="16" style="margin-bottom: -3px;" /><a
                                                    class="MuenA" onclick="CheckMuen(this)" id="order" style="cursor: pointer;"> 订单中心</a>
                                            </asp:PlaceHolder>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="TourDiv" style="display: none; margin-top: 0px;">
                    </div>
                </td>
            </tr>
        </table>
        <table width="210" border="0" cellspacing="0" cellpadding="0" style="margin: 0px;
            margin-top: 3px;">
            <tr>
                <td align="left" background="<%=ImageServerUrl %>/IM/images/topzzh.gif" style="padding: 3px 0 0 3px;">
                    <div id="OrderDiv" style="display: none; margin-bottom: 0px;">
                        <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 0px;">
                            <tr>
                                <td height="20" align="left" width="100%">
                                    <a href="javascript:void(0)" style="width: 85px; float: left;" onclick="MessageReminder()">
                                        设置MQ消息提醒</a> <span style="float: left">|</span> <a href="javascript:void(0)" style="width: 50px;
                                            float: left" onclick="CheckMuen(document.getElementById('order'))" onfocus="blur()">
                                            订单中心</a> <span style="float: left">|</span> <a href="javascript:void(0)" style="width: 30px;
                                                float: left" onclick="CheckMuen(document.getElementById('order'))">刷新</a>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divSubAccount">
                        <uc1:SubAccount ID="SubAccount1" runat="server" />
                    </div>
                    <%= strUserManageHTML.ToString() %>
                </td>
            </tr>
        </table>
        <div id="main" style="width: 210px; margin: 0px; vertical-align: top;">
        </div>
    </center>
    <input type="hidden" value="" id="hidOrderType" />
    <span style="display: none;">

        <script language="JavaScript" src="http://s126.cnzz.com/stat.php?id=1125215&amp;web_id=1125215&amp;show=pic"
            charset="gb2312" type="text/javascript"></script>

        &nbsp;<img height="0" alt="" width="0" border="0" src="http://zs7.cnzz.com/stat.htm?id=1125215&amp;r=http%3A//www.tongye114.com/Register/CompanyUserRegister.aspx&amp;lg=zh-cn&amp;ntime=0.67142400 1283503185&amp;repeatip=576&amp;rtime=4&amp;cnzz_eid=84357694-1283137261-&amp;showp=1024x768&amp;st=48&amp;sin=http%3A//www.tongye114.com/Register/CompanyUserRegister.aspx&amp;res=0" />
    </span>
    </form>
</body>
</html>
