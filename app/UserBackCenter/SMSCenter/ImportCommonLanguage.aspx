<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportCommonLanguage.aspx.cs"
    Inherits="UserBackCenter.SMSCenter.ImportCommonLanguage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>常用短语</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" name="form1">
    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0">
    <tr>
        <td colspan="3">
        <asp:RadioButtonList runat="server" ID="rblTypes" AutoPostBack="true"  
                RepeatDirection="Horizontal" 
                onselectedindexchanged="rblTypes_SelectedIndexChanged" >
            <asp:ListItem  Text="自定义" Value="0"></asp:ListItem>
            <asp:ListItem  Text="系统默认" Value="1"></asp:ListItem>
        </asp:RadioButtonList> 
        </td>
    </tr>
    <tr>
        <td width="10%" align="center" bgcolor="#C5DCF5">
            <span id="Span1">序号</span>
        </td>
        <td width="18%" align="center" bgcolor="#C5DCF5">
            类型
        </td>
        <td width="76%" align="center" bgcolor="#C5DCF5">
            常用语
        </td>
    </tr>
    <asp:Repeater runat="server" ID="Recommounse" OnItemDataBound="Recommounse_ItemDataBound">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <input type="checkbox" name="chkContentList" id="chkContentList" value='<%# DataBinder.Eval(Container.DataItem,"Content")%>{~|}<%#DataBinder.Eval(Container.DataItem,"TemplateId") %>'
                        onclick="checkChange(this);" />
                    <asp:Label ID="lblContentID" runat="server"></asp:Label>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CategoryName")%>
                </td>
                <td>
                    <%#GetContent(Convert.ToString(DataBinder.Eval(Container.DataItem,"Content")))%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="NoDataInfo" runat="server" Visible="false">
            <tr>
                <td align="center" colspan="17" height="100">
                    暂无短语类型信息
                </td>
            </tr>
    </asp:Panel>
    </table>
    <div id="div_Expage" runat="server" style="text-align:right">
         <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
    </div>
    <table width="100%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr align="left">
            <td width="62%" class="shenghui">
                <a href="javascript:checkAll(0)" style="cursor: hand">全选</a>&nbsp;&nbsp;&nbsp;<a
                    href="javascript:checkAll(1)" style="cursor: hand">反选</a>&nbsp;&nbsp;&nbsp;<a href="javascript:checkAll(2)"
                        style="cursor: hand">清空</a>
                <input name="Submit" type="button" value="确定" onclick="saveValue();" />&nbsp;&nbsp;<input name="Submit" type="button" value="取消" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" />
            </td>
        </tr>
    </table>
    <div style="clear: both">
    </div>
    </form>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script language="javascript" type="text/javascript">
		//删除数组指定下标的元素
		Array.prototype.remove = function(dx)
		{
			if(isNaN(dx)||dx> this.length) { return false;}
			for(var i=dx;i<this.length;i++)
			{
				this[i]=this[i+1]
			}
			this.length-=1
		}	
		
		//改变选中
		function checkChange(obj)
		{
			var arr = new Array();
			var arrID = new Array();
			var oid = -1;
			arr =$(parent.document).find("#ContentList").val().split(',');
			arrID =$(parent.document).find("#hidContentID").val().split(',');
			for(var c = 0; c < arrID.length; c ++)
			{
				if($(obj).val().split('{~|}')[1] == arrID[c])
					oid = c;
			}
			if(obj.checked)
			{
				if(oid == -1)
				{
					arrID.push($(obj).val().split('{~|}')[1]);
					arr.push($(obj).val().split('{~|}')[0]);
				}
			}
			else
			{
				if(oid != -1)
				{
					arrID.remove(oid);
					arr.remove(oid);
				}
			}
			
			 $(parent.document).find("#ContentList").val(arr.toString());
			 $(parent.document).find("#hidContentID").val(arrID.toString());
		}
		
		//保存选中的值
		function saveValue() {
		    var arr = new Array();
		    arr = $(parent.document).find("#ContentList").val().split(',');
		    var tmpVal = "";
		    if (arr != null && arr.toString() != "") {
		        for (var i = 0; i < arr.length; i++) {
		            if (arr[i] != "")
		                tmpVal += arr[i] + ',';
		        }
		        if (tmpVal != "") {
		            if (parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_SendContent")) {
		                if (parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_SendContent").value != "")
		                    parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_SendContent").value += "," + tmpVal.substring(0, tmpVal.length - 1);
		                else {
		                    parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_SendContent").value = tmpVal.substring(0, tmpVal.length - 1);
		                }

                        //短信字数统计
		                parent.SendSMS.GetContentNum();
		                //设置短信发送内容针对不同接入商的发送条数统计信息
		                parent.SendSMS.setFactCountHtml();
		            }
		            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
		        }
		    }
		    else {
		        alert("请选择常用语！");
		    }
		}
		
  $(document).ready(function(){
   var element = document.getElementById("form1").elements;
			var element = document.form1.elements;
			var arr = new String();
			var arrID = new String();
			arr = $(parent.document).find("#ContentList").val().split(',');
			arrID =$(parent.document).find("#hidContentID").val().split(',');
			for(var i = 0; i < element.length; i ++)
			{
				if(element[i].type == 'checkbox')
				{
					for(var c = 0; c < arrID.length; c ++)
					{
						if(arrID[c] != "")
						{
							if($(element[i]).val().split('{~|}')[1] == arrID[c])
								element[i].checked = true;
						}
					}
				}
			}
    })
    function checkAll(type)// 全选 反选 清空
    {
        var element = document.getElementById("form1").elements;
        var arr = new Array();
        var arrID = new Array();
        arr = $(parent.document).find("#ContentList").val().split(',');
        arrID = $(parent.document).find("#hidContentID").val().split(',');
        for (var i = 0; i < element.length; i++) {
            if (element[i].type == 'checkbox') {
                var oid = -1;
                for (var j = 0; j < arrID.length; j++) {
                    if ($(element[i]).val().split('{~|}')[1] == arrID[j])
                        oid = j;
                }
                if (oid == -1) {
                    if (type == '0' || type == '1') {
                        element[i].checked = true;
                        arrID.push($(element[i]).val().split('{~|}')[1]);
                        arr.push($(element[i]).val().split('{~|}')[0]);
                    }
                }
                else {
                    if (type == '1' || type == '2') {
                        element[i].checked = false;
                        arrID.remove(oid);
                        arr.remove(oid);
                    }
                }
            }
        }
        $(parent.document).find("#ContentList").val(arr.toString());
        $(parent.document).find("#hidContentID").val(arrID.toString());
    }	
    </script>

</body>
</html>
