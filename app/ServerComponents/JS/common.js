// JavaScript Document

function showSub(layerid)
{
	theLayers = $('teamsubnav').getElementsByTagName("div");
	for(i=0;i<theLayers.length;i++)
	{
		if(theLayers[i].className=="subnavlayer")
		{
			$(theLayers[i].id+"_a").className="";
			theLayers[i].style.display="none";
		}
	}
	if(layerid=="")
		return false;
	else
	{
		if($(layerid).style.display!="none")
		{
			$(layerid).style.display="none";
		}
		else
		{
			$(layerid+"_a").className="selected";
			$(layerid).style.display="block";
		}
	}
}
function scrollMe(str) {
	clearTimeout(x)
	if(stopScroll==1) {
		return
	}
	$(str).scrollTop=$(str).scrollTop+1
	if($(str).scrollTop>=repeatHeight)
		$(str).scrollTop=0;
	if($(str).scrollTop*10%repeatHeight!=0) {
		// keep on scrolin' 
		x = setTimeout("scrollMe('"+str+"')",40)
	}
	else { //we have hit the wrap point
		x = setTimeout("scrollMe('"+str+"')",1000)
	}
}
function notice_pay(str)
{
	if($(str+'_notice').style.display=='none')
	{
		$(str+'_notice').style.display='block';
		$(str+'_notice_obj').style.display='block';
	}
	else
	{
		$(str+'_notice').style.display='none';
		$(str+'_notice_obj').style.display='none';
	}
}

function showContext(obj,content)
{
	if($(obj))
	{
		$(obj).innerHTML = content;
	}
}
var iFloat=null;
function showFloatCategory(str) {
	document.getElementById(str).style.display="block";
	if(iFloat!=null)
		clearInterval(iFloat);
}
function hideFloatCategory(str)
{
	obj = document.getElementById(str);
	if(obj!=null)
		iFloat = setInterval('document.getElementById("'+str+'").style.display="none"',300);
}