<%@ Page Title="发供求_旅游信息_旅行社发布供求信息_同业114发供求频道"  Language="C#" MasterPageFile="~/SupplierInfo/SupplierNew.Master" AutoEventWireup="true" CodeBehind="PublishSupplierInfo.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.PublishSupplierInfo" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/SingleFileUpload.ascx" TagName="SingleFileUpload" TagPrefix="uc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierHead" runat="server">
<meta name="keywords" content="旅游供求，旅游供求信息，旅行社线路报价，酒店团队报价，旅游信息发布，组团拼团信息,旅游票务签证,旅行社收客,特价旅游信息,出团计划,寻求旅游合作,团队询价,地接报价,旅行社合作,车辆租赁,导游招聘,旅游同行,行业资讯,同业学堂" />
<meta name="description" content="发供求，旅游供求频道,为您提供最新旅游信息,旅游产品即时报价,特价旅游线路和旅行社收客信息查询,还可以发布团队询价,地接报价,组团拼团,旅游票务签证,车辆租赁等旅游供求信息" />
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupplierBody" runat="server">

<form id="from1" runat="server">
<div id="supply-right" style=" padding:0; overflow:hidden; border:0; margin:0; padding:0">
<!--供求信息发布表单 start-->
    <div class="addVg addVgFIX" style=" padding:0; margin:0; *width:706px;*position:relative;*overflow:hidden">
        <div id="navi">
            <ul>
                <li class="selectTag"><a onclick="selectTag('tagContent0',this)" href="javascript:void(0)">求购信息</a></li>
                <li><a onclick="selectTag('tagContent1',this)" href="javascript:void(0)">供应信息</a> </li>
            </ul>
        </div>
        <div id=tagContent>
          <div class="tagContent floatBox floatBoxFix" id="tagContent0" name="tagContent0" style=" display:block;"> 
                <div id="desc1" class="desc content">
                  <ul class="widthFix">        
                    <li><span class="im">*</span><label>类别：</label><select name="chooseType" id="chooseType"><%=UserPublicCenter.SupplierInfo.SupplierCom.ExchangeTypeOptionList_Qiu%></select></li>
                    <li class="radio" id="Tags" name="Tags"><span class="im">*</span><label>标签：</label>
                         <asp:Repeater runat="server" ID="rptExchangeTag">
                            <ItemTemplate>
                                    <input type="radio" id="radioTag<%# Container.ItemIndex + 1 %>" <%# (Container.ItemIndex+1)==1?"checked":"" %> name="radioTag" value="<%# Eval("value") %>" />
                                    <label for="radioTag<%# Container.ItemIndex + 1 %>"><%# GetTagImg( Eval("value"),1) %></label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>
                    <li><span class="im">*</span><label>标题：</label>
                        <input type="text" name="txtTitle" id="txtTitle" style=" color: #666; width:350px; margin:0; height:22px; line-height:22px; font-size:13px;" 
                        value="请填写求购信息标题" onfocus="if(this.value == '请填写求购信息标题') {this.value = '';}" onblur="if (this.value == '') {this.value = '请填写求购信息标题';}"/></li>
                    <li class="era"><span class="im">*</span><div class="box-content">
                         <div class="reviewT">
                              <span class="l">请填写您的供求信息内容</span>
                              <span class="R"><strong style="color:Red" id="jallowword">还可输入500字</strong></span>
                         </div> 
                        <div class="reviewC">
                             <textarea  name="txtInfo" id="txtInfo" name="txtInfo" cols="103" rows="10"></textarea>
                        </div>    
                        </div> 
                    </li>
                    <li>
                        <div class="otherInfo">
                           <div>
                                <span class="left">默认信息为本地发送，点击此处将此信息发布到以下区域
                                    <a href="javascript:void(0);" class="openRegin" onclick="javascript:ShowProvince();"><span class="gqlvse"><strong id="strongShow">（展开）</strong></span></a>
                                </span>
                                <span style="float:right; margin-right:100px; font-size:12px;">
                                    <label id="fileInfo" style="margin-right: 4px;font-size:12px;">
                                    </label>
                                    <span id="sp1" style="position: relative; display: block; float: left"><span id="sp"
                                        style="float: left; position: absolute; left: 0px; top: 0px">
                                        <uc2:SingleFileUpload runat="server" ID="SingleFileUpload1" JsMethodfileQueued="fileQueued1" />
                                    </span></span>
                                </span>
                            </div>
                            <div style="display: none;font-size:12px; height:auto; margin-top:8px;" id="menu_1" divstate="0" >
                                <asp:Repeater runat="server" ID="rptProvince">
                                    <FooterTemplate>
                                        <div style="float:right;"><input type="checkbox" id="CkAllProvinces" /><label for="CkAllProvinces">(全选/反选)</label></div>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <nobr><input id="ckb<%# Container.ItemIndex + 1 %>" name="ckbProvince" value="<%# Eval("ProvinceId") %>"
                                            type="checkbox" /><label for="ckb<%# Container.ItemIndex + 1 %>" style=" font-size:12px;"><%# Eval("ProvinceName") %></label></nobr>
                                    </ItemTemplate> 
                                </asp:Repeater>
                            </div>
                        </div>
                     </li>
                     <li class="acc">
                        <label>联系人：</label>
                        <input runat="server" id="txtName" name="txtName" type="text" style="width: 100px; border: 1px solid #ccc;
                                        height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" />
                        <label>MQ：</label>
                        <input runat="server" id="txtMQ" name="txtMQ" type="text" style="width: 100px; border: 1px solid #ccc;
                                        height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" />
                        <div style=" margin-top:10px;">
                        <label>联系电话：</label>
                        <input runat="server" id="txtTel" name="txtTel" type="text" style="width: 250px; border: 1px solid #ccc;
                                        height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" /></div>
                     </li>
                     <li>
                        <input id="btnSave" name="btnSave" type="button" value="我要发布"  <%= publishCount >= 15 ? "class=\"btnStyold\"" : "class=\"btnSty\""%> />
                        <div style=" float:right; margin-right:200px; color:Red; font-weight:bold; font-size:16px;height: 36px;margin-top: 10px;"><%= publishCount >= 15?"今天已发布15条供求信息，发布条数已满。":"" %></div>
                     </li>
                     <li class="links">
                        <span class="s1"><%=strUserName %></span>
                        <span class="s2"><a runat="server" id="aShop" target="_blank">进入网店</a></span>
                        <span class="s3"><a runat="server" id="aEditUser" target="_blank">修改资料</a></span>
                        <span class="s4"><a runat="server" id="aSupplyInfo1" target="_blank">查看已发布的供应</a></span>
                        <span class="s5"><a runat="server" id="aSupplyInfo2" target="_blank">查看已发布的需求 </a></span>
                        <span class="s6"><a runat="server" id="aSupplyInfo3" target="_blank">查看关注的商机</a></span>
                     </li>
                </ul>
                </div>  
          </div>
          <div class="tagContent floatBox floatBoxFix" id="tagContent1" name="tagContent1">
              <div id="desc1" class="desc content">
                  <ul class="widthFix">        
                        <li><span class="im">*</span><label>类别：</label><select name="schooseType" id="schooseType"><%=UserPublicCenter.SupplierInfo.SupplierCom.ExchangeTypeOptionList_Gong %></select></li>
                        <li class="radio"><span class="im">*</span><label>标签：</label>
                             <asp:Repeater runat="server" ID="srptExchangeTag">
                                <ItemTemplate>
                                     <input type="radio"  id="sradioTag<%# Container.ItemIndex + 1 %>"  <%# (Container.ItemIndex+1)==1?"checked":"" %>  name="sradioTag" value="<%# Eval("value") %>" />
                                     <label for="sradioTag<%# Container.ItemIndex + 1 %>"><%# GetTagImg( Eval("value"),1) %></label>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                        <li><span class="im">*</span><label>标题：</label>
                            <input type="text" name="stxtTitle" id="stxtTitle" style=" color: #666; width:350px; margin:0; height:22px; line-height:22px; font-size:13px;" 
                                value="请填写供应信息标题" onfocus="if(this.value == '请填写供应信息标题') {this.value = '';}" onblur="if (this.value == '') {this.value = '请填写供应信息标题';}" /></li>
                        <li class="era"><span class="im">*</span><div class="box-content">
                             <div class="reviewT">
                                  <span class="l">用户名：理智上网，拒绝恶意谩骂</span>
                                  <span class="R">[内容不能超过500个字符]<strong style="color:Red" id="sjallowword">还可输入500字</strong></span>
                             </div> 
                            <div class="reviewC">
                                 <textarea  name="stxtInfo" id="stxtInfo" name="stxtInfo" cols="103" rows="10"></textarea>
                            </div>    
                            </div> 
                        </li>
                        <li>
                            <div class="otherInfo">
                               <div>
                                    <span class="left">默认信息为本地发送，点击此处将此信息发布到以下区域
                                        <a href="javascript:void(0);" onclick="javascript:ShowProvince1();" class="openRegin">
                                        <span class="gqlvse"><strong id="strongShow1">（展开）</strong></span></a>
                                    </span>
                                    <span style="float:right; margin-right:100px;">
                                        <label id="fileInfo2" style="margin-right: 4px; font-size:12px;">
                                        </label>
                                        <span id="sp2" style="position: relative; display: block; float: left"><span id="Span2"
                                            style="float: left; position: absolute; left: 0px; top: 0px">
                                            <uc2:SingleFileUpload runat="server" ID="SingleFileUpload2" JsMethodfileQueued="fileQueued2" />
                                        </span></span>
                                    </span>
                                </div>
                                <div style="display: none;height:auto; margin-top:8px;" id="menu_2" divstate="0">
                                    <asp:Repeater runat="server" ID="srptProvince">
                                        <FooterTemplate>
                                            <div style="float:right;"><input type="checkbox" id="sCkAllProvinces" name="sCkAllProvinces" /><label for="sCkAllProvinces">(全选/反选)</label></div>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <nobr><input id="sckb<%# Container.ItemIndex + 1 %>" name="sckbProvince" value="<%# Eval("ProvinceId") %>"
                                                type="checkbox" /><label for="sckb<%# Container.ItemIndex + 1 %>" style=" font-size:12px;"><%# Eval("ProvinceName") %></label></nobr>
                                        </ItemTemplate> 
                                    </asp:Repeater>
                                </div>
                            </div>
                         </li>
                         <li class="acc">
                            <label>联系人：</label>
                            <input runat="server" id="stxtName"  type="text" style="width: 100px; border: 1px solid #ccc;
                                            height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" />
                            <label>MQ：</label>
                            <input runat="server" id="stxtMQ"  type="text" style="width: 100px; border: 1px solid #ccc;
                                            height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" />
                            <div style=" margin-top:10px;">
                            <label>联系电话：</label>
                            <input runat="server" id="stxtTel" type="text" style="width: 250px; border: 1px solid #ccc;
                                            height: 16px; font-size: 14px; color: #666;  padding-top: 0px;" /></div>
                         </li>
                         
                         <li>
                            <input id="sbtnSave" name="sbtnSave" type="button" value="我要发布" <%= publishCount >= 15 ? "class=\"btnStyold\"" : "class=\"btnSty\""%> /> 
                            <div style=" float:right; margin-right:200px; color:Red; font-weight:bold; font-size:16px;height: 36px;margin-top: 10px;"><%= publishCount>=15?"今天已发布15条供求信息，发布条数已满。":"" %></div>
                         </li>
                         <li class="links">
                            <span class="s1"><%=strUserName %></span>
                            <span class="s2"><a runat="server" id="saShop" target="_blank">进入网店</a></span>
                            <span class="s3"><a runat="server" id="saEditUser" target="_blank">修改资料</a></span>
                            <span class="s4"><a runat="server" id="saSupplyInfo1" target="_blank">查看已发布的供应</a></span>
                            <span class="s5"><a runat="server" id="saSupplyInfo2" target="_blank">查看已发布的需求 </a></span>
                            <span class="s6"><a runat="server" id="saSupplyInfo3" target="_blank">查看关注的商机</a></span>
                         </li>
                    </ul>
              </div> 
        </div>
    </div>
    </div>
