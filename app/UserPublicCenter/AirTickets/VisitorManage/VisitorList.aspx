<%@ Page Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="VisitorList.aspx.cs" Inherits="UserPublicCenter.AirTickets.VisitorManage.VisitorList"  Title="常旅客查询/修改_【频道名称：机票】"%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="c1">
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script> 
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>  
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
<style type="text/css">
/*FENYE*/
DIV.digg {	PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; margin-top:10px; PADDING-TOP: 3px; TEXT-ALIGN: center
}
DIV.digg A {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #54A11C; PADDING-TOP: 2px;TEXT-DECORATION: none
}
DIV.digg A:hover {	BORDER: #54A11C 1px solid; background:#54A11C; COLOR: #fff;}
DIV.digg A:active {	BORDER: #54A11C 1px solid;  COLOR: #000; }
DIV.digg SPAN.current {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 2px; BACKGROUND-COLOR: #54A11C}
DIV.digg SPAN.disabled {	BORDER: #eee 1px solid; PADDING-RIGHT: 5px;  PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #ddd; PADDING-TOP: 2px;}/*end*/
</style>
<div class="sidebar02_con_table02">
    <table width="100%" id="tb_SearchList" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
        <tr>
            <th height="35" colspan="9" align="center" bgcolor="#EDF8FC">
            <table width="80%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td align="center"></td>
                  <td align="center">姓名：
                    <input name="txtName" type="text" id="txtName" value="请输入姓名" size="20" /></td>
                  <td align="left">旅客类型：
                      <asp:DropDownList ID="ddlVisitorType" runat="server">
                      </asp:DropDownList>
                    </td>
                  <td align="center"><span style="padding-left:5px">
                  <a href="javascript:void(0)" id="ImgSearch" >
                  <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/jipiao/btn.jpg" /></span></a></td>
                </tr>
              </table></th>
          </tr>
     </table>
     <div id="divVisitorList" align="center"></div>
</div>   
<style type="text/css">
.text_style{color:#CCCCCC; font-size: 12px;}
</style>
<script type="text/javascript">
$(document).ready(function(){
    $("#txtName").val("请输入姓名");
    $("#txtName").addClass("text_style");
    $("#txtName").focus(function(){
	    if($(this).val()=="请输入姓名"){
		    $(this).val("");
		    $(this).removeClass("text_style");
	    }
});
$("#txtName").blur(function(){
	if($(this).val()==""){
		$(this).val("请输入姓名");
		$("#txtName").addClass("text_style");
	}
});
});
</script>
 <script type="text/javascript">
         var Params={Name:"",Type:"",Page:1};
         var VisitorList=
         {
             GetVisitorList:function(){
                 LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
                 LoadingImg.ShowLoading("divVisitorList");
                 if(LoadingImg.IsLoadAddDataToDiv("divVisitorList")) {
                 $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url:"/AirTickets/VisitorManage/AjaxVisitorList.aspx",
                        data:Params,
                        cache: false,
                        success:function(html)
                        {
                            $("#divVisitorList").html(html);
                        },
                         error: function(xhr, s, errorThrow) {
                                $("#divVisitorList").html("未能成功获取响应结果");
                            }
                     });
                }
            },
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Params.Page = Page;
                this.GetVisitorList();
            }, 
            DeleteVisitorInfo:function(VisitorId)//删除
            {           
               if (confirm('您确定要删除此旅客信息吗？\n\n此操作不可恢复！'))
               {
                    $.ajax
                    ({
                        url: "/AirTickets/VisitorManage/AjaxVisitorList.aspx?DeleteId=" + VisitorId,
                        cache: false,
                        success: function(html) {
                            if(html=="False")
                            {
                                alert("删除失败！");
                                return false;
                            }else{
                                alert("删除成功");
                                VisitorList.GetVisitorList();
                            }
                         }
                     });
                 }
            },
            OnSearch:function(){   //查询         
                Params.Type = $("#<%=ddlVisitorType.ClientID %>").val();           
                Params.Name = $("#txtName").val();                
                Params.Page=1;
                VisitorList.GetVisitorList();
            }    
         };
         $(document).ready(function(){
      
             LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            
             $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    VisitorList.OnSearch();
                    return false;
                }
                });
        
            VisitorList.GetVisitorList();
            $("#ImgSearch").click(function(){              
                var cardType=$("#<%=ddlVisitorType.ClientID %>").val();              
                VisitorList.OnSearch();               
            });            
        });      
</script>      
</asp:Content>

