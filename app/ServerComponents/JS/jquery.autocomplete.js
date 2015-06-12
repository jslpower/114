jQuery.autocomplete = function(input, options) {

    // Create a link to self
    var me = this;

    // Create jQuery object for input element
    var $input = $(input).attr("autocomplete", "off");


    // Apply inputClass if necessary
    if (options.inputClass) $input.addClass(options.inputClass);

    // Create results
    var results = document.createElement("div");
    // Create jQuery object for results
    var $results = $(results);
    $results.hide().addClass(options.resultsClass).css("position", "absolute");
    if (options.width > 0) {
        $results.css("width", options.width);
    }
    // Add to body element
    $("body").append(results);

    input.autocompleter = me;

    var timeout = null;
    var prev = "";
    var active = -1;
    var cache = {};
    var keyb = false;
    var hasFocus = false;
    var lastKeyPressCode = null;

    // flush cache
    function flushCache() {
        cache = {};
        cache.data = {};
        cache.length = 0;
    };

    // flush cache
    flushCache();

    // if there is a data array supplied
    if (options.data != null) {
        var sFirstChar = "", stMatchSets = {}, row = [];

        // no url was specified, we need to adjust the cache length to make sure it fits the local data store
        if (typeof options.url != "string") options.cacheLength = 1;

        // loop through the array and create a lookup structure
        for (var i = 0; i < options.data.length; i++) {
            // if row is a string, make an array otherwise just reference the array
            row = ((typeof options.data[i] == "string") ? [options.data[i]] : options.data[i]);

            // if the length is zero, don't add to list
            if (row[0].length > 0) {
                // get the first character
                sFirstChar = row[0].substring(0, 1).toLowerCase();
                // if no lookup array for this character exists, look it up now
                if (!stMatchSets[sFirstChar]) stMatchSets[sFirstChar] = [];
                // if the match is a string
                stMatchSets[sFirstChar].push(row);
            }
        }

        // add the data items to the cache
        for (var k in stMatchSets) {
            // increase the cache size
            options.cacheLength++;
            // add to the cache
            addToCache(k, stMatchSets[k]);
        }
    }

    $input
	.keydown(function(e) {
	    // track last key pressed
	    lastKeyPressCode = e.keyCode;
	    switch (e.keyCode) {
	        case 16:
	        case 17:
	        case 18:
	            break;
	        case 38: // up
	            e.preventDefault();
	            moveSelect(-1);
	            break;
	        case 40: // down
	            e.preventDefault();
	            moveSelect(1);
	            break;
	        case 9:  // tab
	        case 13: // return
	            if (selectCurrent()) {
	                // make sure to blur off the current field
	                $input.get(0).blur();
	                e.preventDefault();
	            }
	            break;
	        default:
	            active = -1;
	            if (timeout) clearTimeout(timeout);
	            timeout = setTimeout(function() { onChange(); }, options.delay);
	            break;
	    }
	})
	
	.focus(function() {
	    // track whether the field has focus, we shouldn't process any results if the field no longer has focus		
	    hasFocus = true;
	    if (options.IsFocusShow)
	        requestData(' ');
	})
	.blur(function() {
	    // track whether the field has focus
	    hasFocus = false;
	    hideResults();
	});

    hideResultsNow();

    function onChange() {
        // ignore if the following keys are pressed: [del] [shift] [capslock]
        if (lastKeyPressCode == 46 || (lastKeyPressCode > 8 && lastKeyPressCode < 32)) return $results.hide();
        var v = $input.val();
        if (v == prev) return;
        prev = v;
        if (v.length >= options.minChars) {
            $input.addClass(options.loadingClass);
            requestData(v);
        } else {
            $input.removeClass(options.loadingClass);
            $results.hide();
        }
    };

    function moveSelect(step) {
        var lis = $("tr", results);
        if (!lis) return;

        active += step;

        if (active < 0) {
            active = 0;
        } else if (active >= lis.size()) {
            active = lis.size() - 1;
        }

        lis.removeClass("ac_over");

        $(lis[active]).addClass("ac_over");

        // Weird behaviour in IE
        // if (lis[active] && lis[active].scrollIntoView) {
        // 	lis[active].scrollIntoView(false);
        // }

    };

    //使用键盘进行选择的时候调用
    function selectCurrent() {
        var tr = $("tr.ac_over", results)[0];
        if (!tr) {
            var $tr = $("tr", results);
            if (options.selectOnly) {
                if ($tr.length == 1) tr = $tr[0];
            } else if (options.selectFirst) {
                tr = $tr[0];
            }
        }
        if (tr) {
            selectItem(tr);
            return true;
        } else {
            return false;
        }
    };

    //使用鼠标或键盘选择都会调用
    function selectItem(tr) {
        if (!tr) {
            tr = document.createElement("tr");
            tr.extra = [];
            tr.selectValue = "";
        }
        var v = $.trim(tr.selectValue ? tr.selectValue : tr.innerHTML);
        var $tr = $("td", v);
        var text = "";
        if (tr.cells.length > 0) {
            text = $(tr.cells[0]).html();
            //text = tr.cells[0].innerText;
        }

        input.lastSelected = text;
        prev = text;
        $results.html("");
        $input.val(text);
        hideResultsNow();
        if (options.onItemSelect) setTimeout(function() { options.onItemSelect(tr, $input) }, 1); //2010-10-21xuty加传入了$input参数
        //判断是否要选择了之后调用事件
        /* if (options.openSelectedEvent) {
        if (typeof (SelectedEvent) != "undefined" && options.openSelectedEventName == "")
        SelectedEvent();  //在调用的页面统一命名的js函数 
        if (typeof (GetDataFuntion) != "undefined" && options.openSelectedEventName == "GetDataFuntion") {
        //throw '11'
        GetDataFuntion($input[0]);  //在调用的页面统一命名的js函数
        }
        //		    var fun = new FunEvent();
        //            for (var i in options.addSelectedFunParams) {
        //			    fun.addFun(options.addSelectedFunParams[i]);
        //			}

        }*/
    };

    // selects a portion of the input string
    function createSelection(start, end) {
        // get a reference to the input element
        var field = $input.get(0);
        if (field.createTextRange) {
            var selRange = field.createTextRange();
            selRange.collapse(true);
            selRange.moveStart("character", start);
            selRange.moveEnd("character", end);
            selRange.select();
        } else if (field.setSelectionRange) {
            field.setSelectionRange(start, end);
        } else {
            if (field.selectionStart) {
                field.selectionStart = start;
                field.selectionEnd = end;
            }
        }
        field.focus();
    };

    // fills in the input box w/the first match (assumed to be the best match)
    function autoFill(sValue) {
        // if the last user key pressed was backspace, don't autofill
        if (lastKeyPressCode != 8) {
            // fill in the value (keep the case the user has typed)
            $input.val($input.val() + sValue.substring(prev.length)); //alert($input.val() + sValue.substring(prev.length));
            // select the portion of the value not typed by the user (so the next character will erase)
            createSelection(prev.length, sValue.length);
        }
    };

    function showResults() {
        // get the position of the input field right now (in case the DOM is shifted)
        var pos = findPos(input);
        // either use the specified width, or autocalculate based on form element
        var iWidth = (options.width > 0) ? options.width : $input.width() + options.addWidth;
        // reposition
        $results.css({
            width: parseInt(iWidth) + "px",
            top: (pos.y + input.offsetHeight) + "px",
            left: pos.x + "px"
        }).show();
    };

    function hideResults() {
        if (timeout) clearTimeout(timeout);
        timeout = setTimeout(hideResultsNow, 200);
    };

    function hideResultsNow() {
        if (timeout) clearTimeout(timeout);
        $input.removeClass(options.loadingClass);
        if ($results.is(":visible")) {
            $results.hide();
        }
        if (options.mustMatch) {
            var v = $input.val();
            if (v != input.lastSelected) {
                selectItem(null);
            }
        }
    };

    function receiveData(q, data) {
        if (data) {
            $input.removeClass(options.loadingClass);
            results.innerHTML = "";

            // if the field no longer has focus or if there are no matches, do not display the drop down
            if (!hasFocus || data.length == 0) return hideResultsNow();

            if ($.browser.msie) {
                // we put a styled iframe behind the calendar so HTML SELECT elements don't show through
                //                $('<iframe hideFocus="true" frameborder="0" src="javascript:void(0);" style="z-Index:1;position:absolute;height:100%;top:0px;left:0px;width:100%;"' +
                //			  '></iframe>').css('opacity', 0).appendTo($results);
                //                $results.css("zIndex", 501);
                // $results.append(document.createElement('iframe'));
            }

            results.appendChild(dataToDom(data));
            // autofill in the complete box w/the first match as long as the user hasn't entered in more data
            //alert(data + "aa");
            var arrData = data[0][0].split(options.spaceFlag);   //对数据进行分割一下,只取第1个下标的值
            var value = "";
            if (arrData != null && arrData.length > 0)
                value = arrData[0];
            if (options.autoFill && ($input.val().toLowerCase() == q.toLowerCase()))
                autoFill(value);
            showResults();
        } else {
            hideResultsNow();
        }
    };

    function parseData(data) {
        if (!data) return null;
        var parsed = [];
        var rows = data.split(options.lineSeparator);
        for (var i = 0; i < rows.length; i++) {
            var row = $.trim(rows[i]);
            if (row) {
                parsed[parsed.length] = row.split(options.cellSeparator);
            }
        }
        return parsed;
    };

    function dataToDom(data) {
        var table = document.createElement("table");
        table.width = "100%";
        table.bgColor = "#ffffff";
        var num = data.length;
        var innerHTML = "";
        var selectValue = "";

        // limited results to a max number
        if ((options.maxItemsToShow > 0) && (options.maxItemsToShow < num)) num = options.maxItemsToShow;
        for (var i = 0; i < num; i++) {
            var row = data[i];
            if (!row) continue;
            var tr = table.insertRow(-1);    //插入行

            tr.style.cursor = "pointer";
            if (options.formatItem) {
                innerHTML = options.formatItem(row, i, num);
                selectValue = row[0];
            } else {
                innerHTML = row[0];
                selectValue = row[0];
            }
            //			var tmpStr = tr.innerHTML.replace("{~}{~}", "</td><td align='right'>&nbsp;&nbsp;&nbsp;&nbsp;");
            //			if(tr.innerHTML.indexOf("{~}{~}") == -1)
            //			    tmpStr = "</td><td align='right'>&nbsp;&nbsp;&nbsp;&nbsp;";
            //tr.innerHTML = "<tr><td>" + tmpStr + "</td></tr>";

            var arrHTML = innerHTML.split(options.spaceFlag);
            var arrValue = selectValue.split(options.spaceFlag);
            var td = null;

            for (var j = 0; j < options.spaceCount; j++) {
                if (arrHTML != null && arrHTML.length - 1 >= j) {
                    td = tr.insertCell(-1);   //插入列
                    td.valign = "top";
                    td.align = "left";
                    td.innerHTML = arrHTML[j];
                    td.selectValue = arrValue[j];
                }
            }
            var extra = null;
            if (row.length > 1) {
                extra = [];
                for (var j = 1; j < row.length; j++) {
                    extra[extra.length] = row[j];
                }
            }
            tr.extra = extra;
            //table.appendChild(tr);
            $(tr).hover(
				function() { $("tr", table).removeClass("ac_over"); $(this).addClass("ac_over"); active = $("tr", table).indexOf($(this).get(0)); },
				function() { $(this).removeClass("ac_over"); }
			).click(function(e) { e.preventDefault(); e.stopPropagation(); selectItem(this) });
        } //alert(table.innerHTML);
        return table;
    };

    function requestData(q) {
        if (!options.matchCase) q = q.toLowerCase();
        var data = options.cacheLength ? loadFromCache(q) : null;
        // recieve the cached data
        if (data) {
            receiveData(q, data);
            // if an AJAX url has been supplied, try loading the data now
        } else if ((typeof options.url == "string") && (options.url.length > 0)) {
            $.get(makeUrl(q), function(data) {
                data = parseData(data);
                addToCache(q, data);
                receiveData(q, data);
            });
            // if there's been no data found, remove the loading class
        } else {
            $input.removeClass(options.loadingClass);
        }
    };

    function makeUrl(q) {
        var url = options.url + "?SeachKey=" + encodeURI(q);
        for (var i in options.extraParams) {
            url += "&" + i + "=" + encodeURI(options.extraParams[i]);
        }

        //判断是否有要通过页面中的元素控件中取值(原因:在每次查询的时候,有可能每次的值是不相同的,所以要通过实时更新并取得页面元素中的值)
        for (var i in options.extraParamsByElementId) {
            var obj = document.getElementById(options.extraParamsByElementId[i]);
            if (obj != null)
                url += "&" + i + "=" + encodeURI(obj.value);
        }
        return url;

    };

    function loadFromCache(q) {
        if (!q) return null;
        if (cache.data[q]) return cache.data[q];
        if (options.matchSubset) {
            for (var i = q.length - 1; i >= options.minChars; i--) {
                var qs = q.substr(0, i);
                var c = cache.data[qs];
                if (c) {
                    var csub = [];
                    for (var j = 0; j < c.length; j++) {
                        var x = c[j];
                        var x0 = x[0];
                        if (matchSubset(x0, q)) {
                            csub[csub.length] = x;
                        }
                    }
                    return csub;
                }
            }
        }
        return null;
    };

    function matchSubset(s, sub) {
        if (!options.matchCase) s = s.toLowerCase();
        var i = s.indexOf(sub);
        if (i == -1) return false;
        return i == 0 || options.matchContains;
    };

    this.flushCache = function() {
        flushCache();
    };

    this.setExtraParams = function(p) {
        options.extraParams = p;
    };

    this.findValue = function() {
        var q = $input.val();

        if (!options.matchCase) q = q.toLowerCase();
        var data = options.cacheLength ? loadFromCache(q) : null;
        if (data) {
            findValueCallback(q, data);
        } else if ((typeof options.url == "string") && (options.url.length > 0)) {
            $.get(makeUrl(q), function(data) {
                data = parseData(data)
                addToCache(q, data);
                findValueCallback(q, data);
            });
        } else {
            // no matches
            findValueCallback(q, null);
        }
    }

    function findValueCallback(q, data) {
        if (data) $input.removeClass(options.loadingClass);

        var num = (data) ? data.length : 0;
        var tr = null;

        for (var i = 0; i < num; i++) {
            var row = data[i];

            if (row[0].toLowerCase() == q.toLowerCase()) {
                tr = document.createElement("tr");
                if (options.formatItem) {
                    tr.innerHTML = options.formatItem(row, i, num);
                    tr.selectValue = row[0];
                } else {
                    tr.innerHTML = row[0];
                    tr.selectValue = row[0];
                }
                var extra = null;
                if (row.length > 1) {
                    extra = [];
                    for (var j = 1; j < row.length; j++) {
                        extra[extra.length] = row[j];
                    }
                }
                tr.extra = extra;
            }
        }

        if (options.onFindValue) setTimeout(function() { options.onFindValue(tr) }, 1);
    }

    function addToCache(q, data) {
        if (!data || !q || !options.cacheLength) return;
        if (!cache.length || cache.length > options.cacheLength) {
            flushCache();
            cache.length++;
        } else if (!cache[q]) {
            cache.length++;
        }
        cache.data[q] = data;
    };

    function findPos(obj) {
        var curleft = obj.offsetLeft || 0;
        var curtop = obj.offsetTop || 0;
        while (obj = obj.offsetParent) {
            curleft += obj.offsetLeft
            curtop += obj.offsetTop
        }
        return { x: curleft, y: curtop };
    }
}

