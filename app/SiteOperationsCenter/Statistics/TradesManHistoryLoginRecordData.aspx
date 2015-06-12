<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradesManHistoryLoginRecordData.aspx.cs" Inherits="SiteOperationsCenter.Statistics.TradesManHistoryLoginRecordData" %>
<%@ Register src="../usercontrol/StartAndEndDate.ascx" tagname="StartAndEndDate" tagprefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>零售商历史登录记录页页</title>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
       <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>
         <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
        <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="tb_SearchList" width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxunbg.gif">单位名称：
      <input id="txtCompanyName" name="txtCompanyName" type="text" class="textfield" size="15" />
      <uc1:StartAndEndDate ID="StartAndEndDate1" runat="server" />
       <a href="javascript:void(0)" id="ImgSearch"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxun.gif"  width="62" height="21" /></a></td>
  </tr>
</table>
<div  id="divHistoryRecordList" align="center"> 

</div>
    </div>
    <script language="javascript" type="text/javascript">
        var Params = {CompanyName: "", StartDate: "", EndDate:"",Page: 1 };
        var LoginRecordData =       
         {
            GetLoginRecordList:function(){  //获取列表
              LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                LoadingImg.ShowLoading("divHistoryRecordList");
                if (LoadingImg.IsLoadAddDataToDiv("divHistoryRecordList")) {
               $.ajax({
                    type: "GET",
                    dataType: 'html',
                    url:"/Statistics/AjaxTradesManHistoryLoginRecordList.aspx",
                    data:Params,
                    cache: false,
                    success:function(html)
                    {
                        $("#divHistoryRecordList").html(html);
                    },
                    error: function(xhr, s, errorThrow) {
                            $("#divHistoryRecordList").html("未能成功获取响应结果")
                        }
               });
               }
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Params.Page = Page;
                this.GetLoginRecordList();
            },
            SelectLoginRecordInfo:function(){   //查询
                var dateControl=<%=StartAndEndDate1.ClientID %>;    
                Params.Page=1;           
                Params.StartDate=dateControl.GetStartDate();
                Params.EndDate=dateControl.GetEndDate();
                Params.CompanyName=$("#txtCompanyName").val();           
                LoginRecordData.GetLoginRecordList();           
            }       
        }
        $(document).ready(function(){
          LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            
             $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    LoginRecordData.SelectLoginRecordInfo();
                    return false;
                }
                });
        
        
            LoginRecordData.GetLoginRecordList();
            $("#ImgSearch").click(function(){
                LoginRecordData.SelectLoginRecordInfo();               
            });
        });
          function mouseovertr(o) {
              o.style.backgroundColor="#FFF9E7";
              //o.style.cursor="hand";
          }
          function mouseouttr(o) {
              o.style.backgroundColor=""
          }      
        
            var LoginRecordList= {
            CompanyAccessDetail: function(CompanyId) {  
                LoginRecordList.openDialog("/Statistics/LoginRecord.aspx?companyid="+CompanyId+"&BeginTime="+Params.StartDate+"&EndTime="+Params.EndDate, "登录信息","600", "450", null);         
                return false;
            },
            openDialog: function(url, title, width, height) {
                Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: null });
            }
          }
          
          
    </script> 
    </form>
</body>
</html>
