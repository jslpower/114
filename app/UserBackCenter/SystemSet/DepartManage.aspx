<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartManage.aspx.cs" Inherits="UserBackCenter.SystemSet.DepartManage" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<asp:Content id="SystemIndex" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
var DepartManage={
      mouseovertr:function(o){
          o.style.backgroundColor="#FFF9E7";
      },
      mouseouttr:function(o){
         o.style.backgroundColor="";
      },
      departAdd:function(tar_a){
          if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         var txtDepartName=$(tar_a).closest("tr").find("[name='dm_txtDepartName']")[0].value.replace(/\s*/g,'');
         if(txtDepartName.length==0)
         {
            alert("部门名称不能为空!"); 
            return false;
         }
         $.newAjax(
         {
           url:"/SystemSet/DepartManage.aspx",
           data:{method:"add",departname:txtDepartName},
           dataType:"json",
           cache:false,
           type:"get",
           success:function(result)
           { 
             if(result.success=="1")
             {
                 $(tar_a).css("display","none").closest("td").append("<a href=\"javascript:void(0)\" onclick=\"DepartManage.departEdit(this,'"+result.message+"')\" class=\"dm_Edit\">修改</a><span> | </span><a href=\"javascript:void(0)\" onclick=\"return DepartManage.departDel(this,'"+result.message+"')\" class=\"dm_Del\">删除</a>"+ 
                          "<a href=\"javascript:void(0)\" onclick=\"DepartManage.departSave(this,'"+result.message+"')\" class=\"dm_Save\" style=\"display:none\">保存</a><span style=\"display:none\"> | </span><a href=\"javascript:void(0)\" onclick=\"DepartManage.departCancel(this,'"+result.message+"')\" class=\"dm_Cancel\" style=\"display:none\">取消</a>");
                 $(tar_a).closest("td").prev("td").children("span").append("<label name=\"dm_lblDepartName\">"+txtDepartName+"</label>").find("input").css("display","none");
                 $(tar_a).closest("tr").after("<tr class=\"baidi\" onmouseover=\"DepartManage.mouseovertr(this)\" onmouseout=\"DepartManage.mouseouttr(this)\">"+
                          "<td height=\"25\" align=\"center\"><span style=\"line-height:25px;\"><input name=\"dm_txtDepartName\" type=\"text\" maxlength='10' /></span></td>"+
                          "<td align=\"center\"><strong><input type=\"button\" name=\"departAdd\" value=\"新增部门\" onclick=\"DepartManage.departAdd(this)\" /></strong></td></tr>");
                alert("添加成功!");
             }
             else
             {
               alert(result.message);
             }
            
             
           },
           error:function(){
            alert("操作失败!");
           }
          });
    },
    departDel:function(tar_a,depart_id){
       if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
       if(!confirm("你确定要删除该数据吗?"))
          return false;
       $.newAjax(
       {
           url:"/SystemSet/DepartManage.aspx",
           data:{method:"del",departid:depart_id},
           dataType:"json",
           cache:false,
           type:"get",
           success:function(result){
               if(result.success=="1")
               {
                  $(tar_a).closest("tr").remove();
               }
                alert(result.message);
            },
           error:function(){
            alert("操作失败!");
           }
        });
    },
    departEdit:function(tar_a,depart_id){
      
      $(tar_a).closest("tr").find("input[name='dm_txtDepartName']").css("display","").siblings("label").css("display","none");
      $(tar_a).css("display","none").next("span").css("display","none").siblings(".dm_Del").css("display","none").siblings(".dm_Save,.dm_Cancel").css("display","");
      $(tar_a).siblings(".dm_Save").next("span").css("display","");
      
    },
    departCancel:function(tar_a,depart_id){
      $(tar_a).closest("tr").find("input[name='dm_txtDepartName']").css("display","none").siblings("label").css("display","");
      $(tar_a).css("display","none").prev("span").css("display","none").siblings(".dm_Del,.dm_Edit").css("display","").siblings(".dm_Save").css("display","none");
      $(tar_a).siblings(".dm_Edit").next("span").css("display","");
    },
    departSave:function(tar_a,depart_id){
       if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
      var txtDepartName=$(tar_a).closest("tr").find("[name='dm_txtDepartName']")[0].value.replace(/\s*/g,'');
      if(txtDepartName.length==0)
      {
        alert("部门名称不能为空!");
        return false;
      }
      $.newAjax(
      {
       url:"/SystemSet/DepartManage.aspx",
       data:{method:"save",departname:txtDepartName,departid:depart_id},
       dataType:"json",
       cache:false,
       type:"get",
       success:function(result){
        if(result.success=="1")
        { 
           $(tar_a).closest("tr").find("input[name='dm_txtDepartName']").css("display","none").siblings("label").css("display","").html(txtDepartName);
           $(tar_a).css("display","none").next("span").css("display","none").siblings(".dm_Del,.dm_Edit").css("display","").siblings(".dm_Cancel").css("display","none");
           $(tar_a).siblings(".dm_Edit").next("span").css("display","");
        }
        alert(result.message);
       },
       error:function(){
         alert("操作失败!");
       }
      });
    }
    
}
</script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top">
        <%--<uc1:sznb id="sznb1" runat="server" TabIndex="tab4"></uc1:sznb>--%>
         <table width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
