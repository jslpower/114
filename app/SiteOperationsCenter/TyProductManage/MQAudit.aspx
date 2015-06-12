<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQAudit.aspx.cs" Inherits="SiteOperationsCenter.TyProductManage.MQAudit" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <input id="mq_applyStartDate" type="text" class="textfield" style="width:70px;" onfocus="WdatePicker()"/>-<input id="mq_applyFinishDate" type="text" class="textfield"  style="width:70px;" onfocus="WdatePicker()"/>
                 <a href="javascript:void(0);" onclick="return MQAudit.search()"><img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
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
        var MQAudit={
             mouseovertr:function(o) {
	            o.style.backgroundColor="#FFF9E7";
             },
             mouseouttr:function(o) {
	            o.style.backgroundColor="";
             },
             //获取MQ审核列表
             getMqCompanyList:function(){
                  LoadingImg.ShowLoading("mq_companyList");
                  if (LoadingImg.IsLoadAddDataToDiv("mq_companyList"))
                  {
                      $.ajax({
                            type: "GET",
                            dataType: "html",
                            url: "AjaxMQAduit.aspx?isajax=yes",
                            data: searchParams,
                            cache: false,
                            success: function(result) {
                                if(/^notLogin$/.test(result))
                                {
                                  alert("对不起，你尚未登录请登录!");
                                  return false;
                                }
                                $("#mq_companyList").html(result);
                            }
                        });
                  }
             },
             //查询操作
             search:function(){
                 searchParams.province=$("#mq_pcList_ddl_ProvinceList").val();
                 searchParams.city=$("#mq_pcList_ddl_CityList").val();
                 searchParams.state=$("#mq_selState").val();
                 searchParams.companyname=$("#mq_txtCompanyName").val();
                 searchParams.applysdate=$("#mq_applyStartDate").val();
                 searchParams.applyfdate=$("#mq_applyFinishDate").val();
                 MQAudit.getMqCompanyList();
             },
             //分页操作
             loadData: function(obj) {
                var pageIndex = exporpage.getgotopage(obj);
                searchParams.Page =pageIndex;
                MQAudit.getMqCompanyList();
             },
             //审核
             audit:function(tarBtn,id,state){
                 var cTable=$(tarBtn).closest("table");
                 var auditData={method:"audit"};
                 auditData.applystate=state;
                 if($(tarBtn).val()=="修改")
                 {
                  auditData.applystate="3";
                 }
                 auditData.id=id;
                 auditData.operatorId=cTable.find("input[name='mq_auditId']").attr("sourceValue");
                 auditData.checkDate=cTable.find("input[name='mq_auditDate']").val();
                 auditData.sonUserNum=cTable.find("input[name='mq_sonUserNum']").val();
                 auditData.expireDate=cTable.find("input[name='mq_expireDate']").val();
                 auditData.enableDate=cTable.find("input[name='mq_enableDate']").val();
                 if(state!="2")
                 {
                    if(!/^\d+$/.test(auditData.sonUserNum))
                    {
                      cTable.find("input[name='mq_sonUserNum']").next("span").html("请输入有效子账户数");
                       return;
                    }
                 }
                 $.ajax({
                            type: "GET",
                            dataType: "json",
                            url: "MQAudit.aspx?isajax=yes",
                            data: auditData,
                            cache: false,
                            success: function(result) {
                                if(/^notLogin$/.test(result))
                                {
                                  alert("对不起，你尚未登录请登录!");
                                  return false;
                                }
                               alert(result.message);
                               $(tarBtn).closest("div").css("display","none");
                               if(result.success=="1")
                               {  
                                 if(auditData.applystate=="1"||auditData.applystate=="3")
                                 { 
                                    $(tarBtn).closest("#ama_parentTd").prev("td").html("审核通过");
                                 }
                                 else
                                 {
                                    $(tarBtn).closest("#ama_parentTd").prev("td").html("未通过");
                                 }
                               }
                             
                            },
                            error:function(){
                               alert("审核时发生未知错误!");
                            }
                       });
             },
             //打开审核框
             openCheck:function(tar_a){
                 if("<%=haveUpdate %>"=="False")
                 {
                    alert("对不起，你没有该权限!");
                    return false;
                 }
                 var state=$(tar_a).closest("td").prev("td").text();
                 $(".white_content").css("display","none");
                 var divBox= $(tar_a).siblings("div.white_content");
                 divBox.css("display","block").css({top:$(tar_a).position().top-5,left:$(tar_a).position().left-320});
                 if(/审核通过/.test(state))
                 {
                     divBox.find("input[value='审核不通过']").css("display","none").siblings("input[value='审核通过']").val("修改");
                     divBox.find("input[name='mq_enableDate']").attr("disabled","disabled");
                     divBox.find("input[name='mq_expireDate']").attr("disabled","disabled");

                 }
                 else if(/未通过/.test(state))
                 {  
                   divBox.find("input[value='审核不通过']").css("display","none");
                   divBox.find("input[name='mq_enableDate']").val('');
                   divBox.find("input[name='mq_expireDate']").val('');
                   divBox.find("input[name='mq_sonUserNum']").val('0');
                 }
                 else if(/未审核/.test(state))
                 { 
                   divBox.find("input[name='mq_sonUserNum']").val('0');
                   divBox.find("input[name='mq_enableDate']").val('');
                   divBox.find("input[name='mq_expireDate']").val('');
                   divBox.find("input[name='mq_auditId']").val($("#ma_hidden").attr("checkPeople"));
                   divBox.find("input[name='mq_auditDate']").val($("#ma_hidden").attr("checkDate"));
                 }
                  return false;
             },
             //关闭
              closeCheck:function(tar_a){
                   $(tar_a).closest("div").css("display","none");
                    return false;
              },
              //验证输入子账户是否合法
              checkSonNum:function(tar_text){
                 if(!/^\d+$/.test($(tar_text).val()))
                 {
                  $(tar_text).next("span").html("请输入有效子账户数");
                 }
                 else
                 {
                    $(tar_text).next("span").html("");
                 }
                 return;
              }
         }
         
         $(document).ready(function(){
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
             MQAudit.getMqCompanyList();
         });


    </script>
</body>
</html>
