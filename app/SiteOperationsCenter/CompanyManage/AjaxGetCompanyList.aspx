﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGetCompanyList.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.AjaxGetCompanyList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPageByBtn" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<cc1:CustomRepeater ID="repCompanyList" runat="server">
    <HeaderTemplate>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang"
            id="tbCompanyList">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input name="ckAll" id="ckAllCompany1" type="checkbox" onclick=" CompanyManage.ckAllCompany(this);"><label
                        for="ckAllCompany1" style="cursor: pointer"><strong>序号</strong></label>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>经营范围</strong>
                </td>
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>产品销售城市</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>产品区域</strong>
                </td>
                <td width="13%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系方式</strong>
                </td>
                <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>证书</strong>
                </td>
                <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>收费项目</strong>
                </td>
                <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>登录</strong>
                </td>
                <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>加入时间</strong>
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <input type="checkbox" name="ckCompanyId" value='<%#Eval("ID") %>'><%# GetCount() %>
            </td>
            <td align="center">
                <%# GetCompanyRole(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).CompanyRole.RoleItems) %>
            </td>
            <td height="25" align="center">
                <a href="javascript:void(0);" onclick="CompanyManage.CompanyEditOpenDialog('<%# Eval("ID") %>');return false;">
                    <%# Eval("CompanyName")%></a>
                <br />
                许可证号：<%# Eval("License")%><br />
                引荐人：<%# Eval("CommendPeople")%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='CompanyManage.LookCompanySaleCity("<%#Eval("ID") %>");return false;'>
                    查看</a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='CompanyManage.LookCompanyTourArea("<%#Eval("ID") %>");return false;'>
                    查看</a>
            </td>
            <td align="left">
                <a href="javascript:void(0)" onmouseover='wsug(this,"联系人:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName %><br/>手机:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %><br/>电话:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %><br/>传真:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Fax %><br/>QQ:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ %><br />")'
                    onmouseout="wsug(this, 0)">联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName %>
                    <br />
                    手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %>
                    <br />
                </a>
            </td>
            <td width="9%" align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookCompanyCredit("<%#Eval("ID") %>");return false;'>
                    证书</a>
            </td>
            <td align="center">
                <%# GetCompanyServiceItem(Eval("ID").ToString())%>
            </td>
            <td align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookLoginDetail("<%#Eval("ID") %>");return false;'>
                    <%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LoginCount %>次</a>
            </td>
            <td align="center">
                <%#Convert.ToDateTime(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.JoinTime).ToShortDateString() %>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr bgcolor="#f3f7ff" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <input type="checkbox" name="ckCompanyId" value='<%#Eval("ID") %>'><%# GetCount() %>
            </td>
            <td align="center">
                <%# GetCompanyRole(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).CompanyRole.RoleItems) %>
            </td>
            <td height="25" align="center">
                <a href="javascript:void();" onclick="CompanyManage.CompanyEditOpenDialog('<%# Eval("ID") %>');return flase;">
                    <%# Eval("CompanyName")%></a><br />
                许可证号：<%# Eval("License")%><br />
                引荐人：<%# Eval("CommendPeople")%>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='CompanyManage.LookCompanySaleCity("<%#Eval("ID") %>");return false;'>
                    查看</a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" onclick='CompanyManage.LookCompanyTourArea("<%#Eval("ID") %>");return false;'>
                    查看</a>
            </td>
            <td align="left">
                <a href="javascript:void(0)" onmouseover='wsug(this,"联系人:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName %><br/>手机:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %><br/>电话:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %><br/>传真:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Fax %><br/>QQ:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ %><br />")'
                    onmouseout="wsug(this, 0)">联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName %>
                    <br />
                    手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %>
                    <br />
                </a>
            </td>
            <td width="9%" align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookCompanyCredit("<%#Eval("ID") %>");return false;'>
                    证书</a>
            </td>
            <td align="center">
                <%# GetCompanyServiceItem(Eval("ID").ToString())%>
            </td>
            <td align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookLoginDetail("<%#Eval("ID") %>");return false;'>
                    <%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LoginCount %>次
                </a>
            </td>
            <td align="center">
                <%#Convert.ToDateTime(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.JoinTime).ToShortDateString() %>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <input name="ckAll" id="ckAllCompany2" type="checkbox" onclick=" CompanyManage.ckAllCompany(this);"><label
                    for="ckAllCompany2" style="cursor: pointer"><strong>序号</strong></label>
            </td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>经营范围</strong>
            </td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>单位名称</strong>
            </td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>产品销售城市</strong>
            </td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>产品区域</strong>
            </td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>联系方式</strong>
            </td>
            <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>证书</strong>
            </td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>收费项目</strong>
            </td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>登录</strong>
            </td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>加入时间</strong>
            </td>
        </tr>
        </table></FooterTemplate>
</cc1:CustomRepeater>
<div align="right">
    <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>

