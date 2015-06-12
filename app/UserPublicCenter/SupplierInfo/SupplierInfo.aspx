<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierInfo/SupplierNew.Master" AutoEventWireup="true" CodeBehind="SupplierInfo.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.SupplierNewInfo" %>
<%@ Register Src="~/WebControl/GongQiuPublish.ascx" tagname="gongqiu" tagprefix="ucgq" %>    
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierHead" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Show" runat="server">
    <ucgq:gongqiu runat="server"></ucgq:gongqiu>
    <!--列表 end-->
    <div class="hr-10">
    </div>
    <a target="_blank" href="http://im.tongye114.com" title="同业MQ"><img src="<%=ImageServerUrl + "/images/news/news-list_19.gif"%>" alt="同业MQ" /></a>
    <div class="hr-10">
    </div>
    <!--列表 start-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupplierBody" runat="server">
    <div id="supply-right">
        <div class="supply_right_top">
        </div>
        <div class="supply_right_top_bg">
            <div class="galaxy-left">
                <!--焦点图-->
                <div id="newsSlider">
                    <div class="container">
                        <ul class="slides">
                            <%if (piclist != null && piclist.Count > 0)
                              {
                                  foreach (EyouSoft.Model.CommunityStructure.MSupplyDemandPic pic in piclist)
                                  { %>
                            <li><a href="<%= UserPublicCenter.SupplierInfo.SupplierCom.SetUrlHttp(pic.LinkAddress) %>" target="_blank">
                                <img src="<%=EyouSoft.Common.Domain.FileSystem + pic.PicPath %>"></a></li>
                            <%}
                              } %>
                        </ul>
                    </div>
                    <ul class="pagination">
                        <%if (piclist != null && piclist.Count > 0)
                          {
                              int temp = 0;
                              foreach (EyouSoft.Model.CommunityStructure.MSupplyDemandPic pic in piclist)
                              { %>
                        <li><a href="javascript:;"><%=++temp %></a></li>
                        <%}
                          } %>
                    </ul>
                </div>
                <!--焦点图-->
            </div>
            <div class="hot-text" style="height:120px;">
                <%if (msrlist != null && msrlist.Count > 0)
                  { %>
                <h2><a href="<%= UserPublicCenter.SupplierInfo.SupplierCom.SetUrlHttp(msrlist[0].LinkAddress)%>" target="_blank"><%=msrlist[0].NewsTitle %></a></h2>
                <p><%=msrlist[0].NewsContent.Length > 27 ? msrlist[0].NewsContent.Substring(0, 27) + "..." : msrlist[0].NewsContent %>
                    <a href="<%= UserPublicCenter.SupplierInfo.SupplierCom.SetUrlHttp(msrlist[0].LinkAddress)%>" target="_blank">【<b>查看</b>】</a>
                </p>
                <%} %>
                <ul>
                    <%if (msrlist != null && msrlist.Count > 1)
                      {
                          for (int i = 1; i < msrlist.Count; i++)
                          { %>
                    <li><span>[供求规则]</span><a href="<%= UserPublicCenter.SupplierInfo.SupplierCom.SetUrlHttp(msrlist[i].LinkAddress)%>" title="<%=msrlist[i].NewsTitle %>" target="_blank"><%= EyouSoft.Common.Utils.GetText(msrlist[i].NewsTitle,12,false)%></a></li>
                    <%}
                      } %>
                </ul>
            </div>
        </div>
        <div class="hr-10" style="*display: none">
        </div>
        <!--列表 start-->
        <div class="box box-rightFix">
            <div class="box-l">
                <div class="box-r">
                    <div class="box-c box-c-se">
                        <div class="sel">
                            <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,0,CityId)%>" class="current">全部</a>
                            <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",1,0,0,0,0,CityId)%>">今天</a>
                            <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",2,0,0,0,0,CityId)%>">昨天</a>
                            <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",3,0,0,0,0,CityId)%>">前天</a>
                            <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",4,0,0,0,0,CityId)%>">更早</a>
                        </div>
                        <div class="form">
                            <form method="get" action="/SupplierInfo/ExchangeList.aspx">
                            <strong>查询：</strong>
                            <select name="stag" class="selzx2">
                                <option value="0">请选择标签</option>
                                <option value="1">无</option>
                                <option value="2">品质</option>
                                <option value="3">特价</option>
                                <option value="4">急急急</option>
                                <option value="5">最新报价</option>
                                <option value="6">热</option>
                            </select>
                            <select name="stype" class="selzx2">
                                <option value="0">请选择类别</option>
                                <option value="1">团队询价</option>
                                <option value="2">地接报价</option>
                                <option value="3">直通车</option>
                                <option value="4">车辆</option>
                                <option value="5">酒店</option>
                                <option value="6">导游/招聘</option>
                                <option value="7">票务</option>                                
                                <option value="9">找地接</option>
                                <option value="10">机票</option>
                                <option value="11">签证</option>
                                <option value="8">其他</option>
                            </select>
                            <select name="sProvid">
                                <option value="-1">所在地</option>
                                <%if (plist != null && plist.Count > 0)
                                  {foreach (EyouSoft.Model.SystemStructure.SysProvince sp in plist)
                                      {%>
                                  <option value="<%=sp.ProvinceId %>"><%=sp.ProvinceName %></option>
                                  <%}
                                  } %>
                            </select>
                            <!--[if lte IE 8]>
                            <style type="text/css">
                            input.sub{position:relative; top:5px;}
                            </style>
                            <![endif]--> 
                            <input name="keyword" type="text" style="height: 16px; width: 140px; border: 1px solid #999;
                                color: #999999" value="请输入关键字" onfocus="if(this.value == '请输入关键字') {this.value = '';}"
                                onblur="if (this.value == '') {this.value = '请输入关键字';}" />
                            
                            
                            <input type="submit" class="sub" value=""  />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-main">
                <div class="box-content box-content-main">
                    <!--tabs切换导航 开始-->
                    <div class="hr-5">
                    </div>
                    <div id="listContent">
                        <div id="tags">
                            <ul>
                                <li class="selectTag"><a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,CityModel.ProvinceId,0,CityId)%>">
                                    全部信息</a> </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,1,0,0,CityId)%>">团队询价</a>
                                </li>
                                <li><a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,2,0,0,CityId)%>">地接报价</a>
                                </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,3,0,0,CityId)%>">直通车</a>
                                </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,4,0,0,CityId)%>">车辆</a>
                                </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,5,0,0,CityId)%>">酒店</a>
                                </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,6,0,0,CityId)%>">导游/招聘</a>
                                </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,7,0,0,CityId)%>">票务</a>
                                </li>
                                <li><a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,11,0,0,CityId)%>">签证</a> </li>
                                <li><a  href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,8,0,0,CityId)%>">其他</a>
                                </li>
                            </ul>
                            <span class=publish_info><a href="javascript:;" class="jshowdialog" hty="0">发布供求信息</a></span>
                        </div>
                        <!--*****************************内容列表 start***********************************-->
                        <div class="hr-5">
                        </div>
                        <style type="text/css">
                          #tagContent .tagContent ul li div.publish label{ color:#828282; clear:both; display:block}
                        </style>
                        <div id="tagContent">
                            <div class="tagContent selectTag" id="tagContent0">
                                <ul>
                                    <li class="title">
                                        <div class="title1 title2">
                                            标题&nbsp;&nbsp;&nbsp;<a href="javascript:;" class="jslogin" hcat="1" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,1,CityId)%>">只看供应</a>
                                            <a href="javascript:;" class="jslogin" hcat="2" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,2,CityId)%>">只看求购</a>
                                            <a href="javascript:;" class="jslogin" hcat="3" hid="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,3,CityId)%>">旅游QQ群供求</a>
                                        </div>
                                        <div class="publish publish2">
                                            发表</div>
                                    </li>
                                    <%if (exList != null && exList.Count > 0)
                                      {
                                          foreach (EyouSoft.Model.CommunityStructure.ExchangeList el in exList)
                                          { %>
                                    <li>
                                        <div class="title1">
                                            <span><img src="<%= ImageServerUrl + (el.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供 ? "/images/news/tongyeMews_25.gif" : "/images/news/tongyeMews_28.gif")%>" alt="" /></span>
                                            <% =UserPublicCenter.SupplierInfo.SupplierCom.GetTagUrl(el.ExchangeTag, ImageServerUrl, CityId, 0)%>
                                            <span><a target="_blank" href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(el.ID, CityId) %>" title="" <%=el.ExchangeTag == EyouSoft.Model.CommunityStructure.ExchangeTag.急急急?"class='ji'":"" %>><%=el.ExchangeTitle %></a></span> 
                                            <span><%=EyouSoft.Common.Utils.GetMQ(el.OperatorMQ) %></span>
                                        </div>
                                        <div class="publish">
                                            <label><%= el.OperatorName %></label><span><%=el.IssueTime.ToString("yyyy-MM-dd") %></span>
                                        </div>
                                    </li>
                                    <%}
                                      } %>
                                    <!--信息列表循环输出 end-->
                                </ul>

                            </div>
                            <!--*****************************内容列表 end***********************************-->
                            <div class="digg">
                            <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
                            </div>
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
        var bb = $("ul.pagination").switchable("#newsSlider .container ul li", {
            triggerType: "mouse", // or click
            currentCls: "active",
            effect: "default",
            circular: true,
            triggers: "li",
            speed: 4,
            easing: "swing"
        }).autoplay({ api: true, autopause: false });


        $("#newsSlider").hover(function() {
            bb.pause();
        }, function() { setTimeout(bb.play, 1000); });
        
        
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
