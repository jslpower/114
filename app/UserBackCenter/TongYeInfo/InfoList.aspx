<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoList.aspx.cs" Inherits="UserBackCenter.TongYeInfo.InfoList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="ConNewsList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'InfoList'
    });
</script>
<style type="text/css">
    .liststyle .odd	{ background:#F3F7FF;}
    .liststyle tr.highlight{ background:#FFF3BF;}
    .liststyle tr.selected{ background:#FFF3E7;color:#fff;}
</style>
<div class="tablebox">
         <table id="<%=this.tblID %>" cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="searchTable">
           <tbody><tr style="background:url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
             <td width="1%" height="30" align="left">&nbsp;</td>
             <td width="84%" align="left"> 关键字(标题、发布单位)：
               <input size="15" name="txtKeyWord" id="txtKeyWord" type="text" onkeypress="return InfoList.isEnter(event);" />
分类
    <select name="selType" id="selType">
       <option value="-1">--请选择--</option>
      <%=this.getInfo("type")%>
    </select>
    <a href="javascript:void(0);" onclick="InfoList.goSearch();"><img width="62" height="21" style="margin-bottom:-4px;border:0px;" src="<%=ImageServerUrl %>/images/chaxun.gif"></a></td>
             <td width="15%" align="center">
                    <span class="blue_btn" id="btnAddInfo" runat="server"><a href="/GeneralShop/TongYeNews/NewsList.aspx" rel="toptab">添加同业资讯</a></span>
             </td>
           </tr>
         </tbody></table>
         <!--列表-->
         <table cellspacing="0" cellpadding="1" bordercolor="#9dc4dc" border="1" align="center" class="liststyle padd5" style="width:100%; margin-top:1px;">
           <tbody><tr class="list_basicbg odd">
              <th nowrap="nowrap" class="list_basicbg">分类</th>
             <th class="list_basicbg">标题</th>
             <th nowrap="nowrap" class="list_basicbg">发布单位</th>
             <th nowrap="nowrap" class="list_basicbg">发布时间</th>
           </tr>
           <cc1:CustomRepeater runat="server" ID="RepList">
                <ItemTemplate>
                   <tr>
                      <td align="center"><%#Eval("TypeId")%></td>
                     <td align="left">
                         <%--<a class="font12_grean" href="javascript:void(0);" title='<%#Eval("AreaName")%>'><%#string.IsNullOrEmpty(Eval("AreaName").ToString()) ? "" : string.Format("【{0}】", Utils.GetText2(Eval("AreaName").ToString(), 6, true))%></a>--%>
                         <%#this.getInfoAboutHref(Eval("AreaName"), Eval("AreaId"), Eval("ScenicId"), Eval("CompanyId"))%>
                        <a href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex,'/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>')"><%#Eval("Title")%></a>
                        <%--<a href="javascript:void(0);" onclick='parent.Boxy.iframeDialog({ title: "<%#Eval("Title")%>", iframeUrl: "/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>", height: 600, width: 800, draggable: false });'><%#Eval("Title")%></a>--%>
                     </td>
                     <td align="left">
                        <%#this.getShopUrl(Eval("CompanyName"), Eval("CompanyId"))%>
                        <%#Utils.GetMQ(Eval("OperatorMQ").ToString())%>
                     </td>
                     <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
                   </tr>
                </ItemTemplate>
           </cc1:CustomRepeater>
         </tbody></table>
         <!--翻页-->
         <table width="98%" cellspacing="0" cellpadding="4" border="0" align="center" id="Table1">
           <tbody><tr id="tablePageTr">
             <td align="right" class="F2Back">
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
            </td>
           </tr>
         </tbody></table>
       </div>
       <script type="text/javascript">
           var InfoList = {
               //判断是否按回车
               isEnter: function(event) {
                   event = event ? event : window.event;
                   if (event.keyCode == 13) {
                       InfoList.goSearch();
                       return false;
                   }
               },
               _getData: function() {
                   return commonTourModuleData.get('<%=this.tblID %>');
               },
               //搜索
               goSearch: function() {
                   $conObj = $("#" + InfoList._getData().ContainerID);
                   var kw, type;
                   kw = $.trim($conObj.find("#txtKeyWord").val());
                   type = $conObj.find("#selType").val();
                   var searchUrl = "/TongYeInfo/InfoList.aspx?kw=" + encodeURIComponent(kw) + "&type=" + type;
                   topTab.url(topTab.activeTabIndex, searchUrl);
                   return false;
               },
               //单击“相关资讯”搜索
               setKeyWord: function(kw) {
                   $conObj = $("#" + InfoList._getData().ContainerID);
                   $conObj.find("#txtKeyWord").val(kw);
                   InfoList.goSearch();
               }
           }
           $(function() {
               $conObj = $("#" + InfoList._getData().ContainerID);
               //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
               $('.liststyle tr:even').addClass('odd');
               $('.liststyle tr').hover(
		            function() { $(this).addClass('highlight'); },
		            function() { $(this).removeClass('highlight'); }
	            );
               //搜索框初始化
               $conObj.find("#txtKeyWord").val('<%=Server.UrlDecode(Request.QueryString["kw"])%>');
               $conObj.find("#selType option").each(function() {
                   if (this.value == '<%=Request.QueryString["type"]%>') {
                       $(this).attr("selected", "selected");
                   }
               });
               //分页在当前选项卡中加载
               $conObj.parent().find("#tablePageTr a").each(function() {
                   $(this).click(function() {
                       topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                       return false;
                   })
               });
           });
       </script>
</asp:content>