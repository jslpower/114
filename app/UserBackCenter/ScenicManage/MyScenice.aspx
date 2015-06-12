<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyScenice.aspx.cs" Inherits="UserBackCenter.ScenicManage.MyScenice" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="MyScenice" runat="server" contentplaceholderid="ContentPlaceHolder1">

    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr style="background: url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
            <td width="1%" height="30" align="left">
            &nbsp;
            </td>
            <td align="left">
                <span class="search">&nbsp;</span>景区名称：
                <input name="Input" size="15" id="txtSceniceName" type="text" runat ="server" />
                <img src="<%= ImageServerUrl %>/images/chaxun.gif" id="btnSearch" width="62" height="21" style="margin-bottom:-4px; cursor:pointer"/>
            </td>
        </tr>
    </table>
<div class="hr_5"></div>
<div style="text-align:left;" id="divAddScenic" runat="server" >
<span class="yellow_btn"><a id="AddScenic" title="添加景区" href="/ScenicManage/AddOrUpdateScenice.aspx" >添加景区</a></span>
</div><div class="hr_5"></div>
<cc1:CustomRepeater ID="repSceniceList" runat="server">
    <HeaderTemplate>
        <table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc" class="padd5">
            <tr class="list_basicbg">
                <th>
                    <strong>景区名称</strong>
                </th>
                <th>
                    <strong>状态</strong>
                </th>
                <th>
                    <strong>景区管理</strong>
                </th>
                <th>
                    <strong>所有门票类型管理</strong>
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr >
            <td height="30" align="left">
                <a class="a_updateScenice" title="修改景区" href="/ScenicManage/AddOrUpdateScenice.aspx?EditId=<%#Eval("ScenicId")%>">
                    <%#Utils.GetText2( Eval("ScenicName").ToString(),28,false)%></a>
                    &nbsp;
                    <%#Eval("ScenicLevel").ToString() == "0" ? "" : Eval("ScenicLevel").ToString()%>
            </td>
            <td align="center">
                <label>
                    <%#Eval("Status")%></label>
            </td>
            <td align="center">
                <a class="a_updateScenice" title="修改景区"  href="/ScenicManage/AddOrUpdateScenice.aspx?EditId=<%#Eval("ScenicId")%>">修改</a></br> 
                <a class="a_EditPhoto" href='javascript:void(0);' onclick="MyScenice.EidtScenciePhoto('<%#Eval("ScenicId") %>')">照片</a>
                <%--</br> 
                <a href="javascript:Void(0);">订单</a>--%>
            </td>
            <td align="left" valign="top">
                <%#GetList((IList<EyouSoft.Model.ScenicStructure.MScenicTickets>)Eval("TicketsList"),Eval("ScenicId").ToString())%>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
    <asp:Panel runat="server" id="NoData"  visible="false">
        <div style="text-align:center;">
            暂无景区信息！
        </div>
    </asp:Panel>     
    <div id="MyScenice_ExportPage" class="F2Back"  style="text-align:right;" height="40">
        <cc2:ExportPageInfo ID="ExportPageInfo1"  CurrencyPageCssClass="RedFnt" LinkType="6"  runat="server"></cc2:ExportPageInfo>
    </div> 
<script type="text/JavaScript">
    var MyScenice = {
        pageInit: function() {
            //分页控件链接控制
            $("#MyScenice_ExportPage a").each(function() {
                $(this).click(function() {
                    topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                    return false;
                })
            });
        },
        EditOrderInfo: function(visitorId)//修改订单
        {
            topTab.url(topTab.activeTabIndex, "/ScenicManage/SceniceOrder.aspx?SceniceId=" + visitorId);
            return false;
        },
        queryData: function() {
            var SceniceName = $("#<%=txtSceniceName.ClientID %>").val();
            var queryUrl = "/ScenicManage/MyScenice.aspx?SceniceName=" + encodeURI(SceniceName);
            topTab.url(topTab.activeTabIndex, queryUrl);
            return false;
        },
        EidtScenciePhoto: function(EditId) {
            var url = "/ScenicManage/ScenicePhoto.aspx?EditId=" + EditId;
            topTab.url(topTab.activeTabIndex, url);
            return false;
        }
    };
    $(document).ready(function() {
        MyScenice.pageInit();
    });
    $(function() {
        $("#AddScenic").click(function() {
            var url = $(this).attr("href");
            var title = $(this).attr("title");
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 830, width: 860, draggable: false });
            return false;
        })
        $(".a_updateScenice").click(function() {
            var url = $(this).attr("href");
            var title = $(this).attr("title");
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 830, width: 860, draggable: true });
            return false;
        });
        $(".a_AddTicket").click(function() {
            var url = $(this).attr("href");
            var title = $(this).attr("title");
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 550, width: 860, draggable: false });
            return false;
        });
        $(".a_updateTicket").click(function() {
            var url = $(this).attr("href");
            var title = $(this).attr("title");
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 550, width: 860, draggable: false });
            return false;
        });
        $("#btnSearch").click(function() {
            //alert("aaa");
            MyScenice.queryData();
        });

        $("#<%=txtSceniceName.ClientID %>").keydown(function(event) {
            if (event.keyCode == 13) {
                $("#btnSearch").click();
                return false;
            }
        });
    })

</script>
</asp:content>
