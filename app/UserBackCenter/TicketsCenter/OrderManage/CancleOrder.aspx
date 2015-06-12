<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancleOrder.aspx.cs" Inherits="UserBackCenter.TicketsCenter.OrderManage.CancleOrder" %>
<%@ Import Namespace="EyouSoft.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="float_info">
        <table width="40%" border="0" cellspacing="0" cellpadding="0" bgcolor="#e9f6fd" style="border: 2px #82d0fd solid;">
            <tr>
                <td height="10" align="center">
                </td>
            </tr>
            <tr>
                <td height="60" align="center">
                    <textarea name="txtRemark" id="txtRemark" cols="30" rows="5"></textarea>
                </td>
            </tr>
            <tr>
                <td height="35" align="center">
                    <input type="button" name="btnSubmit" id="btnSubmit" value="提交" onclick="return SubmitData();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divRequest" style="display:none;">正在退款中......</div>
    <input type="hidden" id="CancleOrder_hidOrderId" name="CancleOrder_hidOrderId" runat="server" />
    <input type="hidden" id="CancleOrder_hidOrderState" name="CancleOrder_hidOrderState" runat="server" />
    <input type="hidden" id="CancleOrder_hidChangeType" name="CancleOrder_hidChangeType" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
        function SubmitData(){
            var remark = $("#txtRemark").val();
            if(remark == '' || remark == null || remark == undefined){
                alert('请填写拒绝理由!');
                return false;
            }
            
            $(".float_info").css("display", "none");
            $("#divRequest").show().html("正在处理中...");
            
            var data = $($("#btnSubmit").closest("form").get(0)).serializeArray();
            $.ajax({
                type: "POST",
                url: "/ticketscenter/ordermanage/cancleorder.aspx?flag=save",
                data: data,
                dataType:"json",
                cache: false,
                success: function(state) {
                    if (state.success == '1') {//success
                        $(".float_info").css("display", "none");
                        var orderstate = $("#CancleOrder_hidOrderState").val();
                        var changetype = $("#CancleOrder_hidChangeType").val();
                        if(orderstate=='3'){//refund
                        
                            if(state.paytype==undefined||state.paytype==null){
                                alert(state.message);
                                closeWindow();
                                return;
                            }
                            
                            $("#divRequest").show().html(state.message);
                            var search = function(){
                                $.ajax({
                                    type: "POST",
                                    url: "/ticketscenter/ordermanage/cancleorder.aspx?flag=refund&batchno="+state.batchno,
                                    data: data,
                                    dataType:"json",
                                    cache: false,
                                    success:function(result){
                                        if(result.success='1'){
                                            if(result.search=="1"){
                                                setTimeout(function(){
                                                    search();
                                                },500);
                                            }else{
                                              alert(result.message);
                                              closeWindow(); 
                                            }
                                        }else{
                                            alert(result.message);
                                            closeWindow();
                                        } 
                                    }
                                });
                            }
                            setTimeout(function(){search();},100);
                        }else{//other success
                            alert(state.message);
                            closeWindow();
                        }
                    }else{//error
                        $("#divRequest").show().html(state.message);
                    }
                }
            });
            return true;
        }
        function closeWindow(){
            var frameid=window.parent.Boxy.queryString("iframeId");
            window.parent.Boxy.getIframeDialog(frameid).hide(function(){
                parent.topTab.url(parent.topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid='+ $("#CancleOrder_hidOrderId").val() +'');
            });
        }
        
        $(document).ready(function(){
            $("textarea[name=txtRemark]").blur(function(e){ 
                if($(this).val().length + $(this).val().replace(/[\u0000-\u00ff]/g," ").length > 250)
                {
                    alert('拒绝理由不能超过250个字符!');
                    if(e && e.parentDefault)
                        e.parentDefault();
                    else
                        return false;
                    return false;
                }
            });
        });
    </script>

    </form>
</body>
</html>
