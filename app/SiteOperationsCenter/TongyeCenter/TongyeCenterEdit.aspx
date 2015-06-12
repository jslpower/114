<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongyeCenterEdit.aspx.cs"
    Inherits="SiteOperationsCenter.TongyeCenter.TongyeCenterEdit" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%= JsManage.GetJsFilePath("GetCityList")%>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <style>
        .provinceDiv label
        {
            display: inline-block;
        }
        #txtTongyeCenterName
        {
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="0" cellpadding="5" bordercolor="#B9D3F2" border="1">
            <tbody>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>名称：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" class="style2" id="txtTongyeCenterName" runat="server" valid="required"
                            maxlength="50" errmsg="请输入名称!" />
                    </td>
                </tr>
                <tr>
                    <td width="17%" bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>成员构成导入：
                    </td>
                    <td width="83%" bgcolor="#F7FBFF" align="left">
                        <input id="selProvince" type="radio" name="ch" class="province" runat="server" /><label
                            for="selProvince">请选择省市</label><br />
                        <input id="hidProCityIDs" type="hidden" runat="server" />
                        <input id="hidProCityIDsEd" type="hidden" runat="server" />
                        <div style="display: none; border: 1px solid #b9d3f2; padding: 10px; margin: 10px;
                            background: #fff; width: 60%" class="provinceDiv">
                        </div>
                        <input type="radio" name="ch" class="menberId" id="radByIds" runat="server"><label
                            for="radByIds">请选择会员ID</label>
                        <br />
                        <input id="hidMemberIDs" type="hidden" runat="server" />
                        <div style="display: none; border: 1px solid #b9d3f2; padding: 10px; margin: 10px;
                            background: #fff; width: 60%" class="provinceDiv1">
                            请输入会员名：<input type="text" runat="server" id="memberIds">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>排序：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtSort" runat="server" maxlength="3" valid="required|isNumber"
                            errmsg="请输入序号!|请输入数字序号!" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>总管理员：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtIDs" runat="server" valid="required|isNumber" value="13641" errmsg="请输入总管理员ID号!|请输入有效总管理员ID号!" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        密码：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtPassword" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>操作人：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtOper" readonly="readonly" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>时间：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtDate" onfocus="WdatePicker({skin:'default',dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                            valid="required" class="Wdate" errmsg="请输入时间!" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        &nbsp;
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        //初始化
        inload();
        $("#<%=this.btnSave.ClientID%>").click(function() {
            if ($(".province").attr("checked") == false && $(".menberId").attr("checked") == false) {
                alert("请选择会员导入方式!");
                return false;
            }
            if ($(".province").attr("checked") == true) {
                if ($("#hidProCityIDs").val() == "" || $("#hidProCityIDs").val() == null) {
                    alert("请选择省市!");
                    return false;
                }
            }
            if ($(".menberId").attr("checked") == true) {
                if ($("#hidMemberIDs").val() == "" || $("#hidMemberIDs").val() == null) {
                    alert("请输入导入会员ID!");
                    return false;
                }
                else{
                    if(/^\d[,\d]*$/.test($("#hidMemberIDs").val())){
                        return true;
                    }else{
                        alert("会员ID只能是数字且以数字开头，英文逗号分隔!");
                        return false;
                    }
               }
            }
            var form = $(this).closest("form").get(0);
            //点击按纽触发执行的验证函数
            return ValiDatorForm.validator(form, "alert");
        });
        //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
        FV_onBlur.initValid($("#<%=this.btnSave.ClientID%>").closest("form").get(0));
        $(".province").click(function() {
            alert('保存后,输入的会员将被覆盖!');
            $(".provinceDiv").show();
            $(".provinceDiv1").hide();
        });
        $(".menberId").click(function() {
            alert('保存后,按省份导入的会员将被覆盖!');
            $(".provinceDiv1").show();
            $(".provinceDiv").hide();
        });

        function inload() {
            var html = "";
            var arrProvince = GetAllProvince();
            html += "<table>";
            for (var i = 0; i < arrProvince.length; i++) {
                //全国不添加
                if (arrProvince[i][1] != '35') {
                    if (i % 8 == 0 || i == 0) {
                        html += "<tr>";
                    }
                    html += '<td><input  id=' + arrProvince[i][1] + ' type="checkbox" pro="pro" name="shi" value=' + arrProvince[i][1] + '><label for=' + arrProvince[i][1] + '>' + arrProvince[i][0] + '</label></td>';
                    if (i + 1 % 8 == 0) {
                        html += "</tr>";
                    }
                }
            }
            html += "</table>";
            $(".provinceDiv").html(html);
            if ($(".province").attr("checked")) {
                $(".provinceDiv").show();
                $(".provinceDiv1").hide();
            }
            if ($(".menberId").attr("checked")) {
                $(".provinceDiv1").show();
                $(".provinceDiv").hide();
            }
            if ($("#hidProCityIDs").val() != "" && $("#hidProCityIDs").val() != null) {
                var cityids = $("#hidProCityIDs").val().split(',');
                for (var i = 0; i < cityids.length; i++) {
                    $("input[pro='pro'][value='" + cityids[i] + "']").attr("checked", true);
                }
            }
            var cityidseds = $("#hidProCityIDsEd").val().split(',');
            for (var i = 0; i < cityidseds.length; i++) {
                $("input[pro='pro'][value='" + cityidseds[i] + "']").attr("checked", true);
                $("input[pro='pro'][value='" + cityidseds[i] + "']").next("label").attr("style", "color:gray");
                $("input[pro='pro'][value='" + cityidseds[i] + "']").attr("disabled", "disabled");
            }
            if ($("#hidMemberIDs").val() != "" && $("#hidMemberIDs").val() != null) {
                $("#memberIds").val($("#hidMemberIDs").val());
            }
        }
        $("input[pro='pro']").click(function() {
            var ProCityIDs = $("#hidProCityIDs").val().split(',');
            var position;
            var isZxist = false;
            for (var i in ProCityIDs) {
                if ($(this).attr("id") == ProCityIDs[i]) {
                    position = i;
                    isZxist = true;
                    break;
                }
            }
            if (isZxist) {
                ProCityIDs.splice(position, 1);
            } else {
                ProCityIDs.push($(this).attr("id"));
            }
            $("#hidProCityIDs").val(ProCityIDs.join(','));
        });

        $("#memberIds").change(function() {
            $("#hidMemberIDs").val($("#memberIds").val());
        });
        
     </script>

</body>
</html>
