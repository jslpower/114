<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlreadyStartingTeams.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AlreadyStartingTeams" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<asp:content id="AlreadyStartingTeams" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="tblAlreadyStartingTeams" width="99%" border="0" cellspacing="0" cellpadding="0" class="tablewidth" style="margin-left:auto; margin-right:auto;">
    <tr>
        <td width="99%" background="<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif"
            align="center">
            团号：
            <input type="text" class="shurukuang" value="<%=TourNumber%>" style="width:80px;" id="txtTourNumber" name="txtTourNumber" />
            &nbsp;线路名称：
            <input name="text2" type="text" value="<%=RouteName %>" class="shurukuang" id="txtRouteName" name="txtRouteName"  />
            天数：
            <input name="text3" type="text" value="<%=Days==0?null:Days %>" style="width:40px;" class="shurukuang" id="txtDays" name="txtDays" />
            出团日期：
            <input id="txtBeginDate" style="width:65px;" value="<%=ShowBeginDate %>" onfocus="WdatePicker()" class="shurukuang"  type="text"  name="txtBeginDate"/>
            至
            <input id="txtEndDate" style="width:65px;" value="<%=ShowEndDate %>" onfocus="WdatePicker()" class="shurukuang"  type="text" name="txtEndDate" />             
            <img src="<%=ImageServerUrl %>/images/chaxun.gif" width="62" onclick="AlreadyStartingTeams.queryData();return false;" height="21" style="margin-bottom: -4px; cursor:pointer;" />
            <a href="/routeagency/notstartingteams.aspx" rel="toptab" onclick="AlreadyStartingTeams.lookNotStartingTeams(this);return false;">
                <img src="<%=ImageServerUrl %>/images/weichutuan.gif" width="111" height="24" border="0"
                    style="margin-bottom: -3px;" onclick="javascript:return false;" /></a>
        </td>
    </tr>
