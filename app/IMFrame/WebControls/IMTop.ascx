<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IMTop.ascx.cs" Inherits="IMFrame.WebControls.IMTop" %>
<style>  
    a.topqie1
    {
        display: block;
        background: url(<%=ImageServerUrl %>/IM/images/qiebjh.gif);
        height: 19px;
        width: 100px;
        color: #6C1E03;
    }</style>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="21" background="<%=ImageServerUrl %>/IM/images/topqie2.gif">
            <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <a href="/RouteAgency/Main.aspx" style="width:103px" runat="server" id="a_zx" class="topqie1">
                            <img src="<%=ImageServerUrl %>/IM/images/que.gif" />切换至专线商端</a>
                    </td>
                    <td align="center">
                        <a href="/TourAgency/TourManger/TourAreaList.aspx" style="width:107px;" runat="server" id="a_zt" class="topqie1">
                            <img src="<%=ImageServerUrl %>/IM/images/que.gif" />切换至组团社端</a>
                    </td>
                    <td width="119" align="right">
                        <img src="<%=ImageServerUrl %>/IM/images/icolt.gif" width="9" height="9" />
                        <a href="<%=DefaultUrl %>" target="_blank">进入我的后台</a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
