<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TourUnion.WEB.IM.Register.Default" %>

<%@ Register Src="../../WebControls/OurTheme/Head.ascx" TagName="Head" TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/OurTheme/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>注册</title>
    <link href="/IM/css/main.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="/js/jsc.js"></script>

    <script language="javascript" src="/js/indextab.js"></script>

    <script language="javascript" src="/js/common.js"></script>

</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <uc1:Head ID="Head1" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 60px;">
                    <tr>
                        <td width="320" height="147" align="center">
                            <table width="50%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <img src="/IM/images/joinzt.gif" width="310" height="23" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="150" align="center" style="border-left: 1px solid #EBDBAA; border-right: 1px solid #EBDBAA;
                                        background: #FFFCF2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td align="left">
                                                    <p>
                                                        同业114平台汇聚全国各地优秀的旅游专线商（批发商），在这里您可以方便的查阅、订购旅游产品，马上加入我们吧！原来可以这么轻松完成旅游交易！</p>
                                                </td>
                                            </tr>
                                        </table>
                                        <a href="/Register/Register.aspx">
                                            <img src="/IM/images/ztjoin.gif" width="206" height="32" border="0" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="/IM/images/joinbj.gif" width="310" height="6" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="320" align="center">
                            <table width="50%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <img src="/IM/images/joinzx.gif" width="311" height="23" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="150" align="center" style="border-left: 1px solid #EBDBAA; border-right: 1px solid #EBDBAA;
                                        background: #FFFCF2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td align="left">
                                                    <p>
                                                        同业114平台提供便捷的线路发布、收客计划管理、订单受理等业务操作功能，从而能让组团社在第一时间了解您的产品和计划，让您的交易更便捷、更省心。
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                        <a href="/Register/CompanyRegister.aspx">
                                            <img src="/IM/images/zxjoin.gif" width="206" height="32" border="0" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="/IM/images/joinbj.gif" width="310" height="6" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="320" align="center">
                            <table width="50%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <img src="/IM/images/joindj.gif" width="310" height="23" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="150" align="center" style="border-left: 1px solid #EBDBAA; border-right: 1px solid #EBDBAA;
                                        background: #FFFCF2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td align="left">
                                                    <p>
                                                        同业114平台拥有强大的专线分销网络，覆盖全国80%中大型城市，让您的旅游产品延伸更迅速，客户覆盖 更全面！点击注册立刻加入我们。</p>
                                                </td>
                                            </tr>
                                        </table>
                                        <a href="/Register/LocalRegister.aspx">
                                            <img src="/IM/images/djjoin.gif" width="156" height="32" border="0" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="/IM/images/joinbj.gif" width="310" height="6" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td height="80" align="left"><a href="/Register/SuppliersRegister.aspx?Type=Scenic"><img src="../images/styelogin_03.jpg" width="172" height="51" border="0" /></a></td>
          <td height="80" align="left"><a href="/Register/SuppliersRegister.aspx?Type=Hotel"><img src="/images/styelogin_04.jpg" width="172" height="51" border="0" /></a></td>
          <td align="center"><a href="/Register/SuppliersRegister.aspx?Type=Team"><img src="../images/styelogin_05.jpg" width="172" height="51" border="0" /></a></td>
          <td align="center"><a href="/Register/SuppliersRegister.aspx?Type=TravelProducts"><img src="../images/styelogin_07.jpg" width="222" height="51" border="0" /></a></td>
          <td align="right"><a href="/Register/SuppliersRegister.aspx?Type=Shopping"><img src="../images/styelogin_09.jpg" width="195" height="51" border="0" /></a></td>
        </tr>
      </table>
            </td>
        </tr>
    </table>
    
    <uc2:Foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
