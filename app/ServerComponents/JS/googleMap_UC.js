/*
---zhouwenchao [google Map 在线标注与搜索]
*/
function GRulerControl(option) {
    var me = this; // 可国际化的字符串 
    obj = $.extend({
        //删除标题
        DELETE_BUTTON_TITLE_: '删除标注',
        //保存标题
        SAVE_BUTTON_TITLE_: '点此确定设置',
        //搜索栏背景图片
        BACKGROUND_IMAGE_: 'http://www.google.com/intl/zh-CN/apis/maps/demo/distcal/images/ruler_background.png',
        Domain: 'http://localhost:30001',
        //标注保存事件
        SaveF: function() { },
        CancelF: function() { },
        BackUrl: 'javascript:;',
        MidMarker: ''
    }, option || {});
    me.DELETE_BUTTON_TITLE_ = obj.DELETE_BUTTON_TITLE_;
    me.SAVE_BUTTON_TITLE_ = obj.SAVE_BUTTON_TITLE_;
    me.BACKGROUND_IMAGE_ = obj.BACKGROUND_IMAGE_;
    me.SaveF = obj.SaveF; //保存事件
    me.CancelF = obj.CancelF;
    me.Domain = obj.Domain;
    me.BackUrl = obj.BackUrl;
    me.MidMarker=obj.MidMarker;
}

GRulerControl.prototype = new GControl();
/** * 初始化搜索控件 */
GRulerControl.prototype.initialize = function(map) {
    var me = this;
    var container = document.createElement('div');
    container.className = 'gels-form gels-form-idle';

    var div1 = document.createElement('div');
    div1.className = 'gels-form-div';

    var img = document.createElement('img');
    img.style.marginBottom = "-5px";
    img.src = me.Domain + '/scenicspots/T1/images/google_search.png';
    img.className = 'gels-logo';
    div1.appendChild(img);

    var txt = document.createElement('input');
    txt.type = 'text';
    txt.id = 'address';
    txt.Title = '搜索';
    txt.className = 'gels-input';
    txt.style.width = '168px';
    txt.onkeydown = function(e) {
        var kcode;
        if ($.browser.msie)
            kcode = event.keyCode;
        else
            kcode = e.keyCode;
        if (kcode == 13) {
            showAddress(map,me);
            return false;
        }
    }
    div1.appendChild(txt);

    var space1 = document.createElement('label');
    space1.style.marginLeft = "2px";
    div1.appendChild(space1);

    var img1 = document.createElement('img');
    img1.src = me.Domain + '/scenicspots/T1/images/search.gif';
    img1.id = 'btn_Search';
    img1.style.cursor = 'hand';
    img1.style.marginBottom = "-5px";
    img1.onclick = function() {
        showAddress(map,me);
    };
    div1.appendChild(img1);

    var space = document.createElement('label');
    space.style.marginLeft = "2px";
    div1.appendChild(space);

//    var img2 = document.createElement('img');
//    img2.src = me.Domain + '/scenicspots/T1/images/end.gif';
//    img2.id = 'btn_End';
//    img2.style.marginBottom = "-5px";
//    img2.style.cursor = 'hand';
//    img2.onclick = function() {
//        window.open(me.BackUrl, '_self');
//    };
//    div1.appendChild(img2);

    container.appendChild(div1);
    me.setButtonStyle_(container);

    // 初始化内部变量 
    map.rulerControl_ = me;
    me.map_ = map;
    me.head_ = new Object();
    me.tail_ = new Object();
    me.head_.next_ = me.tail_;
    me.tail_.prev_ = me.head_;
    me.setEnabled(true);
    map.getContainer().appendChild(container);
    return container;
}
/** * 返回控件的默认位置 */
GRulerControl.prototype.getDefaultPosition = function() {
    return new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(8, 8));
}
/** * 返回控件是否已启用 */
GRulerControl.prototype.isEnabled = function() {
    return this.enabled_;
}
/** * 设置控件的“启用/禁用"状态 */
GRulerControl.prototype.setEnabled = function(value) {
    var me = this;
    if (value == me.enabled_)
        return;
    me.enabled_ = value;
    if (me.enabled_) {
        me.mapClickHandle_ = GEvent.addListener(me.map_, 'click', me.onMapClick_);
    } else {
        GEvent.removeListener(me.mapClickHandle_);
    }
}

