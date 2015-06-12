<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQContent.aspx.cs" Inherits="UserBackCenter.MQContent" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="fangke">
        <div class="queleft">
        </div>
        <div class="bar_on_comm">
            MQ好友
        </div>
        <div class="fangkerc">
            <span style="cursor: pointer;" onclick="link();">同步</span></div>
        <div class="friendbox" id="divMQUserList">
        </div>
    </div>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript">
     $(document).ready(function(){
        LoadMQUserList();
     });
     var linkstate = 0;  //同步状态 0：未同步  1：同步中
     //获取分组和未在分组中的好友     var rnd = "";

     function LoadMQUserList()
     {
//        linkstate = 1;
        var strUrl = "MQGroupList.aspx?MQID=<%=MQID %>&rnd=" + rnd;
        $.ajax({
            url:strUrl,
            cache:false,
            success:function(html)
            {
                $("#divMQUserList").html(html);
                linkstate = 0;
            }
        });
     }
     
     //获取分组下的好友
     function LoadData(GroupName)
     {     
        var strUrl = "MQFriendList.aspx?MQID=<%=MQID %>&GroupName="+ encodeURIComponent(GroupName);
        $.ajax({
            url: strUrl,
            cache:false,
            success:function(html)
            {
                 $("#divFriendList_"+GroupName).html(html);             
            }
        });
       if($("#divFriendList_"+GroupName).html() != '')
           $("#hidState_"+GroupName).val(GroupName);   
     }
     
     function link()
     {
        if(linkstate == 1)
        {
            alert("同步中...");
        }    
        else if(linkstate == 0)
        {
            linkstate = 1;
            //同步操作
            var strUrl = "MQGroupList.aspx?MQID=<%=MQID %>&IsStep=1";
            
            $.ajax({
                url: strUrl,
                cache:false,
                success:function(html)
                {
                     if(html == "True")             //同步成功
                     {
                        rnd = Math.random();
                        LoadMQUserList();
                     }
                }
            });
         }
     }
    </script>
    </form>
</body>
</html>
