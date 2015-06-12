<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryOrder.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.HistoryOrder"  MasterPageFile="~/MasterPage/AirTicket.Master"%>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="c1" ID="cntOrderManage" runat="server">
    <style type="text/css">
/*FENYE*/
DIV.digg {	PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; margin-top:10px; PADDING-TOP: 3px; TEXT-ALIGN: center
}
DIV.digg A {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #54A11C; PADDING-TOP: 2px;TEXT-DECORATION: none
}
DIV.digg A:hover {	BORDER: #54A11C 1px solid; background:#54A11C; COLOR: #fff;}
DIV.digg A:active {	BORDER: #54A11C 1px solid;  COLOR: #000; }
DIV.digg SPAN.current {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 2px; BACKGROUND-COLOR: #54A11C}
DIV.digg SPAN.disabled {	BORDER: #eee 1px solid; PADDING-RIGHT: 5px;  PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #ddd; PADDING-TOP: 2px;}/*end*/
    #txtContent
    {
        width: 140px;
    }
</style>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>    
    <div class="sidebar02_con">
	<!--sidebar02_con_table01 start-->
   	  <div class="sidebar02_con_table01">

      	<table  width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA" style="border:1px #CCCCCC solid;">
  <tbody><tr>
            <td colspan="3" align="left" height="5"><%--<a 
                href="/AirTickets/history/ExcelInput.aspx">导入数据</a>--%>
          </tr>
              <tr>
                <td height="30" colspan="5" align="left"id="tdSelct1"><strong>选择查询类型：</strong>
                    <input name="rdoSelectType" id="rdoPnr" value="1" checked="checked" type="radio"/>
                    <strong><label for="rdoPnr">PNR记录编号</label> </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input name="rdoSelectType" id="rdoPassengerName" value="2" type="radio"/>
                    <strong><label for="rdoPassengerName">乘客姓名</label></strong>
                </td>
                   <td rowspan="3" width="24%" align="left"><font color="#FF0000"> 备注：<%=NowDate %>前查询订单请点历史订单查询   操作请联系客服：廖小姐  联系电话：0571-56893761 MQ:27440 紧急联系电话:15356126700</font></td>
              </tr>
              <tr>
                <td width="12%" height="40" align="left"><b><label id="lblSelectText">PNR编号:</label></b></td>
                <td width="20%" align="left"><input name="txtContent" id="txtContent" type="text" /></td>
                <td width="15%" align="center" height="30"><strong>选择日期：</strong></td>
                <td width="30%" align="left"><input name="DatePicker1" id="DatePicker1" type="text" onfocus="WdatePicker()"/>
                    <img alt="" style="position: relative; left: -24px; top: 3px;" src="<%=ImageServerPath %>/images/jipiao/time.gif" width="16" height="13" onclick="javascript:$('#DatePicker1').focus()" />
                </td>             
              </tr>
              <tr>
                <td colspan="5" height="10" align="center">
                    <img alt="历史订单查询" style=" cursor:pointer" id="imgSelect" src="<%=ImageServerPath %>/images/jipiao/btnistory.jpg"/>
                </td>
              </tr>
                <tr>
                <td height="10" colspan="5"></td>
              </tr>
  	    </tbody></table>
      </div>
      <!--sidebar02_con_table01 end-->
      <!--sidebar02_con_table02 start-->
      <div class="sidebar02_con_table02" id="divhistoryList">      		
      </div>
      <!--sidebar02_con_table02 end-->
       </div>       
    <!-- sidebar02_con end-->
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>  
<script type="text/javascript">
var parms={TypeName:"",Content:"",Date:"",Page:1};
HistoryOrder={
     GetOrderList:function(){  //获取列表
        LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
        LoadingImg.ShowLoading("divhistoryList");
        if (LoadingImg.IsLoadAddDataToDiv("divhistoryList")) {
           $.ajax({
                type: "GET",
                dataType: 'html',
                url: "/AirTickets/OrderManage/AjaxHistoryOrder.aspx",
                data:parms,
                cache: false,
                success:function(html)
                {
                    $("#divhistoryList").html(html);
                },
                error:function()
                {
                    $("#divhistoryList").html("未能成功获取响应结果");
                }
           });
        }
    },
    LoadData: function(obj) {//分页
        var Page = exporpage.getgotopage(obj);
        parms.Page = Page;
        this.GetOrderList();
    }
}
$(document).ready(function() {
    HistoryOrder.GetOrderList();           //获取列表信息
    $("#tdSelct1").find("input[type='radio'][name='rdoSelectType']").each(function() {
        $(this).bind("click", function() {
            var type = $(this).val();
            $("#txtContent").val("");
            if (type == 2) {         //乘客姓名
                $("#lblSelectText").html("乘客姓名：");
            } else {               //prn查询
                $("#lblSelectText").html("PNR编号：");
            }
        });
    });
    $("#imgSelect").click(function() {    //根据日期 查询
        //查询
        var orderdate = $("#DatePicker1").val();
        var type = 0;
        var typeval = "";
        $("#tdSelct1").find("input[type='radio'][name='rdoSelectType']:checked").each(function() {
        type = $(this).val(); //查询类型
            typeval = decodeURIComponent($.trim($("#txtContent").val()));
        });
        parms.Date = orderdate;
        parms.Page = 1;
        parms.TypeName = type;
        parms.Content = typeval;
        HistoryOrder.GetOrderList();
    });
    $("#txtContent").bind("keypress", function(e) {
        if (e.keyCode == 13) {
            $("#imgSelectByType").click();
            return false;
        }
    });

});
</script>
</asp:Content>