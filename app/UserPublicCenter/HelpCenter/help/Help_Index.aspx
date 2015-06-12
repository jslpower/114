<%@ Page Title="帮助中心" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Help_Index.aspx.cs" Inherits="UserPublicCenter.HelpCenter.help.Help_Index" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
      <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />

    <div style="height: 65px; margin: 0 auto; min-width: 1004px; width: 100%">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 3px solid #EB6F00;">
            <tr>
                <td width="130">
                    <a href="/Default.aspx?CityId=<%=CityId %>">
                        <img src="<%=UnionLogo %>" alt="同业114" border="0" /></a>
                </td>
                <td width="547" align="left">
                    <b><a href="main.html" target="mainFrame">
                        <img src="../images/tel.gif" width="345" height="42" /></a></b>
                </td>
                <td width="90px">
                    <a href="http://im.tongye114.com" title="同业MQ免费下载" target="showframe">
                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/mqgif.gif" alt="同业MQ免费下载" width="78" height="59" border="0" /></a>
                </td>
            </tr>
        </table>
    </div>
    <div style=" clear:both"></div>
    <table width="100%" border="0px" height="100%">
        <tr>
            <td align="left" width="200px">
                <iframe src="menu.htm" id="leftFrame" name="leftFrame" title="leftFrame" style="border:none;"
                    height="515px" width="200px" scrolling="no"  frameborder="0"></iframe>
            </td>
            <td align="left" width="100%" valign="top">
                <iframe src="main.html" id="mainFrame" name="mainFrame" title="mainFrame" style="border: none;
                    width: 100%;" scrolling="auto" height="502px"  frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function() {
            var htmlbody = document.documentElement || document.body;
            htmlbody.style.overflow = "hidden";

            var l = getLocaton();

            if (l != "") {
                $("#mainFrame").attr("src", l);
            }
        })
        function removeHash(hashValue){
            if (hashValue == null || hashValue == undefined)
             return null;
          else if (hashValue == "")
             return "";
          else if (hashValue.length == 1 && hashValue.charAt(0) == "#")
             return "";
          else if (hashValue.length > 1 && hashValue.charAt(0) == "#")
             return hashValue.substring(1);
          else
             return hashValue;  
        }
        function getLocaton(){
            var currentLocation = removeHash(window.location.hash);
            return currentLocation;
        }
    </script>
</asp:Content>