<%--                <tr>
                  <td width="7%">&nbsp;</td>
                  <td width="93%" align="left"><img src="<%=ImageServerUrl%>/images/bumen.gif" width="93" height="33" /></td>
                </tr>--%>
              </table>
			    <table width="98%"  border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#B0C7D5">
			      <asp:Repeater id="dm_rpt_DepartList" runat="server">
                   <HeaderTemplate>
                    <tr class="white" height="23">
                      <td width="52%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>部门名称</strong></td>
                      <td width="26%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>操作</strong></td>
                    </tr>
                </HeaderTemplate>
               <ItemTemplate>
               <tr bgcolor="#F3F7FF" onmouseout="DepartManage.mouseouttr(this)" onmouseover="DepartManage.mouseovertr(this)" style="">
                 
                  <td height="25" align="center"><span style="line-height: 25px;">
                    <input type="text" name="dm_txtDepartName" value='<%# Eval("DepartName") %>' style="display: none;" maxlength='10'>
                  <label name="dm_lblDepartName"><%# Eval("DepartName") %></label></span></td>
                  <td align="center"><strong>
                    <input type="button" onclick="DepartManage.departAdd(this)" value="新增部门" name="departAdd" style="display: none;">
                  </strong><a class="dm_Edit" onclick="DepartManage.departEdit(this,'<%# Eval("ID") %>')" href="javascript:void(0)">修改</a><span> | </span><a class="dm_Del" onclick="return DepartManage.departDel(this,'<%# Eval("ID") %>')" href="javascript:void(0)">删除</a><a style="display: none;" class="dm_Save" onclick="DepartManage.departSave(this,'<%# Eval("ID") %>')" href="javascript:void(0)">保存</a><span style="display: none;"> | </span><a style="display: none;" class="dm_Cancel" onclick="DepartManage.departCancel(this,2001)" href="javascript:void(0)">取消</a></td>
                </tr>
               </ItemTemplate>
                  <AlternatingItemTemplate>
                   <tr class="baidi"  onmouseout="DepartManage.mouseouttr(this)" onmouseover="DepartManage.mouseovertr(this)" style="">
                 
                  <td height="25" align="center"><span style="line-height: 25px;">
                    <input type="text" name="dm_txtDepartName" value='<%# Eval("DepartName") %>' style="display: none;" maxlength='10'>
                  <label name="dm_lblDepartName"><%# Eval("DepartName") %></label></span></td>
                  <td align="center"><strong>
                    <input type="button" onclick="DepartManage.departAdd(this)" value="新增部门" name="departAdd" style="display: none;">
                  </strong><a class="dm_Edit" onclick="DepartManage.departEdit(this,'<%# Eval("ID") %>')" href="javascript:void(0)">修改</a><span> | </span><a class="dm_Del" onclick="return DepartManage.departDel(this,'<%# Eval("ID") %>')" href="javascript:void(0)">删除</a><a style="display: none;" class="dm_Save" onclick="DepartManage.departSave(this,'<%# Eval("ID") %>')" href="javascript:void(0)">保存</a><span style="display: none;"> | </span><a style="display: none;" class="dm_Cancel" onclick="DepartManage.departCancel(this,2001)" href="javascript:void(0)">取消</a></td>
                </tr>
                  </AlternatingItemTemplate>
                
                <FooterTemplate>
               <tr bgcolor="#F3F7FF" onmouseover="DepartManage.mouseovertr(this)" onmouseout="DepartManage.mouseouttr(this)">
                  <td height="25" align="center"><span style="line-height:25px;">
                    <input name="dm_txtDepartName" type="text" maxlength='10' />
                  </span></td>
                  <td align="center"><strong>
                    <input type="button" name="departAdd" value="新增部门" onclick="DepartManage.departAdd(this)" />
                  </strong></td>
                </tr>
                </FooterTemplate></asp:Repeater>
              </table></td>
            </tr>
          </table></td>
      </tr>
    </table>

</asp:Content>