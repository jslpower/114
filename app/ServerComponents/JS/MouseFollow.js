//鼠标跟随代码//
/*
need jquery library 1.3.2 above.

param:  e:srcElement,,str:message.
*/
function wsug(e, str){ 
	var oThis = arguments.callee;
	if(!str) {
	    if(oThis.sug){
		    oThis.sug.style.visibility = 'hidden';
		}
		document.onmousemove = null;
		return;
	}		
	if(!oThis.sug){
		var div = document.createElement('div'), css = 'top:0; left:-30px;text-align:left;color:#2C709F;position:absolute; z-index:501; visibility:hidden';
			div.style.cssText = css;
			div.setAttribute('style',css);
		var sug = document.createElement('div'), css= 'font:normal 12px/16px "宋体"; white-space:nowrap; color:#666; padding:3px; position:absolute; left:-30px; top:0; z-index:10; background:#f9fdfd; border:1px solid #629BC7;text-align:left;color:#2C709F;';
			sug.style.cssText = css;
			sug.setAttribute('style',css);
		var dr = document.createElement('div'), css = 'position:absolute; top:3; left:-27; background-color:#333; filter:alpha(opacity=30); opacity:0.3; z-index:9';
			dr.style.cssText = css;
			dr.setAttribute('style',css);
		var ifr = document.createElement('iframe'), css='position:absolute; left:0; top:-10; z-index:8; filter:alpha(opacity=0); opacity:0';
			ifr.style.cssText = css;
			ifr.setAttribute('style',css);
		div.appendChild(ifr);
		div.appendChild(dr);
		div.appendChild(sug);
		div.sug = sug;
		document.body.appendChild(div);
		oThis.sug = div;
		oThis.dr = dr;
		oThis.ifr = ifr;
		div = dr = ifr = sug = null;
	}
	//var e = e || window.event;
	var offset = $(e).offset();
	
	var obj = oThis.sug, dr = oThis.dr, ifr = oThis.ifr;
	obj.sug.innerHTML = str;
	
	var w = obj.sug.offsetWidth, h = obj.sug.offsetHeight, dw = document.documentElement.clientWidth||document.body.clientWidth; dh = document.documentElement.clientHeight || document.body.clientHeight;
	var st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
	//var st=0,sl=0;
	//var left = offset.left +sl  < dw + sl && offset.left + sl + 15 || offset.left +sl-8 - w, top = offset.top + st + 17;
	var left = 0,top=0;
	if((offset.left+w)<(dw+sl)){
	    left = offset.left;
	    
	}else{
	    left = offset.left-(offset.left+w-dw-sl);
	}
	left+=$(e).outerWidth()
	top = offset.top+$(e).outerHeight();
	obj.style.left = left + 'px';
	obj.style.top = top  + 'px';
	//dr.style.width = w + 'px';
	//dr.style.height = h + 'px';
	ifr.style.width = w + 3 + 'px';
	ifr.style.height = h + 3 + 'px';
	obj.style.visibility = 'visible';
//	document.onmousemove = function(e){ 
//		var e = e || window.event, st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
//		var left = e.clientX +sl +17 + w < dw + sl && e.clientX + sl + 15 || e.clientX +sl-8 - w, top = e.clientY + st +17 + h < dh + st && e.clientY + st + 17 || e.clientY + st - 5 - h;
//		obj.style.left = left + 'px';
//		obj.style.top = top + 'px';
//	}
}

/*for title Recommended   推荐理由*/
var tooltip = function() {
    var id = 'tt';
    var top = 3;
    var left = 3;
    var maxw = 300;
    var speed = 10;
    var timer = 20;
    var endalpha = 95;
    var alpha = 0;
    var tt, t, c, b, h;
    var ie = document.all ? true : false;
    return {
        show: function(v, w) {
            if (tt == null || tt == undefined) {
                tt = document.createElement('div');
                tt.setAttribute('id', id);
                t = document.createElement('div');
                t.setAttribute('id', id + 'top');
                c = document.createElement('div');
                c.setAttribute('id', id + 'cont');
                b = document.createElement('div');
                b.setAttribute('id', id + 'bot');
                tt.appendChild(t);
                tt.appendChild(c);
                tt.appendChild(b);
                document.body.appendChild(tt);
                tt.style.opacity = 0;
                tt.style.filter = 'alpha(opacity=0)';
                document.onmousemove = this.pos;
            }
            tt.style.display = 'block';
            c.innerHTML = v;
            tt.style.width = w ? w + 'px' : 'auto';
            if (!w && ie) {
                t.style.display = 'none';
                b.style.display = 'none';
                tt.style.width = tt.offsetWidth;
                t.style.display = 'block';
                b.style.display = 'block';
            }
            if (tt.offsetWidth > maxw) { tt.style.width = maxw + 'px' }
            h = parseInt(tt.offsetHeight) + top;
            clearInterval(tt.timer);
            tt.timer = setInterval(function() { tooltip.fade(1) }, timer);
        },
        pos: function(e) {
            var u = ie ? event.clientY + document.documentElement.scrollTop : e.pageY;
            var l = ie ? event.clientX + document.documentElement.scrollLeft : e.pageX;
            tt.style.top = (u - h) + 'px';
            tt.style.left = (l + left) + 'px';
        },
        fade: function(d) {
            var a = alpha;
            if ((a != endalpha && d == 1) || (a != 0 && d == -1)) {
                var i = speed;
                if (endalpha - a < speed && d == 1) {
                    i = endalpha - a;
                } else if (alpha < speed && d == -1) {
                    i = a;
                }
                alpha = a + (i * d);
                tt.style.opacity = alpha * .01;
                tt.style.filter = 'alpha(opacity=' + alpha + ')';
            } else {
                clearInterval(tt.timer);
                if (d == -1) { tt.style.display = 'none' }
            }
        },
        hide: function() {
            clearInterval(tt.timer);
            tt.timer = setInterval(function() { tooltip.fade(-1) }, timer);
        }
    };
} ();
