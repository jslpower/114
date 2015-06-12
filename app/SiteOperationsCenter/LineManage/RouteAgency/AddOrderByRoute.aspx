<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrderByRoute.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.RouteAgency.AddOrderByRoute" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/UserOrder/OrderCustomer.ascx" TagName="OrderCustomer"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>线路管理-代定</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'AddOrderByRoute'
    });
    </script>

    <script type="text/JavaScript">
<!--
function ViewAllCarPlan1()
{
	new Controls.Dialog('../../zx/fwbz/yuding_danwei.html', '预订单位', {width:800, height:375, minmize:'no',maximize:"yes", scrollbars: 'no',closebtn: 'yes'});
}
function ViewAllCarPlan2()
{
	new Controls.Dialog('../../zx/fwbz/daoru_md.html', '导入名单', {width:800, height:395, minmize:'no',maximize:"yes", scrollbars: 'no',closebtn: 'yes'});
}

//-->
    </script>

    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ContentWindow") %>"></script>

</head>
<body>
    <form id="form1">
    <%--<table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="33%" align="right" bgcolor="#f2f9fe">
                线路名称：
            </td>
            <td width="67%" align="left" bgcolor="#FFFFFF">
                <a href="#">澳新旅游15天</a> 出发城市：上海
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                预定单位：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="textfield" />
                <a href="javascript:ViewAllCarPlan1();">
                    <img src="../../images/icon_select.jpg" width="28" height="18" border="0" align="absmiddle" /></a>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发日期：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                2011-07-29 周五 <b>剩余空位：23</b> 【<a href="tuandui_jh_update.html">团队出团信息确定修改</a>】
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                游客联系人：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="textfield2" />
                联系电话：
                <input type="text" name="textfield3" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                组团社联系人：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="textfield22" />
                联系电话：
                <input type="text" name="textfield23" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                价格组成：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                成人
                <input name="text" type="text" value="10" size="6" />
                人，儿童
                <input name="text2" type="text" value="10" size="6" />
                人 ，单房差
                <input name="text2" type="text" value="10" size="6" />
                人
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                销售价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <span sizset="0" sizcache="0" jquery1309744253156="18"><span sizset="0" sizcache="0"
                    jquery1309744253156="17">成人
                    <input id="ro_txtManCount6" onchange="RouteOrder.changePeopleCount(this,'1')" value="14000"
                        size="10" name="ro_txtManCount62" jquery1309744253156="2" runat="server" sourcecount="1" />
                    元</span></span>，儿童
                <input id="ro_txtManCount62" onchange="RouteOrder.changePeopleCount(this,'1')" value="13000"
                    size="10" name="ro_txtManCount63" jquery1309744253156="2" runat="server" sourcecount="1" />
                元，单房差
                <input id="ro_txtManCount63" onchange="RouteOrder.changePeopleCount(this,'1')" value="1000"
                    size="10" name="ro_txtManCount64" jquery1309744253156="2" runat="server" sourcecount="1" />
                元，增减费用<span sizset="0" sizcache="0" jquery1309744253156="18"><span sizset="0" sizcache="0"
                    jquery1309744253156="17">
                    <input id="ro_txtManCount10" onchange="RouteOrder.changePeopleCount(this,'1')" value="600"
                        size="10" name="ro_txtManCount6" jquery1309744253156="2" runat="server" sourcecount="1" />
                </span></span>元
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                结算价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <span sizset="0" sizcache="0" jquery1309744253156="18"><span sizset="0" sizcache="0"
                    jquery1309744253156="17">成人
                    <input id="ro_txtManCount622" onchange="RouteOrder.changePeopleCount(this,'1')" value="14000"
                        size="10" name="ro_txtManCount622" jquery1309744253156="2" runat="server" sourcecount="1" />
                    元</span></span>，儿童
                <input id="ro_txtManCount632" onchange="RouteOrder.changePeopleCount(this,'1')" value="13000"
                    size="10" name="ro_txtManCount632" jquery1309744253156="2" runat="server" sourcecount="1" />
                元，单房差
                <input id="ro_txtManCount64" onchange="RouteOrder.changePeopleCount(this,'1')" value="1000"
                    size="10" name="ro_txtManCount642" jquery1309744253156="2" runat="server" sourcecount="1" />
                元，增减费用<span sizset="0" sizcache="0" jquery1309744253156="18"><span sizset="0" sizcache="0"
                    jquery1309744253156="17">
                    <input id="ro_txtManCount65" onchange="RouteOrder.changePeopleCount(this,'1')" value="600"
                        size="10" name="ro_txtManCount65" jquery1309744253156="2" runat="server" sourcecount="1" />
                </span></span>元
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                特殊要求说明：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="textarea18" id="textarea18" cols="85" rows="4"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                订单状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="radio" name="radiobutton" value="radiobutton" />
                预订
                <input name="radiobutton" type="radio" value="radiobutton" checked="checked" />
                预留
                <input type="radio" name="radiobutton" value="radiobutton" />
                已确定
                <input type="radio" name="radiobutton" value="radiobutton" />
                结单
                <input type="radio" name="radiobutton" value="radiobutton" />
                取消
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                支付状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="radio" name="radiobutton" value="radiobutton" />
                未支付
                <input type="radio" name="radiobutton" value="radiobutton" />
                待支付
                <input name="radiobutton" type="radio" value="radiobutton" checked="checked" />
                游客定金已支付
                <input type="radio" name="radiobutton" value="radiobutton" />
                组团社定金已支付
                <input type="radio" name="radiobutton" value="radiobutton" />
                组团社全款已支付
                <input type="radio" name="radiobutton" value="radiobutton" />
                结账
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                总金额：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                销售价
                <input id="ro_txtManCount623" onchange="RouteOrder.changePeopleCount(this,'1')" value="14000"
                    size="10" name="ro_txtManCount623" jquery1309744253156="2" runat="server" sourcecount="1" />
                &nbsp; 结算价
                <input id="ro_txtManCount624" onchange="RouteOrder.changePeopleCount(this,'1')" value="13000"
                    size="10" name="ro_txtManCount624" jquery1309744253156="2" runat="server" sourcecount="1" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                操作留言：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="textarea" id="textarea" cols="85" rows="4"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                批发商操作：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="submit" name="button3" id="button3" value=" 保存修改 " />
            </td>
        </tr>
        <!--  <tr>
    <td colspan="2" align="left" bgcolor="#FFFFFF"><b>游客详细信息</b><span class="daorumd"><a href="javascript:ViewAllCarPlan2();">导入名单</a></span></td>
  </tr>
