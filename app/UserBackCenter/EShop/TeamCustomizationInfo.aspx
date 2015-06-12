<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamCustomizationInfo.aspx.cs"
    Inherits="UserBackCenter.EShop.TeamCustomizationInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("T4.shop.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="665" align="center" id="tbl_TeamCustomization" border="0" align="center" cellpadding="0"
        cellspacing="0" style="margin: 20px 5px;">
        <tbody>
            <tr>
                <td width="132" align="right" class="jiange2">
                    <strong><%--<font color="red">*</font>--%>计划日期：</strong>
                </td>
                <td width="527" align="left" class="jiange2">
                    <input name="PlanDate" id="txtPlanDate" valid="required"
                        errmsg="计划日期不能为空!" class="boder" size="25" value="<%=PlanDate %>" />
                    <span id="errMsg_txtPlanDate" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong><%--<font color="red">*</font>--%>计划人数：</strong>
                </td>
                <td align="left" class="jiange2">
                    <input name="PlanPeopleNum" id="txtPlanPeopleNum" valid="required" errmsg="计划人数不能为空!"
                        class="boder" size="25" value="<%=PlanPeopleNum %>" />
                    <span id="errMsg_txtPlanPeopleNum" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>行程要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <a href="<%=TravelContent %>" target="_blank">
                        <%=TravelContent%></a>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>住宿要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="ResideContent" cols="60"><%=ResideContent%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>用餐要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="DinnerContent" cols="60"><%=DinnerContent%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>用车要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="CarContent" cols="60"><%=CarContent%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>导游要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="GuideContent" cols="60"><%=GuideContent%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>购物要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="ShoppingInfo" cols="60"><%=ShoppingInfo%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong>其它个性要求：</strong>
                </td>
                <td align="left" class="jiange2">
                    <textarea name="OtherContent" cols="60"><%=OtherContent%></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong><%--<font color="red">*</font>--%>联系人：</strong>
                </td>
                <td align="left" class="jiange2">
                    <input name="ContactName" id="txtContactName" valid="required" errmsg="联系人不能为空!"
                        class="boder" size="25" value="<%=ContactName %>" />
                    <span id="errMsg_txtContactName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong><%--<font color="red">*</font>--%>单位名称：</strong>
                </td>
                <td align="left" class="jiange2">
                    <input name="ContactCompanyName" id="txtContactCompanyName" valid="required" errmsg="单位名称不能为空!"
                        class="boder" size="40" value="<%=ContactCompanyName %>" />
                    <span id="errMsg_txtContactCompanyName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" class="jiange2">
                    <strong><%--<font color="red">*</font>--%>电话：</strong>
                </td>
                <td align="left" class="jiange2">
                    <input name="ContactTel" id="txtContactTel" valid="required" errmsg="电话不能为空!" class="boder"
                        size="40" value="<%=ContactTel %>" />
                    <span id="errMsg_txtContactTel" class="errmsg"></span>
                </td>
            </tr>
            <%-- <tr>
                        <td align="right" class="jiange2">
                            &nbsp;
                        </td>
                        <td align="left" class="jiange2">
                            <div class="tijiao" style="width: 133px;">
                                &nbsp;
                                <input type="button" style="width: 100%;" id="btnAdd" /></div>
                        </td>
                    </tr>--%>
        </tbody>
    </table>
    </form>
</body>
</html>
