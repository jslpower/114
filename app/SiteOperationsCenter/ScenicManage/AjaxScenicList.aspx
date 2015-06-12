<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxScenicList.aspx.cs"
    Inherits="SiteOperationsCenter.ScenicManage.AjaxScenicList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <%--<table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang" id="tbCompanyList">--%>
        <table class="table_basic" width="100%" cellspacing="0" cellpadding="0" bordercolor="#C7DEEB"
            border="1" align="center">
            <tr>
                <th>
                    <label for="chkSelAll">
                        <input id="chkSelAll" name="chkall" type="checkbox" value="" />全选序号</label>
                </th>
                <th>
                    景区名
                </th>
                <th>
                    地区
                </th>
                <th>
                    管理公司
                </th>
                <th width="160">
                    联系
                </th>
                <th>
                    状态
                </th>
                <th>
                    B2B
                </th>
                <th>
                    B2C
                </th>
                <th>
                    点击量
                </th>
                <th>
                    景区管理
                </th>
                <th>
                    门票管理
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr id="tr<%#Eval("ScenicId") %>">
            <td align="left">
                <input id="chkScenic" name="chkindex" value="<%#Eval("ScenicId") %>$<%#((EyouSoft.Model.CompanyStructure.Company)Eval("Company")).ID%>"
                    type="checkbox" />
            </td>
            <td align="center">
                <%# Eval("ScenicName")%>
            </td>
            <td align="center">
                <%#GetCompanyAddress(Eval("ProvinceId").ToString(), Eval("CityId").ToString(), Eval("CountyId").ToString())%>
            </td>
            <td align="center">
                <%#((EyouSoft.Model.CompanyStructure.Company)Eval("Company")).CompanyName%>
            </td>
            <td align="left" valign="middle">
                <%# GetCompanyInfo(Eval("ContactOperator").ToString())%>
            </td>
            <td align="center">
                <%#Eval("Status")%>
            </td>
            <td align="center">
                <%#Eval("B2B")%>
            </td>
            <td align="center">
                <%#Eval("B2C")%>
            </td>
            <td align="center">
                <%#Eval("ClickNum")%>
            </td>
            <td align="center">
                <%--<a href="jinqu_gl_dingdan.html">订单</a>--%><br />
                <a href="ScenicPhotos.aspx?ScenicId=<%#Eval("ScenicId")%>&CompanyId=<%#((EyouSoft.Model.CompanyStructure.Company)Eval("Company")).ID%>&ScenicName=<%# Eval("ScenicName") %>">
                    照片</a>
            </td>
            <td align="center">
                <a href="/ScenicManage/EditScenicTicket.aspx?action=add&sid=<%# Eval("ScenicId") %>&cid=<%# Eval("Company") != null ? ((EyouSoft.Model.CompanyStructure.CompanyInfo)Eval("Company")).ID : string.Empty %>">
                    增加门票</a><br />
                <a href="/ScenicManage/ScenicTicket.aspx?sid=<%#Eval("ScenicId") %>">查看门票</a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <th>
                序号
            </th>
            <th>
                景区名
            </th>
            <th>
                地区
            </th>
            <th>
                管理公司
            </th>
            <th>
                联系
            </th>
            <th>
                状态
            </th>
            <th>
                B2B
            </th>
            <th>
                B2C
            </th>
            <th>
                点击量
            </th>
            <th>
                景区管理
            </th>
            <th>
                门票管理
            </th>
        </tr>
        </table></FooterTemplate>
</cc1:CustomRepeater>
<div align="right">
    <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
    
    //全选
    $("#chkSelAll").click(function() {
        var isChecked = $("#chkSelAll").attr("checked");
        $("input[name=chkindex]").each(function() {
            $(this).attr("checked", isChecked);
    });
});

</script>

