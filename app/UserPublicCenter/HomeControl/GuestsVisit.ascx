<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuestsVisit.ascx.cs" Inherits="UserPublicCenter.HomeControl.GuestsVisit" %>
     <%@ Import Namespace="EyouSoft.Common" %>
 <div class="mainbox04-sidebar02">
          <div class="imgArea-title">
            <h2 style="font-size:14px;"><s class="hyzx"></s>名人访谈</h2>
            <a href="<%=EyouSoft.Common.URLREWRITE.Infomation.GetNewsListZhuanTiUrl(9) %>" target="_blank">更多<b style="color:#FF6600;">></b></a></div>
        
          <div class="imgArea-cont02"  style="height:301px">
            <div class="hr_10"></div>
            <%=guestsHtml %>
           
            
            <div class="Community">
              <div class="imgArea-title02" style="background:none; border-bottom:1px #dddddd solid;_height:12px;">
                <h2 style="font-size:14px;"><s class="hyzx"></s>同业资讯</h2>
                <a href="http://club.tongye114.com/"  target="_blank" >更多<b style="color:#FF6600;">></b></a></div>
              <ul>
                  <asp:Repeater ID="rptPeerNewList" runat="server">
                      <ItemTemplate>
                            <li style="height:22px; padding:0;"><a title="<%#Eval("Title")%>" target="_blank" href="<%=EyouSoft.Common.Domain.UserBackCenter %>/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>"><%# Utils.GetText(Convert.ToString(Eval("Title")),15,false)%></a><em><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></em></li>
                      </ItemTemplate>
                  </asp:Repeater>
              </ul>
            </div>
          </div>
        </div>
        
        