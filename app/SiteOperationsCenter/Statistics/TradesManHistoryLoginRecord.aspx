<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradesManHistoryLoginRecord.aspx.cs" Inherits="SiteOperationsCenter.Statistics.TradesManHistoryLoginRecord" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<%@ Register src="../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>零售商历史登录记录页</title>
<link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" /><script language="JavaScript">

<script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }

</script>
</head>

<body>    
    <form id="form1" runat="server">
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td background="<%=ImageServerUrl %>/images/chaxunbg.gif">单位名称：
      <input name="CompanyName" type="text" class="textfield" size="15" />
      <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />
      <img src="<%=ImageServerUrl %>/images/chaxun.gif" width="62" height="21" /><asp:ImageButton 
            ID="ImgBtn" runat="server" />
            </td>
  </tr>
</table>
    <cc1:CustomRepeater ID="repCompanyList" runat="server">
    <HeaderTemplate>
    <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
  <tr background="<%=ImageServerUrl %>/images/hangbg.gif" class="white" height="23">
    <td width="3%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong> 序号</strong></td>
   
    <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>单位名称</strong></td>
   
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>联系人</strong></td>
    <td width="12%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>电话</strong></td>
    <td width="13%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>手机</strong></td>
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>QQ/MSN</strong></td>
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><STRONG>最后登录时间</STRONG></td>
   
    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>登录次数</strong></td>
  </tr>
    </HeaderTemplate>
    <ItemTemplate>
    <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
    <td height="25" align="center"><strong> </strong>
      1</td>
    <td height="25" align="center"><%#Eval("CompanyName")%></td>
    <td width="7%" align="center"><%#Eval("ContactName") %></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %><br/></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %></td>
  
    <td width="11%" align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ %><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.MSN %> </td>
    <td align="center"><%#Eval("LoginEndDate")%></td>
    <td align="center"><a href="javascript:ViewAllCarPlan1();" class="nav4"><%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LoginCount %></a></td>
  </tr>
    </ItemTemplate>
    <FooterTemplate>
     <tr class="white" height="23">
    <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong> 序号</strong></td>
    <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>单位名称</strong></td>
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>联系人</strong></td>
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>电话</strong></td>
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>手机</strong></td>
    
    <td align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>QQ</strong></td>
    <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>交易情况</strong></td>
    <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>登录次数</strong></td>
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
    </form>
</body>
</html>
