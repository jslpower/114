var timeoutHandle=0;
function ClearHandle()
{
    clearTimeout(timeoutHandle);
}
function getPageDiv(DivId,ElementId)
{
    clearTimeout(timeoutHandle);
    var FunctionName='';
    var ActiveElementId=DivId+ElementId;
    switch(DivId)
    {
        case "DivUserPanel_":
            FunctionName = "OperateUserPanelDiv('"+ElementId+"')";
            break;
        case "DivShopPaiHang_Flag":
            FunctionName = "OperatePaiHangDiv('"+ElementId+"')";
            break;
        case "DivShopWantOrGo_":
            FunctionName = "OperateGoAndWantDiv('"+ElementId+"')";
            break;
        case "DivShopMsg_Food":
            FunctionName = "OperateShopMsg_FoodDiv('"+ActiveElementId+"')";
            break;
        case "DivShopMsg_Party":
            FunctionName = "OperateShopMsg_PartyDiv('"+ActiveElementId+"')";
            break;
        case "DivShopMsg_Shopping":
            FunctionName = "OperateShopMsg_ShoppingDiv('"+ActiveElementId+"')";
            break;
        case "DivCircle_Flag":
            FunctionName = "OperateCircleDiv('"+ElementId+"')";
            break;
        case "DivClassMap_Num":
            FunctionName = "OperateClassMapDiv('"+ElementId+"')";
            break;
        case "DivCircleMap_Num":
            FunctionName = "OperateCircleMapDiv('"+ElementId+"')";
            break;
        case "DivBbsTopic":
            FunctionName = "displayLayerBBS('"+ActiveElementId+"')";
            break;
        case "DivCircleAction_Flag":
            FunctionName = "OperateCircleActionDiv('"+ElementId+"')";
            break;
        case "DivActiveUser_Flag":
            FunctionName = "OperateActiveUserDiv('"+ElementId+"')";
            break;
        case "DivDianPing_Flag":
            FunctionName = "OperateDianPingDiv('"+ElementId+"')";
            break;
    }
    //alert(ActiveElementId);
    if(FunctionName!="")
    {
        var obj = _gObjByID(ActiveElementId);
	    if(timeoutHandle !=0 )clearTimeout(timeoutHandle);
	    timeoutHandle = setTimeout(FunctionName,500);
	    obj.onmouseout = function(){clearTimeout(timeoutHandle);};
	    //obj.onclick = function (){return false;};        
    }    
}

function ShowDivContent(PageUrl,objXmlHttp,CurrentDivID,objShowPanel)
{//alert(PageUrl);
        objXmlHttp.open("GET",PageUrl, true);//异步访问
	    if(window.XMLHttpRequest)
	    {
	        objXmlHttp.onload = function () 
	        {
	                    objShowPanel.innerHTML=objXmlHttp.responseText;
    				    objXmlHttp=null;
    				    switch(CurrentDivID)
                        {
                            case "DivPaiHangFlag1": case "DivPaiHangFlag2": case "DivPaiHangFlag3":
                                SwapPaiHangCircleDiv(CurrentDivID,'1');
                                break;
                            case "DivCircleAction1": case "DivCircleAction2":
                                SwapCircleActionDiv(CurrentDivID,'1');
                                break;
                            case "DivUserPanel":
                                //_gObjByID('DivUserPanel_' + CurrentUserPanelDiv +"_Content").innerHTML=objShowPanel.innerHTML;
                                break;
                            case "DivShopPaiHangFlag1": case "DivShopPaiHangFlag2": case "DivShopPaiHangFlag3":
                                SwapPaiHangShopDiv(CurrentDivID,'1');
                                break;
                            case "DivShopwantgo": case "DivShopgo":
                                SwapGoAndWantShopDiv(CurrentDivID,'1');
                                break;
                        }
	        };
	    }
	    else
	    {   
	        objXmlHttp.onreadystatechange = function () 
	        {
		        if (objXmlHttp.readyState == 4) 
		        {
			        if (objXmlHttp.status == 200) 
			        {//alert(objXmlHttp.responseText);
    				    objShowPanel.innerHTML=objXmlHttp.responseText;
    				    objXmlHttp=null;    				    
    				    switch(CurrentDivID)
                        {
                            case "DivPaiHangFlag1": case "DivPaiHangFlag2": case "DivPaiHangFlag3":
                                SwapPaiHangCircleDiv(CurrentDivID,'1');
                                break;
                             case "DivCircleAction1": case "DivCircleAction2":
                                SwapCircleActionDiv(CurrentDivID,'1');
                                break;
                             case "DivUserPanel":
                                _gObjByID('DivUserPanel_' + CurrentUserPanelDiv +"_Content").innerHTML=objShowPanel.innerHTML;
                                break;
                             case "DivShopPaiHangFlag1": case "DivShopPaiHangFlag2": case "DivShopPaiHangFlag3":
                                SwapPaiHangShopDiv(CurrentDivID,'1');
                                break;
                             case "DivShopwantgo": case "DivShopgo":
                                SwapGoAndWantShopDiv(CurrentDivID,'1');
                                break;
                        }
			        } else {
				        alert("An error occurred: " + oXmlHttp.statusText);
			        }
		        }
	        };
	    }	    
	    objXmlHttp.send(null);
}

