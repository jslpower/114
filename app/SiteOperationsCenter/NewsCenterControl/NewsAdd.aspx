<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAdd.aspx.cs" EnableEventValidation="false"
    Inherits="SiteOperationsCenter.NewsCenterControl.NewsAdd" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新闻中心新增修改管理页</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="UpdateTime" type="hidden" runat="server" />
    <table style="border: 1px solid rgb(204, 204, 204); padding: 1px;" border="1" cellpadding="0"
        cellspacing="0" width="980">
        <tbody>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    区域：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;" width="861">
                    <input id="ProvinceText" type="hidden" runat="server" />
                    <input id="CityText" type="hidden" runat="server" />
                    <input id="ProvinceId" type="hidden" runat="server" />
                    <input id="CityId" type="hidden" runat="server" />
                    <uc2:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    <span style="color: red">*</span>类别：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); text-align: left;
                    height: 28px">
                    <asp:DropDownList ID="ddlNewsClass" runat="server" title="请选择" valid="required" errmsg="请选择类型!">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    <span style="color: red">*</span>标题：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <input id="txtNewsTitle" name="txtNewsTitle" size="71" style="height: 24px; line-height: 24px;
                        font-size: 15px;" type="text" runat="server" valid="required" errmsg="请输入标题！" /><br />
                    <asp:CheckBoxList ID="chkRecPositionList" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                    <span id="drumpUrl" style="display: none;">
                        <input id="drumpUr2" name="drumpUrl" size="35" value="请输入跳转的网址" onfocus="if(this.value == '请输入跳转的网址') {this.value = '';}"
                            onblur="if (this.value == '') {this.value = '请输入跳转的网址';}" style="color: rgb(102, 102, 102);"
                            type="text" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    标题样色：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <select id="selTitleColor" name="selTitleColor" runat="server">
                        <option selected="selected" style="color: Black">请选择</option>
                        <option style="background: none repeat scroll 0% 0% rgb(255, 0, 0); color: Black"
                            value="#ff0000">红色</option>
                        <option style="background: none repeat scroll 0% 0% rgb(204, 0, 0); color: Black"
                            value="#cc0000">中国红</option>
                        <option style="background: none repeat scroll 0% 0% rgb(0, 204, 0); color: Black"
                            value="#00cc00">绿色</option>
                        <option style="background: none repeat scroll 0% 0% rgb(0, 51, 255); color: Black"
                            value="#0033ff">蓝色</option>
                        <option style="background: none repeat scroll 0% 0% rgb(255, 153, 0); color: Black"
                            value="#ff9900">黄色</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    关键字：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <input id="txtNewsKey" name="txtNewsKey" type="text" runat="server" value="多个用空格隔开"
                        onblur="if(this.value==''){this.value='多个用空格隔开'}" onfocus="if(this.value=='多个用空格隔开'){this.value=''}"
                        style="color: rgb(102, 102, 102);" />
                    <input id="txtNewsKeys" name="txtNewsKeys" type="text" runat="server" onclick="openDialog('1');"
                        readonly="readonly" />
                    <input id="hidNewsKeysId" type="hidden" runat="server" />
                    <label>
                        <input id="btnSelectKeys" type="button" value="选择" onclick="openDialog('1');" />
                    </label>
                    <span close="01">
                        <asp:CheckBox ID="chkSaveKeys" runat="server" Checked="true" Text="入库" />
                        【<a href="/NewsCenterControl/Keyword.aspx" target="_blank">关键字管理</a>】</span>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    Tag标签：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <input id="txtNewsTag" name="txtNewsTag" type="text" runat="server" value="多个用空格隔开"
                        onblur="if(this.value==''){this.value='多个用空格隔开'}" onfocus="if(this.value=='多个用空格隔开'){this.value=''}"
                        style="color: rgb(102, 102, 102);" />
                    <input id="txtNewsTags" name="txtNewsTags" type="text" runat="server" readonly="readonly"
                        onclick="openDialog('2');" />
                    <input id="hidNewsTagsId" type="hidden" runat="server" />
                    <label>
                        <input id="btnSelectTags" type="button" value="选择" onclick="openDialog('2');" />
                    </label>
                    <span close="01">
                        <asp:CheckBox ID="chkSaveTags" runat="server" Checked="true" Text="入库" />
                        【<a href="/NewsCenterControl/Tag.aspx" target="_blank">Tag管理</a>】</span>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    描述：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <textarea id="txtNewsDesc" name="txtNewsDesc" cols="80" rows="5" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    缩略图：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <font color="red">&nbsp; 参考大小*80像素</font>
                    <uc1:SingleFileUpload ID="SingleFileUpload1" runat="server" ImageHeight="200" ImageWidth="400"
                        SiteModule="新闻中心" />
                    <br />
                    <asp:HyperLink ID="lblImagePath" runat="server" Visible="false" Target="_blank">已上传图片，点击查看</asp:HyperLink>
                    <input id="ImagePath" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    附加信息：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    来 源：<input id="txtNewsSource" name="txtNewsSource" size="20" value="" type="text"
                        runat="server" />
                    作 者：<input id="txtNewsArticle" name="txtNewsArticle" size="20" value="" type="text"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    附加项：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <input id="addWater" name="addWater" type="checkbox" runat="server" />
                    <label for="addWater">
                        图片是否加水印</label>
                    <input id="DelLink" name="DelLink" type="checkbox" runat="server" />
                    <label for="DelLink">
                        删除非站内链接</label>
                    <input id="downloadRemotePic" name="downloadRemotePic" type="checkbox" runat="server" />
                    <label for="downloadRemotePic">
                        下载远程图片和资源</label>
                    <input id="addKey" name="addKey" type="checkbox" runat="server" />
                    <label for="addKey">
                        是否添加关键字内链</label>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    内容：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <font color="red">请手工添加分页符</font>
                    <FCKeditorV2:FCKeditor ID="FCK_PlanTicketContent" ToolbarSet="Default" Height="420px"
                        runat="server">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td style="background: none repeat scroll 0% 0% rgb(192, 222, 243); text-align: right;
                    font-weight: bold;">
                    文章排序：
                </td>
                <td style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 28px;
                    text-align: left;">
                    <asp:RadioButtonList ID="radSortList" runat="server" RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="25%" height="30">
        <tbody>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" class="baocun_an" runat="server" Text="发 布" OnClick="btnSave_Click" />
                </td>
                <td align="center">
                </td>
                <td align="center">
                    <input name="btnCancel" type="button" class="baocun_an" value="返 回" onclick="window.location.href='/NewsCenterControl/NewsList.aspx'" />
                </td>
            </tr>
        </tbody>
    </table>
    </form>

    <script type="text/javascript">
    
        var sfu1 = <%=SingleFileUpload1.ClientID %>;
        var isSubmit = false; //区分按钮是否提交过
        //模拟一个提交按钮事件
        function doSubmit() {
            isSubmit = true;
            $("#<%=btnSave.ClientID%>").click();
        }

        $(function() {

            $("#<%=btnSave.ClientID%>").click(function() {
                //选中的省份名称
                $("#ProvinceText").val($(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].provinceId)).find("option:selected").text());
                //选中的省份编号
                $("#ProvinceId").val($(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].provinceId)).val());
                //选中的城市名称
                $("#CityText").val($(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].cityId)).find("option:selected").text());
                //选中的城市编号
                $("#CityId").val($(("#" + provinceAndCityUserControl["<%=ProvinceAndCityList1.ClientID%>"].cityId)).val());
                if (isSubmit) {
                    //如果按钮已经提交过一次验证，则返回执行保存操作
                    return true;
                }
                var a = ValiDatorForm.validator($("#form1").get(0), "alert");
                if (a) {
                    //如果验证成功，则提交按钮保存事件
                    if (sfu1.getStats().files_queued <= 0) {
                        return true;
                    }
                    sfu1.customSettings.UploadSucessCallback = doSubmit;
                    sfu1.startUpload();
                    return false;
                }
                return false;
            });
            FV_onBlur.initValid($("#form1").get(0));
            var obj = $("#<%=drumpUr2.ClientID%>");
            if (obj.val() != "" && obj.val() != "请输入跳转的网址") {
                obj.parent().attr("style", "display: inline;");
            }

            $("#<%=addWater.ClientID%>").click(function() {
                if ($(this).attr("checked") == true) {
                    $("#<%=downloadRemotePic.ClientID%>").attr("checked", true);
                }
            });

            $("#<%=downloadRemotePic.ClientID%>").click(function() {
                if ($(this).attr("checked") == false) {
                    $("#<%=addWater.ClientID%>").attr("checked", false);
                }
            });

            var TitleColorObj = $("#<%=selTitleColor.ClientID%>");
            TitleColorObj.attr("style", "color:" + TitleColorObj.val());
            TitleColorObj.change(function() {
                $(this).attr("style", "color:" + $(this).val());
            });
        });
        function openDialog(type) {
            var keyName = "";
            var keyId = "";
            var title = "";
            if (type == '1') {
                keyName = "txtNewsKeys";
                keyId = "hidNewsKeysId";
                title = "选择关键字";
            }
            if (type == '2') {
                keyName = "txtNewsTags";
                keyId = "hidNewsTagsId";
                title = "选择Tag标签";
            }
            Boxy.iframeDialog({
                title: title,
                iframeUrl: "/NewsCenterControl/SelectKeys.aspx",
                width: "700px",
                modal: true,
                height: "450px",
                data: {
                    isAjax:"false",
                    keyOrTags: type,
                    keyName: keyName,
                    keyId: keyId
                }
            });
        };

        function showInput() {
            if (document.getElementById("drumpUrl").style.display == "none") {
                document.getElementById("drumpUrl").style.display = "inline";
            }
            else {
                document.getElementById("drumpUrl").style.display = "none";
            }
        }
    </script>

</body>
</html>
