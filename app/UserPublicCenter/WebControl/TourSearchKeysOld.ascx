<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourSearchKeysOld.ascx.cs" Inherits="UserPublicCenter.WebControl.TourSearchKeysOld" %>

<table width="100%" border="0" cellspacing="0" cellpadding="3">
    <tr>
        <td align="left" class="lv" style="padding: 10px 10px 5px 10px; border-bottom: 1px dashed #ccc;">
            <img src="<%=ImageServerPath %>/images/UserPublicCenter/icoline.gif" width="11" height="11" />
            <span>
                <%=AllTourTheme%>
            </span>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="3">
    <tr>
        <td align="left" style="padding-left: 10px; border-bottom: 1px dashed #ccc;">
            <span class="lv" style="padding-top: 10px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/icoline.gif" width="11" height="11" /></span><strong>筛选条件</strong>：<br />
            <%if (!IsDefault)
              { %>
            <span id="span_Price">价格区间：<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId,CityId)%>"
                style="color: red"> 全部</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,1, CityId)%>">
                    1-300元</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,2, CityId)%>">
                        300-800元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,3, CityId)%>">
                            800-1600元</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,4, CityId)%>">
                                1600-3000元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,5, CityId)%>">
                                    3000-6000元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourPriceUrl(TourAreaId,6, CityId)%>">
                                        6000元以上</a> </span>
            <br />
            <span id="span_Day">行程天数：<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId,CityId)%>"
                style="color: red"> 全部</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,1, CityId)%>">
                    1日游</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,2, CityId)%>">2日游</a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,3, CityId)%>"> 3日游</a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,4, CityId)%>"> 4日游 </a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetToruDayUrl(TourAreaId,5, CityId)%>"> 5日游及以上</a>
            </span>
            <%}
              else
              { %>
            <span id="span_Price">价格区间：<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(TourAreaId,CityId)%>"
                style="color: red"> 全部</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,1, CityId)%>">
                    1-300元</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,2, CityId)%>">
                        300-800元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,3, CityId)%>">
                            800-1600元</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,4, CityId)%>">
                                1600-3000元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,5, CityId)%>">
                                    3000-6000元</a> |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourPriceUrl(TourAreaId,6, CityId)%>">
                                        6000元以上</a> </span>
            <br />
            <span id="span_Day">行程天数：<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(TourAreaId,CityId)%>"
                style="color: red"> 全部</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefToruDayUrl(TourAreaId,1, CityId)%>">
                    1日游</a> | <a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefToruDayUrl(TourAreaId,2, CityId)%>">2日游</a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefToruDayUrl(TourAreaId,3, CityId)%>"> 3日游</a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefToruDayUrl(TourAreaId,4, CityId)%>"> 4日游 </a>
                |<a href="<%=EyouSoft.Common.URLREWRITE.Tour.GetDefToruDayUrl(TourAreaId,5, CityId)%>"> 5日游及以上</a>
            </span>
            <%} %>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px  dashed #ccc;
    height: 30px;" runat="server" id="tbMoreSearch">
    <tr>
        <td width="30" align="right">
            <img src="<%=ImageServerPath %>/images/UserPublicCenter/chaxunbiao4.gif" />
        </td>
        <td>
           线路名称：
            <input id="txtRouteName" runat="server" type="text" name="txtRouteName" style="width: 100px;
                height: 15px; border: 1px solid #ccc;" maxlength="100" />天数：<input id="txtDays" runat="server"
                    type="text" name="txtDays" style="width: 25px; height: 15px; border: 1px solid  #ccc;" />批发商名称：<input
                        type="text" runat="server" id="txtCompanyName" name="txtCompanyName" style="width: 90px;
                        height: 15px; border: 1px solid  #ccc;" maxlength="100" />出团日期：<input type="text"
                            runat="server" id="txtStartDate" name="txtStartDate" style="width: 70px; height: 15px;
                            border: 1px solid  #ccc;" />
            -
            <input type="text" runat="server" id="txtEndDate" name="txtEndDate" style="width: 70px;
                height: 15px; border: 1px solid  #ccc;" />
        </td>
        <td>
            <img style="cursor: pointer" id="btnSearch" src="<%=ImageServerPath %>/images/UserPublicCenter/chaxunannui2.gif"
                height="19" />
        </td>
    </tr>
</table>

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
    $(function() {
        $("#<%=txtStartDate.ClientID%>").focus(function() {
            WdatePicker();
        });
        $("#<%= txtEndDate.ClientID%>").focus(function() {
            WdatePicker();
        });
 
        
        $("#btnSearch").click(function() {
            var Url = "<%=strUrl %>?TourAreaId=<%=TourAreaId %>&SearchType=More";
            var RouteName = $.trim($("#<%=txtRouteName.ClientID %>").val());
            var Days = $.trim($("#<%=txtDays.ClientID %>").val());
            var CompanyName = $.trim($("#<%=txtCompanyName.ClientID %>").val());
            var StartDate = $.trim($("#<%=txtStartDate.ClientID %>").val());
            var EndDate = $.trim($("#<%=txtEndDate.ClientID %>").val());
            window.location.href = Url + "&RouteName=" + encodeURIComponent(RouteName) + "&Days=" + Days + "&CompanyName=" + encodeURIComponent(CompanyName) + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "<%=strUrlParms %>";
        });

         if("<%=IsRoute %>"=="False"){
         
          $("#ctl00_Main_TourSearchKeys1_tbMoreSearch input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                   $("#btnSearch").click();
                   return false;
                }
            });
            
          $("#<%=txtCompanyName.ClientID %>").autocomplete("/TourManage/GetCompanyList.ashx?rand=" + Math.random(),{
            delay:10,
            minChars:1,
            matchSubset:false,
            cacheLength:1,
            onItemSelect:selectItem,
            onFindValue:findValue,
            autoFill:false,
            maxItemsToShow:10,
            spaceCount:3,
            spaceFlag:"~&&~",
            addWidth:200, 
            IsFocusShow:true,
            extraParams: { 
                             CityId: <%=CityId %>,
                             TourAreaId: <%= TourAreaId %>
                        } ,
            openSelectedEvent:true
          });
       }
    });
  
</script>
