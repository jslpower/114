<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserPublicCenter.RouteManage.Default"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/UCLineRight.ascx" TagName="UCLineRight" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/RouteList.ascx" TagName="RouteList" TagPrefix="uc4" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">

    <script src="../DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("news2011") %>" rel="Stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop10">
        <tr>
            <td width="735" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="36" class="sptop">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="79%" style="text-align: right">
                                        实时汇集全国优秀旅游线路，今日<%=thisCityName %>共计<%=SumTodayRoute%>条 全国共计<%=SumAllRoute%>条
                                    </td>
                                    <td width="21%" style="text-align: center">
                                        <a href="/Register/CompanyUserRegister.aspx" target="_blank">现在就加入同业114</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="border-left: 1px solid rgb(226, 229, 222); border-right: 1px solid rgb(226, 229, 222);">
                            <uc4:RouteList ID="RouteList1" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="spb">
                        </td>
                    </tr>
                </table>
                <div class="xuanzexian">
         <div class="sysousuo">
            搜索</div>
           <ul>
               <li>关键字：<input type="text" id="txtKeyword" size="20" style="color:#ccc" value="线路,特色,途径区域" /></li>
               <li>出发城市：<input type="text" id="txtcity" size="12" style="color:#ccc" value="如:杭州" /></li>
               <li>出发时间：<input type="text" size="15" id="txtSDate" onfocus="WdatePicker({onpicked:function(){
            $('#ctl00_ContentPlaceHolder1_txt_RDate').focus();},minDate:'%y-%M-#{%d}'})" >-<input type="text" size="15" id="txtEDate" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtSDate\')}'})" /></li>
            </ul>
            <ul>
               <li>主题：
                    <asp:DropDownList ID="Dpltheme" runat="server">
                    </asp:DropDownList>
               </li>
               <li>天数：
                 <select id="selectdays" name="select2">
                   <option value="0">全部</option>
                   <option value="1">1天</option>
                   <option value="2">2天</option>
                   <option value="3">3天</option>
                   <option value="4">4天</option>
                   <option value="5">5天</option>
                   <option value="6">6天</option>
                   <option value="7">7天</option>
                   <option value="8">7天以上</option>
                 </select>
               </li>
               <li>价位：
                  <select id="selectPrice" name="select3">
                   <option value="0">全部</option>
                   <option value="1">100以下</option>
                   <option value="2">100-300</option>
                   <option value="3">300-1000</option>
                   <option value="4">1000-3000</option>
                   <option value="5">3000-10000</option>
                   <option value="6">10000以上</option>
                 </select>
               </li>
               <li>
               <div style="margin-top: -3px; padding-left: 145px;">
               <img width="85" height="30" id="BtnSearch" align="middle" style="cursor:pointer" alt="搜旅游" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/UserPublicCenter/xianllb_30.jpg" /></div>
               </li>
            </ul>
           
      </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;
                    border: 1px solid #ddd; padding: 1px; text-align: left;">
                    <tr>
                        <td>
                            <div class="dijieshe">
                                <strong>目的地地接社</strong></div>
                            <div class="dijieshecheng">
                                <ul>
                                <%= strAllLocalCity %>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="5" class="boxhover">
                    <%=strAllAdv%>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <style type="text/css">
                #news-list-bar{ width:220px;}
                </style>
                <uc2:UCLineRight ID="UCLineRight1" runat="server" />
            </td>
        </tr>
    </table>

<script type="text/javascript">
     //线路关键字焦点
            $("#txtKeyword").focus(function() {
                if ($(this).val() == "线路,特色,途径区域") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val("线路,特色,途径区域").css("color", "#ccc");
                }
            }); 
            $("#txtcity").focus(function() {
                if ($(this).val() == "如:杭州") {
                    $(this).val("").css("color", "#000");
                }
            }).blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val("如:杭州").css("color", "#ccc");

                }
            }); 
            
            //绑定登录回车
            $("#txtSDate,#txtcity,#txtEDate,#txtKeyword").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    $("#BtnSearch").click();
                    return false;
                }
            });


            $("#BtnSearch").click(function() {
                var Url = "";
                var keyword = $.trim($("#txtKeyword").val()) == "线路,特色,途径区域" ? "" : $.trim($("#txtKeyword").val());
                var city = $.trim($("#txtcity").val()) == "如:杭州" ? "" : $.trim($("#txtcity").val());
                var theme = $.trim($("#<%=Dpltheme.ClientID %>").val());
                var days = $.trim($("#selectdays").val());
                var price = $.trim($("#selectPrice").val());
                var startdate = $.trim($("#txtSDate").val());
                var enddate = $.trim($("#txtEDate").val());
                Url = "/TourManage/TourList.aspx?";
                window.location.href = Url + "KeyWord=" + escape(keyword) + "&Days=" + days + "&ThemeName=" + escape(theme) + "&StartDate=" + startdate + "&EndDate=" + enddate + "&Price=" + price + "&City=" + city + "&SearchType=More";
            });
</script>
</asp:Content>

