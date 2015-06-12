<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SonUserManage.aspx.cs" Inherits="UserBackCenter.SystemSet.SonUserManage" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:Content id="SonUserManage" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
 $(document).ready(function()
 {
     $("#sum_ExportPage").find("a").click(function(){
          topTab.url(topTab.activeTabIndex,$(this).attr("href"));
          return false;
      });
 });
var SonUserManage={
    //行背景色切换
    mouseovertr:function(o){
        o.style.backgroundColor="#FFF9E7";
      },
    mouseouttr:function(o){
        o.style.backgroundColor="";
    },
    //打开弹出框
    OpenDialog:function(title,url,width,height){
           var height1=GetAddOrderHeight();
           Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height1,draggable:true,data:null});
           return false;
        },
    //添加子账户
    add:function(title,url,width,height){
        if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
        this.OpenDialog(title,url,width,height);
        return false;
    },
     //复制数据
     copy:function(){
         if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
        
         var chk=$("#sum_SonUserList").find(":checkbox:checked").not("[id*='all']");
         if(chk.length>1)
         {
             alert("复制数据只能选择一条!");
             return false;
         }
         if(chk.length==0)
         {
             alert("请选择要复制的数据!");
             return false;
         }
        this.OpenDialog("添加子账户","/SystemSet/SonUserSet.aspx?method=copy&sonuserid="+chk.val(),"880px","480px");
        return false;
     },
     //删除数据
     del:function(){
          if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         var chks=$("#sum_SonUserList").find(":checkbox:checked").not("[id*='all']");
         var ids="";
         var isMy=false;
         chks.each(function(){
           ids+=$(this).attr("value")+",";
           if($(this).attr("theUserName")=="<%=userName%>")
           {
              isMy=true;
           }
         });
         if(chks.length==0)
         {
             alert("请选择要删除的数据!");
             return false;
         }
         if(isMy)
         {
            alert("对不起，你无法删除自己的账户!");
            return false;
         }
         if(!confirm("你确定要删除选中的数据吗?"))
             return false;
         
          $.newAjax(
              {
               url:"/SystemSet/SonUserManage.aspx",
               data:{method:"del",sonuserids:ids},
               dataType:"json",
               cache:false,
               type:"post",
               success:function(result){
                   alert(result.message);
                   if(result.success=='1')
                   {  
                    topTab.url(topTab.activeTabIndex,"/SystemSet/SonUserManage.aspx");
                   }
               },
               error:function(){
                   alert("操作失败!");
               }
             })
         
      },
      //刷新页面
      refresh:function(){
           topTab.url(topTab.activeTabIndex,"/SystemSet/SonUserManage.aspx");
           return false;
      },
      //修改数据
     update:function(){
          if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         var chk=$("#sum_SonUserList").find(":checkbox:checked").not("[id*='all']");
         if(chk.length>1)
         {
             alert("修改数据只能选择一条!");
             return false;
         }
         if(chk.length==0)
         {
             alert("请选择要修改的数据!");
             return false;
         }
         if("<%=userName %>"==chk.closest("td").siblings("td[class='sus_UserName']").html())
         {
           alert("对不起，你无法修改自己的账户!");
           return false;
         }
        this.OpenDialog("修改子账户","/SystemSet/SonUserSet.aspx?method=update&sonuserid="+chk.val(),"880px","480px");
        return false;
     },
     //全选
     chkAll:function(tar_chk){
         $("#sum_SonUserList").find(":checkbox").attr("checked",$(tar_chk).attr("checked"));
     },
     //单选
     chkOne:function(){
         if($("#sum_SonUserList").find(":checkbox").not("[id*='all']").not(":checked").length>0)
             $("#sum_SonUserList").find("[id*='all']").attr("checked","");
         else
             $("#sum_SonUserList").find("[id*='all']").attr("checked","checked");
     },
     //启用,开启
     forbid:function(tar_a,id){
         if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         
         if("<%=userName %>"==$(tar_a).closest("td").siblings("td[class='sus_UserName']").html())
         {
           alert("对不起，你无法修改自己的账户!");
           return false;
         }
        var enableName=$(tar_a).html();
        var thenable="";
        if(enableName=="停用")
          thenable="forbid";
        else
          thenable="ubforbid"
        
        $.newAjax(
              {
               url:"/SystemSet/SonUserManage.aspx",
               data:{method:"setforbid",sonuserid:id,enable:thenable},
               dataType:"json",
               cache:false,
               type:"post",
               success:function(result){
                   if(result.success=='1')
                   {  
                      if(enableName=="启用")
                      {
                        $(tar_a).html("停用");
                      }
                      else
                      {
                        $(tar_a).html("启用");
                      }
                   }
                   alert(result.message);
               },
               error:function(){
                   alert("操作失败!");
               }
             })
             return false;
          }
     
}

  
</script>
<div id="showmodel"></div>
	<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr><td valign="top" >
	 <%--<uc1:sznb id="sznb1" runat="server" TabIndex="tab5"></uc1:sznb>--%>
	  <table id="sum_SonUserList" width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:15px;">
                <tr>
                  <td width="17%" align="right"><strong>现已开通<span class="chengse"><%=recordCount%></span>个子帐户</strong></td>
                  <td width="83%" align="left" style=" background:url(<%=ImageServerUrl%>/images/jiange.gif) repeat-x;">&nbsp;</td>
                </tr>
                
              </table>
                  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" >
                  <tr>
                    <td class="toolbj">
					<a  href="javascript:void(0)" onclick="return SonUserManage.add('新增子账户','/SystemSet/SonUserSet.aspx?method=add','880px','480px')" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/add.gif" />新增</a>
					<a  href="javascript:void(0)" onclick="return SonUserManage.update();" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/modified.gif" />修改</a>
					<a href="javascript:void(0);" onclick="return SonUserManage.copy()" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/copy.gif" />复制</a>
					<a href="javascript:void(0);" onclick="return SonUserManage.del()" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/dele.gif" />删除</a> </td>
                  </tr>
                </table>
              <asp:Repeater id="sum_rpt_SonUserList" runat="server">
                   <HeaderTemplate>
            
                
                <table width="98%"  border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#B0C7D5">
                  <tr class="white" height="23">
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>
                      <input type="checkbox" name="sum_checkall1" id="sum_checkall1" onclick="SonUserManage.chkAll(this)" value="checkbox" />
                      序号</strong></td>
                    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>用户名</strong></td>
                    <td width="8%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>姓名</strong></td>
                    <td width="11%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>电话</strong></td>
                    <td width="11%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>手机 </strong></td>
                    <%if (isArea)
                      { %>
                    <td width="30%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>产品区域</strong></td><%} %>
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>操作</strong></td>
                    
                  </tr>
                  </HeaderTemplate>
                 
                  <ItemTemplate>
                  <tr class="baidi" onmouseover="SonUserManage.mouseovertr(this)" onmouseout="SonUserManage.mouseouttr(this)">
                    <td height="25" align="center"><input type="checkbox" name="checkbox12" value='<%# Eval("ID") %>' onclick="SonUserManage.chkOne()" theUserName='<%# Eval("UserName") %>' />
                      <%=itemIndex++ %></td>
                    <td height="25" align="center" class="sus_UserName"><%# Eval("UserName")%></td>
                    <td align="center"><%# Eval("ContactInfo.ContactName")%></td>
                    <td align="center"><%# Eval("ContactInfo.Tel")%></td>
                    <td align="center"><%# Eval("ContactInfo.Mobile")%></td>
                    <%if (isArea)
                      {%>
                    <td align="center"><%# GetAreas(Eval("Area")) %></td><%} %>
                    <td align="center"><a href="javascript:void(0)"  onclick="SonUserManage.forbid(this,'<%# Eval("ID") %>')"><%# Convert.ToBoolean(Eval("IsEnable"))?"停用":"启用" %></a> </td>
                   
                  </tr>
                  </ItemTemplate>
                   <AlternatingItemTemplate>
                  <tr bgcolor="#F3F7FF" onmouseover="SonUserManage.mouseovertr(this)" onmouseout="SonUserManage.mouseouttr(this)">
                    <td height="25" align="center"><input type="checkbox" name="checkbox12" value='<%# Eval("ID") %>' onclick="SonUserManage.chkOne()" theUserName='<%# Eval("UserName") %>'/>
                      <%=itemIndex++ %></td>
                    <td height="25" align="center" class="sus_UserName"><%# Eval("UserName")%></td>
                    <td align="center"><%# Eval("ContactInfo.ContactName")%></td>
                    <td align="center"><%# Eval("ContactInfo.Tel")%></td>
                    <td align="center"><%# Eval("ContactInfo.Mobile")%></td>
                    <%if (isArea)
                      { %>
                    <td align="center"><%# GetAreas(Eval("Area")) %></td><%} %>
                    <td align="center"><a href="javascript:void(0)"  onclick="SonUserManage.forbid(this,'<%# Eval("ID") %>')"><%# Convert.ToBoolean(Eval("IsEnable"))?"停用":"启用" %></a> </td>
                    
                  </tr>
                  </AlternatingItemTemplate></asp:Repeater>
                   <div id="sum_noData" style="text-align:center; display:none" runat="server">暂无任何子账户!</div>
                  
                 
                 
                </table>
              <table id="sum_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
      <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="sum_ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
     </table>
              </td>
            </tr>
          </table>
          
          </td>
      </tr></table>

</asp:Content>