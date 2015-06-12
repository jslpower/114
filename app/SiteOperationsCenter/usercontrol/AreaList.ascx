<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AreaList.ascx.cs" Inherits="SiteOperationsCenter.usercontrol.AreaList" %>
<table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
    <tr>
        <td colspan="4" align="left" bgcolor="#E8F2F7">
           <div id="DivSelectArea" runat="server"  class="eali"></div>
        </td>
    </tr>
</table>
<table width="95%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C8E0EB">
    <tr>
        <td>
            <asp:DataList ID="dalLongList" runat="server" RepeatColumns="6" BorderStyle="None"
                GridLines="Horizontal" >
                <ItemTemplate>
                    <table width="100%" id="dalLongList" border="0" cellspacing="0" cellpadding="3">
                        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                            <td width="8%">
                            <input type="checkbox" id="cblistID" name="cbkName" value='<%#DataBinder.Eval(Container.DataItem,"AreaID") %>|<%# DataBinder.Eval(Container.DataItem, "AreaName")%>' />
                              
                             </td>
                            <td width="92%" height="22">
                                <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
            <asp:DataList ID="dalShortList" runat="server" RepeatColumns="6" BorderStyle="None"
                GridLines="Horizontal">
                <ItemTemplate>
                    <table id="dalShortList" width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                            <td width="8%">
                            <input type="checkbox" id="cbShortlistID" name="cbkShortName" value='<%#DataBinder.Eval(Container.DataItem,"AreaID") %>|<%# DataBinder.Eval(Container.DataItem, "AreaName")%>' />
                              
                             </td>
                            <td width="92%" height="22">
                                <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
