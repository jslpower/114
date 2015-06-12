<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelManageList.aspx.cs"
    Inherits="SiteOperationsCenter.HotelHomePageManage.HotelManageList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特价酒店列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
        //***************
        //全选、反选
        //***************
        function ckAll(obj) {
            $("#tbl_HotelList").find("input[type='checkbox'][name='ckId']").each(function() {
                $(this).attr("checked", $(obj).attr("checked"));
            });
       }
            $(function() {
            //**************
            //选择性修改酒店
            //**************
            $("#a_Edit").click(function() {
                var CheckRow = $("#tbl_HotelList").find(":checkbox[name='ckId'][checked='true']").length;
                var ckId = $("#tbl_HotelList").find(":checkbox[name='ckId'][checked='true']:eq(0)").val();
                if (CheckRow == 0) {
                    alert("请选择您要修改的数据！");
                }
                else if (CheckRow > 1) {
                    alert("一次只能修改一条数据！");
                }
                else {
                    location.href = "/HotelHomePageManage/HotelInfoPage.aspx?EditId=" + ckId;
                }
                return false;
            });
            //****************************
            //选择性删除（可批量删除）酒店
            //****************************
            $("#<%= a_Del.ClientID %>").click(function() {
                var CheckRow = $("#tbl_HotelList").find(":checkbox[name='ckId'][checked='true']").length;
                var ckId = $("#tbl_HotelList").find(":checkbox[name='ckId'][checked='true']:eq(0)").val();
                if (CheckRow == 0) {                    
                    alert("请选择您要删除的数据！");
                }
                else {
                    if (confirm('您确定要删除这' + CheckRow + '条酒店信息吗？\n\n此操作不可恢复！')) {
                        return true;
                    }
                }
                return false;
            });
        })
        //****
        //样式
        //****
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        //********************
        //操作列(单个)删除请求
        //********************
        function DeleteHotelInfo(hotelId) {
            if (confirm('您确定要删除此酒店信息吗？\n\n此操作不可恢复！')) {
                window.location.href = "/HotelHomePageManage/HotelManageList.aspx?DeleteId=" + hotelId;
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <%--酒店开始--%>
    <div id="divHotelList" runat="server">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="30%" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="4%" align="right">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_da.gif" width="3"
                                    height="20" />
                            </td>
                            <td width="23%">
                                <a title="新增" id="a_Add" href="/HotelHomePageManage/HotelInfoPage.aspx">
                                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xinzeng.gif" width="50"
                                        height="25" border="0" /></a>
                            </td>
                            <td width="4%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                    height="25" />
                            </td>
                            <td width="24%">
                                <a href="javascript:;" title="修改" id="a_Edit">
                                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xiugai.gif" width="50"
                                        height="25" border="0" /></a>
                            </td>
                            <td width="5%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                    height="25" />
                            </td>
                            <td width="23%">
                                <asp:LinkButton runat="server" ID="a_Del" OnClick="a_Del_Click">
                                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                        height="25" />
                                </asp:LinkButton>
                            </td>
                            <td width="17%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_d.gif" width="11"
                                    height="25" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="70%" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif"
                    align="left">
                </td>
            </tr>
        </table>
        <cc1:CustomRepeater ID="crp_HotelList" runat="server">
            <HeaderTemplate>
                <table id="tbl_HotelList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1"
                    class="kuang">
                    <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                        class="white" height="23">
                        <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <input type="checkbox" name="SelectAll" onclick="ckAll(this)" /><strong>序号</strong>
                        </td>                       
                        <td width="27%" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>城市</strong>
                        </td>
                        <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>酒店名称</strong>
                        </td>
                        <td width="19%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>星级</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>门市价</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>团队价</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>操作</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">
                        <input type="checkbox" name="ckId" value="<%# Eval("Id") %>" /><%# ((PageIndex-1)*PageSize)+Container.ItemIndex+1 %>
                    </td>                   
                    <td height="25" align="center">
                        <%#Eval("CityName")%>
                    </td>
                    <td align="center">
                        <%#Eval("HotelName")%>
                    </td>
                    <td align="center">
                       <%#Eval("HotelStar")%>
                    </td>
                    <td width="15%" align="center">
                       <%#Eval("MenShiPrice", "{0:f2}")%>元
                    </td>
                    <td width="8%" align="center">
                        <%# Eval("TeamPrice", "{0:f2}")%>元
                    </td>
                    <td width="16%" align="center">
                        <a href='/HotelHomePageManage/HotelInfoPage.aspx?EditId=<%#Eval("Id") %>'>修改</a><strong>|</strong>
                        <a href='javascript:;' onclick="return DeleteHotelInfo('<%#Eval("Id") %>')">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                    class="white" height="23">
                    <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                     <%--   <input type="checkbox" name="SelectAll" />--%><strong>序号</strong>
                    </td>
                    <td width="27%" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>地区</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>酒店名称</strong>
                    </td>
                    <td width="19%" align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>星级</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>门市价</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>团队价</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>操作</strong>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </cc1:CustomRepeater>
        <table width="99%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30" align="right">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <%-- 酒店结束--%>
    </form>
</body>
</html>
