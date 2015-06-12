<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMemorList.aspx.cs" Inherits="UserBackCenter.Memorandum.AjaxMemorList" %>
<head id="Head1" runat="server">
</head>
 <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#D0E0F0" style="margin-top:15px;">
    <tr>
      <td width="7%" align="center" background="<%#ImageServerUrl %>/images/gongneng_bg.gif">序号</td>
      <td width="10%" align="center" background="<%#ImageServerUrl %>/images/gongneng_bg.gif">状态</td>
      <td width="29%" align="center" background="<%#ImageServerUrl %>/images/gongneng_bg.gif">事件标题</td>
      <td width="38%" align="center" background="<%#ImageServerUrl %>/images/gongneng_bg.gif">事件正文</td>
      <td width="16%" align="center" background="<%#ImageServerUrl %>/images/gongneng_bg.gif">操作</td>
    </tr>
      <cc1:CustomRepeater ID="crptMemList" runat="server">
        <ItemTemplate>
            <tr>
              <td align="center"><%# (Container.ItemIndex+1)+(CurrentPage-1)*PageSize %></td>
              <td align="center"><%#GetMemState(Eval("MemoState").ToString())%></td>
              <td align="left"><a href="AddMemorandum.aspx?MemId=<%#Eval("ID") %>"><%#Utils.GetText(Eval("MemoTitle").ToString(), 15)%></a></td>
              <td align="center"><%#Utils.GetText(Eval("MemoText").ToString(), 40)%></td>
              <td align="center"><a href="AddMemorandum.aspx?MemId=<%#Eval("ID") %>">修改</a> |
               <a href="javascript:void()" id="DeleMem" onclick="DeleteMem('<%# Eval("ID")%>')"></a>删除</td>
            </tr>        
        </ItemTemplate>
      </cc1:CustomRepeater>
      <tr>
        <td colspan="5">
            <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" LinkType="4"
/>
        </td>
      </tr>
</table>
