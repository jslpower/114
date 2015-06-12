<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScatteredFightPlan.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.ScatteredFightPlan" %>

<asp:content id="ScatteredFightPlan" contentplaceholderid="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=Key %>',
        ReleaseType: 'ScatteredFightPlan'
    });
</script>

<%@ Register Src="~/usercontrol/ILine.ascx" TagName="ILine" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>

<div id="<%=Key %>" class="right">
        <div class="tablebox">
        <uc1:ILine ID="ILine1" runat="server" />
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">&nbsp;</td>
                    <td height="30" align="left">
                        <span class="search">&nbsp;</span>关键字
                        <input id="txt_keyWord" class="keydownSelect" runat="server" type="text" size="20" style="width: 100px;" />
                        专线：
                       <asp:DropDownList runat="server" id="ddl_ZX">
                           <asp:listItem value="-1">-全部-</asp:listItem>
                       </asp:DropDownList>
                        出团日期
                        <input id="txt_goTimeS" class="keydownSelect"  runat="server" type="text" size="12" onfocus="WdatePicker();" width="80px;" />
                        至
                        <input id="txt_goTimeE" class="keydownSelect" runat="server" type="text" size="12" onfocus="WdatePicker();" width="80px;" />
                        <button type="button" id="btn_select" class="search-btn" onclick="ScatteredFightPlan.GetList()">
                            搜索</button><input type="checkbox"id="ShowPrice"  />
                        显示结算价
                    </td>
                    <td width="15%" align="center">
                        <a href="/routeagency/routemanage/routeview.aspx" rel="toptab" tabrefresh="false" onclick='topTab.open($(this).attr("href"),"我的线路库",{isRefresh:false});return false;'><img src="<%=ImgURL %>/images/arrowpl.gif" />返回线路库发布计划</a>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/lmnavm.gif); height: 32px;">
                    <td width="1%" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                    <div id="div_powderTourStatus"  style="float:left">
                        <span class="guestmenu">收客状态</span> 
                        <asp:Repeater runat="server" id="rpt_powderTourStatus">
                            <ItemTemplate>
                                <a href="javascript:void(0);" class="scatteredFightPlan"  val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div  id="div_type"style="float:left ;margin-left: 5px;">                        
                        <span class="guestmenu guestmenu02">类 型</span>
                        <asp:Repeater runat="server" id="rpt_type">
                            <ItemTemplate>
                                <a class="state<%#EyouSoft.Common.Utils.GetInt(Eval("Value").ToString())-1%>" href="javascript:void(0);" val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                            </ItemTemplate>
                        </asp:Repeater>
                        <a class="nostate" href="javascript:void(0)" val=1>取消设置</a>
                    </div>
                    <a href="/SMSCenter/SendSMS.aspx?type=1" id="a_note"class="state1">短信推广</a>
                    </td>
                </tr>
            </table>
            <div id="tab_list">
            <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%; margin-top: 1px;" class="liststylesp">
                <tr class="list_basicbg">
                    <th class="list_basicbg">
                        <input type="checkbox" id="chk_All" />
                        全
                    </th>
                    <th class="list_basicbg">
                        团号
                    </th>
                    <th class="list_basicbg" style="width: 400px">
                        线路名称
                    </th>
                    <th class="list_basicbg">
                        出团日期
                    </th>
                    <th class="list_basicbg">
                        报名截止
                    </th>
                    <th class="list_basicbg">
                        人数
                    </th>
                    <th class="list_basicbg">
                        余位
                    </th>
                    <th class="list_basicbg">
                        推荐类型
                    </th>
                    <th class="list_basicbg">
                        成人价
                    </th>
                    <th class="list_basicbg">
                        儿童价
                    </th>
                    <th  class="list_basicbg ShowJSPrice" style="display:none">
                        结算价
                    </th>
                    <th class="list_basicbg">
                        单房差
                    </th>
                    
                    <th class="list_basicbg">
                        打印
                    </th>
                    <th class="list_basicbg">
                        功能
                    </th>
                </tr>
                <asp:Repeater runat="server" id="rpt_list">
                    <ItemTemplate>
                    <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                        <td align="center">
                            <input type="checkbox" class="list_id" value="<%#Eval("TourId") %>" />
                        </td>
                        <td align="center">
                            <%#Eval("TourNo")%>
                        </td>
                        <td align="left">
                            <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a>
                        </td>
                        <td align="center">
                            <%#((DateTime)Eval("LeaveDate")).ToString("yyyy-MM-dd") %>
                        </td>
                        <td align="center">
                            <%#((DateTime)Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>
                        </td>
                        <td align="center">
                           <%#Eval("TourNum")%>
                        </td>
                        <td align="center">
                            <%#Eval("MoreThan")%>
                        </td>
                        <td align="center">
                            <span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span>
                        </td>
                        <td align="center">
                           <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                        </td>
                        <td align="center">
                          <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                        </td>
                        <td align="center" nowrap="NOWRAP" class="ShowJSPrice"  style="display:none">
                           <span class="ff0000"> <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                            /
                            <span class="ff0000"><%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
                        </td>
                        <td align="center">
                           <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MarketPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MarketPrice").ToString())%>
                        </td>
                        
                        <td align="center" nowrap="nowrap">
                            <a target="_blank" href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>'>名单</a> <a  href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">
                                行程单</a><br />
                            <a href='/PrintPage/OutGroupNotices.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">出团通知书</a>
                        </td>
                        <td align="center" nowrap="nowrap" class="list-caozuo">
                            <a href="/Order/RouteAgency/AddOrderByRoute.aspx?tourID=<%#Eval("TourId") %>" class="boto">代定</a> <a href="/RouteAgency/UpdateScatteredFightPlan.aspx?tourId=<%#Eval("TourId") %>" class="a_Update">修改</a><br />
                            <a href="/RouteAgency/PlanList.aspx?routeId=<%#Eval("RouteId") %>" class="a_goPlanList">计划</a> 
                            <a class="a_orders" href="/Order/RouteAgency/NewOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                            <a class="a_Del" ordernum="<%#Eval("OrderNum") %>" href="javascript:void(0);" tourid="<%#Eval("TourId") %>">删除</a>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
             <asp:Panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
             </asp:Panel>
             <table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right" border="0">
                <tr>
                    <td class="F2Back" align="right" height="40">
                        <cc1:ExportPageInfo ID="ExportPageInfo1" Visible=false LinkType="4" runat="server" />
                    </td>
                </tr>
            </table>
            </div>
            <%--<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <ul>
                            <li><strong>代订是什么意思？</strong>
                                <ul>
                                    <li>答：如果是同业114的组团社临时电话预定产品，为了便于记录和双方操作，专线商可在接听电话后为组团社代订团队位置，组团社也有在以后有空上网的时候，查看和管理代订的线路。</li>
                                </ul>
                            </li>
                            <li><strong>团队修改和线路修改互相冲突吗？</strong>
                                <ul>
                                    <li>答：为了保证单团行程的独立，便于出团通知书的打印，团队的行程是从线路库复制的游程，所以线路库线路内容的修改不会改变团队的行程。所以如果您需要修改行程便于打印出团通知书，请在团队管理的列表，点击对应团队进行修改，填写对应详细的团队内容。</li>
                                </ul>
                            </li>
                            <li><strong>我要看某条线路的所有出团计划怎么看？</strong>
                                <ul>
                                    <li>答：点击该团条目后面功能栏中的计划字样，就会自动筛选显示该线路所有有效计划。如果需要对该线路的多个团队的内容修改，也需要先点击计划，然后选择团队修改内容。</li>
                                </ul>
                            </li>
                            <li><strong>为什么有些团队能删除，有些不能？</strong>
                                <ul>
                                    <li><strong>答：</strong>删除就是删除该条团队计划，如果该天已经有预定订单，则隐藏删除功能而显示订单功能。点击订单显示该团的预定订单。</li>
                                </ul>
                            </li>
                        </ul>
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
            </table>--%>
        </div>
