<%@ Page AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB.OlServer.Default"
    Language="C#" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="Olserver1" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <script src="<%= JsManage.GetJsFilePath("SOlServerCore") %>" type="text/javascript"></script>
	<script src="<%= JsManage.GetJsFilePath("dateformat") %>" type="text/javascript"></script>

	<script type="text/javascript" src="<%= JsManage.GetJsFilePath("json2.js") %>"></script>

	<style type="text/css">
	    body
	    {
	        color: #333;
	        font-size: 12px;
	        font-family: "宋体" ,Arial, Helvetica, sans-serif;
	        text-align: center;
	        background: #fff;
	        margin: 0px;
	    }
	    ul
	    {
	        list-style: none;
	        margin: 0px;
	        padding: 0px;
	    }
	    li
	    {
	        line-height: 150%;
	        list-style-type: none;
	    }
	    img
	    {
	        border: thin none;
	    }
	    .over
	    {
	        border-bottom: #808080 1px solid;
	        border-left: #ffffff 1px solid;
	        border-right: #808080 1px solid;
	        border-top: #ffffff 1px solid;
	        cursor: pointer;
	    }
	    .out
	    {
	        border-bottom: 0px solid;
	        border-left: 0px solid;
	        border-right: 0px solid;
	        border-top: 0px solid;
	    }
	    .down
	    {
	        border-bottom: #ffffff 1px solid;
	        border-left: #808080 1px solid;
	        border-right: #ffffff 1px solid;
	        border-top: #808080 1px solid;
	        padding-left: 2px;
	        padding-top: 2px;
	    }
	    .up
	    {
	        border-bottom: 0px solid;
	        border-left: 0px solid;
	        border-right: 0px solid;
	        border-top: 0px solid;
	    }
	    .tips
	    {
	        color: green;
	    }
	    .errors
	    {
	        color: red;
	    }
	    .online
	    {
	        color: Blue;
	        font-weight: bold;
	        cursor: pointer;
	    }
	    .notonline
	    {
	        color: #333333;
	        cursor: pointer;
	    }
	    .overselectsetkeytips
	    {
	        background: #d9eefa;
	    }
	    .outselectsetkeytips
	    {
	        background: #fff;
	    }
	</style>
    
    <script type="text/javascript">
        function getOffXY(obj) {
            var offx = 0;
            var offy = 0;
            var ndObject = obj;
            while (ndObject.nodeName != "BODY") {
                offx += ndObject.offsetLeft;
                offy += ndObject.offsetTop;
                ndObject = ndObject.offsetParent;
            }
            var xyobj = { "x": offx, "y": offy };
            return xyobj;
        }
        function setKey() {
            if ($("#setkeytips").get(0).style.display == 'none') {
                var xyObj = getOffXY($("#btnsetkey").get(0));
                var offsetX = xyObj.x;
                var offsetY = xyObj.y;
                $("#setkeytips").css("top", (offsetY + 75) + "px").css("left", (offsetX + 10) + "px");
                $("#setkeytips").show();
            } else {
                $("#setkeytips").hide();
            }
        }
        function setkeydown(v) {
            $(document).unbind("keydown");
            switch (v) {
                case 1:
                    $(document).keydown(function(event) {
                        if (event.ctrlKey && event.keyCode == 13) { OLS.sendMessage(); }
                    });
                    $("#keytips").html("按Ctrl+Enter键发送消息");
                    break;
                case 2:
                    $(document).keydown(function(event) {
                        if (event.keyCode == 13 && !event.ctrlKey) { OLS.sendMessage(); }
                    });
                    $("#keytips").html("按Enter键发送消息");
                    break;
                case 3:
                    $(document).keydown(function(event) {
                        if ((event.ctrlKey && event.keyCode == 13) || (event.keyCode == 13 && !event.ctrlKey)) { OLS.sendMessage(); }
                    });
                    $("#keytips").html("按Ctrl+Enter或Enter键均可发送消息");
                    break;
            }
            $("#setkeytips").hide();
        }

        $(document).ready(function() {
            $("#btnsetkey").click(setKey);
            $("#setkeytips li").mouseover(function() { $(this).attr("class", "overselectsetkeytips"); });
            $("#setkeytips li").mouseout(function() { $(this).attr("class", "outselectsetkeytips"); });
        });
    </script>
    
    <div style="width: 695px; margin: 0px auto;">
		<table width="695" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td valign="top" style="border: 1px solid #6F90CA">
					<table width="695" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td style="border: 1px solid #fff; background: url(<%= Domain.ServerComponents %>/images/im/hangbg.gif) repeat-x; height: 24px; padding-left: 5px; padding-top: 3px; color: #FFFFFF; font-weight: bold; font-size: 14px; text-align: left;">
								<asp:Label runat="server" id="lbCompanyName"></asp:Label>竭诚为您服务
							</td>
						</tr>
					</table>
					<table width="695" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td style="border-top: 1px solid #6F90CA">
								<table width="695" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td valign="top" style="border: 1px solid #fff; background: url(<%= Domain.ServerComponents %>/images/im/bg.gif) repeat-x; height: 433px;">
											<table border="0" cellpadding="0" cellspacing="0" style="width: 680px; margin: 0px auto; margin-top: 6px;">
												<tr>
													<td width="494" valign="top">
														<table width="494" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="background: url(<%= Domain.ServerComponents %>/images/im/erhangbg.gif) repeat-x; height: 26px; border: 1px solid #6F90CA; text-align: left;">
																	<div style="float: left; padding-top: 4px; padding-left: 5px;" id="olmessagestitle">
																		您当前没有任何会话
																	</div>
																	<div style="float: right; padding-right: 5px;">
																		<input type="button" id="btnexit" style="background: url(<%= Domain.ServerComponents %>/images/im/duihuajieshu.gif); width: 66px; height: 21px; border: 0px;cursor:pointer;" />
																	</div>
																</td>
															</tr>
														</table>
														<table width="494" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="border-bottom: 1px solid #6F90CA; border-left: 1px solid #6F90CA; border-right: 1px solid #6F90CA; background: #FFFFFF;">
																	<table border="0" cellpadding="0" cellspacing="0" style="height: 240px; width: 492px">
																		<tr>
																			<td valign="top" style="border: 3px solid #E3EBF8; line-height: 24px; text-align: left;">
																				<!--<div style="color: #333; border: 1px solid #8bad76; background: #fffdd2; width: 217px;
                                                                                    margin: 0px auto; overflow: hidden; position: absolute" id="loadingtip">
                                                                                    <div style="float: left; width: 27px; margin-top: 4px; text-align: center">
                                                                                        <img alt="" src="<%= Domain.ServerComponents %>/images/im/loading.gif" /></div>
                                                                                    <div style="float: left; width: 190px;">
                                                                                        正在加载.请稍候...</div>
                                                                                </div>-->
																				<div style="height: 240px; width: 470px; overflow: auto; margin: 5px" id="olmessages">
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
															<tr>
																<td>
																	<table width="494" border="0" cellspacing="0" cellpadding="0">
																		<tr>
																			<td width="109" onmouseup="this.className='up'" onmousedown="this.className='down'" onmouseover="this.className='over'" onmouseout="this.className='out'" style="text-align: left">
																				<img src="<%= Domain.ServerComponents %>/images/im/biaoqing_11.gif" width="102" height="18" style="vertical-align: top; margin: 0px auto" id="btnsavemessages" />
																			</td>
																			<td align="right">
																				<span style="color:#666" id="keytips">按Ctrl+Enter键发送消息</span>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table width="494" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="border: 1px solid #6F90CA; background: #FFFFFF;">
																	<table width="492" height="83" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td valign="top" style="border: 3px solid #E3EBF8; line-height: 24px; padding: 5px; text-align: left;">
																				<table width="100%" border="0" cellspacing="0" cellpadding="0">
																					<tr>
																						<td width="70%">
																							<textarea name="txtmessage" id="txtmessage" style="width: 345px; height: 75px; overflow: hidden; border: 1px solid #FFFFFF;"></textarea>
																						</td>
																						<td width="100" onmouseup="this.className='up'" onmousedown="this.className='down'" onmouseover="this.className='over'" onmouseout="this.className='out'">
																							<img src="<%= Domain.ServerComponents %>/images/im/send.jpg" style="width:91px;height:70px; border:0px" id="btnsend" alt="发送消息" />
																						</td>
																						<td width="30" onmouseup="this.className='up'" onmousedown="this.className='down'" onmouseover="this.className='over'" onmouseout="this.className='out'">
																						    <img src="<%= Domain.ServerComponents %>/images/im/setkey.jpg" style="width:23px;height:70px; border:0px" id="btnsetkey" alt="设置发送消息快捷键" />
																						    <ul style="position:absolute;width:150px;display:none; line-height:24px; border:1px solid #E3EBF8; background:#fff; color:#666666" id="setkeytips"><li onclick="setkeydown(1)">&nbsp;按Ctrl+Enter键发送消息</li><li onclick="setkeydown(2)">&nbsp;按Enter键发送消息</li> <li onclick="setkeydown(3)">&nbsp;两者均可发送消息</li></ul>
																						</td>
																					</tr>
																				</table>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
													</td>
													<td width="7">
													</td>
													<td width="179" valign="top">
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="background: url(<%= Domain.ServerComponents %>/images/im/erhangbg.gif) repeat-x; height: 26px; border: 1px solid #6F90CA; text-align: center;">
																	<span style="color: #1f4a9c; font-weight: bold" id="oluserstitle"><asp:Label runat="server" id="lbCompanyName1"></asp:Label></span>
																</td>
															</tr>
														</table>
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="border-bottom: 1px solid #6F90CA; border-left: 1px solid #6F90CA; border-right: 1px solid #6F90CA; background: #FFFFFF;">
																	<table width="100%" height="240" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td valign="top" style="border: 3px solid #E3EBF8; line-height: 24px; padding: 5px; text-align: left;">
																				<div style="height: 240px; width: 100%; overflow: visible; margin: 0px auto">
																					<ul id="olusers" style="width: 100%;">
																					</ul>
																				</div>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
														<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 7px;">
															<tr>
																<td style="background: url(<%= Domain.ServerComponents %>/images/im/welcomebg.gif) repeat-x; height: 17px; border: 1px solid #6F90CA; text-align: left;">
																	<img src="<%= Domain.ServerComponents %>/images/im/welcome.gif" width="74" height="17" style="vertical-align: top" />
																</td>
															</tr>
														</table>
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td style="border-bottom: 1px solid #6F90CA; border-left: 1px solid #6F90CA; border-right: 1px solid #6F90CA; background: #FFFFFF;">
																	<table width="100%" height="100" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td valign="top" style="border: 1px solid #E3EBF8; line-height: 24px; text-align: left;">
																				<img src="<%= CompanyLogPath %>" width="175px" height="98px" style="vertical-align: top" />
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
</asp:content>
