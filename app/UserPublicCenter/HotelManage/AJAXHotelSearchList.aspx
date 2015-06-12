<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AJAXHotelSearchList.aspx.cs"
    Inherits="UserPublicCenter.HotelManage.AJAXHotelSearchList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:repeater id="rpt_list" runat="server">
                                <ItemTemplate>
                                    <div class="sidebar02_1_content_L">
                                        <div class="bgc">
                                            <div class="imgLAreapic"  style=" margin-right:5px; ">
                                                <div style=" width:145px; height:150px; vertical-align:middle;overflow:hidden;"><%#GetImageByList((System.Collections.Generic.IList<EyouSoft.Model.HotelStructure.HotelImagesInfo>)Eval("HotelImages"))%>
                                                </div>
                                                <div style="width:145px; height:25px;">
                                                酒店星级：<font color="#F68300"><%#GetHotelLevelByEnum((EyouSoft.HotelBI.HotelRankEnum)Eval("rank")) %></font>
                                     </div>
                                     </div>
                                            <div class="imgRArea" >
                                                <span class="font14b"><a href="HotelDetail.aspx?hotelCode=<%#Eval("HotelCode") %>&CityId=<%#this.CityId %>&comeDate=<%=Request.QueryString["inTime"]%>&leaveDate=<%=Request.QueryString["leaveTime"]%>"  target="_blank" >
                                                    <font class="fbb">
                                                        <%#Eval("HotelName")%></font></a></span>
                                                <p>
                                                    <%#EyouSoft.Common.Utils.GetText(Eval("ShortDesc").ToString(),85,true)%><font class="C_red"><a href="HotelDetail.aspx?hotelCode=<%#Eval("HotelCode") %>&CityId=<%#this.CityId %>"
                                                        target="_blank">[查看详情]</a></font><br>
                                                    <img src="<%=ImageServerUrl %>/images/hotel/icon008.gif" /><font class="C_Grb"><%#Eval("District")%></font>
                                                    <img src="<%=ImageServerUrl %>/images/hotel/icon0097.gif" />
                                                    <a href="javascript:void(0);" onclick="OpenMap('<%#Eval("hotelName") %>','<%#((EyouSoft.Model.HotelStructure.HotelPositionInfo)Eval("HotelPosition")).Latitude %>','<%#((EyouSoft.Model.HotelStructure.HotelPositionInfo)Eval("HotelPosition")).Longitude %>')">
                                                        <font class="C_Grb">电子地图</font></a></p>
                                                <ul>
                                                    <li><a class="tabtwo-on" style="width: 76px; height: 23px; text-decoration:none"
                                                        id="two1">前台现付</a></li><li><a   target="_blank"  href="HotelTeamBook.aspx?hotelCode=<%#Eval("HotelCode") %>&comeDate=<%#Request.QueryString["inTime"] %>&leaveDate=<%#Request.QueryString["leavetime"] %>&CityId=<%#this.CityId %>"
                                                            style="width: 76px;" id="two2">团队申请</a></li><div class="clear">
                                                            </div>
                                                </ul>
                                               
                                                <%#GetRoomByHotel((System.Collections.Generic.IList<EyouSoft.Model.HotelStructure.RoomTypeInfo>)Eval("RoomTypeList"), Container.ItemIndex, Eval("HotelCode").ToString())%> 
 
                                            </div>
                                            <!--imgRArea end-->
                                            <div class="clear" style=" clear:both">
                                            </div>
                                        </div>
                                        <!--bgc end-->
                                    </div>
                                    <div class="clear"></div>
                                   <br />
                                    <!--sidebar02_1_content_L end-->
                                </ItemTemplate>
</asp:repeater>
<div class="clear">
</div>
<div class="digg" style="text-align: center" id="div_AjaxPage">
    <cc1:ExporPageInfoSelect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="NewButton"
        CurrencyPageCssClass="RedFnt" />
    <asp:label id="lblMsg" runat="server" text=""></asp:label>
</div>
