<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxFriendLinkManage.aspx.cs" Inherits="UserBackCenter.EShop.AjaxFriendLinkManage" %>

<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
    <tr>
        <td width="8%" class="hang">
            排序号
        </td>
        <td width="30%" class="hang">
            名称
        </td>
        <td width="46%" class="hang">
            链接地址
        </td>
        <td width="16%" class="hang">
            操作
        </td>
    </tr>
    <asp:Repeater id="rpt_FriendLinkManage" runat="server">
            <ItemTemplate>
                <tr>
                    <td  align="center">
                        <span class="right1">
                            <input name="linkSort" class="linkSort" type="text" value="<%#Eval("SortId") %>"
                                size="3" />
                        </span>
                    </td>
                    <td   align="center">
                        <!--名称-->
                        <input name="LinkName" value="<%#Eval("LinkName") %>" style="width:160px;" type="text" />
                    </td>
                    <td  align="center">
                        <!--链接地址-->
                        <input name="LinkAddress" value="<%#Eval("LinkAddress") %>" style="width:290px;"  type="text" />                        
                    </td>
                    <td align="center">
                        <!--操作-->
                        <a href="#" class="a_updatelink" linkId="<%#Eval("id") %>">修改</a>
                        <a href="#" class="a_dellink" linkId="<%#Eval("id") %>">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    <tr>
        <td align="center">
            <input name="txtSortId" id="txtSortId" type="text" value="<%= StrIndex %>" size="3" />
        </td>
        <td align="center">
            <input name="txtLinkName" id="txtLinkName" style="width:95%;" value=""  type="text" size="35" />
        </td>
        <td align="center">
            <input name="txtLinkAddress" id="txtLinkAddress" value="http://" style="width:95%;" type="text" size="35" />
        </td>
        <td align="center">
            <a href="#" id="addFriendLink">添加</a>
        </td>
    </tr>
</table>
