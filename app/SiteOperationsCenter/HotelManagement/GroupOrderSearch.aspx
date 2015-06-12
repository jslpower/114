<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupOrderSearch.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.GroupOrderSearch" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>团队订单查询</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>  
        <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang" id="tbList">
          <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 采购用户名</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>入住时间</strong></td>
            <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>离店时间</strong></td>
            <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>地址位置要求</strong></td>
            <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>是否有指定酒店</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>星级</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>房型</strong></td>
            <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>房型数量</strong></td>
            <td width="3%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>人数</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>团房预算</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>宾客类型</strong></td>
            <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>团队类型</strong></td>
            <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>提交时间</strong></td>
            <td width="11%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>备注要求</strong></td>
            <td colspan="2" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>回复记录</strong></td>
          </tr>
        <cc1:CustomRepeater ID="crpOrderList" runat="server">
            <ItemTemplate>
              <tr bgcolor="#F3F7FF"onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
                <td height="25" align="center"><%#GetCgsCompanyInfo(Convert.ToString(Eval("CompanyId")))%></td>
                <td height="25" align="center"><%#Eval("LiveStartDate", "{0:yyyy-MM-dd}")%></td>
                <td align="center"><%#Eval("LiveEndDate", "{0:yyyy-MM-dd}")%></td>
                <td align="center"><%#Eval("LocationAsk")%></td>
                <td align="center"><%#Eval("HotelName")%></td>
                <td align="center"><%# HotelLevelInit(Convert.ToString(Eval("HotelStar")))%></td>
                <td align="center"><%#Eval("RoomAsk")%></td>
                <td align="center"><%#Eval("RoomCount")%></td>
                <td align="center"><%#Eval("PeopleCount")%></td>
                <td align="left">￥<%#EyouSoft.Common.Utils.GetMoney(Convert.ToDecimal(Eval("BudgetMin"))) + " - " + EyouSoft.Common.Utils.GetMoney(Convert.ToDecimal(Eval("BudgetMax")))%></td>
                <td align="center"><%#Convert.ToInt32(Eval("GuestType"))==0?"内宾":"外宾"%></td>
                <td align="center"><%#Eval("TourType")%></td>
                <td align="center"><%#Eval("IssueTime", "{0:yyyy-MM-dd}")%></td>
                <td align="center">
              <%# GetRemark(Convert.ToString(Eval("OtherRemark")), Convert.ToString(Eval("Id")))%>
	            </td>
                <td width="4%" align="center"><a href="javascript:void(0)"><div onclick="GroupOrderSearch.ShowReverList(this,'<%#Eval("Id") %>')"><img src="<%=ImageServerUrl %>/images/yunying/xia.gif" width="9" height="6" />显示</div></a></td>
                <td width="3%" align="center"><a href="javascript:void(0)" onclick="GroupOrderSearch.AddRever('<%#Eval("Id") %>')"> 添加</a></td>
                </tr>
                <tr onMouseOver="mouseovertr(this)" onMouseOut="mouseouttr(this)" remarkOne="<%#Eval("Id") %>" style=" display:none;"><td colspan="16" align="center" >
                </td></tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
                <td height="25" align="center"><%#GetCgsCompanyInfo(Convert.ToString(Eval("CompanyId")))%></td>
                <td height="25" align="center"><%#Eval("LiveStartDate", "{0:yyyy-MM-dd}")%></td>
                <td align="center"><%#Eval("LiveEndDate", "{0:yyyy-MM-dd}")%></td>
                <td align="center"><%#Eval("LocationAsk")%></td>
                <td align="center"><%#Eval("HotelName")%></td>
                <td align="center"><%# HotelLevelInit(Convert.ToString(Eval("HotelStar")))%></td>
                <td align="center"><%#Eval("RoomAsk")%></td>
                <td align="center"><%#Eval("RoomCount")%></td>
                <td align="center"><%#Eval("PeopleCount")%></td>
                <td align="left">￥<%#EyouSoft.Common.Utils.GetMoney(Convert.ToDecimal(Eval("BudgetMin"))) + " - " + EyouSoft.Common.Utils.GetMoney(Convert.ToDecimal(Eval("BudgetMax")))%></td>
                <td align="center"><%#Convert.ToInt32(Eval("GuestType"))==0?"内宾":"外宾"%></td>
                <td align="center"><%#Eval("TourType")%></td>
                <td align="center"><%#Eval("IssueTime", "{0:yyyy-MM-dd}")%></td>
                <td align="center">
              <%# GetRemark(Convert.ToString(Eval("OtherRemark")), Convert.ToString(Eval("Id")))%>
	            </td>
                <td width="4%" align="center"><a href="javascript:void(0)"><div onclick="GroupOrderSearch.ShowReverList(this,'<%#Eval("Id") %>')"><img src="<%=ImageServerUrl %>/images/yunying/xia.gif" width="9" height="6" />显示</div></a></td>
                <td width="3%" align="center"><a href="javascript:void(0)" onclick="GroupOrderSearch.AddRever('<%#Eval("Id") %>')"> 添加</a></td>
                </tr>
                <tr  onMouseOver="mouseovertr(this)" onMouseOut="mouseouttr(this)"  remarkOne="<%#Eval("Id") %>" style="display:none;">
                <td  colspan="16" align="center">
                </td></tr>
            </AlternatingItemTemplate>
        </cc1:CustomRepeater>          
          <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 采购用户名</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>入住时间</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>离店时间</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>地址位置要求</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>是否有指定酒店</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>星级</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>房型</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>房型数量</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>人数</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>团房预算</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>宾客类型</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>团队类型</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>提交时间</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>备注要求</strong></td>
            <td colspan="2" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>回复记录</strong></td>
          </tr>
        </table>

          
        <table width="99%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="30" align="right">
                <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" />
              </td>
          </tr>
        </table>
	      
    </div>
    </form>
    <script type="text/javascript">
        var parms = { "page": 1, "id": 0 };
      function mouseovertr(o) {
	      o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
      }
      function mouseouttr(o) {
	      o.style.backgroundColor=""
      }
        $(document).ready(function(){
            $()
        });
        var GroupOrderSearch = {
            GetAllReamark: function(orderid) {  //备注隐藏|显示
                $("#tbList td").find("span[remark]").each(function() {
                    if ($(this).attr("remark") == "remark" + orderid) {
                        var display = $(this).css("display");
                        if (display == "none") {
                            $(this).css("display", "block");
                            $(this).next().children().attr("src", "<%=imagePath %>/images/yunying/ns-expand2.gif");
                        } else {
                            $(this).css("display", "none");
                            $(this).next().children().attr("src", "<%=imagePath %>/images/yunying/ns-expand.gif");
                        }
                    }
                });
            },
            ShowReverList: function(obj, id)  //获取订单回复列表
            {
                var showstr = "<img src='<%=imagePath %>/images/yunying/xia.gif' />显示";
                var hiddenstr = "▲隐藏";
                var currentstr = $(obj).html();
                if (currentstr == hiddenstr) {
                    $(obj).html(showstr);
                } else {
                    $(obj).html(hiddenstr);
                }
                var tr = $(obj).parent().parent();
                if (currentstr == hiddenstr) {
                    $("#tbList tr[remarkOne]").each(function() {
                        if ($(this).attr("remarkOne") == id)
                            $(this).hide();

                    });
                } else {
                    parms.id = id;
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxRevertInfo.aspx",
                        data: parms,
                        cache: false,
                        async: false,
                        success: function(html) {
                            $("#tbList tr[remarkOne]").each(function() {
                                if ($(this).attr("remarkOne") == id) {
                                    $(this).children().html(html);
                                    $(this).show();
                                }
                            });
                            //  tr.after("<tr remarkOne='"+id+"'><td colspan='16'>"+ html+"</td></tr>");
                        },
                        error: function() {
                            //tr.after("<tr remarkOne='"+id+"'><td colspan='16'>未能成功获取响应结果</td></tr>");
                            $("#tbList tr[remarkOne]").each(function() {
                                if ($(this).attr("remarkOne") == id) {
                                    $(this).children().html("未能成功获取响应结果");
                                }
                            });
                        }
                    });
                }
            },
            AddRever: function(tourid) {  //添加回复
                Boxy.iframeDialog({ title: "添加回复", iframeUrl: "AddRever.aspx?tid=" + tourid, width: document.documentElement.clientWidth - 500, height: "350", draggable: true, data: null });
                // openDialog("AddRever.aspx", "添加回复", document.documentElement.clientWidth-200, "380", null);
            },
            LoadData: function(obj, tid) {//分页
                var Page = exporpage.getgotopage(obj);
                $.ajax({
                    type: "GET",
                    dataType: 'html',
                    url: "AjaxRevertInfo.aspx?id="+tid+"&Page="+Page,
                    cache: false,
                    success: function(html) {
                        $("#tbList tr[remarkOne]").each(function() {
                            if ($(this).attr("remarkOne") == tid) {
                                $(this).children().html(html);
                                $(this).show();
                            }
                        });
                    },
                    error: function() {
                        $("#tbList tr[remarkOne]").each(function() {
                            if ($(this).attr("remarkOne") == tid) {
                                $(this).children().html("未能成功获取响应结果");
                            }
                        });
                    }
                });
            }
        }
    </script>
</body>
</html>
