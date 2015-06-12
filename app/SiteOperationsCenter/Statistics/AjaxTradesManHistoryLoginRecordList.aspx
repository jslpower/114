<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTradesManHistoryLoginRecordList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AjaxTradesManHistoryLoginRecordList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
   <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <cc1:CustomRepeater ID="crpLoginRecordList" runat="server">
    <HeaderTemplate>
    <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
  <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="white" height="23">
    <td width="6%" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
   
    <td width="20%" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>单位名称</strong></td>
   
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>联系人</strong></td>
    <td width="12%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>电话</strong></td>
    <td width="13%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>手机</strong></td>
    <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>QQ/MSN</strong></td>
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><STRONG>最后登录时间</STRONG></td>
   
    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>登录次数</strong></td>
  </tr>
    </HeaderTemplate>
    <ItemTemplate>
    <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
    <td height="25" align="center"><strong> </strong>
     <%# GetCount()%></td>
    <td height="25" align="center"><%#Eval("CompanyName")%></td>
    <td width="7%" align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.ContactName%></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel%><br/></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile%></td>
  
    <td width="11%" align="center"><%# EyouSoft.Common.Utils.GetQQ(((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ) %>/<%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.MSN%> </td>
    <td align="center">    
  <%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LastLoginTime %></td>
    <td align="center"><a href="javascript:void(0);" onclick='LoginRecordList.CompanyAccessDetail("<%# Eval("ID")%>")' class="nav4"><%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LoginCount%></a></td>
  </tr>
    </ItemTemplate>
    <FooterTemplate>
     <tr class="white" height="23">
    <td align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
    <td align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>单位名称</strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>联系人</strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>电话</strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>手机</strong></td>
    
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>QQ/MSN</strong></td>
    <td align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>最后登录时间</strong></td>
    <td align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"><strong>登录次数</strong></td>
  </tr>
</table>
    </FooterTemplate>
    </cc1:CustomRepeater> 
<table width="99%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" align="right">
        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
    </td>
  </tr>
</table>
