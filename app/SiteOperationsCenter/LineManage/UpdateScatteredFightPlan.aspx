<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UpdateScatteredFightPlan.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.UpdateScatteredFightPlan" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/RouteAgency/TourServiceStandard.ascx" TagName="ServiceStandard"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>线路管理-添加线路</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTour") %>"></script>

    <script type="text/javascript" src="/kindeditor/kindeditor.js"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("CommonTourModule") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <%--    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'UpdateScatteredFightPlan'
        });
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div id="<%=Key %>" class="right">
        <div class="tablebox">
            <%if (!isAllUpdata)
              { %>
            <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%;">
                <tr>
                    <td colspan="4" align="left" valign="top">
                        <img src="<%=ImgURL %>/images/jichu2.gif" />
                    </td>
                </tr>
                <tr>
                    <td width="16%" align="right" bgcolor="#f2f9fe">
                        出发时间：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label runat="server" ID="lbl_LeaveDate"></asp:Label>
                    </td>
                    <td width="15%" align="right" bgcolor="#F2F9FE">
                        报名截止：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox runat="server" ID="txt_registrationEndDate" onfocus="WdatePicker();"
                            size="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        成人：
                    </td>
                    <td align="left">
                        市场价
                        <asp:TextBox runat="server" ID="txt_retailAdultPrice" size="6"></asp:TextBox>
                        结算价
                        <asp:TextBox runat="server" ID="txt_settlementAudltPrice" size="6"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#F2F9FE">
                        儿童：
                    </td>
                    <td align="left">
                        市场价
                        <asp:TextBox runat="server" ID="txt_retailChildrenPrice" size="6"></asp:TextBox>
                        结算价
                        <asp:TextBox runat="server" ID="txt_settlementChildrenPrice" size="6"></asp:TextBox>
                        （元/人）
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        单房差：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txt_marketPrice" size="10"></asp:TextBox>
                        （元/人）
                    </td>
                    <td align="right" bgcolor="#F2F9FE">
                        团队：
                    </td>
                    <td align="left">
                        计划收客人数
                        <asp:TextBox runat="server" ID="txt_orderPeopleNum" size="6"></asp:TextBox>
                        <%if (!isShowMoreThan)
                          {%>
                        余位
                        <asp:TextBox runat="server" ID="txt_moreThan" size="6"></asp:TextBox>
                        <%} %>
                        留位
                        <asp:TextBox runat="server" ID="txt_saveNum" size="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        出发班次时间：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" size="30" ID="txt_startDate"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap" bgcolor="#F2F9FE">
                        返回班次时间：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" size="30" ID="txt_endDate"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        集合说明：
                    </td>
                    <td align="left">
                        <textarea id="txt_setDec" runat="server" cols="45" rows="2"></textarea>
                    </td>
                    <td align="right" bgcolor="#F2F9FE">
                        状态：
                    </td>
                    <td align="left">
                        <asp:DropDownList runat="server" ID="ddl_powderTourStatus">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" bgcolor="#f2f9fe">
                        领队全陪说明：
                    </td>
                    <td align="left">
                        <textarea id="txt_teamLeaderDec" runat="server" cols="45" rows="3"></textarea>
                    </td>
                    <td align="right" bgcolor="#F2F9FE">
                        团队备注：
                    </td>
                    <td align="left">
                        <textarea id="txt_tourNotes" runat="server" cols="45" rows="3"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        团队推荐：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:DropDownList ID="ddl_Status" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        发布时间IP：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:Literal ID="litTime" runat="server"></asp:Literal>
                        IP:<asp:Literal ID="litIP" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <%} %>
            <input type="hidden" id="hd_tourIds" runat="server" />
            <table border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
                style="width: 100%;">
                <tr>
                    <td colspan="2" align="left" valign="top">
                        <img src="<%=ImgURL %>/images/jiben3.gif" />
                    </td>
                </tr>
                <tr>
                    <td width="16%" align="right" bgcolor="#f2f9fe">
                        专线类型：
                    </td>
                    <td align="left">
                        <asp:Label runat="server" ID="lbl_travelRangeName" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 线路名称：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txt_LineName" size="45" valid="required" errmsg="请填写线路名称"></asp:TextBox>
                        <span id="errMsg_<%=txt_LineName.ClientID %>" class="errmsg"></span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        发布专线商：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <input id="txtPublishersName" name="txtPublishersName" runat="server" />
                        全称（易诺管理员控制）
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        发布个人账户：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:DropDownList ID="dropPublisher" runat="server">
                        </asp:DropDownList>
                        <asp:HiddenField ID="hiddropPublisher" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        B2B显示控制：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:DropDownList ID="dropB2B" runat="server">
                        </asp:DropDownList>
                        <input id="txt_B2B" runat="server" size="10" name="txt_B2B" value="50" valid="required|limit"
                            max="100" />
                        （1~50）正向排序，默认50（易诺管理员控制）
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        B2C显示控制：
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:DropDownList ID="dropB2C" runat="server">
                        </asp:DropDownList>
                        <input id="txt_B2C" size="10" name="txt_B2C" runat="server" max="100" value="50" />
                        （1~50）正向排序，默认50（易诺管理员控制）
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        大交通：
                    </td>
                    <td align="left">
                        出发：
                        <asp:DropDownList runat="server" ID="ddl_DepartureTraffic">
                            <asp:ListItem Value="-1">-请选择-</asp:ListItem>
                        </asp:DropDownList>
                        返回：
                        <asp:DropDownList runat="server" ID="ddl_ReturnTraffic">
                            <asp:ListItem Value="-1">-请选择-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <%if (citysOrCountrys)
                  {%>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 主要游览国家：
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <input type="hidden" id="hd_BrowseCountrys" runat="server" valid="required" errmsg="请选择主要浏览国家" />
                                    <span id="sp_BrowseCountrys">
                                        <asp:Repeater ID="rpt_VisitingNational" runat="server">
                                            <ItemTemplate>
                                                <input type="checkbox" class="chk_BrowseCountrys" txt="<%#Eval("CName") %>" value="<%#Eval("CountryId") %>" /><%#Eval("CName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>（<font class="ff0000">如果缺少选项，请电话客服添加区域</font>） <span id="errMsg_<%=hd_BrowseCountrys.ClientID %>"
                                        class="errmsg"></span>
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
                                    <input runat="server" type="checkbox" id="chk_null" />免签
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
                        <asp:TextBox ID="txt_AdultDeposit" size="10" runat="server"></asp:TextBox>
                        元 儿童
                        <asp:TextBox runat="server" ID="txt_ChildrenDeposit" size="10"></asp:TextBox>
                        元 <font class="ff0000">（输入0 表示无需支付定金）</font>
                    </td>
                </tr>
                <%}
                  else
                  {
                %>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 主要游览城市：
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <input type="hidden" id="hd_BrowseCitys" valid="required" errmsg="请选择主要浏览城市" runat="server" />
                                    <span id="sp_BrowseCitys">
                                        <asp:Repeater runat="server" ID="rpt_browseCitys">
                                            <ItemTemplate>
                                                <input type="checkbox" class="chk_BrowseCitys" value="<%#Eval("CityId") %>" /><%#Eval("CityName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="rpt_districtCounty">
                                            <ItemTemplate>
                                                <input type="checkbox" class="chk_DistrictCounty" cityid='<%#Eval("CityId") %>' value="<%#Eval("Id") %>" /><%#Eval("DistrictName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span><span id="errMsg_<%=hd_BrowseCitys.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%  }%>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        线路特色：
                    </td>
                    <td align="left">
                        <textarea id="txt_LineFeatures" cols="65" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 天数：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txt_days" size="6" onblur="UpdateScatteredFightPlan.GenerateTravel(this)"
                            value="1"></asp:TextBox>
                        天
                        <asp:TextBox runat="server" ID="txt_nights" size="6"></asp:TextBox>
                        晚 （<font class="ff0000">住宿天数</font>）
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 线路主题：
                    </td>
                    <td align="left">
                        <input type="hidden" id="hd_SubjectLine" runat="server" valid="required" errmsg="请选择线路主题" />
                        <asp:Repeater runat="server" ID="rpt_SubjectLine">
                            <ItemTemplate>
                                <input type="checkbox" class="inp_SubjectLine" value="<%#Eval("FieldId") %>" nameval="<%#Eval("FieldName")%>" /><%#Eval("FieldName")%>
                            </ItemTemplate>
                        </asp:Repeater>
                        <span id="errMsg_<%=hd_SubjectLine.ClientID %>" class="errmsg"></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 出发城市：
                    </td>
                    <td align="left">
                        <input type="hidden" runat="server" id="hd_goCity" valid="required" errmsg="请选择出发城市" />
                        <table>
                            <tr id="tdBindLeaveCity">
                                <td>
                                    <span>
                                        <asp:Repeater ID="rpt_DepartureCity" runat="server">
                                            <ItemTemplate>
                                                <input class="cfCity" name="cfCity" type="radio" value="<%#Eval("CityId") %>" cname="<%#Eval("CityName")%>"
                                                    onclick="UpdateScatteredFightPlan.SaveGoOrReturnCity(this,'LeaveCity')" /><%#Eval("CityName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <a href="javascript:void(0)" id="a_goSet" onclick="UpdateScatteredFightPlan.SetCity(this)">
                            <span class="huise">更多</span><span id="errMsg_<%=hd_goCity.ClientID%>" class="errmsg"></span></a>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 返回城市：
                    </td>
                    <td align="left">
                        <input type="hidden" runat="server" id="hd_returnCity" valid="required" errmsg="请选择返回城市" />
                        <table>
                            <tr id="tdBindBackCity">
                                <td>
                                    <span>
                                        <asp:Repeater ID="rpt_BackToCities" runat="server">
                                            <ItemTemplate>
                                                <input class="fhCity" name="fhCity" type="radio" value="<%#Eval("CityId") %>" cname="<%#Eval("CityName")%>"
                                                    onclick="UpdateScatteredFightPlan.SaveGoOrReturnCity(this,'BackCity')" /><%#Eval("CityName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <a href="javascript:void(0)" id="a_retnrnSet" onclick="UpdateScatteredFightPlan.SetCity(this)">
                            <span class="huise">更多</span><span id="errMsg_<%=hd_returnCity.ClientID%>" class="errmsg"></span></a>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" bgcolor="#f2f9fe" style="padding-top: 50px;">
                        行程：
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <div id="tb_" class="tb_">
                                        <ul>
                                            <li id="tb_1" class="<%=isTravel.ToString()=="True"?"normaltab":"hovertab" %>" xctype="SAE"
                                                onclick="UpdateScatteredFightPlan.ShownSeparately('#tbc_01','.xc',this)"><a>简易版</a></li>
                                            <li id="tb_2" class="<%=isTravel.ToString()=="True"?"hovertab":"normaltab" %>" xctype="Standard"
                                                onclick="UpdateScatteredFightPlan.ShownSeparately('#tbc_02','.xc',this)"><a>标准版</a></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 80%;">
                                        <input type="hidden" id="hd_TravelContent" name="hd_TravelContent" value="<%=isTravel.ToString()=="True"?"Standard":"SAE" %>" />
                                        <div class="<%=isTravel.ToString()=="True"?"undis":"dis" %> xc" id="tbc_01">
                                            <textarea id="txt_BriefnessTravel" runat="server" cols="85" rows="5">编辑器</textarea></div>
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
                                                        <input type="text" size="15" name="txt_Way" />
                                                    </td>
                                                    <td align="center">
                                                        <select name="sel_Traffic">
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
                                                        <input type="text" name="txt_Stay" size="15" />
                                                    </td>
                                                    <td align="center">
                                                        <input type="hidden" name="hd_Dining" />
                                                        <input type="checkbox" name="chk_Breakfast" value="1" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                        早
                                                        <input type="checkbox" name="chk_Lunch" value="2" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                        中
                                                        <input type="checkbox" name="chk_Dinner" value="3" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                        晚
                                                    </td>
                                                </tr>
                                                <tr id="tr_xcRemarks">
                                                    <td colspan="4" align="left">
                                                        <textarea name="txt_Travel" cols="100" rows="4"></textarea>
                                                    </td>
                                                </tr>
                                                <%}
                                                  else
                                                  {  %>
                                                <asp:Repeater runat="server" ID="rpt_standardPlans">
                                                    <ItemTemplate>
                                                        <tr id="tr1">
                                                            <td rowspan="2" align="center" class="td_index">
                                                                D<%#Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="center">
                                                                途径：
                                                                <input type="text" size="15" name="txt_Way" value="<%#Eval("PlanInterval") %>" />
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
                                                                <input type="text" name="txt_Stay" value="<%#Eval("House") %>" size="15" />
                                                            </td>
                                                            <td align="center">
                                                                <input type="hidden" class="hd_Dining" name="hd_Dining" value="<%#Eval("Early")%>,<%#Eval("Center")%>,<%#Eval("Late")%>" />
                                                                <input type="checkbox" class="chk_Breakfast" name="chk_Breakfast" value="1" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                                早
                                                                <input type="checkbox" class="chk_Lunch" name="chk_Lunch" value="2" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                                中
                                                                <input type="checkbox" class="chk_Dinner" name="chk_Dinner" value="3" onclick="UpdateScatteredFightPlan.SetDining(this)" />
                                                                晚
                                                            </td>
                                                        </tr>
                                                        <tr id="tr2">
                                                            <td colspan="4" align="left">
                                                                <textarea name="txt_Travel" cols="100" rows="4"><%#Eval("PlanContent")%></textarea>
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
                        报价包含：
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <div id="tb1_" class="tb1_">
                                        <ul>
                                            <%--class="hovertab" --%><li id="tb1_1" class="<%=isServiceStandard.ToString()=="True"?"normaltab":"hovertab" %>"
                                                bhtype="FIT" onclick="UpdateScatteredFightPlan.ShownSeparately('#tbc1_01','.bj',this)">
                                                <a>简易版</a></li>
                                            <%-- class="normaltab"--%><li id="tb1_2" class="<%=isServiceStandard.ToString()=="True"?"hovertab":"normaltab" %>"
                                                bhtype="Team" onclick="UpdateScatteredFightPlan.ShownSeparately('#tbc1_02','.bj',this)">
                                                <a>标准版</a></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 80%;">
                                        <input type="hidden" id="hd_FITOrTeam" name="hd_FITOrTeam" value="<%=isServiceStandard.ToString()=="True"?"Team":"FIT" %>" />
                                        <div class="<%=isServiceStandard.ToString()=="True"?"undis1":"dis1" %> bj" id="tbc1_01">
                                            <textarea id="txt_FIT" cols="85" rows="5" runat="server"></textarea></div>
                                        <div class="<%=isServiceStandard.ToString()=="True"?"dis1":"undis1" %> bj" id="tbc1_02">
                                            <cc1:ServiceStandard ID="AddStandardRoute_ServiceStandard" runat="server" ModuleType="route"
                                                ReleaseType="AddStandardRoute" />
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
                        <textarea id="txt_PriceExcluding" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        赠送项目：
                    </td>
                    <td align="left">
                        <textarea id="txt_GiftItems" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        儿童及其他安排：
                    </td>
                    <td align="left">
                        <textarea id="txt_OtherArr" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        购物安排：
                    </td>
                    <td align="left">
                        <textarea id="txt_ShoppingArr" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        自费项目：
                    </td>
                    <td align="left">
                        <textarea id="txt_ThisConsumption" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        销售商须知：
                    </td>
                    <td align="left">
                        <textarea id="txt_Notes" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        备注：
                    </td>
                    <td align="left">
                        <textarea id="txt_Remarks" cols="85" rows="5" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#f2f9fe">
                        <font class="ff0000">*</font> 销售区域：
                    </td>
                    <td align="left">
                        <asp:Repeater runat="server" ID="rpt_SalesArea">
                            <ItemTemplate>
                                <input type="checkbox" class="chk_SellCity" value="<%#Eval("CityId") %>" pid="<%#Eval("ProvinceId") %>"
                                    pname="<%#Eval("ProvinceName") %>" cname="<%#Eval("CityName")%>" /><%#Eval("CityName")%>
                            </ItemTemplate>
                        </asp:Repeater>
                        <input type="hidden" name="hd_SellCity" id="hd_SellCity" runat="server" valid="required"
                            errmsg="请选择销售区域" />
                        <span id="errMsg_<%=hd_SellCity.ClientID %>" class="errmsg"></span><span id="sp_SalesAreaMsg"
                            runat="server">暂无销售区域</span>
                    </td>
                </tr>
                <tr>
                    <td height="35" colspan="2" align="center">
                        <input type="button" runat="server" value="团队修改" class="basic_btn" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/JavaScript">

        var UpdateScatteredFightPlan = {
            KEInit: function(obj) {
                var id = $(obj).attr("id")
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
            SetCity: function(obj,t) {
                var url = "/LineManage/SetLeaveCity.aspx";
                url += "?ContainerID=" + $(obj).attr("id");
                url += "&CompanyId=<%=companyid %>";
                url += "&type="+t;
                url += "&inputclass=" + $(obj).parent().find("input:eq(1)").attr("class");
                url += "&callBack=UpdateScatteredFightPlan.BoxyCallBack"
                url += "&Key=<%=Key %>&GetType=radio";
                url += "&rnd=" + Math.random();
                TourModule.OpenDialog('设置常用出港城市', url, 650, 450);
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
                    var Key = $("#<%=Key %>");
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
            Save: function(obj) {
                var str = "";
                var form = $(obj).closest("form").get(0);
                var Key = $(obj).closest("#<%=Key %>");
                if (Key.find("#hd_TravelContent").val() == "SAE" && Key.find("#<%=txt_BriefnessTravel.ClientID %>").css("display") == "none") {
                    //行程编辑框
                    Key.find("#<%=txt_BriefnessTravel.ClientID %>").val(KE.html("<%=txt_BriefnessTravel.ClientID %>"))
                }
                //验证行程
                if (Key.find("#hd_TravelContent").val() == "SAE") {
                    //简易版行程验证
                    if (Key.find("#<%=txt_BriefnessTravel.ClientID %>").val().length <= 0) {
                        str += "- 请输入简易版行程!\n";
                    }
                }
                else {
                    //标准版行程验证
                    str += UpdateScatteredFightPlan.TestData();
                }
                if (Key.find("#hd_FITOrTeam").val() == "FIT" && Key.find("#<%=txt_FIT.ClientID %>").css("display") == "none") {
                    //散客报价包含编辑框
                    Key.find("#<%=txt_FIT.ClientID %>").val(KE.html("<%=txt_FIT.ClientID %>"))
                }
                //验证报价包含
                if (Key.find("#hd_FITOrTeam").val() == "FIT" && Key.find("#<%=txt_FIT.ClientID %>").val().length <= 0) {
                    str += "- 请输入常规散客报报价包含!";
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

                if ('<%=citysOrCountrys%>' == "True") {
                    str = "";
                    //主要浏览国家
                    if(Key.find("#sp_BrowseCountrys input[type='checkbox']").length>0)
                    {
                        Key.find("#sp_BrowseCountrys input:checked").each(function() {
                            if ($(this).attr("class") == "chk_DistrictCounty") {
                                //县id|城市id
                                str += $(this).val() + "|" + $(this).attr("cityid") + ",";
                            } else {
                                str += $(this).val() + ","
                            }
                        })
                        Key.find("#<%=hd_BrowseCountrys.ClientID %>").val(str.substring(0, str.length - 1));
                    }
                    else
                    {
                        alert("请联系管理员为该专线添加主要浏览国家");
                        return false;
                    }
                }
                else {
                    str = "";
                    //主要浏览城市
                    if(Key.find("#sp_BrowseCitys input[type='checkbox']").length>0)
                    {
                        Key.find("#sp_BrowseCitys input:checked").each(function() {
                            str += $(this).val() + ","
                        })
                        Key.find("#<%=hd_BrowseCitys.ClientID %>").val(str.substring(0, str.length - 1));
                    }
                    else
                    {
                        alert("请联系管理员为该专线添加主要浏览城市");
                        return false;
                    }
                }

                if (ValiDatorForm.validator(form, "alert")) {
                //中间值 选中的发布账号
                var dropvalue=$("#dropPublisher").val();
                $("#hiddropPublisher").val(dropvalue);
                $("#dropPublisher").html("");
                    $.newAjax(
	               {
	                   url: '/LineManage/UpdateScatteredFightPlan.aspx?isAllUpdata=<%=isAllUpdata %>&Operating=UpSave&tourId=<%=tourId %>',
	                   data: $(form).serialize(),
	                   dataType: "html",
	                   cache: false,
	                   type: "POST",
	                   success: function(result) {
	                       if (result.toLowerCase() == "true") {
	                           alert("修改成功!");
	                           if ('<%=isAllUpdata %>' == "True") {
	                               window.location.href="ScatteredfightList.aspx?routeId=<%=routeId %>";
	                           }
	                           else {
	                               window.location.href="ScatteredfightList.aspx";
	                           }
	                       }
	                       else {
	                           alert("修改失败！")
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
                        UpdateScatteredFightPlan.WhileAppend(travelCount - AlreadyExists);
                    }
                    //新输入的天数小于已经存在的个数，从末尾开始删除
                    if (travelCount < AlreadyExists) {
                        UpdateScatteredFightPlan.WhileDel(AlreadyExists - travelCount, travelCount);
                    }
                    //循环插入

                    $("#tab_Standard").find(".td_index").each(function(i) {
                        $(this).html("D" + (i + 1))
                    })
                }
                else {
                    alert("旅游天数不能为0或空!");
                    $(obj).val(1);
                }
            },
            //插入
            WhileAppend: function(travelCount/*插入个数*/) {
                var standard = $("#tab_Standard tbody");
                while (travelCount--) {
                    standard.append(UpdateScatteredFightPlan.GetObj($("#tr_xcContent").clone()));
                    standard.append(UpdateScatteredFightPlan.GetObj($("#tr_xcRemarks").clone()));
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
                    UpdateScatteredFightPlan.SaveGoOrReturnCity(this);
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
                Key.find(".txt_Stay").each(function() {
                    if ($(this).val().length <= 0) {
                        str += "- 请完整填写标准版行程的 住宿 \n";
                        return false;
                    }
                })
                Key.find(".txt_Travel").each(function() {
                    if ($(this).val().length <= 0) {
                        str += "- 请完整填写标准版行程的 行程内容 \n";
                        return false;
                    }
                })
                return str
            },
            QZNull: function(obj) {
                var Key = $("#<%=Key %>")
                if ($(obj).attr("checked")) {
                    Key.find("#sp_qz .chk_QZ")
                    .attr("checked", "")
                    .attr("disabled", "disabled")
                    Key.find("#<%=hd_qz.ClientID %>").val("");
                }
                else {
                    Key.find("#sp_qz .chk_QZ").attr("disabled", "")
                }
            },
            QZ: function() {
                var Key = $("#<%=Key %>");
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
                if (Key.find("#<%=hd_qz.ClientID %>").val().length > 0 && qzstr.length > 0) {
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
                    hd_qz.val(str.substring(0, str.length - 1));
                    /**********************************************************************/
                }
                else {
                    Key.find("#<%= chk_null.ClientID %>").attr("checked", "checked")
                    UpdateScatteredFightPlan.QZNull(Key.find("#<%= chk_null.ClientID %>"))
                }
                //签证复选框选中
                Key.find(".chk_QZ").click(function() {
                    str = "";
                    //取签证数据
                    Key.find("#sp_qz .chk_QZ:checked").each(function() {
                        str += $(this).val() + ",";
                    })
                    //保存签证数据
                    hd_qz.val(str.substring(0, str.length - 1));
                })
            }
        }
        
        //获取帐号
        function GetOperator(v, s) {
        $.ajax({
                url: "UpdateScatteredFightPlan.aspx?argument=" + v,
                cache: false,
                type: "POST",
                dataType: "json",
                success: function(result) {
                            var listContact = result.tolist;
                            $("#<%=dropPublisher.ClientID %>").html("");
                            $("#<%=dropPublisher.ClientID %>").append("<option value=\"0\">请选择</option>");
                            for (var x = 0; x < listContact.length; x++) {
                                $("#<%=dropPublisher.ClientID %>").append("<option value=\"" + listContact[x].ID + "\">" + listContact[x].UserNameID + "</option>");
                            }
                            setTimeout(function() {
                                $("#<%=dropPublisher.ClientID %> option[value='" + s + "']").attr("selected", true);
                            }, 1);
                    },
                error: function() {
                    alert("操作失败!");
                }
            });
        }

        $(function() {
            var Key = $("#<%=Key %>")
            FV_onBlur.initValid(Key.find(".basic_btn").closest("form").get(0));
            Key.find(".basic_btn").click(function() {
                UpdateScatteredFightPlan.Save(this);
                return false;
            })
            if (Key.find("#<%=txt_BriefnessTravel.ClientID %>").val().length > 0) {
                UpdateScatteredFightPlan.KEInit(Key.find("#<%=txt_BriefnessTravel.ClientID %>"));
            }
            if (Key.find("#<%=txt_FIT.ClientID %>").val().length > 0) {
                UpdateScatteredFightPlan.KEInit(Key.find("#<%=txt_FIT.ClientID %>"));
            }
            Key.find("#<%=txt_BriefnessTravel.ClientID %>,#<%=txt_FIT.ClientID %>").focus(function() {
                UpdateScatteredFightPlan.KEInit(this);
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
                    if (id != 0) {
                        Key.find("#<%=hd_goCity.ClientID %>").parent().children("span").append("<input class=\"cfCity\" checked=\"checked\" name=\"cfCity\" type=\"radio\" value=" + id + " cname=" + name + "/>" + name)
                    }
                    else {
                        Key.find("#<%=hd_goCity.ClientID %>").val("");
                    }
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
                    if (id != 0) {
                        Key.find("#<%=hd_returnCity.ClientID %>").parent().children("span").append("<input class=\"fhCity\" checked=\"checked\" name=\"fhCity\" type=\"radio\" value=" + id + " cname=" + name + "/>" + name)
                    }
                    else {
                        Key.find("#<%=hd_returnCity.ClientID %>").val("");
                    }
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
            if ('<%=citysOrCountrys %>' == 'True') {
                if ($("#<%=hd_BrowseCountrys.ClientID %>").val().length > 0) {
                    arr = $("#<%=hd_BrowseCountrys.ClientID %>").val().split(',');
                    i = arr.length;
                    var str = "";
                    while (i--) {
                        Key.find("#sp_BrowseCountrys input[value='" + arr[i] + "']").attr("checked", "checked");
                        str = "<b>" + Key.find("#sp_BrowseCountrys input[value='" + arr[i] + "']").attr("txt") + "</b>" + "签证,"
                    }
                    Key.find("#sp_qz").html(str.substring(0, str.length - 1));
                    Key.find(".chk_BrowseCountrys").click(function() {
                        var str = "";
                        Key.find(".chk_BrowseCountrys:checked").each(function() {
                            str += "<b>" + $(this).attr("txt") + "</b>" + "签证,"
                        })
                        Key.find("#sp_qz").html(str.substring(0, str.length - 1));
                    })
                    UpdateScatteredFightPlan.QZ();
                }
            }
            else if ($("#<%=hd_BrowseCitys.ClientID %>").val().length > 0) {
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
            //签证
            if (Key.find(".chk_BrowseCountrys").length > 0) {
                //主要浏览国家
                Key.find(".chk_BrowseCountrys").click(function() {
                    UpdateScatteredFightPlan.QZ();
                    UpdateScatteredFightPlan.QZNull(Key.find("#<%= chk_null.ClientID %>"));
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
                UpdateScatteredFightPlan.QZNull(this);
            })

            UpdateScatteredFightPlan.QZNull(Key.find("#<%= chk_null.ClientID %>"));
        })
    </script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetCityList") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>

    </form>
</body>
</html>
