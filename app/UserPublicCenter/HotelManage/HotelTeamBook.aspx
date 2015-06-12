<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" AutoEventWireup="true" CodeBehind="HotelTeamBook.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelTeamBook" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/HotelControl/ImgFristControl.ascx" TagName="ImgFrist" TagPrefix="uc6" %>
<%@ Register Src="~/WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUser" TagPrefix="uc5" %>
<%@ Register Src="~/WebControl/HotelControl/HotelSearchControl.ascx" TagName="HotelSearch" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
<link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
<div class="main">
    	<img class="add01" src="<%=ImageServerUrl%>/Images/hotel/hotel_add01.gif" />
    	   <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
        <!--content start-->
        <div class="content">
       		<!--sidebar start-->
        	<div class="sidebar sidebarSearch">
              <!--sidebar_1-->
           <uc2:HotelSearch ID="HotelSearch1" runat="server" />
              <!--sidebar_1 end-->
              <uc6:ImgFrist ID="ImgFrist1" runat="server" />
             <!-- sidebar_2 start-->
                <uc5:CommonUser ID="CommonUser1" runat="server" />
             
              <!-- sidebar_2 end-->
              
              
              <!-- sidebar_2 end-->
            </div>
           <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
            	<div class="sidebar02_1">
                    <p class="xuanzhe"><span>填写 <font color="#D41B1B"><%= hotelName %></font> 团队预订单 </span><img src="<%=ImageServerUrl%>/Images/hotel/liucheng2.gif" width="215" height="22" /></p>
                  
                   <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        	
                      <div class="yd_jiange">入住日期：<%=comeDate %>日&nbsp;离店日期：<%=leaveDate %>日    共 <span class="red strong"><%= days %></span> 天    付款方式：<span class="green">前台现付</span>
                    </div>  

	 
	 <div class="yuding"><h1>团队预订单</h1>
	 <div class="clear"></div>
<div class="biaoge2" id="htb_divOrder">
<form id="form1" action="">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th width="17%" align="right" class="pandl" >房型要求：</th>
    <td width="83%" align="left" class="pandl">
    <input type="text" id="htb_selRoomType" class="inputhui" name="htb_selRoomType"/>
      </td>
  </tr>
  <tr>
    <th align="right" class="pandl" ><font style=" color:Red;">*</font>&nbsp;房间数量：</th>
    <td align="left" class="pandl">
     <input name="htb_txtRoomNum" type="text" id="htb_txtRoomNum" class="inputhui" size="20" />
      间 <font color="red">请填写大于10间的房间数</font></td>
  </tr>
  <tr>
    <th align="right" class="pandl" ><font style=" color:Red;">*</font>&nbsp;人数：</th>
    <td align="left" class="pandl"><input name="htb_txtPCount" id="htb_txtPCount" type="text" class="inputhui" size="20" /></td>
  </tr>
  <tr>
    <th align="right" class="pandl" ><font style=" color:Red;">*</font>&nbsp;团房预算：</th>
    <td align="left" class="pandl">
    <input name="htb_txtBudgetMin" id="htb_txtBudgetMin" type="text" class="inputhui" size="10" /> 
      - 
    <input name="htb_txtBudgetMax" id="htb_txtBudgetMax" type="text" class="inputhui" size="10" /></td>
  </tr>
  <tr>
    <th align="right" class="pandl" >宾客类型：</th>
    <td align="left" class="pandl">
      <select name="htb_selPType" id="htb_selPType">
      <option value="0" selected="selected">内宾</option>
      <option value="1">外宾</option>
    </select></td>
  </tr>
  <tr>
    <th align="right" class="pandl" >团队类型：</th>
    <td align="left" class="pandl"><select name="htb_selTourType" id="htb_selTourType">
      <option selected="selected" value="1">会议团</option>
      <option value="2">旅游团</option>
      <option value="3">其他</option>
    </select></td>
  </tr>
  <tr>
    <th align="right" class="pandl" >其他要求：</th>
    <td align="left" class="pandl">
         <label>
         <textarea name="htb_txtRemark" id="htb_txtRemark" cols="80" rows="10"></textarea>
        </label>
     </td>
  </tr>
