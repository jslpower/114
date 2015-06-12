<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCustomers.aspx.cs" Inherits="UserBackCenter.CustomerManage.MyCustomers" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="MyCustomers" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
$(document).ready(function(){

 $("#mc_ExportPage").find("a").click(
                function(event){
                     window.MyCustomers.search($(this).attr("href"));
                    return false;
                 });
                 
//MyCustomers.mc_pc = <%=mc_pc.ClientID%>;

});
var MyCustomers=
{  
      mouseovertr:function(o){
          o.style.backgroundColor="#FFF9E7";
      },
      mouseouttr:function(o){
         o.style.backgroundColor="";
      },
     tabChange:function(theHref){
         topTab.url(topTab.activeTabIndex,theHref);
         return false;
     },
      OpenDialog:function(companyid){
          Boxy.iframeDialog({title:"查看产品", iframeUrl:"/CustomerManage/TourList.aspx?companyid="+companyid,width:"650px",height:GetAddOrderHeight(),draggable:true,data:null});
      },
       OpenDialog2:function(companyid){
          Boxy.iframeDialog({title:"查看交易记录", iframeUrl:"/CustomerManage/TradeRecords.aspx?companyid="+companyid,width:"650px",height:GetAddOrderHeight(),draggable:true,data:null});
          return false;
      },
     search:function(theHref){
        if(/Page/.test(theHref))
        {
          topTab.url(topTab.activeTabIndex,theHref);
          return false;
        }
         var province1=encodeURIComponent($("#"+<%=mc_pc.ClientID%>.getProvince()).val());
         var city1=encodeURIComponent($("#"+<%=mc_pc.ClientID%>.getCity()).val());
         var companyName1=encodeURIComponent($("#<%=mc_txtCompanyName.ClientID %>").val());
         var admin1=encodeURIComponent($("#<%=mc_txtAdmin.ClientID %>").val());
         var brand1=encodeURIComponent($("#<%=mc_txtBrand.ClientID %>").val());
         
         topTab.url(topTab.activeTabIndex,theHref+"?province="+province1+"&city="+city1+"&companyname="+companyName1+"&admin="+admin1+"&brand="+brand1);
         return false;
     },
      clearMyCustomer:function(){
         if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
      
        if($("#mc_tourTableList").find(":checkbox:checked").length==0)
        {
          alert("请选择客户!");
          return false;
        }
        else
        {
          var ids1="";
          $($("#mc_tourTableList").find(":checkbox[id!='mc_checkall']:checked")).each(function(){
            ids1+=$(this).val()+",";
           
          });
          ids1=ids1.substring(0,ids1.length-1);
          topTab.url(topTab.activeTabIndex,"/CustomerManage/MyCustomers.aspx?method=clearMyCustomers&ids="+encodeURIComponent(ids1));
        }
     },
     selectAll:function(tar_chk){
       $("#mc_tourTableList").find(":checkbox").attr("checked",$(tar_chk).attr("checked"));
     }
     
}
</script>
<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top" >
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="zttoolbar">
          <tr>
            <td width="550"><div class="zttooltitle" style=" padding-top:5px;">我的客户</div>
            <div class="zttooltitleun" style=" padding-top:5px;"><a href="javascript:void(0);" onclick="return MyCustomers.tabChange('/CustomerManage/AllCustomers.aspx')">所有客户</a></div></td>
          
            <td width="3" align="right" style="background:url(<%=ImageServerUrl%>/images/zxtoolright.gif) no-repeat; width:2px;"></td>
          </tr>
          <tr>
            <td colspan="2">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
              <tr>
                <td width="4%" align="center"><img src="<%=ImageServerUrl%>/images/searchico2.gif" width="23" height="24" /></td>
                <td width="96%" align="left">
				<%--省份<select name="mc_province" id="mc_province" class="selzx2">
				  <option>请选择</option>
                  </select>
				  城市<select name="select2" id="mc_city" class="selzx2">
				  <option>请选择</option>
                  </select>--%>
                  <uc1:pc id="mc_pc" runat="server" ></uc1:pc>
				  公司名称<input name="mc_txtCompanyName" id="mc_txtCompanyName" runat="server" type="text" size="15" />
				  总负责人<input name="mc_txtAdmin" id="mc_txtAdmin"  runat="server" type="text" size="15" />
				  品牌名称<input name="mc_txtBrand" id="mc_txtBrand" runat="server" type="text" size="15" />
				  <input type="image" name="mc_btnSearch" id="mc_btnSearch" onclick="return MyCustomers.search('/CustomerManage/MyCustomers.aspx')"  value="提交" src="<%=ImageServerUrl%>/images/chaxun.gif" style="width:62px; height:21px; border:none; margin-bottom:-5px;" />                  </td>
              </tr>
            </table></td>
          </tr>
        </table>
		<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7" class="zttype" id="mc_tourTableList">
         <asp:CustomRepeater id="alc_rpt_customers" runat="server" OnItemCreated="rpt_customers_ItemCreated">
