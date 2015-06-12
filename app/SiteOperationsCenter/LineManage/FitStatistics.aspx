<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FitStatistics.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.FitStatistics" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="search_bg">
                线路类型
                <asp:DropDownList ID="DropSearchLineType" runat="server" onchange="OnchangeWord(this.value,'GetLineByType')">
                </asp:DropDownList>
                专线：
                <asp:DropDownList ID="DropSearchLineId" runat="server" onchange="OnchangeWord(this.value,'GetCompanyByLine')">
                    <asp:ListItem Value="0">专线</asp:ListItem>
                </asp:DropDownList>
                专线商：
                <asp:DropDownList ID="dropBusinessLineId" runat="server">
                    <asp:ListItem Value="0">专线商</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="search_bg">
                出发时间：
                <input type="text" id="txtStartDate" name="StartDate" class="size55" style="width: 85px;"
                    onfocus="WdatePicker();" />至<input type="text" id="txtEndDate" name="EndDate" style="width: 85px;"
                        class="size55" onfocus="WdatePicker();" />
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxun.gif" width="62" height="21" onclick="FitStatistics.OnSearch()" />
            </td>
        </tr>
    </table>
    <div id="divFitStatistics" align="center">
    </div>
    <table width="98%" border="0" align="center">
        <tr>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript">
    
     var type=null;
        //获取选择
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[name='checkboxFit']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id += $(this).val()+"$";
                }
            });
            if (num == 0) {
                return 0;
            }
            else {
                return id;
            }
        }
    
        
    
        //Line1为专线（国内，国外，周边） line2为（所选专线区域） BusinessLine专线商 Departure为出发地
        var Parms = {Line1: -1,Line2:0,BusinessLine: 0,StartDate:"",EndDate:"",Page: 1 };
        var FitStatistics = {//列表
            GetFitStatisticsList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divFitStatistics");
                if (LoadingImg.IsLoadAddDataToDiv("divFitStatistics")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxFitStatistics.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divFitStatistics").html(html);
                        }
                    });
                }
            },
            
            
           

           ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            
            
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetFitStatisticsList();
            },
            OnSearch: function() {//查询
                Parms.Line1 = $("#<%=DropSearchLineType.ClientID%>").val();
                Parms.Line2=$("#<%=DropSearchLineId.ClientID%>").val();
                Parms.BusinessLine=$("#<%=dropBusinessLineId.ClientID%>").val();
                Parms.StartDate = $.trim($("#txtStartDate").val());
                Parms.EndDate = $.trim($("#txtEndDate").val());
                Parms.Page = 1;
                FitStatistics.GetFitStatisticsList();
            }
     }
        $(document).ready(function() {
            var FormObj = $("#<%=form1.ClientID%>");

            //回车查询FitStatistics.OnSearch();   
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    FitStatistics.OnSearch();
                    return false;
                }
            });
            FitStatistics.OnSearch();
        });        
    
    
    </script>

   

    <script type="text/javascript">
    
     //专线类型（国内国际周边等等）
        function OnchangeWord(v,t)
        {
             $.ajax({
                 url: "FitStatistics.aspx?action="+t+"&argument="+v,
                 cache: false,
                 type:"POST",
                 dataType:"json",
                 success: function(result) {
                     if(t=="GetLineByType"){
                         $("#<%=DropSearchLineId.ClientID %>").html("");
                         $("#<%=DropSearchLineId.ClientID %>").append("<option value=\"0\">专线</option>"); 
                         
                         var lista = result.tolist;               
                         for(var i=0;i<lista.length;i++)
                         {
                             $("#<%=DropSearchLineId.ClientID %>").append("<option value=\""+lista[i].AreaId+"\">"+lista[i].AreaName+"</option>"); 
                         }
                     }
                     else if(t=="GetCompanyByLine")
                     {
                         $("#<%=dropBusinessLineId.ClientID %>").html("");
                         $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\"0\">专线商</option>"); 
                         var listBusinessLineId = result.tolist;
                         for(var j=0;j<listBusinessLineId.length;j++)
                         {
                             $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\""+listBusinessLineId[j].ID+"\">"+listBusinessLineId[j].CompanyName+"</option>"); 
                         }
                     }
                 },
                 error: function() {
                     alert("操作失败!");
                 }
             });
        }
        
    </script>

</body>
</html>
