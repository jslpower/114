<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AboutUsLeftControl.ascx.cs" Inherits="UserPublicCenter.AboutUsManage.AboutUsLeftControl" %>
<%@ Import Namespace="EyouSoft.Common" %>
 <table width="142" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <a href="/AboutUs/" class="companyun" runat="server" id="a1">关于我们</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/CustomerCenter/" class="companyun" runat="server" id="a2">客服中心</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/Services/" class="companyun" runat="server" id="a3">服务说明 </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/cooperation/" class="companyun" runat="server" id="a4">代理合作</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/hr/" class="companyun" runat="server" id="a5">招聘英才</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="~/HelpCenter/help/Help_Index.aspx" target="_blank" class="companyun" runat="server" id="a6">帮助中心</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/suggust/" class="companyun" runat="server" id="a7">提建议</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/Sitemap/" class="companyun" runat="server" id="a8">网站地图</a>
                        </td>
                    </tr>
</table>
<script type="text/javascript">
    $(function() {
    if ($.browser.msie) {
        if (parseFloat($.browser.version) <= 6) {
            try {
                document.execCommand('BackgroundImageCache', false, true);
            } catch (e) {
            }
        }
    }
     })
</script>