<HeaderTemplate>
   <tr>     
            <th width="7%" align="center">全选<input type="checkbox" name="checkbox2" value="checkbox" id="mc_checkall" onclick="MyCustomers.selectAll(this)" style="vertical-align:middle; margin-top:-1px"/></th>
            <th width="8%" align="center">线路区域</th>
            <th width="8%" align="center">所在城市</th>
            <th width="17%" align="center">【品牌】公司名称</th>
            <th width="8%" align="center">交易记录</th>
            <th width="8%" align="center">总负责人</th>
            <th width="8%" align="center">手机</th>
            <th width="11%" align="center">电话</th>
            <th width="10%" align="center">传真</th>
            <th width="5%" align="center">MQ</th>
            <th width="10%" align="center">查看产品</th>
   </tr>
   </HeaderTemplate>
   <ItemTemplate>
          <tr onMouseOver="MyCustomers.mouseovertr(this)" onMouseOut="MyCustomers.mouseouttr(this)">
           <td align="center">
              <input type="checkbox" name="checkbox" value='<%# Eval("ID") %>' id="ac_chk1" />
            </td>
            <td align="left"><asp:Literal id="ltrAreaName" runat="server"></asp:Literal></td>
            <td align="center"><%# GetProAndCity(Convert.ToInt32(Eval("ProvinceId")),Convert.ToInt32(Eval("CityId")))%></td>
            <td align="left" class="tbline"><%#string.IsNullOrEmpty(Convert.ToString(Eval("CompanyBrand"))) ? "" : string.Format("【{0}】", Eval("CompanyBrand"))%>            <a href="<%#EyouSoft.Common.Utils.GetShopUrl(Eval("ID").ToString()) %>" target="_blank"> <%#Eval("CompanyName")%></a>
            </td>
            <td align="center"><a href="javascript:void(0)" onclick="return MyCustomers.OpenDialog2('<%# Eval("CustomerCompanyID") %>')"><strong><%#GetRecordNum(Eval("CustomerCompanyID").ToString())%></strong>次</a></td>
            <td align="center"><%#Eval("ContactInfo.ContactName")%></td>
            <td align="center"><%# GetMoible(Eval("ContactInfo.Mobile"))%></td>
            <td align="center"><%# Eval("ContactInfo.Tel")%></td>
            <td align="center"><%# Eval("ContactInfo.Fax")%></td>
            <td align="center"><%# Utils.GetMQ(Eval("ContactInfo.MQ").ToString())%></td>
            <td align="center"><asp:Literal id="ltrLookTour" runat="server"></asp:Literal></td>
            </tr>
    </ItemTemplate>
   
    </asp:CustomRepeater>
        </table><table id="mc_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
  	<table width="100%" border="0" cellspacing="0" cellpadding="3">
          <tr>
            <td width="5%" align="right"><img src="<%=ImageServerUrl%>/images/zhexian.gif" width="20" height="22" /></td>
            <td width="95%" align="left"><input type="button" name="ac_SetCustomer" id="ac_SetCustomer" onclick="MyCustomers.clearMyCustomer()" value="解除合作关系" style="height:26px;" /></td>
          </tr>
        </table>
</td>
      </tr></table>
</asp:content>
