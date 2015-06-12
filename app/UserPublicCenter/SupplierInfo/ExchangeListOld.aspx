<%@ Page Title="供求信息列表" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master" AutoEventWireup="true"
    CodeBehind="ExchangeListOld.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.ExchangeList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/ExchangeLeft.ascx" TagName="ExchangeLeft"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:ExchangeLeft runat="server" ID="ExchangeLeft1" />
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: url(images/bg_hotsale.gif) repeat-x;
                                            padding: 10px;">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="font-size: 14px; color: #CC3300; text-align: left; font-weight: bold;">
                                                                全部信息- 筛选条件
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="DateTypes" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding-left: 50px;">
                                                               <asp:Literal runat="server" ID="ltDateTypes"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Tags" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding-left: 50px;">
                                                                <asp:Literal runat="server" ID="ltTags"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Types" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding-left: 50px;">
                                                                <asp:Literal runat="server" ID="ltTypes"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Provinces" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding-left: 50px;">
                                                                省份：<asp:Literal runat="server" ID="ltProvinces"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="margin15" style="border-bottom: 2px solid #FF5500;">
                                            <tr>
                                                <td width="72%">
                                                    <div class="xianluon">
                                                        <strong><a href="#">全部信息</a></strong></div>
                                                    <div style="float: right; width: 250px;">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td align="right" valign="bottom">
                                                                    <strong>查询： </strong>
                                                                </td>
                                                                <td>
                                                                    <input runat="server" type="text" id="txt_keyword" style="height: 16px; width: 140px; border: 1px solid #999;
                                                                        color: #999999" value="请输入关键字" />
                                                                </td>
                                                                <td>
                                                                    <label id="Query"><img alt="查询" src="<%= ImageServerPath %>/images/UserPublicCenter/chaxunannui2.gif" width="45" height="19" /></label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                             
                                                <td width="28%" >
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="divExchangeList"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="digg" id="DivPage">
                                           
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>
    <script type="text/javascript">
        var LoadCount=0;
        $(function(){
            $("#Types").children().each(function() {
                $(this).click(function(){
                    SelectNode(this);
                });
            });
            $("#DateTypes").children().each(function() {
                 $(this).click(function(){
                    SelectNode(this);
                });
            });
            $("#Tags").children().each(function() {
                 $(this).click(function(){
                    SelectNode(this);
                });
            });
            $("#Provinces").children().each(function() {
                 $(this).click(function(){
                    SelectNode(this);
                });
            }); 
            $("#<%= txt_keyword.ClientID %>").blur(function(){
                if($.trim($(this).val())=="")
                {
                    $(this).val("请输入关键字");
                }
            }); 
            $("#<%= txt_keyword.ClientID %>").focus(function(){
                if($.trim($(this).val())=="请输入关键字")
                {
                    $(this).val("");
                }
            }); 
            $("#Query").click(function(){
                LoadExchangeList(1);
            });
            LoadExchangeList(1);
        });
        function SelectNode(obj)
        {
            var currType=$(obj).attr("value");
            $(obj).parent().children().each(function(){ 
                $(this).removeAttr("state");
                $(this).removeAttr("style");
                if($(this).attr("value")==currType)
                {
                    $(this).attr("state","1");
                    $(this).attr("style","background: #CCCCCC; padding: 3px;")
                }
                else
                {
                    $(this).attr("state","0");
                    $(this).attr("style","padding: 3px;");
                }
            });
             LoadExchangeList(1);
        }
        function LoadExchangeList(pageIndex) {
            var time = 0;
            var type = -1;
            var tag = -1;
            var pid = 0;
            var kw = '';
            var urlParams = '';
            var cityId="<%= base.CityId %>";
            $("#Types").children().each(function() {
                if ($(this).attr("state") == "1") {
                    type = $(this).attr("value");
                }
            });
            $("#DateTypes").children().each(function() {
                if ($(this).attr("state") == '1') {
                    time = $(this).attr("value");
                }
            });
            $("#Tags").children().each(function() {
                if ($(this).attr("state") == "1") {
                    tag = $(this).attr("value");
                }
            });
            $("#Provinces").children().each(function() {
                if ($(this).attr("state") == "1") {
                    pid = $(this).attr("value");
                }
            });     
            if($("#<%= txt_keyword.ClientID %>").val()!="请输入关键字")
            {
                kw=$("#<%= txt_keyword.ClientID %>").val();
            }
            urlParams = "cityid="+cityId+"&SearchType=1&time=" + time + "&type=" + type + "&tag=" + tag + "&pindex=" + pageIndex + "&pid=" + pid + "&kw=" + encodeURI(kw)+"&round="+Math.round(Math.random()*1000);
            if (urlParams != '') {
                $("#divExchangeList").html("<img id=\"img_loading\" src='\<%= ImageServerPath %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
                $.ajax({
                    type: "GET",
                    url: "/SupplierInfo/AjaxSupplierInfo.aspx",
                    data: urlParams,
                    async: false,
                    cache:false,
                    success: function(msg) {
                        //alert(msg);
                        $("#divExchangeList").html(msg);
                    }
                });
                if(LoadCount>0)
                {
                     scroll(0,300);
                }
                var config = {
                    pageSize: parseInt($("#hPageSize").val()),
                    pageIndex: parseInt($("#hPageIndex").val()),
                    recordCount: parseInt($("#hRecordCount").val()),
                    pageCount: 0,
                    gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                    showPrev: true,
                    showNext: true
                }
                AjaxPageControls.replace("DivPage", config);
                AjaxPageControls.gotoPage = function(pIndex) {
                    LoadExchangeList(pIndex);
                }
                LoadCount+=1;
            }
        }
    </script>
</asp:Content>
