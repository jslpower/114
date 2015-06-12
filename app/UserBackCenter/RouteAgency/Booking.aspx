<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs"  Inherits="UserBackCenter.RouteAgency.Booking" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>
    
</head>
<body>
    <form id="form1" runat="server" method="post">
    <input id="hidPriceStandId" name="hidPriceStandId" value="<%=PriceStandId %>" type="hidden" />
    <input id="hidRouteName" name="RouteName" value="<%=RouteName %>" type="hidden" />
    <input id="hidLeaveDate" name="LeaveDate" value="<%=LeaveDate %>" type="hidden" />
    <input id="hidTourID" name="TourID" value="<%=TourID %>" type="hidden" />
    <input id="hidRemnantNumber" name="RemnantNumber" value="<%=RemnantNumber %>" type="hidden" />
    <table width="100%" border="0" cellspacing="0" cellpadding="6">
        <tr>
            <td width="15%" align="right" class="tdleft">
                <strong>线路名称：</strong>
            </td>
            <td width="85%" colspan="5" align="left" class="td2left">
                <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%=TourID %>" target="_blank">
                    <%=RouteName%></a> <strong>出发日期：</strong><span class="lv14"><%=LeaveDate%></span>
                <%=leaveDateDay %>
                <strong>当前剩余空位：<span class="lv14" id="span_RemnantNumber"><%=RemnantNumber%></span></strong>
            </td>
        </tr>
        <tr>
            <td width="15%" align="right" class="tdleft">
                <strong>预定组团社名称：</strong>
            </td>
            <td colspan="5" align="left" class="td2left">
                <input name="BuyCompanyName" id="txtBuyCompanyName"  valid="required" errmsg="请选择组团社!" readonly="readonly" type="text" size="30" />
                <input id="hidBuyCompanyID" name="BuyCompanyID" type="hidden" />
                <a href="javascript:void(0)" dialogurl="/RouteAgency/QueryTour.aspx" id="a_selectTour" >
                    <img src="<%=ImageServerPath %>/images/icon_select.jpg" border="0" /></a>
            </td>
        </tr>
        <tr>
            <td align="right" class="tdleft">
                <strong>交通安排：</strong>
            </td>
            <td colspan="2" align="left" class="td2left">
                <textarea name="Traffic" id="txtTraffic" style="width:60%;" cols="60" rows="2"><%=Traffic%></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" class="tdleft">
                <strong>价格组成：</strong>
            </td>
            <td colspan="5" align="left" class="td2left">
                <table width="100%" cellpadding="0" cellspacing="0" id="price">
                    <asp:Repeater runat="server" ID="rptPrice" OnItemDataBound="rptPrice_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style=" display:inline;">                                    
                                    <input name="radiobutton" type="radio" id="id<%#Eval("PriceStandId") %>"
                                        value="<%#Eval("PriceStandId") %>" /><label for="id<%#Eval("PriceStandId") %>"><%#Eval("PriceStandName")%></label>
                                    <span style=" display:inline;">成人价￥<%#Eval("AdultPrice1")%>&nbsp; 儿童价￥<%#Eval("ChildrenPrice1")%>&nbsp;单房差<%#Eval("SingleRoom1") %></span>
                                    <div style="display:inline;">
                                        成人数<input type="text" value="0" name="<%#Eval("PriceStandId") %>AdultNumber"  valid="required|RegInteger" errmsg="请填写成人数!|成人数只能整数,请输入整数!"  maxlength="3" size="2"
                                            onchange="Booking.createTr(this,1)" style="border: 1px solid #7F9DB9; width: 22px;" />×单价<input
                                                name="<%#Eval("PriceStandId") %>PersonalPrice"  valid="required|isNumber" errmsg="请填写成人价!|成人价只能是数字,请输入数字!" type="text" value="<%#Eval("AdultPrice1","{0:F2}") %>" size="4"
                                                style="border: 1px solid #7F9DB9;" /><span style="font-size: 20px; font-weight: bold;
                                                    color: #cc0000;">+</span>儿童数<input name="<%#Eval("PriceStandId") %>ChildNumber"  valid="required|RegInteger" errmsg="请填写儿童数!|儿童数只能整数,请输入整数!"  maxlength="3" type="text" value="0"
                                                        onchange="Booking.createTr(this,0)" size="2" style="border: 1px solid #7F9DB9;
                                                        width: 22px;" />×单价<input name="<%#Eval("PriceStandId") %>ChildrenPrice1"   valid="required|isNumber" errmsg="请填写儿童价!|儿童价只能是数字,请输入数字!"  type="text" value="<%#Eval("ChildrenPrice1","{0:F2}")%>"
                                                            size="4" style="border: 1px solid #7F9DB9;" /><span style="font-size: 20px; font-weight: bold;
                                                                color: #cc0000;">+</span>单房差<input name="<%#Eval("PriceStandId") %>MarketNumber"   valid="required|RegInteger" errmsg="请填写单房差!|单房差只能整数,请输入整数!"  type="text" value="0"
                                                                    size="2" style="border: 1px solid #7F9DB9;" />×单价<input name="<%#Eval("PriceStandId") %>MarketPrice" type="text"  valid="required|isNumber" errmsg="请填写单房差单价!|单房差单价只能是数字,请输入数字!"
                                                                        value="<%#Eval("SingleRoom1","{0:F2}") %>" size="3" style="border: 1px solid #7F9DB9;" /><%--<span style="font-size: 20px;
                                                                            font-weight: bold; color: #cc0000;">+</span>票差<input name="<%#Eval("PriceStandId") %>22" type="text"
                                                                                value="0" size="3" style="border: 1px solid #7F9DB9;" />×人数<input name="<%#Eval("PriceStandId") %>OneRoomCount"
                                                                                    type="text" value="0" size="2" style="border: 1px solid #7F9DB9;" />--%>
                                        <span style="font-size: 20px; font-weight: bold; color: #cc0000;">+</span>其它费用
                                        <input name="<%#Eval("PriceStandId") %>OtherPrice" type="text" value="0" style="border: 1px solid #7F9DB9; width:40px;" />
                                    </div>
                                    
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="tdleft">
                <strong>总金额：</strong>
            </td>
            <td colspan="5" align="left" class="td2left">
                <input name="SumPrice" id="txtSumMoney" readonly="readonly" type="text"
                    style="height: 20px; width: 100px; text-align: right; font-size: 16px; font-weight: bold;"
                    value="0.00" />
            </td>
        </tr>
        <tr>
            <td align="right" class="tdleft">
                <strong>特殊要求说明：</strong>
            </td>
            <td colspan="5" align="left" class="td2left">
                <textarea name="SpecialContent" id="txtRemark" cols="80" rows="4"><%=SpecialContent%></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="tdleft" id="TourOrderInfo">
            </td>
        </tr>
    </table>
    <input type="button" id="btnSave" name="Submit2" value="提交预订订单" />
    <input type="button" id="btnLeaveSeat" value="提交同意留位订单" onclick="Booking.open(this)" />
    &nbsp;
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="left">
                <strong>可出机票游客信息</strong>
            </td>
        </tr>
    </table>
    <table id="tblCustomerList" cellspacing="1" cellpadding="0"
        width="100%" align="center" bgcolor="#93B5D7" border="0" sortcol="5">
        <tr align="middle" bgcolor="#f9f9f4">
            <td style="width:50px;" height="20" bgcolor="#D4E6F7">
                序号
            </td>
            <td style="width:80px;" bgcolor="#D4E6F7">
                姓&nbsp;&nbsp; 名
            </td>
            <td style="width:100px;" bgcolor="#D4E6F7">
                证件名称
            </td>
            <td style="width:100px;" bgcolor="#D4E6F7">
                证件号码
            </td>
            <td style="width:60px;" bgcolor="#D4E6F7">
                性别
            </td>
            <td style="width:40px;"  bgcolor="#D4E6F7">
                类型
            </td>
            <td style="width:40px;"  bgcolor="#D4E6F7">
                座位号
            </td>
            <td style="width:100px;"  bgcolor="#D4E6F7">
                联系电话
            </td>
            <td bgcolor="#D4E6F7">
                备注
            </td>
        </tr>
        <tr id="noData" bgcolor="white">
            <td colspan="9">暂无游客信息！</td>
        </tr>
    </table>    
    <table style="height: 45px; position: absolute; display:none; background-color: White; border: 1px solid gray;"
        id="tbl_last">
        <tr>
            <td>
                <img src="<%=ImageServerPath %>/images/ico_edit.gif" />
                请设置留位截止时间
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DatePicker ID="txtEndTime" name="SaveSeatDate" DisplayTime="true" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                
                <a href="javascript:void(0)" id="btnYes">确定</a>
                &nbsp;&nbsp;&nbsp; <a href="javascript:void(0)" onclick="Booking.close()">取消</a>
            </td>
        </tr>
    </table>    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>" cache="true"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript" language="javascript">
            var Booking={ 
                funHandleIeLayout:null,
                 isScroll:false,
                 handleIELayout:function(){
                    if(Booking.isScroll){
                        Booking.isScroll = false;
                        document.body.style.display="none";
                        document.body.style.display="";
                        clearInterval(Booking.funHandleIeLayout);
                        Booking.funHandleIeLayout = null;
                    }
                 },
                 scroll1:function()
                 { 
                    Booking.isScroll = true;
                    clearInterval(Booking.funHandleIeLayout);
                    Booking.funHandleIeLayout=setInterval(Booking.handleIELayout,300);
                },             
                open:function(obj){
                    var pos = $(obj).offset();
                    $("#tbl_last").show().css({left:Number(pos.left)+"px",top:Number(pos.top-70)+"px"});
                },
                close:function(){
                    $("#tbl_last").hide()
                },
                createTr:function(obj,type){   //type,1:成人;0:儿童                    
                    var num=$.trim($(obj).val());
                    $("#noData").remove()
                    var input=$(obj).parent().children("input[type='text']");   
                    if(!Booking.checkVistorsNum(input)) return;
                    if(!isNaN(num)){
                        var trLength=$("#tblCustomerList tr[itemType='"+type+"']").length;
                        var trHtml="<tr bgcolor=\"#ffffff\" itemType='"+type+"'>"+
                                    "<td bgcolor=\"#EDF6FF\"></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><input  style=\"width:80px;\" value=\"\" name=\"VisitorName\" /></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><select id=\"CradType\" name=\"CradType\">"+
                                        "<option value=\"0\" selected=\"selected\">请选择证件</option>"+
                                        "<option value=\"1\">身份证</option>"+
                                        "<option value=\"2\">户口本</option>"+
                                        "<option value=\"3\">军官证</option>"+
                                        "<option value=\"4\">护照</option>"+
                                        "<option value=\"5\">边境通行证</option>"+
                                        "<option value=\"6\">其他</option>"+
                                    "</select></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><input name=\"CradNumber\" id=\"CradNumber\"  style=\"width:120px;\" /></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><select id=\"Sex\" name=\"Sex\">"+
                                        "<option value=\"1\" selected=\"selected\">男</option>"+
                                        "<option value=\"0\">女</option>"+
                                    "</select></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\">"+
                                        (type=='0'?"<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"0\" type=\"hidden\" />儿童</option>":"<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"1\" type=\"hidden\" />成人")+
                                    "</td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><input name=\"SeatNumber\" id=\"SeatNumber\" style=\"width:40px;\"  /></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><input name=\"ContactTel\" id=\"ContactTel\"  style=\"width:100px;\" /></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\"><input id=\"Remark\" name=\"Remark\"  style=\"width:100px;\" /></td></tr>";                                                                       
                        if(num>trLength){
                            for(var i=0;i<num-trLength;i++){
                                $("#tblCustomerList tr:last").after(trHtml)                                
                            }
                        }else{
                            $("#tblCustomerList tr[itemType='"+type+"']").each(function(k){
                                if(k>num-1){
                                    $(this).remove();
                                }
                            });
                        }
                        $("#tblCustomerList tr").each(function(i){ 
                            if(i>0)
                               $(this).find("td").eq(0).html(i)
                        });
                    }
                },
               checkVistorsNum:function(objArray){
                   var RemnantNumber=$("#hidRemnantNumber").val();//剩余人数
                   var vistorNum=Number(objArray[0].value)+Number(objArray[2].value);                              
                   if(RemnantNumber<vistorNum)
                   {
                        alert("游客人数大于剩余人数,将不能保存，请重新输入游客数！");
                        return false;             
                   }
                   return true;
                },
                pageInit:function(){
                    radioOfPrice();
                    $("#price input[type='radio']").each(function(i){
                        if(i==0){
                            $(this).next().next().hide();
                            $(this).next().next().next().show();
                        }  
                        $(this).click(function(){
                            $("#hidPriceStandId").val($(this).val())
                            pricePointer(this);
                            radioOfPrice();
                        });                 
                    });
                    $("#price td input[type='text']").each(function(i){  
                         $(this).blur(function(){                            
                               var input=$(this).parent().children("input[type='text']");                                       
                               var result=Number(input[0].value*input[1].value)+Number(input[2].value*input[3].value)+Number(input[4].value*input[5].value)+Number(input[6].value); 
                               if(!isNaN(result)){
                                  $("#txtSumMoney").val(Math.round(result*100)/100)
                               }else{
                                  alert('总金额计算错误，价格组成，计算时请输入数字！')
                               }
                         });  
                    })
                    $("#price input[type='radio']").eq(0).attr("checked","checked");
                    $("#a_selectTour").click(function(){
                        var url=$(this).attr("dialogUrl");
                        var currendIframeId='<%=Request.QueryString["iframeId"] %>';
                        parent.Boxy.iframeDialog({title:"选择组团社", iframeUrl:url,width:760,height:400,draggable:true,data:{NeedId:currendIframeId}});                 
                        return false;
                    });
                    //确认成交
                    $("#btnSave").click(function(){
                        if(confirm("你是否确定要提交该代客预订订单？")){
                            if(ValiDatorForm.validator($("#txtSumMoney").closest("form").get(0),"alert")){
                                bookAjax("TourBook")  
                            }  
                        }
                    });
                    //留位
                     $("#btnYes").click(function(){
                        if(confirm("你是否确定要提交该留位代客预订订单？")){
                           var leaveTime=$("#txtEndTime_dateTextBox").val();      
                           if(leaveTime==""){
                                alert("留位时间不能为空！");
                                return false;
                           }
                           if(Date.parse(leaveTime.replace(/-/g,"/")+":00")<new Date()) {
                                alert("留位时间必须大于当前时间！");
                                return false;
                           }                                                     
                            if(ValiDatorForm.validator($("#txtSumMoney").closest("form").get(0),"alert")){
                                bookAjax("LeaveBook")
                            }
                        }   
                    });        
                }               
            };
            function bookAjax(action){                                       
                $.ajax({
                   type: "POST",
                   url: "/RouteAgency/Booking.aspx?action="+action+"&TourID="+$("#hidTourID").val(),
                   data: $("form").eq(0).serializeArray(),
                   success: function(msg){
                     var returnMsg=eval(msg);
                     if(returnMsg)
                     {
                        alert(returnMsg[0].ErrorMessage)                        
                        if(returnMsg[0].isSuccess){ 
                            if(!confirm("是否继续预订，单击取消将关闭本窗口！")){
                                if(typeof(parent.NotStartingTeamsDetail)=="function"){
                                    parent.NotStartingTeamsDetail.bookReturn();
                                }
                                parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();                            
                            }else{
                                location.href=location.href
                            }
                        }
                     }else{
                        alert('预订失败！')
                     }
                   }
                });     
            }
            function radioOfPrice(){
                $("#price input[type='radio']").each(function(i){
                    if(this.checked){
                        pricePointer(this);                
                    }else{
                        $(this).next().next().eq(0).show();
                        $(this).next().next().next().hide();
                    }
               });
            }            
            //调整，输入价格显示
            function pricePointer(obj){
                $(obj).next().next().hide();
                $(obj).next().next().next().show()
            }
            function closeWin(){
                 parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();  
            }
            $(function(){
                Booking.pageInit();                
                if ( $.browser.msie ){
                   if($.browser.version=="6.0")
                   { 
                     window.parent.$('#<%=Request.QueryString["iframeId"] %>').get(0).contentWindow.document.body.onscroll= Booking.scroll1;
                   }
                }       
            })
    </script>

    </form>
</body>
</html>
