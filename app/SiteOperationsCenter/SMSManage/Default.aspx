<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.SMSManage.Default" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc3" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信充值审核</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                审核状态：<asp:DropDownList ID="dropCheckState" runat="server">
                    <asp:ListItem Value="-1">所有</asp:ListItem>
                    <asp:ListItem Value="0">未审核</asp:ListItem>
                    <asp:ListItem Value="1">审核通过</asp:ListItem>
                    <asp:ListItem Value="2">审核未通过</asp:ListItem>
                </asp:DropDownList>
                单位名称：<asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                充值时间：<input type="text" runat="server" id="txtStartDate" name="txtStartDate" style="width: 80px" />
                &nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" id="img_Search" style="cursor: pointer;
                    margin-bottom: -3px;" width="62" height="21" />
            </td>
        </tr>
    </table>
    <cc1:CustomRepeater ID="repCompanyList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang"
                id="tbCompanyList">
                <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>单位名称</strong>
                    </td>
                    <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>地区</strong>
                    </td>
                    <td width="10%"  align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>充值人</strong>
                    </td>
                    <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>电话</strong>
                    </td>
                    <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>手机</strong>
                    </td>
                    <td width="5%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>MQ</strong>
                    </td>
                    <td align="center" width="18%" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>充值金额(元)/可用金额(元)</strong>
                    </td>
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>充值时间</strong>
                    </td>
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>操作</strong>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="baidi"  onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" height="25px">
                <td align="center">
                    <a href="javascript:void(0);">
                        <%# Eval("CompanyName")%></a>
                </td>
                <td align="center">
                   <%# GetCityName(Convert.ToInt32(Eval("ProvinceId").ToString()),Convert.ToInt32(Eval("CityId").ToString()))%>
                </td>
                <td align="center">
                    <%# Eval("UserFullName")%>
                </td>
                <td align="center">
                    <%# Eval("UserMobile")%>
                </td>
                <td align="center">
                    <%# Eval("UserTel")%>
                </td>
                <td align="center">
                  <%# EyouSoft.Common.Utils.GetMQ(Eval("UserMQId").ToString())%>  
                   
                </td>
                <td align="center">
                    <%# Eval("PayMoney","{0:f2}")%>
                    /<%# Eval("UseMoney","{0:f2}")%>
                </td>
                <td align="center">
                    <%# Convert.ToDateTime( Eval("PayTime").ToString()).ToShortDateString()%>
                </td>
                <td align="center">
                    <asp:Literal ID="ltrDanFangCha" runat="server"></asp:Literal>
                    <%# GetSmsCheck(Convert.ToInt32(Eval("IsChecked").ToString()), Convert.ToDecimal(Eval("PayMoney").ToString()), Eval("PayMoneyId").ToString())%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr bgcolor="#f3f7ff" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" height="25px">
                <td align="center">
                    <a href="javascript:void(0);">
                        <%# Eval("CompanyName")%></a>
                </td>
                <td align="center">
                   <%# GetCityName(Convert.ToInt32(Eval("ProvinceId").ToString()),Convert.ToInt32(Eval("CityId").ToString()))%>
                </td>
                <td align="center">
                    <%# Eval("UserFullName")%>
                </td>
                <td align="center">
                    <%# Eval("UserMobile")%>
                </td>
                <td align="center">
                    <%# Eval("UserTel")%>
                </td>
                <td align="center">
                    <%# EyouSoft.Common.Utils.GetMQ(Eval("UserMQId").ToString())%>  
                </td>
                <td align="center">
                    <%# Eval("PayMoney","{0:f2}")%>
                    /<%# Eval("UseMoney","{0:f2}")%>
                </td>
                <td align="center">
                    <%# Convert.ToDateTime( Eval("PayTime").ToString()).ToShortDateString()%>
                </td>
                <td align="center">
                    <%# GetSmsCheck(Convert.ToInt32(Eval("IsChecked").ToString()), Convert.ToDecimal(Eval("PayMoney").ToString()), Eval("PayMoneyId").ToString())%>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>地区</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>充值人</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>电话</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机</strong>
                </td>
                <td width="5%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>MQ</strong>
                </td>
                <td  align="center" width="18%"  background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>充值金额/可用金额</strong>
                </td>
                <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>充值时间</strong>
                </td>
                <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </cc1:CustomRepeater>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        var CheckPay = {
            ChekSms: function(PayId, isPass) {
                var isTrue = true;
                //验证金额及短信数据格式
                var Money = $.trim($("#div_" + PayId).find("input[id='txtPayMoney']").val());
                var UseMoney = $.trim($("#div_" + PayId).find("input[id='txtUseMoney']").val());
                if (isPass) {
                    if (Money == "") {
                        isTrue = false;
                        alert("请输入充值金额!");
                        return false;
                    }
                    if (parseFloat(Money) == 0 || isNaN(parseFloat(Money)) || !/^\d+(\.\d+)?$/.test(Money)) {
                        isTrue = false;
                        alert("请输入正确的充值金额!");
                        return false;
                    }
                    if (UseMoney == "") {
                        isTrue = false;
                        alert("请输入可用金额!");
                        return false;
                    }
                    if (parseFloat(UseMoney) == 0 || isNaN(parseFloat(UseMoney)) || !/^\d+(\.\d+)?$/.test(UseMoney)) {
                        isTrue = false;
                        alert("请输入正确的可用金额!")
                        return false;
                    }
                }
                if (isTrue) {
                    if (confirm("确定此操作吗?")) {
                        $.ajax({
                            type: "GET",
                            dataType: 'html',
                            url: "Default.aspx",
                            data: { "check": 1, "Id": PayId, State: isPass ? "1" : "2", "Money": Money, "UseMoney": UseMoney },
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    alert("操作成功!");
                                    var url = "<%=Request.QueryString %>" == "" ? "Default.aspx" : "Default.aspx?<%=Request.QueryString %>";
                                    window.location.href = url;
                                }
                            }
                        });
                    }
                }
            },
            OnSearch: function() {
                var SearchUrl = "Default.aspx?ProvinceId=" + $("#ProvinceAndCityList1_ddl_ProvinceList").val() + "&CityId=" + $("#ProvinceAndCityList1_ddl_CityList").val() + "&ckState=" + $("#dropCheckState").val() + "&CompanyName=" + escape($.trim($("#txtCompanyName").val())) + "&ckTime=" + $("#txtStartDate").val();
                window.location.href = SearchUrl;
            },
            OpenCheckdiv: function(Id, obj) {
                $('div.white_content').hide();
                $("#div_" + Id).css({ top: $(obj).position().top - 5, left: $(obj).position().left - 320 });
//                var Money = $("#hid_" + Id).val().split(",")[0];
//                var strNumber = $("#hid_" + Id).val().split(",")[1];
//                $("#div_" + Id).find("input[id='txtPayMoney']").val(Money);
//                $("#div_" + Id).find("input[id='txtSmsNumber']").val(strNumber);
                $("#div_" + Id).show();
            },
            SetSmsNumber: function(obj) {
                var Money = $.trim($(obj).val());
                if (parseFloat(Money) > 0) {
                    $(obj).parents("table").find("input[id='txtSmsNumber']").eq(0).val(parseInt((Money * 10)));
                }
            }
        };
        $(function() {
            $("#<%=txtStartDate.ClientID%>").focus(function() {
                WdatePicker();
            });

            $("#img_Search").click(function() {
                CheckPay.OnSearch();
            });
        });
    </script>

    </form>
</body>
</html>