</table>
<ul class="btn02"><li><img src="<%=ImageServerUrl%>/Images/hotel/tijiaodidanbtn.gif" border="0"  style="cursor:pointer"  onclick="return HotelTeamBook.save();"/></li>
            <li><a href="#"><img src="<%=ImageServerUrl%>/Images/hotel/congxintianxie.gif" onclick="return HotelTeamBook.clear(this);" /></a></li>
            <li style="width:90px; height:35px; line-height:35px; display:none;"  id="hb_mess">正在提交订单…</li>
            <div class="clear"></div></ul>
</form>
</div>

	 </div>
	 
          </div><!--sidebar02SearchC end-->
              </div>
                    </div>
            </div>
          <!--sidebar02 end-->
        </div>

<script type="text/javascript">
    var HotelTeamBook = {
        isFirst: true, //第一次提交表单
        //保存订单
        save: function() {
        //验证表单

        var mess = "";
            if(<%=days%><=0)
            mess+="离店日期必须大于入店日期\n"
            var roomNum = $("#htb_txtRoomNum").val(); //房间数
            if (!(/^[1-9]\d+$/).test(roomNum)||roomNum=="10") {
                
                mess += "请填写正确房间数\n";
            }
            else if (parseInt(roomNum) > 1000)
                mess += "房间数不能超过1000\n";
            var pCount = $("#htb_txtPCount").val(); //人数
            if (!(/^[1-9]\d*$/).test(pCount))
                mess += "请填写正确人数\n";
            else if (parseInt(pCount) > 2000)
                mess += "人数不能超过2000\n";
            var bMin = $("#htb_txtBudgetMin").val(); //团房预算最小值
            var bMax = $("#htb_txtBudgetMax").val(); //团房预算最大值

            if (!(/^[1-9]\d*\.*\d*$/).test(bMin))//最小预算
                if(bMin!="0")
                mess += "团房预算最小值请填写数字\n";
            if (!(/^[1-9]\d*\.*\d*$/).test(bMax))
                if(bMax!="0")
                mess += "团房预算最大值请填写数字\n"; //最大预算
            if (mess != "") {
                alert(mess); //输出验证
                return false;
            }
            //如果是第一次则让其提交表单
            if (HotelTeamBook.isFirst) {
                $("#hb_mess").show();
                HotelTeamBook.isFirst = false; //设置已提交
                //提交订单
                $.ajax(
              {
                  url: "/HotelManage/HotelTeamBook.aspx?hotelCode=<%=hotelCode %>",
                  data: $("#htb_divOrder").find("*").serialize() + "&method=save",
                  dataType: "json",
                  cache: false,
                  type: "post",
                  success: function(result) {
                      $("#hb_mess").hide();
                      HotelTeamBook.isFirst = true; //恢复第一次提交
                      if (result.success == "1") {
                          alert("团队预订成功！我们将在20分钟内回复您！");
                          window.location = "/HotelManage/HotelDetail.aspx?hotelCode=<%=hotelCode %>&comeDate=<%=comeDate %>&leaveDate=<%=leaveDate %>&cityId=<%=cityId %>";
                      }
                      else {
                          alert("团队预订失败！");
                      }
                  },
                  error: function() {
                      $("#hb_mess").hide();
                      alert("团队预订失败！");
                      HotelTeamBook.isFirst = true; //恢复第一次提交
                  }
              });
            }
            return false;
        },
        //重新填写
        clear: function(tar) {
            $("#htb_txtRoomNum").val("");
            $("#htb_txtPCount").val("");
            $("#htb_txtBudgetMin").val("");
            $("#htb_txtBudgetMax").val("");
            $("#htb_selTourType").val("1");
            $("#htb_selPType").val("0");
            $("#htb_selRoomType").val("0");
            $("#htb_txtRemark").val("");
            return false;
        }
    }
</script>
</asp:Content>
