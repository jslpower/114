<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteView.aspx.cs" Inherits="UserBackCenter.RouteAgency.RouteManage.RouteView" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/ILine.ascx" TagName="ILine" TagPrefix="uc1" %>
<asp:content id="RouteView" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'RouteView'
        });
    </script>
<div id="<%=Key %>" class="right" >
       <div class="tablebox" >
         <uc1:ILine ID="ILine1" runat="server" />
        
         <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
           <tr style="background:url(<%=ImageServerPath %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
             <td width="1%" height="30" align="left">&nbsp;</td>
             <td width="89%" align="left" nowrap="nowrap"><span class="search">&nbsp;</span>关键字
               <input id="txt_keyWord" class="keydownSelect" type="text" value="线路名称" size="20" runat="server">
               专线
               <asp:DropDownList runat="server" id="ddl_ZX">
                   <asp:listItem value="-1">-全部-</asp:listItem>
               </asp:DropDownList>
               出发：
               <asp:DropDownList id="ddl_goCity" runat="server">
                    <asp:listItem value="-1">-全部-</asp:listItem>
               </asp:DropDownList>

             <a id="a_Select" href="javascript:void(0);"><img src="<%=ImageServerPath %>/images/chaxun.gif" width="62" height="21" style="margin-bottom:-4px;"/></a> </td>
             <td width="10%" align="center"><a href="/routeagency/scatteredfightplan.aspx" rel="toptab" tabrefresh="false" onclick='topTab.open($(this).attr("href"),"我的散拼计划",{isRefresh:false});return false;'><img src="<%=ImageServerPath %>/images/arrowpl.gif" />散拼计划</a></td>
           </tr>
         </table>
         
         <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
           <tr style="background:url(<%=ImageServerPath %>/images/lmnavm.gif); height:32px;">
             <td width="1%" align="left">&nbsp;</td>
             <td width="99%" align="left">
                <div id="div_status"  style="float:left" >
                    <span class="guestmenu">线路状态</span> 
                    <a href="javascript:void(0);" class="shangjia" val="1">↑上架</a> 
                    <a href="javascript:void(0);" class="xiajia" val="2">↓下架</a> 
                </div>
                <div id="tab_type" style="float:left;margin-left: 5px;" >
                    <span class="guestmenu guestmenu02">类 型</span>
                    <asp:Repeater runat="server" id="rpt_type">
                        <ItemTemplate>
                            <a class="state<%#Utils.GetInt(Eval("Value").ToString())-1%>" href="javascript:void(0);" val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                        </ItemTemplate>
                    </asp:Repeater>
                    <a class="nostate" href="javascript:void(0)" val=1>取消设置</a>
                </div>
                 <a href="/SMSCenter/SendSMS.aspx?type=0" id="a_note"class="state1">短信推广</a></td>
           </tr>
         </table>
         <div id="tab_list">
             <table class="liststylesp" cellpadding="1"cellspacing="0"bordercolor="#9dc4dc"style="width:100%;margin-top:1px;"class="liststyle" border="1">
               <tr class="list_basicbg">
                 <th class="list_basicbg"><input type="checkbox" id="chk_All" />
                   全</th>
                 <th class="list_basicbg" style="width: 450px">线路名称</th>
                 <th class="list_basicbg">出发</th>
                 <th class="list_basicbg">状态</th>
                 <th class="list_basicbg">天数</th>
                 <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                   {%>
                 <th class="list_basicbg">计划班次</th>
                 <th class="list_basicbg">成人价</th>
                 <th class="list_basicbg">儿童价</th>
                 <%}
                   else
                   { %>
                   <th class="list_basicbg">团队参考价</th>
                 <%} %>
                 <th class="list_basicbg">阅览</th>
                 <th class="list_basicbg">打印</th>
                 <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                   {%>
                 <th class="list_basicbg">计划管理</th>
                 <%} %>
                 <th class="list_basicbg">操作</th>
               </tr>
               <asp:Repeater id="rpt_List" runat="server">
                   <ItemTemplate>
                       <tr routeid="<%#Eval("RouteId") %>" routetype="<%#(int)Eval("RouteType") %>" <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                             <td align="center"><input  type="checkbox" class="list_id" value="<%#Eval("RouteId") %>" /></td>
                             <td align="left"><a href='/PrintPage/RouteDetail.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"><%#Eval("RouteName")%></a></td>
                             <td align="center"><%#Utils.GetText2( Eval("StartCityName").ToString(),18,false)%></td>
                             <td align="center"><span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span></td>
                             <td align="center"><%#Eval("Day")%></td>
                             <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                               {%>
                             <td align="center"><a title="<%#Eval("TeamPlanDes")!=null&&Eval("TeamPlanDes").ToString().Length>0?"计划列表":"历史团队"%>" href="/RouteAgency/<%#Eval("TeamPlanDes")!=null&&Eval("TeamPlanDes").ToString().Length>0?"PlanList.aspx":"HistoryTeam.aspx" %>?routeId=<%#Eval("RouteId") %>" class="a_goPlanList"><%#Eval("TeamPlanDes") != null && Eval("TeamPlanDes").ToString().Length>0?Eval("TeamPlanDes"):""%></a></td>
                             <td align="center"><%#Eval("TeamPlanDes") != null && Eval("TeamPlanDes").ToString().Length > 0 ? Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) : Eval("IndependentGroupPrice") != null && Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString()) != "0" ? Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString()) : ""%></td>
                             <td align="center"><%#Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%></td>
                             <%}
                               else
                               { %>
                               <td align="center"><%#Utils.FilterEndOfTheZeroString(Eval("IndependentGroupPrice").ToString())%></td>
                               <%} %>
                             <td align="center"><a href="javascript:void(0);"><%#Eval("ClickNum") %>次</a></td>
                             <td align="center" nowrap="nowrap"><a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank">线路行程单</a></td>
                             <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                               {%>
                             <td align="center" nowrap="nowrap"><a href="/RouteAgency/AddScatteredFightPlan.aspx?routeid=<%#Eval("RouteId") %>&routename=<%#Eval("RouteName")%>" class="AddorUpdate">批量添加修改计划</a></td>
                             <%} %>
                             <td align="center" nowrap="NOWRAP" class="list-caozuo">
                                 <a href="/RouteAgency/RouteManage/AddTourism.aspx" class="Update">修改</a> 
                                 <a href="/RouteAgency/RouteManage/AddTourism.aspx" class="Copy">复制</a><br />
                                 <a href="javascript:void(0);" class="Del">删除</a> 
                                 <a href="javascript:void(0);" class="RouteStatus" val="<%#(int)Eval("RouteStatus") == 1 ? "2" : "1"%>"><span class="<%#(int)Eval("RouteStatus") == 1 ? "greencolor" : "ff0000"%>"><%#(int)Eval("RouteStatus") == 1 ? "下架" : "上架"%></span></a> 
                             </td>
                       </tr>
                   </ItemTemplate>
               </asp:Repeater>
             </table>
             
             <asp:Panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!&nbsp;&nbsp;<a href="/routeagency/routemanage/rmdefault.aspx?RouteSource=<%=(int)routeSource %>" rel="toptab" tabrefresh="false" onclick='topTab.open($(this).attr("href"),"新增线路",{isRefresh:false});return false;' style="color:Red;">点此添加</a></td>
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
         <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
           <tr>
              <td align="left"><strong>如何发布线路？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：点击我的专线后面的添加按钮，或者点击功能栏的添加线路链接即可开始发布线路。</td>
           </tr>
           <tr>
             <td align="left"><strong>线路的状态有什么用？</strong></td>
             </tr>
           <tr>
             <td align="left"><strong>答</strong>：线路状态包括，推荐 特价 豪华 热门 新品 纯玩 经典。系统会在线路列表时会放置图片标签，用于线路团队分类和展示。</td>
           </tr>
           <tr>
             <td align="left"><strong>如何针对线路游程发布月度散客发班计划？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：发布线路后，线路出现在线路库列表中，在该线路后面的散客计划管理栏内点击添加线路即可发布最新的散客计划，如果以及发布计划，点击管理也可以增加和修改已经发布的计划。</td>
           </tr>
           <tr>
             <td align="left"><strong>复制有什么用？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：不少线路游程基本一致，但是由于针对不同的消费人群，住宿，购物点和费用有较大的变化，建议用复制功能发布类似线路，便于针对不同市场进行不同的宣传和推广。方法，点击线路后面线路管理功能栏中的“复制“按钮即可。</td>
           </tr>
           <tr>
             <td align="left"><strong>上架，下架是什么意思？有什么用？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：上架状态的线路和团队，组团社和游客都可以看到并且预定；下架后，就只有发布人能够修改和管理了。由于线路单独也可以单团预定，所以上下架时，系统会让您选择线路和团队是否同时上下架。</td>
           </tr>
           <tr>
             <td align="left"><strong>我的本月价格变了如何快速修改？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：点击计划管理内的功能链接即可。</td>
           </tr>
           <tr>
             <td align="left"><strong>我只是想查看和管理单个线路的团队计划怎么办？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：点击有效班次的链接，可进入该线路所有有效计划的团队列表界面，您在那个界面可以对单团游程和情况进行调整。</td>
           </tr>
           <tr>
             <td align="left"><strong>删除线路会影响该线路的团队计划吗？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：不会影响，如果删除线路后，需要把团队也删除了，请前往团队管理功能页面，逐个删除。</td>
           </tr>
           
         </table>--%>
       </div>
     </div>
