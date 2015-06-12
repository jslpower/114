<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMemorandum.aspx.cs" Inherits="UserBackCenter.Tools.Memorandum.AddMemorandum" MasterPageFile="~/MasterPage/Site1.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="ctnaddMem" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<table height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth" id="<%=tbMemId %>">
      <tr>
        <td valign="top" ><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="2" align="left"><img src="<%=ImageServerUrl %>/images/topjs.gif" width="490" height="55" /></td>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td width="19" align="left" background="<%=ImageServerUrl %>/images/skybar2.gif"><img src="<%=ImageServerUrl %>/images/ubar1.gif" width="9" height="27" /></td>
            <td width="471" align="left" background="<%=ImageServerUrl %>/images/skybar2.gif">当前位置 记事本</td>
            <td width="328" align="right" background="<%=ImageServerUrl %>/images/skybar2.gif">有了备忘录，工作生活真轻松！</td>
            <td width="37" align="right" background="<%=ImageServerUrl %>/images/skybar2.gif"><img src="<%=ImageServerUrl %>/images/skybar3.gif" width="9" height="27" /></td>
          </tr>
        </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td width="21%" align="right" class="tdleft"><span class="ff0000"><strong>*</strong></span>时间：</td>
            <td width="79%" align="left" class="td2left">
               <input type="text" onfocus="WdatePicker()" valid="required" errmsg="请填写时间" id="DatePicker1" name="DatePicker1"  runat="server" class="style2" onchange="AddMemorandum.DateControlChange(this)"/>
                 <span id="errMsg_ctl00_ContentPlaceHolder1_DatePicker1" class="errmsg"></span>
            </td>
            </tr>
          <tr>
            <td align="right" class="tdleft"><span class="ff0000"><strong>*</strong></span>事件紧急程度：</td>
            <td align="left" class="td2left">
                <asp:DropDownList ID="ddlUrgentType" runat="server" valid="required" errmsg="请选择事件紧急程度">
                </asp:DropDownList>
                  <span id="errMsg_ctl00_ContentPlaceHolder1_ddlUrgentType" class="errmsg"></span>                  
              </td>
          </tr>
          <tr>
            <td align="right" class="tdleft"><span class="ff0000"><strong>*</strong></span>完成状态：</td>
            <td align="left" class="td2left">
                <asp:DropDownList ID="ddlMemState" runat="server" valid="required" errmsg="请选择完成状态">
                </asp:DropDownList>
                 <span id="errMsg_ctl00_ContentPlaceHolder1_ddlMemState" class="errmsg"></span>           
               </td>
          </tr></asp:panel>
          <tr>
            <td align="right" class="tdleft"><span class="ff0000"><strong>*</strong></span>事件标题：</td>
            <td align="left" class="td2left"><input name="txtTitle" runat="server" id="txtTitle" type="text" size="50"  valid="required|limit" max="50" errmsg="请填写事件标题|不能大于50个字"/><span class="errmsg" >(字数在1-50个之内)</span>
                <span id="errMsg_ctl00_ContentPlaceHolder1_txtTitle" class="errmsg"></span>
            </td>
            
          </tr>
          <tr>
            <td align="right" class="tdleft">事件详细：</td>
            <td align="left" class="td2left"><textarea name="txtDetialInfo" id="txtDetialInfo" valid="limit" max="200" errmsg="不能大于200个字" runat="server" style="width: 471px; height: 128px"></textarea><span class="errmsg">(不能大于200个字)</span>
            <span id="errMsg_ctl00_ContentPlaceHolder1_txtDetialInfo" class="errmsg"></span>     
                    </td>
          </tr>
          <tr>
            <td align="right" class="tdleft">&nbsp;</td>
            <td align="left" class="td2left"><input type="button" value="保存" id="btnSave" onclick="return AddMemorandum.save(this)" />
            </td>
          </tr>
        </table>
            <asp:HiddenField ID="hdfMemID" runat="server" />
            <asp:HiddenField ID="hdeUppage" runat="server" />
        </td>
      </tr></table>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
   AddMemorandum=
   {
    save:function(obj)
    {
        var form = $("#<%=tbMemId %>").closest("form").get(0);        
	    if(ValiDatorForm.validator(form,"span"))
	    {  
	       $.newAjax(
	       { 
	         url:"/Memorandum/AddMemorandum.aspx",
	         data:$(form).serialize().replace(/&Input=/,'')+"&method=save",
             cache:false,
             type:"post",
             success:function(result){ 
                alert(result);
                var uppage="";
                if(result=="操作成功!")
                {
                    if($("#<%= hdeUppage.ClientID%>").val()=="1")
                    { 
                        uppage="/Memorandum/MemorandumList.aspx?FromDate="+$("#ctl00_ContentPlaceHolder1_DatePicker1").val();
                    }else{
                       
                        uppage="/Memorandum/MemorandumCalendar.aspx";
                    }
                       topTab.url(topTab.activeTabIndex,uppage);
                }
             },
             error:function(){ 
               alert("操作失败!");
             }
	       },true);
	     }
	    return false;
    },
    DateControlChange:function(obj)
    {
       if($(obj).val()!="")
       {
        $("#errMsg_ctl00_ContentPlaceHolder1_DatePicker1").html("");
       }
    }
}
    $(document).ready(function()
    {
        FV_onBlur.initValid( $("#<%=tbMemId %>").closest("form").get(0));
         $("#DatePicker1").blur(function(){
       
        });
    });
    </script>
</asp:Content>
