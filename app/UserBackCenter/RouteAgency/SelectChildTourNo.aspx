<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectChildTourNo.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.SelectChildTourNo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择出团日期</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
        table
        {
            border-collapse: collapse;
        }
        img
        {
            border: none;
        }
        .hui
        {
            color: #aaaaaa;
        }
    </style>
</head>
<body>
    <div id="div1">
    </div><br />
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
        <tr>
            <td align="center">
                <input type="button" name="btnSave" id="btnSave" value=" 确 定 " style="height:25px;display:none;" onclick="QGD.SubmitClick();" />
            </td>
        </tr>
    </table>
    <div id="divTourCodeHTML">
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript">
    $(document).ready(function(){
    var oldLeaveDate,oldTourCode;
    oldLeaveDate = 'hidChildLeaveDateList';
    oldTourCode = 'hidChildTourCodeList';
    //var date = new Date();
    
    QGD.initCalendar({
         containerId:"div1",
         currentDate:<%=CurrentDate %>,
         firstMonthDate:<%=CurrentDate %>,
         nextMonthDate:<%=NextDate %>,
         areatype:<%=AreaType %>,
         listcontainer:"divTourCodeHTML",
         oldLeaveDate:oldLeaveDate,
         oldTourCode:oldTourCode,
         parentContainerID:"<%=ContaierID %>"
        });
     if('<%=flag %>' == 'edit')
     {
        $("#btnSave").css("display","none");
     }else{
        $("#btnSave").css("display","");
     }
    });
    </script>

</body>
</html>
