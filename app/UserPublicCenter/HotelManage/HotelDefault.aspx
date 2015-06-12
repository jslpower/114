<%@ Page Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HotelDefault.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelDefault" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="HotelRightControl.ascx" TagName="HotelRightControl" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc3" %>

<%@ Register src="NewAttrHotelControl.ascx" tagname="NewAttrHotelControl" tagprefix="uc4" %>
<%@ Register src="DiscountControl.ascx" tagname="DiscountControl" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("517autocomplete") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="boxbanner">
        <asp:Literal ID="ltlImgBoxBanner" runat="server"></asp:Literal>
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="740" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" height="240px">
                    <tr>
                        <td class="jiucha">
                            <strong>酒店搜索</strong>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="jiuchak">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="9%" class="hei14" height="35">
                                        <strong>城市</strong>
                                    </td>
                                    <td width="28%" align="left">
                                        <input id="txtFromCity" type="text" style="border: solid 1px #abadb4; line-height: 19px;
                                            font-size: 14px; width: 150px; height:21px; "  value="杭州" runat="server"  />
                                        <input type="hidden" id="txtFromCityLKE" name="txtFromCityLKE" />
                                    </td>
                                    <td width="8%" class="hei14" align="right">
                                        <strong>关键词</strong>&nbsp;
                                    </td>
                                    <td width="33%">
                                        <input name="txtSearchVal" id="txtSearchVal" type="text" class="jiudianshu" style="border: solid 1px #abadb4;
                                            line-height: 19px; font-size: 12px; color: #CCC; width: 150px;height:21px;" value="输入酒店名称、品牌或地址"
                                             runat="server" />
                                    </td>
                                    <td width="22%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="35">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/jiusousuo.gif" width="69"
                                            style="cursor: pointer;" height="21" id="ImgBtn" />
                                    </td>
                                    <td class="hei14" align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15">
                                <tr>
                                    <td align="left" class="danhui">
                                        <span class="huise">按<strong>星级</strong>：</span> <a id="a0" href="<%=Utils.GeneratePublicCenterUrl("/HotelManage/HotelDefault.aspx?level=0",this.CityId) %>">
                                            <span class="lanse"></span>三星以下</a>&nbsp; <a id="a2" href="<%=Utils.GeneratePublicCenterUrl("/HotelManage/HotelDefault.aspx?level=2",this.CityId) %>">
                                                <span class="lanse"></span>三星或同级</a>&nbsp; <a id="a4" href="<%=Utils.GeneratePublicCenterUrl("/HotelManage/HotelDefault.aspx?level=4",this.CityId) %>">
                                                    <span class="lanse"></span>四星或同级</a>&nbsp; <a id="a5" href="<%=Utils.GeneratePublicCenterUrl("/HotelManage/HotelDefault.aspx?level=5",this.CityId) %>">
                                                        <span class="lanse"></span>五星或同级</a>&nbsp;
                                        <br />
                                        <input type="hidden" runat="server"  id="hideStar"/>
                                        <span class="huise"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="jiuchaxbg">
                            同业114覆盖372个城市，<asp:Label ID="lblHotelCount" runat="server" Text=""></asp:Label>
                            家酒店信息。
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
                            本周最热酒店
                        </td>
                    </tr>
                    <tr>
                        <td class="jdrk">
                        
                            <uc4:NewAttrHotelControl ID="NewAttrHotelControl1" runat="server" />
                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="boxbanner">
        <asp:Literal ID="litImgBoxBannerSecond" runat="server"></asp:Literal>
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <div class="goulefths" style="height: 24px;">
                    <strong>酒店列表</strong></div>
                <asp:Repeater ID="rptList" runat="server" OnItemCreated="rptList_ItemCreated">
                    <ItemTemplate>
                        <div class="gouwucp">
                            <div class="gwcptu"><a  target="_blank"  href="<%#EyouSoft.Common.Utils.GetCompanyDomain(Eval("id").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.酒店,this.CityId) %>"><img src="<%#Utils.GetNewImgUrl(Eval("CompanyImgThumb").ToString(),3)%>" border="0" width="92px" height="85px" /></a></div>
                            <div class="gouwunei">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="table-layout: fixed">
                                    <tr>
                                        <td width="49%">
                                            <strong><span class="lan14"><a title="<%#Eval("CompanyName")%>"  target="_blank"  href="<%#EyouSoft.Common.Utils.GetCompanyDomain(Eval("id").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.酒店,this.CityId) %>">
                                                 <%#Utils.GetText(Eval("CompanyName").ToString(),15,true) %>
                                               
                                                </a></span></strong>
                                        </td>
                                        <td width="49%" class="chengse">
                                            <asp:Panel ID="pnlLevel" runat="server">
                                                星级：<%#HotelLevel(Convert.ToInt32(Eval("CompanyLevel")))%></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlCompanyBrand" runat="server">
                                                酒店品牌：<span class="hui12"><%#Eval("CompanyBrand")%>&nbsp;&nbsp;</span></asp:Panel>
                                        </td>
                                        <td>
                                             <asp:Panel runat="server" ID="pnlCompanyTag" >
                                                周边环境：<a><span class="lanse"> 
                                                    <asp:Label ID="lblTag" runat="server" Text=""></asp:Label> </span></a></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="line-height: 140%; word-wrap: break-word; width: 600px;">
                                            酒店介绍：<span class="hui12">
                                                <%#EyouSoft.Common.Utils.GetText(Utils.InputText(Eval("Remark").ToString()), 100, true)%>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="line-height: 140%;">
                                            <asp:Panel ID="pnlContact" runat="server">
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style=" clear:both;"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="digg" style="text-align: center;">
                                <cc2:ExporPageInfoSelect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="NewButton"
                                    CurrencyPageCssClass="RedFnt"  />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="gwright">
                            <div class="gwrhang">
                                特价房
                            </div>
                           <uc5:DiscountControl ID="DiscountControl2" runat="server" />
                        </td>
                    </tr>
                </table>
                <uc2:HotelRightControl ID="HotelRightControl1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("HotelCity") %>"></script>

    <script type="text/javascript">
        $(function() {
            ticketLKE.initAutoComplete();
            $("#ImgBtn").click(function() {
                if ($("#<%=txtSearchVal.ClientID %>").val() == "输入酒店名称、品牌或地址") {
                    $("#<%=txtSearchVal.ClientID %>").val("");
                }
                var params = { cityName: encodeURIComponent($("#<%=txtFromCity.ClientID%>").val()), searchVal: encodeURIComponent($("#<%=txtSearchVal.ClientID %>").val()),CityId:<%=this.CityId %> };
                var str = $.param(params);
                window.location.href = "/HotelManage/HotelDefault.aspx?" + str;
            });

            $("#<%=txtFromCity.ClientID%>").focus(function() {
                $(this).css("color", "black");
                if ($(this).val() == "杭州") {
                }
            });

            $("#<%=txtSearchVal.ClientID %>").focus(function() {
                $(this).css("color", "black");
                if ($(this).val() == "输入酒店名称、品牌或地址") {

                    $(this).val("");
                }
            });

            $(".jiudianshu").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#ImgBtn").click();
                    return false;
                }
            });
            $("#a" + $("#<%=hideStar.ClientID%>").val()).css("color", "red");
        });
       
    </script>



    <script type="text/javascript">
        var searchFromValue = { lke: '' }; var searchToValue = { city: '', lke: '' };
        var ticketLKE = {
            cacheData: new Array(),
            initAutoComplete: function() {
                $("#<%=txtFromCity.ClientID%>").autocomplete(ticketLKEdata, {
                    minChars: 1,
                    width: 125,
                    matchContains: "text",
                    autoFill: true,
                    formatItem: function(row, i, max) {
                        return row.cName;
                    },
                    formatMatch: function(row, i, max) {
                        return row.shortName + "," + row.eName + "," + row.cName + "," + row.lke + "|" + row.lke;
                    },
                    formatResult: function(row) {
                        return row.cName;
                    },
                    formatHidResult: function(row) {
                        return { hidInputId: "txtFromCityLKE", value: row.lke };
                    }
                });
                $("#<%=txtFromCity.ClientID%>").bind("blur", function() { ticketLKE.setDefaultValue("<%=txtFromCity.ClientID%>"); });
                $("#<%=txtFromCity.ClientID%>").val(searchFromValue.city);
            },

            webFormSubmit: function() {
                var fromValue = { city: $.trim($("#<%=txtFromCity.ClientID%>").val()), lke: $("#txtFromCityLKE").val() };

                var isFrom = false;
                var isTo = false;
                if (fromValue.lke == "") { alert("请输入城市"); return false; }


                for (var i = 0; i < ticketLKEdata.length; i++) {
                    if (ticketLKEdata[i].lke == fromValue.lke) {
                        if (ticketLKEdata[i].cName == fromValue.city) { isFrom = true; }
                    }
                }

                if (!isFrom) { alert("请输入正确的城市"); return false; }


                return true;
            },
            setDefaultValue: function(elementId) {
                var obj = $("#" + elementId);
                var objV = obj.val().toLowerCase();
                if ($.trim(objV).length < 1) {
                    obj.next().val('');
                    return;
                }
                if (this.cacheData.length > 0) {
                    var arr1 = this.cacheData[0].split('|')

                    if (arr1.length != 2) { return; }

                    var arr2 = arr1[0].toLowerCase().split(",");

                    if (arr2.length != 4) { return; }

                    var isMatch = false;
                    for (var i = 0; i < arr2.length; i++) {
                        if (arr2[i].indexOf(objV) == 0) {
                            isMatch = true;
                            break;
                        }
                    }
                    if (isMatch) {
                        var obj = $("#" + elementId);
                        obj.val(arr2[2]);
                        obj.next().val(arr1[1]);
                    }
                    this.cacheData = new Array();
                }
            }

        }
    </script>

</asp:Content>
