<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RecommendProductList.aspx.cs"
    Inherits="SiteOperationsCenter.AdManagement.RecommendProductList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityAndAreaList.ascx" TagName="ProvinceAndCityAndAreaList"
    TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>推荐产品</title>
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
                        首页推荐产品广告
                    </h3>
                </td>
            </tr>
            <tr>
                <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxunbg.gif">
                    <input type="hidden" id="hCurrCityId" runat="server" /><%--当前城市编号--%>
                    <input type="hidden" id="hCurrCityName" runat="server" /><%--当前城市名称--%>
                    <input type="hidden" id="hCurrProvinceId" runat="server" /><%--当前省份编号--%>
                    <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" SetCityId="362"
                        SetProvinceId="33" />
                    &nbsp;<a href="javascript:;" id="a_SearchCompany"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif"
                        width="62" height="21" style="margin-bottom: -3px;" /></a>
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
                    <strong>产品名称</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>价格</strong>
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
                    <strong>产品名称</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>价格</strong>
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
                    <uc2:ProvinceAndCityAndAreaList ID="ProvinceAndCityAndAreaList1" runat="server" />
                    &nbsp;<a href="javascript:;" id="a_SearchAllCompany"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif"
                        width="62" height="21" style="margin-bottom: -3px;" /></a>
                </td>
            </tr>
        </table>
        <table id="tbCompanyList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1"
            class="kuang">
            <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="white">
                <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>序号</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>线路名称</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>出发时间</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>门市价</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>MQ</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>出港城市</strong>
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
                    <strong>序号</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>线路名称</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>出发时间</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>门市价</strong>
                </td>
                <td width="18%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>MQ</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>出港城市</strong>
                </td>
                <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" class="digg" style="margin-right: 20px;">
                    <div class="digg" id="PageInfo">
                    </div>
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
                        url: '/AdManagement/RecommendProductList.aspx?mode=del&id=' + id,
                        async: false,
                        success: function(result) {
                            if (result == "success") {
                                $(obj).parent().parent().remove();
                                $("#tbCompanyList").find(":tr[cid='" + cid + "']").html("<a href='javascript:;' IsClick='0' onclick='CompanyFun.Add(this);'>选取</a>");
                            }
                            else
                                alert("删除失败！");
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
                var ProductName = $(obj).parent().parent().find("[id='hfAreaName']").val();
                var ProductPrice = $(obj).parent().parent().find("[id='hfProductPrice']").val();
                var ProductID = $(obj).parent().parent().find("[id='hCId']").val();
                var CityId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val();
                if (CityId == null || CityId == 0) {
                    alert("请选择城市后再执行选取！");
                    return false;
                }
                if ($("#tbList").find("tr[name='midderTR']").length == 5) {
                    alert("您已经选取了5条推荐产品了，请先删除以后再选取！");
                    return false;
                }
                $(obj).removeAttr("onclick");
                $(obj).html("正在选取");
                $.ajax({
                    type: 'get',
                    url: '/AdManagement/RecommendProductList.aspx?mode=add&cid=' + $(obj).parent().attr("cid") + '&cityid=' + CityId + '&ProductName=' + ProductName + '&ProductPrice=' + ProductPrice + '&ProductID=' + ProductID,
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
                    strHtml += '<input type="hidden" id="hfMQ" name="hfMQ" value="' + RpListStr[i]["ContactMQ"] + '" />';
                    strHtml += '<input type="hidden" id="hCId" name="hCId" value="' + RpListStr[i]["CompanyID"] + '" />';
                    strHtml += '<input type="text" id="txt_Sort" name="txt_Sort" value="' + RpListStr[i]["SortID"] + '" size="3" />';
                    strHtml += '</td>';
                    //第二列--"产品名称"
                    strHtml += '<td align="center">' + RpListStr[i]["ProductName"] + '<input type="hidden" name="hProductName" value=' + RpListStr[i]["ProductName"] + ' /><input type="hidden" name="hProductId" value=' + RpListStr[i]["ProductID"] + ' /></td>';
                    //第三列"价格"
                    strHtml += '<td align="center">' + RpListStr[i]["Price"] + '<input type="hidden" name="hPrice" value=' + RpListStr[i]["Price"] + ' /></td>';
                    //第四列--"城市"
                    strHtml += '<td align="center">' + RpListStr[i]["ShowCity"] + '<input type="hidden" name="hfCompanyName" value=' + RpListStr[i]["CompanyName"] + '</td>';
                    //第五列--"删除"
                    strHtml += ' <td align="center"><a href="javascript:;" onclick="CompanyFun.del(this);">删除</a></td>';

                    strHtml += '</tr>';

                    $("#tbList").find("tr:last").before(strHtml);
                }
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
                    //第二列--"线路名称"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["RouteName"] + '<input type="hidden" id="hfAreaName" name="hfAreaName" value="' + RpCompanyListStr[i]["RouteName"] + '" />' + '</td>';
                    //第三列"出发时间"
                    strHtml += '<td align="center">' + new Date(RpCompanyListStr[i]["LeaveDate"]).format("yyyy-mm-dd", "utc") + '</td>';
                    //第四列--"门市价"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["RetailAdultPrice"] + '<input type="hidden" id="hfProductPrice" name="hfProductPrice" value="' + RpCompanyListStr[i]["RetailAdultPrice"] + '" />' + '</td>';
                    //第五列--"MQ"
                    strHtml += '<td align="center">' + RpCompanyListStr[i]["TourContacMQ"] + '</td>';
                    //第六列--"出港城市"
                    strHtml += '<td align="center"> ' + RpCompanyListStr[i]["LeaveCityName"] + '</td>';
                    //第七列--"是否已选取"
                    strHtml += '<td align="center" id="trOperator" cid="' + RpCompanyListStr[i]["ID"] + '">已选取</td>';
                    strHtml += '</tr>';
                    $("#tbCompanyList").find("tr:last").before(strHtml);
                }
                this.InitOperate();
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
                    url: '/AdManagement/RecommendProductList.aspx?mode=getlist&cityid=' + this.CurrCityId + '&pid=' + this.CurrProvinceId + '&cname=' + this.CurrCityName + '&rnum=' + Math.random(),
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
                //获取城市ID
                var cityId = $("#" + provinceAndCityAndAreaUserControl["<%= ProvinceAndCityAndAreaList1.ClientID %>"]["cityId"]).val();
                //线路区域ID
                var areaId = $("#" + provinceAndCityAndAreaUserControl["<%= ProvinceAndCityAndAreaList1.ClientID %>"]["areaId"]).val();
                $.ajax({
                    type: 'get',
                    url: '/AdManagement/RecommendProductList.aspx?mode=getalllist&pageIndex=' + pageIndex + '&cid=' + cityId + '&aid=' + areaId + '&rnum=' + Math.random(),
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

            CompanyFun.InitCompanyList(); //加载以选择的公司列表
            CompanyFun.InitAllCompanyList(1); //加载所有已审核的公司

            $("#a_SearchAllCompany").click(function() {
                CompanyFun.InitAllCompanyList(1);
                return false;
            });

            $("#a_SearchCompany").click(function() {
            if ($("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val() == "0") {
                    alert("请选择城市！");
                }
                else {
                    CompanyFun.InitCompanyList();
                }
                return false;
            });

        });
    </script>

    </form>
</body>
</html>
