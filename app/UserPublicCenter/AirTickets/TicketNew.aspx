<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master"
    AutoEventWireup="true" CodeBehind="TicketNew.aspx.cs" Inherits="UserPublicCenter.AirTickets.TicketNew" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="<%=PageTitle.Plane_Keywords%>" />
    <meta name="description" content="<%=PageTitle.Plane_Des %>" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("head2011")%>' rel="Stylesheet" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("index2011")%>' rel="Stylesheet" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("517autocomplete")%>' rel="Stylesheet" />
    <style type="text/css">        
        .container, .container *
        {
            margin: 0;
            padding: 0;
        }
        .container
        {
            width: 472px;
            height: 240px;
            overflow: hidden;
            position: relative;
            float: left;
        }
        .slider
        {
            position: absolute;
        }
        .slider li
        {
            list-style: none;
            display: inline;
        }
        .slider img, .slider2 img
        {
            width: 471px;
            height: 235px;
            display: block;
        }
        .slider2 li
        {
            float: left;
        }
        .slider2
        {
            width: 3200px;
        }
        .num
        {
            position: absolute;
            right: 5px;
            bottom: 5px;
        }
        .num li
        {
            float: left;
            color: #FF7300;
            text-align: center;
            line-height: 16px;
            width: 16px;
            height: 16px;
            font-family: Arial;
            font-size: 12px;
            cursor: pointer;
            overflow: hidden;
            margin: 3px 1px;
            border: 1px solid #FF7300;
            background-color: #fff;
        }
        .num li.on
        {
            color: #fff;
            line-height: 21px;
            width: 21px;
            height: 21px;
            font-size: 16px;
            margin: 0 1px;
            border: 0;
            background-color: #FF7300;
            font-weight: bold;
        }
        html, body, div, p, ul, li, dl, dt, dd, h1, h3, h4, h5, h6, form, input, select, button, textarea, iframe, table, th, td { margin: 0; padding: 0;}
        bady{ color:#363636; line-height:1.2;}
        body, input, select, button, textarea {font-family: 宋体,Arial,Helvetica,sans-serif;font-size: 12px;}
        .tickets-nousT, .tickets-nousB {background-position: 0 0;height: 48px;line-height: 48px;}
        a:focus{ outline:medium none;}
        .tickets-nousB {background-position: 0 -58px;height: 7px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
    <form id="Form1" runat="server">
    <input type="hidden" name="vc" id="vc" value="" />
    <div id="divHotCitys" style="display: none; z-index: 10000; float: left;">
        <ul id="ulHotCitys">
        </ul>
    </div>
    <div class="mainbox" style="margin-top: 5px;">       
        <!--hot-screen start-->
        <div class="hot-screen fixed">
            <div class="tickets-search" style="float: left">
                <div class="jipiaotop">
                </div>
                <div class="tickets-searchCC">
                    <div class="liucheng">
                        <ul>
                            <li class="bgnone">机票查询</li>
                            <li>预定</li>
                            <li>在线支付</li>
                            <li>出票</li></ul>
                    </div>
                    <div class="liuchengxia">
                        <span>(退票改期)</span>&gt;&gt; 进入老版本机票系统
                    </div>
                    <div class="liuneirong">
                        <div class="t_tupian">
                            <img src="<%=ImageServerPath%>/Images/new2011/images/jp_gai_18.jpg" width="252" height="80"
                                alt=" " /></div>
                        <div class="dianjijr">
                            <a href="http://vipjp.tongye114.com/" target="_blank">
                                <img src="<%=ImageServerPath%>/Images/new2011/images/jp_gai_21.jpg" width="144" height="36"
                                    alt="点击进入" /></a>
                        </div>
                        <div class="dianjinei">
                            VIP大客户注册及授信客户<br />
                            服务电话:0571-56893746
                            <br />
                            QQ:<%=strQQhtml %>
                            客服：<%=strMQhtml%><br />
                            电话：0571-56893761<br />
                            手机：15356126700
                        </div>
                        <div class="liping">
                            诱人的返点政策，支付宝安全，快速出票，让您订购无忧！</div>
                        <div class="zuihoude">
                            <ul>
                                <li><span>
                                    <img src="<%=ImageServerPath%>/Images/new2011/images/diand_32.jpg" width="17" height="17"
                                        alt=" " /></span>政策好→返点最好达20%</li>
                                <li><span>
                                    <img src="<%=ImageServerPath%>/Images/new2011/images/diand_35.jpg" width="17" height="17"
                                        alt=" " /></span>出票快→五分钟搞定！</li>
                                <li><span>
                                    <img src="<%=ImageServerPath%>/Images/new2011/images/diand_37.jpg" width="17" height="17"
                                        alt=" " /></span>退款快→出票失败快速退款</li>
                                <li><span>
                                    <img src="<%=ImageServerPath%>/Images/new2011/images/diand_39.jpg" width="17" height="17"
                                        alt=" " /></span>交易安全→支付宝、网银！</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="tickets-searchBB">
                </div>
            </div>
            <div class="tickets-hot" style="float: left">
                <!--焦点图-->
                <div class="container" id="idTransformView" style="width: 476px; height: 240px;">
                    <ul class="slider slider2" id="idSlider" style="position: absolute; left: 0px; top: 0pt;">
                        <%= FiveAdvImages %>
                    </ul>
                    <ul class="num" id="idNum">
                        <%=FiveImagesNumber %>
                    </ul>
                </div>
                <div class="hr_5">
                </div>
                <div class="tickets-hotC fixed" style="height: 215px;">
                    <div class="tejia-title">
                        <h2 class="tjjp">
                            特价机票</h2>
                        <a class="more02" href="<%=Domain.UserPublicCenter %>/jipiaolist_<%=CityId %>">更多 <s>
                        </s></a>
                    </div>
                    <ul class="fixed">
                        <%=ticketsHtml %>
                    </ul>
                </div>
            </div>
            <div class="Airline-box">
                <span>加盟航空公司</span>
                <div class="AirlineList">
                    <ul class="fixed">
                        <li>
                            <img src="<%=ImageServerPath%>/Images/new2011/images/gh.gif"></li>
                        <li>
                            <img src="<%=ImageServerPath%>/Images/new2011/images/dh.gif"></li>
                        <li>
                            <img src="<%=ImageServerPath%>/Images/new2011/images/hh.gif"></li>
                        <li>
                            <img src="<%=ImageServerPath%>/Images/new2011/images/nh.gif"></li>
                    </ul>
                    <p>
                        上海航空 山东航空 深圳航空 四川航空 厦门航空 成都航空 吉祥航空 华夏航空 奥凯航空 河北航空 中联航空 金鹿航空 幸福航空 昆明航空
                    </p>
                </div>
                <div class="hr_5">
                </div>
                <div class="ssgq-box" style="height: 215px;">
                    <div class="tejia-title">
                        <h2 class="ssgq">
                            实时供求</h2>
                        <a class="more02" href="<%=Domain.UserPublicCenter %>/info">更多 <s></s></a>
                    </div>
                    <ul>
                        <%=supplysHtml%>
                    </ul>
                </div>
            </div>
        </div>
        <!--hot-screen end-->
        <div class="hr_5">
        </div>
        <!--机票常识 开始-->
        <div class="tickets-nous">
            <ul class="tickets-nousT fixed">
                <li class="jpcsTitle">机票常识</li><li><a class="Ticketes-tab-on" href="#">01.机票秘密</a></li><li>
                    <a href="#">02.订票烦劳</a></li><li><a href="#">03.乘机的困惑</a></li><li><a href="#">04.乘机之外</a></li></ul>
            <div class="tickets-nousC" style="display: block;">
                <p>
                    <span>经济舱、公务舱和头等舱具体有什么区别? </span>经济舱、公务舱和头等舱在机票价格、限制条件、机舱位置、服务标准、餐食、登机顺序、行李额上都有区别。<br>
                    头等舱一般设在客舱的前部，座椅本身的尺寸和前后之间的间距都比较大，长航线甚至会采用平躺式座椅，相对舒适；<br>
                    经济舱的座位设在从机身中间到机尾的地方，占机身的四分之三空间或更多，座位尺寸相对小；公务舱介于两者之间。<br>
                    多数航空公司会设有头等舱和公务舱的专用休息室和特殊通道，行李额度相对较多，优先上机，餐食也较经济舱要丰盛。但是各家航空公司并非所有机型都设置这3种舱位。
                </p>
                <p>
                    <span>什么是“三不准”机票? </span>“三不准”即出票以后，不准签转、不准更改、不准退票。<br>
                    不准签转：指出票后不能更改航空公司。<br>
                    不准更改：指出票后不能更改日期或航班。<br>
                    不准退票：指出票后不能退票。
                </p>
                <p>
                    <span>航空公司的票价等级是如何划分的？ </span>票价级别是以机票价格（折扣）来划分。依次分为：头等舱，公务舱，经济舱。
                </p>
                <p>
                    <span>经济舱不同价格的机票座位有差异吗？ </span>票价和座位基本没有关系，在飞机上的待遇都是相同的。价格低的机票只要尽早选订机上座位或办理值机手续，也可以坐在喜欢的位置（如前排或靠窗、靠走道等等）。<br>
                    部分差异可能来源于某些航空公司对本公司常旅客会提供一些优先的政策，但与价格无关。<br>
                    不同折扣的票价只有在办理变更、退票、里程累积时会根据航空公司规定有所不同。
                </p>
                <p>
                    <span>买机票的时候，是否可以选择座位？ </span>您可以在机场柜台办理值机手续时向工作人员表明您偏爱的位置，另外您也可以提前在网上办理值机手续，选择座位。
                </p>
                <p>
                    <span>机票附加税是哪几项？ </span>国内机票附加税包括机场建设费和燃油附加费。
                </p>
                <p>
                    <span>婴儿、儿童各指多大的小孩？ </span>一般情况下，婴儿是指年龄为14天～2周岁，儿童指2～12周岁，以航班起飞日期为准。
                </p>
                <p>
                    <span>儿童、婴儿的机票如何计算价格？儿童需要购买机票附加费吗？ </span>不同航空公司对儿童票、婴儿票的价格规定不尽相同，具体情况请以出票时航空公司的最新规定为准。
                </p>
                <p>
                    <span>如果多人一起订票，价格是不是可以便宜些？ </span>多人一起订票，价格不一定会便宜，需要看订票的人数以及航空公司的具体规定。
                </p>
            </div>
            <!--订票烦劳-->
            <div class="tickets-nousC" style="display: none;">
                <span >预订机票时应该提供哪些信息？哪些信息与价格有直接关系？ </span>
                <p>
                    1、必须提供登机人的姓名、有效证件类型、有效证件号、行程及行程日期。 有效证件包括：中国籍旅客居民身份证、临时身份证、军官证、警官证、士兵证、军队学员证、军队文职干部证、军队离退休干部证和军队职工证；港澳地区居民和台湾同胞旅行证件；外籍旅客的护照、旅行证、外交官证等；16岁以下未成年人可凭其学生证、户口簿或者户口所在地公安机关出具的身份证明。<br>
                    2、有效证件类型，行程及行程日期会与价格有直接关系。
                </p>
                <span>为什么我预订了机票，出票时却告诉我没有票了呢？ </span>
                <p>
                    机票价格及座位数量是时时变化的，航空公司最终以出票为准，航空公司为确保航班收益，对预订而未出票的客票不给予保证，其会根据航班的具体情况来确定预订的座位是否保留。
                    如果您是用支付宝余额支付，订单失败后，票款会立刻回到您的支付宝账户里；如果您是信用卡、网银支付，订单失败后，票款将会在3-5个工作日退回银行卡。
                </p>
                <span>机票有效期是多久？ </span>
                <p>
                    1、正常票价的机票自旅行开始之日起，一年内有效。如果机票全部未使用，则从填开机票之日起一年内运输有效。<br>
                    2、机票有效期的计算，从旅行开始或填开机票之日的次日零时起至有效期满之日的次日零时为止。<br>
                    3、特殊票价的机票有效期，按照承运人规定的适用票价的有效期计算。<br>
                    4、关于有效期在行程单或客票上会有相应日期的标注。
                </p>
                <span>出票后如果要更改时间或行程，我应该联系谁？ </span>
                <p>
                    请致电预订机票的代理或者航空公司，进行更改。更改行程可能产生一定的费用。费用标准以航空公司公布的最新规定为准。
                </p>
                <span>我的机票已经出票了，但是发现有更便宜的，能取消重订吗？ </span>
                <p>
                    如果是允许退的机票，可以退票重出，不过要支付一定的退票费，具体是否合算要根据退票费用具体分析。
                </p>
                <span>机票上的名字错了怎么办？ </span>
                <p>
                    1、机票是实名制的且限旅客本人使用的，一旦出票后即无法换其他姓名，也不能转给他人使用，所以如果旅客姓名订错，一般只能办理退票，然后再重新预订（须是在机票可以退票的前提下）。但各个航空公司的具体规定不尽相同，如果发现机票上的名字错了，应立即联系购票处，极少数航空公司对于同音不同字的错误可以在起飞前限定时间内进行更换。
                    <br>
                    2、若机票上的证件号码错了，请在航班起飞前致电预定机票的代理商或航空公司咨询是否可以更改，以免延误航班。
                </p>
                <span>机票为什么有时候不能退？ </span>
                <p>
                    1、航空公司推出的特价机票，价格低廉，但在退、改、签的政策上都会有较多限制。<br>
                    2、往返程机票，如果倒序使用后，原始发的单程段是不能办理退票的。<br>
                    3、过了有效期的机票也是不可以退票的。
                </p>
                <span>退票的话我需要提供什么？ </span>
                <p>
                    需要提供行程单、旅客有效证件到原购票处办理。
                </p>
                <span>如果碰到台风等原因导致航班取消的情况，我该怎么办？ </span>
                <p>
                    如果碰到台风等原因导致航班取消，需联系卖家，国内航班一般会安排后续航班，或者旅客提供航班取消/延误证明，卖家会为其申请全额退票；国际航班如遇到台风等特殊因素导致航班取消，部分航空公司会安排后续航班及相应的酒店，部分航空公司可以对此类机票进行全额退款，旅客需要提供机场柜台开具的航班取消/延误证明。也有部分航空公司对于不可抗力造成的航班取消不给予全额退款及后续安排。
                </p>
                <span>我订了来回程机票，但没有按照顺序使用，那我没有使用的那一段能退票么？ </span>
                <p>
                    一张机票内行程必须按照顺序使用，如没按顺序使用则无法退票。
                </p>
            </div>
            <!--乘机的困惑-->
            <div class="tickets-nousC" style="display: none;">
                <span>一般需提前多久到机场办理乘机？</span>
                <p>
                    各个航空公司的规定不太相同，一般情况下，国内航班在起飞前30分钟（部分航班是提前45分钟）停止办理登机手续，国际航 班在起飞前45－60分钟停止办理登机手续，所以需要提前抵达机场，预留足够的时间办理登机手续。建议国内航班提前90分钟
                    抵达机场，国际航班提前2.5-3小时抵达机场。</p>
                <span>预订国内机票时提供的是身份证，是否可使用护照或其他证件登机？</span>
                <p>
                    不可以的，机场安检的时候要核对身份证件号，如与录入的证件号码不符，是登不了机的。</p>
                <span>证件丢失怎么办？</span>
                <p>
                    证件丢失的乘客，若及时到公安部门办理有关手续，即能如期乘机。具体补办手续为：先到本人户口所在地公安机关开具身份证 遗失证明，贴上本人1张近期免冠照片并加盖公安机关公章；或是由身份证签发地公安机关出具报失证明，证明内容与以上相同。
                    随后，乘客携带本人户口本或工作证或其它能证明身份的有效证件，1张近期免冠照片，以及公安部门开具的介绍信，向民航公安机关（在机场候机厅设有值班场所）申请开具临时登机证明。若因在外地旅行期间，上述有效证件都未随身携带，可由户口所在地传真有效证件至民航公安机关。<br>
                    您需要联系代理商或航空公司咨询最新规定。</p>
                <span>没赶上航班的话怎么办? 是不是我的机票就作废了?</span>
                <p>
                    请第一时间联系卖家查询所购机票的退改签政策。<br>
                    如果购买的是“三不准”的客票，一般情况下，只能退回机场建设费和燃油附加费。<br>
                    如果是可改签舱位的客票，可以联系卖家操作或直接找机场航空公司柜台改签到其他班次的航班（改签可能存在费用）。</p>
                <span>免费行李额是多少？行李额有哪些规定？</span>
                <p>
                    持成人票或儿童票的旅客，每人免费托运行李的限额为：头等舱40公斤，公务舱30公斤，经济舱20公斤。持婴儿票的旅客无免费行李额。但有时，航空公司有特殊行李额。</p>
                <span>行李托运有哪些手续？</span>
                <p>
                    1、旅客必须凭有效客票托运行李。承运人应在客票及行李票上注明托运行李的件数和重量。<br>
                    2、承运人一般应在航班离站当日办理乘机手续时收运行李；如团体旅客的行李过多，或因其他原因需要提前托运时，需要承运人和旅客约定时间、地点托运。
                    <br>
                    3、承运人对旅客托运的每件行李应拴挂行李牌，并将其中的识别联交旅客。<br>
                    4、不属行李的物品，应按货物托运，不能作为行李托运。</p>
                <span>哪些东西不能随身携带？</span>
                <p>
                    1）危险物品，包括爆炸品、气体、易燃液体、易燃固体、自燃物质、遇水释放易燃气体的物质、氧化剂、有机过 氧化物、毒性物质、传染性物质、放射性物品、腐蚀品和不属于上述任何一类别而在航空运输中具有危险性的物质和物品；<br>
                    2）枪支、弹药、管制刀具及其他类似的物品；<br>
                    3）动物，已按规定办理手续的除外；
                    <br>
                    4）中华人民共和国或者运输过程中有关国家法律规定禁止出境、入境或者过境的物品；
                    <br>
                    5）包装、形状、重量、体积或者性质不适宜运输的物品。</p>
                <span>托运行李中能否包括香烟、酒和锂电池吗？</span>
                <p>
                    在随身携带的行李或者托运行李里面都可以放两条香烟。酒必须要托运且每人只可以托运一公斤。根据规定，不允许旅客在托运 行李中夹带锂电池。旅客可以携带为个人自用的内含锂或锂离子电池芯或电池的消费用电子装置（手表、计算器、照相机、手机、
                    手提电脑、便携式摄像机等）。备用电池必须单个做好保护以防短路，并且仅能在手提行李中携带，备用电池每人限带2个，等质总锂含量在8克以上而不超过25克。对其他类型的备用干电池，比如镍氢电池等做好防短路措施也可以随身携带。</p>
                <span>在机场购买的免税品，能带上飞机吗？限制带多少？</span>
                <p>
                    一般旅客经过安检后，可以在机场控制区内的商店购买液态免税品带上离港航班。对于需要在国外、境外转机的旅客，应保证在 旅行中，塑料袋明显完好无损，不得自行拆封，并保留登机牌和液态物品购买凭证，已备转机地有关人员查验。但某些转机航班
                    不允许携带液体的免税品。旅客可提前向航空公司查询携带有关液体的规定。</p>
                <span>如果要转机的话行李要取下来再登机么？</span>
                <p>
                    1、直接转机时不必出机场，在办理中转手续的柜台即可办理续乘航班的登记手续。如果行李是办理直运目的地，就不必将行李取 出重新办理交运了。但是如果转机点需要办理入关手续的话除外，此情况需要取行李办理相关手续后转机。<br>
                    2、预订的机票如果是同一家航空公司转机的话，一般不需要重新过安检和办理行李托运的。但是如果转机点需要办理入关手续 的，需要取行李办理相关手续后转机。如果订的机票前一段和后一段不是同一个航空公司，则需重新办理乘机手续和行李托运。</p>
                <span>什么是飞机托运行李声明价值？</span>
                <p>
                    托运行李每公斤价值超过人民币50元时，可以办理行李声明价值，航空公司收取相应的声明价值附加费。声明价值不能超过行 李本身的实际价值。每一旅客的行李声明价值最高限额为人民币8000元。如此件行李丢失，航空公司按声明价值赔偿。</p>
                <span>随身或托运行李丢失怎么办？</span>
                <p>
                    随身行李丢失直接联系当地的航空公司。 托运行李，是按照重量和保价运输进行赔偿的，如果没有保价运输，赔偿只能按常规普 通“丢失一件行李”来赔偿。</p>
            </div>
            <!--乘机之外-->
            <div class="tickets-nousC" style="display: none;">
                <span>机票的报销凭证是什么?</span>
                <p>
                    国内机票电子客票行程单就是报销凭证。</p>
                <span>假如行程单丢失的话，是否可以补？</span>
                <p>
                    国内客票行程单不可以重打。</p>
                <span>什么是“无成人陪伴儿童”？如何办理？</span>
                <p>
                    “无成人陪伴儿童”指年龄在5周岁到12周岁的无成人陪伴，单独乘机的儿童（很多国际航班年龄拦到了16岁）。凡在这个年龄段内单独进行航空旅行的儿童，必须向航空公司申请无成人陪伴儿童服务。年龄在5周岁以下的儿童，一般情况应有成人陪伴。
                    无成人陪伴儿童必须由儿童的父母或监护人陪送到上机地点并在儿童的下机地点安排人员迎接。对于无成人陪伴儿童的运输， 不同航空公司的不同航线、不同航班，对票价、每个航班的限制接受人数、年龄等均有差别。具体内容请向有关航空公司咨询。</p>
                <span>如何申请特殊餐食和婴儿摇篮服务？</span>
                <p>
                    国内航线的航班某些餐食需提前72小时申请，个别特殊服务只限航司直属售票处办理。国际航线大部分国际航班允许在航班起 飞前申请特殊餐食，需至少提前72小时申请。 国内及国际航线的婴儿摇篮服务根据各家航空公司规定有所不同，需要旅客直接在航空公司柜台申请并出票，代理人无权限。</p>
                <span>所有时段的航班都提供餐食吗？</span>
                <p>
                    不同的航空公司有不同的餐食提供。餐食的发放是要看飞行时间的，一般飞行时间在两个小时以上的长途飞行会有餐食发放的。 飞机餐都是由专门生产航班餐食的航空食品公司提供。</p>
                <span>孕妇是否可以坐飞机？</span>
                <p>
                    1）怀孕不足8个月（32周）的健康孕妇，可正常乘机。<br>
                    2）若怀孕超过8个月（32周）、不足9个月（36周）的健康孕妇需要乘机，应在乘机前72小时内提供省级以上医疗<br>
                    单位盖章的《诊断证明书》，经航空公司同意后方可购票乘机。<br>
                    3）怀孕超过9个月（36周）的孕妇不接受购票乘机。 注：个别航空公司规定有所不同，如果是有身孕的旅客，请乘机前务必询问清楚相关规定，以免误机。</p>
                <span>宠物可以乘坐飞机么？需要办那些手续？</span>
                <p>
                    可以。宠物乘机需提前预订有氧舱，请凭小动物托运申请单和检疫证明，提前3个工作日向航空公司售票处办理申请手续。</p>
                <span>旅客如果是糖尿病患者，对乘机时随身携带的药物有何限制？</span>
                <p>
                    旅客如患有糖尿病，可以携带足够的胰岛素制剂及带针头的皮下注射器在飞机上使用，但须出示医疗证明。糖尿病旅客携带个人 的规定食物（例如无糖果汁）在飞机上食用，若容器的容量多于100毫升须出示医疗证明方可携带。豁免的药物及食物应与其
                    它手提行李分开，以便接受检查。</p>
                <span>担架病人该如何办理乘机？</span>
                <p>
                    一、为了做好病人旅途中的医疗护理，航空公司规定必须至少由一名医生或护理人员陪同旅行，为此要求提供随行医务人员的身 份证明及职业证明。如医院证明病人在旅途中不需医务护理的，也可由家属或监护人陪同乘机。<br>
                    二、必须提前三天以上为担架旅客购买机票、向航空公司提出申请，并按要求填写患病旅客的《诊断证明书》。该诊断书必须是患 病旅客旅行前48小时内由三级医院出具并盖章的，同时应有主治医师的签名。患有严重疾病的旅客（心血管、癌症、急性外伤
                    等）的诊断书，必须是在旅行前24小时之内出具的。航空公司据此决定是否承运。<br>
                    三、填写《特殊旅客乘机申请书》，并由旅客本人或家属签字。</p>
                <span>哪些人不宜乘飞机？</span>
                <p>
                    一、传染性疾病患者。如传染性肝炎、活动期肺结核、伤寒等传染病患者，在国家规定的隔离期内，不能乘坐飞机。其中水痘病人在损害部位未痊愈，不能乘飞机。<br>
                    二、精神病患者。如癫痫及各种精神病人，因航空气氛容易诱发疾病急性发作，故不宜乘飞机。<br>
                    三、心血管疾病患者。因空中轻度缺氧，可能使心血管病人旧病复发或加重病情，特别是心功能不全、心肌缺氧、心肌梗塞及 严重高血压病人，通常认为不宜乘飞机。<br>
                    四、脑血管病人。如脑栓塞、脑出血、脑肿瘤这类病人，由于飞机起降的轰鸣、震动及缺氧等，可使病情加重，禁止乘飞机。<br>
                    五、呼吸系统疾病患者。如肺气肿、肺心病等患者，因不适应环境，如果有气胸、肺大炮等，飞行途中可能因气体膨胀而加重病 情。<br>
                    六、做过胃肠手术的病人，一般在手术十天内不能乘坐飞机。消化道出血病人要在出血停止三周后才能乘飞机。<br>
                    七、严重贫血的病人。血红蛋白量水平在５０克/升以下者，不宜乘飞机。
                    <br>
                    八、耳鼻疾病患者。耳鼻有急性渗出性炎症，及近期做过中耳手术的病人，不宜空中旅行。<br>
                    九、临近产期的孕妇。由于空中气压的变化，可能致胎儿提早分娩。</p>
            </div>
            <div class="tickets-nousB">
            </div>
        </div>
        <!--机票常识 结束-->
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Switchable") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("HomeImages") %>"></script>

    <script type="text/javascript">
        $(function() {
            $('.tickets-nousT').switchable('.tickets-nousC', { currentCls: 'Ticketes-tab-on', circular: 'true' });

            //轮换广告初始化
            HomeBigImages.init(475, 4);
        });
    </script>

</asp:Content>
