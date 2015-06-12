<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoAdList.aspx.cs" Inherits="SiteOperationsCenter.AdManagement.PhotoAdList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图片广告列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
 
    <style type="text/css">
        .style1
        {
            width: 90px;
        }
        .style2
        {
            width: 90px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr style="height: 30px;">
            <td align="center">
                <h3>
                    <asp:Literal ID="ltr_menuManag" runat="server"></asp:Literal>
                </h3>
            </td>
        </tr>
        <tr>
            <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxunbg.gif">
                <span lang="zh-cn">开始时间：</span>
                <input type="text" onfocus="WdatePicker()" valid="required" errmsg=" 请填写有效期" id="DatePicker1"
                    name="DatePicker1" runat="server" class="style2"><span lang="zh-cn">结束时间：<input type="text"
                        onfocus="WdatePicker()" valid="required" errmsg=" 请填写有效期" id="DatePicker2" name="DatePicker2"
                        runat="server" class="style2"></span><uc1:ProvinceAndCityList ID="ProvinceAndCityList1"
                            runat="server" />
                <span lang="zh-cn">购买单位：</span>
                <input name="txtUnitName" type="text" class="style2" size="12" id="txtUnitName" runat="server" />&nbsp;
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62"
                        height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="23%" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif">
                <table width="51%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_da.gif" width="3"
                                height="20" />
                        </td>
                        <td width="23%">
                            <a href="javascript:void(0)" id="hrefAdd" style="cursor: pointer">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xinzeng.gif" width="50"
                                    height="25" border="0" /></a>
                        </td>
                        <td width="4%">
                            <a href="javascript:void(0)" style="cursor: pointer">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                    height="25" /></a>
                        </td>
                        <td width="24%">
                            <a href="javascript:void(0)" id="hrefUpdate" style="cursor: pointer">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xiugai.gif" width="50"
                                    height="25" border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" />
                        </td>
                        <td class="style1">
                            <a href="javascript:void(0)" id="hrefDelete">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                    height="25" /></a>
                        </td>
                        <td width="17%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_d.gif" width="11"
                                height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="77%" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif"
                align="left">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tbl_AdvList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
        <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
            class="white">
            <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style6">
                <strong>序号
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style3">
                <strong>广告位置</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style8">
                <strong>大图</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style8">
                <strong>缩略图</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>标题</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>简要</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>购买单位</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style7">
                <strong>联系方式</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style4">
                <strong>投放城市</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                <strong>有效期</strong>
            </td>
        </tr>
        <cc2:CustomRepeater ID="crptPhotoList" runat="server">
            <ItemTemplate>
                <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="left">
                        <strong></strong>
                        <input id="chkDelete" value="<%#Eval("AdvId") %>" type="checkbox" /><input name="textfield2"
                            type="text" value="<%#Eval("SortId") %>" size="3" onchange="isTopChange(this,'<%#Eval("AdvId") %>')" />
                        <input type="hidden" id="sort<%#Eval("AdvId") %>" value="<%#Eval("SortId") %>" />
                    </td>
                    <td align="left">
                        <%#Eval("Position").ToString() %>
                    </td>
                    <td align="left">
                        <a href="<%=Domain.FileSystem %><%#Eval("ImgPath").ToString() %>" target="_blank">
                            <%#GetHtmlByFile(Eval("ImgPath").ToString())%>
                        </a>
                    </td>
                    <td align="left">
                        <a href="<%=Domain.FileSystem %><%#Eval("AdvThumb").ToString() %>" target="_blank">
                            <%#GetHtmlByFile(Eval("AdvThumb").ToString())%>
                        </a>
                    </td>
                    <td align="center">
                    <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                    <%#Eval("Title")%>
                    </div>
                     <%--   <%#Eval("Title")%>--%>
                    </td>
                    <td align="center">
                    <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                    <%#Eval("Remark")%>
                    </div>
                     <%--   <%#Utils.GetText(Eval("Remark").ToString(),8)%>--%>
                    </td>
                    <td align="center">
                        <%#Eval("CompanyName")%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" onmouseover='wsug(this,"联系方式:<%# Eval("ContactInfo")%><br/>MQ:<%# Eval("ContactMQ")%><br />")'
                            onmouseout="wsug(this, 0)">
                            <%#Eval("ContactInfo")%></a>
                    </td>
                    <td align="center">
                        <%#GetRelation(Eval("Range"), Eval("Relation"))%>
                    </td>
                    <td align="center">
                        <%#GetDate(Eval("StartDate").ToString(),Eval("EndDate").ToString())%>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="left">
                        <strong></strong>
                        <input id="chkDelete" value="<%#Eval("AdvId") %>" type="checkbox" /><input name="textfield2"
                            type="text" value="<%# Eval("SortId")%>" size="3" onchange="isTopChange(this,'<%#Eval("AdvId") %>')" />
                        <input type="hidden" id="sort<%#Eval("AdvId") %>" value="<%#Eval("SortId") %>" />
                    </td>
                    <td align="left">
                        <%#Eval("Position").ToString() %>
                    </td>
                    <td align="left">
                        <a href="<%=Domain.FileSystem %><%#Eval("ImgPath").ToString() %>" target="_blank">
                            <img src="<%=Domain.FileSystem %><%#Eval("ImgPath").ToString() %>" width="100px"
                                height="50px" /></a>
                    </td>
                    <td align="left">
                        <a href="<%=Domain.FileSystem %><%#Eval("AdvThumb").ToString() %>" target="_blank">
                            <%#GetHtmlByFile(Eval("AdvThumb").ToString())%>
                        </a>
                    </td>
                    <td align="center">
                        <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                    <%#Eval("Title")%>
                    </div>
                    </td>
                    <td align="center">
                         <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                    <%#Eval("Remark")%>
                    </div>
                    </td>
                    <td align="center">
                        <%#Eval("CompanyName")%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" onmouseover='wsug(this,"联系方式:<%# Eval("ContactInfo")%><br/>MQ:<%# Eval("ContactMQ")%><br />")'
                            onmouseout="wsug(this, 0)">
                            <%#Eval("ContactInfo")%></a>
                    </td>
                    <td align="center">
                        <%#GetRelation(Eval("Range"), Eval("Relation"))%>
                    </td>
                    <td align="center">
                        <%#GetDate(Eval("StartDate").ToString(),Eval("EndDate").ToString())%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </cc2:CustomRepeater>
        <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
            class="white">
            <td height="26" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style6">
                <strong>排序</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style3">
                <strong>广告位置</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style8">
                <strong>大图</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style8">
                <strong>缩略图</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>标题</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>简要</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style5">
                <strong>购买单位</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style7">
                <strong>联系方式</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                class="style4">
                <strong>投放城市</strong>
            </td>
            <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                <strong>有效期</strong>
            </td>
        </tr>
        <tr>
            <td colspan="7" align="right">
                <div id="OrdersAllOutSource_ExportPage">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="startPosition" runat="server" />
    <input type="hidden" id="hdAdvId" />
    <input type="hidden" id="hdStartSortId" />
    </form>

    <script type="text/javascript">
        //****
        //样式
        //****
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        //根据权限判断隐藏和显示按钮
        function GantShowAndHide() {
            if ("<%=IsAddGant %>".toLowerCase() == "false") {
                $("#hrefAdd").hide();
            }
            if ("<%=IsUpdateGant %>".toLowerCase() == "false") {
                $("#hrefUpdate").hide();
                $("input[type='checkbox']").each(function() {
                    $(this).focus(function() {
                        this.blur();
                    });
                });
            }
            if ("<%=IsDeleteGant %>".toLowerCase() == "false") {
                $("#hrefDelete").hide();
            }
            if ("<%=IsShowMoreCell %>".toLowerCase() == "false") {
                $("#tbl_AdvList").find("tr").each(function() {
                $(this).find("td:eq(3)").hide();
                $(this).find("td:eq(4)").hide();
                $(this).find("td:eq(5)").hide();
                });
            }

        }
        $(document).ready(function() {

            GantShowAndHide();
            $("#btnSearch").click(function() {
                var url = OnLoadPage("");
                if (url != "请选择城市") {
                    $(this).attr("href", url);
                } else {
                    return false;
                }
            });
            $("#hrefAdd").click(function() {
                if ("<%=IsAddGant %>".toLowerCase() == "true") {
                    var StartPosition = $("#startPosition").val();
                    $(this).attr("href", "/AdManagement/<%=PagePath %>?position=" + StartPosition)
                } else {
                    alert("您没有添加的权限");
                    $(this).attr("onclick", function() { return false });
                    return false;
                }
            });
            $("#hrefUpdate").click(function() {
                if ("<%=IsUpdateGant %>".toLowerCase() == "true") {
                    var StartPosition = $("#startPosition").val();
                    var advid = IsSelectAdv();
                    if (advid != -1) {
                        $(this).attr("href", "/AdManagement/<%=PagePath %>?AdvId=" + advid);
                    } else {
                        alert("请选择一项！");
                        return false;
                    }
                } else {
                    alert("您没有修改的权限");
                    return false;
                }
            });
            $("#hrefDelete").click(function() {
                if ("<%=IsDeleteGant %>".toLowerCase() == "true") {
                    var StartPosition = $("#startPosition").val();
                    var advid = IsSelectAdv();
                    if (advid != -1) {
                        if (!confirm("你确定要删除该条数据吗？")) {
                            return false;
                        }
                        $.ajax
                        ({
                            url: "/AdManagement/AjaxAdSort.aspx?Type=Delete&Id=" + advid,
                            cache: false,
                            async: false,
                            success: function(msg) {
                                switch (msg) {
                                    case "2":
                                        alert("操作成功！");
                                        window.location.href = OnLoadPage("delete");

                                        break;
                                    case "1":
                                        alert("操作失败！");
                                        break;
                                    case "-1":
                                        alert("请选择一项进行操作！");
                                        break;
                                }

                            },
                            error: function() {
                                alert("操作失败");
                            }
                        });
                    } else {
                        alert("请选择一项！");
                        return false;
                    }
                } else {
                    alert("您没有删除的权限");
                    return false;
                }
                return false;
            });
        });
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[type='checkbox']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id = $(this).val();
                }
            });
            if (num == 1) {
                return id;
            }
            else {
                return -1;
            }
        }
        function OnLoadPage(opear) {
            var companyName = escape($("#txtUnitName").val());
            var start = $("#DatePicker1").val();
            var end = $("#DatePicker2").val();
            var cityid = $("#ProvinceAndCityList1_ddl_CityList").val();
            var provinceid = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
            var StartPosition = $("#startPosition").val();
            if (provinceid == 0 || cityid == 0) {
                if (opear != "delete" && opear != "sort") {
                    return "请选择城市";
                }
            }
            return "/AdManagement/PhotoAdList.aspx?unit=" + companyName + "&province=" + provinceid + "&city=" + cityid + "&start=" + start + "&end=" + end + "&position=" + StartPosition;
        }
        function isTopChange(obj, advid, oldsort) {
            var oldsort = $("#sort" + advid).val();
            if ("<%=IsUpdateGant %>".toLowerCase() == "true") {
                var sortId = $(obj).val();
                var position = $("#startPosition").val();
                if (sortId == "" || sortId == null) {
                    alert("请填写序号");
                    $(obj).val(oldsort);
                    return false;
                } else {
                    if (checkIsNumber(sortId)) {
                        var cityid = $("#ProvinceAndCityList1_ddl_CityList").val();
                        var provinceid = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
                        if (provinceid == 0 || cityid == 0) {
                            alert("请选择关联的省份和城市！");
                            $(obj).val(oldsort);
                            return;
                        }
                        else {
                            $.ajax
                            ({
                                url: "AjaxAdSort.aspx?Type=Sort&Id=" + advid + "&sort=" + sortId + "&province=" + provinceid + "&city=" + cityid + "&postion=" + position,
                                cache: false,
                                async: false,
                                success: function(msg) {
                                    var result = "";
                                    switch (msg) {
                                        case "0":
                                            alert("城市和省份为必选、排序值为必填！");
                                            $(obj).val(oldsort);
                                            break;
                                        case "-1":
                                            alert("请选择一项进行操作！");
                                            $(obj).val(oldsort);
                                            break;
                                        case "2":
                                            alert("操作成功！");
                                            $("#sort" + advid).val(sortId);
                                            break;
                                        case "1":
                                            alert("操作失败！");
                                            $(obj).val(oldsort);
                                            break;
                                    }
                                },
                                error: function() {
                                    alert("操作失败");
                                    $(obj).val(oldsort);
                                }
                            });
                        }
                    }
                    else {
                        alert("序号应为整数值并不能为负数！");
                        $(obj).val(oldsort);
                        return;
                    }
                }
            } else {
                alert("您没有修改的权限");
                $(obj).val(oldsort);
                return;
            }
        }
        function checkIsNumber(sortvalue) {
            var checkvalue = $.trim(sortvalue);
            var result = null;
            if (sortvalue != "") {
                var part = /^[+]?\d+(\d+)?$/;
                result = part.exec(sortvalue);

            }
            return result;
        }     
    </script>

</body>
</html>
