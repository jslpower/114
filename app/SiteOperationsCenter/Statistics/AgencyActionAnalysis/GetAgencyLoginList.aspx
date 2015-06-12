<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetAgencyLoginList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.GetAgencyLoginList" %>


<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>

<form id="form1" runat="server">
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>
                        序号</strong>
                </td>
                <td width="46%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>零售商名称</strong>
                </td>
                <td width="24%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <%=GetTableHead(1)%>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <%=GetTableHead(2)%>
                </td>
            </tr>
<cc1:CustomRepeater ID="rep_CompanyLoginList" runat="server">

    <HeaderTemplate>
        
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center" valign="middle">
                <%# GetCount()%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='LoginList.CompanyInfo("<%#Eval("CompanyId") %>");'><%# Eval("CompanyName")%></a>
            </td>
            <td align="center" valign="middle">
                <a href="javascript:void(0)" onclick='LoginList.CompanyAccessDetail("<%#Eval("CompanyId") %>");'>访问轨迹[<%#Eval("VisitNum")%>]</a>
            </td>
            <td width="20%" align="center" valign="middle">
                <a href="javascript:void(0)" onclick='LoginList.LoginDetail("<%#Eval("CompanyId") %>");'><%# Eval("LoginNum")%>次</a>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center" valign="middle">
                <%# GetCount()%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='LoginList.CompanyInfo("<%#Eval("CompanyId") %>");'><%# Eval("CompanyName")%></a>
            </td>
            <td align="center" valign="middle">
                <a href="javascript:void(0)" onclick='LoginList.CompanyAccessDetail("<%#Eval("CompanyId") %>");'>访问轨迹[<%#Eval("VisitNum")%>]</a>
            </td>
            <td width="20%" align="center" valign="middle">
                <a href="javascript:void(0)" onclick='LoginList.LoginDetail("<%#Eval("CompanyId") %>");'><%# Eval("LoginNum")%>次</a>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
       
    </FooterTemplate>
</cc1:CustomRepeater>
<tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>
                        序号</strong>
                </td>
                <td width="46%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>零售商名称</strong>
                </td>
                <td width="24%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>查看对象</strong>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>登录次数</strong>
                </td>
            </tr>
        </table>
<table width="100%" id="tbExporPage" runat="server" visible="false">
    <tr>
        <td align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    var LoginList = {
        OrderParms: {"CompanyId":0,"BeginTime": "<%=Request.QueryString["StartDate"] %>", "EndTime": "<%=Request.QueryString["EndDate"] %>"},
        openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
            Boxy.iframeDialog({ fixed:false,title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
        },
        CompanyInfo:function(CompanyId){
            this.openDialog("/Statistics/CompanyInfo.aspx", "公司信息", "600", "400", "CompanyId="+CompanyId);
            return false;
        },
        CompanyAccessDetail: function(CompanyId) {
            this.OrderParms.CompanyId=CompanyId;
            LoginList.openDialog("/Statistics/AgencyActionAnalysis/VisitLocaList.aspx", "访问轨迹", "600", "400", $.param(LoginList.OrderParms));
            return false;
        },
        LoginDetail: function(CompanyId) { 
            this.OrderParms.CompanyId=CompanyId;
            this.openDialog("/Statistics/LoginRecord.aspx", "登录信息", "600", "450", $.param(LoginList.OrderParms));
            return false;
        }
    };
</script>

</form>