function GetLoadingImg()
{
	var ImgHtml;
	ImgHtml='<div align="center">';
	ImgHtml=ImgHtml+'<span><img src="images/loading.gif" alt="loading...." /></span>'
	ImgHtml=ImgHtml+'</div>';
	return ImgHtml;
}

_gObjByID = function(id) {
	return document.getElementById(id);
}

 function loadNews(containerID)  
 {
    var URL = 'indexContent/DivNews.aspx?tmp='+Math.random()

    new Ajax.Request(URL, 
    {
      method: 'get',
      
      onLoaded:function(response)
      {
          //$(containerID).innerHTML = '<span><img src="images/loading.gif" alt="loading...." /></span>'
      },
      
      onSuccess:function(response)
      {
          $(containerID).update(response.responseText);
          
          marque(400,44,"icefable1","box1left")
          marques(376,22,"icefable2","box1left2")
          
      },
       
      onFailure:function()
      {
          $(containerID).innerHTML = '服务器忙，请稍后再试'
      }
    }
    );
  }
 
//展示页面图片
function ShowImg(ImgId,ImgSrc)
{//alert(_gObjByID(ImgId).src);
    if(_gObjByID(ImgId).src=="")
    {
        _gObjByID(ImgId).src=_gObjByID(ImgSrc).innerHTML;
    }    
}

function LoadHtml()
{
    loadNews('Div_Focuse_NetFriend_CompanyActivity_Show');
    
    OperateUserPanelDiv('1');
    //OperateSearchDiv('1');
    //OperateCircleMapDiv('1');
    SwapPaiHangCircleDiv("DivPaiHangFlag1",'1');//定位朋友圈第一个项
   // SwapCircleActionDiv("DivCircleAction1",'1');//定位朋友圈活动第一个项
   
}
//用户控制面板
var CurrentUserPanelDiv='';
function OperateUserPanelDiv(divID)
{
	//alert(divID);
	if(CurrentUserPanelDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	var shopPanel_=null;
	if(divID=="1" || divID=="2" || divID=="3")
	{
		objShowPanel=_gObjByID("DivUser_ShowPanel_1");
		shopPanel_="DivUser_ShowPanel_1";
		_gObjByID('memberMenu1').style.top='';
		_gObjByID('DivUser_ShowPanel_2').style.top='';
		_gObjByID('DivUser_ShowPanel_1').style.display='';
		_gObjByID('DivUser_ShowPanel_2').style.display='none';
		//alert('A'+divID);
	}
	else
	{
		objShowPanel=_gObjByID("DivUser_ShowPanel_2");
		shopPanel_="DivUser_ShowPanel_2";
		_gObjByID('memberMenu1').style.top='36px';
		_gObjByID('DivUser_ShowPanel_2').style.top='68px';
		_gObjByID('DivUser_ShowPanel_1').style.display='none';
		_gObjByID('DivUser_ShowPanel_2').style.display='';
		//alert('B'+divID);
	}
	//交换数据
	//if(CurrentUserPanelDiv!="") _gObjByID('DivUserPanel_' + CurrentUserPanelDiv +"_Content")=objShowPanel.innerHTML;
	
	objShowPanel.innerHTML=GetLoadingImg();
	for(var i=1;i<=6;i++)
	{
		_gObjByID('DivUserPanel_' + i).removeAttribute("className");
		_gObjByID('DivUserPanel_' + i).removeAttribute("class");
	}
	_gObjByID('DivUserPanel_' + divID).className='current';
	CurrentUserPanelDiv=divID;	
	
	obj=_gObjByID('DivUserPanel_' + divID +"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
	    var x = new XHR("re_OperateUserPanelDiv('"+shopPanel_+"')");
 	    //alert(url);
 	    var url = "indexContent/DivUserPanel.aspx?Flag=" + divID ;
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	}
}
function re_OperateUserPanelDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
 	_gObjByID('DivUserPanel_' + CurrentUserPanelDiv +"_Content").innerHTML=_gObjByID(id).innerHTML;
}

