<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterCompany.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.RegisterCompany" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册会员审核</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" id="tb_SearchList" cellpadding="0"
        cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <uc2:ProvinceAndCityAndCounty ID="ProvinceAndCityAndCounty1" IsShowRequired="false"
                    runat="server" />
                经营范围：<asp:DropDownList ID="dropCompanyType" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">专线商</asp:ListItem>
                    <asp:ListItem Value="2">组团社</asp:ListItem>
                    <asp:ListItem Value="3">地接社</asp:ListItem>
                    <asp:ListItem Value="4">景区</asp:ListItem>
                    <asp:ListItem Value="5">酒 店</asp:ListItem>
                    <asp:ListItem Value="6">车队</asp:ListItem>
                    <asp:ListItem Value="7">旅游用品</asp:ListItem>
                    <asp:ListItem Value="8">购物点</asp:ListItem>
                    <asp:ListItem Value="9">机票供应商</asp:ListItem>
                    <asp:ListItem Value="10">其他采购商</asp:ListItem>
                    <asp:ListItem Value="11">随便逛逛</asp:ListItem>
                </asp:DropDownList>
                单位名称：<input id="txtCompanyName" name="txtCompanyName" runat="server" type="text"
                    class="textfield" size="12" />
                账号：<input id="txtUsername" type="text" class="textfield" size="10" />
                联系人：<input id="txtContactName" type="text" class="textfield" size="10" />
                <img style="cursor: pointer" onclick="CompanyManage.OnSearch();" src="<%=ImageServerUrl %>/images/yunying/chaxun.gif"
                    width="62" height="21" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="18%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" />
                        </td>
                        <td width="24%">
                            <a id="a_Edit" href="javascript:void(0);">
                                <img src="<%=ImageServerUrl %>/images/yunying/xiugai.gif" width="50" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="23%">
                            <img id="img_Delete" src="<%=ImageServerUrl %>/images/yunying/shanchu.gif" width="51"
                                height="25" style="cursor: pointer" />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif" align="left">
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
        var Parms = { ProvinceId: 0, CityId: 0,CountyId:0, CompanyType: 0, CompanyName: "", Page: 1,username:"",contactName:"" };
        var CompanyManage = {
        GetCompanyList: function() { //获的注册会员列表
                if(<%=intPage %> >0 ){
                    Parms.Page=<%=intPage %>;
                }
                LoadingImg.ShowLoading("divCompanyList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxGetRegisterCompanyaspx.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divCompanyList").html(html);
                        }
                    });
                }
            },
            GetCkCompanyList: function() {//获的己选的公司ID
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
                    var Url = this.createUrl("RegisterCompany.aspx", Parms);
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
                    if (window.confirm("您确定要删除吗?\n此操作不可恢复!")) {
                        $.ajax({
                            type: "POST",
                            dataType: 'html',
                            url: "RegisterCompany.aspx?Type=Delete",
                            data: $("#form1").serializeArray(),
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    alert("删除成功!");
                                    CompanyManage.GetCompanyList();
                                } else {
                                    alert(html);
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
                Parms.Page=1;
                Parms.username=$.trim($("#txtUsername").val());
                Parms.contactName=$.trim($("#txtContactName").val());
                CompanyManage.GetCompanyList();
            },
            CompanyCheck: function(CompanyId) { //审核
                if (confirm("是否确认审核通过?")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "RegisterCompany.aspx?Type=Check&CompanyId=" + CompanyId,
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                alert("审核通过!");
                            } else {
                                alert("审核失败!");
                            }
                            CompanyManage.GetCompanyList();
                        }
                    });
                }
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            LookCompanySaleCity: function(CompanyId) { //查看销售城市
                CompanyManage.openDialog("CompanySaleCity.aspx", "销售城市", "400", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            LookCompanyTourArea: function(CompanyId) {//查看经营线路区域
                CompanyManage.openDialog("CompanyTourArea.aspx", "经营线路区域", "500", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            CompanyEditOpenDialog: function(CompanyId) {
                CompanyManage.openDialog("AddBusinessMemeber.aspx", "修改旅行社信息", "850", "1100", "type=list&EditId=" + CompanyId + "&rad=" + new Date().getTime());
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

            $("#a_Edit").click(function() {
                CompanyManage.EditCompany();
                return false;
            });
            $("#img_Delete").click(function() {
                CompanyManage.DeleteCompany();
                return false;
            });
        });
    </script>

    </form>
</body>
</html>
