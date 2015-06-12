<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermitManage.aspx.cs" Inherits="UserBackCenter.SystemSet.PermitManage" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<asp:Content id="SystemIndex" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
var PermitManage={
    //行背景色切换
    mouseovertr:function(o){
         o.style.backgroundColor="#FFF9E7";
    },
    mouseouttr:function(o){
         o.style.backgroundColor="";
    },
    //弹出窗体
    OpenDialog:function(title,url,width,height){
         var height1=GetAddOrderHeight();
         Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height1,draggable:true,data:null});
    },
    //添加角色权限
    add:function(title,url,width,height){
        if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
        this.OpenDialog(title,url,width,height);
        return false;
    },
    refresh:function(){
           topTab.url(topTab.activeTabIndex,"/SystemSet/PermitManage.aspx");
      },
    //修改
    update:function(title,url,width,height,ischk,tar_a){
        if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
        if(ischk)
        {
            var checks= $("#pm_roleList").find(":checkbox:checked").not("[id*='all']")
            if(checks.length==0)
            {
                alert("请选择要修改的数据!");
                return false;
            }
            if(checks.length>1)
            {
                alert("修改数据只能选择1条!");
                return false;
            }
            if (checks.closest("tr").find(".pm_roleName").attr("isadmin") == "True")
            {
               alert("管理员角色不能修改!");
               return false;
            }
            this.OpenDialog("修改权限",url+"?roleid="+checks.get(0).value,width,height);
        }
        else
        {
            if ($(tar_a).closest("tr").find(".pm_roleName").attr("isadmin") == "True")
            {
               alert("管理员角色不能修改!");
               return false;
            }
            this.OpenDialog("修改权限",url,width,height);
        }
        return false;
    },
    //删除
    roleDel:function(roleid,tar_a,isAll){
        if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         if(isAll=="notall")
         {
             if ($(tar_a).closest("tr").find(".pm_roleName").attr("isadmin") == "True")
           {
              alert("管理员角色不能删除!");
              return false;
           }
         }
        
         if(confirm("你确定要删除该数据吗?"))
         { 
            $.newAjax(
              {
               url:"/SystemSet/PermitManage.aspx",
               data:{method:"del",roleids:roleid},
               dataType:"json",
               cache:false,
               type:"post",
               success:function(result){
                  if(result.success=='1')
                   {  
                      if(/0/.test(result.message))
                      { 
                  
                        $("#pm_rpt_RoleList1").css("display","none");
                        $("#<%=pm_noData.ClientID %>").css("display","");
                      }
                      else
                      {
                       $(tar_a).closest("tr").remove();
                      }
                     alert("操作完成!");
                   }
                   else
                   {
                      alert(result.message);
                   }
                   
                   
               },
               error:function(){
                   alert("操作失败!");
               }
             })
           }
           return false;
        },
     //全选
    checkall:function(check_all){
         $("#pm_roleList").find(":checkbox").attr("checked",$(check_all).attr("checked"))
    },
    //按选中删
    delAll:function(){
       if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
       var checks= $("#pm_roleList").find(":checkbox:checked").not("[id*='all']")
       if(checks.length==0)
       {
        alert("请选择要删除的数据!");
        return false;
       }
       var ids="";
       var isDelete=true;
       checks.each(function(){
           if($(this).closest("tr").find(".pm_roleName").attr("isadmin")=="True")
           {
             alert("管理员角色不能删除!");
            isDelete=false;
           }
           
           ids+=$(this).val()+",";
       });
       if(!isDelete)
           {
             return false;
           }
      return this.roleDel(ids.substring(0,ids.length-1),checks,"all");
    }
    
}
</script>
	<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr><td valign="top" >
	  
	  <%--<uc1:sznb id="sznb1" runat="server" TabIndex="tab6"></uc1:sznb>--%>
	  <table id="pm_roleList" width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" >
<%--                  <tr>
                    <td width="4%">&nbsp;</td>
                    <td width="96%" align="left"><img src="<%=ImageServerUrl%>/images/quanxian.gif" width="93" height="33" /></td>
                  </tr>--%>
                </table>
				<span id="pm_list" runat="server">
				<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" >
                  <tr>
                    <td class="toolbj">
					<a   href="javascript:void(0)" onclick="return PermitManage.add('新增角色','/SystemSet/RoleSet.aspx','880px','400px')" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/add.gif" />新增</a>
					<a  href="javascript:void(0)" onclick="return PermitManage.update('修改角色','/SystemSet/RoleSet.aspx','880px','400px',true,this)" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/modified.gif" />修改</a>
					<a href="javascript:void(0);" class="menubarleft" onclick="return PermitManage.delAll();"><img src="<%=ImageServerUrl%>/images/tool/dele.gif" />删除</a> </td>
                  </tr>
                </table>
                
                  <asp:CustomRepeater id="pm_rpt_RoleList" runat="server">
                   <HeaderTemplate>
                   <table width="98%"  border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#B0C7D5" id="pm_rpt_RoleList1">
                   <tr class="white" height="23">
                    <td width="22%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>
                      <input type="checkbox" name="pm_checkall" id="pm_checkall" value="checkbox" onclick="PermitManage.checkall(this)" />
                      序号</strong></td>
                    <td width="52%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>角色名称</strong></td>
                    <td width="26%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>操作</strong></td>
                  </tr>
                  </HeaderTemplate>
                  <ItemTemplate>
                  <tr class="baidi" onmouseover="PermitManage.mouseovertr(this)" onmouseout="PermitManage.mouseouttr(this)">
                    <td height="25" align="center"><input type="checkbox" name="checkbox12" value='<%#Eval("ID") %>' />
                      <%=sortId++%></td>
                    <td height="25" align="center" class="pm_roleName" isadmin='<%# Eval("IsAdminRole") %>'><%#Eval("RoleName") %></td>
                    <td align="center"><a href="javascript:void(0);" onclick="return PermitManage.update('修改角色','/SystemSet/RoleSet.aspx?roleid=<%#Eval("ID") %>','880px','400px',false,this)">修改</a> | <a href="javascript:void(0)" onclick="return PermitManage.roleDel('<%#Eval("ID") %>',this,'notall')">删除</a></td>
                  </tr>
                  </ItemTemplate>
                 
                 <FooterTemplate>
                  <tr class="white" height="23">
                    <td width="22%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>

                      <input type="checkbox" name="pm_checkall2" id="pm_checkall2" value="checkbox" onclick="PermitManage.checkall(this)" />
                      序号</strong></td>
                    <td width="52%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>角色名称</strong></td>
                    <td width="26%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif">&nbsp;</td>
                  </tr>
                  </table>
                  </FooterTemplate>
                  </asp:CustomRepeater>
                <div id="pm_noData" style="text-align:center; display:none" runat="server">暂无角色信息!</div>
                </span>
              </td>
            </tr>
          </table></td>
      </tr></table>

</asp:Content>