//用户状态区域
function OperateUserInfoDiv()
{
    var objShowPanel=null;
    objShowPanel=_gObjByID("DivUser_ShowInfo");
    var shopPanel_="DivUser_ShowInfo";
//    var oXmlHttp = createXMLHttp();
//	ShowDivContent('indexContent/DivUserPanel.aspx?Flag=10',oXmlHttp,'DivUserPanel',objShowPanel);
	var x = new XHR("re_OperateUserPanelDiv('"+shopPanel_+"')");
 	//alert(url);
 	var url = "indexContent/DivUserPanel.aspx?Flag=10";
	x.get(url);
}

//搜索区域
var CurrentSearchDiv='1';
function OperateSearchDiv(divID)
{	//alert(divID);
    if(CurrentSearchDiv==divID) return;
    var obj=null;
	var objShowPanel=null;
    for(var i=1;i<=4;i++)
    {
        _gObjByID('DivSearch_Num' + i).removeAttribute("className");
        _gObjByID('DivSearch_Num' + i).removeAttribute("class");
    }
    _gObjByID('DivSearch_Num' + divID).className='current';
        
    objShowPanel=_gObjByID("DivSearch_ShowPanel");
    _gObjByID('DivSearch_Num' + CurrentSearchDiv +"_Content").innerHTML=objShowPanel.innerHTML;
    obj=_gObjByID('DivSearch_Num' + divID +"_Content");    
    objShowPanel.innerHTML=obj.innerHTML;
    CurrentSearchDiv=divID;
	_gObjByID('TagFlag').value=divID;	
	//alert(_gObjByID('TagFlag').value);
}
//今日焦点
var IsFirst = 1;
function OperateFocus(divID)
{
   if(document.getElementById(divID).className=='current' && IsFirst==0) return;    
   IsFirst = 0;
   _gObjByID('liTodayFocus').className="none";
   _gObjByID('liNetFriend').className="none";
   _gObjByID('liCompanyActivity').className="none";
   _gObjByID(divID).className='current'
   
   var show = _gObjByID("Div_Focuse_NetFriend_CompanyActivity_Show")
   show.innerHTML = GetLoadingImg();
   show.innerHTML = SXMLHttpRequest('indexContent/'+divID+'.aspx');
}


