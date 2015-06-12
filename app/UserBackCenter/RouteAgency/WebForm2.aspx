<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="UserBackCenter.RouteAgency.WebForm2" %>

<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="../usercontrol/UserOrder/OrderOutSourceSearch.ascx" TagName="OrderOutSourceSearch"
    TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/SupplyInformation/SupplyInfoTab.ascx" TagName="SupplyInfoTab"
    TagPrefix="uc2" %>
<%@ Register Src="../SupplyInformation/UserControl/AttatchPathFileUpload.ascx" TagName="AttatchPathFileUpload"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="800" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td bgcolor="#E03231">
                <span style="font-size: 14px; font-weight: bold; color: #ffffff;">立即申请<a name="sq"
                    id="sq"></a></span>
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
                <table width="70%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 20px;">
                    <tr>
                        <td width="16%" align="center" style="line-height: 160%;">
                            <img src="<%=ImageServerUrl %>/images/cg.gif" width="60" height="60" />
                        </td>
                        <td width="84%" style="line-height: 160%;">
                            &nbsp;&nbsp;&nbsp;
                            <%if (IsApply)
                                                          { %><strong style="color: #cc0000;"> 您已成功提交开通“同业高级网店”申请!
                            </strong>
                            <%} %>
                            &nbsp;&nbsp;&nbsp; “同业高级网店”属于同业114平台的收费项目之一，我们将会在一个工作日内与您联系，如您急需开通，请拨打0571-5884918，谢谢！
                        </td>
                    </tr>
                </table>
                <table width="80%" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>公司名称：</strong>
                        </td>
                        <td>
                            <input name="HighApplication_CompanyName" type="text" class="shurukuangsm" value="<%=CompanyName %>"
                                readonly="readonly" size="20" style="border: 0px solid #ffffff; background: #CAF2F7" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>申请人：</strong>
                        </td>
                        <td>
                            <input name="HighApplication_AppName" value="<%=ContactName %>" valid="required"
                                errmsg="申请人不能为空！" type="text" class="shurukuangsm" size="20" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>联系电话：</strong>
                        </td>
                        <td>
                            <input name="HighApplication_Tel" value="<%=Tel %>" valid="required|isPhone" errmsg="电话号码不能为空|电话号码填写错误"
                                type="text" class="shurukuangsm" size="20" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>手机：</strong>
                        </td>
                        <td>
                            <input name="HighApplication_Mobile" value="<%=Mobile %>" valid="required|isMobile"
                                errmsg="手机号码不能为空|手机号码填写错误" type="text" class="shurukuangsm" size="20" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td height="30" align="right">
                            <strong>地址：</strong>
                        </td>
                        <td>
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
                        <td height="50">
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
        var HighApplication={
            save:function(){
                if(ValiDatorForm.validator($("#tbl_HighApplication").closest("form").get(0),"alertspan"))
                {
                    $.newAjax({
                       type: "POST",
                       url: "/supplymanage/highapplication.aspx?action=save",
                       data:$($("#tbl_HighApplication").closest("form").get(0)).serialize(),
                       success: function(msg){ 
                         var returnMsg=eval(msg);                            
                         if(returnMsg)
                         {        
                            if(returnMsg[0].isSuccess){  
                                topTab.url(topTab.activeTabIndex,"/supplymanage/highapplication.aspx"); 
                                return false;
                            }                   
                            alert(returnMsg[0].ErrorMessage);
                         }else{
                            alert("对不起，保存失败！");
                         }
                       }
                    });
                }
            },
            pageInit:function(){
                $("#btnHighApplication_Save").click(function(){
                    HighApplication.save();
                    return false;
                });
            }
        }
        $(function(){
            HighApplication.pageInit();
        })
    </script>

    </form>
</body>
</html>
