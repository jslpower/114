<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SanPinCenterNormalList.aspx.cs"
    Inherits="SiteOperationsCenter.AdManagement.SanPinCenterNormalList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>散拼中心广告普通版列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dateformat") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
         <tr style="height: 30px;">
            <td align="center">
                <h3>
                   散拼中心普通版推荐企业
                </h3>
            </td>
        </tr>
            <tr>
                <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxunbg.gif">
                    <input type="hidden" id="hCurrCityId" runat="server" /><%--当前城市编号--%>
                    <input type="hidden" id="hCurrCityName" runat="server" /><%--当前城市名称--%>
                    <input type="hidden" id="hCurrProvinceId" runat="server" /><%--当前省份编号--%>
                    <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" SetCityId="362" SetProvinceId="33"
                        runat="server" />
                    &nbsp; <a href="javascript:;" id="a_SearchCompany">
                        <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62"
                            height="21" style="margin-bottom: -3px;" /></a>
                    <%--<a href="#" style="padding: 2px 8px;
                            border: 1px solid #F00; color: #f00"><b>预览南昌分站</b></a>--%>
                </td>
            </tr>
        </table>
        <table id="tbList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1"
            class="kuang">
            <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="white">
                <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>排序</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>公司名称</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>产品数</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>登录次数</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>是否加粗</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>替换颜色</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>城市</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
            <tr name="midderTR">
                <td colspan="8" align="center" valign="middle" style="height: 80px;">
                    数据加载中...
                </td>
            </tr>
            <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="white">
                <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>排序</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>公司名称</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>产品数</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>登录次数</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>是否加粗</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>替换颜色</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>城市</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right; padding-right: 30px; padding-top: 10px; padding-bottom: 10px;">
                    <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td background="<%=ImageManage.GetImagerServerUrl(1)%><%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/yunying/chaxunbg.gif">
                    <uc1:ProvinceAndCityList ID="ProvinceAndCityList2" runat="server" />
                    &nbsp;<input type="checkbox" id="IsPay" /><label for="IsPay">是否为收费客户</label>
                    <a href="javascript:;" id="a_SearchAllCompany">
                        <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62"
                            height="21" style="margin-bottom: -3px;" /></a>
                </td>
            </tr>
        </table>
        <table id="tbCompanyList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1"
            class="kuang">
            <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="white">
                <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>排序</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>加入时间</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>公司名称</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>联系人</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>地区</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>收费客户</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
            <tr name="midderTR">
                <td colspan="7" align="center" valign="middle" style="height: 80px;">
                    数据加载中...
                </td>
            </tr>
            <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="white">
                <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>排序</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>加入时间</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>公司名称</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>联系人</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>地区</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>收费客户</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" class="digg" style="margin-right: 20px;">
                <div class="digg" id="PageInfo"></div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var CompanyFun = {
            CurrProvinceId: 0, CurrCityId: 0, CurrCityName: '', PageIndex: 1,
            ///**********************
            ///描述：删除已选择的公司
            ///**********************
            del: function(obj) {
                var CurrRowCount = $(obj).parent().parent().siblings().length;
                var id = $(obj).parent().parent().find("[id='hKeyId']").val();
                var cid = $(obj).parent().parent().find("[id='hCId']").val();
                if (CurrRowCount == 2) {
                    alert("至少保留一行数据！");
                    return false;
                }
                if (confirm("您确定要删除该数据吗？")) {
                    $.ajax({
                        type: 'get',
                        url: '/AdManagement/SanPinCenterNormalList.aspx?mode=del&id=' + id,
                        async: false,
                        success: function(result) {
                            if (result == "success") {
                                $(obj).parent().parent().remove();
                                $("#tbCompanyList").find(":tr[cid='" + cid + "']").html("<a href='javascript:;' IsClick='0' onclick='CompanyFun.Add(this);'>选取</a>");
                            }
                        }
                    });
                }
                else {
                    return false;
                }
            },
            ///**********************
            ///描述：添加已审核的公司
            ///**********************
            Add: function(obj) {
                var CityId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val();
                if (CityId == null || CityId == 0) {
                    alert("请选择城市后再执行选取！");
                    return false;
                }
                if ($("#tbList").find("tr[name='midderTR']").length == 21) {
                    alert("您已经选取了21家推荐企业了，请先删除以后再选取！");
                    return false;
                }
                //防止重复提交
                $(obj).removeAttr("onclick");
                $(obj).html("正在选取");
                $.ajax({
                    type: 'get',
                    url: '/AdManagement/SanPinCenterNormalList.aspx?mode=add&cid=' + $(obj).parent().attr("cid") + '&cityid=' + CityId,
                    success: function(result) {
                        if (result != "error") {
                            $(obj).parent().html("已选取");
                            CompanyFun.LoadRpList(result);
                        }
                    }
                });
            },
            ///**************************
            ///描述：加载已选取的公司列表
            ///*************************
            LoadRpList: function(RpListStr) {
                RpListStr = eval(RpListStr);
                //移除内容行
                $("#tbList").find("tr[name='midderTR']").remove();
                if (RpListStr.length == 0)
                    $("#tbList").find("tr:last").before('<tr class="baidi" name="midderTR"><td colspan="7" align="center" valign="middle" style="height: 80px;">暂无数据！</td></tr>');
                for (var i = 0; i < RpListStr.length; i++) {
                    var strHtml = '<tr class="baidi" name="midderTR">';
                    //第一列--"序号"
                    strHtml += '<td height="25" align="center">';
                    strHtml += '<input type="hidden" id="hKeyId" name="hKeyId" value="' + RpListStr[i]["ID"] + '" />';
                    strHtml += '<input type="hidden" id="hCId" name="hCId" value="' + RpListStr[i]["CompanyID"] + '" />';
                    strHtml += '<input type="text" id="txt_Sort" name="txt_Sort" value="' + RpListStr[i]["SortID"] + '" size="3" />';
                    strHtml += '</td>';
                    //第二列--"公司名称"
                    strHtml += '<td align="center">' + RpListStr[i]["CompanyName"] + '<input type="hidden" name="hCompanyName" value=' + RpListStr[i]["CompanyName"] + ' /></td>';
                    //第三列"产品数"
                    strHtml += '<td align="center">' + RpListStr[i]["ProductNum"] + '</td>';
                    //第四列--"登录次数"
                    strHtml += '<td align="center">' + RpListStr[i]["LoginCount"] + '</td>';
                    //第五列--"是否加粗"
                    strHtml += '<td align="center"><input type="hidden" name="hCk_Blod"><input id="ck_Blod" name="ck_Blod" type="checkbox" bold=' + RpListStr[i]["IsBold"] + ' /></td>';
                    //第六列--"替换颜色"
                    strHtml += '<td align="center">';
                    strHtml += '<input type="hidden" id="hColor" name="hColor" value="' + RpListStr[i]["Color"] + '" />';
                    strHtml += '<select name="SelectColor" id="SelectColor">';
                    strHtml += '<option value="">请选择</option>';
                    strHtml += '<option style="background: #ff0000" value="#ff0000">红色</option>';
                    strHtml += '<option style="background: #CC0000" value="#CC0000">中国红</option>';
                    strHtml += '<option style="background: #00CC00" value="#00CC00">绿色</option>';
                    strHtml += '<option style="background: #0033FF" value="#0033FF">蓝色</option>';
                    strHtml += '<option style="background: #FF9900" value="#FF9900">黄色</option>';
                    strHtml += '</select>';
                    strHtml += '</td>';
                    //第七列--"城市"
                    strHtml += '<td align="center">' + RpListStr[i]["ShowCity"] + '</td>';
                    //第八列--"删除"
                    strHtml += ' <td align="center"><a href="javascript:;" onclick="CompanyFun.del(this);">删除</a></td>';

                    strHtml += '</tr>';

                    $("#tbList").find("tr:last").before(strHtml);
                }
                this.InitBold();
                this.InitColor();
            },
            ///***********************************************
            ///描述：加载所有的已审核公司列表(组团，专线，地接)
            ///***********************************************
            LoadRpCompanyList: function(RpCompanyListStr) {
                if (RpCompanyListStr == "")
                    return false;
                //绑定分页控件
                AjaxPageControls.replace("PageInfo", { pageSize: '<%= pageSize %>', pageIndex: this.PageIndex, recordCount: RpCompanyListStr.split('$$$$')[0], gotoPageFunctionName: 'CompanyFun.InitAllCompanyList' });
                var pageIndex = parseInt(RpCompanyListStr.split('$$$$')[1], 10); //当前页码
                RpCompanyListStr = eval(RpCompanyListStr.split('$$$$')[2]); //列表内容
                //移除内容行
                $("#tbCompanyList").find("tr[name='midderTR']").remove();
                if (RpCompanyListStr.length == 0)
                    $("#tbCompanyList").find("tr:last").before('<tr class="baidi" name="midderTR"><td colspan="7" align="center" valign="middle" style="height: 80px;">暂无数据！</td></tr>');
                for (var i = 0; i < RpCompanyListStr.length; i++) {
                    var strHtml = '<tr class="baidi" name="midderTR">';
                    //第一列--"序号"
                    strHtml += '<td height="25" align="center">';
                    strHtml += '<input type="hidden" id="hCId" name="hCId" value="' + RpCompanyListStr[i]["ID"] + '" />';
                    strHtml += (pageIndex - 1) * parseInt('<%= pageSize %>', 10) + i + 1;
                    strHtml += '</td>';
                    //第二列--"加入时间"
                    strHtml += '<td align="center">' + new Date(RpCompanyListStr[i]["AddDate"]).format("yyyy-mm-dd", "utc") + '</td>';
                    //第三列"公司名称"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["CompanyName"] + '</td>';
                    //第四列--"联系人"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["ContactName"] + '</td>';
                    //第五列--"城市名称"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["CityName"] + '</td>';
                    //第六列--"是否收费客户"
                    strHtml += '<td align="center"> ' + (RpCompanyListStr[i]["IsPay"] ? "是" : "否") + '</td>';
                    //第七列--"是否已选取"
                    strHtml += '<td align="center" id="trOperator" cid="' + RpCompanyListStr[i]["ID"] + '">已选取</td>';

                    strHtml += '</tr>';

                    $("#tbCompanyList").find("tr:last").before(strHtml);
                }
                this.InitOperate();
            },
            ///********************
            ///描述：初始化是否粗体
            ///********************
            InitBold: function() {
                $(":checkbox[id='ck_Blod']").each(function() {
                    if ($(this).attr("bold") == "true") {
                        $(this).attr("checked", true);
                        $(this).prev().val(1);
                    }
                    else {
                        $(this).prev().val(0);
                    }
                });

                $(":checkbox[id='ck_Blod']").each(function() {
                    $(this).click(function() {
                        if ($(this).attr("checked"))
                            $(this).prev().val(1);
                        else
                            $(this).prev().val(0);
                    });
                });
            },
            ///*******************
            ///描述：初始化颜色列表
            ///*******************
            InitColor: function() {
                $("#tbList").find("select").each(function() {
                    if ($(this).prev().val() != "") {
                        var color = $(this).prev().val();
                        $(this).children().each(function() {
                            if ($(this).attr("value") == color)
                                $(this).attr("selected", true);
                        });
                    }
                    $(this).change(function() {
                        $(this).prev().val($(this).val());
                        return false;
                    });
                });
            },
            ///**********************
            ///描述：初始化"选取"操作
            ///**********************
            InitOperate: function() {
                $("#tbCompanyList").find(":tr[id='trOperator']").each(function() {
                    if ($("#tbList").find(":hidden[id='hCId'][value='" + $(this).attr("cid") + "']").length == 0) {
                        $(this).html("<a href='javascript:;' IsClick='0' onclick='CompanyFun.Add(this);'>选取</a>");
                    }
                    else {
                        $(this).html("已选取");
                    }
                });
            },
            ///************************************
            ///描述：点击搜索时获取已选取的单位列表
            ///************************************
            InitCompanyList: function() {
                this.InitProvinceAndCity();
                $.ajax({
                    type: 'get',
                    url: '/AdManagement/SanPinCenterNormalList.aspx?mode=getlist&cityid=' + this.CurrCityId + '&pid=' + this.CurrProvinceId + '&cname=' + this.CurrCityName + '&rnum=' + Math.random(),
                    cache: false,
                    async: false,
                    success: function(result) {
                        if (result != "error") {
                            CompanyFun.LoadRpList(result);
                        }
                    }
                });
                this.InitOperate();

            },
            ///*******************************
            ///描述：分页获取所有的单位信息列表
            ///******************************
            InitAllCompanyList: function(pageIndex) {
                this.PageIndex = pageIndex;
                var IsPay = $("#IsPay").attr("checked") ? "1" : "0";
                var cityId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList2.ClientID %>"]["cityId"]).val();
                $.ajax({
                    type: 'get',
                    url: '/AdManagement/SanPinCenterNormalList.aspx?mode=getalllist&pageIndex=' + pageIndex + '&IsPay=' + IsPay + '&cid=' + cityId + '&rnum=' + Math.random(),
                    cache: false,
                    success: function(result) {
                        if (result != "error") {
                            CompanyFun.LoadRpCompanyList(result);
                        }
                    }
                });
            },
            ///*********************************************************
            ///描述：初始化已选择的单位所属省份城市，默认选中[浙江 杭州]
            ///*********************************************************
            InitProvinceAndCity: function() {
                this.CurrCityId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val();
                this.CurrProvinceId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["provinceId"]).val();
                this.CurrCityName = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).find("[selected='true']").text();
                if (this.CurrCityId == 0) {
                    $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val("362");
                    this.CurrCityId = 362;
                }
                if (this.CurrProvinceId == 0) {
                    $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["provinceId"]).val("33");
                    this.CurrProvinceId = 33;
                }
                if (this.CurrCityName == null || this.CurrCityName == "请选择")
                    this.CurrCityName = "杭州";

                $("#hCurrCityId").val(this.CurrCityId);
                $("#hCurrCityName").val(this.CurrCityName);
                $("#hCurrProvinceId").val(this.CurrProvinceId);
            }
        };
        $(function() {
            //加载已经被选择的公司列表
            CompanyFun.InitCompanyList();
            //加载所有已审核的公司列表，需传分页页码
            CompanyFun.InitAllCompanyList(1);
            //*************************
            //第一个列表查询按钮点击事件
            //*************************
            $("#a_SearchCompany").click(function() {
                CompanyFun.InitCompanyList();
                return false;
            });
            //*************************
            //第二个列表查询按钮点击事件
            //*************************
            $("#a_SearchAllCompany").click(function() {
                CompanyFun.InitAllCompanyList(1);
                return false;
            });
        });
    </script>

    </form>
</body>
</html>
