<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ScatterPlanP.aspx.cs" Inherits="UserBackCenter.TeamService.ScatterPlanP" %>

<asp:Content ID="ScatterPlanP" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc1" %>

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'ScatterPlanP'
        });
    </script>

    <%@ register src="~/usercontrol/ILine.ascx" tagname="ILine" tagprefix="uc1" %>
    <div id="<%=Key %>" class="right">
        <!--表格盒子开始-->
        <div class="tablebox">
            <uc1:ILine ID="ILine1" runat="server" />
            <div class="hr_10">
            </div>
            <table id="tab_travelDays" border="0" align="center" cellpadding="0" cellspacing="0"
                style="width: 100%;" class="toolbj1">
                <tr>
                    <td align="left" class="title">
                        出游天数：
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <a href="javascript:void(0);" val="1">一日游</a> <a href="javascript:void(0);" val="2">
                            两日游</a><a href="javascript:void(0);" val="11">三日游及以上</a>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <table id="tab_goCity" border="0" align="center" cellpadding="0" cellspacing="0"
                style="width: 100%; background: #fff;" class="toolbj padd5">
                <tr>
                    <td width="65" height="30" align="left">
                        <strong>出发地点：</strong>
                    </td>
                    <td align="left" class="leavePlace">
                        <span>
                            <asp:repeater id="rpt_DepartureCity" runat="server">
                            <ItemTemplate>
                                <a class="a_City" href="javascript:void(0);" value="<%#Eval("CityId") %>"cname="<%#Eval("CityName")%>"><%#Eval("CityName")%></a>
                            </ItemTemplate>
                        </asp:repeater>
                        </span><a href="javascript:void(0)" id="a_goSet" onclick="ScatterPlanP.SetCity(this)">
                            <span class="huise">更多</span></a>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <span class="search">&nbsp;</span>关键字
                        <input id="txt_keyWord" runat="server" type="text" size="20" style="width: 60px;" />
                        出发地
                        <input id="txt_goCity" runat="server" type="text" size="20" style="width: 60px;" />
                        出团日期
                        <input id="txt_goTimeS" onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'});"
                            runat="server" type="text" size="12" width="60px;" />
                        至
                        <input id="txt_goTimeE" onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'});"
                            runat="server" type="text" size="12" width="60px;" />
                        <button type="button" class="search-btn" onclick="ScatterPlanP.GetList('')">
                            搜索</button>
                        <input type="checkbox" id="ShowPrice" />
                        显示结算价
                    </td>
                </tr>
            </table>
            <table id="tab_status" border="0" align="center" cellpadding="0" cellspacing="0"
                style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="80%" align="left">
                        <div id="div_type" style="float: left; margin-left: 5px;">
                            <span class="guestmenu guestmenu02">类 型</span>
                            <asp:repeater runat="server" id="rpt_type">
                            <ItemTemplate>
                                <a class="state<%#Container.ItemIndex+1 %>" href="javascript:void(0);" val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                            </ItemTemplate>
                        </asp:repeater>
                        </div>
                    </td>
                    <td width="20%" align="center">
                        <a href="/TeamService/LineLibraryList.aspx?type=<%=(int)areaType %>" id="a_GoLineLibraryList">
                            <img src="<%=ImgURL %>/images/arrowpl.gif" />查看周边游线路库</a>
                    </td>
                </tr>
            </table>
            <%--<table width="100%" border="0" align="center">
                <tr>
                    <td align="left" class="ff0000">
                        上海职工国际旅行社联系人 包先生 电话： 传真： 在线QQ 在线MQ
                    </td>
                </tr>
            </table>--%>
            <div id="tab_list">
                <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    style="width: 100%; margin-top: 1px;" class="liststylesp">
                    <tr class="list_basicbg">
                        <th nowrap="nowrap">
                            出发
                        </th>
                        <th nowrap="NOWRAP">
                            团号
                        </th>
                        <th nowrap="NOWRAP" style="width: 400px">
                            线路名称
                        </th>
                        <th nowrap="NOWRAP">
                            状态
                        </th>
                        <th nowrap="NOWRAP">
                            天数
                        </th>
                        <th nowrap="NOWRAP">
                            出团日期
                        </th>
                        <th nowrap="NOWRAP">
                            人数
                        </th>
                        <th nowrap="NOWRAP">
                            余位
                        </th>
                        <th nowrap="NOWRAP">
                            成人价
                        </th>
                        <th nowrap="NOWRAP">
                            儿童价
                        </th>
                        <th nowrap="nowrap" class="list_basicbg ShowJSPrice" style="display: none">
                            结算价(成人/儿童)
                        </th>
                        <th nowrap="NOWRAP">
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
                                <td align="center" nowrap="nowrap">
                                    <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%#Eval("TourNum")%>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <%# Eval("IsLimit").ToString().ToLower()=="true"?"∞": Eval("MoreThan")%>
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
                                <td align="center" nowrap="nowrap">
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
    </div>

    <script type="text/javascript">
        var ScatterPlanP = {
            Init: function() {
                var form = $("#<%=Key %>");
                //列表颜色效果
                form.find("#tab_list tr").hover(
                function() {
                    if ($(this).attr("class") != "list_basicbg")
                        $(this).addClass("highlight");
                },
                function() {
                    if ($(this).attr("class") != "list_basicbg")
                        $(this).removeClass("highlight");
                })
                .click(function() {
                    $(this).parent().find("tr").removeClass("selected");
                    $(this).addClass("selected");
                })
                form.find("#tab_list .Order,#ExportPageInfo a").click(function() {
                    ScatterPlanP.GoUrl(this);
                    return false;
                })
                form.find(".ShowJSPrice").css("display", form.find("#ShowPrice").attr("checked") ? "" : "none")
            },
            SetTxtGoCity: function(obj) {
                var form = $("#<%=Key %>");
                form.find("#tab_goCity .ff0000").removeClass("ff0000");
                $(obj).addClass("ff0000");
                form.find("#<%=txt_goCity.ClientID %>").val($(obj).text());
                ScatterPlanP.GetList('');
            },
            GetList: function(status) {
                var form = $("#<%=Key %>");
                form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif"/>加载中......</div>')
                //查询参数对象
                var SelectData = {
                    MQ: "",
                    keyWord: "", //关键字
                    goCityName: "", //出发地
                    goCityId: "", //出发地Id
                    goTimeS: "", //出团时间开始
                    goTimeE: "", //出团时间结束
                    lineId: "", //专线id
                    lineType: "", //专线类别
                    status: "", //状态
                    uCityId: "", //用户所在城市Id
                    travelDays: ""//出游天数
                }
                SelectData.MQ = '<%=SiteUserInfo.ContactInfo.MQ %>'
                SelectData.uCityId = '<%=SiteUserInfo.CityId %>'
                //状态
                SelectData.status = status;
                //关键字
                SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
                //出团时间开始
                SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
                //出团时间结束
                SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
                //专线商
                SelectData.businessLine = $.trim(form.find("#sel_businessLine").val())
                //出发地ID
                SelectData.goCityId = $.trim(form.find("#tab_goCity .ff0000").attr("value"))
                SelectData.goCityName = $.trim(form.find("#<%=txt_goCity.ClientID %>").val())
                var tab_ILine_select = form.find("#<%=Key %>_tab_ILine .select");
                //专线id
                SelectData.lineId = tab_ILine_select.attr("lineid");
                //旅游天数
                SelectData.travelDays = form.find("#tab_travelDays .select").attr("val");
                $.newAjax({
                    type: "get",
                    url: "/teamservice/ajaxscatterplanp.aspx",
                    data: SelectData,
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        form.find("#tab_list").html(html);
                        ScatterPlanP.Init();
                    }, error: function() {
                        alert("获取异常")
                        topTab.url(topTab.activeTabIndex, "/TeamService/ScatterPlanP.aspx");
                    }
                })
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
                return false;
            },
            //设置城市
            SetCity: function(obj) {
                Boxy.iframeDialog({
                    iframeUrl: "/RouteAgency/SetLeaveCity.aspx?callBack=ScatterPlanP.BoxyCallBack&Key=<%=Key %>&GetType=a&ContainerID=" + $(obj).attr("id") + "&rnd" + Math.random(), title: "设置出发城市",
                    modal: true,
                    width: "650",
                    height: "450"
                });
                return false;
            },
            BoxyCallBack: function() {
                $("#<%=Key %> #tab_goCity a.a_City").click(function() {
                    ScatterPlanP.SetTxtGoCity(this);
                    return false;
                })
            }
        }
        $(function() {
            var form = $("#<%=Key %>");
            ScatterPlanP.Init();
            form.find("#ShowPrice").click(function() {
                form.find(".ShowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
            })
            form.find("#tab_status a").click(function() {
                ScatterPlanP.GetList($.trim($(this).attr("val")))
                return false;
            })
            form.find("#tab_travelDays a").click(function() {
                var form = $("#<%=Key %>");
                form.find("#tab_travelDays .select").removeClass("select");
                $(this).addClass("select");
                ScatterPlanP.GetList('');
                return false;
            })
            form.find("#tab_goCity a.a_City").click(function() {
                ScatterPlanP.SetTxtGoCity(this);
                return false;
            })
            form.find("#<%=txt_goCity.ClientID %>").blur(function() {
                if (form.find("#tab_goCity a[text='" + $.trim($(this).val()) + "']").length <= 0) {
                    form.find(".ff0000").removeClass("ff0000")
                }
            })
            form.find("#a_GoLineLibraryList").click(function() {
                topTab.remove(topTab.activeTabIndex)
                topTab.open($(this).attr("href"), "周边线路库", {});
                return false;
            })
            if ('<%=startCityId %>'.length > 0) {
                form.find("#tab_goCity a.a_City[value='" + '<%=startCityId %>' + "']").addClass("ff0000");
            }
            if ('<%=powderDay %>'.length > 0) {
                form.find("#tab_travelDays a[val='" + '<%=powderDay %>' + "']").addClass("select");
            }
        })
    </script>

</asp:Content>
