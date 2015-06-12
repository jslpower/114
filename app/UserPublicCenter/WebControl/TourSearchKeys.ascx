<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourSearchKeys.ascx.cs"
    Inherits="UserPublicCenter.WebControl.TourSearchKeys" %>
<ul class="xianluul">
    <li class="xianshaixuan" style="display: block">
        <ul>
            <li class="xianlink"><strong><span>主题：</span></strong>
                <%=AllTourTheme%>
            </li>
            <li class="xianlink"><strong><span>价格：</span></strong> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId,CityId)%>"
                <%= (SearchType!="Price" &&(SearchType=="More" && priceid==0))?"style=\"color: red;font-weight:bold\"":"" %>>
                全部</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,1, CityId)%>"
                    <%= SearchType=="Price"&&SetId=="1"?"style=\"color: red;font-weight:bold\"":"" %>
                    <%= SearchType=="More"&&priceid==1?"style=\"color: red;font-weight:bold\"":"" %>>
                    100元以下</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,2, CityId)%>"
                        <%= SearchType=="Price"&&SetId=="2"?"style=\"color: red;font-weight:bold\"":"" %><%= SearchType=="More"&&priceid==2?"style=\"color: red;font-weight:bold\"":"" %>>
                        100-300元</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,3, CityId)%>"
                            <%= SearchType=="Price"&&SetId=="3"?"style=\"color: red;font-weight:bold\"":"" %>
                            <%= SearchType=="More"&&priceid==3?"style=\"color: red;font-weight:bold\"":"" %>>
                            300-1000元</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,4, CityId)%>"
                                <%= SearchType=="Price"&&SetId=="4"?"style=\"color: red;font-weight:bold\"":"" %>
                                <%= SearchType=="More"&&priceid==4?"style=\"color: red;font-weight:bold\"":"" %>>
                                1000-3000元</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,5, CityId)%>"
                                    <%= SearchType=="Price"&&SetId=="5"?"style=\"color: red;font-weight:bold\"":"" %>
                                    <%= SearchType=="More"&&priceid==5?"style=\"color: red;font-weight:bold\"":"" %>>
                                    3000-10000元</a> <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,6, CityId)%>"
                                        <%= SearchType=="Price"&&SetId=="6"?"style=\"color: red;font-weight:bold\"":"" %>
                                        <%= SearchType=="More"&&priceid==6?"style=\"color: red;font-weight:bold\"":"" %>>
                                        10000元以上</a> </li>
            <li class="xianlink"><strong><span>天数：</span></strong> <a name="aDay" id="aDay0"
                href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId,CityId)%>"
                <%= (SearchType!="Day" &&(SearchType=="More" && daysid==0))?"style=\"color: red;font-weight:bold\"":"" %>>
                全部</a> <a name="aDay" id="aDay1" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,1, CityId)%>"
                    <%= SearchType=="Day"&&SetId=="1"?"style=\"color: red;font-weight:bold\"":"" %>
                    <%= SearchType=="More"&&daysid==1?"style=\"color: red;font-weight:bold\"":"" %>>
                    1日游</a> <a name="aDay" id="aDay2" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,2, CityId)%>"
                        <%= SearchType=="Day"&&SetId=="2"?"style=\"color: red;font-weight:bold\"":"" %>
                        <%= SearchType=="More"&&daysid==2?"style=\"color: red;font-weight:bold\"":"" %>>
                        2日游</a> <a name="aDay" id="aDay3" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,3, CityId)%>"
                            <%= SearchType=="Day"&&SetId=="3"?"style=\"color: red;font-weight:bold\"":"" %>
                            <%= SearchType=="More"&&daysid==3?"style=\"color: red;font-weight:bold\"":"" %>>
                            3日游</a> <a name="aDay" id="aDay4" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,4, CityId)%>"
                                <%= SearchType=="Day"&&SetId=="4"?"style=\"color: red;font-weight:bold\"":"" %>
                                <%= SearchType=="More"&&daysid==4?"style=\"color: red;font-weight:bold\"":"" %>>
                                4日游 </a><a name="aDay" id="aDay5" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,5, CityId)%>"
                                    <%= SearchType=="Day"&&SetId=="5"?"style=\"color: red;font-weight:bold\"":"" %>
                                    <%= SearchType=="More"&&daysid==5?"style=\"color: red;font-weight:bold\"":"" %>>
                                    5日游</a> <a name="aDay" id="aDay6" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,6, CityId)%>"
                                        <%= SearchType=="Day"&&SetId=="6"?"style=\"color: red;font-weight:bold\"":"" %>
                                        <%= SearchType=="More"&&daysid==6?"style=\"color: red;font-weight:bold\"":"" %>>
                                        6日游</a> <a name="aDay" id="aDay7" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,7, CityId)%>"
                                            <%= SearchType=="Day"&&SetId=="7"?"style=\"color: red;font-weight:bold\"":"" %>
                                            <%= SearchType=="More"&&daysid==7?"style=\"color: red;font-weight:bold\"":"" %>>
                                            7日游 </a><a name="aDay" id="aDay8" href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,8, CityId)%>"
                                                <%= SearchType=="Day"&&SetId=="8"?"style=\"color: red;font-weight:bold\"":"" %>
                                                <%= SearchType=="More"&&daysid==8?"style=\"color: red;font-weight:bold\"":"" %>>
                                                7日游及以上</a> </li>
            <li class="xianlink"><strong><span>月份：</span></strong>
                <%=Month%>
            </li>
        </ul>
    </li>
