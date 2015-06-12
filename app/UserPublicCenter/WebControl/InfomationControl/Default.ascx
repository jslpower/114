<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="UserPublicCenter.WebControl.InfomationControl.Default" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div id="moduel_1" class="moduel">
            <div class="left">
                <!--幻灯片-->
                <script type="text/javascript" src="<%=JsManage.GetJsFilePath("InfoJiaodiantu") %>"></script>        
                <SCRIPT type=text/javascript>
                    $(function() {
                        $('#newsSlider').loopedSlider({
                            autoStart: 3000
                        });
                        $('.validate_Slider').loopedSlider({
                            autoStart: 3000
                        });
                        $("#enter_lab").click(function() {
                            window.location = $(this).find("a").attr("href"); return false;
                        });
                    });
                </SCRIPT>   
                
                <div id=newsSlider>
                 <%=HomePictureLists%>
               
                </div> 


            </div>
            <div class="middle">
                
                <div class="c1 c1-top">
                   <%=HeaderNewsTop%>
                </div>
                <div class="noTitle"> 
                   <ul class="noTitle-top">
                     <%=HeaderNews%>
                   </ul>
                </div>
            </div>
            <div class="right">
                <a href='<%=GetAdvertPicture(0,1)%>' target="_blank">
                    <img src='<%=GetAdvertPicture(0,0)%>' width="249px" height="176px" /></a>
                <!--实时供求-->
                <div class="mList mListT">
                    <h3>
                        实时供求<span><a target="_blank" href="<%=EyouSoft.Common.Domain.UserBackCenter %>/Default.aspx#page%3A%2Fsupplyinformation%2Faddsupplyinfo.aspx"
                            class="redh">发布供求</a></span></h3>
                    <%= strAllSupplyList %>

                    <script>
                        $(function() {
                            //多行应用@Mr.Think
                            var _wrap = $('div.mulitline'); //定义滚动区域
                            var _interval = 1500; //定义滚动间隙时间
                            var _moving; //需要清除的动画
                            _wrap.hover(function() {
                                clearInterval(_moving); //当鼠标在滚动区域中时,停止滚动
                            }, function() {
                                _moving = setInterval(function() {
                                    var _field = _wrap.find('a:first'); //此变量不可放置于函数起始处,li:first取值是变化的
                                    var _h = _field.height(); //取得每次滚动高度
                                    _field.animate({ marginTop: -_h + 'px' }, 600, function() {//通过取负margin值,隐藏第一行
                                        _field.css('marginTop', 0).appendTo(_wrap); //隐藏后,将该行的margin值置零,并插入到最后,实现无缝滚动
                                    })
                                }, _interval)//滚动间隔时间取决于_interval
                            }).trigger('mouseleave'); //函数载入时,模拟执行mouseleave,即自动滚动
                        });
                    </script>

                </div>
            </div>
            <!--moduel one end-->
            <div class="hr-7">
            </div>
            <!--moduel two start 旅游资讯-->
            <div id="moduel_2" class="moduel">
                <div class="tt">
                    <div class="lf">
                        <%=ITypeLists%>
                    </div>
                </div>
                <div class="content">
                    <!--左-->
                    <div class="left">
                        <!--广告-->
                        <div class="mImg">
                            <img src='<%=GetAdvertPicture(1,0)%>' alt='<%=GetAdvertPicture(1,2)%>' width="311px"
                                height="202px" />
                            <a href='<%=GetAdvertPicture(1,1)%>' title='<%=GetAdvertPicture(1,2)%>'>
                                <%=GetAdvertPicture(1,2)%></a>
                            <h3>
                            </h3>
                        </div>
                        <!--热点资讯排行-->
                        <div class="mList mListt2">
                            <%=HotLists%>
                        </div>
                    </div>
                    <!--中-->
                    <div class="middle">
                        <!--行业新闻-->
                        <div class="mList mListt2Clear">
                            <%=TradeNewsList%>
                        </div>
                        <!--普通推荐-->
                        <ul class="noTitle noTitleG">
                            <%=TradeBottenLists%>
                        </ul>
                    </div>
                    <!--右-->
                    <div class="right right_border" style="overflow: hidden">
                        <!--嘉宾访谈-->
                        <div class="mList mListM">
                            <%=InterviewLists%>
                        </div>
                        <!--展会大全-->
                        <div class="mList mListTRB">
                            <%=ExpositionLists%>
                        </div>
                    </div>
                </div>
            </div>
            <!--moduel two end-->
            <div class="hr-7">
            </div>
            <!--moduel three start 同业学堂-->
            <div id="moduel_3" class="moduel moduel1">
                <div class="tt">
                    <div class="lf">
                        <%=STypeLists%>
                    </div>
                </div>
                <div class="content">
                    <div class="left leftBG">
                        <!--广告-->
                        <div class="mImg">
                            <img src='<%=GetAdvertPicture(2,0)%>' alt='<%=GetAdvertPicture(2,2)%>' width="311px"
                                height="202px" />
                            <a href='<%=GetAdvertPicture(2,1)%>' title='<%=GetAdvertPicture(2,2)%>'>
                                <%=GetAdvertPicture(2,2)%></a>
                            <h3>
                            </h3>
                        </div>
                        <!--营销工具-->
                        <div class="mList mListBTN">
                            <h3>
                                营销工具</h3>
                            <ul>
                                <li><a target="_blank" href="/DoMarket">
                                    <img src="<%=ImageServerPath %>/Images/News/tongyeMews_93.gif" /></a></li>
                                <li><a target="_blank" href="/CreateShop">
                                    <img src="<%=ImageServerPath %>/Images/News/tongyeMews_95.gif" /></a></li>
                                <li><a target="_blank" href="/DoMarket">
                                    <img src="<%=ImageServerPath %>/Images/News/tongyeMews_97.gif" /></a></li>
                            </ul>
                        </div>
                        <!--最新发布旅游线路-->
                        <div class="mList mListT2">
                            <%=NewestRouteList%>
                        </div>
                    </div>
                    <div class="middle">
                        <!--旅游营销-->
                        <div class="mList mListBo">
                            <%=TourMarketingLists%>
                        </div>
                        <!--企业经营与管理-->
                        <div class="mList mListBo">
                            <%=CompanyManagementLists%>
                        </div>
                        <!--旅游政策法规-->
                        <div class="mList mListBo mListBoC">
                            <%=StartLists%>
                        </div>
                    </div>
                    <div class="right right_border right_borderR">
                        <!--广告-->
                        <a href='<%=GetAdvertPicture(3,1)%>' title='<%=GetAdvertPicture(3,2)%>' target="_blank">
                            <img src='<%=GetAdvertPicture(3,0)%>' alt='<%=GetAdvertPicture(3,2)%>' width="250px"
                                height="80px" /></a>
                        <!--计调指南-->
                        <div class="mList mListM mListMF">
                            <%=TuningmeterLists%>
                        </div>
                        <!--导游带团-->
                        <div class="mList mListM mListMF mListMC">
                            <%=CiceroneLists%>
                        </div>
                    </div>
                </div>
            </div>
            <!--moduel three end-->
            <!--moduel two start 同行社区-->
            <div id="moduel_4" class="moduel moduel2">
                <!--旅游类别-->
                <div class="tt">
                    <div class="lf">
                        <a href="http://club.tongye114.com/news/list.aspx" title="旅游资讯">旅游资讯</a> ┊ <a href="http://club.tongye114.com/lxs/list.aspx"
                            title="旅行社">旅行社</a> ┊ <a href="http://club.tongye114.com/showforum-96.aspx" title="广告区">广告区</a> ┊ <a href="http://club.tongye114.com/showforum-98.aspx" title="新手区">新手区</a>
                    </div>
                </div>
                <div class="content">
                    <%= Utils.GetWebResult(EyouSoft.Common.Domain.UserClub +"/forumblock/aggregationnews.aspx")%>
                    <div class="right" id="rightCompany">
                        <!--最新加盟企业-->
                        <div class="mList mListLa">
                            <%=JoininNews%>
                        </div>
                    </div>

                    <script type="text/javascript">
                        $("#AdvImg1").attr("src", "<%=GetAdvertPicture(4,0)%>").attr("style", "height:80px;width:311px;");
                        $("#AdvImg1").wrap("<a href='<%=GetAdvertPicture(4,1)%>'></a>");

                        $("#pic1").attr("href", "<%=GetPictureMiddle(0,1)%>").html("<img src='<%=GetPictureMiddle(0,0)%>' style='height:66px;width:177px;' />\r");
                        $("#title1").attr("href", "<%=GetPictureMiddle(0,1)%>").text('<%=GetPictureMiddle(0,2)%>');

                        $("#pic2").attr("href", "<%=GetPictureMiddle(1,1)%>").html("<img src='<%=GetPictureMiddle(1,0)%>' style='height:66px;width:177px;' />\r");
                        $("#title2").attr("href", "<%=GetPictureMiddle(1,1)%>").text('<%=GetPictureMiddle(1,2)%>');
                    </script>

                </div>
            </div>
            <!--moduel two end-->
        </div>