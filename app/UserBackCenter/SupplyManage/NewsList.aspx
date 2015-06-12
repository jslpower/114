<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="UserBackCenter.SupplyManage.NewsList" %>

<asp:content id="NewsList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<table id="tbl_SupplyManage_NewsList" width="99%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #ABC9D9;
                                border-left: 1px solid #ABC9D9; border-right: 1px solid #ABC9D9;">
                                <tr>
                                    <td align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="28" background="<%=ImageServerUrl %>/images/managertopbjmm.gif">
                                                    &nbsp;<strong>我的新闻列表</strong>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="cgbj">
                                                        <tr>
                                                            <td>
                                                                <a href="/supplymanage/addnews.aspx" rel="addnews">添加新闻</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                                            class="zttype">
                                            <tr>
                                                <th width="8%" align="center">
                                                    序列
                                                </th>
                                                <th width="52%" align="center">
                                                    标题
                                                </th>
                                                <th width="15%" align="center">
                                                    发布人
                                                </th>
                                                <th width="10%" align="center">
                                                    发布时间
                                                </th>
                                                <th width="10%" align="center">
                                                    管理操作
                                                </th>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rptSupplyManage_NewsList"   OnItemDataBound="rpt_NewsList_ItemDataBound">
                                                <ItemTemplate>
                                                  <tr>
                                                        <td align="center">
                                                            <asp:Literal runat="server" id="ltrXH" ></asp:Literal>
                                                        </td>
                                                        <td align="left" class="tbline">
                                                            <a href="/supplymanage/addnews.aspx?AfficheID=<%#Eval("id") %>" rel="toptab" class="AfficheTitle"><%#Eval("AfficheTitle")%></a>
                                                        </td>
                                                        <td align="center">
                                                            <%#Eval("OperatorName")%>
                                                        </td>
                                                        <td align="center">
                                                            <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                                        </td>
                                                        <td  align="center"><a href="javascript:void(0)" AfficheID="<%#Eval("id") %>">删除</a></td>
                                                </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td align="center" colspan="4" runat="server" id="NoData" visible="false">
                                                    暂无资讯信息！
                                                </td>
                                            </tr>
                                        </table>
                                            <div id="NewsList_ExportPage" class="F2Back" style="text-align:right;" height="40">
                                                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      <script language="javascript" type="text/javascript">
                var NewsList={
                    PageIndex:"<%=intPageIndex %>"
                };
                $(document).ready(function(){
                    $("#tbl_SupplyManage_NewsList a[rel='addnews']").click(function(){
                        topTab.open($(this).attr("href"),"资讯新增",{isRefresh:true});
                        return false;
                    })
                    //分页控件链接控制
                    $("#NewsList_ExportPage a").each(function(){
                        $(this).click(function(){         
                            NewsList.PageIndex=$(this).text();                             
                            topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                            return false;
                        })
                    });
                    $("a[class=AfficheTitle][rel='toptab']").click(function(){                      
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    })
                    $("a[AfficheID]").click(function(){
                        if(confirm("你确定要删除吗？")){
                            var AfficheID=$(this).attr("AfficheID");                      
                            $.newAjax({
                               type: "POST",
                               url: "/SupplyManage/NewsList.aspx?action=delete&AfficheID="+AfficheID,                           
                               success: function(msg){                             
                                 if(msg=="1"){
                                    alert("删除成功！")
                                    topTab.url(topTab.activeTabIndex,"/supplymanage/newslist.aspx?Page="+NewsList.PageIndex);
                                 }else{
                                    alert("删除失败！")
                                 }
                               },error:function(){                                
                                    alert("对不起，删除失败！");
                               }
                            });  
                            return false;
                        }
                    })
                });
            </script>
</asp:content>
