<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetAttentionCompany.aspx.cs" Inherits="IMFrame.TourAgency.TourManger.SetAttentionCompany" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组团栏目</title>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        .bar_on_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            border: 1px solid #94B2E7;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
        
        DIV.digg {	PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; MARGIN: 3px; margin-top:10px; PADDING-TOP: 3px; TEXT-ALIGN: center
}
DIV.digg A {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #54A11C; PADDING-TOP: 2px;TEXT-DECORATION: none
}
DIV.digg A:hover {	BORDER: #54A11C 1px solid; background:#54A11C; COLOR: #fff;}
DIV.digg A:active {	BORDER: #54A11C 1px solid;  COLOR: #000; }
DIV.digg SPAN.current {	BORDER: #54A11C 1px solid; PADDING-RIGHT: 5px;PADDING-LEFT: 5px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; MARGIN: 2px; COLOR: #fff; PADDING-TOP: 2px; BACKGROUND-COLOR: #54A11C}
DIV.digg SPAN.disabled {	BORDER: #eee 1px solid; PADDING-RIGHT: 5px;  PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; MARGIN: 2px;  COLOR: #ddd; PADDING-TOP: 2px;}/*end*/
    </style>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>
    
    <script type="text/javascript">
        function wsug(e, str) {
            var oThis = arguments.callee;
            if (!str) {
                oThis.sug.style.visibility = 'hidden';
                document.onmousemove = null;
                return;
            }
            if (!oThis.sug) {
                var div = document.createElement('div'), css = 'top:0; left:-30px;text-align:left;color:#2C709F;position:absolute; z-index:100; visibility:hidden';
                div.style.cssText = css;
                div.setAttribute('style', css);
                var sug = document.createElement('div'), css = 'font:normal 12px/16px "宋体"; white-space:nowrap; color:#666; padding:3px; position:absolute; left:-30px; top:0; z-index:10; background:#f9fdfd; border:1px solid #629BC7;text-align:left;color:#2C709F;';
                sug.style.cssText = css;
                sug.setAttribute('style', css);
                var dr = document.createElement('div'), css = 'position:absolute; top:3px; left:-27px; background:#333; filter:alpha(opacity=30); opacity:0.3; z-index:9';
                dr.style.cssText = css;
                dr.setAttribute('style', css);
                var ifr = document.createElement('iframe'), css = 'position:absolute; left:0; top:-10; z-index:8; filter:alpha(opacity=0); opacity:0';
                ifr.style.cssText = css;
                ifr.setAttribute('style', css);
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
            var e = e || window.event, obj = oThis.sug, dr = oThis.dr, ifr = oThis.ifr;
            obj.sug.innerHTML = str;

            var w = obj.sug.offsetWidth, h = obj.sug.offsetHeight, dw = document.documentElement.clientWidth || document.body.clientWidth; dh = document.documentElement.clientHeight || document.body.clientHeight;
            var st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
            var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17;
            obj.style.left = left + 10 + 'px';
            obj.style.top = top + 10 + 'px';
            dr.style.width = w + 'px';
            dr.style.height = h + 'px';
            ifr.style.width = w + 3 + 'px';
            ifr.style.height = h + 3 + 'px';
            obj.style.visibility = 'visible';
            document.onmousemove = function(e) {
                var e = e || window.event, st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
                var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17 + h < dh + st && e.clientY + st + 17 || e.clientY + st - 5 - h;
                obj.style.left = left + 'px';
                obj.style.top = top + 'px';
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            initArea(0);

            $("#txtCompanyName").focus(function() {
                if ($.trim(this.value) == "供应商单位名称") {
                    this.value = "";
                }
            });
            $("#txtCompanyName").blur(function() {
                if ($.trim(this.value) == "") {
                    this.value = "供应商单位名称";
                }
            });

            cSearch(1);
        });
        
        //绑定线路区域
        function initArea(areaType) {
            var s = [];

            if (cityAreas.length < 1) {
                s.push('<option value="-1">请选择线路区域</option>');
            } else {
                for (var i = 0; i < cityAreas.length; i++) {
                    if (cityAreas[i].AreaType == areaType)
                        s.push('<option value="' + cityAreas[i].AreaId + '">' + cityAreas[i].AreaName + '</option>');
                }
            }
            
            $("#txtArea").empty();
            $(s.join('')).appendTo("#txtArea");
        }

        //搜索按钮事件
        function cSearch(page) {
            var parms = { AreaId: $("#txtArea").val(), CompanyName: $("#txtCompanyName").val(), Page: page, RequestType: 2 };

            if (parms.AreaId == -1) {
                $("#divCompanyList").html("暂无批发商信息");
                $("#divPageControl").html('');
                return;
            }

            parms.CompanyName = parms.CompanyName == "供应商单位名称" ? "" : parms.CompanyName;

            $.ajax({
                type: "GET",
                url: "SetAttentionCompany.aspx",
                data: parms,
                dataType: "json",
                cache: false,
                success: function(data) {
                    if (data.recordCount > 0) {
                        $("#divCompanyList").html(data.html);

                        var config = {
                            pageSize: data.pageSize,
                            pageIndex: data.pageIndex,
                            recordCount: data.recordCount,
                            pageCount: 0,
                            gotoPageFunctionName: 'cSearch',
                            showNo:false,
                            showText:false
                        }

                        AjaxPageControls.replace("divPageControl", config);
                    } else {
                        $("#divCompanyList").html("暂无批发商信息");
                        $("#divPageControl").html('');
                    }
                }
            });
        }

        function setFavors(obj) {
            var parms = { SetType: obj.checked ? 1 : 0, FavorCompanyId: obj.value, RequestType: 1 };
            obj.disabled = true;
            $.ajax({
                type: "GET",
                url: "SetAttentionCompany.aspx",
                data: parms,
                dataType: "html",
                cache: false,
                success: function(response) {
                    switch (parseInt(response)) {
                        case 1:                            
                            $("#divMessage").html("成功");
                            break;
                        default:
                            $("#divMessage").html("失败");
                            obj.checked = !obj.checked;
                            break;
                    }
                    obj.disabled = false;
                    $("#divMessage").show();
                    setTimeout(function() {
                        $("#divMessage").hide();
                    }, 800);
                }
            });
        }
    </script>
</head>
<body oncontextmenu="return false;"><!---->
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" background="<%=ImageServerUrl %>/IM/images/ztopbj.gif">
                <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 6px;">
                    <tr>
                        <td align="left">
                            <div class="bar_on_comm">
                                <a><strong style="font-size: 14px;">我的采购目录</strong></a>
                            </div>
                        </td>
                        <td width="76" align="center">
                            <a href="javascript:history.back();">
                                <img src="<%=ImageServerUrl %>/IM/images/back.gif" alt="返回" width="16" height="16" border="0" />返回</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 8px;">
                    <tr>
                        <td align="left">
                            <img src="<%=ImageServerUrl %>/IM/images/icobu.gif" width="16" height="16" /><b>选择我想合作的供应商</b>
                        </td>
                        <td>
                            <div style="border: 1px solid #EF9739; background: #FFF8E2;width:30px; text-align:center; display:none;" id="divMessage">&nbsp;</div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="line-height:10px">
                            <img src="<%=ImageServerUrl %>/IM/images/lineb.gif" width="210" height="6" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" onkeydown="if(event.keyCode == 13){document.getElementById('btnSearch').click();event.returnValue=false;return false;}">
                    <tr>
                        <td align="left" width="30%">
                           <select name="txtAreaType" id="txtAreaType" onchange="initArea(this.value)">
                                <option value="0">国内长线</option>
                                <option value="1">国际线</option>
                                <option value="2">国内短线</option>
                           </select>
                        </td>
                        <td align="left" width="70%">
                            <select name="txtArea" id="txtArea">
                                
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" bgcolor="#FFFFFF">
                            <input name="txtCompanyName" value="供应商单位名称" style="color: #999999; font-size: 13px;" type="text" size="20" id="txtCompanyName" />
                            <input type="image" src="<%=ImageServerUrl %>/IM/images/btn1.gif" style="margin-bottom: -4px;" onclick="cSearch(1); return false;" id="btnSearch" />
                        </td>
                    </tr>
                </table>
                <div id="divCompanyList" align="center">
                </div>
                <div id="divPageControl" class="digg">                
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="40" align="center">
                            <input type="button" id="btnEndSettion" onclick="javascript:history.back();" value="完成设置" />
                        </td>
                    </tr>
                </table>
        </tr>
    </table>
    </form>
</body>
</html>
