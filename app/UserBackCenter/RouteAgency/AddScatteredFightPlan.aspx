<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="AddScatteredFightPlan.aspx.cs" Inherits="UserBackCenter.RouteAgency.AddScatteredFightPlan" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:Content ID="AddScatteredFightPlan" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'AddScatteredFightPlan'
        });
    </script>

    <style type="text/css">
        </style>
    <div id="<%=Key %>" class="right Max">
        <div class="tablebox">
            <input type="hidden" id="hd_routeid" runat="server" />
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <a href="javascript:void(0);">
                            <asp:label runat="server" id="lbl_lineName" text=""></asp:label>
                        </a>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr style="background: url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                    <td width="1%" height="30" align="left">
                        &nbsp;
                    </td>
                    <td width="99%" align="left">
                        <span class="search">&nbsp;</span>团号：
                        <input id="txt_TourId" size="15" runat="server" />
                        出团日期：
                        <input id="txt_selectStartDate" type="text" size="15" runat="server" onfocus="WdatePicker();" />
                        至
                        <input id="txt_selectEndDate" type="text" size="15" runat="server" onfocus="WdatePicker();" />
                        <button type="button" class="search-btn" id="btn_Select">
                            搜索</button>
                    </td>
                </tr>
            </table>
            <div id="div_Tlist">
                <table id="tab_Tlist" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                    style="width: 100%; margin-top: 1px;">
                    <tr class="list_basicbg">
                        <th class="list_basicbg">
                            选择
                        </th>
                        <th class="list_basicbg">
                            团号
                        </th>
                        <th class="list_basicbg">
                            出团日期
                        </th>
                        <th class="list_basicbg" style="width: 85px">
                            报名截止
                        </th>
                        <th class="list_basicbg" style="width: 35px">
                            人数
                        </th>
                        <th class="list_basicbg" style="width: 35px">
                            余位
                        </th>
                        <th class="list_basicbg" style="width: 35px">
                            留位
                        </th>
                        <th class="list_basicbg" style="width: 60px">
                            状态
                        </th>
                        <th class="list_basicbg" style="width: 125px">
                            成人(市/结)
                        </th>
                        <th class="list_basicbg" style="width: 125px">
                            儿童(市/结)
                        </th>
                        <th class="list_basicbg" style="width: 55px">
                            单房差
                        </th>
                        <th class="list_basicbg" style="width: 70px">
                            功能
                        </th>
                    </tr>
                    <asp:repeater runat="server" id="rpt_Tlist">
                        <ItemTemplate>
                        <tr style="height:30px" val="<%#Eval("TourId") %>">
                            <td align="center">
                               <input type="checkbox" class="chk_select" orderoeoplenum="<%#Eval("OrderPeopleNum")%>"  value="<%#Eval("TourId") %>"  />
                            </td>
                            <td align="center">
                                <font class="huise"><%#Eval("TourNo")%></font>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <font class="huise"><label style="display:inline" class="lbl_LeaveDate"><%#((DateTime)Eval("LeaveDate")).ToString("yyyy-MM-dd")%></label></font>
                            </td>
                            <td align="center" reuval="<%#((DateTime)Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>">
                               <input class="txt_registrationEndDate Uptxt" onfocus="WdatePicker({onpicked:function(){$(this).closest('tr').find('input.chk_select:not(:disabled)').attr('checked', 'checked');},maxDate:'<%#((DateTime)Eval("LeaveDate")).ToString("yyyy-MM-dd")%>'});" type="text" size="11" value="<%# ((DateTime)Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>" />
                            </td>
                            <td align="center" reuval="<%#Eval("TourNum")%>">
                               <input class="txt_orderPeopleNum Uptxt" style="width:30px" type="text" size="4"value="<%#Eval("TourNum")%>" />
                            </td>
                            <td align="center" reuval="<%#Eval("MoreThan")%>">
                               <input class="txt_moreThan Uptxt" style="width:30px" type="text"  size="4"value="<%#Eval("MoreThan")%>" />
                            </td>
                            <td align="center" reuval="<%#Eval("SaveNum")%>">
                                <input class="txt_saveNum Uptxt" style="width:30px" type="text"  size="4"value="<%#Eval("SaveNum")%>" />
                            </td>
                            <td align="center" reuval="<%#(int)Eval("PowderTourStatus")%>">
                                <%=PowderTourStatusStr%>
                            </td>
                            <td align="center">
                                    <input class="txt_retailAdultPrice Uptxt" isdecimal="1"style="width:40px; text-align:right" type="text" value="<%# Utils.GetDecimal(Eval("RetailAdultPrice").ToString()).ToString("0.##")%>" size="5" />
                               |
                                    <input class="txt_settlementAudltPrice Uptxt"  isdecimal="1"style="width:40px" type="text" value="<%# Utils.GetDecimal(Eval("SettlementAudltPrice").ToString()).ToString("0.##")%>" size="5" />
                            </td>
                            <td align="center">
                                    <input class="txt_retailChildrenPrice Uptxt" isdecimal="1" style="width:40px;text-align:right" type="text" value="<%# Utils.GetDecimal(Eval("RetailChildrenPrice").ToString()).ToString("0.##")%>" size="5" />
                                |
                                    <input class="txt_settlementChildrenPrice Uptxt" isdecimal="1"style="width:40px" type="text" value="<%# Utils.GetDecimal(Eval("SettlementChildrenPrice").ToString()).ToString("0.##")%>" size="5" />
                                
                            </td>
                            <td align="center" reuval="<%# Utils.GetDecimal(Eval("MarketPrice").ToString()).ToString("0.##")%>">
                                <input class="txt_marketPrice Uptxt" isdecimal="1" style="width:50px" type="text" value="<%# Utils.GetDecimal(Eval("MarketPrice").ToString()).ToString("0.##")%>" size="6" />
                            </td>
                            <td align="center">
                                <div class="div_UpdateSave" style="display:inline">
                                    <a href="javascript:void(0);" class="UpdateSave">保存</a>|</div><a style="display:<%#(int)Eval("OrderPeopleNum")<=0?"":"none"%>" class="a_Delect"href="javascript:void(0);">删除</a><a style="display:<%#(int)Eval("OrderPeopleNum")>0?"":"none"%>" class="a_goAllFITOrders" href="/RouteAgency/AllFITOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:repeater>
                    <tr>
                        <td>
                            <input type="checkbox" id="chk_allSelect" />
                        </td>
                        <td colspan="8" align="left">
                            &nbsp; <a href="javascript:void(0);" id="btn_delect" class="basic_btn"><span>删除</span></a>
                            <asp:repeater runat="server" id="rpt_updateStatus">
                                <ItemTemplate>
                                    <a href="/RouteAgency/AddScatteredFightPlan.aspx?Operating=UpdatePowderTourStatus&TourStatus=<%#Eval("Value") %>" class="basic_btn UpdatePowderTourStatus"><span>线路<%#Eval("Text") %></span></a>
                                </ItemTemplate>
                            </asp:repeater>
                            <a href="javascript:void(0);" id="UpdateSave" class="basic_btn"><span>保存修改</span></a>
                        </td>
                        <td colspan="3" id="td_ExportPageInfo1" class="F2Back" align="center" height="40">
                            <cc1:ExportPageInfo ID="ExportPageInfo1" Visible="false" LinkType="4" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table id="tab_Save" border="0" cellspacing="0" cellpadding="4" style="width: 99%;
                margin-top: 10px;">
                <tr>
                    <td align="left" class="ff0000">
                        ·团队行程其它信息（出团集合时间，领队联系方式），可单独修改，一般在出团前，专线商单独修改用于打印出团单
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="88%" border="1" cellpadding="2" cellspacing="0" bordercolor="#9DC4DC">
                            <tr bgcolor="#C8E6F7">
                                <td colspan="2" align="left">
                                    <b>快速添加同一价格团队计划</b>
                                </td>
                            </tr>
                            <tr>
                                <td id="td_DateTime" width="50%" align="left" valign="top" style="position: relative;">
                                    <iframe scrolling="no" id="iframeLoad" src="/RouteAgency/CalendarIframePage.aspx?areaType=<%= AreaType %>"
                                        width="100%" height="485px" frameborder="0" style="overflow: hidden;"></iframe>
                                </td> 
                                <td width="50%" valign="top">
                                    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#addaf8">
                                        <tr>
                                            <td width="25%" align="right" nowrap="nowrap">
                                                <span class="ff0000">*</span>团队人数：
                                            </td>
                                            <td width="75%" align="left">
                                                <input id="txt_tourNum" type="text" size="10" class="Addtxt" valid="required" errmsg="请填写团队人数" />
                                                <input type="checkbox" id="chk_isLimit" />
                                                不限制报名数 <span id="errMsg_txt_tourNum" class="errmsg"></span>
                                            </td>
                                        </tr>
                                        <tr id="tr_moreThan">
                                            <td align="right">
                                                余位：
                                            </td>
                                            <td align="left">
                                                <input id="txt_moreThan" class="Addtxt" type="text" size="10" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                <span class="ff0000">*</span>成人（市/结）
                                            </td>
                                            <td align="left">
                                                <input id="txt_retailAdultPrice" class="Addtxt" isdecimal="1" type="text" size="10" />
                                                <input id="txt_settlementAudltPrice" class="Addtxt" isdecimal="1" type="text" size="10" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                儿童（市/结）
                                            </td>
                                            <td align="left">
                                                <input id="txt_retailChildrenPrice" class="Addtxt" isdecimal="1" type="text" size="10" />
                                                <input id="txt_settlementChildrenPrice" class="Addtxt" isdecimal="1" type="text"
                                                    size="10" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                单房差：
                                            </td>
                                            <td align="left">
                                                <input class="Addtxt" type="text" size="10" id="txt_marketPrice" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                集合说明：
                                            </td>
                                            <td align="left">
                                                <input id="txt_setDec" type="text" size="25" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                出发班次时间：
                                            </td>
                                            <td align="left">
                                                <input id="txt_startDate" type="text" size="25" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                返回班次时间：
                                            </td>
                                            <td align="left">
                                                <input id="txt_endDate" type="text" size="25" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                线路销售备注：
                                            </td>
                                            <td align="left">
                                                <input id="txt_tourNotes" type="text" size="25" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap">
                                                领队全陪说明：
                                            </td>
                                            <td align="left">
                                                <textarea id="txt_teamLeaderDec" cols="45" rows="4"></textarea>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <a href="javascript:void(0);" class="tianjia_btn" id="AddSave">添 加</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <a href="/RouteAgency/SetTourNo.aspx" id="a_SetOr">自定义团号生产前缀</a>
                                            </td>
                                        </tr>
                                    </table>
                                    <p align="left" style="padding: 5px;">
                                        ·非必填项可以在后期，发班前单独修改<br />
                                        ·为了方便修改出团单，每个团队的行程是独立的，如果需要统一修改行程，请点击这里，选择要修改的团队，一起修改行程。</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        var AddScatteredFightPlan = {
            OpenBoxy: function(obj) {/*团队规则弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: $(obj).attr("href") + "?rnd" + Math.random(),
                    title: "设置团号规则",
                    modal: true,
                    width: "500",
                    height: "400"
                });
                return false;
            },
            AddSave: function(obj/*保存按钮对象*/) {/*保存*/
                var Key = $("#" + $(obj).closest(".Max").attr("id"));
                var data = {
                    routeid: "", //线路Id
                    leaveDate: "", //出团时间
                    txt_tourNum: "", //团队人数
                    hd_isLimit: "", //是否显示报名人数
                    txt_moreThan: "", //余位
                    txt_retailAdultPrice: "", //成人市场价
                    txt_settlementChildrenPrice: "", //成人结算价
                    txt_retailChildrenPrice: "", //儿童市场价
                    txt_settlementChildrenPrice: "", //儿童结算价
                    txt_marketPrice: "", //单房差
                    txt_setDec: "", //集合说明
                    txt_startDate: "", //出发班次时间
                    txt_endDate: "", //返回班次时间
                    txt_tourNotes: "", //备注
                    txt_teamLeaderDec: ""//领队全陪说明
                };
                /*iframe日历获取选择中日期*/
                Key.find("#iframeLoad")[0].contentWindow.QGD.GetCheckedDate();
                /****验证当天以及不可添加的日期****/
                leaveDateStr = '<%=leaveDateStr %>';
                leaveDatenow = leaveDateStr.substring(0, leaveDateStr.length - 1).split(",").pop();
                var arrLeaveDate = Key.find("#iframeLoad")[0].contentWindow.QGD.config.arrLeaveDate;
                var i = arrLeaveDate.length;
                var msg = "";
                while (i--) {
                    if (leaveDateStr.indexOf(arrLeaveDate[i] + ",") >= 0) {
                        msg += arrLeaveDate[i] + "-已存在计划-\n"
                    }
                    else {
                        data.leaveDate += arrLeaveDate[i] + ",";
                    }
                }
                data.leaveDate = data.leaveDate.substring(0, data.leaveDate.length - 1);
                //data.leaveDate = Key.find("#iframeLoad")[0].contentWindow.QGD.config.arrLeaveDate.join(',');
                if (msg <= 0 && data.leaveDate.length <= 0) {
                    msg += "-请选择出团日期-\n";
                }
                /*****************************************************************************************************/
                data.routeid = Key.closest("form").find("#<%=hd_routeid.ClientID %>").val(); //线路id
                data.txt_tourNum = Key.find("#txt_tourNum").val(); //团队人数
                if (data.txt_tourNum.length <= 0) {
                    msg += "-请输入团队人数-\n";
                }
                data.hd_isLimit = Key.find("#chk_isLimit").attr("checked") ? 0 : 1; //是否显示报名人数
                data.txt_moreThan = Key.find("#txt_moreThan").val(); //余位
                data.txt_retailAdultPrice = Key.find("#txt_retailAdultPrice").val(); //成人市场价
                if (data.txt_retailAdultPrice.length <= 0) {
                    msg += "-请填写成人市场价-\n";
                }
                data.txt_settlementAudltPrice = Key.find("#txt_settlementAudltPrice").val(); //成人结算价
                if (data.txt_settlementAudltPrice.length <= 0) {
                    msg += "-请填写成人结算价-\n";
                }
                data.txt_retailChildrenPrice = Key.find("#txt_retailChildrenPrice").val(); //儿童市场价
                //                if (data.txt_retailChildrenPrice.length <= 0) {
                //                    msg += "-请填写儿童市场价-\n";
                //                }
                data.txt_settlementChildrenPrice = Key.find("#txt_settlementChildrenPrice").val(); //儿童结算价
                //                if (data.txt_settlementChildrenPrice.length <= 0) {
                //                    msg += "-请填写儿童结算价-\n";
                //                }
                data.txt_marketPrice = Key.find("#txt_marketPrice").val(); //单房差
                data.txt_setDec = Key.find("#txt_setDec").val(); //集合说明
                data.txt_startDate = Key.find("#txt_startDate").val(); //出发班次时间
                data.txt_endDate = Key.find("#txt_endDate").val(); //返回班次时间
                data.txt_tourNotes = Key.find("#txt_tourNotes").val(); //备注
                data.txt_teamLeaderDec = Key.find("#txt_teamLeaderDec").val(); //领队全陪说明
                if (msg.length <= 0) {
                    $.newAjax(
	                   {
	                       url: '/RouteAgency/AddScatteredFightPlan.aspx?Operating=AddSave',
	                       data: data,
	                       dataType: "html",
	                       cache: false,
	                       type: "post",
	                       success: function(result) {
	                           if (result.toLowerCase() == "true") {
	                               alert("添加成功！")
	                               topTab.url(topTab.activeTabIndex, encodeURI("/RouteAgency/AddScatteredFightPlan.aspx?routeid=" + Key.closest("form").find("#<%=hd_routeid.ClientID %>").val() + "&routename=" + Key.closest("form").find("#<%=lbl_lineName.ClientID %>").html()));
	                           }
	                           else {
	                               alert("删除失败！")
	                           }
	                       },
	                       error: function() {
	                           alert("操作失败!");
	                       }
	                   });
                }
                else {
                    alert(msg)
                }
            },
            UpSave: function(obj) {/*修改,批量修改已有计划*/
                var str = "";
                var Key = $("#" + $(obj).closest(".Max").attr("id") + " #div_Tlist");
                /*判断批量修改还是单个修改(根据触发保存的按钮判断)*/
                if ($(obj).attr("id") == "UpdateSave") {
                    /*批量修改,取得勾选的复选框的上级tr对象*/
                    Key.find("#tab_Tlist .chk_select:checked").each(function() {
                        str += $.trim(AddScatteredFightPlan.GetData($(this).closest("tr")));
                    })
                }
                else {
                    /*单行修改*/
                    str = AddScatteredFightPlan.GetData(obj);
                }

                if (str.length > 0) {
                    if (confirm("确定修改？")) {
                        $.newAjax(
	                   {
	                       url: '/RouteAgency/AddScatteredFightPlan.aspx?Operating=UpSave',
	                       data: { data: str.substring(0, str.length - 1) },
	                       dataType: "html",
	                       cache: false,
	                       type: "post",
	                       success: function(result) {
	                           if (result.toLowerCase() == "true") {
	                               alert("修改成功!")
	                               topTab.url(topTab.activeTabIndex, encodeURI("/RouteAgency/AddScatteredFightPlan.aspx?routeid=" + Key.closest("form").find("#<%=hd_routeid.ClientID %>").val() + "&routename=" + Key.closest("form").find("#<%=lbl_lineName.ClientID %>").html()));
	                           }
	                           else {
	                               alert("删除失败！")
	                           }
	                       },
	                       error: function() {
	                           alert("操作失败!");
	                       }
	                   });
                    }
                }
                else {

                    alert("请先修改再保存!")
                }
            },
            GetData: function(obj) {
                var returnarray = new Array();
                returnarray.push($(obj).attr("val")); //计划ID
                returnarray.push($(obj).find(".txt_registrationEndDate").val()); //截止时间
                returnarray.push($(obj).find(".txt_orderPeopleNum").val()); //人数
                returnarray.push($(obj).find(".txt_saveNum").val()); //留位
                returnarray.push($(obj).find(".sel_PowderTourStatus").val()); //状态
                returnarray.push($(obj).find(".txt_moreThan").val()); //余位
                returnarray.push($(obj).find(".txt_retailAdultPrice").val()); //成人市场价
                returnarray.push($(obj).find(".txt_settlementAudltPrice").val()); //成人结算价
                returnarray.push($(obj).find(".txt_retailChildrenPrice").val()); //儿童市场价
                returnarray.push($(obj).find(".txt_settlementChildrenPrice").val()); //儿童结算价
                returnarray.push($(obj).find(".txt_marketPrice").val()); //单房差
                return returnarray.join(',') + "|";
            },
            SetIsLimit: function(obj) {
                var Key = $("#" + $(obj).closest(".Max").attr("id"));
                Key.find("#tr_moreThan").css("display", Key.find("#chk_isLimit").attr("checked") ? "none" : "")
            },
            Delect: function(tourIds, obj) {
                var Key = $("#" + $(obj).closest(".Max").attr("id"));
                if (tourIds.length > 0) {
                    if (confirm("确定删除计划？")) {
                        $.newAjax(
	                   {
	                       url: '/RouteAgency/AddScatteredFightPlan.aspx?Operating=Delect&tourIds=' + tourIds,
	                       dataType: "html",
	                       cache: false,
	                       type: "get",
	                       success: function(result) {
	                           if (result.toLowerCase() == "true") {
	                               alert("删除成功！")
	                               topTab.url(topTab.activeTabIndex, encodeURI("/RouteAgency/AddScatteredFightPlan.aspx?routeid=" + Key.closest("form").find("#<%=hd_routeid.ClientID %>").val() + "&routename=" + Key.closest("form").find("#<%=lbl_lineName.ClientID %>").html()));
	                           }
	                           else {
	                               alert("删除失败！")
	                           }
	                       },
	                       error: function() {
	                           alert("操作失败!");
	                       }
	                   });
                    }
                }
                else {
                    alert("请选择计划");
                    return false;
                }
            },
            UpdatePowderTourStatus: function(obj) {
                var Key = $("#" + $(obj).closest(".Max").attr("id"));
                var tourIds = AddScatteredFightPlan.GetTourIds(obj);
                if (tourIds.length > 0) {
                    if (confirm("确定修改订单状态？")) {
                        $.newAjax(
	                       {
	                           url: $(obj).attr("href") + '&tourIds=' + tourIds,
	                           dataType: "html",
	                           cache: false,
	                           type: "get",
	                           success: function(result) {
	                               if (result.toLowerCase() == "true") {
	                                   alert("修改成功！")

	                                   topTab.url(topTab.activeTabIndex, encodeURI("/RouteAgency/AddScatteredFightPlan.aspx?routeid=" + Key.closest("form").find("#<%=hd_routeid.ClientID %>").val() + "&routename=" + Key.closest("form").find("#<%=lbl_lineName.ClientID %>").html()));
	                               }
	                               else {
	                                   alert("删除失败！")
	                               }
	                           },
	                           error: function() {
	                               alert("操作失败!");
	                           }
	                       });
                    }
                }
                else {
                    alert("请选择计划");
                    return false;
                }
            },
            GetTourIds: function(obj) {
                var form = $("#" + $(obj).closest(".Max").attr("id"));
                var tourIds = "";
                form.find(".chk_select:checked").each(function() {
                    tourIds += $(this).val() + "|";
                })
                return tourIds.substring(0, tourIds.length - 1);
            },
            GoUrl: function(obj) {
                topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
                return false;
            },
            Select: function(obj) {
                var Key = $("#" + $(obj).closest(".Max").attr("id"));
                var url = "/RouteAgency/AddScatteredFightPlan.aspx?routeid=" + Key.find("#<%=hd_routeid.ClientID %>").val();
                url += "&tourNo=" + Key.find("#<%=txt_TourId.ClientID %>").val();
                url += "&startDate=" + Key.find("#<%=txt_selectStartDate.ClientID %>").val();
                url += "&endDate=" + Key.find("#<%=txt_selectEndDate.ClientID %>").val();
                topTab.url(topTab.activeTabIndex, url);
                return false;
            }
        }
        $(function() {
            var Key = $("#<%=Key %>");
            FV_onBlur.initValid(Key.find("#AddSave").closest("form").get(0));
            Key.find("#AddSave").click(function() {
                AddScatteredFightPlan.AddSave(this);
                return false;
            })
            //是否显示
            Key.find("#chk_isLimit").click(function() {
                AddScatteredFightPlan.SetIsLimit(this);
            })
            //选中列表中的状态
            Key.find(".sel_PowderTourStatus").each(function() {
                $(this).val(parseInt($(this).closest("td").attr("reuval")))
            })
            //批量删除
            Key.find("#btn_delect").click(function() {
                AddScatteredFightPlan.Delect(AddScatteredFightPlan.GetTourIds(this), this)
                return false
            })
            //列表单个删除
            Key.find(".a_Delect").click(function() {
                AddScatteredFightPlan.Delect($(this).closest("tr").attr("val"), this);
                return false
            })
            //列表全选功能
            Key.find("#chk_allSelect").click(function() {
                Key.find(".chk_select:enabled").attr("checked", $(this).attr("checked"));
                //return false
            })
            //计划状态修改
            Key.find(".UpdatePowderTourStatus").click(function() {
                AddScatteredFightPlan.UpdatePowderTourStatus(this);
                return false
            })
            //批量保存按钮
            Key.find("#UpdateSave").click(function() {
                AddScatteredFightPlan.UpSave(this);
                return false;
            })
            //列表单个保存
            Key.find(".div_UpdateSave").click(function() {
                AddScatteredFightPlan.UpSave($(this).closest("tr"));
                return false;
            })
            //禁用列表上存在订单的计划复选框
            Key.find(".chk_select[orderoeoplenum!='0']").attr("disabled", "disabled");
            //列表分页
            Key.find("#td_ExportPageInfo1 a").click(function() {
                AddScatteredFightPlan.GoUrl(this);
                return false;
            })
            //列表订单跳转(有订单才有订单按钮,否则为删除按钮)
            Key.find(".a_goAllFITOrders").click(function() {
                topTab.open($(this).attr("href"), "所有散拼订单", {});
                return false;
            })
            //查询
            Key.find("#btn_Select").click(function() {
                AddScatteredFightPlan.Select(this);
                return false;
            })
            //自定义团号生产前缀弹窗
            Key.find("#a_SetOr").click(function() {
                AddScatteredFightPlan.OpenBoxy(this);
                return false;
            })
            /*列表效果*/
            Key.find(".Uptxt").keypress(function() {

                $(this).closest("tr").find("input.chk_select:not(:disabled)").attr("checked", "checked");
            })
            //            .mouseover(function() {
            //                $(this).addClass("Uptxt_");
            //                return false;
            //            })
            //            .mouseout(function() {
            //                if ($(this).attr("isfocus") != "true") {
            //                    $(this).removeClass("Uptxt_");
            //                }
            //            })
            //            .focus(function() {
            //                $(this).attr("isfocus", "true");
            //            })
            //            .blur(function() {
            //                $(this).removeAttr("isfocus");
            //                $(this).removeClass("Uptxt_");
            //            })

            Key.find(".sel_PowderTourStatus").change(function() {
                $(this).closest("tr").find("input.chk_select:not(:disabled)").attr("checked", "checked");
            })
            /*列表效果结束*/
        });
    </script>

</asp:Content>
