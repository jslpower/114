<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Collect.aspx.cs" Inherits="UserBackCenter.TeamService.Collect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:Content ID="Collect" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'Collect'
        });
    </script>

    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;"
                class="toolbj">
                <tr>
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <b>我收藏的专线商：</b>
                        <asp:repeater runat="server" id="rpt_iCollect">
                            <ItemTemplate>
                                <%#Eval("CompanyBrand")%>   
                            </ItemTemplate>
                        </asp:repeater>
                        <span class="fav_btn"><a id="a_AddCollect" href="javascript:void(0);">添加收藏</a> </span>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span>关键字
                        <input id="txt_keyWord" class="keydownSelect" runat="server" type="text" size="20"
                            style="width: 60px;" />
                        收藏专线商
                        <asp:dropdownlist runat="server" id="ddl_iCollect">
                            <asp:listItem value="-1">-全部-</asp:listItem>
                        </asp:dropdownlist>
                        出发地点
                        <asp:dropdownlist id="ddl_goCity" runat="server">
                            <asp:listItem value="-1">-全部-</asp:listItem>
                       </asp:dropdownlist>
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect" runat="server" onfocus="WdatePicker();"
                            type="text" size="12" width="60px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" runat="server" onfocus="WdatePicker();"
                            type="text" size="12" width="60px;" />
                        <button type="button" id="btn_Select" class="search-btn">
                            搜索</button>
                        <input type="checkbox" id="ShowPrice" />
                        显示结算价
                    </td>
                </tr>
            </table>
            <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%; margin-top: 1px;" class="liststylesp">
                <tr class="list_basicbg">
                    <th nowrap="nowrap" class="list_basicbg">
                        出发
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        团号
                    </th>
                    <th nowrap="nowrap" class="list_basicbg" style="width: 400px">
                        线路名称
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        推荐状态
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        天数
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        出团日期
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        报名截止
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        人数
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        余位
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        成人价
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        儿童价
                    </th>
                    <th nowrap="nowrap" class="list_basicbg ShowJSPrice" style="display: none">
                        结算价(成人/儿童)
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        功能
                    </th>
                </tr>
                <asp:repeater runat="server" id="rpt_list">
                    <ItemTemplate>
                         <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                            <td align="center" nowrap="NOWRAP">
                                <%#Eval("StartCityName")%>
                            </td>
                            <td align="left" nowrap="nowrap">
                                <%#EyouSoft.Common.Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)Eval("CompanyLev"))%><%#Eval("TourNo")%>
                            </td>
                            <td align="left">
                                <a target="_blank" href='/PrintPage/TeamRouteDetails.aspx?TeamId=<%#Eval("TourId") %>'><%#Eval("RouteName")%></a><%=EyouSoft.Common.Utils.GetMQ(SiteUserInfo.ContactInfo.MQ)%>
                            </td>
                            <td align="center" nowrap="NOWRAP">
                               <span class="state<%#(int)Eval("RecommendType")-1 %>"><%#Eval("RecommendType").ToString() == "0" || Eval("RecommendType").ToString() =="无"? "" : Eval("RecommendType")%></span> 
                            </td>
                            <td align="center" nowrap="nowrap">
                                <%#Eval("Day")%>
                            </td>
                            <td align="center">
                                <%# Eval("LeaveDate","{0:MM/dd(ddd)}")%>
                            </td> 
                            <td align="center">
                                <span class="ff0000"><%# Eval("RegistrationEndDate", "{0:MM/dd(ddd)}")%></span>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <%#Eval("TourNum")%>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <%#Eval("MoreThan")%>
                            </td>
                            <td align="center" nowrap="nowrap">

                                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                            </td>
                            <td align="center" nowrap="nowrap">

                                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                            </td>
                            <td align="center" nowrap="NOWRAP" class="ShowJSPrice"  style="display:none">
                               <span class="ff0000"> <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                                /
                                <span class="ff0000"><%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
                            </td>
                            <td align="center" nowrap="NOWRAP">
                                <%#((int)Eval("PowderTourStatus") == 1 || (int)Eval("PowderTourStatus") == 2 ? Eval("PowderTourStatus") : GetButt(Eval("TourId").ToString()))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
            </table>
            <asp:panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
                 </asp:panel>
            <table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right"
                border="0">
                <tr>
                    <td class="F2Back" align="right" height="40">
                        <cc1:ExportPageInfo ID="ExportPageInfo1" Visible="false" LinkType="4" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        var Collect = {
            GetList: function() {
                var form = $("#<%=Key %>");
                //查询参数对象
                var SelectData = {
                    keyWord: "", //关键字
                    goCityId: "", //出发地Id
                    goTimeS: "", //出团时间开始
                    goTimeE: "", //出团时间结束
                    publishers: "", //专线商ID
                    isShowPrice: ""//是否显示结算价
                }
                //关键字
                SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                //出发地ID
                SelectData.goCityId = $.trim(form.find("#<%=ddl_goCity.ClientID %>").val());
                //专线商ID
                SelectData.publishers = $.trim(form.find("#<%=ddl_iCollect.ClientID %>").val());
                SelectData.isShowPrice = form.find("#ShowPrice").attr("checked");
                var queryUrl = encodeURI("/TeamService/Collect.aspx?" +
                "keyWord=" + SelectData.keyWord +
                "&goCityId=" + (SelectData.goCityId == "-1" ? "" : SelectData.goCityId) +
                "&goTimeS=" + SelectData.goTimeS +
                "&goTimeE=" + SelectData.goTimeE +
                "&publishers=" + (SelectData.publishers == "-1" ? "" : SelectData.publishers) +
                "&isShowPrice=" + SelectData.isShowPrice);
                topTab.url(topTab.activeTabIndex, queryUrl);
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
                return false;
            }
        }
        $(function() {
            var Key = $("#<%=Key %>");
            Key.find("#a_AddCollect").click(function() {
                topTab.open("/TeamService/DirectorySet.aspx", "添加收藏", {});
                return false;
            })
            Key.find("#btn_Select").click(function() {
                Collect.GetList();
                return false;
            })
            Key.find("#ShowPrice").click(function() {
                Key.find(".ShowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
            })
            Key.find(".Order,#ExportPageInfo a").click(function() {
                Collect.GoUrl(this);
                return false;
            })
            if ('<%=isShowPrice %>' == "true") {
                Key.find(".ShowJSPrice").css("display", "")
                Key.find("#ShowPrice").attr("checked", "checked")
            }
        })
    </script>

</asp:Content>
