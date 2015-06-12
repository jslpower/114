<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserListManage.aspx.cs" Inherits="SiteOperationsCenter.UserManage.UserListManage" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
 <%--   <link href='http://localhost:30001/css/YunYing/manager2.css?v=1' rel="stylesheet" type="text/css" />--%>
   <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
      <form id="form1" name="form1" method="post" action=""  runat="server">
<table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="30%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif" ><table width="99%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="4%" align="right"><img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" /></td>
        <td width="23%"><a href="javascript:void(0);" onclick="return UserListManage.addUser()"><img src="<%=ImageServerUrl %>/images/yunying/xinzeng.gif" width="50" height="25" border="0" /></a></td>
        <td width="4%"><img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" /></td>
        <td width="24%"><a href="javascript:void(0);" onclick="return UserListManage.updateUser()"><img src="<%=ImageServerUrl %>/images/yunying/xiugai.gif" width="50" height="25" border="0" /></a></td>
		<td width="5%"><img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" /></td>
        <td width="23%"><a href="javascript:void(0)" onclick="return UserListManage.deleteUser()"><img src="<%=ImageServerUrl %>/images/yunying/shanchu.gif" width="51" height="25" /></a></td>
        <td width="17%"><img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" /></td>
      </tr>
    </table></td>
    <td width="70%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif" align="left">
     <uc1:pc id="ulm_pcList" runat="server" ></uc1:pc>
      用户名
      <input id="ulm_txtUserName" type="text" class="textfield" size="10" onkeyup="UserListManage.isEnter(event)" />
      姓名
      <input id="ulm_txtRealName" type="text" class="textfield" size="10"  onkeyup="UserListManage.isEnter(event)"/>
     <a href="javascript:void(0);" onclick="return UserListManage.search()"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21"  /></a></td>
  </tr>
</table>
<div id="ulm_divUserList" style="text-align:center;">
</div>

</form>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    <script language="JavaScript" type="text/javascript">
        var searchParams={province:"",city:"",username:"",realname:"",Page:""};
        var UserListManage = {
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            },
            //获取MQ审核列表
            getUserList: function() {
                LoadingImg.ShowLoading("ulm_divUserList");
                if (LoadingImg.IsLoadAddDataToDiv("ulm_divUserList")) {
                    $.ajax({
                        type: "post",
                        dataType: "html",
                        url: "AjaxUserListManage.aspx?isajax=yes",
                        data: searchParams,
                        cache: false,
                        success: function(result) {
                            if (/^notLogin$/.test(result)) {
                                alert("对不起，你尚未登录请登录!");
                                return false;
                            }
                            $("#ulm_divUserList").html(result);
                        },
                        error: function() {
                            $("#ulm_divUserList").html("");
                            alert("请求数据时发生错误!");
                        }
                    });
                }
            },
            addUser: function() {
                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
                window.location = "UserSet.aspx";
                return false;
            },
            updateUser_link: function() {
                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
            },
            updateUser: function() {

                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
                var checkUser = $("#ulm_divUserList").find(":checkbox:checked");
                if (checkUser.length == 0) {
                    alert("请选择要修改的账户!");
                    return false;
                }
                if (checkUser.length > 1) {
                    alert("只能选择一个账户修改!");
                    return false;
                }
                window.location = "UserSet.aspx?userid=" + checkUser.val();
                return false;
            },
            deleteUser: function() {
                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
                var checkUser = $("#ulm_divUserList").find(":checkbox:checked");
                if (checkUser.length == 0) {
                    alert("请选择要删除的账户!");
                    return false;
                }
                if (!confirm("你确定要删除选中的账户吗?")) {
                    return false;
                }
                var userIds = "";
                checkUser.each(function() {
                    userIds += $(this).val() + ",";
                });
                userIds = userIds.replace(/,$/, '');
                searchParams.method = "delete";
                searchParams.userid = userIds;
                UserListManage.search();
                delete searchParams.method;
                delete searchParams.userid;
                return false;
            },
            //查询操作
            search: function() {
                searchParams.province = $("#ulm_pcList_ddl_ProvinceList").val();
                searchParams.city = $("#ulm_pcList_ddl_CityList").val();
                if (searchParams.province != "0" && searchParams.city == "0") {
                    alert("请选择城市！");
                    return false;
                }

                searchParams.username = $("#ulm_txtUserName").val();
                searchParams.realname = $("#ulm_txtRealName").val();
                UserListManage.getUserList();
                return false;
            },
            //判断是否按回车
            isEnter: function(event) {
                event = event ? event : window.event;
                if (event.keyCode == 13) {
                    UserListManage.search();
                }
            },
            //分页操作
            loadData: function(obj) {
                var pageIndex = exporpage.getgotopage(obj);
                searchParams.Page = pageIndex;
                UserListManage.getUserList();
            },
            //设置用户状态
            setState: function(tar_a, userId) {
                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
                var theState = $(tar_a).html() == "启用" ? "unforbid" : "forbid";
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: "UserListManage.aspx?isajax=yes",
                    data: { id: userId, method: "setState", state: theState },
                    cache: false,
                    success: function(result) {
                        if (/^notLogin$/.test(result)) {
                            alert("对不起，你尚未登录请登录!");
                            return false;
                        }
                        if (result.success == "1") {
                            theState == "forbid" ? $(tar_a).html("启用") : $(tar_a).html("停用");
                            if (theState == "启用") {
                                alert("启用成功!");
                            }
                            else {
                                alert("停用成功!");
                            }
                        }
                    },
                    error: function() {
                        alert("操作失败!");
                    }
                });
            }
        }
         $(document).ready(function(){
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
             UserListManage.getUserList();
         });


    </script>

</body>
</html>
