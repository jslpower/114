<%@ Page Title="旅游电子_商务网站推广_短信群发_旅游网站推广_同业114做推广频道" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true" CodeBehind="DoMarket.aspx.cs" Inherits="UserPublicCenter.StaticPage.DoMarket" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta name="keywords" content="网站推广，短信群发，在线销售，网站推广方案，网站推广方法，网站推广方式，网站推广技巧" />
<meta name="description" content="做营销，旅游网站推广，短信群发业务，开通在线销售平台，快速抢占旅游市场，获取大批订单" />
<link href="<%=CssManage.GetCssFilePath("index2011") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("boxy2011") %>" rel="stylesheet" type="text/css" />
<style type="text/css">
    .boxy-wrapper .title-bar h2 { font-size: 14px; color: #f16202; line-height: 1; margin: 0; padding:1px 0 0 0; font-weight: bold; text-align:left; height:40px; line-height:30px; overflow:hidden; text-indent:50px; background:url(<%=ImageServerPath %>/images/new2011/boxy/yxsq_02.png) no-repeat 25px 5px;}
</style>
<script type="text/javascript">
    function showinf(n) {
        var showcount = 3;
        for (var i = 1; i <= showcount; i++) {
            if (i == n) {
                document.getElementById("tu" + n).style.display = "block";
                document.getElementById("pro_a" + n).src = "<%=ImageServerPath %>/images/new2011/index/flashImg/pro_a" + n + "_a.gif";
            }
            else {
                document.getElementById("tu" + i).style.display = "none";
                document.getElementById("pro_a" + i).src = "<%=ImageServerPath %>/images/new2011/index/flashImg/pro_a" + i + ".gif";
            }
        }
    }
</script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
 <div class="hr_10"></div>
    <div class="banner">
    	<div class="fl"><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/yx1_03.gif" /></div>
        <div class="fr"><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/yx1_05.gif" /></div>
    </div>
    <div class="hr_10"></div>
    <div class="mainbox">
    	<!--图片切换-->
    	<div class="yx-flash fixed">
        	<div class="yx-flash-nav">
                <ul>
                    <li><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro_a1_a.gif" onmousemove="showinf(1)" alt="" id="pro_a1"></li>
                    <li><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro_a2.gif" onmousemove="showinf(2)" alt="" id="pro_a2"></li>
                    <li><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro_a3.gif" onmousemove="showinf(3)" alt="" id="pro_a3"></li>
                </ul>
        	</div>
            <div class="yx-flash-con">
                <div class="con1" id="tu1"> <div class="img"><a href="/StaticPage/DoMarketDialog.aspx?type=1" id="link1" name="link1"><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro-01.jpg" alt=""></a></div></div>
                <div class="con2" id="tu2" style="display:none;"> <div class="img"><a href="/StaticPage/DoMarketDialog.aspx?type=2" id="link2" name="link2"><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro-02.jpg" alt=""></a></div></div>
                <div class="con3" id="tu3" style="display:none;"> <div class="img"><a href="/StaticPage/DoMarketDialog.aspx?type=3" id="link3" name="link3"><img src="<%=ImageServerPath %>/images/new2011/index/flashImg/pro-03.jpg" alt=""></a></div></div>
            </div>
        </div>
        <div class="hr_10"></div>
        <!--成功案例-->
        <div class="yx-case">
        	<div class="yx-caseT"><h2>成功案例</h2></div>
            <ul class="ImgText fixed">
                <li><img src="<%=ImageServerPath %>/images/new2011/index/kaiwd_17.jpg" /><br />青岛春秋旅行社</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/anyang.jpg" /><br />安阳市安旅假期旅行社</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/hulanht.jpg" /><br />湖南华天国旅常德旅行社</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/zaozhuang.jpg" /><br />枣庄中国旅行社</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/yingqiao.jpg" /><br />浙江银桥旅业有限公司</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/anhuixiaoyun.jpg" /><br />安徽祥云旅行社有限责任公司</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/kanghui.jpg" /><br />康辉旅游集团(青岛)旅行社有限公司</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/tongxiang.jpg" /><br />桐乡市春秋旅游有限公司</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/tienyzx.jpg" />铁旅在线</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/dahua.jpg" />杭州大华旅行社有限公司</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/xiangyuebj.jpg" />相约北京-同业联盟分销系统</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/wanghai.jpg" />望海假期</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/jingchenggn_01.gif" />精诚假期</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/shouke.gif" />收客网—福建旅游同业分销平台</li>
                <li><img src="<%=ImageServerPath %>/images/new2011/index/wanmei.gif" />昆明完美旅行社有限公司</li>
            </ul>
            <ul class="yx-caseList fixed">
            	<li>·江苏连云港天马旅行社有限公司</li>
                <li>·河南新华飞扬旅行社</li>
                <li>·浙江立喜国际旅行社有限公司</li>
                <li>·邯郸江山旅行社</li>
                <li>·武夷山市中侨旅行社</li>
                <li>·青岛龙宇天成商务有限公司</li>
                <li>·嵊州天地旅行社</li>
                <li>·浙江新假日旅行社</li>
                <li>·温州大众旅行社</li>
                <li>·焦作好时光旅行社</li>
                <li>·焦作山川旅行社</li>
                <li>·濮阳市金阳光旅行社</li>
                <li>·东营市毓龙航空旅行社</li>
                <li>·广州凌拓文化传播有限公司</li>
                <li>·百姓假期</li>
                <li>·黑龙江鹤岗青年旅行社</li>
                <li>·西峡旅行社有限公司</li>
                <li>·河北唐山华夏旅行社有限公司</li>
                <li>·吉安文山旅行社</li>
                <li>·北海中洲旅行社</li>
                <li>·青岛中国青年旅行社</li>
                <li>·门源县仙境旅行社</li>
                <li>·淮安富通旅行社有限公司</li>
                <li>·西藏茶马古道国际旅行社</li>
                <li>·烟台市福山旅行社</li>
                <li>·广西商务国际旅行社有限公司</li>
                <li>·兰溪市亚泰旅行社</li>
                <li>·湖北星桥旅行社有限公司公司</li>
                <li>·贵州三丰旅行社</li>
                <li>·平湖市星宇达旅行社有限公司</li>
                <li>·任丘市阳光旅行社有限公司</li>
                <li>·江都市蓝天旅行社有限公司</li>
                <li>·河北非常假期旅行社</li>
                <li>·高青黄河旅行社</li>
                <li>·上海亚亨国际旅行社</li>
                <li>·上海新康辉旅行社</li>
                <li>·赣州逍遥假期旅行社有限公司</li>
                <li>·北京尚之旅国际旅行社有限公司</li>
                <li>·济南文华旅行社有限公司</li>
                <li>·宿州市教育旅行社有限责任公司</li>
                <li>·什邡旅行社</li>
                <li>·广西北海雄鹰商务旅行社</li>
                <li>·青岛中国旅行社济南分公司</li>
                <li>·上海美旅展通旅行社有限公司</li>
                <li>·云南翡翠假期</li>
                <li>·广州海运集团海星国际旅游公司</li>
                <li>·广州众汇旅行社</li>
                <li>·北京微纳光科仪器有限公司</li>
                <li>·利川楚东源旅行社有限公司</li>
                <li>·唐山市飞扬旅行社有限公司</li>
                <li>·罗田县青年旅行社有限公司</li>
                <li>·杭州风光旅游服务有限公司</li>
                <li>·四川空港旅行社</li>
                <li>·南京德基旅游有限公司</li>
                <li>·烟台环球旅行社有限公司</li>
                <li>·宿州市三川旅行社</li>
                <li>·嘉善金桥旅行社有限公司</li>
                <li>·甘肃南方之旅旅行社</li>
                <li>·枣庄嘉华国际旅行社</li>
                <li>·青岛胶州湾旅行社有限公司</li>
                <li>·南通光辉国际旅行社有限公司</li>
                <li>·广西贺州华安国际旅行社</li>
                <li>·河南平顶山环球假日旅行社</li>
                <li>·万绿湖旅行社</li>
                <li>·绩溪县中汇旅行社</li>
                <li>·南通第一城旅行社有限公司</li>
                <li>·驻马店假日旅行社</li>
                <li>·成都思亿文化传播</li>
                <li>·山西丁村人旅游有限公司</li>
                <li>·江西赣州市凯旋旅行社有限公司</li>
                <li>·深圳市新文化旅行社有限公司</li>
                <li>·天津金昇国际旅行社有限责任公司</li>
                <li>·四川山水旅行社</li>
                <li>·河南省焦作光大国际旅行社有限公司</li>
                <li>·南京洲际旅行社有限公司</li>
                <li>·柳州市中国旅行社有限公司</li>
                <li>·杭州康泰旅行社有限公司</li>
                <li>·吉林市松花江旅行社</li>
                <li>·邢台铁龙旅行社</li>
                <li>·邢台中国青年旅行社有限公司</li>
                <li>·芜湖商会旅行社</li>
                <li>·华信旅行社</li>
                <li>·昆明锦爱国际旅行社有限公司</li>
                <li>·上海新天地旅行社</li>
                <li>·武夷山天马旅行社---东南假期</li>
                <li>·莱钢蓝天国际旅行社有限公司</li>
                <li>·上海好好之旅旅行社有限公司</li>
                <li>·福建省里程旅游有限公司屏南分公司</li> 
                <li>·长沙中国青年旅行社有限责任公司</li>
                <li>·南通和平旅行社有限公司</li>
				<li>·南京佰祥旅行社有限公司</li>
                <li>·南通环球国际旅行社</li>
                <li>·攀枝花市地平线旅行社</li>
                <li>·深圳市天泰旅行社有限公司</li>
                <li>·芜湖阳光假日旅行社有限责任公司</li>
                <li>·赣州鹏航旅行社有限公司</li>
                <li>·山西北方国旅</li>
                <li>·北京通达旅行社上海办</li>
                <li>·浙江遂昌凯恩旅行社有限公司</li>
                <li>·内蒙古金昊旅行社有限责任公司</li>
                <li>·河北完美假期旅行社有限公司</li>
				<li>·云南国贸--中云假日(武汉办)</li>
                <li>·张家界旅游联盟同业分销系统</li>
                <li>·福地之旅</li>
                <li>·同业160旅游批发平台</li>
				<li>·海南九洲假日分销系统</li>
				<li>·361°旅游</li>
				<li>·嘉年华旅游--南阳同业</li>
				<li>·诚信假期</li>
				<li>·365旅游同业分销系统</li>
				<li>·旅游同业网-提供热门旅游线路...</li>
                <li>·旅游同业网-提供最新,最全...</li>
                <li>·春秋旅游网</li>
                <li>·东部假期</li>
                <li>·黄山星之旅在线收客系统</li>
                <li>·旅游博览汇</li>
                <li>·中孚假日</li>
                <li>·大家旅游网-（批发商产品展示平台）</li>
				<li>·联合假期/非常之旅</li>
                <li>·旅游同行信息网</li>
                <li>·赣州国旅（综合部）</li>
                <li>·昆明康辉旅行社有限公司吉祥假期</li>
                <li>·北玛假期</li>
                <li>·鲁南旅游同业中心</li>
                <li>·快乐同行</li>
            </ul>
        </div>
	</div>
    <div class="hr_10"></div>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery1.4") %>"></script>
    
    <script language="javascript" type="text/javascript">
    $(function() {
        
        $("#link1").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "专家团队为您服务！立即申请或请致电：0571-56884627",
                modal: true,
                width: "613px",
                height: "410px"
            });
            return false;
        });
        $("#link2").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "专家团队为您服务！立即申请或请致电：0571-56884627",
                modal: true,
                width: "613px",
                height: "410px"
            });
            return false;
        });
        $("#link3").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "专家团队为您服务！立即申请或请致电：0571-56884627",
                modal: true,
                width: "613px",
                height: "410px"
            });
            return false;
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