</ul>
<div class="xianluss">
    <label>
        关键字:</label>
    <input id="txtRouteName" runat="server" type="text" name="txtRouteName" style="width: 120px;
        height: 15px; border: 1px solid #ccc; color:#ccc" maxlength="100" value="线路,特色,途径区域" />
    <label>
        出发城市:</label>
    <input id="txtCity" runat="server" type="text" name="txtCity" style="width: 60px;
         height: 15px; border: 1px solid  #ccc; color:#ccc" value="如:杭州" />
    <label>
        出发日期:</label>
    <input type="text" size="15" id="txtStartDate" name="txtStartDate" runat="server"
        style="width: 65px; height: 15px; border: 1px solid  #ccc;" onfocus="WdatePicker({onpicked:function(){$('#ctl00_c1_TourSearchKeys1_txtStartDate').focus();},minDate:'%y-%M-#{%d}'})" />-<input
            type="text" runat="server" size="15" name="txtEndDate" style="width: 65px; height: 15px;
            border: 1px solid  #ccc;" id="txtEndDate" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_c1_TourSearchKeys1_txtStartDate\')}'})" />
    <input type="button" id="btnSearch" class="btnstyle" />
</div>

<script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

<script type="text/javascript">
    function findValue(li) {
        if (li == null) return alert("No match!");
        if (!!li.extra)
            var sValue = li.extra[0];
    }
    function selectItem(li) {
        findValue(li);
    }
    function lookupLocal() {
        var oSuggest = $(".ajaxinput")[0].autocompleter;
        oSuggest.findValue();
        return false;
    }
    function SetSearchItem(Type, SetValue) {
        $($("#span_" + Type).find("a").get(0)).css("color", "");
        $($("#span_" + Type).find("a").get(SetValue)).css("color", "red");
    }
    
     $("#<%=txtCity.ClientID %>").focus(function() {
                if ($(this).val() == "如:杭州") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val("如:杭州").css("color", "#ccc");

                }
            }); 
     $("#<%=txtRouteName.ClientID %>").focus(function() {
                if ($(this).val() == "线路,特色,途径区域") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val("线路,特色,途径区域").css("color", "#ccc");

                }
            }); 
            
            //绑定登录回车
            $("#<%=txtCity.ClientID %>,#<%=txtRouteName.ClientID %>").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    $("#btnSearch").click();
                    return false;
                }
            });    
    
    $(function() {
        $("#<%=txtStartDate.ClientID%>").focus(function() {
            WdatePicker({onpicked:function(){
            $('#ctl00_ContentPlaceHolder1_txt_RDate').focus();},minDate:'%y-%M-#{%d}'});
        });
        $("#<%= txtEndDate.ClientID%>").focus(function() {
            WdatePicker({minDate:'#F{$dp.$D(\'txtStartDate\')}'});
        });
        
        $("#btnSearch").click(function() {
            var Url = "<%=strUrl %>?TourAreaId=<%=TourAreaId %>&SearchType=More";
            var KeyWord = $.trim($("#<%=txtRouteName.ClientID %>").val())=="线路,特色,途径区域"?"":$.trim($("#<%=txtRouteName.ClientID %>").val());
            var City = $.trim($("#<%=txtCity.ClientID %>").val())=="如:杭州"?"":$.trim($("#<%=txtCity.ClientID %>").val());
            var StartDate = $.trim($("#<%=txtStartDate.ClientID %>").val());
            var EndDate = $.trim($("#<%=txtEndDate.ClientID %>").val());
            window.location.href = Url + "&keyWord=" + escape(KeyWord) + "&City=" + escape(City) + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "<%=strUrlParms %>";
        });

         if("<%=IsRoute %>"=="False"){
         
          $("#ctl00_Main_TourSearchKeys1_tbMoreSearch input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                   $("#btnSearch").click();
                   return false;
                }
            });
       }
    });
  
</script>

