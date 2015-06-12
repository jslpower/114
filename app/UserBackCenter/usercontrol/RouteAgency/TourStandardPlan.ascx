<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourStandardPlan.ascx.cs"
    Inherits="UserBackCenter.usercontrol.RouteAgency.TourStandardPlan" %>
<table width="96%" border="1" align="center" cellpadding="0" cellspacing="0" class="lankuang"
    style="margin-top: 8px; " id="<%=ContainerID %>tbDateInfo">
    <tr class="white" height="23" id="tr_header">
        <td width="5%" align="center" class="shenglan">
            <strong>日程</strong>
        </td>
        <td width="17%" align="center" class="shenglan_lr">
            <strong>行程内容</strong>
        </td>
        <td width="15%" align="center" class="shenglan_lr">
            <strong>交通工具</strong>
        </td>
        <td width="14%" align="center" class="shenglan_lr">
            <strong>班次</strong>
        </td>
        <td width="24%" align="center" class="shenglan_lr">
            <strong>住宿</strong>
        </td>
        <td width="20%" align="center" class="shenglan_lr">
            <strong>用餐</strong>
        </td>
        <td width="5%" align="center" class="shenglan_lr">
            操作
        </td>
    </tr>
    <%=tmpStandardPlan%>
</table>
<script type="text/javascript">
    var TourStandardPlan = {  
	    _getData:function(id){
		    return commonTourModuleData.get(id);
	    },      
        AddDateInfo: function(initmodel,id) {//新增行
            var obj = this._getData(id);
            var strTraffic = "";
            var trString = "<tr>";
            var Index = 1;
            if ($("#"+ obj.ContainerID +"tbDateInfo").find("tr").length != 1) {
                Index = parseInt((($("#"+ obj.ContainerID +"tbDateInfo").find("tr").length / 2) + 1).toString());
            }
            var arrTraffic = ["飞机","火车","轮船","汽车" ];
            var PlanInterval = "", TrafficNumber = "", House = "", PlanContent = "", Dinner = "";
            if(initmodel != null)
            {
                PlanInterval = initmodel.PlanInterval;
                Index = initmodel.PlanDay;
                strTraffic = initmodel.Vehicle;
                TrafficNumber = initmodel.TrafficNumber;
                House = initmodel.House;
                Dinner = initmodel.Dinner;
                PlanContent = initmodel.PlanContent;
            }
            //区间
            trString += "<td rowspan='2' class='zhonglan' align='center'>";
            trString += "<input type=\"hidden\" class=\"input\" id=\"DayJourney\" name=\"DayJourney\" value=\"" + Index + "\">D" + Index + "";
            trString += "</td>";
            trString += "<td  class='zhonglan_l'>区间：<input name=\"SightArea" + Index + "\" class=\"input\" size='12' type=\"text\" id=\"SightArea\" value=\""+ PlanInterval +"\">";
            trString += "</td>";

            //交通工具
            trString += "<td class='zhonglan_l' width='120' align='center'>";
            trString += "<select name=\"Vehicle"+ Index +"\" id=\"Vehicle\">";
            trString += "<option value=''>请选择</option>";
            for(var i = 0; i < arrTraffic.length; i ++)
            {
                if(arrTraffic[i] == strTraffic)
                {
                    trString += "<option value=\""+ arrTraffic[i] +"\" selected>"+ arrTraffic[i] +"</option>";
                }else{
                    trString += "<option value=\""+ arrTraffic[i] +"\">"+ arrTraffic[i] +"</option>";
                }
            }
            trString += "</select></td>";

            //班次
            trString += "<td class='zhonglan_l' width='120' align='center'><input name=\"TrafficNumber" + Index + "\" class=\"input\" type=\"text\" id=\"TrafficNumber\" value=\""+ TrafficNumber +"\" size=\"15\">";
            trString += "</td>";

            //住宿
            trString += "<td class='zhonglan_l' align='center'><input name=\"Resideplan" + Index + "\" class=\"input\" type=\"text\" id=\"Resideplan\" value=\""+ House +"\" size=\"15\">";
            trString += "</td>";

            //用餐
            trString += "<td class='zhonglan_l'' width='150' align='center'>";
            var oneChecked,twoChecked,threeChecked;
            if(Dinner.indexOf('早') > -1)
            {
                oneChecked = "checked";
            }
            if(Dinner.indexOf('中') > -1)
            {
                twoChecked = "checked";
            }
            if(Dinner.indexOf('晚') > -1)
            {
                threeChecked = "checked";
            }
            trString += "<input name=\"DinnerPlan" + Index + "\" type=\"checkbox\" id=\"DinnerPlan\" value=\"早\" "+ oneChecked +">&nbsp;早&nbsp;";
            trString += "<input name=\"DinnerPlan" + Index + "\" type=\"checkbox\" id=\"DinnerPlan\" value=\"中\" "+ twoChecked +">&nbsp;中&nbsp;";
            trString += "<input name=\"DinnerPlan" + Index + "\" type=\"checkbox\" id=\"DinnerPlan\" value=\"晚\" "+ threeChecked +">&nbsp;晚&nbsp;";
            trString += "</td>";
            //操作列 删除
            trString += "<td rowspan='2' width='75' class='zhonglan_l' align='center'><a href=\"javascript:void(0)\" onclick=\"TourStandardPlan.DeleteOneyDay(this,'"+ obj.ContainerID +"');return false;\">删除</a>";
            trString += "</td>";

            trString += "</tr>";
            trString += "<tr>";
            trString += "<td colSpan='5' class='zhonglan_l' align='left'><textarea name=\"JourneyInfo" + Index + "\" class=\"input\" cols=\"120\" rows=\"3\" id=\"JourneyInfo\">"+ PlanContent +"</textarea>";
            trString += "</tr>";
            $("#"+ obj.ContainerID +"tbDateInfo").append(trString);
        },
        DeleteOneyDay: function(element,id) {
            var obj = this._getData(id);
            //删除当前行
            var deleteIndex = $(element).parent().parent().find("input[id=DayJourney]").val();
            $("#"+ obj.ContainerID +"tbDateInfo").find("tr").each(function(i) {
                if (i == (deleteIndex * 2) || i + 1 == (deleteIndex * 2)) {
                    $(this).remove();
                }
            });
            //重新排列
            //序号
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=DayJourney]").each(function(i) {
                $(this).parent().html("<input type=\"hidden\" class=\"input\" id=\"DayJourney\" name=\"DayJourney\" value=\"" + (i+1) + "\">D" + (i+1) + "");
            });
            //区间内容
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=SightArea]").each(function(i) {
                $(this).attr("name", "SightArea" + (i + 1));
            });
            //交通工具
            $("#"+ obj.ContainerID +"tbDateInfo").find("select[id=Vehicle]").each(function(i) {
                $(this).attr("name", "Vehicle" + (i + 1));
            });
            //班次
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=TrafficNumber]").each(function(i) {
                $(this).attr("name", "TrafficNumber" + (i + 1));
            });
            //住宿
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=Resideplan]").each(function(i) {
                $(this).attr("name", "Resideplan" + (i + 1));
            });
            //用餐
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=DinnerPlan]").each(function() {
                var index = $(this).parent().parent().find("input[id=DayJourney]").val();
                $(this).attr("name", "DinnerPlan" + index);
            });
            //行程内容
            $("#"+ obj.ContainerID +"tbDateInfo").find("input[id=JourneyInfo]").each(function(i) {
                $(this).attr("name", "JourneyInfo" + (i + 1));
            });

            //修改天数                   
            var Day = 0;   
            var DayObj;
            DayObj = $("#"+ obj.ContainerID).find("input[type=text][id$=TourDays]");
            txtDay = $.trim(DayObj.val());
            if (txtDay != "" && !isNaN(parseInt(txtDay)) && parseInt(txtDay) > 0) {
                Day = txtDay;
            }
            if (Day != 0) {
                DayObj.val(parseInt(parseInt(Day) - 1));
            }
            if (($("#"+ obj.ContainerID +"tbDateInfo").find("tr").length) == 1) {
                $("#"+ obj.ContainerID +"tbDateInfo").hide();
            }
        }
    };
</script>
