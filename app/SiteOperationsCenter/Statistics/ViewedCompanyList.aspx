<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewedCompanyList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.ViewedCompanyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/StartAndEndDate.ascx" TagName="StartAndEndDate"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc3" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>专线商被访问记录</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainbody">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="tab_luand">
            <tr class="lr_hangbg">
                <td align="left" id="tdSearch">
                    <span class="unnamed1">
                        <img src="<%=ImageServerUrl %>/Images/point16.gif" width="21" height="21"></span>&nbsp;&nbsp;
                    组团社名称：
                    <input name="txtCompanyName" type="text" id="txtCompanyName" class="textfield" style="width:120px" value="<%=Request.QueryString["CompanyName"] %>" />
                    线路名称：
                    <input name="txtRouteName" type="text" id="txtRouteName" class="textfield" style="width:120px" value="<%=Request.QueryString["RouteName"] %>" />
                    <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />
                    <input type="button" value="查询" id="btnSearch" class="an_tijiaobaocun" />
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" id="td_content">
                    <table width="100%">
                        <tr class="tab_luan">
                            <td width="6%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                                序号
                            </td>
                            <td width="24%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                                组团社名称
                            </td>
                            <td width="53%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                                团号/线路名称
                            </td>
                            <td width="17%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                                时间
                            </td>
                        </tr>
                        <cc1:CustomRepeater ID="rptCompanyList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td width="6%" align="center">
                                        <%# GetCount()%>
                                    </td>
                                    <td align="center" width="24%">
                                        <a onclick='return OpenCompanyInfo("<%#Eval("ClientCompanyId") %>");' href='javascript:void(0);'><%#Eval("ClientCompanyName") %></a>
                                    </td>
                                    <td align="center" width="53%">
                                        【<%#Eval("VisitTourCode")%>】
                                        <%# "<a href=\"" + Utils.GetTeamInformationPagePath(Eval("TourId").ToString()) + "\" target=\"_blank\" >" + Eval("VisitTourRouteName") + "</a>"%>
                                    </td>
                                    <td align="center" width="17%">
                                        <%# Eval("VisitedTime")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <cc3:exportpageinfo id="ExportPageInfo1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    <script type="text/javascript">
    function openDialog(strurl, strtitle, strwidth, strheight, strdate) {
        Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
    }
    function OpenCompanyInfo(CompanyId){
        openDialog("/Statistics/CompanyInfo.aspx", "公司信息", "500", "300", "CompanyId="+CompanyId);
        return false;
    }
    function search(){
        var param = {
            "CompanyId":"<%=CompanyId %>",  
            "RouteArea":"<%=RouteArea %>",
            "companyname":$("#txtCompanyName").val(),
            "routename":$("#txtRouteName").val(),
            "BeginTime":StartAndEndDate1.GetStartDate(),
            "EndTime":StartAndEndDate1.GetEndDate()
          };
          
          window.location.href = "/Statistics/ViewedCompanyList.aspx?"+$.param(param);
    }
    $(function() {
       $("#btnSearch").click(function(){
          search();
       });
       //回车查询
        $("#tdSearch input[type='text']").keypress(function(e){
            if(e.keyCode == 13)
            {
                search();
                return false;
            }
        });
    });
    </script>
</body>
</html>
