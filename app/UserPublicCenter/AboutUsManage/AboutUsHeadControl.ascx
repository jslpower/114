<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AboutUsHeadControl.ascx.cs"
    Inherits="UserPublicCenter.AboutUsManage.AboutUsHeadControl" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<div class="logo">
       <a href="<%=SubStation.CityUrlRewrite(CityId) %>">
                        <img src="<%=UnionLogo %>" alt="同业114" border="0" height="70px" width="170px" /></a></div>
<div style="width: 300px; height: 30px; float: left; padding-top: 40px; color: #ff6600;
    font-size: 30px; text-align: left">
    <b>&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/<%=ImageName %>" width="125"
        height="36" /></b></div>
<div class="tymq">
    <a href="http://im.tongye114.com" title="同业MQ免费下载">
        <img src="<%=ImageServerPath %>/images/UserPublicCenter/mqgif.gif" alt="同业MQ免费下载"
            border="0" /></a></div>
