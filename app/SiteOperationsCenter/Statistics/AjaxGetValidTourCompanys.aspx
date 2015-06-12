<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGetValidTourCompanys.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.AjaxGetValidTourCompanys" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="uc1" %>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
    <tr background="<%=ImageServerUrl %>/images/hangbg.gif" class="white" height="23">
        <td width="5%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>序号 <strong>
        </td>
        <td width="21%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>单位名称</strong>
        </td>
        <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>销售城市</strong>
        </td>
        <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>线路区域</strong>
        </td>
        <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>用户名</strong>
        </td>
        <td width="18%" align="left" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>联系方式</strong>
        </td>
        <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>QQ</strong>
        </td>
        <td width="13%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>MQ洽谈</strong>
        </td>
    </tr>
    <asp:repeater id="rptValidTourCompanyList" runat="server">
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">                        
                        <%# GetCount()%>
                    </td>
                    <td height="25" align="center">
                        <a href="javascript:ValidTourCompanys.OpenDialog('/Statistics/CompanyInfo.aspx?companyid=<%# DataBinder.Eval(Container.DataItem,"ID") %>','公司详细信息',500,400,null);"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></a><%--<br />
                        许可证号：<%# DataBinder.Eval(Container.DataItem, "License ")%>--%>
                    </td>
                    <td align="center">
                <a href="javascript:void(0)" onclick='ValidTourCompanys.OpenDialog("/CompanyManage/CompanySaleCity.aspx","销售城市",400,300,"CompanyId=<%# DataBinder.Eval(Container.DataItem,"ID") %>&rad=" + new Date().getTime());return false;'>
                    查看</a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='ValidTourCompanys.OpenDialog("/CompanyManage/CompanyTourArea.aspx","线路区域",500,300,"CompanyId=<%# DataBinder.Eval(Container.DataItem,"ID") %>&rad=" + new Date().getTime());return false;'>
                    查看</a>
            </td>
                    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).AdminAccount.UserName%></td>
                    <td align="left">
                    联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName%><br />电话：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %><br />手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %>
                    </td>
                    <td align="center"><%# EyouSoft.Common.Utils.GetQQ(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ) %></td>
                    <td align="center"><%# EyouSoft.Common.Utils.GetMQ(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.MQ) %></td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr bgcolor="#f3f7ff" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">
                        <%# GetCount()%>
                    </td>
                    <td height="25" align="center">
                        <a href="javascript:ValidTourCompanys.OpenDialog('/Statistics/CompanyInfo.aspx?companyid=<%# DataBinder.Eval(Container.DataItem,"ID") %>','公司详细信息',500,400,null);"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></a><%--<br />
                        许可证号：<%# DataBinder.Eval(Container.DataItem, "License ")%>--%>
                    </td>
                    <td align="center">
                <a href="javascript:void(0)" onclick='ValidTourCompanys.OpenDialog("/CompanyManage/CompanySaleCity.aspx","销售城市",400,300,"CompanyId=<%# DataBinder.Eval(Container.DataItem,"ID") %>&rad=" + new Date().getTime());return false;'>
                    查看</a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='ValidTourCompanys.OpenDialog("/CompanyManage/CompanyTourArea.aspx","线路区域",500,300,"CompanyId=<%# DataBinder.Eval(Container.DataItem,"ID") %>&rad=" + new Date().getTime());return false;'>
                    查看</a>
            </td>
                    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).AdminAccount.UserName%></td>
                    <td align="left">
                    联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName%><br />电话：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %><br />手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %>
                    </td>
                    <td align="center"><%# EyouSoft.Common.Utils.GetQQ(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ) %></td>
                    <td align="center"><%# EyouSoft.Common.Utils.GetMQ(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.MQ) %></td>
                </tr>
            </AlternatingItemTemplate>
        </asp:repeater>
    <tr id="tr_NoData" runat="server">
        <td align="center" style="height: 50px;" colspan="8">
            暂无数据!
        </td>
    </tr>
    <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
        <td width="5%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>序号 <strong>
        </td>
        <td width="21%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>单位名称</strong>
        </td>
        <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>销售城市</strong>
        </td>
        <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>线路区域</strong>
        </td>
        <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>用户名</strong>
        </td>
        <td width="18%" align="left" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>联系方式</strong>
        </td>
        <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>QQ</strong>
        </td>
        <td width="13%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>MQ洽谈</strong>
        </td>
    </tr>
</table>
<table width="99%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="30" align="right">
            <uc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>
