 var NotStartingTeamsDetail={ 
                 //获取对象的左边位置，上面位置
                getTourIDs:function(){
                    var tourIds="";
                    $("#tbl_NotStartingTeamsDetail input[type='checkbox']").each(function(){
                        if(this.checked){                
                          tourIds+=$(this).attr("TourID")+",";
                        }
                    });
                    return tourIds;
                },   
                getPosition:function(obj) 
                {
                    var objPosition={Top:0,Left:0}
                    var offset = $(obj).offset();
                    objPosition.Left=offset.left;
                    objPosition.Top=offset.top+$(obj).height();
                    return objPosition;
                } ,
                queryData:function(){           
                    var queryUrl="/routeagency/notstartingteamsdetail.aspx?"+NotStartingTeamsDetailParams.getParam();          
                    topTab.url(topTab.activeTabIndex,queryUrl);
                    return false;
                },
                CheckAll:function(obj){
                    $("#tbl_NotStartingTeamsDetail input[type='checkbox']").each(function(){
                       this.checked=obj.checked
                    });
                },
                dialog:function(title,obj,width,height){//弹出窗
                    var url=$(obj).attr("dialogUrl");
                    Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:null});                    
                },
                orderDialog:function(title,obj,width,height){
                    var url=$(obj).attr("dialogUrl");
                    Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:GetAddOrderHeight(),draggable:true,data:null});
                },
                ajax:function(url,type,dataArr,successCallBack){
                    $.newAjax({
                        cache:false,
                        type:type,
                        url:url,                
                        data:dataArr,
                        success:function(html){
                            successCallBack(html);
                        },
                        error:function(){
                            alert('操作失败！');
                        }
                     });
                },
                pageInit:function(){  
                    //成人价，儿童价，单房差
                    $("#tbl_NotStartingTeamsDetail tr:first").siblings().each(function(i){                        
                          $(this).find("td[pepolePrice]").hover(function(){
                                 var href_a=$(this).find("a")[0]
                                 var pos=NotStartingTeamsDetail.getPosition(href_a);
                                 var id="#NotStartingTeamsDetail_PriceInfo"+i;
                                 var priceinfo=$(id);                     
                                 var width=priceinfo.width();                          
                                 priceinfo.css({"display":"",left:Number(pos.Left)+'px',top:Number(pos.Top-15)+'px'})
                            },
                            function(){
                                 var id="#NotStartingTeamsDetail_PriceInfo"+i;
                                 $(id).hide()
                        });
                    })
                    $("#tblNotStartingTeamsDetail #BtnQuery").click(function(){
                        NotStartingTeamsDetail.queryData();
                        return false;
                    });
                    //分页控件链接控制
                    $("#NotStartingTeamsDetail_ExportPage a").each(function(){
                        $(this).click(function(){         
                            topTab.url(topTab.activeTabIndex,$(this).attr("href")+"&"+NotStartingTeamsDetailParams.getParam());
                            return false;
                        })
                    });
                    $("#tbl_NotStartingTeamsDetail tr:first").siblings().hover(function(){
                            this.style.backgroundColor="#FFF9E7";
                        },function(){
                            this.style.backgroundColor=""
                    })
                    $("#tbl_NotStartingTeamsDetail input[ReleaseType]").each(function(){
                        $(this).click(function(){
                            if(this.checked){ 
                                NotStartingTeamsDetailParams.TourID=$(this).attr("TourID");
                                NotStartingTeamsDetailParams.ReleaseType=$(this).attr("ReleaseType");              
                                var pos= NotStartingTeamsDetail.getPosition(this)
                                $("#divShortcutMenu").show().css({left:Number( pos.Left+23)+"px",top:pos.Top+"px",'z-index':10});
                            }else{$("#divShortcutMenu").hide()}
                        })
                    });    
                    $("#tbl_NotStartingTeamsDetail input[SingleTourID]").change(function(){                             
                        if(!isNaN(this.value)){
                            var RealRemnantNumber=$(this).attr("RealRemnantNumber")   //实际剩余                            
                            if(Number(this.value) > Number(RealRemnantNumber)){
                                alert("剩余人数不能大于实际剩余人数"+RealRemnantNumber+"！")
                                return;
                            }
                            NotStartingTeamsDetail.saveSurplus($(this).attr("SingleTourID"),this.value);
                            $(this).hide();
                            $(this).parent().find("span").show().html($(this).parent().find("input[SingleTourID]").val());
                        }else{
                            alert('你输入字符不是数字，请输入数字！');
                        }
                    }).blur(function(){
                        $(this).parent().find("span").show();       
                        $(this).hide()
                    })
                },
                /*-------快捷方式部分-------*/
                menuClose:function(){
                    $("#divShortcutMenu").hide()
                },
                tourUpdate:function(){//子团修改                    
                    if(NotStartingTeamsDetailParams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有修改权限！");
                        return;
                    }
                    var toUrl="/routeagency/addstandardtour.aspx?type=edit&TourID="+NotStartingTeamsDetailParams.TourID;
                    //发布的类型                        
                    var ReleaseType=NotStartingTeamsDetailParams.ReleaseType;
                    if(ReleaseType!="Standard"){
                        toUrl="/routeagency/addquicktour.aspx?type=edit&TourID="+NotStartingTeamsDetailParams.TourID;
                    }
                    topTab.open(toUrl,"团队维护",{isRefresh:false});            
                    return false;
                },
                tourDelete:function(){
                    if(NotStartingTeamsDetailParams.IsGrantDelete=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                    }
                    var action="tourDelete";
                    var TourID=NotStartingTeamsDetail.getTourIDs();
                    if(confirm("你确定要执行删除操作吗，此操作不可恢复！")){
                        NotStartingTeamsDetail.ajax("/routeagency/notstartingteamsdetail.aspx?TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID,"GET",{action:action,TourID:TourID},function(html){
                            var returnMsg=eval(html);
                             if(returnMsg)
                             {                                
                                if(returnMsg[0].isSuccess){
                                    alert("删除成功！");
                                    topTab.url(topTab.activeTabIndex,"/routeagency/notstartingteamsdetail.aspx?TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID+"&Page="+NotStartingTeamsDetailParams.Page);
                                }else{
                                    alert('操作失败或该团队是否含有订单，含有订单的团队不能被删除！');
                                }
                             }
                        })                        
                    }
                },
                changeState:function(state){//状态设置：客满，停收，正常
                    if(NotStartingTeamsDetailParams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                    }
                    var action="changeState";                        
                    var TourID=NotStartingTeamsDetail.getTourIDs();
                    if(TourID=="") {
                        alert("对不起，你还未选择团队，请选择团队！");                        
                        return;
                    }
                    NotStartingTeamsDetail.ajax("/routeagency/notstartingteamsdetail.aspx?TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID,"GET",{action:action,TourID:TourID,TourState:state},function(html){
                        var returnMsg=eval(html);
                         if(returnMsg)
                         {
                            alert(returnMsg[0].ErrorMessage)
                            if(returnMsg[0].isSuccess){
                                topTab.url(topTab.activeTabIndex,"/routeagency/notstartingteamsdetail.aspx?TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID);
                                return false;
                            }
                         }else{
                            alert('预订失败！')
                         }
                    });
                },
                setTourMarkerNote:function(TourMarkerNote){//团队类型设置
                    if(NotStartingTeamsDetailParams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                     }
                     var TourID=NotStartingTeamsDetail.getTourIDs();
                     if(TourID!=""){
                        Boxy.iframeDialog({title:'团队推广说明', iframeUrl:'/routeagency/tourdescription.aspx',width:315,height:150,draggable:true,data:{callBack:'NotStartingTeamsDetail.saveTourMarkerNote'}});                        
                     }else{
                        alert("对不起，你还未选择团队，请选择团队！");                        
                        return;
                     }
                     NotStartingTeamsDetailParams.TourMarkerNote=TourMarkerNote;
                     return false;
                },
                /*-----快捷方式部分结束-------*/ 
                //剩余 
                surplus:function(obj){
                    var span=$(obj).find("span")[0];
                    $(span).hide()         
                    $(obj).find("input[SingleTourID]").show().focus().val($(span).text());
                },
                //保存修改的剩余数 
                saveSurplus:function(TourID,num){
                    if(NotStartingTeamsDetailParams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                    }
                    NotStartingTeamsDetail.ajax("/routeagency/notstartingteamsdetail.aspx?action=setTourRemnantNumber&SingleTourID="+TourID+"&RemnantNumber="+num+"&TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID,"POST",{},function(html){
                         var returnMsg=eval(html);
                         if(returnMsg)
                         {
                            alert(returnMsg[0].ErrorMessage)
                         }else{
                            alert('操作失败！')
                         }
                    });                   
                },            
                saveTourMarkerNote:function(Description){
                    if(NotStartingTeamsDetailParams.IsGrantUpdate=="False"){
                        alert("对不起，你目前的帐号没有该权限！");
                        return;
                    } 
                    var TourID=TourID=NotStartingTeamsDetail.getTourIDs();   
                    if(Description==''){                                        
                        if(TourID==""){
                             alert("对不起，你还未选择团队，请选择团队！");                        
                            return;
                        }
                    }                    
                    NotStartingTeamsDetail.ajax("/routeagency/notstartingteamsdetail.aspx?action=setTourMarkerNote&TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID,"POST",{TourID:TourID,Description:Description,TourMarkerNote:NotStartingTeamsDetailParams.TourMarkerNote},function(html){
                         var returnMsg=eval(html);
                         if(returnMsg)
                         {
                            alert(returnMsg[0].ErrorMessage)
                            if(returnMsg[0].isSuccess){                                
                                topTab.url(topTab.activeTabIndex,"/routeagency/notstartingteamsdetail.aspx?Page="+NotStartingTeamsDetailParams.Page+"&"+NotStartingTeamsDetailParams.getParam());
                                return false;
                            }
                         }else{
                            alert('操作失败！')
                         }
                    });                       
                    return false;
                },
                bookReturn:function(){
                    topTab.url(topTab.activeTabIndex,"/routeagency/notstartingteamsdetail.aspx?TemplateTourID="+NotStartingTeamsDetailParams.TemplateTourID+"&Page="+NotStartingTeamsDetailParams.Page);                
                    return false;
                }
            }
            function getTourInfo(iframe_id,tourName,companyId){                
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
            $(function(){
                NotStartingTeamsDetail.pageInit();
            });