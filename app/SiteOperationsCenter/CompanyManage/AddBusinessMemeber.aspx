<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBusinessMemeber.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.AddBusinessMemeber" EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>商家会员添加</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetCityList") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_bg table_basic" id="tbCompanyInfo">
        <tr>
            <td width="13%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>省&nbsp;&nbsp;&nbsp;&nbsp;份：
            </td>
            <td width="87%" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropProvinceId" runat="server">
                </asp:DropDownList>
                <span class="unnamed1" style="display: none">*</span>城市：
                <asp:DropDownList ID="dropCityId" runat="server">
                </asp:DropDownList>
                <span class="unnamed1" style="display: none">*</span>县区：
                <asp:DropDownList ID="dropCountyId" runat="server">
                </asp:DropDownList>
                <span id="errMsg_Province" style="display: none" class="unnamed1">请选择省份</span> <span
                    id="errMsg_City" style="display: none" class="unnamed1">请选择城市</span><span id="errMsg_County"
                        style="display: none" class="unnamed1">请选择县区</span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>单位名称：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtCompanyName" runat="server" name="txtCompanyName" type="text" class="textfield"
                    size="25" valid="required" errmsg="请输入单位名称" />
                <span id="errMsg_txtCompanyName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                公司网址(不含http://)：
            </td>
            <td bgcolor="#FFFFFF">
                <input name="txtCompanyWebsite" id="txtCompanyWebsite" type="text" runat="server"
                    class="textfield" size="25" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>公司简称：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtCompanyOtherName" size="25" name="txtCompanyOtherName" runat="server"
                    class="textfield" valid="required" errmsg="请输入单位名称" />
                <span id="errMsg_txtCompanyOtherName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                品牌名：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtCompanyBrand" size="25" name="txtCompanyBrand" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                公司规模：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropCompanyScale" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                许可证号：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtLicenseNumber" runat="server" runat="server" name="txtLicenseNumber"
                    type="text" class="textfield" size="25" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>联系人：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtContactName" size="25" name="txtContactName" runat="server" errmsg="请输入联系人"
                    valid="required" />
                <span id="errMsg_txtContactName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                客服电话：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtContactTel" size="25" name="txtContactTel" runat="server"  />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                客服手机：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtContactMobile" size="25" runat="server" name="txtContactMobile" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                客服传真：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtContactFax" size="25" runat="server" name="txtContactFax" />
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                Email：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="hidContactEmail" runat="server" type="hidden" />
                <input id="txtContactEmail" runat="server" name="txtContactEmail" type="text" class="textfield"
                    size="40" valid="isEmail" errmsg="请输入正确的Email" />
                <span id="errMsg_txtContactEmail" class="errmsg"></span><span id="errMsg_ExitEmail"
                    class="errmsg" style="display: none">该Email已经注册</span>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                支付宝账户：
            </td>
            <td bgcolor="#FFFFFF">
                <input name="txtAlipayAccount" id="txtAlipayAccount" runat="server" type="text" class="textfield"
                    size="25" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <span class="bgcolor_sky_blue">品牌LOGO</span>：
            </td>
            <td bgcolor="#FFFFFF">
                <uc1:SingleFileUpload ID="SfpBrandLogo" runat="server" IsGenerateThumbnail="true"
                    ImageHeight="300" ImageWidth="400" />
                <asp:Literal ID="ltrBrandLogo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>办公地点：
            </td>
            <td bgcolor="#FFFFFF">
                <input id="txtOfficeAddress" runat="server" value="广州市海珠区工业大道中凤岗路3号" size="80" name="txtOfficeAddress"
                    errmsg="请输入办公地点" valid="required" />
                <span id="errMsg_txtOfficeAddress" class="errmsg"></span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                证书管理：
            </td>
            <td bgcolor="#FFFFFF">
                <table>
                    <tr>
                        <td>
                            营 业 执照：
                        </td>
                        <td>
                            <uc1:SingleFileUpload ID="SingleFilelLicence" runat="server" />
                            <span id="errMsg_hid1" class="errmsg"></span>
                        </td>
                        <td>
                            <asp:Literal ID="ltrLicenceImg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            经营许可证：
                        </td>
                        <td>
                            <uc1:SingleFileUpload ID="SingleFileBusinessCertImg" runat="server" />
                            <span id="errMsg_hid2" class="errmsg"></span>
                        </td>
                        <td>
                            <asp:Literal ID="ltrBusinessCertImg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            税务登记证：
                        </td>
                        <td>
                            <uc1:SingleFileUpload ID="SingleFileTaxRegImg" runat="server" />
                            <span id="errMsg_hid3" class="errmsg"></span>
                        </td>
                        <td>
                            <asp:Literal ID="ltrTaxRegImg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            授权 证 书：
                        </td>
                        <td>
                            <uc1:SingleFileUpload ID="SingleFileAuthorizeImg" runat="server" />
                            <span id="Span3" class="errmsg"></span>
                        </td>
                        <td>
                            <asp:Literal ID="ltrAuthorizeImg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            负责人身份证：
                        </td>
                        <td>
                            <uc1:SingleFileUpload ID="SingleFileCardsImg" runat="server" />
                            <span id="Span4" class="errmsg"></span>
                        </td>
                        <td>
                            <asp:Literal ID="ltrCardsImg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                公司介绍：
            </td>
            <td bgcolor="#FFFFFF">
                <FCKeditorV2:FCKeditor ID="txt_Description" ToolbarSet="Default" Height="420px" runat="server">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                业务优势：
            </td>
            <td bgcolor="#FFFFFF">
                <textarea rows="4" runat="server" id="txt_txtBusinessSuperior" name="txt_txtBusinessSuperior"
                    cols="70"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                公司照片：
            </td>
            <td bgcolor="#FFFFFF">
                <uc1:SingleFileUpload ID="Sfpcompanyimg1" runat="server" IsGenerateThumbnail="true" />
                <asp:Literal ID="ltrcompanyimg1" runat="server"></asp:Literal>
                <uc1:SingleFileUpload ID="Sfpcompanyimg2" runat="server" IsGenerateThumbnail="true" />
                <asp:Literal ID="ltrcompanyimg2" runat="server"></asp:Literal>
                <uc1:SingleFileUpload ID="Sfpcompanyimg3" runat="server" IsGenerateThumbnail="true" />
                <asp:Literal ID="ltrcompanyimg3" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                旅行社资质：
            </td>
            <td bgcolor="#FFFFFF">
                <%=strQualification%>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                电子地图位置：
            </td>
            <td bgcolor="#FFFFFF">
                (<label id="X" runat="server"></label>,<label id="Y" runat="server"></label>)
                <input type="button" name="Setmap" id="Setmap" value="地图选择" />
                <asp:HiddenField runat="server" ID="jingdu" />
                <asp:HiddenField runat="server" ID="weidu" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                同业联系方式：<br />
                <span class="unnamed1">（会员才能看到）</span>
            </td>
            <td bgcolor="#FFFFFF">
                <textarea name="txtPeerContact" id="txtPeerContact" runat="server" cols="100" rows="4"
                    class="textfield"></textarea>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>经营范围：
            </td>
            <td bgcolor="#FFFFFF">
                <table>
                    <tr>
                        <td>
                            <table width="500" border="0" cellspacing="0" cellpadding="2" style="border: 1px solid #D1E7F4;
                                background: #F2FAFF">
                                <tr>
                                    <td align="left">
                                        <input type="radio" id="CompanyType1" name="radManageArea" value="0" valid="requiredRadioed"
                                            errmsg="请选择经营范围" errmsgend="radManageArea" /><strong style="color: #3366CC"><label
                                                style="cursor: pointer" for="CompanyType1">旅行社</label>
                                            </strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType5" name="radManageArea" value="4" /><strong style="color: #3366CC">
                                            <label style="cursor: pointer" for="CompanyType5">
                                                景区</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType6" name="radManageArea" value="5" /><strong style="color: #3366CC"><label
                                            style="cursor: pointer" for="CompanyType6">酒 店</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType7" id="CompanyType6" name="radManageArea" value="6" /><strong
                                            style="color: #3366CC"><label style="cursor: pointer" for="CompanyType7">车队</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType8" name="radManageArea" value="7" /><strong style="color: #3366CC"><label
                                            style="cursor: pointer" for="CompanyType8">旅游用品</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType9" name="radManageArea" value="8" /><strong style="color: #3366CC"><label
                                            style="cursor: pointer" for="CompanyType9">购物点</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType10" name="radManageArea" value="9" /><strong style="color: #3366CC"><label
                                            style="cursor: pointer" for="CompanyType10">机票供应商</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType11" name="radManageArea" value="10" /><strong
                                            style="color: #3366CC"><label style="cursor: pointer" for="CompanyType11">其他采购商</label></strong>
                                    </td>
                                    <td align="left">
                                        <input type="radio" id="CompanyType12" name="radManageArea" value="11" /><strong
                                            style="color: #3366CC"><label style="cursor: pointer" for="CompanyType12">随便逛逛</label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <input type="checkbox" id="CompanyType2" name="ckCompanyType" value="2" /><label
                                            style="cursor: pointer" for="CompanyType2">组团社</label>
                                        <input type="checkbox" name="ckCompanyType" id="CompanyType3" value="1" /><label
                                            style="cursor: pointer" for="CompanyType3">专线商</label>
                                        <input type="checkbox" name="ckCompanyType" id="CompanyType4" value="3" /><label
                                            style="cursor: pointer" for="CompanyType4">地接社</label>
                                </tr>
                            </table>
                        </td>
                        <td width="100px">
                            <span class="errmsg" id="Span1"></span><span class="errmsg" id="Span1" style="display: none">
                                请选择旅行社类型</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trSellCity" style="display: none" class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>销售城市：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%--<%=MySaleCity %>--%><span id="spanSellCity"></span><span><a href='javascript:void(0);'
                    onclick='SetOtherSaleCity();'> 其它地区</a>&nbsp;&nbsp;<span id="errMsgckSellCity" class="unnamed1"
                        style="display: none">请选择销售城市</span></span>
            </td>
        </tr>
        <tr class="lr_hangbg" id="tr_CompanyBank">
            <td align="right" class="lr_shangbg">
                公司银行账户：
            </td>
            <td bgcolor="#FFFFFF">
                公司全称
                <input id="txtCompanyBackName" name="txtCompanyBackName" runat="server" />
                开户行
                <input id="txtCompanyBack" runat="server" name="txtCompanyBack" />
                帐号
                <input id="txtCompanyBackNumber" runat="server" name="txtCompanyBackNumber" />
            </td>
        </tr>
        <tr class="lr_hangbg" id="tr_CompanyUserBank">
            <td align="right" class="lr_shangbg">
                个人银行账户：
            </td>
            <td bgcolor="#FFFFFF">
                <cc1:CustomRepeater ID="RepeaterBank" runat="server">
                    <HeaderTemplate>
                        <table id="tb_Bank">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                户&nbsp;&nbsp;&nbsp;&nbsp;名<input id="txtBankAccountName1" name="txtBankAccountName"
                                    class="textfield" type="text" value='<%# DataBinder.Eval(Container.DataItem, "BankAccountName")%>' />
                            </td>
                            <td>
                                开户行<input id="txtBankName1" name="txtBankName" class="textfield" type="text" value='<%# DataBinder.Eval(Container.DataItem, "BankName")%>' />
                            </td>
                            <td>
                                帐号<input id="txtAccountNumber1" name="txtAccountNumber" class="textfield" type="text"
                                    value='<%# DataBinder.Eval(Container.DataItem, "AccountNumber")%>' />
                            </td>
                            <td>
                                <input id="btnDelBank" type="button" value="删除" class="an_tijiaobaocun_d" onclick="Edit.DeleteBankAccount(this);" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </cc1:CustomRepeater>
                <table id="tbBankAdd">
                    <tr>
                        <td>
                            户&nbsp;&nbsp;&nbsp;&nbsp;名<input type="text" class="textfield" name="txtBankAccountName">
                        </td>
                        <td>
                            开户行<input type="text" class="textfield" name="txtBankName">
                        </td>
                        <td>
                            帐号<input type="text" class="textfield" name="txtAccountNumber">
                        </td>
                        <td>
                            <input type="button" onclick="Edit.AddBankAccount(this);" class="an_tijiaobaocun_d"
                                value="添加" id="btnAddBank"><span style="color: red;" style="diplay: none"></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                公司等级：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropUserGrade" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                内部信息：
            </td>
            <td bgcolor="#FFFFFF">
                MQ：
                <input id="txt_MQ" name="txt_MQ" runat="server" type="text" class="textfield" size="20" />
                QQ：
                <input name="txt_QQ" id="txt_QQ" type="text" runat="server" class="textfield" size="15" />
                MSN：
                <input name="txt_MSN" id="txt_MSN" type="text" runat="server" class="textfield" size="25" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                B2B显示控制：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlB2B" runat="server" errmsg="请选择" valid="required">
                </asp:DropDownList>
                <span class="unnamed1">*</span>
                <input id="txt_B2BOrder" name="txt_B2BOrder" type="text" runat="server" value="50" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                B2C显示控制：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlB2C" runat="server">
                </asp:DropDownList>
                <span class="unnamed1">*</span>
                <input id="txt_B2COrder" size="10" name="txt_B2COrder" runat="server" type="text"
                    value="50" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                签约时间：
            </td>
            <td bgcolor="#FFFFFF">
                <input type="text" id="txtStartDate" name="StartDate" class="size55" style="width: 85px;"
                    onfocus="WdatePicker();" runat="server" />至<input type="text" id="txtEndDate" name="EndDate"
                        style="width: 85px;" class="size55" onfocus="WdatePicker();" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                点击量：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Literal ID="lal_ClickNum" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" class="lr_shangbg">
                资料完整度：
            </td>
            <td bgcolor="#FFFFFF">
                <asp:Literal ID="lal_InfoFull" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="trSellCityArea" class="lr_hangbg" style="display: none">
            <td colspan="3" id="tdAllSellCityAndArea">
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" class="baocun_an" Text="保 存" OnClick="btnSave_Click" />
            </td>
            <td align="center">
            </td>
            <td align="center">
                <input type="button" class="baocun_an" value="取 消" id="btncancel" runat="server"
                    onclick="javascript:window.history.go(-1);" />
            </td>
        </tr>
        <asp:HiddenField ID="hidCompanyLogoImg" runat="server" />
        <asp:HiddenField ID="hidOldLicenceImg" runat="server" />
        <asp:HiddenField ID="hidOldBusinessCertImg" runat="server" />
        <asp:HiddenField ID="hidOldTaxRegImg" runat="server" />
        <asp:HiddenField ID="hidOldAuthorizeImg" runat="server" />
        <asp:HiddenField ID="hidOldCardsImg" runat="server" />
        <asp:HiddenField ID="hidCompanyImg1" runat="server" />
        <asp:HiddenField ID="hidCompanyImg2" runat="server" />
        <asp:HiddenField ID="hidCompanyImg3" runat="server" />
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

    <script type="text/javascript">

        //设置公司经纬度
        $("#Setmap").click(function() {
            var Y = $("#weidu").val();
            var X = $("#jingdu").val();
            var url = "SetGoogleMap.aspx?weidu=" + Y + "&jindu=" + X + "";
            var title = "设置地图";
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 720, width: 900, draggable: false })
            return false;
        });
    </script>

    <script type="text/javascript">
        var Edit = {
            GetSellCity: function(strSaleCity, strAreaCity) {
                var ProvinceId = $("#<%=dropProvinceId.ClientID %>").val();
                var isCk = $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='1']").attr("checked");
                var isDj = $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='3']").attr("checked");
                var radCkVal = $("#tbCompanyInfo").find("input[type='radio']:checked").val();
                var CityId = $("#<%=dropCityId.ClientID %>").val();
                var CityName = $("#<%=dropCityId.ClientID %>").text();
                if (isCk) {
                    $("#trSellCity").show();
                } else {
                    $("#trSellCity").hide();
                }
                if (isCk || isDj) {
                    $("#trSellCityArea").show();
                } else {
                    $("#trSellCityArea").hide();
                }
                if (ProvinceId != "0" && radCkVal == "0") {
                    //销售城市
                    if (isCk) {
                        $("#trSellCity").show();
                        if (strSaleCity) {
                            $("#spanSellCity").html(strSaleCity);
                        } else {
                            $("#spanSellCity").html("");
                        }
                        $("#spanSellCity").find("input[name='ckSellCity']").click(function() {
                            var ckSellCity = new Array();
                            $("#spanSellCity").find("input[type='checkbox'][name='ckSellCity']:checked").each(function() {
                                ckSellCity.push($(this).val());
                            });
                            if (ckSellCity.length == 0) {
                                $("#errMsgckSellCity").show();
                            } else {
                                $("#errMsgckSellCity").hide();
                            }
                        });
                    }
                    if (isCk || isDj) {
                        //经营线路区域
                        $("#trSellCityArea").show();
                        $.ajax({
                            type: "POST",
                            dataType: 'html',
                            url: "AddBusinessMemeber.aspx?EditId=" + "<%=EditId %>" + "&type=GetTourArea&ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&CityName=" + CityName,
                            cache: false,
                            success: function(html) {
                                $("#tdAllSellCityAndArea").html(html);
                                if (strAreaCity) {
                                    var list = strAreaCity.split(",");
                                    for (var i = 0; i < list.length; i++) {
                                        var Index = list[i];
                                        if (Index && $("#Area_" + Index).length > 0) {
                                            $("#Area_" + Index).attr("checked", "checked");
                                        }
                                    }
                                }
                            }
                        });
                    }
                }
            },
            DeleteBankAccount: function(obj) {
                $($(obj).parents("tr").get(0)).remove();
            },
            AddBankAccount: function(obj) {
                var isNull = true;
                $($(obj).parents("tr").get(0)).find("input[type='text']").each(function(i) {
                    if ($.trim($(this).val()) == "") {
                        isNull = false;
                        var strErrMessage = "";
                        if (i == 0 || i == 12) {
                            strErrMessage = "户名不能为空";
                        } else if (i == 1 || i == 13) {
                            strErrMessage = "开户行不能为空";
                        } else if (i == 2 || i == 14) {
                            strErrMessage = "帐号不能为空";
                        }
                        $(obj).parent("td").find("span").show();
                        $(obj).parent("td").find("span").html(strErrMessage);

                        $(this).focus(function() {
                            $(obj).parent("td").find("span").hide();
                        });
                        return false;
                    }
                });
                if (isNull) {
                    var AddTr = $("#tbBankAdd tr:last").html();
                    $(obj).val("删除");
                    $(obj).attr("onclick", "");
                    $(obj).click(function() {
                        Edit.DeleteBankAccount(obj);
                    });
                    $("#tbBankAdd").append("<tr>" + AddTr + "</tr>");

                }
            },
            ckPassWord: function(e, formElements) {
                var objValue = $.trim(e.value);
                if (objValue != "") {
                    //不能为纯数字，纯字母，纯符号
                    if ((/^[\d]*$/.test(objValue)) || (/^[a-z]*$/.test(objValue)) || (/^[\W_]*$/gi.test(objValue))) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return true;
                }
            },
            SetCompanyType: function(TypeId) {

                if (TypeId.split(",").length == 1 && parseInt(TypeId) > 3) {
                    $("#tbCompanyInfo").find("input[type='radio'][value='" + TypeId + "']").attr("checked", true);
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "disabled");
                } else {
                    $("#tbCompanyInfo").find("input[type='radio'][value='0']").attr("checked", true);
                    for (var i = 0; i < TypeId.split(",").length; i++) {
                        var Index = TypeId.split(",")[i];
                        $("input[type='checkbox'][name='ckCompanyType'][value='" + Index + "']").attr("checked", true);

                    }
                }
            }
        };
        
    </script>

    <script type="text/javascript">

        var strLicenceImg, strBusinessCertImg, strTaxRegImg;
        var isSubmit = false;
        $(function() {

            //品牌logo
            strSfpBrandLogo = <%=SfpBrandLogo.ClientID %>;
            strLicenceImg = <%=SingleFilelLicence.ClientID %>;
            strBusinessCertImg = <%=SingleFileBusinessCertImg.ClientID %>;
            strTaxRegImg = <%=SingleFileTaxRegImg.ClientID %>;
            strAuthorizeImg = <%=SingleFileAuthorizeImg.ClientID %>;
            strCardsImg = <%=SingleFileCardsImg.ClientID %>;
            //公司照片
            strSfpcompanyimg1 = <%=Sfpcompanyimg1.ClientID %>;
            strSfpcompanyimg2 = <%=Sfpcompanyimg2.ClientID %>;
            strSfpcompanyimg3 = <%=Sfpcompanyimg3.ClientID %>;

            SWFUpload.WINDOW_MODE = "TRANSPARENT";
            $("#txtContactEmail").focus(function() {
                $("#errMsg_ExitEmail").hide();
            });
            $("#<%=dropProvinceId.ClientID %>").change(function() {
                $("#errMsg_City").hide();
                if ($(this).val() != "0") {
                    $("#errMsg_Province").hide();
                } else {
                    $("#errMsg_Province").show();
                }
            });

            $("#tbCompanyInfo").find("input[type='radio']").click(function() {
                $("#errMsg_radManageArea").hide();
                $("#errMsg_ckCompanyType").hide();
                var ckType = $(this).val();
                if (ckType == 0) {//旅行社
                    //                    $("#tr_CompanyUserBank").show();
                    //                    $("#tr_CompanyBank").show();
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");

                } else {
                    $("#tr_CompanyUserBank").hide();
                    $("#tr_CompanyBank").hide();
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "disabled");
                }
                if ($("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']"))
                    Edit.GetSellCity();
            });

            $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='2']").click(function() {
                var ckCompanyType = new Array();
                $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                    ckCompanyType.push($(this).val());
                });
                if (ckCompanyType.length == 0) {
                    $("#errMsg_ckCompanyType").show();
                } else {
                    $("#errMsg_ckCompanyType").hide();
                };
            });


            $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value!='2']").click(function() {
                var ckCompanyType = new Array();
                $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                    ckCompanyType.push($(this).val());
                });
                if (ckCompanyType.length == 0) {
                    $("#errMsg_ckCompanyType").show();
                } else {
                    $("#errMsg_ckCompanyType").hide();
                }
                Edit.GetSellCity();
            });



            
            $("#<%=btnSave.ClientID %>").click(function() {
                var isPass = true;
                if (isSubmit) {
                    return true;
                }

                if ($("#<%=dropProvinceId.ClientID %>").val() == "0") {
                    $("#errMsg_Province").show();
                    $("#<%=dropProvinceId.ClientID %>").focus();
                    isPass = false;
                }
                if ($("#<%=dropProvinceId.ClientID %>").val() != "0") {

                } else {
                    $("#<%=dropProvinceId.ClientID %>").focus();
                    $("#errMsg_Province").show();
                    isPass = false;
                }


                var ckTypeId = $("#tbCompanyInfo").find("input[type='radio']:checked").val();
                if (ckTypeId == 0) { //旅行社
                    var ckCompanyType = new Array();
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                        ckCompanyType.push($(this).val());
                    });
                    if (ckCompanyType.length == 0) {
                        isPass = false;
                        $("#errMsg_ckCompanyType").show();
                    } else {
                        if ($("#<%=dropCityId.ClientID %>").val() != "0") {
                            if ($("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='1']").attr("checked")) {
                                var ckSellCity = new Array();
                                $("#spanSellCity").find("input[type='checkbox'][name='ckSellCity']:checked").each(function() {
                                    ckSellCity.push($(this).val());
                                });
                                if (ckSellCity.length == 0) {
                                    isPass = false;
                                    $("#errMsgckSellCity").show();
                                }
                                else {
                                    isPass = true;
                                }
                            }
                        }
                    }
                }

                var form = $("#<%=btnSave.ClientID %>").closest("form").get(0);
                if (ValiDatorForm.validator(form, "span")) {
                } else {
                    isPass = false;
                };
                
                //客服电话
                var txtContactTel=$.trim($("#<%=txtContactTel.ClientID %>").val());
                var regexp=/^0\d{2,4}-?\d{7,8}$/;
                //if(!regexp.test(txtContactTel))
                //{
                    //alert("请输入真确的客服电话");
                    //$("#<%=txtContactTel.ClientID %>").focus();
                    //isPass= false;
                //}

                var oldEmail = $.trim($("#<%=hidContactEmail.ClientID %>").val());
                var NewEmail = $.trim($("#<%=txtContactEmail.ClientID %>").val());

                if (oldEmail != NewEmail && NewEmail != "") {
                    $.ajax({
                        type: "GET",
                        async: false,
                        dataType: 'html',
                        url: "AddBusinessMemeber.aspx?type=isEmail&Email=" + NewEmail + "&EditId=<%=EditId %>",
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                $("#errMsg_ExitEmail").show();
                                isPass = false;
                            }
                        }
                    });
                };
                if (isPass) {
                    $("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");
                    if (strLicenceImg.getStats().files_queued == 0
                    && strBusinessCertImg.getStats().files_queued == 0
                    && strTaxRegImg.getStats().files_queued == 0
                    && strAuthorizeImg.getStats().files_queued == 0
                    && strCardsImg.getStats().files_queued == 0
                    && strSfpcompanyimg1.getStats().files_queued == 0
                    && strSfpcompanyimg2.getStats().files_queued == 0
                    && strSfpcompanyimg3.getStats().files_queued == 0
                    && strSfpBrandLogo.getStats().files_queued == 0) {
                        return true;
                    }
                    UploadSfpBrandLogo();
                    return false;
                } else {
                    return false;
                }

            })
        })

        function openDialog(url, title, width, height, date) {
            $("#errMsgckSellCity").hide();
            Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: date });
        }
        function SetOtherSaleCity() {
            openDialog("OtherAllSaleCity.aspx?CompanyId=<%=EditId %>", "设置销售城市", "700", GetAddOrderHeight(), null);
        }


        function UploadSfpBrandLogo() {
            if (strSfpBrandLogo.getStats().files_queued > 0) {
                strSfpBrandLogo.customSettings.UploadSucessCallback = UploadLicenceImg;
                strSfpBrandLogo.startUpload();
            } else {
                UploadLicenceImg();
            }
        }


        function UploadLicenceImg() {
            if (strLicenceImg.getStats().files_queued > 0) {
                strLicenceImg.customSettings.UploadSucessCallback = UploadBusinessCertImg;
                strLicenceImg.startUpload();
            } else {
                UploadBusinessCertImg();
            }
        }
        function UploadBusinessCertImg() {
            if (strBusinessCertImg.getStats().files_queued > 0) {
                strBusinessCertImg.customSettings.UploadSucessCallback = UploadTaxRegImg;
                strBusinessCertImg.startUpload();
            } else {
                UploadTaxRegImg();
            }
        }
        function UploadTaxRegImg() {
            if (strTaxRegImg.getStats().files_queued > 0) {
                strTaxRegImg.customSettings.UploadSucessCallback = UploadAuthorizeImg;
                strTaxRegImg.startUpload();
            } else {
                UploadAuthorizeImg();
            }
        }

        function UploadAuthorizeImg() {
            if (strAuthorizeImg.getStats().files_queued > 0) {
                strAuthorizeImg.customSettings.UploadSucessCallback = UploadCardsImg;
                strAuthorizeImg.startUpload();
            } else {
                UploadCardsImg();
            }
        }

        function UploadCardsImg() {
            if (strCardsImg.getStats().files_queued > 0) {
                strCardsImg.customSettings.UploadSucessCallback = UploadSfpcompanyimg1;
                strCardsImg.startUpload();
            } else {
                UploadSfpcompanyimg1();
            }
        }

        function UploadSfpcompanyimg1() {
            if (strSfpcompanyimg1.getStats().files_queued > 0) {
                strSfpcompanyimg1.customSettings.UploadSucessCallback = UploadSfpcompanyimg2;
                strSfpcompanyimg1.startUpload();
            } else {
                UploadSfpcompanyimg2();
            }
        }

        function UploadSfpcompanyimg2() {
            if (strSfpcompanyimg2.getStats().files_queued > 0) {
                strSfpcompanyimg2.customSettings.UploadSucessCallback = UploadSfpcompanyimg3;
                strSfpcompanyimg2.startUpload();
            } else {
                UploadSfpcompanyimg3();
            }
        }

        function UploadSfpcompanyimg3() {
            if (strSfpcompanyimg3.getStats().files_queued > 0) {
                strSfpcompanyimg3.customSettings.UploadSucessCallback = UploadSave;
                strSfpcompanyimg3.startUpload();
            } else {
                UploadSave();
            }
        }

        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        function UploadSave() {
            isSubmit = true;
            $("#<%=btnSave.ClientID %>").click();
        }



        function SetProvince(ProvinceId) {
            $("#<%=dropProvinceId.ClientID %>").attr("value", ProvinceId);
        }
        function SetCity(CityId) {
            $("#<%=dropCityId.ClientID %>").attr("value", CityId);
        }
        function SetCounty(CountyId) {
            $("#<%=dropCountyId.ClientID %>").attr("value", CountyId);
        }
        
    </script>

    </form>
</body>
</html>
