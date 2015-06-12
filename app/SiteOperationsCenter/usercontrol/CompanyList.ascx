<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.CompanyList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
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
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>地区</strong>
                </td>
                <td width="13%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系方式</strong>
                </td>
                <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>收费项目</strong>
                </td>
                <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>登录次数</strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>加入时间</strong>
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <input type="checkbox" name="ckCompanyId" value='<%#Eval("ID") %>'><%# GetCount() %>
            </td>
            <td height="25" align="center">
                <a href="javascript:void(0);" onclick="CompanyManage.CompanyEditOpenDialog('<%# Eval("ID") %>');return false;">
                    <%# Eval("CompanyName")%></a>
                <br />
                引荐人：<%# Eval("CommendPeople")%>
            </td>
            <td align="center">
                <%# GetCityName(Convert.ToInt32(Eval("ProvinceId").ToString()),Convert.ToInt32(Eval("CityId").ToString()))%>
            </td>
            <td align="left">
                <a href="javascript:void(0)" onmouseover='wsug(this,"联系人:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.ContactName %><br/>手机:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Mobile %><br/>电话:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Tel %><br/>传真:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Fax %><br/>QQ:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.QQ %><br />")'
                    onmouseout="wsug(this, 0)">联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.ContactName %>
                    <br />
                    手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Mobile%>
                    <br />
                </a>
            </td>
            <td align="center">
                <%# GetCompanyServiceItem(Eval("ID").ToString())%>
            </td>
            <td align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookLoginDetail("<%#Eval("ID") %>");return false;'>
                    <%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).StateMore.LoginCount %>次</a>
            </td>
            <td align="center">
                <%#Convert.ToDateTime(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).StateMore.JoinTime).ToShortDateString() %>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr bgcolor="#f3f7ff" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <input type="checkbox" name="ckCompanyId" value='<%#Eval("ID") %>'><%# GetCount() %>
            </td>
            <td height="25" align="center">
                <a href="javascript:void(0);" onclick="CompanyManage.CompanyEditOpenDialog('<%# Eval("ID") %>');return flase;">
                    <%# Eval("CompanyName")%></a>
                <br />
                引荐人：<%# Eval("CommendPeople")%>
            </td>
            <td align="center">
                <%# GetCityName(Convert.ToInt32(Eval("ProvinceId").ToString()),Convert.ToInt32(Eval("CityId").ToString()))%>
            </td>
            <td align="left">
                <a href="javascript:void(0)" onmouseover='wsug(this,"联系人:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.ContactName %><br/>手机:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Mobile %><br/>电话:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Tel %><br/>传真:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Fax %><br/>QQ:<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.QQ %><br />")'
                    onmouseout="wsug(this, 0)">联系人：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.ContactName %>
                    <br />
                    手机：<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).ContactInfo.Mobile %>
                    <br />
                </a>
            </td>
            <td align="center">
                <%# GetCompanyServiceItem(Eval("ID").ToString())%>
            </td>
            <td align="center">
                <a href="javascript:void(0);" onclick='CompanyManage.LookLoginDetail("<%#Eval("ID") %>");return false;'>
                    <%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).StateMore.LoginCount %>次</a>
            </td>
            <td align="center">
                <%#Convert.ToDateTime(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)Container.DataItem).StateMore.JoinTime).ToShortDateString() %>
            </td>
        </tr>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <input name="ckAll" id="ckAllCompany1" type="checkbox" onclick=" CompanyManage.ckAllCompany(this);"><label
                    for="ckAllCompany1" style="cursor: pointer"><strong>序号</strong></label>
            </td>
            <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>单位名称</strong>
            </td>
            <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>地区</strong>
            </td>
            <td width="13%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>联系方式</strong>
            </td>
            <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>收费项目</strong>
            </td>
            <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>登录次数</strong>
            </td>
            <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>加入时间</strong>
            </td>
        </tr>
        </table></FooterTemplate>
</cc1:CustomRepeater>
<div align="right">
    <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
</div>


