<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.NewsList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border: 1px solid rgb(204, 204, 204);" width="100%" border="1" cellpadding="0"
            cellspacing="0">
            <tbody>
                <tr style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 24px;
                    text-align: center;">
                    <td colspan="9">
                        <uc2:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                        标题：<input name="txtNewTitle" id="txtNewTitle" type="text" runat="server" />
                        <select id="selNewClass" runat="server">
                        </select>
                        <asp:CheckBox ID="chkTueiJian" runat="server" Text="推荐" />
                        <select id="selZhidin" runat="server">
                        </select>
                        <input id="btnSearch" type="button" value="搜索" />
                        <a target="_blank" title="注意：切勿频繁点击！" href="<%= Domain.UserPublicCenter %>/Information/clearcache.aspx">更新前台页面缓存</a>
                    </td>
                </tr>
                <tr style="background: none repeat scroll 0% 0% rgb(192, 222, 243); height: 28px;
                    text-align: center; font-weight: bold;">
                    <td>
                        序号
                        <%--<input id="chkSelAll" type="checkbox" />--%>
                    </td>
                    <td>
                        地区
                    </td>
                    <td>
                        类目
                    </td>
                    <td>
                        标题
                    </td>
                    <td>
                        发布时间
                    </td>
                    <td>
                        加推荐
                    </td>
                    <td>
                        加置顶
                    </td>
                    <td>
                        操作人
                    </td>
                    <td>
                        操作
                    </td>
                </tr>
                <cc1:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr bgcolor="<%#Container.ItemIndex%2==0?"#f3f7ff":"white" %>" onmouseover="NewsList.mouseovertr(this);" onmouseout="NewsList.mouseouttr(this)" style="height: 24px;text-align: center;">
                            <td width="40px">
                                <%#Container.ItemIndex+1 %>
                                <%--<input name="chkSel" type="checkbox" />--%>
                            </td>
                            <td>
                                <%#Eval("ProvinceName")%>-<%#Eval("CityName")%>
                            </td>
                            <td>
                                <%#Eval("ClassName")%>
                            </td>
                            <td align="left">
                                <a href="NewsAdd.aspx?id=<%#Eval("Id")%>">
                                    <%#Eval("AfficheTitle")!=null?Utils.GetText2(Eval("AfficheTitle").ToString(),20,true):""%></a>
                                <a href="<%#ReWrite(Eval("RecPositionId"),Eval("AfficheClass"),Eval("Id"),Eval("CityId"),Eval("GotoUrl"))%>" target="_blank">【浏览】</a>&nbsp;&nbsp;
                                <%#Eval("Clicks")%>
                            </td>
                            <td>
                                <%#Eval("IssueTime","{0:MM/dd HH:mm}")%>
                            </td>
                            <td>
                                <%#RecPositionList(Eval("RecPositionId"))%>
                            </td>
                            <td>
                                <%#Eval("AfficheSort")%>
                            </td>
                            <td>
                                <%#Eval("OperatorName")%>
                            </td>
                            <td width="9%">
                                <%if (EditFlag)
                                  {
                                %>
                                <a href="NewsAdd.aspx?id=<%#Eval("Id")%>">修改</a>
                                <%
                                    }%>
                                <%if (DeleteFlag)
                                  {
                                %>
                                <a href="javascript:" onclick="if(confirm('确认删除该新闻!')){NewsList.DelNew(<%#Eval("Id")%>);return false;}">
                                    删除</a>
                                <%
                                    }%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc1:CustomRepeater>
                <tr>
                    <td colspan="9" align="right">
                        <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        var NewsList = {

            OnSearch: function() {
                //省份
                var ProId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].provinceId)).val();
                //城市
                var CityId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].cityId)).val();
                //标题
                var Title = $.trim($("#txtNewTitle").val());
                //类别编号
                var ClassId = $.trim($("#<%=selNewClass.ClientID %>").val());
                //置顶
                var selZhidin = $.trim($("#selZhidin").val());
                //推荐
                var chkTueiJian = $("#chkTueiJian").attr("checked");
                var Params = { Title: Title, ClassId: ClassId, ProId: ProId, CityId: CityId, selZhidin: selZhidin, chkTueiJian: chkTueiJian };
                window.location.href = "/NewsCenterControl/NewsList.aspx?" + $.param(Params);
            },

            DelNew: function(Id) {
                //省份
                var ProId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].provinceId)).val();
                //城市
                var CityId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].cityId)).val();
                //标题
                var Title = $.trim($("#txtNewTitle").val());
                //类别编号
                var ClassId = $.trim($("#<%=selNewClass.ClientID %>").val());
                //置顶
                var selZhidin = $.trim($("#selZhidin").val());
                //推荐
                var chkTueiJian = $("#chkTueiJian").attr("checked");
                var Params = { Id: Id, State: "Del", Title: Title, ClassId: ClassId, ProId: ProId, CityId: CityId, selZhidin: selZhidin, chkTueiJian: chkTueiJian };
                window.location.href = "/NewsCenterControl/NewsList.aspx?" + $.param(Params);
            },
          //鼠标选中后的背景样式
          mouseovertr: function(o){
	            o.style.backgroundColor="#FFF9E7";
          },
          mouseouttr: function(o){
	            o.style.backgroundColor="";
          }
        };
        $(document).ready(function() {

            var FormObj = $("#<%=form1.ClientID%>");
            $("#txtNewTitle").focus();
            //查询按钮
            $("#btnSearch").click(function() {
                NewsList.OnSearch();
                return false;
            });

            //全选
            //            $("#chkSelAll").click(function() {
            //                var isChecked = $("#chkSelAll").attr("checked");
            //                $("input[name=chkSel]").each(function() {
            //                    $(this).attr("checked", isChecked);
            //                });
            //            });

            //回车查询
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    NewsList.OnSearch();
                    return false;
                }
            });
        });
    </script>

    </form>
</body>
</html>
