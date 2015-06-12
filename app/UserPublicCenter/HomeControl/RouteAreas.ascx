<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RouteAreas.ascx.cs" Inherits="UserPublicCenter.HomeControl.RouteAreas" %>
<div class="glcx_l">
     <div class="glcx_li">
       <h2><span class="h2title"><img width="72" height="18" alt=" " src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/changx_03.jpg"></span><span class="h2neirong">品牌推荐：<%=InremmCompany.ToString() %> </span><span class="h2more"><a href="/TourManage/TourList.aspx?RouteType=0" target="_blank">更多&gt;&gt;</a></span></h2>
       <%=inAreaHtml %>
       <div class="kkkk"></div>
     </div>
     <div class="px10"></div>
     <div style="width:720px; height:79px;"><a target="_blank" href="<%=EyouSoft.Common.Domain.SeniorOnlineShop%>/shop_3dd53e9b-7cf5-4515-8ad2-cf4bd547b92a_362"><img width="720px" height="79px" alt="" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/{C3FBA927-4ECB-42D8-BFE9-E1DAB355F7B8}.jpg"></a></div>
     <div class="px10"></div>
     <div class="glcx_li">
       <h2><span class="h2title"><img alt=" " src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/changx_07.jpg"></span><span class="h2neirong">品牌推荐：<%=outremmCompany.ToString() %> </span><span class="h2more"><a href="/TourManage/TourList.aspx?RouteType=1" target="_blank">更多&gt;&gt;</a></span></h2>
       <%=outAreaHtml%>
     </div>
     <div class="px10"></div>
     <div class="glcx_li">
       <h2><span class="h2title"><img alt=" " src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/changx_10.jpg"></span><span class="h2neirong">品牌推荐：<%=sideremmCompany.ToString() %> </span><span class="h2more"><a href="/TourManage/TourList.aspx?RouteType=2" target="_blank">更多&gt;&gt;</a></span></h2>
       <%=sideAreaHtml%>
     </div>
  </div>