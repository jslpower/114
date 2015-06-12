<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HighShopAuit.aspx.cs" Inherits="SiteOperationsCenter.TyProductManage.HighShopAuit" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title>无标题页</title>
 <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" name="form1" method="post" action="" runat="server">
    <input type="hidden" id="ma_hidden" checkPeople="<%=checkPeople %>" checkDate="<%=checkDate %>" />
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <uc1:pc id="mq_pcList" runat="server" ></uc1:pc>
                审核状态
                <select name="mq_selState" id="mq_selState">
                    <option value="">请选择</option>
                    <option  value="0">未审核</option>
                    <option  value="1">审核通过</option>
                    <option  value="2">未通过</option>
                </select>
                单位名称
                <input  type="text" class="textfield" size="12" id="mq_txtCompanyName" />
                申请时间
                <input id="mq_applyStartDate" type="text" class="textfield" size="9" onfocus="WdatePicker()"/>-<input id="mq_applyFinishDate" type="text" class="textfield" size="9" onfocus="WdatePicker()"/>
                 <a href="javascript:void(0);" onclick="return ShopAudit.search()"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <div id="mq_companyList" style="text-align:center;">
  
    </div>
   
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>  
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script language="JavaScript" type="text/javascript">
        var searchParams={province:"0",city:"0",state:"",companyname:"",applysdate:"",applyfdate:"",Page:""};
        var ShopAudit = {
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            },
            //获取审核列表
            getMqCompanyList: function() {
                LoadingImg.ShowLoading("mq_companyList");
                if (LoadingImg.IsLoadAddDataToDiv("mq_companyList")) {
                    $.ajax({
                        type: "GET",
                        dataType: "html",
                        url: "AjaxHighShopAuit.aspx",
                        data: searchParams,
                        cache: false,
                        success: function(result) {
                            $("#mq_companyList").html(result);
                        }
                    });
                }
            },
            //查询操作
            search: function() {
                searchParams.province = $("#mq_pcList_ddl_ProvinceList").val();
                searchParams.city = $("#mq_pcList_ddl_CityList").val();
                searchParams.state = $("#mq_selState").val();
                searchParams.companyname = $("#mq_txtCompanyName").val();
                searchParams.applysdate = $("#mq_applyStartDate").val();
                searchParams.applyfdate = $("#mq_applyFinishDate").val();
                ShopAudit.getMqCompanyList();
            },
            //分页操作
            loadData: function(obj) {
                var pageIndex = exporpage.getgotopage(obj);
                searchParams.Page = pageIndex;
                ShopAudit.getMqCompanyList();
            },
            //            DateControl:function(obj1,obj2)
            //            {
            //            alert("<%= MaxDate%>--<%= NowDate%>");
            //                $(obj1).focus(function(){
            //                    WdatePicker({
            //                        maxDate:"#F{$dp.$D(\'DatePicker2\')||\'<%= MaxDate %>\'}",
            //                        minDate:"<%= NowDate%>"                        
            //                    });
            //                });
            //                $(obj2).focus(function(){   
            //                    WdatePicker({
            //                         minDate:"#F{$dp.$D(\'DatePicker1\')||\'<%= NowDate %>\'}",
            //                       maxDate:"<%= MaxDate %>"
            //                    });
            //                });    
            //            },

            //审核
            audit: function(tarBtn, id, state) {
                var cTable = $(tarBtn).closest("table");
                var auditData = { method: "audit" };
                auditData.applystate = state;
                if ($(tarBtn).val() == "修改") {
                    auditData.applystate = "3";
                }
                auditData.id = id;
                auditData.operatorId = cTable.find("input[name='mq_auditId']").attr("sourceValue");
                auditData.checkDate = cTable.find("input[name='mq_auditDate']").val();
                auditData.expireDate = cTable.find("input[name='mq_expireDate']").val();
                auditData.enableDate = cTable.find("input[name='mq_enableDate']").val();
                auditData.shopCompanyId = cTable.find("input[name='mq_auditId']").attr("shopCompanyId");   //开通网店的公司Id
                auditData.cType = cTable.find("input[name='txtCType']").val();
                auditData.googleMapKey = cTable.find("input[name='txtGoogleMapKey']").val();
                if (cTable.find("tr[name='trDomainUrl']").length > 0) {
                    auditData.url = cTable.find("input[name='shop_url']").val();
                    if (state != "2") {
                        var regu = /\w+.(\w+|\w+\/)$/;
                        if (!regu.test(auditData.url)) {
                            alert("域名填写错误！");
                            return;
                        }
                    }
                }
                if ($(tarBtn).val() != "审核不通过") {
                    if (auditData.expireDate == "" || auditData.enableDate == "") {
                        alert("有效期的起始时间不能为空");
                        return;
                    }
                }

                if (auditData.cType == "4" && $.trim(auditData.googleMapKey) == "" && $.trim(auditData.url).toLowerCase().indexOf(".tongye114.com") < 0) {
                    if (!confirm("独立域名的景区网店不填写Google Map Key会导致网店地图不能正确显示!\n\n你确定不填写吗?")) return;
                }

                $.ajax
                    ({
                        type: "GET",
                        dataType: "json",
                        url: "HighShopAuit.aspx",
                        data: auditData,
                        cache: false,
                        success: function(result) {
                            $(tarBtn).closest("div").css("display", "none");
                            alert(result.message);
                            if (result.success == "1") {
                                if (auditData.applystate == "1" || auditData.applystate == "3") {
                                    $(tarBtn).closest("#ama_parentTd").prev("td").html("审核通过");
                                    ShopAudit.getMqCompanyList();
                                }
                                else {
                                    $(tarBtn).closest("#ama_parentTd").prev("td").html("未通过");
                                }
                                ShopAudit.getMqCompanyList();
                            }
                        },
                        error: function() {
                            alert("审核时发生未知错误!");
                        }
                    });
            },
            //打开审核框
            openCheck: function(tar_a) {
                if ("<%=IsUpdateGant %>".toLowerCase() == "false") {
                    alert("对不起，您没有审核的权限！");
                    return false;
                }

                var state = $(tar_a).closest("td").prev("td").text();
                $(".white_content").css("display", "none");
                var divBox = $(tar_a).siblings("div.white_content");
                divBox.css("display", "block").css({ top: $(tar_a).position().top - 5, left: $(tar_a).position().left - 320 });
                if (/审核通过/.test(state)) {
                    divBox.find("input[name='mq_enableDate']").attr("disabled", "disabled");
                    divBox.find("input[name='mq_expireDate']").attr("disabled", "disabled");
                    if (divBox.find("tr[name='trDomainUrl']").length < 1)  //供应商修改只有查看功能
                    {
                        divBox.find("input[value='审核不通过']").css("display", "none");
                        divBox.find("input[value='审核通过']").css("display", "none");
                    }
                    else {
                        divBox.find("input[value='审核不通过']").css("display", "none").siblings("input[value='审核通过']").val("修改");
                    }

                }
                else if (/未通过/.test(state)) {
                    divBox.find("input[value='审核不通过']").css("display", "none");
                    var obj1 = divBox.find("input[name='mq_enableDate']");
                    var obj2 = divBox.find("input[name='mq_expireDate']");

                    $(obj1).val('');
                    $(obj2).val('');
                    //                   ShopAudit.DateControl(obj1,obj2);
                    divBox.find("input[name='shop_url']").val(divBox.find("input[name='shop_url']").attr("domaintext"));
                }
                else if (/未审核/.test(state)) {
                    divBox.find("input[name='shop_url']").val(divBox.find("input[name='shop_url']").attr("domaintext"));
                    var obj1 = divBox.find("input[name='mq_enableDate']");
                    var obj2 = divBox.find("input[name='mq_expireDate']");
                    $(obj1).val('');
                    $(obj2).val('');
                    //                   ShopAudit.DateControl(obj1,obj2);
                    divBox.find("input[name='mq_auditId']").val($("#ma_hidden").attr("checkPeople"));
                    divBox.find("input[name='mq_auditDate']").val($("#ma_hidden").attr("checkDate"));
                }
                return false;
            },
            //关闭
            closeCheck: function(tar_a) {
                $(tar_a).closest("div").css("display", "none");
                return false;
            }
        }
         
         $(document).ready(function(){
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
             ShopAudit.getMqCompanyList();
         });
       

    </script>
</body>
</html>