//商家排行
var CurrentShopPaiHangFlag='1';
function OperatePaiHangDiv(divID)
{
	//alert(CurrentCircleFlag);
	var obj=null;
    var objShowPanel=null;
    objShowPanel=_gObjByID("DivShopPaiHang_ShowPanel");
    var shopPanel_="DivShopPaiHang_ShowPanel";
	if(CurrentShopPaiHangFlag!=divID)
	{	        	        
	        for(var i=1;i<=3;i++)
	        {
		        _gObjByID("DivShopPaiHang_Flag" + i).removeAttribute("className");
		        _gObjByID("DivShopPaiHang_Flag" + i).removeAttribute("class");
	        }
	        _gObjByID("DivShopPaiHang_Flag" + divID).className='current';
        	
	        
	        if(CurrentShopPaiHangFlag!="")
	        {
				//alert('CurrentCircleFlag!=""');
	            obj=_gObjByID("DivShopPaiHang_Flag" + divID + "_Content");
		        _gObjByID("DivShopPaiHang_Flag" + CurrentShopPaiHangFlag + "_Content").innerHTML=objShowPanel.innerHTML;
				objShowPanel.innerHTML=GetLoadingImg();
		        if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	            {//alert('GetFromAspx');
//		            var oXmlHttp = createXMLHttp();		
//	                ShowDivContent('indexContent/DivShopPaiHang.aspx?Flag=' + divID,oXmlHttp,"DivShopPaiHangFlag"+divID,objShowPanel);
	                
	                var x = new XHR("re_OperatePaiHangDiv('DivShopPaiHang_ShowPanel','DivShopPaiHangFlag"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivShopPaiHang.aspx?Flag=" + divID ;
	                x.get(url);
	            }
	            else
	            {//alert('GetFromDiv');
		            objShowPanel.innerHTML=obj.innerHTML;
		            obj.innerHTML="";
	            }
	        }
	        else
	        {
	                var x = new XHR("re_OperatePaiHangDiv('DivShopPaiHang_ShowPanel','DivShopPaiHangFlag"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivShopPaiHang.aspx?Flag=" + divID ;
	                x.get(url);
	        }	        
	        
	        CurrentShopPaiHangFlag=divID;
	        //SwapPaiHangShopDiv('DivShopPaiHangFlag'+divID,'1');
	}
}
function re_OperatePaiHangDiv(id,id2,cc)
{
 	_gObjByID(id).innerHTML = cc;
 	SwapPaiHangShopDiv(id2,'1');
}

//餐饮 美食 菜圃 折扣
var CurrentShopMsg_FoodDiv='DivShopMsg_Food_All';
function OperateShopMsg_FoodDiv(divID)
{
	//alert(divID);
	if(CurrentShopMsg_FoodDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	var shopPanel_=null;
	objShowPanel=_gObjByID("DivShopMsg_Food_ShowPanel");
	shopPanel_="DivShopMsg_Food_ShowPanel";
	//交换数据
	_gObjByID(CurrentShopMsg_FoodDiv+"_Content").innerHTML=objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	
	_gObjByID('DivShopMsg_Food_All').className="none";
	_gObjByID('DivShopMsg_Food_Sweet').className="none";
	_gObjByID('DivShopMsg_Food_CaiPu').className="none";
	_gObjByID('DivShopMsg_Food_Discount').className="none";
	_gObjByID(divID).className='current'; 
	CurrentShopMsg_FoodDiv=divID;
	
	obj=_gObjByID(divID+"_Content");
     if(obj.innerHTML == null || obj.innerHTML == ""  || obj.innerHTML == GetLoadingImg())
	{
//		var oXmlHttp = createXMLHttp();		
//	    ShowDivContent('indexContent/' + divID + '.aspx',oXmlHttp,'',objShowPanel);
	    
	    var x = new XHR("re_OperateShopMsg_FoodDiv('DivShopMsg_Food_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/" + divID +".aspx";
	    x.get(url);
	    
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	    obj.innerHTML="";
	}
}
function re_OperateShopMsg_FoodDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}

//玩乐 桑拿 聚会 娱乐
var CurrentShopMsg_PartyDiv='DivShopMsg_Party_All';
function OperateShopMsg_PartyDiv(divID)
{
	//alert(divID);
	if(CurrentShopMsg_PartyDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	objShowPanel=_gObjByID("DivShopMsg_Party_ShowPanel");
	//交换数据
	_gObjByID(CurrentShopMsg_PartyDiv+"_Content").innerHTML=objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	
	_gObjByID('DivShopMsg_Party_All').removeAttribute("className");
	_gObjByID('DivShopMsg_Party_SanNa').removeAttribute("className");
	_gObjByID('DivShopMsg_Party_Join').removeAttribute("className");
	_gObjByID('DivShopMsg_Party_YuLe').removeAttribute("className");
	
	_gObjByID('DivShopMsg_Party_All').removeAttribute("class");
	_gObjByID('DivShopMsg_Party_SanNa').removeAttribute("class");
	_gObjByID('DivShopMsg_Party_Join').removeAttribute("class");
	_gObjByID('DivShopMsg_Party_YuLe').removeAttribute("class");
	_gObjByID(divID).className='current';
	CurrentShopMsg_PartyDiv=divID;
	
	obj=_gObjByID(divID+"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
		var x = new XHR("re_OperateShopMsg_PartyDiv('DivShopMsg_Party_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/" + divID + ".aspx";
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	    obj.innerHTML="";
	}
}
function re_OperateShopMsg_PartyDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}

//购物 促销 导购 服饰
var CurrentShopMsg_ShoppingDiv='DivShopMsg_Shopping_All';
function OperateShopMsg_ShoppingDiv(divID)
{
	//alert(divID);
	if(CurrentShopMsg_ShoppingDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	var shopPanel_=null;
	objShowPanel=_gObjByID("DivShopMsg_Shopping_ShowPanel");
	shopPanel_="DivShopMsg_Shopping_ShowPanel";
	//交换数据
	_gObjByID(CurrentShopMsg_ShoppingDiv+"_Content").innerHTML=objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	_gObjByID('DivShopMsg_Shopping_All').removeAttribute("className");
	_gObjByID('DivShopMsg_Shopping_CuXiao').removeAttribute("className");
	_gObjByID('DivShopMsg_Shopping_DaoGou').removeAttribute("className");
	_gObjByID('DivShopMsg_Shopping_FuShi').removeAttribute("className");
	
	_gObjByID('DivShopMsg_Shopping_All').removeAttribute("class");
	_gObjByID('DivShopMsg_Shopping_CuXiao').removeAttribute("class");
	_gObjByID('DivShopMsg_Shopping_DaoGou').removeAttribute("class");
	_gObjByID('DivShopMsg_Shopping_FuShi').removeAttribute("class");
	_gObjByID(divID).className='current';
	CurrentShopMsg_ShoppingDiv=divID;
	
	obj=_gObjByID(divID+"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
//		var oXmlHttp = createXMLHttp();		
//	    ShowDivContent('indexContent/' + divID + '.aspx',oXmlHttp,null,objShowPanel);
	    
	    var x = new XHR("re_OperateShopMsg_ShoppingDiv('DivShopMsg_Shopping_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/" + divID +".aspx";
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	    obj.innerHTML="";
	}
}
function re_OperateShopMsg_ShoppingDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;}

//分类图片
var CurrentClassMapDiv='1';
function OperateClassMapDiv(divID)
{
	//alert(divID);
	if(CurrentClassMapDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	objShowPanel=_gObjByID("DivClassMap_ShowPanel");
	//交换数据
	_gObjByID('DivClassMap_Num' + CurrentClassMapDiv +"_Content").innerHTML=objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	for(var i=1;i<=5;i++)
	{
		_gObjByID('DivClassMap_Num' + i).removeAttribute("className");
		_gObjByID('DivClassMap_Num' + i).removeAttribute("class");
	}
	_gObjByID('DivClassMap_Num' + divID).className='current';
	CurrentClassMapDiv=divID;
	
	obj=_gObjByID('DivClassMap_Num' + divID +"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
//		var oXmlHttp = createXMLHttp();		
//	    ShowDivContent('indexContent/DivClassMap.aspx?Num=' + divID,oXmlHttp,'',objShowPanel);

        var x = new XHR("re_OperateClassMapDiv('DivClassMap_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/DivClassMap.aspx?Num=" + divID ;
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	}
}
function re_OperateClassMapDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}

//商家 资讯点评
function displayLayerDianP(ID)
{
	     _gObjByID("Div_ShopDianP").style.display='none';
         _gObjByID("Div_MessageDianP").style.display='none';
	     _gObjByID(ID).style.display = 'block';
}

//论坛区域
function displayLayerBBS(ID)
{
	     _gObjByID("DivBbsTopic_HotBBS").style.display='none';
         _gObjByID("DivBbsTopic_NewBBS").style.display='none';
	     _gObjByID(ID).style.display = 'block';
}

//朋友圈展示
var CurrentCircleFlag='1';
function OperateCircleDiv(divID)
{
	//alert(CurrentCircleFlag);
	var obj=null;
    var objShowPanel=null;
    objShowPanel=_gObjByID("DivCircle_ShowPanel");
	if(CurrentCircleFlag!=divID)
	{	        	        
	        for(var i=1;i<=3;i++)
	        {
		        _gObjByID("DivCircle_Flag" + i).removeAttribute("className");
		        _gObjByID("DivCircle_Flag" + i).removeAttribute("class");
	        }
	        _gObjByID("DivCircle_Flag" + divID).className='current';
        	
	        
	        if(CurrentCircleFlag!="")
	        {
				//alert('CurrentCircleFlag!=""');
	            obj=_gObjByID("DivCircle_Flag" + divID + "_Content");
		        _gObjByID("DivCircle_Flag" + CurrentCircleFlag + "_Content").innerHTML=objShowPanel.innerHTML;
				objShowPanel.innerHTML=GetLoadingImg();
		        if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	            {//alert('GetFromAspx');
//		            var oXmlHttp = createXMLHttp();		
//	                ShowDivContent('indexContent/DivCircle.aspx?Flag=' + divID,oXmlHttp,"DivPaiHangFlag"+divID,objShowPanel);
	                var x = new XHR("re_OperateCircleDiv('DivCircle_ShowPanel','DivPaiHangFlag"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivCircle.aspx?Flag=" + divID ;
	                x.get(url);
	            }
	            else
	            {//alert('GetFromDiv');
		            objShowPanel.innerHTML=obj.innerHTML;
		            obj.innerHTML="";
	            }
	        }
	        else
	        {
	                var x = new XHR("re_OperateCircleDiv('DivCircle_ShowPanel','DivPaiHangFlag"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivCircle.aspx?Flag=" + divID ;
	                x.get(url);
	        }	        
	        
	        CurrentCircleFlag=divID;
	        //SwapPaiHangCircleDiv('DivPaiHangFlag'+divID,'1');
	}
}
function re_OperateCircleDiv(id,id2,cc)
{
 	_gObjByID(id).innerHTML = cc;
 	SwapPaiHangCircleDiv(id2,'1');
}

function SwapPaiHangCircleDiv(DivKey,ID)
{//alert(DivKey);
    for(var i=1;i<=10;i++)
       {
                   _gObjByID(DivKey + '_S' + i).style.display='block';
                   _gObjByID(DivKey + '_B' + i).style.display='none'; 
				   
        }
            _gObjByID(DivKey + '_S' + ID).style.display='none';              
            _gObjByID(DivKey + '_B' + ID).style.display='block';
}

//朋友圈活动展示
var CurrentCircleActionFlag='1';
function OperateCircleActionDiv(divID)
{
	//alert(CurrentCircleActionFlag);
	if(CurrentCircleActionFlag!=divID)
	{
	        var obj=null;
	        var objShowPanel=null;
	        objShowPanel=_gObjByID("DivCircleAction_ShowPanel");	        
	        for(var i=1;i<=2;i++)
	        {
		        _gObjByID("DivCircleAction_Flag" + i).removeAttribute("className");
		        _gObjByID("DivCircleAction_Flag" + i).removeAttribute("class");
	        }
	        _gObjByID("DivCircleAction_Flag" + divID).className='current';
        	
	        
	        if(CurrentCircleActionFlag!="")
	        {
				//alert('CurrentCircleActionFlag!=""');
	            obj=_gObjByID("DivCircleAction_Flag" + divID + "_Content");
	            //交换数据
		        _gObjByID("DivCircleAction_Flag" + CurrentCircleActionFlag + "_Content").innerHTML=objShowPanel.innerHTML;
				objShowPanel.innerHTML=GetLoadingImg();
		        if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	            {//alert('GetFromAspx');
//		            var oXmlHttp = createXMLHttp();		
//	                ShowDivContent('indexContent/DivCircleAction.aspx?Flag=' + divID,oXmlHttp,"DivCircleAction"+divID,objShowPanel);
	                var x = new XHR("re_OperateCircleActionDiv('DivCircleAction_ShowPanel','DivCircleAction"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivCircleAction.aspx?Flag=" + divID ;
	                x.get(url);
	            }
	            else
	            {//alert('GetFromDiv');
		            objShowPanel.innerHTML=obj.innerHTML;
		            obj.innerHTML="";
	            }
	        }
	        else
	        {
	                var x = new XHR("re_OperateCircleActionDiv('DivCircleAction_ShowPanel','DivCircleAction"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivCircleAction.aspx?Flag=" + divID ;
	                x.get(url);
	        }	        
	        
	        CurrentCircleActionFlag=divID;
	        //SwapCircleActionDiv("DivCircleAction"+divID,'1');
	}
}
function re_OperateCircleActionDiv(id,id2,cc)
{
 	_gObjByID(id).innerHTML = cc;
 	SwapCircleActionDiv(id2,'1');
}

function SwapCircleActionDiv(DivKey,ID)
{//alert(DivKey);
    for(var i=1;i<=5;i++)
       {
                   _gObjByID(DivKey + '_S' + i).style.display='block';
                   _gObjByID(DivKey + '_B' + i).style.display='none'; 
				   
        }
            _gObjByID(DivKey + '_S' + ID).style.display='none';              
            _gObjByID(DivKey + '_B' + ID).style.display='block';
}


//朋友圈图片
var CurrentCircleMapDiv='1';
function OperateCircleMapDiv(divID)
{
	//alert(divID);
	if(CurrentCircleMapDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	objShowPanel=_gObjByID("DivCircleMap_ShowPanel");
	//交换数据
	_gObjByID('DivCircleMap_Num' + CurrentCircleMapDiv +"_Content").innerHTML =objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	for(var i=1;i<=5;i++)
	{
		_gObjByID('DivCircleMap_Num' + i).removeAttribute("className");
		_gObjByID('DivCircleMap_Num' + i).removeAttribute("class");
	}
	_gObjByID('DivCircleMap_Num' + divID).className='current';
	CurrentCircleMapDiv=divID;
	
	obj=_gObjByID('DivCircleMap_Num' + divID +"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
	    var x = new XHR("re_OperateCircleMapDiv('DivCircleMap_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/DivCircleMap.aspx?Num=" + divID ;
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	}
}
function re_OperateCircleMapDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}

//活跃用户 积分排行
var CurrentActiveUserDiv='1';
function OperateActiveUserDiv(divID)
{
	//alert(divID);
	if(CurrentActiveUserDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	objShowPanel=_gObjByID("DivActiveUser_ShowPanel");
	//交换数据
	_gObjByID('DivActiveUser_Flag' + CurrentActiveUserDiv +"_Content").innerHTML = objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	for(var i=1;i<=2;i++)
	{
		_gObjByID('DivActiveUser_Flag' + i).removeAttribute("className");
		_gObjByID('DivActiveUser_Flag' + i).removeAttribute("class");
	}
	_gObjByID('DivActiveUser_Flag' + divID).className='current';
	CurrentActiveUserDiv=divID;
	
	obj=_gObjByID('DivActiveUser_Flag' + divID +"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
	    var x = new XHR("re_OperateActiveUserDiv('DivActiveUser_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/DivActiveUser.aspx?Flag=" + divID ;
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	}
}
function re_OperateActiveUserDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}


function SwapPaiHangShopDiv(DivKey,ID)
{//DivS1  DivB1
    for(var i=1;i<=10;i++)
       {
                   _gObjByID(DivKey + '_S' + i).style.display='';
                   _gObjByID(DivKey + '_B' + i).style.display='none';                        
        }
            _gObjByID(DivKey + '_S' + ID).style.display='none';              
            _gObjByID(DivKey + '_B' + ID).style.display='';
	    ShowImg("Img_"+DivKey+"_"+ID,"ImgSrc_"+DivKey+"_"+ID);
}

//想去，最想去
var CurrentGoAndWantFlag='wantgo';
function OperateGoAndWantDiv(divID)
{
	var obj=null;
    var objShowPanel=null;
    objShowPanel=_gObjByID("DivShopWantOrGo_ShowPanel");
	if(CurrentGoAndWantFlag!=divID)
	{	        	        
	        _gObjByID("DivShopWantOrGo_wantgo").removeAttribute("className");
	        _gObjByID("DivShopWantOrGo_wantgo").removeAttribute("class");
	        _gObjByID("DivShopWantOrGo_go").removeAttribute("className");
	        _gObjByID("DivShopWantOrGo_go").removeAttribute("class");
	        _gObjByID("DivShopWantOrGo_" + divID).className='current';
        	
	        
	        if(CurrentGoAndWantFlag!="")
	        {
				//alert('CurrentCircleFlag!=""');
	            obj=_gObjByID("DivShopWantOrGo_" + divID + "_Content");
		        _gObjByID("DivShopWantOrGo_" + CurrentGoAndWantFlag + "_Content").innerHTML=objShowPanel.innerHTML;
				objShowPanel.innerHTML=GetLoadingImg();
		        if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	            {//alert('GetFromAspx');
//		            var oXmlHttp = createXMLHttp();		
//	                ShowDivContent('indexContent/DivShopWantOrGo.aspx?Flag=' + divID,oXmlHttp,"DivShop"+divID,objShowPanel);
	                var x = new XHR("re_OperateGoAndWantDiv('DivShopWantOrGo_ShowPanel','DivShop"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivShopWantOrGo.aspx?Flag=" + divID ;
	                x.get(url);
	            }
	            else
	            {//alert('GetFromDiv');
		            objShowPanel.innerHTML=obj.innerHTML;
		            obj.innerHTML="";
	            }
	        }
	        else
	        {
//	                var oXmlHttp = createXMLHttp();		
//	                ShowDivContent('indexContent/DivShopPaiHang.aspx?Flag=' + divID,oXmlHttp,"DivShop"+divID,objShowPanel);
	                var x = new XHR("re_OperateGoAndWantDiv('DivShopWantOrGo_ShowPanel','DivShop"+divID+"')");
 	                //alert(url);
 	                var url = "indexContent/DivShopWantOrGo.aspx?Flag=" + divID ;
	                x.get(url);
	        }	        
	        
	        CurrentGoAndWantFlag=divID;
	        //SwapGoAndWantShopDiv('DivShop'+divID,'1');
	}
}
function re_OperateGoAndWantDiv(id,id2,cc)
{
 	_gObjByID(id).innerHTML = cc;
 	SwapGoAndWantShopDiv(id2,'1');
}

function SwapGoAndWantShopDiv(DivKey,ID)
{//DivS1  DivB1
    for(var i=1;i<=10;i++)
       {
                   _gObjByID(DivKey + '_S' + i).style.display='block';
                   _gObjByID(DivKey + '_B' + i).style.display='none';                        
        }
            _gObjByID(DivKey + '_S' + ID).style.display='none';              
            _gObjByID(DivKey + '_B' + ID).style.display='block';
            ShowImg("Img_"+DivKey+"_"+ID,"ImgSrc_"+DivKey+"_"+ID);
}

//商家点评 资讯点评
var CurrentDianPingDiv='1';
function OperateDianPingDiv(divID)
{
	//alert(divID);
	if(CurrentDianPingDiv==divID) return;
	var obj=null;
	var objShowPanel=null;
	objShowPanel=_gObjByID("DivDianPing_ShowPanel");
	//交换数据
	_gObjByID('DivDianPing_Flag' + CurrentDianPingDiv +"_Content").innerHTML = objShowPanel.innerHTML;
	objShowPanel.innerHTML=GetLoadingImg();
	for(var i=1;i<=2;i++)
	{
		_gObjByID('DivDianPing_Flag' + i).removeAttribute("className");
		_gObjByID('DivDianPing_Flag' + i).removeAttribute("class");
	}
	_gObjByID('DivDianPing_Flag' + divID).className='current';
	CurrentDianPingDiv=divID;
	
	obj=_gObjByID('DivDianPing_Flag' + divID +"_Content");
     if(obj.innerHTML == null || obj.innerHTML == "" || obj.innerHTML == GetLoadingImg())
	{
		var x = new XHR("re_OperateDianPingDiv('DivDianPing_ShowPanel')");
 	    //alert(url);
 	    var url = "indexContent/DivDianPing.aspx?Flag=" + divID ;
	    x.get(url);
	}
	else
	{
	    objShowPanel.innerHTML=obj.innerHTML;
	}
}
function re_OperateDianPingDiv(id,cc)
{
 	_gObjByID(id).innerHTML = cc;
}

//便民服务
function OperateServerDiv(divID)
{
	_gObjByID('DivServer1').style.display='none';
	_gObjByID('DivServer2').style.display='none';
	_gObjByID('DivServer'+divID).style.display='block'; 
}

//滚动 
var preTop;
var leftElem;
var currentTop;
var marqueesHeight;
function marque(width,height,marqueName,marqueCName){
	try{
	  marqueesHeight = height;
	  stopscroll     = false;

	  scrollElem = document.getElementById(marqueName);
	  with(scrollElem){
		style.width     = width;
		style.height    = marqueesHeight;
		style.overflow  = 'hidden';
		noWrap          = true;
	  }

	  scrollElem.onmouseover = new Function('stopscroll = true');
	  scrollElem.onmouseout  = new Function('stopscroll = false');

	  preTop     = 0; 
	  currentTop = 0; 
	  stoptime   = 0;

	  leftElem = document.getElementById(marqueCName);
	  scrollElem.appendChild(leftElem.cloneNode(true));
		  
	  init_srolltext();

	}catch(e) {}
}
function init_srolltext(){
  scrollElem.scrollTop = 0;
  setInterval('scrollUp()', 24);
}

function scrollUp(){
  if(stopscroll) return;
  currentTop += 1;
  if(currentTop == marqueesHeight+1) 
  {
    stoptime += 1;
    currentTop -= 1;
    if(stoptime == (marqueesHeight)*4) {//停顿时间
      currentTop = 0;
      stoptime = 0;
    }
  }else
  {
    preTop = scrollElem.scrollTop;
    scrollElem.scrollTop += 1;
    if(preTop == scrollElem.scrollTop){
      scrollElem.scrollTop = marqueesHeight;
      scrollElem.scrollTop += 1;
    }
  }
}

var preTops;
var leftElems;
var currentTops;
var marqueesHeights;
function marques(width,height,marqueName,marqueCName){
	try{
	  marqueesHeights = height;
	  stopscrolls     = false;

	  scrollElems = document.getElementById(marqueName);
	  with(scrollElems){
		style.width     = width;
		style.height    = marqueesHeights;
		style.overflow  = 'hidden';
		noWrap          = true;
	  }

	  scrollElems.onmouseover = new Function('stopscrolls = true');
	  scrollElems.onmouseout  = new Function('stopscrolls = false');

	  preTops     = 0; 
	  currentTops = 0; 
	  stoptimes   = 0;

	  leftElems = document.getElementById(marqueCName);
	  scrollElems.appendChild(leftElems.cloneNode(true));
		  
	  init_srolltexts();

	}catch(e) {}
}
function init_srolltexts(){
  scrollElems.scrollTop = 0;
  setInterval('scrollUps()', 24);
}

function scrollUps(){
  if(stopscrolls) return;
  currentTops += 1;
  if(currentTops == marqueesHeights+1) 
  {
    stoptimes += 1;
    currentTops -= 1;
    if(stoptimes == (marqueesHeights)*4) {//停顿时间
      currentTops = 0;
      stoptimes = 0;
    }
  }else
  {
    preTops = scrollElems.scrollTop;
    scrollElems.scrollTop += 1;
    if(preTops == scrollElems.scrollTop){
      scrollElems.scrollTop = marqueesHeights;
      scrollElems.scrollTop += 1;
    }
  }
}