/** * 设置控件的格式 */
GRulerControl.prototype.setButtonStyle_ = function(button) {
    button.style.backgroundImage = 'url(' + this.BACKGROUND_IMAGE_ + ')';
    //button.style.font = "small Arial"; 
    button.style.border = "1px solid #888888";
    button.style.padding = "2px";
    button.style.textAlign = "center";
    button.style.cursor = "pointer";
}
/** * 格式化经纬度为字符串 */
GRulerControl.prototype.formatLatLng_ = function(pt) {
    return "将此处设置为新所在地！";
    var me = this;
    var latName, lngName;
    var lat = pt.lat();
    var lng = pt.lng();
    latName = lat >= 0 ? '北纬' : '南纬';
    lngName = lng >= 0 ? '东经' : '西经';
    return lngName + lng + '<br>' + latName + lat; //lngName + me.formatDegree_(lng) + '<br/>' + latName + me.formatDegree_(lat); 
}

/** * 事件处理函数：当用户单击地图时，要在该位置添加一个标注 */
GRulerControl.prototype.onMapClick_ = function(marker, point) {
    var me = this.rulerControl_;
    // 如果用户单击的是标注，不再这里处理 
    if (marker)
        return;
    //保证只有一个标注
    me.reset();
    // 创建标注，并添加到链表中
    var newMarker = new GMarker(point, { draggable: true });
    var pos = me.tail_.prev_;
    newMarker.prev_ = pos;
    newMarker.next_ = pos.next_;
    pos.next_.prev_ = newMarker;
    pos.next_ = newMarker;
    // 为标注添加事件处理函数：拖拽标注时要更新连接线段和距离
    GEvent.addListener(newMarker, 'dragend', function() {
        me.map_.closeInfoWindow();
        me.updateSegments_(newMarker);
    });
    // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
    GEvent.addListener(newMarker, 'click', function() {
        newMarker.openInfoWindow(me.createInfoWindow_(newMarker));
    });
    // 将创建的标注添加到地图中 
    me.map_.addOverlay(newMarker);
    //默认显示提示DIV 
    newMarker.openInfoWindow(me.createInfoWindow_(newMarker));
}
/** * 清除所有标注，初始化链表 */
GRulerControl.prototype.reset = function() {
    var me = this;
    me.map_.clearOverlays(); //清除所有标注
    me.head_ = new Object();
    me.tail_ = new Object();
    me.head_.next_ = me.tail_;
    me.tail_.prev_ = me.head_;
    //me.updateDistance_(); 
}

/** * 事件处理函数：当用户拖拽标注、标注坐标改变时被调用，这里要更新与该标注连接的线段 * @param {GMarker} marker 被拖拽的标注 */
GRulerControl.prototype.updateSegments_ = function(marker) {
    var me = this; var segment; // 更新连接前驱的线段 
    if (marker.segPrev_ && marker.prev_.getPoint) {
        // 从地图上删除旧的线段 
        me.map_.removeOverlay(marker.segPrev_);
        // 根据标注的当前坐标构造新的线段，并更新链表结点的相关字段 
        segment = [marker.prev_.getPoint(), marker.getPoint()];
        marker.segPrev_ = new GPolyline(segment);
        marker.prev_.segNext_ = marker.segPrev_; // 将新线段添加到地图中 
        me.map_.addOverlay(marker.segPrev_);
    }
    // 更新连接后继的线段，与上类似
    if (marker.segNext_ && marker.next_.getPoint) {
        me.map_.removeOverlay(marker.segNext_);
        segment = [marker.getPoint(), marker.next_.getPoint()];
        marker.segNext_ = new GPolyline(segment);
        marker.next_.segPrev_ = marker.segNext_;
        me.map_.addOverlay(marker.segNext_);
    }
}

