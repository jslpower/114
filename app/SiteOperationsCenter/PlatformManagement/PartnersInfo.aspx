<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartnersInfo.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.PartnersInfo" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>战略合作伙伴</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <%--<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
   <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>--%>

    <script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor="";
  }
//  function opendialog()
//  {
//    
//  }

//取被选中的Id
function GetChkValue()
{
    var arr=new Array();
    var jQueryObj=$("#rpt_PartList tr input[type='checkbox']");
    jQueryObj.each(function(i){
        var parentObj=$(this).parent();
        var checkBoxValue=parentObj.attr("InnerValue");
        if(this.checked&&i>0&&i<jQueryObj.length)
	    {				
		    arr.push([checkBoxValue,this]);
	    }
       
})
return arr; 
}
//列表数据修改链接
function OnEditSubmit()
{   
  
	var Url="OperatorPartners.aspx?EditID=";
	var arr=GetChkValue();
	
	if(arr.length==0)
	{
		alert("未选择数据");	
	}
	if(arr.length>1)
	{
		alert("只能选择一条数据");	
	}
	else if(arr.length==1)
	{
	  var str=Url+arr[0][0];
	  window.location.href=Url+arr[0][0];
	}
}
//列表数据删除
function OnDeleteSubmit()
{
	var arr=GetChkValue();
	if(arr.length==0)
	{
		alert("未选择数据");
		return false;
	}
	else
	{ 
		return confirm('您确定要删除这条数据吗？\n此操作不可恢复');
	}
}     function CheckAll(obj)
        {	
            var ck=document.getElementsByTagName("input");
            for(var i=0 ;i<ck.length;i++)
             {
                if(ck[i].type=="checkbox")
                {
                 ck[i].checked=obj.checked;
	        } 	
	       }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="30%" height="25" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageServerUrl%>/images/yunying/ge_da.gif" width="3" height="20" />
                        </td>
                        <td width="11%">
                        <a href="OperatorPartners.aspx">
                                <img src="<%=ImageServerUrl%>/images/yunying/xinzeng.gif" width="49" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="2%">
                            <img src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="11%">
                                <img src="<%=ImageServerUrl%>/images/yunying/xiugai.gif"  width="50" height="25" border="0" style="cursor: hand" onclick="OnEditSubmit();"  />
                        </td>
                        <td width="2%">
                            <img src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="11%">
                         <asp:ImageButton runat="server" border="0"  ID="hyDelItem"
                                Width="51" Height="25" onclick="hyDelItem_Click" />
                               
                        </td>
                        <td width="2%">
                            <img src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="11%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            <img src="<%=ImageServerUrl%>/images/yunying/ge_d.gif" width="11" height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="70%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif" align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rpt_PartList" runat="server" 
        onitemdatabound="rpt_PartList_ItemDataBound">
        <HeaderTemplate>
            <table width="98%" id="rpt_PartList" border="0" align="center" cellpadding="0" cellspacing="1"
                class="kuang">
                <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong><input type="checkbox" onclick="CheckAll(this)" />序号</strong>
                    </td>
                    <td width="30%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>图片</strong>
                    </td>
                    <td width="38%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>文字</strong>
                    </td>
                </tr>
        </HeaderTemplate>
        <AlternatingItemTemplate>
            <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center">
                    <asp:CheckBox runat="server" ID="cbListId"></asp:CheckBox>
                    <asp:Label runat="server" ID="lblItemID" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'
                        Visible="false"></asp:Label>
                </td>
                <td align="center">
                  <a target="_blank" href="<%=imgUrl %><%# DataBinder.Eval(Container.DataItem, "ImgPath")%>" > <img  width="100px" height="30px"  src="<%=imgUrl %><%# DataBinder.Eval(Container.DataItem, "ImgPath")%>"  /></a>
                </td>
                <td height="25" align="center">
                    <%#DataBinder.Eval(Container.DataItem, "LinkName")%>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <ItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center">
                    <asp:CheckBox runat="server" ID="cbListId"></asp:CheckBox>
                    <asp:Label runat="server" ID="lblItemID" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'
                        Visible="false"></asp:Label>
                </td>
                <td height="25" align="center">
                    <a target="_blank" href="<%=imgUrl %><%# DataBinder.Eval(Container.DataItem, "ImgPath")%>" > <img width="100px" height="30px" src="<%=imgUrl %><%# DataBinder.Eval(Container.DataItem, "ImgPath")%>" /></a>
                </td>
                <td align="center">
                    <%# DataBinder.Eval(Container.DataItem, "LinkName")%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white" height="23">
                <td align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong><input type="checkbox" onclick="CheckAll(this)" />序号</strong>
                </td>
                <td align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>图片</strong>
                </td>
                <td align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>文字</strong>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="NoData" runat="server" Visible="false">
         <table width="98%"  border="1" align="center" cellpadding="0" cellspacing="1"
                class="kuang">
                <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong><input type="checkbox" onclick="CheckAll(this)" />序号</strong>
                    </td>
                    <td width="30%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>图片</strong>
                    </td>
                    <td width="38%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>文字</strong>
                    </td>
                </tr>
                <tr align="center" height="150px">
                <td colspan="3">暂无数据</td>
                </tr>
        </table>
    </asp:Panel>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo2" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
