var TourModule = {
	_getData:function(id){
		return commonTourModuleData.get(id);
	},
	CheckForm: function(objLink,id){
		var obj = this._getData(id);
		var isModified=CheckFormIsChange.isFormChanaged($("#"+obj.ContainerID).closest("form").get(0));
		if(isModified==true){
			if(obj.ReleaseType != 'AddQuickRoute' || obj.ReleaseType != 'AddStandardRoute' || obj.ReleaseType != 'LocalQuickRoute' || obj.ReleaseType != 'LocalStandardRoute'){
				if(!confirm("当前的线路信息尚未保存。\n\n是否舍弃该线路信息？")){
					return false;
				}
			}else{
				if(!confirm("当前的团队信息尚未保存。\n\n是否舍弃该团队信息？")){
					return false;
				}
			}
		}
		topTab.url(topTab.activeTabIndex,$(objLink).attr("href"));
		return false;
	},
	addTourDays:function(id,maxDays){
		var obj = this._getData(id);
		var Day = 0;
		var DaysObj = $("#"+obj.ContainerID).find("input[type=text][id$=TourDays]").val();
		var TourDays = $.trim(DaysObj);
		if(TourDays != "" && !isNaN(parseInt(TourDays)) && parseInt(TourDays) > 0)
		{
			Day = TourDays;
		}
		Day = parseInt(parseInt(Day) + 1);
		$("#"+obj.ContainerID).find("input[type=text][id$=TourDays]").val(Day);
		
		//if(obj.ReleaseType == 'AddStandardTour' || obj.ReleaseType == 'AddStandardRoute' || obj.ReleaseType == 'LocalStandardRoute'){
			this.TourDaysFocus(id,maxDays);
		//}
	},
	TourDaysFocus:function(id,maxDays){
		var obj = this._getData(id);
		//if(maxDays != '' && !isNaN(parseInt(maxDays)))
		//{
			var DaysObj = $("#"+obj.ContainerID).find("input[type=text][id$=TourDays]").val();
			var TourDays = $.trim(DaysObj);
			if(TourDays != "" && !isNaN(parseInt(TourDays)) && parseInt(TourDays) > 0 && parseInt(TourDays) > parseInt(maxDays))
			{
				//alert('天数不能大于'+ maxDays +'天!');
				if(obj.ReleaseType == 'AddStandardTour' || obj.ReleaseType == 'AddStandardRoute' || obj.ReleaseType == 'LocalStandardRoute'){
					$("#"+ obj.ContainerID +"tbDateInfo").find("tr").not($("#tr_header")).each(function(i) {
						$(this).remove();
					});
				}
				return false;
			}else{
				if(obj.ReleaseType == 'AddStandardTour' || obj.ReleaseType == 'AddStandardRoute' || obj.ReleaseType == 'LocalStandardRoute'){
					this.GenerateDate(id);
				}
				return true;
			}
			//return true;
		//}
	},
	GenerateDate: function(id) {
		var obj = this._getData(id);
		var Day = 0;
		var TourDays = $.trim($("#"+obj.ContainerID).find("input[type=text][id$=TourDays]").val());
		if (TourDays != "" && !isNaN(parseInt(TourDays)) && parseInt(TourDays) > 0) {
			Day = TourDays;
		}
		$("#"+ obj.ContainerID +"tbDateInfo").show();
		var HaveDateCount = parseInt((($("#"+ obj.ContainerID +"tbDateInfo").find("tr").length / 2)).toString());

		if (HaveDateCount > Day) { //删除
			$("#"+ obj.ContainerID +"tbDateInfo").find("tr").each(function(i) {
				if ((i) > (Day * 2)) {
					$(this).remove();
				}
			});
		}
		else if (HaveDateCount < Day) { //新增列
			var Count = parseInt(Day - HaveDateCount) - 1;
			for (var i = 0; i <= Count; i++) {
				TourStandardPlan.AddDateInfo(null,id)
			}
		}

		if ($("#"+ obj.ContainerID +"tbDateInfo").find("tr").length == 1) {
			$("#"+ obj.ContainerID +"tbDateInfo").hide();
		}
	},
	OpenDialog: function(title,url,width,height,data){
		Boxy.iframeDialog({title:title, iframeUrl:url,width:width,height:height,draggable:true,data:data});
	},
	OpenDateDialog: function(id){
		var obj = this._getData(id);
		var tmpAreaVal = $("#"+obj.ContainerID).find("select[id$=RouteArea]").val();
		if(tmpAreaVal.length > 0)
		{
			tmpAreaVal = tmpAreaVal.split('|')[0];
			var url = "/RouteAgency/SelectChildTourNo.aspx?ReleaseType="+ obj.ReleaseType +"&AreaValue="+ tmpAreaVal +"&ContaierID="+ obj.ContainerID +"&rnd="+ Math.random();

			if(($("#"+obj.ContainerID).find('input[type=hidden][id$=TemplateTourID]').val() != undefined && $("#"+obj.ContainerID).find('input[type=hidden][id$=TemplateTourID]').val().length > 0) || ($("#"+obj.ContainerID).find('input[type=hidden][id$=hidTourID]').val() != undefined && $("#"+obj.ContainerID).find('input[type=hidden][id$=hidTourID]').val().length > 0))
			{ 
				url += "&flag=edit";
			}
			this.OpenDialog('选择出团时间',url,850,300);
		}else{
			alert('请选择线路区域!');
			return;
		}
	},
	AddOnline: function(obj){
		var tr = $(obj).parent().parent();
		var tb = $(obj).parent().parent().parent();
		var table = $(obj).parent().parent().parent().parent();
		var clonetr = $(tr).clone();
		tb.append(clonetr);
		var tmpTD = $(table).find("tr:last td").eq(1);
		tmpTD.find("input[type=text]").val("");
   },
   Delline: function(obj,flag){
		var tr = $(obj).parent().parent();
		var tb = $(obj).parent().parent().parent();
		var trCount = tb.find("tr").length;
		if(flag == "price")
		{
			if (trCount > 2)
				tr.remove();
			else{
				alert('至少要留一行报价信息'); 
			}
		}else{
			if(trCount > 1)
				tr.remove();
			else{
				alert('至少要留一行地接社信息'); 
			}
		}
	},
	InitRouteInfo: function(jsonObj,id){		
		var obj = this._getData(id);
		var RouteModel = eval(jsonObj);
		try{
			// 初始化线路区域
		$("#"+obj.ContainerID).find("select[id$=RouteArea] option").each(function(){
			if($.trim($(this).val()).split('|')[0] == RouteModel.AreaId)
			{
				$(this).attr("selected","selected");
			}
		});
		
		$("#"+obj.ContainerID).find("input[type=text][id$=RouteName]").val(RouteModel.RouteName);
		$("#"+obj.ContainerID).find("input[type=text][id$=TourDays]").val(RouteModel.TourDays);
		// 线路主题
		$("#"+obj.ContainerID).find("input[type=checkbox][name$=chkRouteTopic]").each(function(){
			var strRouteTheme = RouteModel.RouteTheme.toString().split(',');
			if(strRouteTheme.length > 0)
			{
				var oid = -1;
				for(var i = 0;i < strRouteTheme.length; i ++)
				{
					if(strRouteTheme[i] == $.trim($(this).val())){
						oid = 0;
						break;
					}
				}
				if(oid != -1)
				{
					$(this).attr("checked","checked");
				}
			}                    
		});
		// 出港城市
		$("#"+obj.ContainerID).find("input[type=radio][name$=radPortCity]").each(function(){
			if(RouteModel.LeaveCityId == $.trim($(this).val())){
				$(this).attr("checked","checked");
			}
		});
		// 销售城市
		$("#"+obj.ContainerID).find("input[type=checkbox][name$=chkSaleCity]").each(function(){
			var strSaleCity = RouteModel.SaleCity.toString().split(',');
			if(strSaleCity.length > 0){
				var oid = -1;
				for(var i = 0; i < strSaleCity.length; i ++)
				{
					if(strSaleCity[i] == $.trim($(this).val())) {
						oid = 0;
						break;
					}
				}
				if(oid != -1){
					$(this).attr("checked","checked");
				}
			}
		});
		TourPriceStand.addthis(null,RouteModel.PriceDetails,obj.ContainerID); 

		if(obj.ReleaseType == 'AddStandardTour' || obj.ReleaseType == 'AddStandardRoute' || obj.ReleaseType == 'LocalStandardRoute'){
			// 行程
			if(RouteModel.StandardPlans.length > 0)
			{
				for(var i = 0; i < RouteModel.StandardPlans.length; i ++)
				{
					TourStandardPlan.AddDateInfo(RouteModel.StandardPlans[i],id);
				}
			}
			// 包含项目
			if(RouteModel.ServiceStandard != null){
				$("#"+obj.ContainerID).find("textarea[name$=ResideContent]").val(RouteModel.ServiceStandard.ResideContent);
				$("#"+obj.ContainerID).find("textarea[name$=DinnerContent]").val(RouteModel.ServiceStandard.DinnerContent);
				$("#"+obj.ContainerID).find("textarea[name$=SightContent]").val(RouteModel.ServiceStandard.SightContent);
				$("#"+obj.ContainerID).find("textarea[name$=CarContent]").val(RouteModel.ServiceStandard.CarContent);
				$("#"+obj.ContainerID).find("textarea[name$=GuideContent]").val(RouteModel.ServiceStandard.GuideContent);
				$("#"+obj.ContainerID).find("textarea[name$=TrafficContent]").val(RouteModel.ServiceStandard.TrafficContent);
				$("#"+obj.ContainerID).find("textarea[name$=IncludeOtherContent]").val(RouteModel.ServiceStandard.IncludeOtherContent);
				$("#"+obj.ContainerID).find("textarea[id$=Remark]").val(RouteModel.ServiceStandard.SpeciallyNotice);
				$("#"+obj.ContainerID).find("textarea[id$=Service]").val(RouteModel.ServiceStandard.NotContainService);
				
				if($.trim($("#"+obj.ContainerID).find("textarea[id$=Service]").val()) != $.trim('不含项目：\n自费项目：\n儿童安排：\n购物安排：\n赠送项目：\n温馨提醒：\n'))
        {
            $("#"+obj.ContainerID).find("textarea[id$=Service]").removeAttr("style");
        }
			}
			// 线路负责人信息
			$("#"+obj.ContainerID).find("textarea[id$=TourContact]").val(RouteModel.ContactName);
			$("#"+obj.ContainerID).find("textarea[id$=TourContactTel]").val(RouteModel.ContactTel);
			$("#"+obj.ContainerID).find("textarea[id$=TourContactMQ]").val(RouteModel.ContactMQID);
		}else{
			$("#"+obj.ContainerID).find("textarea[id=hidFCKVal]").html(RouteModel.QuickPlan);
			if(KE.g[obj.FCKID] != null){
				KE.html(''+ obj.FCKID +'',$("#"+obj.ContainerID).find("textarea[id=hidFCKVal]").html());
			}else{
			KEInit();
			setTimeout(
                function(){
                  KE.create(''+ obj.FCKID +'',0);//创建编辑器
                  KE.html(''+ obj.FCKID +'',$("#"+obj.ContainerID).find("textarea[id=hidFCKVal]").html()) //赋值
                },200);
			}
		}
		}catch(err){
			alert(err.message);
		}
   },
   SubmitCheck: function(IsContinue,url,title,GoURL,id,maxDays){			
		var obj = this._getData(id);
		if(ValiDatorForm.validator($("#"+obj.ContainerID).closest("form").get(0),"alertspan")){
			//初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
//	        FV_onBlur.initValid($("#AddQuickTour_btnSubmit,#AddQuickTour_btnAdd").closest("form").get(0));

			if(this.CheckData(id) && this.TourDaysFocus(id,maxDays))
			{
				this.CheckExistRoute(IsContinue,url,title,GoURL,id);
			}
		}
   },
   AjaxSubmit: function(IsContinue,url,title,GoURL,id){		
		var obj = this._getData(id);
		if(!IsContinue){
			$("#"+obj.ContainerID).find("span[id=spanSubmit1]").css("display","");
		}else{
			$("#"+obj.ContainerID).find("span[id=spanSubmit1]").css("display","");
		}
		
		$("#" + obj.ContainerID).find("img[id$=btnSubmit]").css("color","#aaa").css("cursor","default").get(0).onclick = null;
	    $("#" + obj.ContainerID).find("a[id$=btnAdd]").css("color","#aaa").css("cursor","default").get(0).onclick = null;
		
		if(obj.ReleaseType == 'AddQuickTour' || obj.ReleaseType == 'AddQuickRoute' || obj.ReleaseType == 'LocalQuickRoute')
	    {
			if(KE.g[obj.FCKID] != null){
				$("#" + obj.FCKID).val(KE.html(''+ obj.FCKID +''));
			}
	    }

		$.newAjax({
			type:"POST",
			url:url + "?flag=add",
			data:$($("#"+obj.ContainerID).closest("form").get(0)).serializeArray(),
			cache:false,
			success:function(state){
				if(state == 'True'){
					alert("保存成功!");
					if(!IsContinue)
					{
						var AreaID = 0;
						if(obj.ReleaseType == 'AddQuickTour' || obj.ReleaseType == 'AddStandardTour'){
							var AreaType = $("#"+obj.ContainerID).find("select[name$=RouteArea]").val();
							AreaID = AreaType.split('|')[0];
						}
						topTab.remove(topTab.activeTabIndex);
						if(GoURL.toLowerCase().indexOf("/routeagency/notstartingteamsdetail.aspx")!=-1){
						    topTab.open("/routeagency/notstartingteams.aspx",title,{desUrl:GoURL,isRefresh:true});
						    return false;
						}
						topTab.open(GoURL,title,{isRefresh:true,data:{AreaId:AreaID}});
						return false;
					}else{
						topTab.url(topTab.activeTabIndex,GoURL);
						return false;  
					}                      
				}else{
					alert("保存失败!");
				}
			}
		}); 
   },
   CheckExistRoute: function(IsContinue,url,title,GoURL,id)    // 判断线路名称是否存在
   {	
		var obj = this._getData(id);
		if(obj.ReleaseType == 'AddQuickRoute' || obj.ReleaseType == 'AddStandardRoute' || obj.ReleaseType == 'LocalQuickRoute' || obj.ReleaseType == 'LocalStandardRoute'){   // 线路
			if($("#"+ obj.ContainerID).find("input[type=hidden][name$=hidRouteID]").val() != '')   // 修改
			{
				TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id); 
			}else{   // 新增
				if($("#"+obj.ContainerID).find("input[type=text][id$=RouteName]").val() != '')
				{
					TourModule.CheckRouteName(IsContinue,url,title,GoURL,id);
				}else{
					TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id);
				}
			}
		}else{   // 团队
			if($("#"+obj.ContainerID).find("input[type=text][id$=RouteName]").val() != '' && $("#"+obj.ContainerID).find("input[type=checkbox][id$=AddToRoute]").attr("checked"))
			{
				this.CheckRouteName(IsContinue,url,title,GoURL,id);
			}else{
				TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id);
			}
		}
	},
	CheckRouteName: function(IsContinue,url,title,GoURL,id){
		var obj = this._getData(id);
		$.newAjax({
			url:url + "?flag=Exist&RouteName="+ encodeURI($("#"+obj.ContainerID).find("input[type=text][id$=RouteName]").val()) +"&rnd="+ Math.random(),
			success:function(state){
				if(state == 'True')
				{
					if(!confirm("线路名称在线路库已经存在，是否确认添加到线路库？"))
					{
						$("#"+obj.ContainerID).find("input[type=checkbox][id$=AddToRoute]").attr("checked",false);
						if(obj.ReleaseType != 'AddQuickRoute' && obj.ReleaseType != 'AddStandardRoute' && obj.ReleaseType != 'LocalQuickRoute' && obj.ReleaseType != 'LocalStandardRoute'){
							TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id);
						}
					}else{
						TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id);
					}
				}else{
					TourModule.AjaxSubmit(IsContinue,url,title,GoURL,id);
				}
			}	                
		});
	},
	CheckData: function(id)
    {
		var obj = this._getData(id);
		//提交时检查是否选择相同的报价等级
		var ckPriceList = new Array();
		$("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function(i) {
			ckPriceList.push($(this).val());
		});

		var arrMessage = new Array();
		var isHave = false;
		var Newarr = ckPriceList.join(",") + ",";
		for (var i = 0; i < Newarr.length; i++) {
			if (Newarr.replace(ckPriceList[i] + ",", "").indexOf(ckPriceList[i] + ",") > -1) {
				isHave = true;
			}
		}
		if (ckPriceList.toString() == "") {
			arrMessage.push("请设置报价等级!\n");
		}
		if (isHave) {
			arrMessage.push("不能选择相同的报价等级!\n");
		}
		//是否有填成人价
		var isTrue = true;
		var isNull = true;
		$("#"+ obj.ContainerID +"tblPriceStand").find("input[type='text'][name^='PeoplePrice'].bitiansm").each(function() {
			var People = $.trim($(this).val());
			if (People == "" || People == '成人价') {
				isNull = false;
				return false;
			}
			if (isNaN(parseInt(People)) || parseInt(People) < 1) {
				isTrue = false;
				return false;
			}
		});
		if (!isNull) {
			arrMessage.push("请填写同行和门市价的成人价格!\n");
		}
		if (!isTrue) {
			arrMessage.push("请填写正确的同行和门市价的成人价格!\n");
		}
		if (arrMessage.length > 0) {
			alert(arrMessage.join(""));
			return false;
		}
		return true;
	}
};
