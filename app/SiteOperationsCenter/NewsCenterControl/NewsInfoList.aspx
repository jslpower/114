<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsInfoList.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.NewsInfoList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻中心列表页</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript" type="text/javascript">
      //页面成员方法
      var NewsInfoPage={
          //鼠标选中后的背景样式
          mouseovertr: function(o){
	            o.style.backgroundColor="#FFF9E7";
          },
          mouseouttr: function(o){
	            o.style.backgroundColor="";
          },
          //取被选中的Id  
          GetChecValue: function(){
              var arr=new Array();
              var jQueryObj=$("#rpt_NewsList tr input[type='checkbox']");
              jQueryObj.each(function(i){
                    var parentObj=$(this).parent();
                    var checkBoxValue=parentObj.attr("InnerValue");
                    if(this.checked&&i>0&&i<jQueryObj.length)
                    {				
                        arr.push([checkBoxValue,this]);
                    }
                })
              return arr; 
          },
          //列表数据批量删除操作
          OnDeleteSubmit: function(){
                var arr=NewsInfoPage.GetChecValue();
	            if(arr.length==0)
	            {
		            alert("未选择数据");
		            return false;
	            }
	            else
	            { 
		            return confirm('您确定要删除这些数据吗？\n此操作不可恢复');
	            }
          },
          //列表单条数据删除操作
          OnItemDeleteSubmit: function(deleteID){
	            if(confirm('您确定要删除这条数据吗？\n此操作不可恢复'))
	            {
		            window.location.href="NewsInfoList.aspx?DeleteID="+deleteID;
	            }
          },
          //全选
          CheckAll: function(obj){
                var ck=document.getElementsByTagName("input");
                for(var i=0 ;i<ck.length;i++)
                {
                    if(ck[i].type=="checkbox")
                    {
                        ck[i].checked=obj.checked;
	                } 	
	           }
          }
      };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="30%" height="25" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="11%">
                            <asp:ImageButton runat="server" border="0" ID="hyDelItem" Width="51" Height="25"
                                OnClick="hyDelItem_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="70%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif" align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rpt_NewsList" runat="server" OnItemDataBound="rpt_NewsList_ItemDataBound"
        OnItemCommand="rpt_NewsList_ItemCommand">
        <HeaderTemplate>
            <table width="100%" id="rpt_NewsList" border="1" cellspacing="0" cellpadding="0"
                style="border: 1px solid #ccc;">
                <tr style="background: #C0DEF3; height: 28px; text-align: center; font-weight: bold;">
                    <td width="8%">
                        <strong>
                            <input type="checkbox" onclick="NewsInfoPage.CheckAll(this)" />
                            序号</strong>
                    </td>
                    <td width="13%">
                        类别
                    </td>
                    <td width="45%">
                        标题
                    </td>
                    <td width="9%">
                        发布时间
                    </td>
                    <td width="8%">
                        查看次数
                    </td>
                    <td width="8%">
                        设置置顶
                    </td>
                    <td width="9%">
                        操作
                    </td>
                </tr>
        </HeaderTemplate>
        <AlternatingItemTemplate>
            <tr style="background: #fff; height: 24px; text-align: center;" onmouseover="NewsInfoPage.mouseovertr(this)"
                onmouseout="NewsInfoPage.mouseouttr(this)">
                <td height="25" align="center">
                    <asp:CheckBox runat="server" ID="cbListId"></asp:CheckBox>
                    <asp:Label runat="server" ID="lblItemID" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'
                        Visible="false"></asp:Label>
                </td>
                <td align="center">
                    <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "AfficheClass"))%>
                </td>
                <td align="left">
                    <%# GetTitle(Convert.ToString(DataBinder.Eval(Container.DataItem, "AfficheTitle")),Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID")))%>
                </td>
                <td>
                    <%#GetTime(DateTime.Parse(DataBinder.Eval(Container.DataItem, "IssueTime").ToString()))%>
                </td>
                <td>
                    <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "Clicks"))%>
                </td>
                <td>
                    <!--设置置顶-->
                    <asp:Label ID="lblIsEnable" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"IsHot") %>'></asp:Label>
                    <asp:LinkButton ID="linkState" CommandName="Lock" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                        runat="server"></asp:LinkButton>
                </td>
                <td>
                    <%# CreateOperation(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID")))%>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <ItemTemplate>
            <tr style="background: #fff; height: 24px; text-align: center;" onmouseover="NewsInfoPage.mouseovertr(this)"
                onmouseout="NewsInfoPage.mouseouttr(this)">
                <td height="25" align="center">
                    <asp:CheckBox runat="server" ID="cbListId"></asp:CheckBox>
                    <asp:Label runat="server" ID="lblItemID" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'
                        Visible="false"></asp:Label>
                </td>
                <td align="center">
                    <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "AfficheClass"))%>
                </td>
                <td align="left">
                    <%# GetTitle(Convert.ToString(DataBinder.Eval(Container.DataItem, "AfficheTitle")),Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID")))%>
                </td>
                <td>
                    <%#GetTime(DateTime.Parse(DataBinder.Eval(Container.DataItem, "IssueTime").ToString()))%>
                </td>
                <td>
                    <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "Clicks"))%>
                </td>
                <td>
                    <!--设置置顶-->
                    <asp:Label ID="lblIsEnable" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"IsHot") %>'></asp:Label>
                    <asp:LinkButton ID="linkState" CommandName="Lock" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                        runat="server"></asp:LinkButton>
                </td>
                <td>
                    <%# CreateOperation(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID")))%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr style="background: #C0DEF3; height: 28px; text-align: center; font-weight: bold;">
                <td width="8%">
                    <strong>
                        <input type="checkbox" onclick="NewsInfoPage.CheckAll(this)" />
                        序号</strong>
                </td>
                <td width="13%">
                    类别
                </td>
                <td width="45%">
                    标题
                </td>
                <td width="9%">
                    发布时间
                </td>
                <td width="8%">
                    查看次数
                </td>
                <td width="8%">
                    设置置顶
                </td>
                <td width="9%">
                    操作
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="NoData" runat="server" Visible="false">
        <table width="100%" id="Table1" border="1" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;">
            <tr style="background: #C0DEF3; height: 28px; text-align: center; font-weight: bold;">
                <td width="8%">
                    序号
                </td>
                <td width="13%">
                    类别
                </td>
                <td width="45%">
                    标题
                </td>
                <td width="9%">
                    发布时间
                </td>
                <td width="8%">
                    查看次数
                </td>
                <td width="8%">
                    设置置顶
                </td>
                <td width="9%">
                    操作
                </td>
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
    </form>
</body>
</html>
