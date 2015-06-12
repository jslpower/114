<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditOrder.aspx.cs"
    Inherits="UserBackCenter.UserOrder.EditOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <% 
        if (!Page.IsPostBack)
        {
            ro_urlReferrer.Value = Request.UrlReferrer.Host;
        }

        if (Request.Url.HostNameType == UriHostNameType.Dns)
        {
            if (!Request.Url.ToString().ToLower().Contains("localhost"))
            {
                if (ro_urlReferrer.Value != Request.Url.Host)
                {%>

    <script type="text/javascript">
             document.domain="tongye114.com";
    </script>

    <%}
           }
       } %>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="ro_urlReferrer" value="" runat="server" />
    <input id="hidBuyCompanyName" name="hidBuyCompanyName" value="<%=BuyCompanyName %>"
        type="hidden" />
    <input id="hidContactName" name="hidContactName" value="<%=ContactName %>" type="hidden" />
    <input id="hidContactTel" name="hidContactTel" value="<%=ContactTel %>" type="hidden" />
    <input id="hidContactQQ" name="hidContactQQ" value="<%=ContactQQ %>" type="hidden" />
    <input id="hidLeaveDate" name="hidLeaveDate" value="<%=LeaveDate %>" type="hidden" />
    <input id="hidContactMQ" name="hidContactMQ" value="<%=ContactMQ %>" type="hidden" />
    <input id="hidLeaveTraffic" name="hidLeaveTraffic" value="<%=LeaveTraffic %>" type="hidden" />
    <input id="hidOrderSource" name="hidOrderSource" value="<%=OrderSource %>" type="hidden" />
    <input id="hidSeatList" name="hidSeatList" value="" type="hidden" />
    <input id="hidOrderState" name="hidOrderState" value="<%=OrderState %>" type="hidden" />
    <input id="hidOrderType" name="hidOrderType" value="<%=OrderType %>" type="hidden" />
    <asp:HiddenField ID="hidRemnantNumber" runat="server" />
    <asp:Repeater runat="server" ID="rptEditOrder">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="6" style="background-color: #F5FAFB;">
                <tr>
                    <td align="right" class="tdleft">
                        <strong>线路名称：</strong>
                    </td>
                    <td width="85%" colspan="5" align="left" class="td2left">
                        <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%=TourID %>" target="_blank">
                            <%#Eval("RouteName")%></a> <strong>出发日期：</strong><span class="lv14"><%#Eval("LeaveDate","{0:yyyy-MM-dd}")%></span>
                        <%#EyouSoft.Common.Utils.ConvertWeekDayToChinese(Convert.ToDateTime(Eval("LeaveDate")))%>
                        <strong>当前剩余空位：<span class="lv14"><%=ShowRemnantNumber%></span></strong>
                    </td>
                </tr>
                <tr>
                    <td width="15%" align="right" class="tdleft">
                        <strong>预定单位：</strong>
                    </td>
                    <td colspan="5" align="left" class="td2left">
                        <%#Eval("BuyCompanyName")%>&nbsp;&nbsp; <strong>
                            <br />
                            联系人</strong>：
                        <%#Eval("ContactName")%>
                        <strong>电话</strong>：
                        <%#Eval("ContactTel")%>
                        <strong>传真</strong>：<%#Eval("ContactFax")%><strong> 联系MQ：</strong><%#Utils.GetBigImgMQ(Eval("ContactMQ").ToString())%>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="tdleft">
                        <strong>交通安排：</strong>
                    </td>
                    <td colspan="2" align="left" class="td2left">
                        <textarea name="Traffic" style="width: 500px;" id="txtTraffic" cols="60" rows="2"><%#Eval("LeaveTraffic")%></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="tdleft">
                        <strong>价格组成：</strong>
                    </td>
                    <td colspan="5" align="left" class="td2left">
                        <table width="100%" cellpadding="0" cellspacing="0" id="price">
                            <tr>
                                <td>
                                    <input type="radio" id="id<%#Eval("PriceStandId") %>" value="<%#Eval("PriceStandId") %>"
                                        name="PriceStandId" checked="checked" value="radiobutton" /><label for="id<%#Eval("PriceStandId") %>"></label>&nbsp;
                                    成人数<input type="text" value="<%#Eval("AdultNumber") %>" valid="required|RegInteger"
                                        errmsg="请填写成人数!|成人数只能整数,请输入整数!" maxlength="3" name="AdultNumber" size="2" onchange="EditOrder.createTr(this,1)"
                                        style="border: 1px solid #7F9DB9; width: 22px;" />×单价<input name="PersonalPrice"
                                            valid="required|isNumber" errmsg="请填写成人价!|成人价只能是数字,请输入数字!" type="text" value="<%#Eval("PersonalPrice","{0:F2}") %>"
                                            size="4" style="border: 1px solid #7F9DB9;" /><span style="font-size: 20px; font-weight: bold;
                                                color: #cc0000;">+</span>儿童数<input name="ChildNumber" valid="required|RegInteger"
                                                    errmsg="请填写儿童数!|儿童数只能整数,请输入整数!" maxlength="3" type="text" value="<%#Eval("ChildNumber") %>"
                                                    onchange="EditOrder.createTr(this,0)" size="2" style="border: 1px solid #7F9DB9;
                                                    width: 22px;" />×单价<input name="ChildPrice" type="text" valid="required|isNumber"
                                                        errmsg="请填写儿童价!|儿童价只能是数字,请输入数字!" value="<%#Eval("ChildPrice","{0:F2}")%>" size="4"
                                                        style="border: 1px solid #7F9DB9;" /><span style="font-size: 20px; font-weight: bold;
                                                            color: #cc0000;">+</span>单房差<input name="MarketNumber" type="text" value="<%#Eval("MarketNumber") %>"
                                                                valid="required|RegInteger" errmsg="请填写单房差!|单房差只能整数,请输入整数!" size="2" style="border: 1px solid #7F9DB9;" />×单价<input
                                                                    name="MarketPrice" type="text" valid="required|isNumber" errmsg="请填写单房差单价!|单房差单价只能是数字,请输入数字!"
                                                                    value="<%#Eval("MarketPrice","{0:F2}") %>" size="3" style="border: 1px solid #7F9DB9;" />
                                    <span style="font-size: 20px; font-weight: bold; color: #cc0000;">+</span>其它费用
                                    <input name="OtherPrice" type="text" valid="required|isNumber" errmsg="请填写其它费用!|其它费用只能是数字,请输入数字!"
                                        value="<%#Eval("OtherPrice","{0:F2}") %>" style="border: 1px solid #7F9DB9; width: 50px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="tdleft">
                        <strong>特殊要求说明：</strong>
                    </td>
                    <td colspan="5" align="left" class="td2left">
                        <textarea name="SpecialContent" id="txtSpecialContent" style="width: 500px;" cols="80"
                            rows="4"><%#Eval("SpecialContent")%>
                </textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="tdleft">
                        <strong>总金额：</strong>
                    </td>
                    <td colspan="5" align="left" class="td2left">
                        <input name="SumPrice" id="txtSumPrice" valid="required|isNumber" errmsg="请填写总金额!|总金额只能是数字,请输入数字!"
                            type="text" style="height: 20px; width: 100px; text-align: right; font-size: 16px;
                            font-weight: bold;" value="<%#Eval("SumPrice","{0:F2}") %>" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>操作留言：</strong>
                    </td>
                    <td colspan="5" align="left">
                        <textarea name="OperatorContent" style="width: 500px;" cols="60"><%#Eval("OperatorContent")%></textarea>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <table width="100%" runat="server" id="tblOrderBtnsContainer" border="0" cellpadding="3"
        cellspacing="0" bgcolor="#FEF7CD" style="border: 1px solid #D8BC81; margin-top: 8px;">
        <tr>
            <td align="right" style="width: 150px;">
                <strong>
                    <asp:Literal ID="ltrCompanyText" Text="批发商操作" runat="server"></asp:Literal>：</strong>
            </td>
            <td align="left">
                &nbsp;
                <%if (isGrant)
                  {%>
                <%if (isRouteCompany && isRouteCompanyProduct)
                  { %>
                <asp:Button ID="btnSave" runat="server" Text="确认成交" OnClick="btnSave_Click" />
                <asp:Button ID="btnLaeve" runat="server" Text="同意留位" />
                <asp:Button ID="btnNotEdit" runat="server" Text="不受理" OnClick="btnNotEdit_Click" />
                <%} %>
                <asp:Button ID="btnUpdate" runat="server" Text="保存修改" OnClick="btnUpdate_Click" />
                <%if (isRouteCompany && isRouteCompanyProduct)
                  { %>
                <asp:Button ID="btnSuccess" runat="server" Text="交易成功" OnClick="btnSuccess_Click" />
                <asp:Button ID="btnCancelLeave" runat="server" Text="取消留位" OnClick="btnCancelLeave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="取消订单" OnClick="btnCancel_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="删除无效订单" OnClick="btnDelete_Click" />
                <%} %>
                <%} %>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="left">
                <strong>可出机票游客信息</strong>
            </td>
        </tr>
    </table>
    <table id="tblCustomerList" cellspacing="1" cellpadding="0" width="100%" align="center"
        bgcolor="#93B5D7" border="0" sortcol="5">
        <tr bgcolor="#f9f9f4">
            <td style="width: 50px;" height="20" bgcolor="#D4E6F7">
                序号
            </td>
            <td style="width: 80px;" bgcolor="#D4E6F7">
                姓&nbsp;&nbsp; 名
            </td>
            <td style="width: 100px;" bgcolor="#D4E6F7">
                证件名称
            </td>
            <td style="width: 100px;" bgcolor="#D4E6F7">
                证件号码
            </td>
            <td style="width: 60px;" bgcolor="#D4E6F7">
                性别
            </td>
            <td style="width: 40px;" bgcolor="#D4E6F7">
                类型
            </td>
            <td style="width: 40px;" bgcolor="#D4E6F7">
                座位号
            </td>
            <td style="width: 100px;" bgcolor="#D4E6F7">
                联系电话
            </td>
            <td bgcolor="#D4E6F7">
                备注
            </td>
        </tr>
        <%=CustomerHtml%>
    </table>
    <table style="height: 45px; position: absolute; background-color: White; display: none;
        border: 1px solid gray;" id="tbl_last">
        <tr>
            <td>
                <img src="<%=ImageServerUrl %>/images/ico_edit.gif" />
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
                <asp:LinkButton ID="btnYes" runat="server" OnClick="btnYes_Click">确定</asp:LinkButton>
                &nbsp;&nbsp;&nbsp; <a href="javascript:void(0)" onclick="EditOrder.close()">取消</a>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" language="javascript">
            var EditOrder={
                 funHandleIeLayout:null,
                 isScroll:false,
                 handleIELayout:function(){
                    if(EditOrder.isScroll){
                        EditOrder.isScroll = false;
                        document.body.style.display="none";
                        document.body.style.display="";
                        clearInterval(EditOrder.funHandleIeLayout);
                        EditOrder.funHandleIeLayout = null;
                    }
                 },
                 scroll1:function()
                 { 
                    EditOrder.isScroll = true;
                    clearInterval(EditOrder.funHandleIeLayout);
                    EditOrder.funHandleIeLayout=setInterval(EditOrder.handleIELayout,300);
                },
                open:function(obj){
                    var pos=EditOrder.getPosition(obj);
                    $("#tbl_last").show().css({left:Number(pos.Left)+"px",top:Number(pos.Top-70)+"px"});
                    return false;
                },
                close:function(){
                    $("#tbl_last").hide()
                },
                createTr:function(obj,type){   //type,1:成人;0:儿童                    
                    var num=$.trim($(obj).val());
                    var input=$(obj).parent().children("input[type='text']");        
                    if(!checkVistorsNum(input)) return;
                    if(!isNaN(num)){
                        var trLength=$("#tblCustomerList tr[itemType='"+type+"']").length;
                        var trHtml="<tr bgcolor=\"#ffffff\" itemType='"+type+"'>"+
                                    "<td  bgcolor=\"#EDF6FF\" align=\"center\"></td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><input style=\"width:80px;\"  value=\"\" name=\"VisitorName\" /></td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><select id=\"CradType\" name=\"CradType\">"+
                                        "<option value=\"0\" selected=\"selected\">请选择证件</option>"+
                                        "<option value=\"1\">身份证</option>"+
                                        "<option value=\"2\">户口本</option>"+
                                        "<option value=\"3\">军官证</option>"+
                                        "<option value=\"4\">护照</option>"+
                                        "<option value=\"5\">边境通行证</option>"+
                                        "<option value=\"6\">其他</option>"+
                                    "</select></td>"+
                                    "<td  align=\"left\"  bgcolor=\"#EDF6FF\"><input name=\"CradNumber\" id=\"CradNumber\" style=\"width:120px;\"  /></td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><select id=\"Sex\" name=\"Sex\">"+
                                        "<option value=\"1\" selected=\"selected\">男</option>"+
                                        "<option value=\"0\">女</option>"+
                                    "</select></td>"+
                                    "<td align=\"left\" bgcolor=\"#EDF6FF\">"+
                                        (type=='0'?"<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"0\" type=\"hidden\" />儿童":"<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"1\" type=\"hidden\" />成人")+
                                    "</td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><input name=\"SeatNumber\" id=\"SeatNumber\" style=\"width:40px;\"  /></td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><input name=\"ContactTel\" id=\"ContactTel\" style=\"width:100px;\" /></td>"+
                                    "<td  align=\"left\" bgcolor=\"#EDF6FF\"><input id=\"Remark\" name=\"Remark\" style=\"width:100px;\"  /></td></tr>";                                                                       
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
                pageInit:function(){
                    $("#price td input[type='text']").each(function(i){  
                         $(this).blur(function(){
                            if(isNaN($(this).val())){
                               alert('你输入的不是数字，请输入数字！');
                               $(this).select().focus();
                            }else{
                                SumMoney(this);
                            }
                         });  
                    }).mouseover(function(){
                        $(this).select();
                    });
                    $("#<%=btnLaeve.ClientID %>").click(function(){
                        EditOrder.open(this);
                        return false;
                    });
                    $("#<%=btnYes.ClientID %>").click(function(){
                       var leaveTime=$("#txtEndTime_dateTextBox").val();      
                       if(leaveTime==""){
                            alert("留位时间不能为空！");
                            return false;
                       }
                       if(Date.parse(leaveTime.replace(/-/g,"/")+":00")<new Date()) {
                            alert("留位时间必须大于当前时间！");
                            return false;
                       }  
                       if(EditOrder.postData()){
                           return true;
                       }else{
                           return false;
                       }
                    });
                    $("#<%=btnSave.ClientID %>,#<%=btnUpdate.ClientID %>").click(function(){
                       if(EditOrder.postData()){
                           return true;
                       }else{
                           return false;
                       }
                    });                                             
                },
                postData:function(){
                    if(confirm("你确定要执行该操作吗？")){
		                return ValiDatorForm.validator($("#txtSumPrice").closest("form").get(0),"alert");  
		            }else{
		                return false;
		            }
                },
                getPosition:function(obj){
                    var objPosition={Top:0,Left:0}
                    var offset = $(obj).offset();
                    objPosition.Left=offset.left;
                    objPosition.Top=offset.top+$(obj).height();
                    return objPosition;
                },
                confirm:function(msg){
                    return confirm(msg);
                }           
            };
            function SumMoney(obj){
               var input=$(obj).parent().children("input[type='text']");        
               var result=Number(input[0].value*input[1].value)+Number(input[2].value*input[3].value)+Number(input[4].value*input[5].value)+Number(input[6].value);
               if(!isNaN(result)){
                  $("#txtSumPrice").val(Math.round(result*100)/100)
               }else{
                  alert('总金额计算错误，价格组成，计算时请输入数字！')
               }
            }
            function checkVistorsNum(objArray){
               var RemnantNumber=$("#<%=hidRemnantNumber.ClientID %>").val();//剩余人数
               var vistorNum=Number(objArray[0].value)+Number(objArray[2].value);                              
               if(RemnantNumber<vistorNum)
               {
                    alert("游客人数大于剩余人数,将不能保存，请重新输入游客数！");
                    return false;             
               }
               return true;
            }
            function closeWin(){
                var callBack='<%=Request.QueryString["OrderCallBack"] %>';
                var orderId='<%=Request.QueryString["OrderID"] %>';  
                var page='<%=Request.QueryString["Page"] %>';                 
                parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide(function(){
                    if(callBack!=""){
                        eval("parent."+callBack+"()")
                    }
                });                                     
            }
            $(function(){                           
                EditOrder.pageInit();
                if ( $.browser.msie ){
                   if($.browser.version=="6.0")
                   { 
                     window.parent.$('#<%=Request.QueryString["iframeId"] %>').get(0).contentWindow.document.body.onscroll= EditOrder.scroll1;
                   }
                }           
            })

    </script>

    </form>
</body>
</html>
