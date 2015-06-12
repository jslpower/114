<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AddTourAgain.aspx.cs" Inherits="UserBackCenter.RouteAgency.AddTourAgain" %>

<%@ Register Src="~/usercontrol/RouteAgency/TourPriceStand.ascx" TagName="TourPriceStand"
    TagPrefix="cc1" %>
<asp:content id="AddTourAgain" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
commonTourModuleData.add({
    ContainerID:'<%=ThisTableContainerID %>',
    ReleaseType:'AddTourAgain'
});
</script>
        <table border="0" id="<%=ThisTableContainerID %>" cellpadding="0" cellspacing="0" class="tablewidth">
            <tr>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                        <tr>
                            <td width="2%">
                                &nbsp;
                            </td>
                            <td width="98%" align="left">
                                <img src="<%=ImageServerUrl %>/images/zaifabu.gif" width="88" height="36" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin15" style="border-bottom: 2px solid #FF5500;">
                        <tr>
                            <td width="17%">
                                <div class="xianluon2">
                                    <strong><a href="javascript:void(0)"><%=AreaName%></a></strong></div>
                            </td>
                            <td width="83%" align="left" valign="bottom">
                            <asp:Literal runat="server" id="ltrAddNewPlan"></asp:Literal>
                               
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="xianluhangcx"
                        style="line-height: 10px; padding: 0px; border: 1px solid #ccc; border-bottom: 0px;"
                        height="10">
                        <tr>
                            <td width="66%" align="left" style="padding-left: 65px;">
                                <strong>团队基本信息</strong>
                            </td>
                            <td width="21%" align="center">
                                <strong>成人价/儿童价</strong>
                            </td>
                            <td width="13%" align="center">
                                <strong>操作明细表</strong>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0" style="border: 1px solid #D8D8D8;
                        text-align: left;">
                        <tr bgcolor="#FFFFFF">
                            <td width="489" style="border-bottom: 1px dashed #ccc; height: 50px; padding-top: 5px;">
                                <strong>
                                    <img src="<%=ImageServerUrl %>/images/ico.gif" width="11" height="11" /><a href="/PrintPage/TeamInformationPrintPage.aspx"
                                        target="_blank" class="lan14"><%=RouteName%></a></strong><br />
                                &nbsp;&nbsp;<span class="danhui">最近一班：</span><span class="huise"><%=LeaveDate %>(<%=EyouSoft.Common.Utils.ConvertWeekDayToChinese(LeaveTime)%>)/</span><span
                                    class="chengse"><strong>剩:<%=RemnantNumber %></strong></span>&nbsp;&nbsp; 
                            </td>
                            <td width="150" style="border-bottom: 1px dashed #ccc; line-height: 18px;">
                                门市价：<span class="chengse">￥<%=PeerAdultPrice %></span>/<%=PeerChildrenPrice%><br />
                                同行价：<span class="chengse">￥<%=StoreAdultPrice%></span>/<%=StoreChildrenPrice%>
                            </td>
                            <td width="94" align="center" style="border-bottom: 1px dashed #ccc; line-height: 14px;">
                                <a href="javascript:void(0)" id="a_detail">
                                    <img src="<%=ImageServerUrl %>/images/jihuab.gif" alt="点击查看发班计划" width="30" height="28" border="0" /></a>
                            </td>
                        </tr>
                    </table>
                    <table border="1" align="center" cellpadding="4" cellspacing="1" style="border: 1px solid #ECECEC;
                        background: #FAFAFA; margin: 8px 0px 10px 0px; width: 99%;">
                        <tr>
                            <td colspan="2" align="left">
                                继续发布“<strong><%=RouteName%></strong>”更多发团计划
                            </td>
                        </tr>
                        <tr>
                            <td width="11%" align="right">
                                <span class="ff0000">*</span>人数：
                            </td>
                            <td align="left">
                                <input name="VistorNum" id="txtVistorNum" type="text"  valid="required|RegInteger" errmsg="请填写计划人数！|计划人数只能是正整数！" class="bitian" value="" size="15" />
                                <span id="errMsg_txtVistorNum" style=" color:Red;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#FBFFFB">
                                <span class="ff0000">*</span>出团日期：
                            </td>
                            <td align="left" bgcolor="#FBFFFB">
                                <a href="javascript:void(0)" id="a_selectTourDate">
                                    <img src="<%=ImageServerUrl %>/images/index_22.gif" width="21" height="14" /><span style="font-size: 14px;
                                        font-weight: bold">请选择时间</span></a>
                                        <input type="hidden" id="hidTourLeaveDate" name="hidTourLeaveDate" valid="required" errmsg="请选择出团日期！" />
                                        <input type="hidden" id="TourLeaveDate" value="" name="TourLeaveDate" />
                                        <input type="hidden" value="" id="AddQuickTour_hidTourNo" name="AddQuickTour_hidTourNo" runat="server" />
                                        <input type="hidden" id="AddTourAgain_hidChildLeaveDateList" name="AddTourAgain_hidChildLeaveDateList" runat="server" />
                                        <input type="hidden" id="AddTourAgain_hidChildTourCodeList" name="AddTourAgain_hidChildTourCodeList" runat="server" />
                                        
                            </td>
                        </tr> 
                    </table>
                    <cc1:TourPriceStand ID="AddTourAgain_tourpricestand" runat="server" ReleaseType="AddTourAgain" ModuleType="tour" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="2">
                                <h2 style="line-height: 200%;">
                                    本操作是在原计划上追加发团计划，行程及服务标准不变！</h2>
                            </td>
                        </tr>
                        <tr>
                            <td width="28%">
                                &nbsp;
                            </td>
                            <td width="72%">                                
                                <a href="JavaScript:void(0)" id="a_AddTourAgain_Save" class="xiayiyec">提交发布</a><div id="div_AddTourAgain_saveinfo" style="display:inline; color:Red; text-align:left;"></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table><input id="hidTourID" value="<%=TourID %>" type="hidden" />
        <script language="javascript" type="text/javascript">
            var AddTourAgain = {
                RouteArea: "<%=RouteArea %>",
                Url: function(url) {
                    topTab.url(topTab.activeTabIndex, url);
                    return false;
                },
                update: function() {
                if (ValiDatorForm.validator($("#<%=ThisTableContainerID %>").closest("form").get(0), "alertspan") && this.CheckData('<%=ThisTableContainerID %>')) {
                        var tourId = $("#hidTourID").val();
                        var VistorNum = $("#txtVistorNum").val()
                        var TourLeaveDate = $("#hidTourLeaveDate").val();
                        var dataArr = { TourID: tourId, VistorNum: VistorNum, TourLeaveDate: TourLeaveDate };
                        $("#a_AddTourAgain_Save").hide();
                        $("#div_AddTourAgain_saveinfo").html("正在保存再发布...").show();
                        $.newAjax({
                            type: "POST",
                            url: "/RouteAgency/AddTourAgain.aspx?action=update&" + $.param(dataArr),
                            data: $($("#<%=ThisTableContainerID %>").closest("form").get(0)).serializeArray(),
                            success: function(msg) {
                                $("#a_AddTourAgain_Save").show(500);
                                var returnMsg = eval(msg);
                                if (returnMsg) {
                                    alert(returnMsg[0].ErrorMessage)
                                    if (returnMsg[0].isSuccess) {                                        
                                        AddTourAgain.Url("/routeagency/notstartingteams.aspx?TemplateTourID=<%=TemplateTourID %>&AreaId=" + AddTourAgain.RouteArea);
                                    } else {
                                    $("#div_AddTourAgain_saveinfo").html(returnMsg[0].ErrorMessage);
                                    }
                                } else {
                                    alert("对不起，发布失败，请重新发布！")
                                }
                            }
                        });
                    }
                    return false;
                },
                dialog: function(title, url, width, height, data) {
                    Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: data });
                },
                OpenDateDialog: function() {
                    var tmpAreaVal = "<%=RouteArea %>";
                    var url = "/RouteAgency/SelectChildTourNo.aspx?ReleaseType=AddTourAgain&AreaValue=" + tmpAreaVal + "&ContaierID=<%=ThisTableContainerID %>";
                    this.dialog('选择出团时间', url, 850, 300, null);
                },
                _getData: function(id) {
                    return commonTourModuleData.get(id);
                },
                CheckData: function(id) {
                    var obj = this._getData(id);
                    //提交时检查是否选择相同的报价等级
                    var ckPriceList = new Array();
                    $("#" + obj.ContainerID + "tblPriceStand").find("select[name='drpPriceRank']").each(function(i) {
                        ckPriceList.push($(this).val());
                    });

                    var arrMessage = new Array();
                    var isHave = false;
                    var Newarr = ckPriceList.join(",") + ",";
                    for (var i = 0; i < Newarr.length; i++) {
                        if (Newarr.replace(ckPriceList[i] + ",", "").indexOf(ckPriceList[i] + ",") > -1) {
                            isHave = true;
                        }
                    }
                    if (ckPriceList.toString() == "") {
                        arrMessage.push("请设置报价等级!\n");
                    }
                    if (isHave) {
                        arrMessage.push("不能选择相同的报价等级!\n");
                    }
                    //是否有填成人价
                    var isTrue = true;
                    var isNull = true;
                    $("#" + obj.ContainerID + "tblPriceStand").find("input[type='text'][name^='PeoplePrice'].bitiansm").each(function() {
                        var People = $.trim($(this).val());
                        if (People == "" || People == '成人价') {
                            isNull = false;
                            return false;
                        }
                        if (isNaN(parseInt(People)) || parseInt(People) < 1) {
                            isTrue = false;
                            return false;
                        }
                    });
                    if (!isNull) {
                        arrMessage.push("请填写同行和门市价的成人价格!\n");
                    }
                    if (!isTrue) {
                        arrMessage.push("请填写正确的同行和门市价的成人价格!\n");
                    }
                    if (arrMessage.length > 0) {
                        alert(arrMessage.join(""));
                        return false;
                    }
                    return true;
                }

            };
            $(document).ready(function(){                
                $("#<%=ThisTableContainerID %> a[rel='TapAddTourAgain']").click(function(){
                    topTab.open($(this).attr("href"),"追加计划",{isRefresh:false});
                    return false;
                })
                $("#<%=ThisTableContainerID %> #a_detail").click(function(){
                    AddTourAgain.Url("/routeagency/NotStartingTeamsDetail.aspx?TemplateTourID=<%=TemplateTourID %>");
                    return false;
                });
                $("#<%=ThisTableContainerID %> #a_selectTourDate").click(function(){
                     var dataArr={ReleaseType:'AddTourAgain'};
                    AddTourAgain.OpenDateDialog();
                     return false;
                })
                $("#a_AddTourAgain_Save").click(function(){
                    AddTourAgain.update()
                });
                FV_onBlur.initValid($("#a_Save").closest("form").get(0));
            });
           
        
        </script>
</asp:content>