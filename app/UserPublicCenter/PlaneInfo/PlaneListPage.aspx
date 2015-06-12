<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlaneListPage.aspx.cs"
    Title="机票" Inherits="UserPublicCenter.PlaneInfo.PlaneListPage" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="boxbanner">
        <%=BannerImgUrl1%></div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="740" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jiucha">
                            <div style="float: left;">
                                <strong>全国机票查询</strong></div>
                            <div style="float: right; font-size: 12px;">
                                返点高 出票快 退款方便！</div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="jichak">

                            <script type="text/javascript" src="<%=JsManage.GetJsFilePath("517ticketcore") %>"></script>

                            <script type="text/javascript">
                                ticketLKE.CityInputConfig.FromCityId = "txtFromCity";
                                ticketLKE.CityInputConfig.ToCityId = "txtToCity";
                                ticketLKE.TimeDateConfig.dateTimeControlId = 'dpTakeOffDate';
                                ticketLKE.TimeDateConfig.EnddateTimeControlId = 'endDateTime';
                            </script>

                            <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="37%" height="35" align="left" class="hei12">
                                        航程类型：
                                        <input type="radio" value="1" id="radVoyageType_1" name="radVoyageType" checked="checked"
                                            onclick="PlaneListPage.isShowEndDate();">
                                        <span class="hei14">单程
                                            <input type="radio" value="2" id="radVoyageType_2" name="radVoyageType" onclick="PlaneListPage.isShowEndDate();">
                                            往返</span>
                                    </td>
                                    <td width="63%" align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="37%" height="32" align="left" class="hei12">
                                        <div style="float: left; margin-top: 5px;">
                                            &nbsp;出发城市：</div>
                                        <div style="background: #FFF; border: 1px solid #ABADB4; padding: 1px; float: left;
                                            margin-right: 10px; width: 160px; height: 20px;">
                                            <div style="float: left; height: 17px;">
                                                <input name="txtFromCity" id="txtFromCity" type="text" class="searchinput"><input
                                                    type="hidden" id="txtFromCityLKE" name="txtFromCityLKE" />
                                            </div>
                                            <div style="float: right; margin: 2px 1px 0 0;">
                                                <a href="javascript:void(0);">
                                                    <img id="fromCity" src="<%=ImageServerPath %>/images/UserPublicCenter/inputico.gif"
                                                        alt="" style="margin-bottom: -3px;" /></a></div>
                                        </div>
                                    </td>
                                    <td width="63%" align="left">
                                        <div style="float: left; margin-top: 5px;">
                                            &nbsp;到达城市：</div>
                                        <div style="background: #FFF; border: 1px solid #ABADB4; padding: 1px; float: left;
                                            margin-right: 10px; width: 160px; height: 20px;">
                                            <div style="float: left; height: 17px;">
                                                <input name="txtToCity" id="txtToCity" type="text" class="searchinput" /><input type="hidden"
                                                    id="txtToCityLKE" name="txtToCityLKE" />
                                            </div>
                                            <div style="float: right; margin: 2px 1px 0 0;">
                                                <a href="javascript:void(0);">
                                                    <img id="toCity" src="<%=ImageServerPath %>/images/UserPublicCenter/inputico.gif"
                                                        style="margin-bottom: -3px;" /></a></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="60" align="left">
                                        <div style="float: left; margin-top: 5px;">
                                            &nbsp;出发日期：</div>
                                        <div style="background: #FFF; border: 1px solid #ABADB4; padding: 1px; float: left;
                                            margin-right: 10px; width: 160px; height: 20px;">
                                            <div style="float: left; height: 17px;">
                                                <input type="text" name="dpTakeOffDate" id="dpTakeOffDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})"
                                                    style="border: 0px; font-size: 14px;" size="16" />
                                            </div>
                                            <div style="float: right; margin: 2px 1px 0 0;">
                                                <img onclick="javascript:$('#dpTakeOffDate').focus();" src="<%=ImageServerPath %>/images/time1.gif" /></div>
                                        </div>
                                    </td>
                                    <td id="divEndDate" height="70" align="left">
                                        <div style="float: left; margin-top: 5px;">
                                            &nbsp;返回日期：</div>
                                        <div style="background: #FFF; border: 1px solid #ABADB4; padding: 1px; float: left;
                                            margin-right: 10px; width: 160px; height: 20px;">
                                            <div style="float: left; height: 17px;">
                                                <input type="text" name="endDateTime" id="endDateTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})"
                                                    style="border: 0px; font-size: 14px;" size="16" />
                                            </div>
                                            <div style="float: right; margin: 2px 1px 0 0;">
                                                <img onclick="javascript:$('#endDateTime').focus();" src="<%=ImageServerPath %>/images/time1.gif" /></div>
                                        </div>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <div id="divHotCitys">
                                            <ul id="ulHotCitys">
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="35" align="left">
                                        <img style="cursor: pointer" src="<%=ImageServerPath %>/images/UserPublicCenter/jiupiaosousuo.gif"
                                            onclick="PlaneListPage.onClickTitck();" width="117" height="24" />
                                    </td>
                                    <td height="35" align="left" class="lanse">
                                        <a target="_blank" href="/HelpCenter/help/TicketHelp.html">帮助</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="jiuchaxbg">
                            实时连接20家航空公司数据库，全国机票一键购买！
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jdrh">
                            <div style="float: left">
                                合作供应商</div>
                            <div style="float: right; font-size: 12px; color: #333; padding-right: 10px;">
                                <a href="PlaneNewsList.aspx?TypeID=2&CityId=<%=CityId %>" class="hui12" target="_blank">
                                    更多>></a></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="jipiaonei">
                            <asp:DataList ID="dal_PlaneAgu" runat="server" BorderWidth="0px" CellPadding="0"
                                CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="1"
                                RepeatDirection="Horizontal" Width="98%">
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" align="left">
                                                <ul>
                                                    <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString(),2)%>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="boxbanner">
        <%=BannerImgUrl2%></div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="740" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jiucha">
                            <div style="float: left;">
                                <strong>运价参考</strong></div>
                            <div style="float: right; font-size: 12px; padding-right: 10px;">
                                <a href="PlaneNewsList.aspx?TypeID=0&CityId=<%=CityId %>" class="hui12" target="_blank">
                                    更多>></a></div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="jipiaokk">
                            <asp:DataList ID="dl_TicketbookList" runat="server" BorderWidth="0px" CellPadding="0"
                                CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="2"
                                RepeatDirection="Horizontal" Width="98%">
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" align="left" class="jipiaoxx">
                                                <ul>
                                                    <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString(),0)%>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jdrh">
                            <div style="float: left">
                                帮助信息</div>
                            <div style="float: right; font-size: 12px; color: #333; padding-right: 10px;">
                                <a href="PlaneNewsList.aspx?TypeID=1&CityId=<%=CityId %>" class="hui12" target="_blank">
                                    更多>></a></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="jipiaonei">
                            <asp:DataList ID="dal_BookTichetList" runat="server" BorderWidth="0px" CellPadding="0"
                                CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="1"
                                RepeatDirection="Horizontal" Width="98%">
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" align="left">
                                                <ul>
                                                    <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString(),1)%>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("toplist") %>"></script>

    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("517autocomplete") %>" />

    <script type="text/javascript">
           var PlaneListPage = {          
            onClickTitck:function(){
                ticketLKE.CenterPost("<% = IsLogin %>","<% = CityId %>");
                return false;
            },
            isShowEndDate:function(){
                var  radVoyageType= $("input[name='radVoyageType']:checked").val();
                if(radVoyageType==2){
                    $("#divEndDate").show();
                }else{
                     $("#divEndDate").hide();
                }
            }
        }                
        $(function() {
            /*机票 BEGIN */
            PlaneListPage.isShowEndDate();

            ticketLKE.initAutoComplete();
            ticketLKE.initHotCitys();

            ticketLKE.stringPort="<% = EyouSoft.Common.Domain.UserPublicCenter %>";
            
            $("#fromCity").click(function() {
                ticketLKE.showHotCitys(1);
                ticketLKE.hiddenDiv();
            });

            $("#toCity").click(function() {
                ticketLKE.showHotCitys(2);
                ticketLKE.hiddenDiv();
            });
            /*机票 END */            
            
        });
        
    </script>

</asp:Content>
