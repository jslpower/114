<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderUpdate.aspx.cs" Inherits="UserBackCenter.Order.TourAgency.OrderUpdate" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/UserOrder/OrderCustomer.ascx" TagName="OrderCustomer"
    TagPrefix="uc1" %>
<asp:content id="ScatterPlan" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'OrderUpdate'
    });
</script>

<div id="<%=tblID %>" class="tablebox">
    <!--添加信息表格-->
    <table width="100%" border="0" align="center">
        <tbody>
            <tr>
                <td valign="top" align="left" class="ftxt">
                    <table cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center"
                        style="width: 100%;">
                        <tbody>
                            <tr>
                                <td valign="top" align="left" colspan="2">
                                    <img src="<%=ImageServerUrl %>/images/jiben3.gif">
                                </td>
                            </tr>
                             <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>订单编号：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblOrder"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>线路名称：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblRouteName"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>出团日期：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblLeaveDate"></asp:label>
                                    <asp:Literal runat="server" id="lbPrice_d"></asp:Literal>
                                    <strong>空位：<asp:label runat="server" text="" id="lblCount"></asp:label></strong>&nbsp;
                                    <strong>状态：</strong><asp:label runat="server" text="" id="lblTourState"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td width="18%" bgcolor="#F2F9FE" align="right">
                                    <strong>发布单位：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblCompanyName"></asp:label>
                                    <strong>MQ：</strong>
                                    <asp:label runat="server" text="" id="lblMq"></asp:label>
                                    <strong>QQ：</strong>
                                    <asp:label runat="server" text="" id="lblQQ"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>出发交通和城市：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblLeaveCityandTracffic"></asp:label>
                                    <strong>出发航班:</strong>
                                    <asp:Label runat="server" Text="" id="lbLeavedate"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>返程交通和城市：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lbEndCityandTracffic"></asp:label>
                                    <strong>返程航班:</strong>
                                    <asp:Label runat="server" Text="" id="lbEnddate"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>集合说明：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblMsg"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>领队全陪说明：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblAllMsg"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>游客联系人：</strong>
                                </td>
                                <td align="left">
                                    <input id="txtContact" name="txtContact" value="" runat="server">
                                    联系电话
                                    <input id="txtConTactTel" name="txtConTactTel" value="" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>组团社联系人：</strong>
                                </td>
                                <td align="left">
                                    <input id="txtFzr" name="txtFzr" runat="server">
                                    联系电话
                                    <input id="txtFzrTel" name="txtFzrTel" runat="server">
                                    【预定的时候默认取出预订者的信息，但是可以修改】
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>价格组成：</strong>
                                </td>
                                <td align="left">
                                    成人
                                    <input id="txtAdultCount" name="txtAdultCount" runat="server">
                                    人，儿童
                                    <input id="txtChildCount" name="txtChildCount" runat="server">
                                    人，单房差
                                    <input id="txtOtherCount" name="txtOtherCount" runat="server">人
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc1:OrderCustomer ID="OrderCustomer1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>市场价：<br>
                                        结算价：<br></strong>
                                </td>
                                <td align="left">
                                    成人
                                    <asp:label runat="server" text="" id="lblRetailAdultPrice"></asp:label>
                                    元，儿童<asp:label runat="server" text="" id="lblRetailChildrenPrice"></asp:label>
                                    元，单房差
                                    <asp:label runat="server" text="" id="lblMarketPrice"></asp:label>
                                    元，增减销售价
                                    <input id="txtAddPrice" name="txtAddPrice" runat="server">
                                    元
                                    <br>
                                    成人
                                    <asp:label runat="server" text="" id="lblSettlementAudltPrice"></asp:label>
                                    元，儿童
                                    <asp:label runat="server" text="" id="lblSettlementChildrenPrice"></asp:label>
                                    元, 增减结算价
                                    <input id="txtReductPrice" name="txtReductPrice" disabled="disabled" runat="server"><br />
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>游客备注：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblCusRemark"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>组团社备注：</strong>
                                </td>
                                <td align="left">
                                    <textarea rows="4" cols="85" id="txtRemark" runat="server"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>专线备注：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblRouteRemarks"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>订单状态：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblOrderState"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>支付状态：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblPayState"></asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label runat="server" id="pnlLitPay">
                                    <a class="basic_btn" ref="1" id="btnLitPay" href="javascript:void(0);"><span>已收定金</span></a>
                                    </asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label runat="server" id="pnlAllPay">
                                    <a class="basic_btn" ref="2" id="btnAllPay" href="javascript:void(0);"><span>已收全款</span></a>
                                    </asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label runat="server" id="pnlTuiKuan">
                                    <a class="basic_btn" ref="3" id="btnTuiKuan" href="javascript:void(0);"><span>专线已退款</span></a>
                                    </asp:label>
                                </td>
                            </tr>
                            <tr sizset="14" sizcache="0">
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>总金额：</strong>
                                </td>
                                <td align="left" sizset="14" sizcache="0">
                                    销售价<asp:label runat="server" text="" id="lblReailPriceAll"></asp:label>
                                    &nbsp; 结算价
                                    <asp:label runat="server" text="" id="lblSettlePriceAll"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>下单时间：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblAddDate"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:label runat="server" id="pnlSave">
                                    <a class="baocun_btn" href="javascript:void(0);" id="btnSave" ref="<%=tblID %>">保 存</a>&nbsp;
                                    </asp:label>
                                    <asp:label runat="server" id="pnlCanel">
                                     <a class="baocun_btn" ref="4" id="btnCanel"  href="javascript:void(0);">订单取消</a>
                                     </asp:label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</div>

