<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScatterPlanReserve.aspx.cs"
    Inherits="UserBackCenter.TeamService.ScatterPlanReserve" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="ScatterPlan" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'ScatterPlanReserve',
    });
</script>
       <div id="<%=tblID %>" class="tablebox">
         <table width="100%" border="0" align="center">
           <tr>
             <td align="left" valign="top" class="ftxt"><form id="form1">
                   <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc" style="width:100%;">
                       <tr>
                       <td colspan="2" align="left" valign="top"><img src="<%=ImageServerUrl %>/images/jiben3.gif" /></td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>订单号</strong>：</td>
                         <td align="left"><asp:Label runat="server" ID="lblOrderNo" Text=""></asp:Label></td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>线路名称：</strong></td>
                         <td align="left">                         
                             <a href="javascript:void(0);" target="_blank" class="PrintSingle">
                               <asp:Label runat="server" ID="lblRouteName" Text=""></asp:Label>
                             </a>
                              <asp:DropDownList ID="ddlRouteList" runat="server"></asp:DropDownList>
                              <strong>当前剩余空位：<asp:Literal ID="litSYKW" runat="server"></asp:Literal></strong>&nbsp;&nbsp; 
                              <strong>状态：<asp:Literal ID="litTourState" runat="server"></asp:Literal> </strong>
                         </td>
                       </tr>
                       <tr>
                         <td width="18%" align="right" bgcolor="#F2F9FE"><strong>发布单位：</strong></td>
                         <td align="left">
                            <a href="<%=StrMQUrl %>" target="_blank" >
                             <asp:Literal ID="litCompanyName" runat="server"></asp:Literal></a><strong> </strong>
                            <strong> MQ：</strong> 
                            <%=StrMQImg%>
                            <strong> QQ：</strong> 
                            <%=StrQQUrl %>
                            <strong> </strong>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>出发城市：</strong></td>
                         <td align="left">
                           <label>
                              <asp:Literal ID="litStartPlace" runat="server"></asp:Literal> 
                              <asp:Literal ID="litRouteState" runat="server"></asp:Literal>
                           </label>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>交通：</strong></td>
                         <td align="left"> 
                            <asp:Literal ID="litStartTraffic" runat="server"></asp:Literal> 
                            <asp:Literal ID="litEndTraffic" runat="server"></asp:Literal>
                        </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>出发时间 航班：</strong></td>
                         <td align="left">
                            <asp:Literal ID="litStartDate" runat="server"></asp:Literal> 
                            <asp:Literal ID="litStartFlight" runat="server"></asp:Literal>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>返回时间 航班：</strong></td>
                         <td align="left">
                            <asp:Literal ID="litEndDate" runat="server"></asp:Literal> 
                            <asp:Literal ID="litEndFlight" runat="server"></asp:Literal>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>集合说明：</strong></td>
                         <td align="left">
                           <asp:Literal ID="litSetexplain" runat="server"></asp:Literal>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>领队全陪说明：</strong></td>
                         <td align="left">
                           <asp:Literal ID="litManagexplain" runat="server"></asp:Literal>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>游客联系人：</strong></td>
                         <td align="left"><input  id="txtTourisContectName"  runat="server"/>
                           联系电话
                           <input id="txtTourisContectPhone"  runat="server"/></td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>商家负责人：</strong></td>
                         <td align="left"><input  id="txtMerchants"  runat="server" />
                           </span></span>联系电话
                           <input id="txtMerchantsPhone" runat="server"/>
                           【预定的时候默认取出预订者的信息，但是可以修改】 </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>价格组成：</strong></td>
                         <td align="left"> 成人
                           <input  id="txtAdultPrice" runat="server"/>
                           人，儿童
                           <input  id="txtChildrenPrice" runat="server"/>
                           人，单房差
                           <input  id="txtRoomSent" runat="server"/>
                         人</td>
                       </tr>
                       <tr>
                         <td colspan="2" align="left"><b>游客详细信息</b>
                           <a href="javascript:" class="daorumd">导入名单</a>
                         </td>
                       </tr>
                       <tr>
                         <td colspan="2" align="right">
                          <table width="99%" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#9DC4DC">
                           <tr align="middle" bgcolor="#D4E6F7">
                             <td align="center">序号</td>
                             <td align="center">姓名</td>
                             <td align="center">联系电话</td>
                             <td align="center">身份证</td>
                             <td align="center">护照</td>
                             <td align="center">其他证件</td>
                             <td align="center">性别</td>
                             <td align="center">类型</td>
                             <td align="center">座号</td>
                             <td align="center">备注（勾选保存）
                               <input name="checkbox3" type="checkbox" value="checkbox" /></td>
                           </tr>
                           <tr>
                             <td align="center">1</td>
                             <td align="center"><input value="游客" size="8" name="CustomerName12" />
                               <a href="" class="jian_btn">删</a></td>
                             <td align="center"><input id="CertificateNo1472" name="CertificateNo1473" style="width:80px;" /></td>
                             <td align="center"><input id="CertificateNo142" name="CertificateNo143" style="width:100px;" /></td>
                             <td align="center"><input id="CertificateNo144" name="CertificateNo145" style="width:100px;" /></td>
                             <td align="center"><input id="CertificateNo146" name="CertificateNo147" style="width:80px;" /></td>
                             <td align="center"><select id="select" name="select">
                                 <option selected="selected" value="1">男</option>
                                 <option value="0">女</option>
                             </select></td>
                             <td align="center"><select id="select3" name="select3">
                                 <option value="1">成人</option>
                                 <option value="0">儿童</option>
                             </select></td>
                             <td align="center"><input name="CertificateNo149" id="CertificateNo148"  size="6" /></td>
                             <td align="center"><input id="CertificateNo1410" name="CertificateNo1411" style="width:100px;" />
                                 <input name="checkbox3" type="checkbox" value="checkbox" /></td>
                           </tr>                           
                         </table>
                        </td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE">
                            <strong>市场价：<br />
                             <input type="checkbox" name="checkbox" value="checkbox" />
                                    结算价：<br />&nbsp;
                            </strong>
                         </td>
                         <td align="left">成人14000元，儿童 13000 元，单房差 600 元，增减销售价
                           <input name="ro_txtManCount4" id="ro_txtManCount4"/>元
                             <br />成人13000 元，儿童 12000 元<br />同业销售须知
                         </td>
                       </tr>
                       <tr>
                         <td align="right" nowrap="nowrap" bgcolor="#F2F9FE"><strong>游客备注：</strong></td>
                         <td align="left"><textarea name="textarea18" id="textarea18" cols="85" rows="4"></textarea></td>
                       </tr>
                       <tr>
                         <td align="right" bgcolor="#F2F9FE"><strong>组团社备注：</strong></td>
                         <td align="left"><textarea name="textarea" id="textarea" cols="85" rows="4"></textarea></td>
                       </tr>
                       <tr sizcache="0" sizset="14">
                         <td align="right" bgcolor="#F2F9FE"><strong>总金额：</strong></td>
                         <td align="left" sizcache="0" sizset="14">销售价100 
                          &nbsp; 结算价 13000                           </td>
                       </tr>
                       <tr sizcache="0" sizset="14">
                         <td colspan="2" align="center">
                         <asp:LinkButton class="baocun_btn" id="btnSave" runat="server"  onclick="btnSave_Click">保 存</asp:LinkButton>
                         <asp:LinkButton  class="baocun_btn" id="btnClose" runat="server">关 闭</asp:LinkButton> 
                         </td>
                       </tr>
               </table>
                   </form>
             </td>
           </tr>
         </table>
       </div>

     <script type="text/javascript">
         $(document).ready(function() {
             $("#<%=btnClose.ClientID %>").click(function() {
                 topTab.url(topTab.activeTabIndex, "/TeamService/ScatterPlanC.aspx");
                 return false;
             });

             //导入游客
             $(".daorumd").click(function() {
                 var AdultC = parseInt($("#<%=txtAdultPrice.ClientID %>").val());
                 if (!AdultC) { AdultC = 0; }
                 var ChildRenC = parseInt($("#<%=txtChildrenPrice.ClientID %>").val());
                 if (!ChildRenC) { ChildRenC = 0; }
                 var PeopleCount = AdultC + ChildRenC;
                 Boxy.iframeDialog({ title: "游客名单", iframeUrl: "/TeamService/TouristsList.aspx?Count=" + PeopleCount, height: 460, width: 800, draggable: false });
                 return false;
             });
         });
     </script>
</asp:content>
