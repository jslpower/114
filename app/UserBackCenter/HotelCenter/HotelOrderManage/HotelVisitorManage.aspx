<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelVisitorManage.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.HotelVisitorManage" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content id="HotelVisitorManage" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">   	
<table id="tbl_hvm_VisitorList" width="815" border="0" cellpadding="0" cellspacing="0">
		<tr>
            <td width="693" height="35" align="left"  class="pand" style="font-size:14px;"><strong><font color="#003C61">常旅客管理</font></strong></td>
            <td width="130" height="35" align="right"><strong><font color="#0000FF">   
            <%--/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx
            topTab.open($(this).attr('href'),'添加常旅客',{isRefresh:false,data:{UpPage:0}});return false;   --%>  
            <a  href='javascript:void(0);' onclick="HotelVisitorManage.AddVisitorInfo();">
            <img alt="添加常旅客" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_03.jpg" width="95" height="25" border="0" />
            </a></font></strong>
            </td>
		</tr>
        </table>
       <table width="815" border="1" cellpadding="0" cellspacing="0" bordercolor="#9AC8EC">
          <tr>
            <th width="75" height="28" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">中文名</th>
            <th width="79" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">英文名</th>
            <th width="100" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">国家 </th>
            <th width="69" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF"> 用户类型</th>
            <th width="89" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">证件类型</th>
            <th width="161" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">证件号码 </th>
            <th width="116" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF">联系方式</th>
            <th width="108" align="center" background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" bgcolor="#EEF7FF"> 操 作</th>
          </tr>
        <cc1:CustomRepeater ID="hvm_crp_HotelVisitorList" runat="server">
            <ItemTemplate>
                <tr>
                <td height="28" align="center" bgcolor="#FFFFFF"><a href='javascript:void(0);' onclick="HotelVisitorManage.EditVisitorInfo('<%#Eval("Id") %>')"><%#Eval("ChinaName")%></a></td>
                <td align="center" bgcolor="#FFFFFF"><%#Eval("EnglishName")%></td>
                <td align="center" bgcolor="#FFFFFF"><%#((EyouSoft.Model.TicketStructure.TicketVistorInfo)GetDataItem()).NationInfo.CountryName.ToString()%></td>
                <td align="center" bgcolor="#FFFFFF"><%#Eval("VistorType")%></td>
                <td align="center" bgcolor="#FFFFFF"><%#GetCardType(Convert.ToString(Eval("CardType")))%></td>
                <td align="center" bgcolor="#FFFFFF"><%#Eval("CardNo")%></td>
                <td align="center" bgcolor="#FFFFFF"><%#Eval("ContactTel")%></td>
                <td align="center" bgcolor="#FFFFFF">
                 <a href='javascript:void(0);' onclick="HotelVisitorManage.EditVisitorInfo('<%#Eval("Id") %>')">修改资料</a><strong>|</strong>
                <a href='javascript:void(0);' onclick="HotelVisitorManage.DeleteVisitorInfo('<%#Eval("Id") %>')">删除</a>
                </td>
              </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
        <tr runat="server" id="trPage" >
		     <td height="28" colspan="8" bgcolor="#EEF7FF"  background="<%=ImageServerUrl %>/images/hotel/userBackCenter/clk_07.jpg" class="F2Back" align="center">
		        <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
              </td>
		    </tr>
        </table>	
<script type="text/javascript">
///////////////////绑定分页控件///////////////////////
    $(document).ready(function() {
        $("#<%=trPage.ClientID%>").find("a").click(
                function(event) {
                    HotelVisitorManage.GetVisitorList($(this).attr("href"));
                    return false;
                });
    });
//////////////////////////////////////////////////////////
var HotelVisitorManage={
    DeleteVisitorInfo:function(VisitorId)//删除
        {           
           if (confirm('您确定要删除此旅客信息吗？\n\n此操作不可恢复！'))
           {
                $.newAjax
                ({
                    type: "POST",
                    url: "/HotelCenter/HotelOrderManage/HotelVisitorManage.aspx?DeleteId=" + VisitorId,
                    cache: false,
                    success: function(html) {
                        if(html=="False")
                        {
                            alert("删除失败！");
                            return false;
                        }else{
                            alert("删除成功");
                            topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorManage.aspx");
                        }
                     },error:function(){
                            alert("对不起，删除失败！");
                     }
                 });
                 return false;
             }
        },
       AddVisitorInfo:function()//新增
       {
            topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx");
            return false;
       },
       EditVisitorInfo:function(visitorId)//修改
       {   
           topTab.url(topTab.activeTabIndex,"/HotelCenter/HotelOrderManage/HotelVisitorsPage.aspx?EditId="+visitorId);
           return false;
       },
      GetVisitorList:function(PageUrl)//获取常旅客列表
      {
           topTab.url(topTab.activeTabIndex,PageUrl);
           return false;
      }
}
</script>
</asp:Content>
			

