<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQFriendList.aspx.cs" Inherits="UserBackCenter.MQFriendList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <p>
        <asp:Repeater ID="rptMQFriendList" runat="server">
            <ItemTemplate>
                <%# GetFriendInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "FriendMQId")), Convert.ToString(DataBinder.Eval(Container.DataItem, "FriendName")), Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsOnline")))%>
            </ItemTemplate>
        </asp:Repeater>
    </p>
</body>
</html>
