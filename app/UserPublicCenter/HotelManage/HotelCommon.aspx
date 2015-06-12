<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HotelCommon.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelCommon" %>

<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register Src="../WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUserControl"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/HotelControl/SpecialHotelControl.ascx" TagName="SpecialHotelControl"
    TagPrefix="uc3" %>
<%@ Register Src="../WebControl/HotelControl/HotHotelControl.ascx" TagName="HotHotelControl"
    TagPrefix="uc4" %>
<%@ Register src="../WebControl/CityAndMenu.ascx" tagname="CityAndMenu" tagprefix="uc5" %>
<%@ Register src="../WebControl/HotelControl/HotelSearchControl.ascx" tagname="HotelSearchControl" tagprefix="uc1" %>
<%@ Register src="../WebControl/HotelControl/ImgFristControl.ascx" tagname="ImgFristControl" tagprefix="uc6" %>
<%@ Register src="../WebControl/HotelControl/ImgSecondControl.ascx" tagname="ImgSecondControl" tagprefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
 <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
    <!--main start-->
    <div class="main">
    <uc5:CityAndMenu ID="CityAndMenu1" runat="server" />
        <img class="add01" src="<%=ImageServerPath %>/images/hotel/hotel_add01.gif" />
        <!--content start-->
        
        
        
        <div class="content">
            <!--sidebar start-->
            <div class="sidebar sidebarSearch">
               <uc1:HotelSearchControl ID="HotelSearchControl1" runat="server" />
                <div style="margin-top: 10px;">
                   <uc6:ImgFristControl ID="ImgFristControl1" runat="server" /> </div>
                <!-- sidebar_2 start-->
                <uc2:CommonUserControl ID="CommonUserControl1" runat="server" />
                <uc3:SpecialHotelControl ID="SpecialHotelControl1" runat="server" />
                <!-- sidebar_2 end-->
                <div style="margin-top: 10px;">
                    <uc7:ImgSecondControl ID="ImgSecondControl1" runat="server" /></div>
                <uc4:HotHotelControl ID="HotHotelControl1" runat="server" />
                <!-- sidebar_2 end-->
                
            </div>
            <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
                
                <div class="sidebar02_1">
                    <p class="xuanzhe">
                        <span>酒店常识 </span>
                    </p>
                    <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        <div class="yd_jiange" style="height: 20px;">
                        </div>
                        <div class="yuding">
                            <h1>
                                住店注意事项<a href="#" name="t1"></a></h1>
                            <p>
                                1、 客人在酒店前台办理入住手续时，通常需交纳押金，押金数额视各酒店的规定有所不同；
                                <br />
                                2、 酒店房间内所配备设备、用品等将根据酒店级别及酒店情况而定，并非所有的酒店都备有吹风机、电热壶、拖鞋；<br />
                                3、 在酒店房间内打长途或市内电话、饮用冰箱内或吧台上的饮料、酒水等，都需要在离店前到酒店前台收银处付款；
                                <br />
                                4、 有的酒店设有收费电视频道，使用前请先确认您了解其收费标准；
                                <br />
                                5、 请爱护酒店设施，如有损坏，需由个人赔偿；
                                <br />
                                6、 洗浴时请先开凉水，后开热水，调好温度，以免烫伤。请将浴帘底襟拉入浴缸内侧，如不慎将房间地毯弄湿，酒店会要求赔偿；
                                <br />
                                7、 请勿躺在床上吸烟；
                                <br />
                                8、 勿将洗涤物挂在窗外或阳台上；<br />
                                9、 如有衣物需要由客房部进行清洗，请填好洗衣清单，将清单及衣物一起放入酒店为您准备的洗衣袋中（一般放置在写字台抽屉内）即可，同时请您详细阅读洗衣清单上的备注说明；<br />
                                10、 贵重物品请勿长时间存放在房间内，多数酒店前台设有免费保险箱供客人使用，也有部分酒店在每个房间内准备了保险箱。
                            </p>
                        </div>
                        <div class="yuding">
                            <h1>
                                <a href="#" name="t2"></a>酒店的部门设置
                            </h1>
                            <p>
                                了解酒店部门设置，有助于您顺利解决住店期间遇到的各种问题。世界各地的酒店不论等级如何,大都拥有以下几个部门为住客提供服务：前厅部、客房部、餐饮部、保安部、康乐部
                            </p>
                        </div>
                        <div class="yuding">
                            <h1>
                                <a href="#" name="t3"></a>酒店基本房型</h1>
                            <p>
                                <font color="#FF6600"><strong>按设施及规格分:</strong></font><br />
                                单人间 Single Room 双人间 Double Room 大床间 King Size & Queen Size Room<br />
                                标准间 Standard 标准间单人住 TSU (Twin for Sole Use)
                                <br />
                                三人间 Triple 四人间 Quad 五人间 套间 Suite<br />
                                公寓 Apartment 别墅 Villa
                                <br />
                                <font color="#FF6600"><strong>按级别分:</strong></font><br />
                                经济间 Economic Room 普通间 Standard Room 高级间 Superior Room<br />
                                豪华间 Deluxe Room 商务标间 Business Room 行政标间 Executive Room<br />
                                行政楼层 Executive Floor<br />
                                <strong><font color="#FF6600">按朝向分:</font></strong><br />
                                朝街房 Front View Room 背街房 Rear View Room 城景房 City View Room<br />
                                园景房 Garden View Room 海景房 Sea View Room 湖景房 Lake View Room
                                <br />
                                <font color="#FF6600"><strong>特殊房型:</strong></font><br />
                                不限房型 Run of the House 无烟标准间 Non Smoking 残疾人客房 Handicapped Room<br />
                                带厨房客房 Room with Kitchen 相邻房 Adjoining Room<br />
                            </p>
                        </div>
                        <div class="yuding">
                            <h1>
                                <a href="#" name="t4"></a>酒店星级</h1>
                            <p>
                                1、 五星级酒店：这是旅游饭店的最高等级。设备十分豪华，设施更加完善，包括多种多样的餐厅选择，较大规模的宴会厅、会议厅，综合服务比较齐全。是集社交、会议、娱乐、购物、消遣、保健等功能为一体的活动中心。<br />
                                2、 四星级酒店：设备豪华，综合服务设施完善，服务项目多，服务质量优良，室内环境优雅。客人不仅能够得到高级的物质享受，也能得到很好的精神享受。<br />
                                3、 三星级酒店：设备齐全，不仅提供食宿，还有会议室、游艺厅、酒吧间、咖啡厅、美容室等综合服务设施。这种属于中等水平的饭店在国际上最受欢迎，数量较多。<br />
                                4、二星级酒店：设备一般，除具备客房、餐厅等基本设备外，还有卖品部、邮电、理发等综合服务设施，服务质量较好，属于一般旅行等级。
                                <br />
                                5、一星级酒店：设备简单，具备食、宿两个最基本功能，能满足客人最简单的旅行需要
                            </p>
                        </div>
                        <div class="yuding">
                            <h1>
                                <a href="#" name="t5"></a>酒店类型</h1>
                            <p>
                                1、 按客户需求划分：<br />
                                商务型 会议型 度假型 青年旅馆<br />
                                2、 按管理性质划分：<br />
                                集团管理 连锁经营 自主经营<br />
                                <br />
                            </p>
                        </div>
                    </div>
                    <!--sidebar02SearchC end-->
                </div>
            </div>
        </div>
        <!--sidebar02 end-->
        <div class="clear">
        </div>
        <!--content end-->
    </div>
</asp:Content>
