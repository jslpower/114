<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationIndustry.aspx.cs"
    Inherits="SiteOperationsCenter.NewsCenterControl.InformationIndustry" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="search_bg">
                关键字：
                <input name="KeyWord" id="KeyWord" type="text" size="30" />
                分类：
                <asp:DropDownList ID="DdlType" runat="server">
                </asp:DropDownList>
                <img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/yunying/chaxun.gif" width="62"
                    height="21" onclick="Informationindustry.OnSearch()" style="margin-bottom: -4px;
                    cursor: pointer" />
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="caozuo_bg">
                <table border="0" cellspacing="0" cellpadding="0" style="width: 19%">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_da.gif" width="3"
                                height="20" />
                        </td>
                        <td width="24%">
                            <a href="javascript:void(0)" id="hrefUpdate">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xiugai.gif" width="50"
                                    height="25" border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" border="0" />
                        </td>
                        <td width="23%">
                            <a href="javascript:void(0)" id="hrefDelete">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                    height="25" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" border="0" />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_d.gif" width="11"
                                height="25" border="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <div id="divInfomationIndustry" align="center">
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
        var Parms = { Page: 1, KeyWord: "", InfoType:"" };
        var Informationindustry = {
            GetInformationList: function() {
                if(<%=intPage %> >0){
                    //判断是否返回修改前的那一页
                    Parms.Page=<%=intPage %>;
                }
                LoadingImg.ShowLoading("divInfomationIndustry");
                if (LoadingImg.IsLoadAddDataToDiv("divInfomationIndustry")) {
                    $.ajax({//获取列表页
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxGetInformationList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divInfomationIndustry").html(html);
                        }
                    });
                }
            },
            GetCheckedInformationList: function() {//获取已选的资讯编号
                var CheckList = new Array();
                $("#tbInformationList").find("input[type='checkbox'][name='ckInformationId']:checked").each(function() {
                    CheckList.push($(this).val());
                })
                return CheckList;
            },
            ChAll: function(obj) {//全选
                $("#tbInformationList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
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
            EditInfomation: function() {
                var chList = this.GetCheckedInformationList();
                if (chList.length == 0) {
                    alert("未选择修改项!");
                    return false;
                }
                if (chList.length > 1) {
                    alert("只能选择一个修改项!");
                    return false;
                }
                if (chList.length == 1) {//页面跳转
                    var Url = this.createUrl("InformationIndustry.aspx", Parms);
                    var returnUrl = "&returnUrl=" + escape(Url);
                    window.location.href = "EditInformation.aspx?EditId=" + chList[0] + returnUrl;
                }
            },

            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                var InfoType=$("#<%=DdlType.ClientID %>").val();
                Parms.Page = Page;
                Parms.KeyWord =$.trim($("#KeyWord").val());
                Parms.InfoType=InfoType;
                this.GetInformationList();
            },
            OnSearch: function() {
                Parms.KeyWord = $("#KeyWord").val();
                var InfoType=$("#<%=DdlType.ClientID %>").val();
                //alert(InfoType);
                Parms.Page = 1;
                Parms.InfoType=InfoType;
                this.GetInformationList();
            },
            DelteInfo: function() {// 删除勾选项
                var chList = this.GetCheckedInformationList();
                if (chList.length == 0) {
                    alert("未选择删除项!");
                    return false;
                }
                if (confirm("您确定要删除该项信息吗？\n 此操作不可回复")) {
                    $.ajax({
                        type: "POST",
                        dataType: 'html',
                        url: "InformationIndustry.aspx?Type=Delete",
                        data: $("#<%=form1.ClientID %>").serializeArray(),
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                alert("删除成功!");
                                Informationindustry.GetInformationList();
                            }
                            else {
                                alert("删除失败！")
                            }
                        }
                    });
                }
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            EditInfomationOpenDialog: function(EditId) {//弹框
                var returnUrl = "InformationIndustry.aspx";
                var Page = Parms.Page;
                Informationindustry.openDialog("EditInformation.aspx", "修改同业资讯", "850", "900", "type=list&EditId=" + EditId + "&returnUrl=" + returnUrl + "&Page=" + Page + "&rad=" + new Date().getTime());
            }
        }
        $(function() { //页面加载完成之后
            $("#KeyWord").bind("keypress", function(e) {
                if (e.keyCode == 13) { //绑定回车查询
                    Informationindustry.OnSearch();
                    return false;
                }
            });
            Informationindustry.GetInformationList(); //请求资讯列表
            $("#hrefUpdate").click(function() {//绑定修改点击事件
                Informationindustry.EditInfomation();
                return false;

            });
            $("#hrefDelete").click(function() {//绑定删除事件
                Informationindustry.DelteInfo();
                return false;
            });
        })
    </script>

    </form>
</body>
</html>