jQuery.fn.autocomplete = function(url, options, data) {
	// Make sure options exists
	options = options || {};
	// Set url as option
	options.url = url;
	// set some bulk local data
	options.data = ((typeof data == "object") && (data.constructor == Array)) ? data : null;

	// Set default values for required options
	options.inputClass = options.inputClass || "ac_input";
	options.resultsClass = options.resultsClass || "ac_results";
	options.lineSeparator = options.lineSeparator || "\n";
	options.cellSeparator = options.cellSeparator || "|";
	options.minChars = options.minChars || 1;
	options.delay = options.delay || 400;
	options.matchCase = options.matchCase || 1;
	options.matchSubset = options.matchSubset || 1;
	options.matchContains = options.matchContains || 1;
	options.cacheLength = options.cacheLength || 1;
	options.mustMatch = options.mustMatch || 0;
	options.extraParams = options.extraParams || {};
	options.extraParamsByElementId = options.extraParamsByElementId || {};
	options.loadingClass = options.loadingClass || "ac_loading";
	options.selectFirst = options.selectFirst || false;
	options.selectOnly = options.selectOnly || false;
	options.maxItemsToShow = options.maxItemsToShow || -1;
	options.autoFill = options.autoFill || false;
	options.width = parseInt(options.width, 10) || 0;
	options.spaceFlag = options.spaceFlag || "{~}{~}";   //定义分割符,默认为{~}{~}
	options.spaceCount = options.spaceCount || 1;      //定义分割的个数,默认为1
	options.addWidth = options.addWidth || 400;       //定义搜索结果显示的宽度,在文本框的基础上累加的像素值,默认为400 
	//options.openSelectedEvent = options.openSelectedEvent || false;   //选择之后是否要激发事件(默认事件名称为SelectedEvent),默认不激发	
	//options.openSelectedEventName = options.openSelectedEventName || "";  //选择之后要激发的事件名称
	options.IsFocusShow = options.IsFocusShow || false;  //判断是否点击文本框就显示查询结果,默认为false
	options.addSelectedFunParams = options.addSelectedFunParams || {};  //添加函数,该参数必须将options.openSelectedEvent＝true
	

	this.each(function() {
		var input = this;
		new jQuery.autocomplete(input, options);
	});
	
	//传参说明:extraParamsByElementId
	/*
	extraParamsByElementId: { 
                                TourId: "TourId",     变量名称:值  变量名称可以随便取
                                TourAreaId: "AreaId"
                        }  
    */
	// Don't break the chain
	return this;
}

jQuery.fn.autocompleteArray = function(data, options) {
	return this.autocomplete(null, options, data);
}

jQuery.fn.indexOf = function(e){
	for( var i=0; i<this.length; i++ ){
		if( this[i] == e ) return i;
	}
	return -1;
};
 
/*function FunEvent()
{}

FunEvent.prototype.addFun=function()
{
    var funName = "";
    var funArgs = "";
    if(arguments.length == 0)
	    return;
    funName = arguments[0];	
    if(arguments.length > 1)
    {
	    for(var i=1; i<=arguments.length-1; i++)
	    {
		    funArgs = funArgs + "," + arguments[i];
	    }
	    funArgs = funArgs.replace(",", "");
    }

    var strVal = "<script>" + funName + "(" + funArgs + ");" + "</scr" + "ipt>";
    document.write(strVal);
}*/