</div>
<script type="text/javascript">
    var ScatteredFightPlan = {
        Init: function() {
            var form = $("#<%=Key %>");
            //全选
            form.find("#chk_All").click(function() {
                ScatteredFightPlan.AllChk(this);
            })
            //分页
            form.find("#ExportPageInfo a").click(function() {
                ScatteredFightPlan.GoUrl(this);
                return false;
            })
            //修改
            form.find(".a_Update").click(function() {
                ScatteredFightPlan.GoUrl(this);
                return false;
            })
            //计划
            form.find(".a_goPlanList").click(function() {
                topTab.open($(this).attr("href"), "计划列表", {})
                return false;
            })
            //代订,订单
            form.find(".boto,.a_orders").click(function() {
                topTab.open($(this).attr("href"), "散拼计划代订", {})
                return false;
            })
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
            form.find(".a_Del").each(function() {
                if (parseInt($(this).attr("ordernum")) > 0) {
                    $(this).remove();
                }
                else {
                    $(this).parent().find(".a_orders").remove();
                }
            })
            form.find(".a_Del").click(function() {
                if (confirm("确定删除该计划？")) {
                    ScatteredFightPlan.Set("Del", null, $(this).attr("tourid"));
                }
                return false;

            })

        },
        AllChk: function(obj) {
            var form = $("#<%=Key %>");
            form.find("#tab_list :checkbox:not(#chk_All)").attr("checked", $(obj).attr("checked"))
        },
        //设置选中专线
        SetSelectLine: function(obj) {
            var iLine1 = $("#<%=Key %>" + "_tab_ILine");
            iLine1.find(".select").removeClass("select");
            iLine1.find("[lineid='" + $(obj).val() + "']").addClass("select");
        },
        GetList: function() {
            var form = $("#<%=Key %>");
            form.find("#tab_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif"/>加载中......</div>')
            //查询参数对象
            var SelectData = {
                keyWord: "", //关键字
                goTimeS: "", //出团时间开始
                goTimeE: "", //出团时间结束
                lineId: "", //专线id
                lineType: "", //专线类别
                companyID: ""
            }
            SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
            //关键字
            SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
            //出团时间开始
            SelectData.goTimeS = $.trim(form.find("#<%=txt_goTimeS.ClientID %>").val());
            //出团时间结束
            SelectData.goTimeE = $.trim(form.find("#<%=txt_goTimeE.ClientID %>").val());
            //选中的专线对象
            var tab_ILine_select = form.find("#<%=Key %>" + "_tab_ILine .select");
            form.find("#<%=ddl_ZX.ClientID %>").val(tab_ILine_select.attr("lineid"));
            //专线id
            SelectData.lineId = tab_ILine_select.attr("lineid");
            //专线类型
            SelectData.lineType = tab_ILine_select.attr("linetype");

            $.newAjax({
                type: "get",
                url: "/RouteAgency/AjaxScatteredFightPlanList.aspx",
                data: SelectData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    form.find("#tab_list").html(html);
                    ScatteredFightPlan.Init();
                }, error: function() {
                    alert("获取异常")
                }
            })
        },
        GoUrl: function(obj) {
            topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
            return false;
        },
        Set: function(settype, setvalue, ids) {
            var form = $("#<%=Key %>");
            var url = "/RouteAgency/ScatteredFightPlan.aspx"
            var SetData = {
                ids: ids, //id列表
                SetType: settype, //修改类型（线路状态or推荐状态or删除）
                SetValue: setvalue//修改值
            }
            var msg = "";
            switch (settype) {
                case "RecommendType":
                    msg += "设置推荐状态";
                    break;
                case "PowderTourStatus":
                    msg += "设置收客状态";
                    break;
                case "Del":
                    msg += "删除计划";
                    break;
            }
            $.newAjax({
                type: "get",
                url: url,
                data: SetData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    if (html.toLowerCase() == "true") {
                        alert(msg + "成功！")
                        ScatteredFightPlan.GetList();
                    }
                    else {
                        alert(msg + "失败！")
                    }
                }, error: function() {
                    alert("设置异常")
                    topTab.url(topTab.activeTabIndex, "/RouteAgency/ScatteredFightPlan.aspx");
                }
            })
        },
        Note: function(obj) {
            var form = $($("#<%=Key %>"));
            var id = "";
            form.find(".list_id:checked").each(function() {
                id += $(this).val() + ",";
            })
            topTab.open($(obj).attr("href") + "&id=" + id.substring(0, id.length - 1), "短信推广", {});
        }
    }
    $(function() {
        var form = $("#<%=Key %>");
        //回车查询效果
        form.find(".keydownSelect").keydown(function(e) {
            if (e.keyCode == 13) {
                ScatteredFightPlan.GetList();
                return false;
            } else {
                return true;
            }
        });
        ScatteredFightPlan.Init();
        //专线下拉效果
        form.find("#<%=ddl_ZX.ClientID %>").change(function() {
            ScatteredFightPlan.SetSelectLine(this);
        })
        //显示结算价
        form.find("#ShowPrice").change(function() {
            form.find(".ShowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
        })
        //团队状态添加class
        var className = ["keman", "tings", "zhengc", "chengtuan"]
        form.find("a.scatteredFightPlan").each(function() {
            $(this).addClass(className[parseInt($(this).attr("val")) - 1])
        })

        //推荐状态
        form.find("#div_type a").click(function() {
            var str = "";
            form.find("#tab_list .list_id:checked").each(function() {
                str += $(this).attr("value") + ",";
            })
            if (str.length > 0) {
                if (confirm("确定要将选中的计划 进行 " + $(this).text() + " 设置？")) {
                    ScatteredFightPlan.Set("RecommendType", $(this).attr("val"), str.substring(0, str.length - 1));
                }
            }
            else {
                alert("请先选择数据!");
            }
            return false;
        })
        //收客状态
        form.find("#div_powderTourStatus a").click(function() {
            var str = "";
            form.find("#tab_list .list_id:checked").each(function() {
                str += $(this).attr("value") + ",";
            })
            if (str.length > 0) {
                if (confirm("确定要将选中的计划 进行 " + $(this).text() + " 设置？")) {
                    ScatteredFightPlan.Set("PowderTourStatus", $(this).attr("val"), str.substring(0, str.length - 1));
                }
            }
            else {
                alert("请先选择数据!");
            }
        })
        form.find("#a_note").click(function() {
            ScatteredFightPlan.Note(this);
            return false;
        })

    })
</script>
</asp:content>
