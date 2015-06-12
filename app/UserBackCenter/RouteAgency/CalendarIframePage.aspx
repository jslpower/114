<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarIframePage.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.CalendarIframePage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #div_DateTime table
        {
            border: solid 1px #addaf8;
            border-collapse: collapse;
            margin-bottom: 5px;
        }
        #div_DateTime td
        {
            border: solid 1px #addaf8;
            border-collapse: collapse;
            height: 30px;
        }
        #div_DateTime th
        {
            background-color: #d9eefc;
            border: solid 1px #addaf8;
            border-collapse: collapse;
            height: 30px;
        }
        .weektitle
        {
            background-color: #f0dc82;
        }
        #divMonthPreNext
        {
            width: 100%;
            height: 30px;
            border: solid 1px #addaf8;
            background-color: #d9eefc;
            margin: 2px 0px;
            line-height: 30px;
        }
        #linkPreMonth
        {
            float: left;
            margin-left: 15px;
        }
        #linkNextMonth
        {
            float: right;
            margin-right: 15px;
        }
        TD, #divMonthPreNext
        {
            color: #333333;
            font-size: 12px;
            line-height: 20px;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="parentDiv">
        <div id="divMonthPreNext">
        </div>
        <div id="div_DateTime" style="margin-left: 1px">
        </div>
        <div id="hide_div_DateTime" style="display: none;">
        </div>
        <div id="divTourCodeHTML">
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">

    var ThisPage = {
        LoadInitCalendar: function() {
            QGD.initCalendar({
                containerId: "hide_div_DateTime",
                currentDate: <%=CurrentDate %>,
                firstMonthDate: <%=CurrentDate %>,
                nextMonthDate: <%=NextDate %>,
                areatype: <%= AreaType %>,
                listcontainer: "divTourCodeHTML",
                parentContainerID: "parentDiv"
            });
        },
        ChangeCalender: function() {
            var form = $("#parentDiv");
            var divFristHtml = form.find("#thisMonthCalendar");
            var divSecondHtml = form.find("#nextMonthCalendar");
            var preMonth = form.find("#linkPreMonth");
            var nextMonth = form.find("#linkNextMonth");
            form.find("#hide_div_DateTime").html("");
            form.find("#div_DateTime").append(divFristHtml);
            form.find("#div_DateTime").append(divSecondHtml);
            form.find("#divMonthPreNext").append(preMonth);
            form.find("#divMonthPreNext").append(nextMonth);
            QGD.updateCalendar(this.option);
        }
    }
    $(function() {
        //加载日历
        ThisPage.LoadInitCalendar();
        setTimeout(function() {
            ThisPage.ChangeCalender();
        }, 500);
    })
</script>

