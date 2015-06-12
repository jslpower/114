var FV_onBlur = {
    initValid: function(frm, NoKeyword, model) {
        var self = this;
        var formElements = frm.elements;
        if (model == undefined || model == null) {
            ValiDatorForm.model = true;
        } else {
            ValiDatorForm.model = model;
        }
        for (var i = 0; i < formElements.length; i++) {
            var validType;
            if (NoKeyword != null) {
                if (formElements[i].id.indexOf(NoKeyword) < 0)
                { validType = formElements[i].getAttribute('valid'); }
            }
            else {
                validType = formElements[i].getAttribute('valid');
            }
            if (validType != null) {
                var fn = (function(a, b) {
                    return function() { self._validInput(a, b) };
                })(formElements[i], frm);
                self._addEvent(formElements[i], "blur", fn, false);
            }
        }
    },
    /*private*/
    _validInput: function(ipt, frm, p) {
        if (p == null) p = 'errMsg_';
        var fv = new FormValid(frm);
        var formElements = frm.elements;
        var msgs = ValiDatorForm.fvCheck(ipt, fv, formElements);
        var errmsgend = ipt.getAttribute("errmsgend");
        if (errmsgend == null) {
            errmsgend = ipt.id;
        }
        var jForm = $(frm);
        if (msgs.length > 0) {
            //document.getElementById(p+errmsgend).innerHTML = msgs.join(',');
            jForm.find("span[id=" + p + errmsgend + "]").html(msgs.join(','));
        } else {
            //document.getElementById(p+errmsgend).innerHTML = '';
            jForm.find("span[id=" + p + errmsgend + "]").html('');
        }
    },
    /*private*/
    _addEvent: function(elm, evType, fn, useCapture) {
        if (elm.addEventListener) {
            elm.addEventListener(evType, fn, useCapture);
            return true;
        } else if (elm.attachEvent) {
            var r = elm.attachEvent('on' + evType, fn);
            return r;
        } else {
            elm['on' + evType] = fn;
        }
    }
};
var FormValid = function(frm) {
    this.frm = frm;
    this.errMsg = new Array();
    this.errName = new Array();
    this.required = function(inputObj) {
        if (typeof (inputObj) == "undefined" || inputObj.value.trim() == "") {
            return false;
        }
        return true;
    },
    this.checked = function(inputObj) {
        return inputObj.checked;
    },
    this.equal = function(inputObj, formElements) {
        var fstObj = inputObj;
        var sndObj = formElements[inputObj.getAttribute('eqaulName')];
        if (fstObj != null && sndObj != null) {
            if (fstObj.value != sndObj.value) {
                return false;
            }
        }
        return true;
    }
    this.gt = function(inputObj, formElements) {
        var fstObj = inputObj;
        var sndObj = formElements[inputObj.getAttribute('eqaulName')];
        if (fstObj != null && sndObj != null && fstObj.value.trim() != '' && sndObj.value.trim() != '') {
            if (parseFloat(fstObj.value) <= parseFloat(sndObj.value)) {
                return false;
            }
        }
        return true;
    }
    this.compare = function(inputObj, formElements) {
        var fstObj = inputObj;
        var sndObj = formElements[inputObj.getAttribute('objectName')];
        if (fstObj != null && sndObj != null && fstObj.value.trim() != '' && sndObj.value.trim() != '') {
            if (!(eval(parseFloat(fstObj.value) + inputObj.getAttribute('operate') + parseFloat(sndObj.value)))) {
                return false;
            }
        }
        return true;
    }
    this.comparedate = function(inputObj, formElements) {
        var startdate = inputObj.value;
        var enddate = formElements[inputObj.getAttribute('compareName')].value;

        startdate = startdate.replace(/-/g, "/");
        enddate = enddate.replace(/-/g, "/");

        enddate = Date.parse(enddate);
        startdate = Date.parse(startdate);
        if (startdate - enddate > -1) {
            return false;
        }
        else {
            return true;
        }
    }
    this.limitDate = function(inputObj) {
        var len = inputObj.value.length;
        if (len) {
            var minDate = Date.parse(inputObj.getAttribute('mindate').replace(/-/g, "/"));
            var currDate = Date.parse(inputObj.value.replace(/-/g, "/"));
            if (minDate - currDate > -1) {
                return false;
            }
            return true;
        }
        return true;
    }
    this.limit = function(inputObj) {
        var len = inputObj.value.length;
        if (len) {
            var minv = parseInt(inputObj.getAttribute('min'));
            var maxv = parseInt(inputObj.getAttribute('max'));
            minv = minv || 0;
            maxv = maxv || Number.MAX_VALUE;
            return minv <= len && len <= maxv;
        }
        return true;
    }
    this.range = function(inputObj) {
        var val = parseInt(inputObj.value);
        if (inputObj.value) {
            var minv = parseInt(inputObj.getAttribute('min'));
            var maxv = parseInt(inputObj.getAttribute('max'));
            minv = minv || 0;
            maxv = maxv || Number.MAX_VALUE;
            return minv <= val && val <= maxv;
        }
        return true;
    },
	this.requiredSelected = function(selectObj, formElements) {
	    var minv = parseInt(selectObj.getAttribute('min'));
	    var maxv = parseInt(selectObj.getAttribute('max'));
	    minv = minv || 0;
	    maxv = maxv || Number.MAX_VALUE;

	    var selectedCount = 0, isSelected = false;
	    for (var i = 0; i < selectObj.options.length; i++) {
	        isSelected = selectObj.options[i].selected;
	        if (isSelected) {
	            selectedCount++;
	        }
	    }
	    return minv <= selectedCount && selectedCount <= maxv;
	},
	this.requiredRadioed = function(inputObj, formElements) {
	    var groups = document.getElementsByName(inputObj.name);
	    var isChecked = false;
	    for (var i = 0; i < groups.length; i++) {
	        if (groups[i].checked) { isChecked = true; break; }
	    }
	    return isChecked;
	},
	this.requireChecked = function(inputObj, formElements) {
	    var minv = parseInt(inputObj.getAttribute('min'));
	    var maxv = parseInt(inputObj.getAttribute('max'));
	    var arrayName = null;
	    var pos = inputObj.name.indexOf('[');
	    if (pos != -1) {
	        arrayName = inputObj.name.substr(0, pos);
	    }
	    minv = minv || 1;
	    maxv = maxv || Number.MAX_VALUE;
	    var checked = 0;
	    if (!arrayName) {
	        var groups = document.getElementsByName(inputObj.name);
	        for (var i = 0; i < groups.length; i++) {
	            if (groups[i].checked) { checked++; }
	        }
	    } else {
	        for (var i = 0; i < formElements.length; i++) {
	            var e = formElements[i];
	            if (e.checked == true && e.type == 'checkbox'
					&& e.name.substring(0, arrayName.length) == arrayName) {
	                checked++;
	            }
	        }
	    }
	    return minv <= checked && checked <= maxv;
	}
    this.filter = function(inputObj) {
        var value = inputObj.value;
        var allow = inputObj.getAttribute('allow');
        if (value.trim()) {
            return new RegExp("^.+\.(?=EXT)(EXT)$".replace(/EXT/g, allow.split(/\s*,\s*/).join("|")), "gi").test(value);
        }
        return true;
    }
    this.isNo = function(inputObj) {
        var value = inputObj.value;
        var noValue = inputObj.getAttribute('noValue');
        return value != noValue;
    }
    this.isTelephone = function(inputObj) {
        inputObj.value = inputObj.value.trim();
        if (inputObj.value == '') {
            return true;
        } else {
            if (!RegExps.isMobile.test(inputObj.value) && !RegExps.isPhone.test(inputObj.value)) {
                return false;
            }
        }
        return true;
    }
    this.checkReg = function(inputObj, reg, msg) {
        inputObj.value = inputObj.value.trim();
        if (inputObj.value == '') {
            return true;
        } else {
            return reg.test(inputObj.value);
        }
    }
    this.passed = function(errMode) {
        if (this.errMsg.length > 0) {
            if (errMode == "span") {
                FormValid.showError(this.errMsg, this.errName, this.frm);
            }
            if (errMode == "alert") {
                FormValid.alertError(this.errMsg);
            }
            if (errMode == 'alertspan') {
                FormValid.alertError(this.errMsg);
                FormValid.showError(this.errMsg, this.errName, this.frm);
            }
            if (this.errName[0].indexOf('[') == -1) {
                frt = document.getElementById(this.errName[0]);
                if (frt != null) {
                    if (frt.type == 'text' || frt.type == 'password' || frt.type == 'select-one') {
                        frt.focus();
                    }
                } else {
                    var obj = document.getElementById("errMsg_" + this.errName[0]);
                    if (obj != null) {
                        try { obj.focus();} catch (e) { }
                    }
                }
            }
            return false;
        }
        else {
            return FormValid.succeed(this.frm.name);
        }
    }
    this.addErrorMsg = function(name, str) {
        this.errMsg.push(str);
        this.errName.push(name);
    }
    this.addAllName = function(name) {
        FormValid.allName.push(name);
    }
};
FormValid.allName = new Array();
FormValid.alertError = function(errMsg) {
    var msg = "";
    for (i = 0; i < errMsg.length; i++) {
        msg += "- " + errMsg[i] + "\n";
    }
    alert(msg);
};
FormValid.showError = function(errMsg, errName, formName) {
    //if (formName=='form1') {
    /*for (key in FormValid.allName) {
    var ss=FormValid.allName[key];
    //this.controlPostfix()
    alert(ss);
    //$('#errMsg_'+this.controlPostfix(FormValid.allName[key])).html("");
    }*/
    for (var key = 0; key < errMsg.length; key++) {
        //var tableId = null;
//        $(formName).find("*").each(function() {
//            if ($(this).attr("id") != undefined && $(this).attr("id") != "") {
//                tableId = $(this).attr("id");
//            }
//        });
//        if (tableId != null) {
//            $("#" + tableId).find("span[id$=errMsg_" + errName[key] + "]").html(errMsg[key]);
//        } else {
//            $('#errMsg_' + errName[key]).html(errMsg[key]);
//        }
        var obj = $(formName).find("span[id$=errMsg_" + errName[key] + "]");
        if(obj.length==0){
            obj = $('#errMsg_' + errName[key]);
        }
        obj.html(errMsg[key]);
        //$(formName).find("span[id$=errMsg_" + errName[key] + "]").html(errMsg[key]);
    }
    //}
}
FormValid.succeed = function() {
    return true;
};
var ValiDatorForm = {
    /*
    frm【FormValid对象】，
    errPatten参数说明【alert表示提示信息使用弹框，span表示提示信息在span标签显示】，
    NoKeyword【匹配ID属性是以NoKeyword开始的元素，这些元素不进行验证】.
    model【默认为true,验证元素valid中所有的验证表达式，设置为false时,当元素valid第一个验证表达式不通过时，不继续其他的验证，跳转到下一个元素 】.
    */
    model: true,
    validator: function(frm, errPattern, NoKeyword, model) {
        //debugger;
        if (model == undefined || model == null) {
            ValiDatorForm.model = true;
        } else {
            ValiDatorForm.model = model;
        }
        this.ClearerrMsg(frm);
        var formElements = frm.elements;
        var fv = new FormValid(frm);
        FormValid.allName = new Array();
        for (var i = 0; i < formElements.length; i++) {
            var f;
            if (NoKeyword != null) {
                if (formElements[i].id.indexOf(NoKeyword) > -1)
                { f = formElements[i] }
            }
            else {
                f = formElements[i]
            }
            if (f != null) {
                var msgs = this.fvCheck(f, fv, formElements);
                var errMsgEnd = f.getAttribute("errmsgend");
                if (errMsgEnd == null) {
                    errMsgEnd = f.id;
                }
                if (msgs.length > 0) {
                    for (var j = 0; j < msgs.length; j++) {
                        //fv.addErrorMsg(f.id,msgs[j]);
                        fv.addErrorMsg(errMsgEnd, msgs[j]);
                    }
                }
            }
        }
        return fv.passed(errPattern);
    },
    /*
    frm【FormValid对象】，
    errPatten参数说明【alert表示提示信息使用弹框，span表示提示信息在span标签显示】，
    attribute_No_Key,attribute_No_Value
    【匹配元素中是否有自定义attribute_No_Key属性，并且该自定义属性的值等于attribute_No_Value
    ，这些元素不进行验证】.
    model【默认为true,验证元素valid中所有的验证表达式，设置为false时,当元素valid第一个验证表达式不通过时，不继续其他的验证，跳转到下一个元素 】.
    */
    validator2: function(frm, errPattern, attribute_No_Key, attribute_No_Value, model) {
        if (model == undefined || model == null) {
            ValiDatorForm.model = true;
        } else {
            ValiDatorForm.model = model;
        }
        this.ClearerrMsg(frm);
        var formElements = frm.elements;
        var fv = new FormValid(frm);
        FormValid.allName = new Array();
        for (var i = 0; i < formElements.length; i++) {
            var f;
            if (attribute_No_Key != null) {

                if ($(formElements[i]).attr(attribute_No_Key) == undefined || $(formElements[i]).attr(attribute_No_Key) != attribute_No_Value) {
                    f = formElements[i]
                }
            }
            else {
                f = formElements[i]
            }
            if (f != null) {
                var msgs = this.fvCheck(f, fv, formElements);
                alert(msgs);
                var errMsgEnd = f.getAttribute("errmsgend");
                if (errMsgEnd == null) {
                    errMsgEnd = f.id;
                }
                if (msgs.length > 0) {
                    for (var j = 0; j < msgs.length; j++) {
                        //fv.addErrorMsg(f.id,msgs[j]);
                        fv.addErrorMsg(errMsgEnd, msgs[j]);
                    }
                }
            }
        }
        return fv.passed(errPattern);
    },
    CheckValidator: function(obj, frm, errPattern, NoKeyword, objcontrol) {
        var self = this;
        if (!obj.checked) {
            document.getElementById(objcontrol).style.display = 'none';
            FV_onBlur.initValid(frm, NoKeyword);
            frm.onsubmit = function() {
                return self.validator(frm, errPattern, NoKeyword);
            }
            this.ClearerrMsg(frm, NoKeyword);
        }
        else {
            document.getElementById(objcontrol).style.display = '';
            FV_onBlur.initValid(frm);
            frm.onsubmit = function() {
                return self.validator(frm, errPattern);
            }
        }
    },
    ClearerrMsg: function(frm, NoKeyword) {
        var errMsgEnd;
        for (var i = 0; i < frm.length; i++) {
            if (frm[i].id.indexOf(NoKeyword) > -1) {
                errMsgEnd = frm[i].getAttribute("errmsgend");
                if (errMsgEnd == null) {
                    errMsgEnd = frm[i].id;
                }
                var obj = document.getElementById('errMsg_' + errMsgEnd);
                if (obj != null) {
                    obj.innerHTML = '';
                    frm[i].onblur = null;
                }
            }
        }
    },
    ClearerrMsg: function(frm) {
        var errMsgEnd;
        for (var i = 0; i < frm.length; i++) {
            errMsgEnd = frm[i].getAttribute("errmsgend");
            if (errMsgEnd == null) {
                errMsgEnd = frm[i].id;
            }
            var obj = document.getElementById('errMsg_' + errMsgEnd);
            if (obj != null) {
                obj.innerHTML = '';
            }
        }
    },
    fvCheck: function(e, fv, formElements) {
        var validType = e.getAttribute('valid');
        var errorMsg = e.getAttribute('errmsg');
        if (!errorMsg) {
            errorMsg = '';
        }
        if (validType == null) { return [] };
        fv.addAllName(e.id);
        var vts = validType.split('|');
        var ems = errorMsg.split('|');
        var r = [];
        for (var j = 0; j < vts.length; j++) {
            var curValidType = vts[j];
            var curErrorMsg = ems[j];
            var validResult;
            switch (curValidType) {
                case 'isNumber':
                case 'isEmail':
                case 'isPhone':
                case 'isMobile':
                case 'isIdCard':
                case 'isMoney':
                case 'isZip':
                case 'isQQ':
                case 'isInt':
                case 'isEnglish':
                case 'isChinese':
                case 'isUrl':
                case 'notHttpUrl':
                case 'isDate':
                case 'isTime':
                case 'RegInteger':
                case 'PositiveIntegers':
                case 'isTel':
                case 'IsDecimalTwo':
                case 'isPIntegers':
                case 'IsDecimalOne':
                    validResult = fv.checkReg(e, RegExps[curValidType], curErrorMsg);
                    break;
                case 'regexp':
                    validResult = fv.checkReg(e, new RegExp(e.getAttribute('regexp'), "g"), curErrorMsg);
                    break;
                case 'custom':
                    validResult = eval(e.getAttribute('custom') + '(e,formElements)');
                    break;
                /*
                case 'custom1':
                validResult = eval(e.getAttribute('custom1')+'(e,'+validResult+')');
                break;
                */ 
                default:
                    validResult = eval('fv.' + curValidType + '(e,formElements)');
                    break;
            }
            if (!validResult) r.push(curErrorMsg);
            if (!validResult && ValiDatorForm.model == false) {
                break;
            }
        }
        return r;
    }
};
String.prototype.trim = function() {
    return this.replace(/^\s*|\s*$/g, "");
};
var RegExps = function() { };
RegExps.isNumber = /^[-\+]?\d+(\.\d+)?$/;
RegExps.isEmail = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)/;
RegExps.isPhone = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$/;
RegExps.isMobile = /^(13|15|18|14)\d{9}$/;
RegExps.isIdCard = /(^\d{15}$)|(^\d{17}[0-9Xx]$)/;
RegExps.isMoney = /^\d+(\.\d+)?$/;
RegExps.isZip = /^[1-9]\d{5}$/;
RegExps.isQQ = /^[1-9]\d{4,10}$/;
RegExps.isInt = /^[-\+]?\d+$/;
RegExps.isEnglish = /^[A-Za-z]+$/;
RegExps.isChinese = /^[\u0391-\uFFE5]+$/;

RegExps.notHttpUrl = /[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
RegExps.isUrl = /^http[s]?:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
RegExps.isDate = /^\d{4}-\d{1,2}-\d{1,2}$/;
RegExps.isTime = /^\d{4}-\d{1,2}-\d{1,2}\s\d{1,2}:\d{1,2}:\d{1,2}$/;
RegExps.RegInteger = /^[0-9]+$/;
RegExps.PositiveIntegers = /^[1-9]+$/;
RegExps.IsDecimalTwo = /^[0-9]+([.]\d{1,2})?$/;
RegExps.IsDecimalOne = /^[0-9]+([.]\d{1})?$/;
//Phone code or Mobile Code
RegExps.isTel = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$|^(13|15|18|14)\d{9}$/;
RegExps.isPIntegers = /^[1-9]\d*$/;