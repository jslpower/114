<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetWorkMarketingList.aspx.cs"
    Inherits="SiteOperationsCenter.FeedbackManage.NetWorkMarketingList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网络营销反馈列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = "";
        }
        $(function() {
            $("#ImgSearch").click(function() {
                var typeId = $("#<%=ddlType.ClientID %>").val();
                var provinceId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["provinceId"]).val(); 
                var cityId = $("#" + provinceAndCityUserControl["<%= ProvinceAndCityList1.ClientID %>"]["cityId"]).val();
                var companyName = $.trim($("#<%=txtCompanyName.ClientID %>").val());
                var startTime = $.trim($("#<%=txtStartTime.ClientID %>").val());
                var endTime = $.trim($("#<%=txtEndTime.ClientID %>").val());
                window.location.href = "/FeedbackManage/NetWorkMarketingList.aspx?ProvinceId=" + provinceId + "&CityId=" + cityId + "&CompanyName=" + companyName + "&StartTime=" + startTime + "&EndTime=" + endTime + "&TypeId=" + typeId;
                return false;
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" bgcolor="#E2ECFE">
                    <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                    单位名称：
                    <input id="txtCompanyName" name='txtCompanyName' type="text" class="textfield" size="10"
                        runat="server" />申请类型：<asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    申请时间：<input type="text" runat="server" onfocus="WdatePicker()" style="width: 100px"
                        id="txtStartTime" name="txtStartTime" />至<input type="text" style="width: 100px"
                            runat="server" id="txtEndTime" onfocus="WdatePicker()" name="txtEndTime" />
                    <a href="javascript:;" id="ImgSearch">
                        <img alt="查询" src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif"
                            width="62" height="21" style="margin-bottom: -3px;" /></a>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/yunying/hangbg.gif" class="white"
                height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>序号</strong>
                </td>          
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称 </strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系人 </strong>
                </td>
                <td width="9%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系方式 </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>申请时间 </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>类别 </strong>
                </td>
                <td width="35%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>碰到的问题</strong>
                </td>
            </tr>
            <cc1:CustomRepeater ID="crp_NetWorkMarketList" runat="server">
                <ItemTemplate>
                    <%# (Container.ItemIndex+1)%2==1? "<tr class='baidi' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">" : "<tr bgcolor='#f3f7ff' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">"%>
                    <td height="25" align="center">                       
                      <%#((PageIndex - 1) * PageSize) + Container.ItemIndex + 1%>
                    </td>                   
                    <td align="center">
                     <%#Eval("CompanyName")%>
                    </td>
                    <td width="8%" align="center">
                      <%#Eval("Contact")%>
                    </td>
                    <td align="center">
                       <%#Eval("ContactWay")%>
                    </td>
                    <td align="center">
                        <%#Eval("RegisterDate", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("MarketingCompanyType")%> 
                    </td>
                    <td align="center">
                        <div style='word-wrap: break-word; width: 210px; overflow: hidden;'>
                         <%#Eval("Question")%></div>
                    </td>
                    </tr>
                </ItemTemplate>
            </cc1:CustomRepeater>
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>序号</strong>
                </td>                
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称<br />
                    </strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系人<br />
                    </strong>
                </td>
                <td width="9%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系方式<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>申请时间<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>类别 </strong>
                </td>
                <td width="35%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>碰到的问题</strong>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30" align="right">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