/** * 为气泡提示窗口创建 DOM 对象，包括标注的坐标和“删除”按钮 * @param {GMarker} marker 对应的标注 */
GRulerControl.prototype.createInfoWindow_ = function(marker) {
    var me = this;
    // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签 
    var div = document.createElement('div');
    div.style.fontSize = '10.5pt';
    div.style.width = '250px';
    div.innerHTML = me.formatLatLng_(marker.getPoint());
    var hr = document.createElement('hr');
    hr.style.border = 'solid 1px #cccccc';
    div.appendChild(hr);

    var div1 = document.createElement('div');
    //创建“保存”按钮
    var save = document.createElement('div');
    save.innerHTML = me.SAVE_BUTTON_TITLE_;
    save.style.color = '#0000cc';
    save.style.cursor = 'pointer';
    save.style.marginRight = "5px";
    if (!$.browser.msie)
        save.style.cssFloat = 'left';
    else
        save.style.styleFloat = 'left';
    save.style.textDecoration = 'underline';
    //save.style.width='140px';
    //保存事件
    var p = marker.getPoint()
    save.onclick = function() {
        me.SaveF(p.lng(), p.lat(), me.map_.getZoom());
    };
    div1.appendChild(save);

    //创建返回上一页按钮    2011-11-16 注释-----
    //	var aback=document.createElement('div'); 
    //	aback.innerHTML="结束设置";
    //	aback.style.color = '#0000cc'; 
    //	aback.style.cursor = 'pointer'; 
    //	aback.style.marginRight="5px";
    //	if(!$.browser.msie)
    //	    aback.style.cssFloat='left';
    //	else
    //	    aback.style.styleFloat='left';
    //	//lnk.style.width='40px';
    //	aback.style.textDecoration = 'underline'; 
    //	
    //	// 为“删除”按钮添加事件
    //	aback.onclick = function() { 
    //	   window.open(me.BackUrl,'_self');
    //	}; 
    //	div1.appendChild(aback);
    //---------------------------------------------	
    // 创建“删除”按钮 
    var lnk = document.createElement('div');
    lnk.innerHTML = me.DELETE_BUTTON_TITLE_;
    lnk.style.color = '#0000cc';
    lnk.style.cursor = 'pointer';
    if (!$.browser.msie)
        lnk.style.cssFloat = 'left';
    else
        lnk.style.styleFloat = 'left';
    //lnk.style.width='40px';
    lnk.style.textDecoration = 'underline';

    // 为“删除”按钮添加事件
    lnk.onclick = function() {
        me.map_.closeInfoWindow();
        me.reset(); //删除所有标注
        //2011-11-16 添加
        me.CancelF();
    };
    div1.appendChild(lnk);

    div.appendChild(div1);
    // 当用户关闭气泡时 Google Map API 会自动释放该对象 
    return div;
}



var oldmarker=null;

/*自定义搜索功能*/
function showAddress(smap,me) {
    var search = document.getElementById("address").value;
    if ($.trim(search) == "") {
        alert("请输入地址！");
        return false;
    }
    smap.clearOverlays();
    var point;
    new GClientGeocoder(new GFactualGeocodeCache()).getLocations(search, function(result) {
        if (result.Status.code == G_GEO_SUCCESS) {
            for (var i = 0; i < result.Placemark.length; i++) {
                var p = result.Placemark[i].Point.coordinates;
                point = new GLatLng(p[1], p[0]);
                if (i == 0) {   
                    //原中心坐标
                    var oldcenter=smap.getCenter();
                    smap.setCenter(point, 13);
                    var ico = new GIcon(G_DEFAULT_ICON, me.Domain+'/scenicspots/T1/images/googleMapRed.png');
                    if(oldmarker!=null)//第二次搜索
                    {
                        oldmarker.closeInfoWindow();
                        oldmarker.setLatLng(point);
                    }
                    else//第一次搜索
                    {
                        if(me.MidMarker!=null&&me.MidMarker!='')
                        {
                            me.MidMarker.closeInfoWindow();
                            me.MidMarker.setLatLng(point);
                        }
                        
                    }
                    var targetPt = new GLatLng(p[1], p[0]);
                    var marker = new GMarker(targetPt, { icon: new GIcon(ico),title:$.trim(search) });   
                    GEvent.addListener(marker, 'click', function() { 
			            marker.openInfoWindow(me.createInfoWindow_(marker)); 
	                } );
                    smap.addOverlay(marker); 
                    marker.openInfoWindow(me.createInfoWindow_(marker)); 
                    oldmarker=marker; 
                }
            }
        }
        else {
            alert('未能找到：' + search);
        }
    });
}