<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFriend.aspx.cs" Inherits="IMFrame.OnlineMQ.AddFriend" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加人员</title>
    <style>
        BODY
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
            color: #0E3F70;
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
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
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
        .pagetype span
        {
            display: block;
            float: left;
        }
        .pagetype .pagetypenolink
        {
            border: 1px solid #D5DCE4;
            float: left;
            line-height: 17px;
            background: #E9EEF2;
            margin-left: 3px;
            margin-right: 3px;
            padding: 0 2px 0 2px;
        }
        .pagetype a
        {
            display: inline;
            border: 1px solid #1570CB;
            float: left;
            line-height: 17px;
            background: #E2F1FF url(<%= ImageServerUrl %>/IM/images/pagebj.gif) repeat-x;
            margin-left: 3px;
            margin-right: 3px;
            padding: 0 2px 0 2px;
        }
        .pagetype a:hover
        {
            display: inline;
            border: 1px solid #ff6600;
            float: left;
            line-height: 17px;
            background: #FFE690;
            margin-left: 3px;
            margin-right: 3px;
            padding: 0 2px 0 2px;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("more_JS") %>"></script>

    <script type="text/javascript">
        function CheckFormAdd(im_userid, CustomerCompanyId, CurrUserMQId) {
            //非地接，不设置我的客户
            var boolFals = CheckOrbberInstalled();
            if (boolFals == true) {
                if (im_userid == CurrUserMQId) {
                    alert("不能加自己为好友！");
                    return;
                }

                //是否允许添加MQ好友
                $.ajax({
                    type: "GET",
                    url: "AddFriend.aspx?action=CheckIsAdd&CurrUserMQId=" + CurrUserMQId + "&rnd=" + Math.random(),
                    success: function(result) {
                        if (result == "True") {
                            location.href = 'tongye114://mored.orb/cmd=add?uid=' + im_userid;
                            //添加到我的客户
                            if (CustomerCompanyId != "") {
                                SetCustomer(CustomerCompanyId);
                            }
                            AddFriendCount(CurrUserMQId);
                        }
                        else {
                            alert("注意：您今日加好友已超过<%= TodayMaxFriendCout %>人；\r\n同行反应加好友和发消息过于频繁影响他人正常工作！祝您生意兴隆。");
                        }
                    }
                });
            }
            else {
                alert("您没有安装同业通即时通信系统, 点击 '确定' 后下载安装!");
                location.href = 'http://www.tongye114.com/IM/DownLoad/tongyetong.exe';
            }
        }

        //累加所添加的好友数量
        function AddFriendCount(CurrUserMQId) {
            $.ajax({
                type: "GET",
                url: "AddFriend.aspx?action=AddFriendCount&CurrUserMQId=" + CurrUserMQId + "&rnd=" + Math.random()
            })
        }
        
        function SetCustomer(CustomerCompanyId) {
            $.ajax({
                type: "GET",
                url: "AddFriend.aspx?action=SetCustomer" + "&CompanyId=" + CustomerCompanyId
            })
        }

        function Return() {
            window.location.href = "SearchFriend.aspx" + "<%=urlPars %>";
        }

        function ChangeType() {
            var TypeId = $("#<%=ddlCompanyType.ClientID%>").val();
            var labCityOrUserId = $("#<%=labCity.ClientID %>");
            var lastVal = $("#hidLast").val()
            switch (TypeId) {
                case "0":
                    labCityOrUserId.html("ID号:");
                    if (lastVal == "") {
                        $("#hidLast").val(TypeId);
                    }
                    else {
                        if (lastVal != "0")
                            clearTxtCityName(true);
                    }
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                    labCityOrUserId.html("城市:");
                    if (lastVal == "") {
                        $("#hidLast").val(TypeId);
                    }
                    else {
                        if (lastVal == "0")
                            clearTxtCityName(true);
                    }
                    break;
            }
        }

        function clearTxtCityName(isClear) {
            if (isClear) $("#txtCityName").val('')
        }

        function Query() {
            var typeid = $("#<%= ddlCompanyType.ClientID %>").val();
            var CompanyName = $("#<%= txtCompanyName.ClientID %>").val();
            var ContactName = $("#<%= txtContactName.ClientID %>").val();
            var CityName = $("#<%= txtCityName.ClientID %>").val();
            var url = "/Search/AddFriend.aspx?CompanyName=" + escape(CompanyName) + "&ContactName=" + escape(ContactName) + "&TypeId=" + typeid;
            if (typeid > 0) {
                url = url + "&CityName=" + escape(CityName);
            }
            else {
                url = url + "&UserId=" + escape(CityName);
            }
            window.location.href = url;
        }
		function checkmqid()
		{
			var _delobject = $("#delobject").val();
			var _onlinemq = $("#onlinemq").val();
			if(_delobject=="")
			{
				alert("请输入MQ");
				return;
			}
			var desArr = new Array();
			var mqArr = _onlinemq.split(",");
			var srcArr = _delobject.split(",");
			for(var i=0;i<mqArr.length;i++)
			{
			    var flag = false;
				for(var j=0;j<srcArr.length;j++)
				{
					if(mqArr[i]==srcArr[j]){
						flag=true;
						break;
					}
				}
				if(!flag)
					desArr.push(mqArr[i]);
			}
			$("#onlinemq").val(desArr);
		}
    </script>

</head>
<body oncontextmenu="return false;" scroll="no" style="border: 0px;">
    <form id="form1" runat="server">
    <table width="504" height="320" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top">
                查找方式:<asp:DropDownList ID="ddlCompanyType" runat="server">
                    <asp:ListItem Value="1">找批发商</asp:ListItem>
                    <asp:ListItem Value="2">找组团社</asp:ListItem>
                    <asp:ListItem Value="3">找地接社</asp:ListItem>
                    <asp:ListItem Value="4">找供应商</asp:ListItem>
                    <asp:ListItem Value="0">精确查找</asp:ListItem>
                </asp:DropDownList>
                公司名称:<input type="text" id="txtCompanyName" runat="server" name="CompanyName" size="4" />
                用户姓名:<input type="text" id="txtContactName" runat="server" name="ContactName" size="5" />
                <asp:Label runat="server" ID="labCity" Text="城市:"></asp:Label><input type="text"
                    id="txtCityName" runat="server" name="CityName" size="3" /><input type="button" id="btnQuery"
                        value="搜索" onclick="Query();" />
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="1" cellpadding="2" cellspacing="1" bordercolorlight="#999999"
                            bordercolordark="#ffffff" bgcolor="#ffffff" style="border: 1px solid #cccccc;">
                            <tr>
                                <td width="17%" align="left" bgcolor="#eeeeee">
                                    姓名
                                </td>
                                <td width="30%" align="left" bgcolor="#eeeeee">
                                    单位名称
                                </td>
                                <%--<td width="31%" align="left" bgcolor="#eeeeee">
                                    经营专线
                                </td>--%>
                                <td width="10%" align="left" bgcolor="#eeeeee">
                                    所在城市
                                </td>
                                <!--<td width="10%" align="left" bgcolor="#eeeeee">
                                    加为联系人
                                </td>-->
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Literal ID="LiteralTr" runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="NoData" runat="server">
                    <table width="100%" border="1" cellpadding="2" cellspacing="1" bordercolorlight="#999999"
                        bordercolordark="#ffffff" bgcolor="#ffffff" style="border: 1px solid #cccccc;">
                        <tr>
                            <td width="17%" align="left" bgcolor="#eeeeee">
                                姓名
                            </td>
                            <td width="30%" align="left" bgcolor="#eeeeee">
                                单位名称
                            </td>
                            <%--<td width="31%" align="left" bgcolor="#eeeeee">
                                    经营专线
                                </td>--%>
                            <td width="10%" align="left" bgcolor="#eeeeee">
                                所在城市
                            </td>
                            <!--<td width="10%" align="left" bgcolor="#eeeeee">
                                加为联系人
                            </td>-->
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 100; text-align: center">
                                暂无数据
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="504" border="0" align="center" id="ExproPage" runat="server" class="pagetype"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" style="padding-top: 10px; padding-left: 2px;">
                            <a href="javascript:void(0);" class="pagetypenolink" onclick="Return();return false;">
                                <img src="<%= ImageServerUrl %>/IM/images/z_l.gif" width="16" height="12" style="margin-bottom: -3px;" />返回</a>
                            <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" IsInitJs="false" IsInputPageNum="true"
                                EnDisableBtnClass="pagetypenolink" IsInitBaseCssStyle="false" NextBtnText="下一页"
                                PrevBtnText="上一页" runat="server" PageStyleType="MQSeachFriendStyle"></cc3:ExporPageInfoSelect>
                            <%--<img style="margin-bottom: -3px;" src="<%= ImageServerUrl %>/IM/images/go.gif" />--%>
                        </td>
                    </tr>
                </table>
				<textarea cols=60 rows=2 id=delobject name=delobject></textarea><input type=button value='排除账号' onclick="checkmqid()">
	           <textarea cols=60 rows=10 id=onlinemq name=onlinemq><%=strMQOnline%></textarea>
            </td>
        </tr>
    </table>
    <input id="hidLast" type="hidden" />
    </form>
</body>
</html>
