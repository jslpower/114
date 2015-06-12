<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectorySet.aspx.cs" Inherits="UserBackCenter.TeamService.DirectorySet" %>
<%@ Register Src="~/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<asp:Content id="DirectorySet" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<style>
  table{
   border-collapse:collapse;
  }
</style>
<script type="text/javascript">

var DirectorySet={
    //选择线路区域,并显示该区域下的专线商
    lineon:function(id,tar_a){
      $("#ds_lineCompanyList").html("<p style='text-align:center'>正在加载数据……</p>");
       var pageIndex=1;
       $("#ds_areaId").val(id);
      $("#ds_strong").text($(tar_a).html())
       if(typeof tar_a!="string")
       {  
           if($(tar_a).parent("td").attr("class")!="aon")
           { 
             $("#ds_tdAreaList").find("td[class='aon']").attr("class","aun");
             $(tar_a).parent("td").attr("class","aon");
           };
       }
       else
          pageIndex=tar_a;
    
       $.ajax(
           {
             url:"/TeamService/AjaxLineCompanyList.aspx",
             type:"get",
             dataType:"html",
             data:{areaid:id,Page:pageIndex},
             cache:false,
             success:function(result){
                $("#ds_lineCompanyList").html(result);
                $("#ajlc_ExportPage").find("a").click(
                   function(event){
                      window.DirectorySet.lineon(id,$(this).attr("href").match(/Page=\d+/)[0].match(/\d+/)[0]);
                      return false;
                   });
              
             },
             error:function(){
                alert("请求时发生错误!")
             }
         });
     },
     mouseovertr:function(o){
        o.style.backgroundColor="#FFF9E7";
     },
     mouseouttr:function(o){
        o.style.backgroundColor="";
     },
      OpenDialog:function(title,url,width,height){
          Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:null});
          return false;
    },
    checkCompany:function(tar_chk){
         if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         var method1="nosel";
         if($(tar_chk).attr("checked"))
            method1="sel";
             $.newAjax(
              {
               url:"/TeamService/DirectorySet.aspx",
               data:{method:method1,areaid:$("#ds_areaId").val(),companyid:$(tar_chk).attr("value")},
               dataType:"json",
               cache:false,
               type:"get",
               success:function(result){
                  alert(result.message);
               },
               error:function(){
                   alert("操作失败!");
               }
             })
            
    }
  }
  $(document).ready(function() {
      //线路选项卡切换
      $("#ds_home1,#ds_home2,#ds_abroad1").click(function() {
          $(this).attr("class", "lineon").parent().siblings().children("div").attr("class", "lineun");
          var tableid = "#" + $(this).attr("id") + "1";
          $(tableid).css("display", "").siblings("[id!='ds_routeTab']").css("display", "none");
          var firstRoute = $(tableid).find("td[class!='noarea']:first");
          if (firstRoute.length > 0) {
              firstRoute.children("a").click();
          }
          else {
              $("#ds_strong").text("线路区域")
              $("#ds_lineCompanyList").html("暂无批发商信息！");
          }
          return false;
      });
      var readyFirst = $("#ds_home11").find("td[class!='noarea']:first");
      if (readyFirst.length > 0) {
          readyFirst.children("a").click();
      }
      else {
          $("#ds_lineCompanyList").html("暂无批发商信息！");
      }

  })
</script>

	<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top" >
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="zttoolbar">
          <tr>
            <td height="32" align="left" style="padding-left:10px;">目前平台加盟<%=totalCompany%>家 您设置了<span class="chengse"><strong><%=setCompanyNum%></strong></span>家 </td>
            <td width="6" align="right" style="background:url(<%=ImageServerUrl%>/images/zxtoolright.gif); width:4px;"></td>
          </tr>
        </table>
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="56"><img src="<%=ImageServerUrl%>/images/setcl.gif" width="740" height="42" /></td>
          </tr>
        </table>
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
           <tr>
            <td align="center" class="font12_grean" style="font-size:14px; padding-bottom:10px;"><table width="99%" border="0" cellspacing="0" cellpadding="0">
              <tr>
               <td align="center" style="border:1px dashed #CC0000; border-right:0px; padding:3px; background:#FFFFF2;"><img src="<%=ImageServerUrl%>/images/tisimg.gif" width="66" height="67" /></td>
                <td align="left" style="border:1px dashed #CC0000; border-left:0px; padding:3px; background:#FFFFF2;"><span class="font12_grean" style="font-size:14px; padding-bottom:10px;">系统说明：<br />