<script type="text/javascript">
    var RouteView = {
        Itin: function() {
            var form = $("#<%=Key %>");
            //全选
            form.find("#chk_All").click(function() {
                RouteView.AllChk(this);
            })
            //修改
            form.find(".Update").click(function() {
                RouteView.Update(this);
                return false;
            })
            //复制
            form.find(".Copy").click(function() {
                RouteView.Copy(this);
                return false;
            })
            //删除
            form.find(".Del").click(function() {
                RouteView.Del($(this).closest("tr").attr("routeid"));
                return false;
            })
            //添加或修改计划
            form.find(".AddorUpdate").click(function() {
                RouteView.AddOrUpdate(this);
                return false;
            })
            //上下价
            form.find(".RouteStatus").click(function() {
                if (confirm("确定要对 " + $(this).closest("tr").find("td:eq(1)").text() + " 进行 " + $(this).text() + " ？")) {
                    RouteView.Set("RouteStatus", $(this).attr("val"), $(this).closest("tr").attr("routeid"));
                }
                return false;
            })
            //分页
            form.find("#ExportPageInfo a").click(function() {
                RouteView.BindGoUrl(this);
                return false;
            })
            //计划班次
            form.find("#tab_list .a_goPlanList").click(function() {
                topTab.open($(this).attr("href"), $(this).attr("title"), {})
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

        },
        AllChk: function(obj) {
            var form = $("#<%=Key %>");
            form.find("#tab_list :checkbox:not(#chk_All)").attr("checked", $(obj).attr("checked"))
        },
        BindGoUrl: function(obj) {
            topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
            return false;
        },
        //设置选中专线
        SetSelectLine: function(obj) {
            var iLine1 = $("#<%=Key %>" + "_tab_ILine");
            iLine1.find(".select").removeClass("select");
            iLine1.find("[lineid='" + $(obj).val() + "']").addClass("select");
        },
        GetList: function() {
            var form = $("#<%=Key %>");
            form.find("#tab_list").html('<div id="div_load"><img src="<%= ImageServerPath%>/images/default/tree/loading.gif" alt="加载中......" />加载中......</div>')
            //查询参数对象
            var SelectData = {
                keyWord: "", //关键字
                goCity: "", //出发地
                lineId: "", //专线id
                lineType: "", //专线类别
                companyID: "", //公司id
                routeSource: ""
            }
            SelectData.routeSource = '<%=(int)routeSource %>';
            SelectData.companyID = '<%=SiteUserInfo.CompanyID %>'
            var tab_ILine_select = form.find("#<%=Key %>_tab_ILine .select");
            form.find("#<%=ddl_ZX.ClientID %>").val(tab_ILine_select.attr("lineid"));
            //关键字
            SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
            //出发地
            SelectData.goCity = $.trim(form.find("#<%=ddl_goCity.ClientID %>").val())
            var tab_ILine_select = form.find("#<%=Key %>_tab_ILine .select");
            //专线id
            SelectData.lineId = tab_ILine_select.attr("lineid");
            //专线类型
            SelectData.lineType = tab_ILine_select.attr("linetype");
            //旅游天数
            SelectData.travelDays = form.find("#tab_travelDays .select").attr("val");
            $.newAjax({
                type: "get",
                url: "/routeagency/routemanage/ajaxrouteview.aspx",
                data: SelectData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    form.find("#tab_list").html(html);
                    RouteView.Itin();
                }, error: function() {
                    alert("获取异常-重新加载");
                    topTab.url(topTab.activeTabIndex, "/routeagency/routemanage/routeview.aspx");
                }
            })
        },
        Set: function(settype, setvalue, ids) {
            var form = $($("#<%=Key %>"));
            var url = "/RouteAgency/RouteManage/RouteView.aspx"
            var SetData = {
                ids: ids, //id列表
                SetType: settype, //修改类型（线路状态or推荐状态）
                SetValue: setvalue, //修改值
                RouteSource: ""//操作来源(专线or地接)
            }
            var msg = "设置";
            switch (settype) {
                case "Del":
                    msg = "删除";
                    break;
                case "RecommendType":
                    msg += "推荐状态";
                    break;
                case "RouteStatus":
                    msg += "上下架状态";
                    break;
            }
            SetData.RouteSource = '<%=(int)routeSource %>';
            $.newAjax({
                type: "get",
                url: url,
                data: SetData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    if (html.toLowerCase() == "true") {
                        alert(msg + "成功！")
                        RouteView.GetList();
                    }
                    else {
                        alert(msg + "失败！")
                    }
                }, error: function() {
                    alert(msg + "异常")
                }
            })
        },
        Update: function(obj) {
            var tr = $(obj).closest("tr")
            var url = $(obj).attr("href") + "?RouteId=" + tr.attr("routeid");
            //url += "&travelRangeName=" + tr.attr("routetype");
            url += "&RouteSource=" + '<%=(int)routeSource %>'
            url += "&type=update";
            topTab.open(url, "修改线路", { isOpen: false });
            return false;
        },
        Copy: function(obj) {
            var tr = $(obj).closest("tr")
            var url = $(obj).attr("href") + "?RouteId=" + tr.attr("routeid");
            //url += "&travelRangeName=" + tr.attr("routetype");
            url += "&RouteSource=" + '<%=(int)routeSource %>'
            url += "&type=copy";
            topTab.open(url, "复制线路", { isOpen: false });
            return false;
        },
        Del: function(ids) {
            if (confirm("确定删除该数据？")) {
                RouteView.Set("Del", "", ids);
            }
        },
        AddOrUpdate: function(obj) {
            var url = encodeURI($(obj).attr("href"));
            topTab.open(url, "批量添加或修改计划", {});
            return false;
        },
        Note: function(obj) {
            var form = $("#<%=Key %>");
            var id = "";
            if (form.find(".list_id:checked").length > 1) {
                alert("只能选择一条数据!")
                return false;
            }
            else if (form.find(".list_id:checked").length <= 0) {
                alert("请选择一条数据!")
                return false;
            }
            else {
                id = form.find(".list_id:checked").closest("tr").find(".list_id:checked").val();
            }
            topTab.open($(obj).attr("href") + "&id=" + id, "短信推广", {});
        }
    }
    $(function() {
        var form = $("#<%=Key %>");
        //推荐状态
        form.find("#tab_type a").click(function() {
            var str = "";
            form.find("#tab_list .list_id:checked").each(function() {
                str += $(this).attr("value") + ",";
            })
            if (str.length > 0) {
                if (confirm("确定要将选中的线路 进行 " + $(this).text() + " 设置？")) {
                    RouteView.Set("RecommendType", $(this).attr("val"), str.substring(0, str.length - 1));
                }
            }
            else {
                alert("请先选择数据！");
            }
            return false;
        })
        //上下架状态
        form.find("#div_status a").click(function() {
            var str = "";
            form.find("#tab_list .list_id:checked").each(function() {
                str += $(this).attr("value") + ",";
            })
            if (str.length > 0) {
                if (confirm("确定要将选中的线路数据 进行 " + $(this).text() + " 设置？")) {
                    RouteView.Set("RouteStatus", $(this).attr("val"), str.substring(0, str.length - 1));
                }
            }
            else {
                alert("请先选择数据！");
            }
            return false;

        })
        //专线下拉效果
        form.find("#<%=ddl_ZX.ClientID %>").change(function() {
            RouteView.SetSelectLine(this);
        })
        //查询按钮
        form.find("#a_Select").click(function() {
            RouteView.GetList();
        })
        //关键字
        form.find("#<%=txt_keyWord.ClientID %>")
        .focus(function() {
            if ($.trim($(this).val()) == "线路名称") {
                $(this).val("");
            }
        })
        .blur(function() {
            if ($.trim($(this).val()).length == 0) {
                $(this).val("线路名称");
            }
        })

        form.find("#<%=txt_keyWord.ClientID %>").keypress(function(e) {
            if (document.all) e = event;
            if (e.keyCode == 13) {
                RouteView.GetList();
                return false;
            }
        })
        form.find("#a_note").click(function() {
            RouteView.Note(this);

            return false;
        })
        RouteView.Itin();
        //回车查询效果
        form.find(".keydownSelect").keydown(function(e) {
            if (e.keyCode == 13) {
                RouteView.GetList();
                return false;
            } else {
                return true;
            }
        });
        return false;
    })
</script>
</asp:content>
