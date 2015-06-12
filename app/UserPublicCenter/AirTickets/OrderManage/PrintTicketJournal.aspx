<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTicketJournal.aspx.cs"
    Inherits="UserPublicCenter.AirTickets.OrderManage.PrintTicketJournal" MasterPageFile="~/MasterPage/AirTicket.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <div class="sidebar02_con_table01">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA"
            style="border: 1px #CCCCCC solid; margin-top: 5px;">
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td width="15%" height="30" align="left">
                    <strong>订单编号：</strong>
                </td>
                <td width="23%" align="left">
                    <input name="txtOrderNumber" type="text" id="txtOrderNumber" size="20" />
                </td>
                <td width="62%" align="left">
                    <img src="<%=ImageServerUrl %>/images/jipiao/btn.jpg" onclick="PrintTicketJournal.Search();"
                        style="cursor: pointer" alt="查询" /><span style="color:Red" id="spOrderNo"></span>
                </td>
            </tr>
            <tr>
                <td height="5" colspan="3">
                </td>
            </tr>
        </table>
    </div>
    <div class="sidebar02_con_table02" id="div_SearchOrderList" >
    </div>
    <style type="text/css">
        @media print
        {
            #top {  display:none }
            #header {  display:none }
            #tb_OrderInfo {width:720px}
            .sidebar {  display:none }
            .sidebar02_toptext {  display:none }
            .sidebar02_con_table01 {  display:none }
            .pringBtn {  display:none }
            
        }
        .printhidden
        {
        	display:none;
        }
        .printshow
        {
        	display:block;
        }
    </style>

    <script type="text/javascript">
        var arrTravelerId = new Array();
        var arrTravelerHtml = new Array();          
        var TraveArry=new Array(); //根据订单查询，旅客对象数组     
        var PrintTicketJournal = {
            GetOrderInfo: function() {
                var searchParm = { "SearchOrderNumber": "" };
                if($.trim($("#txtOrderNumber").val())!=""){
                    searchParm.SearchOrderNumber = escape($.trim($("#txtOrderNumber").val()));
                    $("#spOrderNo").html("");
                }else {
                   $("#spOrderNo").html("订单编号不能为空！");
                   return;
                }
                $.ajax({
                    type: "GET",
                    dataType: 'html',
                    url: "GetOrderInfo.aspx",
                    data: searchParm,
                    cache: false,
                    success: function(html) {
                        $("#div_SearchOrderList").html(html);
                        PrintTicketJournal.ProiorNextBindClick();
                    }
                });
            },
            Search: function() {
                PrintTicketJournal.GetOrderInfo();
            },
            PriorOrNext: function(thisId, PriorOrNextId) {
                arrTravelerId.push(thisId);
                arrTravelerHtml.push($("#div_SearchOrderList").html());
                var Index = $.inArray(PriorOrNextId, arrTravelerId);
                if (Index == -1) { //未加载过
                    PrintTicketJournal.GetOrderInfo();
                    if ($("#div_SearchOrderList").html() != "") {
                        arrTravelerId.push(PriorOrNextId);
                        arrTravelerHtml.push($("#div_SearchOrderList").html());
                    }
                } else {
                    $("#div_SearchOrderList").html(arrTravelerHtml[Index]);
                }
            },
            Print: function() {
                if (window.print != null) {
                    var cssname = "printhidden";
                    $("#top").addClass(cssname);
                    $("#header").addClass(cssname);
                    $(".sidebar").addClass(cssname);
                    $(".sidebar02_toptext").addClass(cssname);
                    $(".sidebar02_con_table01").addClass(cssname);
                    window.print();
                } else {
                    alert("没有安装打印机!");
                }
            },
            PrintShow:function()
            {
                 var cssname = "printhidden";
                 var printshow="printshow";
                    $("#top").addClass(cssname);
                    $("#header").addClass(cssname);
                    $(".sidebar").addClass(cssname);
                    $(".sidebar02_toptext").addClass(cssname);
                    $(".sidebar02_con_table01").addClass(cssname);
                $("#hfprior").addClass(cssname);
                $("#hfnext").addClass(cssname);
                $("#hfClear").addClass(cssname);
                $("#hfPrint").removeClass(printshow);
                $("#hfPrint").addClass(cssname);
                
                $("#hfPrint2").removeClass(cssname);
                $("#hfPrint2").addClass(printshow);
                
                $("#hfReturn").removeClass(cssname);
                $("#hfReturn").addClass(printshow);
                
             },
            PrintReturn:function()
            { 
                 var cssname = "printhidden";
                 var printshow="printshow";
                $("#top").removeClass(cssname);
                $("#header").removeClass(cssname);
                $(".sidebar").removeClass(cssname);
                $(".sidebar02_toptext").removeClass(cssname);
                $(".sidebar02_con_table01").removeClass(cssname);
                  $("#hfprior").removeClass(cssname);
                $("#hfnext").removeClass(cssname);
                $("#hfClear").removeClass(cssname);
                
                $("#hfPrint").removeClass(cssname);
                $("#hfPrint").addClass(printshow);
                
                $("#hfPrint2").removeClass(printshow);
                $("#hfPrint2").addClass(cssname);  
                $("#hfReturn").removeClass(printshow); 
                $("#hfReturn").addClass(cssname);
            },
            ClearTableInput:function(){
                $("#tb_OrderInfo").find("input[type=text]").each(function()
                {
                    $(this).val("");
                });
            },
            ShowProiorOrNext:function(CurrentNum){
                
                if(CurrentNum>0 && CurrentNum<=TraveArry.length)
                {
                    pnr=eval(TraveArry[CurrentNum-1]).TicketNumber; //暂显示票号
                    number=eval(TraveArry[CurrentNum-1]).CertNo;
                    trename=eval(TraveArry[CurrentNum-1]).TravellerName ;   
                   $("#txtpnr").val(pnr);
                   $("#txtTraNum").val(number);
                   $("#txtTraName").val(trename);
                   var prior=parseInt(CurrentNum)-1;
                   var next=parseInt(CurrentNum)+1;
                   $("#hfnext").attr("showpage",next)
                   $("#hfprior").attr("showpage",prior);   
                }
            },
            ProiorNextBindClick:function()
            {
                if($("#hidTraveJson").length>0){
                    var trajson= $("#hidTraveJson").val();                
                    TraveArry=eval(trajson);
                    $("#hfprior").click(function(){
                        var currentpage= $("#hfprior").attr("showpage");
                        PrintTicketJournal.ShowProiorOrNext(currentpage);  
                    });
                    $("#hfnext").click(function(){
                        var currentpage=$("#hfnext").attr("showpage");
                        PrintTicketJournal.ShowProiorOrNext(currentpage);  
                    }); 
                }
            }
        };
        $(document).ready(function() {

            $("#txtOrderNumber").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    PrintTicketJournal.Search();
                    return false;
                }
            });
            
        });
    </script>

</asp:Content>
