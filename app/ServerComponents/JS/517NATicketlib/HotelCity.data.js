/*
* jQuery Autocomplete plugin 1.1
*
* Copyright (c) 2009 Jörn Zaefferer
*
* Dual licensed under the MIT and GPL licenses:
*   http://www.opensource.org/licenses/mit-license.php
*   http://www.gnu.org/licenses/gpl.html
*
* Revision: $Id: jquery.autocomplete.js 15 2009-08-22 10:30:27Z joern.zaefferer $
*/

; (function($) {

    $.fn.extend({
        autocomplete: function(urlOrData, options) {
            var isUrl = typeof urlOrData == "string";
            options = $.extend({}, $.Autocompleter.defaults, {
                url: isUrl ? urlOrData : null,
                data: isUrl ? null : urlOrData,
                delay: isUrl ? $.Autocompleter.defaults.delay : 10,
                max: options && !options.scroll ? 10 : 150
            }, options);

            // if highlight is set to false, replace it with a do-nothing function
            options.highlight = options.highlight || function(value) { return value; };

            // if the formatMatch option is not specified, then use formatItem for backwards compatibility
            options.formatMatch = options.formatMatch || options.formatItem;

            return this.each(function() {
                new $.Autocompleter(this, options);
            });
        },
        result: function(handler) {
            return this.bind("result", handler);
        },
        search: function(handler) {
            return this.trigger("search", [handler]);
        },
        flushCache: function() {
            return this.trigger("flushCache");
        },
        setOptions: function(options) {
            return this.trigger("setOptions", [options]);
        },
        unautocomplete: function() {
            return this.trigger("unautocomplete");
        }
    });

    $.Autocompleter = function(input, options) {

        var KEY = {
            UP: 38,
            DOWN: 40,
            DEL: 46,
            TAB: 9,
            RETURN: 13,
            ESC: 27,
            COMMA: 188,
            PAGEUP: 33,
            PAGEDOWN: 34,
            BACKSPACE: 8
        };

        // Create $ object for input element
        var $input = $(input).attr("autocomplete", "off").addClass(options.inputClass);

        var timeout;
        var previousValue = "";
        var cache = $.Autocompleter.Cache(options);
        var hasFocus = 0;
        var lastKeyPressCode;
        var config = {
            mouseDownOnSelect: false
        };
        var select = $.Autocompleter.Select(options, input, selectCurrent, config);

        var blockSubmit;

        // prevent form submit in opera when selecting with return key
        $.browser.opera && $(input.form).bind("submit.autocomplete", function() {
            if (blockSubmit) {
                blockSubmit = false;
                return false;
            }
        });

        // only opera doesn't trigger keydown multiple times while pressed, others don't work with keypress at all
        $input.bind(($.browser.opera ? "keypress" : "keydown") + ".autocomplete", function(event) {
            // a keypress means the input has focus
            // avoids issue where input had focus before the autocomplete was applied
            hasFocus = 1;
            // track last key pressed
            lastKeyPressCode = event.keyCode;
            switch (event.keyCode) {

                case KEY.UP:
                    event.preventDefault();
                    if (select.visible()) {
                        select.prev();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.DOWN:
                    event.preventDefault();
                    if (select.visible()) {
                        select.next();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.PAGEUP:
                    event.preventDefault();
                    if (select.visible()) {
                        select.pageUp();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.PAGEDOWN:
                    event.preventDefault();
                    if (select.visible()) {
                        select.pageDown();
                    } else {
                        onChange(0, true);
                    }
                    break;

                // matches also semicolon                            
                case options.multiple && $.trim(options.multipleSeparator) == "," && KEY.COMMA:
                case KEY.TAB:
                case KEY.RETURN:
                    if (selectCurrent()) {
                        // stop default to prevent a form submit, Opera needs special handling
                        event.preventDefault();
                        blockSubmit = true;
                        return false;
                    }
                    break;

                case KEY.ESC:
                    select.hide();
                    break;

                default:
                    clearTimeout(timeout);
                    timeout = setTimeout(onChange, options.delay);
                    break;
            }
        }).focus(function() {
            // track whether the field has focus, we shouldn't process any
            // results if the field no longer has focus
            hasFocus++;
        }).blur(function() {
            hasFocus = 0;
            if (!config.mouseDownOnSelect) {
                hideResults();
            }
        }).click(function() {
            // show select when clicking in a focused field
            if (hasFocus++ > 1 && !select.visible()) {
                onChange(0, true);
            }
        }).bind("search", function() {
            // TODO why not just specifying both arguments?
            var fn = (arguments.length > 1) ? arguments[1] : null;
            function findValueCallback(q, data) {
                var result;
                if (data && data.length) {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].result.toLowerCase() == q.toLowerCase()) {
                            result = data[i];
                            break;
                        }
                    }
                }
                if (typeof fn == "function") fn(result);
                else $input.trigger("result", result && [result.data, result.value]);
            }
            $.each(trimWords($input.val()), function(i, value) {
                request(value, findValueCallback, findValueCallback);
            });
        }).bind("flushCache", function() {
            cache.flush();
        }).bind("setOptions", function() {
            $.extend(options, arguments[1]);
            // if we've updated the data, repopulate
            if ("data" in arguments[1])
                cache.populate();
        }).bind("unautocomplete", function() {
            select.unbind();
            $input.unbind();
            $(input.form).unbind(".autocomplete");
        });


        function selectCurrent() {
            var selected = select.selected();
            if (!selected)
                return false;

            var v = selected.result;
            previousValue = v;
            if (options.multiple) {
                var words = trimWords($input.val());
                if (words.length > 1) {
                    var seperator = options.multipleSeparator.length;
                    var cursorAt = $(input).selection().start;
                    var wordAt, progress = 0;
                    $.each(words, function(i, word) {
                        progress += word.length;
                        if (cursorAt <= progress) {
                            wordAt = i;
                            return false;
                        }
                        progress += seperator;
                    });
                    words[wordAt] = v;
                    // TODO this should set the cursor to the right position, but it gets overriden somewhere
                    //$.Autocompleter.Selection(input, progress + seperator, progress + seperator);
                    v = words.join(options.multipleSeparator);
                }
                v += options.multipleSeparator;
            }
            var a = this;
            $input.val(v);
            var hidInputId = selected.hideResult.hidInputId;
            var hidInputValue = selected.hideResult.value;
            if (hidInputId != 'undefined' && hidInputId != '') {
                $("#" + hidInputId).val(hidInputValue);
            } else {
                $input.next().val(hidInputValue);
            }
            hideResultsNow();
            $input.trigger("result", [selected.data, selected.value]);
            return true;
        }

        function onChange(crap, skipPrevCheck) {
            if (lastKeyPressCode == KEY.DEL) {
                select.hide();
                return;
            }

            var currentValue = $input.val();

            if (!skipPrevCheck && currentValue == previousValue)
                return;

            previousValue = currentValue;

            currentValue = lastWord(currentValue);
            if (currentValue.length >= options.minChars) {
                $input.addClass(options.loadingClass);
                if (!options.matchCase)
                    currentValue = currentValue.toLowerCase();
                request(currentValue, receiveData, hideResultsNow);
            } else {
                stopLoading();
                select.hide();
            }
        };

        function trimWords(value) {
            if (!value)
                return [""];
            if (!options.multiple)
                return [$.trim(value)];
            return $.map(value.split(options.multipleSeparator), function(word) {
                return $.trim(value).length ? $.trim(word) : null;
            });
        }

        function lastWord(value) {
            if (!options.multiple)
                return value;
            var words = trimWords(value);
            if (words.length == 1)
                return words[0];
            var cursorAt = $(input).selection().start;
            if (cursorAt == value.length) {
                words = trimWords(value)
            } else {
                words = trimWords(value.replace(value.substring(cursorAt), ""));
            }
            return words[words.length - 1];
        }

        // fills in the input box w/the first match (assumed to be the best match)
        // q: the term entered
        // sValue: the first matching result
        function autoFill(q, sValue) {
            ticketLKE.cacheData.push(sValue);
            // autofill in the complete box w/the first match as long as the user hasn't entered in more data
            // if the last user key pressed was backspace, don't autofill
            if (options.autoFill && (lastWord($input.val()).toLowerCase() == q.toLowerCase()) && lastKeyPressCode != KEY.BACKSPACE) {
                // fill in the value (keep the case the user has typed)
                //$input.val($input.val() + sValue.substring(lastWord(previousValue).length));
                // select the portion of the value not typed by the user (so the next character will erase)
                //$(input).selection(previousValue.length, previousValue.length + sValue.length);
                var arrVal1 = sValue.split("|");
                var arrVal2 = arrVal1[0].split(",");
                var retVal = false;
                for (var i = 0; i < arrVal2.length; i++) {
                    if (arrVal2[i].indexOf(q) == 0) {
                        retVal = true;
                        break;
                    }
                }
                if (retVal) {
                    $(input).next().val(arrVal1[1]);
                }
            }
        };

        function hideResults() {
            clearTimeout(timeout);
            timeout = setTimeout(hideResultsNow, 200);
        };

        function hideResultsNow() {
            var wasVisible = select.visible();
            select.hide();
            clearTimeout(timeout);
            stopLoading();
            if (options.mustMatch) {
                // call search and run callback
                $input.search(
				function(result) {
				    // if no value found, clear the input box
				    if (!result) {
				        if (options.multiple) {
				            var words = trimWords($input.val()).slice(0, -1);
				            $input.val(words.join(options.multipleSeparator) + (words.length ? options.multipleSeparator : ""));
				        }
				        else {
				            $input.val("");
				            $input.trigger("result", null);
				        }
				    }
				}
			);
            }
        };

        function receiveData(q, data) {
            if (data && data.length && hasFocus) {
                ticketLKE.cacheData = new Array();
                stopLoading();
                select.display(data, q);
                autoFill(q, data[0].value);
                select.show();
            } else {
                hideResultsNow();
            }
        };

        function request(term, success, failure) {
            if (!options.matchCase)
                term = term.toLowerCase();
            var data = cache.load(term);
            // recieve the cached data
            if (data && data.length) {
                success(term, data);
                // if an AJAX url has been supplied, try loading the data now
            } else if ((typeof options.url == "string") && (options.url.length > 0)) {

                var extraParams = {
                    timestamp: +new Date()
                };
                $.each(options.extraParams, function(key, param) {
                    extraParams[key] = typeof param == "function" ? param() : param;
                });

                $.ajax({
                    // try to leverage ajaxQueue plugin to abort previous requests
                    mode: "abort",
                    // limit abortion to this input
                    port: "autocomplete" + input.name,
                    dataType: options.dataType,
                    url: options.url,
                    data: $.extend({
                        q: lastWord(term),
                        limit: options.max
                    }, extraParams),
                    success: function(data) {
                        var parsed = options.parse && options.parse(data) || parse(data);
                        cache.add(term, parsed);
                        success(term, parsed);
                    }
                });
            } else {
                // if we have a failure, we need to empty the list -- this prevents the the [TAB] key from selecting the last successful match
                select.emptyList();
                failure(term);
            }
        };

        function parse(data) {
            var parsed = [];
            var rows = data.split("\n");
            for (var i = 0; i < rows.length; i++) {
                var row = $.trim(rows[i]);
                if (row) {
                    row = row.split("|");
                    parsed[parsed.length] = {
                        data: row,
                        value: row[0],
                        result: options.formatResult && options.formatResult(row, row[0]) || row[0],
                        hideResult: option.formatHidResult && options.formatHidResult(row, row[0]) || row[0]
                    };
                }
            }
            return parsed;
        };

        function stopLoading() {
            $input.removeClass(options.loadingClass);
        };

    };

    $.Autocompleter.defaults = {
        inputClass: "ac_input",
        resultsClass: "ac_results",
        loadingClass: "ac_loading",
        minChars: 1,
        delay: 400,
        matchCase: false,
        matchSubset: true,
        matchContains: false,
        cacheLength: 10,
        max: 100,
        mustMatch: false,
        extraParams: {},
        selectFirst: true,
        formatItem: function(row) { return row[0]; },
        formatMatch: null,
        autoFill: false,
        width: 0,
        multiple: false,
        multipleSeparator: ", ",
        highlight: function(value, term) {
            return value.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1") + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>");
        },
        scroll: true,
        scrollHeight: 180
    };

    $.Autocompleter.Cache = function(options) {

        var data = {};
        var length = 0;

        function matchSubset(s, sub) {
            if (!options.matchCase)
                s = s.toLowerCase();
            //            var i = s.indexOf(sub);
            //            if (options.matchContains == "word") {
            //                i = s.toLowerCase().search("\\b" + sub.toLowerCase());
            //            }
            //            if (i == -1) return false;
            //            return i == 0 || options.matchContains;
            var arrVal = s.split("|")[0].split(",");
            var retVal = false;
            for (var i = 0; i < arrVal.length; i++) {
                if (arrVal[i].indexOf(sub) == 0) {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        };

        function add(q, value) {
            if (length > options.cacheLength) {
                flush();
            }
            if (!data[q]) {
                length++;
            }
            data[q] = value;
        }

        function populate() {
            if (!options.data) return false;
            // track the matches
            var stMatchSets = {},
			nullData = 0;

            // no url was specified, we need to adjust the cache length to make sure it fits the local data store
            if (!options.url) options.cacheLength = 1;

            // track all options for minChars = 0
            stMatchSets[""] = [];

            // loop through the array and create a lookup structure
            for (var i = 0, ol = options.data.length; i < ol; i++) {
                var rawValue = options.data[i];
                // if rawValue is a string, make an array otherwise just reference the array
                rawValue = (typeof rawValue == "string") ? [rawValue] : rawValue;

                var value = options.formatMatch(rawValue, i + 1, options.data.length);
                if (value === false)
                    continue;

                var firstChar = value.charAt(0).toLowerCase();
                // if no lookup array for this character exists, look it up now
                if (!stMatchSets[firstChar])
                    stMatchSets[firstChar] = [];

                // if the match is a string
                var row = {
                    value: value,
                    data: rawValue,
                    result: options.formatResult && options.formatResult(rawValue) || value,
                    hideResult: options.formatHidResult && options.formatHidResult(rawValue) || value
                };

                // push the current match into the set list
                stMatchSets[firstChar].push(row);

                // keep track of minChars zero items
                if (nullData++ < options.max) {
                    stMatchSets[""].push(row);
                }
            };

            // add the data items to the cache
            $.each(stMatchSets, function(i, value) {
                // increase the cache size
                options.cacheLength++;
                // add to the cache
                add(i, value);
            });
        }

        // populate any existing data
        setTimeout(populate, 25);

        function flush() {
            data = {};
            length = 0;
        }

        return {
            flush: flush,
            add: add,
            populate: populate,
            load: function(q) {
                if (!options.cacheLength || !length)
                    return null;
                /* 
                * if dealing w/local data and matchContains than we must make sure
                * to loop through all the data collections looking for matches
                */
                if (!options.url && options.matchContains) {
                    // track all matches
                    var csub = [];
                    // loop through all the data grids for matches
                    for (var k in data) {
                        // don't search through the stMatchSets[""] (minChars: 0) cache
                        // this prevents duplicates
                        if (k.length > 0) {
                            var c = data[k];
                            $.each(c, function(i, x) {
                                // if we've got a match, add it to the array
                                if (matchSubset(x.value, q)) {
                                    csub.push(x);
                                }
                            });
                        }
                    }
                    return csub;
                } else
                // if the exact item exists, use it
                    if (data[q]) {
                    return data[q];
                } else
                    if (options.matchSubset) {
                    for (var i = q.length - 1; i >= options.minChars; i--) {
                        var c = data[q.substr(0, i)];
                        if (c) {
                            var csub = [];
                            $.each(c, function(i, x) {
                                if (matchSubset(x.value, q)) {
                                    csub[csub.length] = x;
                                }
                            });
                            return csub;
                        }
                    }
                }
                return null;
            }
        };
    };

    $.Autocompleter.Select = function(options, input, select, config) {
        var CLASSES = {
            ACTIVE: "ac_over"
        };

        var listItems,
		active = -1,
		data,
		term = "",
		needsInit = true,
		element,
		list;

        // Create results
        function init() {
            if (!needsInit)
                return;
            element = $("<div/>")
		.hide()
		.addClass(options.resultsClass)
		.css("position", "absolute")
		.appendTo(document.body);

            list = $("<ul/>").appendTo(element).mouseover(function(event) {
                if (target(event).nodeName && target(event).nodeName.toUpperCase() == 'LI') {
                    active = $("li", list).removeClass(CLASSES.ACTIVE).index(target(event));
                    $(target(event)).addClass(CLASSES.ACTIVE);
                }
            }).click(function(event) {
                $(target(event)).addClass(CLASSES.ACTIVE);
                select();
                // TODO provide option to avoid setting focus again after selection? useful for cleanup-on-focus
                input.focus();
                return false;
            }).mousedown(function() {
                config.mouseDownOnSelect = true;
            }).mouseup(function() {
                config.mouseDownOnSelect = false;
            });

            if (options.width > 0)
                element.css("width", options.width);

            needsInit = false;
        }

        function target(event) {
            var element = event.target;
            while (element && element.tagName != "LI")
                element = element.parentNode;
            // more fun with IE, sometimes event.target is empty, just ignore it then
            if (!element)
                return [];
            return element;
        }

        function moveSelect(step) {
            listItems.slice(active, active + 1).removeClass(CLASSES.ACTIVE);
            movePosition(step);
            var activeItem = listItems.slice(active, active + 1).addClass(CLASSES.ACTIVE);
            if (options.scroll) {
                var offset = 0;
                listItems.slice(0, active).each(function() {
                    offset += this.offsetHeight;
                });
                if ((offset + activeItem[0].offsetHeight - list.scrollTop()) > list[0].clientHeight) {
                    list.scrollTop(offset + activeItem[0].offsetHeight - list.innerHeight());
                } else if (offset < list.scrollTop()) {
                    list.scrollTop(offset);
                }
            }
        };

        function movePosition(step) {
            active += step;
            if (active < 0) {
                active = listItems.size() - 1;
            } else if (active >= listItems.size()) {
                active = 0;
            }
        }

        function limitNumberOfItems(available) {
            return options.max && options.max < available
			? options.max
			: available;
        }

        function fillList() {
            list.empty();
            var max = limitNumberOfItems(data.length);
            for (var i = 0; i < max; i++) {
                if (!data[i])
                    continue;
                var formatted = options.formatItem(data[i].data, i + 1, max, data[i].value, term);
                if (formatted === false)
                    continue;
                var li = $("<li/>").html(options.highlight(formatted, term)).addClass(i % 2 == 0 ? "ac_even" : "ac_odd").appendTo(list)[0];
                $.data(li, "ac_data", data[i]);
            }
            listItems = list.find("li");
            if (options.selectFirst) {
                listItems.slice(0, 1).addClass(CLASSES.ACTIVE);
                active = 0;
            }
            // apply bgiframe if available
            if ($.fn.bgiframe)
                list.bgiframe();
        }

        return {
            display: function(d, q) {
                init();
                data = d;
                term = q;
                fillList();
            },
            next: function() {
                moveSelect(1);
            },
            prev: function() {
                moveSelect(-1);
            },
            pageUp: function() {
                if (active != 0 && active - 8 < 0) {
                    moveSelect(-active);
                } else {
                    moveSelect(-8);
                }
            },
            pageDown: function() {
                if (active != listItems.size() - 1 && active + 8 > listItems.size()) {
                    moveSelect(listItems.size() - 1 - active);
                } else {
                    moveSelect(8);
                }
            },
            hide: function() {
                element && element.hide();
                listItems && listItems.removeClass(CLASSES.ACTIVE);
                active = -1;
            },
            visible: function() {
                return element && element.is(":visible");
            },
            current: function() {
                return this.visible() && (listItems.filter("." + CLASSES.ACTIVE)[0] || options.selectFirst && listItems[0]);
            },
            show: function() {
                var offset = $(input).offset();
                element.css({
                    width: typeof options.width == "string" || options.width > 0 ? options.width : $(input).width(),
                    top: offset.top + input.offsetHeight,
                    left: offset.left
                }).show();
                if (options.scroll) {
                    list.scrollTop(0);
                    list.css({
                        maxHeight: options.scrollHeight,
                        overflow: 'auto'
                    });

                    if ($.browser.msie && typeof document.body.style.maxHeight === "undefined") {
                        var listHeight = 0;
                        listItems.each(function() {
                            listHeight += this.offsetHeight;
                        });
                        var scrollbarsVisible = listHeight > options.scrollHeight;
                        list.css('height', scrollbarsVisible ? options.scrollHeight : listHeight);
                        if (!scrollbarsVisible) {
                            // IE doesn't recalculate width when scrollbar disappears
                            listItems.width(list.width() - parseInt(listItems.css("padding-left")) - parseInt(listItems.css("padding-right")));
                        }
                    }

                }
            },
            selected: function() {
                var selected = listItems && listItems.filter("." + CLASSES.ACTIVE).removeClass(CLASSES.ACTIVE);
                return selected && selected.length && $.data(selected[0], "ac_data");
            },
            emptyList: function() {
                list && list.empty();
            },
            unbind: function() {
                element && element.remove();
            }
        };
    };

    $.fn.selection = function(start, end) {
        if (start !== undefined) {
            return this.each(function() {
                if (this.createTextRange) {
                    var selRange = this.createTextRange();
                    if (end === undefined || start == end) {
                        selRange.move("character", start);
                        selRange.select();
                    } else {
                        selRange.collapse(true);
                        selRange.moveStart("character", start);
                        selRange.moveEnd("character", end);
                        selRange.select();
                    }
                } else if (this.setSelectionRange) {
                    this.setSelectionRange(start, end);
                } else if (this.selectionStart) {
                    this.selectionStart = start;
                    this.selectionEnd = end;
                }
            });
        }
        var field = this[0];
        if (field.createTextRange) {
            var range = document.selection.createRange(),
			orig = field.value,
			teststring = "<->",
			textLength = range.text.length;
            range.text = teststring;
            var caretAt = field.value.indexOf(teststring);
            field.value = orig;
            this.selection(caretAt, caretAt + textLength);
            return {
                start: caretAt,
                end: caretAt + textLength
            }
        } else if (field.selectionStart !== undefined) {
            return {
                start: field.selectionStart,
                end: field.selectionEnd
            }
        }
    };

})(jQuery);


var ticketLKEdata=[{"lke":"AAT","cName":"阿勒泰","eName":"ALETAI","shortName":"ALT","isHot":false},{"lke":"ACX","cName":"兴义","eName":"XINGYI","shortName":"XY","isHot":false},{"lke":"AEB","cName":"百色田阳","eName":"BAISETIANYANG","shortName":"BSTY","isHot":false},{"lke":"AKA","cName":"安康","eName":"ANKANG","shortName":"AK","isHot":false},{"lke":"AKU","cName":"阿克苏","eName":"AKESU","shortName":"AKS","isHot":false},{"lke":"ALA","cName":"阿尔玛塔","eName":"AERMATA","shortName":"AEMT","isHot":false},{"lke":"AOG","cName":"鞍山","eName":"ANSHAN","shortName":"AS","isHot":false},{"lke":"AQG","cName":"安庆","eName":"ANQING","shortName":"AQ","isHot":false},{"lke":"AVA","cName":"安顺","eName":"ANSHUN","shortName":"AS","isHot":false},{"lke":"AYN","cName":"安阳","eName":"ANYANG","shortName":"AY","isHot":false},{"lke":"BAV","cName":"包头","eName":"BAOTOU","shortName":"BT","isHot":false},{"lke":"BFU","cName":"蚌埠","eName":"BANGBU","shortName":"BB","isHot":false},{"lke":"BHY","cName":"北海","eName":"BEIHAI","shortName":"BH","isHot":false},{"lke":"BPX","cName":"昌都","eName":"CHANGDOU","shortName":"CD","isHot":false},{"lke":"BSD","cName":"保山","eName":"BAOSHAN","shortName":"BS","isHot":false},{"lke":"CAN","cName":"广州","eName":"GUANGZHOU","shortName":"GZ","isHot":true},{"lke":"CCC","cName":"潮州","eName":"CHAOZHOU","shortName":"CZ","isHot":false},{"lke":"CGD","cName":"常德","eName":"CHANGDE","shortName":"CD","isHot":false},{"lke":"CGO","cName":"郑州","eName":"ZHENGZHOU","shortName":"ZZ","isHot":true},{"lke":"CGQ","cName":"长春","eName":"CHANGCHUN","shortName":"CC","isHot":false},{"lke":"CHG","cName":"朝阳","eName":"CHAOYANG","shortName":"CY","isHot":false},{"lke":"CHW","cName":"酒泉","eName":"JIUQUAN","shortName":"JQ","isHot":false},{"lke":"CIF","cName":"赤峰","eName":"CHIFENG","shortName":"CF","isHot":false},{"lke":"CIH","cName":"长治","eName":"CHANGZHI","shortName":"CZ","isHot":false},{"lke":"CJU","cName":"济州","eName":"JIZHOU","shortName":"JZ","isHot":false},{"lke":"CKG","cName":"重庆","eName":"CHONGQING","shortName":"CQ","isHot":true},{"lke":"CMJ","cName":"澎湖县","eName":"PENGHUXIAN","shortName":"PH","isHot":false},{"lke":"CNI","cName":"长海","eName":"CHANGHAI","shortName":"CH","isHot":false},{"lke":"CSX","cName":"长沙","eName":"CHANGSHA","shortName":"CS","isHot":true},{"lke":"CTU","cName":"成都","eName":"CHENGDOU","shortName":"CD","isHot":true},{"lke":"CYI","cName":"嘉义","eName":"JIAYI","shortName":"JY","isHot":false},{"lke":"CZX","cName":"常州","eName":"CHANGZHOU","shortName":"CZ","isHot":false},{"lke":"DAT","cName":"大同","eName":"DATONG","shortName":"DT","isHot":false},{"lke":"DAX","cName":"达州","eName":"DAZHOU","shortName":"DZ","isHot":false},{"lke":"DDG","cName":"丹东","eName":"DANDONG","shortName":"DD","isHot":false},{"lke":"DIG","cName":"迪庆","eName":"DIQING","shortName":"DQ","isHot":false},{"lke":"DLC","cName":"大连","eName":"DALIAN","shortName":"DL","isHot":true},{"lke":"DLU","cName":"大理","eName":"DALI","shortName":"DL","isHot":false},{"lke":"DNH","cName":"敦煌","eName":"DUNHUANG","shortName":"DH","isHot":false},{"lke":"DOY","cName":"东营","eName":"DONGYING","shortName":"DY","isHot":false},{"lke":"DQA","cName":"大庆","eName":"DAQING","shortName":"DQ","isHot":false},{"lke":"DSN","cName":"鄂尔多斯","eName":"EERDUOSI","shortName":"EEDS","isHot":false},{"lke":"DYG","cName":"张家界","eName":"ZHANGJIAJIE","shortName":"ZJJ","isHot":false},{"lke":"DZU","cName":"大足","eName":"DAZU","shortName":"DZ","isHot":false},{"lke":"ENH","cName":"恩施","eName":"ENSHI","shortName":"ES","isHot":false},{"lke":"ENY","cName":"延安","eName":"YANAN","shortName":"YA","isHot":false},{"lke":"ERL","cName":"内蒙古二连浩特","eName":"NEIMENGGUERLIANHAOTE","shortName":"NMGELHT","isHot":false},{"lke":"FOC","cName":"福州","eName":"FUZHOU","shortName":"FZ","isHot":false},{"lke":"FUG","cName":"阜阳","eName":"FUYANG","shortName":"FY","isHot":false},{"lke":"FUO","cName":"佛山","eName":"FUSHAN","shortName":"FS","isHot":false},{"lke":"FYN","cName":"富蕴","eName":"FUYUN","shortName":"FY","isHot":false},{"lke":"GHN","cName":"广汉","eName":"GUANGHAN","shortName":"GH","isHot":false},{"lke":"GNI","cName":"台东县","eName":"TAIDONGXIAN","shortName":"TD","isHot":false},{"lke":"GOQ","cName":"格尔木","eName":"GEERMU","shortName":"GEM","isHot":false},{"lke":"GYS","cName":"广元","eName":"GUANGYUAN","shortName":"GY","isHot":false},{"lke":"HAK","cName":"海口","eName":"HAIKOU","shortName":"HK","isHot":true},{"lke":"HCN","cName":"屏东县","eName":"PINGDONGXIAN","shortName":"PD","isHot":false},{"lke":"HDG","cName":"邯郸","eName":"HANDAN","shortName":"HD","isHot":false},{"lke":"HEK","cName":"黑河","eName":"HEIHE","shortName":"HH","isHot":false},{"lke":"HET","cName":"呼和浩特","eName":"HUHEHAOTE","shortName":"HHHT","isHot":false},{"lke":"HFE","cName":"合肥","eName":"HEFEI","shortName":"HF","isHot":true},{"lke":"HGH","cName":"杭州","eName":"HANGZHOU","shortName":"HZ","isHot":true},{"lke":"HIN","cName":"清州","eName":"QINGZHOU","shortName":"QZ","isHot":false},{"lke":"HJJ","cName":"怀化","eName":"HUAIHUA","shortName":"HH","isHot":false},{"lke":"HKG","cName":"香港","eName":"XIANGGANG","shortName":"XG","isHot":false},{"lke":"HLD","cName":"海拉尔","eName":"HAILAER","shortName":"HLE","isHot":false},{"lke":"HLH","cName":"乌兰浩特","eName":"WULANHAOTE","shortName":"WLHT","isHot":false},{"lke":"HMI","cName":"哈密","eName":"HAMI","shortName":"HM","isHot":false},{"lke":"HNY","cName":"衡阳","eName":"HENGYANG","shortName":"HY","isHot":false},{"lke":"HRB","cName":"哈尔滨","eName":"HAERBIN","shortName":"HEB","isHot":true},{"lke":"HSN","cName":"舟山","eName":"ZHOUSHAN","shortName":"ZS","isHot":false},{"lke":"HTN","cName":"和田","eName":"HETIAN","shortName":"HT","isHot":false},{"lke":"HUN","cName":"花莲","eName":"HUALIAN","shortName":"HL","isHot":false},{"lke":"HUZ","cName":"徽州","eName":"HUIZHOU","shortName":"HZ","isHot":false},{"lke":"HYN","cName":"台州(黄岩)","eName":"TAIZHOU(HUANGYAN)","shortName":"TZ(HY)","isHot":false},{"lke":"HZG","cName":"汉中","eName":"HANZHONG","shortName":"HZ","isHot":false},{"lke":"HZH","cName":"黎平","eName":"LIPING","shortName":"LP","isHot":false},{"lke":"INC","cName":"银川","eName":"YINCHUAN","shortName":"YC","isHot":false},{"lke":"IQM","cName":"且末","eName":"QIEMO","shortName":"QM","isHot":false},{"lke":"IQN","cName":"庆阳","eName":"QINGYANG","shortName":"QY","isHot":false},{"lke":"JDZ","cName":"景德镇","eName":"JINGDEZHEN","shortName":"JDZ","isHot":false},{"lke":"JGN","cName":"嘉峪关","eName":"JIAYUGUAN","shortName":"JYG","isHot":false},{"lke":"JGS","cName":"井冈山","eName":"JINGGANGSHAN","shortName":"JGS","isHot":false},{"lke":"JHG","cName":"景洪","eName":"JINGHONG","shortName":"JH","isHot":false},{"lke":"JIL","cName":"吉林","eName":"JILIN","shortName":"JL","isHot":false},{"lke":"JIU","cName":"九江","eName":"JIUJIANG","shortName":"JJ","isHot":false},{"lke":"JJN","cName":"晋江","eName":"JINJIANG","shortName":"JJ","isHot":false},{"lke":"JMU","cName":"佳木斯","eName":"JIAMUSI","shortName":"JMS","isHot":false},{"lke":"JNG","cName":"济宁","eName":"JINING","shortName":"JN","isHot":false},{"lke":"JNZ","cName":"锦州","eName":"JINZHOU","shortName":"JZ","isHot":false},{"lke":"JUZ","cName":"衢州","eName":"QUZHOU","shortName":"QZ","isHot":false},{"lke":"JXA","cName":"鸡西","eName":"JIXI","shortName":"JX","isHot":false},{"lke":"JZH","cName":"九寨沟","eName":"JIUZHAIGOU","shortName":"JZG","isHot":false},{"lke":"KCA","cName":"库车","eName":"KUCHE","shortName":"KC","isHot":false},{"lke":"KGT","cName":"康定","eName":"KANGDING","shortName":"KD","isHot":false},{"lke":"KHG","cName":"喀什","eName":"KASHEN","shortName":"KS","isHot":false},{"lke":"KHH","cName":"高雄","eName":"GAOXIONG","shortName":"GX","isHot":false},{"lke":"KHN","cName":"南昌","eName":"NANCHANG","shortName":"NC","isHot":true},{"lke":"KJI","cName":"新疆","eName":"XINJIANG","shortName":"XJ","isHot":false},{"lke":"KMG","cName":"昆明","eName":"KUNMING","shortName":"KM","isHot":true},{"lke":"KNC","cName":"吉安","eName":"JIAN","shortName":"JA","isHot":false},{"lke":"KNH","cName":"金门县","eName":"JINMENXIAN","shortName":"JM","isHot":false},{"lke":"KOW","cName":"赣州","eName":"GANZHOU","shortName":"GZ","isHot":false},{"lke":"KRL","cName":"库尔勒","eName":"KUERLE","shortName":"KEL","isHot":false},{"lke":"KRY","cName":"克拉玛依","eName":"KELAMAYI","shortName":"KLMY","isHot":false},{"lke":"KWE","cName":"贵阳","eName":"GUIYANG","shortName":"GY","isHot":true},{"lke":"KWL","cName":"桂林","eName":"GUILIN","shortName":"GL","isHot":false},{"lke":"KYD","cName":"兰屿","eName":"LANYU","shortName":"LY","isHot":false},{"lke":"LCX","cName":"连城","eName":"LIANCHENG","shortName":"LC","isHot":false},{"lke":"LDS","cName":"伊春","eName":"YICHUN","shortName":"YC","isHot":false},{"lke":"LHN","cName":"梨山","eName":"LISHAN","shortName":"LS","isHot":false},{"lke":"LHW","cName":"兰州","eName":"LANZHOU","shortName":"LZ","isHot":false},{"lke":"LIA","cName":"梁平","eName":"LIANGPING","shortName":"LP","isHot":false},{"lke":"LJG","cName":"丽江","eName":"LIJIANG","shortName":"LJ","isHot":false},{"lke":"LLB","cName":"荔波","eName":"LIBO","shortName":"LB","isHot":false},{"lke":"LLF","cName":"永州","eName":"YONGZHOU","shortName":"YZ","isHot":false},{"lke":"LNJ","cName":"临沧","eName":"LINCANG","shortName":"LC","isHot":false},{"lke":"LUM","cName":"芒市","eName":"MANGSHI","shortName":"MS","isHot":false},{"lke":"LUZ","cName":"庐山","eName":"LUSHAN","shortName":"LS","isHot":false},{"lke":"LXA","cName":"拉萨","eName":"LASA","shortName":"LS","isHot":false},{"lke":"LXI","cName":"林西","eName":"LINXI","shortName":"LX","isHot":false},{"lke":"LYA","cName":"洛阳","eName":"LUOYANG","shortName":"LY","isHot":false},{"lke":"LYG","cName":"连云港","eName":"LIANYUNGANG","shortName":"LYG","isHot":false},{"lke":"LYI","cName":"临沂","eName":"LINYI","shortName":"LY","isHot":false},{"lke":"LZH","cName":"柳州","eName":"LIUZHOU","shortName":"LZ","isHot":false},{"lke":"LZN","cName":"马祖","eName":"MAZU","shortName":"MZ","isHot":false},{"lke":"LZO","cName":"泸州","eName":"LUZHOU","shortName":"LZ","isHot":false},{"lke":"LZY","cName":"林芝","eName":"LINZHI","shortName":"LZ","isHot":false},{"lke":"MDG","cName":"牡丹江","eName":"MUDANJIANG","shortName":"MDJ","isHot":false},{"lke":"MFK","cName":"马祖","eName":"MAZU","shortName":"MZ","isHot":false},{"lke":"MFM","cName":"澳门","eName":"AOMEN","shortName":"AM","isHot":false},{"lke":"MIG","cName":"绵阳","eName":"MIANYANG","shortName":"MY","isHot":false},{"lke":"MXZ","cName":"梅州","eName":"MEIZHOU","shortName":"MZ","isHot":false},{"lke":"MZG","cName":"马公","eName":"MAGONG","shortName":"MG","isHot":false},{"lke":"NAO","cName":"南充","eName":"NANCHONG","shortName":"NC","isHot":false},{"lke":"NAY","cName":"北京","eName":"BEIJING","shortName":"BJ","isHot":false},{"lke":"NBS","cName":"长白山","eName":"CHANGBAISHAN","shortName":"CBS","isHot":false},{"lke":"NDG","cName":"齐齐哈尔","eName":"QIQIHAER","shortName":"QQHE","isHot":false},{"lke":"NGB","cName":"宁波","eName":"NINGBO","shortName":"NB","isHot":false},{"lke":"NKG","cName":"南京","eName":"NANJING","shortName":"NJ","isHot":true},{"lke":"NNG","cName":"南宁","eName":"NANNING","shortName":"NN","isHot":false},{"lke":"NNY","cName":"南阳","eName":"NANYANG","shortName":"NY","isHot":false},{"lke":"NTG","cName":"南通","eName":"NANTONG","shortName":"NT","isHot":false},{"lke":"NZH","cName":"满洲里","eName":"MANZHOULI","shortName":"MZL","isHot":false},{"lke":"OHE","cName":"漠河","eName":"MOHE","shortName":"MH","isHot":false},{"lke":"PEK","cName":"北京","eName":"BEIJING","shortName":"BJ","isHot":true},{"lke":"PIF","cName":"屏东市","eName":"PINGDONGSHI","shortName":"PD","isHot":false},{"lke":"PZI","cName":"攀枝花","eName":"PANZHIHUA","shortName":"PZH","isHot":false},{"lke":"RMQ","cName":"台中","eName":"TAIZHONG","shortName":"TZ","isHot":false},{"lke":"SHA","cName":"上海","eName":"SHANGHAI","shortName":"SH","isHot":false},{"lke":"SHE","cName":"沈阳","eName":"SHENYANG","shortName":"SY","isHot":true},{"lke":"SHP","cName":"秦皇岛","eName":"QINHUANGDAO","shortName":"QHD","isHot":false},{"lke":"SHS","cName":"荆州","eName":"JINGZHOU","shortName":"JZ","isHot":false},{"lke":"SIA","cName":"西安","eName":"XIAN","shortName":"XA","isHot":true},{"lke":"SJW","cName":"石家庄","eName":"SHIJIAZHUANG","shortName":"SJZ","isHot":false},{"lke":"SWA","cName":"汕头","eName":"SHANTOU","shortName":"ST","isHot":false},{"lke":"SYM","cName":"思茅","eName":"SIMAO","shortName":"SM","isHot":false},{"lke":"SYX","cName":"三亚","eName":"SANYA","shortName":"SY","isHot":false},{"lke":"SZV","cName":"苏州","eName":"SUZHOU","shortName":"SZ","isHot":false},{"lke":"SZX","cName":"深圳","eName":"SHENZHEN","shortName":"SZ","isHot":true},{"lke":"TAO","cName":"青岛","eName":"QINGDAO","shortName":"QD","isHot":true},{"lke":"TCG","cName":"塔城","eName":"TACHENG","shortName":"TC","isHot":false},{"lke":"TCZ","cName":"腾冲","eName":"TENGCHONG","shortName":"TC","isHot":false},{"lke":"TEN","cName":"铜仁","eName":"TONGREN","shortName":"TR","isHot":false},{"lke":"TGO","cName":"通辽","eName":"TONGLIAO","shortName":"TL","isHot":false},{"lke":"THQ","cName":"天水","eName":"TIANSHUI","shortName":"TS","isHot":false},{"lke":"TNA","cName":"济南","eName":"JINAN","shortName":"JN","isHot":false},{"lke":"TNH","cName":"通化","eName":"TONGHUA","shortName":"TH","isHot":false},{"lke":"TNN","cName":"台南","eName":"TAINAN","shortName":"TN","isHot":false},{"lke":"TPE","cName":"桃园","eName":"TAOYUAN","shortName":"TB","isHot":false},{"lke":"TSA","cName":"台北","eName":"TAIBEI","shortName":"TB","isHot":false},{"lke":"TSN","cName":"天津","eName":"TIANJIN","shortName":"TJ","isHot":false},{"lke":"TTT","cName":"台东市","eName":"TAIDONGSHI","shortName":"TD","isHot":false},{"lke":"TXG","cName":"台中","eName":"TAIZHONG","shortName":"TZ","isHot":false},{"lke":"TXN","cName":"黄山","eName":"HUANGSHAN","shortName":"HS","isHot":false},{"lke":"TYN","cName":"太原","eName":"TAIYUAN","shortName":"TY","isHot":true},{"lke":"URC","cName":"乌鲁木齐","eName":"WULUMUQI","shortName":"WLMQ","isHot":true},{"lke":"UYN","cName":"榆林","eName":"YULIN","shortName":"YL","isHot":false},{"lke":"WEF","cName":"潍坊","eName":"WEIFANG","shortName":"WF","isHot":false},{"lke":"WEH","cName":"威海","eName":"WEIHAI","shortName":"WH","isHot":false},{"lke":"WHU","cName":"芜湖","eName":"WUHU","shortName":"WH","isHot":false},{"lke":"WNH","cName":"文山","eName":"WENSHAN","shortName":"WS","isHot":false},{"lke":"WNZ","cName":"温州","eName":"WENZHOU","shortName":"WZ","isHot":false},{"lke":"WOT","cName":"澎湖县","eName":"PENGHUXIAN","shortName":"PH","isHot":false},{"lke":"WUA","cName":"乌海","eName":"WUHAI","shortName":"WH","isHot":false},{"lke":"WUH","cName":"武汉","eName":"WUHAN","shortName":"WH","isHot":true},{"lke":"WUS","cName":"武夷山","eName":"WUYISHAN","shortName":"WYS","isHot":false},{"lke":"WUX","cName":"无锡","eName":"WUXI","shortName":"WX","isHot":false},{"lke":"WUZ","cName":"梧州","eName":"WUZHOU","shortName":"WZ","isHot":false},{"lke":"WXN","cName":"万州","eName":"WANZHOU","shortName":"WZ","isHot":false},{"lke":"XEN","cName":"兴城","eName":"XINGCHENG","shortName":"XC","isHot":false},{"lke":"XFN","cName":"襄樊","eName":"XIANGFAN","shortName":"XF","isHot":false},{"lke":"XIC","cName":"西昌","eName":"XICHANG","shortName":"XC","isHot":false},{"lke":"XIL","cName":"锡林浩特","eName":"XILINHAOTE","shortName":"XLHT","isHot":false},{"lke":"XIN","cName":"兴宁","eName":"XINGNING","shortName":"XN","isHot":false},{"lke":"XIY","cName":"西安","eName":"XIAN","shortName":"XA","isHot":false},{"lke":"XMN","cName":"厦门","eName":"SHAMEN","shortName":"XM","isHot":true},{"lke":"XNN","cName":"西宁","eName":"XINING","shortName":"XN","isHot":false},{"lke":"XNT","cName":"邢台","eName":"XINGTAI","shortName":"XT","isHot":false},{"lke":"XUZ","cName":"徐州","eName":"XUZHOU","shortName":"XZ","isHot":false},{"lke":"YBP","cName":"宜宾","eName":"YIBIN","shortName":"YB","isHot":false},{"lke":"YCU","cName":"运城","eName":"YUNCHENG","shortName":"YC","isHot":false},{"lke":"YIH","cName":"宜昌","eName":"YICHANG","shortName":"YC","isHot":false},{"lke":"YIN","cName":"伊宁","eName":"YINING","shortName":"YN","isHot":false},{"lke":"YIW","cName":"义乌","eName":"YIWU","shortName":"YW","isHot":false},{"lke":"YLN","cName":"铱兰","eName":"YILAN","shortName":"YL","isHot":false},{"lke":"YNJ","cName":"延吉","eName":"YANJI","shortName":"YJ","isHot":false},{"lke":"YNT","cName":"烟台","eName":"YANTAI","shortName":"YT","isHot":false},{"lke":"YNZ","cName":"盐城","eName":"YANCHENG","shortName":"YC","isHot":false},{"lke":"YUA","cName":"元谋","eName":"YUANMOU","shortName":"YM","isHot":false},{"lke":"YUS","cName":"青海省玉树","eName":"QINGHAISHENGYUSHU","shortName":"QHSYS","isHot":false},{"lke":"ZAT","cName":"昭通","eName":"ZHAOTONG","shortName":"ZT","isHot":false},{"lke":"ZHA","cName":"湛江","eName":"ZHANJIANG","shortName":"ZJ","isHot":false},{"lke":"ZHY","cName":"中卫","eName":"ZHONGWEI","shortName":"ZW","isHot":false},{"lke":"ZUH","cName":"珠海","eName":"ZHUHAI","shortName":"ZH","isHot":false},{"lke":"ZYI","cName":"遵义","eName":"ZUNYI","shortName":"ZY","isHot":false}];