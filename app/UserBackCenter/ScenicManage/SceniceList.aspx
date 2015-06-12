<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SceniceList.aspx.cs" Inherits="UserBackCenter.ScenicManage.SceniceList" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty"
    TagPrefix="uc1" %>
<asp:content id="SceniceList" runat="server" contentplaceholderid="ContentPlaceHolder1">
  <link href="<%=CssManage.GetCssFilePath("rightnew") %>" rel="stylesheet" type="text/css" />
  <style type="text/css">
        .showOrHideTr{display:none;}
        .TicketEndPrice{display:none;}
  </style>
  <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetCityList") %>" ></script>
        <div class="tablebox">
         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="tblHeadSearch">
           <tbody><tr>
             <td width="1%" align="left" class="toolbj">&nbsp;</td>
             <td align="left" class="toolbj"><b>景区等级：</b><%=this.GetSencinceLevel()%></td>
           </tr>
         </tbody></table>
		 
         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="tblHeadSearch">
           <tbody><tr>
             <td width="1%" align="left" class="toolbj">&nbsp;</td>
             <td align="left" class="toolbj"><b>景区主题：</b><%=this.GetTheme()%></td>
           </tr>
         </tbody></table>

         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="tblHeadSearch">
           <tbody><tr>
             <td width="1%" align="left" class="toolbj">&nbsp;</td>
             <td align="left" class="toolbj"><b>所在地区：</b><%=this.GetRegionHtml()%></td>
           </tr>
         </tbody></table>

         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="tblHeadSearch">
           <tbody><tr>
             <td width="1%" align="left" class="toolbj">&nbsp;</td>
             <td align="left" class="toolbj"><b>排序方式：</b>
               <a href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%=EyouSoft.Common.Utils.GetQueryStringValue("ProvinceId") %>&CityId=<%=EyouSoft.Common.Utils.GetQueryStringValue("CityId")%>&CountyID=<%=EyouSoft.Common.Utils.GetQueryStringValue("CountyID")%>&sortType=1&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>" <%=EyouSoft.Common.Utils.GetQueryStringValue("sortType")=="1"?"class=ff0000":"" %>>推荐</a> 
               <a href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%=EyouSoft.Common.Utils.GetQueryStringValue("ProvinceId") %>&CityId=<%=EyouSoft.Common.Utils.GetQueryStringValue("CityId")%>&CountyID=<%=EyouSoft.Common.Utils.GetQueryStringValue("CountyID")%>&sortType=2&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>" <%=EyouSoft.Common.Utils.GetQueryStringValue("sortType")=="2"?"class=ff0000":"" %> >点击量</a> 
               <a href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%=EyouSoft.Common.Utils.GetQueryStringValue("ProvinceId") %>&CityId=<%=EyouSoft.Common.Utils.GetQueryStringValue("CityId")%>&CountyID=<%=EyouSoft.Common.Utils.GetQueryStringValue("CountyID")%>&sortType=3&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>" <%=EyouSoft.Common.Utils.GetQueryStringValue("sortType")=="3"?"class=ff0000":"" %>>景区等级</a>
             </td>
           </tr>
         </tbody></table>
		 
         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="tblHeadSearch">
           <tbody><tr>
             <td height="30" align="left"><span class="search">&nbsp;</span>景区名称
               <input type="text" size="30" id="SceniceName" runat="server"/>
               城市
                    <asp:DropDownList id="ProvinceList" runat="server"></asp:DropDownList>
                   <asp:DropDownList id="CityList" runat="server"></asp:DropDownList>
                   <asp:DropDownList runat="server" id="CountyList"></asp:DropDownList>
             <img id="imgSearch" src="<%=ImageServerUrl %>/images/chaxun.gif" width="62" height="21" style="margin-bottom:-4px;"  alt=""/></td>
           </tr>
         </tbody></table>
         <!--列表-->
        <cc2:CustomRepeater ID="CustomRepeater1" runat="server">          
          <ItemTemplate>
		            <div class="jinqu_tablebk">
		               <table width="100%" border="0">
                             <tbody>
                             <tr>
                                   <td width="110" rowspan="2">
                                        <a title="<%# Eval("ScenicName") %>" href="<%=EyouSoft.Common.Domain.UserPublicCenter%>/ScenicManage/NewScenicDetails.aspx?ScenicId=<%# Eval("ScenicId") %>" target="_blank"><%#this.getPic(Eval("Img"))%></a>
                                   </td>
                                   <td valign="top" align="left">
                                        <%#this.getImgFlag(Eval("Company.CompanyLev"))%>&nbsp;
                                        <font class="title"><a title="<%# Eval("ScenicName") %>" href="<%=EyouSoft.Common.Domain.UserPublicCenter%>/ScenicManage/NewScenicDetails.aspx?ScenicId=<%# Eval("ScenicId") %>" target="_blank"><%# Eval("ScenicName") %></a> <%# GetSceniceLevel(Eval("ScenicLevel")) %></font>
                                        <br><b>地址：</b>
                                                <a class="provinces" href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%# Eval("ProvinceId") %>&CityId=<%# Eval("CityId")%>&CountyID=<%# Eval("CountyId")%>&sortType=<%=EyouSoft.Common.Utils.GetQueryStringValue("sortType") %>&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>"><%# Eval("ProvinceName")%></a>
                                               <a  class="provinces" href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%# Eval("ProvinceId") %>&CityId=<%# Eval("CityId")%>&CountyID=<%# Eval("CountyId")%>&sortType=<%=EyouSoft.Common.Utils.GetQueryStringValue("sortType") %>&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>"><%# Eval("CityName")%></a>  
                                               <a class="provinces" href="/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&ProvinceId=<%# Eval("ProvinceId") %>&CityId=<%# Eval("CityId")%>&CountyID=<%# Eval("CountyId")%>&sortType=<%=EyouSoft.Common.Utils.GetQueryStringValue("sortType") %>&SceniceName=<%=EyouSoft.Common.Utils.GetQueryStringValue("SceniceName") %>"><%# Eval("CountyName") %></a>
                                               <%# Eval("CnAddress")%>
                                        <strong>主题：</strong><%# GetThemeHtml(Eval("ThemeId"))%>  
                                        <a href="javascript:void(0);" onclick="SceniceList.getMap(this,'<%#Eval("X")%>','<%#Eval("Y")%>','<%#Eval("ScenicName") %>');">地图</a>
                                        <div class="map_canvas" style="display:none;"></div>
                                   </td>
                                   <td valign="top" align="right">
                                        <span class="toolbj1" style="float:right;"><a href="javascript:void(0);" onclick="SceniceList.showOrHideEndPrice(this,'<%#Container.ItemIndex%>')">显示结算价</a></span>
                                   </td>
                             </tr>
                             <tr>
                               <td valign="top" align="left" colspan="2">
                                    门票：<%#this.getTicketPrice(Eval("TicketsList"), 0, Eval("ScenicId"), Eval("ScenicName"))%>
                                    开放时间：<%#Eval("OpenTime")%>
                                    电话咨询：<%#Eval("ContactTel")%>
                                    景点简介：
                                    <%#EyouSoft.Common.Utils.GetText2(EyouSoft.Common.Utils.InputText(Eval("Description")),220,true)%>
                                </td>
                             </tr>
                           </tbody>
                       </table>
		               <div class="hr_10"></div>
		               <div id="tableTicketsPrice_<%#Container.ItemIndex%>">
		                    <table width="100%" border="0" class="tablelist">
                             <tbody>
                                <tr>
                                       <th>门票类型</th>
                                       <th>市场价</th>
                                       <th>优惠价</th>
                                       <th class="TicketEndPrice" style="display:none">结算价</th>
                                       <th>有效时间</th>
                                       <th>功能</th>
                                 </tr>
                                 <%#this.getTicketPrice(Eval("TicketsList"), 1, Eval("ScenicId"), Eval("ScenicName"))%>                                                                                               
                           </tbody>
                       </table>
		               </div>
		               <div class="hr_5"></div>
                       <table width="100%" border="0" align="center">
                             <tbody>
                                  <tr>
                                    <td align="right">
                                        <a href="javascript:void(0);" onclick="SceniceList.showOrHideTicketPrice(this,'<%#Container.ItemIndex%>')"><span>全部门票类型</span>
                                            <img border="0" align="middle" src="<%=ImageServerUrl %>/images/shang_img.gif">
                                        </a>
                                   </td>
                                 </tr>
                           </tbody>
                       </table>
		        </div>
		 </ItemTemplate>
		 </cc2:CustomRepeater>
		 <!--表格盒子结束-->
         <!--翻页-->
