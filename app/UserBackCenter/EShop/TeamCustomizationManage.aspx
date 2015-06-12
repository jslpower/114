<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamCustomizationManage.aspx.cs"
    Inherits="UserBackCenter.EShop.TeamCustomizationManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
            font-size: 12px;
        }
        table
        {
            border-collapse: collapse;
        }
        .left1
        {
            background: #BFDAF8;
            color: #074263;
            border-bottom: 1px solid #A5ADC4;
            border-top: 1px solid #fff;
            height: 26px;
            text-align: right;
            font-weight: bold;
        }
        .right1
        {
            background: #fff;
            border-bottom: 1px solid #A5ADC4;
        }
        .kuang
        {
            border: 1px solid #A5ADC4;
        }
        .hang
        {
            background: url(<%=ImageServerUrl%>/images/detail_list_th.gif) repeat-x;
            text-align: center;
            color: #074263;
            font-weight: bold;
            height: 18px;
            padding-top: 5px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="content">
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="javascript" type="text/javascript">
        var TeamCustomizationManage = {
            page: "1",
            ajax: function() {
                var self = this;
                $.ajax({
                url: "AjaxTeamCustomizationManage.aspx?page=" + self.page + "&rd=" + Math.random(),
                    success: function(html) {
                        $("#content").html(html);
                        self.page = $("#hid_intPageIndex").val();
                        self.initPage();
                    }
                });
            },
            initPage: function() {
                $("#content a[teamid]").click(function() {
                    if (confirm("你确定要删除该团队定制信息吗？")) {
                        var id = $(this).attr("teamid")
                        $.ajax({
                            url: "/EShop/AjaxTeamCustomizationManage.aspx?action=del&id=" + id+"&rd="+Math.random(),
                            success: function(html) {
                                var returnMsg = eval(html);
                                if (returnMsg) {
                                    alert(returnMsg[0].Message)
                                    TeamCustomizationManage.ajax();
                                } else {
                                    alert('对不起，删除失败！')
                                }
                            }
                        });
                    }
                    return false;
                });

                $("#TeamCustomizationManage_ExportPage a").click(function() {
                    var equalIndex = $(this).attr("href").lastIndexOf("=");
                    TeamCustomizationManage.page = $(this).attr("href").substring(equalIndex).split("=")[1];
                    TeamCustomizationManage.ajax();
                });
                $("td a[lookid]").click(function() {
                    parent.Boxy.iframeDialog({ title: '团队定制明细', iframeUrl: 'TeamCustomizationInfo.aspx?id=' + $(this).attr("lookid"), width: 700, height: 425, draggable: true });
                    return false;
                });
            }
        };
        $(function() {
            TeamCustomizationManage.ajax();
        });
    </script>

    </form>
</body>
</html>