</table>
<table id="tbl_AlreadyStartingTeams" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC"
    style="width: 99%;">
    <tr height="23">
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <input name="checkbox" style="display:none" type="checkbox" onclick="AlreadyStartingTeams.CheckAll(this)" />
            <strong>序号</strong>
        </td>
        <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>团号</strong>
        </td>
        <td width="32%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>线路名</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>出发</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>返回</strong>
        </td>
        <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>成人价</strong>
        </td>
        <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>儿童价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>单房差</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>计划</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>实收</strong>
        </td>
        <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>留位过期<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>不受理<span class="font12_grean"></span></strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>阅</strong>
        </td>
    </tr>
    <asp:repeater runat="server" id="rpt_AlreadyStartingTeams"  OnItemDataBound="rpt_AlreadyStartingTeams_ItemDataBound">
            <ItemTemplate>
                <tr class="baidi">
                     <td height="24" align="center" >
                        <input type="checkbox" style="display:none" id="Checkbox2" name='cbTourId' TourID="<%#Eval("id") %>" ReleaseType="<%#Eval("ReleaseType") %>" />
                        <asp:Literal runat="server" id="ltrXH" ></asp:Literal>
                       
                    </td>
                    <td align="center">
                        <%#Eval("TourNo")%>
                        <!--团号-->
                    </td>
                    <td align="left"style="padding: 6px;padding-left:0px;">
                        <asp:Literal runat="server" id="ltrSateName"></asp:Literal><a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id") %>" target="_blank" style="display:block;"> &nbsp;<%#Eval("RouteName")%></a>
                                          
                    </td>
                    <td align="center" class="hong18">                        
                         <!--出团日期-->
                         <%#Eval("LeaveDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center" class="hong18">                        
                         <!--返回日期-->
                         <%#Eval("ComeBackDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center"  pepolePrice="pepolePrice" class="hong18">
                        <!--成人价-->
                        <strong><a 
                            style="cursor: hand;cursor:pointer;"><%#Eval("TravelAdultPrice", "{0:F2}")%></a>
                            <div id="PriceInfo<%#Container.ItemIndex %>" style='display:none; position: absolute; margin-top: 12px; margin-left: -180px; background-color:White;'
                                    class='kuang1'>
                                    <table width='380' class='font1' border='1' style="background-color: White;" bordercolor='#D0D8E0'>
                                        <asp:Repeater runat="server" ID="rptPriceInfo">
                                            <HeaderTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#57b9f0'" onmouseout="this.style.backgroundColor='#57b9f0'" style="background-color:#57b9f0">
                                                    <td width='20%' class='shenghui1' >
                                                        类别
                                                    </td>
                                                    <td width='30%' class='shenghui1'>
                                                        门市价
                                                    </td>
                                                    <td width='18%' class='shenghui1'>
                                                        普通同行
                                                    </td>                       
                                                    <td width='14%' class='shenghui1'>
                                                        单房差
                                                    </td>
                                                </tr>
                                                <tr onmouseover="this.style.backgroundColor='#cbe7f7'" style="background-color:#cbe7f7">
                                                    <td width='20%' class='shenghui2'>
                                                        &nbsp;
                                                    </td>
                                                    <td width='30%' class='shenghui2'>
                                                        成人/儿童/单房差
                                                    </td>
                                                    <td width='18%' class='shenghui2'>
                                                        成人/儿童
                                                    </td>
                                                   
                                                    <td width='14%' class='shenghui2'>
                                                        结算
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style=" font-weight:lighter; color:#ff6600">
                                                     <td>
                                                        <%#Eval("PriceStandName")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("AdultPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("SingleRoom2", "{0:F2}")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("AdultPrice1", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice1", "{0:F2}")%>
                                                    </td>                      
                                                    <td>
                                                        <%#Eval("SingleRoom1", "{0:F2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            
                        </strong>
                    </td>
                    <td align="center"  pepolePrice="pepolePrice" class="hong18">
                     <!--儿童价-->
                     
                        <strong><a 
                            style="cursor: hand; cursor:pointer;"><%#Eval("TravelChildrenPrice", "{0:F2}")%></a>                                                         
                        </strong>
                    </td>
                    <td align="center" pepolePrice="pepolePrice">
                        <!--单房差结算价 -->
                      <strong><a style="cursor:pointer;"><%#Eval("RoomDiffSettlementPrice", "{0:F2}")%></a></strong>
                    </td>
                    <td align="center">
                    <!--计划-->
                         <%#Eval("PlanPeopleCount")%>
                    </td>
                    <td align="center">
                    <!--实收-->
                        <strong><%#Eval("CollectAdultNumber")%><sup>+<%#Eval("CollectChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                    <!--留位过期-->
                        <strong style="color: #3399FF">
                            <%#Eval("OverdueAdultNumber")%><sup>+<%#Eval("OverdueChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                     <!--未处理-->
                        <strong style="color: #FF0000">
                            <%#Eval("UntreatedAdultNumber")%><sup>+<%#Eval("UntreatedChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center" width="42">
                        <!--阅-->
                         <a class="fontcolor" href="javascript:void(0)" dialogUrl="/RouteAgency/AccessRecords.aspx?TourID=<%#Eval("ID") %>" onclick="AlreadyStartingTeams.dialog('浏览次数',this,800,400);return false;"><%#Eval("Clicks")%>次</a>                        
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="baidi" >
                    <td height="24" align="center">
                        <input type="checkbox" style="display:none" id="Checkbox1" name='cbTourId' TourID="<%#Eval("id") %>" ReleaseType="<%#Eval("ReleaseType") %>" />
                        <asp:Literal runat="server" id="ltrXH" ></asp:Literal>
                        <!--序号-->
                    </td>
                    <td align="center">
                        <%#Eval("TourNo")%>
                        <!--团号-->
                    </td>
                    <td align="left"style="padding: 6px; padding-left:0px;">
                       <asp:Literal runat="server" id="ltrSateName"></asp:Literal><a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id") %>" target="_blank" style="display:block;">&nbsp;<%#Eval("RouteName")%></a>                                
                    </td>
                    <td align="center" class="hong18">                        
                         <!--出团日期-->
                         <%#Eval("LeaveDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center" class="hong18">                        
                         <!--返回日期-->
                         <%#Eval("ComeBackDate", "{0:MM-dd}")%>
                    </td>
                    <td align="center"  pepolePrice="pepolePrice" class="hong18">
                        <!--成人价-->
                        <strong><a 
                            style="cursor:pointer;"><%#Eval("TravelAdultPrice", "{0:F2}")%></a>
                            <div id="PriceInfo<%#Container.ItemIndex %>" style='display:none; position: absolute; background-color:White; margin-top: 12px; margin-left: -180px;'
                                    class='kuang1'>
                                    <table width='380' class='font1' border='1' style="background-color: White;" bordercolor='#D0D8E0'>
                                        <asp:Repeater runat="server" ID="rptPriceInfo">
                                            <HeaderTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#57b9f0'" onmouseout="this.style.backgroundColor='#57b9f0'" style="background-color:#57b9f0">
                                                    <td width='20%' class='shenghui1'>
                                                        类别
                                                    </td>
                                                    <td width='30%' class='shenghui1'>
                                                        门市价
                                                    </td>
                                                    <td width='18%' class='shenghui1'>
                                                        普通同行
                                                    </td>                       
                                                    <td width='14%' class='shenghui1'>
                                                        单房差
                                                    </td>
                                                </tr>
                                                <tr style="background-color:#cbe7f7">
                                                    <td width='20%' class='shenghui2'>
                                                        &nbsp;
                                                    </td>
                                                    <td width='30%' class='shenghui2'>
                                                        成人/儿童/单房差
                                                    </td>
                                                    <td width='18%' class='shenghui2'>
                                                        成人/儿童
                                                    </td>
                                                   
                                                    <td width='14%' class='shenghui2'>
                                                        结算
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style=" font-weight:lighter; color:#ff6600">
                                                     <td>
                                                        <%#Eval("PriceStandName")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("AdultPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice2", "{0:F2}")%><span class='fonth'>/</span><%#Eval("SingleRoom2", "{0:F2}")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("AdultPrice1", "{0:F2}")%><span class='fonth'>/</span><%#Eval("ChildrenPrice1", "{0:F2}")%>
                                                    </td>                      
                                                    <td>
                                                        <%#Eval("SingleRoom1", "{0:F2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>                            
                        </strong>
                    </td>
                    <td align="center"  pepolePrice="pepolePrice" class="hong18">
                     <!--儿童价-->
                        <strong><a 
                            style="cursor: hand; cursor:pointer;"><%#Eval("TravelChildrenPrice", "{0:F2}")%></a>                                                         
                        </strong>
                    </td>
                    <td align="center" pepolePrice="pepolePrice">
                        <!--单房差结算价 -->
                         <strong><a style="cursor:pointer;"><%#Eval("RoomDiffSettlementPrice", "{0:F2}")%></a></strong>
                    </td>
                    <td align="center">
                    <!--计划-->
                         <%#Eval("PlanPeopleCount")%>
                    </td>
                    <td align="center">
                    <!--实收-->
                        <strong><%#Eval("CollectAdultNumber")%><sup>+<%#Eval("CollectChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                    <!--留位过期-->
                        <strong style="color: #3399FF">
                            <%#Eval("OverdueAdultNumber")%><sup>+<%#Eval("OverdueChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center">
                     <!--未处理-->
                        <strong style="color: #FF0000">
                            <%#Eval("UntreatedAdultNumber")%><sup>+<%#Eval("UntreatedChildrenNumber")%></sup></strong>
                    </td>
                    <td align="center" width="42">
                        <!--阅-->
                         <a class="fontcolor" href="javascript:void(0)" dialogUrl="/RouteAgency/AccessRecords.aspx?TourID=<%#Eval("ID") %>" onclick="AlreadyStartingTeams.dialog('浏览次数',this,800,400)"><%#Eval("Clicks")%>次</a>                        
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:repeater>        
        <tr runat="server" id="NoData"  visible="false">
            <td colspan="13">
                暂无已出发的团队信息！
            </td>
        </tr>            
    <tr height="23">
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <input name="checkbox" style="display:none" type="checkbox" onclick="AlreadyStartingTeams.CheckAll(this)" />
            <strong>序号</strong>
        </td>
        <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>团号</strong>
        </td>
        <td width="32%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>线路名</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>出发</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>返回</strong>
        </td>
        <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>成人价</strong>
        </td>
        <td width="6%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>儿童价</strong>
        </td>
        <td align="center" valign="middle" background="<%=ImageServerUrl %>/Images/detail_list_th.gif">
            <strong>单房差</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>计划</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>实收</strong>
        </td>
        <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>留位过期<span class="hot"></span></strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>不受理<span class="font12_grean"></span></strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/detail_list_th.gif">
            <strong>阅</strong>
        </td>
    </tr>
