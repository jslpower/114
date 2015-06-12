<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderCustomer.ascx.cs"
    Inherits="UserBackCenter.usercontrol.UserOrder.OrderCustomer" %>
<div id="<%=tblID %>">
    <div style="height: 30px; width: 100%; line-height: 30px;">
        <b>游客详细信息</b><a href="javascript:void(0);" id="btnOpenCustomer" class="daorumd">导入名单</a>
    </div>
    <table id="tbl_<%=tblID %>" width="99%" cellspacing="0" cellpadding="2" bordercolor="#9DC4DC"
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
                            <input name="txtName" size="8" value="<%#Eval("VisitorName") %>" /><a class="jian_btn"
                                href="javascript:void(0);" ref="<%=tblID %>" onclick="OrderCustomer.BtnDelete(this);return false;">删</a>
                        </td>
                        <td align="center">
                            <input style="width: 80px;" name="txtTel" value="<%#Eval("Mobile") %>" />
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
                            <select name="sltSex" onchange="OrderCustomer.SltChange(this);">
                                <option value="0" <%#(int)Eval("Sex") == 0 ? "selected=selected":"" %>>男</option>
                                <option value="1" <%#(int)Eval("Sex")==0 ? "":"selected=selected" %>>女</option>
                            </select>
                        </td>
                        <td align="center">
                            <select name="sltChild" onchange="OrderCustomer.SltChange(this);">
                                <option value="0" <%#(int)Eval("CradType") ==0 ? "selected=selected":"" %>>成人</option>
                                <option value="1" <%#(int)Eval("CradType") ==0 ? "":"selected=selected" %>>儿童</option>
                            </select>
                        </td>
                        <td align="center">
                            <input size="6" name="txtNumber" value="<%#Eval("SiteNo") %>" />
                        </td>
                        <td align="center">
                            <input style="width: 100px;" name="txtRemarks" value="<%#Eval("Notes") %>" /><input
                                type="checkbox" name="cbxVisitor" value="1" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>

