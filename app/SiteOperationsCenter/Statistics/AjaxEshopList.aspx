<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxEshopList.aspx.cs" Inherits="SiteOperationsCenter.Statistics.AjaxEshopList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>

 <head>
     <style type="text/css">
         .style4
         {
             width: 6%;
         }
         .style5
         {
             width: 132px;
         }
         .style6
         {
             width: 9%;
         }
         .style8
         {
             width: 109px;
         }
         .style9
         {
             width: 101px;
         }
         .style10
         {
             width: 117px;
         }
         .style11
         {
             width: 85px;
         }
         .style12
         {
             width: 12%;
         }
     </style>
</head>

 <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
  <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="white" height="23">
    <td height="23" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"align="center" valign="middle" class="style4"><strong> 序号</strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" valign="middle" class="style6"><strong>地区</strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"valign="middle" class="style12"><strong>单位名称<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style8"><strong>联系人<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style8"><strong>电话<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style6"><strong>手机<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style11"><strong>MQ<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style9"><strong>QQ<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style5"><strong>开通时间</strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"class="style10"><strong>到期时间</strong></td>
    <td width="7%" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"align="center"><strong>操作</td>
    </tr>
<cc1:CustomRepeater ID="crptEshopList" runat="server">     
<ItemTemplate>
  <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
    <td height="25"width="3%" align="center"><strong> </strong>
      <%#(Container.ItemIndex+1)+(intCurrentPage-1)*intPageSize %></td>
    <td align="center"><%#Eval("ProvinceName")%><%#Eval("CityName") %></td>
    <td height="10" align="center"><%#Eval("CompanyName")%></td>
    <td align="center"><%#Eval("ContactName") %></td>
     <td  align="center"><%#Eval("ContactTel")%></td>
     <td  align="center"><%#Eval("ContactMobile")%></td>
     <td align="center"><%#Eval("ContactMQ")%></td>
     <td align="center"><%#Eval("ContactQQ")%></td>
     <td  align="center"><%#Eval("EnableTime","{0:yyyy-MM-dd}")%></td>
     <td  align="center"><%#Eval("ExpireTime","{0:yyyy-MM-dd}")%></td>
     <td align="center"><input type="button" CompanyId="<%#Eval("UserId") %>" ApplyId="<%#Eval("ApplyId") %>" value="续费" onclick="EshopDueDate.OpearAddMoney(this)" /></td> 
 </tr>
     </ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#F3F7FF" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
    <td height="23" width="3%" align="center"><strong> </strong>
      2</td>
    <td align="center"><%#Eval("ProvinceName")%><%#Eval("CityName") %></td>
    <td height="10" align="center"><%#Eval("CompanyName")%></td>
    <td align="center"><%#Eval("ContactName") %></td>
    <td  align="center"><%#Eval("ContactTel")%></td>
    <td  align="center"><%#Eval("ContactMobile")%></td>
    <td  align="center"><%#Eval("ContactMQ")%></td>
    <td align="center"><%#Eval("ContactQQ")%></td>
    <td align="center"><%#Eval("EnableTime", "{0:yyyy-MM-dd}")%></td>
    <td align="center"><%#Eval("ExpireTime", "{0:yyyy-MM-dd}")%></td>
    <td align="center"><input type="button" CompanyId="<%#Eval("UserId") %>" ApplyId="<%#Eval("ApplyId") %>" value="续费" onclick="EshopDueDate.OpearAddMoney(this)" /></td> 
</tr>
     </AlternatingItemTemplate>
</cc1:CustomRepeater>  
  <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="white" height="23">
    <td height="23" align="center" valign="middle"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style4"><strong> 序号</strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" valign="middle" class="style6"><strong>地区</strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style12"><strong>      单位名称<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style8"><strong>联系人<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style8"><strong>电话<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style6"><strong>手机<br />
    </strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style11"><strong>MQ<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style9"><strong>QQ<br />
    </strong></td>
    <td align="center"background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style5"><strong>开通时间</strong></td>
    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" class="style10"><strong>到期时间</strong></td>
    <td width="7%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif" ><strong>操作</td>
    </tr>
  </table>
  <table width="99%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" align="right"> 
        <div align="right">
            <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
    </td>
  </tr>
</table>