“挑选专线”是为了方便组团社采购产品，您可以预设好哪些专线与您长期合作，<br />
<span style="color:#990000; background:#FFFF00">此后被选中的供应商产品将直接在“<font color="#0000FF"><a target="_blank" style="text-decoration:none">进入采购区</a></font>”中显示!</span></span><br/>
在左侧选中线路区域名称，右侧自动显示该线路的产品供应商，选择您合作的专线商
<span style=" font-size:12px; font-weight:normal">调整或增加供应商，请重复以上操作</span></td>
              </tr>
            </table>
              </td>
          </tr>
        </table>
		<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="tablewidth">
          <tr>
            <td width="25%" valign="top" id="ds_tdAreaList">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:5px;" id="ds_routeTab">
          <tr>
          <input type="hidden"  id="ds_areaId"/>
            <td width="70" class="linebj"><Div class="lineon" id="ds_home1" ><a href="javascript:void(0)" >国内长线</a></Div></td>
            <td width="70" class="linebj"><Div class="lineun" id="ds_home2"  ><a href="javascript:void(0)" >国内短线</a></Div></td>
            <td width="70" class="linebj"><Div class="lineun" id="ds_abroad1"><a href="javascript:void(0)" >国际线</a></Div></td>
          </tr>
     </table>
   
     <%--国内长线 --%>
	    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:200px;" id="ds_home11">
	    <%=longArea %>
	        <asp:Repeater id="ds_rpt_home1" runat="server">
	         <HeaderTemplate><tr></HeaderTemplate>
	         <ItemTemplate>
	         <td height="23" class="aun" width="95"><a href="javascript:void(0)" onclick="DirectorySet.lineon('<%#Eval("AreaId") %>',this);return false;"><%#Eval("AreaName") %></a></td>
	         <%#GetItem()%>
	         </ItemTemplate>
	     <FooterTemplate><%#GetFooterItem()%></FooterTemplate>
            </asp:Repeater>
	        
	      
      </table>
      <%--国内短线 --%>
        <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:200px; display:none;" id="ds_home21">
        <%=shortArea %>
           <asp:Repeater id="ds_rpt_home2" runat="server">
	         <HeaderTemplate><tr></HeaderTemplate>
	         <ItemTemplate>
	         <td height="23" class="aun"  width="95"><a href="javascript:void(0)" onclick="DirectorySet.lineon('<%#Eval("AreaId") %>',this);return false;"><%#Eval("AreaName") %></a></td>
	         <%#GetItem()%>
	         </ItemTemplate>
	        <FooterTemplate><%#GetFooterItem()%></FooterTemplate>
	        </asp:Repeater>
      </table>
      
      <%--国际线 --%>
        <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:200px; display:none;" id="ds_abroad11">
        <%=exitArea %>
         <asp:Repeater id="ds_rpt_abroad1" runat="server">
	       <HeaderTemplate><tr></HeaderTemplate>
	         <ItemTemplate>
	         <td height="23" class="aun"  width="95"><a href="javascript:void(0)" onclick="DirectorySet.lineon('<%#Eval("AreaId") %>',this);return false;"><%#Eval("AreaName") %></a></td>
	         <%#GetItem()%>
	         </ItemTemplate>
	        <FooterTemplate><%#GetFooterItem()%></FooterTemplate>
	        </asp:Repeater>
       </table>
         </td>
            <td width="3%"><img src="<%=ImageServerUrl%>/images/icojt.gif" width="16" height="75" /></td>
            <td width="75%" valign="top">
	  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td width="2%" height="25" align="left" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#DAE7F6">&nbsp;</td>
          <td width="20%" align="center" valign="bottom" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#DAE7F6"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
              <tr>
                <td height="22" align="center" style="border:1px solid #81C6FF; border-bottom:0px;">&nbsp;<img src="<%=ImageServerUrl%>/images/icobu.gif" width="16" height="16" style="margin-bottom:-3px;"/> <strong id="ds_strong">线路区域</strong></td>
              </tr>
          </table></td>
          <td align="left" valign="bottom" background="<%=ImageServerUrl%>/images/szxlqybj.gif" bgcolor="#DAE7F6">&nbsp;勾选“√选定”</td>
        </tr>
      </table>
    <div id="ds_lineCompanyList">
      
    </div>
	
			</td>
          </tr>
        </table></td>
      </tr></table>

</asp:Content>
