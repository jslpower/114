<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchFriend.aspx.cs" Inherits="IMFrame.OnlineMQ.SearchFriend" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControls/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>搜索</title>
    <style type="text/css">
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            margin: 0px;
            background: url(<%= ImageServerUrl %>/IM/images/searchbj.gif) repeat-x top;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #036;
            text-decoration: none;
            border-bottom: 1px solid #036;
        }
        a:hover
        {
            color: #f00;
            text-decoration: none;
            border-bottom: 1px solid #f00;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        a.red
        {
            color: #cc0000;
        }
        a.red:visited
        {
            color: #cc0000;
        }
        a.red:hover
        {
            color: #ff0000;
        }
        .tij
        {
            background: url(<%= ImageServerUrl %>/IM/images/buttonsearch.gif);
            width: 76px;
            height: 23px;
            color: #00427A;
            border: 0px;
        }
        .search
        {
            background: url(<%= ImageServerUrl %>/IM/images/buttonsearch2.gif);
            width: 76px;
            padding-left: 5px;
            height: 23px;
            color: #00427A;
            border: 0px;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">

        var SearchFriend = {
            Search: function() {
                //修改查找按钮文字
                $("#btnSubmit").val("查找中...");
                //禁用查找按钮
                $("#btnSubmit").attr("disabled", "true");

                var strUrl = "AddFriend.aspx";

                //已经选用的公司类型 0：精确查找 1：批发商 2：零售商  3:地接 4:其它
                var checkedTypeId = "0";

                //省份
                var ProvinceId = 0;
                //城市
                var CityId = 0;
                var CityName = "";
                ProvinceId = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
                CityId = $("#ProvinceAndCityList1_ddl_CityList").val();
                if (CityId != 0) {
                    CityName = $("#ProvinceAndCityList1_ddl_CityList").find("[selected]='true'").text();
                }
                //公司名称
                var CompanyName = $("#<%=txtCompanyName.ClientID %>").val().split("'").join("");
                //联系人姓名
                var ContactName = $("#<%=txtContactName.ClientID %>").val().split("'").join("");
                //用户名称
                var UserName = $("#<%=txtUserName.ClientID %>").val().split("'").join("");
                //用户ID
                var UserId = "";
                var strPar = "?ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&CompanyName=" + escape(CompanyName) + "&ContactName=" + escape(ContactName) + "&UserName=" + escape(UserName) + "&UserId=" + UserId + "&TypeId=" + checkedTypeId + "&CityName=" + escape(CityName);
                window.location.href = strUrl + strPar;
            }
        }

       
    </script>

</head>
<body oncontextmenu="return false;" scroll="no" style="border:0px;">
    <form id="form1" runat="server">
    <table width="520" height="320" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top">
                <table width="516px" height="22px" border="0" align="center" cellpadding="0" runat="server"
                    id="tbIsCheck" visible="false" cellspacing="0" style="border: 1px solid #EF9739;
                    background: #FFF8E2">
                    <tr>
                        <td style="padding: 2px; line-height: 15px;" align="center">
                            <img style="margin-top: 4px; " src="<%= ImageServerUrl %>/IM/images/woring.gif" width="15" height="15" />&nbsp;当前您的账号未审核，MQ暂时不能添加好友及查看好友信息！<br />
                            我们会在24小时内联系您！或请致电 0571-56892810！

                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="15%" height="41" align="center" valign="bottom">
                            <img src="<%= ImageServerUrl %>/IM/images/search.gif" width="26" height="25" />
                        </td>
                        <td width="85%" valign="bottom">
                            请选择您要查找的用户类型，当前平台用户人数<asp:Label ID="labSumNumber" runat="server" Text="0"></asp:Label>人
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="center">
                            &nbsp;
                        </td>
                        <td height="20" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>查找条件</legend>
                                <table width="98%" border="0" cellspacing="0" cellpadding="4">
                                    <tr id="tr_TourAgency">
                                        <td align="right" bgcolor="#F8FBFF">
                                            所在地：
                                        </td>
                                        <td bgcolor="#F8FBFF">
                                            <uc3:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#F8FBFF">
                                            公司名称：
                                        </td>
                                        <td bgcolor="#F8FBFF">
                                            <asp:TextBox ID="txtCompanyName" runat="server" size="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#F8FBFF">
                                            联系人姓名：
                                        </td>
                                        <td bgcolor="#F8FBFF">
                                            <asp:TextBox ID="txtContactName" runat="server" size="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" bgcolor="#F8FBFF">
                                            用户名：
                                        </td>
                                        <td bgcolor="#F8FBFF">
                                            <asp:TextBox ID="txtUserName" runat="server" size="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="tr_UserId" style="display: none">
                                        <td align="right" bgcolor="#F8FBFF">
                                            用户ID号：
                                        </td>
                                        <td bgcolor="#F8FBFF">
                                            <asp:TextBox ID="txtUserId" runat="server" size="15" MaxLength="9"></asp:TextBox>(数字)
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <input type="button" id="btnSubmit" value=" 开始查找" onclick="SearchFriend.Search();"
                                class="search" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
