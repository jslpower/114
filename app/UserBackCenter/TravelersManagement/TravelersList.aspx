<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelersList.aspx.cs" Inherits="UserBackCenter.TravelersManagement.TravelersList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="ConNewsList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'TravelersList'
    });
</script>
<style type="text/css">
    table .odd	{ background:#F3F7FF;}
    table tr.highlight{ background:#FFF3BF;}
    table tr.selected{ background:#FFF3E7;color:#fff;}
</style>
<div class="right">
	  <div class="tablebox">
       <table id="<%=tblID %>" width="99%" cellspacing="0" cellpadding="0" border="0" class="margintop5">
         <tbody><tr>
           <td valign="top">
           <table width="100%" cellspacing="0" cellpadding="0" border="0" class="zttoolbar">
               <tbody><tr>
                 <td valign="bottom"><div class="zttooltitle">所有客户</div></td>
                 <td align="center" style="padding-left:10px;"><a style="cursor:hand" href="javascript:void(0);" onclick="TravelersList.add();"><img width="67" height="23" border="0" style="margin-top:6px;" src="<%=ImageServerUrl %>/images/xz001.gif"></a>
                 <%--<div style="background:url(<%=ImageServerUrl %>/images/zxtoolright.gif); width:4px; height:32px; float:right;"></div>--%>
                 </td>
               </tr>
               <tr>
                 <td colspan="2">
                         <table width="100%" cellspacing="0" cellpadding="0" border="0" >
                           <tbody>
                              <tr>
                                 <td width="4%" align="center"><img width="23" height="24" src="<%=ImageServerUrl %>/images/searchico2.gif"></td>
                                 <td width="30%" align="left" valign="middle"> 
                                   关键字(姓名、手机号)
                                     <input type="text" size="30" name="txtKeyWord" id="txtKeyWord"  onkeypress="return TravelersList.isEnter(event);" />
                                 </td>
                                 <td width="66%" align="left" valign="middle">
                                     <a href="javascript:void(0);" onclick="TravelersList.goSearch();"><img style="border:none;" src="<%=ImageServerUrl %>/images/chaxun.gif"/></a>
                                 </td>
                               </tr>
                            </tbody>
                         </table>
                 </td>
               </tr>
             </tbody></table>
			 
			 <table width="100%" cellspacing="0" cellpadding="2" bordercolor="#B9D3E7" border="1" class="zttype">
                 <tbody><tr>
                   <th width="10%" align="center">中文名</th>
                   <th width="10%" align="center">英文名</th>
                   <th width="14%" align="center">所在地区</th>
                   <th width="11%" align="center">用户类型</th>
                   <th width="5%" align="center">性别</th>
                   <th width="15%" align="center">身份证</th>
                   <th width="10%" align="center">护照</th>
                   <th width="10%" align="center">手机号码</th>
                   <th width="15%" align="center">操作</th>
                 </tr>
                 <cc1:CustomRepeater runat="server" ID="RepList">
                    <ItemTemplate>
                         <tr>
                           <td align="center"><%#Eval("ChinaName")%></td>
                           <td align="center"><%#Eval("EnglishName")%></td>
                           <td align="center" class="tbline"><%#this.getCityName(Eval("CityId"), Eval("CountryId"))%></td>
                           <td align="center"><%#Eval("VistorType")%></td>
                           <td align="center"><%#Eval("ContactSex")%></td>
                           <td align="center"><%#Eval("IdCardCode")%></td>
                           <td align="center"><%#Eval("PassportCode")%></td>
                           <td align="center"><%#Eval("Mobile")%></td>
                           <td align="center">
                                <a href="javascript:void(0);" onclick="TravelersList.edit('<%#Eval("Id")%>')" class="basic_btn"><span>修改</span></a>
                                <a href="javascript:void(0);" onclick="TravelersList.del('<%#Eval("Id")%>')" class="zhuangtai_btn"><span>删除</span></a>
                           </td>
                         </tr>                        
                    </ItemTemplate>
                 </cc1:CustomRepeater>
             </tbody></table></td>
         </tr>
       </tbody></table>
                <!--翻页-->
         <table width="98%" cellspacing="0" cellpadding="4" border="0" align="center">
               <tbody><tr id="tablePageTr">
                 <td align="right" class="F2Back"> 
                        <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
                  </td>
               </tr>
             </tbody>
         </table>
	   </div>
     </div>
     <script type="text/javascript">
         var TravelersList = {
             //判断是否按回车
             isEnter: function(event) {
                 event = event ? event : window.event;
                 if (event.keyCode == 13) {
                     TravelersList.goSearch();
                     return false;
                 }
             },
             _getData: function() {
                 return commonTourModuleData.get('<%=this.tblID %>');
             },
             goSearch: function() { //搜索
                 $conObj = $("#" + TravelersList._getData().ContainerID);
                 var kw = $.trim($conObj.find("#txtKeyWord").val());
                 var searchUrl = "/TravelersManagement/TravelersList.aspx?kw=" + encodeURIComponent(kw);
                 topTab.url(topTab.activeTabIndex, searchUrl);
                 return false;
             },
             //新增
             add: function() {
                 topTab.url(topTab.activeTabIndex, '/TravelersManagement/TravelersAdd.aspx');
             },
             //修改
             edit: function(id) {
                 topTab.url(topTab.activeTabIndex, '/TravelersManagement/TravelersAdd.aspx?id=' + id);
                 return false;
             },
             //删除
             del: function(id) {
                 if (confirm("您确定要删除该条记录吗？")) {
                     $.newAjax({
                         url: "/TravelersManagement/TravelersList.aspx?method=ajax",
                         data: { id: id, type: "del" },
                         type: "GET",
                         success: function(data) {
                            alert(data);
                            topTab.url(topTab.activeTabIndex, '/TravelersManagement/TravelersList.aspx?kw=<%=Request.QueryString["kw"]%>');
                         },
                         error: function() { alert("服务器繁忙，请稍后再试！"); }
                     });
                 }
                 return false;
             }
         };
             $(function(){
                   $conObj = $("#" + TravelersList._getData().ContainerID);
                   //分页在当前选项卡中加载
                   $conObj.parent().find("#tablePageTr a").each(function() {
                       $(this).click(function() {
                           topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                           return false;
                       })
                   });
                   //搜索框初始化
                   $conObj.find("#txtKeyWord").val('<%=Server.UrlDecode(Request.QueryString["kw"])%>');
                   //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
                   $conObj.find("table.zttype").find("tr:gt(0):even").addClass('odd');
                   $conObj.find("table.zttype").find("tr:gt(0)").hover(
		            function() { $(this).addClass('highlight'); },
		            function() { $(this).removeClass('highlight'); }
	            );
             });
    </script> 
</asp:content>