<script type="text/javascript">
    var OrderByTour = {
        UpdatePrice: function(ref) {
            var obj = $("#" + OrderByTour._getData(ref).ContainerID);
            var adultCount = parseInt(obj.find("[id$=<%=txtAdultCount.ClientID %>]").val());  //成人数
            var childCount = parseInt(obj.find("[id$=<%=txtChildCount.ClientID %>]").val());  //儿童数
            var otherCount = parseInt(obj.find("[id$=<%=txtOtherCount.ClientID %>]").val());  //单房差
            var reAdultPrice = parseInt(obj.find("[id$=<%=lblRetailAdultPrice.ClientID %>]").html());  //成人市场价
            var reChilePrice = parseInt(obj.find("[id$=<%=lblRetailChildrenPrice.ClientID %>]").html()); //儿童市场价
            var reOtherPrice = parseInt(obj.find("[id$=<%=lblMarketPrice.ClientID %>]").html());    //单房差价格
            var seAdultPrice = parseInt(obj.find("[id$=<%=lblSettlementAudltPrice.ClientID %>]").html()); //成人结算价
            var seChildPrice = parseInt(obj.find("[id$=<%=lblSettlementChildrenPrice.ClientID %>]").html()); //儿童结算价
            var addPrice = parseInt(obj.find("[id$=<%=txtAddPrice.ClientID %>]").val());  //增减销售价
            var reductPrice = parseInt(obj.find("[id$=<%=txtReductPrice.ClientID %>]").val());  //增减结算价
            //处理错误数据
            if (!adultCount > 0) { adultCount = 0; }
            if (!childCount > 0) { childCount = 0; }
            if (!otherCount > 0) { otherCount = 0; }
            if (!reAdultPrice > 0) { reAdultPrice = 0; }
            if (!reChilePrice > 0) { reChilePrice = 0; }
            if (!reOtherPrice > 0) { reOtherPrice = 0; }
            if (!seAdultPrice > 0) { seAdultPrice = 0; }
            if (!seChildPrice > 0) { seChildPrice = 0; }
            if (!addPrice > 0) { addPrice = 0; }
            if (!reductPrice > 0) { reductPrice = 0; }

            //市场总价
            var rePriceAll = adultCount * reAdultPrice + childCount * reChilePrice + otherCount * reOtherPrice + addPrice;
            rePriceAll = parseInt(rePriceAll * 100) / 100;
            //结算总价
            var sePriceAll = adultCount * seAdultPrice + childCount * seChildPrice + reductPrice;
            sePriceAll = parseInt(sePriceAll * 100) / 100;

            obj.find("[id$=<%=lblReailPriceAll.ClientID %>]").html(rePriceAll);
            obj.find("[id$=<%=lblSettlePriceAll.ClientID %>]").html(sePriceAll);
        },
        SubmitForm: function(ref) {
            var obj = $("#" + OrderByTour._getData(ref).ContainerID);
            var adultCount = parseInt($.trim(obj.find("[id$=<%=txtAdultCount.ClientID %>]").val()));
            if (!adultCount > 0) {
                alert("请输入成人数!");
                return;
            }
            obj.find("[id$=btnSave]").html("提交中..");
            obj.find("[id$=btnSave]").unbind();

            $.newAjax({
                type: "POST",
                url: '/Order/TourAgency/OrderUpdate.aspx?orderID=<%=Request.QueryString["orderID"] %>&dotype=save&v=' + Math.random(),
                data: obj.find("[id$=btnSave]").closest("form").serialize(),
                cache: false,
                success: function(state) {
                    if (state == "ok") {
                        alert("订单修改成功!");
                        topTab.url(topTab.activeTabIndex, '/Order/TourAgency/OrderUpdate.aspx?orderID=<%=Request.QueryString["orderID"] %>');
                    } else {
                        alert(state);
                        obj.find("[id$=btnSave]").html("保 存");
                        obj.find("[id$=btnSave]").click(function() {
                            OrderByTour.SubmitForm($(this).attr("ref"));
                        })
                    }
                },
                error: function() {
                    alert("服务器忙,请稍后再试!");
                    obj.find("[id$=btnSave]").html("保 存");
                    obj.find("[id$=btnSave]").click(function() {
                        OrderByTour.SubmitForm($(this).attr("ref"));
                    })
                }
            });
        },
        UpdateOrderState: function(state) {
            $.newAjax({
                type: "POST",
                url: '/Order/TourAgency/OrderUpdate.aspx?orderID=<%=Request.QueryString["orderID"] %>&dotype=state&state=' + state + '&v=' + Math.random(),
                cache: false,
                success: function(state) {
                    if (state == "ok") {
                        alert("状态修改成功!");
                        topTab.url(topTab.activeTabIndex, '/Order/TourAgency/OrderUpdate.aspx?orderID=<%=Request.QueryString["orderID"] %>');
                    } else {
                        alert(state);
                    }
                },
                error: function() {
                    alert("服务器忙,请稍后再试!");
                }
            });
        },
        _getData: function(id) {
            return commonTourModuleData.get(id);
        }
    }


    $(function() {
        var obj = $("#" + OrderByTour._getData('<%=tblID %>').ContainerID);
        obj.find("[id$=btnSave]").click(function() {
            OrderByTour.SubmitForm($(this).attr("ref"));
        })
        obj.find("#<%=txtAdultCount.ClientID %>,#<%=txtChildCount.ClientID %>,#<%=txtOtherCount.ClientID %>,#<%=txtAddPrice.ClientID %>,#<%=txtReductPrice.ClientID %>").blur(function() {

            OrderByTour.UpdatePrice('<%=tblID %>');
        })

        obj.find("#btnLitPay,#btnAllPay,#btnTuiKuan,#btnCanel").click(function() {
            if (confirm("确定执行该操作?")) {
                OrderByTour.UpdateOrderState($(this).attr("ref"));
            }
            return false;
        })

    })
</script>
</asp:content>
