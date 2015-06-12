<%@ Page  Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="TicketBook.aspx.cs" Inherits="UserPublicCenter.AirTickets.TeamBook.TicketBook" %>
<%@ Register Src="~/AirTickets/TeamBook/TicketTopMenu.ascx" TagName="TopMenu" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
<link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
<link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
 <link href="<%=CssManage.GetCssFilePath("517autocomplete") %>" rel="stylesheet" type="text/css" />
 <script type="text/javascript">
     //切换选项卡
     function setTab(name, cursel, n) {
         for (i = 1; i <= n; i++) {
             var menu = document.getElementById(name + i);
             var con = document.getElementById("con_" + name + "_" + i);
             menu.className = i == cursel ? "book_default" : "";
             con.style.display = i == cursel ? "block" : "none";
         }
     }
 </script>
<myASP:TopMenu id="ts_ucTopMenu" runat="server" TabIndex="tab3"></myASP:TopMenu>
<div class="sidebar02_con">

<div id="tb_outMess" title="432432" style="position:absolute; width:150px; display:none; color:#fff;border:1px solid #777; border-left:3px solid #777; border-bottom:3px solid #777; background:red;">中文不超过13个字，英文不超过27个字符</div>
   	  <div class="sidebar02_con_table01">
   		    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FAFAFA" style="border:1px #CCCCCC solid;">
            <tr>
              <td height="5" colspan="5" align="left"></td>
            </tr>
            <tr>
              <td height="30" colspan="5" align="left"><b>航班类型：</b>
                  <input name="radVoyageType" type="radio" id="radVType1"  value="1" checked="checked"  onclick="TicketBook.isShowEndDate();"/>
                  <strong>单程</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <input name="radVoyageType" type="radio" id="radVType2" value="2"  onclick="TicketBook.isShowEndDate();"/>
                  <strong>往返-联程</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </td>
            </tr>
            <tr>
              <td width="9%" height="30" align="left"><b>出发地</b></td>
              <td width="22%" align="left">
                 <select id="ts_selFromCity" onchange="return TicketBook.changeSeattle(this);">
                      <option value="0">请选择城市</option>
                    </select>
             <%-- <input type="text" id="txtFromCity" value="<%=startCityName %>"/><input type="hidden" id="txtFromCityLKE"  value="<%=startCity %>"/>--%>
                 <%-- <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" id="fromCity"/>--%></td>
              <td width="9%" align="right"><b>目的地</b></td>
              <td colspan="2" align="left">
            
                    <select id="ts_selToCity" >
                      <option value="0">请选择城市</option>
                    </select>
             <%-- <input type="text"  id="txtToCity" value="<%=toCityName %>"/><input type="hidden" id="txtToCityLKE"  value="<%=toCity %>"/>--%>
                 <%-- <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" id="toCity" />--%></td>
            </tr>
            <tr>
              <td height="30" align="left"><b>出发时间</b></td>
              <td align="left"><input type="text"  id="tb_txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" value="<%=startDate %>"/>
                  <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" onclick="javascript:$('#tb_txtStartDate').focus();"/></td>
              <td align="right"><b  style="display:none;">返回时间</b></td>
              <td width="24%" align="left"><input type="text" id="tb_txtBackDate" style="display:none;" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" value="<%=backDate %>"/>
                  <img style="position:relative; left:-24px; display:none; top:3px; *top:1px;" src="<%=ImageServerUrl%>/images/jipiao/time.gif" width="16" height="13" onclick="javascript:$('#tb_txtBackDate').focus();" /></td>
              <td width="36%" align="left"><img src="<%=ImageServerUrl%>/images/jipiao/btn.jpg" alt="查询" onclick="return TicketBook.search();" style="cursor:pointer;" /></td>
            </tr>
            <tr>
              <td colspan="5">&nbsp;</td>
            </tr>
          </table>
             <div id="divHotCitys">
                    <ul id="ulHotCitys">
                    </ul>
             </div>
      </div>
      
      <div class="sidebar02_con_table02">
        <div class="sidebar02_con_table02">
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr align="left" bgcolor="#E0F4FD">
              <td height="30" bgcolor="#E0F4FD"><span class="title">航班信息</span></td>
            </tr>
            <tr>
              <td height="35"><table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                  <tr>
                    <td><table width="100%" border="0" cellspacing="0" cellpadding="0" class="search_results">
                        <tr>
                          <th width="19%" align="center"><span style="font-size:14px;"><%=info.FlightName%></span></th>
                          <td width="8%" height="25" align="center">去程：</td>
                          <td width="27%" align="left"><%=info.NoGadHomeCityIdName %>--&gt;<%=info.NoGadDestCityName %></td>
                          <td width="16%" align="left">出发时间：<%=startDate %></td>
                          <td width="30%" align="left">旅客类型：<%=peopleType %></td>
                        </tr>
                        <%if (info.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                          { %>
                        <tr>
                          <td width="19%" align="center">航班类型：<%=info.FreightType%> </td>
                          <td height="25" align="center">回程：</td>
                          <td align="left"><%=info.NoGadDestCityName%>--&gt;<%=info.NoGadHomeCityIdName%></td>
                          <td align="left">返回时间：<%=backDate%></td>
                          <td align="left">旅客类型：<%=peopleType%></td>
                        </tr><%} %>
                    </table></td>
                  </tr>
                  <tr>
                    <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td height="30" align="center"><table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                              <tr bgcolor="#EDF8FC">
                                <th width="12%" height="30" align="center">订单号</th>
                                <th width="12%" align="center">PNR</th>
                                <th width="12%" align="center">更换PNR</th>
                                <th width="12%" align="center">代理商名称</th>
                                <th width="12%" align="center">类型</th>
                              </tr>
                              <tr>
                                <td height="25" align="center"></td>
                                <td align="center"></td>
                                <td align="center"></td>
                              
                                <td align="center"><%=info.Company.CompanyName%></td>
                                <td align="center"><%=info.ProductType %></td>
                              </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td height="30" align="center"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
                              <tr bgcolor="#EDF8FC">
                                <th width="5%" height="30" align="center">&nbsp;</th>
                                <th width="10%" align="center">面价</th>
                                <th width="10%" align="center">参考扣率</th>
                                <th width="14%" align="center">运价有效期</th>
                                <th width="18%" align="center">结算价（不含税）</th>
                                <th width="10%" align="center">人数上限</th>
                                <th width="14%" align="center">燃油/机建</th>
                              </tr>
                              <!--  <tr><td colspan="7" height="1" class="line"></td></tr>-->
                              <tr>
                                <td height="25" align="center">去程</td>
                                <td align="center">￥<%=Utils.GetMoney(info.FromReferPrice)%></td>
                                <td align="center"><font color="#FF6600"><%=Utils.GetMoney(info.FromReferRate)%>%</font></td>
                                <td align="center"><%=freightDate%></td>
                                <td align="center"><span class="jiesuanjia">￥<%=Utils.GetMoney(info.FromSetPrice) %></span></td>
                                <td align="center"><%=info.MaxPCount%></td>
                                <td align="center">￥<%=Utils.GetMoney(info.FromFuelPrice)%>/<%=Utils.GetMoney(info.FromBuildPrice)%></td>
                              </tr>
                              <%if (info.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                                { %>
                              <tr>
                                <td height="25" align="center">回程</td>
                                <td align="center">￥<%=Utils.GetMoney(info.ToReferPrice)%></td>
                                <td align="center"><font color="#FF6600"><%=Utils.GetMoney(info.ToReferRate)%>%</font></td>
                                <td align="center"><%=freightDateBack%></td>
                                <td align="center"><span class="jiesuanjia">￥<%=Utils.GetMoney(info.ToSetPrice)%></span></td>
                                <td align="center"><%=info.MaxPCount%></td>
                                <td align="center">￥<%=Utils.GetMoney(info.ToFuelPrice)%>/<%=Utils.GetMoney(info.ToBuildPrice)%></td>
                              </tr><%} %>
                          </table></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="8%" rowspan="2" align="center"><%=EyouSoft.Common.Utils.GetMQ(supplierInfo.ContactMQ) %></td>
                          <td width="18%" height="25" align="left">联系人：<%=supplierInfo.ContactName %></td>
                          <td width="18%" align="left">上下班时间：<%=supplierInfo.WorkStartTime%>-<%=supplierInfo.WorkEndTime%></td>
                         
                          <td width="20%" align="left">代理级别：<%=supplierInfo.ProxyLev%></td>
                          <td width="28%" align="left">供应商主页：<a href="http://<%=supplierInfo.WebSite %>" target="_blank">http://<%=supplierInfo.WebSite %></a></td>
                        </tr>
                        <tr>
                          <td height="25" align="left">联系电话：<%=supplierInfo.ContactTel %></td>
                          <td align="left">出票成功率：<%=(supplierInfo.SuccessRate*100).ToString("F2") %>%</td>
                          <td align="left"><span style="line-height:33px;">处理数/提交数：<%=supplierInfo.HandleNum %>/<%=supplierInfo.SubmitNum%>（张）</span></td>
                          <td colspan="2" align="left">退票平均时间：自愿/非自愿：<%=supplierInfo.RefundAvgTime%>/<%=supplierInfo.NoRefundAvgTime %>（小时）</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td align="left"><span class="zhechebz">！供应商备注：<%=info.SupplierRemark%></span></td>
                  </tr>
              </table></td>
            </tr>
          </table>
        </div>
      </div>
      <div class="sidebar02_con_table03">
      	<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC" bgcolor="#FAFAFA">
          <tr align="left" bgcolor="#E0F4FD">
            <td height="30"><span class="title">添加旅客信息</span></td>
          </tr>
          <tr>
            <td align="left">
            	<div class="add_travellerInfo">
                  <ul>
                    <li><a href="javascript:void(0);" class="book_default" id="three1" onmousemove="setTab('three',1,2)">手工添加信息</a></li>
                    <li><a href="javascript:void(0);" id="three2" onmousemove="setTab('three',2,2)">导入旅客名单</a></li>
              
                  </ul>
                  <div class="clearboth"></div>
                  
                    <div class="add_travellerInfo_cont" id="con_three_1" >
                     
                     
                  
                       <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#fafafa">
                          <tr>
                            <td height="30" colspan="7" align="left" style="padding-left:7px;">旅客人数：
                              <select name="select" id="tb_selTourist" onchange="TicketBook.changeTouristNum(this);">
                              <%for (int i = 1; i <= touristCount; i++)
                                {%>
                               
                                <option value="<%=i%>" ><%=i%></option><%
                                } %>
                              </select>
                            位</td>
                          </tr>
                          <tr id="tb_Tourists1">
                            <th width="14%" height="30" align="center">旅客姓名</th>
                            <th width="5%" height="30" align="center">性别</th>
                             <th width="8%" height="30" align="center">游客类型</th>
                            <th width="16%" height="30" align="center">证件类型</th>
                            <th width="20%" height="30" align="center">证件号码</th>
                            <th width="13%" height="30" align="center">添加为常旅客</th>
                            <th width="12%" height="30" align="center">购买保险</th>
                            <th width="17%" align="center">购买行程单</th>
                          </tr>
                        <tr>
                        <td height="30" align="center"><input name="tb_tourName1" type="text" title="fsdf" onkeyup="return TicketBook.BindTourName(this);" onblur="return TicketBook.cancelMess(this);"  size="10" /></td>
                        <td height="30" align="center"><select name="tb_tourSex1"><option value="0">男</option><option value="1">女</option></select></td>
                         <td height="30" align="center"><select name="tb_tourType1"><option value="0">成人</option><option value="1">儿童</option><option value="2">婴儿</option></select></td>
                       <td height="30" align="center"><select name="tb_cerType1"><option value="0">身份证</option><option value="1">护照</option><option value="2">军官证</option><option value="3">台胞证</option><option value="4">港澳通行证</option></select></td>
                        <td height="30" align="center"><input name="tb_cerNo1" type="text" size="20" /></td>
                         <td height="30" align="center"><input name="tb_isOften1_1" type="checkbox" id="checkbox"  value="1" /></td>
                        <td height="30" align="center"><input name="tb_isInsure1_1"  type="radio"  value="1" />是<input name="tb_isInsure1_1" type="radio" value="0" checked="checked" />否</td>
                       <td height="30" align="center"><input name="tb_isTable1_1"  type="radio"  value="1" />是<input name="tb_isTable1_1" type="radio"  value="0" checked="checked" />否</td>
                    </tr>
                    <tr id="tb_Tourists3">
                            <td height="30" colspan="7" align="left"><span style="margin-left:8px; color:#FF0000;"><strong>！注：</strong>请确认输入的姓名与证件上的相同，否则将无法登机，英文名请在姓与名之间加“/”如“zhou/jielun”如乘客为常旅客主直接输乘客或者首字母，系统将自动调出乘客信息！</span></td>
                          </tr>
                          <tr style="display:none;">
                            <td height="35" colspan="7" align="center"><img src="<%=ImageServerUrl%>/images/jipiao/tijiaobtn.jpg" /></td>
                          </tr>
                      </table>
                    </div>
                  <div class="add_travellerInfo_cont" id="con_three_2"  style="display:none;">
                    <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#fafafa">
                          <tr>
                            <td height="35" align="center">导入旅客名单：</td>
                            <td align="center">
                         <%--上传控件--%>    
                        <div style=" margin-left:10px; margin-bottom:3px; margin-right:5px; float:left;">
                        <div>
                            <input type="hidden" id="hidFileName" runat="server" />
                            <span runat="server" id="spanButtonPlaceholder"></span>
                            <span id="errMsg_<%=hidFileName.ClientID %>" class="errmsg"></span>
                        </div>
                        <div id="divFileProgressContainer" runat="server">
                        </div>
                        <div id="thumbnails">
                        </div>
                        </div>  
                        <a href="<%=Domain.ServerComponents %>/TouristModel/TravellersModel.xls">Excel的模板下载</a></td>
                            <td align="center"><input type="image" name="imageField3" id="imageField3" src="<%=ImageServerUrl%>/images/jipiao/drbtn.jpg" onclick="return TicketBook.upLoad();" alt="导入名单"/></td>
                          </tr>
                        </table>
                        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#fafafa">
                          <tr>
                            <td height="30" colspan="7" align="left" style="padding-left:7px;">旅客人数： <font color="#FF0000"><span id="tb_tourNum"></span> </font>位 &nbsp;<span  style="color:Red;">(重新导入将覆盖原有数据)</span></td>
                          </tr>
                          <tr id="tb_Tourists2">
                            <th width="14%" height="30" align="center">旅客姓名</th>
                              <th width="5%" height="30" align="center">性别</th>
                             <th width="8%" height="30" align="center">游客类型</th>
                            <th width="16%" height="30" align="center">证件类型</th>
                            <th width="20%" height="30" align="center">证件号码</th>
                            <th width="13%" height="30" align="center">添加为常旅客</th>
                            <th width="12%" height="30" align="center">购买保险</th>
                            <th width="17%" align="center">购买行程单</th>
                          </tr>
                          <tr id="tb_Tourists4">
                            <td height="30" colspan="7" align="left"><span style="margin-left:8px; color:#FF0000;"><strong>！注：</strong>请确认输入的姓名与证件上的相同，否则将无法登机，英文名请在姓与名之间加“/”如“zhou/jielun”如乘客为常旅客主直接输乘客或者首字母，系统将自动调出乘客信息！</span></td>
                          </tr>
                      </table>
                     </div>
                    </div>
              </td>
          </tr>
        </table>
        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC" style="margin-top:10px;">
          <tr>
            <th width="20%" height="100" align="center">特殊要求备注</th>
            <td align="center"><textarea name="tb_remark" cols="60" rows="5" maxlength="25" id="tb_remark"></textarea></td>
          </tr>
        </table>
      </div>
  
       <div class="btn">
       		<table width="40%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td align="center"><a href="javascript:void(0);"  onclick="return TicketBook.save(0);"><img src="<%=ImageServerUrl%>/images/jipiao/shenghe_btn.jpg" width="103" height="30" alt="等待审核" /></a></td>
                <td align="center"><a href="javascript:void(0);"  onclick="return TicketBook.save(1);"><img src="<%=ImageServerUrl%>/images/jipiao/pay_btn.jpg" width="103" height="30" alt="直接支付"/></a></td>
              </tr>
         </table>
       </div>
    </div>
   
  
     <script type="text/javascript" src="<%=JsManage.GetJsFilePath("autocomplete") %>"></script>
      <script type="text/javascript">
         

          var TicketBook =
         {
             //选中旅客项
             selSex: function(v) {
                 if (v == "男")
                     return "0";
                 if (v == "女")
                     return "1";
                 else return "0";
             },
             selTtype: function(v) {
                 if (v == "成人")
                     return "0";
                 if (v == "儿童")
                     return "1";
                 if (v == "婴儿")
                     return "2";
             },
             selCType: function(v) {
                 if (v == "身份证")
                     return "0";
                 if (v == "护照")
                     return "1";
                 if (v == "军官证")
                     return "2";
                 if (v == "台胞证")
                     return "3";
                 if (v == "港澳通行证")
                     return "4";
             }
             ,
             selectItem: function(tr, input1) {
                 if (tr.cells.length > 0) {

                     input1.closest("tr").find("select[name='tb_tourSex1']").val(TicketBook.selSex($(tr.cells[1]).html()));
                     input1.closest("tr").find("select[name='tb_tourType1']").val(TicketBook.selTtype($(tr.cells[2]).html()));
                     input1.closest("tr").find("select[name='tb_cerType1']").val(TicketBook.selCType($(tr.cells[3]).html()));
                     input1.closest("tr").find("input[name='tb_cerNo1']").val($(tr.cells[4]).html());

                 }
             },
             //常旅客自动获取绑定
             bindComplete: function() {
                 $("#con_three_1").find("input[name='tb_tourName1']").autocomplete("/AirTickets/TeamBook/GetTourist.ashx", {
                     delay: 10,
                     minChars: 1,
                     matchSubset: false,
                     cacheLength: 1,
                     autoFill: false,
                     maxItemsToShow: 10,
                     onItemSelect: TicketBook.selectItem,
                     spaceCount: 5,
                     spaceFlag: "~&&~",
                     addWidth: 250,
                     IsFocusShow: false,
                     extraParams: { companyid: "<%=companyId %>", rand: Math.random() }
                 });
             },
             //保存审核或支付
             save: function(meth) {

                 var postDiv = $("#con_three_1");
                 if (postDiv.css("display") == "none") {
                     postDiv = $("#con_three_2");
                 }
                 var isTrue = true;
                 if (postDiv.find("input[name='tb_tourName1']").length == 0) {
                     alert("请添加旅客");
                     return false;
                 }

                 var travelArr = new Array();
                 postDiv.find("input[name='tb_tourName1']").each(function() {
                     var theName = $.trim($(this).val());
                     if (theName == "") {
                         isTrue = false;
                     }
                     else {
                         travelArr.push(theName);
                     }

                 });
                 if (!isTrue) {
                     alert("姓名不能为空")
                     return false;
                 }
                 var cerNoArr = new Array(); //所有证件号
                 postDiv.find("input[name='tb_cerNo1']").each(function() {
                     var value1 = $.trim($(this).val());
                     cerNoArr.push(value1);
                     if (value1 == "") {
                         isTrue = false;
                     }
                 });
                 if (!isTrue) {
                     alert("证件号不能为空")
                     return false;
                 }



                 var cerTypeArr = new Array();
                 postDiv.find("select[name='tb_cerType1']").each(function() {
                     var value2 = $.trim($(this).val());
                     cerTypeArr.push(value2);
                 });
                 var notCerNoArr = new Array();
                 var isCardId = /(^\d{15}$)|(^\d{17}[0-9Xx]$)/
                 for (var i in cerTypeArr) {
                     if (cerTypeArr[i] == "0")
                         if (!cerNoArr[i].match(isCardId)) {
                         notCerNoArr.push(cerNoArr[i]);
                     }
                 }
                 var travelArr2 = new Array();
                 var regName = new RegExp('^[\u4e00-\u9fa5]+$|^[a-zA-Z]+/[a-zA-Z]+$', 'gi');

                 var travelArr3 = new Array();
                 var regName2 = new RegExp('[^\x00-\xff]', 'gi');
                 for (var i in travelArr) {
                     var newvalue = travelArr[i].replace(regName2, "**");
                     if (newvalue.length > 27)
                         travelArr3.push(travelArr[i]);
                     else
                         if (!travelArr[i].match(regName))
                         travelArr2.push(travelArr[i]);
                 }
                 var message = "";
                 isTrue = true;

                 if (travelArr3.length != 0) {
                     message = "(" + travelArr3.join(",") + ")--姓名长度超过!\n";
                     isTrue = false;
                 }
                 if (notCerNoArr.length != 0) {
                     message += "(" + notCerNoArr.join(",") + ")--身份证格式不正确!\n";
                     isTrue = false;
                 }
                 if (travelArr2.length != 0) {
                     message += "(" + travelArr2.join(",") + ")--姓名格式不正确!";
                     isTrue = false;
                 }
                 if (!isTrue) {
                     alert(message);
                     return false;
                 }
                 cerNoArr.sort();
                 for (var i = 0; i < cerNoArr.length; i++) {
                     if (cerNoArr[i] == cerNoArr[i + 1]) {
                         alert("证件号码重复：" + cerNoArr[i]);
                         return false;
                     }
                 }
                 if ($("#tb_remark").val().length > 250) {
                     alert("特殊要求备注不能超过250个字符");
                     return false;
                 }
                 //Ajax提交数据
                 $.ajax(
	             {
	                 url: "TicketBook.aspx",
	                 data: postDiv.find("*").serialize() + "&" + $("#tb_remark").serialize() + "&method=save&act=" + meth + "&id=<%=id %>&peopleType=<%=peopleType %>&startDate=<%=startDate%>&backDate=<%=backDate %>",
	                 dataType: "json",
	                 cache: false,
	                 type: "post",
	                 success: function(result) {
	                     if (result.success == "1") {
	                         if (meth == 1)
	                             window.location = "TicketPay.aspx?orderId=" + result.message;
	                         else {
	                             alert("订单保存成功");
	                             window.location = "/AirTickets/OrderManage/CurrentOrderList.aspx";
	                         }

	                     }
	                     else {
	                         alert("操作失败");
	                     }
	                 },
	                 error: function() {
	                     alert("操作失败!");
	                 }
	             });
                 return false;
             },
             cancelMess: function(tar) {
                 $(tar).tipsy("hide");
             },
             BindTourName: function(tar) {
                 var tar1 = $(tar);
                 var value1 = $(tar).val();
                 var mess1 = $("#tb_outMess")
                 var dis = mess1.css("display");
                 if (/^[a-zA-Z]/.test(value1)) {
                     if (value1.length > 27) {


                             tar1.tipsy("show");

                     }
                     else

                         tar1.tipsy("hide");
                 }
                 else {
                     if (value1.length > 13) {
                         tar1.tipsy("show");
                        
                     }
                     else {
                         tar1.tipsy("hide");
                        
                     }
                 }
             },
             //改变游客人数
             changeTouristNum: function(tar_sel) {
                 var beforeNum = $("#tb_Tourists1").nextAll("tr").length - 2;
                 var nowNum = tar_sel.value;
                 var growNum = Math.abs(nowNum - beforeNum);
                 if (nowNum > beforeNum) {
                     for (var i = 1; i <= growNum; i++) {
                         var arr1 = new Array();
                         var t = beforeNum + i;
                         arr1.push(' <tr><td height="30" align="center"><input name="tb_tourName1" type="text"  size="10" onkeyup="return TicketBook.BindTourName(this);" onblur="TicketBook.cancelMess(this);" /></td>');
                         arr1.push(' <td height="30" align="center"><select name="tb_tourSex1"><option value="0">男</option><option value="1">女</option></select></td>');
                         arr1.push(' <td height="30" align="center"><select name="tb_tourType1" ><option value="0">成人</option><option value="1">儿童</option><option value="2">婴儿</option></select></td>');
                         arr1.push(' <td height="30" align="center"><select name="tb_cerType1" ><option value="0">身份证</option><option value="1">护照</option><option value="2">军官证</option><option value="3">台胞证</option><option value="4">港澳通行证</option></select></td>');
                         arr1.push(' <td height="30" align="center"><input name="tb_cerNo1" type="text"  size="20" /></td>');
                         arr1.push(' <td height="30" align="center"><input name="tb_isOften1_' + t + '" value="1" type="checkbox" /></td>');
                         arr1.push(' <td height="30" align="center"><input name="tb_isInsure1_' + t + '" type="radio"  value="1" />是<input name="tb_isInsure1_' + t + '" type="radio"  value="0" checked="checked" />否</td>');
                         arr1.push(' <td height="30" align="center"><input name="tb_isTable1_' + t + '"  type="radio"  value="1" />是<input name="tb_isTable1_' + t + '" type="radio"  value="0" checked="checked" />否</td></tr>');
                         $("#tb_Tourists3").before(arr1.join(""));


                     }
                     TicketBook.bindComplete();
                     $("#con_three_1").find("input[name='tb_tourName1']").tipsy({ trigger: 'manual', gravity: "se", fade: true, content: '中文不超过13个字，英文不超过27个字符' });
                 }
                 else {
                     for (var i = 1; i <= growNum; i++) {
                         $("#tb_Tourists3").prev("tr").remove();
                     }
                 }
             },
             //查询机票
             search: function() {
               
                 var airType = $("input[name='radVoyageType']:checked").val();
                 var startCity = $("#ts_selFromCity").val();
                 var toCity = $("#ts_selToCity").val();
                 if (startCity == "0") {
                     alert("请选择始发地");
                     return false;
                 }
                 if (toCity == "0") {
                     alert("请选择目的地");
                     return false;
                 }
                 var startDate = $("#tb_txtStartDate").val();
                 var backDate = $("#tb_txtBackDate").val();
                 if (startDate == "") {
                     alert("请选择出发时间");
                     return false;
                 }
                 if (airType == "2" && backDate == "") {
                     alert("请选择返回时间");
                     return false;
                 }
                 if (airType == "2") {
                     if (startDate > backDate) {
                         alert("出发时间不能大于返回时间");
                         return false;
                     }

                 }
                 var startCity1 = encodeURIComponent($("#ts_selFromCity").find("option:selected").html());
                 var toCity1 = encodeURIComponent($("#ts_selToCity").find("option:selected").html());
                 var peopleNum = "";
                
                 var airComapny = "";
                 var peopleType = "<%=peopleTypeInt %>"
                 window.location = "TicketSelect.aspx?airType=" + airType + "&startCity=" + startCity + "&toCity=" + toCity + "&startCity1=" + startCity1 + "&toCity1=" + toCity1 + "&peopleNum=" +
                 peopleNum + "&startDate=" + startDate + "&backDate=" + backDate + "&airComapny=" + airComapny + "&peopleType=" + peopleType;
                 return false;
             },
             //是否显示返程日期
             isShowEndDate: function() {
                 var airType = $("input[name='radVoyageType']:checked").val();
                 if (airType == 1) {

                     $("#tb_txtBackDate").hide().next("img").hide().parent("td").prev("td").html("");
                 } else {
                     $("#tb_txtBackDate").show().next("img").show().parent("td").prev("td").html("<b>返回时间</b>");
                 }
             },
             //             //返回选中证件类型
             //             rep: function($1, $2) {
             //                 return $2 + ' selected="selected"';
             //             },
             //上传控件
             swfObj: null,
             //创建游客数据
             createData: function(arr) {
                 var length = arr.length;
                 if (length == 0) { alert("未能导入任何数据!"); return false; }
                 var s = new Array();
                 //绑定列表数据
                 //
                 $("#tb_Tourists2").nextAll("tr").not("#tb_Tourists4").remove();
                 for (var i = 0; i < length; i++) {
                     var s = new Array();
                     var t = i + 1;
                     //                     var reg1 = new RegExp('e="' + arr[i].CerType + '"', 'gi'); //匹配证件类型
                     //                     var reg2 = new RegExp('e="' + arr[i].TourType + '"', 'gi');
                     //                     var reg3= new RegExp('e="' + arr[i].TourSex + '"', 'gi');
                     s.push('<tr><td height="30" align="center"><input name="tb_tourName1" type="text" value="' + arr[i].TourName + '" size="10" /></td>');
                     s.push(' <td height="30" align="center"><select name="tb_tourSex1"><option value="0" selected="selected">男</option><option value="1">女</option></select></td>');
                     s.push('<td height="30" align="center"><select name="tb_tourType1" ><option value="0" selected="selected">成人</option><option value="1">儿童</option><option value="2">婴儿</option></select></td>');
                     s.push('<td height="30" align="center"><select name="tb_cerType1" ><option value="0" selected="selected">身份证</option><option value="1">护照</option><option value="2">军官证</option><option value="3">台胞证</option><option value=4">港澳通行证</option></select></td>');
                     s.push('<td height="30" align="center"><input name="tb_cerNo1" type="text"  value="' + arr[i].CerNo + '" size="20" /></td>');
                     s.push('<td height="30" align="center"><input name="tb_isOften1_' + t + '" value="1" type="checkbox"/></td>');
                     s.push('<td height="30" align="center"><input name="tb_isInsure1_' + t + '" type="radio"  value="1" /> 是 <input type="radio" name="tb_isInsure1_' + t + '" id="radio2" value="0" checked="checked"/> 否</td>');
                     s.push('<td height="30" align="center"><input name="tb_isTable1_' + t + '" type="radio"  value="1" /> 是 <input type="radio" name="tb_isTable1_' + t + '" id="radio4" value="0" checked="checked"/> 否 </td></tr>');

                     $("#tb_Tourists4").before(s.join(""));
                 }
                 $("#tb_tourNum").html(t)
             },

             changeSeattle: function(tar) {
             var seatid = $(tar).val();
                 TicketBook.getSeattle(seatid,"","");
                 return false;
             },
             getSeattle: function(seattleId1,isSelect1,selectId1) {
                 $.ajax(
	               {
	                   url: "GetSeattles.ashx",
	                   data: { seattleId: seattleId1, isSelect: isSelect1, selectId: selectId1 },
	                   dataType: "text",
	                   cache: false,
	                   type: "get",
	                   success: function(result) {
	                       if (seattleId1 == "all") {
	                           $("#ts_selFromCity").html(result);
	                       }
	                       else if (seattleId1 == "0") {
	                           return false;
	                       }
	                       else
	                           $("#ts_selToCity").html(result);
	                   },
	                   error: function() {
	                       alert("获取城市时出错!");
	                   }
	               });
                 return false;

             },
             //上传excel文件
             upLoad: function() {
                 if (TicketBook.swfObj.getStats().files_queued > 0) {
                     TicketBook.swfObj.startUpload();
                 }
                 return false;
             },
             //上传成功回调
             upLoadSuccess: function(file, serverData) {
                 try {

                     var obj = eval(serverData);
                     if (obj.error) {
                         return;
                     }
                     else {
                         var progress = new FileProgress(file, this.customSettings.upload_target);
                         progress.setStatus("上传成功.");
                         TicketBook.createData(obj); //生成列表数据
                         resetSwfupload(TicketBook.swfObj, file); //恢复上传控件原有状态
                     }
                 }
                 catch (ex) { }
             }
         }

         $(function() {
            
             $("#tb_selTourist").val("1");
             TicketBook.getSeattle("all", "isSelect", "<%=startCity%>");
           
             TicketBook.getSeattle("<%=startCity%>", "isSelect", "<%=toCity%>");

             /*机票 END */


             if ("<%=airType%>" == "2") {
                 $("#radVType2").attr("checked", "checked");
                 $("#tb_txtBackDate").show().next("img").show().parent("td").prev("td").html("<b>返回时间</b>");
             }
             TicketBook.bindComplete();


         });
    </script>
     <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
  <script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>
      <%--上传文件 --%>
    <script type="text/javascript">
        $(function() {

            
           $("#con_three_1").find("input[name='tb_tourName1']").tipsy({trigger:'manual',gravity:"se", fade:true,content:'中文不超过13个字，英文不超过27个字符'});
            TicketBook.swfObj = new SWFUpload({
                // Backend Settings
                upload_url: "<%=EyouSoft.Common.Domain.UserPublicCenter%>/AirTickets/TeamBook/LoadTourist.ashx",
                file_post_name: "Filedata",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>",
                    "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value %>",
                    "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value %>",
                       "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_Password %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password].Value:string.Empty %>"

                },

                // File Upload Settings
                file_size_limit: "1 MB",
                file_types: "*.xlsx;*.xls",
                file_types_description: "",
                file_upload_limit: "1",    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_loaded_handler: swfUploadLoaded,
                file_dialog_start_handler: fileDialogStart,
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: TicketBook.upLoadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "<%=Domain.ServerComponents%>/images/swfupload/XPButtonNoText_160x22.png",
                button_placeholder_id: "<%=spanButtonPlaceholder.ClientID %>",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">选择文件<span class="buttonSmall">(最大1 MB)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
                button_cursor: SWFUpload.CURSOR.HAND,

                // Flash Settings
                flash_url: "<%=Domain.ServerComponents%>/js/swfupload/swfupload.swf", // Relative to this file

                custom_settings: {
                    upload_target: "<%=divFileProgressContainer.ClientID %>",
                    HidFileNameId: "<%=hidFileName.ClientID %>",
                    ErrMsgId: "errMsg_<%=hidFileName.ClientID %>",
                    UploadSucessCallback: null
                },

                // Debug Settings
                debug: false,

                // SWFObject settings
                minimum_flash_version: "9.0.28",
                swfupload_pre_load_handler: swfUploadPreLoad,
                swfupload_load_failed_handler: swfUploadLoadFailed
            });

        });
		  </script>
    
</asp:Content>
