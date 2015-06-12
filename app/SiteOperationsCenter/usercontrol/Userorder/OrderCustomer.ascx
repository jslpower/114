<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderCustomer.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.UserOrder.OrderCustomer" %>
<div id="div_<%=this.ClientID %>">
    <div style="height: 30px; width: 100%; line-height: 30px;">
        <b>游客详细信息</b><a href="javascript:void(0);" id="btnOpenCustomer" class="daorumd">导入名单</a>
    </div>
    <table id="tbl_<%=this.ClientID %>" width="99%" cellspacing="0" cellpadding="2" bordercolor="#9DC4DC"
        border="1" align="center">
        <tbody>
            <tr bgcolor="#D4E6F7" align="center">
                <td align="center">
                    序号
                </td>
                <td align="center">
                    姓名
                </td>
                <td align="center">
                    联系电话
                </td>
                <td align="center">
                    身份证
                </td>
                <td align="center">
                    护照
                </td>
                <td align="center">
                    其他证件
                </td>
                <td align="center">
                    性别
                </td>
                <td align="center">
                    类型
                </td>
                <td align="center">
                    座号
                </td>
                <td align="center">
                    备注（勾选保存）
                    <input type="checkbox" value="1" name="cbxIsSave" id="cbxIsSave" />
                </td>
            </tr>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <span>
                                <%#Container.ItemIndex+1 %></span>
                        </td>
                        <td align="center">
                            <input name="txtName" size="8" value="<%#Eval("VisitorName") %>" />
                            <a class="jian_btn" href="javascript:void(0);">删</a>
                        </td>
                        <td align="center">
                            <input style="width: 80px;" name="txtTel" value="<%#Eval("ContactTel") %>" />
                        </td>
                        <td align="center">
                            <input style="width: 100px;" name="txtCard" value="<%#Eval("IdentityCard") %>" />
                        </td>
                        <td align="center">
                            <input style="width: 100px;" name="txtCardS" value="<%#Eval("Passport") %>" />
                        </td>
                        <td align="center">
                            <input style="width: 80px;" name="txtCardT" value="<%#Eval("OtherCard") %>">
                        </td>
                        <td align="center">
                            <select name="sltSex">
                                <option value="0" <%#(int)Eval("Sex") == 1 ? "selected:selected":"" %>>男</option>
                                <option value="1" <%#(int)Eval("Sex")==1 ? "":"selected:selected" %>>女</option>
                            </select>
                        </td>
                        <td align="center">
                            <select name="sltChild">
                                <option value="1" <%#(int)Eval("CradType") ==1 ? "selected:selected":"" %>>成人</option>
                                <option value="0" <%#(int)Eval("CradType") ==1 ? "":"selected:selected" %>>儿童</option>
                            </select>
                        </td>
                        <td align="center">
                            <input size="6" name="txtNumber" value="<%#Eval("SiteNo") %>" />
                        </td>
                        <td align="center">
                            <input style="width: 100px;" name="txtRemarks" value="<%#Eval("Notes") %>" />
                            <input type="checkbox" name="cbxVisitor" value="1" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>

