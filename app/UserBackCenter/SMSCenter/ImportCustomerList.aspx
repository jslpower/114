<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ImportCustomerList.aspx.cs" Inherits="UserBackCenter.SMSCenter.ImportCustomerList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="~/usercontrol/ProvinceAndCityList.ascx" TagName="CityList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>从客户列表导入手机号码</title>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function mouseovertr(o) { o.style.backgroundColor = "#D8E5FF"; }
        function mouseouttr(o) { o.style.backgroundColor = ""; }
        var p = parent;
        var iframeId='<%=Request.QueryString["iframeId"] %>';
        //获取勾选的客户数据
        function getSelectMobiles() {
            var pdata = p.$data.getSelectMobiles();
            
            if (pdata == undefined || pdata == 'undefined') {
                pdata = [];
            }

            return pdata;
        }
        //checkbox选中后的操作
        function setChecked(data) {
            var pdata = getSelectMobiles();
            var isPush = true;
            
            for (var i = 0; i < pdata.length; i++) {
                if (pdata[i].customerId == data.customerId) {
                    isPush = false;
                    break;
                }
            }

            if (isPush) {
                pdata.push(data);
                p.$data.setSelectMobiles(pdata);
            }            
        }
        //checkbox取消选中后的操作
        function setUnChecked(data) {
            var pdata = getSelectMobiles();
            pdata=$.grep(pdata, function(v, i) {
                if (v.customerId == data.customerId) return false;
                return true;
            });
            p.$data.setSelectMobiles(pdata);
        }    
        //checkbox click事件
        function chkClick(obj) {
            var objValues = obj.value.split("|");
            var data = { mobile: objValues[0], customerId: objValues[1], isEncrypt: objValues[2], encryptMobile: objValues[3] };
            if (obj.checked) {
                setChecked(data);
            } else {
                setUnChecked(data);
            }
        }
        //全选反选清空按钮事件 type 0:全选 1:反选 清空
        function checkAll(type) {
            var $obj = $("#tblCustomers input[type='checkbox']");
            switch (type) {
                case 0:
                    $obj.each(function() {
                        this.checked = true;
                        chkClick(this);
                    })
                    break;
                case 1:
                    $obj.each(function() {
                        this.checked = (!this.checked);
                        chkClick(this);
                    })
                    break;
                case 2:
                    $obj.each(function() {
                        this.checked = false;
                        chkClick(this);
                    })
                    break;
            }
        }
        //号码唯一
        function uniqueMobile(data) {
            var done = [];            
            if(data.length<1) return done;            
            done.push(data[0]);
            
            for (var i = 1; i < data.length; i++) {            
                var ispush=true;
                for (var j = 0; j < done.length; j++) {
                    if ($.trim(data[i].mobile) == "" || data[i].mobile == done[j].mobile) {
                        ispush = false;
                        break;
                    }
                }

                if (ispush) {
                    done.push(data[i]);
                }
            }

            done = $.grep(done, function(v, i) {
                if ($.trim(v.mobile) == "") return false;
                return true;
            });
            
            return done;
        }
        //确定按钮click事件
        function btnSubmit() {
            var pdata = getSelectMobiles(); 
            //var data = uniqueMobile(pdata);

            //if (data.length < 1) { alert("请选择联系人"); return; }

            p.$data.setConfirmedMobiles(pdata);
            if($("#typeList_0").attr("checked") == true)  //我的客户
                p.SendSMS.GetPhoneNum();
            else if($("#typeList_1").attr("checked") == true)
                p.SendSMS.Get114PhoneNum();
            
            p.Boxy.getIframeDialog(iframeId).hide();
        }
        //page load checkbox checked
        function init() {
             $("#<%= typeList.ClientID %>").find(":radio").each(function(){
                $(this).click(function(){
                    location.href="/SMSCenter/ImportCustomerList.aspx?t="+$(this).val()+"&iframeId=<%=Request.QueryString["iframeId"] %>";
                });
            });
            var pdata = getSelectMobiles();
            if (pdata.length < 1) return;

            for (var i = 0; i < pdata.length; i++) {
                $("#chk" + pdata[i].customerId).attr("checked", true);
            }
        }
        
        //发送所有
        function btnSendAll()
        {            
            var Phone114Count = parseInt("<%= intRecordCount %>");  //要发送的号码个数
            /*
            if(Phone114Count == 0)
            {
                alert("没有需要发送的号码！");
                return false;
            }*/
            
            //(0:所有客户  1:我的客户  2:平台组团社客户)                
            p.$data.sendAll.isSend = true;
            p.$data.sendAll.phoneCount = Phone114Count;
            if($("#typeList_0").attr("checked") == true)  //我的客户
            {
                p.$data.sendAll.dataSource = 1;
                if($(".baidi").length == 0)
                    return false;
            }
            else if($("#typeList_1").attr("checked") == true)
                p.$data.sendAll.dataSource = 2;
                
            if(Phone114Count > 0 && !confirm("即将发送查询结果中的号码(共" + Phone114Count + "个)，是否确定发送所有？"))
                return false;    
                
            p.$data.sendAll.customerType = $("#ddl_CustomerType").val();
            p.$data.sendAll.provinceId = $("#CityList1_ddl_ProvinceList").val();
            p.$data.sendAll.cityId = $("#CityList1_ddl_CityList").val();  
            if(p.$data.sendAll.provinceId == "")
                p.$data.sendAll.provinceId = 0;
            if(p.$data.sendAll.cityId == "")
                p.$data.sendAll.cityId = 0;
            //清空选择的同业114号码
            p.$data.setConfirmedMobiles([]);    
            //重新设置要发送的同业114号码个数            
            p.$("#Phone_114_Count").html(Phone114Count);
            p.$("#Phone_All_Count").html(Phone114Count + parseInt(p.$("#Phone_Txt_Count").html()));
            p.Boxy.getIframeDialog(iframeId).hide();             
        }

        $(document).ready(init);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:RadioButtonList runat="server" ID="typeList" RepeatDirection="Horizontal" >    
                    <asp:ListItem Text="我的客户" Value="0"></asp:ListItem>
                    <asp:ListItem Text="同业114客户" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="left">
                &nbsp;筛选： <span style="display:none;"><asp:TextBox ID="txt_CompanyName" runat="server"></asp:TextBox></span>
                &nbsp;客户类型<asp:DropDownList ID="ddl_CustomerType" runat="server">
                </asp:DropDownList>
                &nbsp;
                <uc1:CityList runat="server" ID="CityList1" />
                <asp:Button ID="btnQuery" runat="server" Text="搜索" OnClick="btnQuery_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rptCustomerList" runat="server" OnItemDataBound="rptCustomerList_ItemDataBound">
        <HeaderTemplate>
            <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0" id="tblCustomers">
                <tr>
                    <td width="8%" align="center" bgcolor="#C5DCF5">
                        序号
                    </td>
                    <td width="15%" align="center" bgcolor="#C5DCF5">
                        客户类型<br />
                    </td>
                    <td width="10%" align="center" bgcolor="#C5DCF5">
                        所在地
                    </td>
                    <td width="20%" align="center" bgcolor="#C5DCF5">
                        手机号码
                    </td>
                   
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td align="center">
                    <%--checkbox value:号码|客户编号|是否显示时加密(1是 0否)|显示时加密的号码 --%>
                    <input type="checkbox" id="chk<%#Eval("CustomerId")%><%#Eval("RetailersCompanyId") %>" value='<%# Eval("Mobile")%>|<%#Eval("CustomerId")%><%#Eval("RetailersCompanyId") %>|<%#Eval("CustomerId").ToString()=="0"?"1":"0" %>|<%#Utils.GetEncryptMobile(Eval("mobile").ToString(),true) %>' onclick="chkClick(this);">
                    <asp:Label ID="lblCustomerID" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <%# DataBinder.Eval(Container.DataItem, "CategoryName")%>
                </td>
                 <td align="center">
                    <%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>-<%# DataBinder.Eval(Container.DataItem, "CityName")%>
                </td>
                <td align="center">
                    <%#Utils.GetEncryptMobile(Eval("mobile").ToString(), Eval("CustomerId").ToString() == "0" ? true : false)%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="NoData" runat="server" Visible="false">
        <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0">
            <tr>
                <td width="8%" align="center" bgcolor="#C5DCF5">
                    序号
                </td>
                <td width="15%" align="center" bgcolor="#C5DCF5">
                    客户类型
                </td>
                <td width="10%" align="center" bgcolor="#C5DCF5">
                   所在地
                </td>
                <td width="20%" align="center" bgcolor="#C5DCF5">
                    手机号码
                </td>
            </tr>
            <tr >
                <td align="center" colspan="16" height="100" runat="server" id="TdNoData">
                    暂无客户列表
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="div_Expage" runat="server" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td class="F2Back" align="right" style="height: 31px">
               <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
            </td>
        </tr>
    </table>
    <table width="100%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr align="left">
            <td width="62%" class="shenghui">
                <a href="javascript:checkAll(0)" style="cursor: hand">全选</a>&nbsp;&nbsp;&nbsp;<a href="javascript:checkAll(1)" style="cursor: hand">反选</a>&nbsp;&nbsp;&nbsp;<a href="javascript:checkAll(2)"
                        style="cursor: hand">清空</a>
                <input type="button" value="发送所有" onclick="btnSendAll()" /><input name="Submit" type="button" value="确定" onclick="btnSubmit();" />&nbsp;&nbsp;<input name="Submit" type="button" value="取消" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" />
            </td>
        </tr>
    </table>
    </form>
    
</body>
</html>
