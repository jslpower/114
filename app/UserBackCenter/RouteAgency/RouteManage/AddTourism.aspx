<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTourism.aspx.cs" Inherits="UserBackCenter.RouteAgency.RouteManage.AddTourism" %>

<%@ Register Src="~/usercontrol/RouteAgency/TourServiceStandard.ascx" TagName="ServiceStandard"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="DocFileUpload" TagPrefix="ucDoc" %>
<asp:content id="AddTourism" contentplaceholderid="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"  cache="true"></script>
<script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
   <script type="text/javascript">
       commonTourModuleData.add({
           ContainerID: '<%=Key %>',
           ReleaseType: 'AddTourism'
       });
    </script>
<div id="<%=Key %>" class="right Max">
   <div class="tablebox">
   <input type="hidden" id="hd_isUp" runat="server" />
   
                <!--添加信息表格-->
                <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
                    style="width: 100%;">
                    <tr>
                        <td width="16%" align="right" bgcolor="#f2f9fe">
                            专线类型：
                        </td>
                        <td align="left">
                            <asp:Label runat="server" id="lbl_travelRangeName" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 线路名称：
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" id="txt_LineName" size="45" valid="required"  errmsg="请填写线路名称"></asp:TextBox>
                            <span id="errMsg_<%=txt_LineName.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            状态：
                        </td>
                        <td align="left">
                            <asp:DropDownList  id="ddl_Status" runat="server">
                                
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            大交通：
                        </td>
                        <td align="left">
                            出发：
                            <asp:DropDownList runat="server" id="ddl_DepartureTraffic">
                                <asp:listItem value="-1">-请选择-</asp:listItem>
                            </asp:DropDownList>
                            返回：
                            <asp:DropDownList runat="server" id="ddl_ReturnTraffic">   
                                <asp:listItem value="-1">-请选择-</asp:listItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 出发城市：
                        </td>
                        <td align="left">
                            <input type="hidden"  runat="server" id="hd_goCity" valid="required"  errmsg="请选择出发城市" />
                                <span>
                                    <asp:Repeater id="rpt_DepartureCity" runat="server">
                                        <ItemTemplate>
                                            <input class="cfCity" name="cfCity" type="radio" value="<%#Eval("CityId") %>"cname="<%#Eval("CityName")%>"/><%#Eval("CityName")%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </span>
                            <a href="javascript:void(0)"  id="a_goSet"  onclick="AddTourism.SetCity(this)" ><span class="huise">更多</span><span id="errMsg_<%=hd_goCity.ClientID%>" class="errmsg"></span></a>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 返回城市：
                        </td>
                        <td align="left">
                        <input type="hidden"  runat="server" id="hd_returnCity" valid="required"  errmsg="请选择返回城市" />
                        <span>
                       
                             <asp:Repeater id="rpt_BackToCities" runat="server">
                                <ItemTemplate>
                                    <input class="fhCity" name="fhCity" type="radio" value="<%#Eval("CityId") %>" cname="<%#Eval("CityName")%>"/><%#Eval("CityName")%>
                                </ItemTemplate>
                            </asp:Repeater>
                            </span>
                            <a href="javascript:void(0)" id="a_retnrnSet" onclick="AddTourism.SetCity(this)"><span class="huise">更多</span><span id="errMsg_<%=hd_returnCity.ClientID%>" class="errmsg"></span></a>
                        </td>
                    </tr>
                    <%if (travelRangeType == 1)
                      {%>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                             主要游览国家：
                        </td>
                        <td align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                    <input type="hidden" id="hd_BrowseCountrys" runat="server"/>
                                     <span id="sp_BrowseCountrys">
                                        <asp:Repeater id="rpt_VisitingNational"  runat="server">
                                            <ItemTemplate>
                                                <input type="checkbox" class="chk_BrowseCountrys" value="<%#Eval("CountryId") %>" txt="<%#Eval("CName")%>" /><%#Eval("CName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>
                                        （<font class="ff0000">如果缺少选项，请电话客服添加区域</font>）
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            签证或通行证：
                        </td>
                        <td align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        <input runat="server" type="checkbox" id="chk_null" value="1" />免签
                                        <input type="hidden" id="hd_qz" runat="server" /> 
                                        <span id="sp_qz"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            线路定金：
                        </td>
                        <td align="left">
                            成人
                            <asp:TextBox id="txt_AdultDeposit" size="10" runat="server" valid="RegInteger" errmsg="成人定金格式错误"></asp:TextBox>
                            元 儿童
                            <asp:TextBox runat="server" id="txt_ChildrenDeposit" size="10" valid="RegInteger" errmsg="儿童定金格式错误"></asp:TextBox>
                            元 <font class="ff0000">（输入0 表示无需支付定金）</font>
                             <span id="errMsg_<%=txt_AdultDeposit.ClientID %>" class="errmsg"></span>
                             <span id="errMsg_<%=txt_ChildrenDeposit.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <%}
                      else
                      {
                              %>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                             主要游览城市：
                        </td>
                        <td align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        <input type="hidden" id="hd_BrowseCitys"  runat="server"/>
                                        <span id="sp_BrowseCitys">
                                            <asp:Repeater runat="server" id="rpt_browseCitys">
                                                <ItemTemplate>
                                                    <input type="checkbox" class="chk_BrowseCitys" value="<%#Eval("CityId") %>" /><%#Eval("CityName")%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater runat="server" id="rpt_districtCounty">
                                                <ItemTemplate>
                                                    <input type="checkbox" class="chk_DistrictCounty" cityid=<%#Eval("CityId") %> value="<%#Eval("Id") %>" /><%#Eval("DistrictName")%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr  id="tr_pass" style="display:none">
                       <td align="right" bgcolor="#f2f9fe">
                                通行证：
                       </td>
                       <td align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                   <td>
                                            
                                   </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%  }%>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            线路配图：
                        </td>
                        <td align="left">
                            <img id="img_showImg" src="" runat="server"  visible=false />
                            <%--<a runat="server" id="a_showImg" style="padding-left:10px;" target="_blank" visible="false">查看原图</a>--%><ucDoc:DocFileUpload id="files" runat="server"/>
                           （<font class="ff0000">最佳比例4：3，不大于2M，上传后生成等比缩放的
                                400*300范围内的缩略图和1024*768范围内的大图 </font>）
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            线路特色：
                        </td>
                        <td align="left">
                            <textarea  id="txt_LineFeatures" cols="65" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            独立成团：
                        </td>
                        <td align="left">
                            团队参考价格：
                            <asp:TextBox runat="server" id="txt_ReferencePrice" size="10" valid="RegInteger" errmsg="团队参考价格格式错误"></asp:TextBox>   
                             最小成团人数
                            <asp:TextBox runat="server" id="txt_MinNumberPeople" size="10" valid="RegInteger" errmsg="最小成团人数格式错误"></asp:TextBox>              
                            <span id="errMsg_<%=txt_MinNumberPeople.ClientID %>" class="errmsg"></span>
                            <span id="errMsg_<%=txt_ReferencePrice.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 天数：
                        </td>
                        <td align="left">                       
                            <asp:TextBox runat="server" id="txt_days" size="6" valid="isPIntegers" errmsg="天 数不能为负" onblur="AddTourism.GenerateTravel(this)"></asp:TextBox>
                            天
                            <asp:TextBox runat="server" id="txt_nights" size="6" valid="RegInteger" errmsg="晚 不能为负"></asp:TextBox>
                            晚 （<font class="ff0000">住宿天数</font>）
                            <span id="errMsg_<%=txt_days.ClientID %>" class="errmsg"></span>
                            <span id="errMsg_<%=txt_nights.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 提前几天报名：
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" id="txt_EarlyDays" size="6" valid="required|RegInteger"  errmsg="请填写提前报名天数|提前报名天数不能为负"></asp:TextBox>
                            天<font class="ff0000">（用于计算报名截止时间，请尽量写准确）</font>
                            <input type="checkbox"  id="chk_FITRegistration"  value="true" runat="server"/>
                            散客报名无需成团，铁定发团   <span id="errMsg_<%=txt_EarlyDays.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 线路主题：
                        </td>
                        <td align="left">
                        <input type="hidden" id="hd_SubjectLine" runat="server" valid="required"  errmsg="请选择线路主题" />
                            <asp:Repeater runat="server" id="rpt_SubjectLine">
                                <ItemTemplate>
                                    <input type="checkbox" class="inp_SubjectLine" value="<%#Eval("FieldId") %>" nameval="<%#Eval("FieldName")%>" /><%#Eval("FieldName")%>
                                </ItemTemplate>
                            </asp:Repeater>
                            <span id="errMsg_<%=hd_SubjectLine.ClientID %>" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" bgcolor="#f2f9fe" style="padding-top: 50px;">
                            <font class="ff0000">*</font> 行程：
                        </td>
                        <td align="left">
                             <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <div id="tb_" class="tb_">
                                        <ul>
                                            <li id="tb_1" class="<%=isTravel.ToString()=="True"?"normaltab":"hovertab" %>" xctype="SAE" onclick="AddTourism.ShownSeparately('#tbc_01','.xc',this)">
                                                <a>简易版</a></li>
                                            <li id="tb_2" class="<%=isTravel.ToString()=="True"?"hovertab":"normaltab" %>" xctype="Standard" onclick="AddTourism.ShownSeparately('#tbc_02','.xc',this)">
                                                <a>标准版</a></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 80%;">
                                        <input type="hidden" id="hd_TravelContent" name="hd_TravelContent" value="<%=isTravel.ToString()=="True"?"Standard":"SAE" %>" />
                                        <div class="<%=isTravel.ToString()=="True"?"undis":"dis" %> xc" id="tbc_01">
                                            <textarea id="txt_AddTourismBriefnessTravel" runat="server" cols="85" rows="5"></textarea></div>
                                        <div class="<%=isTravel.ToString()=="True"?"dis":"undis" %> xc" id="tbc_02">
                                            <table id="tab_Standard" width="100%" border="1" align="center" cellpadding="2" cellspacing="0"
                                                bordercolor="#CCCCCC">
                                                <tr bgcolor="#F2F9FE">
                                                    <th width="12%" align="center" bgcolor="#F2F9FE">
                                                        日程
                                                    </th>
                                                    <th width="22%" align="center">
                                                        行程内容
                                                    </th>
                                                    <th width="22%" align="center">
                                                        交通工具
                                                    </th>
                                                    <th width="22%" align="center">
                                                        住宿
                                                    </th>
                                                    <th width="22%" align="center">
                                                        用餐
                                                    </th>
                                                </tr>
                                                <%if (!isTravel)
                                                  { %>
                                                <tr id="tr_xcContent">
                                                    <td rowspan="2" align="center" class="td_index">
                                                        D1
                                                    </td>
                                                    <td align="center">
                                                        途径：
                                                        <input type="text" size="15" class="txt_Way" name="txt_Way" />
                                                    </td>
                                                    <td align="center">
                                                        <select class="sel_Traffic" name="sel_Traffic">
                                                            <option selected="selected" value="-1">请选择</option>
                                                            <option value="1">飞机</option>
                                                            <option value="2">大巴</option>
                                                            <option value="3">火车</option>
                                                            <option value="4">轮船</option>
                                                            <option value="5">不包含</option>
                                                            <option value="6">其它</option>
                                                        </select>
                                                    </td>
                                                    <td align="center">
                                                        <input type="text" class="txt_Stay" name="txt_Stay" size="15" />
                                                    </td>
                                                    <td align="center">
                                                        <input type="hidden" name="hd_Dining" />
                                                        <input type="checkbox" name="chk_Breakfast" value="1" onclick="AddTourism.SetDining(this)" />
                                                        早
                                                        <input type="checkbox" name="chk_Lunch" value="2" onclick="AddTourism.SetDining(this)" />
                                                        中
                                                        <input type="checkbox" name="chk_Dinner" value="3" onclick="AddTourism.SetDining(this)" />
                                                        晚
                                                    </td>
                                                </tr>
                                                <tr id="tr_xcRemarks">
                                                    <td colspan="4" align="left">
                                                        <textarea class="txt_Travel" name="txt_Travel" cols="100" rows="4"></textarea>
                                                    </td>
                                                </tr>
                                                <%}
                                                  else
                                                  {  %>
                                                  <asp:Repeater runat="server" id="rpt_standardPlans">
                                                        <ItemTemplate>
                                                              <tr id="tr1">
                                                                <td rowspan="2" align="center" class="td_index">
                                                                    D<%#Container.ItemIndex+1 %>
                                                                </td>
                                                                <td align="center">
                                                                    途径：
                                                                    <input type="text" size="15" class="txt_Way" name="txt_Way" value="<%#Eval("PlanInterval") %>" />
                                                                </td>
                                                                <td align="center">
                                                                <input type="hidden" class="hd_Traffic" value="<%#(int)Eval("Vehicle") %>" />
                                                                    <select class="sel_Traffic" name="sel_Traffic">
                                                                        <option selected="selected" value="-1">请选择</option>
                                                                        <option value="1">飞机</option>
	                                                                    <option value="2">大巴</option>
	                                                                    <option value="3">火车</option>
	                                                                    <option value="4">轮船</option>
	                                                                    <option value="5">不包含</option>
	                                                                    <option value="6">其它</option>
                                                                    </select>
                                                                </td>
                                                                <td align="center">
                                                                    <input type="text" class="txt_Stay" name="txt_Stay" value="<%#Eval("House") %>" size="15" />
                                                                </td>
                                                                <td align="center">
                                                                    <input type="hidden" class="hd_Dining" name="hd_Dining" value="<%#Eval("Early")%>,<%#Eval("Center")%>,<%#Eval("Late")%>" />
                                                                    <input type="checkbox"  class="chk_Breakfast" name="chk_Breakfast" value="1" onclick="AddTourism.SetDining(this)"/>
                                                                    早
                                                                    <input type="checkbox" class="chk_Lunch" name="chk_Lunch" value="2" onclick="AddTourism.SetDining(this)"/>
                                                                    中
                                                                    <input type="checkbox"class="chk_Dinner" name="chk_Dinner" value="3" onclick="AddTourism.SetDining(this)"/>
                                                                    晚
                                                                </td>
                                                            </tr>
                                                            <tr id="tr2">
                                                                <td colspan="4"  align="left">
                                                                    <textarea class="txt_Travel" name="txt_Travel" cols="100" rows="4"><%#Eval("PlanContent")%></textarea>
                                                                </td>
                                                            </tr>
                                                          </ItemTemplate>
                                                  </asp:Repeater>
                                                <%} %>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" bgcolor="#f2f9fe" style="padding-top: 50px;">
                            <font class="ff0000">*</font> 报价包含：
                        </td>
                        <td align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        <div id="tb1_" class="tb1_">
                                            <ul>
                                                <li id="tb1_1" class="<%=isServiceStandard.ToString()=="True"?"normaltab":"hovertab" %>" bhtype="FIT" onclick="AddTourism.ShownSeparately('#tbc1_01','.bj',this)"><a>简易版</a></li>
                                                <li id="tb1_2" class="<%=isServiceStandard.ToString()=="True"?"hovertab":"normaltab" %>" bhtype="Team" onclick="AddTourism.ShownSeparately('#tbc1_02','.bj',this)"><a>标准版</a></li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 80%;">
                                        <input type="hidden" id="hd_FITOrTeam" name="hd_FITOrTeam" value="<%=isServiceStandard.ToString()=="True"?"Team":"FIT" %>" />
                                            <div class="<%=isServiceStandard.ToString()=="True"?"undis1":"dis1" %> bj" id="tbc1_01">
                                                <textarea  id="txt_FIT"  cols="85" rows="5"  runat="server" ></textarea></div>
                                            <div class="<%=isServiceStandard.ToString()=="True"?"dis1":"undis1" %> bj" id="tbc1_02">
                                            <cc1:ServiceStandard ID="AddStandardRoute_ServiceStandard" runat="server" ModuleType="route" ReleaseType="AddStandardRoute" />                
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            报价不含：
                        </td>
                        <td align="left">
                            <textarea  id="txt_PriceExcluding" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            赠送项目：
                        </td>
                        <td align="left">
                            <textarea  id="txt_GiftItems" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            儿童及其他安排：
                        </td>
                        <td align="left">
                            <textarea  id="txt_OtherArr" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            购物安排：
                        </td>
                        <td align="left">
                            <textarea  id="txt_ShoppingArr" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            自费项目：
                        </td>
                        <td align="left">
                            <textarea  id="txt_ThisConsumption" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            销售商须知：
                        </td>
                        <td align="left">
                            <textarea  id="txt_Notes" cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            备注：
                        </td>
                        <td align="left">
                            <textarea  id="txt_Remarks"  cols="85" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                    <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                      {%>
                    <tr>
                        <td align="right" bgcolor="#f2f9fe">
                            <font class="ff0000">*</font> 销售区域：
                        </td>
                        <td align="left">
                            <input type="checkbox" id="chk_AllSellCity" />全选
                            <asp:Repeater runat="server" id="rpt_SalesArea">
                                <ItemTemplate>
                                    <input type="checkbox" class="chk_SellCity"   value="<%#Eval("CityId") %>" pid="<%#Eval("ProvinceId") %>" pname="<%#Eval("ProvinceName") %>" cname="<%#Eval("CityName")%>" /><%#Eval("CityName")%>
                                </ItemTemplate>
                            </asp:Repeater>
                            <input type="hidden" name="hd_SellCity" id="hd_SellCity" runat="server" valid="required"  errmsg="请选择销售区域" />
                            <span id="errMsg_<%=hd_SellCity.ClientID %>" class="errmsg"></span>
                            <span id="sp_SalesAreaMsg" runat="server">暂无销售区域</span>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td height="35" colspan="2" align="center">
                        <a href="javascript:void(0);" class="basic_btn" toptitle="我的线路库" ret="/routeagency/routemanage/routeview.aspx?routeSource=<%=(int)routeSource %>"><span>保存后，返回线路库</span></a>
                        <%if (routeSource != EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                          {%>
                             <a href="javascript:void(0);" toptitle="批量添加修改计划"class="basic_btn" ret="/routeagency/addscatteredfightplan.aspx?routeSource=<%=(int)routeSource %>">
                            <span>保存后，发布出团计划</span></a>
                       <%} %>
                                 <a href="javascript:void(0);" class="basic_btn" ret="/printpage/linetourinfo.aspx"><span>保存后，打印线路行程单</span></a>
                            <%--<asp:LinkButton runat="server" id="lbtn_SaveReturnLibrary" class="basic_btn" onclcik="lbtn_SaveReturnLibrary_Click"><span>保存后，返回线路库</span></asp:LinkButton>
                            <asp:LinkButton runat="server" id="lbtn_SaveReturnRelease" class="basic_btn" onclcik="lbtn_SaveReturnRelease_Click"><span>保存后，发布出团计划</span></asp:LinkButton>
                            <asp:LinkButton runat="server" id="lbtn_SaveReturnPrint" class="basic_btn" onclcik="lbtn_SaveReturnPrint_Click"><span>保存后，打印线路行程单</span></asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
</div>

<script type="text/JavaScript">

    var AddTourism = {
        Files:<%=this.files.ClientID%>,//上传用户控件js对象
        //图片上传
        FilePicUpload: function(obj) {
            AddTourism.UploadSucessCallbackData = obj;
            if (AddTourism.Files.getStats().files_queued > 0) {
                AddTourism.Files.startUpload();
                AddTourism.Files.uploadError = function() { alert("图片上传失败！"); };
                AddTourism.Files.customSettings.UploadSucessCallback = AddTourism.Save;
            }
            else {
                AddTourism.Save();
            }
        },
        UploadSucessCallbackData: null, //上传回调函数参数
        KEInit: function(obj) {
            var id = $(obj).attr("id");
            KE.init({
                id: id, //编辑器对应文本框id
                width: '700px',
                height: '350px',
                skinsPath: '/kindeditor/skins/',
                pluginsPath: '/kindeditor/plugins/',
                scriptPath: '/kindeditor/skins/',
                resizeMode: 0, //宽高不可变
                items: keMore //功能模式(keMore:多功能,keSimple:简易)
            });
            setTimeout(function() {
                KE.create(id, 0);
                KE.html(id, $(obj).val());
            }, 200)
        },
        //设置出发返回城市
        SetCity: function(obj) {
            var url = "/RouteAgency/SetLeaveCity.aspx";
            url += "?ContainerID=" + $(obj).attr("id");
            url += "&inputclass=" + $(obj).parent().find("input:eq(1)").attr("class");
            url += "&callBack=AddTourism.BoxyCallBack"
            url += "&Key="+$(obj).closest(".Max").attr("id")+"&GetType=radio";
            url += "&rnd=" + Math.random();
            TourModule.OpenDialog('设置常用出发返回城市', url, 650, 450);
            return false;
        },
        //保存出发城市和返回城市隐藏域(后台存储数据读取该隐藏域内容)
        SaveGoOrReturnCity: function(obj) {
            var thiss = $(obj);
            thiss.closest("td").find("input:hidden").eq(0).val(thiss.val() + "|" + thiss.attr("cname"));
        },
        //设置就餐隐藏域(后台存储数据读取该隐藏域内容)
        SetDining: function(obj) {
            var tdobj = $(obj).closest("td");
            var str = "";
            tdobj.find("input:checked").each(function() {
                str += $(this).val() + ","
            })
            tdobj.find("input:hidden").eq(0).val(str.substring(0, str.length - 1));
        },
        //单独显示方法
        ShownSeparately: function(obj/*显示对象id*/, domain/*域*/, thisobj/*按钮本身*/) {
            if (domain.constructor === String && obj.constructor === String) {
                var Key = $("#" + $(thisobj).closest(".Max").attr("id"));
                $(thisobj).closest("ul").find(".hovertab").attr("class", "normaltab")
                $(thisobj).attr("class", "hovertab")
                Key.find(domain).hide();
                Key.find(obj).show();

                Key.find(obj).parent("div").find("#hd_TravelContent").val($(thisobj).attr("xctype"))
                Key.find(obj).parent("div").find("#hd_FITOrTeam").val($(thisobj).attr("bhtype"))
            }
            else {
                alert("参数必须是字符串")
            }
            return false;
        },
        //保存提交
        Save: function() {
            var Key = $("#<%=Key %>");
            var str = "";
            var form = $(Key).closest("form").get(0);
            if (Key.find("#hd_TravelContent").val() == "SAE" && Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>").css("display") == "none") {
                //行程编辑框
                Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>").val(KE.html("<%=txt_AddTourismBriefnessTravel.ClientID %>"))
            }
            //验证行程
            if (Key.find("#hd_TravelContent").val() == "SAE") {
                //简易版行程验证
                if (Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>").val().length <= 0) {
                    str += "- 请输入简易版行程!\n";
                }
            }
            else {
                //标准版行程验证
                str += AddTourism.TestData();
            }
            
            //验证报价包含
            if (Key.find("#hd_FITOrTeam").val() == "FIT" && Key.find("#<%=txt_FIT.ClientID %>").val().length <= 0) {
                str += "- 请输入常规散客报价包含!";
            }
            if (str.length > 0) {
                alert(str)
                return false;
            }
            //销售区域
            str = "";
            Key.find(".chk_SellCity:checked").each(function() {
                str += $(this).val() + "," + $(this).attr("cname") + "," + $(this).attr("pid") + "," + $(this).attr("pname") + "|"
            })
            Key.find("#<%=hd_SellCity.ClientID %>").val(str.substring(0, str.length - 1));
            //线路主题
            str = "";
            Key.find(".inp_SubjectLine:checked").each(function() {
                str += $(this).val() + "," + $(this).attr("nameval") + "|"
            })
            Key.find("#<%=hd_SubjectLine.ClientID %>").val(str.substring(0, str.length - 1));

            if ('<%=travelRangeType%>' == 1) {
                str = "";
                //主要浏览国家
                Key.find("#sp_BrowseCountrys input:checked").each(function() {
                    str += $(this).val() + ","
                })
                Key.find("#<%=hd_BrowseCountrys.ClientID %>").val(str.substring(0, str.length - 1));
            }
            else {
                str = "";
                //主要浏览城市
                Key.find("#sp_BrowseCitys input:checked").each(function() {
                    if ($(this).attr("class") == "chk_DistrictCounty") {
                        //县id|城市id
                        str += $(this).val() + "|" + $(this).attr("cityid") + ",";
                    } else {
                        str += $(this).val() + ","
                    }
                })
                Key.find("#<%=hd_BrowseCitys.ClientID %>").val(str.substring(0, str.length - 1));
            }

            var isUp = '<%=routeId %>'.length > 0;
            if (ValiDatorForm.validator(form, "alert")) {
                $.newAjax(
	               {
	                   url: "/RouteAgency/RouteManage/AddTourism.aspx?RouteSource=<%=(int)routeSource %>&Operating=" + (isUp > 0 ? "UpSave&routeId=<%=routeId %>" : "AddSave") + "&travelRangeId=<%=travelRangeId %>&travelRangeType=<%=travelRangeType %>",
	                   data: $(form).serialize().replace(),
	                   dataType: "html",
	                   cache: false,
	                   type: "post",
	                   success: function(result) {
	                       switch (result) {
	                           case "":
	                               alert("保存失败!")
	                               break;
	                           case "-2":
	                               alert("线路名已存在!");
	                               break;
	                           default:
	                               alert("保存成功!");
	                               topTab.remove(topTab.activeTabIndex)
	                               if (AddTourism.UploadSucessCallbackData != null) {
	                                   var url = $(AddTourism.UploadSucessCallbackData).attr("ret")
	                                   if (url != "/printpage/linetourinfo.aspx") {
	                                       topTab.open(encodeURI(url + "&routeid=" + result + "&routename=" + Key.find("#<%=txt_LineName.ClientID %>").val()), $(AddTourism.UploadSucessCallbackData).attr("toptitle"), {isOpen:false});
	                                   }
	                                   else {
	                                       window.open("/printpage/linetourinfo.aspx?RouteId=" + result)
	                                   }
	                                   //topTab.open("/routeagency/routemanage/routeview.aspx", "我的线路库", {});
	                               }
	                               break;
	                       }
	                   },
	                   error: function() {
	                       alert("操作失败!");
	                   }
	               });
            }
        },
        //生成行程
        GenerateTravel: function(obj) {
            //生成个数
            var travelCount = parseInt($(obj).val());
            if (travelCount > 0) {
                //已经存在的个数
                var AlreadyExists = $("#tab_Standard").find(".td_index").length;
                //新输入的天数大于已经存在的个数，补全插入
                if (travelCount > AlreadyExists) {
                    AddTourism.WhileAppend(travelCount - AlreadyExists);
                }
                //新输入的天数小于已经存在的个数，从末尾开始删除
                if (travelCount < AlreadyExists) {
                    AddTourism.WhileDel(AlreadyExists - travelCount, travelCount);
                }
                //循环插入

                $("#tab_Standard").find(".td_index").each(function(i) {
                    $(this).html("D" + (i + 1))
                })
            }
            else {
                alert("旅游天数格式错误!");
            }
        },
        //插入
        WhileAppend: function(travelCount/*插入个数*/) {
            var standard = $("#tab_Standard tbody");
            while (travelCount--) {
                standard.append(AddTourism.GetObj($("#tr_xcContent").clone()));
                standard.append(AddTourism.GetObj($("#tr_xcRemarks").clone()));
            }
        },
        //从末尾开始删除
        WhileDel: function(delCount/*删除个数*/, travelCount/*删除起始位置*/) {
            var standard = $("#tab_Standard tbody");
            while (delCount--) {
                standard.find("tr").eq(travelCount * 2 + 1).remove();
                standard.find("tr").eq(travelCount * 2 + 1).remove();
            }
        },
        GetObj: function(obj) {
            return $(obj).removeAttr("id");
        },
        BoxyCallBack: function() {
            var Key = $("#<%=Key %>");
            if (Key.find(".cfCity[value='" + Key.find("#<%=hd_goCity.ClientID %>").val().split('|')[0] + "']").length > 0) {
                Key.find(".cfCity[value='" + Key.find("#<%=hd_goCity.ClientID %>").val().split('|')[0] + "']").attr("checked", "checked");
            }
            else {
                Key.find("#<%=hd_goCity.ClientID %>").val("");
            }

            if (Key.find(".fhCity[value='" + Key.find("#<%=hd_returnCity.ClientID %>").val().split('|')[0] + "']").length > 0) {
                Key.find(".fhCity[value='" + Key.find("#<%=hd_returnCity.ClientID %>").val().split('|')[0] + "']").attr("checked", "checked");
            }
            else {
                Key.find("#<%=hd_returnCity.ClientID %>").val("");
            }
            Key.find(".cfCity,.fhCity").click(function() {
                AddTourism.SaveGoOrReturnCity(this);
            })

        },
        TestData: function() {
            var Key = $("#<%=Key %>");
            var str = "";
            Key.find(".txt_Way").each(function() {
                if ($(this).val().length <= 0) {
                    str += "- 请完整填写标准版行程的 途径 \n";
                    return false;
                }
            })
            Key.find(".sel_Traffic").each(function() {
                if ($(this).val() <= 0) {
                    str += "- 请选择标准版行程的 交通工具 \n";
                    return false;
                }
            })
//            Key.find(".txt_Stay").each(function() {
//                if ($(this).val().length <= 0) {
//                    str += "- 请完整填写标准版行程的 住宿 \n";
//                    return false;
//                }
//            })
            Key.find(".txt_Travel").each(function() {
                if ($(this).val().length <= 0) {
                    str += "- 请完整填写标准版行程的 行程内容 \n";
                    return false;
                }
            })
            return str
        },
        QZNull:function(obj){
        var Key=$(obj).closest(".Max")
            if ($(obj).attr("checked")) {
                Key.find("#sp_qz .chk_QZ")
                    .attr("checked", "")
                    .attr("disabled", "disabled")
                Key.find("#<%=hd_qz.ClientID %>").val("");
            }
            else
            {
                Key.find("#sp_qz .chk_QZ").attr("disabled", "")
            }
        },
        QZ: function(key) {
            var Key = $("#"+key);
            var str = "";
            var hd_qz = Key.find("#<%=hd_qz.ClientID %>");
            //生成签证复选框
            Key.find(".chk_BrowseCountrys:checked").each(function() {
                str += "<input type=\"checkbox\" class=\"chk_QZ\" value=\"" + $(this).val() + "\" txt=\"" + $(this).attr("txt") + "\" /><b>" + $(this).attr("txt") + "</b>" + "签证,"
            })
            Key.find("#sp_qz").html(str.substring(0, str.length - 1));
            //已选签证项
            var qzstr = Key.find("#<%=hd_qz.ClientID %>").val().split(',');
            //从新生成的签证项 选中 已选签证项
            if (Key.find("#<%=hd_qz.ClientID %>").val().length>0&&qzstr.length > 0) {
                var i = qzstr.length;
                while (i--) {
                    Key.find(".chk_QZ[value='" + qzstr[i] + "']").attr("checked", "checked")
                }
                str = "";
                /*此段代码用于某国签证已选但是从主要浏览国家去除后任存在签证信息*/
                //重新取签证数据
                Key.find("#sp_qz .chk_QZ:checked").each(function() {
                    str += $(this).val() + ",";
                })
                //重新保存签证数据
                hd_qz.val(str);
                /**********************************************************************/
            }
            else
            {
                if(Key.find("#<%= chk_null.ClientID %>").attr("checked")=="checked")
                {
                    Key.find("#<%= chk_null.ClientID %>").attr("checked","checked")
                    AddTourism.QZNull(Key.find("#<%= chk_null.ClientID %>"))
                }
            }
            //签证复选框选中
            Key.find(".chk_QZ").click(function() {
                str = "";
                //取签证数据
                Key.find("#sp_qz .chk_QZ:checked").each(function() {
                    str += $(this).val() + ",";
                })
                //保存签证数据
                hd_qz.val(str);
            })
        }
        
       
    }

    $(function() {
        FV_onBlur.initValid($(".basic_btn").closest("form").get(0));
        var Key = $("#<%=Key %>")
        Key.find(".cfCity,.fhCity").click(function() {
            AddTourism.SaveGoOrReturnCity(this);
        })
        Key.find(".basic_btn").click(function() {
            AddTourism.FilePicUpload(this);
            return false;
        })
        if (Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>").val().length > 0) {
            AddTourism.KEInit(Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>"));
        }
        Key.find("#<%=txt_AddTourismBriefnessTravel.ClientID %>").focus(function() {
            AddTourism.KEInit(this);
        })
        //行程(用餐)
        Key.find(".hd_Dining").each(function() {
            var str = $(this).val().split(',');
            $(this).val($.trim(str[0] == "True" ? "1," : "") + $.trim(str[1] == "True" ? "2," : "") + $.trim(str[2] == "True" ? "3" : ""))
            var parent = $(this).parent();
            if (str[0] == "True") {
                parent.find(".chk_Breakfast").attr("checked", "checked");
            }
            if (str[1] == "True") {
                parent.find(".chk_Lunch").attr("checked", "checked");
            }
            if (str[2] == "True") {
                parent.find(".chk_Dinner").attr("checked", "checked");
            }
        })
        //行程(交通)
        Key.find(".hd_Traffic").each(function() {
            var parent = $(this).parent();
            parent.find(".sel_Traffic").val(parseInt($(this).val()))
        })
        //出发城市
        if (Key.find("#<%=hd_goCity.ClientID %>").val() != null && Key.find("#<%=hd_goCity.ClientID %>").val().length > 0) {
            var id = Key.find("#<%= hd_goCity.ClientID %>").val().split('|')[0];
            var name = Key.find("#<%= hd_goCity.ClientID %>").val().split('|')[1];
            var checkedObj = Key.find(".cfCity[value='" + id + "']");
            if (checkedObj.length > 0) {
                checkedObj.attr("checked", "checked");
            }
            else {

                Key.find("#<%=hd_goCity.ClientID %>").parent().children("span").append("<input class=\"cfCity\" checked=\"checked\" name=\"cfCity\" type=\"radio\" value=" + id + " cname=" + name + "/>" + name)
                Key.find("#<%=hd_goCity.ClientID %>").parent().children("span input.cfCity").click(function() {
                    AddTourism.SaveGoOrReturnCity(this);
                })
            }
        }
        //返回城市
        if (Key.find("#<%=hd_returnCity.ClientID %>").val() != null && Key.find("#<%=hd_returnCity.ClientID %>").val().length > 0) {
            var id = Key.find("#<%= hd_returnCity.ClientID %>").val().split('|')[0];
            var name = Key.find("#<%= hd_returnCity.ClientID %>").val().split('|')[1];
            var checkedObj = Key.find(".fhCity[value='" + id + "']");
            if (checkedObj.length > 0) {
                checkedObj.attr("checked", "checked");
            }
            else {
                Key.find("#<%=hd_returnCity.ClientID %>").parent().children("span").append("<input class=\"fhCity\" checked=\"checked\" name=\"fhCity\" type=\"radio\" value=" + id + " cname=" + name + "/>" + name)
                Key.find("#<%=hd_returnCity.ClientID %>").parent().children("span input.fhCity").click(function() {
                    AddTourism.SaveGoOrReturnCity(this);
                })
            }
        }
        //线路主题
        if (Key.find("#<%=hd_SubjectLine.ClientID %>").val() != null && Key.find("#<%=hd_SubjectLine.ClientID %>").val().length > 0) {
            var str = Key.find("#<%=hd_SubjectLine.ClientID %>").val();
            var arr = str.split('|');
            var i = arr.length;
            while (i--) {
                Key.find(".inp_SubjectLine[value='" + arr[i].split(',')[0] + "']").attr("checked", "checked");
            }
        }
        //销售区域
        if (Key.find("#<%=hd_SellCity.ClientID %>").val() != null && Key.find("#<%=hd_SellCity.ClientID %>").val().length > 0) {
            var str = Key.find("#<%=hd_SellCity.ClientID %>").val();
            var arr = str.split('|');
            var i = arr.length;
            while (i--) {
                Key.find(".chk_SellCity[value='" + arr[i].split(',')[0] + "']").attr("checked", "checked");
            }
        }
        //主要浏览城市
        if ('<%=travelRangeType %>' == '1') {
            if ($("#<%=hd_BrowseCountrys.ClientID %>").val().length > 0) {
                arr = $("#<%=hd_BrowseCountrys.ClientID %>").val().split(',');
                i = arr.length;
                while (i--) {
                    Key.find("#sp_BrowseCountrys input[value='" + arr[i] + "']").attr("checked", "checked");
                }
                AddTourism.QZ('<%=Key %>');
            }
        }
        else {
            if ($("#<%=hd_BrowseCitys.ClientID %>").val().length > 0) {
                arr = $("#<%=hd_BrowseCitys.ClientID %>").val().split(',');
                i = arr.length;
                while (i--) {
                    if (arr[i].split('|').length > 0) {
                        Key.find("#sp_BrowseCitys input[value='" + arr[i].split('|')[0] + "']").attr("checked", "checked");
                    }
                    else {
                        Key.find("#sp_BrowseCitys input[value='" + arr[i] + "']").attr("checked", "checked");
                    }
                }

            }
        }
        //签证
        if (Key.find(".chk_BrowseCountrys").length > 0) {
            //主要浏览国家
            Key.find(".chk_BrowseCountrys").click(function() {
                AddTourism.QZ('<%=Key %>');
                AddTourism.QZNull(Key.find("#<%= chk_null.ClientID %>"));
                
            })
        }
        Key.find(".inp_SubjectLine").click(function() {
            if ($(this).attr("checked")) {
                if (Key.find(".inp_SubjectLine:checked").length > 4) {
                    alert("线路主题只能选择4个!")
                    return false;
                }
            }
        })
        //无签证
        Key.find("#<%= chk_null.ClientID %>").click(function() {
            AddTourism.QZNull(this);
        })
        //销售区域全选
        Key.find("#chk_AllSellCity").click(function(){
            $(this).parent("td").find(".chk_SellCity").attr("checked",$(this).attr("checked"));
        })
        
        AddTourism.QZNull(Key.find("#<%= chk_null.ClientID %>"));
    })
</script>
</asp:content>
