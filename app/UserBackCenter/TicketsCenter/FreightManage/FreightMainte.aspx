<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreightMainte.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.FreightManage.FreightMainte" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="FreightTop.ascx" TagName="FreightTop" TagPrefix="uc1" %>
<asp:content id="FreightAdd" contentplaceholderid="ContentPlaceHolder1" runat="server">
  
<div id="fre_topdiv">
    <ul class="sub_leftmenu">
        <li><a  href="/ticketscenter/freightmanage/freightadd.aspx" rel="toptaburl">运价添加</a></li>
        <li><a class="book_default" style=" text-decoration:none; cursor:default;">运价维护</a></li>
    </ul>
    <div class="clearboth">
    </div>
        <uc1:FreightTop ID="FreightTop1" runat="server" />
    </div>
<table width="835" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff"
    class="admin_tablebox">
    <tr>
        <td colspan="4" height="10">
        </td>
    </tr>
    <tr>
        <td width="175" height="35" align="right">
            <strong>航空公司：</strong>
        </td>
        <td colspan="3" align="left">
            <asp:dropdownlist id="mai_AirCompany" runat="server">
                            <asp:ListItem Value="0" Text="CA-国际航空"></asp:ListItem>
                        </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td height="35" align="right">
            <strong>始发地：</strong>
        </td>
        <td colspan="3" align="left">
            <asp:dropdownlist id="mai_DdlBegin" runat="server">
                            <asp:ListItem Value="0" Text="A-ak47"></asp:ListItem>
                        </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td height="35" align="right">
            <strong>目的地：</strong>
        </td>
        <td width="134" align="left">
            <asp:dropdownlist id="mai_DdlEnd" runat="server">
                            <asp:ListItem Value="0" Text="A-M16"></asp:ListItem>
                        </asp:dropdownlist>
        </td>
        <td width="129" align="left">
            <a href="javascript:void(0);">
                <img id="mai_ImgBtnSearch" width="79px" height="25px" src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg" onclick="GetListBySearch('1')"/>
            </a>
        </td>
        <td width="395" align="left">
        </td>
    </tr>
    <tr>
        <td height="10" colspan="4" align="right">
        </td>
    </tr>
</table>
<div class="clearboth">
</div>
<table width="835" border="0" cellpadding="0" cellspacing="0" bgcolor="#EEF7FF" class="admin_tablebox01" style=" display:none;">
    <tr>
        <td width="198" height="35" align="right">
            <strong>过期查询：</strong>
        </td>
        <td width="204" align="center">
            <asp:textbox id="mai_txtExpiredDate" runat="server" onfocus="WdatePicker()"></asp:textbox>
            <img style="position: relative; left: -24px; top: 3px; *top: 1px;" src="<%=ImageServerUrl %>/images/jipiao/time.gif"
                width="16" height="13" />
        </td>
        <td width="222" align="center">
            <a href="javascript:void(0);">
                <img id="btnDateSearch" src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg"
                    width="79" height="25" alt="查询"  />
            </a>
        </td>
        <td width="209" align="left">
            <font color="#FF0000">查询此日期以前过期的运价</font>
        </td>
    </tr>
</table>
<div id="divList" style=" width:840px; height:500px;">
</div>
    <div style=" clear:both"></div>
<script type="text/javascript">
    $(function() {
        $("#fre_topdiv a[rel='toptaburl']").click(function() {
            topTab.open("/ticketscenter/freightmanage/freightadd.aspx", "团队／散拼");
            //topTab.url(topTab.activeTabIndex, $(this).attr("href"));
            return false;
        });
        GetListBySearch("1");
    });

    function GetListBySearch(pageIndex) {
        var airCompanyId = $("#<%=mai_AirCompany.ClientID%> option:selected").val();
        var start = $("#<%=mai_DdlBegin.ClientID%> option:selected").val();
        var end = $("#<%=mai_DdlEnd.ClientID%> option:selected").val();
        var params = { aId: airCompanyId, startId: start, endId: end, Page: pageIndex };
        var str = $.param(params);

        $("#divList").html("正在查找....");
        $.ajax({
            type: "GET",
            url: "/ticketscenter/freightmanage/ajaxmainte.aspx?v=" + Math.random(),
            data: str,
            cache: false,
            success: function(result) {
                if (result == "Login") {
                    window.location = "TicketsCenter/Default.aspx";
                    return;
                }
                $("#divList").html(result);

                $("#div_AjaxList a").click(function() {
                    var str = $(this).attr("href").match(/&[^&]+$/);
                    pageIndex = str.toString().replace("&Page=", "");
                    GetListBySearch(pageIndex);
                    return false;
                });

                $("#div_AjaxList select").change(function() {
                    pageIndex = $(this).val();
                    GetListBySearch(pageIndex);
                    return false;
                });
            }
        });
    }
    
    
</script>

</asp:content>
