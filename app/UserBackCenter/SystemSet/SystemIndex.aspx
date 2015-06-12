<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemIndex.aspx.cs" Inherits="UserBackCenter.SystemSet.SystemIndex" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content id="SystemIndex" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

<script type="text/javascript">
    var SystemIndex =
 {
     tabChange: function(source_link) {
        
         topTab.url(topTab.activeTabIndex, "/SystemSet/" + source_link);


         return false;
     }
 }
 
</script>
<div id="showmodel"></div>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top">
        <uc1:sznb id="sznb1" runat="server" ></uc1:sznb>
       <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="2%">&nbsp;</td>
                  <td width="98%" align="left"><img src="<%=ImageServerUrl%>/images/titleczzn.gif" width="156" height="34" /></td>
                </tr>
              </table>
                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td height="65"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="18%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="javascript:void(0)" onclick="return SystemIndex.tabChange('PersonInfoSet.aspx')"><span style=" font-family:Arial, Helvetica, sans-serif;font-size:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>1</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 个人设置：</span></td>
                          <td width="60%" background="<%=ImageServerUrl%>/images/bgsz.gif">设置您个人信息、个人联系方式、密码</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td height="65"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="23%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="javascript:void(0)" onclick="return SystemIndex.tabChange('CompanyInfoSet.aspx')"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>2</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 单位信息管理：</span></a></td>
                          <td width="55%" background="<%=ImageServerUrl%>/images/bgsz.gif">请填写完善公司信息，方便客户通过<a href="<%=Domain.UserPublicCenter %>/Default.aspx" target="_blank"><img src="<%=ImageServerUrl%>/images/tongye114s.gif" width="44" height="15" />同业114平台</a>找到您。</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>
                 <%-- <tr>
                    <td height="65"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="23%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="shezhi2.html"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>3</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 线路区域管理：</span></a></td>
                          <td width="55%" background="<%=ImageServerUrl%>/images/bgsz.gif">针对您经营的线路区域，<span class="font"><strong>可分别设置</strong> </span><a href="http://www.tongye114.com/Theme/ThirdTheme/CompanyData.aspx?company_id=1714&amp;acompany_id=247&amp;TourAreaId=134" target="_blank">专线介绍</a>(<a href="zhuanxianxinxi.htm" target="_blank"><img src="<%=ImageServerUrl%>/images/left_icon.gif" width="9" height="9" />设置</a>)、团号前缀、联系qq<br/>
                              <a href="http://www.tongye114.com/Theme/ThirdTheme/Destination_Manual.aspx?company_id=2152&amp;ProvinceId=13&amp;SiteId=110&amp;CityId=160&amp;TourAreaId=384" target="_blank">目的地信息</a>(<a href="zhuanxianxinxi_3.htm" target="_blank"><img src="<%=ImageServerUrl%>/images/left_icon.gif" width="9" height="9" />设置</a>)( 包括酒店   交通   景点   饮食   购物等信息，方便用户使用 )</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>--%>
                    <tr>
                    <td height="70"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="20%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="javascript:void(0)" onclick="return SystemIndex.tabChange('DepartManage.aspx')"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>3</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 部门设置：</span></a></td>
                          <td width="58%" background="<%=ImageServerUrl%>/images/bgsz.gif">新建，修改部门信息</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td height="70"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="20%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="javascript:void(0)" onclick="return SystemIndex.tabChange('SonUserManage.aspx')"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>4</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 子帐户管理：</span></a></td>
                          <td width="58%" background="<%=ImageServerUrl%>/images/bgsz.gif">如果您需要多个用户共同使用此系统，请<a href="javascript:void(0)" onclick="return SystemIndex.tabChange('SonUserManage.aspx')"><img src="<%=ImageServerUrl%>/images/xingzengzhanghu.gif" width="75" height="25" /></a>，并指定其角色权限。</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td height="70"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="23%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="javascript:void(0)" onclick="return SystemIndex.tabChange('PermitManage.aspx')"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>5</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 权限管理：</span></a></td>
                          <td width="55%" background="<%=ImageServerUrl%>/images/bgsz.gif">添加，修改角色权限 </td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>
                 <%-- <tr>
                    <td height="70"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="1%" align="left" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/leftsz.gif" width="11" height="59" /></td>
                          <td width="29%" background="<%=ImageServerUrl%>/images/bgsz.gif"><a href="shezhi0.html"><span style="font-family:Arial, Helvetica, sans-serif;font-size:30px; line-height:30px; font-weight:bold; color:#009900; text-decoration:none;"><em>6</em></span><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;"> 常用信息维护：</span></a></td>
                          <td width="49%" background="<%=ImageServerUrl%>/images/bgsz.gif">您可以设置日常供应商信息包括：住宿、用餐、景点、用车、导服、往返交通等以供生成线路团队时调用(极大方便您工作)</td>
                          <td width="18%" align="center" background="<%=ImageServerUrl%>/images/bgsz.gif">&nbsp;</td>
                          <td width="3%" align="right" background="<%=ImageServerUrl%>/images/bgsz.gif"><img src="<%=ImageServerUrl%>/images/rightsz.gif" width="20" height="59" /></td>
                        </tr>
                    </table></td>
                  </tr>--%>
                </table></td>
            </tr>
          </table></td>
      </tr>
    </table>

</asp:Content>