<%@ Page Title="开网店_在线开通网店_同业114网店频道" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master"
    AutoEventWireup="true" CodeBehind="CreateShop.aspx.cs" Inherits="UserPublicCenter.StaticPage.CreateShop" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="开网店 ，在线网店，旅游社网店，旅游同行网站，旅游同行销售，旅游同行价，旅游同行群" />
    <meta name="description" content="免费开通企业网站，轻松管理旅行社，客户管理更方便，线路上传更轻松，品牌传递更快捷，让您安坐家中，运筹帷幄千里之外。" />
    <link href="<%=CssManage.GetCssFilePath("index2011") %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
    <%if (Request.Browser.Browser == "IE" && Request.Browser.MajorVersion == 6)
      { %>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Png") %>"></script>

    <script type="text/javascript">
        if ($.browser.msie && $.browser.version == "6.0") {

            DD_belatedPNG.fix('div,ul,li,a,p,img,s,span');
        }
    </script>

    <%} %>

    <script type="text/javascript">
        $(function() {

            $("#mouse li").each(function(i) {
                var img = "<%=ImageServerPath %>";

                $(this).bind("mouseover", function() {
                    $("#flash").attr("src", img + "/images/new2011/index/flashImg/big0" + (i + 1) + ".gif");
                });

            });

        });
    </script>

    <div class="hr_5">
    </div>
    <!--banner start-->
    <div class="banner-bg">
        <div class="banner">
            <div id="boxID" class="myfocus myfocus02" style="visibility: hidden;">
                <ul class="pic">
                    <li><a href="#">
                        <img id="flash" src="<%=ImageServerPath %>/images/new2011/index/flashImg/big01.gif" /></a></li>
                </ul>
                <ul class="thu-bg" style="height: 40px;">
                    <li><span></span></li>
                    <li><span></span></li>
                    <li><span></span></li>
                    <li><span></span></li>
                </ul>
                <div class="win" style="width: 970px; height: 40px;">
                    <ul id="mouse" class="thumb fixted">
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/flashImg/small1.png" /></li>
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/flashImg/small2.png"></li>
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/flashImg/small3.png"></li>
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/flashImg/small4.png"></li>
                    </ul>
                </div>
            </div>
            <div class="OpenShop" onclick="window.showModalDialog('http://v.tongye114.com/Register.aspx','','dialogWidth=800px;dialogHeight=400px;center:yes')">
                <a href="#">提交申请</a></div>
        </div>
    </div>
    <!--banner end-->
    <!--mainbox start-->
    <div class="mainbox-kwd">
        <div class="hr_10">
        </div>
        <div class="mainbox fixed">
            <div class="wd-sidebar fl">
                <!--促销-->
                <div class="Promotions">
                    <h2>
                        <img src="<%=ImageServerPath %>/images/new2011/index/kaiwd_08.gif" /></h2>
                    <p>
                        <strong>不要犹豫，时间就是金钱！</strong><br />
                        现在预订还有礼包送，包括：<br />
                        送一年的行业资讯；<br />
                        送1000条短信；<br />
                        更多好礼等着送，赶紧拨打下方电话；</p>
                </div>
                <div class="hr_10">
                </div>
                <span>
                    <img src="<%=ImageServerPath %>/images/new2011/index/wangdian_10.gif" /></span>
                <div class="hr_10">
                </div>
                <span style="margin-top: -1px; display: block;">
                    <img src="<%=ImageServerPath %>/images/new2011/index/kaiwd_12.gif" /></span>
                <div class="hr_10">
                </div>
                <!--扶持计划-->
                <div class="SupportPlan">
                    <h2>
                        扶持计划</h2>
                    <div class="SupportPlan-border">
                        <span>推进旅游信息化，提高工作效率，节省销售成本， 同业114鼎力支持！</span>
                        <ul>
                            <li class="title">立即在线预订，同业114立马送上开业大礼包（每个地区限十家）：</li>
                            <li>1、精准数据库营销倍增计划；</li>
                            <li>2、专业团队为您的新店进行SEO搜索引擎优化，提高新店曝光率；</li>
                            <li>3、定期行业资讯洽谈会，增加商业机会；</li>
                            <li>4、定期投递行业资讯，及时把握行业动态；</li>
                            <li>5、专业的营销团队，为您的新店推广制定营销计划。</li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="wd-sidebar02 fr">
                <h2>
                    网店优势</h2>
                <!--wzys start-->
                <div class="wzys fixed">
                    <ul class="fl wzys-L">
                        <li><a href="#"><font color="#ff6600">·更新快，及时向客户传递最新产品信息；</font></a></li>
                        <li><a href="#">·信息量大，可详细介绍企业产品和服务；</a></li>
                        <li><a href="#">·提高经营效率、减少中间环节、降低成本；</a></li>
                        <li><a href="#">·帮助企业寻找客户，增加数倍生意机会；</a></li>
                        <li><a href="#">·随时随地洽谈联系，增加生意成交机会；</a></li>
                        <li><a href="#">·将企业的宣传、营销手段提升到高水准；</a></li>
                    </ul>
                    <div class="fr wzys-R">
                        <h4>
                            网站功能是什么</h4>
                        <ul class="fixed">
                            <li class="xianlu">线路产品集中展示<br />
                                已有1238人使用</li>
                            <li class="dingdan">订单一目了然<br />
                                已有3454人使用</li>
                            <li class="kehu">客户科学管理<br />
                                已有6546人使用</li>
                            <li class="ssyq">专业的SEO搜索引擎优化<br />
                                已有10982人使用</li>
                            <li class="data">精准数据库营销<br />
                                已有7838人使用</li>
                        </ul>
                    </div>
                </div>
                <!--wzys end-->
                <div class="hr_10">
                </div>
                <!--网站开通流程-->
                <h3>
                    网店开通流程</h3>
                <div class="wdkt fixed">
                    <ul class="fixed">
                        <li><a onclick="window.showModalDialog('login.html','','dialogWidth=800px;dialogHeight=400px')"
                            href="#">
                            <img src="<%=ImageServerPath %>/images/new2011/index/wangdianh_18.gif" /></a></li>
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/wangdianh_22.gif" /></li>
                        <li><a target="_blank" href="http://www.tongye114.com/AboutUsManage/Customer.aspx">
                            <img src="<%=ImageServerPath %>/images/new2011/index/wangdianh_20.gif" /></a></li>
                        <li>
                            <img src="<%=ImageServerPath %>/images/new2011/index/wangdianh_22.gif" /></li>
                        <li><a target="_blank" href="http://www.tongye114.com/AboutUsManage/Customer.aspx">
                            <img src="<%=ImageServerPath %>/images/new2011/index/wangdianh_21.gif" /></a></li>
                    </ul>
                </div>
                <!--wzys end-->
                <div class="hr_10">
                </div>
                <!--明星客户-->
                <h2>
                    明星客户</h2>
                <div class="StarCustomers">
                    <ul class="fixed">
                        <li><a href="http://www.oksc.net" target="_blank" title="成都（成旅）国际旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx1.gif" /><br />
                            成都（成旅）国际旅行社</a></li>
                        <li><a href="http://www.0574tour.com" target="_blank" title="快乐旅程(青岛大连\东北专线)">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx2.jpg" /><br />
                            快乐旅程(青岛大连\东北专线)</a></li>
                        <li><a target="_blank" href="http://www.sky-ts.com" title="镇江蓝天航空旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx3.gif" /><br />
                            镇江蓝天航空旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://www.chinabeihaitour.cn" title="北海市青年国际旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx4.gif" /><br />
                            北海市青年国际旅行社</a></li>
                        <li><a target="_blank" href="http://xcly.tongye114.com" title="携诚旅游--四川康辉国旅/西藏翡">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx5.jpg" /><br />
                            携诚旅游--四川康辉国旅/西藏翡</a></li>
                        <li><a target="_blank" href="http://kmkh.tongye114.com" title="昆明康辉旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx6.gif" /><br />
                            昆明康辉旅行社</a></li>
                        <li><a target="_blank" href="http://thzy.tongye114.com" title="海南燕归来旅行社(海南同行之友）">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx7.jpg" /><br />
                            海南燕归来旅行社(海南同行之友）</a></li>
                        <li><a target="_blank" href="http://www.0531hn.com" title="海南海天假期旅行社（济南）分公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx8.jpg" /><br />
                            海南海天假期旅行社（济南）分公司</a></li>
                        <li><a target="_blank" href="http://www.0898ht.com" title="海南海天假期旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx9.gif" /><br />
                            海南海天假期旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://fhjq.tongye114.com" title="南京凤凰假期">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx10.jpg" /><br />
                            南京凤凰假期</a></li>
                        <li><a target="_blank" href="http://jxzl.tongye114.com" title="广西景运甲秀国际旅行社北京分社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx11.jpg" /><br />
                            广西景运甲秀国际旅行社北京分社</a></li>
                        <li><a target="_blank" href="http://gxxgx.tongye114.com" title="广西新干线">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx12.jpg" /><br />
                            广西新干线</a></li>
                        <li><a target="_blank" href="http://ax.tongye114.com" title="FLAG TRAVEL">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx13.jpg" /><br />
                            FLAG TRAVEL</a></li>
                        <li><a target="_blank" href="http://yglhz.tongye114.com" title="海南燕归来旅行社有限公司（杭州办事处）">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx14.jpg" /><br />
                            海南燕归来旅行社有限公司（杭州办事处）</a></li>
                        <li><a target="_blank" href="http://xtly.tongye114.com" title="北海康辉国际旅行社/桂林台联国际">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx15.jpg" /><br />
                            北海康辉国际旅行社/桂林台联国际</a></li>
                        <li><a target="_blank" href="http://yytx.tongye114.com" title="易游天下国际旅行社(北京)有限公">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx16.jpg" /><br />
                            易游天下国际旅行社(北京)有限公</a></li>
                        <li><a target="_blank" href="http://ygjq.tongye114.com" title="北京阳光假期国际旅行社有限责任">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx17.jpg" /><br />
                            北京阳光假期国际旅行社有限责任</a></li>
                        <li><a target="_blank" href="http://cytx.tongye114.com" title="桂林市中国旅行社-畅游天下">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx18.jpg" /><br />
                            桂林市中国旅行社-畅游天下</a></li>
                        <li><a target="_blank" href="http://wxyn.tongye114.com" title="万象云南（北京王府国际旅行社有限">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx19.jpg" /><br />
                            万象云南（北京王府国际旅行社有限</a></li>
                        <li><a target="_blank" href="http://lytx.tongye114.com" title="北京龙游天下旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx20.jpg" /><br />
                            北京龙游天下旅行社</a></li>
                        <li><a target="_blank" href="http://wyscitsbj.tongye114.com" title="武夷山中国国际旅行社有限责任公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx21.jpg" /><br />
                            武夷山中国国际旅行社有限责任公司</a></li>
                        <li><a target="_blank" href="http://zybl.tongye114.com" title="海南沃德国际旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx22.jpg" /><br />
                            海南沃德国际旅行社</a></li>
                        <li><a target="_blank" href="http://jhfhjq.tongye114.com" title="海口凤凰之旅旅行社有限公司金华分">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx32.jpg" /><br />
                            海口凤凰之旅旅行社有限公司金华分</a></li>
                        <li><a target="_blank" href="http://cits.tongye114.com" title="锦绣江西(九江中旅)">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx23.bmp" /><br />
                            锦绣江西(九江中旅)</a></li>
                        <li><a target="_blank" href="http://yukunjiaqi.tongye114.com" title="张家界湘西中旅国际旅行社-宇坤假">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx24.jpg" /><br />
                            张家界湘西中旅国际旅行社-宇坤假</a></li>
                        <li><a target="_blank" href="http://zjjmlxx.tongye114.com" title="张家界市中国旅行社有限责任公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx25.jpg" /><br />
                            张家界市中国旅行社有限责任公司</a></li>
                        <li><a target="_blank" href="http://jinai.tongye114.com" title="中泰旅业">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx26.jpg" /><br />
                            中泰旅业</a></li>
                        <li><a target="_blank" href="http://mzhn.tongye114.com" title="北京天之涯假日旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx27.jpg" /><br />
                            北京天之涯假日旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://wyzq.tongye114.com" title="武夷山中侨旅行社---山海之旅">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx28.jpg" /><br />
                            武夷山中侨旅行社---山海之旅</a></li>
                        <li><a target="_blank" href="http://jrzl.tongye114.com" title="九江神州行国际旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx29.jpg" /><br />
                            九江神州行国际旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://kyeyc.tongye114.com" title="成都中青旅  宜昌华商 中国龙系">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx30.jpg" /><br />
                            成都中青旅宜昌华商 中国龙系</a></li>
                        <li><a target="_blank" href="http://hdfc.tongye114.com" title="华东风采">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx31.jpg" /><br />
                            华东风采</a></li>
                        <li><a target="_blank" href="http://www.u0851.com" title="贵客行">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx33.jpg" /><br />
                            贵客行</a></li>
                        <li><a target="_blank" href="http://shsh.tongye114.com" title="上海山河国际旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx34.jpg" /><br />
                            上海山河国际旅行社</a></li>
                        <li><a target="_blank" href="http://znz.tongye114.com" title="杭州指南针旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx35.jpg" /><br />
                            杭州指南针旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://yphs.tongye114.com" title="黄山市和平国际旅行社有限公司">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx36.jpg" /><br />
                            黄山市和平国际旅行社有限公司</a></li>
                        <li><a target="_blank" href="http://khly.tongye114.com" title="张家界运通康辉国际旅行社">
                            <img src="<%=ImageServerPath %>/images/new2011/index/mx38.jpg" /><br />
                            张家界运通康辉国际旅行社</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!--mainbox end-->
    <div class="hr_10">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
