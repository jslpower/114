<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQReNew.aspx.cs" Inherits="SiteOperationsCenter.Statistics.MQReNew" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="/usercontrol/StartAndEndDate.ascx"  TagName="date" TagPrefix="uc2"%>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td background="<%=ImageServerUrl %>/images/yunying/chaxunbg.gif">
                <uc1:pc id="mq_pcList" runat="server" ></uc1:pc>
                单位名称
                <input  type="text" class="textfield" size="12" id="mq_txtCompanyName" onkeyup="MQReNew.isEnter(event)"/>
                <uc2:date id="mq_date" runat="server" ></uc2:date>
                <a href="javascript:void(0);" onclick="return MQReNew.search()"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <div id="mq_companyList" style="text-align:center;">
  
    </div>
    </div>
    </form>
</body>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
<script language="JavaScript" type="text/javascript">
        var searchParams={province:"0",city:"0",companyname:"",oversdate:"",overedate:"",Page:""};
        var MQReNew = {
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            },
            //获取MQ审核列表
            getMqCompanyList: function() {
                LoadingImg.ShowLoading("mq_companyList");
                if (LoadingImg.IsLoadAddDataToDiv("mq_companyList")) {
                    $.ajax({
                        type: "GET",
                        dataType: "html",
                        url: "AjaxMqReNew.aspx?isajax=yes",
                        data: searchParams,
                        cache: false,
                        success: function(result) {
                            if (/^notLogin$/.test(result)) {
                                alert("对不起，你尚未登录请登录!");
                                return false;
                            }
                            $("#mq_companyList").html(result);
                        },
                        error:function()
                        {
                            $("#mq_companyList").html("未能成功获取响应结果");
                        }
                    });
                }
            },
            //查询操作
            search: function() {
                searchParams.Page=1;
                searchParams.province = $("#mq_pcList_ddl_ProvinceList").val();
                searchParams.city = $("#mq_pcList_ddl_CityList").val();
                searchParams.companyname = $("#mq_txtCompanyName").val();
                searchParams.oversdate = <%=mq_date.ClientID %>.GetStartDate();
                searchParams.overedate =<%=mq_date.ClientID %>.GetEndDate();
                MQReNew.getMqCompanyList();
            },
            //分页操作
            loadData: function(obj) {
                var pageIndex = exporpage.getgotopage(obj);
                searchParams.Page = pageIndex;
                MQReNew.getMqCompanyList();
            },
            //续费
            reNew: function(tarBtn, id) {
                var cTable = $(tarBtn).closest("table");
                var reNewData = { method: "reNew" };
                reNewData.id = id;
                reNewData.sonUserNum = cTable.find("input[name='mq_sonUserNum']").val();
                reNewData.expireDate = cTable.find("input[name='mq_expireDate']").val();
                reNewData.enableDate = cTable.find("input[name='mq_enableDate']").val();
                if (!/^\d+$/.test(reNewData.sonUserNum)) {
                    cTable.find("input[name='mq_sonUserNum']").next("span").html("请输入有效子账户数");
                    return;
                }
                $(tarBtn).attr("disabled", "disabled");
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: "MQReNew.aspx?isajax=yes",
                    data: reNewData,
                    cache: false,
                    success: function(result) {
                        if (/^notLogin$/.test(result)) {
                            alert("对不起，你尚未登录请登录!");
                            return false;
                        }
                        alert(result.message);
                        if(result.success=="1")
                        {
                          $(tarBtn).closest(".white_content").parent("td").prev("td").html(reNewData.expireDate);
                        }
                        $(tarBtn).closest("div").css("display", "none");
                        $(tarBtn).removeAttr("disabled");
                    },
                    error: function() {
                    alert("审核时发生未知错误!");
                     $(tarBtn).removeAttr("disabled");
                    }
                });
            },
            //打开审核框
            openCheck: function(tar_a) {
                if ("<%=haveUpdate %>" == "False") {
                    alert("对不起，你没有该权限!");
                    return false;
                }
                $(".white_content").css("display", "none");
                var divBox = $(tar_a).siblings("div.white_content");
                divBox.css("display", "block").css({ top: $(tar_a).position().top - 5, left: $(tar_a).position().left - 320 });
                return false;
            },
            //关闭
            closeCheck: function(tar_a) {
                $(tar_a).closest("div").css("display", "none");
                return false;
            },
            //验证输入子账户是否合法
            checkSonNum: function(tar_text) {
                if (!/^\d+$/.test($(tar_text).val())) {
                    $(tar_text).next("span").html("请输入有效子账户数");
                }
                else {
                    $(tar_text).next("span").html("");
                }
                return;
            },
             //判断是否按回车
              isEnter: function(event) {
                  event = event ? event : window.event;
                  if (event.keyCode == 13) {
                      MQReNew.search();
                  }
              }
        }
         
         $(document).ready(function(){
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
             MQReNew.getMqCompanyList();
         });


    </script>
</html>
