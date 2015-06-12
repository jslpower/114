<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicInfoDetail.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicInfoDetail" MasterPageFile="/master/ScenicSpotsT1.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ScenicPic.ascx" TagName="ScenicPic" TagPrefix="uc1" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
    <div class="sidebar02 sidebar02Scenic">
        <p class="more more03">
            <span>
                <%=TabName %></span>您现在所在位置：<a href="Default.aspx?cid=<%=CompanyId %>">首页</a>
            >> <a runat="server" id="likReturn">
                <%=TabName %></a>
        </p>
        <div class="content">
            <%if (!IsAdmissionPolicy)
              { %><%--非门票政策时显示--%>
            <div class="newscontenttitle">
                <h1 class="C_green">
                    <%=StrTitle%></h1>
                <em>发布时间：[<%=IssueTime%>]</em></div>
            <%if (!string.IsNullOrEmpty(ImagePath))
              { %>
            <div style=" text-align:center;">
                <img id="ScenicInfoPic" src="<%=EyouSoft.Common.Domain.FileSystem+ImagePath%>"
                    alt="图片" />
            </div>
            <%}%>
            <%}%>
            <div class="text" style="width:679px;">
                <%=ContentText%>
                <%if (IsAdmissionPolicy && string.IsNullOrEmpty(ContentText))
                  { %>
                <%--门票政策，且没有设置门票政策时--%>
                <font class="C_green">地点：</font><br>
                <font class="C_green">门票价：</font><font class="C_orange Font14b">￥0.0</font><br>
                <font class="C_green">优惠说明：</font><br>
                <font class="C_green">团队预订说明：</font>
                <%} %>
            </div>
            <div>
                &nbsp;
            </div>
            <uc1:ScenicPic ID="ScenicPic1" runat="server" />
        </div>
        <div>
            &nbsp;
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        var ScenicInfoDetail = {
            QFord_getImageSize: function(FilePath) {
                var imgSize = { width: 0, height: 0 };
                var image = new Image();
                image.src = FilePath;
                imgSize.width = image.width;
                imgSize.height = image.height;
                image = null;
                return imgSize;
            },
            initPage: function() {
                var imgUrl = $("#ScenicInfoPic").attr("src");
                var imgSize = this.QFord_getImageSize(imgUrl);

                if (imgSize.width >= 679 || imgSize.width == 0) {
                    $("#ScenicInfoPic").attr("width", 679);
                } else {
                    $("#ScenicInfoPic").removeAttr("width");
                }
                if (imgSize.height >= 454 || imgSize.height == 0) {
                    $("#ScenicInfoPic").attr("height", 454);
                } else {
                    $("#ScenicInfoPic").removeAttr("height");
                }
            }
        };
        $(function() {
            ScenicInfoDetail.initPage();
        });
    </script>

</asp:Content>
