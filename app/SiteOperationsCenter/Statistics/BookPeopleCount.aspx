<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookPeopleCount.aspx.cs" Inherits="SiteOperationsCenter.Statistics.BookPeopleCount" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Register src="/usercontrol/StartAndEndDate.ascx"  TagName="date" TagPrefix="uc2"%>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
       <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
       <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery")%>"></script>
      <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
      <script type="text/javascript">
    var BookPeopleCount = {
        mouseovertr: function(o) {
            o.style.backgroundColor = "#FFF9E7";
        },
        mouseouttr: function(o) {
            o.style.backgroundColor = "";
        },
         //判断是否按回车
              isEnter: function(event) {
                  event = event ? event : window.event;
                  if (event.keyCode == 13) {
                      BookPeopleCount.search();
                  }
              },
        search: function(tar_btn) {
            var orderState="<%=strOrderState %>";
            var companyName = encodeURIComponent($("#bpc_txtCompanyName").val());
            var peopleName = encodeURIComponent($("#bpc_txtPeopleName").val());
            var sex = $("#<%=bpc_selSex.ClientID %>").val();
            var startDate = <%=bpc_date.ClientID%>.GetStartDate();
            var endDate=<%=bpc_date.ClientID %>.GetEndDate();
            window.location="?companyId=<%=companyId %>&companyname="+companyName+"&peoplename="+peopleName+"&sex="+sex+"&BeginTime="+startDate+"&EndTime="+endDate+"&OrderState="+orderState;
            return false;
        },
            //弹出窗体
    OpenDialog:function(id){
     var title="专线商信息";
     var width="550px";
     var url="/Statistics/CompanyInfo.aspx?companyid="+id;
     var height1=GetAddOrderHeight()-100;if(height1<=200) height1="384px";
        Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height1,draggable:true,data:null});
          return false;
     },
     PrintPage:function(toutId){
       var url="<%=EyouSoft.Common.Domain.UserBackCenter%>/PrintPage/TeamInformationPrintPage.aspx?TourID="+toutId;
       window.open(url);
     }
  
        
    }
</script>
</head>
<body>
    <form id="form1" runat="server" method="get">
    <div>
    
      <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
        
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
          
                专线商名称
                <input type="text" id="bpc_txtCompanyName"  style="width:90px;" value="<%=companyName %>" onkeyup="BookPeopleCount.isEnter(event)" />
                姓名
                <input type="text" class="textfield" size="12" style="width:80px;" value="<%=peopleName %>" id="bpc_txtPeopleName" onkeyup="BookPeopleCount.isEnter(event)"/>
                性别<select name="bpc_selSex" id="bpc_selSex"  runat="server" >
                <option value="">——</option>
                <option value="1">男</option>
                <option value="0">女</option>
                </select>
                <uc2:date id="bpc_date" runat="server" ></uc2:date>
                
                <a href="javascript:void(0);" onclick="return BookPeopleCount.search(this);"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <div style="text-align:center; height:15px; margin-top:6px; margin-bottom:5px;">预定定总人数<%=recordCount %>,下列表中为订单中填写的游客信息</div>
   <div style="text-align:center">
    <asp:CustomRepeater runat="server" id="bpc_rpt_applyList">
    <HeaderTemplate>
       <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="12%" height="25" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>姓名</strong></td>
            <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>性别</strong></td>
            <td width="14%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>证件号码</strong></td>
            <td width="14%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>联系电话</strong></td>
            <td width="28%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>【团号】旅游线路</strong></td>
            <td width="27%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>所属专线商</strong></td>
            
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
          
              <tr class="baidi" onMouseOver="BookPeopleCount.mouseovertr(this)" onMouseOut="BookPeopleCount.mouseouttr(this)">
                <td height="25" align="center"><%# Eval("VisitorName") %></td>
                <td  align="center"><%# Convert.ToBoolean(Eval("Sex"))?"男":"女" %></td>
                <td align="center"><%# Eval("CradNumber") %></td>
                <td align="center"><%# Eval("ContactTel") %></td>
                <td  align="center">【<%# Eval("TourNo") %>】<a href='javascript:void(0);' onclick='BookPeopleCount.PrintPage("<%# Eval("TourId")%>")'><%# Eval("RouteName") %></a></td>
                <td  align="center"><a href='javascript:' onclick='BookPeopleCount.OpenDialog("<%# Eval("CompanyID") %>")'><%# Eval("CompanyName") %></a></td>
                
            
            </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
     <tr bgcolor="#F3F7FF" onMouseOver="BookPeopleCount.mouseovertr(this)" onMouseOut="BookPeopleCount.mouseouttr(this)">
                <td height="25" align="center"><%# Eval("VisitorName") %></td>
                <td height="25" align="center"><%# Convert.ToBoolean(Eval("Sex"))?"男":"女" %></td>
                <td align="center"><%# Eval("CradNumber") %></td>
                <td align="center"><%# Eval("ContactTel") %></td>
                <td width="15%" align="center">【<%# Eval("TourNo") %>】<a href='javascript:void(0)' onclick='return BookPeopleCount.PrintPage("<%# Eval("TourId")%>")'><%# Eval("RouteName") %></a></td>
                <td width="8%" align="center"><a href='javascript:' onclick='return BookPeopleCount.OpenDialog("<%#Eval("CompanyID") %>")'><%# Eval("CompanyName") %></a></td>
                
              
            
            </tr>
    
    </AlternatingItemTemplate>
    <FooterTemplate>
       </table>
    </FooterTemplate>
    </asp:CustomRepeater></div>

            
            
         
             <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
   
  
   
      

</html>
