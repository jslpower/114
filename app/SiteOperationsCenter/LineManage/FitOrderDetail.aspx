<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FitOrderDetail.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.FitOption" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>订单查看</title>
    <%@ register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>
    <%@ register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/JavaScript">
<!--
function ViewAllCarPlan2()
{
	new Controls.Dialog('../../zx/fwbz/daoru_md.html', '导入名单', {width:800, height:395, minmize:'no',maximize:"yes", scrollbars: 'no',closebtn: 'yes'});
}

//-->
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="15%" align="right" bgcolor="#f2f9fe">
                团号：
            </td>
            <td width="85%" align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.TourNo %>
            </td>
        </tr>
        <tr>
            <td width="15%" align="right" bgcolor="#f2f9fe">
                线路名称：
            </td>
            <td width="85%" align="left" bgcolor="#FFFFFF">
                <a href="#">
                    <%=modelMTourOrder.RouteName %></a> 出发城市：<%=GetLineModel(modelMTourOrder.RouteId)%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                组团社：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%--                <%=modelMTourOrder.TravelName %>
                组团社联系人：<%=modelMTourOrder.TravelContact%>
                手机：<%=modelMTourOrder.TravelTel%><br />--%>
                <%=GetContact(modelMTourOrder.Travel, "组团社")%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                专线商：
            </td>
            <td colspan="3" align="left" bgcolor="#FFFFFF">
                <%=GetContact(modelMTourOrder.Publishers, "专线商")%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发日期：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.LeaveDate.ToShortDateString()%>
                <b>剩余空位：<%=modelMTourOrder.MoreThan%></b> 【<a href="UpdateScatteredFightPlan.aspx?tourId=<%=modelMTourOrder.TourId %>">团队出团信息确定修改</a>】
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发班次时间：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.StartDate %>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                返回班次时间：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.EndDate %>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                领队全陪说明：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.TeamLeaderDec %>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                集合说明：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.SetDec %>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                游客联系人：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <span sizcache="0" sizset="0" jquery1309744253156="18"><span sizcache="0" sizset="0"
                    jquery1309744253156="17">
                    <%=modelMTourOrder.VisitorContact%>
                </span></span>联系电话：<%=modelMTourOrder.VisitorTel%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                游客备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.VisitorNotes%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                组团社备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.TravelNotes%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                专线商备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourOrder.BusinessNotes%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                订单状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=strOrderStatus%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                支付状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=strPaymentStatus%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                价格组成：
            </td>
            <td colspan="3" align="left" bgcolor="#FFFFFF">
                成人<%=modelMTourOrder.AdultNum%>人，儿童<%=modelMTourOrder.ChildrenNum%>人 ，单房差数<%=modelMTourOrder.SingleRoomNum%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                价格：
            </td>
            <td colspan="3" align="left" bgcolor="#FFFFFF">
                <table width="90%" border="0">
                    <tr>
                        <td scope="row">
                            市场价：
                        </td>
                        <td>
                            成人
                            <%=modelMTourOrder.PersonalPrice.ToString("0.00")%>
                        </td>
                        <td>
                            儿童
                            <%=modelMTourOrder.ChildPrice.ToString("0.00")%>
                        </td>
                        <td>
                            增减销售价
                            <%=modelMTourOrder.Add.ToString("0.00")%>
                        </td>
                        <td>
                            单房差
                            <%=modelMTourOrder.MarketPrice.ToString("0.00")%>
                        </td>
                    </tr>
                    <tr>
                        <td scope="row">
                            结算价：
                        </td>
                        <td>
                            成人
                            <%=modelMTourOrder.SettlementAudltPrice.ToString("0.00")%>
                        </td>
                        <td>
                            儿童
                            <%=modelMTourOrder.SettlementChildrenPrice.ToString("0.00")%>
                        </td>
                        <td>
                            增减结算价
                            <%=modelMTourOrder.Reduction.ToString("0.00")%>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                总金额：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                销售价
                <%=modelMTourOrder.TotalSalePrice.ToString("0.00")%>
                &nbsp; 结算价
                <%=modelMTourOrder.TotalSettlementPrice.ToString("0.00")%>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                批发商操作：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:Button ID="btnsave" runat="server" Text="保存修改" OnClick="btnsave_Click" />
                <asp:Button ID="btndelete" runat="server" Text="删除无效订单" OnClick="btndelete_Click" />
            </td>
        </tr>
        <!--  <tr>
    <td colspan="2" align="left" bgcolor="#FFFFFF"><b>游客详细信息</b><span class="daorumd"><a href="javascript:ViewAllCarPlan2();">导入名单</a></span></td>
  </tr>
-->
        <div id="divFitCustomes">
            <cc1:CustomRepeater ID="repList" runat="server">
                <HeaderTemplate>
                    <tr>
                        <td colspan="2" align="center" bgcolor="#FFFFFF">
                            <table width="100%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc">
                                <tr>
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        姓名
                                    </th>
                                    <th>
                                        联系电话
                                    </th>
                                    <th>
                                        身份证
                                    </th>
                                    <th>
                                        护照
                                    </th>
                                    <th>
                                        其他证件
                                    </th>
                                    <th>
                                        性别
                                    </th>
                                    <th>
                                        类型
                                    </th>
                                    <th>
                                        座号
                                    </th>
                                    <th>
                                        备注
                                    </th>
                                </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <td align="center">
                        <%# Container.ItemIndex+1%>
                    </td>
                    <td align="center">
                        <%# Eval("VisitorName")%>
                    </td>
                    <td align="center">
                        <%# Eval("ContactTel")%>
                    </td>
                    <td align="center">
                        <%# Eval("IdentityCard")%>
                    </td>
                    <td align="center">
                        <%# Eval("Passport")%>
                    </td>
                    <td align="center">
                        <%# Eval("OtherCard")%>
                    </td>
                    <td align="center">
                        <%# Eval("Sex")%>
                    </td>
                    <td align="center">
                        <%# Eval("CradType")%>
                    </td>
                    <td align="center">
                        <%# Eval("SiteNo")%>
                    </td>
                    <td align="center">
                        <%# Eval("Notes")%>
                    </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table> </td> </tr>
                </FooterTemplate>
            </cc1:CustomRepeater>
            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="30" align="right">
                        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript">
    
    var FitCustomesManage = {//景区列表
            GetFitCustomesList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divFitCustomes");
                if (LoadingImg.IsLoadAddDataToDiv("divFitCustomes")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "FitOrderDetail.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divFitCustomes").html(html);
                        }
                    });
                }
            },
            
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetFitList();
            }
    }
    
    </script>

    </form>
</body>
</html>
