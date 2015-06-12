<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.CompanyManage.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>旅行社汇总管理</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" id="tb_SearchList" align="center" cellpadding="0"
        cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <uc2:ProvinceAndCityAndCounty ID="ProvinceAndCityAndCounty1" IsShowRequired="false"
                    runat="server" />
                经营范围
                <asp:DropDownList ID="dropCompanyType" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">专线商</asp:ListItem>
                    <asp:ListItem Value="2">组团社</asp:ListItem>
                    <asp:ListItem Value="3">地接社</asp:ListItem>
                </asp:DropDownList>
                单位名称<input id="txtCompanyName" name="txtCompanyName" runat="server" type="text" class="textfield"
                    size="12" />
                引荐人<input id="txtRecommendPerson" name="txtRecommendPerson" runat="server" type="text"
                    class="textfield" size="12" />
                账号：<input id="txtUsername" type="text" class="textfield" size="12" />
                联系人：<input id="txtContactName" type="text" class="textfield" size="12" />
                <img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21"
                    style="margin-bottom: -3px; cursor: pointer" onclick="CompanyManage.OnSearch();" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="23%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                <table width="51%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" />
                        </td>
                        <td width="23%">
                            <a id="a_Add" href="javascript:void(0);">
                                <img src="<%=ImageServerUrl %>/images/yunying/xinzeng.gif" width="50" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="4%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="24%">
                            <a id="a_Edit" href="javascript:void(0);" style="display: none">
                                <img src="<%=ImageServerUrl %>/images/yunying/xiugai.gif" width="50" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="23%">
                            <img id="img_Delete" src="<%=ImageServerUrl %>/images/yunying/shanchu.gif" width="51"
                                height="25" style="cursor: pointer; display: none" />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="77%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                align="left">
            </td>
        </tr>
    </table>
    <div id="divCompanyList" align="center">
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
        var Parms = { ProvinceId: 0, CityId: 0,CountyId:0, CompanyType: 0, CompanyName: "", RecommendPerson: "", Page: 1,username:"",contactName:"" };
        var CompanyManage = {//旅行社列表
            GetCompanyList: function() {
                 if(<%=intPage %> >0 ){
                     Parms.Page=<%=intPage %>;
                 }
                LoadingImg.ShowLoading("divCompanyList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxGetCompanyList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divCompanyList").html(html);
                        }
                    });
                }
            },
            GetCkCompanyList: function() {//获的己选择的公司ID
                var ckList = new Array();
                $("#tbCompanyList").find("input[type='checkbox'][name!='ckAll']:checked").each(function() {
                    ckList.push($(this).val());
                });
                return ckList;
            },
            ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            createUrl: function(url, params) {
                var isHaveParam = false;
                var isHaveQuestionMark = false;
                var questionMark = "?";
                var questionMarkIndex = url.indexOf(questionMark);
                var urlLength = url.length;

                if (questionMarkIndex == urlLength - 1) {
                    isHaveQuestionMark = true;
                } else if (questionMarkIndex != -1) {
                    isHaveParam = true;
                }

                if (isHaveParam == true) {
                    for (var key in params) {
                        url = url + "&" + key + "=" + params[key];
                    }
                } else {
                    if (isHaveQuestionMark == false) {
                        url += questionMark;
                    }
                    for (var key in params) {
                        url = url + key + "=" + params[key] + "&";
                    }
                    url = url.substr(0, url.length - 1);
                }
                return url;
            },
            EditCompany: function() {//修改
                var ckList = this.GetCkCompanyList();
                if (ckList.length == 0) {
                    alert("未选择修改项!");
                    return false;
                }
                if (ckList.length > 1) {
                    alert("只能选择一个修改项!");
                    return false;
                }
                if (ckList.length == 1) {
                    var Url = this.createUrl("Default.aspx", Parms);
                    var returnUrl = "&returnUrl=" + escape(Url);
                    window.location.href = "AddBusinessMemeber.aspx?type=list&EditId=" + ckList[0] + returnUrl;
                }
            },
            DeleteCompany: function() {//删除
                var ckList = this.GetCkCompanyList();
                if (ckList.length == 0) {
                    alert("未选择删除项!");
                    return false;
                }
                if (ckList.length > 0) {
                    if (window.confirm("您确定要删除此旅行社吗?\n此操作不可恢复!")) {
                        $.ajax({
                            type: "POST",
                            dataType: 'html',
                            url: "Default.aspx?Type=Delete",
                            data: $("#form1").serializeArray(),
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    alert("删除成功!");
                                    CompanyManage.GetCompanyList();
                                }
                            }
                        });
                    }
                }

            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetCompanyList();
            },
            OnSearch: function() {//查询
                Parms.ProvinceId = $("#ProvinceAndCityAndCounty1_ddl_ProvinceList").val();
                Parms.CityId = $("#ProvinceAndCityAndCounty1_ddl_CityList").val();
                Parms.CountyId=$("#ProvinceAndCityAndCounty1_ddl_CountyList").val();
                Parms.CompanyType = $("#<%=dropCompanyType.ClientID %>").val();
                Parms.CompanyName = $.trim($("#<%=txtCompanyName.ClientID %>").val());
                Parms.RecommendPerson = $.trim($("#<%=txtRecommendPerson.ClientID %>").val());
                Parms.Page = 1;
                Parms.username = $.trim($("#txtUsername").val());
                Parms.contactName = $.trim($("#txtContactName").val());
                CompanyManage.GetCompanyList();
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            CompanyEditOpenDialog: function(CompanyId) {
                CompanyManage.openDialog("AddBusinessMemeber.aspx", "修改旅行社信息", "850", "1100", "type=list&EditId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            LookLoginDetail: function(CompanyId) { //登录细明
                CompanyManage.openDialog("LoginDetail.aspx", "登录细明", "400", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            LookCompanySaleCity: function(CompanyId) { //查看销售城市
                CompanyManage.openDialog("CompanySaleCity.aspx", "销售城市", "400", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            LookCompanyTourArea: function(CompanyId) {//查看经营线路区域
                CompanyManage.openDialog("CompanyTourArea.aspx", "经营线路区域", "500", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            LookCompanyCredit: function(CompanyId) { //查看证书
                CompanyManage.openDialog("CompanyCreditList.aspx", "查看证书", "400", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            }
        };
        $(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            
             $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    CompanyManage.OnSearch();
                    return false;
                }
            });
            CompanyManage.GetCompanyList();
            $("#a_Add").click(function() {
                window.top.open("<%=EyouSoft.Common.Domain.UserPublicCenter %>/Register/CompanyUserRegister.aspx");
            });
            if("<%=isUpdate %>"=="True"){
                $("#a_Edit").show();
                $("#a_Edit").click(function() {
                    CompanyManage.EditCompany();
                    return false;
                });
            }
            if("<%=isDelete %>"=="True"){
                $("#img_Delete").show();
                $("#img_Delete").click(function() {
                    CompanyManage.DeleteCompany();
                    return false;
                });
            }
        });
    </script>

    </form>
</body>
</html>
