<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelInput.aspx.cs" Inherits="UserPublicCenter.AirTickets.history.ExcelInput" MasterPageFile="~/MasterPage/AirTicket.Master" %>

<asp:Content ContentPlaceHolderID="c1" ID="cntOrderManage" runat="server">
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
    #txtContent
    {
        width: 140px;
    }
</style>
    <div class="sidebar02_con">
	<!--sidebar02_con_table01 start-->
   	  <div class="sidebar02_con_table01">

      	<table  width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA" style="border:1px #CCCCCC solid;">
  <tbody><tr>
            <td align="left" height="5"></td>
          </tr>
              <tr>
                <td height="30" align="left"id="tdSelct1">
                   <strong>请选择导入的excel文件：</strong>
                   <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="Button1" runat="server" Text="上传" onclick="Button1_Click" /> 
                </td>
                <td width="24%" align="left"><font color="#FF0000"> 备注：导入的excel文件必须是标准的excel文件。</font></td>
              </tr>
                <tr>
                <td height="10"></td>
              </tr>
  	    </tbody></table>
      </div>
      <!--sidebar02_con_table01 end-->
      <!--sidebar02_con_table02 start-->
      <div class="sidebar02_con_table02" id="divhistoryList">      		
      </div>
      <!--sidebar02_con_table02 end-->
       </div>       
    <!-- sidebar02_con end-->
</asp:Content>