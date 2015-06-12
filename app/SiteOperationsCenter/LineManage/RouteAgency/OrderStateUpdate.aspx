<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStateUpdate.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.RouteAgency.OrderStateUpdate"  %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/UserOrder/OrderCustomer.ascx" TagName="OrderCustomer"
    TagPrefix="uc1" %>
<asp:content id="OrderStateUpdate" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'OrderStateUpdate',
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
                                    <strong>订单号</strong>：
                                </td>
                                <td align="left">
                                    <asp:label runat="server" id="lblOrderNo" text=""></asp:label>
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
                                    <strong>当前剩余空位：</strong><asp:label runat="server" text="" id="lblCount"></asp:label>
                                    &nbsp;&nbsp;
                                    <strong>状态：</strong><asp:label runat="server" text="" id="lblTourState"></asp:label>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td width="18%" bgcolor="#F2F9FE" align="right">
                                    <strong>预定单位：</strong>
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
                                    <strong>出发城市：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblLeaveCity"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>交通：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblCar"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>出发时间 航班：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblLeaveCon"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>返回时间 航班：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblBackCon"></asp:label>
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
                                    <asp:label runat="server" text="" id="lblContact"></asp:label>
                                    联系电话
                                    <asp:label runat="server" text="" id="lblConTactTel"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F9FE" align="right">
                                    <strong>商家负责人：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblFzr"></asp:label>
                                    联系电话
                                    <asp:label runat="server" text="" id="lblFzrTel"></asp:label>
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
                                    <strong>市场价：</strong>
                                </td>
                                <td align="left">
                                    成人
                                    <asp:label runat="server" text="" id="lblRetailAdultPrice"></asp:label>
                                    元，儿童<asp:label runat="server" text="" id="lblRetailChildrenPrice"></asp:label>
                                    元，单房差
                                    <asp:label runat="server" text="" id="lblMarketPrice"></asp:label>
                                    元
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>结算价：</strong>
                                </td>
                                <td align="left">
                                    成人
                                    <asp:label runat="server" text="" id="lblSettlementAudltPrice"></asp:label>
                                    元，儿童
                                    <asp:label runat="server" text="" id="lblSettlementChildrenPrice"></asp:label>
                                    元,增减结算价
                                    <input id="txtAddPrice" runat="server" name="txtAddPrice">
                                    元
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
                                    <asp:label runat="server" text="" id="lblRemarks"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>专线备注：</strong>
                                </td>
                                <td align="left">
                                <textarea id="txtRouteRemarks" name="txtRouteRemarks" runat="server" cols="60"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>订单状态：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblOrderState"></asp:label>
                                    <asp:label runat="server" id="pnlYuLiu">
                                        <a class="basic_btn" ref="5" href="javascript:void(0);" id="btnYuLiu"><span>预留</span></a> 留位时间<input runat="server" id="txtYuLiuDate"  onfocus="WdatePicker()"/>
                                    </asp:label>
                                    <asp:label runat="server" id="pnlQueDing">
                                      <a class="basic_btn"  ref="6" href="javascript:void(0);" id="btnQueDing"><span>确定</span></a>
                                     </asp:label>
                                    <asp:label runat="server" id="pnlJieDan">
                                      <a class="basic_btn" ref="7" href="javascript:void(0);" id="btnJieDan"><span>结单</span></a>
                                     </asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" bgcolor="#F2F9FE" align="right">
                                    <strong>支付状态：</strong>
                                </td>
                                <td align="left">
                                    <asp:label runat="server" text="" id="lblPayState" ></asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label runat="server" id="pnlLitPay">
                                    <a class="basic_btn" ref="8" id="btnLitPay" href="javascript:void(0);"><span>定金收取</span></a>
                                    </asp:label>
                                    &nbsp;&nbsp;
                                    <asp:label runat="server" id="pnlAllPay">
                                    <a class="basic_btn" ref="9" id="btnAllPay" href="javascript:void(0);"><span>全款收取</span></a>
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
                                    <a class="baocun_btn" href="javascript:void(0);" id="btnSave">保 存</a>&nbsp;
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
    var OrderStateUpdate = {
        BoxDiv: $("#<%=tblID %>"),
        OrderID: '<%=Request.QueryString["orderID"] %>',
        UpdatePrice: function() {
            var adultCount = parseInt(this.BoxDiv.find("[id$=<%=txtAdultCount.ClientID %>]").val());  //成人数
            var childCount = parseInt(this.BoxDiv.find("[id$=<%=txtChildCount.ClientID %>]").val());  //儿童数
            var otherCount = parseInt(this.BoxDiv.find("[id$=<%=txtOtherCount.ClientID %>]").val());  //单房差
            var reAdultPrice = parseInt(this.BoxDiv.find("[id$=<%=lblRetailAdultPrice.ClientID %>]").html());  //成人市场价
            var reChilePrice = parseInt(this.BoxDiv.find("[id$=<%=lblRetailChildrenPrice.ClientID %>]").html()); //儿童市场价
            var reOtherPrice = parseInt(this.BoxDiv.find("[id$=<%=lblMarketPrice.ClientID %>]").html());    //单房差价格
            var seAdultPrice = parseInt(this.BoxDiv.find("[id$=<%=lblSettlementAudltPrice.ClientID %>]").html()); //成人结算价
            var seChildPrice = parseInt(this.BoxDiv.find("[id$=<%=lblSettlementChildrenPrice.ClientID %>]").html()); //儿童结算价
            var addPrice = parseInt(this.BoxDiv.find("[id$=<%=txtAddPrice.ClientID %>]").val());  //增减结算价

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

            //市场总价
            var rePriceAll = adultCount * reAdultPrice + childCount * reChilePrice + otherCount * reOtherPrice;
            rePriceAll = parseInt(rePriceAll * 100) / 100;
            //结算总价
            var sePriceAll = adultCount * seAdultPrice + childCount * seChildPrice + addPrice;
            sePriceAll = parseInt(sePriceAll * 100) / 100;

            this.BoxDiv.find("[id$=<%=lblReailPriceAll.ClientID %>]").html(rePriceAll);
            this.BoxDiv.find("[id$=<%=lblSettlePriceAll.ClientID %>]").html(sePriceAll);
        },
        SubmitForm: function() {
            var adultCount = parseInt($.trim(OrderStateUpdate.BoxDiv.find("[id$=<%=txtAdultCount.ClientID %>]").val()));
            if (!adultCount > 0) {
                alert("请输入成人数!");
                return;
            }
            this.BoxDiv.find("[id$=btnSave]").html("提交中..");
            this.BoxDiv.find("[id$=btnSave]").unbind();

            $.newAjax({
                type: "POST",
                url: "/Order/RouteAgency/OrderStateUpdate.aspx?orderID=" + OrderStateUpdate.OrderID + "&dotype=save&v=" + Math.random(),
                data: OrderStateUpdate.BoxDiv.find("[id$=btnSave]").closest("form").serialize(),
                cache: false,
                success: function(state) {
                    if (state == "ok") {
                        alert("订单修改成功!");
                        topTab.url(topTab.activeTabIndex, "/Order/RouteAgency/OrderStateUpdate.aspx?orderID=<%=Request.QueryString["orderID"] %>");
                    } else {
                        alert(state);
                        OrderStateUpdate.BoxDiv.find("[id$=btnSave]").html("保 存");
                        OrderStateUpdate.BoxDiv.find("[id$=btnSave]").click(function() {
                            OrderStateUpdate.SubmitForm();
                        })
                    }
                },
                error: function() {
                    alert("服务器忙,请稍后再试!");
                    OrderStateUpdate.BoxDiv.find("[id$=btnSave]").html("保 存");
                    OrderStateUpdate.BoxDiv.find("[id$=btnSave]").click(function() {
                        OrderStateUpdate.SubmitForm();
                    })
                }
            });
        },
        UpdateOrderState: function(state, date) {
            $.newAjax({
                type: "POST",
                url: "/Order/RouteAgency/OrderStateUpdate.aspx?orderID=" + OrderStateUpdate.OrderID + "&dotype=state&date=" + date + "&state=" + state + "&v=" + Math.random(),
                cache: false,
                success: function(state) {
                    if (state == "ok") {
                        alert("状态修改成功!");
                        topTab.url(topTab.activeTabIndex,"<%=Request.Url.ToString() %>");
                    } else {
                        alert(state);
                    }
                },
                error: function() {
                    alert("服务器忙,请稍后再试!");
                }
            });
        }
    }


    $(function() {
        OrderStateUpdate.BoxDiv.find("[id$=btnSave]").click(function() {

            OrderStateUpdate.SubmitForm();
        })
        OrderStateUpdate.BoxDiv.find("#<%=txtAdultCount.ClientID %>,#<%=txtChildCount.ClientID %>,#<%=txtOtherCount.ClientID %>,#<%=txtAddPrice.ClientID %>").blur(function() {

            OrderStateUpdate.UpdatePrice();
        })

        OrderStateUpdate.BoxDiv.find("#btnYuLiu,#btnQueDing,#btnJieDan,#btnLitPay,#btnAllPay").click(function() {
            var type = $(this).attr("ref");
            if (type == "5") {
                var date = OrderStateUpdate.BoxDiv.find("#<%=txtYuLiuDate.ClientID %>").val();
                if ($.trim(date) != "") {
                    OrderStateUpdate.UpdateOrderState(type, date);
                } else {
                    alert("请输入留位时间!");
                    return false;
                }
            } else {
                if (confirm("提交后订单将不能被修改,确定?")) {
                    OrderStateUpdate.UpdateOrderState($(this).attr("ref"));
                }
            }

            return false;
        })

    })
</script>
</asp:content>
