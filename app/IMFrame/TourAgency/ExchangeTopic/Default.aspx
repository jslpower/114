<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMFrame.ExchangeTopic.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>同业互动</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style type="text/css">
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        .topbj
        {
            background: url(<%= ImageServerUrl %>/IM/images/mqhdbarbj.gif) repeat-x;
            height: 28px;
        }
        .topbj ul
        {
            margin: auto 0;
            padding: 0;
            width: 510px;
            display: block;
        }
        .topnav
        {
            padding: 1px 3px 0 3px;
        }
        .topnav2
        {
            padding: 8px 3px 0 4px;
        }
        .topbj ul li
        {
            display: inline;
            float: left;
        }
        .topnav a
        {
            background: url(<%= ImageServerUrl %>/IM/images/mqbaronl.gif) no-repeat left;
            float: left;
            margin: 0;
            padding: 0px 0px 0 3px;
            font-size: 14px;
            font-weight: bold;
            color: #ff0000;
        }
        .topnav span
        {
            display: block;
            background: url(<%= ImageServerUrl %>/IM/images/mqbaronr.gif) no-repeat right;
            padding: 8px 5px 4px 5px;
        }
        .topnav span
        {
            float: none;
        }
        a.white:link
        {
            color: #ffffff;
        }
        a.white:visited
        {
            color: #ffffff;
        }
        a.white:hover
        {
            color: #ffffff;
        }
        .hdbartable
        {
            background: #E9F6FC;
            color: #346B84;
            font-weight: bold;
            border-top: 1px solid #fff;
        }
        .hdbox span
        {
            color: #999999;
        }
        .hdbox a
        {
            color: #333333;
            line-height: 15px;
        }
        .hdbox a span
        {
            color: #333333;
        }
        .hdbox a.hds-1, a.hds-2, a.hds-3, a.hds-4
        {
            font-weight: bold;
        }
        .hdbox a.hds-1
        {
            color: #AE1C1C;
        }
        .hdbox a.hds-2
        {
            color: #118B2B;
        }
        .hdbox a.hds-3
        {
            color: #009DB9;
        }
        .hdbox a.hds-4
        {
            color: #042D77;
        }
        DIV.yahoo2
        {
            padding-right: 3px;
            padding-left: 3px;
            padding-bottom: 3px;
            margin: 0px;
            padding-top: 3px;
            text-align: center;
            width: 100%;
        }
        DIV.yahoo2 A
        {
            border: #ccdbe4 1px solid;
            padding-left: 4px;
            padding-right: 4px;
            background-position: 50% bottom;
            padding-bottom: 2px;
            margin-right: 3px;
            padding-top: 2px;
            text-decoration: none;
            text-align: center;
        }
        DIV.yahoo2 A:hover
        {
            border: #2b55af 1px solid;
            background-image: none;
            background-color: #4690B9;
            text-align: center;
        }
        DIV.yahoo2 A:active
        {
            border: #2b55af 1px solid;
            background-image: none;
            background-color: #4690B9;
            text-align: center;
        }
        DIV.yahoo2 SPAN.current
        {
            padding-right: 2px;
            padding-left: 2px;
            font-weight: bold;
            padding-bottom: 2px;
            color: #000;
            margin-right: 2px;
            padding-top: 2px;
            text-align: center;
        }
        DIV.yahoo2 SPAN.disabled
        {
            display: none;
        }
        DIV.yahoo2 A.next
        {
            border: #ccdbe4 2px solid;
            margin: 0px 0px 0px 5px;
        }
        DIV.yahoo2 A.next:hover
        {
            border: #2b55af 2px solid;
        }
        DIV.yahoo2 A.prev
        {
            border: #ccdbe4 2px solid;
            margin: 0px 5px 0px 0px;
        }
        DIV.yahoo2 A.prev:hover
        {
            border: #2b55af 2px solid;
        }
        .gqbiaoqian1
        {
            background: #FF7200;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
        .gqbiaoqian2
        {
            background: #FF0000;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
        .gqbiaoqian3
        {
            background: #C2A000;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
        .gqbiaoqian4
        {
            background: #006ABC;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
        .gqbiaoqian5
        {
            background: #B554BE;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
        .gqbiaoqian6
        {
            background: #17A600;
            color: #fff;
            padding: 2px 4px 2px 4px;
            float: left;
            line-height: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background: url(<%= ImageServerUrl %>/IM/images/mqhdbarbj.gif) repeat-x;
            text-align: center; line-height: 28px; font-size: larger; width: 100%; font-size: 14px;">
            <a target="_blank" href="<%= GetDesPlatformUrl(Domain.UserBackCenter+"/tongyeinfo/infolist.aspx") %>">
                <b>同业资讯</b></a>|<a target="_blank" href="<%= GetDesPlatformUrl(Domain.UserPublicCenter+"/Information/Default.aspx") %>"><b>行业动态</b></a>
        </div>
        <asp:Repeater runat="server" ID="RpInfos">
            <HeaderTemplate>
                <table width="100%" border="0">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        ·<a target="_blank" href="<%# GetDesPlatformUrl(Domain.UserBackCenter+"/tongyeinfo/infoshow.aspx?infoId="+Eval("NewId").ToString()) %>"><%# Utils.GetText2(Eval("Title").ToString(), 16, false)%></a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="background: url(<%= ImageServerUrl %>/IM/images/gqtitle.gif) no-repeat;
        width: 210px; height: 27px;">
        <tr>
            <td width="88" align="right" style="font-size: 14px; font-weight: bold">
                <a href="<%= GetDesPlatformUrl(Domain.UserPublicCenter+"/SupplierInfo/SupplierInfo.aspx") %>"
                    target="_blank">供求信息</a>
            </td>
            <td width="122" align="right">
                <a href="javascript:;" id="AddExchange" state="0" class="ff0000">快速发布供求</a>|<a target="_blank"
                    id="a_GL" runat="server">管理</a>&nbsp;
            </td>
        </tr>
    </table>
    <!--快速发布开始-->
    <table id="QuickExchang" width="208" border="0" align="center" cellpadding="0" cellspacing="0"
        style="margin-top: 3px; display: none;">
        <tr>
            <td align="left" colspan="2">
                <asp:DropDownList runat="server" ID="ddlTags">
                </asp:DropDownList>
                <asp:DropDownList runat="server" ID="ddlTypes">
                </asp:DropDownList>
                <br />
                <select name="dllCategory">
                    <option value="1">供</option>
                    <option value="2">求</option>
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox runat="server" TextMode="MultiLine" Style="overflow: hidden;" ID="txtContent"
                    MaxLength="250" Width="205" Height="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" id="TdProvinces" state="0" style="cursor: pointer;" colspan="2">
                点击此处将此信息发布到以下区域
            </td>
        </tr>
        <tr id="ProvinceList" style="display: none;">
            <td align="left" colspan="2">
                <asp:Repeater runat="server" ID="rptProvince">
                    <ItemTemplate>
                        <nobr><input id="ckb<%# Container.ItemIndex + 1 %>" name="ckbProvince" value="<%# Eval("ProvinceId") %>"
                type="checkbox"><label for="ckb<%# Container.ItemIndex + 1 %>"><%# Eval("ProvinceName") %></label></nobr>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td align="left" width="50%">
                <asp:Button runat="server" ID="btnSave" Text="快速发布" OnClick="btnSave_Click" />
                <td align="left" width="50%">
                    <div id="errinfo" style="color: Red; font-size: 12px;">
                    </div>
                </td>
            </td>
        </tr>
    </table>
    <!--快速发布结束-->
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
        <tr>
            <td align="left" background="<%= ImageServerUrl %>/IM/images/untitled.gif">
                搜索条件
            </td>
        </tr>
        <tr>
            <td align="left" id="DateTypes" style="border-bottom: 1px dashed #cccccc;">
                <asp:Literal runat="server" ID="ltDateTypes"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" id="Types" style="border-bottom: 1px dashed #cccccc;">
                <asp:Literal runat="server" ID="ltTypes"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="left" id="Tags" style="border-bottom: 1px dashed #cccccc;">
                <asp:Literal runat="server" ID="ltTags"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="hdbox">
        <%--<tr>
            <td id="DateTypes" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left;">
               <asp:Literal runat="server" ID="ltDateTypes"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td id="Tags" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left;">
                <asp:Literal runat="server" ID="ltTags"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td id="Provinces" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left;">
                省份：<asp:Literal runat="server" ID="ltProvinces"></asp:Literal>
            </td>
        </tr>--%>
        <tr>
            <td id="divExchangeList">
            </td>
        </tr>
        <tr>
            <td>
                <div class="yahoo2" id="DivPage">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

<script type="text/javascript">
    //      var Index=-1
    //      function MoveMenu(mode)
    //      {
    //        if(mode=="prev")
    //        {
    //            if(Index!=5) //始终要显示4个
    //            {
    //                $("#"+Index).removeAttr("class").hide().attr("class","topnav2");;
    //                if(Index==-1)
    //                {
    //                   Index=1; 
    //                }
    //                else
    //                {
    //                    Index+=1;
    //                }
    //                ChangeCss(Index)
    //                $("#"+(Index+3)).show();
    //                GetTopicList(1);
    //            }
    //            return false;
    //        }
    //        else
    //        {
    //            if(Index!=-1) //始终要显示4个
    //            {
    //                $("#"+(Index+3)).removeAttr("class").hide().attr("class","topnav2");
    //                Index-=1;
    //                if(Index==0)
    //                    Index=-1;
    //                $("#"+Index).show();
    //                ChangeCss(Index);
    //                GetTopicList(1);
    //            }
    //            return false;
    //        }
    //      }
    //      function ChangeCss(index)
    //      {
    //            $(".topbj").find("li.topnav").each(function(){
    //                         $(this).removeClass("topnav").addClass("topnav2");
    //                 });
    //            $("#" + index).removeClass("topnav2").addClass("topnav");
    //       }
    //       
    function SelectNode(obj) {
        var currType = $(obj).attr("value");
        var link = $(obj).attr("alink");
        $(obj).parent().children().each(function() {
            $(this).removeAttr("state");
            $(this).removeAttr("style");
            if ($(this).attr("value") == currType) {
                $(this).attr("state", "1");
                $(this).attr("style", "background: #CCCCCC; padding: 3px;");
                $(this).attr("href", "");
                $(this).attr("target", "_blank");
                $(this).attr("href", link);
            }
            else {
                $(this).attr("href", "javascript:;");
                $(this).attr("state", "0");
                $(this).attr("style", "padding: 3px;");
            }
        });
        //GetTopicList(1);
    }
    function GetTopicList(pageIndex) {
        /*var time = 0;
        var type = -1;
        var tag = -1;
        var pid = 0;
        var urlParams = '';
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
        $(".topbj").find("li.topnav").each(function(){
        type=$(this).attr("id");
        }); */
        //urlParams = "SearchType=1&time=" + time + "&type=" + type + "&tag=" + tag + "&pindex=" + pageIndex + "&pid=" + pid;
        urlParams = "pindex=" + pageIndex + "&pid=" + "<%= CityModel.ProvinceId %>";
        if (urlParams != '') {
            $("#divExchangeList").html("<img id=\"img_loading\" src='\<%= ImageServerUrl %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
            $.ajax({
                type: "GET",
                url: "/TourAgency/ExchangeTopic/AjaxSupplierInfo.aspx",
                data: urlParams,
                async: false,
                success: function(msg) {
                    $("#divExchangeList").html(msg);
                }
            });
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
                GetTopicList(pIndex);
            }
        }
    }


    $(document).ready(function() {
        $("#<%= btnSave.ClientID %>").click(function() {
            $("#errinfo").html("");
            var errmsg = "";
            if ($("#<%= ddlTypes.ClientID %>").val() == "-1") {
                errmsg += "请选择类别<br>";
            }
            if ($("#<%= ddlTags.ClientID %>").val() == "-1") {
                errmsg += "请选择标签<br>";
            }
            if ($.trim($("#<%= txtContent.ClientID %>").val()) == "") {
                errmsg += "请输入内容";
            }
            if (errmsg != "") {
                $("#errinfo").html(errmsg);
                return false;
            }
            else {
                $(this).hide();
                $(this).parent().append("<input type='button' value='正在发布...' disabled='disabled' />");
            }

        });

        $("#AddExchange").click(function() {
            if ($(this).attr("state") == "0") {
                $(this).attr("state", "1");
                $("#QuickExchang").css("display", "");
            }
            else {
                $(this).attr("state", "0");
                $("#QuickExchang").css("display", "none");
            }
            return false;
        });

        $("#TdProvinces").click(function() {
            if ($(this).attr("state") == "0") {
                $(this).attr("state", "1");
                $("#ProvinceList").css("display", "");
            }
            else {
                $(this).attr("state", "0");
                $("#ProvinceList").css("display", "none");
            }
            return false;
        });

        $("#DateTypes").children().each(function() {
            $(this).click(function() {
                SelectNode(this);
            });
        });
        $("#Tags").children().each(function() {
            $(this).click(function() {
                SelectNode(this);
            });
        });
        $("#Types").children().each(function() {
            $(this).click(function() {
                SelectNode(this);
            });
        });
        GetTopicList(1);
        //ChangeCss(-1);
    });
         
</script>

<span style="display: none;">

    <script language="JavaScript" src="http://s126.cnzz.com/stat.php?id=1125215&amp;web_id=1125215&amp;show=pic"
        charset="gb2312" type="text/javascript"></script>

    &nbsp;<img height="0" alt="" width="0" border="0" src="http://zs7.cnzz.com/stat.htm?id=1125215&amp;r=http%3A//www.tongye114.com/Register/CompanyUserRegister.aspx&amp;lg=zh-cn&amp;ntime=0.67142400 1283503185&amp;repeatip=576&amp;rtime=4&amp;cnzz_eid=84357694-1283137261-&amp;showp=1024x768&amp;st=48&amp;sin=http%3A//www.tongye114.com/Register/CompanyUserRegister.aspx&amp;res=0" />
</span>