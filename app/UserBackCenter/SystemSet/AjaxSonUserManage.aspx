<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSonUserManage.aspx.cs" Inherits="UserBackCenter.SystemSet.AjaxSonUserManage" %>
<table id="sum_SonUserList" width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:15px;">
                <tr>
                  <td width="15%" align="right"><strong>现已开通<span class="chengse">11</span>个子帐户</strong></td>
                  <td width="85%" align="left" style=" background:url(<%=ImageServerUrl%>/images/jiange.gif) repeat-x;">&nbsp;</td>
                </tr>
                
              </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" >
                  <tr>
                    <td class="toolbj">
					<a  href="javascript:void(0)" onclick="SonUserManage.OpenDialog('新增子账户','/SystemSet/SonUserSet.aspx','780px','480px')" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/add.gif" />新增</a>
					<a  href="javascript:void(0)" onclick="SonUserManage.edit();" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/modified.gif" />修改</a>
					<a href="javascript:void(0);" onclick="SonUserManage.copy()" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/copy.gif" />复制</a>
					<a href="javascript:void(0);" onclick="SonUserManage.del()" class="menubarleft"><img src="<%=ImageServerUrl%>/images/tool/dele.gif" />删除</a> </td>
                  </tr>
                </table><table width="98%"  border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#B0C7D5">
                  <tr class="white" height="23">
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>
                      <input type="checkbox" name="sum_checkall1" id="sum_checkall1" onclick="SonUserManage.chkAll(this)" value="checkbox" />
                      序号</strong></td>
                    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>用户名</strong></td>
                    <td width="8%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>姓名</strong></td>
                    <td width="11%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>电话</strong></td>
                    <td width="14%" align="center" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>手机 </strong></td>
                    <td width="36%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>产品区域</strong></td>
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/hangbg.gif"><strong>用户状态</strong></td>
                  </tr>
                  <tr class="baidi" onmouseover="SonUserManage.mouseovertr(this)" onmouseout="SonUserManage.mouseouttr(this)">
                    <td height="25" align="center"><input type="checkbox" name="checkbox12" value="id1" onclick="SonUserManage.chkOne()" />
                      1</td>
                    <td height="25" align="center">fengaiyu2008</td>
                    <td align="center">陈思思</td>
                    <td align="center">0571-88888888</td>
                    <td align="center">0571-88888888</td>
                    <td align="center"></td>
                    <td align="center"><a href="javascript:void(0)"  onclick="SonUserManage.forbid(this,'id1','forbid')">停用</a> <a href="javascript:void(0)" style="display:none" onclick="SonUserManage.forbid(this,'id1','unforbid')">启用</a></td>
                  </tr>
                  <tr bgcolor="#F3F7FF" onmouseover="SonUserManage.mouseovertr(this)" onmouseout="SonUserManage.mouseouttr(this)">
                    <td height="23" align="center"><input type="checkbox" name="checkbox" value="id2" onclick="SonUserManage.chkOne()"/>
                      2</td>
                    <td height="23" align="center">fdfgaiyu2008</td>
                    <td align="center">陈思思</td>
                    <td align="center">0571-88888888</td>
                    <td align="center">0571-88888888</td>
                    <td align="center"></td>
                    <td align="center"><a href="javascript:void(0)"  onclick="SonUserManage.forbid(this,'id1','forbid')">停用</a> <a href="javascript:void(0)" style="display:none"  onclick="SonUserManage.forbid(this,'id1','unforbid')">启用</a></td>
                  </tr>
                 
                </table>
                <table id="Table1" cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
                  <tr>
                    <td class="F2Back" align="right" height="40"> 第 1 页/总 11 页 首页 前页 <a href="/RouteAgency/TourManger/Default.aspx?Page=2">后页</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=11">末页</a> <br />
                        <span class="RedFnt">1</span> <a href="/RouteAgency/TourManger/Default.aspx?Page=2">2</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=3">3</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=4">4</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=5">5</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=6">6</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=7">7</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=8">8</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=9">9</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=10">10</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=11">&gt;&gt;</a> </td>
                  </tr>
                </table></td>
            </tr>
          </table>


