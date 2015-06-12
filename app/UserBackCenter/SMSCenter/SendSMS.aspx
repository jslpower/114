<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="UserBackCenter.SMSCenter.SendSMS" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="SmsHeaderMenu"
    TagPrefix="uc1" %>
<asp:content id="SendSMS" runat="server" contentplaceholderid="ContentPlaceHolder1">

    <uc1:SmsHeaderMenu ID="SmsHeaderMenu1" runat="server" TabIndex="tab1" />
<div class="left">
        <input type="hidden" name="hidContentID" id="hidContentID" />
        <input type="hidden" name="ContentList" id="ContentList" />
        <%--<input type="hidden" name="hidCustomerID" id="hidCustomerID">
        <input type="hidden" name="CustomerIdList" id="CustomerIdList">--%>
        
        <%--<input type="hidden" id="txtSMSUnEncryptCustomerId" />存放选中客户的编号
        <input type="hidden" id="txtSMSUnEncryptCustomerMobile" />存放选中客户的号码
        <input type="hidden" id="txtSMSEncryptCustomerId" />存放选中平台客户的编号
        <input type="hidden" id="txtSMSEncryptCustomerMobile" />存放选中平台客户的号码
        
        <input type="hidden" id="txtEncryptMobiles" />存放选中要发送的平台客户的号码--%>
       
        <table align="left" width="100%" border="0" cellspacing="0" cellpadding="4" class="mobilebox"
            style="width: 75%; margin: 10px;">
            <tr>
                <td width="114" align="right">
                    <strong><span class="style2" style="color: #ff0000">*</span>编辑发送内容：</strong>
                </td>
                <td width="584" align="left">短信内容：当前短信共有 <span id="Content_Count">0</span> 字。移动分为 <span id="factCountChinaMobile">0</span> 条发送，联通分为 <span id="factCountChinaUnicom">0</span> 条发送，小灵通分为 <span id="factCountChinaTelecom">0</span> 条发送<br>
                    <asp:TextBox ID="txt_SendContent" EnableViewState="True"
                        runat="server" TextMode="MultiLine" Width="462px" Columns="55" Rows="5" Height="83px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">                    
                </td>
                <td style="text-align:left">
                    <span style="color: #ff0000;">*注:移动、联通短信内容不能超过70字!小灵通短信内容不超过45个字</span> <br />           
                    <a href="javascript:SendSMS.openDialog('/SMSCenter/ImportCommonLanguage.aspx','常用语列表');">
                        <img src="<%=ImageServerUrl %>/images/open.gif" /><strong>自动填写发送内容</strong></a>
                    <br />
                    当前手机号共有<span id="Phone_All_Count" style="color:Red; font-size:15px; font-weight:bolder">0</span>个，其中从同业114导入共享号码<span id="Phone_114_Count" style="color:Red; font-size:15px; font-weight:bolder">0</span>个，手动输入号码<span style="color:Red; font-size:15px; font-weight:bolder" id="Phone_Txt_Count">0</span>个。<br />输入号码时在号码与号码之间必须用，号隔开<br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <strong><span class="style2" style="color: #ff0000">*</span>手机号码：</strong>
                </td>
                <td align="left">                    
                    <asp:TextBox  EnableViewState="True" ID="txt_TelePhoneGroup"
                        runat="server" TextMode="MultiLine" Width="459px" Columns="55" Rows="20" Height="98px"></asp:TextBox>
                    <br />
                    <img src="<%=ImageServerUrl %>/images/ax_goforward.gif" width="18" height="18" style="margin-bottom: -3px;" />
                    <a href="javascript:SendSMS.openDialog('/SMSCenter/ImportCustomerList.aspx','客户列表');">导入同业114平台组团社手机号码（现有<%=((Show114PhoneCount + 20000.0)/10000.0).ToString("0.00") %>万个）</a><br />
                    <a href="javascript:SendSMS.openDialog('/SMSCenter/ImportNumFromFile.aspx?typeID=0','从文件导入');"><img src="<%=ImageServerUrl %>/images/ax_goforward.gif" width="18" height="18" style="margin-bottom: -3px;" />导入自有客户手机号码</a>
                    <span id="file_view" style="font-weight: bold; height: 25px; text-decoration: underline;
                        float: left; margin-left: 10px;"></span>
                    <br />
                    （只能导入格式为.xls和.txt的文件，txt文件必须一个号码一行）<br />
                    <a href="<%=Domain.ServerComponents %>/SMSmodel/短信模板.xls" target="_blank"><span style="color: #ff0000;">文件模板下载</span></a>
                </td>
            </tr>
            
            <tr style="display:none">
                <td style="text-align:right; line-height:22px;vertical-align:top"><b>平台共享号码：</b></td>
                <td id="tdShareMobile" style="text-align:left; line-height:22px;vertical-align:top"></td>
            </tr>
            
            <tr>
                <td align="right">
                    <strong>发送方式：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddl_SendType" runat="server" Width="73px">
                        <asp:ListItem Value="0">直接发送</asp:ListItem>
                        <asp:ListItem Value="1">定时发送</asp:ListItem>
                    </asp:DropDownList>
                    定时短信不能取消<span id="spTime"></span>
                      <input size="16" type="text" name="txt_SendSMS_OrderTime" id="txt_SendSMS_OrderTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                </td>
            </tr>
            <tr>
                 <td style="text-align:right">
                    <strong>选择发送通道：</strong>
                </td>
                <td style="text-align:left">
                    <asp:DropDownList runat="server" id="ddlSendChannel">
                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input type="checkbox" runat="server" id="ShowSender" /><strong>发信人：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_SendPeople" runat="server"></asp:TextBox>
                    &nbsp;打勾后短信内容将包含发信人信息（发信人要占用内容字数）
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    你在使用短信业务时，已默认同意<a href="/SMSCenter/ShortMessageAgreement.html" target="_blank">短信服务协议</a>
                    <br />
                    <asp:Button ID="btn_Send" runat="server" Text="提交并发送短信" CssClass="baocunan_an" Style="width: 150px;
                        height: 40px; font-size: 18px; font-weight: bold" ></asp:Button>
                    <br />
                    <asp:Label runat="server" ID="lab_NoMoney" ForeColor="Red" Visible="false" Text="您的剩余短信条数为0！"></asp:Label><br />
                    <div id="div_pay" style="width:143px;" runat="server" >
                        <img alt="立即充值" id="imb_pay" width="143" height="41" border="0" src='<%=ImageServerUrl + "/images/result.gif" %>' /></div>
                </td>
            </tr>
        </table>
        <div class="jiange">
        </div>
    </div>
    <div style="clear: both">
    </div>
    
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Smskeys") %>"></script>
<script type="text/javascript" language="javascript">
    //--------------debug
    //var d1 = new Date();
    
    //jQuery data
    var $data = {
        elements: { txtMobile: "<%=txt_TelePhoneGroup.ClientID %>", htmlEncryptMobile: "tdShareMobile" },
        sendAll: { isSend: false, dataSource: 2, companyName: "", customerType: 0, provinceId: 0, cityId: 0, phoneCount: 0 },   //dataSource(0:所有客户  1:我的客户  2:平台组团社客户)  CustomerType客户类型、对应公司类型的枚举int值,  phoneCount要发送的号码个数
        //设置勾选的客户数据 data =[{ mobile: '', customerId: '', isEncrypt: '',encryptMobile:'' }]
        setSelectMobiles: function(data) {
            $("div").data("SelectMobiles", data);
        },
        //获取勾选的客户数据 undefined为未勾选任何客户
        getSelectMobiles: function() {
            if ($("div").data("SelectMobiles") == "undefined" || $("div").data("SelectMobiles") == undefined)
                return []
            else
                return $("div").data("SelectMobiles");
        },
        //设置确定选中的客户数据 data =[{ mobile: '', customerId: '', isEncrypt: '',encryptMobile:'' }]
        setConfirmedMobiles: function(data) {
            var encryptMobiles = [];
            var s = [];
            //alert($("#" + this.elements.txtMobile).val() + "前"); 
            var strMobileHtml = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].isEncrypt == "1") {
                    encryptMobiles.push(data[i].mobile);
                    s.push(data[i].encryptMobile);
                } else {
                    strMobileHtml += "," + data[i].mobile; //alert(data[i].mobile);
                }
            }

            $("#" + this.elements.txtMobile).val($("#" + this.elements.txtMobile).val() + strMobileHtml);
            //alert($("#" + this.elements.txtMobile).val() + "后"); 

            var encryptMobileHtml = "";
            if (s.length == 1) {
                encryptMobileHtml = s[0];
            } else {
                for (var i = 0; i < s.length; i++) {
                    encryptMobileHtml += s[i];

                    if (i != s.length - 1) { encryptMobileHtml += ","; }

                    if (i != 0 && i % 7 == 0) { encryptMobileHtml += "<br/>"; }
                }
            }

            $("#" + this.elements.htmlEncryptMobile).html(encryptMobileHtml);
            $("div").data("EncryConfirmedMobiles", encryptMobiles);
        },
        //获取确定选中的显示时要加密的客户数据 undefined为未确定选中任何客户
        getEncryConfirmedMobiles: function() {
            if ($("div").data("EncryConfirmedMobiles") == undefined || $("div").data("EncryConfirmedMobiles") == "undefined")
                return []
            else
                return $("div").data("EncryConfirmedMobiles");
        },
        //移除数据
        removeData: function() {
            $("div").removeData("SelectMobiles");
            $("div").removeData("EncryConfirmedMobiles");
        }
    };

    var SendSMS = {
        //新弹窗
        openDialog: function(linkUrl, title) {
            Boxy.iframeDialog({ title: title, iframeUrl: linkUrl, width: "840px", height: "480", draggable: true, data: null });
        },
        ChangeTimeShow: function() {
            var DDL_SendType_Index = $("#<%=ddl_SendType.ClientID %>").val();
            if (DDL_SendType_Index == "1") {//显示
                $("#spTime").show();
                $("#spTime").html("，<span class='style2' style='color: red'>*</span>请选择发送时间：");
                $("#txt_SendSMS_OrderTime").show();
            } else {//隐藏
                $("#spTime").hide();
                $("#txt_SendSMS_OrderTime").hide();
            }
        },
        GetContentNum: function() {   //计算短信字数
            var Content = $.trim($("#<%=txt_SendContent.ClientID %>").val());
            if (Content.length == 0) {
                $("#Content_Count").html("0");
            }
            else {
                $("#Content_Count").html(Content.length);
            }
        },
        //计算手动输入的手机号码个数
        GetPhoneNum: function() {
            var PhoneCount = 0;
            var txtPhone = $.trim($("#<%=txt_TelePhoneGroup.ClientID %>").val());
            var reg = /[,，]/;
            var txtPhoneArr = txtPhone.split(reg);
            for (var i = 0; i < txtPhoneArr.length; i++) {
                if (this.IsPhoneFormat(txtPhoneArr[i]))
                    PhoneCount++;
            }

            $("#Phone_Txt_Count").html(PhoneCount);
            $("#Phone_All_Count").html(PhoneCount + parseInt($("#Phone_114_Count").html()));
        },
        //计算114手机号码个数
        Get114PhoneNum: function() {
            var EncryMobile = $data.getEncryConfirmedMobiles(); //平台共享客户手机号码
            var Phone114Count = 0;  //同业114共享号码个数

            for (var i = 0; i < EncryMobile.length; i++) {
                if (this.IsPhoneFormat(EncryMobile[i]))
                    Phone114Count++;
            }

            //同业114共享号码个数
            $("#Phone_114_Count").html(Phone114Count);
            $("#Phone_All_Count").html(Phone114Count + parseInt($("#Phone_Txt_Count").html()));
        },
        //序列化发送所有
        serializeSendAll: function() {
            var msg = "&isSendAll=" + $data.sendAll.isSend;
            if ($data.sendAll.isSend)
                msg += "&sendAll.dataSource=" + $data.sendAll.dataSource + "&sendAll.companyName=" + $data.sendAll.companyName + "&sendAll.customerType=" + $data.sendAll.customerType + "&sendAll.provinceId=" + $data.sendAll.provinceId + "&sendAll.cityId=" + $data.sendAll.cityId;
            return msg;
        },
        sendMessage: function() {
            //----------debug
            //d1 = new Date();
            var self = this;
            self.SetSendButtonTip(2);
            var data = $("#<%=btn_Send.ClientID %>").closest("form").serialize();
            if ($data.getEncryConfirmedMobiles() != null && $data.getEncryConfirmedMobiles().length > 0)
                data += "&EncryMobile=" + $data.getEncryConfirmedMobiles().join(',');
            //追加是否发送所有
            data += this.serializeSendAll();

            $.newAjax({
                type: "post",
                url: "/SMSCenter/SendSMS.aspx?btn_Submit=1",
                data: data,
                success: function(datas) {   //返回结果代码  0:失败  1:成功   2:余额不足
                    returnVal = datas.split('@');
                    $("#<%=div_pay.ClientID %>").hide();

                    //--------------debug
                    //var d2 = new Date();
                    //alert("发送开始时间：" + d1.getHours() + ":" + d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + "\r\n" + "发送结束时间：" + d2.getHours() + ":" + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());
                    self.SetSendButtonTip(0);
                    switch (returnVal[0]) {
                        case "0":
                            alert(returnVal[1]);
                            return false;
                            break;
                        case "1":
                            alert(returnVal[1]);
                            topTab.url(topTab.activeTabIndex, '/SMSCenter/SendSMS.aspx');
                            break;
                        case "2":
                            alert(returnVal[1]);
                            $("#<%=div_pay.ClientID %>").show();
                            return false;
                            break;
                    }
                },
                error: function() {
                    self.SetSendButtonTip(0);
                    alert('服务器繁忙!请稍候再进行此操作!');
                    return false;
                }
            });
        },
        //验证号码格式是否正确
        IsPhoneFormat: function(phone) {
            var isTrue = false;
            if (phone == null || phone == "")
                return isTrue;

            var result_mobile = null;  //验证是否为手机
            var result_phs = null;  //验证是否为小灵通
            result_mobile = phone.match(RegExps.isMobile);
            if (result_mobile != null)  //为正确的
                isTrue = true;
            else {//非手机的时候验证是否为小灵通
                result_phs = phone.match(RegExps.isPhone);
                if (result_phs != null)
                    isTrue = true;
            }

            return isTrue;
        },
        //设置发送按钮的提示信息
        SetSendButtonTip: function(type) {  //type=1  提交短信验证信息  type=2  提交短信发送  type=0恢复发送状态
            var obj = $("#<%= btn_Send.ClientID %>");
            switch (type) {
                case 0:
                    obj.val("提交并发送短信");
                    obj.attr("disabled", "");
                    break;
                case 1:
                    obj.val("正在提交...");
                    obj.attr("disabled", "true");
                    break;
                case 2:
                    obj.val("正在发送短信...");
                    obj.attr("disabled", "true");
                    break;
            }
        },
        CheckTheSMSSend: function() {
            //--------------debug
            //d1 = new Date();

            var self = this;
            self.SetSendButtonTip(1);
            //验证短信发送
            var txtPhoneArr = new Array(); //存储手动输入的号码数组        
            var errorPhoneArr = new Array();   //存储格式不正确的手机号码数组
            var repeatPhoneArr = new Array();   //存储重复的号码数组
            var jsCheckPhoneMax = 10;  //js验证手机号码的最大个数

            if ($.trim($("#<%=txt_SendContent.ClientID %>").val()) == "") {
                alert('请输入或导入发送内容!');
                self.SetSendButtonTip(0);
                return false;
            }
            if($("#<%= ShowSender.ClientID %>").attr("checked") && $.trim($("#<%= txt_SendPeople.ClientID %>").val())=="")
            {
                    alert('请输入发信人!');
                    self.SetSendButtonTip(0);
                    return false;
            }
            var DDL_SendType_Index = $("#<%=ddl_SendType.ClientID %>").val();
            if (DDL_SendType_Index == "1") {//显示  
                var sendTimeNow = this.GetNowTime();
                if ($("#txt_SendSMS_OrderTime ").val() == "") {
                    alert('请选择发送时间!');
                    self.SetSendButtonTip(0);
                    return false;
                }
                if (Date.parse($("#txt_SendSMS_OrderTime").val().replace("-", "/")) < Date.parse(sendTimeNow.replace("-", "/"))) {
                    alert('定时发送的时间不能小于当前时间!');
                    self.SetSendButtonTip(0);
                    return false;
                }
            }
            var txtPhone = $.trim($("#<%=txt_TelePhoneGroup.ClientID %>").val());
            var EncryMobile = $data.getEncryConfirmedMobiles(); //平台共享客户手机号码

            if (txtPhone == "" && (EncryMobile == null || EncryMobile.length <= 0) && $data.sendAll.phoneCount == 0) {
                alert('请输入或导入号码!');
                self.SetSendButtonTip(0);
                return false;
            }

            //验证发送内容中是否包含禁止发送的关键字
            var errorMessageList = "";
            var sendContentInfo = $.trim($("#<%=txt_SendContent.ClientID %>").val()) + $("#<%=txt_SendPeople.ClientID %>").val();
            $.each(SmsDisabledKeys, function(i, n) {
                if (sendContentInfo.indexOf(n) != -1) {
                    errorMessageList += n + ",";
                }
            });
            if (errorMessageList != "") {
                errorMessageList = errorMessageList.substring(0, errorMessageList.length - 1);
                alert("您要发送的短信内容中包含" + errorMessageList + " 这些禁止发送的关键字，请重新编辑！");
                self.SetSendButtonTip(0);
                return false;
            }

            var reg = /[,，]/;

            if ($.trim(txtPhone) != "")
                txtPhoneArr = txtPhone.split(reg);

            //$.grep(txtPhoneArr,function(v,i){})

            var telNum = txtPhoneArr.length;  //号码个数
            var factNum = []; //存放正确的号码集合
            var factEncryMobile = new Array();  //存放正确的要加密号码集合

            if (telNum > 0 && telNum <= jsCheckPhoneMax) {
                for (var i = 0; i < telNum; i++) {
                    if (this.IsPhoneFormat(txtPhoneArr[i])) { //正确手机号码
                        //判断当前手机号码是否已经存在
                        var isExists = false;
                        for (var j = 0; j < factNum.length; j++) {
                            if (factNum[j] == txtPhoneArr[i]) {
                                repeatPhoneArr.push(txtPhoneArr[i]);
                                isExists = true;
                                break;
                            }
                        }

                        if (!isExists)
                            factNum.push(txtPhoneArr[i]);
                    }
                    else {
                        errorPhoneArr.push(txtPhoneArr[i]);
                    }
                }
            }

            if (errorPhoneArr.length > 0) {
                if (confirm(errorPhoneArr.toString() + '以上号码格式不正确,是否自动取消无效的号码！')) {
                    $("#<%=txt_TelePhoneGroup.ClientID %>").val(factNum.join(","));
                }
                self.SetSendButtonTip(0);
                return false;
            }
            if (repeatPhoneArr.length > 0) {
                if (!confirm("有" + repeatPhoneArr.length + "个重复号码" + repeatPhoneArr.toString() + "，是否自动删除重复号码后继续发送?")) {
                    self.SetSendButtonTip(0);
                    return false;
                }
                else {
                    $("#<%=txt_TelePhoneGroup.ClientID %>").val(factNum.join(","));
                }
            }

            var returnVal = new Array();
            var data = $("#<%=btn_Send.ClientID %>").closest("form").serialize();
            //追加上要发送的平台号码
            if ($data.getEncryConfirmedMobiles() != null && $data.getEncryConfirmedMobiles().length > 0)
                data += "&EncryMobile=" + $data.getEncryConfirmedMobiles().join(',');

            //追加是否发送所有
            data += this.serializeSendAll();

            //return false;
            $.newAjax({
                url: "/SMSCenter/SendSMS.aspx?btn_Submit=0",
                type: "post",
                cache: false,
                data: data,
                success: function(result) {  //返回结果代码  0:失败  1:成功   2:余额不足
                    //---------debug
                    //var d2 = new Date();
                    //alert("开始提交时间：" + d1.getHours() + ":" + d1.getMinutes() + ":" + d1.getSeconds() + "." + d1.getMilliseconds() + "\r\n" + "验证结束时间：" + d2.getHours() + ":" + d2.getMinutes() + ":" + d2.getSeconds() + "." + d2.getMilliseconds());
                    returnVal = result.split('@');
                    $("#<%=div_pay.ClientID %>").hide();
                    switch (returnVal[0]) {
                        case "0":
                            alert(returnVal[1]);
                            self.SetSendButtonTip(0);
                            return false;
                            break;
                        case "1":
                            if (!confirm(returnVal[1])) {
                                self.SetSendButtonTip(0);
                                return false;
                            }
                            self.sendMessage();
                            break;
                        case "2":
                            $("#<%=div_pay.ClientID %>").show();
                            alert(returnVal[1]);
                            self.SetSendButtonTip(0);
                            return false;
                            break;
                    }
                },
                error: function() {
                    alert("请求时发生错误!");
                    self.SetSendButtonTip(0);
                    return false;
                }
            });

            return false;
        },
        GetNowTime: function() {
            //获取当前日期和时间
            var now = new Date();
            var year = now.getYear();
            var month = now.getMonth() + 1;
            var day = now.getDate();
            var hours = now.getHours();
            var minutes = now.getMinutes();

            return year + "-" + month + "-" + day + " " + hours + ":" + minutes;
        },
        //短信发送通道
        channel: [],
        //获取发送通道信息
        getChannel: function(channelIndex) {
            var cdata = {};
            $.each(this.channel, function(i, data) {
                if (data.index == channelIndex) {
                    cdata = data;
                    return false;
                }
            })
            return cdata;
        },
        //获取短信发送内容针对不同接入商的发送条数统计信息 contactLength:短信内容长度 channelIndex:发送通道
        getFactCount: function(contactLength, channelIndex) {
            var channelData = this.getChannel(channelIndex);
            //每条短信发送字符长度
            var singleFactLength = { mobile: 70, telecom: 45 };
            //实际发送短信条数
            var factCount = { mobile: 0, telecom: 0 };
            //长短信每条短信发送字符长度
            if (channelData.isLong) { singleFactLength.mobile = singleFactLength.telecom = 210; }
            //实际发送短信条数统计
            if (contactLength > 0) {
                factCount.mobile = Math.ceil(contactLength / singleFactLength.mobile);
                factCount.telecom = Math.ceil(contactLength / singleFactLength.telecom);
            }

            return factCount;
        },
        //设置短信发送内容针对不同接入商的发送条数统计信息
        setFactCountHtml: function() {
            var contentLength = $.trim($("#<%=txt_SendContent.ClientID %>").val()).length;
            var channelIndex = $("#<%=ddlSendChannel.ClientID%>").val();

            var factCount = this.getFactCount(contentLength, channelIndex);

            $("#factCountChinaMobile").html(factCount.mobile.toString());
            $("#factCountChinaUnicom").html(factCount.mobile.toString());
            $("#factCountChinaTelecom").html(factCount.telecom.toString());
        }
    };

    $(document).ready(function() {
        $("#txt_SendSMS_OrderTime ").hide();
        $("#<%=txt_TelePhoneGroup.ClientID %>").bind("keyup", function() { SendSMS.GetPhoneNum(); });
        $("#<%=txt_SendContent.ClientID %>").bind("keyup", function() { SendSMS.GetContentNum(); SendSMS.setFactCountHtml(); });
        $("#imb_pay").click(function() { topTab.open("/SMSCenter/PayMoney.aspx", "帐户充值", { isRefresh: false }); return false; });
        $("#<%=ddlSendChannel.ClientID %>").bind("change", function() { SendSMS.setFactCountHtml(); });
        $("#<%= txt_SendPeople.ClientID %>").attr("disabled", "disabled");
        $("#<%= ShowSender.ClientID %>").click(function() {
            if ($(this).attr("checked"))
                $("#<%= txt_SendPeople.ClientID %>").removeAttr("disabled");
            else
                $("#<%= txt_SendPeople.ClientID %>").attr("disabled", "disabled");
        });
        $data.removeData();
        /*计算短信字数开始*/
        $("#<%=this.txt_SendContent.ClientID%>").change(function() {
            SendSMS.GetContentNum();
        });
        SendSMS.GetContentNum();
        SendSMS.setFactCountHtml();
        /*计算短信字数结束*/
    });
</script>
</asp:content>