<table width="98%" cellspacing="0" cellpadding="4" border="0" align="center" id="Table1">
           <tbody><tr>
             <td align="right" class="F2Back">
                    <asp:Literal ID="lit_msg"  runat="server"></asp:Literal>              
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" CurrencyPageCssClass="RedFnt" LinkType="4" />
               </td>
           </tr>
         </tbody></table>
     </div>
     <script type="text/javascript">
      var SceniceList={
              //隐藏或显示“所有门票”
              showOrHideTicketPrice:function(obj,num)
              {
                    $con=$(obj).closest(".jinqu_tablebk").find('#tableTicketsPrice_'+num).find(".showOrHideTr");
                    if($con.length==0)
                        return false;
                    $con.toggle($con.css("display")=="none");//table的滑动太差,tr也不能直接兼容toggle
                    //if($con.is(":hidden"))
                    if($con.css("display")=="none")
                    {
                        $(obj).find("img").attr({src:"<%=ImageServerUrl %>/images/xia_img.gif"});
                        $(obj).find("span").text("全部门票类型");
                    }
                    else{
                        $(obj).find("img").attr({src:"<%=ImageServerUrl %>/images/shang_img.gif"});
                        $(obj).find("span").text("收起门票类型");
                    }
                    $conTd=$(obj).closest(".jinqu_tablebk").find('#tableTicketsPrice_'+num).find(".TicketEndPrice");
                    //if($($conTd[0]).is(":hidden"))
                    if($($conTd[0]).css("display")=="none")
                    {
                        $conTd.hide();
                    }
                    else
                    {
                        $conTd.show();
                    }
              },
              //显示或隐藏“结算价”列
              showOrHideEndPrice:function(obj,num)
              {
                    $con=$(obj).closest(".jinqu_tablebk").find('#tableTicketsPrice_'+num).find(".TicketEndPrice");
                    $con.toggle($con.css("display")=="none");
                    //if($($con[0]).is(":hidden"))//IE8中的is(":hidden")失效啊。
                    if($($con[0]).css("display")=="none")
                    {
                        $(obj).text("显示结算价").removeClass("select");
                    }
                    else{
                        $(obj).text("隐藏结算价").addClass("select");
                    }
              },
              //获取地图
              getMap:function(obj,x,y,title){//x:经度  y:纬度 测试：x=123.4353447  y=41.8081968
                var url="/ScenicManage/GoogleMapShow.aspx?x="+x+"&y="+y;
                parent.Boxy.iframeDialog({ title: title, iframeUrl: url, height: 430, width: 560, draggable: false });
                return false;
              },
              //单击门票后的详细信息
              showInfo:function(url){
                parent.Boxy.iframeDialog({ title: "景区门票详细信息", iframeUrl: url, height: 430, width: 860, draggable: false });
                return false;
              }
      };
      $(document).ready(function() {                    
          $("#imgSearch").click(function() {
              var SceniceName = $("#<%=SceniceName.ClientID %>").val();             
              var ProvinceID = $("#<%=ProvinceList.ClientID %>").val();            
              var CityID = $("#<%=CityList.ClientID %>").val();              
              var CountyID = $("#<%=CountyList.ClientID %>").val();
              var queryUrl = "/ScenicManage/SceniceList.aspx?LevelID=<%=EyouSoft.Common.Utils.GetQueryStringValue("LevelID") %>&themeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("themeId")%>&SceniceName=" + encodeURI(SceniceName) + "&ProvinceId=" + ProvinceID + "&CityId=" + CityID + "&CountyID=" + CountyID;
              topTab.url(topTab.activeTabIndex, queryUrl);
              return false;
          });

          $("#<%=SceniceName.ClientID %>").keydown(function(event) {
              if (event.keyCode == 13) {
                  $("#imgSearch").click();
                  return false;
              }
          });

          $("#Table1 a").each(function() {
              $(this).click(function() {
                  topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                  return false;
              })
          });

          $(".tblHeadSearch .toolbj a").click(function() {
              topTab.url(topTab.activeTabIndex, $(this).attr("href"));
              return false;
          });

          $(".showinfo").click(function() {
              var url = $(this).attr("href");
              parent.Boxy.iframeDialog({ title: "景区门票详细信息", iframeUrl: url, height: 430, width: 860, draggable: false });
              return false;
          });
          
          $(".provinces").click(function(){
              topTab.url(topTab.activeTabIndex, $(this).attr("href"));
              return false;
          });
      });
  </script>
</asp:content>