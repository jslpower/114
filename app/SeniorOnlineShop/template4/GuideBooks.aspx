<%@ Page Language="C#" MasterPageFile="~/master/T4.Master" AutoEventWireup="true"
    CodeBehind="GuideBooks.aspx.cs" Inherits="SeniorOnlineShop.template4.GuideBooks" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server" ID="Content1">
    <div class="linetj">
        <div class="linetjtk">
            <%--搜索框开始--%>
            <div class="linetjth">
                出游指南
                <table width="220" border="0" align="right" cellpadding="0" cellspacing="0" style="margin-top: -7px;*margin-top: -22px;">
                    <tr>
                        <td width="150" align="right">
                            <input  type="text" value="请输入关键字" id="txtKeyWord" size="25" />
                        </td>
                        <td width="70" align="center">
                            <input type="button" id="Search" value="搜索" />
                        </td>
                    </tr>
                </table>
            </div>
            <%--搜索框结束--%>
            <%--主体内容开始--%>
            <div class="linetjxx">
                <table width="660" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="330" align="left">
                            <%--风土人情开始--%>
                            <table width="660" border="0" cellspacing="0" cellpadding="0" style="margin: 10px;">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="99" align="left" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">风土人情介绍</font></strong>
                                                </td>
                                                <td width="477" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="44" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a target="_blank" href="/GuideBookList_<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情 %>_<%= cMaster.CompanyId %>">更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="90px">
                                        <div class="fengtu">
		                                <ul>
                                        <asp:DataList runat="server" ID="LocalCultures" RepeatColumns="3">
                                            <ItemTemplate>
                                                <li>
                                                <table width="210" border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 5px;">
                                                    <tr>
                                                        <td width="44%" rowspan="2">
                                                            <a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>'>
                                                                <img src="<%# Utils.GetLineShopImgPath(Eval("ImagePath").ToString(),4) %>" width="79"
                                                                    height="67" border="0" class="borderhui" /></a>
                                                        </td>
                                                        <td width="56%" height="24" style="padding-left: 3px;">
                                                            <a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>' class="zhinanbt">
                                                                <%# Utils.GetText2(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Title").ToString()),8,false)%></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="padding-left: 3px; line-height: 16px;">
                                                            <a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>' class="zhinanbt">
                                                            <font color="#666666">
                                                                <%# Utils.GetText2(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("ContentText").ToString()), 25, true)%></font>
                                                           </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </li>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        </ul>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <%--风土人情结束--%>
                            
                            <%--温馨提醒开始--%>
                            <table width="660" border="0" cellspacing="0" cellpadding="0" style="margin: 10px;">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="65" align="left" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">温馨提醒</font></strong>
                                                </td>
                                                <td width="511" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="44" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a target="_blank" href="/GuideBookList_<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒 %>_<%= cMaster.CompanyId %>">更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="90">
                                        <div class="tixing">
                                            <div class="hd">
                                                <div class="media">
                                                    <asp:Literal runat="server" ID="ltrTopOneWarm"></asp:Literal>
                                                </div>
                                                <div class="info">
                                                    <h6><asp:Label runat="server" ID="lbTopOneTitle"></asp:Label></h6>
                                                    <div class="con">
                                                        <asp:Label runat="server" ID="lbTopOneContent"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="introlist">
                                                <ul>
                                                    <asp:Repeater runat="server" ID="rpWarmInfos">
                                                        <ItemTemplate>
                                                            <li><a href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>'>·<%# Utils.GetText2(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Title").ToString()),38,true)%></a></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <%--温馨提醒开始--%>
                            
                            <%--综合介绍开始--%>
                            <table width="660" border="0" cellspacing="0" cellpadding="0" style="margin: 10px;">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="65" align="left" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">综合介绍</font></strong>
                                                </td>
                                                <td width="511" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="44" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a target="_blank" href="/GuideBookList_<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍 %>_<%= cMaster.CompanyId %>">更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="introlist">
                                            <ul>
                                                <asp:Repeater runat="server" ID="CompositeReferral">
                                                    <ItemTemplate>
                                                        <li><a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>'>·<%# Utils.GetText2(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Title").ToString()),38,true)%></a></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <%--综合介绍结束--%>
                            
                            <%--旅游资源推荐开始--%>
                            <table width="660" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;
                                margin-bottom: 20px;">
                                <tr>
                                    <td height="30" align="center" class="zhinantt">
                                        <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="84" align="left" bgcolor="#FFFFFF">
                                                    <strong><font color="#356902">旅游资源推荐</font></strong>
                                                </td>
                                                <td width="492" align="center">
                                                    &nbsp;
                                                </td>
                                                <td width="44" align="center" bgcolor="#FFFFFF" class="borderlv">
                                                    <a target="_blank" href="/template4/GuideBookList.aspx?t=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.旅游资源推荐 %>&cid=<%= cMaster.CompanyId %>">更多</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10;">
                                        <asp:DataList runat="server" ID="HotResource" RepeatColumns="5" Width="100%">
                                            <ItemTemplate>
                                                <table width="120" border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 5px;
                                                    margin-top: 10px;">
                                                    <tr>
                                                        <td width="44%" height="24" align="center">
                                                            <a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>'>
                                                                <img src="<%# Utils.GetLineShopImgPath(Eval("ImagePath").ToString(),4) %>" width="113"
                                                                    height="82" border="0" class="borderhui" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="24" align="center">
                                                            <a target="_blank" href='<%# string.Format("/GuideBookInfo_{1}_{0}",cMaster.CompanyId,Eval("Id")) %>' class="zhinanbt">
                                                                <%# Utils.GetText2(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Title").ToString()),8,false)%></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <%--旅游资源推荐结束--%>
                        </td>
                    </tr>
                </table>
            </div>
            <%--主题内容结束--%>
        </div>
    </div>
    
    <script type="text/javascript">
    var newsList = {
        search:function(){
            var k = $("#txtKeyWord").val()=="请输入关键字"?"":$("#txtKeyWord").val();
            var cid="<%= cMaster.CompanyId %>";
            window.location.href="/template4/GuideBookList.aspx?"+$.param({k:k,cid:cid});
            return false;
        }
    };
    $(function(){
        $("#Search").click(function(){
            return newsList.search();
        });
        $("#txtKeyWord").blur(function(){
            $(this).val($.trim($(this).val())==""?"请输入关键字":$(this).val());
        });
        $("#txtKeyWord").focus(function(){
            $(this).val($.trim($(this).val())=="请输入关键字"?"":$(this).val());
        });
        $("#txtKeyWord").bind("keypress", function(e) {
            if (e.keyCode == 13) {
            $("#Search").click(); 
            return false;
            }
        });
    });
    </script>
</asp:Content>
