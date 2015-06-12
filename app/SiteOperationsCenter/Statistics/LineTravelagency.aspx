<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="LineTravelagency.aspx.cs" Inherits="SiteOperationsCenter.Statistics.LineTravelagency" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>

<%@ Register src="../usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc1" %>

<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>在线组团社页</title>
<link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" /><script language="JavaScript">
 </script>
</head>
<body>   
    <form id="form1" runat="server"> 
 
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td background="<%=ImageServerUrl %>/images/chaxunbg.gif">
    <table width="70%"  border="0" align="left" cellpadding="1" cellspacing="0">
      <tr>
        <td><table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="right" nowrap="nowrap">单位名称：</td>
              <td><div align="left">
                  <input name="txtCompanyName" type="text" class="textfield" size="15" />
              </div></td>
            </tr>
        </table></td>
        <td>
        <table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="right" nowrap="nowrap">               
                  <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />              
              </td>             
            </tr>
        </table>      
        </td>
        <td><table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td rowspan="2" align="right" nowrap="nowrap">负责人：</td>
              <td align="left"><input name="txtPrincipal" type="text" class="textfield" size="10" />
              </td>
            </tr>
        </table></td>
        <td>
            <asp:ImageButton ID="ImgBtn"  width="62" height="21" runat="server" 
                onclick="ImgBtn_Click" />
   </td>
      </tr>
    </table></td>
  </tr>
</table>
<table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc1:CustomRepeater ID="rpCompanyList" runat="server">
                <HeaderTemplate>
                 <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
  <tr background="<%=ImageServerUrl %>/images/hangbg.gif" class="white" height="23">
    <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"> 
    <input name="ckAll" id="ckAllCompany1" type="checkbox" onclick=" CompanyManage.ckAllCompany(this);"><label
                    for="ckAllCompany1" style="cursor: pointer"><strong>序号</strong></label></td>
   
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>单位名称</strong></td>
  
    <td width="6%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>联系人</strong></td>
    <td width="11%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>电话</strong></td>
    <td width="11%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>手机</strong></td>
    <td width="18%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>QQ/MSN</strong></td>
    <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>登录时间</strong></td>
    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>总登录次数</strong></td>
  </tr>  
    </HeaderTemplate>
    <ItemTemplate>
    <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
    <td height="25" align="center"><strong> </strong>
       <input type="checkbox" name="ckCompanyId" value='<%#Eval("ID") %>'><%# GetCount() %>
      </td>
    <td height="25" align="center"><a href="javascript:ViewAllCarPlan1();"> <%# Eval("CompanyName")%></a></td>
    <td align="center"><%#Eval("ContactName") %></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Tel %></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.Mobile %></td>
    <td align="center"><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.QQ %><%# ((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).ContactInfo.MSN %></td>
    <td align="center"><%#Eval("LoginStartDate")%></td>
    <td align="center"><%#((EyouSoft.Model.CompanyStructure.CompanyDetailInfo)GetDataItem()).StateMore.LoginCount %></td>
  </tr>   
    </ItemTemplate>
    <FooterTemplate>  
    <tr background="<%=ImageServerUrl %>/images/hangbg.gif" class="white" height="23">
    <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong> 序号</strong></td>
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>单位名称</strong></td>
    <td width="6%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>联系人</strong></td>
    <td width="11%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>电话</strong></td>
    <td width="11%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>传真</strong></td>
    <td width="18%" align="center" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>QQ/MSN</strong></td>
    <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>登录时间</strong></td>
    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/hangbg.gif"><strong>总登录次数</strong></td>
  </tr>
    </table>
    </FooterTemplate>
                </cc1:CustomRepeater>
                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </td>
        </tr>
</table>

    </form>
</body>
<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>
</html>
