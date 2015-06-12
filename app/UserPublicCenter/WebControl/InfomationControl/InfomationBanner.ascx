<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfomationBanner.ascx.cs"
    Inherits="UserPublicCenter.WebControl.InfomationControl.InfomationBanner" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<div class="newsNav">
    <div class="newsNavR">
        <div class="newsNavC">
            <ul>
                <li class="tot1">
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(1) %>" >行业新闻</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(2) %>" >旅行社</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(3) %>" >酒店</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(5) %>" >景区</a> |
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(9) %>" >嘉宾访谈</a>  
                     <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(7) %>" >旅游展会</a> |
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(8) %>" >旅游局</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(4) %>" >交通</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(6) %>" >专题</a> |
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(10) %>" >游记攻略</a> 
                </li>
                <li class="tot2">
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(11) %>">旅游营销</a> | 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(12) %>">旅行社经营管理</a>| 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(13) %>">计调指南</a> 
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(14) %>">导游带团</a> | 
                     <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(17) %>">  旅游政策法规</a>|
                    <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(15) %>">  营销工具</a> 
                   
                </li>
                <li class="tot3">
                    <a href="http://club.tongye114.com/showforum-97.aspx">经验交流</a> | 
                    <a href="http://club.tongye114.com/dest/list.aspx">地方特色旅游 </a>
                    <a href="http://club.tongye114.com/showforum-96.aspx">广告区</a> | 
                    <a href="http://club.tongye114.com/showforum-98.aspx">新手区</a> 
                </li>
            </ul>
        </div>
    </div>
</div>
