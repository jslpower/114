<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetAgencyOrderList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.AgencyActionAnalysis.GetAgencyOrderList" %>

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
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>
                        序号</strong>
                </td>
                <td width="46%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <%=GetTableHead(1) %>
                </td>
                <td width="12%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>预订状态</strong>
                </td>
                <td width="12%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <%=GetTableHead(2) %>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <%=GetTableHead(3) %>
                </td>
            </tr>
<cc1:CustomRepeater ID="rep_CompanyOrderList" runat="server">
    
    <HeaderTemplate>
        
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center" valign="middle">
                <%# GetCount()%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='OrderList.CompanyInfo("<%#Eval("CompanyId") %>");'><%# Eval("CompanyName")%></a>
            </td>
            <td align="center" valign="middle">
                <%=strOrderTypeName%>
            </td>
            <td align="center" valign="middle">
                <a href="javascript:void(0)" onclick='OrderList.OrderCountDetail("<%#Eval("CompanyId") %>");'><%#Convert.ToInt32(Eval("OrdainNum").ToString()) + Convert.ToInt32(Eval("SaveSeatExpiredNum").ToString()) + Convert.ToInt32(Eval("NotAcceptedNum").ToString())%>次</a>
            </td>
            <td width="20%" align="center" valign="middle">
                <a href="javascript:void(0)" onclick='OrderList.OrderPeopleDetail("<%#Eval("CompanyId") %>");'><%# Eval("OrdainPeopleNum")%>人</a>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
    <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center" valign="middle">
                <%# GetCount()%>
            </td>
            <td align="center">
                <a  href="javascript:void(0)" onclick='OrderList.CompanyInfo("<%#Eval("CompanyId") %>");'><%# Eval("CompanyName")%></a>
            </td>
            <td align="center" valign="middle">
                <%=strOrderTypeName%>
            </td>
            <td align="center" valign="middle">
                <a href="javascript:void(0)" onclick='OrderList.OrderCountDetail("<%#Eval("CompanyId") %>");'><%#Convert.ToInt32(Eval("OrdainNum").ToString()) + Convert.ToInt32(Eval("SaveSeatExpiredNum").ToString()) + Convert.ToInt32(Eval("NotAcceptedNum").ToString())%>次</a>
            </td>
            <td width="20%" align="center" valign="middle">
                <a href="javascript:void(0)" onclick='OrderList.OrderPeopleDetail("<%#Eval("CompanyId") %>");'><%# Eval("OrdainPeopleNum")%>人</a>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        
    </FooterTemplate>
    
</cc1:CustomRepeater>
<tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>序号</strong>
            </td>
            <td width="46%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>零售商名称</strong>
            </td>
            <td width="12%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>预订状态</strong>
            </td>
            <td width="12%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>预订次数</strong>
            </td>
            <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>预订总人数</strong>
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
    var OrderList = {
        OrderParms: {"CompanyId":0,"BeginTime": "<%=Request.QueryString["StartDate"] %>", "EndTime": "<%=Request.QueryString["EndDate"] %>", "OrderState": "<%=Request.QueryString["OrderType"] %>" },
        openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
            Boxy.iframeDialog({ fixed:false,title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
        },
        CompanyInfo:function(CompanyId){
            this.openDialog("/Statistics/CompanyInfo.aspx", "公司信息", "500", "400", "CompanyId="+CompanyId);
            return false;
        },
        OrderCountDetail: function(CompanyId) {
            this.OrderParms.CompanyId=CompanyId;
            this.openDialog("/Statistics/OrderListOfCompany.aspx", "预定次数", "700", "400", $.param(OrderList.OrderParms));
            return false;
        },
        OrderPeopleDetail: function(CompanyId) { 
            this.OrderParms.CompanyId=CompanyId;
            this.openDialog("/Statistics/BookPeopleCount.aspx", "预定总人数", "700", "450", $.param(OrderList.OrderParms));
            return false;
        }
    };
</script>
