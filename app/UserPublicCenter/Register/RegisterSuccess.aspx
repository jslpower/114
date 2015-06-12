<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs"
    Inherits="UserPublicCenter.Register.RegisterSuccess" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Title="注册成功页" %>
<%@ Register Src="../WebControl/RegisterHead.ascx" TagName="RegisterHead" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="phMain" runat="server">
    <uc1:RegisterHead ID="RegisterHead1" runat="server" /><style type="text/css">
	.xinhao{ color:#FF0000;}
	.jutixx02{border:1px solid #A7A6AA;color:#000000;font-size:14px;font-weight:bold;height:12px;padding-top:5px;}
	.MQ_title{ display:block; line-height:30px; margin-top:20px;}
	.MQ_title a{font-size:14px; color:#ff6600; font-weight:bold; }
	.MQ_text{margin:0 0 20px 0; padding:0; line-height:18px; }
	.MQ_text a{font-size:12px; color:#59930b; margin:0; padding:0;}
</style>

    <div class="body">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center">
                    <img id="RegisterHeadimg" src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/join-3.gif"
                        width="956" height="30" />
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:4px #ff6600 solid;">
  <tr>
    <td align="left"><table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="4" align="right" height="10"></td>
        </tr>
      <tr>
        <td width="10%" height="76" align="left" valign="top"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/zhucgg.gif" /></td>
        <td width="35%" align="left"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/zhuce_t.gif" /></td>
        <td width="22%" align="left"><a runat="server" id="hfPerfect"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/btn_02.gif" /></a></td>
        <td width="33%" align="left"><a runat="server" style="font-size: 20px; font-weight: bold; line-height: 120%; cursor: pointer" id="a_GoBackCenter"><img runat="server" id="imgBack" /></a></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td align="left"><table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td colspan="2">&nbsp;</td>
        </tr>
      <tr>
        <td colspan="2" align="left">
       
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="33%" align="left"> <img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/mailinfo_12.jpg" width="234" height="37" /></td>
              <td width="33%"><a href="http://im.tongye114.com/IM/DownLoad/download.aspx" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/dlmq2.gif" width="265" height="35" border="0" /></a></td>
              <td width="34%"><a href="http://im.tongye114.com" target="_blank">同业MQ，被同行亲切的誉为“旅游版QQ”！<br />
  旅游人专用的商务洽谈工具   了解详情&gt;&gt; </a></td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td width="50%"><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /> 1、结交商友！---------MQ下载</a></span>
          <p class="MQ_text"><a href="http://im.tongye114.com">在这里，我们都是旅游同行，我们的专线遍布全国各省市，
          <br />旅行社、酒店供应商、机票供应商累计数<strong style="color:#FF0000">十万会员！</strong></a></p></td>
        <td width="50%" align="left"><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /></a><a href="http://www.tongye114.com/hotel_362" target="_blank" class="font14zs"> 4、找特价酒店</a></span>
          <p class="MQ_text"><a href="http://www.tongye114.com/hotel_362" target="_blank" class="font12ls">全国8000多家酒店实时在线预定，前台现付<strong style="color:#FF0000">返佣高达15%</strong>，<br />
          预付还能享受更多优惠超低价格。。。。让您订房无忧</a></p></td>
      </tr>
      <tr>
        <td><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /></a><a href="http://www.tongye114.com/RouteManage/Default.aspx" target="_blank" class="font14zs"> 2、拓宽产品线，实时预订报名！</a></span>
          <p class="MQ_text">   <a href="http://www.tongye114.com/RouteManage/Default.aspx" target="_blank" class="font12ls">同业114每日更新最新的旅游产品，<br />优质的供应商是您企业腾飞的保证！</a></p></td>
          
        <td align="left"><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /></a> <a target="_blank" href="http://www.tongye114.com/SupplierInfo/SupplierInfo.aspx" id="a_AddSupplyMessage" runat="server" style="font-size: 20px; font-weight: bold; line-height: 120%; cursor: pointer"> 5、供求信息 </a></span>
          <p class="MQ_text"><a href="http://www.tongye114.com/SupplierInfo/SupplierInfo.aspx" target="_blank" class="font12ls">找酒店，找车辆，找专线，找同业信息，<br />
国内最大的旅游供求信息中心</a></p></td>
      </tr>
      <tr>
        <td><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /></a><a href="http://www.tongye114.com/AirTickets/Login.aspx" target="_blank" class="font14zs"> 3、找特价机票，团队机票</a> </span>
          <p class="MQ_text"> <a href="http://www.tongye114.com/AirTickets/Login.aspx" target="_blank" class="font12ls"><strong style="color:#FF0000">返点最高20%！</strong><br />
散客票实时预订，还能预订团队票</a></p></td>
        <td align="left"><span class="MQ_title"><a href="http://im.tongye114.com" target="_blank"><img src="<%=ImageServerPath %>/images/UserPublicCenter/jionicon/icoline.gif" width="11" height="11" /></a><a href="http://club.tongye114.com/default.aspx" target="_blank" class="font14zs"> 6、同行社区</a> </span>
          <p class="MQ_text"> <a href="http://club.tongye114.com/default.aspx" target="_blank" class="font12ls">汇集旅游专家观点，解读营销策略，探讨行业方向，<br />
参与同业活动 专业的旅游同业交流社区</a></p></td>
      </tr>
    </table></td>
  </tr>
</table>
       <%-- <table width="940" border="0" cellspacing="0" cellpadding="5" class="maintop15">
            <tr>
                <td width="330" height="112" align="center" valign="bottom">
                    <img src="<%=ImageServerPath %>/images/UserPublicCenter/zhucgg.gif" width="84" height="76" />
                </td>
                <td width="590" rowspan="2" align="left">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                您可以：
                            </td>
                        </tr>
                        <tr>
                            <td height="100" align="left">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="33%" height="50">
                                            <%--<a runat="server" style="font-size: 20px; font-weight: bold; line-height: 120%; cursor: pointer"
                                                id="a_GoBackCenter">进入管理后台</a>
                                        </td>
                                        <td width="33%">
                                            <a href="http://im.tongye114.com" target="_blank" style="font-size: 20px; font-weight: bold;
                                                color: #CC3300; line-height: 120%;">下载使用同业MQ</a>
                                        </td>
                                        <td width="33%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="50">
                                            <a id="a_AddTourList" runat="server" style="font-size: 20px; font-weight: bold; line-height: 120%;
                                                cursor: pointer">发布产品</a>
                                        </td>
                                        <td>
                                           <%-- <a id="a_AddSupplyMessage" runat="server" style="font-size: 20px; font-weight: bold;
                                                line-height: 120%; cursor: pointer">发布供求信息</a>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <a href="/Default.aspx?CityId=<%=CityId %>" class="bitian2">返回首页</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-size: 25px; color: #ff6600; font-weight: bold; line-height: 200%">
                        注册成功！</span><br />
                  
                </td>
            </tr>
        </table> <span style="font-size: 16px; color: #009900; font-weight: bold">
                        <asp:Label ID="labRegisterMessage" runat="server"></asp:Label></span>--%> 
        <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td height="30" align="left">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
