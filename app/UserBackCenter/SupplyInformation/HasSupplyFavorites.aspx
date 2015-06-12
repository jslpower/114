<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HasSupplyFavorites.aspx.cs" Inherits="UserBackCenter.SupplyInformation.HasSupplyFavorites" %>
<%@ Register src="../usercontrol/SupplyInformation/SupplyInfoTab.ascx" tagname="SupplyInfoTab" tagprefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
    <%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="HasSupplyFavorites" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="tbl_HasSupplyFavorites" width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                <uc2:SupplyInfoTab ID="SupplyInfoTab1" TabIndex="3" runat="server" />                    
                    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                        class="zttype">
                        <tr>
                            <th width="11%" align="center">
                                类别
                            </th>
                            <th width="60%" align="center">
                                主题
                            </th>
                            <th width="9%" align="center">
                                留言数
                            </th>
                            <th width="10%" align="center">
                                发布日期
                            </th>
                             <th width="10%" align="center">
                                管理操作
                            </th>
                        </tr>
                        <asp:Repeater ID="rpt_HasSupplyFavorites" runat="server" OnItemDataBound="rpt_HasSupplyFavorites_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%#Eval("TopicClassID")%>
                                    </td>
                                    <td align="left" class="tbline">
                                        <asp:Literal runat="server" id="ltrExchangeTag"></asp:Literal> 
                                        <div>
                                            <a target="_blank" href="<%=Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?pid=<%#Eval("ProvinceId") %>">【<%#GetProvinceName(Convert.ToInt32(Eval("ProvinceId")))%>】</a><a target="_blank" href="<%=Domain.UserPublicCenter %>/SupplierInfo/ExchangeInfo.aspx?Id=<%#Eval("ExchangeId")%>" ><%#Eval("ExchangeTitle")%></a>
                                        </div>
                                    </td>
                                    <td align="center">
                                        <strong><%#Eval("WriteBackCount")%></strong>次
                                    </td>
                                    <td align="center">
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <a ExchangeId="<%#Eval("id")%>" class="cancelExchange" href="javascript:void(0)">取消关注</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                         <tr id="Nodata" runat="server" visible="false">
                            <td colspan="5" align="center" style="padding:5px;">
                                对不起，你暂时没有设置关注的商机！
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
                </td>
            </tr>
        </table>
     <div id="HasSupplyFavorites_ExportPage" class="F2Back"  style="text-align:right;" height="40">
        <cc2:ExportPageInfo ID="ExportPageInfo1"  CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
    </div> 
    <script type="text/javascript" language="javascript">
        var HasSupplyFavorites={
            intPageIndex:"<%=intPageIndex %>",
            pageInit:function(){
                $("#tbl_HasSupplyFavorites a[class='cancelExchange']").click(function(){
                    if(confirm("你确定要取消关注该商机吗？")){
                        HasSupplyFavorites.cancelSupply($(this).attr("ExchangeId"));
                    }
                    return false;             
                })
                $("#HasSupplyFavorites_ExportPage a").click(function(){
                    topTab.url(topTab.activeTabIndex,$(this).attr("href")); 
                    return false;
                })
            },
            cancelSupply:function(id){
                $.newAjax({
                   type: "POST",
                   url: "/SupplyInformation/HasSupplyFavorites.aspx?action=delete&ExchangeId="+id,
                   success: function(msg){
                         var returnMsg=eval(msg);                         
                         if(returnMsg)
                         {
                            if(returnMsg[0].isSuccess){                                    
                                topTab.url(topTab.activeTabIndex,"/supplyinformation/hassupplyfavorites.aspx?Page="+HasSupplyFavorites.intPageIndex); 
                            }
                            alert(returnMsg[0].ErrorMessage);
                         }else{                                
                            alert('对不起，取消关注失败！');
                         }                                             
                   },error:function(){                            
                        alert("对不起，取消关注失败！");
                   }
                },true);
            }
        }
        $(function(){
            HasSupplyFavorites.pageInit();
        })
    </script>
</asp:content>
