<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllSupplyManage.aspx.cs"
    Inherits="UserBackCenter.SupplyInformation.AllSupplyManage" %>
    <%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/SupplyInformation/SupplyInfoTab.ascx" TagName="SupplyInfoTab"
    TagPrefix="uc2" %>
<asp:content id="AllSupplyManage" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                <uc2:SupplyInfoTab ID="SupplyInfoTab1"  TabIndex="1" runat="server" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="zttoolbar">                        
                        <tr>
                            <td style="height:30px;" valign="bottom">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
                                    <tr>
                                        <td width="4%" align="center">
                                            <img src="<%=ImageServerUrl %>/images/searchico2.gif" width="23" height="24" />
                                        </td>
                                        <td width="96%" align="left" valign="middle">
                                            类别：
                                            <asp:DropDownList runat="server" id="dpl_ExchangeTag"></asp:DropDownList>
                                            <asp:DropDownList runat="server" id="dpl_ExchangeType"></asp:DropDownList>区域：
                                            <asp:DropDownList runat="server" id="dpl_ProvinceList" ></asp:DropDownList>
                                            <input type="image" id="btn_AllSupplyManage_Search" value="查询" src="<%=ImageServerUrl %>/images/chaxun.gif" style="width: 62px;
                                                height: 21px; border: none; vertical-align:bottom;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                        class="zttype">
                        <tr>
                            <th width="11%" align="center">
                                类别
                            </th>
                            <th width="60%" align="center">
                                主题
                            </th>
                            <th width="7%" align="center">
                                留言数
                            </th>
                            <th width="10%" align="center">
                                发布日期
                            </th>
                            <th width="10%" align="center">
                                管理操作
                            </th>
                        </tr>
                        <asp:Repeater ID="rpt_AllSupplyManage" runat="server" OnItemDataBound="rpt_AllSupplyManage_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%# ((int)Eval("TopicClassID"))==0?"无":Eval("TopicClassID")%>
                                    </td>
                                    <td align="left" class="tbline">                                            
                                           <asp:Literal runat="server" id="ltrExchangeTag"></asp:Literal> 
                                        <div>
                                            <a target="_blank" href="<%=Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?pid=<%#Eval("ProvinceId") %>">【<%#GetProvinceName(Convert.ToInt32(Eval("ProvinceId")))%>】</a><a target="_blank" href="<%=Domain.UserPublicCenter %>/SupplierInfo/ExchangeInfo.aspx?Id=<%#Eval("id")%>" ><%#Eval("ExchangeTitle")%></a>
                                        </div>
                                    </td>
                                    <td align="center">
                                        <strong><%#Eval("WriteBackCount")%></strong>次
                                    </td>
                                    <td align="center">
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" ExchangeId="<%#Eval("id")%>" class="Exchange_update">修改</a> <a ExchangeId="<%#Eval("id")%>" class="Exchange_delete" href="javascript:void(0)">删除</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr id="Nodata" runat="server" visible="false">
                            <td colspan="5" align="center" style="padding:5px;">
                                未找到符合条件的供求信息！
                            </td>
                        </tr>
                    </table>                    
                </td>
            </tr>
        </table>
     <div id="AllSupplyManage_ExportPage" class="F2Back"  style="text-align:right;" height="40">
        <cc2:ExportPageInfo ID="ExportPageInfo1"  CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
    </div> 
        <script language="javascript" type="text/javascript">            
            var AllSupplyManage={
                 pageIndex:"<%=intPageIndex %>",
                 search:function(){
                    var ExchangeTag=$("#<%=dpl_ExchangeTag.ClientID %>").val();
                    var ExchangeType=$("#<%=dpl_ExchangeType.ClientID %>").val();
                    var ProvinceList=$("#<%=dpl_ProvinceList.ClientID %>").val();
                    var param=encodeURI("?PageExchangeTag="+ExchangeTag+"&ExchangeType="+ExchangeType+"&ProvinceList="+ProvinceList);
                    topTab.url(topTab.activeTabIndex,"/supplyinformation/allsupplymanage.aspx"+param);
                 },
                 supplyDelete:function(id){
                    $.newAjax({
                       type: "POST",
                       url: "/SupplyInformation/AllSupplyManage.aspx?action=delete&ExchangeId="+id,
                       success: function(msg){
                             var returnMsg=eval(msg);                         
                             if(returnMsg)
                             {
                                if(returnMsg[0].isSuccess){                                    
                                    topTab.url(topTab.activeTabIndex,"/supplyinformation/allsupplymanage.aspx?Page="+AllSupplyManage.pageIndex); 
                                }
                                alert(returnMsg[0].ErrorMessage)
                             }else{                                
                                alert('对不起，删除失败！')
                             }                                             
                       },error:function(){                            
                            alert("对不起，删除失败！");
                       }
                    },true);
                 },
                 pageInit:function(){
                    $("#btn_AllSupplyManage_Search").click(function(){
                        AllSupplyManage.search();
                        return false;
                    });
                    $("a[class='Exchange_delete']").click(function(){
                        if(confirm("你确定要删除该供应信息吗？")){
                            AllSupplyManage.supplyDelete($(this).attr("ExchangeId"));
                        }
                        return false;
                    });
                    $("a[class='Exchange_update']").click(function(){
                        topTab.url(topTab.activeTabIndex,"/supplyinformation/AddSupplyInfo.aspx?ActionType=update&ExchangeID="+$(this).attr("ExchangeId"));
                        return false;
                    });
                    $("#AllSupplyManage_ExportPage a").click(function(){
                        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                        return false;
                    });
                 }
            }
            $(document).ready(function(){
                AllSupplyManage.pageInit();                
            })
        </script>
</asp:content>
