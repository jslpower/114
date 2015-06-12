<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteAreaManage.aspx.cs" Inherits="UserBackCenter.SystemSet.RouteAreaManage" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<asp:Content id="RouteAreaManage" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <script type="text/javascript">
  $(document).ready(function()
  { 
   if("<%=areaTabId%>"=="")
    {
    
     $(".zhuanxian:first").attr("class","zhuanxianon");
    
    }
    $("#ram_<%=areaTabId%>").attr("class","zhuanxianon");
  });
var RouteAreaManage={
    tabChange:function(tar_a,argTab){
      var parentNode1=$(tar_a.parentNode.parentNode);
      if(parentNode1.attr("class")=="un")
      {  
        parentNode1.attr("class","on").attr("background","<%=ImageServerUrl%>/images/weichulidingdan.gif");
        parentNode1.siblings("[class='on']").attr("class","un").attr("background","<%=ImageServerUrl%>/images/weichulidingdanf.gif");
       
        if(argTab=="unit")
        {
         
          $("#ram_unitInfoTab").css("display","");
           $("#ram_areaSetTab").css("display","none");
        }
        else
        {  
          $("#ram_unitInfoTab").css("display","none");
          $("#ram_areaSetTab").css("display","");
        }
      }
    },
    tabChange2:function(areaId)
    {
        topTab.url(topTab.activeTabIndex,"/SystemSet/RouteAreaManage.aspx?areatabid="+areaId);
        return false;
    }
}
</script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top">
         <uc1:sznb id="sznb1" runat="server" TabIndex="tab3"></uc1:sznb>
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:15px;">
                <tr>
                  <td width="20%" align="right"><strong>以下是您所经营的<span class="chengse">5</span>个专线</strong></td>
                  <td width="80%" align="left" style=" background:url(<%=ImageServerUrl%>/images/jiange.gif) repeat-x;">&nbsp;</td>
                </tr>
                <tr>
                  <td align="right">&nbsp;</td>
                  <td align="left"> <a href="javascript:void(0)" class="zhuanxian" id="ram_areaTabId1" onclick="RouteAreaManage.tabChange2('areaTabId1')">江西专线</a>  <a href="javascript:void(0)" class="zhuanxian" id="ram_areaTabId2" onclick="RouteAreaManage.tabChange2('areaTabId2')">非洲专线</a>  <a href="javascript:void(0)" class="zhuanxian" id="ram_areaTabId3" onclick="RouteAreaManage.tabChange2('areaTabId3')">北京专线</a>  <a href="javascript:void(0)" class="zhuanxian" id="ram_areaTabId4" onclick="RouteAreaManage.tabChange2('areaTabId4')">海南专线</a>  <a href="javascript:void(0)" class="zhuanxian" id="ram_areaTabId5" onclick="RouteAreaManage.tabChange2('areaTabId5')">云南专线</a></td>
                </tr>
              </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="magin10" style=" margin-top:10px; margin-bottom:3px;">
                  <tr>
                    <td width="15" style="border-bottom:1px solid #62A8E4">&nbsp;</td>
                    <td width="105" height="24" background="<%=ImageServerUrl%>/images/weichulidingdan.gif" align="center" class="on"><strong class="shenglanz"><a href="javascript:void(0)" onclick="RouteAreaManage.tabChange(this,'unit')">经营单位信息</a></strong></td>
                    <td width="105" height="24" background="<%=ImageServerUrl%>/images/weichulidingdanf.gif" align="center" class="un"><strong class="shenglanz"><a href="javascript:void(0)" onclick="RouteAreaManage.tabChange(this,'area')">区域功能设置</a></strong></td>
                    
                    <td style="border-bottom:1px solid #62A8E4" align="right">&nbsp;</td>
                  </tr>
                </table>
                <table width="85%"  id="ram_unitInfoTab" border="0" align="center" cellpadding="2" cellspacing="1"style="border-bottom:2px dashed #ccc; margin-bottom:15px;">
                  <tr>
                    <td width="12%" align="right">所属城市：</td>
                    <td colspan="3">&nbsp;
                      浙江 杭州 </td>
                  </tr>
                  <tr>
                    <td align="right"><span class="ff0000">*</span>单位名称：</td>
                    <td width="38%"><input name="textfield226322" type="text" class="bitian" value="yytyyt" size="35" /></td>
                    <td width="15%" align="right"><span class="ff0000">*</span>许可证号：</td>
                    <td width="35%"><input name="textfield2263223" type="text" class="bitian" size="20" /></td>
                  </tr>
                  <tr>
                    <td align="right"><span class="ff0000">*</span>总负责人：</td>
                    <td><input name="textfield2263222" type="text" class="bitian" size="20" /></td>
                    <td align="right">手机：</td>
                    <td><input name="textfield22663" type="text" class="shurukuang" value="130" size="20" /></td>
                  </tr>
                  <tr>
                    <td align="right">电话：</td>
                    <td><input name="textfield2266" type="text" class="shurukuang" value="0571-88865524" size="20" /></td>
                    <td align="right">传真：</td>
                    <td><input name="textfield22662" type="text" class="shurukuang" value="0571-88865524" size="20" /></td>
                  </tr>
                  <tr>
                    <td align="right">MSN：</td>
                    <td><input name="textfield22664" type="text" class="shurukuang" value="0571-88865524" size="35" /></td>
                    <td align="right">品牌名称：</td>
                    <td><input name="textfield226642" type="text" class="shurukuang" value="巴黎春天" size="20" /></td>
                  </tr>
                  <tr>
                    <td align="right">QQ：</td>
                    <td colspan="3"><input name="textfield2266434" type="text" class="shurukuang" size="55" />
                        <span class="huise">(多个QQ请用，隔开)</span></td>
                  </tr>
                  <tr>
                    <td align="right">MQ：</td>
                    <td colspan="3"><input name="textfield2266433" type="text" class="shurukuang" size="55" />
                        <span class="huise">(多个MQ请用，隔开)</span></td>
                  </tr>
                  <tr>
                    <td align="right">宣传图片：</td>
                    <td colspan="3"><input name="file2" type="file" size="15" />
                        <span class="huise">(600*180)</span> 20090520.jpg<a href="#"><img src="<%=ImageServerUrl%>/images/fujian_x.gif" alt="删除" width="14" height="13" border="0"/></a></td>
                  </tr>
                  <tr>
                    <td align="right">企业LOGO：</td>
                    <td colspan="3"><input name="file22" type="file" size="15" />
                        <span class="huise">(160*70)</span> 20090520.jpg<a href="#"><img src="<%=ImageServerUrl%>/images/fujian_x.gif" alt="删除" width="14" height="13" border="0"/></a></td>
                  </tr>
                  <tr>
                    <td align="right">公司地址：</td>
                    <td colspan="3"><input name="textfield226643" type="text" class="shurukuang" size="55" /></td>
                  </tr>
                
                  <tr>
                    <td align="right">专线文字说明：</td>
                    <td colspan="3"><textarea name="textarea3" cols="100" rows="4" class="textfield">编辑器</textarea></td>
                  </tr>
                 
                  <tr>
                    <td align="right">&nbsp;</td>
                    <td colspan="3"height="50"><a href="#" class="xiayiye">保存</a><a href="#" class="xiayiye">重置</a></td>
                  </tr>
                </table>
                <table width="85%" id="ram_areaSetTab"  border="0" align="center" cellpadding="2" cellspacing="1"style="border-bottom:2px dashed #ccc; margin-bottom:15px;">
                  <tr>
                    <td width="20%" align="right" height="30">开通选座位：</td>
                    <td><input type="radio" name="radiobutton" value="radiobutton" />                      
                      是 
                      <input type="radio" name="radiobutton" value="radiobutton" />
                      否 
                    <span class="huise">(此线路区域的团队在接受订单时，是否允许开通选座位功能?)</span></td>
                  </tr>
                  <tr>
                    <td align="right" height="30">自定义团号前缀：</td>
                    <td width="80%"><input name="textfield22662" type="text" class="shurukuang" value="ttt" size="20" />
                      <span class="huise">(按线路区域设置团号)</span></td>
                  </tr>  <tr>
                    <td align="right">&nbsp;</td>
                    <td height="50"><a href="#" class="xiayiye">保存</a><a href="#" class="xiayiye">重置</a></td>
                  </tr>
                </table>
                </td>
            </tr>
          </table></td>
      </tr>
    </table>

</asp:Content>
