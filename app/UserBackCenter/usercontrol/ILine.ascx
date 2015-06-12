<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ILine.ascx.cs" Inherits="UserBackCenter.usercontrol.ILine" %>
<%if (IsTongYe)
  {%>
<table id="<%=Key %>tab_IsTongYe" border="0" align="center" cellpadding="0" cellspacing="0"
    style="width: 100%;">
    <tr>
        <td align="left" colspan="3">
        </td>
    </tr>
    <tr>
        <td width="10%" align="left" class="zt_line_font">
            <%=Title %>
        </td>
        <td width="3%" align="center" valign="middle">
            <img src="<%=ImageServerPath %>/images/icon_04.gif" width="11" height="13" />
        </td>
        <td width="85%" align="left">
            <div class="scroll_zt">
                <ul class="scroll_zt_line">
                    <asp:Repeater ID="RepList" runat="server">
                        <ItemTemplate>
                            <li><a class="a_goInfoShow" href="/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>">
                                <%#Eval("Title") != null?EyouSoft.Common.Utils.GetText2(Eval("Title").ToString(),35,true):""%></a><%#Eval("IssueTime")!=null?((DateTime)Eval("IssueTime")).ToString("MM-dd"):""%>
                                <%#Eval("AreaName") != null && Eval("AreaName").ToString().Length > 0 ? "[" + Eval("AreaName") + "]" : ""%>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </td>
    </tr>
</table>
<table id="<%=Key %>_tab_ILine" border="0" align="center" cellpadding="0" cellspacing="0"
    style="width: 100%;" class="toolbj1">
    <tr>
        <td align="left">
            <asp:Repeater ID="rpt_line2" runat="server">
                <ItemTemplate>
                    <a href="javascript:void(0);" class="sel" lineid="<%#Eval("AreaId") %>" linetype="<%#(int)Eval("RouteType") %>">
                        <%#Eval("AreaName") != null ? EyouSoft.Common.Utils.GetText2(Eval("AreaName").ToString(), 6, false) : Eval("AreaName")%></a>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
<%}
  else
  { %>
<table id="<%=Key %>_tab_ILine" border="0" align="center" cellpadding="0" cellspacing="0"
    style="width: 100%;" class="toolbj1">
    <tr>
        <td align="left" class="title">
            <%=Title %>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Repeater ID="rpt_line1" runat="server">
                <ItemTemplate>
                    <a href="javascript:void(0);" class="sel" lineid="<%#Eval("AreaId") %>" linetype="<%#(int)Eval("RouteType") %>">
                        <%#Eval("AreaName") != null ? EyouSoft.Common.Utils.GetText2(Eval("AreaName").ToString(), 6, false) : Eval("AreaName")%></a>
                </ItemTemplate>
            </asp:Repeater>
            <%--<a href="javascript:void(0);" class="sel" lineid="1" linetype="美加专线">美加专线</a>--%>
        </td>
    </tr>
</table>
<%} %>

<script type="text/javascript">

    $(function() {
        $("#<%=Key %>_tab_ILine .sel[lineid='<%=CheckedId %>']").addClass("select");
        $("#<%=Key %>_tab_ILine .sel").click(function() {
            $("#<%=Key %>" + "_tab_ILine").find(".select").removeClass("select");
            $(this).addClass("select");
            eval('<%=SelectFunctionName %>');
            return false;
        })
        if ($('#<%=Key %>tab_IsTongYe ul.scroll_zt_line').length > 0) {
            var _wrap = $('#<%=Key %>tab_IsTongYe ul.scroll_zt_line');
            var _interval = 2000;
            var _moving;
            _wrap.hover(function() {
                clearInterval(_moving);
            }, function() {
                _moving = setInterval(function() {
                    var _field = _wrap.find('li:first');
                    var _h = _field.height();
                    _field.animate({ marginTop: -_h + 'px' }, 600, function() {
                        _field.css('marginTop', 0).appendTo(_wrap);
                    })
                }, _interval)
            }).trigger('mouseleave');
        }
        if ($("#<%=Key %> .a_goInfoShow").length > 0) {
            $("#<%=Key %> .a_goInfoShow").click(function() {
                topTab.open($(this).attr("href"), "同业资讯", {})
                return false;
            })
        }
    })
</script>

