<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotStartingTeams.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.NotStartingTeams" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<asp:content id="NotStartingTeams" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="tblNotStartingTeams" width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="98%" align="left">
                            <img src="<%=ImageServerUrl %>/images/weichufa.gif" width="131" height="37" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin15" style="border-bottom: 2px solid #FF5500;">
                    <tr>
                        <td>
                            <asp:Repeater ID="rptSysArea" runat="server">
                                <ItemTemplate>
                                    <div class="xianluon1" areaid="<%#Eval("AreaId") %>">
                                        <strong><a href="javascript:void(0)">
                                            <%#Eval("AreaName")%></a></strong></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
               <div id="NotStartingTeams_ajaxTable"></div>
            </td>
        </tr>
    </table>    
        <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>" cache="true"></script>
        <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("TourCalendar") %>" cache="true"></script>
        <script language="javascript" type="text/javascript">
            var NotStartingTeams={
                TourMarkerNote:"",
                TemplateTourID:"",
                CurrentAreaId:"<%=CurrentAreaId %>",
                Page:$("#hidAjaxNotStartingTeamsPage").val(),
                IsGrantUpdate:"<%=IsGrantUpdate %>",
                IsGrantDelete:"<%=IsGrantDelete %>",
                tourUpdate:function(obj,state){
                    if(NotStartingTeams.IsGrantUpdate=="False"){
                         alert("对不起，你目前的帐号没有修改模板团权限！");
                         return;
                    }
                    var AreaId=$(obj).attr("AreaId");
                    var toUrl="/routeagency/NotStartingTeams.aspx?action=setTourMarkerNote&AreaId="+AreaId+"&TourMarkerNote="+state;                    
                    topTab.open(toUrl,"未出发团队",{isRefresh:false});            
                    return false;
                },
                saveTourMarkerNote: function(Description){
                      if(NotStartingTeams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                      }
                      var TemplateTourID=NotStartingTeams.TemplateTourID;
                      $.newAjax({
                        cache:false,
                        url:"/routeagency/NotStartingTeams.aspx?action=setTourMarkerNote&rd="+Math.random(),                
                        data:{TemplateTourID:TemplateTourID,Description:Description,TourMarkerNote:NotStartingTeams.TourMarkerNote,Page:NotStartingTeams.Page},
                        success:function(html){
                            if(html=="1"){                                
                                notStartingTeamsAjax("/routeagency/AjaxNotStartingTeams.aspx?Page="+$("#hidAjaxNotStartingTeamsPage").val())
                                alert("操作成功！")
                            }else{
                                alert("操作失败！")
                            }
                        }
                     })
                    return false;
                },
                setTourMarkerNote:function(TourMarkerNote,obj){//团队类型设置
                    if(NotStartingTeams.IsGrantUpdate=="False"){
                         alert("对不起，你目前的帐号没有修改权限！");
                         return;
                    }
                     NotStartingTeams.TourMarkerNote=TourMarkerNote;
                     NotStartingTeams.TemplateTourID=$(obj).attr("TemplateTourID");                     
                     if(TourMarkerNote!="0"){                     
                        Boxy.iframeDialog({title:'团队推广说明', iframeUrl:'/routeagency/tourdescription.aspx',width:315,height:180,draggable:true,data:{callBack:'NotStartingTeams.saveTourMarkerNote'}});                                             
                     }else{
                        this.saveTourMarkerNote('');
                     }
                     return false;
                },
                //预定按钮调用的方法 模板团ID，点击对象
                ClickCalendar:function(ParentTourID,areatype,obj) {                    
                    SingleCalendar.config.isLogin = "True"; //是否登陆
                    SingleCalendar.config.stringPort ="<%=EyouSoft.Common.Domain.UserPublicCenter %>";//配置
                    SingleCalendar.initCalendar({
                        currentDate:<%=thisDate %>,//当时月
                        firstMonthDate: <%=thisDate %>,//当时月
                        srcElement: obj,
                        areatype:areatype,//当前模板团线路区域类型 
                        TourId:ParentTourID, //模板团ID
                        AddOrder:NotStartingTeams.addOrder
                    });
                },
                addOrder:function(TourId){
                    Boxy.iframeDialog({title:'代客预订', iframeUrl:'/RouteAgency/Booking.aspx?TourID='+TourId,width:800,height:GetAddOrderHeight(),draggable:true,data:null});                                             
                    return false;
                },
                getTourInfo:function(iframe_id,tourName,companyId){                
                    var iframe = document.getElementById(iframe_id);
                    var Doc = iframe.document;                
                    if(iframe.contentDocument){ // For NS6
                        Doc = iframe.contentDocument;
                    }else if(iframe.contentWindow){ // For IE5.5 and IE6
                        Doc = iframe.contentWindow.document;
                    }
                    Doc.getElementById("txtBuyCompanyName").value=tourName.trim();
                    Doc.getElementById("hidBuyCompanyID").value=companyId;
                    $.ajax({
                       type: "GET",
                       async:true,
                       url: "/RouteAgency/AjaxBooking.aspx?CompanyID="+companyId,
                       success: function(html){
                            Doc.getElementById("TourOrderInfo").innerHTML=html;
                       }
                    });     
                }                                    
            };
            function notStartingTeamsAjax(url){
                   $("#NotStartingTeams_ajaxTable").html("<div>请稍候，正在加载...</div>");    
                   clearSingleCalendar();            
                   $.newAjax({
                        cache:false,
                        url:url,                
                        data:{CurrentAreaId:NotStartingTeams.CurrentAreaId},
                        success:function(html){                                                 
                           if(html!="1"){   
                               $("#NotStartingTeams_ajaxTable").html(html)
                           }                         
                            $("#tblNotStartingTeams .NotStartingTeamsDetail").each(function(){
                                $(this).click(function(){
                                    var TemplateTourID=$(this).attr("TemplateTourID")
                                    topTab.url(topTab.activeTabIndex,"/routeagency/NotStartingTeamsDetail.aspx?TemplateTourID="+TemplateTourID);
                                    return false;
                                });                              
                            })
                             //分页控件链接控制
                            $("#NotStartingTeams_ExportPage a").each(function(){
                                $(this).click(function(){                                    
                                    notStartingTeamsAjax($(this).attr("href"))                              
                                    return false;
                                })
                            });
                            $("#tblNotStartingTeams .a_again").click(function(){
                                if(NotStartingTeams.IsGrantUpdate=="False"){
                                    alert("对不起，你目前的帐号没有该权限！");
                                    return false;
                                }
                                var TemplateTourID=$(this).attr("TemplateTourID")
                                topTab.url(topTab.activeTabIndex,"/routeagency/AddTourAgain.aspx?TourID="+TemplateTourID);//
                                return false;
                            });
                             $("#tblNotStartingTeams .a_updateTemplate").click(function(){
                                if(NotStartingTeams.IsGrantUpdate=="False"){
                                    alert("对不起，你目前的帐号没有该权限！");
                                    return false;
                                }
                                var TemplateTourID=$(this).attr("TemplateTourID")
                                var toUrl="/routeagency/addstandardtour.aspx?type=edit&TourID="+TemplateTourID;
                                //发布的类型            
                                if($(this).attr("ReleaseType")!="Standard"){
                                    toUrl="/routeagency/addquicktour.aspx?type=edit&TourID="+TemplateTourID;
                                }
                                topTab.open(toUrl,"模板团维护",{isRefresh:false});            
                                return false;
                            });
                            $("#tblNotStartingTeams .a_deleteTemplate").click(function(){
                               var TemplateTourID=$(this).attr("TemplateTourID");
                               if(!NotStartingTeams.IsGrantDelete=="False"){
                                    alert("对不起，你目前的帐号没有删除模板团权限！");
                                    return;
                               }
                               if(confirm("模板团删除操作将不能删除含有订单的子团，你确定要执行删除操作吗，此操作不可恢复！")){
                                    $.newAjax({
                                        cache:false,
                                        type:"POST",                                        
                                        url:"/routeagency/NotStartingTeamsDetail.aspx?action=TemplateTourDelete&TourID="+TemplateTourID,                                                        
                                        success:function(html){
                                             var returnMsg=eval(html);
                                             if(returnMsg)
                                             {
                                                alert(returnMsg[0].ErrorMessage)
                                                if(returnMsg[0].isSuccess){
                                                    notStartingTeamsAjax("/routeagency/AjaxNotStartingTeams.aspx?Page="+NotStartingTeams.Page)
                                                    return false;
                                                }
                                             }else{
                                                alert('操作失败！')
                                             }
                                        }
                                    })
                              }
                            });
                            $("#tblNotStartingTeams .a_otherTour").click(function(){
                                 var ParentTourID=$(this).attr("TemplateTourID");
                                 var areatype=$(this).attr("areatype");
                                 NotStartingTeams.ClickCalendar(ParentTourID,areatype,this);
                                 return false;
                            });
                            $("#NotStartingTeams_ajaxTable tr").hover(function(){
                                    this.style.backgroundColor="#FFF9E7";
                                },function(){
                                    this.style.backgroundColor=""
                            })
                        }
                    });
            }          
            $(document).ready(function(){     
                notStartingTeamsAjax("/routeagency/AjaxNotStartingTeams.aspx")
                $("#tblNotStartingTeams div[class^='xianluon']").each(function(i){
                    var areaid=$(this).attr("areaid")
                    if(areaid==NotStartingTeams.CurrentAreaId) {
                        $(this).attr("class","xianluon1")
                    }else{
                        $(this).attr("class","xianluon2")
                    }
                    $(this).click(function(){
                        $("div[class^='xianluon']").attr("class","xianluon2")
                        $(this).attr("class","xianluon1")                        
                        NotStartingTeams.CurrentAreaId=$(this).attr("areaid");
                        notStartingTeamsAjax("/routeagency/AjaxNotStartingTeams.aspx")
                        return false;
                    });
                })                          
            });
        </script>
</asp:content>
