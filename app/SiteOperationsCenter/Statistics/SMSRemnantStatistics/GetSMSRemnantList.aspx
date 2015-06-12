<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetSMSRemnantList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.SMSRemnantStatistics.GetSMSRemnantList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>
<cc1:CustomRepeater ID="rep_SmsRemnatList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr  class="white" height="23">
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="6%" height="23" align="center" valign="middle">
                    <strong>序号</strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="10%" align="center" valign="middle">
                    <strong>地区</strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="25%" align="center" valign="middle">
                    <strong>单位名称 </strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="8%" align="center">
                    <strong>联系人 </strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="9%">
                    <strong>电话 </strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="10%">
                    <strong>手机 </strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="5%">
                    <strong>QQ</strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="8%">
                    <strong>MQ </strong>
                </td>
                <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="11%" align="center">
                    <strong>余额</strong>
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <%# GetCount() %>
            </td>
            <td align="center">
                <%# GetCityName(Convert.ToInt32(Eval("PrivinceId").ToString()), Convert.ToInt32(Eval("CityId").ToString()))%>
            </td>
            <td height="25" align="center">
                <%# Eval("CompanyName") %>
            </td>
            <td align="center">
                <%# Eval("ContactName")%>
            </td>
            <td align="center">
                <%# Eval("Tel") %>
            </td>
            <td align="center">
                <%# Eval("Mobile")%>
            </td>
            <td align="center">
            <%#  EyouSoft.Common.Utils.GetQQ(Eval("QQ").ToString())%>
            </td>
            <td align="center">
                <%# EyouSoft.Common.Utils.GetMQ(Eval("MQId").ToString())%>
            </td>
            <td align="center">
                <%# Convert.ToDecimal(Eval("AccountMoney")).ToString("F3")%>元
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <%# GetCount() %>
            </td>
            <td align="center">
                <%# GetCityName(Convert.ToInt32(Eval("PrivinceId").ToString()), Convert.ToInt32(Eval("CityId").ToString()))%>
            </td>
            <td height="25" align="center">
                <%# Eval("CompanyName") %>
            </td>
            <td align="center">
                <%# Eval("ContactName")%>
            </td>
            <td align="center">
                <%# Eval("Tel") %>
            </td>
            <td align="center">
                <%# Eval("Mobile")%>
            </td>
            <td align="center">
               <%#  EyouSoft.Common.Utils.GetQQ(Eval("QQ").ToString())%>
            </td>
            <td align="center">
                <%# EyouSoft.Common.Utils.GetMQ(Eval("MQId").ToString())%>
            </td>
            <td align="center">
                <%# Convert.ToDecimal(Eval("AccountMoney")).ToString("F3")%>元
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        <tr class="white" height="23">
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="6%" height="23" align="center" valign="middle">
                <strong>序号</strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="10%" align="center" valign="middle">
                <strong>地区</strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="25%" align="center" valign="middle">
                <strong>单位名称 </strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="8%" align="center">
                <strong>联系人 </strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="9%">
                <strong>电话 </strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="10%">
                <strong>手机 </strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="5%">
                <strong>QQ</strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="8%">
                <strong>MQ </strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" width="11%" align="center">
                <strong>余额</strong>
            </td>
        </tr>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
<table width="98%" id="tbExporPage" runat="server">
    <tr>
        <td align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>
