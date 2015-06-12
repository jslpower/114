// JavaScript Document

//显示动态切换的图片
function ShowChangeImg(pic_width, pic_height, button_pos, stop_time, show_text, txtcolor, bgcolor, resultUrl, resultText, resultLinkId, strSpace, pageUrl)
{
    var pic_width = pic_width;   //图片宽度
    var pic_height = pic_height;   //图片高度
    var button_pos = button_pos; //按扭位置 1左 2右 3上 4下
    var stop_time = stop_time; //图片停留时间(1000为1秒钟)
    var show_text = show_text; //
    var txtcolor = txtcolor;  //文本颜色
    var bgcolor = bgcolor;   //背景颜色
                    
    //获得图片路径,图片标题,以及图片要进行链接的ID的字符串(每张图片的图片路径以分隔符号分隔)
    var resultUrl = resultUrl;
    var resultText = resultText;
    var resultLinkId = resultLinkId;
    var strSpace = strSpace;     //分隔符号
    var index = 0;
    var tmp = "";
    var len = 0;  //元素个数

    var imgLink=new Array();
    var imgUrl=new Array();
    var imgtext=new Array();

    var pics="", links="", texts="";    		
    				
    if(resultUrl != null && resultUrl != "")
    {
	    tmp = resultUrl.split(strSpace);
	    if(tmp != null && tmp.length > 0)
	    {
	        len = tmp.length;
		    for(index=0; index<tmp.length; index++)
			    imgUrl[index] = tmp[index];		
	    }
    }
    		
    if(resultLinkId != null && resultLinkId != "")
    {
	    tmp = resultLinkId.split(strSpace);
	    if(tmp != null && tmp.length > 0)
	    {
		    for(index=0; index<tmp.length; index++)
		    {
		        if(pageUrl != "")
			        imgLink[index] = escape(pageUrl + tmp[index]);	
				else
				{
					imgLink[index] = tmp[index];
	            }		
		    }
	    }
    }

    if(resultText != null && resultText != "")
    {
	    tmp = resultText.split(strSpace);
	    if(tmp != null && tmp.length > 0)
	    {
		    for(index=0; index<tmp.length; index++)
			    imgtext[index] = tmp[index];		
	    }
    }
                    
    for(var i=0; i<len; i++)
    {
	    pics=pics+("|"+imgUrl[i]);
	    texts=texts+("|"+imgtext[i]);
		
		if(imgLink[i] == '' || imgLink[i] == undefined)
			links = links + ("|javascript:return false;");
		else
			links=links+("|"+imgLink[i]);
    }
    
    pics=pics.substring(1);
    links=links.substring(1);
    texts=texts.substring(1);

    var swf_height=show_text==1?pic_height+20:pic_height;

    document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cabversion=6,0,0,0" width="'+ pic_width +'" height="'+ swf_height +'">');
document.write('<param name="movie" value="/Images/AddedServices/EShop/focus.swf">');
document.write('<param name="quality" value="high"><param name="wmode" value="transparent">');
document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'">');
document.write('<embed src="http://localhost:30001/images/seniorshop/focus.swf" wmode="transparent" FlashVars="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'" quality="high" width="'+ pic_width +'" height="'+ swf_height +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
document.write('</object>');

}