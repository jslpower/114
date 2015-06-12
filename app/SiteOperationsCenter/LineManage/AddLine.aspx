<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AddLine.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.AddLine" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/Linecontrol/TourStandardPlan.ascx" TagName="TourStandardPlan"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/Linecontrol/TourServiceStandard.ascx" TagName="TourServiceStandard"
    TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>线路管理-添加线路</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
        var commonTourModuleData = {
            _data: [],
            add: function(obj) {
                this._data[obj.ContainerID] = obj;
            },
            get: function(id) {
                return this._data[id];
            }
        };
    </script>

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: 'form1',
            ReleaseType: 'AddStandardTour'
        });
    </script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("CommonTourModule") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"
        cache="true"></script>

    <script type="text/javascript" language="javascript">
        //<!CDATA**行程**按钮切换[
        function g(o) { return document.getElementById(o); }
        function HoverLi(n) {
            //如果有N个标签,就将i<=N;
            //本功能非常OK,兼容IE7,FF,IE6;
            for (var i = 1; i <= 2; i++) { g('tb_' + i).className = 'normaltab'; g('tbc_0' + i).className = 'undis'; } g('tbc_0' + n).className = 'dis'; g('tb_' + n).className = 'hovertab';
            if (n == 2) {
                TourModule.TourDaysFocus('form1', '100');
            }
        }
        //如果要做成点击后再转到请将<li>中的onmouseover 改成 onclick;
        //]]>
    </script>

    <script type="text/javascript" language="javascript">
        //<!CDATA**报价包含**按钮切换[
        function g(o) { return document.getElementById(o); }
        function HoverLi1(n) {
            //如果有N个标签,就将i<=N;
            //本功能非常OK,兼容IE7,FF,IE6;
            for (var i = 1; i <= 2; i++) { g('tb1_' + i).className = 'normaltab1'; g('tbc1_0' + i).className = 'undis1'; } g('tbc1_0' + n).className = 'dis1'; g('tb1_' + n).className = 'hovertab1';
        }
        //如果要做成点击后再转到请将<li>中的onmouseover 改成 onclick;
        //]]>
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidOperatType" runat="server" />
    <table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#CCCCCC"
        class="lr_hangbg table_basic ">
        <tr>
            <td colspan="2" align="left" valign="top" bgcolor="#FFFFFF">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/jiben3.gif" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font>线路来源：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="Td4">
                <input type="radio" name="radiolxs" id="chkzx" runat="server" onclick="Changechkdj(this)"
                    value="1" /><label for="chkzx" hidefocus="false">专线商</label>
                <input type="radio" name="radiolxs" id="chkdj" runat="server" onclick="Changechkdj(this)"
                    value="2" /><label for="chkdj" hidefocus="false">地接社</label>
            </td>
        </tr>
        <tr>
            <td width="16%" align="right">
                <font class="ff0000">*</font> 专线类型：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropWord" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font>线路区域：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:HiddenField ID="hideAreaTypeID" runat="server" />
                <asp:TextBox ID="txtAreaType" runat="server" ReadOnly="true" BackColor="#dadada"></asp:TextBox><a
                    id="a_AreaType" href="javascript:void(0);">
                    <img width="28" height="18" align="absmiddle" src="<%=ImageServerUrl %>/images/icon_select.jpg"></a>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 旅行社：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="Td2">
                <asp:HiddenField ID="hideCompanyID" runat="server" />
                <asp:TextBox ID="txtCompany" runat="server" ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                <a id="btnSelectTravel" href="javascript:void(0);">
                    <img width="28" height="18" align="absmiddle" src="<%=ImageServerUrl %>/images/icon_select.jpg"></a>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 联系人：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="Td3">
                <asp:DropDownList ID="dropPublisher" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 销售区域：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="trBindSaleCite">
                <asp:Label ID="lblSSArea" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 线路名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input name="txt_LineName" type="text" id="txt_LineName" runat="server" size="45" />
            </td>
        </tr>
        <tr>
            <td align="right">
                B2C网站线路名：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input id="txt_D2BwebSite" size="60" name="txt_D2BwebSite" runat="server" />
                （易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                B2B显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropB2B" runat="server">
                </asp:DropDownList>
                <input id="txt_B2B" runat="server" size="10" name="txt_B2B" value="50" valid="required|limit"
                    max="100" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                B2C显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropB2C" runat="server">
                </asp:DropDownList>
                <input id="txt_B2C" size="10" name="txt_B2C" runat="server" max="100" value="50" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                类型：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropRecommendType" runat="server">
                </asp:DropDownList>
                （推荐状态只有易诺可以修改，默认推荐线路有排序加成）
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 大交通：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                出发：
                <asp:DropDownList ID="dropStartTraffic" runat="server">
                </asp:DropDownList>
                返回：
                <asp:DropDownList ID="dropEndTraffic" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr name="showCity" style="display: none;">
            <td align="right">
                主要游览城市：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="trlistMBrowseCity">
            </td>
        </tr>
        <tr name="showword" style="display: none;">
            <td align="right">
                主要游览国家：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="trlistMBrowseCountryControl">
            </td>
        </tr>
        <tr name="showword" style="display: none;">
            <td align="right">
                签证或通行证：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="tdIsviso">
                <label for="<%= AddStandardTour_radIsviso0.ClientID %>" hidefocus="false">
                    <input runat="server" type="checkbox" name="IsOrNoradIsviso" onclick="IsorNoViso(this)" id="AddStandardTour_radIsviso0"
                        value="0" />免签</label>
            </td>
        </tr>
        <tr>
            <td align="right">
                线路定金：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                成人
                <input id="txt_Adultdeposit" runat="server" size="10" name="txt_Adultdeposit" />
                元 儿童
                <input name="txt_Childrendeposit" id="txt_Childrendeposit" type="text" runat="server" />
                元（输入0 表示无需支付定金）
            </td>
        </tr>
        <tr>
            <td align="right">
                线路配图：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <uc3:SingleFileUpload ID="SfUpload" runat="server" IsGenerateThumbnail="true" ImageHeight="400"
                    ImageWidth="300" />
                <asp:Literal ID="lalUploadImg" runat="server"></asp:Literal>
                <asp:HiddenField ID="hidUploadImg" runat="server" />
                <span id="errMsgsfuPhotoImg" style="color: Red"></span>（最佳比例3:2，不大于2M，上传后生成等比缩放到800分辨率一下）
            </td>
        </tr>
        <tr>
            <td align="right">
                线路特色：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_LineFeatures" id="txt_LineFeatures" runat="server" cols="65"
                    rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                独立成团：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                最小成团人数
                <input name="txt_Tourminnumber" runat="server" id="txt_Tourminnumber" type="text"
                    size="10" />
                团队参考价格：
                <input name="txt_TeamPrice" id="txt_TeamPrice" runat="server" type="text" size="10" />
                （仅供参考，市场价，0为一团一议）
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 天数：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input name="AddStandardTour_txtTourDays" id="AddStandardTour_txtTourDays" runat="server"
                    type="text" class="bitian" size="5" valid="required|RegInteger|custom" custom="CheckMaxDay" />
                <span id="errMsg_<%=AddStandardTour_txtTourDays.ClientID %>" class="errmsg"></span>
                天
                <input name="txt_Late" id="txt_Late" runat="server" type="text" size="6" />
                晚 （<font class="ff0000">住宿天数</font>）
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 提前几天报名：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                提前
                <input id="txt_AdvanceDayRegistration" size="4" name="txt_AdvanceDayRegistration"
                    runat="server" />
                天（用于计算报名截止时间，请尽量写准确）
                <input type="checkbox" name="chkIsCertain" id="chkIsCertain" value="1" runat="server" />
                散客报名无需成团，铁定发团
            </td>
        </tr>
        <tr>
            <td align="right">
                线路主题：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="tdMThemeControl">
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 出发城市：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="tdBindLeaveCity">
                <asp:Literal ID="litLeaveCity" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font class="ff0000">*</font> 返回城市：
            </td>
            <td align="left" bgcolor="#FFFFFF" id="tdBindBackCity">
                <asp:Literal ID="litbackcity" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_xc">
            <td align="right" valign="top" style="padding-top: 50px;">
                <font class="ff0000">*</font>行程(标准版可通过天数来控制)：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <div id="tb_" class="tb_">
                                <ul>
                                    <li id="tb_1" class="hovertab" onclick="HoverLi(1);"><a>简易版</a></li>
                                    <li id="tb_2" class="normaltab" onclick="HoverLi(2);"><a>标准版</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 96%;">
                                <div class="dis" id="tbc_01">
                                    <asp:TextBox ID="txtTravel" runat="server" valid="required"></asp:TextBox>
                                </div>
                                <div class="undis" id="tbc_02">
                                    <uc1:TourStandardPlan ID="AddStandardTour_StandardPlan" ContainerID="form1" runat="server"
                                        ReleaseType="AddStandardTour" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="padding-top: 50px;">
                <font class="ff0000">*</font> 报价包含：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <div id="tb1_" class="tb1_">
                                <ul>
                                    <li id="tb1_1" class="hovertab1" onclick="HoverLi1(1);"><a>简易版</a></li>
                                    <li id="tb1_2" class="normaltab1" onclick="HoverLi1(2);"><a>标准版</a> </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 80%;">
                                <div class="dis1" id="tbc1_01">
                                    <textarea name="txt_Priceincludes" id="txt_Priceincludes" runat="server" cols="85"
                                        rows="5"></textarea>
                                </div>
                                <div class="undis1" id="tbc1_02">
                                    <uc2:TourServiceStandard ID="AddStandardRoute_ServiceStandard" runat="server" ReleaseType="AddStandardRoute"
                                        ContainerID="form1" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                报价不含：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_PriceExcluding" id="txt_PriceExcluding" runat="server" cols="85"
                    rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                赠送项目：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Giftitems" id="txt_Giftitems" runat="server" cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                儿童及其他安排：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Childrenandotherarrangements" id="txt_Childrenandotherarrangements"
                    runat="server" cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                购物安排：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Shoppingarrangements" id="txt_Shoppingarrangements" runat="server"
                    cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                自费项目：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Projectattheirownexpense" id="txt_Projectattheirownexpense" runat="server"
                    cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                销售商须知：<br />
                只有组团社能看到：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_VendorInformation" id="txt_VendorInformation" runat="server"
                    cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Remarks" id="txt_Remarks" runat="server" cols="85" rows="5"></textarea>
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" align="center" bgcolor="#FFFFFF">
                <asp:Button ID="btn_save" runat="server" Text="保存后，返回线路库" OnClick="btn_save_Click" />
            </td>
        </tr>
        <%--销售城市--%>
        <asp:HiddenField ID="hidsqlcity" runat="server" />
        <%--行程类型--%>
        <asp:HiddenField ID="hidReleaseType" runat="server" />
        <%--专线商id--%>
        <asp:HiddenField ID="hideTravelID" runat="server" />
        <%--专线商名称--%>
        <asp:HiddenField ID="hideTravelName" runat="server" />
        <%--联系人ID--%>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <%--联系人名称--%>
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>

    <script type="text/javascript" src="/kindeditor/kindeditor-min.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">
        $("#form1").find("input[type=text][id$=TourDays]").attr("errmsg", "请填写天数!|天数只能是数字!|天数不能大于100天!");
        CheckFormIsChange.recodeInitialDataForm($("#form1").get(0));

        function CheckMaxDay(e, formElements) {
            if (parseInt(e.value) <= parseInt(100)) {
                return true;
            } else {
                return false;
            }
        }

        KE.init({
            id: '<%=txtTravel.ClientID %>', //编辑器对应文本框id
            width: '700px',
            height: '150px',
            skinsPath: '/kindeditor/skins/',
            pluginsPath: '/kindeditor/plugins/',
            scriptPath: '/kindeditor/skins/',
            resizeMode: 0, //宽高不可变
            items: keSimple //功能模式(keMore:多功能,keSimple:简易)
        });


        //选择地接社
        function Changechkdj(obj) {
            var midvalue = "";
            if ($("#hidsqlcity").val() != "" || $("#hidsqlcity").val() != "全国")
                midvalue = $("#hidsqlcity").val();
            else
                midvalue = $("#trBindSaleCite").html();
            if ($("#chkdj").attr("checked")) {
                $("#hidsqlcity").val(midvalue);
                $("#trBindSaleCite").html("全国");
            }
            else {
                $("#trBindSaleCite").html("");
                $("#trBindSaleCite").html(midvalue);
            }
        }



        //专线类型（国内国际周边等等）
        function OnchangeWord(v, t, s) {

            if ("<%=lineid %>" != "") {
                return false;
            }

            var LineType = $("#dropWord").val();
            var LineID = $("#<%=hideAreaTypeID.ClientID %>").val();

            if (LineType == "1") {
                $("tr[name='showword']").show();
                $("tr[name='showCity']").hide();
            }
            else {
                $("tr[name='showword']").hide();
                $("tr[name='showCity']").show();

            }

            //选中地接社
            if (s != null) {
                var selectDj = null;
                var selectval = null;
                if (s.toString().split('$').length == 2) {
                    selectval = s.toString().split('$')[0];
                    selectDj = s.toString().split('$')[1];
                }
            }
            //选中地接社
            var IsChecked = $("#chkdj").attr("checked");
            $.ajax({
                url: "AddLine.aspx?type=" + t + "&argument=" + v + "&LineId=<%=lineid %>&IsChecked=" + IsChecked,
                cache: false,
                type: "GET",
                dataType: "json",
                success: function(result) {
                    switch (t) {
                        case "GetContactByBusinessLineId": //获取联系人通过专线商
                            var listContact = result.tolist;
                            $("#<%=dropPublisher.ClientID %>").html("");
                            $("#<%=dropPublisher.ClientID %>").append("<option value=\"0\">请选择</option>");
                            for (var x = 0; x < listContact.length; x++) {
                                $("#<%=dropPublisher.ClientID %>").append("<option value=\"" + listContact[x].ID + "\">" + listContact[x].UserNameID + "</option>");
                            }
                            setTimeout(function() {
                                $("#<%=dropPublisher.ClientID %> option[value='" + s + "']").attr("selected", true);
                            }, 1);
                            break;
                    }
                },
                error: function() {
                    alert("操作失败!");
                }
            });
        }



        //销售城市  出港城市  主要预览国家
        function OnchangeWordByGey(v, t, selected) {
            if ("<%=lineid %>" != "") {
                return false;
            }
            var LineType = $("#dropWord").val();
            if (LineType == "1") {
                $("tr[name='showword']").show();
                $("tr[name='showCity']").hide();
                if (t == "listMBrowseCity")
                    return false;
            }
            else {
                $("tr[name='showword']").hide();
                $("tr[name='showCity']").show();
                if (t == "listMBrowseCountryControl")
                    return false;
            }

            $.ajax({
                url: "AddLine.aspx?type=" + t + "&argument=" + v + "&LineId=<%=lineid %>",
                cache: false,
                type: "GET",
                dataType: "html",
                success: function(result) {
                    switch (t) {
                        case "BindSaleCite": //获取销售城市
                            $("#hidsqlcity").val(result);
                            if ($("#chkdj").attr("checked"))
                                $("#trBindSaleCite").html("全国");
                            else
                                $("#trBindSaleCite").html(result);
                            break;
                        case "BindLeaveCity": //获取出港城市
                            $("#tdBindLeaveCity").html(result);
                            break;
                        case "BindBackCity": //获取返回城市
                            $("#tdBindBackCity").html(result);
                            break;
                        case "listMBrowseCountryControl": //主要预览国家
                            $("#trlistMBrowseCountryControl").html(result);
                            break;
                        case "listMBrowseCity": //主要预览城市
                            $("#trlistMBrowseCity").html(result);
                            break;

                    }
                },
                error: function() {
                    alert("操作失败!");
                }
            });
        }




        //选择是否签证
        function IsViso(obj) {
            var str = "";

            if ($("#tdIsviso #AddStandardTour_radIsviso" + $(obj).val() + "").length > 0) {
                if (!$(obj).attr("checked")) {
                    $("#tdIsviso #AddStandardTour_radIsviso" + $(obj).val()).closest('label').remove();
                }
            }
            else {
                $("#tdIsviso").append("<label for=\"AddStandardTour_radIsviso" + $(obj).val() + "\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardTour_radIsviso\" id=\"AddStandardTour_radIsviso" + $(obj).val() + "\"  value=\"" + $(obj).val() + "\" />" + $(obj).closest('label').text() + "签证</label>");
            }
        }

        //是否免签
        var str = "";
        function IsorNoViso(obj) {
            if ($(obj).attr("checked")) {
                if ($("#tdIsviso input[name=AddStandardTour_radIsviso]").length > 0) {
                    $("#tdIsviso input[name=AddStandardTour_radIsviso]").each(function() {
                        if ($(this).attr("checked")) {
                            str += $(this).val() + "&";
                        }
                        $(this).attr("checked", "");
                        $(this).attr("disabled", "disabled");
                    })
                }
            }
            else {
                if ($("#tdIsviso input[name=AddStandardTour_radIsviso]").length > 0) {
                    $("#tdIsviso input[name=AddStandardTour_radIsviso]").each(function() {
                        $(this).attr("disabled", "");
                    })
                }

                var strlist = str.split('&');
                $.each(strlist, function(j) {
                    if (strlist[j] != "") {
                        $("#tdIsviso #AddStandardTour_radIsviso" + strlist[j]).attr("checked", "checked");
                    }
                })
            }
        }



        //专线以及专线商赋值
        function SelectValue(vLine, vpublicer) {

        }

        $("#<%=AddStandardTour_txtTourDays.ClientID %>").blur(function() {
            TourModule.TourDaysFocus('form1', '100');
        });




        //主题
        function SetMThemeControl(v) {
            $("#tdMThemeControl").html(v);
        }
        //出发城市
        function SetLeaveCity(v) {
            $("#tdBindLeaveCity").html(v);
        }
        //返回城市
        function SetBackCity(v) {
            $("#tdBindBackCity").html(v);
        }
        //销售城市
        function SetSaleCite(v) {
            $("#trBindSaleCite").html(v);
        }
        //主要预览城市
        function SetBrowseCity(v) {
            $("#trlistMBrowseCity").html(v);
        }

        //主要预览国家
        function SetBrowseCountryControl(v) {
            $("#trlistMBrowseCountryControl").html(v);
        }

        //主要预览国家签证
        function SetAddStandardTour_radIsviso(v) {
            $("#tdIsviso").append(v);
        }


        function OpenDialoge(CompanyId, LeaveCity) {
            Boxy.iframeDialog({ title: "设置常用出港城市", iframeUrl: "/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&CityId=" + LeaveCity + "&type=LeaveCity&ContainerID=form1&rnd=" + Math.random(), height: 650, width: 450 })
        }

        function backCallFun(data, key) {

        }
        
        
    </script>

    <script type="text/javascript">

        var isSubmit = false;
        $(function() {

            $("#a_AreaType").click(function() {
                if ($("#<%=dropWord.ClientID %>").val() == "-1") {
                    alert("请选择专线类型");
                    return false;
                }
                Boxy.iframeDialog({ title: "专线类型", iframeUrl: "/LineManage/AreaSelect.aspx?backCallFun=BackCallFun&typeId=" + $("#<%=dropWord.ClientID %>").val() + "&rnd=" + Math.random(), height: 400, width: 750, draggable: false });
                return false;
            })

            $("#btnSelectTravel").click(function() {
                var AeraID = $("#<%=hideAreaTypeID.ClientID %>").val();
                var comType = $("input[name='radiolxs']:checked").val();
                if (!comType) {
                    alert("请选择线路来源！");
                    return;
                }
                Boxy.iframeDialog({ title: "选择单位", iframeUrl: "/LineManage/QueryTour.aspx?type=new&comType=" + comType + "&AeraID=" + AeraID + "&backCallFun=BackCallFun", height: 400, width: 750, draggable: false });
                return false;
            })

            var linetype = $("#<%=dropWord.ClientID %>").val();
            var line = $("#<%=hideAreaTypeID.ClientID %>").val();
            KE.create('<%=txtTravel.ClientID %>', 0);

            if (linetype == "1")//线路区域类型
            {
                $("tr[name='showword']").show();
            }
            $("#<%=btn_save.ClientID%>").click(function() {
                if (isSubmit) {
                    //如果按钮已经提交过一次验证，则返回执行保存操作
                    return true;
                }

                //线路来源
                if (!$("#chkzx").attr("checked") && !$("#chkdj").attr("checked")) {
                    alert("请选择线路来源");
                    return false;
                }
                //线路来源


                //专线类型  专线商  专线联系人
                var linetype = $("#<%=dropWord.ClientID%>").val(); //专线类型
                var line = $("#<%=hideAreaTypeID.ClientID%>").val(); //专线
                var businessLine = $("#<%=hideCompanyID.ClientID%>").val(); //专线商
                var contactname = $("#<%=dropPublisher.ClientID%>").val(); //联系人
                if ('<%=lineid %>' == "" && linetype != "3") {
                    if (linetype == "-1") {
                        alert("请选择专线类型");
                        $("#<%=dropWord.ClientID%>").focus();
                        return false;
                    }
                    if (line == "0") {
                        alert("请选择专线");
                        $("#<%=txtAreaType.ClientID%>").focus();
                        return false;
                    }
                    if (businessLine == "0") {
                        alert("请选择专线商");
                        $("#<%=hideCompanyID.ClientID%>").focus();
                        return false;
                    }
                    if (contactname == "0") {
                        alert("请选择联系人");
                        $("#<%=dropPublisher.ClientID%>").focus();
                        return false;
                    }
                }

                //销售区域
                var num = 0;
                $("input[name='AddLine_chkSaleCity']").each(function() {
                    if ($(this).attr("checked")) {
                        num++;
                    }
                });
                if (num == 0) {
                    if (!$("#chkdj").attr("checked")) {
                        if ($.trim($("#trBindSaleCite").html()) != "") {
                            alert("请选择销售区域");
                            return false;
                        }
                    }
                }

                //线路名称
                var Linename = $("#<%=txt_LineName.ClientID%>").val();
                if (Linename == "") {
                    alert("请输入线路名称");
                    $("#<%=txt_LineName.ClientID%>").focus();
                    return false;
                }

                //大交通
                var start = $("#<%=dropStartTraffic.ClientID%>").val(); //出发交通
                var back = $("#<%=dropEndTraffic.ClientID%>").val(); //回来交通
                if (start == "0" || back == "0") {
                    alert("请选择大交通");
                    $("#<%=dropStartTraffic.ClientID%>").focus();
                    return false;
                }


                //线路配图
                if (SfUpload.getStats().files_queued <= 0) {
                    var hidimg = $("<%=hidUploadImg.ClientID %>").val();
                    if (hidimg == "") {
                        $("#errMsgsfuPhotoImg").html("请选择一张图片");
                        return false;
                    }
                }

                if (SfUpload.getStats().files_queued > 0) {//有图片
                    SfUpload.customSettings.UploadSucessCallback = FormSubmit;
                    SfUpload.startUpload();
                    return false;
                }


                //天数
                var day = $("#<%=AddStandardTour_txtTourDays.ClientID%>").val(); //天数
                if (day == "" || day == "0") {
                    alert("请输入天数必须大于0");
                    $("#<%=AddStandardTour_txtTourDays.ClientID%>").focus();
                    return false;
                }

                //出发城市
                num = 0;
                $("input[name='AddStandardTour_radPortCity']").each(function() {
                    if ($(this).attr("checked")) {
                        num++;
                    }
                });
                if (num == 0) {
                    if (linetype != "3") {
                        alert("请选择出发城市");
                        return false;
                    }
                }
                //返回城市
                num = 0;
                $("input[name='AddStandardTour_radBackCity']").each(function() {
                    if ($(this).attr("checked")) {
                        num++;
                    }
                });
                if (num == 0) {
                    if (linetype != "3") {
                        alert("请选择返回城市");
                        return false;
                    }
                }



                //提前几天报名
                var earlyday = $("#<%=txt_AdvanceDayRegistration.ClientID%>").val(); //提前天数
                if (earlyday == "" || earlyday == "0") {
                    alert("请输入提前天数必须大于0");
                    $("#<%=txt_AdvanceDayRegistration.ClientID%>").focus();
                    return false;
                }

                //提前几天报名


                //判断行程标准版的交通工具
                var dropReleaseType = $("#tb_2").attr("class"); //专线发布类型
                var number = 0;
                if (dropReleaseType == "hovertab") {
                    $("#tr_xc select").each(function() {
                        if ($(this).val() == "") {
                            number++;
                        }
                    })
                    if (number > 0) {
                        alert("请选择行程标准版的交通工具");
                        return false;
                    }
                    $("#hidReleaseType").val("0");
                }
                else {
                    if ($("#<%=txtTravel.ClientID%>").val() == "")//getEditorTextContents
                    {
                        alert("简易行程不能为空");
                        return false;
                    }
                    $("#hidReleaseType").val("1");
                }
                //判断行程标准版的交通工具

                //报价包含
                var Priceincludes2 = $("#tb1_1").attr("class");
                if (Priceincludes2 == "hovertab1") {
                    var Priceincludes = $("#<%=txt_Priceincludes.ClientID%>").val(); //报价包含
                    if (Priceincludes == "" && $("#tbc1_01").css("display") != "none") {
                        alert("报价包含不能为空");
                        $("#<%=txt_Priceincludes.ClientID%>").focus();
                        return false;
                    }
                    //else if (Priceincludes.length > 1000) {
                        //alert("报价包含为1000个子以内");
                        //$("#<%=txt_Priceincludes.ClientID%>").focus();
                        //return false;
                    //}
                    $("#AddStandardRouteCarContent").val("");
                    $("#AddStandardRouteDinnerContent").val("");
                    $("#AddStandardRouteGuideContent").val("");
                    $("#AddStandardRouteIncludeOtherContent").val("");
                    $("#AddStandardRouteResideContent").val("");
                    $("#AddStandardRouteSightContent").val("");
                    $("#AddStandardRouteTrafficContent").val("");
                }

                //团队版验证
                if ($("#tbc1_02").css("display") != "none") {
                    var isT = false;
                    //                    $("#tbc1_02").find("textarea").each(function() {
                    //                        if ($.trim($(this).val()) != "") {
                    //                            isT = true;
                    //                        }
                    //                    })
                    //                    if (isT == false) {
                    //                        alert("报价包含：包含项目不能为空!");
                    //                        $("#AddStandardRouteResideContent").focus();
                    //                        return false;
                    //                    }
                }

                //报价不含
                var PriceExcluding = $("#<%=txt_PriceExcluding.ClientID%>").val(); //报价包含
                //if (PriceExcluding == "") {
                    //alert("报价不含不能为空");
                    //$("#<%=txt_PriceExcluding.ClientID%>").focus();
                    //return false;
                //}
                //else if (PriceExcluding.length > 1000) {
                    //alert("报价不含为1000个子以内");
                    //$("#<%=txt_PriceExcluding.ClientID%>").focus();
                    //return false;
                //}
                //赠送项目
                var Giftitems = $("#<%=txt_Giftitems.ClientID%>").val(); //赠送项目
                //if (Giftitems.length > 1000) {
                    //alert("赠送项目为1000个子以内");
                    //$("#<%=txt_Giftitems.ClientID%>").focus();
                    //return false;
                //}

                //儿童及其他安排
                var Childrenandotherarrangements = $("#<%=txt_Childrenandotherarrangements.ClientID%>").val(); //儿童及其他安排
                //if (Childrenandotherarrangements.length > 1000) {
                    //alert("赠送项目为1000个子以内");
                    //$("#<%=txt_Childrenandotherarrangements.ClientID%>").focus();
                //}

                //购物安排
                var Shoppingarrangements = $("#<%=txt_Shoppingarrangements.ClientID%>").val(); //购物安排
                //if (Shoppingarrangements.length > 1000) {
                    //alert("购物安排为1000个子以内");
                    //$("#<%=txt_Shoppingarrangements.ClientID%>").focus();
                    //return false;
                //}

                //自费项目
                var Projectattheirownexpense = $("#<%=txt_Projectattheirownexpense.ClientID%>").val(); //自费项目
                //if (Projectattheirownexpense.length > 1000) {
                    //alert("自费项目为1000个子以内");
                    //$("#<%=txt_Projectattheirownexpense.ClientID%>").focus();
                    //return false;
                //}
                //销售商须知
                var VendorInformation = $("#<%=txt_VendorInformation.ClientID%>").val(); //销售商须知
                //if (VendorInformation.length > 1000) {
                    //alert("销售商须知为1000个子以内");
                    //$("#<%=txt_VendorInformation.ClientID%>").focus();
                    //return false;
                //}
                //备注
                var Remarks = $("#<%=txt_Remarks.ClientID%>").val(); //备注
                //if (Remarks.length > 1000) {
                    //alert("备注为1000个子以内");
                    //$("#<%=txt_Remarks.ClientID%>").focus();
                    //return false;
                //}
            });
            FV_onBlur.initValid($("#form1").get(0));
        });

        function FormSubmit() {
            isSubmit = true;
            $("#<%=btn_save.ClientID %>").click();
        }
       
    </script>

    <script type="text/javascript">
        function BackCallFun(data, type) {
            //选线路区域
            if (type == "1") {
                $("#<%=hideAreaTypeID.ClientID %>").val(data.id);
                $("#<%=txtAreaType.ClientID %>").val(data.name);
                //OnchangeWord(this.value, 'GetCompanyByLine', null);
                OnchangeWordByGey(data.id, 'listMBrowseCountryControl', null);
                OnchangeWordByGey(data.id, 'listMBrowseCity', null)
            }
            //选公司
            if (type == "2") {
                $("#<%=hideCompanyID.ClientID %>").val(data.comID);
                $("#<%=txtCompany.ClientID %>").val(data.comName);
                OnchangeWord(data.comID, 'GetContactByBusinessLineId', null);
                OnchangeWordByGey(data.comID, 'BindSaleCite', null);
                OnchangeWordByGey(data.comID, 'BindLeaveCity', null);
                OnchangeWordByGey(data.comID, 'BindBackCity', null);
            }
        }

    </script>

    </form>
</body>
</html>
