<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommendCompany.aspx.cs"
    Inherits="TourUnion.WEB.IM.CommendCompany" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        <!
        -- BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        .bar_on_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            border: 1px solid #94B2E7;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
            color: #cc0000;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
        .alginCenter
        {
            text-align:center;
        }
        -- ></style>
        <script language="javascript" src="/js/ext/jquery.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
    
            function GetDefalutText(obj,Text)
          {
               if(obj.value==""||obj.value.replace(/^[" "|"　"]*/, "")=="")
               {
                    obj.value=Text;
                     obj.style.color="#999999";
               }
          }
          function DeleteText(obj,Text)
          {
                if(obj.value==Text)
                {
                    obj.value="";
                }
                obj.style.color="#333";
          }
          
            $(function() {
                $("#txtCompanyName").focus(function(){DeleteText(this,"请输入批发商公司名称")});
                $("#txtCompanyName").blur(function(){GetDefalutText(this,"请输入批发商公司名称")});
                $("#txtContactName").focus(function(){DeleteText(this,"请输入负责人")});
                $("#txtContactName").blur(function(){GetDefalutText(this,"请输入负责人")});
                $("#txtContactTel").focus(function(){DeleteText(this,"请输入联系电话")});
                $("#txtContactTel").blur(function(){GetDefalutText(this,"请输入联系电话")});
                $("#txtContactMobil").focus(function(){DeleteText(this,"请输入手机号码")});
                $("#txtContactMobil").blur(function(){GetDefalutText(this,"请输入手机号码")});
                $("#txtAreaContent").focus(function(){DeleteText(this,"请说明对方经营的专线")});
                $("#txtAreaContent").blur(function(){GetDefalutText(this,"请说明对方经营的专线")});
            });
        </script>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="26" valign="top" background="/Images/ztopbj.gif">
                    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 6px;">
                        <tr>
                            <td width="134" align="center">
                                <div class="bar_on_comm">
                                    我推荐供应商</div>
                            </td>
                            <td width="76" align="center">
                                <a href="/IM/TourAgency/TourManger/SetAttentionCompany.aspx?IsSettionCompany=1">
                                    <img src="images/back.gif" alt="后退" width="16" height="16" border="0"  />返回</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtCompanyName" runat="server"  Width="200px" 
                        Text="请输入批发商公司名称" ForeColor="#999999" />
                </td>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtCompanyName" ValueToCompare="请输入批发商公司名称" runat="server" ErrorMessage="请输入批发商公司名称" Operator="NotEqual">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtContactName" runat="server" Width="200px" Text="请输入负责人" 
                        ForeColor="#999999" />
                </td>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtContactName" ValueToCompare="请输入负责人" runat="server" ErrorMessage="请输入负责人" Operator="NotEqual">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtContactTel" runat="server" Width="200px" Text="请输入联系电话" 
                        ForeColor="#999999" />
                </td>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtContactTel" ValueToCompare="请输入联系电话" runat="server" ErrorMessage="请输入联系电话" Operator="NotEqual">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtContactMobil" runat="server" Width="200px" Text="请输入手机号码" 
                        ForeColor="#999999" />
                </td>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtAreaContent" runat="server" Width="200px" Text="请说明对方经营的专线" 
                        ForeColor="#999999" />
                </td>
                <td height="30" align="left" bgcolor="#FFFFFF">
                    <asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtAreaContent" ValueToCompare="请说明对方经营的专线" runat="server" ErrorMessage="请说明对方经营的专线" Operator="NotEqual">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td height="25" align="center" bgcolor="#FFFFFF" colspan="2">
                    <asp:Button ID="Button1" runat="server" Width="80px" Height="25px" Text="提交" 
                        onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td height="25" align="center" bgcolor="#FFFFFF" colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="alginCenter" runat="server" ShowMessageBox="False"
            ShowSummary="True" />            
                </td>
            </tr>
        </table>        
    </div>
    </form>
</body>
</html>
