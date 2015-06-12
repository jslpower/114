<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQGroupList.aspx.cs" Inherits="UserBackCenter.MQGroupList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <asp:Repeater ID="rptMQGroupName" runat="server">
        <ItemTemplate>
            <h3>
                <%# DataBinder.Eval(Container.DataItem, "GroupName")%>(<%# DataBinder.Eval(Container.DataItem, "OnlineFriendCount")%>/<%# DataBinder.Eval(Container.DataItem, "FriendCount")%>)</h3>
            <input type="hidden" name="hidGroupName" id="hidGroupName" value='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>' />
            <div id="divFriendList_<%# DataBinder.Eval(Container.DataItem, "GroupName")%>">
            </div>
            <input type="hidden" id="hidState_<%# DataBinder.Eval(Container.DataItem, "GroupName")%>" />
            <input type="hidden" id="hidFriendCount_<%# DataBinder.Eval(Container.DataItem, "GroupName")%>"
                value='<%# DataBinder.Eval(Container.DataItem, "FriendCount")%>' />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="rptNoGroupFriend" runat="server">
        <ItemTemplate>
            <h4>
                <%# GetFriendInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "FriendMQId")), Convert.ToString(DataBinder.Eval(Container.DataItem, "FriendName")), Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsOnline")))%>
            </h4>
        </ItemTemplate>
    </asp:Repeater>
    <a href="http://im.tongye114.com/" target="_blank"><img id="imgMQAdv" src="<%=ImageServerPath %>/images/mqad.gif" /></a>

    <script type="text/javascript">
    
    var fcount = '<%=FriendCount %>';
//    if(fcount > "0")
//    {
//        $("#imgMQAdv").attr("src","<%=ImageServerPath %>/images/aa.gif");
//    }
    var rnd = '<%= Request.QueryString["rnd"] %>';
    if(rnd == "" || rnd == undefined)
    {
        var tmpcount = '<%=GroupCount %>';
        if(fcount == '0' && tmpcount == '0')
        {
            link();
        }
    }
    
     $(".friendbox h3").next().next().hide();
        $(".friendbox h3").each(function(){
            $(this).click(function(){
                if($(this).next().next().css("display") == "none")
                {
                    var GroupName = $(this).next().val();
                    if($("#hidState_"+GroupName).val() == '')
                       LoadData(GroupName);
                                              
                    if($("#hidFriendCount_"+ GroupName).val() != '' && $("#hidFriendCount_"+ GroupName).val() != '0')
                    {
                        $(this).next().next("div").css("display","block");
                        $(this).siblings("h3").next().next("div").css("display","none");
                        $(this).toggleClass("active");
		                $(this).siblings("h3").removeClass("active");
                    }
                }else{
                    $(this).next().next().css("display","none");
                    $(this).removeClass("active");
                }
            });
        });
    </script>

</body>
</html>
