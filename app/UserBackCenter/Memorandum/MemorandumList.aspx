<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemorandumList.aspx.cs" Inherits="UserBackCenter.Tools.Memorandum.MemorandumList"  MasterPageFile="~/MasterPage/Site1.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="ctnaddMem" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<table height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
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
        
         <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#D0E0F0" style="margin-top:15px;">
    <tr>
      <td width="7%" align="center" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">序号</td>
      <td width="10%" align="center" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">状态</td>
      <td width="29%" align="center" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">事件标题</td>
      <td width="38%" align="center" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">事件正文</td>
      <td width="16%" align="center" background="<%=ImageServerUrl %>/images/gongneng_bg.gif">操作</td>
    </tr>
    <tr>
    <td colspan="5">
       <cc1:CustomRepeater ID="crptMemList" runat="server">
        <ItemTemplate>
            <tr>
              <td align="center"><%#++sort%></td>
              <td align="center"><%#GetMemState(Eval("MemoState").ToString())%></td>
              <td align="left"><a href="javascript:void(0)" onclick="return MemorandumList.tabChange('/Memorandum/AddMemorandum.aspx?UpPage=1&MemId=<%#Eval("ID") %>')"><%#Utils.GetText(Eval("MemoTitle").ToString(), 20)%></a></td>
              <td align="center"><%#Utils.GetText(Eval("MemoText").ToString(), 30)%></td>
              <td align="center"><a href="javascript:void(0)" onclick="return MemorandumList.tabChange('/Memorandum/AddMemorandum.aspx?UpPage=1&MemId=<%#Eval("ID") %>')">修改</a> | 
               <a href="javascript:void(0)" onclick="return MemorandumList.DeleteMem('<%#Eval("ID") %>')">删除</a></td>
            </tr>        
        </ItemTemplate>
      </cc1:CustomRepeater>
      
        <asp:hiddenfield runat="server" id="hdfDateTime"></asp:hiddenfield>
    </td>
    </tr>
</table>
            </td>
          </tr></table>             
      <script type="text/javascript">
      //页面跳转
      MemorandumList=
      {
           tabChange:function(page){
                   topTab.url(topTab.activeTabIndex,page);
                   return false;
            },
           DeleteMem:function (memId)
            {
                if(memId!="")
                {
                     if(!confirm("你确定要删除该条数据吗？"))
                     {
                       return;
                     }
                    $.newAjax
                    ({  
                        url:"/Memorandum/MemorandumList.aspx?Action=Delete&MemId="+memId,
                        cache:false,
                        success:function(msg)
                        {
                            if(msg==1)
                            {
                                alert("操作成功！");
                                 MemorandumList.tabChange("/Memorandum/MemorandumList.aspx?FromDate="+$("#<%=hdfDateTime.ClientID%>").val());
                            }else{
                                alert("操作失败！");
                            }
                        },            
                        error:function()
                         {
                            alert("操作失败");
                         }
                    },true);
                }
                return false;
            }
    }
   
   </script>
</asp:Content>
