<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuList.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.LeftMenuList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>运营后台左侧菜单</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript" type="">
        function setMenuTitle(spanId, imgId) {
            //$("#" + spanId).toggle();
            document.getElementById(spanId).style.display = (document.getElementById(spanId).style.display == 'none') ? '' : 'none';
            var $objImg = $("#" + imgId);
            if ($objImg.attr("src") == "<%=ImageServerUrl%>/images/yunying/button_up.gif") {
                $objImg.attr("src", "<%=ImageServerUrl%>/images/yunying/button_down.gif");
            }
            else if ($objImg.attr("src") == "<%=ImageServerUrl%>/images/yunying/button_down.gif") {
                $objImg.attr("src", "<%=ImageServerUrl%>/images/yunying/button_up.gif");
            }
        }

        function setAcss(id_num, cap) {

            setvalue("当前操作：" + cap);
        }

        var strColumns_Current = "20,*";
        //菜单隐藏rows="20,*"
        function hidetoc() {
            strColumns_Current = top.contset.rows
            top.contset.rows = "1,*";
        }
        //菜单显示
        function showtoc() {
            top.contset.rows = strColumns_Current;
        }

        function mouseovertoc() {
            //      window.status = "隐藏菜单";
            document.all.hidemenu.src = "<%=ImageServerUrl%>/images/yunying/hidetoc2.gif";
        }

        function mouseouttoc() {
            document.all.hidemenu.src = "<%=ImageServerUrl%>/images/yunying/hidetoc1.gif";
        }
        //设置单项选中样式
        function setMenuClass(obj) {
            $("a").each(function() {
                $(this).attr("class", "");
            })
            $(obj).attr("class", "menuon");
        }
        $(function() {
            $("a").click(function() {
                setMenuClass(this);
            });
        });
    </script>

    <style type="text/css">
        body
        {
            background-color: #E6EDF2;
            overflow-x: hidden;
        }
        HTML
        {
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>
</head>
<body>
    <table width="170" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <img src="<%=ImageServerUrl%>/images/yunying/htlogo.gif" width="160" height="21" />
            </td>
        </tr>
    </table>
    <div class="leftmenu" id="divCompanyManage" runat="server">
        <span class="linkcsstitle" id="main0" onclick="javascript: setMenuTitle('spanCompanyManage','CompanyManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>会员管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="CompanyManage" src="<%=ImageServerUrl%>/images/yunying/button_up.gif" name="menutitle0">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanCompanyManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trRegisterCompany" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" />
                        <a href="/CompanyManage/RegisterCompany.aspx" target="mainFrame">注册会员审核</a>
                    </td>
                </tr>
                <tr id="trDefaultCompanyManage" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/Default.aspx" target="mainFrame">旅行社汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther1Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=1" target="mainFrame">景区汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther2Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=2" target="mainFrame">酒店汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther3Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=3" target="mainFrame">车队汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther4Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=4" target="mainFrame">旅游用品汇总管理</a>
                    </td>
                </tr>
                <%-- <tr>
                    <td >
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=5" target="mainFrame">购物点汇总管理</a>
                    </td>
                </tr>--%>
                <tr id="trOther5Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=6" target="mainFrame">机票供应商汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther6Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=7" target="mainFrame">其他采购商汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther7Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/OtherCompany.aspx?CompanyType=11" target="mainFrame">随便逛逛汇总管理</a>
                    </td>
                </tr>
                <tr id="trOther8Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/PersonalMemberList.aspx" target="mainFrame">个人会员管理</a>
                    </td>
                </tr>
                <tr id="trOther9Company" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/CompanyManage/BusinessMemeberList.aspx" target="mainFrame">商家会员管理</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divRouteManage" runat="server">
        <span class="linkcsstitle" id="main1" onclick="javascript: setMenuTitle('spanRouteManage','RouteManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>线路产品管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="RouteManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle2">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanRouteManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr>
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/LineList.aspx" target="mainFrame">线路管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/ScatteredfightList.aspx" target="mainFrame">散拼计划管理</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="sltm01" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/FitList.aspx" target="mainFrame">散客订单管理</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="Img13" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/FitStatistics.aspx" target="mainFrame">散客订单统计</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="Img14" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/TeamorderManage.aspx" target="mainFrame">团队订单管理</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="Img15" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="LineManage/OrderMsgManage.aspx" target="mainFrame">订单短信管理</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divCompanyFile" runat="server" visible="false">
        <span class="linkcsstitle" id="main1" onclick="javascript: setMenuTitle('spanCompanyFile','CompanyFile');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>诚信档案</strong></font>
                    </td>
                    <td width="22">
                        <img id="CompanyFile" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle1">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanCompanyFile" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr>
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" />
                        <a href="cx-list.html" target="mainFrame">发布承诺书审核</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" />
                        <a href="cx-gl-list.html" target="mainFrame">诚信档案管理</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divNewsCenter" runat="server">
        <span class="linkcsstitle" id="main11" onclick="javascript: setMenuTitle('spanNewsCenter','NewsCenter');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>新闻中心</strong></font>
                    </td>
                    <td width="22">
                        <img id="NewsCenter" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle4">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanNewsCenter" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="tr6" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/InformationIndustry.aspx" target="mainFrame">同业资讯</a>
                    </td>
                </tr>
                <tr id="trOperatorNewsPage" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/NewsList.aspx" target="mainFrame">新版新闻管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/NewsAdd.aspx" target="mainFrame">新版添加新闻</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/OperatorNewsPage.aspx" target="mainFrame">添加新闻</a>
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/PictureUpdate.aspx" target="mainFrame">添加焦点图片</a>
                    </td>
                </tr>
                <tr id="trNewsInfoList" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/NewsInfoList.aspx" target="mainFrame">新闻管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/Category.aspx" target="mainFrame">类别管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/Keyword.aspx" target="mainFrame">关键字管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="NewsCenterControl/Tag.aspx" target="mainFrame">Tag管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/LinksInfo.aspx?linkType=4" target="mainFrame">友情链接</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divSupplyManage" runat="server">
        <span class="linkcsstitle" id="main12" onclick="javascript: setMenuTitle('spanSupplyManage','SupplyManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>供求管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="SupplyManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle2">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanSupplyManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr runat="server" id="trSupplyManage">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/SupplierInfo.aspx" target="mainFrame">供求信息 </a>
                    </td>
                </tr>
                <tr runat="server" id="trGuestManage">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/GuestInterview.aspx" target="mainFrame">嘉宾访谈</a>
                    </td>
                </tr>
                <tr runat="server" id="trTongyeSchool">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/SchoolIntroduction.aspx" target="mainFrame">同业学堂</a>
                    </td>
                </tr>
                <tr runat="server" id="trSupDemandBannerImg">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/SupplyFocusBannerImg.aspx" target="mainFrame">供求首页五张焦点图</a>
                    </td>
                </tr>
                <tr runat="server" id="trSupRule">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/SupplyRuleInfo.aspx" target="mainFrame">供求规则（5条）</a>
                    </td>
                </tr>
                <tr runat="server" id="trQInformationManager">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SupplierManage/QInformationManager.aspx" target="mainFrame">Q群信息管理</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divtongye114Manage" runat="server">
        <span class="linkcsstitle" id="main8" onclick="javascript: setMenuTitle('spantongye114Manage','tongye114Manage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>同业114产品管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="tongye114Manage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle8">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spantongye114Manage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr style="display: none">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="taocan.html" target="mainFrame">收费套餐</a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="taocan.html" target="mainFrame">单项购买</a>
                    </td>
                </tr>
                <tr runat="server" id="trMQApplication">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TyProductManage/MQAudit.aspx" target="mainFrame">企业MQ申请审核</a>
                    </td>
                </tr>
                <tr runat="server" id="trHightShop">
                    <td width="1154">
                        <img id="Img1" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TyProductManage/HighShopAuit.aspx" target="mainFrame">高级网店申请审核</a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="shop_dq.html" target="mainFrame">快到期高级商铺</a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="#" target="mainFrame">平台软件用户汇总</a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="#" target="mainFrame">快要到期软件用户</a>
                    </td>
                </tr>
                <tr runat="server" id="trSMSManage">
                    <td>
                        <img id="Img2" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/SMSManage/Default.aspx" target="mainFrame">短信充值审核</a>
                    </td>
                </tr>
                <tr runat="server" id="trPlanerBuyerQuery" visible="false">
                    <td>
                        <img id="Img4" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TyProductManage/PlanerBuyerQuery.aspx" target="mainFrame">机票采购商查询</a>
                    </td>
                </tr>
                <tr runat="server" id="trSendIMMessage" visible="false">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu"
                            alt="" />
                        <a href="/im/message.aspx" target="mainFrame">发送MQ消息</a>
                    </td>
                </tr>
                <tr runat="server" id="trSetGroupMember" visible="true">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu"
                            alt="" />
                        <a href="/im/setgroupmember.aspx" target="mainFrame">设置MQ群人数</a>
                    </td>
                </tr>
                <tr runat="server" id="trLogTicket" visible="true">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu"
                            alt="" />
                        <a href="/others/ticketlogs.aspx" target="mainFrame">机票接口访问记录</a>
                    </td>
                </tr>
                <%--<tr runat="server" id="trLoginedRemind" visible="true">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" alt="" />
                        <a href="/TyProductManage/LoginedRemindList.aspx" target="mainFrame">提醒（登录下面的提醒）</a>
                    </td>
                </tr>--%>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divAdvMange" runat="server">
        <span class="linkcsstitle" id="main4" onclick="javascript: setMenuTitle('spanAdvMange','AdvMange');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>广告管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="AdvMange" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle3">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanAdvMange" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr runat="server" id="trTonye114Adv">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/AdManagement/AdManageHome.aspx" target="mainFrame">114平台广告</a>
                    </td>
                </tr>
                <tr style="display: none" runat="server" id="trMQAdv">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="#" target="mainFrame">MQ广告</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divScenicManage" runat="server">
        <span class="linkcsstitle" id="Span8" onclick="javascript: setMenuTitle('spanScenicManage','ScenicManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>景区</strong></font>
                    </td>
                    <td width="22">
                        <img id="Img10" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle3">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanScenicManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr>
                    <td width="1154">
                        <img id="Img11" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/ScenicManage/ScenicList.aspx" target="mainFrame">景区管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/ScenicManage/ScenicTicket.aspx" target="mainFrame">门票管理</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        门票订单
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divAnalysisManage" runat="server">
        <span class="linkcsstitle" id="main5" onclick="javascript: setMenuTitle('spanAnalysisManage','AnalysisManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>统计分析</strong></font>
                    </td>
                    <td width="22">
                        <img id="AnalysisManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle5">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanAnalysisManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr style="display: none">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="ad-tongji.html" target="mainFrame">广告投放统计</a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="adddq-tongji.html" target="mainFrame">广告到期统计</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154" id="trStaMQ" runat="server">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="Statistics/MQReNew.aspx" target="mainFrame">收费MQ到期统计</a>
                    </td>
                </tr>
                <tr>
                    <td width="1154" id="trStaHighShop" runat="server">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/Statistics/EshopDueDate.aspx" target="mainFrame">收费网店到期统计</a>
                    </td>
                </tr>
                <tr id="tr_SMSRemnantStatistics" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/Statistics/SMSRemnantStatistics/Default.aspx" target="mainFrame">短信余额统计</a>
                    </td>
                </tr>
                <tr id="tr_AgencyActionAnalysis" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/Statistics/AgencyActionAnalysis/Default.aspx" target="mainFrame">组团社行为分析</a>
                    </td>
                </tr>
                <tr id="trZXAgencyActionAnalysis" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="Statistics/LineCompanyAnalysis.aspx" target="mainFrame">专线商行为分析</a>
                    </td>
                </tr>
                <tr id="trNoValidZXAgency" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/Statistics/ValidTourCompanyList.aspx?InValid=false" target="mainFrame">无有效产品批发商</a>
                    </td>
                </tr>
                <tr id="trValidZXAgency" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/Statistics/ValidTourCompanyList.aspx?InValid=true" target="mainFrame">有有效产品批发商</a>
                    </td>
                </tr>
                <%--<tr style="display:none;">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" />
                        <a href="pf-display-list.html" target="mainFrame">平台专线控制显示</a>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" />
                        <a href="pf-MQT-list.html" target="mainFrame">MQ推荐专线</a>
                    </td>
                </tr>--%>
                <tr id="trOnLineAgency" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/Statistics/LineTravelagencyData.aspx" target="mainFrame">在线组团社</a>
                    </td>
                </tr>
                <tr id="trHistoryAgencyLogin" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/Statistics/TradesManHistoryLoginRecordData.aspx" target="mainFrame">零售商历史登录记录</a>
                    </td>
                </tr>
                <tr id="tr7" runat="server">
                    <td>
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif">
                        <a href="/Statistics/LogManagement.aspx" target="mainFrame">日志管理</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divTerraceManage" runat="server">
        <span class="linkcsstitle" id="main6" onclick="javascript: setMenuTitle('spanTerraceManage','TerraceManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>平台管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="TerraceManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle6">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanTerraceManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trBasicInfoManagement" runat="server">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/BasicInfoManagement.aspx" target="mainFrame">基本信息</a>
                    </td>
                </tr>
                <tr id="trSalesRegionManage" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/SalesRegionManage.aspx" target="mainFrame">销售区域维护</a>
                    </td>
                </tr>
                <tr id="trBasicDataMange" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/BasicDataManage.aspx" target="mainFrame">基础数据维护</a>
                    </td>
                </tr>
                <tr id="trRouteAreaType" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/RouteAreaType.aspx" target="mainFrame">线路区域分类</a>
                    </td>
                </tr>
                <tr id="trUniversalLineManage" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/UniversalLineManage.aspx" target="mainFrame">通用专线区域维护</a>
                    </td>
                </tr>
                <tr id="trDataCount" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/DataCount.aspx" target="mainFrame">统计数据维护</a>
                    </td>
                </tr>
                <tr id="trPartnersInfo" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/PartnersInfo.aspx" target="mainFrame">战略合作伙伴</a>
                    </td>
                </tr>
                <tr id="trLinksInfo" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="PlatformManagement/LinksInfo.aspx?linkType=1" target="mainFrame">友情链接</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divFeedManage" runat="server">
        <span class="linkcsstitle" id="Span1" onclick="javascript: setMenuTitle('spanFeedManage','FeedManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>用户反馈</strong></font>
                    </td>
                    <td width="22">
                        <img id="FeedManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanFeedManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr runat="server" id="trFeedHighShop">
                    <td width="1154">
                        <img id="Img3" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/FeedBack.aspx?type=1" target="mainFrame">高级网店反馈</a>
                    </td>
                </tr>
                <tr runat="server" id="trFeedMQ">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/FeedBack.aspx?type=2" target="mainFrame">MQ反馈</a>
                    </td>
                </tr>
                <tr runat="server" id="trFeedTonye114">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/FeedBack.aspx?type=3" target="mainFrame">同业114平台反馈</a>
                    </td>
                </tr>
                <tr runat="server" id="trFeedCompany">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/FeedBack.aspx?type=4" target="mainFrame">旅行社后台反馈</a>
                    </td>
                </tr>
                <tr runat="server" id="trFeedGuest">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/FeedBack.aspx?type=5" target="mainFrame">嘉宾申请反馈</a>
                    </td>
                </tr>
                <tr runat="server" id="trNetWorkMarketing">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/FeedbackManage/NetWorkMarketingList.aspx" target="mainFrame">网络营销反馈</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divAdvPlant" runat="server">
        <span class="linkcsstitle" id="SPAN2" onclick="javascript: setMenuTitle('spanAdvPlant','AdvPlant');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>广告机票</strong></font>
                    </td>
                    <td width="22">
                        <img id="AdvPlant" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle4">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanAdvPlant" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trAdvertiseTicket" runat="server">
                    <td width="1154">
                        <img id="Img5" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/AdvertiseTicket/Default.aspx" target="mainFrame">机票查询</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divAccountManage" runat="server">
        <span class="linkcsstitle" id="main7" onclick="javascript: setMenuTitle('spanAccountManage','AccountManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>账户</strong></font>
                    </td>
                    <td width="22">
                        <img id="AccountManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanAccountManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trAccountManage" runat="server">
                    <td width="1154">
                        <img id="sltm00" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/UserManage/UserListManage.aspx" target="mainFrame">子账号管理</a>
                    </td>
                </tr>
                <tr id="trPwdModi" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/UserManage/PasswordChange.aspx" target="mainFrame">修改密码</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divCustomerManage" runat="server">
        <span class="linkcsstitle" id="Span3" onclick="javascript: setMenuTitle('spanCustomerManage','CustomerManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>客户资料</strong></font>
                    </td>
                    <td width="22">
                        <img id="CustomerManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanCustomerManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trCustomerManage" runat="server">
                    <td width="1154">
                        <img id="Img7" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/CustomerManage/Default.aspx" target="mainFrame">客户资料管理</a>
                    </td>
                </tr>
                <tr id="trSysMaintenance" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/CustomerManage/SysMaintenance.aspx" target="mainFrame">系统维护</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divHotelManage" runat="server">
        <span class="linkcsstitle" id="Span4" onclick="javascript: setMenuTitle('spanHotelManage','HotelManage');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>酒店管理后台</strong></font>
                    </td>
                    <td width="22">
                        <img id="HotelManage" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanHotelManage" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trHotelOrderSear" runat="server">
                    <td width="1154">
                        <img id="Img8" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/HotelManagement/OrderSearch.aspx" target="mainFrame">订单查询</a>
                    </td>
                </tr>
                <tr id="trGroupOrder" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/HotelManagement/GroupOrderSearch.aspx" target="mainFrame">团队订单查询</a>
                    </td>
                </tr>
                <tr id="trHotelFirstAdd" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/HotelManagement/FirstPageDataAdd.aspx" target="mainFrame">首页板块数据添加</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divHotelHomePageManage" runat="server">
        <span class="linkcsstitle" id="Span5" onclick="javascript: setMenuTitle('spanHomePageHotel','ImgSpecial');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>酒店首页管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="ImgSpecial" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanHomePageHotel" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trHotelList" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/HotelHomePageManage/HotelManageList.aspx" target="mainFrame">特价酒店</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="divAirTickets" runat="server">
        <span class="linkcsstitle" id="Span7" onclick="javascript: setMenuTitle('spanAirTicket','ImgAirTicket');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr id="trAirTicketManage">
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>机票管理</strong></font>
                    </td>
                    <td width="22">
                        <img id="ImgAirTicket" src="<%=ImageServerUrl%>/images/yunying/button_down.gif" name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanAirTicket" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="trAirItem" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/AirTicktManage/AirTicketItemList.aspx" target="mainFrame">特价/免票/K位管理</a>
                    </td>
                </tr>
                <tr id="trTourTeamApply" runat="server">
                    <td width="1154">
                        <img id="Img12" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/AirTicktManage/TourTeamApplyList.aspx" target="mainFrame">团队票申请</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div class="leftmenu" id="div1" runat="server">
        <span class="linkcsstitle" id="Span6" onclick="javascript: setMenuTitle('spanTongyeCenter','ImgTongyeCenter');">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr id="tr2">
                    <td width="125" height="26" valign="center" nowrap="nowrap">
                        <font class="fonttitle">·<strong>同业中心后台</strong></font>
                    </td>
                    <td width="22">
                        <img id="ImgTongyeCenter" src="<%=ImageServerUrl%>/images/yunying/button_down.gif"
                            name="menutitle7">
                    </td>
                    <td width="23">
                    </td>
                </tr>
            </table>
        </span><span id="spanTongyeCenter" style="display: none">
            <table cellspacing="0" cellpadding="2" width="100%" align="left">
                <tr id="tr3" runat="server">
                    <td width="1154">
                        <img src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TongyeCenter/TongyeCenterManager.aspx" target="mainFrame">同业中心管理</a>
                    </td>
                </tr>
                <tr id="tr4" runat="server">
                    <td width="1154">
                        <img id="Img9" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TongyeCenter/TongyeNotice.aspx?NoticeClass=0" target="mainFrame">公告管理</a>
                    </td>
                </tr>
                <tr id="tr5" runat="server">
                    <td width="1154">
                        <img id="Img6" src="<%=ImageServerUrl%>/images/yunying/func_default.gif" name="SelectMenu" />
                        <a href="/TongyeCenter/TongyeNotice.aspx?NoticeClass=1" target="mainFrame">广播</a>
                    </td>
                </tr>
            </table>
        </span>
    </div>
    <div style="height: 30px;">
    </div>
</body>
</html>
