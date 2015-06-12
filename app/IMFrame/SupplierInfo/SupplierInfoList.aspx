<%--<%@ OutputCache Duration="1800" Location="Client" VaryByParam="ProductType"  %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierInfoList.aspx.cs"
    Inherits="IMFrame.SupplierInfo.SupplierInfoList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供求信息列表</title>
    <style type="text/css">
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体";
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        input, textarea, select
        {
            font-size: 12px;
            font-family: "宋体";
            color: #333;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
        }
        TD
        {
            font-size: 12px;
            color: #333;
            line-height: 20px;
        }
        sup
        {
            color: #5B8E12;
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
        .body
        {
            clear: both;
            margin: 0 auto;
            width: 970px;
        }
        /* CSS Document */h1
        {
            font-size: 18px;
        }
        h2
        {
            font-size: 16px;
        }
        h3
        {
            font-size: 14px;
        }
        ul
        {
            list-style: none outside none;
            margin: 0;
            padding: 0;
        }
        li
        {
            line-height: 150%;
            list-style-type: none;
            margin: 0;
            padding: 0;
        }
        a
        {
            color: #074387;
            text-decoration: none;
        }
        .hr-5
        {
            clear: both;
            height: 5px;
            margin: 0;
            padding: 0;
            overflow: hidden;
        }
        .box
        {
            top: 0;
            width: auto;
        }
        .box-l
        {
            background: url(    "<%=ImageServerUrl %>/IM/images/news-list_25.gif" ) no-repeat scroll left top transparent;
            height: 29px;
            padding-left: 3px;
        }
        .box-r
        {
            background: url(       "<%=ImageServerUrl %>/IM/images/news-list_26.gif" ) no-repeat scroll right top transparent;
            height: 29px;
            padding-right: 3px;
        }
        .box-c
        {
            background: url(       "<%=ImageServerUrl %>/IM/images/news-list_27.gif" ) repeat-x scroll left top transparent;
            height: 29px;
            line-height: 29px;
            padding-left: 3px;
            text-align: left;
            width: auto;
        }
        .box-c-se div.sel
        {
            float: left;
            height: 22px;
            line-height: 22px;
            overflow: hidden;
            padding-top: 5px;
            width: 170px;
        }
        .box-c-se div.sel a
        {
            display: block;
            float: left;
            height: 20px;
            margin-right: 2px;
            padding-left: 3px;
            padding-right: 3px;
            width: 25px;
        }
        .box-c-se div.sel a.current, .box-c-se div.sel a:hover
        {
            background: none repeat scroll 0 0 #EE8905;
            color: #FFFFFF;
        }
        .box-c-se div.form strong
        {
            background: url(       "<%=ImageServerUrl %>/IM/images/suplly_30.gif" ) no-repeat scroll left center transparent;
            padding-bottom: 2px;
            padding-left: 18px;
            padding-top: 2px;
        }
        input, textarea, select
        {
            color: #333333;
            font-family: "宋体";
            font-size: 12px;
        }
        .box-c-se div.form .sub
        {
            background: url(       "<%=ImageServerUrl %>/IM/images/chaxunannui2.gif" ) no-repeat scroll center top transparent;
            border: 0 none;
            cursor: pointer;
            height: 19px;
            width: 45px;
            _margin-top: 6px;
        }
        .box-main
        {
            border-top: 0 none;
            float: left;
            overflow: hidden;
            width: 100%;
        }
        #listContent
        {
            width: 100%;
        }
        #tagContent div.selectTag
        {
            display: block;
        }
        .tagContent
        {
            display: none;
            width: 99.7%;
        }
        #tagContent .tagContent ul li
        {
            border-bottom: 1px solid #DEDEDC;
            height: 42px;
            overflow: hidden;
            position: relative;
            margin: 0 auto;
            margin-left: 4px;
        }
        #tagContent .tagContent ul li.title
        {
            background: none repeat scroll 0 0 #F4F4F1;
            border: 1px solid #DEDEDC;
            height: 25px;
            width: 100%;
        }
        #tagContent .tagContent ul li div.title1
        {
            float: left;
            height: 42px;
            line-height: 53px;
            overflow: hidden;
            text-align: left;
            _padding-top: 13px;
        }
        #tagContent .tagContent ul li div.title2
        {
            height: 25px;
            line-height: 27px;
            _padding-top: 0;
            padding-left: 15px;
        }
        #tagContent .tagContent ul li div.title2 a
        {
            background: url(       "<%=ImageServerUrl %>/IM/images/suplly_18.gif" ) repeat-x scroll left top transparent;
            color: #FFFFFF;
            font-size: 12px;
            font-weight: normal;
            height: 20px;
            line-height: 20px;
            margin-left: 10px;
            padding: 4px 10px;
        }
        #tagContent .tagContent ul li div.publish
        {
            height: 42px;
            float: right;
            line-height: 18px;
            padding-top: 2px;
            text-align: center;
            width: 80px;
            position: absolute;
            right: 0;
        }
        #tagContent .tagContent ul li div.publish2
        {
            height: 25px;
            line-height: 25px;
        }
        #tagContent .tagContent ul li div.title1 span a.icon
        {
            position: relative;
            top: 0;
        }
        #tagContent .tagContent ul li div.title1 span a
        {
            color: #3366CC;
            font-size: 14px;
            position: relative;
            top: -4px;
        }
        #tagContent .tagContent ul li div.title1 span a.ji
        {
            color: #EC5000;
        }
        #tagContent .tagContent ul li div.publish a
        {
            clear: both;
            color: #3366CC;
            display: block;
        }
        #tagContent .tagContent ul li div.publish span
        {
            color: #828282;
        }
        /*FENYE*/div.digg
        {
            margin: 3px;
            margin-top: 10px;
            text-align: center;
        }
        div.digg A
        {
            color: #333;
            text-decoration: none;
            height: 23px;
            line-height: 26px;
            _padding-top: 3px;
            display: block;
            width: 350px;
            margin: 0 auto;
            background: url(<%=ImageServerUrl %>/IM/images/btnn.gif) no-repeat left center;
        }
        div.digg A:hover
        {
            text-decoration: none;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c box-c-se">
                    <div class="sel" id="TimeNav">
                        <a class="current" title="全部">全部</a> <a title="今天">今天</a> <a title="昨天">昨天</a> <a
                            title="前天">前天</a> <a title="更早">更早</a>
                    </div>
                    <div class="form">
                        <strong>查询：</strong>
                        <input type="text" onblur="if (this.value == '') {this.value = '请输入关键字';}" onfocus="if(this.value == '请输入关键字') {this.value = '';}"
                            value="请输入关键字" style="height: 16px; width: 140px; border: 1px solid #999; color: #999999"
                            name="textfield" />
                        <input type="submit" style="position: relative; top: 5px; top: 0;" value="" class="sub" />
                        <a target="_blank" href="http://www.tongye114.com/info">进入同业供求首页</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-main">
            <!--tabs切换导航 开始-->
            <div id="listContent">
                <!--*****************************内容列表 start***********************************-->
                <div class="hr-5">
                </div>
                <div id="tagContent">
                    <div id="tagContent0" class="tagContent selectTag">
                        <ul>
                            <!--注：如果是急急急，标题连接a标签添加调用class=“ji”，调用的图片为：suplly_60.gif
               如果是热：图片为：suplly_68.gif
               无：suplly_72.gif
               求：suplly_79.gif
               特价：suplly_83.gif
       -->
                            <!--信息列表循环输出() start-->
                            <%if (exList != null && exList.Count > 0)
                              {
                                  foreach (EyouSoft.Model.CommunityStructure.ExchangeList el in exList)
                                  { %>
                            <li>
                                <div class="title1">
                                    <span>
                                        <img src="<%= ImageServerUrl + (el.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供 ? "/images/news/tongyeMews_25.gif" : "/images/news/tongyeMews_28.gif")%>"
                                            alt="" /></span>
                                    <% =GetTagUrl(el.ExchangeTag, ImageServerUrl, CityId, 0)%>
                                    <span><a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(el.ID,CityId)%>" target="_blank" 
                                        <%=el.ExchangeTag == EyouSoft.Model.CommunityStructure.ExchangeTag.急急急?"class='ji'":"" %> >
                                        <%=el.ExchangeTitle%></a></span> <span>
                                            <%=EyouSoft.Common.Utils.GetMQ(el.OperatorMQ)%></span>
                                </div>
                                <div class="publish">
                                    <label>
                                        <%= el.OperatorName%></label><br />
                                    <span>
                                        <%=el.IssueTime.ToString("yyyy-MM-dd")%></span>
                                </div>
                            </li>
                            <%}
                              }
                              else
                              { 
                            %>
                            <li>暂无相关记录！</li>
                            <%}%>
                            <!--信息列表循环输出 end-->
                        </ul>
                        <%--<div class="digg" style="display: none">
                            <a href="<%=Domain.UserPublicCenter %>/info">当前为您显示40条，共有53200条，查阅更多，请点击本按钮&gt;&gt;</a></div>--%>
                        <!--分页 结束-->
                    </div>
                    <!--*****************************内容列表 end***********************************-->
                </div>
            </div>

            <script type="text/javascript">
            //同业中心编号
            var SuperID=<%=Request.QueryString["SuperID"]==null?"0":Request.QueryString["SuperID"]%>;
            //时间
            var Stime=<%=Request.QueryString["Stime"]==null?"0":Request.QueryString["Stime"]%>;
            var tag=document.getElementById("TimeNav").getElementsByTagName("a");
            for(var i=0;i<tag.length;i++){
                if(i==Stime){
                    tag[i].className="current";
                }else{
                    tag[i].className="";
                }
                tag[i].href="/SupplierInfo/SupplierInfoList.aspx?Stime="+i+"&SuperID="+SuperID;
            }
            
            </script>

        </div>
    </div>
    </form>
</body>
</html>
