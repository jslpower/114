<%@ Page Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.ReportList" Title="机票订单管理报表页" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="c1">
<script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script> 
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />  
   <div class="sidebar02_con_table01">
     <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA" style="border:1px #CCCCCC solid; margin-top:5px;">
     		
              <tr bgcolor="#EDF8FC">
                <th align="left" bgcolor="#EDF8FC"><table width="50%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td><a href="#" id="two1" onclick="setTab('two',1,2)" class="book_default">日报表</a></td>
                      <td><a href="#" id="two2" onclick="setTab('two',2,2)">月报表</a></td>
                    </tr>
                </table></th>
                <th height="30" align="center">&nbsp;</th>
                <td height="30" colspan="2" align="left">&nbsp;</td>
          </tr>
              <tr bgcolor="#DCD8D8">
                <td height="1" colspan="4" align="left"></td>
       	  </tr>
              <tr id="con_two_1">
                <td width="35%" height="35" align="left">选择日期查询：
                  <input name="txtStartTime" type="text" id="txtStartTime" value=""onfocus="WdatePicker()" />
                <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageManage.GetImagerServerUrl(1)%>/images/jipiao/time.gif" width="16" height="13" />到</td>
                <td width="25%" align="left"><input name="txtEndTime" type="text" id="txtEndTime" onfocus="WdatePicker()" value="" />
                <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageManage.GetImagerServerUrl(1)%>/images/jipiao/time.gif" width="16" height="13" /></td>
                <td width="20%" align="center"><input type="button" name="button" id="btnSearchDetail" value="按条件查询明细"  onclick="ReportList.OnSearchDay();" /></td>
                <td width="20%" align="center"><input type="button" name="button2" id="btnSearchTotal" value="按条件查询合计" onclick="ReportList.GetReportTotal();"/></td>
          </tr>
          <tr id="con_two_2" style="display:none;">
                <td width="35%" height="35" align="left">选择日期：
                    <select name="select" id="selYear">                  
                  </select>
                 年 
                 <select name="select2" id="selMonth">0
                   <option value="1">1</option>
                   <option value="2">2</option>
                   <option value="3">3</option>
                   <option value="4">4</option>
                   <option value="5">5</option>
                   <option value="6">6</option>
                   <option value="7">7</option>
                   <option value="8">8</option>
                   <option value="9">9</option>
                   <option value="10">10</option>
                   <option value="11">11</option>
                   <option value="12">12</option>
                   </select>               
                月</td>
            <td width="35%" align="left"><input type="button" name="button3" id="btnMonthReport" onclick="ReportList.OnSearchMonth()" value="查询"  /></td>
                <td width="20%"align="center">&nbsp;</td>
                 <td width="20%"align="center">&nbsp;</td>
          </tr>
              <tr>
                <td height="5" colspan="4"></td>
              </tr>
  	    </table>
    </div>     
    <div id="divReportList" align="center"></div> 
  
<script type="text/javascript">
////////////切换日报表，月报表/////////////////////
function setTab(name,cursel,n){
var ff = !(document.all) ;
 for(i=1;i<=n;i++){
  var menu=document.getElementById(name+i);
  var con=document.getElementById("con_"+name+"_"+i);
  menu.className=i==cursel?"book_default":"";
  con.style.display=i==cursel?(ff?"table-row":"block"):"none";
 }
}
 </script>  
<script type="text/javascript">
//////////动态绑定年份///////////////
    var yearSel=document.getElementById("selYear");//获取年份下拉框对象
    var date=new Date();//实例化一个日期对象
    var i=0;
    var minYear=2009;//最小年份从2009年开始--年份上限
    var maxYear=date.getFullYear();//最大年份为当前年份--年份下限
    for(var year=minYear;year<=maxYear;year++){
           yearSel.options[i]=new Option(year,year);
		   i++;  
    }
//    for(var year=date.getFullYear();year>date.getFullYear()-10;year--){
//		    yearSel.options[i]=new Option(year,year);
//		    i++;
//    }
</script>
 <script type="text/javascript">
         var Params={StartTime:"",EndTime:"",Year:"",Month:"",Type:0,Stats:0};
         var ReportList=
         {
             GetReportList:function()//按条件查询明细
             {
                 LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                 LoadingImg.ShowLoading("divReportList");
                 if(LoadingImg.IsLoadAddDataToDiv("divReportList")) {
                 $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url:"/AirTickets/OrderManage/AjaxReportList.aspx",
                        data:Params,
                        cache: false,
                        success:function(html)
                        {
                            $("#divReportList").html(html);
                        },
                         error: function(xhr, s, errorThrow) {
                                $("#divReportList").html("未能成功获取响应结果")
                            }
                     });
                  }
             },
             GetReportTotal:function()//按条件查询合计
             {
                 LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                 LoadingImg.ShowLoading("divReportList");
                 if(LoadingImg.IsLoadAddDataToDiv("divReportList")) {
                 $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url:"/AirTickets/OrderManage/AjaxReportTotal.aspx?StartTime="+$.trim($("#txtStartTime").val())+"&EndTime="+ $.trim($("#txtEndTime").val())+"&Type=0&Stats=1",                      
                        cache: false,
                        success:function(html)
                        {
                            $("#divReportList").html(html);
                        },
                         error: function(xhr, s, errorThrow) {
                                $("#divReportList").html("未能成功获取响应结果")
                            }
                     });
                  }
             },
             OnSearchDay:function()//日报表 查询明细
             {
                  Params.StartTime = $.trim($("#txtStartTime").val());             
                  Params.EndTime = $.trim($("#txtEndTime").val());  
                  Params.Type=0;  
                  Params.Stats=0;       
                  ReportList.GetReportList();       
             },                    
             OnSearchMonth:function()//月报表
             {                  
                  Params.Year=$("#selYear").val();      
                  Params.Month=$("#selMonth").val();   
                  Params.Type=1;         
                  ReportList.GetReportList();       
             }
         };  
         $(document).ready(function(){
               $("#two1").click(function(){
                   ReportList.OnSearchDay();
               });
              $("#two2").click(function(){
                 ReportList.OnSearchMonth();
              });         
         });     
 
 </script>
</asp:Content>
