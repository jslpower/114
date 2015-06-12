<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetResourcesList.aspx.cs" Inherits="UserBackCenter.EShop.SetResourcesList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload" TagPrefix="uc1" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>旅游资源推荐列表</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />     
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin:3px 0 3px 0;">
        <tr>
            <td width="80" style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
             <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqupc.gif" align="center">
                <a href="SetResources.aspx">旅游资源推荐添加</a></td>
            <td width="113" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/quanziyqonc.gif"  align="center"><a href="SetResourcesList.aspx">
                旅游资源推荐列表</a></td>
            <td style="border-bottom:1px solid #8BA2BE;">&nbsp;</td>
          </tr>
        </table>
       <asp:Repeater ID="rptResources" runat="server">
             <HeaderTemplate>              
            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
              <tr>
                <td width="7%" class="hang">序号</td>
                <td width="55%" class="hang">标题</td>
                <td width="18%" class="hang">发布时间</td>
                <td width="20%" class="hang">操作</td>
              </tr>             
            </HeaderTemplate>
            <ItemTemplate>
                 <tr>
                    <td align="center" height="26"><span class="right1">
                      <%# (Container.ItemIndex + 1) + (CurrencyPage - 1) * intPageSize%>
                    </span></td>
                    <td align="left"><%#Eval("Title") %></td>
                    <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%></td>
                    <td align="center">
                        <a href="SetResources.aspx?Reso_Id=<%#Eval("Id")%>">修改</a>
                      <a href='javascript:void(0)' onclick="setTop('<%#Eval("IsTop") %>','<%#Eval("Id") %>')"><%#Convert.ToBoolean(Eval("IsTop")) == true ? "取消置顶" : "置顶"%></a>
                        <a href="javascript:void(0)" onclick="<%# Eval("Id","deleteData('{0}')") %>">删除</a>
                     </td>            
                      <asp:HiddenField ID="hdfIsTop" runat="server" Value='<%# Eval("IsTop") %>' />
                  </tr>            
            </ItemTemplate>
            <FooterTemplate>
                </table>            
            </FooterTemplate>
        </asp:Repeater>
   
    <asp:Panel ID="pnlNoData" runat="server" Visible="false">
        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
          <tr>
            <td width="7%" class="hang">排序</td>
            <td width="55%" class="hang">标题</td>
            <td width="18%" class="hang">发布时间</td>
            <td width="20%" class="hang">操作</td>
          </tr>             
        </table>
      <div id="div_NoDataMessage"style="text-align:center;  margin-top:75px; margin-bottom:75px;">暂无<span lang="zh-cn">旅游资源推荐信息</span></div>
    </asp:Panel>
       <cc2:ExportPageInfo ID="ExportPageInfo1"  LinkType="4"  runat="server"></cc2:ExportPageInfo>
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
             window.location.href="/EShop/SetResourcesList.aspx";
            }        
         }   
          //置顶操作
       function setTop(istop,reso_id)
       {  
            $.ajax
            ({  
                url:"AjaxListIsTop.aspx?Id="+reso_id+"&DataType=Resources&IsTop="+istop,
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
       function deleteData(reso_id)
       {  
         if(!confirm("你确定要删除该条数据吗？"))
         {
           return;
         }
        $.ajax
        ({  
            url:"AjaxListDelete.aspx?Id="+reso_id+"&DataType=Resources",
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
