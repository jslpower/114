<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelInfoPage.aspx.cs"
    Inherits="SiteOperationsCenter.HotelHomePageManage.HotelInfoPage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>酒店管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>


    <script type="text/javascript">
        $(function() {
            $("#<%=ddlAreaList.ClientID %>").change(function() {
                var areaType = $(this).find("option:selected").attr("AreaType");
                $("#<%=hCityAreaId.ClientID %>").val(areaType);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
            <tr>
                <td width="17%" align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>地区：
                </td>
                <td width="83%" align="left" bgcolor="#F7FBFF">
                    <input type="hidden" name="hCityAreaId" id="hCityAreaId" value="-1" runat="server" />
                    <asp:DropDownList ID="ddlAreaList" runat="server" valid="required" errmsg="请选择地区！">
                        <asp:ListItem Text="请选择" Value="" AreaType="-1"></asp:ListItem>
                        <asp:ListItem Text="上海" Value="上海" AreaType="0"></asp:ListItem>
                        <asp:ListItem Text="杭州" Value="杭州" AreaType="0"></asp:ListItem>
                        <asp:ListItem Text="苏州" Value="苏州" AreaType="0"></asp:ListItem>
                        <asp:ListItem Text="南京" Value="南京" AreaType="0"></asp:ListItem>
                        <asp:ListItem Text="无锡" Value="无锡" AreaType="0"></asp:ListItem>
                        <asp:ListItem Text="香港" Value="香港" AreaType="1"></asp:ListItem>
                        <asp:ListItem Text="澳门" Value="澳门" AreaType="1"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="errMsg_ddlAreaList" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>酒店名称：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" id="txt_HotelName" runat="server" size="50" valid="required" errmsg="请填写酒店名称！" />
                    <span id="errMsg_txt_HotelName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>星级：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:DropDownList ID="ddlHotelRank" runat="server" valid="required" errmsg="请选择星级！">
                    </asp:DropDownList>
                    <span id="errMsg_ddlHotelRank" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span><span>门市价</span>：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" name="txt_SalesRoomPrice" id="txt_SalesRoomPrice" runat="server"
                        valid="required" errmsg="请填写门市价！" />元 <span id="errMsg_txt_SalesRoomPrice" class="errmsg">
                        </span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span><span>团队价</span>：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" name="txt_TourTeamPrice" id="txt_TourTeamPrice" runat="server"
                        valid="required" errmsg="请填写团队价！" />元 <span id="errMsg_txt_TourTeamPrice" class="errmsg">
                        </span>
                </td>
            </tr>
            <tr>
            <td align="right" bgcolor="#D7E9FF"><span style=" color:Red;">*</span>QQ：</td>
            <td align="left" bgcolor="#F7FBFF">
                <input id="txtQQ" name="txtQQ" valid="required|isQQ" runat="server" errMsg="请输入QQ|请输入正确的QQ" type="text"/>
                <span id="errMsg_txtQQ" class="errmsg"></span>
            </td>
          </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>发布时间：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:Label ID="txt_OperateDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:Button runat="server" ID="btnSave" Text="提交" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        //********
        //表单验证
        //********
        $(document).ready(function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                return ValiDatorForm.validator($(this).closest("form").get(0), "span");
            });
            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
        });
    </script>

</body>
</html>