<script type="text/javascript">
    var OrderCustomerControl = {
        TxtAudlt: $("#<%=TxtAudltID %>"),
        TxtChildID: $("#<%=TxtChildID %>"),
        divBox: $("#div_<%=this.ClientID %>"),
        tableID: $("#tbl_<%=this.ClientID %>"),
        PreValue: 0,
        CreatTR: function(obj, type) {
            var data = { id: "", txtName: "", txtTel: "", txtCard: "", txtCardS: "", txtCardT: "", sltSex: "", sltChild: "", txtNumber: "", txtRemarks: "" };

            if (type == 'Array') {
                for (var i = 0; i < obj.length; i++) {
                    var index = OrderCustomerControl.tableID.find("tr").length;
                    var tr = "<tr><td align=\"center\"><span>" + index + "</span></td><td align=\"center\"><input name=\"txtName\" size=\"8\" value=\"" + obj[i].txtName + "\" /><a class=\"jian_btn\" href=\"javascript:void(0);\">删</a></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtTel\" value=\"" + obj[i].txtTel + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCard\" value=\"" + obj[i].txtCard + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCardS\" value=\"" + data.txtCardS + "\"/></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtCardT\" value=\"" + obj[i].txtCardT + "\"></td><td align=\"center\">  <select name=\"sltSex\"><option value=\"1\" >男</option><option value=\"0\">女</option></select> </td><td align=\"center\"><select name=\"sltChild\"><option value=\"1\">成人</option><option value=\"0\">儿童</option></select></td><td align=\"center\"><input size=\"6\" name=\"txtNumber\" value=\"" + obj[i].txtNumber + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtRemarks\" value=\"" + obj[i].txtRemarks + "\"/><input type=\"checkbox\" name=\"cbxVisitor\" value=\"" + index + "\" /></td></tr>";
                    OrderCustomerControl.tableID.append(tr);
                }
            } else {
                for (var i = 0; i < obj; i++) {
                    var index = this.tableID.find("tr").length;
                    var tr = "<tr><td align=\"center\"><span>" + index + "</span></td><td align=\"center\"><input name=\"txtName\" size=\"8\" value=\"" + data.txtName + "\" /><a class=\"jian_btn\" href=\"javascript:void(0);\">删</a></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtTel\" value=\"" + data.txtTel + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCard\" value=\"" + data.txtCard + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCardS\" value=\"" + data.txtCardS + "\"/></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtCardT\" value=\"" + data.txtCardT + "\"></td><td align=\"center\">  <select name=\"sltSex\"><option value=\"1\" >男</option><option value=\"0\">女</option></select> </td><td align=\"center\"><select name=\"sltChild\"><option value=\"1\">成人</option><option value=\"0\">儿童</option></select></td><td align=\"center\"><input size=\"6\" name=\"txtNumber\" value=\"" + data.txtNumber + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtRemarks\" value=\"" + data.txtRemarks + "\"/><input type=\"checkbox\" name=\"cbxVisitor\" value=\"" + index + "\" /></td></tr>";
                    this.tableID.append(tr);
                }

            }
            OrderCustomerControl.divBox.find(".jian_btn").unbind("click");
            OrderCustomerControl.divBox.find(".jian_btn").click(function() {
                if (OrderCustomerControl.tableID.find("tr").length > 2) {
                    OrderCustomerControl.TxtAudlt.val(parseInt(OrderCustomerControl.TxtAudlt.val()) - 1);
                    OrderCustomerControl.TxtAudlt.blur();
                }
            })
        },
        DeleteTrByCount: function(obj) {
            for (var i = 0; i < obj; i++) {
                this.tableID.find("tr:last").remove();
            }
        }
    }
    $(function() {
        OrderCustomerControl.PreValue = OrderCustomerControl.tableID.find("tr").length - 1;
        var adultCount = parseInt(OrderCustomerControl.TxtAudlt.val());
        var childCount = parseInt(OrderCustomerControl.TxtChildID.val());
        if (!adultCount > 0) {
            adultCount = 1;
        }
        if (!childCount > 0) {
            childCount = 0;
        }
        OrderCustomerControl.PreValue = adultCount + childCount;

        OrderCustomerControl.divBox.find("[id$=btnOpenCustomer]").click(function() {
            Boxy.iframeDialog({ title: "旅客选择", iframeUrl: "/TravelersManagement/TravelersSelect.aspx?CallBackFun=CallBackFun", width: "800", height: "450", draggable: true, data: null });
        })


        OrderCustomerControl.divBox.find("[id$=cbxIsSave]").click(function() {
            var ck = $(this).attr("checked") ? "checked" : "";

            OrderCustomerControl.tableID.find("input[name='cbxVisitor']").attr("checked", ck);
        })

        OrderCustomerControl.TxtAudlt.blur(function() {
            if ($.trim($(this).val()) != "") {
                var NowCount = parseInt($(this).val());
                if ($.trim(OrderCustomerControl.TxtChildID.val()) != "" && parseInt($.trim(OrderCustomerControl.TxtChildID.val())) > 0) {
                    NowCount = NowCount + parseInt($.trim(OrderCustomerControl.TxtChildID.val()));
                }
                if (!NowCount > 0) {
                    NowCount = 0;
                }

                var createTrCount = NowCount - OrderCustomerControl.PreValue;
                if (createTrCount > 0) {
                    OrderCustomerControl.CreatTR(createTrCount,"");
                }
                if (createTrCount < 0) {
                    OrderCustomerControl.DeleteTrByCount(Math.abs(createTrCount));
                }
                OrderCustomerControl.PreValue = NowCount;
            }
        })

        OrderCustomerControl.TxtChildID.blur(function() {
            OrderCustomerControl.TxtAudlt.blur();
        })

        var noLength = OrderCustomerControl.tableID.find("tr").length - 1;

        if (OrderCustomerControl.PreValue - noLength > 0) {
            OrderCustomerControl.CreatTR(OrderCustomerControl.PreValue - noLength,"");
        } else if (OrderCustomerControl.PreValue - noLength < 0) {
            OrderCustomerControl.DeleteTrByCount(OrderCustomerControl.PreValue - noLength);
        } else {
            if (OrderCustomerControl.tableID.find("tr").length == 1) {
                OrderCustomerControl.CreatTR(1,"");
            }
        }

    })

    function CallBackFun(obj) {

        OrderCustomerControl.CreatTR(obj,"Array");
    }
</script>

