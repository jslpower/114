<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBusinessMemeberList.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.AjaxBusinessMemeberList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="1" cellpadding="1" cellspacing="0" bordercolor="#C7DEEB"
            class="table_basic">
            <tr>
                <th nowrap="nowrap">
                    编号
                </th>
                <th nowrap="nowrap">
                    经营范围
                </th>
                <th nowrap="nowrap">
                    单位名称
                </th>
                <th nowrap="nowrap">
                    地区
                </th>
                <th nowrap="nowrap">
                    管理员
                </th>
                <th nowrap="nowrap">
                    收费
                </th>
                <th nowrap="nowrap">
                    注册
                </th>
                <th nowrap="nowrap">
                    登录
                </th>
                <th nowrap="nowrap">
                    公司等级
                </th>
                <th nowrap="nowrap">
                    功能
                </th>
                <th nowrap="nowrap">
                    查看
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="left">
                <input type="checkbox" name="checkbox" value="<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>" />
                <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>
            </td>
            <td align="center">
                <%# GetCompanyRole(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).CompanyRole.RoleItems)%>
            </td>
            <td align="left">
                <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).CompanyName%>
            </td>
            <td align="center" nowrap="nowrap">
                <%# GetCompanyproCityCountry(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ProvinceId, ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).CityId, ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).CountyId)%>
            </td>
            <td align="left" nowrap="nowrap">
                <a href="javascript:void(0)" onmouseover="wsug(event, '联系人: <%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.ContactName%>&lt;/br&gt;手机:<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.Mobile%>&lt;/br&gt;电话:<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.Tel%>&lt;/br&gt;传真:<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.Fax%>&lt;/br&gt;QQ:<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.QQ%>&lt;br /&gt;')"
                    onmouseout="wsug(event, 0)">联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.ContactName%><br />
                    手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyUser)Eval("User")).ContactInfo.Mobile%>
                </a>
            </td>
            <td align="center">
                <%# GetCompanyServices(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).StateMore.CompanyService)%>
            </td>
            <td align="center">
                <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).StateMore.JoinTime.ToString("yyyy-MM-dd hh:mm:ss") %>
            </td>
            <td align="center">
                <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).StateMore.LoginCount %>
            </td>
            <td align="center">
                <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).CompanyLev%>
            </td>
            <td align="center" nowrap="nowrap">
                <a href="AddBusinessMemeber.aspx?EditId=<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>">
                    修改</a><br />
                <a href="PersonalMemberList.aspx?CompanyId=<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>">
                    账户</a>
            </td>
            <td align="center" nowrap="nowrap">
                <a href="javascript:void(0)" onclick='BusinessMemManage.LookCompanySaleCity("<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>");return false;'>
                    销售城市</a><br />
                <a href="javascript:void(0)" onclick='BusinessMemManage.LookCompanyTourArea("<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Eval("Company")).ID%>");return false;'>
                    产品区域</a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <th>
                编号
            </th>
            <th>
                经营范围
            </th>
            <th>
                单位名称
            </th>
            <th>
                地区
            </th>
            <th>
                管理员
            </th>
            <th>
                收费
            </th>
            <th>
                注册
            </th>
            <th>
                登录
            </th>
            <th>
                会员等级
            </th>
            <th>
                功能
            </th>
            <th>
                查看
            </th>
        </tr>
        </table></FooterTemplate>
</cc1:CustomRepeater>
<table width="99%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>