</table>  

<div  id="AlreadyStartingTeams_ExportPage"  class="F2Back" style="text-align:right;" >
  <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
</div>
<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>" cache="true"></script>
<script language="javascript" type="text/javascript">
      /*操作快捷方式变量
        TourID:组团编号
        TourAddType,Standard:标准发布,1:快速发布;
      */
    var ShortcutMenu={
        TourID:null,
        ReleaseType:0
    };
   var AlreadyStartingTeams={
          Page:"<%=intPageIndex %>",
          isGrant:"<%=isGrant %>",
          getPosition:function(obj) 
          {
            var objPosition={Top:0,Left:0}
            var offset = $(obj).offset();
            objPosition.Left=offset.left;
            objPosition.Top=offset.top+$(obj).height();
            return objPosition;
          } ,
          getParam:function(){
                var TourNumber=$("#tblAlreadyStartingTeams #txtTourNumber").val();
                var RouteName=encodeURI($.trim($("#tblAlreadyStartingTeams #txtRouteName").val()));
                var Days=$("#tblAlreadyStartingTeams #txtDays").val();
                var BeginDate=$("#tblAlreadyStartingTeams #txtBeginDate").val();
                var EndDate=$("#tblAlreadyStartingTeams #txtEndDate").val();
                return $.param({TourNumber:TourNumber,RouteName:RouteName,Days:Days,BeginDate:BeginDate,EndDate:EndDate});
         },
         pageInit:function(){  //成人价，儿童价，单房差
            $("#tbl_AlreadyStartingTeams tr[class='baidi']").each(function(i){                        
                  $("#tbl_AlreadyStartingTeams tr[class='baidi']").eq(i).find("td[pepolePrice]").hover(function(){
                         var href_a=$(this).find("a")[0]
                         var pos=AlreadyStartingTeams.getPosition(href_a);
                         var id="#PriceInfo"+i;
                         var priceinfo=$(id);                     
                         var width=priceinfo.width();                                
                         priceinfo.css({"display":"",left:Number(pos.Left)+'px',top:Number(pos.Top-15)+'px'})
                    },
                    function(){
                         var id="#PriceInfo"+i;
                         $(id).hide()
                });
            })
            $("#tbl_AlreadyStartingTeams tr:first").siblings().hover(function(){
                    this.style.backgroundColor="#FFF9E7";
                },function(){
                    this.style.backgroundColor=""
            })
            //分页控件链接控制
            $("#AlreadyStartingTeams_ExportPage a").each(function(){
                $(this).click(function(){
                    AlreadyStartingTeams.Page= $(this).text();                             
                    topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+AlreadyStartingTeams.getParam());
                    return false;
                })
            });            
            $("a[class^='state']").hover(function(){
                    var href_a=this
                    //alert(2)
                    var pos=AlreadyStartingTeams.getPosition(href_a);
                    var width=$(this).width();                                                                          
                    $(this).next().css({"display":"",left:Number(pos.Left+width/2)+'px',top:Number(pos.Top-40)+'px'})
                },function(){
                    $(this).next().hide()
            }); 
        },
        queryData:function(){           
            var queryUrl="/routeagency/alreadystartingteams.aspx?"+AlreadyStartingTeams.getParam();
            topTab.url(topTab.activeTabIndex,queryUrl);
            return false;
        },
        CheckAll:function(obj){
            $("#tbl_AlreadyStartingTeams input[type='checkbox']").each(function(){
                if(obj.checked){
                    $(this).attr("checked","checked")
                }else{
                    $(this).attr("checked","")
                }
            });
        },
        dialog:function(title,obj,width,height){//弹出窗
            var url=$(obj).attr("dialogUrl");
            Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:null});
        },
        //查看未出发团队
        lookNotStartingTeams:function(obj){
            var a = $(obj);
            var tabrefresh=a.attr("tabrefresh")=="false"?false:true;
            var href = a.attr("href");
            var hash = href.replace(/^.*#/, '');
            if(href.indexOf("#")==-1){
                var b = topTab.open(href,"未出发团队",{isRefresh:tabrefresh});
            }
            return false;
        }
          /*-------快捷方式部分-------*/
//        menuClose:function(){
//            $("#divShortcutMenu").hide()
//        },
//        updateTour:function(){
//            if(AlreadyStartingTeams.isGrant=="False"){
//                alert("对不起，你目前的帐号没有修改权限！");
//                return;
//            }
//            var toUrl="/routeagency/addstandardtour.aspx?TourID="+ShortcutMenu.TourID;
//            //发布的类型            
//            if(ShortcutMenu.ReleaseType!="Standard"){
//                toUrl="/routeagency/addquicktour.aspx?TourID="+ShortcutMenu.TourID;
//            }
//             var hash = toUrl.replace(/^.*#/, '');
//            topTab.open(toUrl,"团队维护",{isRefresh:false});      
//        },
//        copyTour:function(){            
//            if(AlreadyStartingTeams.isGrant=="False"){
//                alert("对不起，你目前的帐号没有修改权限！");
//                return;
//            }
//            var toUrl="/routeagency/addstandardtour.aspx?TourID="+ShortcutMenu.TourID;
//            //发布的类型            
//            if(ShortcutMenu.ReleaseType!="Standard"){
//                toUrl="/routeagency/addquicktour.aspx?TourID="+ShortcutMenu.TourID;
//            }
//            var hash = toUrl.replace(/^.*#/, '');
//             topTab.open(toUrl,"新增计划",{isRefresh:false});         
//        },
//        delTour:function(){
//            var action="tourDelete";
//            if(AlreadyStartingTeams.isGrant=="False"){
//                alert("对不起，你目前的帐号没有修改权限！");
//                return;
//            }
//            $.newAjax({
//                url: '/RouteAgency/AlreadyStartingTeams.aspx?TourID='+ShortcutMenu.TourID,
//                cache:false,
//                type:"GET",
//                data:{action:'tourDelete',rd:Math.random(),isLogin:'false'},
//                success:function(html){
//                    if(html=="1"){
//                        topTab.url(topTab.activeTabIndex,"/routeagency/alreadystartingteams.aspx?Page="+AlreadyStartingTeams.Page);
//                        alert('删除操作成功！')
//                    }else{
//                        alert('删除操作失败！')
//                    }
//                }
//            });
//        }
        /*-------快捷方式部分 -------*/
    }
    $(function(){
//        $("#tbl_AlreadyStartingTeams input[ReleaseType]").each(function(){
//            $(this).click(function(){
//                if(this.checked){ 
//                    ShortcutMenu.TourID=$(this).attr("TourID");
//                    ShortcutMenu.ReleaseType=$(this).attr("ReleaseType");              
//                    var pos= AlreadyStartingTeams.getPosition(this)
//                    $("#divShortcutMenu").show().css({left:Number( pos.Left+18)+"px",top:Number(pos.Top-10)+"px",'z-index':10});
//                }else{$("#divShortcutMenu").hide()}
//            })
//       });
       AlreadyStartingTeams.pageInit();
    });
</script>

</asp:content>
