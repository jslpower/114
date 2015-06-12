<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SingleGroupPre.aspx.cs" Inherits="UserBackCenter.TeamService.SingleGroupPre" %>

<asp:Content ID="SingleGroupPre" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'SingleGroupPre'
        });
    </script>

    <div id="<%=Key %>" class="right Max">
        <div class="tablebox">
            <!--添加信息表格-->
            <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%;">
                <tr>
                    <td colspan="2" align="left" valign="top">
                        <strong>
                            <%=isZT?"组团社-":title+"-" %><%=tourId.Length <= 0?"单团预订":"订单查看"%></strong>
                    </td>
                </tr>
                <%if (isZT && tourId.Length > 0)
                  { %>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <strong>订单编号：</strong>
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_OrderNo" text=""></asp:label>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <strong>线路名称：</strong>
                    </td>
                    <td align="left">
                        <a href="/PrintPage/RouteDetail.aspx?RouteId=<%=routeId %>" target="_blank">
                            <asp:label runat="server" id="lbl_routeName" text=""></asp:label>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#F2F9FE">
                        <font class="ff0000">*</font> <strong>预定出发时间：</strong>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <%if (tourId.Length <= 0)
                          { %>
                        <asp:textbox runat="server" id="txt_leaveDate" onfocus="WdatePicker({onpicked:function(){if($(this).val().length>0){$(this).parent().find('span').hide();}}});"
                            valid="required" errmsg="出发时间不能为空"></asp:textbox>
                        <img src="<%=ImgURL %>/images/time.gif" alt="" width="16" height="13" align="middle" />（至少提前<%=advanceDayRegistration %>天预定）
                        <span id="errMsg_<%=txt_leaveDate.ClientID %>" class="errmsg"></span>
                        <%}
                          else
                          { %>
                        <asp:label runat="server" id="lbl_leaveDate" text=""></asp:label>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td width="18%" align="right" bgcolor="#F2F9FE">
                        <%if (isZT)
                          { %>
                        <strong>专线地接社：</strong><%}
                          else
                          { %>
                        <strong>定团旅行社</strong>
                        <%} %>
                    </td>
                    <td align="left">
                        <a href="" target="_blank" id="a_gowd" runat="server">
                            <asp:label runat="server" id="lbl_operatorName" text=""></asp:label>
                        </a>
                        <%=EyouSoft.Common.Utils.GetMQ(MQ)%>
                        <%=EyouSoft.Common.Utils.GetQQ(QQ)%>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#f2f9fe">
                        <strong>出发交通和城市：</strong>
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_sTraffic" text=""></asp:label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#f2f9fe">
                        <strong>返回交通和城市：</strong>
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_eTraffic" text=""></asp:label>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <font class="ff0000">*</font> <strong>组团社联系人：</strong>
                    </td>
                    <td align="left">
                        <%if (isZT)
                          { %>
                        <input type="text" id="txt_travelContact" runat="server" valid="required" errmsg="组团社联系人不能为空" />
                        联系电话
                        <input type="text" id="txt_travelTel" runat="server" valid="required" errmsg="组团社联系人电话不能为空" />
                        <span id="errMsg_<%=txt_travelContact.ClientID %>" class="errmsg"></span><span id="errMsg_<%=txt_travelTel.ClientID %>"
                            class="errmsg"></span>
                        <%}
                          else
                          { %>
                        <asp:label runat="server" id="lbl_travel" text=""></asp:label>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <font class="ff0000">*</font> <strong>游客联系人：</strong>
                    </td>
                    <td align="left">
                        <%if (isZT)
                          { %>
                        <input type="text" id="txt_visitorContact" runat="server" valid="required" errmsg="游客联系人不能为空" />
                        联系电话
                        <input type="text" id="txt_visitorTel" runat="server" valid="required" errmsg="游客联系人电话不能为空" />
                        <span id="errMsg_<%=txt_visitorContact.ClientID %>" class="errmsg"></span><span id="errMsg_<%=txt_visitorTel.ClientID %>"
                            class="errmsg"></span>
                        <%}
                          else
                          { %>
                        <asp:label runat="server" id="lbl_visitor" text=""></asp:label>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <strong>团队参考价：</strong>
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_groupNum" text=""></asp:label>
                    </td>
                </tr>
                <%if (isGJ)
                  { %>
                <tr class="margintop5">
                    <td align="right" bgcolor="#F2F9FE">
                        <strong>定金</strong>：
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_Price" text=""></asp:label>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td align="right" bgcolor="#F2F9FE">
                        <font class="ff0000">*</font> <strong>预订人数：</strong>
                    </td>
                    <td align="left">
                        <input type="text" id="txt_adultPrice" runat="server" valid="isPIntegers|required"
                            errmsg="预订人数格式错误|预订人数不能为空" />
                        人 <span id="errMsg_<%=txt_adultPrice.ClientID %>" class="errmsg"></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#F2F9FE">
                        <strong>组团社备注：</strong>
                    </td>
                    <td align="left">
                        <%if (isZT)
                          { %>
                        <textarea id="txt_travelNotes" cols="85" rows="4" runat="server"></textarea>
                        <%}
                          else
                          { %>
                        <asp:label runat="server" id="lbl_travelNotes" text=""></asp:label>
                        <%} %>
                    </td>
                </tr>
                <%if (tourId.Length > 0)
                  { %>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#F2F9FE">
                        <strong>专线地接社备注：</strong>
                    </td>
                    <td align="left">
                        <%if (!isZT)
                          { %>
                        <textarea id="txt_businessNotes" cols="85" rows="4" runat="server"></textarea>
                        <%}
                          else
                          { %>
                        <asp:label runat="server" id="lbl_businessNotes" text=""></asp:label>
                        <%} %>
                    </td>
                </tr>
                <%}%>
                <tr sizcache="0" sizset="14">
                    <td align="right" bgcolor="#F2F9FE">
                        <strong>参考总金额：</strong>
                    </td>
                    <td align="left" sizcache="0" sizset="14">
                        <asp:label runat="server" id="lbl_tourPrice" text=""></asp:label>
                    </td>
                </tr>
                <%if (tourId.Length > 0)
                  { %>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <strong>下单时间：</strong>
                    </td>
                    <td align="left">
                        <asp:label runat="server" id="lbl_issueTime" text=""></asp:label>
                    </td>
                </tr>
                <%if (!isZT)
                  { %>
                <tr id="td_">
                    <td align="right" bgcolor="#f2f9fe">
                        <strong>订单状态操作</strong>：
                    </td>
                    <td align="left" id="td_TourOrderStatusChange">
                        当前状态：
                        <%= orderStatus%>
                        <%if (orderStatus != EyouSoft.Model.NewTourStructure.TourOrderStatus.结单)
                          {%>
                        <%if (orderStatus == EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认)
                          { %>
                        <a href="/TeamService/TourOrderStatusChange.ashx?tourId=<%=tourId %>&intStatus=1"
                            class="basic_btn"><span>团队确认</span></a>
                        <%} %>
                        <%if (orderStatus == EyouSoft.Model.NewTourStructure.TourOrderStatus.已确认)
                          { %>
                        <a href="/TeamService/TourOrderStatusChange.ashx?tourId=<%=tourId %>&intStatus=2"
                            class="basic_btn"><span>结单</span></a>
                        <%} %>
                        <%if (orderStatus == EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认)
                          { %>
                        <a href="/TeamService/TourOrderStatusChange.ashx?tourId=<%=tourId %>&intStatus=3"
                            class="basic_btn"><span>取消</span></a>
                        <%}
                          }%>
                    </td>
                </tr>
                <%}
                  } %>
                <tr sizcache="0" sizset="14">
                    <td colspan="2" align="center">
                        <%if (tourId.Length <= 0)
                          { %>
                        <a href="javascript:void(0);" class="baocun_btn a_Save">预 订</a><%}
                          else
                          { %>
                        <%if (orderStatus == EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认)
                          { %>
                        <a href="javascript:void(0);" class="baocun_btn a_Save">保 存</a><%} %>
                        <%} %>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        var SingleGroupPre = {
            Calculate: function(obj) {
                var Key = $("#<%=Key %>");
                if ($.trim($(obj).val()).length <= 0 || parseInt($.trim($(obj).val())) == 0) {
                    Key.find("#<%=lbl_tourPrice.ClientID %>").html("0 元")
                    return false;
                }
                var IndependentGroupPrice = '<%=IndependentGroupPrice %>'
                if (IndependentGroupPrice != "一团一议") {
                    Key.find("#<%=lbl_tourPrice.ClientID %>").html(parseInt(IndependentGroupPrice) * parseInt($.trim($(obj).val())) + " 元");
                }

            },
            Save: function(obj) {
                $(obj).unbind("click");
                var Key = $("#<%=Key %>");
                var form = $(Key).closest("form").get(0);
                var url = "/TeamService/SingleGroupPre.aspx?Operating=AddSave&companyID=<%=companyID %>&routeId=<%=routeId %>&tourId=<%=tourId %>";
                if ('<%=isZT %>' == "True") {
                    url += "&isZT=true";
                }
                if (ValiDatorForm.validator(form, "alert")) {
                    $.newAjax(
	               {
	                   url: url,
	                   data: $(form).serialize().replace(),
	                   dataType: "html",
	                   cache: false,
	                   type: "post",
	                   success: function(result) {
	                       if (result.toLowerCase() == "true") {
	                           alert(Key.find(".a_Save").text() + " 成 功!")
	                           topTab.remove(topTab.activeTabIndex)
	                           /*
	                           判断状态改变,保存后刷新的页面
	                           专线商  routeSource=1
	                           地接社  routeSource=2
	                           组团社  ref=0
	                           */
	                           var returl = '<%=IntRouteSource %>' == '1' ? "routeSource=1" : '<%=IntRouteSource %>' == '2' ? "routeSource=2" : "ref=0";
	                           topTab.url(topTab.activeTabIndex, "/TeamService/TeamOrders.aspx?" + returl);
	                       } else {
	                           alert(Key.find(".a_Save").text() + "失败,请稍后再试!");
	                       }
	                       $(obj).click(function() {
	                           SingleGroupPre.Save(this);
	                           return false;
	                       });
	                   },
	                   error: function() {
	                       alert("操作失败!");
	                       $(obj).click(function() {
	                           SingleGroupPre.Save(this);
	                           return false;
	                       });
	                   }
	               });
                }
            },
            TourOrderStatusChange: function(title, url) {
                if (confirm("确定" + title + "?")) {
                    $.newAjax(
	               {
	                   url: url,
	                   dataType: "html",
	                   cache: false,
	                   type: "get",
	                   success: function(result) {
	                       if (result.toLowerCase() == "true") {
	                           alert(title + "成功!")
	                           topTab.remove(topTab.activeTabIndex)
	                           /*
	                           判断状态改变,保存后刷新的页面
	                           专线商  routeSource=1
	                           地接社  routeSource=2
	                           组团社  ref=0
	                           */
	                           var returl = '<%=IntRouteSource %>' == '1' ? "routeSource=1" : '<%=IntRouteSource %>' == '2' ? "routeSource=2" : "ref=0";
	                           topTab.url(topTab.activeTabIndex, "/TeamService/TeamOrders.aspx?" + returl);
	                       }
	                   },
	                   error: function() {
	                       alert("操作失败!");
	                   }
	               });
                }
            }
        }
        $(function() {

            var Key = $("#<%=Key %>");
            if (Key.find(".a_Save").length > 0) {
                FV_onBlur.initValid(Key.find(".a_Save").closest("form").get(0));
            }
            Key.find("#<%=txt_adultPrice.ClientID %>")
            .keydown(function(e) {
                //小键盘0-9,大键盘0-9，退格键，delete键
                return (e.keyCode >= 96 && e.keyCode <= 105) || (e.keyCode >= 48 && e.keyCode <= 57) || e.keyCode == 8 || e.keyCode == 64;
            })
            .keyup(function() {
                if (parseInt($(this).val()) >= 0) {
                    SingleGroupPre.Calculate(this);
                }
            })

            Key.find(".a_Save").click(function() {
                SingleGroupPre.Save(this);

                return false;
            })

            Key.find("#td_TourOrderStatusChange a.basic_btn").click(function() {

                SingleGroupPre.TourOrderStatusChange($(this).text(), $(this).attr("href"))
                return false;
            })

        })
    </script>

</asp:Content>