<!--发布供求信息 end-->
    <div class="hr-10"></div>
<!--列表 start-->
   <div class="box">
       <div class="box-l">
          <div class="box-r">
             <div class="box-c">
                 <h3 class="add">最新供需 </h3>
             </div>
          </div>
       </div>
   <div class="box-main">
       <div class="box-content">
        <table width="643" border="0" class="reList reList1">
            <cc1:CustomRepeater ID="crptNewest" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="57"><div><%# GetTagImg(Eval("ExchangeTag"),2)%></div></td>
                        <td width="100" class="regin"><%# InitProvince(EyouSoft.Common.Utils.GetInt(Eval("ProvinceId").ToString())) %></td>
                        <td width="350" class="title" style="text-align:left"><a target="_blank" href='<%# EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId) %>' title='<%# Eval("ExchangeTitle").ToString() %>'><%# Utils.GetText(Eval("ExchangeTitle").ToString(), 20) %></a></td>
                        <td width="100" class="pubTime"><%# Eval("IssueTime","{0:MM-dd hh:mm}") %></td>
                        <td width="128" class="supplier"><%# Eval("CompanyName")%></td>
                        <td width="46"><%# Utils.GetMQ(Eval("OperatorMQ").ToString())%></td>
                      </tr>
                </ItemTemplate>
            </cc1:CustomRepeater>
        </table>
       </div> 
   </div>
   </div>
