<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EshopDueDate.aspx.cs" Inherits="SiteOperationsCenter.Statistics.EshopDueDate" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="/usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>
<%@ Register src="/usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>高级网店到期统计</title>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>  
    <style type="text/css">
        .style1
        {
            width: 155px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxunbg.gif">
          
              <uc2:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
          
            单位名称
            <input name="txtcompanyName" id="txtcompanyName" type="text" class="style1" 
                  size="12" />

              <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />

        <a href="javascript:void(0)" id="imgSelect"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px;" /></a></td>
        </tr>
      </table>
      <div id="divlist" align="center"> 
      
      </div>
      <div id="light" class="white_content" style="text-align:right;">
  	    <a href = "javascript:void(0)" id="closeform">关闭</a>
  	        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr> 
                <td align="left">&nbsp;<uc1:StartAndEndDate ID="StartAndEndDate2" runat="server" /></td>
              </tr>
              <tr>
                <td align="left">审 核 人 ：<input name="operUser" type="text" class="textfield" id="OpeerUser" size="15"  readonly="readonly"/></td>
              </tr>
               <tr>
                 <td align="left">审核时间 ：<input name="operTime" type="text" class="textfield" size="15" id="OperTime" readonly="readonly"/></td>
               </tr>
               <tr>
               <td align="center"><input type="button" id="btnSureMoney" value="确认续费" /></td> 
              </tr>
            </table>
            <input type="hidden" id="hidCompayId" />
            <input type="hidden" id="hidApplyId" />
    </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
       var ShopParms = { ProvinceId: 0, CityId: 0, CompanyName: "", StartDate: "", EndDate:"",Page: 1 };
       var EshopDueDate=       
       {
            GetShopList:function(){  //获取列表
                LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                LoadingImg.ShowLoading("divlist");
                if (LoadingImg.IsLoadAddDataToDiv("divlist")) {
                   $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url:"/Statistics/AjaxEshopList.aspx",
                        data:ShopParms,
                        cache: false,
                        success:function(html)
                        {
                            $("#divlist").html(html);
                        },
                        error:function()
                        {
                            $("#divlist").html("未能成功获取响应结果");
                        }
                   });
                }
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                ShopParms.Page = Page;
                this.GetShopList();
            },
            SelectShopInfo:function(){   //查询
                var dateControl=<%=StartAndEndDate1.ClientID %>;
                ShopParms.CityId=$("#ProvinceAndCityList1_ddl_CityList").val();
                ShopParms.ProvinceId=$("#ProvinceAndCityList1_ddl_ProvinceList").val();
                ShopParms.StartDate=dateControl.GetStartDate();
                ShopParms.EndDate=dateControl.GetEndDate();
                ShopParms.CompanyName=$("#txtcompanyName").val();
                ShopParms.Page=1;           
                EshopDueDate.GetShopList();           
            },
            getOffsetSize:function(obj) 
            {
                var objPosition={Top:0,Left:0}
                var offset = $(obj).offset();
                objPosition.Left=offset.left;
                objPosition.Top=offset.top+$(obj).height()+6;
                return objPosition;                
            },
            OpearAddMoney:function(obj)
            {
               $("#hidApplyId").val($(obj).attr("ApplyId"));
               $("#hidCompayId").val($(obj).attr("CompanyId"));
               $("#OpeerUser").val("<%=MasterUserInfo.UserName %>");
               $("#OperTime").val("<%=NowDate %>");
               var pos =EshopDueDate.getOffsetSize(obj);
               $("#light").show().css({ top: pos.Top + "px", left: (pos.Left - 290) + "px" });
               var AjaxDateControl=<%=StartAndEndDate2.ClientID %>;       
               AjaxDateControl.ClearText();  //清除日期
            }
        }
        $(document).ready(function(){
            EshopDueDate.GetShopList();
             $("#txtcompanyName").bind("keypress", function(e)
              {
                if (e.keyCode == 13) {
                $("#imgSelect").click(); 
                return false;
                }
            });
        
            $("#imgSelect").click(function(){
                EshopDueDate.SelectShopInfo();               
            });
            $("#btnSureMoney").click(function(){
                var AjaxDateControl=<%=StartAndEndDate2.ClientID %>;
                var startDate=AjaxDateControl.GetStartDate();
                var endDate=AjaxDateControl.GetEndDate();
                if(startDate==""  || endDate=="")
                {
                    alert("请选择到期时间");
                    return;
                }
//                else{
//                     var regu=/^(?:[01]?\d|2[0-3])(?::[0-5]?\d){2}$/;
//                     alert((regu.test(startDate) && regu.test(endDate)));
//                     if((regu.test(startDate) && regu.test(endDate))==false)
//                     {
//                        alert("到期时间填写错误！");
//                        return;
//                    }             
//                }

                var companyId=$("#hidCompayId").val();
                var applyId=$("#hidApplyId").val();
                var urls="EshopDueDate.aspx?Method=method&CompanyId="+companyId+"&StartDate="+startDate+"&EndDate="+endDate+"&ApplyId="+applyId;
                $.ajax({  
                    url:urls,
                    cache:false,
                    async:false,
                    dataType: "json",
                    success:function(msg)
                    {
                        if(msg.success=="1"){
                            alert(msg.message);
                             EshopDueDate.GetShopList();  
                        }else{
                           alert(msg.message);
                        }
                    },          
                    error:function()
                     {
                        alert("操作失败");
                     }
                });
                
                $("#light").hide();
            });
            $("#closeform").click(function(){
                $("#light").hide();
            });
        });
          function mouseovertr(o) {
              o.style.backgroundColor="#FFF9E7";
              //o.style.cursor="hand";
          }
          function mouseouttr(o) {
              o.style.backgroundColor=""
          }
    </script>    
</body>
</html>
