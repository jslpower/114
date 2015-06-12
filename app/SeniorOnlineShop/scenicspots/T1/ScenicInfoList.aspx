<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicInfoList.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicInfo" MasterPageFile="/master/ScenicSpotsT1.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ScenicPic.ascx" TagName="ScenicPic" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/master/ScenicSpotsT1.Master" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
    <div class="sidebar02 sidebar02Scenic" onkeyup="return false;">
        <p class="more more03">
            <span>
                <%=TabName%></span>您现在所在位置：<a href="Default.aspx?cid=<%=CompanyId %>">首页</a>
            >> <a>
                <%=TabName%></a></p>
        <div style=" margin-left:15px; margin-right:15px;">
            <div class="searchnews">
                <asp:Literal ID="ltrGL" Visible="false" runat="server" Text="类型："></asp:Literal>
                <asp:DropDownList ID="dplType" runat="server" Visible="false">
                </asp:DropDownList>
                <label>
                    标题：</label>
                <input name="ScenicTitle" type="text" value="" id="txtScenicTitle" size="15">
                <a style="cursor: pointer;" id="btnSearch">
                    <img src="<%=ImageServerPath %>/scenicspots/T1/images/search_btn.gif" class="searchbtn" /></a>
            </div>
            <ul class="news_title">
                <li class="xuhao">序号</li>
                <li>标题</li>
                <li>时间</li>
                <div class="clearboth">
                </div>
            </ul>
            <div id="ScenicInfoListContent" style=" width:97%;">
            </div>
        </div>
        <div class="content">
            <uc1:ScenicPic ID="ScenicPic1" runat="server" />
        </div>
        <div class="clearboth">
        </div>
         <div>
         &nbsp;
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        var ScenicInfoList = {
            page: 1,
            search: function() {
                this.ajax();
            },
            ajax: function() {
                $("#ScenicInfoListContent").html("数据加载中...");
                $.ajax({
                    type: "GET",
                    url: "/scenicspots/T1/AjaxScenicInfoList.aspx?st=<%=TabIndex %>&cid=<%=CompanyId %>" + "&title=" + encodeURI($("#txtScenicTitle").val()) + "&gl=" + $("#ctl00_MainPlaceHolder_dplType").val() + "&Page=" + ScenicInfoList.page + "&rd=" + Math.random(),
                    cache: false,
                    success: function(html) {
                        $("#ScenicInfoListContent").html(html);
                        $("#page a").click(function() {
                            var page = $(this).attr("href").split("Page")[1].replace("=", "");
                            ScenicInfoList.page = page;
                            ScenicInfoList.ajax();
                            return false;
                        });
                    }
                });
            }
        };
        $(function() {
            ScenicInfoList.ajax();
            $("#btnSearch").click(function() {
                ScenicInfoList.search();
                return false;
            });
            $("#txtScenicTitle").keypress(function(e) {
                if (e.keyCode == 13) {
                    ScenicInfoList.search();
                    return false;
                }
            });
        })
    </script>

</asp:Content>