<!--列表 end-->
<!--列表 end-->
</div>
<input id="hidSupply" name="hidSupply" type="hidden" value="" />
<input id="hidReach" name="hidReach" type="hidden" value="" />
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SupplierBottom" runat="server">
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>
    
    <script type="text/javascript">
        $(function() {
            ///内容效果
            $("#txtInfo").keyup(function() {
                var jallow = $("#jallowword");
                var lieve = 500 - $("#txtInfo").val().length;
                if (lieve > 0) {
                    jallow.html("还可还输入" + lieve + "字!");
                } else {
                    jallow.html("已经超过" + (-lieve) + "字!");
                }
            });
            ///内容效果
            $("#stxtInfo").keyup(function() {
                var jallow = $("#sjallowword");
                var lieve = 500 - $("#stxtInfo").val().length;
                if (lieve > 0) {
                    jallow.html("还可还输入" + lieve + "字!");
                } else {
                    jallow.html("已经超过" + (-lieve) + "字!");
                }
            });
            
        });
        //供应和求购DIV切换
         function selectTag(showContent, selfObj) {
             var tag = document.getElementById("navi").getElementsByTagName("li");
             var taglength = tag.length;
             for (i = 0; i < taglength; i++) {
                 tag[i].className = "";
             }
             selfObj.parentNode.className = "selectTag";
             for (i = 0; j = document.getElementById("tagContent" + i); i++) {
                 j.style.display = "none";
             }
             document.getElementById(showContent).style.display = "block";
         }

         //显示/隐藏省份选择框
         function ShowProvince() {
             if ($("#menu_1").attr("divState") == 0) {
                 $("#menu_1").show();
                 $("#menu_1").attr("divState", "1");
                 $("#strongShow").html("（关闭）");
             }
             else {
                 $("#menu_1").hide();
                 $("#menu_1").attr("divState", "0");
                 $("#strongShow").html("（展开）");
             }
         }

         function ShowProvince1() {
             if ($("#menu_2").attr("divState") == 0) {
                 $("#menu_2").show();
                 $("#menu_2").attr("divState", "1");
                 $("#strongShow1").html("（关闭）");
             }
             else {
                 $("#menu_2").hide();
                 $("#menu_2").attr("divState", "0");
                 $("#strongShow1").html("（展开）");
             }
         }

         

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
         
         //上传文件2
         function fileQueued2(file) {
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

                 $("#fileInfo2").show();
                 $("#fileInfo2").html("<label title=\"" + file.name + "\">" + newName + "</label><a id=\"delfile2\" href=\"javascript:;\">&nbsp;删除</a>");
                 $("#delfile2").click(function() {
                 $("#fileInfo2").hide();
                     if ($.browser.mozilla) {
                         $("#SWFUpload_1").attr("height", 20);
                         $("#SWFUpload_1").attr("width", 95);
                     }
                     else {
                         $("#sp2").css({ left: '0px' });
                         $("#sp2").css({ position: 'relative' });
                     }
                     resetSwfupload(self, file);
                     return false;
                 });
                 if ($.browser.mozilla) {
                     $("#SWFUpload_1").attr("height", 0);
                     $("#SWFUpload_1").attr("width", 0);
                 }
                 else {
                     $("#sp2").css({ position: 'absolute', left: '-100px' });
                 }
                 var progress = new FileProgress(file, this.customSettings.upload_target, this);

             } catch (e) {
             }
         }
         var isSubmit = false; //区分按钮是否提交过
         //模拟一个提交按钮事件
         function doSubmit() {
             isSubmit = true;
             $("#<%= from1.ClientID %>").get(0).submit();
        }

        $(document).ready(function() {
            $("#CkAllProvinces").click(function() {
                var state = $(this).attr("checked");
                $(":checkbox[name]='ckbProvince'").each(function() {
                    $(this).attr("checked", state);
                });
            });

            $("#sCkAllProvinces").click(function() {
                var state = $(this).attr("checked");
                $(":checkbox[name]='sckbProvince'").each(function() {
                    $(this).attr("checked", state);
                });
            });

            //提交求购
            $("#btnSave").click(function() {
                if ("<%=publishCount %>" < 15) {
                    var sfu1 = ctl00_ctl00_c1_SupplierBody_SingleFileUpload1;
                    var _islogin = "<%= IsLogin %>";
                    if (_islogin == "False") {
                        location.href = "/Register/Login.aspx?returnurl=/SupplierInfo/PublishSupplierInfo.aspx";
                        return false;
                    }
                    else {
                        if (isSubmit) {
                            //如果按钮已经提交过一次验证，则返回执行保存操作
                            __doPostBack('btnSave', '')
                        }
                        var errmsg = "";
                        if ($.trim($("#txtTitle").val()) == "请填写求购信息标题") {
                            errmsg += "请输入求购标题\n";
                        }
                        if ($.trim($("#txtInfo").val()) == "") {
                            errmsg += "请输入求购信息内容\n";
                        }
                        if ($.trim($("#txtInfo").val()).length > 500) {
                            errmsg += "求购信息内容太长！";
                        }
                        if ($.trim($("#<%= txtMQ.ClientID %>").val()) == "") {
                            errmsg += "请输入MQ\n";
                        }
                        if ($.trim(errmsg) != "") {
                            alert(errmsg);
                            return false;
                        }
                        $("#hidReach").val("Reach");
                        $("#hidSupply").val("");
                        var boolSameTime = GetAjaxSameTitle($.trim($("#txtTitle").val()));
                        if (boolSameTime == "false") {
                            if (sfu1.getStats().files_queued <= 0) {
                                doSubmit();
                                return true;
                            }
                            sfu1.customSettings.UploadSucessCallback = doSubmit;
                            sfu1.startUpload();
                            return false;
                        } else {
                            alert("您已经发布过该供求信息，请修改后重新发布！");
                            return false;
                        }
                    }
                }
            });

            //提交供应
            $("#sbtnSave").click(function() {
                if ("<%=publishCount %>" < 15) {
                    var sfu1 = ctl00_ctl00_c1_SupplierBody_SingleFileUpload2;
                    var _islogin = "<%= IsLogin %>";
                    if (_islogin == "False") {
                        location.href = "/Register/Login.aspx?returnurl=/SupplierInfo/PublishSupplierInfo.aspx";
                        return false;
                    }
                    else {
                        if (isSubmit) {
                            //如果按钮已经提交过一次验证，则返回执行保存操作
                            __doPostBack('sbtnSave', '')
                        }
                        var errmsg = "";
                        if ($.trim($("#stxtTitle").val()) == "请填写供应信息标题") {
                            errmsg += "请输入供应标题\n";
                        }
                        if ($.trim($("#stxtInfo").val()) == "") {
                            errmsg += "请输入供应信息内容\n";
                        }
                        if ($.trim($("#stxtInfo").val()).length > 500) {
                            errmsg += "供应信息内容太长！";
                        }
                        if ($.trim($("#<%= stxtMQ.ClientID %>").val()) == "") {
                            errmsg += "请输入MQ\n";
                        }
                        if ($.trim(errmsg) != "") {
                            alert(errmsg);
                            return false;
                        }
                        $("#hidReach").val("");
                        $("#hidSupply").val("Supply");
                        var boolSameTime = GetAjaxSameTitle($.trim($("#stxtTitle").val()));
                        if (boolSameTime == "false") {
                            if (sfu1.getStats().files_queued <= 0) {
                                doSubmit();
                                return true;
                            }

                            sfu1.customSettings.UploadSucessCallback = doSubmit;
                            sfu1.startUpload();
                            return false;
                        } else {
                            alert("您已经发布过该供求信息，请修改后重新发布！");
                            return false;
                        }
                    }
                }
            });
        });

         function GetAjaxSameTitle(title) {
             var m = true;
             $.ajax({
                url: "/SupplierInfo/PublishSupplierInfo.aspx?type=sametitle&titleinfo=" + escape(title),
                 cache: false,
                 async: false,
                 success: function(results) {
                     m = results;
                 }
             });
             return m;
         }
    </script>

</asp:Content>