<script type="text/javascript">
    var OrderCustomer = {
        CreatTR: function(obj, type, key) {
            var data = { id: "", txtName: "", txtTel: "", txtCard: "", txtCardS: "", txtCardT: "", sltSex: "", sltChild: "", txtNumber: "", txtRemarks: "" };
            if (type == 'Array') {
                for (var i = 0; i < obj.length; i++) {
                    var index = $("#" + key).find("table").find("tr").length;
                    var sex = obj[i].sltSex == "0" ? "<option value=\"0\" selected='selected' >男</option><option value=\"1\">女</option>" : "<option value=\"0\" >男</option><option value=\"1\" selected='selected'>女</option>";
                    var child = obj[i].sltChild == "0" ? "<option value=\"0\" selected='selected'>成人</option><option value=\"1\">儿童</option>" : "<option value=\"0\">成人</option><option value=\"1\" selected='selected'>儿童</option>";
                    var tr = "<tr><td align=\"center\"><span>" + index + "</span></td><td align=\"center\"><input name=\"txtName\" size=\"8\" value=\"" + obj[i].txtName + "\" /><a class=\"jian_btn\" href=\"javascript:void(0);\" ref=\"<%=tblID %>\" onclick='OrderCustomer.BtnDelete(this);return false;'>删</a></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtTel\" value=\"" + obj[i].txtTel + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCard\" value=\"" + obj[i].txtCard + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCardS\" value=\"" + obj[i].txtCardS + "\"/></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtCardT\" value=\"" + obj[i].txtCardT + "\"></td><td align=\"center\">  <select  name=\"sltSex\">" + sex + "</select> </td><td align=\"center\"><select onchange=\"OrderCustomer.SltChange(this)\" name=\"sltChild\">" + child + "</select></td><td align=\"center\"><input size=\"6\" name=\"txtNumber\" value=\"" + obj[i].txtNumber + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtRemarks\" value=\"" + obj[i].txtRemarks + "\"/></td></tr>";
                    $("#" + key).find("table").append(tr);
                }


            } else {
                for (var i = 0; i < obj; i++) {
                    var index = $("#" + key).find("table").find("tr").length;
                    var tr = "<tr><td align=\"center\"><span>" + index + "</span></td><td align=\"center\"><input name=\"txtName\" size=\"8\" value=\"" + data.txtName + "\" /><a class=\"jian_btn\" href=\"javascript:void(0);\" ref=\"<%=tblID %>\" onclick='OrderCustomer.BtnDelete(this);return false;'>删</a></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtTel\" value=\"" + data.txtTel + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCard\" value=\"" + data.txtCard + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtCardS\" value=\"" + data.txtCardS + "\"/></td><td align=\"center\"><input style=\"width: 80px;\" name=\"txtCardT\" value=\"" + data.txtCardT + "\"></td><td align=\"center\">  <select name=\"sltSex\"><option value=\"0\" >男</option><option value=\"1\">女</option></select> </td><td align=\"center\"><select name=\"sltChild\" onchange=\"OrderCustomer.SltChange(this)\"><option value=\"0\">成人</option><option value=\"1\">儿童</option></select></td><td align=\"center\"><input size=\"6\" name=\"txtNumber\" value=\"" + data.txtNumber + "\" /></td><td align=\"center\"><input style=\"width: 100px;\" name=\"txtRemarks\" value=\"" + data.txtRemarks + "\"/><input type=\"checkbox\" name=\"cbxVisitor\" value=\"" + index + "\" /></td></tr>";
                    $("#" + key).find("table").append(tr);
                }

            }
        },
        DeleteTrByCount: function(obj, key) {
            for (var i = 0; i < obj; i++) {
                $("#" + key).find("table").find("tr:last").remove();
            }
        },
        UpdateCount: function(key) {
        var trCount = $("#" + key).find("table").find("tr").length - 1;
            var childCount = parseInt($("#" + key).closest("form").find("#<%=TxtChildID %>").val());
            if (!childCount > 0) {
                childCount = 0;
            }
            if (trCount - childCount <= 0) {
                childCount--;
                $("#" + key).closest("form").find("#<%=TxtChildID %>").val(childCount);
            } else {
                $("#" + key).closest("form").find("#<%=TxtAudltID %>").val(trCount - childCount);
            }
           
            $("#" + key).closest("form").find("#<%=TxtAudltID %>").attr("PreValue", trCount);
        },
        BtnDelete: function(o) {
            var key = $(o).attr("ref");
            if ($("#" + key).find("table").find("tr").length > 2) {
                $(o).closest("tr").remove();
                OrderCustomer.UpdateCount(key);
                $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").blur();
            }
        },
        SltChange: function(o) {
            var form = typeof (o) == 'string' ? $("#" + o).closest("form") : $(o).closest("form");
            var allLen = form.find("select[name='sltChild']").length;
            var childLen = form.find("select[name='sltChild'][value='1']").length;
            form.find('#<%=TxtAudltID %>').val(allLen - childLen);
            form.find('#<%=TxtChildID %>').val(childLen);
            $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").focus();
        }
    }
    $(function() {

        $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("ref", "<%=tblID %>");
        $("#<%=tblID %>").closest("form").find("#<%=TxtChildID %>").attr("ref", "<%=tblID %>");
        var adultCount = parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").val());
        var childCount = parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtChildID %>").val());
        if (!adultCount > 0) {
            adultCount = 1;
        }
        if (!childCount > 0) {
            childCount = 0;
        }
        $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("PreValue", adultCount + childCount);

        var noLength = $("#<%=tblID %>").find("table").find("tr").length - 1;

        if (parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("PreValue")) - noLength > 0) {
            OrderCustomer.CreatTR(parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("PreValue")) - noLength, "", "<%=tblID %>");
        } else if (parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("PreValue")) - noLength < 0) {
            OrderCustomer.DeleteTrByCount(parseInt($("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").attr("PreValue")) - noLength, "<%=tblID %>");
        } else {
            if ($("#<%=tblID %>").find("table").find("tr").length == 1) {
                OrderCustomer.CreatTR(1, "", "<%=tblID %>");
            }
        }

        $("#<%=tblID %>").find("#btnOpenCustomer").click(function() {
            Boxy.iframeDialog({ title: "旅客选择", iframeUrl: "/TravelersManagement/TravelersSelect.aspx?CallBackFun=CallBackFun&key=<%=tblID %>", width: "800", height: "450", draggable: true, data: null });
            return false;
        })

        $("#<%=tblID %>").find("#cbxIsSave").click(function() {
            var ck = $(this).attr("checked") ? "checked" : "";
            $("#<%=tblID %>").find("table").find("input[name='cbxVisitor']").attr("checked", ck);
        })

        $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").blur(function() {
            var obj = $("#" + $(this).attr("ref")).closest("form");
            if ($.trim($(this).val()) != "") {
                var NowCount = parseInt($(this).val());
                if ($.trim(obj.find("#<%=TxtChildID %>").val()) != "" && parseInt($.trim(obj.find("#<%=TxtChildID %>").val())) > 0) {
                    NowCount = NowCount + parseInt($.trim(obj.find("#<%=TxtChildID %>").val()));
                }
                if (!NowCount > 0) {
                    NowCount = 0;
                }
                var createTrCount = NowCount - parseInt($(this).attr("PreValue"));
                if (createTrCount > 0) {
                    if (createTrCount > 100) {
                        if (!confirm("人数过多!可能会导致浏览器崩溃!确定要执行吗?")) {
                            return false;
                        }
                    }
                    OrderCustomer.CreatTR(createTrCount, "", $(this).attr("ref"));
                }
                if (createTrCount < 0) {
                    OrderCustomer.DeleteTrByCount(Math.abs(createTrCount), $(this).attr("ref"));
                }
                obj.find("#<%=TxtAudltID %>").attr("PreValue", NowCount);
            }
        })

        $("#<%=tblID %>").closest("form").find("#<%=TxtChildID %>").blur(function() {
            $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").blur();
        })
    })

    function CallBackFun(obj, key) {
        OrderCustomer.CreatTR(obj, "Array", key);
        OrderCustomer.SltChange(key);
        OrderCustomer.UpdateCount(key);
        $("#<%=tblID %>").closest("form").find("#<%=TxtAudltID %>").blur();
    }
</script>

