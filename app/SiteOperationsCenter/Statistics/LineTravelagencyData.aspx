<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineTravelagencyData.aspx.cs" Inherits="SiteOperationsCenter.Statistics.LineTravelagencyData" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="../usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>在线组团社</title>
     <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>   
       <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>   
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxunbg.gif">
    <table id="tb_SearchList" width="70%"  border="0" align="left" cellpadding="1" cellspacing="0" id="tb_Search">
      <tr>
        <td><table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="right" nowrap="nowrap">单位名称：</td>
              <td><div align="left">
                  <input id="txtCompanyName" name="txtCompanyName" runat="server" type="text" class="textfield" size="15" />
              </div></td>
            </tr>
        </table></td>
        <td>
        <table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="right" nowrap="nowrap">               
                 <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />                       
              </td>             
            </tr>
        </table>      
        </td>
        <td><table width="99%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td rowspan="2" align="right" nowrap="nowrap">负责人：</td>
              <td align="left"><input id="txtCommendName" runat="server" name="txtCommendName" type="text" class="textfield" size="10" />
              </td>
            </tr>
        </table></td>
        <td>
           <a href="javascript:void(0)" id="ImgSearch"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62" height="21" style="margin-bottom:-3px;" /></a>
   </td>
      </tr>
    </table></td>
  </tr>
</table>
<div id="divTravelagencyList" align="center"> 
  
</div>
    </div>    
    <script language="javascript" type="text/javascript">
        var Parms = { ProvinceId: 0, CityId: 0, CompanyName: "", CommendName: "", Page: 1 };
        var LineTravelagencyData=       
       {
       
            GetCompanyList:function(){  //获取列表
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                LoadingImg.ShowLoading("divTravelagencyList");
                if (LoadingImg.IsLoadAddDataToDiv("divTravelagencyList")) {
               $.ajax({
                    type: "GET",
                    dataType: 'html',
                    url:"/Statistics/AjaxLineTravelagencyList.aspx",
                    data:Parms,
                    cache: false,
                    success:function(html)
                    {
                        $("#divTravelagencyList").html(html);
                    },
                     error: function(xhr, s, errorThrow) {
                            $("#divTravelagencyList").html("未能成功获取响应结果")
                        }
               });
               }
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetCompanyList();
            },
            OnSearch:function(){   //查询           
                Parms.ProvinceId = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
                Parms.CityId = $("#ProvinceAndCityList1_ddl_CityList").val();               
                Parms.CompanyName = $.trim($("#<%=txtCompanyName.ClientID %>").val());
                Parms.CommendName = $.trim($("#<%=txtCommendName.ClientID %>").val());
                Parms.Page=1;
                LineTravelagencyData.GetCompanyList();
            }
        };        
        $(document).ready(function(){
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            
             $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    LineTravelagencyData.OnSearch();
                    return false;
                }
                });
        
            LineTravelagencyData.GetCompanyList();
            $("#ImgSearch").click(function(){
                LineTravelagencyData.OnSearch();               
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
                LoginRecordList.openDialog("/Statistics/LoginRecord.aspx?companyid="+CompanyId+"&BeginTime="+Parms.StartDate+"&EndTime="+Parms.EndDate, "登录信息","600", "450", null);         
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
