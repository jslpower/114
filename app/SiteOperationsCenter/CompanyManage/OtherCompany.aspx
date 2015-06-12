<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherCompany.aspx.cs" EnableEventValidation="false"
    Inherits="SiteOperationsCenter.CompanyManage.OtherCompany" %>

<%@ Register Src="../usercontrol/CompanyList.ascx" TagName="CompanyList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty"
    TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供应商管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

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
                单位名称<input id="txtCompanyName" name="txtCompanyName" type="text" runat="server" class="textfield"
                    size="15" />
                账号：<input id="txtUsername" type="text" class="textfield" size="15" value="<%=EyouSoft.Common.Utils.InputText(Request.QueryString["username"]) %>" />
                联系人：<input id="txtContactName" type="text" class="textfield" size="15" value="<%=EyouSoft.Common.Utils.InputText(Request.QueryString["contactName"]) %>" />
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
    <uc1:CompanyList ID="CompanyList1" runat="server" />

    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <%--<script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>--%>

    <script type="text/javascript">
        var CompanyManage = {
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
                    var Url ="<%=Request.ServerVariables["SCRIPT_NAME"] %>?<%=Request.QueryString %>";
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
                    if (window.confirm("您确定要删除此会员吗?\n此操作不可恢复!")) {
                        $.ajax({
                            type: "POST",
                            dataType: 'html',
                            url: "OtherCompany.aspx?Type=Delete",
                            data: $("#form1").serializeArray(),
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    alert("删除成功!");
                                    window.location.reload();
                                }
                            }
                        });
                    }
                }

            },
            OnSearch: function() {
                var ProvinceId = $("#ProvinceAndCityAndCounty1_ddl_ProvinceList").val();
                var CityId = $("#ProvinceAndCityAndCounty1_ddl_CityList").val();
                var CountyId=$("#ProvinceAndCityAndCounty1_ddl_CountyList").val();
                var CompanyName = $.trim($("#txtCompanyName").val());
                var username=$.trim($("#txtUsername").val());
                var contactName=$.trim($("#txtContactName").val());
                window.location.href = "OtherCompany.aspx?CompanyType=<%=Request.QueryString["CompanyType"] %>&ProvinceId="+ProvinceId+"&CityId="+CityId+"&CountyId="+CountyId+"&CompanyName="+escape(CompanyName)+"&username="+ encodeURIComponent(username)+"&contactName="+encodeURIComponent(contactName);
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            LookLoginDetail: function(CompanyId) { //登录细明
                CompanyManage.openDialog("LoginDetail.aspx", "登录细明", "400", "300", "CompanyId=" + CompanyId + "&rad=" + new Date().getTime());
            },
            CompanyEditOpenDialog: function(CompanyId) {
                CompanyManage.openDialog("AddBusinessMemeber.aspx", "修改旅行社信息", "850", "1100", "type=list&EditId=" + CompanyId + "&rad=" + new Date().getTime());
            }
        };
        $(function() {
             $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    CompanyManage.OnSearch();
                    return false;
                }
             });
                
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