-->
        <tr>
            <td colspan="2" align="center" bgcolor="#FFFFFF">
                <%--<table width="100%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc">
                    <tr>
                        <th>
                            序号
                        </th>
                        <th>
                            姓名
                        </th>
                        <th>
                            联系电话
                        </th>
                        <th>
                            身份证
                        </th>
                        <th>
                            护照
                        </th>
                        <th>
                            其他证件
                        </th>
                        <th>
                            性别
                        </th>
                        <th>
                            类型
                        </th>
                        <th>
                            座号
                        </th>
                        <th>
                            备注（勾选保存入常旅客库）
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            1
                        </td>
                        <td align="center">
                            <input value="游客" size="8" name="CustomerName1" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo143" id="CertificateNo142" size="11" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1433" id="CertificateNo1432" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1435" id="CertificateNo1434" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1437" id="CertificateNo1436" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <select id="select" name="select">
                                <option selected="selected" value="1">男</option>
                                <option value="0">女</option>
                            </select>
                        </td>
                        <td align="center">
                            <select id="select3" name="select3">
                                <option value="1">成人</option>
                                <option value="0">儿童</option>
                            </select>
                        </td>
                        <td align="center">
                            <input name="Input" id="Input" size="10" />
                        </td>
                        <td align="center">
                            <input name="Input3" id="Input3" size="30" style="width: 120px;" />
                            <input name="checkbox" type="checkbox" value="checkbox" checked="checked" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            2
                        </td>
                        <td align="center">
                            <input value="游客" size="8" name="CustomerName12" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1432" id="CertificateNo143" size="11" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1434" id="CertificateNo1433" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1436" id="CertificateNo1435" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <input name="CertificateNo1438" id="CertificateNo1437" size="18" style="width: 80px;" />
                        </td>
                        <td align="center">
                            <select id="select2" name="select2">
                                <option selected="selected" value="1">男</option>
                                <option value="0">女</option>
                            </select>
                        </td>
                        <td align="center">
                            <select id="select4" name="select4">
                                <option value="1">成人</option>
                                <option value="0">儿童</option>
                            </select>
                        </td>
                        <td align="center">
                            <input name="Input2" id="Input2" size="10" />
                        </td>
                        <td align="center">
                            <input name="Input32" id="Input32" size="30" style="width: 120px;" />
                            <input name="checkbox2" type="checkbox" value="checkbox" checked="checked" />
                        </td>
                    </tr>
                </table>--%>
    <div id="<%=tblID %>" class="tablebox">
        <!--添加信息表格-->
        <table width="100%" border="0" align="center">
            <tbody>
                <tr>
                    <td valign="top" align="left" class="ftxt">
                        <table cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center"
                            style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td valign="top" align="left" colspan="2">
                                        <img src="<%=ImageServerUrl %>/images/jiben3.gif">
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>订单号</strong>：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="lblOrderNo" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>线路名称：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblRouteName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>出团日期：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblLeaveDate"></asp:Label>
                                        <strong>当前剩余空位：</strong><asp:Label runat="server" Text="" ID="lblCount"></asp:Label>&nbsp;&nbsp;
                                        <strong>状态：</strong><asp:Label runat="server" Text="" ID="lblTourState"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="18%" bgcolor="#F2F9FE" align="right">
                                        <strong>发布单位：</strong>
                                    </td>
                                    <td align="left">
                                        <input type="hidden" id="hideTravelID" runat="server" />
                                        <input id="txtTravel" name="txtTravel" value="" runat="server" readonly="readonly" />
                                        <a id="btnSelectTravel" href="javascript:void(0);">
                                            <img width="28" height="18" align="absmiddle" src="<%=ImageServerUrl %>/images/icon_select.jpg"></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>出发城市：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblLeaveCity"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>交通：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblCar"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>出发时间 航班：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblLeaveCon"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>返回时间 航班：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblBackCon"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>集合说明：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblMsg"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>领队全陪说明：</strong>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" Text="" ID="lblAllMsg"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>游客联系人：</strong>
                                    </td>
                                    <td align="left">
                                        <input id="txtContact" name="txtContact" value="" runat="server">
                                        联系电话
                                        <input id="txtConTactTel" name="txtConTactTel" value="" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>商家负责人：</strong>
                                    </td>
                                    <td align="left">
                                        <input id="txtFzr" name="txtFzr" runat="server">
                                        联系电话
                                        <input id="txtFzrTel" name="txtFzrTel" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>市场价：</strong>
                                    </td>
                                    <td align="left">
                                        成人
                                        <asp:Label runat="server" Text="" ID="lblRetailAdultPrice"></asp:Label>
                                        元，儿童<asp:Label runat="server" Text="" ID="lblRetailChildrenPrice"></asp:Label>
                                        元，单房差
                                        <asp:Label runat="server" Text="" ID="lblMarketPrice"></asp:Label>
                                        元
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        结算价：
                                    </td>
                                    <td align="left">
                                        成人
                                        <asp:Label runat="server" Text="" ID="lblSettlementAudltPrice"></asp:Label>
                                        元，儿童
                                        <asp:Label runat="server" Text="" ID="lblSettlementChildrenPrice"></asp:Label>
                                        元 增减销售价
                                        <input id="txtAddPrice" name="txtAddPrice" runat="server">
                                        元
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>价格组成：</strong>
                                    </td>
                                    <td align="left">
                                        成人
                                        <input id="txtAdultCount" name="txtAdultCount" runat="server">
                                        人，儿童
                                        <input id="txtChildCount" name="txtChildCount" runat="server">
                                        人，单房差
                                        <input id="txtOtherCount" name="txtOtherCount" runat="server">人
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <uc1:ordercustomer id="OrderCustomer1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>组团社备注：</strong>
                                    </td>
                                    <td align="left">
                                        <textarea rows="4" cols="85" id="txtRemark" runat="server"></textarea>
                                    </td>
                                </tr>
                                <tr sizset="14" sizcache="0">
                                    <td bgcolor="#F2F9FE" align="right">
                                        <strong>总金额：</strong>
                                    </td>
                                    <td align="left" sizset="14" sizcache="0">
                                        销售价<asp:Label runat="server" Text="" ID="lblReailPriceAll"></asp:Label>
                                        &nbsp; 结算价
                                        <asp:Label runat="server" Text="" ID="lblSettlePriceAll"></asp:Label>
                                    </td>
                                </tr>
                                <tr sizset="14" sizcache="0">
                                    <td align="center" colspan="2">
                                        <a class="baocun_btn" href="javascript:void(0);" id="btnSave">保 存</a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script type="text/javascript">
    var AddOrderByRoute = {
        BoxDiv: $("#<%=tblID %>"),
        TourID: '<%=Request.QueryString["tourID"] %>',
        UpdatePrice: function() {
            var adultCount = parseInt(this.BoxDiv.find("[id$=<%=txtAdultCount.ClientID %>]").val());  //成人数
            var childCount = parseInt(this.BoxDiv.find("[id$=<%=txtChildCount.ClientID %>]").val());  //儿童数
            var otherCount = parseInt(this.BoxDiv.find("[id$=<%=txtOtherCount.ClientID %>]").val());  //单房差
            var reAdultPrice = parseInt(this.BoxDiv.find("[id$=<%=lblRetailAdultPrice.ClientID %>]").html());  //成人市场价
            var reChilePrice = parseInt(this.BoxDiv.find("[id$=<%=lblRetailChildrenPrice.ClientID %>]").html()); //儿童市场价
            var reOtherPrice = parseInt(this.BoxDiv.find("[id$=<%=lblMarketPrice.ClientID %>]").html());    //单房差价格
            var seAdultPrice = parseInt(this.BoxDiv.find("[id$=<%=lblSettlementAudltPrice.ClientID %>]").html()); //成人结算价
            var seChildPrice = parseInt(this.BoxDiv.find("[id$=<%=lblSettlementChildrenPrice.ClientID %>]").html()); //儿童结算价
            var addPrice = parseInt(this.BoxDiv.find("[id$=<%=txtAddPrice.ClientID %>]").val());  //增减结算价

            //处理错误数据
            if (!adultCount > 0) { adultCount = 0; }
            if (!childCount > 0) { childCount = 0; }
            if (!otherCount > 0) { otherCount = 0; }
            if (!reAdultPrice > 0) { reAdultPrice = 0; }
            if (!reChilePrice > 0) { reChilePrice = 0; }
            if (!reOtherPrice > 0) { reOtherPrice = 0; }
            if (!seAdultPrice > 0) { seAdultPrice = 0; }
            if (!seChildPrice > 0) { seChildPrice = 0; }
            if (!addPrice > 0) { addPrice = 0; }

            //市场总价
            var rePriceAll = adultCount * reAdultPrice + childCount * reChilePrice + otherCount * reOtherPrice;
            rePriceAll = parseInt(rePriceAll * 100) / 100;
            //结算总价
            var sePriceAll = adultCount * seAdultPrice + childCount * seChildPrice + addPrice;
            sePriceAll = parseInt(sePriceAll * 100) / 100;

            this.BoxDiv.find("[id$=<%=lblReailPriceAll.ClientID %>]").html(rePriceAll);
            this.BoxDiv.find("[id$=<%=lblSettlePriceAll.ClientID %>]").html(sePriceAll);
        },
        SubmitForm: function() {
            var adultCount = parseInt($.trim(AddOrderByRoute.BoxDiv.find("[id$=<%=txtAdultCount.ClientID %>]").val()));
            if (!adultCount > 0) {
                alert("请输入成人数!");
                return;
            }
            if ($.trim(AddOrderByRoute.BoxDiv.find("[id$=<%=txtTravel.ClientID %>]").val()) == "") {
                alert("请选择组团社!");
                return;
            }
            this.BoxDiv.find("#btnSave").html("提交中..");
            this.BoxDiv.find("#btnSave").unbind();
            $.ajax({
                type: "POST",
                url: "/LineManage/RouteAgency/AddOrderByRoute.aspx?tourID=" + AddOrderByRoute.TourID + "&dotype=save&v=" + Math.random(),
                data: AddOrderByRoute.BoxDiv.find("#btnSave").closest("form").serialize(),
                cache: false,
                success: function(state) {
                    if (state == "ok") {
                        alert("订单提交成功!");
                        topTab.open("/LineManage/RouteAgency/neworders.aspx", "最新散客订单");
                    } else {
                        alert(state);
                        AddOrderByRoute.BoxDiv.find("#btnSave").html("保 存");
                        AddOrderByRoute.BoxDiv.find("#btnSave").click(function() {
                            AddOrderByRoute.SubmitForm();
                        })
                    }
                },
                error: function() {
                    alert("服务器忙,请稍后再试!");
                    AddOrderByRoute.BoxDiv.find("#btnSave").html("保 存");
                    AddOrderByRoute.BoxDiv.find("#btnSave").click(function() {
                        AddOrderByRoute.SubmitForm();
                    })
                }
            });
        }
    }

    function backCallFun(data) {
        AddOrderByRoute.BoxDiv.find("#<%=hideTravelID.ClientID %>").val(data.comID);
        AddOrderByRoute.BoxDiv.find("#<%=txtTravel.ClientID %>").val(data.comName);
        AddOrderByRoute.BoxDiv.find("#<%=txtFzr.ClientID %>").val(data.conName);
        AddOrderByRoute.BoxDiv.find("#<%=txtFzrTel.ClientID %>").val(data.conTel);
    }
    $(function() {
        AddOrderByRoute.BoxDiv.find("#btnSave").click(function() {

            AddOrderByRoute.SubmitForm();
        })
        AddOrderByRoute.BoxDiv.find("#<%=txtAdultCount.ClientID %>,#<%=txtChildCount.ClientID %>,#<%=txtOtherCount.ClientID %>,#<%=txtAddPrice.ClientID %>").blur(function() {

            AddOrderByRoute.UpdatePrice();
        })

        AddOrderByRoute.BoxDiv.find("#btnSelectTravel").click(function() {
            Boxy.iframeDialog({ title: "选择单位", iframeUrl: "/LineManage/QueryTour.aspx?type=new&backCallFun=backCallFun", height: 400, width: 760, draggable: false });
            return false;
        })


    })
    </script>

    </form>
</body>
</html>
