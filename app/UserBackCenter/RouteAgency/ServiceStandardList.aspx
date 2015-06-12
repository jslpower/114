<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceStandardList.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.ServiceStandardList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择服务标准信息</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="lankuang" id="ListTable">
        <tr>
            <td width="14%" background="<%=ImageServerPath %>/images/b1.jpg" height="25" align="center">
                <input type="checkbox" name="ckAll" id="ckAll" onclick="CheckAll(this)">
                序号
            </td>
            <td width="86%" background="<%=ImageServerPath %>/images/b1.jpg" height="25" align="center">
                内容
            </td>
        </tr>
        <asp:Repeater ID="RepeaterList" runat="server" OnItemDataBound="RepeaterList_ItemDataBound">
            <ItemTemplate>
                <tr class="zhonghui_l">
                    <td align="center">
                        <input type="checkbox" name="chkID" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                        <asp:Label ID="lblNum" runat="server"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Id")%>'></asp:Label>
                    </td>
                    <td>
                        <input type="hidden" id='hidContent_<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                            value='<%# DataBinder.Eval(Container.DataItem, "Content")%>' />
                        <%# DataBinder.Eval(Container.DataItem, "Content")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlNoData" runat="server" Visible="false">
            <tr>
                <td colspan="2" height="35px">
                    暂无数据!
                </td>
            </tr>
        </asp:Panel>
    </table>
    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" LinkType="4" />
    <table width="50%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <input type="button" id="btnSave" name="btnSave" value="确 定" class="baocunan_an" />
            </td>
            <td align="center">
                <input type="button" value="取消" class="baocunan_an" id="btnCancle" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="kuang">
        <tr>
            <td background="<%=ImageServerPath %>/images/b1.jpg" height="25" align="center">
                <asp:Label ID="lbltype" runat="server"></asp:Label>
            </td>
            <td background="<%=ImageServerPath %>/images/b1.jpg" align="center">
                操作
            </td>
        </tr>
        <tr class="zhonghui_l">
            <td width="82%" align="left">
                <asp:TextBox ID="txtServer" runat="server" class="input" Width="95%" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="center">
                <label>
                    &nbsp;<input type="button" id="btnAdd" name="btnAdd" class="an_tijiaobaocun" value=" 添 加 " /></label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCkIdList" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="javascript" type="text/javascript">
        function CheckAll(obj) {
            $("#ListTable").find("input:checkbox").attr("checked", obj.checked);
        }

        function GetChkValue() {
            var arr = new Array();
            $("#hidCkIdList").val("");  //先清空
            $("#ListTable").find("input[type='checkbox']:checked").each(function() {
                arr.push($(this).val())
            });
            if (arr.length == 0) {
                var strErr;
                var ServiceType = '<%=ServiceType %>';
                switch (ServiceType) {
                    case '1':
                        strErr = '请选择住宿安排!';
                        break;
                    case '2':
                        strErr = '请选择用餐安排!';
                        break;
                    case '3':
                        strErr = '请选择景点安排!';
                        break;
                    case '4':
                        strErr = '请选择用车安排!';
                        break;
                    case '5':
                        strErr = '请选择导游安排!';
                        break;
                    case '6':
                        strErr = '请选择往返交通安排!';
                        break;
                    case '7':
                        strErr = '请选择其它安排';
                        break;
                    case '8':
                        strErr = '请选择集合方式';
                        break;
                    case '9':
                        strErr = '请选择接团方式';
                        break;
                }
                alert(strErr);
            }
            return arr;
        }

        $("#btnAdd").click(function() {
            var parentControlID = '<%=ControlID %>';
            var txtAddValue = $.trim($("#<%=txtServer.ClientID %>").val());
            if (txtAddValue == "") {
                alert("内容不能为空!");
                return false;
            } else {
                if ('<%=IsCompanyCheck %>' == 'False') {
                    alert('对不起，您的账号未审核，不能进行操作!');
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/RouteAgency/ServiceStandardList.aspx?flag=add&AddValue=" + encodeURI(txtAddValue) + "&Type=<%=ServiceType %>&rnd=" + Math.random(),
                    cache: false,
                    success: function(html) {
                        if (html == "True") {
                            alert("保存成功!");
                            var parentVal = parent.$("#<%=ContainerID %>").find("textarea[name=" + parentControlID + "]").val();
                            if (parentVal != '') {
                                parent.$("#<%=ContainerID %>").find("textarea[name=" + parentControlID + "]").val(parentVal + ";    " + txtAddValue);
                            } else {
                                parent.$("#<%=ContainerID %>").find("textarea[name=" + parentControlID + "]").val(txtAddValue);
                            }
                            var frameid = window.parent.Boxy.queryString("iframeId")
                            window.parent.Boxy.getIframeDialog(frameid).hide();
                        } else {
                            alert("保存失败!");
                        }
                    }
                });
            }
        });

        $("#btnSave").click(function() {
            var arr = GetChkValue();
            var parentControlID = '<%=ControlID %>';
            var hidCkIdList = $("#hidCkIdList").val();
            var arrContent = new Array();

            if (arr.length != 0) {

                for (var i = 0; i < arr.length; i++) {
                    var content = $("#hidContent_" + arr[i]).val();
                    if (content != '' && content != undefined)
                        arrContent.push(content);
                }
                if (arrContent != null) {
                    var parentVal = parent.$("#<%=ContainerID %>").find("#" + parentControlID).val();
                    if (parentVal != '') {
                        parent.$("#<%=ContainerID %>").find("#" + parentControlID).val(parentVal + ";    " + arrContent.join(";    "));
                    } else {
                        parent.$("#<%=ContainerID %>").find("#" + parentControlID).val(arrContent.join(";    "));
                    }
                    var frameid = window.parent.Boxy.queryString("iframeId")
                    window.parent.Boxy.getIframeDialog(frameid).hide();
                }
            }
        });

        $("#btnCancle").click(function() {
            var frameid = window.parent.Boxy.queryString("iframeId")
            window.parent.Boxy.getIframeDialog(frameid).hide();
        });
    </script>

    </form>
</body>
</html>
