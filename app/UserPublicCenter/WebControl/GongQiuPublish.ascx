<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GongQiuPublish.ascx.cs" Inherits="UserPublicCenter.WebControl.GongQiuPublish" %>
<div class="box">
    <div class="supply_top">
        <h3 class="add">
            共有<span><%=pqtotalcount%></span>条供求信息</h3>
    </div>
    <div class="orange_wrap">
        <a href="javascript:;" class="publishBtn jshowdialog" jty="0" title="我要发布供求信息" id="toPublish">
            我要发布供求信息</a>
        <ul>
            <li><span><em>(<%=pqtotalcount%>)</em></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,0,0,ppage.CityId) %>" title="最新全部供求" style="color:#ff3300">最新全部供求<img src="<%=ppage.ImageServerUrl %>/images/new2011/gif0249.gif" /></a></li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.团队询价] %>)</em><a href="javascript:;" class="jshowdialog" jty="1">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,1,0,0,ppage.CityId) %>" title="">团队询价</a></li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.地接报价] %>)</em><a href="javascript:;" class="jshowdialog" jty="2">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,2,0,0,ppage.CityId) %>" title="">地接报价</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.直通车] %>)</em><a href="javascript:;" class="jshowdialog" jty="3">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,3,0,0,ppage.CityId) %>" title="">直通车</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.车辆] %>)</em><a href="javascript:;" class="jshowdialog" jty="4">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,4,0,0,ppage.CityId) %>" title="">车辆</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.酒店] %>)</em><a href="javascript:;" class="jshowdialog" jty="5">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,5,0,0,ppage.CityId) %>" title="">酒店</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.招聘] %>)</em><a href="javascript:;" class="jshowdialog" jty="6">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,6,0,0,ppage.CityId) %>" title="">导游/招聘</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.票务] %>)</em><a href="javascript:;" class="jshowdialog" jty="7">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,7,0,0,ppage.CityId) %>" title="">票务</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.其他] %>)</em><a href="javascript:;" class="jshowdialog" jty="8">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,8,0,0,ppage.CityId) %>" title="">其他</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.找地接] %>)</em><a href="javascript:;" class="jshowdialog" jty="9">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,9,0,0,ppage.CityId) %>" title="">找地接</a> </li> 
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.机票] %>)</em><a href="javascript:;" class="jshowdialog" jty="10">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,10,0,0,ppage.CityId) %>" title="">机票</a> </li>
            <li><span><em>(<%=typecountdic[EyouSoft.Model.CommunityStructure.ExchangeType.签证] %>)</em><a href="javascript:;" class="jshowdialog" jty="11">我要发布</a></span><a href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,11,0,0,ppage.CityId) %>" title="">签证</a> </li> 
        </ul>
    </div>
    <div class="orange_bottom">
    </div>
</div>

<!--发布弹出框-选择发布种类 start-->  
<div id="floatBox" class="floatBoxLogin pubish_fix" style="display: none; position:absolute; width: 713px; height: 522px; left:50%; margin-left:-360px; ">
<div class="title">
<h4>信息发布选择</h4>
<span id="jclosedialog">
   <img src='<% =ppage.ImageServerUrl+"/images/new2011/closeIcon.gif" %>'/>
</span>
</div>
<div class="content" style="_display:inline;">
   <div>
      <a href="javascript:;" class="jfirst" hrd="/SupplierInfo/SupplierDialog.aspx?htype=1" ht="1" style="_margin:22px 0 0 -25px;" >我要发布求购信息</a>
      <a href="javascript:;" hrd="/SupplierInfo/SupplierDialog.aspx?htype=2" class="last jfirst" ht="2" style="_margin:40px 0 0 -25px;">我要发布供应信息</a>
   </div>
</div>

</div>
<!--发布弹出框-选择发布种类 end-->  

<script type="text/javascript">
    $(function() {
        function showSDDialog(ssel) {
            if ($.browser.msie) {
                if ($.browser.version == "6.0") {
                    $(".box-c div.form").css("display", "none");
                }
            }
            var t = 0;
            var h = 0;
            var newtop = 0;
            if (document.documentElement) {
                t = document.documentElement.scrollTop;
                h = document.documentElement.clientHeight;
            } else if (document.body) {
                t = document.body.scrollTop;
                h = document.body.offsetHeight;
            }

            $("a.jfirst").each(function() {
                var that = $(this);
                that.attr("hrd", "/SupplierInfo/SupplierDialog.aspx?htype=" + that.attr("ht") + "&ssel=" + ssel);
            });
            if (h > 575) {
                newtop = t+(h - 535) / 2;
            }
            else {
                newtop = t + 50;
            }
            $("#floatBox").css("top", newtop + "px");
            $("#floatBox").fadeIn("slow");

        }

        $("#jclosedialog").click(function() {
            $('#floatBox').fadeOut('slow');
            if ($.browser.msie) {
                if ($.browser.version == "6.0") {
                    $(".box-c div.form").css("display", "block");
                }
            }
        });

        $("a.jfirst").click(function() {
            drump($(this).attr("hrd"));
            return false;
        });
        function drump(arg) {
            $("#floatBox").hide();
            if ($.browser.msie) {
                if ($.browser.version == "6.0") {
                    $(".box-c div.form").css("display", "block");
                }
            }
            var btitle = "发布供求";
            //var islo = '<%= ppage.IsLogin %>';
            //if (islo === 'False') {
            //   btitle = "请登陆后再发布供求。";
            //}
            var url = arg;
            Boxy.iframeDialog({
                iframeUrl: url,
                title: btitle,
                modal: true,
                width: "724px",
                height: "600px"
            });
            return false;
        }

        $("a.jshowdialog").click(function() {
            var ty = $(this).attr("jty");
            showSDDialog(ty);
            return false;
        });
    });

    </script>