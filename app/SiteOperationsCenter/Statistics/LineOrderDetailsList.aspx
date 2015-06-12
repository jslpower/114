<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineOrderDetailsList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.LineOrderDetailsList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/StartAndEndDate.ascx" TagName="StartAndEndDate"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var UserListManage = {
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td id="tb_txt" background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                    <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />
                    组团社名称<input type="text" id="txtTourCompanyName" style="width: 85px;" runat="server" />
                    团号<input type="text" id="txtTourNo" style="width: 85px;" runat="server" />
                    线路名称
                    <input type="text" class="textfield" size="12" id="txtRouteName" style="width: 85px;"
                        runat="server" />
                    <img id="imgSearch" src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62"
                        height="21" style="margin-bottom: -3px; cursor: pointer;" />
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>预定人数</strong>
                </td>
                <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>
                        <asp:Literal ID="litTime" runat="server"></asp:Literal>
                    </strong>
                </td>
                <td width="20%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>组团社名称</strong>
                </td>
                <td width="20%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>专线商名称</strong>
                </td>
                <td width="38%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>【团号】线路名称</strong>
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <%# (Container.ItemIndex+1)%2==1? "<tr class='baidi'>" : "<tr bgcolor='#f3f7ff'>"%>
                    <td align="center">
                        <%#Eval("AdultNumber")%>
                        +
                        <%#Eval("ChildNumber")%>
                    </td>
                    <td align="center">
                        <%#Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd hh:mm:ss")%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick="OpenCompany('1','<%#Eval("BuyCompanyID") %>')">
                            <%#Eval("BuyCompanyName")%></a>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick="OpenCompany('2','<%#Eval("TourCompanyId") %>')">
                            <%#Eval("TourCompanyName")%></a>
                    </td>
                    <td align="left">
                        【<%#Eval("TourNo")%>】<a href="<%#EyouSoft.Common.Utils.GetTeamInformationPagePath(Eval("TourID").ToString()) %>"
                            target="_blank"><%#Eval("RouteName")%></a>
                    </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30" align="center">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hideOrderType" runat="server" />
    <asp:HiddenField ID="hideOrderIndex" runat="server" />
    <asp:HiddenField ID="hideCompanyId" runat="server" />
    <asp:HiddenField ID="hideType" runat="server" />
    </form>
</body>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

<script src="../DatePicker/WdatePicker.js" type="text/javascript"></script>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

<script type="text/javascript">
    $(function() {
        $("#imgSearch").click(function() {
            $("#hideOrderIndex").val("2");
            $("#hideType").val("");
            SearchData();
        });

        $("#tb_txt input[type='text']").keydown(function(event) {
            if (event.keyCode == 13) {
                $("#imgSearch").click();
                return false;
            }
        });

    });

    function SearchData() {
        var startTime = $("#StartAndEndDate1_dpkStart").val();
        var endTime = $("#StartAndEndDate1_dpkEnd").val();
        //组团社名称
        var CompanyName = encodeURIComponent($("#txtTourCompanyName").val());
        //团号
        var TourNo = encodeURIComponent($("#txtTourNo").val());
        //线路名
        var RouteName = encodeURIComponent($("#txtRouteName").val());
        //订单的类型
        var OrderType = $("#hideOrderType").val();
        //批发商ID
        var CompanyId = $("#hideCompanyId").val();

        var strUrl = "LineOrderDetailsList.aspx?startTime=" + startTime + "&endTime=" + endTime + "&buyCompanyName=" + CompanyName + "&tourNo=" + TourNo + "&RouteName=" + RouteName + "&orderIndex=" + $("#hideOrderIndex").val() + "&companyId=" + CompanyId + "&type=" + $("#hideType").val() + "&orderType=" + OrderType;

        window.location.href = strUrl;
        return false;
    }
    //对列表进行排序
    function DataSort() {

        if ($("#hideOrderIndex").val() == "0") {
            $("#hideOrderIndex").val("0");
            SearchData();
        } else {
            $("#hideOrderIndex").val("1");
            SearchData();
        }
    }

    function OpenCompany(type, companyId) {
        if (type == "1") {
            OpenDialog("CompanyInfo.aspx", "组团社信息", 450, 300, "companyId=" + companyId);
        } else {
            OpenDialog("CompanyInfo.aspx", "专线商信息", 450, 300, "companyId=" + companyId);
        }
    }

    //弹层方法
    function OpenDialog(strurl, strtitle, strwidth, strheight, strdate) {
        parent.Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate,fixed:false });
    }
</script>

</html>
