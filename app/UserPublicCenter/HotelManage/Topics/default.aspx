<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="UserPublicCenter.HotelManage.Topics._default" %>
<%@ Register Src="~/WebControl/PageHead.ascx" TagName="PageHead" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="head1">
    <title>超低价订团队房_50万现金直返_酒店_同业114</title>
    <meta charset="utf-8" />
    
</head>
<body>
<style type="text/css">
        *
        {
            padding: 0px;
            margin: 0px;
            font-size: 12px;
            font-weight: normal;
        }
        img
        {
            border: 0px;
        }
        .h10
        {
            clear: both;
            height: 10px;
            overflow: hidden;
        }
        .h20
        {
            margin-top: -20px;
        }
        .clear
        {
            clear: both;
            text-align: center;
        }
        #top
        {
            width: 840px;
            height: 20px;
            margin: 0px auto;
            background: url(<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/index_02.jpg) repeat-x;
        }.topda { width:970px; margin:0 auto; padding:0; padding-top:5px;}
.topda .daleft{ width:460px; float:left}
.topda .daright{ width:510px; float:left; text-align:right}
.topda .daright a { color:#333;}
        .top_01
        {
            float: left;
            width: 200px;
            padding-left: 60px;
        }
        .top_01 dd
        {
            padding: 0px 3px;
            float: left;
            line-height: 20px;
        }
        .top_01 dd a
        {
            text-decoration: none;
        }
        .dd_style1 a
        {
            color: #0048A3;
        }
        .dd_style2 a
        {
            color: #F00;
        }
        .top_02
        {
            width: 500px;
            text-align: right;
            line-height: 20px;
            float: right;
            padding-right: 10px;
        }
        .top_02 a
        {
            padding: 0px 2px;
            color: #000;
            text-decoration: none;
        }
        #banner
        {
            width: 840px;
            height: 287px;
            margin: 0px auto;
            position: relative;
            overflow: hidden;
        }
        .a1
        {
            width: 50px;
            height: 21px;
            position: absolute;
            display: block;
            top: 186px;
            left: 102px;
        }
        .a2
        {
            width: 74px;
            height: 24px;
            position: absolute;
            display: block;
            top: 212px;
            left: 60px;
        }
        h2
        {
            clear: both;
            width: 840px;
            height: 44px;
            margin: 0px auto;
            background: url(<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/index_12.jpg) repeat-x;
        }
        h2 span
        {
            width: 207px;
            height: 44px;
            display: block;
            margin-left: 30px;
            text-align: center;
            line-height: 44px;
            font-family: Microsoft YaHei;
            font-style: italic;
            color: #FFF;
            font-size: 20px;
            font-weight: bold;
            background: url(<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/index_10.jpg) no-repeat;
        }
        .tips
        {
            width: 820px;
            margin: 0px auto;
            line-height: 30px;
            font-size: 16px;
            font-weight: bold;
            color: #FC3312;
        }
        .tips1
        {
            font-size: 12px;
        }
        #footer
        {
            clear: both;
            width: 840px;
            margin: 0px auto;
            padding: 10px 0px 0px 0px;
            background: url(<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/index_25.jpg) no-repeat center top;
            border-top: 1px solid #E2E6F1;
            text-align: center;
            line-height: 20px;
            color: #333;
        }
        #footer a
        {
            color: #333;
            text-decoration: none;
            padding: 0px 3px;
        }
        .table1
        {
            width: 838px;
            line-height: 26px;
            margin: 0px auto;
            text-align: center;
            border: 1px solid #cad7f0;
        }
        .table1Top
        {
            border-top: 1px solid #cad7f0;
            border-right: 1px solid #cad7f0;
            border-left: 0;
            border-bottom: 0;
            text-align: center;
        }
        .table1Top tr
        {
        }
        .table1Top td
        {
            border-bottom: 1px solid #cad7f0;
            border-left: 1px solid #cad7f0;
            padding-left: 5px;
            padding-right: 5px;
            line-height: 20px;
        }
        .table1Top .bgFix
        {
            background: #dfedfc;
        }
        .table1Top .boldFix
        {
            font-weight: bolder;
            font-size: 14px;
            color: red;
        }
        .table1Top .bgFix td
        {
            padding-top: 5px;
            padding-bottom: 5px;
            font-size: 14px;
        }
        body .banner2
        {
            height: auto;
        }
        .IM
        {
            position: relative;
            left: 410px;
            top: -225px;
            _top: -230px;
        }
        .IM .a2
        {
            padding-left: 45px;
            left: 172px;
            position: relative;
            top: 183px;
            top: 182px;
        }
        .itemList
        {
            width: 842px;
            margin: 0 auto;
        }
        .itemList h3
        {
            color: #e44a14;
            font-size: 16px;
            font-weight: bolder;
        }
        .itemList UL
        {
            list-style-position: inside;
            line-height: 20px;
        }
        .itemList UL LI
        {
            padding-bottom: 8px;
            border-bottom: 1PX dashed #CCC;
            margin-bottom: 5px;
        }
        .itemList UL LI span
        {
            font-weight: bolder;
        }
        .floatFix
        {
            float: right;
            display: block;
            position: relative;
            top: -30px;
            padding-right: 10px;
            color: #FFF;
            text-decoration: none;
            font-weight: 700;
            font-size: 14px;
        }
        .textAlign
        {
            margin: 0 auto;
            width: 848px;
            margin-top: -13px;
        }
        .b_b
        {
            border-bottom: 1px solid #cad7f0;
        }
        .b_r
        {
            border-right: 1px solid #cad7f0;
        }
    </style>
    <uc1:PageHead ID="PageHead1" runat="server" />
    <div id="banner" style="height: auto">
        <img src="<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/banner2.jpg" />
        <div class="IM">
            <a href="http://im.tongye114.com:9000/webmsg.cgi?version=1&uid=30692" class="a1">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/page_icon_42.gif" /></a> <a class="a2" target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=1721162058&site=qq&menu=yes">
                    <img border="0" src="http://wpa.qq.com/pa?p=2:1721162058:41" alt="" title=""></a>
        </div>
    </div>
    <div class="h10 h20">
    </div>
    <h2>
        <span>3月华东团队房报价</span> <a target="_blank" href="<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/精选酒店报价下载.xls" title="更多团队报价点击下载" class="floatFix">>>更多团队报价点击下载...</a>
    </h2>
    <div class="h10">
    </div>
    <div class="textAlign">
        <table cellpadding="0" cellspacing="0" border="0" class="table1 table1Top">
            <col width="62" />
            <col width="188" />
            <col width="173" />
            <col width="177" />
            <col width="172" />
            <col width="164" />
            <col width="173" />
            <col width="296" />
            <tr class="bgFix">
                <td width="80" style="font-weight: bolder">
                    区域
                </td>
                <td width="400" style="font-weight: bolder">
                    酒店名称
                </td>
                <td width="50" style="font-weight: bolder">
                    星级
                </td>
                <td width="50" style="font-weight: bolder">
                    门市价
                </td>
                <td width="50" style="font-weight: bolder">
                    团队价
                </td>
                <td width="50" style="font-weight: bolder">
                    早餐
                </td>
                <td width="250" style="font-weight: bolder">
                    房型
                </td>
                <td width="1000" style="font-weight: bolder">
                    地址/简介
                </td>
            </tr>
            <tr>
                <td rowspan="4" style="font-weight: 700;">
                    上海
                </td>
                <td>
                    上海真西大酒店
                </td>
                <td>
                    准三
                </td>
                <td>
                    198
                </td>
                <td class="boldFix">
                    80
                </td>
                <td>
                    含双早
                </td>
                <td>
                    商务大床/标间
                </td>
                <td>
                    上海市普陀区武宁路2345号<br />
                    近梅川公园，真如古镇，梅川路步行街等
                </td>
            </tr>
            <tr>
                <td>
                    上海泓和宾馆
                </td>
                <td>
                    挂三
                </td>
                <td>
                    498
                </td>
                <td class="boldFix">
                    130
                </td>
                <td>
                    含双早
                </td>
                <td>
                    标间
                </td>
                <td width="296">
                    上海徐汇区桂林路46号
                    <br />
                    上海南站、光大会展中心、桂林公园等<br />
                </td>
            </tr>
            <tr>
                <td>
                    上海北外滩东信酒店
                </td>
                <td>
                    准四
                </td>
                <td>
                    428
                </td>
                <td class="boldFix">
                    150
                </td>
                <td>
                    含双早
                </td>
                <td>
                    标间
                </td>
                <td>
                    上海市虹口区惠民路36号<br />
                    近地铁四号线和大连路隧道
                </td>
            </tr>
            <tr>
                <td>
                    上海瀚海明玉大酒店
                </td>
                <td>
                    挂四
                </td>
                <td>
                    1265
                </td>
                <td class="boldFix">
                    300
                </td>
                <td>
                    含早
                </td>
                <td>
                    高级房
                </td>
                <td width="296">
                    上海 杨浦区 周家嘴路1888号
                    <br />
                    近大连路隧道、近北外滩国际航运中心
                </td>
            </tr>
            <tr>
                <td rowspan="4" style="font-weight: 700;">
                    杭州
                </td>
                <td>
                    杭州定和商务酒店
                </td>
                <td>
                    准三
                </td>
                <td>
                    208
                </td>
                <td class="boldFix">
                    80
                </td>
                <td>
                    不含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    杭州市江干区艮山东路146号
                    <br />
                    近西湖景区、汽车东站、火车东站、新杭州客运中心
                </td>
            </tr>
            <tr>
                <td>
                    杭州天丽商务酒店
                </td>
                <td>
                    挂三
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    120
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td width="296">
                    杭州市下城区绍兴路355号
                    <br />
                    近和平会展中心、武林广场
                </td>
            </tr>
            <tr>
                <td>
                    杭州瑞金银座酒店
                </td>
                <td>
                    准四
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    160
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td width="296">
                    杭州市江干区彭埠备塘中路17号<br />
                    近沪杭高速出入口，火车新东站
                </td>
            </tr>
            <tr>
                <td>
                    杭州凯瑞大酒店
                </td>
                <td>
                    准五
                </td>
                <td>
                    788
                </td>
                <td class="boldFix">
                    180
                </td>
                <td>
                    含早
                </td>
                <td>
                    商务大床/标间
                </td>
                <td width="296">
                    杭州江干区杭海路1199号
                    <br />
                    近四季青服装市场、华贸鞋城<br />
                </td>
            </tr>
            <tr>
                <td rowspan="3" style="font-weight: 700;">
                    南京
                </td>
                <td>
                    南京都市客栈大桥店
                </td>
                <td>
                    准三
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    80
                </td>
                <td>
                    不含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    江苏省南京市下关区建宁路255号
                    <br />
                    近长江大桥南岸、中山码头,阅江楼
                </td>
            </tr>
            <tr>
                <td>
                    南京龙珠宾馆
                </td>
                <td>
                    挂三
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    150
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    江苏省南京市秦淮区大明路135号
                    <br />
                    近南京市中心――新街口仅十分钟路程
                </td>
            </tr>
            <tr>
                <td>
                    南京白宫大酒店
                </td>
                <td>
                    准四
                </td>
                <td>
                    550
                </td>
                <td class="boldFix">
                    180
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td width="296">
                    南京玄武区龙蟠路1号
                    <br />
                    玄武湖、国展中心、长途汽车站<br />
                </td>
            </tr>
            <tr>
                <td rowspan="3" style="font-weight: 700;">
                    无锡
                </td>
                <td>
                    无锡正泰商务快捷酒店
                </td>
                <td>
                    准三
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    70
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    无锡市青祁路86
                </td>
            </tr>
            <tr>
                <td>
                    无锡运河大酒店
                </td>
                <td>
                    挂三
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    120
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    无锡湖滨路7号<br />
                    近无锡市博物馆附近
                </td>
            </tr>
            <tr>
                <td>
                    无锡天骄宾馆
                </td>
                <td>
                    准四
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    160
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    无锡市滨湖区钟秀路128号
                    <br />
                    近太湖风景区
                </td>
            </tr>
            <tr>
                <td rowspan="3" style="font-weight: 700;">
                    苏州
                </td>
                <td>
                    苏州24K国际连锁酒店
                </td>
                <td>
                    准三
                </td>
                <td>
                    238
                </td>
                <td class="boldFix">
                    70
                </td>
                <td>
                    不含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    苏州金阊区桐泾北路11号
                    <br />
                    近石路商业步行街，地处名胜园林的中心地带
                </td>
            </tr>
            <tr>
                <td>
                    阊门饭店
                </td>
                <td>
                    挂三
                </td>
                <td>
                    580
                </td>
                <td class="boldFix">
                    120
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td width="296">
                    苏州 平江区 西中市139号
                    <br />
                    近石路商业步行街<br />
                </td>
            </tr>
            <tr>
                <td>
                    苏州锦江白玉兰酒店
                </td>
                <td>
                    准四
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="boldFix">
                    160
                </td>
                <td>
                    含早
                </td>
                <td>
                    标间
                </td>
                <td>
                    苏州市沧浪区白塔西路33号
                    <br />
                    近观前街商业区，长途汽车站
                </td>
            </tr>
        </table>
    </div>
    <div class="h10">
    </div>
    <div class="h10">
    </div>
    <h2>
        <span>3月港澳低价酒店报价</span>
    </h2>
    <div class="h10">
    </div>
    <div class="itemList">
        <ul>
            <li><span>澳门金龙</span><br />
                1-3/520 4/685 5/890 7-10/520 11/685 12/890 13-17/520 18/685 19/890 20-24/520 25/685
                26/890 27-31/520
                <li><span>澳门维景</span><br />
                    1-2/460 3-4/700 5/780 6-10/460 11/800 12/780 13-17/460 18/545 19/780 20-24/460 25/545
                    26/780 27-31/460
                    <li><span>油麻地红茶馆</span><br />
                        1-2/380 3/450 4/600 6-8/450 9-10/380 13-17/380 18-19/500 20/380 21-24/450 25-26/650
                        27/550 28-31/380
                        <li><span>温斯劳街红茶馆</span><br />
                            1-3/250 4/400 5/450 6-8/350 9-10/250 11-12/350 13-17/250 18-19/380 20/250 21-24/350
                            25-26/400 27/300 28-31/250
                            <li><span>CASA</span><br />
                                1-3/460 4-7/700 8/650 9-10/460 11-12/650 13-17/460 18-19/650 20/460 21-24/650 25-26/750
                                27/60028-31/460
                                <li><span>好来坞</span><br />
                                    8/400 9/500 10/770 11/880 12/1100 13-14/750 15-16/750 17/800 18/880
                                    <li><span>帝豪海景</span><br />
            1-3/460 4/850 5/800 6-7/600 8/550 9-10/460 11/700 12/800 13-15/460 16-17/540 18-19/650
            20/540 21-24/650 25-26/750 27/600 28-31/540
        </ul>
    </div>
    <div class="tips tips1">
        有更多特价酒店未能尽录，详情请致电预订 因港澳房间价格实时变动，同业114对以上报价拥有最终解释权<br />
        联系人：胡小姐 电话：15988896909 &nbsp;&nbsp;MQ:<a href="http://im.tongye114.com:9000/webmsg.cgi?version=1&uid=30692"><img
            src="<%=ImageServerPath %>/images/UserPublicCenter/hoteltopics/page_icon_42.gif" /></a> &nbsp;&nbsp;QQ:<a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=1721162058&site=qq&menu=yes"><img
                border="0" src="http://wpa.qq.com/pa?p=2:1721162058:41" alt="" title=""></a>
    </div>
    <div class="h10">
    </div>
    <div id="footer">
    <% 
        EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
       if (Model != null)
       {
           Response.Write(Model.AllRight);
           Model = null;
       }
    %>
    </div>
</body>
</html>
