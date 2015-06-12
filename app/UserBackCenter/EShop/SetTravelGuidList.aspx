<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTravelGuidList.aspx.cs" Inherits="UserBackCenter.EShop.SetTravelGuidList" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc2" %>
<%--<%@ Register Assembly="ExporPage" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>最新动态列表</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin:3px 0 3px 0;">
          <tr>
            <td width="80" style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
            <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqupc.gif" align="center"><a href="SetTravelGuid.aspx?GuideType=<%= GuideType %>&TypeId=<%= TypeId %>"><asp:Literal runat="server" ID="ltrEditTitle">目的地指南添加</asp:Literal></a></td>
            <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqonc.gif"  align="center"><a href="SetTravelGuidList.aspx?GuideType=<% = GuideType %>&TypeId=<%= TypeId %>"><asp:Literal runat="server" ID="ltrListTitle">目的地指南列表</asp:Literal></a></td>
            <td style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
          </tr>
        </table>
        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC" id="guidTable">
          <tr>
            <td width="5%" class="hang">序号</td>
            <td width="12%" class="hang">类别</td>
            <td width="53%" class="hang">标题</td>
            <td width="15%" class="hang">发布时间</td>
            <td width="15%" class="hang">操作</td>
          </tr>
         
          <asp:Repeater ID="guidRepeater" runat="server">
          <ItemTemplate>
          <tr>
            <td align="center" height="26"><span class="right1">
             <%# (Container.ItemIndex + 1) + (pageIndex - 1) * pageSize%>
            </span></td>
            <td align="center"><%# Eval("TypeID")%></td>
            <td align="left"><%#Eval("Title")%></td>
            <td align="center"><%#Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd HH:mm") %></td>
            <td align="center"><a href="SetTravelGuid.aspx?guid_Id=<%#Eval("Id")%>&GuideType=<%= GuideType %>&TypeId=<%= TypeId %>">修改</a> 
            <a href='javascript:void(0)' onclick="setTop('<%#Eval("IsTop") %>','<%#Eval("Id") %>')"><%#Convert.ToBoolean(Eval("IsTop")) == true ? "取消置顶" : "置顶"%></a>
            <a href="javascript:void(0)" onclick="<%# Eval("Id","deleteData('{0}')") %>">删除</a>
            </td>
          </tr>
          </ItemTemplate>
          </asp:Repeater>
        </table>
         <div id="div_NoDataMessage" runat="server" visible="false" style="text-align:center;  margin-top:75px; margin-bottom:75px;">暂无<span lang="zh-cn"></span>信息</div>
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
          <tr>
        <td>
                  <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>

        </td>
              
              
          </tr>
        </table>
    </div>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript">
    function IsIE6()
         {
            var ua = navigator.userAgent.toLowerCase();
            var msie;         //6.0|ie6.0
            if (window.ActiveXObject)
            {
                 msie= ua.match(/msie ([\d.]+)/)[1]
            }
            if(msie=="6.0")
            {
                 window.location.reload();
            }else
            {
                window.location.href = window.location.href;
            }        
         }   
   //置顶操作
   function setTop(istop,guid_id)
   {  
       $.ajax
        ({  
            url:"AjaxListIsTop.aspx?Id="+guid_id+"&DataType=Guide&IsTop="+istop,
            cache:false,
            async:false,
            success:function(msg)
            {
                 alert(msg);
                 IsIE6();
            },            
            error:function()
             {
                alert("操作失败");
             }
        });
      }
      //删除操作
   function deleteData(guid_id)
   {  
     if(!confirm("你确定要删除该条数据吗？"))
     {
       return;
     }
       $.ajax
        ({  
            url:"AjaxListDelete.aspx?Id="+guid_id+"&DataType=Guide",
            cache:false,
            async:false,
            success:function(msg)
            {
                 alert(msg);
                IsIE6();
            },            
            error:function()
             {
                alert("操作失败");
             }
        });
   }

</script>
    </form>
</body>
</html>
