<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryTour.aspx.cs" Inherits="UserBackCenter.RouteAgency.QueryTour" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组团社查找</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />
    <style>
        body
        {
            width: 730px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#E1E6F1"
        style="padding-top: 20px;">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="tab_luand">
                    <tr class="lr_hangbg">
                        <td width="12%" align="left">
                            <span class="unnamed1">
                                <img src="<%=ImageServerUrl %>/Images/point16.gif" width="21" height="21"></span>
                            &nbsp;&nbsp;
                            <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                            公司名称：
                            <input name="CompanyName" type="text" id="txtCompanyName" value="<%=CompanyName %>"
                                class="textfield" style="width: 179px;" />
                            <input type="button" name="btnSearch" value="查询" id="btnSearch" class="an_tijiaobaocun" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr style="text-align: center;">
                        <asp:Repeater runat="server" ID="rptQueryTour" OnItemDataBound="rptQueryTour_ItemDataBound">
                            <ItemTemplate>
                                <td width="33%" style="background-color: #EBF4FF; text-align: left; font-size: 12px;
                                    word-break: break-all; border: 1px solid #4F9DE3;">
                                    <strong style="color: #A94711">
                                        <input name="CompanyID" id="companyId<%#Eval("ID") %>" type="radio" refname="<%#((EyouSoft.Model.CompanyStructure.ContactPersonInfo)Eval("ContactInfo")).ContactName %>"
                                            reftel="<%#((EyouSoft.Model.CompanyStructure.ContactPersonInfo)Eval("ContactInfo")).Tel %>"
                                            value='<%#Eval("ID") %>'>
                                        <label for="companyId<%#Eval("ID") %>">
                                            <%#Eval("CompanyName")%></label></strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <div id="ExportPage" class="F2Back" style="text-align: right;" height="40">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
                        runat="server"></cc2:ExportPageInfo>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" height="40" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            &nbsp;<input type="button" name="btnSelect" value="选择组团社" id="btnSelect" class="renyuan_an" />
                            &nbsp;&nbsp;<input name="button" type="button" class="renyuan_an" value="关　闭" onclick="closeWin()"
                                id="Button1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>

    <script type="text/javascript" language="javascript">
        function closeWin() {
            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();
        }
        function GetSelectCompany() {
            var tourId = "";
            var tourName = "";
            var NeedId = '<%=Request.QueryString["NeedId"] %>'; //预订窗口的iframeID
            var radios = document.getElementsByName("CompanyID");
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked) {
                    tourId = radios[i].value;
                    tourName = $(radios[i]).next().text();
                }
            }
            if (tourId == "") {
                alert("请选择组团社！")
                return;
            }
            if (typeof (parent.getTourInfo) == "function") {
                parent.getTourInfo(NeedId, tourName, tourId);
            } else {
                parent.NotStartingTeams.getTourInfo(NeedId, tourName, tourId);
            }
            closeWin();
        }
        function query() {
            var companyName = document.getElementById("txtCompanyName").value;
            var NeedId = '<%=Request.QueryString["NeedId"] %>'; //预订窗口的iframeID
            var type = '<%=Request.QueryString["type"] %>';
            var backCallFun,key,iframeId;
            var url="QueryTour.aspx?CompanyName=" + companyName + "&Province=" + $("#ProvinceAndCityList1_ddl_ProvinceList").val() + "&City=" + $("#ProvinceAndCityList1_ddl_CityList").val() + "&NeedId=" + NeedId;
            if (type == "new") {
                 backCallFun = '<%=Request.QueryString["backCallFun"] %>';
                 key='<%=Request.QueryString["key"] %>';
                 iframeId='<%=Request.QueryString["iframeId"] %>';
       
                location.href =encodeURI( url+ "&type="+type+"&backCallFun="+backCallFun+"&key="+key+"&iframeId="+iframeId);
            } else {
                location.href = encodeURI(url);
            }
        }
        $(function() {
            $("#btnSelect").click(function() {
                var type = '<%=Request.QueryString["type"] %>';
                if (type != "new") {
                    GetSelectCompany();
                } else {
                    var data = { comID: "", comName: "", conName: "", conTel: "" };
                    var selectObj = $("input[type='radio']:checked");
                    if (selectObj.length > 0) {
                        var backCallFun = '<%=Request.QueryString["backCallFun"] %>';
                        data.comID = selectObj.val();
                        data.comName = $.trim(selectObj.next().html());
                        data.conName = selectObj.attr("refname");
                        data.conTel = selectObj.attr("reftel");
                        parent.window[backCallFun](data, '<%=Request.QueryString["key"] %>');
                        closeWin();
                    } else {
                        alert("请选择！");
                    }
                }
            })

            document.getElementById("btnSearch").onclick = function() {
                query();
            }
            $("input[id^='companyId'],label[for^='companyId']").dblclick(function() {
                $("#btnSelect").click();
            })
            $("#ExportPage a").click(function() {

                var NeedId = '<%=Request.QueryString["NeedId"] %>'; //预订窗口的iframeID  
                window.location.href = $(this).attr("href") + "&NeedId=" + NeedId;
                return false;

            })
            $("#txtCompanyName").keydown(function(e) {
                if (e.keyCode == 13) {
                    query();
                    return false;
                } else {
                    return true;
                }
            });
        })        
    </script>

    </form>
</body>
</html>
