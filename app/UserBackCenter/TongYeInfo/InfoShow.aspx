<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoShow.aspx.cs" Inherits="UserBackCenter.TongYeInfo.InfoShow" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="HotelOrderList" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="tablebox">
         <!--列表-->
         <!--翻页-->
         <table width="96%" cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" style="width:100%; margin-top:10px;" class="padd5">
           <tbody><tr>
             <td align="center" colspan="4">标 题：<asp:Label runat="server" ID="lbTitle"></asp:Label></td>
           </tr>
           <tr>
             <td width="17%" bgcolor="#CCE8F8" align="right">相关专线：</td>
             <td width="30%" align="left"><asp:Label runat="server" ID="lbRoute"></asp:Label></td>
             <td width="9%" bgcolor="#CCE8F8" align="right">发布企业：</td>
             <td width="44%" align="left"><asp:Label runat="server" ID="lbCompany"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right">类别：</td>
             <td align="left"><asp:Label runat="server" ID="lbType"></asp:Label></td>
             <td bgcolor="#CCE8F8" align="right">发布时间：</td>
             <td align="left"><asp:Label runat="server" ID="lbTime"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right">图片：</td>
             <td align="left" colspan="3"><asp:Label runat="server" ID="lbPic"></asp:Label></td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right">内容：</td>
             <td align="left" colspan="3"><asp:Label runat="server" ID="lbContent"></asp:Label>

</td>
           </tr>
           <tr>
             <td align="center" colspan="4">附件下载：<asp:Label runat="server" ID="lbFile"></asp:Label></td>
           </tr>
           <tr>
            <td align="center" colspan="4"><a class="baocun_btn" href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex,'<%=string.IsNullOrEmpty(Request.QueryString["pageFrom"])?"/TongYeInfo/InfoList.aspx":"/GeneralShop/TongYeNews/NewsList.aspx"%>')">返 回</a></td>
           </tr>
         </tbody></table>
       </div>
</asp:content>