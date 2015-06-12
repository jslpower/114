<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierInfo/SupplierNew.Master"
    AutoEventWireup="true" CodeBehind="ExchangeList.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.SupplierList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Src="~/WebControl/GongQiuPublish.ascx" tagname="gongqiu" tagprefix="ucgq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SupplierHead" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Show" runat="server">
   <ucgq:gongqiu ID="Gongqiu1" runat="server"></ucgq:gongqiu>
    <!--列表 end-->
    <div class="hr-10">
    </div>
    <a href="http://im.tongye114.com/" target="_blank" title="同业MQ"><img alt="同业MQ" src="<%=ImageServerUrl + "/images/news/news-list_19.gif"%>" /></a>
    <div class="hr-10">
    </div>
    <!--列表 start-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupplierBody" runat="server">
    <div id="supply-right">
        <!--产品筛选-->
        <h3 class="filtertitle">
            <span>全部信息- 筛选条件</span></h3>
        <div class="filterBox">
            <div class="division">
                <form action="/SupplierInfo/ExchangeList.aspx" method="get">
                <div class="screeBox">
                    
                        <div id="tags2">
                        
                            <span class="publish_info_btn">
                                <label><b>查询：</b></label>
                                
                                <input type="text" value="<%= string.IsNullOrEmpty(keyword) ? "请输入关键字" : keyword%>" onfocus="if(this.value == '请输入关键字') {this.value = '';}"
                                        onblur="if (this.value == '') {this.value = '请输入关键字';}" class="search" name="keyword" />
                                <input type="hidden" name="stime" value="<%=sTime %>" />
                                <input type="hidden" name="stag" value="<%=sTage %>" />
                                <input type="hidden" name="stype" value="<%=sType %>" />
                                <input type="hidden" name="sCat" value="<%=0 %>" />
                                <input type="hidden" name="sProvid" value="<%=sProvid %>" />
                                <input type="submit" class="sub" value="搜索" />
                                
                            </span>
                            
                        </div>
                    
                    <strong>时间：</strong><%=getTimeNavHtml(keyword, sTime, sTage, sType, sProvid, 0, CityId, UserPublicCenter.SupplierInfo.SupplierCom.TimeEnum(),1)%>
                </div>
                </form>
                <div class="screeBox">
                    <strong>标签：</strong><%=getTimeNavHtml(keyword, sTime, sTage, sType, sProvid, 0, CityId, UserPublicCenter.SupplierInfo.SupplierCom.TagEnum(),2)%>
                </div>
                <div class="screeBox">
                    <strong>类别：</strong><%=getTimeNavHtml(keyword, sTime, sTage, sType, sProvid, 0, CityId, UserPublicCenter.SupplierInfo.SupplierCom.CatEnum(),3)%>
                </div>
                <div class="screeBox">
                    <strong>省份：</strong><%=GetProNavHtml(keyword,sTime,sTage,sType,sProvid,0,CityId) %>
                </div>
                
            </div>
        </div>
        <!--产品筛选-->
        <div class="hr-10" style="display: none">
        </div>
        <!--列表 start-->
        <div class="box box-rightFix" style="position: static;">
            <div class="box-main">
                <div class="box-content box-content-main">
                    <div id="listContent">
                        <div id="tags">
                            <ul>
                                <li class="selectTag"><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,0,CityId)%>">
                                    全部信息</a> </li>
                            </ul>
                        </div>
                        <!--*****************************内容列表 start***********************************-->
                        <div class="hr-5">
                        </div>
                        <style type="text/css">
                          #tagContent .tagContent ul li div.publish label{ color:#828282; clear:both; display:block}
                        </style>
                        <div id="tagContent">
                            <div class="tagContent" style="display: block">
                                <ul>
                                    <li class="title">
                                        <div class="title1 title2">标题&nbsp;&nbsp;&nbsp;&nbsp;
                                            <a href="javascript:;" class="jslogin" hcat="1" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,1,CityId)%>">只看供应</a>
                                            <a href="javascript:;" class="jslogin" hcat="2" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,2,CityId)%>">只看求购</a>
                                            <a href="javascript:;" class="jslogin" hcat="3" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,3,CityId)%>">旅游QQ群供求</a>
                                        </div>
                                        <div class="publish publish2">发表</div>
                                    </li>
                                    <!--注：如果是急急急，标题连接a标签添加调用class=“ji”，调用的图片为：suplly_60.gif
                                           如果是热：图片为：suplly_68.gif
                                           无：suplly_72.gif
                                           求：suplly_79.gif
                                           特价：suplly_83.gif
                                   -->
                                    <!--信息列表循环输出() start-->
                                    <%if (exList != null && exList.Count > 0)
                                      {
                                          foreach (EyouSoft.Model.CommunityStructure.ExchangeList el in exList)
                                          { %>
                                    <li>
                                        <div class="title1">
                                            <span><img src="<%= ImageServerUrl + (el.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.求 ? "/images/news/tongyeMews_28.gif" : "/images/news/tongyeMews_25.gif")%>" alt="" /></span>
                                            <% =UserPublicCenter.SupplierInfo.SupplierCom.GetTagUrl(el.ExchangeTag, ImageServerUrl, CityId, sProvid)%>
                                            <span><a target="_blank" href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(el.ID,CityId)%>" title="#" <%=el.ExchangeTag == EyouSoft.Model.CommunityStructure.ExchangeTag.急急急?"class='ji'":"" %>><%=el.ExchangeTitle %></a></span> 
                                            <span><%=EyouSoft.Common.Utils.GetMQ(el.OperatorMQ) %></span>
                                        </div>
                                        
                                        <div class="publish">
                                            <label><%if (el.ExchangeCategory != EyouSoft.Model.CommunityStructure.ExchangeCategory.QGroup)
                                                     {%>
                                                        <%= el.OperatorName%><%}
                                                     else
                                                     { %>
                                                        <a title="在线即时交谈" href="tencent://message/?uin=<%=el.QGroupSendU %>&Site=<%=Request.Url.Host %>&Menu=yes"><img border="0" src="<%=ImageServerUrl %>/images/new2011/newqq.jpg" alt="在线即时交谈" /></a>
                                                     <%} %></label><span><%=el.IssueTime.ToString("yyyy-MM-dd") %></span>
                                        </div>
                                    </li>
                                    <%}
                                      } %>
                                </ul>
                                <!--分页 开始-->
                                <div class="digg">
                                    <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" PageStyleType="NewButton" />
                                </div>
                                <!--分页 结束-->
                            </div>
                            <!--*****************************内容列表 end***********************************-->
                        </div>
                    </div>

                    <!--tabs切换导航 结束-->
                </div>
            </div>
        </div>
        <!--列表 end-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SupplierBottom" runat="server">
<script type="text/javascript">
    $(function() {
        function show(hcat) {
            if (hcat == 1) {
                Boxy.iframeDialog({ title: "马上登录同业114，查看供求信息", iframeUrl: '<%=GetReturnUrl(1) %>', width: "400px", height: "250px", modal: true });
            } else {
                Boxy.iframeDialog({ title: "马上登录同业114，查看供求信息", iframeUrl: '<%=GetReturnUrl(1) %>', width: "400px", height: "250px", modal: true });
            }
        }
        var ilogin = '<% =IsLogin %>';
        $("a.jslogin").click(function() {
            var url = $(this).attr('hid');
            var hcat = $(this).attr("hcat");
            if (ilogin == 'False') {
                show(hcat);
            }
            else {
                location.href = url;
            }
            return false;
        });
    });

</script>
</asp:Content>
