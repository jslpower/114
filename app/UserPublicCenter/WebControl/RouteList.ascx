<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RouteList.ascx.cs" Inherits="UserPublicCenter.WebControl.RouteList" %>


<% if(IsNew) { %>
<ul>
    <li>
        <div class="leftt">国内长线</div>
        <div class="rightc">
            <%=strRouteList1 %>
        </div>
        <div class="hr-10"></div>
    </li>
    <li>
         <div class="leftt leftt1">国际线</div>
         <div class="rightc">
            <%=strRouteList2%>
         </div>
          <div class="hr-10"></div>
      </li>
      <li>
         <div class="leftt leftt2">周边游</div>
         <div class="rightc">
            <%=strRouteList3 %>
         </div>
      </li>
</ul>
<% }else{%>

<table bgcolor="#FFFFFF" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="3%"valign="top" style="padding-top:8px;">
                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/gncx.gif" width="24" height="77" />
                    </td>
                    <td width="97%" valign="top">
                        <div class="linestye1">
                            <%=strRouteList1 %>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="3%" valign="top" style="padding-top:10px;">
                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/gjx.gif" width="24" height="57" />
                    </td>
                    <td width="97%" valign="top">
                        <div class="linestye1">
                           <%=strRouteList2%>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="3%" valign="top" style="padding-top:10px;">
                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/zby.gif" width="24" height="57" />
                    </td>
                    <td width="97%" valign="top">
                        <div class="linestye1">
                          <%=strRouteList3 %>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<% } %>