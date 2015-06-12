<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBooking.aspx.cs" Inherits="UserBackCenter.RouteAgency.AjaxBooking" %>
&nbsp;预订人： <strong><%=ContactName%> </strong>&nbsp;&nbsp;电话： <strong><%=Tel%></strong> &nbsp;&nbsp;&nbsp;传真：
<strong><%=Fax%></strong> &nbsp;&nbsp;&nbsp;手机： <strong><%=Phone%> </strong>

<input id="BuyCompanyContactName" name="BuyCompanyContactName" value="<%=ContactName %>" type="hidden" />
<input id="BuyCompanyTel" name="BuyCompanyTel"  value="<%=Tel %>"type="hidden" />
<input id="BuyCompanyFax" name="BuyCompanyFax"  value="<%=Fax %>"type="hidden" />
<input id="BuyCompanyMQ" name="BuyCompanyMQ" value="<%=MQ %>" type="hidden" />
<input id="BuyCompanyQQ" name="BuyCompanyQQ" value="<%=QQ %>" type="hidden" />
<input id="BuyCompanyPhone" name="BuyCompanyPhone" value="<%=Phone %>" type="hidden" />


