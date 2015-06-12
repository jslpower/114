<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMQAduit.aspx.cs" Inherits="SiteOperationsCenter.TyProductManage.AjaxMQAduit" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>

     
     <asp:CustomRepeater runat="server" id="ama_rpt_applyList">
         <HeaderTemplate>
         <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>序号</strong>
                </td>
                <td width="14%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>地区</strong>
                </td>
                <td width="25%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="19%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系方式</strong>
                </td>
                <td width="14%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>申请时间</strong>
                </td>
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>审核状态</strong>
                </td>
                <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
          </HeaderTemplate>
          <ItemTemplate>
            <tr class="baidi" onmouseover="MQAudit.mouseovertr(this)" onmouseout="MQAudit.mouseouttr(this)">
                <td height="25" align="center">
                    <strong></strong>
                  
                    <%=itemIndex++%>
                </td>
                <td align="center">
                    <%# Eval("ProvinceName") %>-<%# Eval("CityName") %>
                </td>
                <td height="25" align="center">
                    <a href="javascript:;"><%# Eval("CompanyName") %></a>
                  
                </td>
                <td align="center">
                    <a href="javascript:void(0)" onmouseover="wsug(this, '联系人:<%# Eval("ContactName") %><br/>手机:<%# Eval("ContactMobile") %><br/>电话:<%# Eval("ContactTel") %><br/>MQ:<%# Eval("ContactMQ") %><br />')"
                        onmouseout="wsug(this, 0)">联系人：<%# Eval("ContactName") %>
                     
                    </a>
                </td>
                <td align="center">
                    <%# Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy-MM-dd")%>
                </td>
                 <td align="center">
                    <%#((EyouSoft.Model.SystemStructure.ApplyServiceState)Eval("ApplyState")).ToString()%>
                </td>
                <td align="center" id="ama_parentTd" >
                    <a href="javascript:void(0)" onclick="return MQAudit.openCheck(this);">
                        审核</a>
                    <div class="white_content" style="text-align: right; z-index:10000">
                        <a href="javascript:void(0)" onclick="return MQAudit.closeCheck(this);">
                            关闭</a>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    子账号数：<input name="mq_sonUserNum"  value='<%# Eval("CheckText") %>' type="text" value="0" class="textfield" size="6" onblur="return MQAudit.checkSonNum(this)"/>
                                    <span style='color:red'></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    有 效 期：<input name="mq_enableDate"  value='<%# Convert.ToDateTime(Eval("EnableTime")).ToString("yyyy-MM-dd")%>'  type="text" class="textfield" size="10" onfocus="WdatePicker()"/>-<input
                                        name="mq_expireDate" type="text" value='<%#  Convert.ToDateTime(Eval("ExpireTime")).ToString("yyyy-MM-dd")%>'  class="textfield" size="10" onfocus="WdatePicker()" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    审 核 人 ：<input name="mq_auditId" type="text" value='<%#GetSysUserName(Convert.ToInt32(Eval("OperatorId")))%>' class="textfield" size="15" disabled="disabled"  />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    审核时间 ：<input name="mq_auditDate" value='<%# Convert.ToDateTime(Eval("CheckTime")).ToString("yyyy-MM-dd")%>'  type="text" class="textfield"  disabled="disabled" size="15" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="button" value="审核通过" onclick="MQAudit.audit(this,'<%#Eval("ApplyId") %>',1)" />
                                    <input type="button" value="审核不通过" onclick="MQAudit.audit(this,'<%#Eval("ApplyId") %>',2)" />
                                </td>
                            </tr>
                        </table>
                        <iframe frameborder="0" marginheight="0" marginwidth="0" scrolling="no" style="position:absolute; visibility:inherit; top:0px; left:0px; width:100%; height:100%; z-index:-1; ">
                        </iframe>
                    </div>
                </td>
            </tr>
          </ItemTemplate>
          <AlternatingItemTemplate>
            <tr bgcolor="#F3F7FF" onmouseover="MQAudit.mouseovertr(this)" onmouseout="MQAudit.mouseouttr(this)">
                <td height="25" align="center">
                    <strong></strong>
                   <%=itemIndex++%>
                </td>
                <td align="center">
                    <%# Eval("ProvinceName") %>-<%# Eval("CityName") %>
                </td>
                <td height="25" align="center">
                    <a href="javascript:;"><%# Eval("CompanyName") %></a>
                  
                </td>
                <td align="center">
                    <a href="javascript:void(0)" onmouseover="wsug(this, '联系人:<%# Eval("ContactName") %><br/>手机:<%# Eval("ContactMobile") %><br/>电话:<%# Eval("ContactTel") %><br/>MQ:<%# Eval("ContactMQ") %><br />')"
                        onmouseout="wsug(this, 0)">联系人：<%# Eval("ContactName") %>
                      
                    </a>
                </td>
                <td align="center">
                    <%# Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy-MM-dd")%>
                </td>
                 <td align="center">
                    <%# ((EyouSoft.Model.SystemStructure.ApplyServiceState)Eval("ApplyState")).ToString()%>
                </td>
                <td align="center" id="ama_parentTd">
                    <a href="javascript:void(0)" onclick="return MQAudit.openCheck(this);">
                        审核</a>
                    <div class="white_content" style="text-align:right;z-index:10000">
                        <a href="javascript:void(0)" onclick="return MQAudit.closeCheck(this);">
                            关闭</a>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    子账号数：<input name="mq_sonUserNum" type="text" value='<%# Eval("CheckText") %>' value="0" class="textfield" size="6"  onblur="return MQAudit.checkSonNum(this)" />
                                    <span style='color:red'></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    有 效 期：<input name="mq_enableDate" type="text" value='<%# Convert.ToDateTime(Eval("EnableTime")).ToString("yyyy-MM-dd")%>' class="textfield" size="10" onfocus="WdatePicker()" />-<input
                                        name="mq_expireDate" type="text" value='<%# Convert.ToDateTime(Eval("ExpireTime")).ToString("yyyy-MM-dd")%>' class="textfield" size="10" onfocus="WdatePicker()" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    审 核 人 ：<input name="mq_auditId" value='<%# GetSysUserName(Convert.ToInt32(Eval("OperatorId")))%>' sourceValue='<%#Eval("OperatorId") %>' type="text" class="textfield" size="15" disabled="disabled" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    审核时间 ：<input name="mq_auditDate" value='<%#Convert.ToDateTime(Eval("CheckTime")).ToString("yyyy-MM-dd")%>' type="text" class="textfield" size="15" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="button" value="审核通过" onclick="MQAudit.audit(this,'<%#Eval("ApplyId") %>',1)" />
                                    <input type="button" value="审核不通过" onclick="MQAudit.audit(this,'<%#Eval("ApplyId") %>',2)" />
                                </td>
                            </tr>
                        </table>
                        <iframe frameborder="0" marginheight="0" marginwidth="0" scrolling="no" style="position:absolute; visibility:inherit; top:0px; left:0px; width:100%; height:100%; z-index:-1; ">
                        </iframe>
                    </div>
                </td>
            </tr>
          </AlternatingItemTemplate>
          <FooterTemplate>
                <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>序号</strong>
                    </td>
                    <td width="14%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>类型</strong>
                    </td>
                    <td width="25%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>单位名称</strong>
                    </td>
                    <td width="19%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>联系方式</strong>
                    </td>
                    <td width="14%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>加入时间</strong>
                    </td>
                     <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>审核状态</strong>
                    </td>
                    <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>审核</strong>
                    </td>
                </tr>
                </table>
          </FooterTemplate>
      </asp:CustomRepeater>
    
 <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </td>
        </tr>
    </table>
