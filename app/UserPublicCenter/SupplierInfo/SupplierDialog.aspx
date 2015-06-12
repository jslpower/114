<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierDialog.aspx.cs"
    Inherits="UserPublicCenter.SupplierInfo.SupplierDialog" %>

<%@ Register Src="~/SupplierInfo/UserControl/SingleFileUpload.ascx" TagName="SingleFileUpload" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布</title>
    
</head>
<body  style="width: 724px;">
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("gongqiudialog") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("news2011") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("gongqiu2011") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("swfupload") %>"></script>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("boxy") %>"></script>
    
    <form id="form2" runat="server" >
    <div id="floatBox" class="floatBox" style="display: block; width: 724px; height: auto;
        left: 0; top: 0; border: 0">
        <div class="content">
            <div id="navi">
                <ul>
                    <li class="<%=htype==1?"hover":"" %> jchange" hid="1" ><a href="javascript:;" >求购信息发布</a></li>
                    <li class="<%=htype==2?"hover":"" %> jchange" hid="2"><a href="javascript:;" >供应信息发布</a></li>
                    <input type="hidden" id="htype" name="htype" value="<%=htype %>" />
                    <div class="clear">
                    </div>
                </ul>
            </div>
            <div id="desc0" class="desc">
                <ul class="widthFix">
                    <li><span class="im">*</span><label>类别：</label>
                        <select id="sel2" name="seltype2" <%=htype==1?"style='display:none'":"" %>>
                            <% foreach (EyouSoft.Common.EnumObj obj in UserPublicCenter.SupplierInfo.SupplierCom.ExchangeTypeList_Gong)
                               { %>
                              <option <%= (ssel.ToString() == obj.Value)?"selected='selected'":""%>  value="<%=obj.Value %>"><%=obj.Text%></option>     
                             <%  } %>
                        </select>
                        <select id="sel1" name="seltype1"  <%=htype==2?"style='display:none'":"" %>>
                        
                           <% foreach (EyouSoft.Common.EnumObj obj in UserPublicCenter.SupplierInfo.SupplierCom.ExchangeTypeList_Qiu)
                              { %>
                           <option <%= (ssel.ToString() == obj.Value)?"selected='selected'":""%>  value="<%=obj.Value %>"><%=obj.Text%></option> 
                           <%} %>
                        </select>
                    </li>
                    <li class="radio"><span class="im">*</span><label>标签：</label>
                        <label for="none">
                            <input type="radio" name="type" checked="checked" value="1" style="width: 15px; height: 15px;" />
                            <img src="<%= ImageServerUrl + "/images/new2011/suplly_72.gif"%>" alt="无" /></label>
                        <label for="quality">
                            <input type="radio" name="type" style="width: 15px; height: 15px;" value="2" id="quality" /><img
                                src="<%= ImageServerUrl + "/images/new2011/icons_14.gif"%>" alt="品质" /></label>
                        <label for="special">
                            <input type="radio" name="type" style="width: 15px; height: 15px;" value="3" id="special" /><img
                                src="<%= ImageServerUrl + "/images/new2011/suplly_83.gif"%>" alt="特价" /></label>
                        <label for="hurry">
                            <input type="radio" name="type" style="width: 15px; height: 15px;" value="4" id="hurry" /><img
                                src="<%= ImageServerUrl + "/images/new2011/suplly_76.gif"%>" alt="急急急" /></label>
                        <label for="quote">
                            <input type="radio" name="type" style="width: 15px; height: 15px;" value="5" id="quote" /><img
                                src="<%= ImageServerUrl + "/images/new2011/icons_07.gif"%>" alt="最新报价" /></label>
                        <label for="hot">
                            <input type="radio" name="type" style="width: 15px; height: 15px;" value="6" id="quote" /><img
                                src="<%= ImageServerUrl + "/images/new2011/icons_09.gif"%>" alt="热" /></label>
                    </li>
                    <li><span class="im">*</span><label>标题：</label><input type="text" name="title" id="jtitle" style="width: 350px;
                        margin: 0; height: 22px; line-height: 22px; font-size: 13px; color: #333" value="请填写求购信息标题" /></li>
                    <li class="era"><span class="im">*</span><div class="box-content">
                        <div class="reviewT">
                            <span class="l">请填写您的供求信息内容</span><span class="R"><strong style="color:Red" id="jallowword">还可输入500字</strong></span>
                        </div>
                        <div class="reviewC">
                            <textarea name="content" class="" cols="103" rows="10" id="jcontent"></textarea>
                        </div>
                    </div>
                    </li>
                    
                     <li>
                       <div class="otherInfo">
                           <div>
                               <span class="left">默认信息为本地发送，点击此处将此信息发布到以下区域
                                    <a href="javascript:;" class="openRegin"><span class="gqlvse"><strong id="provshow" sta="1">（展开）</strong></span></a>
                                </span>
                                <span style="float:right; margin-right:100px;">
                                        <label id="fileInfo" style="margin-right: 4px; font-size:12px;">
                                        </label>
                                        <span id="sp1" style="position: relative; display: block; float: left"><span id="Span2"
                                            style="float: left; position: absolute; left: 0px; top: 0px">
                                        <uc2:SingleFileUpload runat="server" ID="SingleFileUpload1" JsMethodfileQueued="fileQueued1" />
                                    </span></span>
                                    </span>
                            </div>
                            
                            <div class="Allregin" style="display:none; height:auto; margin-top:8px; " id="provcon" >
                                <%if (provlist != null && provlist.Count > 0)
                                  {
                                      foreach (EyouSoft.Model.SystemStructure.SysProvince prov in provlist)
                                      { %>
                                <input value="<%=prov.ProvinceId %>" type="checkbox" name="selprov" style="width:14px;" /> <label style="font-size:12px; color:#474747; font-weight:normal;"><%=prov.ProvinceName %></label>
                                <%}
                                  } %>
                                
                            </div>
                        </div>
                    </li>
                    <li class="acc">
                        <label>联系人：</label><input type="text" id="concatname" name="txtname" value="<%=SiteUserInfo.ContactInfo.ContactName  %>" />
                        <label>MQ：</label><input type="text" id="concatmq" name="txtmq" value="<%=SiteUserInfo.ContactInfo.MQ  %>"/>
                        
                        <div style=" margin-top:10px;"><label>联系电话：</label><input type="text" style="width:250px;" id="concattel" name="txttel" value="<%=SiteUserInfo.ContactInfo.Tel  %>" /></div>
                    </li>
                    <li>
                        <input id="sss" name="sbtnSave" type="button" value="我要发布" <%= publishCount >= 15 ? "class=\"btnStyold\"" : "class=\"btnSty\""%> /> 
                        <div style=" float:right; margin-right:200px; color:Red; font-weight:bold; font-size:16px;height: 36px;margin-top: 10px;"><%= publishCount>=15?"今天已发布15条供求信息，发布条数已满。":"" %></div>
                    </li>
                    <%--<li class="links"><span class="s1"><%=SiteUserInfo.ContactInfo.ContactName  %></span> 
                                     <span class="s2"><%=string.IsNullOrEmpty(href1)?"":"<a href='"+href1+"'>进入网店</a>" %></span> 
                                     <span class="s3"><a href="<%=href2 %>" title="#">修改资料</a></span>
                                     <span class="s4"><a href="<%=href3 %>" title="#">查看已发布的供应</a></span> 
                                     <span class="s5"><a href="<%=href4 %>" title="#">查看已发布的需求</a></span> 
                                     <span class="s6"><a href="<%=href5 %>" title="#">查看关注的商机</a></span>
                    </li>--%>
                </ul>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>
    <script type="text/javascript">
        $(function() {
            ///切换
            $("li.jchange").click(function() {
                $("li.jchange").removeClass("hover");
                $(this).addClass("hover");
                $("#jtitle").focus().blur();
                var hid = $(".hover.jchange").attr("hid");
                $("#htype").val(hid);
                if (hid == 1) {
                    $("#sel1").show();
                    $("#sel2").hide();
                } else {
                    $("#sel2").show();
                    $("#sel1").hide();
                }

            });

            ///标题效果
            $("#jtitle").focus(function() {
                var that = $(this);
                if (that.val() === '请填写供应信息标题' || that.val() === '请填写求购信息标题') {
                    that.val('');
                }
            }).blur(function() {
                var that = $(this);
                if (!that.val()) {
                    that.val(gongqiu.isgong() === '1' ? '请填写求购信息标题' : '请填写供应信息标题');
                }
            });
            ///内容效果
            $("#jcontent").keyup(function() {
                var jallow = $("#jallowword");
                var lieve = 500 - $("#jcontent").val().length;
                if (lieve > 0) {
                    jallow.html("还可还输入" + lieve + "字!");
                } else {
                    jallow.html("已经超过" + (-lieve) + "字!");
                }
            });

            $("#provshow").click(function() {
                var that = $(this);
                var sta = that.attr("sta");
                if (sta === '1') {
                    that.attr("sta", "2");
                    that.html("（收起）");
                    $("#provcon").show();
                } else {
                    that.attr("sta", "1");
                    that.html("（展开）");
                    $("#provcon").hide();
                }


            })

            var gongqiu = {};
            //供还是求1求2供
            var issub = false;
            gongqiu.isgong = function() {
                issub = true;
                return $(".hover.jchange").attr("hid");
            }

            function doSubmit() {
                $("#<%= form2.ClientID %>").submit();
            }

            //提交求购
            $("#sss").click(function() {
                if ("<%=publishCount %>" < 15) {
                    if (issub) {
                        return false;
                    }
                    var sfu1 = SingleFileUpload1;
                    var _islogin = "<%= IsLogin %>";
                    if (_islogin == "False") {
                        location.href = "/Register/Login.aspx?returnurl=/SupplierInfo/PublishSupplierInfo.aspx";
                        return false;
                    } else {
                        if ($.trim($("#jtitle").val()) === "请填写求购信息标题" || $.trim($("#jtitle").val()) === "请填写供应信息标题") {
                            alert("请填写标题！");
                            $("#jtitle").focus();
                            return false;
                        }
                        if (!$.trim($("#jcontent").val())) {
                            alert("请输入内容！");
                            $("#jcontent").focus();
                            return false;
                        }
                        if ($("#jcontent").val().length > 500) {
                            alert("输入内容过长！");
                            $("#jcontent").focus();
                            return false;
                        }
                        var boolSameTime = GetAjaxSameTitle($.trim($("#jtitle").val()));
                        if (boolSameTime == "false") {
                            if (sfu1.getStats().files_queued <= 0) {
                                doSubmit();
                            }
                            else {
                                sfu1.customSettings.UploadSucessCallback = doSubmit;
                                sfu1.startUpload();
                            }
                        } else {
                            alert("您已经发布过该供求信息，请修改后重新发布！");
                            return false;
                        }
                    }
                }
            });

        });


        //检查上传文件
        function fileQueueError(file, errorCode, message) {
            try {
                var object = this.getStats();
                switch (errorCode) {
                    case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                        var fileCount = this.getSetting("file_upload_limit");
                        errorName = "当前只能上传" + fileCount + "个文件.";
                        break;
                    case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                        errorName = "您选择的文件是空的"
                        break;
                    case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                        errorName = "您选择的文件超过了指定的大小" + this.getSetting("file_size_limit");
                        break;
                    case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                        errorName = "错误的文件类型,只能上传" + this.getSetting("file_types");
                        break;
                    default:
                        errorName = message;
                        break;
                }
                alert(errorName);

            } catch (ex) {
                //this.debug(ex);
            }
        }

        //上传文件1
        function fileQueued1(file) {
            try {
                var self = this;
                var hidFileName = document.getElementById(this.customSettings.HidFileNameId);
                hidFileName.value = "";
                var newName;
                var str = file.name.split('.');
                if (str[0].length > 5) {
                    newName = str[0].substring(0, 5);
                }
                else {
                    newName = str[0];
                }
                newName += "." + str[1];

                $("#fileInfo").show();
                $("#fileInfo").html("<label title=\"" + file.name + "\">" + newName + "</label><a id=\"delfile\" href=\"javascript:;\">&nbsp;删除</a>");
                $("#delfile").click(function() {
                    $("#fileInfo").hide();
                    if ($.browser.mozilla) {
                        $("#SWFUpload_0").attr("height", 20);
                        $("#SWFUpload_0").attr("width", 95);
                    }
                    else {
                        $("#sp1").css({ left: '0px' });
                        $("#sp1").css({ position: 'relative' });
                    }
                    resetSwfupload(self, file);
                    return false;
                });
                if ($.browser.mozilla) {
                    $("#SWFUpload_0").attr("height", 0);
                    $("#SWFUpload_0").attr("width", 0);
                }
                else {
                    $("#sp1").css({ position: 'absolute', left: '-100px' });
                }
                var progress = new FileProgress(file, this.customSettings.upload_target, this);

            } catch (e) {
            }
        }

        function GetAjaxSameTitle(title) {
            var m = true;
            $.ajax({
            url: "/SupplierInfo/SupplierDialog.aspx?type=sametitle&titleinfo=" + escape(title),
                cache: false,
                async: false,
                success: function(results) {
                    m = results;
                }
            });
            return m;
        }
    </script>
</body>
</html>
