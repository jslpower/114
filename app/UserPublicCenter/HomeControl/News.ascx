<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="News.ascx.cs" Inherits="UserPublicCenter.HomeControl.News" %>
    <%@ Import Namespace="EyouSoft.Common" %>
  <div class="mainbox04-sidebar">
          <div class="imgArea-title">
            <h2 style="font-size:14px;"><s class="hyzx"></s>行业动态</h2>
            <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.InfoDefaultUrlWrite() %>">更多<b style="color:#FF6600;">></b></a></div>
         
          <div class="imgArea-cont">
            <div class="hr_10"></div>
            <div class="fixed">
              <div class="imgLAreapic"  style="width:111px; height:104px">
                <%=newImg%>
              </div>
              <div class="imgRArea">
              <asp:Repeater ID="rptNew1" runat="server">
                <ItemTemplate>
               <div style="width:202px; overflow:hidden; height:23px;"> <a href='<%#EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl((int)Eval("AfficheClass")) %>' class="zx-title">[<%# Eval("ClassName") %>]</a> <a href="<%# GetUrl(Eval("GotoUrl"),Eval("Id")) %>"  title='<%# Eval("AfficheTitle") %>'  target="_blank"><%# Utils.GetText(Eval("AfficheTitle").ToString(),11)%></a></div>
                </ItemTemplate>
              </asp:Repeater>
                </div>
            </div>
            <div class="hr_5"></div>
            <ul class="textArea fixed" style="height:66px;">
            <asp:Repeater ID="rptNew2" runat="server">
               <ItemTemplate>
                 <li style="height:22px; line-height:22px;"><a title="<%#Eval("AfficheTitle") %>" href="<%# GetUrl(Eval("GotoUrl"),Eval("Id")) %>"><%# Utils.GetText(Eval("AfficheTitle").ToString(),12)%></a></li>
              </ItemTemplate>
            </asp:Repeater>
            </ul>
            <div class="hr_5"></div>
            <div class="jiaguanj">
      <div class="jiagunj_b"></div>
      <div class="jiaguanlo">
        <dl>
          <dd><img width="87" height="60" alt=" " src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/home2_59.jpg"></dd>
          <dt><a href="http://www.enowinfo.com">杭州易诺科技</a><img width="15" height="11" alt=" " src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/new2011/index/v_06.jpg"><span>浙江 杭州</span></dt>
          <dt class="jiaanniu"><a  rel="nofollow" href="http://widget.weibo.com/weiboshow/index.php?language=&width=0&height=550&fansRow=2&ptype=1&speed=0&skin=1&isTitle=0&noborder=0&isWeibo=0&isFans=0&uid=2217932193&verifier=d0eaf97b&dpc=1">加关注</a></dt>
        </dl>
      </div>
    </div>
          </div>
        </div>