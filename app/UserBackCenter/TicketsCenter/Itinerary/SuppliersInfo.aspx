<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuppliersInfo.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.Itinerary.SuppliersInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="FreightAdd" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <table width="835" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#EEF7FF"
        class="userInfo">
        <tr>
            <td height="10" colspan="3" align="right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="200" height="35" align="right">
                公司名字：
            </td>
            <td width="40" align="left">
                &nbsp;
            </td>
            <td width="580" align="left">
              <asp:Label runat="server" Text="" ID="sup_lblSuppliers"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="35" align="right">
                联系人：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_txtContact" runat="server" errmsg="联系人不能为空!" valid="required"></asp:textbox>
                <span id="errMsg_<%=sup_txtContact.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td height="35" align="right">
                联系电话：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_txtTel" runat="server" errmsg="电话不能为空|电话号码格式不正确" valid="required|isPhone"></asp:textbox>
                <span id="errMsg_<%=sup_txtTel.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
       <%-- <tr>
            <td height="35" align="right">
                地址：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_txtAddress" runat="server" errmsg="地址不能为空" valid="required"></asp:textbox>
                <span id="errMsg_<%=sup_txtAddress.ClientID %>" class="errmsg"></span>
            </td>
        </tr>--%>
        <tr>
            <td height="35" align="right">
                服务价格：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_ServicePrice" runat="server" errmsg="服务价格不能为空!|格式不正确!" valid="required|IsDecimalTwo"></asp:textbox>
                元 / 张<span id="errMsg_<%=sup_ServicePrice.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td height="35" align="right">
                上班时间：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_BeginTime" runat="server" errmsg="上班时间不能为空" valid="required" ></asp:textbox>
                <img src="<%=ImageServerUrl %>/images/jipiao/time1.gif" width="16" height="13" style="position: relative;
                    left: -28px; top:3px;" />
                <span id="errMsg_<%=sup_BeginTime.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
         <tr>
            <td height="35" align="right">
                下班时间：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_EndTime" runat="server" errmsg="下班时间不能为空" valid="required" ></asp:textbox>
                <img src="<%=ImageServerUrl %>/images/jipiao/time1.gif" width="16" height="13" style="position: relative;
                    left: -28px; top:3px;" />
                <span id="errMsg_<%=sup_EndTime.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td height="35" align="right">
                快递费：
            </td>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:textbox id="sup_txtPrice" runat="server" errmsg="快递费不能为空!|格式不正确!" valid="required|isMoney"></asp:textbox>
                元 <span id="errMsg_<%=sup_txtPrice.ClientID %>" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td height="35" align="right">
            </td>
            <td>
                &nbsp;
            </td>
            <td align="left">
                <img id="sup_imgBtnSave" src="<%=ImageServerUrl%>/images/jipiao/baocun_btn.jpg" width="79px"
                    height="25px"  alt="保存"/>
                    <span id="sup_spanmsg" style="color:red;"></span>
            </td>
        </tr>
        <tr>
            <td height="10" colspan="3" align="right">
            </td>
        </tr>
    </table>
<!--right end-->

<script type="text/javascript">
    $(document).ready(function() {
        var form = $("#sup_imgBtnSave").closest("form").get(0);
        $("#sup_imgBtnSave").css("cursor", "pointer");
        FV_onBlur.initValid(form);

        $("#sup_imgBtnSave").click(function() {
            ThisPage.save();
        });
    });

    var ThisPage = {
        save: function() {
            var form = $("#sup_imgBtnSave").closest("form").get(0);
            var b = ValiDatorForm.validator(form, "span");

            if (b) {
                $("#sup_spanmsg").html("正在保存...");
                $("#sup_imgBtnSave").unbind().css("cursor", "default");
                $.newAjax({
                    type: "POST",
                    url: "Itinerary/SuppliersInfo.aspx?type=Update&v=" + Math.random(),
                    data: $($("#sup_imgBtnSave").closest("form").get(0)).serializeArray(),
                    cache: false,
                    success: function(state) {
                        if (state == "UpdateOk") {
                            $("#sup_spanmsg").html("保存成功");
                        } else if (state == "error") {
                            $("#sup_spanmsg").html("保存失败,请稍后再试.");
                        } else {
                            $("#sup_spanmsg").html("");
                            alert(state);
                        }
                        setTimeout(function() {
                            $("#sup_imgBtnSave").click(ThisPage.save).css("cursor", "pointer");
                        }, 1500);

                    },
                    error: function() {
                        $("#sup_spanmsg").html("保存失败,请稍后再试.");
                        setTimeout(function() {
                            $("#sup_imgBtnSave").click(ThisPage.save).css("cursor", "pointer");
                        }, 1500);
                    }

                });
            }
        }
    };
  
    
    
   
      
</script>

</asp:content>
