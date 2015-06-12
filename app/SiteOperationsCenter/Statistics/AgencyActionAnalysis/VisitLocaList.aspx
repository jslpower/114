<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitLocaList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.VisitLocaList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc3" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<%@ Register src="../../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>组团访问轨迹</title>
        <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
        <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
        <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
    单位名称<input type="text" id="txtcompanyName" runat="server" />
        <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" /> 
       <input id="btnSelect" type="button"  value="查 询"/>
         <table width="100%" >
            <tr class="tab_luan" style=" color:White; font-weight:bold; height:20px;">
                <td width="6%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">序号</td>
                <td width="24%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">批发商名称</td>
                <td width="17%"align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">操作/时间</td>
                <td width="53%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">团号/线路名称</td>
            </tr>
            <cc1:CustomRepeater ID="crptinfoList" runat="server">
                <ItemTemplate>
                     <tr class="baidi" onMouseOver="mouseovertr(this)" onMouseOut="mouseouttr(this)">
                        <td width="6%" align="center"><%# (Container.ItemIndex+1)+(pageIndex-1)*pageSize %></td>
                        <td width="24%">
                            <a href="javascript:void(0)" onclick='VisitLocaList.CompanyAccessDetail("<%# Eval("VisitedCompanyId")%>")'><%#Eval("VisitedCompanyName")%></a>
                        </td>
                        <td width="17%" ><%# ((EyouSoft.Model.TourStructure.VisitTourType)Eval("VisitTourType")).ToString()%>/<%# Eval("VisitedTime", "{0:yyyy-MM-dd}")%></td>
                        <td width="53%" align="left">【<%#Eval("VisitTourCode")%>】
                        <%# "<a href=\"" + Utils.GetTeamInformationPagePath(Eval("TourId").ToString()) + "\" target=\"_blank\" >" + Eval("VisitTourRouteName") + "</a>"%></td>
                       </tr>                       
                </ItemTemplate>
                <AlternatingItemTemplate>
                     <tr bgcolor="#F3F7FF" onMouseOver="mouseovertr(this)" onMouseOut="mouseouttr(this)">
                        <td width="6%" align="center"><%# (Container.ItemIndex+1)+(pageIndex-1)*pageSize %></td>
                        <td width="24%">
                            <a href="javascript:void(0)" onclick='VisitLocaList.CompanyAccessDetail("<%# Eval("VisitedCompanyId")%>")'><%#Eval("VisitedCompanyName")%></a>
                        </td>
                        <td width="17%" ><%#((EyouSoft.Model.TourStructure.VisitTourType)Eval("VisitTourType")).ToString()%>/<%# Eval("VisitedTime", "{0:yyyy-MM-dd}")%></td>
                        <td width="53%" align="left">【<%#Eval("VisitTourCode")%>】
                        <%# "<a href=\"" + Utils.GetTeamInformationPagePath(Eval("TourId").ToString()) + "\" target=\"_blank\" >" + Eval("VisitTourRouteName") + "</a>"%></td>
                       </tr>                
                </AlternatingItemTemplate>
        </cc1:CustomRepeater>
        </table>
        <table width="100%" >
            <tr><td align="right">
                <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td></tr>
        </table>
        <asp:HiddenField ID="hidCompanyId" runat="server" />
    </div>
    </form>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
        <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
        <script type="text/javascript" language="javascript">
          function mouseovertr(o) {
              o.style.backgroundColor="#FFF9E7";
              //o.style.cursor="hand";
          }
          function mouseouttr(o) {
              o.style.backgroundColor=""
          }
          var VisitLocaList= {
            CompanyAccessDetail: function(CompanyId) {  
                VisitLocaList.openDialog("/Statistics/CompanyInfo.aspx?companyid="+CompanyId, "公司信息","450", "300", null);         
                return false;
            },
            openDialog: function(url, title, width, height) {
                Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: null });
            }
          }
            $(document).ready(function(){                
                $("#txtcompanyName").bind("keypress", function(e)
                {
                    if (e.keyCode == 13) {
                    $("#btnSelect").click(); 
                    return false;
                    }
                });
                $("#btnSelect").click(function(){
                    var dateControl=<%=StartAndEndDate1.ClientID %>;
                    var StartDate=dateControl.GetStartDate();
                    var EndDate=dateControl.GetEndDate();
                    var CompanyName=encodeURIComponent($("#txtcompanyName").val());
                    var VisitedCompanyId=$("#hidCompanyId").val()
                    var urlstr="/Statistics/AgencyActionAnalysis/VisitLocaList.aspx?CompanyId="+VisitedCompanyId+"&BeginTime="+StartDate+"&EndTime="+EndDate+"&CompanyName="+CompanyName;
                    window.location.href=urlstr;
                    return false;
                });
            
            });
        </script>
</body>
</html>
