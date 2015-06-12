<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Treatment.aspx.cs" Inherits="UserBackCenter.TicketsCenter.Itinerary.Treatment" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("jipiaoadminstyle") %>" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <!--right start-->
   	  <div class="admin_right">
      	<div class="rigtop" style="display:none;">
       	  <span style="text-align:left; display:block; padding-left:25px; line-height:25px; color:#0056A3;">您好，华盛顿　欢迎登录同业114。<br />目前平台已经有 <strong class="chengse">25648</strong> 家机票供应商加盟</span>
        </div>

        <table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8" style="display:none;">
          <tr>
            <th width="167" height="30" bgcolor="#EEF7FF">订单号</th>
            <th width="167" bgcolor="#EEF7FF">旅客名字</th>
            <th width="167" bgcolor="#EEF7FF">订单日期</th>
            <th width="167" bgcolor="#EEF7FF">打印日期</th>
            <th width="167" bgcolor="#EEF7FF">服务方式</th>

          </tr>
          <tr>
            <td height="30" align="center"><a href="#">01234567891</a></td>
            <td align="center">王先生</td>
            <td align="center">2010-9-10</td>
            <td align="center">2010-9-16</td>
            <td align="center">邮递</td>

          </tr>
          <tr>
            <td height="30" align="center"><a href="#">01234567892</a></td>
            <td align="center">张先生</td>
            <td align="center">2010-9-10</td>
            <td align="center">2010-9-16</td>
            <td align="center">支付后上门取</td>

          </tr>
          <tr>
            <td height="30" align="center"><a href="#">01234567893</a></td>
            <td align="center">李先生</td>
            <td align="center">2010-9-10</td>
            <td align="center">2010-9-16</td>
            <td align="center">支付后上门取</td>

          </tr>
        </table><table width="835" border="1" cellpadding="0" cellspacing="0" bordercolor="#7dabd8">
          <tr>
            <th width="93" height="30" align="center" bgcolor="#EEF7FF">行程单号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">订单号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">订单日期</th>
            <th width="93" align="center" bgcolor="#EEF7FF">旅客名字</th>

            <th width="93" align="center" bgcolor="#EEF7FF">服务方式</th>
            <th width="93" align="center" bgcolor="#EEF7FF">票号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">打印日期</th>
            <th width="93" align="center" bgcolor="#EEF7FF">张数</th>
            <th width="93" align="center" bgcolor="#EEF7FF">总价（元）</th>
          </tr>

          <tr>
            <td height="30" align="center">001</td>
            <td align="center"><a href="#">01234567891</a></td>
            <td align="center">2010-9-10</td>
            <td align="center">王先生</td>
            <td align="center">邮递</td>

            <td align="center">1</td>
            <td align="center">2010-9-16</td>
            <td align="center">10</td>
            <td align="center">200</td>
          </tr>
         <tr>
            <td colspan="9" align="center"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EEF7FF">

              <tr>
                <th width="17%" height="25" align="center">收件人详情：</th>
                <td width="17%" align="left"><strong>公司：</strong>杭州易诺科技</td>
                <td width="17%" align="left"><strong>收件人：</strong>王先生</td>
                <td align="left"><strong>联系方式：</strong>13819168563</td>

              </tr>
              <tr>
                <th width="17%" height="25" align="center">&nbsp;</th>
                <td align="left"><strong>邮编：</strong>310012</td>
                <td align="center">&nbsp;</td>
                <td align="left"><strong>地址：</strong>浙江省杭州市西湖区文三西路459号</td>
              </tr>

            </table></td>
          </tr>
        </table>
		<table width="835" border="1" cellpadding="0" cellspacing="0" bordercolor="#7dabd8" style="margin-top:5px;">
          <tr>
            <th width="93" height="30" align="center" bgcolor="#EEF7FF">行程单号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">订单号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">订单日期</th>

            <th width="93" align="center" bgcolor="#EEF7FF">旅客名字</th>
            <th width="93" align="center" bgcolor="#EEF7FF">服务方式</th>
            <th width="93" align="center" bgcolor="#EEF7FF">票号</th>
            <th width="93" align="center" bgcolor="#EEF7FF">打印日期</th>
            <th width="93" align="center" bgcolor="#EEF7FF">张数</th>
            <th width="93" align="center" bgcolor="#EEF7FF">总价（元）</th>

          </tr>
          <tr>
            <td height="30" align="center">002</td>
            <td align="center"><a href="#">01234567892</a></td>
            <td align="center">2010-9-10</td>
            <td align="center">张先生</td>
            <td align="center">邮递</td>

            <td align="center">2</td>
            <td align="center">2010-9-16</td>
            <td align="center">20</td>
            <td align="center">400</td>
          </tr>
         <tr>
            <td colspan="9" align="center"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EEF7FF">

              <tr>
                <th width="17%" height="25" align="center">收件人详情：</th>
                <td width="17%" align="left"><strong>公司：</strong>杭州易诺科技</td>
                <td width="17%" align="left"><strong>收件人：</strong>张先生</td>
                <td align="left"><strong>联系方式：</strong>13202005687</td>

              </tr>
              <tr>
                <th width="17%" height="25" align="center">&nbsp;</th>
                <td align="left"><strong>邮编：</strong>310012</td>
                <td align="center">&nbsp;</td>
                <td align="left"><strong>地址：</strong>浙江省杭州市西湖区文二西路459号</td>
              </tr>

            </table></td>
          </tr>
        </table>
   	  </div>
   	  <!--right end-->

    </div>
    </form>
</body>
</html>
