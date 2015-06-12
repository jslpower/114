<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCompany.aspx.cs" Inherits="SiteOperationsCenter.CompanyManage.EditCompany"
    EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改旅行社信息</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="855" border="0" align="center" cellpadding="2" cellspacing="1" class="lr_bg"
        id="tbCompanyInfo">
        <tr class="lr_hangbg">
            <td width="15%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>省份：
            </td>
            <td width="55%">
                <asp:DropDownList ID="dropProvinceId" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                </asp:DropDownList>
                <span class="unnamed1" style="display: none">*</span>城市：
                <asp:DropDownList ID="dropCityId" runat="server">
                </asp:DropDownList>
                <span class="unnamed1" style="display: none">*</span>县区：
                <asp:DropDownList ID="dropCountyId" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                </asp:DropDownList>
                <span id="errMsg_Province" style="display: none" class="unnamed1">请选择省份</span> <span
                    id="errMsg_City" style="display: none" class="unnamed1">请选择城市</span><span id="errMsg_County"
                        style="display: none" class="unnamed1">请选择县区</span>
            </td>
            <td width="30%" bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>单位名称：
            </td>
            <td>
                <input id="txtCompanyName" runat="server" name="txtCompanyName" type="text" class="textfield"
                    size="25" valid="required" errmsg="请输入单位名称" />
                <span id="errMsg_txtCompanyName" class="unnamed1"></span>
            </td>
            <td bgcolor="#F1FAFE">
                请输入您所在单位的全称，如：杭州某某旅行社
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                许可证号：
            </td>
            <td>
                <input id="txtLicenseNumber" runat="server" name="txtLicenseNumber" type="text" class="textfield"
                    size="25" />
            </td>
            <td bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                用户名：
            </td>
            <td>
                <input id="txtUserName" runat="server" disabled="disabled" name="txtUserName" type="text"
                    class="yonghushuru" size="25" />
            </td>
            <td bgcolor="#F1FAFE">
                5-20个字符(包括字母、数字、下划线、中文)，一个汉字为两个字符，推荐使用中文会员名。一旦注册成功会员名不能修改
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                登录密码：
            </td>
            <td>
                <input id="txtPassWord" name="txtPassWord" runat="server" type="password" class="textfield"
                    size="25" custom="Edit.ckPassWord" valid="custom" errmsg="密码格式不正确" />
                <span class="unnamed1" id="errMsg_txtPassWord"></span>
            </td>
            <td bgcolor="#F1FAFE">
                <div id="t2">
                    密码由6-16个字符组成，请使用英文字母加数字或符号的组合密码，不能单独使用英文字母、数字或符号作为您的密码。</div>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                登录密码确认：
            </td>
            <td>
                <input id="txtSecondPassWord" name="txtSecondPassWord" runat="server" type="password"
                    class="textfield" size="25" eqaulname="txtPassWord" valid="equal" errmsg="请再次输入密码" />
                <span id="errMsg_txtSecondPassWord" class="unnamed1"></span>
            </td>
            <td bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>真实姓名：
            </td>
            <td>
                <input id="txtContactName" name="txtContactName" runat="server" type="text" class="textfield"
                    size="25" valid="required" errmsg="请输入真实姓名" />
                <span id="errMsg_txtContactName" class="unnamed1"></span>
            </td>
            <td bgcolor="#F1FAFE">
                <div>
                    为方便客服与您联系，请填写您的真实姓名。</div>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                客户引荐单位：
            </td>
            <td>
                <label id="labCommendCompany" runat="server">
                </label>
            </td>
            <td bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                电话：
            </td>
            <td>
                <input id="txtContactTel" runat="server" name="txtContactTel" type="text" class="textfield"
                    size="45" />
            </td>
            <td bgcolor="#F1FAFE">
                <div>
                    为了客户能及时联系到您，请仔细填写。请填写含区号的完整格式，如：0571-88888888</div>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                手机：
            </td>
            <td>
                <input id="txtContactMobile" runat="server" name="txtContactMobile" type="text" class="textfield"
                    size="25" />
            </td>
            <td bgcolor="#F1FAFE">
                <div>
                    手机号码是以后客服与您沟通联系的主要方式之一</div>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                传真：
            </td>
            <td>
                <input id="txtContactFax" runat="server" name="txtContactFax" type="text" class="textfield"
                    size="25" />
            </td>
            <td bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>Email：
            </td>
            <td>
                <input id="hidContactEmail" runat="server" type="hidden" />
                <input id="txtContactEmail" runat="server" name="txtContactEmail" type="text" class="textfield"
                    size="40" valid="required|isEmail" errmsg="请输入Email|请输入正确的Email" />
                <span id="errMsg_txtContactEmail" class="errmsg"></span><span id="errMsg_ExitEmail"
                    class="errmsg" style="display: none">该Email已经注册</span>
            </td>
            <td bgcolor="#F1FAFE">
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                品牌名称：
            </td>
            <td>
                <input id="txtBrandName" name="txtBrandName" runat="server" type="text" class="textfield"
                    size="40" />
            </td>
            <td bgcolor="#F1FAFE">
                <div>
                    请输入您在推广产品时所使用的品牌名称,如:魅力云南</div>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>办公地点：
            </td>
            <td colspan="2">
                <input id="txtOfficeAddress" runat="server" name="txtOfficeAddress" type="text" class="textfield"
                    size="80" valid="required" errmsg="请输入办公地点" />
                <span id="errMsg_txtOfficeAddress" class="unnamed1"></span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>QQ：
            </td>
            <td colspan="2">
                <input id="txtContactQQ" runat="server" name="txtContactQQ" type="text" class="textfield"
                    size="15" valid="required" errmsg="请输入QQ" /><span id="errMsg_txtContactQQ" class="unnamed1"></span>
                MSN：
                <input id="txtContactMSN" runat="server" name="txtContactMSN" type="text" class="textfield"
                    size="15" />
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                证书管理：
            </td>
            <td colspan="2">
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
                </table>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                公司介绍：
            </td>
            <td colspan="2">
                <FCKeditorV2:FCKeditor ID="txtCompanyInfo" runat="server" Height="300px">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                业务优势：
            </td>
            <td colspan="2">
                <textarea rows="4" runat="server" id="txtBusinessSuperior" name="txtBusinessSuperior"
                    cols="70"></textarea>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>经营范围：
            </td>
            <td>
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
                            <span class="errmsg" id="Span1"></span><span class="errmsg" id="Span2" style="display: none">
                                请选择旅行社类型</span>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <span class="unnamed1" id="errMsg_radManageArea"></span><span class="unnamed1" id="errMsg_ckCompanyType"
                    style="display: none">请选择旅行社类型</span>
            </td>
        </tr>
        <tr class="lr_hangbg" id="tr_CompanyBank">
            <td align="right" class="lr_shangbg">
                公司银行账户：
            </td>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            公司全称<input id="txtCompanyBackName" name="txtCompanyBackName" runat="server" />
                        </td>
                        <td>
                            开户行<input id="txtCompanyBack" runat="server" name="txtCompanyBack" />
                        </td>
                        <td>
                            帐号<input id="txtCompanyBackNumber" runat="server" name="txtCompanyBackNumber" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="lr_hangbg" id="tr_CompanyUserBank">
            <td align="right" class="lr_shangbg">
                个人银行账户：
            </td>
            <td colspan="2">
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
        <tr id="trSellCity" style="display: none" class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>销售城市：
            </td>
            <td align="left" colspan="2">
                <span id="spanSellCity"></span><span><a href='javascript:void(0);' onclick='SetOtherSaleCity();'>
                    其它地区</a>&nbsp;&nbsp;<span id="errMsgckSellCity" class="unnamed1" style="display: none">请选择销售城市</span></span>
            </td>
        </tr>
        <%=OtherSaleCity %>
        <tr class="lr_hangbg" style="display: none" id="trSellCityArea">
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
                <asp:Button ID="Button1" runat="server" class="baocun_an" Text="取 消" OnClick="Button1_Click" />
            </td>
        </tr>
        <asp:HiddenField ID="hidOldLicenceImg" runat="server" />
        <asp:HiddenField ID="hidOldBusinessCertImg" runat="server" />
        <asp:HiddenField ID="hidOldTaxRegImg" runat="server" />
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
        var Edit = {
            GetSellCity: function(strSaleCity, strAreaCity) {
                var ProvinceId = $("#<%=dropProvinceId.ClientID %>").val();
                var isCk = $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='1']").attr("checked");
                var radCkVal = $("#tbCompanyInfo").find("input[type='radio']:checked").val();
                var CityId = $("#<%=dropCityId.ClientID %>").val();
                if (ProvinceId != "0" && isCk && radCkVal == "0") {
                    //销售城市
                    $("#trSellCity").show();
                    if (strSaleCity != null && strSaleCity != undefined) {
                        $("#spanSellCity").html(strSaleCity);
                    }else{
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

                    //经营线路区域
                    $("#trSellCityArea").show();
                    $.ajax({
                        type: "POST",
                        dataType: 'html',
                        url: "EditCompany.aspx?EditId=" + "<%=EditId %>" + "&type=GetTourArea&ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&CityName=" + $("#<%=dropCityId.ClientID %> option:selected").text(),
                        cache: false,
                        success: function(html) {
                            $("#tdAllSellCityAndArea").html(html);
                            if (strAreaCity != null && strAreaCity != undefined) {
                                for (var i = 0; i < strAreaCity.split(",").length; i++) {
                                    var Index = strAreaCity.split(",")[i];
                                    $("#tdAllSellCityAndArea").find("input[type='checkbox'][name='checkbox_Area'][value='" + Index + "']").attr("checked", "checked");
                                }
                            }
                        }
                    });
                }
                else {
                    $("#trSellCity").hide();
                    $("#trSellCityArea").hide();
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
                     $("#tr_CompanyUserBank").hide();
                    $("#tr_CompanyBank").hide();
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
        function SetProvince(ProvinceId) {
            $("#<%=dropProvinceId.ClientID %>").attr("value", ProvinceId);
        }
        function SetCity(CityId) {
            $("#<%=dropCityId.ClientID %>").attr("value", CityId);
        }
        function SetCounty(CountyId){
            $("#<%=dropCountyId.ClientID %>").attr("value",CountyId)        
        }
        var strLicenceImg, strBusinessCertImg, strTaxRegImg;
        var isSubmit=false;
        $(function() {
             strLicenceImg = <%=SingleFilelLicence.ClientID %>;
             strBusinessCertImg = <%=SingleFileBusinessCertImg.ClientID %>;
             strTaxRegImg = <%=SingleFileTaxRegImg.ClientID %>;
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
                    $("#tr_CompanyUserBank").show();
                    $("#tr_CompanyBank").show();
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");

                } else {
                    $("#tr_CompanyUserBank").hide();
                    $("#tr_CompanyBank").hide();
                    $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "disabled");
                }
                if( $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']"))
                Edit.GetSellCity();
            });

            $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value!='1']").click(function() {
                var ckCompanyType = new Array();
                $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                    ckCompanyType.push($(this).val());
                });
                if (ckCompanyType.length == 0) {
                    $("#errMsg_ckCompanyType").show();
                } else {
                    $("#errMsg_ckCompanyType").hide();
                }
            });

             $("#tbCompanyInfo").find("input[type='checkbox'][name='ckCompanyType'][value='1']").click(function(){
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

            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
            $("#<%=btnSave.ClientID %>").click(function() {      
                
               if(isSubmit){
                    return true;
                }
                var isPass = true;
                if ($("#<%=dropProvinceId.ClientID %>").val() == "0") {
                    $("#errMsg_Province").show();
                    $("#<%=dropProvinceId.ClientID %>").focus();
                    isPass = false;
                }
                if ($("#<%=dropProvinceId.ClientID %>").val() != "0") {
//                    if ($("#<%=dropCityId.ClientID %>").val() != "0") {
//                        if($("#<%=dropCountyId.ClientID %>").val()!="0"){
//                            $("#errMsg_County").hide();
//                        }
//                        else{
//                            $("#errMsg_County").show();
//                            $("#<%=dropCountyId.ClientID %>").focus();
//                            isPass=false;
//                        }
//                        $("#errMsg_City").hide();
//                    } else {
//                        $("#errMsg_City").show();
//                        $("#<%=dropCityId.ClientID %>").focus();
//                        isPass = false;
//                    }
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
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "span")) {
                } else {
                    isPass = false;
                }
                var oldEmail=$.trim($("#<%=hidContactEmail.ClientID %>").val());
                var NewEmail=$.trim($("#<%=txtContactEmail.ClientID %>").val());

                if(oldEmail!=NewEmail && NewEmail!=""){
                    $.ajax({
                        type: "POST",
                        async: false,
                        dataType: 'html',
                        url: "EditCompany.aspx?type=isEmail&Email=" + NewEmail+"&EditId=<%=EditId %>",
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                $("#errMsg_ExitEmail").show();
                                isPass = false;
                            }
                        }
                    });
                }  
                if (isPass) {
                    $("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");
                    if(strLicenceImg.getStats().files_queued ==0
                    &&strBusinessCertImg.getStats().files_queued ==0
                    && strTaxRegImg.getStats().files_queued ==0){
                        return true;
                    }
                    UploadLicenceImg();
                    return false;
                }else{
                    return false;
                }
            });
        });
        function openDialog(url, title, width, height, date) {
                $("#errMsgckSellCity").hide();
                Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: date });
         }
        function SetOtherSaleCity(){
            openDialog("OtherAllSaleCity.aspx?CompanyId=<%=EditId %>", "设置销售城市", "700", GetAddOrderHeight(), null);
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
                strTaxRegImg.customSettings.UploadSucessCallback = UploadSave;
                strTaxRegImg.startUpload();
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
           isSubmit=true;
           $("#<%=btnSave.ClientID %>").click();
        }
   
    </script>

    </form>
</body>
</html>
