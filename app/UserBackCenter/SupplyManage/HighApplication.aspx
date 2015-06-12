<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HighApplication.aspx.cs"
    Inherits="UserBackCenter.SupplyManage.HighApplication" %>

<asp:content id="HighApplication" runat="server" contentplaceholderid="ContentPlaceHolder1">
<style type="text/css">
    body
    {
        margin: 0 auto;
        padding: 0;
        margin-left: 0px;
        margin-right: 0px;
        background-color: White;
    }
    td
    {
        font-size: 12px;
    }
</style>
     <table width="800" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td bgcolor="#E03231">
                <span style="font-size: 14px; font-weight: bold; color: #ffffff; text-align:left">
                <%if (IsApply)
                  { %>
                    贵公司已提交申请，如比较急请致电客服！
                <%}
                  else
                  { %>
                立即申请
                <%} %>
                <a name="sq" id="sq"></a></span>
            </td>
        </tr>
    </table>
    <table width="800" border="0" id="tbl_HighApplication" align="center" cellpadding="0"
        cellspacing="0" style="background: url(<%=ImageServerUrl %>/images/b-bj.gif) no-repeat top;">
        <tr>
            <td width="101" align="center" style="font-size: 14px; padding: 10px;">
                <img src="<%=ImageServerUrl %>/images/dhsq.gif" width="117" height="39" />
            </td>
            <td width="699" align="left" style="font-size: 14px; padding: 10px;">
                0571-56884918 MQ：<a href="javascript:void(0)" onclick="window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid=28733')"
                    title="点击MQ图标洽谈！"><img src="<%=ImageServerUrl %>/images/MQWORD.gif" style="border: 0" /></a>
                QQ：<a href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img
                    src="http://wpa.qq.com/pa?p=1:774931073:10" border="0" alt="点击这里给我发消息" width="61"
                    height="16" /></a> 陈小姐；
                <br />
                0571-56884627 MQ：<a href="javascript:void(0)" onclick="window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid=22547')"
                    title="点击MQ图标洽谈！"><img src="<%=ImageServerUrl %>/images/MQWORD.gif" style="border: 0" /></a>
                QQ：<a href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img
                    src="http://wpa.qq.com/pa?p=1:774931073:10" border="0" alt="点击这里给我发消息" width="61"
                    height="16" /></a> 何小姐。
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
         
                
                <table width="582" border="0" align="center" cellpadding="0" cellspacing="0" style="background:url(<%=ImageServerUrl %>/images/shengqinghao.gif) no-repeat; height:123px; width:582px;">
  <tr>
    <td width="181" valign="top" style="font-size:12px; line-height:20px; color:#666; padding:40px 15px 0 20px;">通过图文的形式全面<br />
      地展示企业信息，<br />
      增加客户的合作信心。</td>
    <td width="211" valign="top" style="font-size:12px; line-height:20px; color:#666; padding:40px 10px 0 0px;">展示企业的详细联系方式，
      如<br />联系人、电话，让客户      
      在有<br />采购需求时能找准
联系人。</td>
    <td width="190" valign="top" style="font-size:12px; line-height:20px; color:#666; padding:40px 10px 0 0px;">发布企业的最新动态，让
      同业114平台近3万且不断
      增长的客户资源，在第一
      时间掌握企业的促销以及
      优惠活动。</td>
  </tr>
</table>
                <table width="800px" border="0" cellspacing="0" cellpadding="0" align="center" style="border:1px solid #CCC; background:url(<%=ImageServerUrl %>/images/shengqingbg.gif) repeat-x; height:207px;">
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>公司名称：</strong>
                        </td>
                        <td align="left">
                            <input name="HighApplication_CompanyName" type="text" class="shurukuangsm" value="<%=CompanyName %>"
                                readonly="readonly" size="20" style="border: 0px solid #ffffff; background: #CAF2F7" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>申请人：</strong>
                        </td>
                        <td align="left">
                            <input name="HighApplication_AppName" id="txt_HighApplication_AppName" value="<%=ContactName %>" valid="required"
                                errmsg="申请人不能为空！" type="text" class="shurukuangsm" size="20" />
                                <span id="errMsg_txt_HighApplication_AppName" class="errmsg" style="position:absolute;"></span>
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>联系电话：</strong>
                        </td>
                        <td align="left">
                            <input name="HighApplication_Tel" id="txt_HighApplication_Tel" value="<%=Tel %>" valid="required|isPhone" errmsg="电话号码不能为空|电话号码填写错误"
                                type="text" class="shurukuangsm" size="20" />
                                <span id="errMsg_txt_HighApplication_Tel" class="errmsg" style="position:absolute;"></span>
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>手机：</strong>
                        </td>
                        <td align="left">
                            <input name="HighApplication_Mobile" id+"txt_HighApplication_Mobile" value="<%=Mobile %>" valid="required|isMobile"
                                errmsg="手机号码不能为空|手机号码填写错误" type="text" class="shurukuangsm" size="20" />
                            <span id="errMsg_txt_HighApplication_Mobile" class="errmsg" style="position:absolute;"></span>
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>地址：</strong>
                        </td>
                        <td align="left">
                            <input name="HighApplication_Address" value="<%=Address %>" type="text" class="shurukuang"
                                size="20" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <%if (!IsApply)
                              { %>
                            <input type="image" name="imageField" id="btnHighApplication_Save" src="<%=ImageServerUrl %>/images/add_button.gif" />
                            <%} %>
                        </td>
                    </tr>
                    <tr align="center">
                        <td height="5">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        var HighApplication = {
            save: function() {
                if (ValiDatorForm.validator($("#tbl_HighApplication").closest("form").get(0), "alertspan")) {
                    $.newAjax({
                        type: "POST",
                        url: "/supplymanage/highapplication.aspx?action=save",
                        data: $($("#tbl_HighApplication").closest("form").get(0)).serialize(),
                        success: function(msg) {
                            var returnMsg = eval(msg);
                            if (returnMsg) {
                                alert(returnMsg[0].ErrorMessage);
                                if (returnMsg[0].isSuccess) {
                                    topTab.url(topTab.activeTabIndex, "/supplymanage/highapplication.aspx");
                                    return false;
                                }
                            } else {
                                alert("对不起，保存失败！");
                            }
                        }
                    });
                }
                return false;
            },
            pageInit: function() {
                $("#btnHighApplication_Save").click(function() {
                    HighApplication.save();
                    return false;
                });
            }
        }
        $(function() {
            HighApplication.pageInit();
        })
    </script>
</asp:content>
