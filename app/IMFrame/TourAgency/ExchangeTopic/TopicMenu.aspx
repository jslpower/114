<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicMenu.aspx.cs" Inherits="TourUnion.WEB.IM.TourAgency.ExchangeTopic.TopicMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
     <script type="text/javascript" src="/js/urlutility.js"></script>
    <script language="javascript" src="/Js/ext/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
    function ChangeCss(obj)
    {
     $(".boxr").find("a.hot").each(function(){
             $(this).removeClass("hot");
        });
        $(obj).addClass("hot");
    }
    function GetUrl(Id)
    {
        var urlParms = UrlUtility.getUrlParms([]);
        var Parms=window.location.protocol + "//" + window.location.host + "/IM/AdvLoginToBs.aspx";
        Parms=Parms+"?<%= mqidurlpar %>=" + urlParms.im_username;
        Parms=Parms+"&CsToBsRedirectUrl=/TourAgency/ExchangeTopic/Default.aspx?ClassId="+Id;
        window.open(Parms);
    }
    </script>
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
        .box
        {
            border: 1px solid #D3741F;
            background: #FEC669;
            width: 210px;
            height: 47px;
        }
        .boxl
        {
            width: 45px;
            height: 45px;
            float: left;
        }
        .boxr
        {
            width: 153px;
            height: 35px;
            padding: 5px;
            float: left;
            background: url(/IM/images/mqindexadr.gif) repeat-x;
            color: #999999;
        }
        .boxr a
        {
            color: #5F3207;
            font-size: 14px;
            line-height: 17px;
        }
        .boxr a.hot
        {
            color: #f00;
            font-weight: bold;
        }
    </style>
</head>
<body oncontextmenu="return false;" scroll="no">
    <form id="form1" runat="server">
    <div class="box">
        <div style="border: 1px solid #FFF2BF; width: 210px; height: 45px;">
            <div class="boxl">
                <a href="javascript:void(0);" onclick="GetUrl('-1');">
                    <img src="/IM/images/mqindexadl.gif" border="0"  /></a></div>
            <div class="boxr">
                <a  id="0" onclick="ChangeCss(this);GetUrl('0');"  href="javascript:void(0);"  class="hot">热 门</a>|<%=strAllClass%></div>
        </div>
    </div>
    </form>
</body>
</html>
