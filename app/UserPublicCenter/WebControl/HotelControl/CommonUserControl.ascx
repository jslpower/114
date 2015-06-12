<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonUserControl.ascx.cs"
    Inherits="UserPublicCenter.HotelManage.CommonUserControl" %>
<!-- sidebar_2 start-->
<div class="sidebar_2 sidebar_2_ClassPage">
    <p class="more moreClassPage">
        <span>酒店常识</span></p>
    <ul>
        <li><a href="HotelCommon.aspx?CityId=<%=CityId %>#t1" >住店注意事项 </a></li>
        <li><a href="HotelCommon.aspx?CityId=<%=CityId %>#t2" >酒店的部分设置</a></li>
        <li><a href="HotelCommon.aspx?CityId=<%=CityId %>#t3" >酒店基本房型</a></li>
        <li><a href="HotelCommon.aspx?CityId=<%=CityId %>#t4" >酒店星级</a></li>
        <li><a href="HotelCommon.aspx?CityId=<%=CityId %>#t5" >酒店类型</a></li>
    </ul>
</div>
